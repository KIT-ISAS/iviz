using System;
using Unity.Collections;
using UnityEngine;

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
            boxCollider = billboard.GetComponent<BoxCollider>();
            base.Awake();
            
            billboard.UseAbsoluteOffset = false;
            billboardStartPose = billboard.transform.AsLocalPose();
        }

        public void Set(int width, int height, int bpp, in ArraySegment<byte> data)
        {
            if (texture == null)
            {
                texture = new ImageTexture();
            }

            switch (bpp)
            {
                case 1:
                    texture.Set(width, height, "mono8", data);
                    break;
                case 3:
                    texture.Set(width, height, "rgb8", data);
                    break;
                case 4:
                    texture.Set(width, height, "rgba8", data);
                    break;
                default:
                    Debug.LogWarning("ImageResource: Set function could not find encoding!");
                    break;
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            
            if (texture != null)
            {
                texture.TextureChanged -= OnTextureChanged;
                texture = null;
            }
            Offset = Vector3.zero;
            BillboardEnabled = false;
            Scale = 1;
        }

        void UpdateSides()
        {
            billboard.transform.localScale = new Vector3(Width, Height, 1);
        }

        void OnTextureChanged(Texture2D obj)
        {
            UpdateSides();
            front.GetComponent<MeshRenderer>().sharedMaterial = Texture.Material;
        }
    }

}