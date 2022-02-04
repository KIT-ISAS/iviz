#nullable enable

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BitMiracle.LibJpeg;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
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

        byte[] bitmapBuffer = Array.Empty<byte>();
        Vector2 normalizedIntensityBounds;
        ColormapId colormap;
        float normalizationFactor = 1;
        bool flipMinMax;
        bool overrideIntensityBounds;

        public event Action<Texture2D?>? TextureChanged;

        public Vector2 IntensityBounds
        {
            get => NormalizedIntensityBounds / normalizationFactor;
            set => NormalizedIntensityBounds = value * normalizationFactor;
        }

        public Vector2 NormalizedIntensityBounds
        {
            get => normalizedIntensityBounds;
            set
            {
                normalizedIntensityBounds = value;

                float intensitySpan = normalizedIntensityBounds.y - normalizedIntensityBounds.x;

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
                        Material.SetFloat(IntensityAddID, -normalizedIntensityBounds.x / intensitySpan);
                    }
                    else
                    {
                        Material.SetFloat(IntensityCoeffID, -1 / intensitySpan);
                        Material.SetFloat(IntensityAddID, normalizedIntensityBounds.y / intensitySpan);
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
                NormalizedIntensityBounds = NormalizedIntensityBounds;
            }
        }

        public Texture2D? Texture { get; private set; }
        public Material Material { get; }
        public string Description { get; private set; } = "";
        public bool IsMono { get; private set; }
        public int Width => Texture != null ? Texture.width : 0;
        public int Height => Texture != null ? Texture.height : 0;
        public Vector2? MeasuredIntensityBounds { get; private set; }

        public bool OverrideIntensityBounds
        {
            get => overrideIntensityBounds;
            set
            {
                overrideIntensityBounds = value;
                if (!value && MeasuredIntensityBounds is { } bounds)
                {
                    IntensityBounds = bounds;
                }
            }
        }

        public ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;
                Material.SetTexture(IntensityID, ColormapTexture);
            }
        }

        Texture2D ColormapTexture => Resource.Colormaps.Textures[Colormap];

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
            return png.Header.ColorType switch
            {
                BigGustave.ColorType.None => png.Header.BitDepth switch
                {
                    8 => "mono8",
                    16 => "mono16",
                    _ => null
                },
                BigGustave.ColorType.ColorUsed => png.Header.BitDepth switch
                {
                    8 => "rgb8",
                    16 => "rgb16",
                    _ => null
                },
                BigGustave.ColorType.AlphaChannelUsed => png.Header.BitDepth switch
                {
                    8 => "rgba8",
                    16 => "rgba16",
                    _ => null
                },
                _ => null
            };
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
                        if (bitmapBuffer.Length < reqSize)
                        {
                            bitmapBuffer = new byte[reqSize];
                        }

                        int srcOffset = png.RowOffset;
                        int dstOffset = 0;
                        int rowSize = png.RowSize;
                        foreach (int _ in ..png.Height)
                        {
                            Buffer.BlockCopy(png.Data, srcOffset, bitmapBuffer, dstOffset, rowSize);
                            srcOffset += png.RowStep;
                            dstOffset += rowSize;
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
                    var image = new JpegImage(new MemoryStream(data));

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
                        RosLogger.Debug($"{this}: Unsupported encoding '{image.Colorspace}' " +
                                        $"with size {image.BitsPerComponent.ToString()}");
                        return;
                    }

                    const int bmpHeaderLength = 54;
                    reqSize += bmpHeaderLength;

                    if (bitmapBuffer.Length < reqSize)
                    {
                        bitmapBuffer = new byte[reqSize];
                    }

                    using (var outStream = new MemoryStream(bitmapBuffer))
                    {
                        image.WriteBitmap(outStream);
                    }

                    byte[] newBitmapBuffer = bitmapBuffer;

                    GameThread.PostInListenerQueue(() =>
                    {
                        try
                        {
                            Set(image.Width, image.Height, encoding, newBitmapBuffer.AsReadOnlySpan(bmpHeaderLength..));
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

        public void Set(int width, int height, string encoding, ReadOnlySpan<byte> data, bool generateMipmaps = false)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            int size = width * height;

            if (FieldSizeFromEncoding(encoding) is not { } bpp)
            {
                RosLogger.Debug($"{this}: Unsupported encoding '{encoding}'");
                return;
            }

            if (data.Length < size * bpp)
            {
                RosLogger.Debug(
                    $"{this}: Invalid image! Expected at least {(size * bpp).ToString()} bytes, " +
                    $"received {data.Length.ToString()}");
                return;
            }

            Description = $"{width.ToString()}x{height.ToString()} px | {encoding}";

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

            ApplyTexture(width, height, data[..(size * bpp)], encoding, generateMipmaps);
        }

        void ApplyTexture(int width, int height, ReadOnlySpan<byte> data, string encoding, bool generateMipmaps)
        {
            bool alreadyCopied = false;
            Texture2D texture;
            Vector2 intensityBounds;
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
                        CopyRgb24ToRgba32(data, texture.GetRawTextureData<byte>().AsSpan());
                        alreadyCopied = true;
                    }
                    else
                    {
                        texture = EnsureSize(width, height, TextureFormat.RGB24);
                    }

                    MeasuredIntensityBounds = null;
                    normalizationFactor = 1;
                    break;
                case "RGBA8":
                case "BGRA8":
                case "BGRA":
                case "RGBA":
                case "8SC4":
                case "8UC4":
                    texture = EnsureSize(width, height, TextureFormat.RGBA32);
                    MeasuredIntensityBounds = null;
                    normalizationFactor = 1;
                    break;
                case "MONO16":
                case "16UC1":
                case "16SC1":
                case "16UC":
                case "16SC":
                    if (!Settings.SupportsR16)
                    {
                        texture = EnsureSize(width, height, TextureFormat.R8);
                        CopyR16ToR8(data, texture.GetRawTextureData<byte>().AsSpan());
                        alreadyCopied = true;
                    }
                    else
                    {
                        texture = EnsureSize(width, height, TextureFormat.R16);
                    }

                    intensityBounds = CalculateBounds(data.Cast<ushort>());
                    MeasuredIntensityBounds = intensityBounds;
                    normalizationFactor = 1f / ushort.MaxValue;
                    if (!OverrideIntensityBounds)
                    {
                        IntensityBounds = intensityBounds;
                    }

                    break;
                case "MONO8":
                case "8UC1":
                case "8UC":
                case "8SC1":
                case "8SC":
                    texture = EnsureSize(width, height, TextureFormat.R8);
                    intensityBounds = CalculateBounds(data);
                    MeasuredIntensityBounds = intensityBounds;
                    normalizationFactor = 1f / byte.MaxValue;
                    if (!OverrideIntensityBounds)
                    {
                        IntensityBounds = intensityBounds;
                    }

                    break;
                case "32FC1":
                case "32FC":
                    texture = EnsureSize(width, height, TextureFormat.RFloat);
                    intensityBounds = CalculateBounds(data.Cast<float>());
                    MeasuredIntensityBounds = intensityBounds;
                    normalizationFactor = 1;
                    if (!OverrideIntensityBounds)
                    {
                        IntensityBounds = intensityBounds;
                    }

                    break;
                default:
                    return;
            }

            if (!alreadyCopied)
            {
                texture.GetRawTextureData<byte>().CopyFrom(data);
            }

            texture.Apply(generateMipmaps);
        }

        [StructLayout(LayoutKind.Sequential)]
        readonly struct R16
        {
            readonly byte high;
            public readonly byte low;
        }

        static void CopyR16ToR8(ReadOnlySpan<byte> src, Span<byte> dst)
        {
            var srcPtr = src.Cast<R16>();
            for (int i = 0; i < srcPtr.Length; i++)
            {
                dst[i] = srcPtr[i].low;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Rgba
        {
            public byte r, g, b, a;
        }

        [StructLayout(LayoutKind.Sequential)]
        readonly struct Rgb
        {
            public readonly byte r, g, b;
        }

        static void CopyRgb24ToRgba32(ReadOnlySpan<byte> src, Span<byte> dst)
        {
            var srcPtr = src.Cast<Rgb>();
            var dstPtr = dst.Cast<Rgba>();
            for (int i = 0; i < srcPtr.Length; i++)
            {
                var colorIn = srcPtr[i];

                Rgba colorOut;
                colorOut.r = colorIn.r;
                colorOut.g = colorIn.g;
                colorOut.b = colorIn.b;
                colorOut.a = 255;

                dstPtr[i] = colorOut;
            }
        }

        static Vector2 CalculateBounds(ReadOnlySpan<byte> src)
        {
            byte min = byte.MaxValue;
            byte max = byte.MinValue;
            foreach (byte b in src)
            {
                if (b > max)
                {
                    max = b;
                }

                if (b < min)
                {
                    min = b;
                }
            }

            return new Vector2(min, max);
        }

        static Vector2 CalculateBounds(ReadOnlySpan<ushort> src)
        {
            ushort min = ushort.MaxValue;
            ushort max = ushort.MinValue;
            foreach (ushort b in src)
            {
                if (b > max)
                {
                    max = b;
                }

                if (b < min)
                {
                    min = b;
                }
            }

            return new Vector2(min, max);
        }

        static Vector2 CalculateBounds(ReadOnlySpan<float> src)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            foreach (float b in src)
            {
                if (b > max)
                {
                    max = b;
                }

                if (b < min)
                {
                    min = b;
                }
            }

            return new Vector2(min, max);
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

        public bool TrySampleColor(in Vector2 rawUV, out Vector2Int uv, out TextureFormat format, out Vector4 color)
        {
            if (Texture == null)
            {
                format = default;
                color = default;
                uv = default;
                return false;
            }

            format = Texture.format;
            int u = Mathf.Clamp((int)(rawUV.x * Texture.width), 0, Texture.width - 1);
            int v = Mathf.Clamp((int)(rawUV.y * Texture.height), 0, Texture.height - 1);
            bool rbFlipped = Material.IsKeywordEnabled("FLIP_RB");
            uv = new Vector2Int(u, v);

            int offset = v * Texture.width + u;
            var span = Texture.GetRawTextureData<byte>().AsReadOnlySpan();
            var maybeColor = format switch
            {
                TextureFormat.R8 => FromScalar(span[offset]),
                TextureFormat.R16 => FromScalar(span.Cast<ushort>()[offset]),
                TextureFormat.RFloat => FromScalar(span.Cast<float>()[offset]),
                TextureFormat.RGB24 when rbFlipped => FromBgr(span.Cast<Rgb>()[offset]),
                TextureFormat.RGBA32 when rbFlipped => FromBgra(span.Cast<Rgba>()[offset]),
                TextureFormat.RGB24 => FromRgb(span.Cast<Rgb>()[offset]),
                TextureFormat.RGBA32 => FromRgba(span.Cast<Rgba>()[offset]),
                _ => (Vector4?)null
            };

            if (maybeColor is not { } validatedColor)
            {
                color = default;
                Debug.Log($"{this}: Unhandled format {format}");
                return false;
            }

            color = validatedColor;
            return true;

            static Vector4 FromScalar(float f) => new(f, 0, 0, 1);
            static Vector4 FromRgb(in Rgb rgb) => new(rgb.r, rgb.g, rgb.b, 1);
            static Vector4 FromRgba(in Rgba rgb) => new(rgb.r, rgb.g, rgb.b, rgb.a);
            static Vector4 FromBgr(in Rgb rgb) => new(rgb.b, rgb.g, rgb.r, 1);
            static Vector4 FromBgra(in Rgba rgb) => new(rgb.b, rgb.g, rgb.r, rgb.a);
        }

        public void Reset()
        {
            if (Texture == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(Texture);
            Texture = null;
            Material.SetTexture(MainTexID, Texture2D.whiteTexture);
            Material.DisableKeyword("USE_INTENSITY");
            TextureChanged?.Invoke(Texture);
        }

        public void Dispose()
        {
            TextureChanged?.Invoke(null);
            TextureChanged = null;

            if (Texture != null)
            {
                UnityEngine.Object.Destroy(Texture);
            }

            UnityEngine.Object.Destroy(Material);
            bitmapBuffer = Array.Empty<byte>();
        }
    }
}