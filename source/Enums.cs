using System;

namespace SharpGameInput
{
    public enum GameInputResult
    {
        FACILITY_GAMEINPUT = 906,

        DEVICE_DISCONNECTED = unchecked((int)0x838A0001),
        DEVICE_NOT_FOUND = unchecked((int)0x838A0002),
        READING_NOT_FOUND = unchecked((int)0x838A0003),
        REFERENCE_READING_TOO_OLD = unchecked((int)0x838A0004),
        TIMESTAMP_OUT_OF_RANGE = unchecked((int)0x838A0005),
        INSUFFICIENT_FORCE_FEEDBACK_RESOURCES = unchecked((int)0x838A0006),
    }

    [Flags]
    public enum GameInputKind
    {
        GameInputKindUnknown          = 0x00000000,
        GameInputKindRawDeviceReport  = 0x00000001,
        GameInputKindControllerAxis   = 0x00000002,
        GameInputKindControllerButton = 0x00000004,
        GameInputKindControllerSwitch = 0x00000008,
        GameInputKindController       = 0x0000000E,
        GameInputKindKeyboard         = 0x00000010,
        GameInputKindMouse            = 0x00000020,
        GameInputKindTouch            = 0x00000100,
        GameInputKindMotion           = 0x00001000,
        GameInputKindArcadeStick      = 0x00010000,
        GameInputKindFlightStick      = 0x00020000,
        GameInputKindGamepad          = 0x00040000,
        GameInputKindRacingWheel      = 0x00080000,
        GameInputKindUiNavigation     = 0x01000000
    }

    public enum GameInputEnumerationKind
    {
        GameInputNoEnumeration       = 0,
        GameInputAsyncEnumeration    = 1,
        GameInputBlockingEnumeration = 2
    }

    [Flags]
    public enum GameInputFocusPolicy
    {
        GameInputDefaultFocusPolicy       = 0x00000000,
        GameInputDisableBackgroundInput   = 0x00000001,
        GameInputExclusiveForegroundInput = 0x00000002
    }

    public enum GameInputSwitchKind
    {
        GameInputUnknownSwitchKind = -1,
        GameInput2WaySwitch        =  0,
        GameInput4WaySwitch        =  1,
        GameInput8WaySwitch        =  2
    }

    public enum GameInputSwitchPosition
    {
        GameInputSwitchCenter    = 0,
        GameInputSwitchUp        = 1,
        GameInputSwitchUpRight   = 2,
        GameInputSwitchRight     = 3,
        GameInputSwitchDownRight = 4,
        GameInputSwitchDown      = 5,
        GameInputSwitchDownLeft  = 6,
        GameInputSwitchLeft      = 7,
        GameInputSwitchUpLeft    = 8
    }

    public enum GameInputKeyboardKind
    {
        GameInputUnknownKeyboard = -1,
        GameInputAnsiKeyboard    =  0,
        GameInputIsoKeyboard     =  1,
        GameInputKsKeyboard      =  2,
        GameInputAbntKeyboard    =  3,
        GameInputJisKeyboard     =  4
    }

    [Flags]
    public enum GameInputMouseButtons
    {
        GameInputMouseNone           = 0x00000000,
        GameInputMouseLeftButton     = 0x00000001,
        GameInputMouseRightButton    = 0x00000002,
        GameInputMouseMiddleButton   = 0x00000004,
        GameInputMouseButton4        = 0x00000008,
        GameInputMouseButton5        = 0x00000010,
        GameInputMouseWheelTiltLeft  = 0x00000020,
        GameInputMouseWheelTiltRight = 0x00000040
    }

    public enum GameInputTouchShape
    {
        GameInputTouchShapeUnknown       = -1,
        GameInputTouchShapePoint         =  0,
        GameInputTouchShape1DLinear      =  1,
        GameInputTouchShape1DRadial      =  2,
        GameInputTouchShape1DIrregular   =  3,
        GameInputTouchShape2DRectangular =  4,
        GameInputTouchShape2DElliptical  =  5,
        GameInputTouchShape2DIrregular   =  6
    }

    public enum GameInputMotionAccuracy
    {
        GameInputMotionAccuracyUnknown = -1,
        GameInputMotionUnavailable     =  0,
        GameInputMotionUnreliable      =  1,
        GameInputMotionApproximate     =  2,
        GameInputMotionAccurate        =  3
    }

