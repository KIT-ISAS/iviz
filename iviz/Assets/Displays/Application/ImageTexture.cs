#nullable enable

using System;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Core;
using Iviz.ImageDecoders;
using Iviz.Resources;
using MarcusW.VncClient.Protocol.Implementation.Native;
using Unity.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector4 = UnityEngine.Vector4;

namespace Iviz.Displays
{
    public sealed class ImageTexture
    {
        readonly object owner;

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
                    Material.SetFloat(ShaderIds.IntensityCoeffId, 1);
                    Material.SetFloat(ShaderIds.IntensityAddId, 0);
                }
                else
                {
                    if (!FlipMinMax)
                    {
                        Material.SetFloat(ShaderIds.IntensityCoeffId, 1 / intensitySpan);
                        Material.SetFloat(ShaderIds.IntensityAddId, -normalizedIntensityBounds.x / intensitySpan);
                    }
                    else
                    {
                        Material.SetFloat(ShaderIds.IntensityCoeffId, -1 / intensitySpan);
                        Material.SetFloat(ShaderIds.IntensityAddId, normalizedIntensityBounds.y / intensitySpan);
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
                Material.SetTexture(ShaderIds.IntensityTextureId, ColormapTexture);
            }
        }

        Texture2D ColormapTexture => Resource.Colormaps.Textures[Colormap];

