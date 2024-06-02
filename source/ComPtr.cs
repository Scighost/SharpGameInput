using System;
using System.Runtime.InteropServices;

namespace SharpGameInput
{
    public abstract class GameInputComPtr : SafeHandle
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
    }
}