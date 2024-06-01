using System;
using System.Threading.Tasks;

namespace SharpGameInput
{
    public delegate void GameInputReadingCallback(
        LightGameInputCallbackToken callbackToken,
        object? context,
        LightIGameInputReading reading,
        bool hasOverrunOccurred
    );

    public delegate void GameInputDeviceCallback(
        LightGameInputCallbackToken callbackToken,
        object? context,
        LightIGameInputDevice device,
        ulong timestamp,
        GameInputDeviceStatus currentStatus,
        GameInputDeviceStatus previousStatus
    );

    public delegate void GameInputGuideButtonCallback(
        LightGameInputCallbackToken callbackToken,
        object? context,
        LightIGameInputDevice device,
        ulong timestamp,
        bool isPressed
    );

    public delegate void GameInputKeyboardLayoutCallback(
        LightGameInputCallbackToken callbackToken,
        object? context,
        LightIGameInputDevice device,
        ulong timestamp,
        uint currentLayout,
        uint previousLayout
    );

    public class GameInputCallbackToken
    {
        private IGameInput? _gameInput;
        private readonly ulong _callbackToken;

        public GameInputCallbackToken(IGameInput gameInput, ulong callbackToken)
        {
            ThrowHelper.CheckNull(gameInput);
            if (callbackToken is GameInput.InvalidCallbackToken or GameInput.CurrentCallbackToken)
                throw new ArgumentException("The given token is invalid.", nameof(callbackToken));

            _gameInput = gameInput;
            _callbackToken = callbackToken;
        }

        public void Stop()
        {
            ThrowHelper.CheckDisposed(_gameInput);
            _gameInput.StopCallback(_callbackToken);
        }

        public void Unregister(ulong timeoutInMicroseconds)
        {
            if (!TryUnregister(timeoutInMicroseconds))
                throw new TimeoutException("Could not unregister callback within the given timeout period.");
        }

        public async Task UnregisterAsync(ulong timeoutInMicroseconds)
        {
            if (!await TryUnregisterAsync(timeoutInMicroseconds))
                throw new TimeoutException("Could not unregister callback within the given timeout period.");
        }

        public bool TryUnregister(ulong timeoutInMicroseconds)
        {
            if (_gameInput == null)
                return true;

            if (!_gameInput.UnregisterCallback(_callbackToken, timeoutInMicroseconds))
                return false;

            _gameInput = null;
            return true;
        }

        public async Task<bool> TryUnregisterAsync(ulong timeoutInMicroseconds)
        {
            if (_gameInput == null)
                return true;

            if (!await Task.Run(() => _gameInput.UnregisterCallback(_callbackToken, timeoutInMicroseconds)))
                return false;

            _gameInput = null;
            return true;
        }
    }

    public ref struct LightGameInputCallbackToken
    {
        private readonly IGameInput _gameInput;
        private readonly ulong _callbackToken;

        public LightGameInputCallbackToken(IGameInput gameInput, ulong callbackToken)
        {
            ThrowHelper.CheckNull(gameInput);
            if (callbackToken is GameInput.InvalidCallbackToken or GameInput.CurrentCallbackToken)
                throw new ArgumentException("The given token is invalid.", nameof(callbackToken));

            _gameInput = gameInput;
            _callbackToken = callbackToken;
        }

        // Only StopCallback is given functionality here, as it's the
        // only available operation on them when in callbacks according to the docs
        public void Stop()
        {
            _gameInput.StopCallback(_callbackToken);
        }
    }

    public ref struct CallbackTokenDisposer
    {
        private GameInputCallbackToken? _callbackToken;
        private readonly ulong _timeoutInMicroseconds;

        public CallbackTokenDisposer(GameInputCallbackToken? callbackToken, ulong timeoutInMicroseconds)
        {
            _callbackToken = callbackToken;
            _timeoutInMicroseconds = timeoutInMicroseconds;
        }

        public void Dispose()
        {
            _callbackToken?.Unregister(_timeoutInMicroseconds);
            _callbackToken = null!;
        }
    }
}
