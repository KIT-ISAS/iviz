using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Iviz.Roslib2.RclInterop.Wrappers;
using Iviz.Tools;

namespace Iviz.Roslib2.RclInterop;

public delegate void LoggingHandler(int severity, IntPtr name, long timestamp, IntPtr message);

public delegate void CdrDeserializeCallback(IntPtr contextHandle, IntPtr ptr, int length);

public delegate void CdrSerializeCallback(IntPtr contextHandle, IntPtr ptr, int length);

public delegate int CdrGetSerializedSizeCallback(IntPtr contextHandle);

internal static class Rcl
{
    static IRclWrapper? impl;
    static LoggingHandler? consoleLoggingHandlerDel;
    static CdrGetSerializedSizeCallback? cdrGetSerializedSizeCallbackDel;
    static CdrSerializeCallback? cdrSerializeCallbackDel;
    static CdrDeserializeCallback? cdrDeserializeCallbackDel;
    
    static IRclWrapper ThrowMissingWrapper() => throw new NullReferenceException("Rcl wrapper has not been set!");

    public static IRclWrapper Impl => impl ?? ThrowMissingWrapper();

    public static void SetRclWrapper(IRclWrapper rclWrapper)
    {
        impl = rclWrapper;
    }
    
    public static void SetLoggingHandler()
    {
        consoleLoggingHandlerDel = ConsoleLoggingHandler;
        Impl.SetLoggingHandler(consoleLoggingHandlerDel);
    }

    public static void SetMessageCallbacks()
    {
        cdrDeserializeCallbackDel = CdrDeserializeCallback;
        cdrSerializeCallbackDel = CdrSerializeCallback;
        cdrGetSerializedSizeCallbackDel = CdrGetSerializedSizeCallback;
        
        Impl.SetMessageCallbacks(cdrDeserializeCallbackDel, cdrSerializeCallbackDel, cdrGetSerializedSizeCallbackDel);
    }

    public static string ToString(IntPtr ptr)
    {
        try
        {
            return Marshal.PtrToStringUTF8(ptr) ?? "";
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat(nameof(Rcl) + ": Exception in " + nameof(ToString) + " {0}", e);
            return "";
        }
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ReadOnlySpan<byte> CreateReadOnlyByteSpan(IntPtr ptr, int size)
    {
        return new ReadOnlySpan<byte>(ptr.ToPointer(), size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref byte GetReference(IntPtr ptr) => ref *(byte*)ptr;

    public static void Check(IntPtr context, int result)
    {
        if (result == (int)RclRet.Ok)
        {
            return;
        }

        string msg = ToString(Impl.GetErrorString(context));
        throw new RosRclException(msg, result);
    }

    public static void Check(int result)
    {
        if (result == (int)RclRet.Ok)
        {
            return;
        }

        throw new RosRclException("Rcl operation failed", result);
    }
    
    [MonoPInvokeCallback(typeof(CdrDeserializeCallback))]
    static void CdrDeserializeCallback(IntPtr messageContextHandle, IntPtr ptr, int length)
    {
        var gcHandle = (GCHandle)messageContextHandle;
        var handler = (IRclDeserializeHandler?)gcHandle.Target;

        try
        {
            handler!.DeserializeFrom(ptr, length);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat(nameof(Rcl) + ": Exception in " + nameof(CdrDeserializeCallback) + " {0}", e);
        }
    }
    
    [MonoPInvokeCallback(typeof(CdrSerializeCallback))]
    static void CdrSerializeCallback(IntPtr messageContextHandle, IntPtr ptr, int length)
    {
        var gcHandle = (GCHandle)messageContextHandle;
        var handler = (IRclSerializeHandler?)gcHandle.Target;

        try
        {
            handler!.SerializeInto(ptr, length);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat(nameof(Rcl) + ": Exception in " + nameof(CdrSerializeCallback) + " {0}", e);
        }
    }
    
    [MonoPInvokeCallback(typeof(CdrGetSerializedSizeCallback))]
    static int CdrGetSerializedSizeCallback(IntPtr messageContextHandle)
    {
        var gcHandle = (GCHandle)messageContextHandle;
        var handler = (IRclSerializeHandler?)gcHandle.Target;

        try
        {
            return handler!.GetSerializedSize();
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat(nameof(Rcl) + ": Exception in " + nameof(CdrGetSerializedSizeCallback) + " {0}", e);
            return 0;
        }
    }


    [MonoPInvokeCallback(typeof(LoggingHandler))]
    static void ConsoleLoggingHandler(int severity, IntPtr name, long timestamp, IntPtr message)
    {
        switch (severity)
        {
            case <= (int)RclLogSeverity.Debug:
                if (Logger.LogDebugCallback != null)
                {
                    Logger.LogDebugFormat("[{0}]: {1}", ToString(name), ToString(message));
                }

                break;
            case <= (int)RclLogSeverity.Warn:
                if (Logger.LogCallback != null)
                {
                    Logger.LogFormat("[{0}]: {1}", ToString(name), ToString(message));
                }

                break;
            case <= (int)RclLogSeverity.Fatal:
                if (Logger.LogErrorCallback != null)
                {
                    Logger.LogErrorFormat("[{0}]: {1}", ToString(name), ToString(message));
                }

                break;
        }
    }
}