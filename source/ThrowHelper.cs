using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpGameInput
{
    internal static class ThrowHelper
    {
        public static void CheckNull([NotNull] object? obj, [CallerArgumentExpression(nameof(obj))] string name = "")
        {
            if (obj is null)
                throw new ArgumentNullException(name);
        }

        public static unsafe void CheckNull([NotNull] void* ptr, [CallerArgumentExpression(nameof(ptr))] string name = "")
        {
            if (ptr == null)
                throw new ArgumentNullException(name);
        }

        public static void CheckDisposed([DoesNotReturnIf(true)] bool disposed, string name)
        {
            if (disposed)
                throw new ObjectDisposedException(name);
        }

        public static void CheckDisposed([NotNull] object? obj, [CallerArgumentExpression(nameof(obj))] string name = "")
            => CheckDisposed(obj is null, name);

        public static void CheckHandle([NotNull] SafeHandle? handle, [CallerArgumentExpression(nameof(handle))] string name = "")
        {
            CheckNull(handle, name);
            if (handle!.IsInvalid || handle.IsClosed)
                throw new ObjectDisposedException(name);
        }

        public static void CheckRange(int index, int size, [CallerArgumentExpression(nameof(index))] string name = "")
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(name);
        }
    }
}