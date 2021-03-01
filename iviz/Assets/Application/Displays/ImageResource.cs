using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ImageResource : MarkerResource
    {
        [SerializeField] GameObject front = null;
        [SerializeField] Billboard billboard = null;
        [SerializeField] float scale = 1;

        Pose billboardStartPose;
        ImageTexture texture;
        Vector3 offset;

        public ImageTexture Texture
        {
            get => texture;
            set
            {
                if (texture != null) texture.TextureChanged -= OnTextureChanged;
                texture = value;
                if (texture != null)
                {
                    texture.TextureChanged += OnTextureChanged;
                    OnTextureChanged(texture.Texture);
                }
            }
        }

        public override int Layer
        {
            get => billboard.gameObject.layer;
            set => billboard.gameObject.layer = value;
        }

        public bool BillboardEnabled
        {
            get => billboard.enabled;
            set
            {
                billboard.enabled = value;
                if (!value)
                {
                    billboard.transform.SetLocalPose(new Pose(Offset, billboardStartPose.rotation));
                }
            }
        }

        public Vector3 Offset
        {
            get => offset;
            set
            {
                offset = value;
                billboard.transform.localPosition = value;
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                UpdateSides();
            }
        }

        float Width => Scale;
        float Height => Width * AspectRatio;
        float AspectRatio => Texture == null || Texture.Width == 0 ? 1 : (float) Texture.Height / Texture.Width;

        protected override void Awake()
        {
            BoxCollider = billboard.GetComponent<BoxCollider>();
            base.Awake();

            billboard.UseAbsoluteOffset = false;
            billboardStartPose = billboard.transform.AsLocalPose();
        }

        public void Set(int width, int height, int bpp, in ArraySegment<byte> data, bool generateMipmaps = false)
        {
            if (Texture == null)
            {
                Texture = new ImageTexture();
            }

            string encoding;
            switch (bpp)
            {
                case 1:
                    encoding = "mono8";
                    break;
                case 2:
                    encoding = "mono16";
                    break;
                case 3:
                    encoding = "rgb8";
                    break;
                case 4:
                    encoding = "rgba8";
                    break;
                default:
                    Debug.LogWarning("ImageResource: Set function could not find encoding!");
                    return;
            }

            Texture.Set(width, height, encoding, data, generateMipmaps);
        }

        public override void Suspend()
        {
            base.Suspend();

            Texture = null;
            Offset = Vector3.zero;
            BillboardEnabled = false;
            Scale = 1;
        }

        void UpdateSides()
        {
            billboard.transform.localScale = new Vector3(Width, Height, 1);
        }

        void OnTextureChanged(Texture2D newTexture)
        {
            UpdateSides();

            if (newTexture != null)
            {
                newTexture.wrapMode = TextureWrapMode.Clamp;
            }

            front.GetComponent<MeshRenderer>().sharedMaterial = Texture.Material;
        }
    }
}