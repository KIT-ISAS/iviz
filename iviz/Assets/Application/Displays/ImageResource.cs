using UnityEngine;

namespace Iviz.App.Displays
{
    public class ImageResource : MonoBehaviour, IDisplay
    {
        public GameObject front;
        public GameObject back;
        BoxCollider Collider;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

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
        public string Name => "ImageResource";
        public Bounds Bounds => new Bounds(Collider.center, Collider.size);
        public Bounds WorldBounds => Collider.bounds;

        public bool ColliderEnabled
        {
            get => Collider.enabled;
            set => Collider.enabled = value;
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        void Awake()
        {
            Collider = GetComponent<BoxCollider>();
        }

        public void Stop()
        {
            if (texture != null)
            {
                texture.TextureChanged -= OnTextureChanged;
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