    [Flags]
    public enum GameInputArcadeStickButtons
    {
        GameInputArcadeStickNone     = 0x00000000,
        GameInputArcadeStickMenu     = 0x00000001,
        GameInputArcadeStickView     = 0x00000002,
        GameInputArcadeStickUp       = 0x00000004,
        GameInputArcadeStickDown     = 0x00000008,
        GameInputArcadeStickLeft     = 0x00000010,
        GameInputArcadeStickRight    = 0x00000020,
        GameInputArcadeStickAction1  = 0x00000040,
        GameInputArcadeStickAction2  = 0x00000080,
        GameInputArcadeStickAction3  = 0x00000100,
        GameInputArcadeStickAction4  = 0x00000200,
        GameInputArcadeStickAction5  = 0x00000400,
        GameInputArcadeStickAction6  = 0x00000800,
        GameInputArcadeStickSpecial1 = 0x00001000,
        GameInputArcadeStickSpecial2 = 0x00002000
    }

    [Flags]
    public enum GameInputFlightStickButtons
    {
        GameInputFlightStickNone          = 0x00000000,
        GameInputFlightStickMenu          = 0x00000001,
        GameInputFlightStickView          = 0x00000002,
        GameInputFlightStickFirePrimary   = 0x00000004,
        GameInputFlightStickFireSecondary = 0x00000008
    }

    [Flags]
    public enum GameInputGamepadButtons
    {
        GameInputGamepadNone            = 0x00000000,
        GameInputGamepadMenu            = 0x00000001,
        GameInputGamepadView            = 0x00000002,
        GameInputGamepadA               = 0x00000004,
        GameInputGamepadB               = 0x00000008,
        GameInputGamepadX               = 0x00000010,
        GameInputGamepadY               = 0x00000020,
        GameInputGamepadDPadUp          = 0x00000040,
        GameInputGamepadDPadDown        = 0x00000080,
        GameInputGamepadDPadLeft        = 0x00000100,
        GameInputGamepadDPadRight       = 0x00000200,
        GameInputGamepadLeftShoulder    = 0x00000400,
        GameInputGamepadRightShoulder   = 0x00000800,
        GameInputGamepadLeftThumbstick  = 0x00001000,
        GameInputGamepadRightThumbstick = 0x00002000
    }

    [Flags]
    public enum GameInputRacingWheelButtons
    {
        GameInputRacingWheelNone         = 0x00000000,
        GameInputRacingWheelMenu         = 0x00000001,
        GameInputRacingWheelView         = 0x00000002,
        GameInputRacingWheelPreviousGear = 0x00000004,
        GameInputRacingWheelNextGear     = 0x00000008,
        GameInputRacingWheelDpadUp       = 0x00000010,
        GameInputRacingWheelDpadDown     = 0x00000020,
        GameInputRacingWheelDpadLeft     = 0x00000040,
        GameInputRacingWheelDpadRight    = 0x00000080
    }

    [Flags]
    public enum GameInputUiNavigationButtons
    {
        GameInputUiNavigationNone        = 0x00000000,
        GameInputUiNavigationMenu        = 0x00000001,
        GameInputUiNavigationView        = 0x00000002,
        GameInputUiNavigationAccept      = 0x00000004,
        GameInputUiNavigationCancel      = 0x00000008,
        GameInputUiNavigationUp          = 0x00000010,
        GameInputUiNavigationDown        = 0x00000020,
        GameInputUiNavigationLeft        = 0x00000040,
        GameInputUiNavigationRight       = 0x00000080,
        GameInputUiNavigationContext1    = 0x00000100,
        GameInputUiNavigationContext2    = 0x00000200,
        GameInputUiNavigationContext3    = 0x00000400,
        GameInputUiNavigationContext4    = 0x00000800,
        GameInputUiNavigationPageUp      = 0x00001000,
        GameInputUiNavigationPageDown    = 0x00002000,
        GameInputUiNavigationPageLeft    = 0x00004000,
        GameInputUiNavigationPageRight   = 0x00008000,
        GameInputUiNavigationScrollUp    = 0x00010000,
        GameInputUiNavigationScrollDown  = 0x00020000,
        GameInputUiNavigationScrollLeft  = 0x00040000,
        GameInputUiNavigationScrollRight = 0x00080000
    }

