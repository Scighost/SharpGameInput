using System;
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

        public static int Create(out IGameInput gameInput)
        {
            int result = Create(out IntPtr handle);
            if (result >= 0 && handle != IntPtr.Zero)
                gameInput = new(handle);
            else
                gameInput = null!;
            return result;
        }
    }
}
