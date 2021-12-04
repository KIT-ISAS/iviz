#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ImageResource : MonoBehaviour, IDisplay
    {
        [SerializeField] MeshRenderer? front = null;
        [SerializeField] Billboard? billboard = null;
        [SerializeField] float scale = 1;

        Transform? mTransform;
        ImageTexture? texture;
        BoxCollider? boxCollider;
        Pose billboardStartPose;
        Vector3 offset;

        Billboard Billboard => billboard.AssertNotNull(nameof(billboard));
        MeshRenderer Front => front.AssertNotNull(nameof(front));

        BoxCollider Collider =>
            boxCollider != null ? boxCollider : (boxCollider = Billboard.GetComponent<BoxCollider>());

        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        
        
        public ImageTexture? Texture
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

        public Bounds? Bounds => new Bounds(Collider.center, Collider.size);

        public int Layer
        {
            set => Billboard.gameObject.layer = value;
        }

        public bool BillboardEnabled
        {
            get => Billboard.enabled;
            set
            {
                Billboard.enabled = value;
                if (!value)
                {
                    Billboard.transform.SetLocalPose(new Pose(Offset, billboardStartPose.rotation));
                }
            }
        }

        public Vector3 Offset
        {
            get => offset;
            set
            {
                offset = value;
                Billboard.transform.localPosition = value;
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
        float AspectRatio => Texture != null && Texture.Width != 0 ? (float)Texture.Height / Texture.Width : 1;

        void Awake()
        {
            Billboard.UseAbsoluteOffset = false;
            billboardStartPose = Billboard.transform.AsLocalPose();
        }

        public void Set(int width, int height, int bpp, byte[] data, bool generateMipmaps = false)
        {
            Texture ??= new ImageTexture();

            string? encoding = bpp switch
            {
                1 => "mono8",
                2 => "mono16",
                3 => "rgb8",
                4 => "rgba8",
                _ => null
            };

            if (encoding is null)
            {
                Debug.LogWarning("ImageResource: Set function could not find encoding!");
                return;
            }

            Texture.Set(width, height, encoding, data, generateMipmaps);
        }

        void IDisplay.Suspend()
        {
            Visible = true;
            Texture = null;
            Offset = Vector3.zero;
            BillboardEnabled = false;
            Scale = 1;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void UpdateSides()
        {
            Billboard.transform.localScale = new Vector3(Width, Height, 1);
        }

        void OnTextureChanged(Texture2D? newTexture)
        {
            UpdateSides();

            if (newTexture != null)
            {
                newTexture.wrapMode = TextureWrapMode.Clamp;
            }

            if (Texture != null)
            {
                Front.sharedMaterial = Texture.Material;
            }
        }
    }
}