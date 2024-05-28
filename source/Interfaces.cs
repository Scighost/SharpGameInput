using System;
using System.Runtime.InteropServices;

namespace SharpGameInput
{
    public abstract class InterfaceHandle : SafeHandle
    {
        public override bool IsInvalid => handle == IntPtr.Zero;

        internal InterfaceHandle(IntPtr handle)
            : base(IntPtr.Zero, ownsHandle: true) // Handle is always owned in our case
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            Marshal.Release(handle);
            return true;
        }
    }
}
