#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;

namespace Iviz.Core
{
    public static class ThrowHelper
    {
        public static void ThrowIfNull([NotNull] UnityEngine.Object? t, string nameOfT)
        {
            if (t == null)
            {
                ThrowArgumentNull(nameOfT);
            }
        }

        public static void ThrowIfNull([NotNull] object? t, string nameOfT)
        {
            if (t is null)
            {
                ThrowArgumentNull(nameOfT);
            }
        }

        public static void ThrowIfNullOrEmpty([NotNull] string? t, string nameOfT)
        {
            if (string.IsNullOrEmpty(t))
            {
                throw new ArgumentException("Argument '" + nameOfT + "' cannot be null or empty");
            }
        }

        [DoesNotReturn]
        public static void ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();

        [DoesNotReturn]
        public static void ThrowArgumentOutOfRange() => throw new IndexOutOfRangeException();

        [DoesNotReturn]
        static void ThrowArgumentNull(string paramName) => throw new ArgumentNullException(paramName);
    }
}