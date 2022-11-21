#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Resources;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
{
    public sealed class XRJoystick : MonoBehaviour, IDisplay
    {
        [SerializeField] SpringDisc? disc;
        [SerializeField] TMP_Text? text;
        [SerializeField] bool backgroundVisible;
        Transform? mTransform;
        RoundedPlaneDisplay? background;
        
        RoundedPlaneDisplay Background => ResourcePool.RentChecked(ref background, Transform);
        SpringDisc Disc => disc.AssertNotNull(nameof(disc));
        TMP_Text Text => text.AssertNotNull(nameof(text));

        public Transform Transform => this.EnsureHasTransform(ref mTransform);
        
        public event Action<float>? Changed;
        public event Action? PointerUp;

        public bool Interactable
        {
            set => Disc.Interactable = value;
        }

        public Color Color
        {
            set => Disc.Color = value;
        }

        public string Caption
        {
            set => Text.text = value;
        }

        void Awake()
        {
            Disc.Moved += f =>
            {
                Changed?.Invoke(f.z);
            };

            Disc.PointerUp += () =>
            {
                PointerUp?.Invoke();
            };

            if (backgroundVisible)
            {
                Background.Size = new Vector2(1, 1.3f);
                Background.Transform.localPosition = new Vector3(0, 0.125f, 0);
                Background.Radius = 0.3f;
                Background.Color = Resource.Colors.TooltipBackground;
            }
        }

        public void Suspend()
        {
            Changed = null;
            PointerUp = null;
        }
    }
}