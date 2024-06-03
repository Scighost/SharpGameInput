using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SharpGameInput
{
    public abstract class GameInputComPtr : SafeHandle, IEquatable<GameInputComPtr>
    {
        public override bool IsInvalid => handle == IntPtr.Zero;

        internal GameInputComPtr(IntPtr handle, bool ownsHandle)
            : base(IntPtr.Zero, ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            return Marshal.Release(handle) >= 0;
        }

        public static bool operator ==(GameInputComPtr? left, GameInputComPtr? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            // if (left is null || right is null)
            //     return false;

            // GameInput interfaces can be compared directly by pointer for equality
            return left?.handle == right?.handle;
        }

        public static bool operator !=(GameInputComPtr? left, GameInputComPtr? right)
            => !(left == right);

        public bool Equals([NotNullWhen(true)] GameInputComPtr? ptr)
            => ptr == this;

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is GameInputComPtr ptr && Equals(ptr);

        public override int GetHashCode()
            => handle.GetHashCode();
    }
}