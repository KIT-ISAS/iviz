#nullable enable

using System;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class ARButton : MonoBehaviour, IDisplay, IHasBounds, IRecyclable
    {
        [SerializeField] Texture2D[] icons = Array.Empty<Texture2D>();
        [SerializeField] TMP_Text? text;
        [SerializeField] MeshRenderer? iconMeshRenderer;
        [SerializeField] MeshMarkerResource? cylinder;
        [SerializeField] RoundedPlaneResource? background;
        [SerializeField] Transform? m_Transform;
        [SerializeField] BoxCollider? boxCollider;

        Color backgroundColor = Resource.Colors.HighlighterBackground;
        ButtonIcon icon = ButtonIcon.Cross;
        Material? material;
        StaticBoundsControl? boundsControl;

        RoundedPlaneResource Background =>
            background != null ? background : background = ResourcePool.RentDisplay<RoundedPlaneResource>(Transform);

        TMP_Text Text => text.AssertNotNull(nameof(text));
        MeshRenderer IconMeshRenderer => iconMeshRenderer.AssertNotNull(nameof(iconMeshRenderer));
        Material Material => material != null ? material : material = Instantiate(IconMeshRenderer.material);
        MeshMarkerResource Cylinder => cylinder.AssertNotNull(nameof(cylinder));
        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));

        public Transform Transform => m_Transform != null ? m_Transform : (m_Transform = transform);
        public event Action? Clicked;


        public enum ButtonIcon
        {
            None,
            Cross,
            Ok,
            Forward,
            Backward
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

        Transform IHasBounds.BoundsTransform => Transform;
        string? IHasBounds.Caption => null;
        bool IHasBounds.AcceptsHighlighter => false;
        public event Action? BoundsChanged;

        public string Caption
        {
            set => Text.text = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public ButtonIcon Icon
        {
            get => icon;
            set
            {
                icon = value;
                Material.mainTexture = value == ButtonIcon.Backward
                    ? icons[(int)ButtonIcon.Forward]
                    : icons[(int)value];
                (float x, _, float z) = IconMeshRenderer.transform.localRotation.eulerAngles;

                Quaternion rotation;
                switch (value)
                {
                    case ButtonIcon.Cross:
                        rotation = Quaternion.Euler(x, 45, z);
                        break;
                    case ButtonIcon.Backward:
                        rotation = Quaternion.Euler(x, 0, z);
                        break;
                    default:
                        rotation = Quaternion.Euler(x, 180, z);
                        break;
                }

                IconMeshRenderer.transform.localRotation = rotation;
            }
        }

        void Awake()
        {
            Icon = Icon;
            IconMeshRenderer.material = Material;
            boundsControl = new StaticBoundsControl(this);
            Background.Size = new Vector2(1, 1);
            Background.Radius = 0.3f;
            Background.Color = BackgroundColor;
        }

        void OnClick()
        {
            GuiInputModule.PlayClickAudio(Transform.position);
            Clicked?.Invoke();
        }

        public void Stop()
        {
            Clicked = null;
            boundsControl?.Dispose();
        }

        public void SplitForRecycle()
        {
        }

        Bounds? IHasBounds.Bounds => Bounds;
        Bounds? IDisplay.Bounds => Bounds;

        Bounds Bounds
        {
            get => new(BoxCollider.center, BoxCollider.size);
            set
            {
                BoxCollider.center = value.center;
                BoxCollider.size = value.size;
            }
        }

        public int Layer { get; set; }

        void IDisplay.Suspend()
        {
        }
    }
}