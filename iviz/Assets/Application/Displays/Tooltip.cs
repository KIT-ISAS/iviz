#nullable enable

using System.Text;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
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

        RoundedPlaneResource Background =>
            background != null ? background : background = ResourcePool.RentDisplay<RoundedPlaneResource>(Transform);

        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        public Bounds? Bounds => new Bounds(BoxCollider.center, BoxCollider.size);

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

        void Awake()
        {
            Layer = LayerType.IgnoreRaycast;
        }

        public Color BackgroundColor
        {
            set => Background.Color = value;
        }

        public Color MainColor
        {
            set => BackgroundColor = value;
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

        public void SetCaption(StringBuilder str)
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

        void UpdateSize()
        {
            Background.Size = new Vector2(Text.preferredWidth + 5f, Text.preferredHeight + 2f);
            Background.Radius = 2f;
            BoxCollider.size = Background.Bounds.size;
        }

        void IDisplay.Suspend()
        {
            Caption = "";
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
            float distanceToCam = Settings.MainCameraTransform
                .InverseTransformDirection(unityPosition - Settings.MainCameraTransform.position).z;
            float size = 0.2f * Mathf.Max(distanceToCam, 0);

            float baseFrameSize = TfListener.Instance.FrameSize;
            float labelSize = baseFrameSize * size * 0.375f / 2;
            return labelSize;
        }
    }
}