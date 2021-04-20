using System;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.XmlRpc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class ARButton : MarkerResource, IPointerClickHandler
    {
        [SerializeField] Texture2D[] icons;
        [SerializeField] TextMesh text;
        [SerializeField] MeshRenderer iconMeshRenderer;

        Material material;
        Material Material => material != null ? material : (material = Instantiate(iconMeshRenderer.material));

        public event Action Clicked;

        public enum ButtonIcon
        {
            Cross,
            Ok,
            Forward,
            Backward
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
    }
}