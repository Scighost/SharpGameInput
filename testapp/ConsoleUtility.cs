using System;
using System.Runtime.InteropServices;

namespace SharpGameInput.TestApp;

internal static class ConsoleUtility
{
    public static void WriteMenuHeader(string headerText, bool padHeader = true)
    {
        if (padHeader)
        {
            // Padding between previous section and new section
            Console.WriteLine();
        }

        // Write header
        string dashes = new('-', headerText.Length);
        Console.WriteLine(dashes);
        Console.WriteLine(headerText);
        Console.WriteLine(dashes);

        // Padding between header and contents
        Console.WriteLine();
    }

    public static void WritePInvokeError(string message, int error)
    {
        Console.WriteLine($"{message}: 0x{error:X8} ({Marshal.GetPInvokeErrorMessage(error)})");
    }

    public static ConsoleKey WaitForKey(string message = "Press any key to continue...")
    {
        Console.WriteLine(message);
        return Console.ReadKey(intercept: true).Key;
    }

    public static bool PromptYesNo(string message)
    {
        while (true)
        {
            Console.Write($"{message} (y/n) ");

            string? entry = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entry))
            {
                Console.WriteLine("Invalid entry, please try again.");
            }
            else
            {
                var lower = entry.ToLowerInvariant();
                if (lower == "y" || lower == "yes")
                {
                    return true;
                }
                else if (lower == "n" || lower == "no")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid entry, please try again.");
                }
            }
        }
    }

    public static int PromptChoice(string title, params string[] options)
    {
        // Title
        Console.Write(title);
        Console.WriteLine(": ");

        // Options
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. " + options[i]);
        }
        Console.Write("Selection: ");

        while (true)
        {
            int selection = PromptChoice_ReadSelection();
            if (selection < 0 || selection >= options.Length)
            {
                Console.Write("Invalid selection, please try again: ");
                continue;
            }

            return selection;
        }
    }

    public static int PromptRange(string title, int min, int max)
    {
        // Title
        Console.Write(title);
        Console.WriteLine(": ");

        while (true)
        {
            int selection = PromptChoice_ReadSelection();
            if (selection < min || selection > max)
            {
                Console.Write("Selection out of range, please try again: ");
                continue;
            }

            return selection;
        }
    }

    private static int PromptChoice_ReadSelection()
    {
        while (true)
        {
            // Read selection
            string? entry = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entry) || !int.TryParse(entry, out int selection))
            {
                Console.Write("Could not parse, please try again: ");
                continue;
            }

            return selection;
        }
    }
}