#nullable enable

using System;
using Iviz.ImageDecoders;
//using MarcusW.VncClient.Protocol.Implementation.Native;

namespace Iviz.ImageDecoders
{
    public readonly struct JpegInfo
    {
        public int Width { get; }
        public int Height { get; }
        public int Subsampling { get; }
        public TurboJpegColorspace Colorspace { get; }
        public TurboJpegPixelFormat Format { get; }

        public int DecompressedSize => Width * Height * 4;

        public JpegInfo(int width, int height, int subsampling, TurboJpegColorspace colorspace, TurboJpegPixelFormat format)
        {
            Width = width;
            Height = height;
            Subsampling = subsampling;
            Colorspace = colorspace;
            Format = format;
        }
    }
    
    public enum TurboJpegPixelFormat
    {
        RGB = 0,
        BGR,
        RGBX,
        BGRX,
        XBGR,
        XRGB,
        Gray,
        RGBA,
        BGRA,
        ABGR,
        ARGB,
        CMYK,
    }
    
    [Flags]
    public enum TurboJpegFlags
    {
        None = 0,
        BottomUp = 2,
        FastUpsample = 256,
        NoRealloc = 1024,
        FastDct = 2048,
        AccurateDct = 4096,
    }    
    
    public enum TurboJpegColorspace
    {
        TJCS_RGB,
        TJCS_YCbCr,
        TJCS_GRAY,
        TJCS_CMYK,
        TJCS_YCCK
    }
}