    [Flags]
    public enum GameInputDeviceStatus
    {
        GameInputDeviceNoStatus      = 0x00000000,
        GameInputDeviceConnected     = 0x00000001,
        GameInputDeviceInputEnabled  = 0x00000002,
        GameInputDeviceOutputEnabled = 0x00000004,
        GameInputDeviceRawIoEnabled  = 0x00000008,
        GameInputDeviceAudioCapture  = 0x00000010,
        GameInputDeviceAudioRender   = 0x00000020,
        GameInputDeviceSynchronized  = 0x00000040,
        GameInputDeviceWireless      = 0x00000080,
        GameInputDeviceUserIdle      = 0x00100000,
        GameInputDeviceAnyStatus     = 0x00FFFFFF
    }

    public enum GameInputBatteryStatus
    {
        GameInputBatteryUnknown     = -1,
        GameInputBatteryNotPresent  =  0,
        GameInputBatteryDischarging =  1,
        GameInputBatteryIdle        =  2,
        GameInputBatteryCharging    =  3
    }

    public enum GameInputDeviceFamily
    {
        GameInputFamilyVirtual   = -1,
        GameInputFamilyAggregate =  0,
        GameInputFamilyXboxOne   =  1,
        GameInputFamilyXbox360   =  2,
        GameInputFamilyHid       =  3,
        GameInputFamilyI8042     =  4
    }

    [Flags]
    public enum GameInputDeviceCapabilities
    {
        GameInputDeviceCapabilityNone            = 0x00000000,
        GameInputDeviceCapabilityAudio           = 0x00000001,
        GameInputDeviceCapabilityPluginModule    = 0x00000002,
        GameInputDeviceCapabilityPowerOff        = 0x00000004,
        GameInputDeviceCapabilitySynchronization = 0x00000008,
        GameInputDeviceCapabilityWireless        = 0x00000010
    }

    public enum GameInputRawDeviceReportKind
    {
        GameInputRawInputReport   = 0,
        GameInputRawOutputReport  = 1,
        GameInputRawFeatureReport = 2
    }

    [Flags]
    public enum GameInputRawDeviceReportItemFlags
    {
        GameInputDefaultItem    = 0x00000000,
        GameInputConstantItem   = 0x00000001,
        GameInputArrayItem      = 0x00000002,
        GameInputRelativeItem   = 0x00000004,
        GameInputWraparoundItem = 0x00000008,
        GameInputNonlinearItem  = 0x00000010,
        GameInputStableItem     = 0x00000020,
        GameInputNullableItem   = 0x00000040,
        GameInputVolatileItem   = 0x00000080,
        GameInputBufferedItem   = 0x00000100
    }

    public enum GameInputRawDeviceItemCollectionKind
    {
        GameInputUnknownItemCollection       = -1,
        GameInputPhysicalItemCollection      =  0,
        GameInputApplicationItemCollection   =  1,
        GameInputLogicalItemCollection       =  2,
        GameInputReportItemCollection        =  3,
        GameInputNamedArrayItemCollection    =  4,
        GameInputUsageSwitchItemCollection   =  5,
        GameInputUsageModifierItemCollection =  6
    }

    public enum GameInputRawDevicePhysicalUnitKind
    {
        GameInputPhysicalUnitUnknown             = -1,
        GameInputPhysicalUnitNone                =  0,
        GameInputPhysicalUnitTime                =  1,
        GameInputPhysicalUnitFrequency           =  2,
        GameInputPhysicalUnitLength              =  3,
        GameInputPhysicalUnitVelocity            =  4,
        GameInputPhysicalUnitAcceleration        =  5,
        GameInputPhysicalUnitMass                =  6,
        GameInputPhysicalUnitMomentum            =  7,
        GameInputPhysicalUnitForce               =  8,
        GameInputPhysicalUnitPressure            =  9,
        GameInputPhysicalUnitAngle               = 10,
        GameInputPhysicalUnitAngularVelocity     = 11,
        GameInputPhysicalUnitAngularAcceleration = 12,
        GameInputPhysicalUnitAngularMass         = 13,
        GameInputPhysicalUnitAngularMomentum     = 14,
        GameInputPhysicalUnitAngularTorque       = 15,
        GameInputPhysicalUnitElectricCurrent     = 16,
        GameInputPhysicalUnitElectricCharge      = 17,
        GameInputPhysicalUnitElectricPotential   = 18,
        GameInputPhysicalUnitEnergy              = 19,
        GameInputPhysicalUnitPower               = 20,
        GameInputPhysicalUnitTemperature         = 21,
        GameInputPhysicalUnitLuminousIntensity   = 22,
        GameInputPhysicalUnitLuminousFlux        = 23,
        GameInputPhysicalUnitIlluminance         = 24
    }

