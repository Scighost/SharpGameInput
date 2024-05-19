using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using GameInputCallbackToken = ulong;

namespace SharpGameInput;

using unsafe GameInputReadingCallback = delegate* unmanaged[Stdcall]<
    GameInputCallbackToken, // callbackToken
    void*, // context
    IGameInputReading, // reading
    bool // hasOverrunOccurred
>;

using unsafe GameInputDeviceCallback = delegate* unmanaged[Stdcall]<
    GameInputCallbackToken, // callbackToken
    void*, // context
    IGameInputDevice, // device
    ulong, // timestamp
    GameInputDeviceStatus, // currentStatus
    GameInputDeviceStatus // previousStatus
>;

using unsafe GameInputGuideButtonCallback = delegate* unmanaged[Stdcall]<
    GameInputCallbackToken, // callbackToken
    void*, // context
    IGameInputDevice, // device
    ulong, // timestamp
    bool // isPressed
>;

using unsafe GameInputKeyboardLayoutCallback = delegate* unmanaged[Stdcall]<
    GameInputCallbackToken, // callbackToken
    void*, // context
    IGameInputDevice, // device
    ulong, // timestamp
    uint, // currentLayout,
    uint // previousLayout
>;

[GeneratedComInterface]
[Guid("11BE2A7E-4254-445A-9C09-FFC40F006918")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public unsafe partial interface IGameInput
{
    [PreserveSig]
    ulong GetCurrentTimestamp();

    [PreserveSig]
    int GetCurrentReading(
        GameInputKind inputKind,
        [Optional] IGameInputDevice? device,
        out IGameInputReading reading
    );

    [PreserveSig]
    int GetNextReading(
        IGameInputReading referenceReading,
        GameInputKind inputKind,
        [Optional] IGameInputDevice? device,
        out IGameInputReading reading
    );

    [PreserveSig]
    int GetPreviousReading(
        IGameInputReading referenceReading,
        GameInputKind inputKind,
        [Optional] IGameInputDevice? device,
        out IGameInputReading reading
    );

    [PreserveSig]
    int GetTemporalReading(
        ulong timestamp,
        IGameInputDevice device,
        out IGameInputReading reading
    );

    [PreserveSig]
    int RegisterReadingCallback(
        [Optional] IGameInputDevice? device,
        GameInputKind inputKind,
        float analogThreshold,
        [Optional] void* context,
        GameInputReadingCallback callbackFunc,
        [Optional] out GameInputCallbackToken callbackToken
    );

    [PreserveSig]
    int RegisterDeviceCallback(
        [Optional] IGameInputDevice? device,
        GameInputKind inputKind,
        GameInputDeviceStatus statusFilter,
        GameInputEnumerationKind enumerationKind,
        [Optional] void* context,
        GameInputDeviceCallback callbackFunc,
        [Optional] out GameInputCallbackToken callbackToken
    );

    [PreserveSig]
    int RegisterGuideButtonCallback(
        [Optional] IGameInputDevice? device,
        [Optional] void* context,
        GameInputGuideButtonCallback callbackFunc,
        [Optional] out GameInputCallbackToken callbackToken
    );

    [PreserveSig]
    int RegisterKeyboardLayoutCallback(
        [Optional] IGameInputDevice? device,
        [Optional] void* context,
        GameInputKeyboardLayoutCallback callbackFunc,
        [Optional] out GameInputCallbackToken callbackToken
    );

    [PreserveSig]
    void StopCallback(
        GameInputCallbackToken callbackToken
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool UnregisterCallback(
        GameInputCallbackToken callbackToken,
        ulong timeoutInMicroseconds
    );

    [PreserveSig]
    int CreateDispatcher(
        out IGameInputDispatcher dispatcher
    );

    [PreserveSig]
    int CreateAggregateDevice(
        GameInputKind inputKind,
        out IGameInputDevice device
    );

    [PreserveSig]
    int FindDeviceFromId(
        in APP_LOCAL_DEVICE_ID value,
        out IGameInputDevice device
    );

    [PreserveSig]
    int FindDeviceFromObject(
        nint value, // IUnknown
        out IGameInputDevice device
    );

    [PreserveSig]
    int FindDeviceFromPlatformHandle(
        void* value,
        out IGameInputDevice device
    );

    [PreserveSig]
    int FindDeviceFromPlatformString(
        char* value, // LPCWSTR
        out IGameInputDevice device
    );

    [PreserveSig]
    int EnableOemDeviceSupport(
        ushort vendorId,
        ushort productId,
        byte interfaceNumber,
        byte collectionNumber
    );

    [PreserveSig]
    void SetFocusPolicy(
        GameInputFocusPolicy policy
    );
}

[GeneratedComInterface]
[Guid("2156947A-E1FA-4DE0-A30B-D812931DBD8D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public unsafe partial interface IGameInputReading
{
    [PreserveSig]
    GameInputKind GetInputKind();

    [PreserveSig]
    ulong GetSequenceNumber(
        GameInputKind inputKind
    );

    [PreserveSig]
    ulong GetTimestamp();

    [PreserveSig]
    void GetDevice(
        out IGameInputDevice device
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetRawReport(
        [NotNullWhen(true)] out IGameInputRawDeviceReport? report
    );

    [PreserveSig]
    uint GetControllerAxisCount();

    [PreserveSig]
    uint GetControllerAxisState(
        uint stateArrayCount,
        float* stateArray
    );

    [PreserveSig]
    uint GetControllerButtonCount();

    [PreserveSig]
    uint GetControllerButtonState(
        uint stateArrayCount,
        bool* stateArray
    );

    [PreserveSig]
    uint GetControllerSwitchCount();

    [PreserveSig]
    uint GetControllerSwitchState(
        uint stateArrayCount,
        GameInputSwitchPosition* stateArray
    );

    [PreserveSig]
    uint GetKeyCount();

    [PreserveSig]
    uint GetKeyState(
        uint stateArrayCount,
        GameInputKeyState* stateArray
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetMouseState(
        out GameInputMouseState state
    );

    [PreserveSig]
    uint GetTouchCount();

    [PreserveSig]
    uint GetTouchState(
        uint stateArrayCount,
        GameInputTouchState* stateArray
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetMotionState(
        out GameInputMotionState state
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetArcadeStickState(
        out GameInputArcadeStickState state
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetFlightStickState(
        out GameInputFlightStickState state
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetGamepadState(
        out GameInputGamepadState state
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetRacingWheelState(
        out GameInputRacingWheelState state
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetUiNavigationState(
        out GameInputUiNavigationState state
    );
}

[GeneratedComInterface]
[Guid("31DD86FB-4C1B-408A-868F-439B3CD47125")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public unsafe partial interface IGameInputDevice
{
    [PreserveSig]
    GameInputDeviceInfo* GetDeviceInfo();

    [PreserveSig]
    GameInputDeviceStatus GetDeviceStatus();

    [PreserveSig]
    void GetBatteryState(
        out GameInputBatteryState state
    );

    // [return: DoNotIgnore]
    [PreserveSig]
    int CreateForceFeedbackEffect(
        uint motorIndex,
        in GameInputForceFeedbackParams ffbParams,
        out IGameInputForceFeedbackEffect effect
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool IsForceFeedbackMotorPoweredOn(
        uint motorIndex
    );

    [PreserveSig]
    void SetForceFeedbackMotorGain(
        uint motorIndex,
        float masterGain
    );

    [PreserveSig]
    int SetHapticMotorState(
        uint motorIndex,
        [Optional] in GameInputHapticFeedbackParams hapticParams
    );

    [PreserveSig]
    void SetRumbleState(
        [Optional] in GameInputRumbleParams rumbleParams
    );

    [PreserveSig]
    void SetInputSynchronizationState(
        [MarshalAs(UnmanagedType.U1)] bool enabled
    );

    [PreserveSig]
    void SendInputSynchronizationHint();

    [PreserveSig]
    void PowerOff();

    // [return: DoNotIgnore]
    [PreserveSig]
    int CreateRawDeviceReport(
        uint reportId,
        GameInputRawDeviceReportKind reportKind,
        out IGameInputRawDeviceReport report
    );

    // [return: DoNotIgnore]
    [PreserveSig]
    int GetRawDeviceFeature(
        uint reportId,
        out IGameInputRawDeviceReport report
    );

    [PreserveSig]
    int SetRawDeviceFeature(
        IGameInputRawDeviceReport report
    );

    [PreserveSig]
    int SendRawDeviceOutput(
        IGameInputRawDeviceReport report
    );

    [PreserveSig]
    int SendRawDeviceOutputWithResponse(
        IGameInputRawDeviceReport requestReport,
        out IGameInputRawDeviceReport responseReport
    );

    [PreserveSig]
    int ExecuteRawDeviceIoControl(
        uint controlCode,
        nuint inputBufferSize,
        [Optional] void* inputBuffer,
        nuint outputBufferSize,
        [Optional] void* outputBuffer,
        [Optional] out nuint outputSize
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool AcquireExclusiveRawDeviceAccess(
        ulong timeoutInMicroseconds
    );

    [PreserveSig]
    void ReleaseExclusiveRawDeviceAccess();
}

[GeneratedComInterface]
[Guid("415EED2E-98CB-42C2-8F28-B94601074E31")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public unsafe partial interface IGameInputDispatcher
{
    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool Dispatch(
        ulong quotaInMicroseconds
    );

    [PreserveSig]
    int OpenWaitHandle(
        out void* waitHandle
    );
}

[GeneratedComInterface]
[Guid("51BDA05E-F742-45D9-B085-9444AE48381D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public unsafe partial interface IGameInputForceFeedbackEffect
{
    [PreserveSig]
    void GetDevice(
        out IGameInputDevice device
    );

    [PreserveSig]
    uint GetMotorIndex();

    [PreserveSig]
    float GetGain();

    [PreserveSig]
    void SetGain(
        float gain
    );

    [PreserveSig]
    void GetParams(
        out GameInputForceFeedbackParams ffbParams
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool SetParams(
        in GameInputForceFeedbackParams ffbParams
    );

    [PreserveSig]
    GameInputFeedbackEffectState GetState();

    [PreserveSig]
    void SetState(
        GameInputFeedbackEffectState state
    );
}

[GeneratedComInterface]
[Guid("61F08CF1-1FFC-40CA-A2B8-E1AB8BC5B6DC")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public unsafe partial interface IGameInputRawDeviceReport
{
    [PreserveSig]
    void GetDevice(
        out IGameInputDevice device
    );

    [PreserveSig]
    GameInputRawDeviceReportInfo GetReportInfo();

    [PreserveSig]
    nuint GetRawDataSize();

    [PreserveSig]
    nuint GetRawData(
        nuint bufferSize,
        void * buffer
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool SetRawData(
        nuint bufferSize,
        void* buffer
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool GetItemValue(
        uint itemIndex,
        out long value
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool SetItemValue(
        uint itemIndex,
        long value
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool ResetItemValue(
        uint itemIndex
    );

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.U1)]
    bool ResetAllItems();
}
