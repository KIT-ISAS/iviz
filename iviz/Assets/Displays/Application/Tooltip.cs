#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using TMPro;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class Tooltip : MonoBehaviour, IDisplay, IRecyclable, ISupportsColor, ISupportsPbr, ISupportsTint
    {
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] TMP_Text? text;

        RoundedPlaneDisplay? background;
        uint? prevTextHash;
        Transform? mTransform;

        BoxCollider BoxCollider => this.EnsureHasComponent(ref boxCollider, nameof(boxCollider));
        TMP_Text Text => text.AssertNotNull(nameof(text));

        public RoundedPlaneDisplay Background => ResourcePool.RentChecked(ref background, Transform);
        public Transform Transform => this.EnsureHasTransform(ref mTransform);

        public float Scale
        {
            set => Transform.localScale = value * Vector3.one;
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

        public float Metallic
        {
            set => Background.Metallic = value;
        }

        public float Smoothness
        {
            set => Background.Smoothness = value;
        }

        public Color Tint
        {
            set => Background.Tint = value;
        }
        
        public string Caption
        {
            set
            {
                uint hash = HashCalculator.Compute(value);
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
            set => gameObject.SetActive(value);
        }

        public void SetCaption(BuilderPool.BuilderRent str)
        {
            uint hash = HashCalculator.Compute(str);
            if (hash == prevTextHash)
            {
                return;
            }

            prevTextHash = hash;
            Text.SetTextRent(str);
            UpdateSize();
        }

        public bool FixedWidth { get; set; }
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
            Transform.LookAt(2 * Transform.position - Settings.MainCameraPose.position, Vector3.up);
        }

        void OnEnable()
        {
            GameThread.AfterFramesUpdatedLate += PointToCamera;
        }

        void OnDisable()
        {
            GameThread.AfterFramesUpdatedLate -= PointToCamera;
        }

        public static float GetRecommendedSize(in Vector3 unityPosition)
        {
            float distanceToCam = Vector3.Distance(Settings.MainCameraPose.position, unityPosition);
            float size = 0.2f * Mathf.Max(distanceToCam, 0);
            float baseFrameSize = TfModule.Instance.FrameSize;
            float labelSize = baseFrameSize * size * (1.2f * 0.375f / 2);
            return labelSize;
        }
    }
}