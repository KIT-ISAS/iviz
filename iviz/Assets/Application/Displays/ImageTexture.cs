using System;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.Resources;
using Unity.Collections;
using UnityEngine;
using Logger = Iviz.App.Logger;

namespace Iviz.Displays
{
    public class ImageTexture
    {
        byte[] rgbaBuffer;
        byte[] pngBuffer;

        public event Action<Texture2D> TextureChanged;

        public event Action<Texture2D> ColormapChanged;

        float minIntensity = 0;
        public float MinIntensity
        {
            get => minIntensity;
            set
            {
                minIntensity = value;
                UpdateIntensitySpan();
            }
        }

        float maxIntensity = 1;
        public float MaxIntensity
        {
            get => maxIntensity;
            set
            {
                maxIntensity = value;
                UpdateIntensitySpan();
            }
        }

        void UpdateIntensitySpan()
        {
            float intensitySpan = MaxIntensity - MinIntensity;
            if (intensitySpan == 0)
            {
                Material.SetFloat("_IntensityCoeff", 1);
                Material.SetFloat("_IntensityAdd", 0);
            }
            else
            {
                Material.SetFloat("_IntensityCoeff", 1 / intensitySpan);
                Material.SetFloat("_IntensityAdd", -MinIntensity / intensitySpan);
            }
        }

        public Texture2D Texture { get; private set; }
        public Material Material { get; }
        public string Description { get; private set; }
        public bool IsMono { get; private set; }
        public int Width => Texture?.width ?? 0;
        public int Height => Texture?.height ?? 0;

        Resource.ColormapId colormap;
        public Resource.ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;

                Material.SetTexture("_IntensityTex", ColormapTexture);
                ColormapChanged?.Invoke(ColormapTexture);
            }
        }

        public Texture2D ColormapTexture => Resource.Colormaps.Textures[Colormap];

        public ImageTexture()
        {
            Material = Resource.Materials.ImagePreview.Instantiate();
        }

        static int FieldSizeFromEncoding(string encoding)
        {
            switch (encoding)
            {
                case "rgba8":
                case "bgra8":
                    return 4;
                case "rgb8":
                case "bgr8":
                    return 3;
                case "mono16":
                    return 2;
                case "mono8":
                    return 1;
                default:
                    return -1;
            }
        }

        static string EncodingFromPng(BigGustave.Png png)
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

        public void SetPng(byte[] data)
        {
            Task.Run(() =>
            {
                byte[] newData;
                BigGustave.Png png = BigGustave.Png.Open(data);

                if (png.RowOffset != 0)
                {
                    int reqSize = png.Height * png.RowSize;
                    if (pngBuffer == null || pngBuffer.Length < reqSize)
                    {
                        pngBuffer = new byte[reqSize];
                    }

                    int srcOffset = png.RowOffset;
                    int dstOffset = 0;
                    int rowSize = png.RowSize;
                    for (int i = png.Height; i != 0; i--, srcOffset += png.RowStep, dstOffset += rowSize)
                    {
                        Buffer.BlockCopy(png.Data, srcOffset, pngBuffer, dstOffset, rowSize);
                    }
                    newData = pngBuffer;
                }
                else
                {
                    newData = png.Data;
                }

                GameThread.RunOnce(() => Set(png.Width, png.Height, EncodingFromPng(png), newData));
            });
        }


        public void Set(int width, int height, string encoding, byte[] data)
        {
            int size = width * height;
            int bpp = FieldSizeFromEncoding(encoding);

            if (bpp == -1)
            {
                Logger.Error("ImageListener: Unsupported encoding '" + encoding + "'");
                return;
            }
            else if (data.Length < size * bpp)
            {
                Debug.Log($"ImageListener: Invalid image! Expected at least {size * bpp} bytes, received {data.Length}");
                return;
            }

            Description = $"Format: {width}x{height} {encoding}";

            switch (encoding)
            {
                case "rgba8":
                    IsMono = false;
                    Material.DisableKeyword("USE_INTENSITY");
                    Material.DisableKeyword("FLIP_RB");
                    ApplyTexture(width, height, data, encoding, size * 4);
                    break;
                case "bgra8":
                    IsMono = false;
                    Material.DisableKeyword("USE_INTENSITY");
                    Material.EnableKeyword("FLIP_RB");
                    ApplyTexture(width, height, data, encoding, size * 4);
                    break;
                case "rgb8":
                    Task.Run(() =>
                    {
                        FillRGBABuffer(width, height, data);
                        GameThread.RunOnce(() =>
                        {
                            ApplyTexture(width, height, rgbaBuffer, encoding, size * 4);
                            IsMono = false;
                            Material.DisableKeyword("USE_INTENSITY");
                            Material.DisableKeyword("FLIP_RB");
                        });
                    });
                    break;
                case "bgr8":
                    Task.Run(() =>
                    {
                        FillRGBABuffer(width, height, data);
                        GameThread.RunOnce(() =>
                        {
                            ApplyTexture(width, height, rgbaBuffer, encoding, size * 4);
                            IsMono = false;
                            Material.DisableKeyword("USE_INTENSITY");
                            Material.EnableKeyword("FLIP_RB");
                        });
                    });
                    break;
                case "mono16":
                    IsMono = true;
                    Material.EnableKeyword("USE_INTENSITY");
                    ApplyTexture(width, height, data, encoding, size * 2);
                    break;
                case "mono8":
                    IsMono = true;
                    Material.EnableKeyword("USE_INTENSITY");
                    ApplyTexture(width, height, data, encoding, size);
                    break;
            }
        }


        void FillRGBABuffer(int width, int height, byte[] data)
        {
            int size = width * height;
            if (rgbaBuffer == null || rgbaBuffer.Length < size * 4)
            {
                rgbaBuffer = new byte[size * 4];
            }
            unsafe
            {
                fixed (byte* tmpBufferPtr = rgbaBuffer, dataPtr = data)
                {
                    byte* tmpOff = tmpBufferPtr, dataOff = dataPtr;
                    for (int i = size; i > 0; i--)
                    {
                        *tmpOff++ = *dataOff++;
                        *tmpOff++ = *dataOff++;
                        *tmpOff++ = *dataOff++;
                        *tmpOff++ = 255;
                    }
                }
            }
        }

        void ApplyTexture(int width, int height, byte[] data, string type, int length)
        {
            switch (type)
            {
                case "rgb8":
                case "bgr8":
                case "rgba8":
                case "bgra8":
                    EnsureSize(width, height, TextureFormat.RGBA32);
                    break;
                case "mono16":
                    EnsureSize(width, height, TextureFormat.R16);
                    break;
                case "mono8":
                    EnsureSize(width, height, TextureFormat.R8);
                    break;
                default:
                    return;
            }
            NativeArray<byte>.Copy(data, Texture.GetRawTextureData<byte>(), length);
            Texture.Apply(false, false);
        }

        void EnsureSize(int width, int height, TextureFormat format)
        {
            if (Texture == null ||
                Texture.width != width ||
                Texture.height != height ||
                Texture.format != format)
            {
                if (Texture != null)
                {
                    UnityEngine.Object.Destroy(Texture);
                }
                Texture = new Texture2D(width, height, format, false);
                Material.SetTexture("_MainTex", Texture);
                TextureChanged?.Invoke(Texture);
            }
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
            if (Texture != null) UnityEngine.Object.Destroy(Texture);
            if (Material != null) UnityEngine.Object.Destroy(Material);
        }
    }

}