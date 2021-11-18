#nullable enable

using System;
using System.IO;
using System.Threading.Tasks;
using BitMiracle.LibJpeg;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Buffer = System.Buffer;

namespace Iviz.Displays
{
    public sealed class ImageTexture
    {
        static readonly int IntensityCoeffID = Shader.PropertyToID("_IntensityCoeff");
        static readonly int IntensityAddID = Shader.PropertyToID("_IntensityAdd");
        static readonly int IntensityID = Shader.PropertyToID("_IntensityTexture");
        static readonly int MainTexID = Shader.PropertyToID("_MainTex");

        byte[]? bitmapBuffer;
        Vector2 intensityBounds;
        bool flipMinMax;

        public event Action<Texture2D?>? TextureChanged;
        public event Action<Texture2D?>? ColormapChanged;

        public Vector2 IntensityBounds
        {
            get => intensityBounds;
            set
            {
                intensityBounds = value;
                float intensitySpan = intensityBounds.y - intensityBounds.x;

                if (intensitySpan == 0)
                {
                    Material.SetFloat(IntensityCoeffID, 1);
                    Material.SetFloat(IntensityAddID, 0);
                }
                else
                {
                    if (!FlipMinMax)
                    {
                        Material.SetFloat(IntensityCoeffID, 1 / intensitySpan);
                        Material.SetFloat(IntensityAddID, -intensityBounds.x / intensitySpan);
                    }
                    else
                    {
                        Material.SetFloat(IntensityCoeffID, -1 / intensitySpan);
                        Material.SetFloat(IntensityAddID, intensityBounds.y / intensitySpan);
                    }
                }
            }
        }

