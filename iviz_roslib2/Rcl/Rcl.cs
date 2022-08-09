using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Roslib2.Rcl.Wrappers;

namespace Iviz.Roslib2.Rcl;

public delegate void LoggingHandler(int severity, IntPtr name, long timestamp, IntPtr message);

internal static class Rcl
{
    static IRclWrapper? impl;

    public static IRclWrapper Impl => impl ?? ThrowMissingWrapper();

    static IRclWrapper ThrowMissingWrapper() => throw new NullReferenceException("Rcl wrapper has not been set!");

    public static void SetRclWrapper(IRclWrapper rclWrapper)
    {
        impl = rclWrapper;
    }

    public const int Ok = (int)RclRet.Ok;

    public static string ToString(IntPtr ptr)
    {
        return Marshal.PtrToStringUTF8(ptr) ?? "";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Span<T> CreateSpan<T>(IntPtr ptr, int size) where T : unmanaged
    {
        return new Span<T>(ptr.ToPointer(), size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Span<IntPtr> CreateIntPtrSpan(IntPtr ptr, int size)
    {
        return new Span<IntPtr>(ptr.ToPointer(), size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Span<byte> CreateByteSpan(IntPtr ptr, int size)
    {
        return new Span<byte>(ptr.ToPointer(), size);
    }

    public static void Check(IntPtr context, int result)
    {
        if (result == Ok)
        {
            return;
        }

        string msg = ToString(Impl.GetErrorString(context));
        throw new RosRclException(msg, result);
    }

    public static void Check(int result)
    {
        if (result == Ok)
        {
            return;
        }

        throw new RosRclException("Rcl operation failed", result);
    }
}