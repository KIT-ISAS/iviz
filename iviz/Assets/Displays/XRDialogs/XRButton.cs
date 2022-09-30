#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRButton : MonoBehaviour, IDisplay, IHasBounds, IRecyclable
    {
        [SerializeField] string? caption;
        [SerializeField] XRIcon icon;
        [SerializeField] bool backgroundVisible = true;

        [SerializeField] TMP_Text? text;
        [SerializeField] Transform? m_Transform;
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] MeshMarkerDisplay? cylinder;
        [SerializeField] XRIconPlane? iconPlane;

        Color backgroundColor = Resource.Colors.TooltipBackground;
        Color color = new(0.47f, 0.68f, 1);
        StaticBoundsControl? boundsControl;
        RoundedPlaneDisplay? background;

        RoundedPlaneDisplay Background => ResourcePool.RentChecked(ref background, Transform);
        TMP_Text Text => text.AssertNotNull(nameof(text));
        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));
        Transform IHasBounds.BoundsTransform => BoxCollider.transform;
        Bounds? IHasBounds.Bounds => Bounds;
        MeshMarkerDisplay Cylinder => cylinder.AssertNotNull(nameof(cylinder));
        XRIconPlane IconObject => iconPlane.AssertNotNull(nameof(iconPlane));

        StaticBoundsControl BoundsControl =>
            boundsControl ??= new StaticBoundsControl(this) { FrameColumnWidth = 0.01f };

        public Bounds Bounds => BoxCollider.GetLocalBounds();
        public Transform Transform => this.EnsureHasTransform(ref m_Transform);
        public event Action? BoundsChanged;
        public event Action? Clicked;

        public Color BackgroundColor
        {
            set
            {
                backgroundColor = value;
                Background.Color = value;
            }
        }

        public Color Color
        {
            set
            {
                color = value;
                Cylinder.Color = value;
            }
        }

        public bool BackgroundVisible
        {
            set
            {
                backgroundVisible = value;
                Background.Visible = value;
            }
        }

        public string Caption
        {
            get => caption ?? "";
            set
            {
                caption = value;
                Text.text = value;
                UpdateSize();
            }
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public XRIcon Icon
        {
            set
            {
                icon = value;
                IconObject.Icon = value;
            }
        }

        public bool Interactable
        {
            set => BoundsControl.Interactable = value;
        }

        void Awake()
        {
            Icon = icon;
            Caption = Caption;

            IconObject.Color = Color.white;
            IconObject.EmissiveColor = Color.white.ScaledBy(0.5f);

            BoundsControl.PointerUp += OnClick;

            BoundsControl.StartDragging += () =>
            {
                Cylinder.EmissiveColor = color;
                IconObject.EmissiveColor = Color.white;
            };
            BoundsControl.EndDragging += () =>
            {
                Cylinder.EmissiveColor = Color.black;
                IconObject.EmissiveColor = Color.white.ScaledBy(0.5f);
            };
            BoundsControl.FrameColumnWidth = 0.0025f;

            Background.Size = new Vector2(1, 1);
            Background.Radius = 0.3f;
            BackgroundVisible = backgroundVisible;
            BackgroundColor = backgroundColor;
            Color = color;
        }

        void Start()
        {
            BoundsChanged?.Invoke();
        }

        void OnClick()
        {
            Resource.Audio.PlayAt(Transform.position, AudioClipType.Click);
            Clicked?.Invoke();
        }

        public void SplitForRecycle()
        {
            boundsControl?.Dispose();
            background.ReturnToPool();
        }

        public void OnDisable()
        {
            Cylinder.EmissiveColor = Color.black;
            IconObject.EmissiveColor = Color.white.WithValue(0.5f);
            GameThread.Post(() => boundsControl?.Reset()); // may cause issues with re-parenting while disabled
        }

        public void Suspend()
        {
            BoundsChanged = null;
            Clicked = null;
        }

        void UpdateSize()
        {
            Span<Rect> bounds = stackalloc[]
            {
                XRDialog.GetIconBounds(Cylinder),
                XRDialog.GetCaptionBounds(Text),
            };

            const float padding = 0.1f;
            var (center, size) = bounds.Combine(padding);
            var newBounds = new Bounds(center, size.WithZ(0.1f));

            if (Background.Visible)
            {
                var bgCenter = new Vector3(0, 0, -0.125f);
                var bgSize = new Vector3(1, 1, 0.25f);
                newBounds.Encapsulate(new Bounds(bgCenter, bgSize));
            }

            BoxCollider.SetLocalBounds(newBounds);
            BoundsChanged?.Invoke();
        }
    }
}