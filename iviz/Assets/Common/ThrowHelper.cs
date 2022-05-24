#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Iviz.Core
{
    public static class ThrowHelper
    {
        public static void ThrowIfNull([System.Diagnostics.CodeAnalysis.NotNull] UnityEngine.Object? t, string nameOfT)
        {
            if (t == null)
            {
                ThrowArgumentNull(nameOfT);
            }
        }

        public static void ThrowIfNull([System.Diagnostics.CodeAnalysis.NotNull] object? t, string nameOfT)
        {
            if (t is null)
            {
                ThrowArgumentNull(nameOfT);
            }
        }

        public static void ThrowIfNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNull] string? t, string nameOfT)
        {
            if (string.IsNullOrWhiteSpace(t))
            {
                ThrowArgumentNull(nameOfT, "Argument cannot be null or empty");
            }
        }

        [DoesNotReturn]
        public static void ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();

        [DoesNotReturn]
        public static void ThrowArgumentOutOfRange(string arg) => throw new ArgumentOutOfRangeException(arg);

        [DoesNotReturn]
        static void ThrowArgumentNull(string paramName) => throw new ArgumentNullException(paramName);

        [DoesNotReturn, AssertionMethod]
        public static void ThrowArgumentNull(string paramName, string message) =>
            throw new ArgumentNullException(paramName, message);

        [DoesNotReturn]
        public static void ThrowMissingAssetField(string message) => throw new MissingAssetFieldException(message);

        [DoesNotReturn, AssertionMethod]
        public static void ThrowArgument(string message, string paramName) =>
            throw new ArgumentException(message, paramName);
    }
}