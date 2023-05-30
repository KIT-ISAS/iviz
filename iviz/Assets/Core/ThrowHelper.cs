#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using Iviz.Msgs;
using JetBrains.Annotations;

namespace Iviz.Core
{
    public static class ThrowHelper
    {
        public static void ThrowIfNull([System.Diagnostics.CodeAnalysis.NotNull] UnityEngine.Object? t, string nameOfT)
        {
            if (t == null)
            {
                BuiltIns.ThrowArgumentNull(nameOfT);
            }
        }

        public static void ThrowIfNull([System.Diagnostics.CodeAnalysis.NotNull] object? t, string nameOfT)
        {
            if (t is null)
            {
                BuiltIns.ThrowArgumentNull(nameOfT);
            }
        }

        public static void ThrowIfNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNull] string? t, string nameOfT)
        {
            if (t is not { Length: not 0 })
            {
                BuiltIns.ThrowArgumentNull(nameOfT, "Argument cannot be null or empty");
            }
        }

        [DoesNotReturn]
        public static void ThrowArgumentOutOfRange() => BuiltIns.ThrowArgumentOutOfRange();

        [DoesNotReturn]
        public static void ThrowArgumentOutOfRange(string arg) => BuiltIns.ThrowArgumentOutOfRange(arg);

        [DoesNotReturn]
        public static void ThrowMissingAssetField(string message) => throw new MissingAssetFieldException(message);

        [DoesNotReturn, AssertionMethod]
        public static void ThrowArgument(string message, string paramName) =>
            BuiltIns.ThrowArgument(message, paramName);

        [DoesNotReturn]
        public static void ThrowInvalidOperation(string message) => throw new InvalidOperationException(message);

        [DoesNotReturn, AssertionMethod]
        public static void ThrowArgumentNull(string arg, string message) => BuiltIns.ThrowArgumentNull(arg, message);

        [DoesNotReturn, AssertionMethod]
        public static object ThrowObjectDisposed(string name, string? message = null) =>
            throw new ObjectDisposedException(name, message);
    }
}