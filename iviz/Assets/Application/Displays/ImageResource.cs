using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.Displays
{
    public sealed class ImageResource : MarkerResource
    {
        [SerializeField] GameObject front = null;
        [SerializeField] Billboard billboard = null;
        Pose billboardStartPose;

        public override Vector3 WorldScale => billboard.transform.lossyScale;
        public override Pose WorldPose => billboard.transform.AsPose();
        
        ImageTexture texture;
        public ImageTexture Texture
        {
            get => texture;
            set
            {
                if (texture != null)
                {
                    texture.TextureChanged -= OnTextureChanged;
                }
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
                    billboard.transform.SetLocalPose(billboardStartPose);
                }
            }
        }

        public Vector3 Offset
        {
            get => billboard.transform.localPosition;
            set => billboard.transform.localPosition = value;
        }

        [SerializeField] float scale = 1;
        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                UpdateSides();
            }
        }

        public float Width => Scale;

        float AspectRatio => (Texture == null || Texture.Width == 0) ? 1 : (float)Texture.Height / Texture.Width;

        public float Height => Width * AspectRatio;

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

            switch (bpp)
            {
                case 1:
                    Texture.Set(width, height, "mono8", data, generateMipmaps);
                    break;
                case 3:
                    Texture.Set(width, height, "rgb8", data, generateMipmaps);
                    break;
                case 4:
                    Texture.Set(width, height, "rgba8", data, generateMipmaps);
                    break;
                default:
                    Debug.LogWarning("ImageResource: Set function could not find encoding!");
                    break;
            }
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