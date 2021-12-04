#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ManipulableFrame : MonoBehaviour, IHasBounds, IDisplay
    {
        static readonly Quaternion ZtoX = Quaternion.Euler(0, 90, 0);
        static readonly Quaternion ZtoY = Quaternion.Euler(90, 0, 0);

        [SerializeField] TranslationConstraintType translationConstraint;
        [SerializeField] RotationConstraintType rotationConstraint;

        [SerializeField] Transform? childTransform;
        [SerializeField] Bounds bounds;

        readonly List<WrapperBoundsControl> wrappers = new();
        IBoundsControl? control;

        public event Action? BoundsChanged;

        public TranslationConstraintType TranslationConstraint
        {
            set
            {
                if (translationConstraint == value)
                {
                    return;
                }

                translationConstraint = value;
                UpdateControls();
            }
        }

        public RotationConstraintType RotationConstraint
        {
            set
            {
                if (rotationConstraint == value)
                {
                    return;
                }

                rotationConstraint = value;
                UpdateControls();
            }
        }

        void UpdateControls()
        {
            control?.Dispose();
            foreach (var wrapper in wrappers)
            {
                wrapper.Dispose();
            }

            wrappers.Clear();

            var targetTransform = transform;

            control = translationConstraint switch
            {
                0 => new StaticBoundsControl(this),
                TranslationConstraintType.X =>
                    new LineBoundsControl(this, targetTransform, ZtoX),
                TranslationConstraintType.Y =>
                    new LineBoundsControl(this, targetTransform, ZtoY),
                TranslationConstraintType.Z =>
                    new LineBoundsControl(this, targetTransform, Quaternion.identity),
                TranslationConstraintType.X | TranslationConstraintType.Y =>
                    new PlaneBoundsControl(this, targetTransform, Quaternion.identity),
                TranslationConstraintType.X | TranslationConstraintType.Z =>
                    new PlaneBoundsControl(this, targetTransform, ZtoY),
                TranslationConstraintType.Y | TranslationConstraintType.Z =>
                    new PlaneBoundsControl(this, targetTransform, ZtoX),
                (TranslationConstraintType.X | TranslationConstraintType.Y | TranslationConstraintType.Z) or
                    TranslationConstraintType.Everything =>
                    new FixedDistanceBoundsControl(this, targetTransform),
                _ => throw new ArgumentException("Invalid flag " + translationConstraint)
            };

            if (translationConstraint is not
                (0 or TranslationConstraintType.X or TranslationConstraintType.Y or TranslationConstraintType.Z))
            {
                if ((translationConstraint & TranslationConstraintType.X) != 0)
                {
                    wrappers.Add(new LineWrapperBoundsControl(targetTransform, targetTransform, ZtoX));
                }

                if ((translationConstraint & TranslationConstraintType.Y) != 0)
                {
                    wrappers.Add(new LineWrapperBoundsControl(targetTransform, targetTransform, ZtoY));
                }

                if ((translationConstraint & TranslationConstraintType.Z) != 0)
                {
                    wrappers.Add(new LineWrapperBoundsControl(targetTransform, targetTransform, Quaternion.identity));
                }
            }

            if ((rotationConstraint & RotationConstraintType.XY) != 0)
            {
                wrappers.Add(new RotationWrapperBoundsControl(targetTransform, targetTransform, Quaternion.identity));
            }

            if ((rotationConstraint & RotationConstraintType.YZ) != 0)
            {
                wrappers.Add(new RotationWrapperBoundsControl(targetTransform, targetTransform, ZtoX));
            }

            if ((rotationConstraint & RotationConstraintType.XZ) != 0)
            {
                wrappers.Add(new RotationWrapperBoundsControl(targetTransform, targetTransform, ZtoY));
            }

            control.StartDragging += () => OnStartDragging(control);
            control.EndDragging += OnEndDragging;
            
            foreach (var wrapper in wrappers)
            {
                wrapper.StartDragging += () => OnStartDragging(wrapper);
                wrapper.EndDragging += OnEndDragging;
                wrapper.Bounds = bounds;
            }
        }

        void OnStartDragging(IBoundsControl selectedControl)
        {
            if (control != null && control != selectedControl)
            {
                control.Interactable = false;
            }

            foreach (var wrapper in wrappers)
            {
                if (wrapper != selectedControl)
                {
                    wrapper.Interactable = false;
                }
            }
        }

        void OnEndDragging()
        {
            if (control != null)
            {
                control.Interactable = true;
            }

            foreach (var wrapper in wrappers)
            {
                wrapper.Interactable = true;
            }
        }

        public Bounds Bounds
        {
            set
            {
                foreach (var wrapper in wrappers)
                {
                    wrapper.Bounds = value;
                }

                BoundsChanged?.Invoke();
            }
        }

        int IDisplay.Layer
        {
            set { }
        }

        void IDisplay.Suspend()
        {
            control?.Dispose();
            foreach (var wrapper in wrappers)
            {
                wrapper.Dispose();
            }

            translationConstraint = 0;
            rotationConstraint = 0;
        }

        void Awake()
        {
            UpdateControls();
        }

        Bounds? IHasBounds.Bounds => bounds;
        Transform IHasBounds.BoundsTransform => childTransform.AssertNotNull(nameof(childTransform));
        string? IHasBounds.Caption => null;
        bool IHasBounds.AcceptsHighlighter => true;
        Bounds? IDisplay.Bounds => bounds;
    }

    [Flags]
    public enum RotationConstraintType
    {
        XY = 1,
        XZ = 2,
        YZ = 4,
    }

    [Flags]
    public enum TranslationConstraintType
    {
        Everything = -1,
        X = 1,
        Y = 2,
        Z = 4,
    }
}