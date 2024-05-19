using System.Runtime.InteropServices;

namespace SharpGameInput;

public static partial class GameInput
{
    public const int HResultFacility = 906;

    public const ulong CurrentCallbackToken = 0xFFFFFFFFFFFFFFFF;
    public const ulong InvalidCallbackToken = 0x0000000000000000;

    [LibraryImport("gameinput.dll", EntryPoint = "GameInputCreate")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
    public static partial int Create(out IGameInput gameInput);
}