        public ImageTexture(object owner)
        {
            Material = Resource.Materials.ImagePreview.Instantiate();
            this.owner = owner;
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

        public void ProcessPng(byte[] data, Action onFinished)
        {
            ThrowHelper.ThrowIfNull(data, nameof(data));
            ThrowHelper.ThrowIfNull(onFinished, nameof(onFinished));

            if (data.Length < 8)
            {
                RosLogger.Debug($"{this}: Error processing PNG image. Data array is too short to be a PNG file");
                GameThread.PostInListenerQueue(onFinished);
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    ProcessPngCore(data, onFinished);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error processing PNG image", e);
                    GameThread.PostInListenerQueue(onFinished);
                }
            });
        }

        void ProcessPngCore(byte[] data, Action onFinished)
        {
            int width;
            int height;
            string? encoding;

            using (var pngDecoder = new PngDecoder(data))
            {
                var info = pngDecoder.PngInfo;
                if (info.Width >= Settings.MaxTextureSize || info.Height >= Settings.MaxTextureSize)
                {
                    RosLogger.Error($"{this}: Error processing PNG image. Required destination buffer is too large");
                    GameThread.PostInListenerQueue(onFinished);
                    return;
                }

                encoding = info.ColorType switch
                {
                    PngColorType.Gray when info.BitDepth == 8 => "mono8",
                    PngColorType.Gray when info.BitDepth == 16 => "mono16",
                    PngColorType.Gray when info.BitDepth == 16 => "mono16",
                    PngColorType.RGB when info.BitDepth == 8 => "rgb8",
                    PngColorType.RGBA when info.BitDepth == 8 => "rgba8",
                    _ => null
                };

                if (encoding == null)
                {
                    RosLogger.Error($"{this}: Ignoring PNG with unsupported encoding '{info.ColorType}'");
                    GameThread.PostInListenerQueue(onFinished);
                    return;
                }

                int reqSize = info.DecompressedSize;
                if (bitmapBuffer.Length < reqSize)
                {
                    bitmapBuffer = new byte[reqSize];
                }

                pngDecoder.Decode(bitmapBuffer);

                width = info.Width;
                height = info.Height;
            }

            GameThread.PostInListenerQueue(() =>
            {
                try
                {
                    Set(width, height, encoding, bitmapBuffer);
                }
                finally
                {
                    onFinished();
                }
            });
        }

        public void ProcessJpeg(byte[] data, Action onFinished)
        {
            ThrowHelper.ThrowIfNull(data, nameof(data));
            ThrowHelper.ThrowIfNull(onFinished, nameof(onFinished));

            if (data.Length < 2)
            {
                RosLogger.Debug($"{this}: Error processing JPG image. Data array is too short to be a JPG file");
                GameThread.PostInListenerQueue(onFinished);
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    ProcessJpegCore(data, onFinished);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error processing JPG image", e);
                    GameThread.PostInListenerQueue(onFinished);
                }
            });
        }

        void ProcessJpegCore(byte[] data, Action onFinished)
        {
            int width;
            int height;
            string? encoding;

            using (var jpegDecoder = new JpegDecoder(data))
            {
                var info = jpegDecoder.JpegInfo;
                if (info.Width >= Settings.MaxTextureSize || info.Height >= Settings.MaxTextureSize)
                {
                    RosLogger.Error($"{this}: Error processing JPG image. Required destination buffer is too large");
                    GameThread.PostInListenerQueue(onFinished);
                    return;
                }

                encoding = info.Format switch
                {
                    TurboJpegPixelFormat.Gray => "mono8",
                    TurboJpegPixelFormat.RGB => "rgb",
                    TurboJpegPixelFormat.RGBA or TurboJpegPixelFormat.RGBX => "rgba",
                    TurboJpegPixelFormat.BGR => "bgr",
                    TurboJpegPixelFormat.BGRA or TurboJpegPixelFormat.BGRX => "bgra",
                    _ => null
                };

                if (encoding == null)
                {
                    RosLogger.Error($"{this}: Ignoring PNG with unsupported encoding '{info.Colorspace}'");
                    GameThread.PostInListenerQueue(onFinished);
                    return;
                }

                int reqSize = info.DecompressedSize;
                if (bitmapBuffer.Length < reqSize)
                {
                    bitmapBuffer = new byte[reqSize];
                }

                jpegDecoder.Decode(bitmapBuffer);

                width = info.Width;
                height = info.Height;
            }

            GameThread.PostInListenerQueue(() =>
            {
                if (Material == null) // check if we're shutting down
                {
                    onFinished();
                    return;
                }

                try
                {
                    Set(width, height, encoding, bitmapBuffer);
                }
                finally
                {
                    onFinished();
                }
            });
        }

        public void Set(int width, int height, string encoding, ReadOnlySpan<byte> data, bool generateMipmaps = false)
        {
            ThrowHelper.ThrowIfNull(encoding, nameof(encoding));

            if (width >= Settings.MaxTextureSize || height >= Settings.MaxTextureSize)
            {
                RosLogger.Debug($"{this}: Required destination buffer is too large");
                return;
            }

            int size = width * height;

            if (FieldSizeFromEncoding(encoding) is not { } bpp)
            {
                RosLogger.Debug($"{this}: Unsupported encoding '{encoding}'");
                return;
            }

            if (data.Length < size * bpp)
            {
                RosLogger.Error(
                    $"{this}: Invalid image! Expected at least {(size * bpp).ToString()} bytes, " +
                    $"but received {data.Length.ToString()}");

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
            Texture2D texture;
            Vector2 intensityBounds;
            switch (encoding.ToUpperInvariant())
            {
                case "RGB8" or "BGR8" or "RGB" or "BGR" or "8SC3" or "8UC3":
                    if (!Settings.SupportsRGB24)
                    {
                        texture = EnsureSize(width, height, TextureFormat.RGBA32);
                        ConversionUtils.CopyPixelsRgbToRgba(texture.AsSpan(), data);
                    }
                    else
                    {
                        texture = EnsureSize(width, height, TextureFormat.RGB24);
                        texture.CopyFrom(data);
                    }

                    MeasuredIntensityBounds = null;
                    normalizationFactor = 1;
                    break;
                case "RGBA8" or "BGRA8" or "BGRA" or "RGBA" or "8SC4" or "8UC4":
                    texture = EnsureSize(width, height, TextureFormat.RGBA32);
                    texture.CopyFrom(data);
                    MeasuredIntensityBounds = null;
                    normalizationFactor = 1;
                    break;
                case "MONO16" or "16UC1" or "16SC1" or "16UC" or "16SC":
                    if (!Settings.SupportsR16)
                    {
                        texture = EnsureSize(width, height, TextureFormat.R8);
                        ConversionUtils.CopyPixelsR16ToR8(texture.AsSpan(), data);

                        intensityBounds = CalculateBounds(texture.GetRawTextureData<byte>());
                        MeasuredIntensityBounds = intensityBounds;
                        normalizationFactor = 1f / byte.MaxValue;
                    }
                    else
                    {
                        texture = EnsureSize(width, height, TextureFormat.R16);
                        texture.CopyFrom(data);

                        intensityBounds = CalculateBounds(texture.GetRawTextureData<ushort>());
                        MeasuredIntensityBounds = intensityBounds;
                        normalizationFactor = 1f / ushort.MaxValue;
                    }

                    if (!OverrideIntensityBounds)
                    {
                        IntensityBounds = intensityBounds;
                    }

                    break;
                case "MONO8" or "8UC1" or "8UC" or "8SC1" or "8SC":
                    texture = EnsureSize(width, height, TextureFormat.R8);
                    texture.CopyFrom(data);
                    intensityBounds = CalculateBounds(texture.GetRawTextureData<byte>());
                    MeasuredIntensityBounds = intensityBounds;
                    normalizationFactor = 1f / byte.MaxValue;
                    if (!OverrideIntensityBounds)
                    {
                        IntensityBounds = intensityBounds;
                    }

                    break;
                case "32FC1" or "32FC":
                    texture = EnsureSize(width, height, TextureFormat.RFloat);
                    texture.CopyFrom(data);
                    intensityBounds = CalculateBounds(texture.GetRawTextureData<float>());
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

            texture.Apply(generateMipmaps);
        }

        static Vector2 CalculateBounds(NativeArray<byte> src)
        {
            (byte min, byte max) = MinMaxJobs.CalculateBounds(src);
            return new Vector2(min, max);
        }

        static Vector2 CalculateBounds(NativeArray<ushort> src)
        {
            (ushort min, ushort max) = MinMaxJobs.CalculateBounds(src);
            return new Vector2(min, max);
        }

        static Vector2 CalculateBounds(NativeArray<float> src)
        {
            (float min, float max) = MinMaxJobs.CalculateBounds(src);
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
            Material.SetTexture(ShaderIds.MainTexId, Texture);
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
            var pickedColor = format switch
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

            if (pickedColor is not { } validatedColor)
            {
                color = default;
                RosLogger.Debug($"{this}: Unhandled format {format}");
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
            Material.SetTexture(ShaderIds.MainTexId, Texture2D.whiteTexture);
            Material.DisableKeyword("USE_INTENSITY");
            TextureChanged?.Invoke(null);
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

        public override string ToString() => $"[{nameof(ImageTexture)} from {owner}]";
    }
}