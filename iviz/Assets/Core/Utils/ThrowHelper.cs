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
                ThrowNull(nameOfT);
            }
        }

        public static void ThrowIfNull([NotNull] object? t, string nameOfT)
        {
            if (t is null)
            {
                ThrowNull(nameOfT);
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
        static void ThrowNull(string paramName) => throw new ArgumentNullException(paramName);
    }
}