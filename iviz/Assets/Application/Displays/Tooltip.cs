#nullable enable

using System;
using System.Text;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class Tooltip : MonoBehaviour, IDisplay, IRecyclable
    {
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] TMP_Text? text;

        RoundedPlaneResource? background;
        uint? prevTextHash;
        Transform? mTransform;

        BoxCollider BoxCollider => boxCollider != null ? boxCollider : (boxCollider = GetComponent<BoxCollider>());
        TMP_Text Text => text.AssertNotNull(nameof(text));

        public RoundedPlaneResource Background =>
            background != null ? background : background = ResourcePool.RentDisplay<RoundedPlaneResource>(Transform);

        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        public Bounds? Bounds => BoxCollider.GetBounds();

        public int Layer
        {
            set
            {
                gameObject.layer = value;
                Text.gameObject.layer = value;
                Background.Layer = value;
            }
        }

        public float Scale
        {
            set => Transform.localScale = value * Vector3.one;
        }

        public float AbsoluteScale
        {
            set => Transform.localScale = value *
                                          (Transform.parent is { } parent
                                              ? parent.lossyScale.InvCoeff()
                                              : Vector3.one);
        }

        void Awake()
        {
            Layer = LayerType.IgnoreRaycast;
        }

        public Color Color
        {
            set => Background.Color = value;
        }

        public Color EmissiveColor
        {
            set => Background.EmissiveColor = value;
        }

        public Color CaptionColor
        {
            set => Text.color = value;
        }

        public string Caption
        {
            set
            {
                uint hash = Crc32Calculator.Compute(value);
                if (hash == prevTextHash)
                {
                    return;
                }

                prevTextHash = hash;
                Text.text = value;
                UpdateSize();
            }
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void SetCaption(BuilderPool.BuilderRent str)
        {
            uint hash = Crc32Calculator.Compute(str);
            if (hash == prevTextHash)
            {
                return;
            }

            prevTextHash = hash;
            Text.SetText(str);
            UpdateSize();
        }

        public bool FixedWidth { get; set; } = false;
        public float PaddingX { get; set; } = 5;
        public float PaddingY { get; set; } = 2;

        void UpdateSize()
        {
            float width = FixedWidth ? Text.rectTransform.rect.width : Text.preferredWidth;
            Background.Size = new Vector2(width + PaddingX, Text.preferredHeight + PaddingY);
            Background.Radius = 2f;
            BoxCollider.size = Background.Bounds.size;
        }

        void IDisplay.Suspend()
        {
            Caption = "";
            EmissiveColor = Color.black;
            Color = Resource.Colors.TooltipBackground;
            CaptionColor = Color.white;
        }

        void IRecyclable.SplitForRecycle()
        {
            Background.ReturnToPool();
        }

        public void PointToCamera()
        {
            Transform.LookAt(2 * Transform.position - Settings.MainCameraTransform.position, Vector3.up);
        }

        void Update()
        {
            PointToCamera();
        }

        public static float GetRecommendedSize(in Vector3 unityPosition)
        {
            float distanceToCam = Vector3.Distance(Settings.MainCameraTransform.position, unityPosition);
            float size = 0.2f * Math.Max(distanceToCam, 0);
            float baseFrameSize = TfListener.Instance.FrameSize;
            float labelSize = baseFrameSize * size * (1.2f * 0.375f / 2);
            return labelSize;
        }
    }
}