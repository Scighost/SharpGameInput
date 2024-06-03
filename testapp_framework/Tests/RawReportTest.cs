using System;
using System.Diagnostics;
using System.Threading;

namespace SharpGameInput.TestApp
{
    internal partial class RawReportTest
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

                try
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
                finally
                {
                    reading.Dispose();
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

            try
            {
                uint reportId = rawReport.ReportInfo.id;
                ulong size = rawReport.GetRawDataSize();
                Console.Write("Report ID: ");
                Console.Write(reportId);
                Console.Write(", size: ");
                Console.Write(size);
                Console.Write(", ");

                Span<byte> buffer = stackalloc byte[(int)size];
                unsafe
                {
                    fixed (byte* ptr = buffer)
                    {
                        ulong readSize = rawReport.GetRawData((UIntPtr)buffer.Length, ptr);
                        Debug.Assert(size == readSize);
                    }
                }

                ConsoleUtility.WriteLine(buffer);
            }
            finally
            {
                rawReport.Dispose();
            }
        }
    }
}