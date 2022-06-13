#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using MarcusW.VncClient.Protocol.Implementation.Native;

namespace Iviz.ImageDecoders
{
    public enum TurboJpegColorspace
    {
        /// <summary>
        /// RGB colorspace.
        /// When compressing the JPEG image, the R, G, and B components in the source image
        /// are reordered into image planes, but no colorspace conversion or subsampling is
        /// performed. RGB JPEG images can be decompressed to any of the extended RGB pixel
        /// formats or grayscale, but they cannot be decompressed to YUV images.
        /// </summary>
        TJCS_RGB,

        /// <summary>
        /// YCbCr colorspace.
        /// YCbCr is not an absolute colorspace but rather a mathematical transformation of
        /// RGB designed solely for storage and transmission. YCbCr images must be converted
        /// to RGB before they can actually be displayed. In the YCbCr colorspace, the Y (luminance)
        /// component represents the black & white portion of the original image, and the Cb
        /// and Cr (chrominance) components represent the color portion of the original image.
        /// Originally, the analog equivalent of this transformation allowed the same signal
        /// to drive both black & white and color televisions, but JPEG images use YCbCr
        /// primarily because it allows the color data to be optionally subsampled for the
        /// purposes of reducing bandwidth or disk space. YCbCr is the most common JPEG
        /// colorspace, and YCbCr JPEG images can be compressed from and decompressed to any
        /// of the extended RGB pixel formats or grayscale, or they can be decompressed to
        /// YUV planar images. 
        /// </summary>
        TJCS_YCbCr,

        /// <summary>
        /// Grayscale colorspace.
        /// The JPEG image retains only the luminance data (Y component), and any color data
        /// from the source image is discarded. Grayscale JPEG images can be compressed from
        /// and decompressed to any of the extended RGB pixel formats or grayscale, or they
        /// can be decompressed to YUV planar images.
        /// </summary>
        TJCS_GRAY,

        /// <summary>
        /// CMYK colorspace.
        /// When compressing the JPEG image, the C, M, Y, and K components in the source
        /// image are reordered into image planes, but no colorspace conversion or subsampling
        /// is performed. CMYK JPEG images can only be decompressed to CMYK pixels.
        /// </summary>
        TJCS_CMYK,

        /// <summary>
        /// YCCK colorspace.
        /// YCCK (AKA "YCbCrK") is not an absolute colorspace but rather a mathematical
        /// transformation of CMYK designed solely for storage and transmission. It is to
        /// CMYK as YCbCr is to RGB. CMYK pixels can be reversibly transformed into YCCK,
        /// and as with YCbCr, the chrominance components in the YCCK pixels can be subsampled
        /// without incurring major perceptual loss. YCCK JPEG images can only be compressed
        /// from and decompressed to CMYK pixels.
        /// </summary>
        TJCS_YCCK
    }

    /// <summary>
    /// Bindings for the JPEG library wrapped in an interface. Also used by the VNC module.
    /// </summary>
    public sealed class TurboJpegUnityWrapper : ITurboJpeg
    {
        public IntPtr InitDecompressorInstance() =>
#if UNITY_IOS
            TurboJpegNativeIOS.InitDecompressorInstance();
#else
            TurboJpegNative.InitDecompressorInstance();
#endif

        public int DestroyInstance(IntPtr handle) =>
#if UNITY_IOS
            TurboJpegNativeIOS.DestroyInstance(handle);
#else
            TurboJpegNative.DestroyInstance(handle);
#endif

        public int DecompressHeader(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, out int width,
            out int height, out int subsampling, out int colorspace) =>
#if UNITY_IOS
            TurboJpegNativeIOS.DecompressHeader(handle, jpegBuf, jpegSize, out width, out height, out subsampling,
                out colorspace);
#else
            TurboJpegNative.DecompressHeader(handle, jpegBuf, jpegSize, out width, out height, out subsampling,
                out colorspace);
#endif

        public int Decompress(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, IntPtr dstBuf, int width,
            int pitch, int height, int pixelFormat, int flags) =>
#if UNITY_IOS
            TurboJpegNativeIOS.Decompress(handle, jpegBuf, jpegSize, dstBuf, width, pitch, height, pixelFormat, flags);
#else
            TurboJpegNative.Decompress(handle, jpegBuf, jpegSize, dstBuf, width, pitch, height, pixelFormat, flags);
#endif

        public IntPtr GetLastError() =>
#if UNITY_IOS
            TurboJpegNativeIOS.GetLastError();
#else
            TurboJpegNative.GetLastError();
#endif

        public IntPtr GetLastError(IntPtr handle) =>
#if UNITY_IOS
            TurboJpegNativeIOS.GetLastError(handle);
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

#if UNITY_IOS
    /// <summary>
    /// Native bindings for the turbojpeg decoder, iOS version.
    /// Names (entry points) changed because Unity has its own version of turbojpeg and the names would collide.
    /// </summary>
    internal static class TurboJpegNativeIOS
    {
        const string Library = "__Internal";

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

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IvizDecompress")]
        public static extern int Decompress(IntPtr handle, IntPtr jpegBuf, ulong jpegSize, IntPtr dstBuf,
            int width, int pitch, int height, int pixelFormat, int flags);
    }
#endif
}