#nullable enable

using System;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class XRButton : MonoBehaviour, IDisplay, IHasBounds, IRecyclable
    {
        [SerializeField] Texture2D[] icons = Array.Empty<Texture2D>();
        [SerializeField] TMP_Text? text;
        [SerializeField] MeshRenderer? iconMeshRenderer;
        [SerializeField] Transform? m_Transform;
        [SerializeField] BoxCollider? boxCollider;

        Color backgroundColor = Resource.Colors.HighlighterBackground;
        ButtonIcon? icon;
        Material? material;
        StaticBoundsControl? boundsControl;
        RoundedPlaneResource? background;

        RoundedPlaneResource Background =>
            background != null ? background : background = ResourcePool.RentDisplay<RoundedPlaneResource>(Transform);

        TMP_Text Text => text.AssertNotNull(nameof(text));
        MeshRenderer IconMeshRenderer => iconMeshRenderer.AssertNotNull(nameof(iconMeshRenderer));
        Material Material => material != null ? material : material = Instantiate(IconMeshRenderer.material);
        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));

        Transform IHasBounds.BoundsTransform => Transform;
        public Transform Transform => m_Transform != null ? m_Transform : (m_Transform = transform);
        public event Action? BoundsChanged;
        public event Action? Clicked;

        public enum ButtonIcon
        {
            Cross,
            Ok,
            Forward,
            Backward,
            Dialog,
            Up,
            Down,
        }

        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                if (background != null)
                {
                    background.Color = value;
                }
            }
        }

        public string Caption
        {
            set => Text.text = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public ButtonIcon? Icon
        {
            get => icon;
            set
            {
                icon = value;
                if (icon is { } validatedIcon)
                {
                    Material.mainTexture = icons[(int)validatedIcon];
                    IconMeshRenderer.enabled = true;
                }
                else
                {
                    IconMeshRenderer.enabled = false;
                }
            }
        }

        void Awake()
        {
            Icon = Icon;
            IconMeshRenderer.material = Material;
            boundsControl = new StaticBoundsControl(this);
            boundsControl.PointerUp += OnClick;
            Background.Size = new Vector2(1, 1);
            Background.Radius = 0.3f;
            Background.Color = BackgroundColor;
        }

        void Start()
        {
            BoundsChanged?.Invoke();
        }

        void OnClick()
        {
            GuiInputModule.PlayClickAudio(Transform.position);
            Clicked?.Invoke();
        }

        public void SplitForRecycle()
        {
            boundsControl?.Dispose();
            background.ReturnToPool();
        }

        Bounds? IHasBounds.Bounds => Bounds;
        Bounds? IDisplay.Bounds => Bounds;

        Bounds Bounds => new(BoxCollider.center, BoxCollider.size);

        public int Layer { get; set; }

        public void TriggerBoundsChanged()
        {
            BoundsChanged?.Invoke();
        }

        void IDisplay.Suspend()
        {
            BoundsChanged = null;
            Clicked = null;
        }
    }
}