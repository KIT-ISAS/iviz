using Iviz.App.Displays;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ImageResource : MonoBehaviour, IDisplay
    {
        [SerializeField] GameObject front = null;
        //[SerializeField] GameObject back = null;
        [SerializeField] Billboard billboard = null;
        Pose billboardStartPose;
        BoxCollider Collider;

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

        public int Layer
        {
            get => billboard.gameObject.layer;
            set => billboard.gameObject.layer = value;
        }

        public bool EnableBillboard
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
            set
            {
                //Vector3 v = new Vector3(-value.x, value.y, -value.z);
                billboard.transform.localPosition = value;
            }
        }

        [SerializeField] float scale_ = 1;
        public float Scale
        {
            get => scale_;
            set
            {
                scale_ = value;
                UpdateSides();
            }
        }

        public float Width => Scale;

        float AspectRatio => (Texture == null || Texture.Width == 0) ? 1 : (float)Texture.Height / Texture.Width;

        public float Height => Width * AspectRatio;

        public string Name => "ImageResource";

        public Bounds Bounds => new Bounds(Collider.center, Collider.size);

        public Bounds WorldBounds => Collider.bounds;

        public Pose WorldPose => billboard.transform.AsPose();

        public Vector3 WorldScale => billboard.transform.lossyScale;

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

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            Collider = billboard.GetComponent<BoxCollider>();
            billboard.UseAbsoluteOffset = false;
            billboardStartPose = billboard.transform.AsLocalPose();
        }

        public void Stop()
        {
            if (texture != null)
            {
                texture.TextureChanged -= OnTextureChanged;
                texture = null;
            }
            Offset = Vector3.zero;
            EnableBillboard = false;
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