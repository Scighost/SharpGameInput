using System;
using System.Collections.Generic;
using System.Linq;
using SharpGameInput;
using SharpGameInput.TestApp;

AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

// Initialize GameInput
if (!GameInput.Create(out var gameInput, out int result))
{
    ConsoleUtility.WritePInvokeError("Failed to create IGameInput", result);
    ConsoleUtility.WaitForKey("Press any key to exit...");
    return;
}

using (gameInput)
{
    // List of available tests
    List<(string name, Action<IGameInput> func)> tests =
    [
        ("Read Raw Reports", Tests.ReadRawReports),
        ("Callbacks", Tests.Callbacks),
    ];
    string[] choices = tests.Select((i) => i.name).ToArray();

    // Don't pad the header on startup, but do after startup
    bool padHeader = false;

    while (true)
    {
        try
        {
            ConsoleUtility.WriteMenuHeader("GameInput Tests", padHeader);
            padHeader = true;

            int choice = ConsoleUtility.PromptChoice("Select a test", choices);
            if (choice < 0)
                return;

            tests[choice].func(gameInput);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unhandled exception has occured:");
            Console.WriteLine(ex);

            var key = ConsoleUtility.WaitForKey("Press Escape to exit, or press any other key to go back to the main menu.");
            if (key == ConsoleKey.Escape)
                return;
        }
    }
}

static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
{
    Console.WriteLine("An unhandled exception has occured:");
    Console.WriteLine(args.ExceptionObject);
    ConsoleUtility.WaitForKey("Press any key to exit...");
}