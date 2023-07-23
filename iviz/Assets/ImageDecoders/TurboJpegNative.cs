#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
//using MarcusW.VncClient.Protocol.Implementation.Native;

namespace Iviz.ImageDecoders
{
    /// <summary>
    /// Bindings for the JPEG library wrapped in an interface. Also used by the VNC module.
    /// </summary>
    public sealed class TurboJpegUnityWrapper // : ITurboJpeg
    {
        public IntPtr InitDecompressorInstance() =>
#if UNITY_IOS || UNITY_ANDROID
            TurboJpegNativeMobile.InitDecompressorInstance();
#else
            TurboJpegNative.InitDecompressorInstance();
#endif

        public int DestroyInstance(IntPtr handle) =>
#if UNITY_IOS || UNITY_ANDROID
            TurboJpegNativeMobile.DestroyInstance(handle);
#else
            TurboJpegNative.DestroyInstance(handle);
#endif

        public int DecompressHeader(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, out int width,
            out int height, out int subsampling, out int colorspace) =>
#if UNITY_IOS || UNITY_ANDROID
            TurboJpegNativeMobile.DecompressHeader(handle, jpegBuf, jpegSize, out width, out height, out subsampling,
                out colorspace);
#else
            TurboJpegNative.DecompressHeader(handle, jpegBuf, jpegSize, out width, out height, out subsampling,
                out colorspace);
#endif

        public int Decompress(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, IntPtr dstBuf, int width,
            int pitch, int height, int pixelFormat, int flags) =>
#if UNITY_IOS || UNITY_ANDROID
            TurboJpegNativeMobile.Decompress(handle, jpegBuf, jpegSize, dstBuf, width, pitch, height, pixelFormat,
                flags);
#else
            TurboJpegNative.Decompress(handle, jpegBuf, jpegSize, dstBuf, width, pitch, height, pixelFormat, flags);
#endif

        public IntPtr GetLastError() =>
#if UNITY_IOS || UNITY_ANDROID
            TurboJpegNativeMobile.GetLastError();
#else
            TurboJpegNative.GetLastError();
#endif

        public IntPtr GetLastError(IntPtr handle) =>
#if UNITY_IOS || UNITY_ANDROID
            TurboJpegNativeMobile.GetLastError(handle);
#else
            TurboJpegNative.GetLastError(handle);
#endif
    }

    /// <summary>
    /// Native bindings for the turbojpeg decoder, standalone / editor version.
    /// This code was adapted from the <see cref="TurboJpeg"/> class in the <see cref="MarcusW.VncClient"/> namespace.
    /// Original from https://github.com/MarcusWichelmann/MarcusW.VncClient
    /// </summary>
    internal static class TurboJpegNative
    {
        const string Library = "turbojpeg";

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tjInitDecompress")]
        public static extern IntPtr InitDecompressorInstance();

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tjDestroy")]
        public static extern int DestroyInstance(IntPtr handle);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tjGetErrorStr")]
        public static extern IntPtr GetLastError();

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tjGetErrorStr2")]
        public static extern IntPtr GetLastError(IntPtr handle);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tjDecompressHeader3")]
        public static extern int DecompressHeader(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, out int width,
            out int height, out int jpegSubsamp, out int colorspace);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tjDecompress2")]
        public static extern int Decompress(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, IntPtr dstBuf,
            int width, int pitch, int height, int pixelFormat, int flags);
    }

    /// <summary>
    /// Native bindings for the turbojpeg decoder, iOS version.
    /// Names (entry points) changed because Unity has its own version of turbojpeg and the names would collide.
    /// </summary>
    internal static class TurboJpegNativeMobile
    {
        const string Library = Settings.IsIPhone
            ? "__Internal"
            : "iviz_jpegturbo";

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IvizInitDecompress")]
        public static extern IntPtr InitDecompressorInstance();

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IvizDestroy")]
        public static extern int DestroyInstance(IntPtr handle);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IvizGetErrorStr")]
        public static extern IntPtr GetLastError();

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IvizGetErrorStr2")]
        public static extern IntPtr GetLastError(IntPtr handle);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IvizDecompressHeader3")]
        public static extern int DecompressHeader(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, out int width,
            out int height, out int jpegSubsamp, out int jpegColorSpace);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IvizDecompress2")]
        public static extern int Decompress(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, IntPtr dstBuf,
            int width, int pitch, int height, int pixelFormat, int flags);
    }
}