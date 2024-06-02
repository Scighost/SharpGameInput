using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpGameInput
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct APP_LOCAL_DEVICE_ID
    {
        public const int Size = 32;

        public fixed byte value[Size];

        public override string ToString()
        {
            const string characters = "0123456789ABCDEF";

            Span<char> stringBuffer = stackalloc char[Size * 3];
            for (int i = 0; i < Size; i++)
            {
                byte v = value[i];
                int stringIndex = i * 3;
                stringBuffer[stringIndex] = characters[(v & 0xF0) >> 4];
                stringBuffer[stringIndex + 1] = characters[v & 0x0F];
                stringBuffer[stringIndex + 2] = '-';
            }
            // Exclude last '-'
            stringBuffer = stringBuffer[..^1];

            return stringBuffer.ToString();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputKeyState
    {
        public uint scanCode;
        public uint codePoint;
        public byte virtualKey;
        [MarshalAs(UnmanagedType.U1)]
        public bool isDeadKey;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMouseState
    {
        public GameInputMouseButtons buttons;
        public long positionX;
        public long positionY;
        public long wheelX;
        public long wheelY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputTouchState
    {
        public ulong touchId;
        public uint sensorIndex;
        public float positionX;
        public float positionY;
        public float pressure;
        public float proximity;
        public float contactRectTop;
        public float contactRectLeft;
        public float contactRectRight;
        public float contactRectBottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMotionState
    {
        public float accelerationX;
        public float accelerationY;
        public float accelerationZ;
        public float angularVelocityX;
        public float angularVelocityY;
        public float angularVelocityZ;
        public float magneticFieldX;
        public float magneticFieldY;
        public float magneticFieldZ;
        public float orientationW;
        public float orientationX;
        public float orientationY;
        public float orientationZ;
        public GameInputMotionAccuracy accelerometerAccuracy;
        public GameInputMotionAccuracy gyroscopeAccuracy;
        public GameInputMotionAccuracy magnetometerAccuracy;
        public GameInputMotionAccuracy orientationAccuracy;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputArcadeStickState
    {
        public GameInputArcadeStickButtons buttons;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputFlightStickState
    {
        public GameInputFlightStickButtons buttons;
        public GameInputSwitchPosition hatSwitch;
        public float roll;
        public float pitch;
        public float yaw;
        public float throttle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputGamepadState
    {
        public GameInputGamepadButtons buttons;
        public float leftTrigger;
        public float rightTrigger;
        public float leftThumbstickX;
        public float leftThumbstickY;
        public float rightThumbstickX;
        public float rightThumbstickY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputRacingWheelState
    {
        public GameInputRacingWheelButtons buttons;
        public int patternShifterGear;
        public float wheel;
        public float throttle;
        public float brake;
        public float clutch;
        public float handbrake;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputUiNavigationState
    {
        public GameInputUiNavigationButtons buttons;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputBatteryState
    {
        public float chargeRate;
        public float maxChargeRate;
        public float remainingCapacity;
        public float fullChargeCapacity;
        public GameInputBatteryStatus status;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputString
    {
        public uint sizeInBytes;
        public uint codePointCount;
        public byte* data;

        public override string ToString()
        {
            return Encoding.UTF8.GetString(data, (int)sizeInBytes);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputUsage
    {
        public ushort page;
        public ushort id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputVersion
    {
        public ushort major;
        public ushort minor;
        public ushort build;
        public ushort revision;

        public override string ToString()
        {
            return $"{major}.{minor}.{build}.{revision}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputRawDeviceItemCollectionInfo
    {
        public GameInputRawDeviceItemCollectionKind kind;
        public uint childCount;
        public uint siblingCount;
        public uint usageCount;
        public GameInputUsage* usages;
        public GameInputRawDeviceItemCollectionInfo* parent;
        public GameInputRawDeviceItemCollectionInfo* firstSibling;
        public GameInputRawDeviceItemCollectionInfo* previousSibling;
        public GameInputRawDeviceItemCollectionInfo* nextSibling;
        public GameInputRawDeviceItemCollectionInfo* lastSibling;
        public GameInputRawDeviceItemCollectionInfo* firstChild;
        public GameInputRawDeviceItemCollectionInfo* lastChild;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputRawDeviceReportItemInfo
    {
        public uint bitOffset;
        public uint bitSize;
        public long logicalMin;
        public long logicalMax;
        public double physicalMin;
        public double physicalMax;
        public GameInputRawDevicePhysicalUnitKind physicalUnits;
        public uint rawPhysicalUnits;
        public int rawPhysicalUnitsExponent;
        public GameInputRawDeviceReportItemFlags flags;
        public uint usageCount;
        public GameInputUsage* usages;
        public GameInputRawDeviceItemCollectionInfo* collection;
        public GameInputString* itemString;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputRawDeviceReportInfo
    {
        public GameInputRawDeviceReportKind kind;
        public uint id;
        public uint size;
        public uint itemCount;
        public GameInputRawDeviceReportItemInfo* items;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputControllerAxisInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        [MarshalAs(UnmanagedType.U1)]
        public bool isContinuous;
        [MarshalAs(UnmanagedType.U1)]
        public bool isNonlinear;
        [MarshalAs(UnmanagedType.U1)]
        public bool isQuantized;
        [MarshalAs(UnmanagedType.U1)]
        public bool hasRestValue;
        public float restValue;
        public ulong resolution;
        public ushort legacyDInputIndex;
        public ushort legacyHidIndex;
        public uint rawReportIndex;
        public GameInputRawDeviceReportInfo* inputReport;
        public GameInputRawDeviceReportItemInfo* inputReportItem;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputControllerButtonInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        public ushort legacyDInputIndex;
        public ushort legacyHidIndex;
        public uint rawReportIndex;
        public GameInputRawDeviceReportInfo* inputReport;
        public GameInputRawDeviceReportItemInfo* inputReportItem;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputControllerSwitchInfo
    {
        public const int positionLabels_Size = 9;

        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        // public fixed GameInputLabel positionLabels[positionLabels_Size];
        internal fixed int _positionLabels[positionLabels_Size];
        public GameInputSwitchKind kind;
        public ushort legacyDInputIndex;
        public ushort legacyHidIndex;
        public uint rawReportIndex;
        public GameInputRawDeviceReportInfo* inputReport;
        public GameInputRawDeviceReportItemInfo* inputReportItem;

        public GameInputLabel positionLabel(int index)
        {
            ThrowHelper.CheckRange(index, positionLabels_Size);
            return (GameInputLabel)_positionLabels[index];
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputKeyboardInfo
    {
        public GameInputKeyboardKind kind;
        public uint layout;
        public uint keyCount;
        public uint functionKeyCount;
        public uint maxSimultaneousKeys;
        public uint platformType;
        public uint platformSubtype;
        public GameInputString* nativeLanguage;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMouseInfo
    {
        public GameInputMouseButtons supportedButtons;
        public uint sampleRate;
        public uint sensorDpi;
        [MarshalAs(UnmanagedType.U1)]
        public bool hasWheelX;
        [MarshalAs(UnmanagedType.U1)]
        public bool hasWheelY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputTouchSensorInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        public GameInputLocation location;
        public uint locationId;
        public ulong resolutionX;
        public ulong resolutionY;
        public GameInputTouchShape shape;
        public float aspectRatio;
        public float orientation;
        public float physicalWidth;
        public float physicalHeight;
        public float maxPressure;
        public float maxProximity;
        public uint maxTouchPoints;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMotionInfo
    {
        public float maxAcceleration;
        public float maxAngularVelocity;
        public float maxMagneticFieldStrength;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputArcadeStickInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel stickUpLabel;
        public GameInputLabel stickDownLabel;
        public GameInputLabel stickLeftLabel;
        public GameInputLabel stickRightLabel;
        public GameInputLabel actionButton1Label;
        public GameInputLabel actionButton2Label;
        public GameInputLabel actionButton3Label;
        public GameInputLabel actionButton4Label;
        public GameInputLabel actionButton5Label;
        public GameInputLabel actionButton6Label;
        public GameInputLabel specialButton1Label;
        public GameInputLabel specialButton2Label;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputFlightStickInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel firePrimaryButtonLabel;
        public GameInputLabel fireSecondaryButtonLabel;
        public GameInputSwitchKind hatSwitchKind;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputGamepadInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel aButtonLabel;
        public GameInputLabel bButtonLabel;
        public GameInputLabel xButtonLabel;
        public GameInputLabel yButtonLabel;
        public GameInputLabel dpadUpLabel;
        public GameInputLabel dpadDownLabel;
        public GameInputLabel dpadLeftLabel;
        public GameInputLabel dpadRightLabel;
        public GameInputLabel leftShoulderButtonLabel;
        public GameInputLabel rightShoulderButtonLabel;
        public GameInputLabel leftThumbstickButtonLabel;
        public GameInputLabel rightThumbstickButtonLabel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputRacingWheelInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel previousGearButtonLabel;
        public GameInputLabel nextGearButtonLabel;
        public GameInputLabel dpadUpLabel;
        public GameInputLabel dpadDownLabel;
        public GameInputLabel dpadLeftLabel;
        public GameInputLabel dpadRightLabel;
        [MarshalAs(UnmanagedType.U1)]
        public bool hasClutch;
        [MarshalAs(UnmanagedType.U1)]
        public bool hasHandbrake;
        [MarshalAs(UnmanagedType.U1)]
        public bool hasPatternShifter;
        public int minPatternShifterGear;
        public int maxPatternShifterGear;
        public float maxWheelAngle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputUiNavigationInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel acceptButtonLabel;
        public GameInputLabel cancelButtonLabel;
        public GameInputLabel upButtonLabel;
        public GameInputLabel downButtonLabel;
        public GameInputLabel leftButtonLabel;
        public GameInputLabel rightButtonLabel;
        public GameInputLabel contextButton1Label;
        public GameInputLabel contextButton2Label;
        public GameInputLabel contextButton3Label;
        public GameInputLabel contextButton4Label;
        public GameInputLabel pageUpButtonLabel;
        public GameInputLabel pageDownButtonLabel;
        public GameInputLabel pageLeftButtonLabel;
        public GameInputLabel pageRightButtonLabel;
        public GameInputLabel scrollUpButtonLabel;
        public GameInputLabel scrollDownButtonLabel;
        public GameInputLabel scrollLeftButtonLabel;
        public GameInputLabel scrollRightButtonLabel;
        public GameInputLabel guideButtonLabel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackMotorInfo
    {
        public GameInputFeedbackAxes supportedAxes;
        public GameInputLocation location;
        public uint locationId;
        public uint maxSimultaneousEffects;
        [MarshalAs(UnmanagedType.U1)]
        public bool isConstantEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isRampEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isSineWaveEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isSquareWaveEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isTriangleWaveEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isSawtoothUpWaveEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isSawtoothDownWaveEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isSpringEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isFrictionEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isDamperEffectSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isInertiaEffectSupported;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputHapticWaveformInfo
    {
        public GameInputUsage usage;
        [MarshalAs(UnmanagedType.U1)]
        public bool isDurationSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isIntensitySupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isRepeatSupported;
        [MarshalAs(UnmanagedType.U1)]
        public bool isRepeatDelaySupported;
        public ulong defaultDuration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputHapticFeedbackMotorInfo
    {
        public GameInputRumbleMotors mappedRumbleMotors;
        public GameInputLocation location;
        public uint locationId;
        public uint waveformCount;
        public GameInputHapticWaveformInfo* waveformInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputDeviceInfo
    {
        public uint infoSize;
        public ushort vendorId;
        public ushort productId;
        public ushort revisionNumber;
        public byte interfaceNumber;
        public byte collectionNumber;
        public GameInputUsage usage;
        public GameInputVersion hardwareVersion;
        public GameInputVersion firmwareVersion;
        public APP_LOCAL_DEVICE_ID deviceId;
        public APP_LOCAL_DEVICE_ID deviceRootId;
        public GameInputDeviceFamily deviceFamily;
        public GameInputDeviceCapabilities capabilities;
        public GameInputKind supportedInput;
        public GameInputRumbleMotors supportedRumbleMotors;
        public uint inputReportCount;
        public uint outputReportCount;
        public uint featureReportCount;
        public uint controllerAxisCount;
        public uint controllerButtonCount;
        public uint controllerSwitchCount;
        public uint touchPointCount;
        public uint touchSensorCount;
        public uint forceFeedbackMotorCount;
        public uint hapticFeedbackMotorCount;
        public uint deviceStringCount;
        public uint deviceDescriptorSize;
        public GameInputRawDeviceReportInfo* inputReportInfo;
        public GameInputRawDeviceReportInfo* outputReportInfo;
        public GameInputRawDeviceReportInfo* featureReportInfo;
        public GameInputControllerAxisInfo* controllerAxisInfo;
        public GameInputControllerButtonInfo* controllerButtonInfo;
        public GameInputControllerSwitchInfo* controllerSwitchInfo;
        public GameInputKeyboardInfo* keyboardInfo;
        public GameInputMouseInfo* mouseInfo;
        public GameInputTouchSensorInfo* touchSensorInfo;
        public GameInputMotionInfo* motionInfo;
        public GameInputArcadeStickInfo* arcadeStickInfo;
        public GameInputFlightStickInfo* flightStickInfo;
        public GameInputGamepadInfo* gamepadInfo;
        public GameInputRacingWheelInfo* racingWheelInfo;
        public GameInputUiNavigationInfo* uiNavigationInfo;
        public GameInputForceFeedbackMotorInfo* forceFeedbackMotorInfo;
        public GameInputHapticFeedbackMotorInfo* hapticFeedbackMotorInfo;
        public GameInputString* displayName;
        public GameInputString* deviceStrings;
        public void* deviceDescriptorData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackEnvelope
    {
        public ulong attackDuration;
        public ulong sustainDuration;
        public ulong releaseDuration;
        public float attackGain;
        public float sustainGain;
        public float releaseGain;
        public uint playCount;
        public ulong repeatDelay;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackMagnitude
    {
        public float linearX;
        public float linearY;
        public float linearZ;
        public float angularX;
        public float angularY;
        public float angularZ;
        public float normal;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackConditionParams
    {
        public GameInputForceFeedbackMagnitude magnitude;
        public float positiveCoefficient;
        public float negativeCoefficient;
        public float maxPositiveMagnitude;
        public float maxNegativeMagnitude;
        public float deadZone;
        public float bias;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackConstantParams
    {
        public GameInputForceFeedbackEnvelope envelope;
        public GameInputForceFeedbackMagnitude magnitude;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackPeriodicParams
    {
        public GameInputForceFeedbackEnvelope envelope;
        public GameInputForceFeedbackMagnitude magnitude;
        public float frequency;
        public float phase;
        public float bias;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackRampParams
    {
        public GameInputForceFeedbackEnvelope envelope;
        public GameInputForceFeedbackMagnitude startMagnitude;
        public GameInputForceFeedbackMagnitude endMagnitude;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GameInputForceFeedbackParams
    {
        [FieldOffset(0)]
        public GameInputForceFeedbackEffectKind kind;

        [FieldOffset(4)]
        public GameInputForceFeedbackConstantParams constant;

        [FieldOffset(4)]
        public GameInputForceFeedbackRampParams ramp;

        [FieldOffset(4)]
        public GameInputForceFeedbackPeriodicParams sineWave;

        [FieldOffset(4)]
        public GameInputForceFeedbackPeriodicParams squareWave;

        [FieldOffset(4)]
        public GameInputForceFeedbackPeriodicParams triangleWave;

        [FieldOffset(4)]
        public GameInputForceFeedbackPeriodicParams sawtoothUpWave;

        [FieldOffset(4)]
        public GameInputForceFeedbackPeriodicParams sawtoothDownWave;

        [FieldOffset(4)]
        public GameInputForceFeedbackConditionParams spring;

        [FieldOffset(4)]
        public GameInputForceFeedbackConditionParams friction;

        [FieldOffset(4)]
        public GameInputForceFeedbackConditionParams damper;

        [FieldOffset(4)]
        public GameInputForceFeedbackConditionParams inertia;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputHapticFeedbackParams
    {
        public uint waveformIndex;
        public ulong duration;
        public float intensity;
        public uint playCount;
        public ulong repeatDelay;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputRumbleParams
    {
        public float lowFrequency;
        public float highFrequency;
        public float leftTrigger;
        public float rightTrigger;
    }
}
