using System;
using System.Diagnostics;
using System.Threading;

namespace SharpGameInput.TestApp;

internal class RawReportTest
{
    public static void Run(IGameInput gameInput)
    {
        ConsoleUtility.WriteMenuHeader("Read Raw Reports");

        ulong lastTimestamp = 0;
        for (; !Console.KeyAvailable; Thread.Sleep(1))
        {
            // Poll for raw reports
            int result = gameInput.GetCurrentReading(GameInputKind.RawDeviceReport, null, out var reading);
            if (result < 0)
            {
                if (result != (int)GameInputResult.ReadingNotFound)
                    ConsoleUtility.WritePInvokeError("Failed to get current reading", result);
                continue;
            }

            using (reading)
            {
                // Ignore unchanged reports
                ulong timestamp = reading.GetTimestamp();
                if (lastTimestamp == timestamp)
                    continue;
                lastTimestamp = timestamp;

                Program.PrintTimestamp(timestamp);
                Console.Write(": ");
                PrintRawReport(reading);
            }
        }

        // Consume keypress
        Console.ReadKey(intercept: true);
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