        public bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
                IntensityBounds = IntensityBounds;
            }
        }

        public Texture2D? Texture { get; private set; }
        public Material Material { get; }
        public string Description { get; private set; } = "";
        public bool IsMono { get; private set; }
        public int Width => Texture != null ? Texture.width : 0;
        public int Height => Texture != null ? Texture.height : 0;

        ColormapId colormap;

        public ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;

                Material.SetTexture(IntensityID, ColormapTexture);
                ColormapChanged?.Invoke(ColormapTexture);
            }
        }

        public Texture2D ColormapTexture => Resource.Colormaps.Textures[Colormap];

        public ImageTexture()
        {
            Material = Resource.Materials.ImagePreview.Instantiate();
        }

        static int? FieldSizeFromEncoding(string encoding)
        {
            switch (encoding.ToUpperInvariant())
            {
                case "32FC":
                case "32FC1":
                case "RGBA":
                case "BGRA":
                case "RGBA8":
                case "BGRA8":
                case "8SC4":
                case "8UC4":
                    return 4;
                case "RGB":
                case "BGR":
                case "RGB8":
                case "BGR8":
                case "8SC3":
                case "8UC3":
                    return 3;
                case "MONO16":
                case "16UC":
                case "16UC1":
                case "16SC":
                case "16SC1":
                    return 2;
                case "MONO8":
                case "8UC":
                case "8UC1":
                case "8SC":
                case "8SC1":
                    return 1;
                default:
                    return null;
            }
        }

        static string? EncodingFromPng(BigGustave.Png png)
        {
            switch (png.Header.ColorType)
            {
                case BigGustave.ColorType.None:
                    switch (png.Header.BitDepth)
                    {
                        case 8: return "mono8";
                        case 16: return "mono16";
                    }

                    break;
                case BigGustave.ColorType.ColorUsed:
                    switch (png.Header.BitDepth)
                    {
                        case 8: return "rgb8";
                        case 16: return "rgb16";
                    }

                    break;
                case BigGustave.ColorType.AlphaChannelUsed:
                    switch (png.Header.BitDepth)
                    {
                        case 8: return "rgba8";
                        case 16: return "rgba16";
                    }

                    break;
            }

            return null;
        }

        public void ProcessPng(byte[] data, Action onFinished)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (onFinished == null)
            {
                throw new ArgumentNullException(nameof(onFinished));
            }

            Task.Run(() =>
            {
                try
                {
                    var png = BigGustave.Png.Open(data);
                    string? encoding = EncodingFromPng(png);
                    if (encoding == null)
                    {
                        RosLogger.Error($"{this}: Ignoring PNG with unsupported encoding '{png.Header.ColorType}'");
                        return;
                    }

                    byte[] newData;
                    if (png.RowOffset != 0)
                    {
                        int reqSize = png.Height * png.RowSize;
                        if (bitmapBuffer == null || bitmapBuffer.Length < reqSize)
                        {
                            bitmapBuffer = new byte[reqSize];
                        }

                        int srcOffset = png.RowOffset;
                        int dstOffset = 0;
                        int rowSize = png.RowSize;
                        for (int i = png.Height; i != 0; i--, srcOffset += png.RowStep, dstOffset += rowSize)
                        {
                            Buffer.BlockCopy(png.Data, srcOffset, bitmapBuffer, dstOffset, rowSize);
                        }

                        newData = bitmapBuffer;
                    }
                    else
                    {
                        newData = png.Data;
                    }

                    GameThread.PostInListenerQueue(() =>
                    {
                        try
                        {
                            Set(png.Width, png.Height, encoding, newData);
                        }
                        finally
                        {
                            onFinished();
                        }
                    });
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error processing PNG", e);
                    GameThread.PostInListenerQueue(onFinished);
                }
            });
        }

        public void ProcessJpg(byte[] data, Action onFinished)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (onFinished == null)
            {
                throw new ArgumentNullException(nameof(onFinished));
            }

            Task.Run(() =>
            {
                try
                {
                    JpegImage image = new JpegImage(new MemoryStream(data));

                    string? encoding;
                    int reqSize = image.Height * image.Width;
                    switch (image.Colorspace)
                    {
                        case Colorspace.RGB when image.BitsPerComponent == 8:
                        {
                            if (image.Width % 4 != 0)
                            {
                                RosLogger.Debug($"{this}: Row padding not implemented");
                                return;
                            }

                            encoding = "rgb";
                            reqSize *= 3;
                            break;
                        }
                        case Colorspace.Grayscale when image.BitsPerComponent == 8:
                        {
                            if (image.Width % 4 != 0)
                            {
                                RosLogger.Debug($"{this}: Row padding not implemented");
                                return;
                            }

                            encoding = "mono8";
                            break;
                        }
                        case Colorspace.Grayscale when image.BitsPerComponent == 16:
                        {
                            if (image.Width % 2 != 0)
                            {
                                RosLogger.Debug($"{this}: Row padding not implemented");
                                return;
                            }

                            encoding = "mono16";
                            reqSize *= 2;
                            break;
                        }
                        default:
                            encoding = null;
                            break;
                    }

                    if (encoding == null)
                    {
                        RosLogger.Debug(
                            $"{this}: Unsupported encoding '{image.Colorspace}' with size {image.BitsPerComponent}");
                        return;
                    }

                    const int bmpHeaderLength = 54;
                    reqSize += bmpHeaderLength;

                    if (bitmapBuffer == null || bitmapBuffer.Length < reqSize)
                    {
                        bitmapBuffer = new byte[reqSize];
                    }

                    using (var outStream = new MemoryStream(bitmapBuffer))
                    {
                        image.WriteBitmap(outStream);
                    }

                    GameThread.PostInListenerQueue(() =>
                    {
                        try
                        {
                            Set(image.Width, image.Height, encoding, bitmapBuffer.AsSegment(bmpHeaderLength));
                        }
                        finally
                        {
                            onFinished();
                        }
                    });
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error processing JPG", e);
                    GameThread.PostInListenerQueue(onFinished);
                }
            });
        }

        public void Set(int width, int height, string encoding, byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Set(width, height, encoding, data.AsSegment());
        }

        public void Set(int width, int height, string encoding, in ArraySegment<byte> data,
            bool generateMipmaps = false)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            int size = width * height;
            int? bpp = FieldSizeFromEncoding(encoding);

            if (bpp == null)
            {
                RosLogger.Debug($"{this}: Unsupported encoding '{encoding}'");
                return;
            }

            if (data.Count < size * bpp)
            {
                RosLogger.Debug(
                    $"{this}: Invalid image! Expected at least {(size * bpp).ToString()} bytes, " +
                    $"received {data.Count.ToString()}");
                return;
            }

            Description = $"{width.ToString()}x{height.ToString()} pixels | {encoding}";

            switch (encoding.ToUpperInvariant())
            {
                case "RGBA":
                case "RGBA8":
                case "8SC4":
                case "8UC4":
                case "RGB":
                case "RGB8":
                case "8SC3":
                case "8UC3":
                    IsMono = false;
                    Material.DisableKeyword("USE_INTENSITY");
                    Material.DisableKeyword("FLIP_RB");
                    break;
                case "BGRA":
                case "BGRA8":
                case "BGR":
                case "BGR8":
                    IsMono = false;
                    Material.DisableKeyword("USE_INTENSITY");
                    Material.EnableKeyword("FLIP_RB");
                    break;
                case "MONO16":
                case "16UC":
                case "16UC1":
                case "16SC":
                case "16SC1":
                case "MONO8":
                case "8UC":
                case "8UC1":
                case "8SC":
                case "8SC1":
                case "32FC":
                case "32FC1":
                    IsMono = true;
                    Material.EnableKeyword("USE_INTENSITY");
                    break;
                default:
                    return;
            }

            ApplyTexture(width, height, data, encoding, size * bpp.Value, generateMipmaps);
        }

        void ApplyTexture(int width, int height, in ArraySegment<byte> data, string encoding, int length,
            bool generateMipmaps)
        {
            bool alreadyCopied = false;
            Texture2D texture;
            switch (encoding.ToUpperInvariant())
            {
                case "RGB8":
                case "BGR8":
                case "RGB":
                case "BGR":
                case "8SC3":
                case "8UC3":
                    if (!Settings.SupportsRGB24)
                    {
                        texture = EnsureSize(width, height, TextureFormat.RGBA32);
                        CopyRgb24ToRgba32(data, texture.GetRawTextureData<byte>(), length);
                        alreadyCopied = true;
                    }
                    else
                    {
                        texture = EnsureSize(width, height, TextureFormat.RGB24);
                    }

                    break;
                case "RGBA8":
                case "BGRA8":
                case "BGRA":
                case "RGBA":
                case "8SC4":
                case "8UC4":
                    texture = EnsureSize(width, height, TextureFormat.RGBA32);
                    break;
                case "MONO16":
                case "16UC1":
                case "16SC1":
                case "16UC":
                case "16SC":
                    if (!Settings.SupportsR16)
                    {
                        texture = EnsureSize(width, height, TextureFormat.R8);
                        CopyR16ToR8(data, texture.GetRawTextureData<byte>(), length);
                        alreadyCopied = true;
                    }
                    else
                    {
                        texture = EnsureSize(width, height, TextureFormat.R16);
                    }

                    break;
                case "MONO8":
                case "8UC1":
                case "8UC":
                case "8SC1":
                case "8SC":
                    texture = EnsureSize(width, height, TextureFormat.R8);
                    break;
                case "32FC1":
                case "32FC":
                    texture = EnsureSize(width, height, TextureFormat.RFloat);
                    break;
                default:
                    return;
            }

            if (!alreadyCopied)
            {
                NativeArray<byte>.Copy(data.Array, data.Offset, texture.GetRawTextureData<byte>(), 0, length);
            }

            texture.Apply(generateMipmaps);
        }

        unsafe void CopyR16ToR8(in ArraySegment<byte> src, NativeArray<byte> dst, int lengthInBytes)
        {
            if (src.Array == null)
            {
                throw new NullReferenceException($"{this}: Source array in Copy() was null");
            }

            int numElements = lengthInBytes / 2;
            if (src.Offset + lengthInBytes > src.Count || numElements > dst.Length)
            {
                throw new InvalidOperationException($"{this}: Skipping copy. Possible buffer overflow.");
            }

            byte* dstPtr = (byte*)dst.GetUnsafePtr();
            fixed (byte* srcPtr = &src.Array[src.Offset])
            {
                byte* srcPtrOff = srcPtr + 1;

                for (int i = numElements; i >= 0; i--)
                {
                    *dstPtr = *srcPtrOff;
                    dstPtr++;
                    srcPtrOff += 2;
                }
            }
        }

        unsafe void CopyRgb24ToRgba32(in ArraySegment<byte> src, NativeArray<byte> dst, int lengthInBytes)
        {
            if (src.Array == null)
            {
                throw new NullReferenceException($"{this}: Source array in Copy() was null");
            }

            int numElements = lengthInBytes / 3;
            if (src.Offset + lengthInBytes > src.Count || numElements * 4 > dst.Length)
            {
                throw new InvalidOperationException($"{this}: Skipping copy. Possible buffer overflow.");
            }

            byte* dstPtr = (byte*)dst.GetUnsafePtr();
            fixed (byte* srcPtr0 = &src.Array[src.Offset])
            {
                byte* srcPtr = srcPtr0;

                for (int i = numElements; i >= 0; i--)
                {
                    *dstPtr++ = *srcPtr++;
                    *dstPtr++ = *srcPtr++;
                    *dstPtr++ = *srcPtr++;
                    *dstPtr++ = 255;
                }
            }
        }

        Texture2D EnsureSize(int width, int height, TextureFormat format)
        {
            if (Texture != null &&
                Texture.width == width &&
                Texture.height == height &&
                Texture.format == format)
            {
                return Texture;
            }

            if (Texture != null)
            {
                UnityEngine.Object.Destroy(Texture);
            }

            Texture = new Texture2D(width, height, format, false);
            Material.SetTexture(MainTexID, Texture);
            TextureChanged?.Invoke(Texture);
            return Texture;
        }

        public void Reset()
        {
            if (Texture == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(Texture);
            Texture = null;
            Material.SetTexture(MainTexID, Texture);
            TextureChanged?.Invoke(Texture);
        }

        public void Stop()
        {
            TextureChanged?.Invoke(null);
            TextureChanged = null;
            ColormapChanged?.Invoke(null);
            ColormapChanged = null;
        }

        public void Destroy()
        {
            if (Texture != null)
            {
                UnityEngine.Object.Destroy(Texture);
            }

            UnityEngine.Object.Destroy(Material);
        }
    }
}