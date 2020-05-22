using UnityEngine;

namespace Iviz.Displays
{
    public class ImageResource : MarkerResource
    {
        public GameObject front;
        public GameObject back;

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

        float scale = 1;
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

        public override string Name => "ImageResource";

        protected override void Awake()
        {
            base.Awake();
            Collider = GetComponent<BoxCollider>();
        }

        public override void Stop()
        {
            base.Stop();
            if (texture != null)
            {
                texture.TextureChanged -= OnTextureChanged;
                texture = null;
            }
        }

        void UpdateSides()
        {
            transform.localScale = new Vector3(Width, 1, Height);
        }

        void OnTextureChanged(Texture2D obj)
        {
            UpdateSides();
            front.GetComponent<MeshRenderer>().sharedMaterial = Texture.Material;
        }
    }

}