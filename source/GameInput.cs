using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SharpGameInput
{
    public static partial class GameInput
    {
        public const int HResultFacility = 906;

        public const ulong CurrentCallbackToken = 0xFFFFFFFFFFFFFFFF;
        public const ulong InvalidCallbackToken = 0x0000000000000000;

        [DllImport("gameinput.dll", EntryPoint = "GameInputCreate", CallingConvention = CallingConvention.StdCall)]
        private static extern int Create(out IntPtr gameInput);

        public static bool Create([NotNullWhen(true)] out IGameInput? gameInput)
            => Create(out gameInput, out _);

        public static bool Create([NotNullWhen(true)] out IGameInput? gameInput, out int result)
        {
            result = Create(out IntPtr handle);
            bool success = result >= 0 && handle != IntPtr.Zero;
            gameInput = success ? new(handle, ownsHandle: true) : null;
            return success;
        }
    }
}
