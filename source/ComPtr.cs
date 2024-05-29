using System;
using System.Runtime.InteropServices;

namespace SharpGameInput
{
    public abstract class ComPtr : SafeHandle
    {
        public override bool IsInvalid => handle == IntPtr.Zero;

        internal ComPtr(IntPtr handle, bool ownsHandle)
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