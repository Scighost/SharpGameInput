using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SharpGameInput.TestApp;

internal class RawReportTest
{
    public static void Run(IGameInput gameInput)
    {
        ConsoleUtility.WriteMenuHeader("Read Raw Reports");

        (string name, Action<IGameInput> func)[] subTests =
        [
            ("Direct Polling", Direct),
            ("Polling with Device Callback", WithCallbacks),
        ];

        int choice = ConsoleUtility.PromptChoice("Select a sub-test", subTests.Select((i) => i.name));
        if (choice < 0)
            return;

        subTests[choice].func(gameInput);
    }

    public static void Direct(IGameInput gameInput)
    {
        Console.WriteLine("Press any key to stop the test.");

        ulong lastTimestamp = 0;
        for (; !Console.KeyAvailable; Thread.Sleep(1))
        {
            PollAndPrintReport(gameInput, null, ref lastTimestamp);
        }

        // Consume keypress
        Console.ReadKey(intercept: true);
    }

    public static void WithCallbacks(IGameInput gameInput)
    {
        var deviceThreads = new Dictionary<IGameInputDevice, (Thread thread, EventWaitHandle stopHandle)>();

        if (!gameInput.RegisterDeviceCallback(
            null,
            GameInputKind.RawDeviceReport,
            GameInputDeviceStatus.Connected,
            GameInputEnumerationKind.AsyncEnumeration,
            null,
            (token, context, _device, timestamp, currentStatus, previousStatus) =>
            {
                var device = _device.ToSafeHandle();
                if ((currentStatus & GameInputDeviceStatus.Connected) != 0)
                {
                    device = device.Duplicate();

                    ref readonly var info = ref device.DeviceInfo;
                    Console.WriteLine($"Device {info.deviceId} connected.");

                    var stopHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
                    var thread = new Thread(() =>
                    {
                        using (device)
                        {
                            ulong lastTimestamp = 0;
                            while (!stopHandle.WaitOne(0) && PollAndPrintReport(gameInput, device, ref lastTimestamp));
                        }
                    });
                    thread.Start();
                    deviceThreads.Add(device, (thread, stopHandle));
                }
                else
                {
                    if (!deviceThreads.Remove(device, out var read))
                        return;

                    read.stopHandle.Set();
                    read.thread.Join();
                }
            },
            out var callbackToken,
            out int result
        ))
        {
            ConsoleUtility.WritePInvokeError("Failed to register device callback", result);
            return;
        }

        ConsoleUtility.WaitForKey("Press any key to stop the test.");

        foreach (var (thread, stopHandle) in deviceThreads.Values)
        {
            stopHandle.Set();
            thread.Join();
        }

        deviceThreads.Clear();
    }

    private static bool PollAndPrintReport(IGameInput gameInput, IGameInputDevice? device, ref ulong lastTimestamp)
    {
        int result = gameInput.GetCurrentReading(GameInputKind.RawDeviceReport, device, out var reading);
        if (result < 0)
        {
            if (result == (int)GameInputResult.ReadingNotFound)
            {
                return true;
            }
            else if (result == (int)GameInputResult.DeviceDisconnected)
            {
                if (device != null)
                {
                    ref readonly var info = ref device.DeviceInfo;
                    Console.WriteLine($"Device {info.deviceId} disconnected.");
                }
                return false;
            }
            else
            {
                if (device != null)
                {
                    ref readonly var info = ref device.DeviceInfo;
                    ConsoleUtility.WritePInvokeError($"Failed to get current reading for device {info.deviceId}", result);
                }
                else
                {
                    ConsoleUtility.WritePInvokeError("Failed to get current reading", result);
                }
                return false;
            }
        }

        using (reading)
        {
            // Ignore unchanged reports
            ulong timestamp = reading.GetTimestamp();
            if (lastTimestamp == timestamp)
                return true;
            lastTimestamp = timestamp;

            Program.PrintTimestamp(timestamp);
            Console.Write(": ");
            PrintRawReport(reading);
        }

        return true;
    }

    public static void PrintRawReport(LightIGameInputReading reading)
    {
        if (!reading.GetRawReport(out var rawReport))
        {
            Console.WriteLine("Could not get raw report!");
            return;
        }

        using (rawReport)
        {
            uint reportId = rawReport.ReportInfo.id;
            nuint size = rawReport.GetRawDataSize();
            Console.Write("Report ID: ");
            Console.Write(reportId);
            Console.Write(", size: ");
            Console.Write(size);
            Console.Write(", ");

            Span<byte> buffer = stackalloc byte[(int)size];
            nuint readSize = rawReport.GetRawData(buffer);
            Debug.Assert(size == readSize);

            ConsoleUtility.WriteLine(buffer);
        }
    }
}