    public enum GameInputLabel
    {
        GameInputLabelUnknown                  =  -1,
        GameInputLabelNone                     =   0,
        GameInputLabelXboxGuide                =   1,
        GameInputLabelXboxBack                 =   2,
        GameInputLabelXboxStart                =   3,
        GameInputLabelXboxMenu                 =   4,
        GameInputLabelXboxView                 =   5,
        GameInputLabelXboxA                    =   7,
        GameInputLabelXboxB                    =   8,
        GameInputLabelXboxX                    =   9,
        GameInputLabelXboxY                    =  10,
        GameInputLabelXboxDPadUp               =  11,
        GameInputLabelXboxDPadDown             =  12,
        GameInputLabelXboxDPadLeft             =  13,
        GameInputLabelXboxDPadRight            =  14,
        GameInputLabelXboxLeftShoulder         =  15,
        GameInputLabelXboxLeftTrigger          =  16,
        GameInputLabelXboxLeftStickButton      =  17,
        GameInputLabelXboxRightShoulder        =  18,
        GameInputLabelXboxRightTrigger         =  19,
        GameInputLabelXboxRightStickButton     =  20,
        GameInputLabelXboxPaddle1              =  21,
        GameInputLabelXboxPaddle2              =  22,
        GameInputLabelXboxPaddle3              =  23,
        GameInputLabelXboxPaddle4              =  24,
        GameInputLabelLetterA                  =  25,
        GameInputLabelLetterB                  =  26,
        GameInputLabelLetterC                  =  27,
        GameInputLabelLetterD                  =  28,
        GameInputLabelLetterE                  =  29,
        GameInputLabelLetterF                  =  30,
        GameInputLabelLetterG                  =  31,
        GameInputLabelLetterH                  =  32,
        GameInputLabelLetterI                  =  33,
        GameInputLabelLetterJ                  =  34,
        GameInputLabelLetterK                  =  35,
        GameInputLabelLetterL                  =  36,
        GameInputLabelLetterM                  =  37,
        GameInputLabelLetterN                  =  38,
        GameInputLabelLetterO                  =  39,
        GameInputLabelLetterP                  =  40,
        GameInputLabelLetterQ                  =  41,
        GameInputLabelLetterR                  =  42,
        GameInputLabelLetterS                  =  43,
        GameInputLabelLetterT                  =  44,
        GameInputLabelLetterU                  =  45,
        GameInputLabelLetterV                  =  46,
        GameInputLabelLetterW                  =  47,
        GameInputLabelLetterX                  =  48,
        GameInputLabelLetterY                  =  49,
        GameInputLabelLetterZ                  =  50,
        GameInputLabelNumber0                  =  51,
        GameInputLabelNumber1                  =  52,
        GameInputLabelNumber2                  =  53,
        GameInputLabelNumber3                  =  54,
        GameInputLabelNumber4                  =  55,
        GameInputLabelNumber5                  =  56,
        GameInputLabelNumber6                  =  57,
        GameInputLabelNumber7                  =  58,
        GameInputLabelNumber8                  =  59,
        GameInputLabelNumber9                  =  60,
        GameInputLabelArrowUp                  =  61,
        GameInputLabelArrowUpRight             =  62,
        GameInputLabelArrowRight               =  63,
        GameInputLabelArrowDownRight           =  64,
        GameInputLabelArrowDown                =  65,
        GameInputLabelArrowDownLLeft           =  66,
        GameInputLabelArrowLeft                =  67,
        GameInputLabelArrowUpLeft              =  68,
        GameInputLabelArrowUpDown              =  69,
        GameInputLabelArrowLeftRight           =  70,
        GameInputLabelArrowUpDownLeftRight     =  71,
        GameInputLabelArrowClockwise           =  72,
        GameInputLabelArrowCounterClockwise    =  73,
        GameInputLabelArrowReturn              =  74,
        GameInputLabelIconBranding             =  75,
        GameInputLabelIconHome                 =  76,
        GameInputLabelIconMenu                 =  77,
        GameInputLabelIconCross                =  78,
        GameInputLabelIconCircle               =  79,
        GameInputLabelIconSquare               =  80,
        GameInputLabelIconTriangle             =  81,
        GameInputLabelIconStar                 =  82,
        GameInputLabelIconDPadUp               =  83,
        GameInputLabelIconDPadDown             =  84,
        GameInputLabelIconDPadLeft             =  85,
        GameInputLabelIconDPadRight            =  86,
        GameInputLabelIconDialClockwise        =  87,
        GameInputLabelIconDialCounterClockwise =  88,
        GameInputLabelIconSliderLeftRight      =  89,
        GameInputLabelIconSliderUpDown         =  90,
        GameInputLabelIconWheelUpDown          =  91,
        GameInputLabelIconPlus                 =  92,
        GameInputLabelIconMinus                =  93,
        GameInputLabelIconSuspension           =  94,
        GameInputLabelHome                     =  95,
        GameInputLabelGuide                    =  96,
        GameInputLabelMode                     =  97,
        GameInputLabelSelect                   =  98,
        GameInputLabelMenu                     =  99,
        GameInputLabelView                     = 100,
        GameInputLabelBack                     = 101,
        GameInputLabelStart                    = 102,
        GameInputLabelOptions                  = 103,
        GameInputLabelShare                    = 104,
        GameInputLabelUp                       = 105,
        GameInputLabelDown                     = 106,
        GameInputLabelLeft                     = 107,
        GameInputLabelRight                    = 108,
        GameInputLabelLB                       = 109,
        GameInputLabelLT                       = 110,
        GameInputLabelLSB                      = 111,
        GameInputLabelL1                       = 112,
        GameInputLabelL2                       = 113,
        GameInputLabelL3                       = 114,
        GameInputLabelRB                       = 115,
        GameInputLabelRT                       = 116,
        GameInputLabelRSB                      = 117,
        GameInputLabelR1                       = 118,
        GameInputLabelR2                       = 119,
        GameInputLabelR3                       = 120,
        GameInputLabelP1                       = 121,
        GameInputLabelP2                       = 122,
        GameInputLabelP3                       = 123,
        GameInputLabelP4                       = 124
    }

