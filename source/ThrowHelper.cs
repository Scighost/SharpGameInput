using System;

namespace SharpGameInput;

internal static class ThrowHelper
{
    public static void CheckRange(int index, int size)
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException(nameof(index));
    }
}