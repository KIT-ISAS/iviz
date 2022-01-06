#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ImageResource : MonoBehaviour, IDisplay, IHasBounds, IHighlightable
    {
        [SerializeField] MeshRenderer? front;
        [SerializeField] Billboard? billboard;
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] float scale = 1;

        Transform? mTransform;
        ImageTexture? texture;
        Pose billboardStartPose;
        bool billboardEnabled;
        Vector3 offset;
        Intrinsic? intrinsic;

        Billboard Billboard => billboard.AssertNotNull(nameof(billboard));
        MeshRenderer Front => front.AssertNotNull(nameof(front));
        BoxCollider Collider => boxCollider.AssertNotNull(nameof(boxCollider));

        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        public string Title { get; set; } = "";

        public Intrinsic? Intrinsic
        {
            set
            {
                if (value is { IsValid: false } || Nullable.Equals(intrinsic, value))
                {
                    return;
                }

                intrinsic = value;
                UpdateSize();
            }
        }

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

        public Bounds? Bounds => Collider.GetBounds();
        public Transform BoundsTransform => Billboard.transform;
        public event Action? BoundsChanged;

        public int Layer
        {
            set => Billboard.gameObject.layer = value;
        }

        public bool BillboardEnabled
        {
            set
            {
                billboardEnabled = value;
                Billboard.enabled = value;
                UpdateSize();
            }
        }

        public Vector3 Offset
        {
            set
            {
                offset = value;
                UpdateSize();
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                UpdateSize();
            }
        }

        float Width => Scale;
        float Height => Width * AspectRatio;
        float AspectRatio => Texture != null && Texture.Width != 0 ? (float)Texture.Height / Texture.Width : 1;

        bool IHighlightable.IsAlive => gameObject.activeSelf;
        string IHasBounds.Caption => $"<b>{Title}</b>\n{(Texture != null ? Texture.Description : "(unset)")}";

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
            Intrinsic = null;
            Title = "";
            BoundsChanged = null;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void UpdateSize()
        {
            var billboardTransform = Billboard.transform;
            var baseScale = new Vector3(Width, Height, 1);

            if (intrinsic is { } validatedIntrinsic && !billboardEnabled && Texture is { Width: not 0 })
            {
                float perspectiveScale = Mathf.Abs(offset.y) / Texture.Width;
                billboardTransform.localScale = baseScale * (validatedIntrinsic.Fx * perspectiveScale);
                var intrinsicOffset = new Vector3(
                    validatedIntrinsic.Cx - Texture.Width / 2f,
                    Texture.Height / 2f - validatedIntrinsic.Cy,
                    0);
                billboardTransform.localPosition = offset + intrinsicOffset.Ros2Unity() * perspectiveScale;
                billboardTransform.localRotation = billboardStartPose.rotation;
            }
            else
            {
                billboardTransform.localScale = baseScale;
                billboardTransform.localPosition = offset;
                billboardTransform.localRotation = billboardEnabled
                    ? Quaternion.identity
                    : billboardStartPose.rotation;
            }

            BoundsChanged?.Invoke();
        }

        void OnTextureChanged(Texture2D? newTexture)
        {
            UpdateSize();

            if (newTexture != null)
            {
                newTexture.wrapMode = TextureWrapMode.Clamp;
            }

            if (Texture != null)
            {
                Front.sharedMaterial = Texture.Material;
            }
        }

        public void Highlight(in Vector3 hitPoint)
        {
            FAnimator.Start(new BoundsHighlighter(this, duration: 2));
        }
    }
}