using System.Runtime.InteropServices;

namespace SharpGameInput
{
    public static partial class GameInput
    {
        public const int HResultFacility = 906;

        public const ulong CurrentCallbackToken = 0xFFFFFFFFFFFFFFFF;
        public const ulong InvalidCallbackToken = 0x0000000000000000;

        [DllImport("gameinput.dll", EntryPoint = "GameInputCreate", CallingConvention = CallingConvention.StdCall)]
        public static extern int Create(out IGameInput gameInput);
    }
}