    public enum GameInputLocation
    {
        GameInputLocationUnknown  = -1,
        GameInputLocationChassis  =  0,
        GameInputLocationDisplay  =  1,
        GameInputLocationAxis     =  2,
        GameInputLocationButton   =  3,
        GameInputLocationSwitch   =  4,
        GameInputLocationKey      =  5,
        GameInputLocationTouchPad =  6
    }

    [Flags]
    public enum GameInputFeedbackAxes
    {
        GameInputFeedbackAxisNone     = 0x00000000,
        GameInputFeedbackAxisLinearX  = 0x00000001,
        GameInputFeedbackAxisLinearY  = 0x00000002,
        GameInputFeedbackAxisLinearZ  = 0x00000004,
        GameInputFeedbackAxisAngularX = 0x00000008,
        GameInputFeedbackAxisAngularY = 0x00000010,
        GameInputFeedbackAxisAngularZ = 0x00000020,
        GameInputFeedbackAxisNormal   = 0x00000040
    }

    public enum GameInputFeedbackEffectState
    {
        GameInputFeedbackStopped = 0,
        GameInputFeedbackRunning = 1,
        GameInputFeedbackPaused  = 2
    }

    public enum GameInputForceFeedbackEffectKind
    {
        GameInputForceFeedbackConstant         = 0,
        GameInputForceFeedbackRamp             = 1,
        GameInputForceFeedbackSineWave         = 2,
        GameInputForceFeedbackSquareWave       = 3,
        GameInputForceFeedbackTriangleWave     = 4,
        GameInputForceFeedbackSawtoothUpWave   = 5,
        GameInputForceFeedbackSawtoothDownWave = 6,
        GameInputForceFeedbackSpring           = 7,
        GameInputForceFeedbackFriction         = 8,
        GameInputForceFeedbackDamper           = 9,
        GameInputForceFeedbackInertia          = 10
    }

    [Flags]
    public enum GameInputRumbleMotors
    {
        GameInputRumbleNone          = 0x00000000,
        GameInputRumbleLowFrequency  = 0x00000001,
        GameInputRumbleHighFrequency = 0x00000002,
        GameInputRumbleLeftTrigger   = 0x00000004,
        GameInputRumbleRightTrigger  = 0x00000008
    }
}
