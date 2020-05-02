using UnityEngine;
using System.Threading.Tasks;
using System;
using Unity.Collections;
using Iviz.Msgs.sensor_msgs;

namespace Iviz.App
{
    public class ImageListener : DisplayableListener
    {
        byte[] rgbaBuffer;
        byte[] pngBuffer;

        public Texture2D Texture { get; private set; }
        public Material Material { get; private set; }
        public string Description { get; private set; }
        public bool IsMono { get; private set; }

        public event Action<Texture2D> TextureChanged;
        public event Action<Texture2D> ColormapChanged;

        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.Image;
            public string topic = "";
            public string type = "";
            public Resource.ColormapId colormap = Resource.ColormapId.gray;
            public AnchorCanvas.AnchorType anchor = AnchorCanvas.AnchorType.None;
            public float minIntensity = 0.0f;
            public float maxIntensity = 1.0f;
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                config.topic = value.topic;
                config.type = value.type;
                Colormap = value.colormap;
                Anchor = value.anchor;
                MinIntensity = value.minIntensity;
                MaxIntensity = value.maxIntensity;
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.colormap;
            set
            {
                config.colormap = value;
                UpdateColormap();
            }
        }

        public Texture2D ColormapTexture => Resource.Colormaps.Textures[Colormap];

        public AnchorCanvas.AnchorType Anchor
        {
            get => config.anchor;
            set
            {
                config.anchor = value;
            }
        }

        public float MinIntensity
        {
            get => config.minIntensity;
            set
            {
                config.minIntensity = value;
                UpdateIntensitySpan();
            }
        }

        public float MaxIntensity
        {
            get => config.maxIntensity;
            set
            {
                config.maxIntensity = value;
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

        void Awake()
        {
            Material = Instantiate(Resource.Materials.ImagePreview);

            Config = new Configuration();
        }

        public override void StartListening()
        {
            Topic = config.topic;
            if (config.type == Image.RosMessageType)
            {
                Listener = new RosListener<Image>(config.topic, Handler);
            }
            else if (config.type == CompressedImage.RosMessageType)
            {
                Listener = new RosListener<CompressedImage>(config.topic, HandlerCompressed);
            }
            GameThread.EverySecond += UpdateStats;
        }

        public override void Unsubscribe()
        {
            GameThread.EverySecond -= UpdateStats;
            Listener?.Stop();
            Listener = null;

            TextureChanged?.Invoke(null);
            TextureChanged = null;
            ColormapChanged?.Invoke(null);
            ColormapChanged = null;
        }

        void HandlerCompressed(CompressedImage msg)
        {
            if (msg.format != "png")
            {
                Logger.Error("ImageListener: Can only handle png compression");
                return;
            }
            Task.Run(() =>
            {
                BigGustave.Png png = BigGustave.Png.Open(msg.data);

                Image newMsg = new Image()
                {
                    header = msg.header,
                    width = (uint)png.Width,
                    height = (uint)png.Height,
                    step = (uint)png.RowStep,
                    encoding = EncodingFromPng(png),
                    is_bigendian = 0
                };

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
                    newMsg.data = pngBuffer;
                }
                else
                {
                    newMsg.data = png.Data;
                }

                GameThread.RunOnce(() => Handler(newMsg));
            });
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

        /*
        
        void PngToNativeArray(BigGustave.Png src, NativeArray<byte> dst)
        {
            int srcOffset = src.RowOffset;
            int dstOffset = 0;
            int rowSize = src.RowSize;
            for (int i = src.Height; i != 0; i--, srcOffset += src.RowStep, dstOffset += rowSize)
            {
                NativeArray<byte>.Copy(src.Data, srcOffset, dst, dstOffset, rowSize);
            }
        }
        */



        void Handler(Image msg)
        {
            SetParent(msg.header.frame_id);

            int width = (int)msg.width;
            int height = (int)msg.height;
            int size = width * height;
            int bpp = FieldSizeFromEncoding(msg);

            if (bpp == -1)
            {
                Logger.Error("ImageListener: Unsupported encoding '" + msg.encoding + "'");
                return;
            }
            else if (msg.data.Length < size * bpp)
            {
                Debug.Log($"ImageListener: Invalid image! Expected at least {size * bpp} bytes, received {msg.data.Length}");
                return;
            }

            IsMono = false;

            Material.DisableKeyword("FLIP_RB");
            Material.DisableKeyword("USE_INTENSITY");

            switch (msg.encoding)
            {
                case "rgba8":
                case "bgra8":
                    ApplyTexture(msg.data, width, height, msg.encoding, size * 4);
                    break;
                case "rgb8":
                case "bgr8":
                    Task.Run(() =>
                    {
                        FillRGBABuffer(msg);
                        GameThread.RunOnce(() =>
                        {
                            ApplyTexture(rgbaBuffer, width, height, msg.encoding, size * 4);
                            if (msg.encoding == "bgr8")
                            {
                                Material.EnableKeyword("FLIP_RB");
                            }
                        });
                    });
                    break;
                case "mono16":
                    IsMono = true;
                    ApplyTexture(msg.data, width, height, msg.encoding, size * 2);
                    break;
                case "mono8":
                    IsMono = true;
                    ApplyTexture(msg.data, width, height, msg.encoding, size);
                    break;
            }

            if (IsMono)
            {
                Material.EnableKeyword("USE_INTENSITY");
            }

            Description = $"Format: {msg.width}x{msg.height} {msg.encoding}";
        }

        static int FieldSizeFromEncoding(Image msg)
        {
            switch (msg.encoding)
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

        void FillRGBABuffer(Image msg)
        {
            int size = (int)(msg.width * msg.height);
            if (rgbaBuffer == null || rgbaBuffer.Length < size * 4)
            {
                rgbaBuffer = new byte[size * 4];
            }
            unsafe
            {
                fixed (byte* tmpBufferPtr = rgbaBuffer, dataPtr = msg.data)
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

        void UpdateColormap()
        {
            Material.SetTexture("_IntensityTex", ColormapTexture);
            ColormapChanged?.Invoke(ColormapTexture);
        }

        void ApplyTexture(byte[] data, int width, int height, string type, int length)
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
                    Destroy(Texture);
                }
                Texture = new Texture2D(width, height, format, false);
                Material.SetTexture("_MainTex", Texture);
                TextureChanged?.Invoke(Texture);
            }
        }

        /*
        void ApplyTexturePng(BigGustave.Png png)
        {
            EnsureSize(png.Width, png.Height);
            PngToNativeArray(png, texture.GetRawTextureData<byte>());
            texture.Apply(false, false);
        }
        */

        void OnDestroy()
        {
            if (Texture != null) Destroy(Texture);
            if (Material != null) Destroy(Material);
        }

    }
}