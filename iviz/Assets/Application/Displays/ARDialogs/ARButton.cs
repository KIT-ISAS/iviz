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
    public sealed class ARButton : MarkerResource, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,
        IRecyclable
    {
        [SerializeField] Texture2D[] icons = null;
        [SerializeField] TextMesh text = null;
        [SerializeField] MeshRenderer iconMeshRenderer = null;

        [SerializeField] Color backgroundColor = new Color(0, 0.2f, 0.5f);
        [SerializeField] MeshMarkerResource background = null;

        [SerializeField] BoxCollider colliderForFrame = null;

        Material material;

        [NotNull]
        Material Material => material != null ? material : (material = Instantiate(iconMeshRenderer.material));

        [CanBeNull] BoundaryFrame frame;

        [NotNull]
        BoundaryFrame Frame
        {
            get
            {
                if (frame != null)
                {
                    return frame;
                }

                frame = ResourcePool.RentDisplay<BoundaryFrame>(Transform);
                frame.Color = Color.white.WithAlpha(0.5f);
                return frame;
            }
        }

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
                    // TODO
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

        ButtonIcon icon = ButtonIcon.Cross;

        public ButtonIcon Icon
        {
            get => icon;
            set
            {
                icon = value;
                Material.mainTexture = value == ButtonIcon.Backward
                    ? icons[(int)ButtonIcon.Forward]
                    : icons[(int)value];
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

        void Awake()
        {
            Icon = Icon;
            iconMeshRenderer.material = Material;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();

            var assetHolder = UnityEngine.Resources.Load<GameObject>("App Asset Holder").GetComponent<AppAssetHolder>();
            AudioSource.PlayClipAtPoint(assetHolder.Click, Transform.position);
        }

        public void ClearSubscribers()
        {
            Clicked = null;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            Frame.Bounds = new Bounds(colliderForFrame.center,
                colliderForFrame.size.Mult(colliderForFrame.transform.localScale));
            Frame.Visible = true;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            Frame.Visible = false;
        }

        void OnDisable()
        {
            if (frame != null)
            {
                frame.Visible = false;
            }
        }

        public void OnDialogDisabled()
        {
        }

        public void SplitForRecycle()
        {
            if (frame != null)
            {
                frame.ReturnToPool();
            }
        }
    }
}