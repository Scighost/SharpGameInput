using System;
using System.Diagnostics;
using System.Threading;

namespace SharpGameInput.TestApp;

internal partial class Tests
{
    public static void ReadRawReports(IGameInput gameInput)
    {
        ConsoleUtility.WriteMenuHeader("Read Raw Reports");

        byte[]? buffer = null;
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

                // Read report
                if (!reading.GetRawReport(out var rawReport))
                {
                    Console.WriteLine($"Could not get raw report!");
                    continue;
                }

                using (rawReport) unsafe
                {
                    uint reportId = rawReport.GetReportInfo()->id;
                    nuint size = rawReport.GetRawDataSize();
                    Console.Write($"Report ID: {reportId}, size: {size}, ");

                    // Read report data
                    if (buffer == null || (nuint)buffer.Length < size)
                        buffer = new byte[size];

                    fixed (byte* ptr = buffer)
                    {
                        nuint readSize = rawReport.GetRawData(size, ptr);
                        Debug.Assert(size == readSize);
                    }
                }

                Console.WriteLine(BitConverter.ToString(buffer));
            }
        }
    }
}