using System;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class ARButton : MarkerResource, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Texture2D[] icons;
        [SerializeField] TextMesh text;
        [SerializeField] MeshRenderer iconMeshRenderer;

        [SerializeField] Color backgroundColor = new Color(0, 0.2f, 0.5f);
        [SerializeField] MeshMarkerResource background = null;

        [SerializeField] BoxCollider colliderForFrame = null;

        Material material;

        [NotNull]
        Material Material => material != null ? material : (material = Instantiate(iconMeshRenderer.material));

        [CanBeNull] BoundaryFrame frame;

        public event Action Clicked;

        public enum ButtonIcon
        {
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
                }
            }
        }

        public string Caption
        {
            get => text.text;
            set => text.text = value;
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        ButtonIcon icon;

        public ButtonIcon Icon
        {
            get => icon;
            set
            {
                icon = value;
                Material.mainTexture = value == ButtonIcon.Backward
                    ? icons[(int) ButtonIcon.Forward]
                    : icons[(int) value];
                (float x, _, float z) = iconMeshRenderer.transform.localRotation.eulerAngles;

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

                iconMeshRenderer.transform.localRotation = rotation;
            }
        }

        protected override void Awake()
        {
            Icon = ButtonIcon.Ok;
            iconMeshRenderer.material = Material;

            if (Settings.IsHololens)
            {
                var meshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
                foreach (var meshRenderer in meshRenderers)
                {
                    meshRenderer.material = Resource.Materials.Lit.Object;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public void ClearSubscribers()
        {
            Clicked = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            frame = ResourcePool.RentDisplay<BoundaryFrame>(transform);
            frame.Color = Color.white.WithAlpha(0.5f);
            frame.Bounds = new Bounds(colliderForFrame.center, colliderForFrame.size);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            frame.ReturnToPool();
            frame = null;
        }

        public override void Suspend()
        {
            base.Suspend();
            if (frame != null)
            {
                frame.ReturnToPool();
            }
        }
    }
}