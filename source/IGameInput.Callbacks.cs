using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SharpGameInput
{
    public unsafe partial class IGameInput
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate void ReadingCallback_Native(
            ulong callbackToken,
            void* context,
            IntPtr reading, // IGameInputReading
            bool hasOverrunOccurred
        );

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate void DeviceCallback_Native(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            GameInputDeviceStatus currentStatus,
            GameInputDeviceStatus previousStatus
        );

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate void GuideButtonCallback_Native(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            bool isPressed
        );

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate void KeyboardLayoutCallback_Native(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            uint currentLayout,
            uint previousLayout
        );

        private readonly ConcurrentDictionary<ulong, object> _callbacks = new();

        public bool RegisterReadingCallback(
            IGameInputDevice? device,
            GameInputKind inputKind,
            float analogThreshold,
            object? context,
            GameInputReadingCallback callbackFunc,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken,
            out int result
        )
        {
            ThrowHelper.CheckNull(callbackFunc);

            ReadingCallback_Native nativeCallback = (_token, _, _reading, hasOverrunOccurred) =>
            {
                try
                {
                    var token = new LightGameInputCallbackToken(this, _token);
                    var reading = new LightIGameInputReading(_reading, true);
                    callbackFunc(token, context, reading, hasOverrunOccurred);
                }
                catch (Exception ex)
                {
                    OnUnhandledCallbackException(ex);
                }
            };

            result = RegisterReadingCallback(
                device,
                inputKind,
                analogThreshold,
                null,
                (delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, bool, void>)
                    Marshal.GetFunctionPointerForDelegate(nativeCallback),
                out ulong token
            );

            return FinishRegisteringCallback(result, token, nativeCallback, out callbackToken);
        }

        public bool RegisterDeviceCallback(
            IGameInputDevice? device,
            GameInputKind inputKind,
            GameInputDeviceStatus statusFilter,
            GameInputEnumerationKind enumerationKind,
            object? context,
            GameInputDeviceCallback callbackFunc,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken,
            out int result
        )
        {
            ThrowHelper.CheckNull(callbackFunc);

            DeviceCallback_Native nativeCallback = (_token, _, _device, timestamp, currentStatus, previousStatus) =>
            {
                try
                {
                    var token = new LightGameInputCallbackToken(this, _token);
                    var device = new LightIGameInputDevice(_device, true);
                    callbackFunc(token, context, device, timestamp, currentStatus, previousStatus);
                }
                catch (Exception ex)
                {
                    OnUnhandledCallbackException(ex);
                }
            };

            result = RegisterDeviceCallback(
                device,
                inputKind,
                statusFilter,
                enumerationKind,
                null,
                (delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, GameInputDeviceStatus, GameInputDeviceStatus, void>)
                    Marshal.GetFunctionPointerForDelegate(nativeCallback),
                out ulong token
            );

            return FinishRegisteringCallback(result, token, nativeCallback, out callbackToken);
        }

        public bool RegisterGuideButtonCallback(
            IGameInputDevice? device,
            object? context,
            GameInputGuideButtonCallback callbackFunc,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken,
            out int result
        )
        {
            ThrowHelper.CheckNull(callbackFunc);

            GuideButtonCallback_Native nativeCallback = (_token, _, _device, timestamp, isPressed) =>
            {
                try
                {
                    var token = new LightGameInputCallbackToken(this, _token);
                    var device = new LightIGameInputDevice(_device, true);
                    callbackFunc(token, context, device, timestamp, isPressed);
                }
                catch (Exception ex)
                {
                    OnUnhandledCallbackException(ex);
                }
            };

            result = RegisterGuideButtonCallback(
                device,
                null,
                (delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, bool, void>)
                    Marshal.GetFunctionPointerForDelegate(nativeCallback),
                out ulong token
            );

            return FinishRegisteringCallback(result, token, nativeCallback, out callbackToken);
        }

        public bool RegisterKeyboardLayoutCallback(
            IGameInputDevice? device,
            object? context,
            GameInputKeyboardLayoutCallback callbackFunc,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken,
            out int result
        )
        {
            ThrowHelper.CheckNull(callbackFunc);

            KeyboardLayoutCallback_Native nativeCallback = (_token, _, _device, timestamp, currentLayout, previousLayout) =>
            {
                try
                {
                    var token = new LightGameInputCallbackToken(this, _token);
                    var device = new LightIGameInputDevice(_device, true);
                    callbackFunc(token, context, device, timestamp, currentLayout, previousLayout);
                }
                catch (Exception ex)
                {
                    OnUnhandledCallbackException(ex);
                }
            };

            result = RegisterKeyboardLayoutCallback(
                device,
                null,
                (delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, uint, uint, void>)
                    Marshal.GetFunctionPointerForDelegate(nativeCallback),
                out ulong token
            );

            return FinishRegisteringCallback(result, token, nativeCallback, out callbackToken);
        }

        public bool UnregisterCallback(ulong callbackToken, ulong timeoutInMicroseconds)
        {
            if (!_UnregisterCallback(callbackToken, timeoutInMicroseconds))
                return false;

            if (!_callbacks.TryRemove(callbackToken, out _))
            {
                // This should never happen; throw to notify of the problem
                throw new Exception("Failed to remove internal callback function reference, this should never happen!");
            }

            return true;
        }

        private bool FinishRegisteringCallback(int result, ulong token, object nativeCallback,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken)
        {
            if (result < 0)
            {
                callbackToken = null;
                return false;
            }

            if (!_callbacks.TryAdd(token, nativeCallback))
            {
                // This should never happen; make a best attempt to prevent the
                // callback from being leaked/called and then throw to notify of the problem
                StopCallback(token);
                _UnregisterCallback(token, 500);
                throw new Exception("A duplicate callback token has been encountered! This should never happen; if it does you're in some big trouble.");
            }

            callbackToken = new(this, token);
            return true;
        }

        private static void OnUnhandledCallbackException(Exception ex)
        {
            // TODO: Find a more proper way of reporting unhandled exceptions
            Environment.FailFast("Unhandled exception in GameInput callback!", ex);
        }
    }
}
