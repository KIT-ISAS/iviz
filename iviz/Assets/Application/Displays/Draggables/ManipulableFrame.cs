#nullable enable

using System.Linq;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using UnityEngine;

namespace Iviz.Displays
{
    public class ManipulableFrame : MonoBehaviour, IDisplay, IRecyclable
    {
        [SerializeField] SelectionFrame? frame = null;
        [SerializeField] BoxCollider? boxCollider = null;
        [SerializeField] FixedDistanceDraggable? draggable = null;

        [SerializeField] MeshMarkerHolderResource? xyResource = null;
        [SerializeField] MeshMarkerHolderResource? xzResource = null;
        [SerializeField] MeshMarkerHolderResource? yzResource = null;

        [SerializeField] RotationDraggable? xyRotation = null;
        [SerializeField] RotationDraggable? xzRotation = null;
        [SerializeField] RotationDraggable? yzRotation = null;

        Bounds bounds;

        SelectionFrame Frame => frame.AssertNotNull(nameof(frame));
        BoxCollider Collider => boxCollider.AssertNotNull(nameof(boxCollider));
        FixedDistanceDraggable Draggable => draggable.AssertNotNull(nameof(draggable));

        MeshMarkerHolderResource XYResource => xyResource.AssertNotNull(nameof(xyResource));
        MeshMarkerHolderResource XZResource => xzResource.AssertNotNull(nameof(xzResource));
        MeshMarkerHolderResource YZResource => yzResource.AssertNotNull(nameof(yzResource));

        RotationDraggable XYRotation => xyRotation.AssertNotNull(nameof(xyRotation));
        RotationDraggable XZRotation => xzRotation.AssertNotNull(nameof(xzRotation));
        RotationDraggable YZRotation => yzRotation.AssertNotNull(nameof(yzRotation));

        public Bounds Bounds
        {
            get => bounds;
            set
            {
                bounds = value;
                Frame.Size = bounds.size;
                Frame.Transform.localPosition = bounds.center;
                Collider.size = bounds.size;
                Collider.center = bounds.center;

                var (halfX, halfY, halfZ) = bounds.size / 2;

                var xzChildren = XZResource.Children;
                xzChildren[0].Transform.localPosition = new Vector3(halfX, 0, halfZ);
                xzChildren[1].Transform.localPosition = new Vector3(-halfX, 0, halfZ);
                xzChildren[2].Transform.localPosition = new Vector3(-halfX, 0, -halfZ);
                xzChildren[3].Transform.localPosition = new Vector3(halfX, 0, -halfZ);
                
                var xyChildren = XYResource.Children;
                xyChildren[0].Transform.localPosition = new Vector3(halfX, -halfY, 0);
                xyChildren[1].Transform.localPosition = new Vector3(-halfX, -halfY, 0);
                xyChildren[2].Transform.localPosition = new Vector3(-halfX, halfY, 0);
                xyChildren[3].Transform.localPosition = new Vector3(halfX, -halfY, 0);
                
                var yzChildren = YZResource.Children;
                yzChildren[0].Transform.localPosition = new Vector3(0, -halfY, -halfZ);
                yzChildren[1].Transform.localPosition = new Vector3(0, -halfY, halfZ);
                yzChildren[2].Transform.localPosition = new Vector3(0, halfY, halfZ);
                yzChildren[3].Transform.localPosition = new Vector3(0, halfY, -halfZ);
            }
        }

        public float ElementScale
        {
            set
            {
                var scaleVector = value * Vector3.one;
                var resources = 
                    XZResource.Children
                    .Concat(YZResource.Children)
                    .Concat(XZResource.Children);
                foreach (var resource in resources)
                {
                    resource.Transform.localScale = scaleVector;
                }
            }
        }

        public RotationConstraintType RotationConstraint
        {
            set
            {
                (XYResource.Visible, XZResource.Visible, YZResource.Visible) =
                    value switch
                    {
                        RotationConstraintType.XY => (true, false, false),
                        RotationConstraintType.XZ => (false, true, false),
                        RotationConstraintType.YZ => (false, false, true),
                        _ => (true, true, true),
                    };
            }
        }

        void Awake()
        {
            Frame.Color = Color.white.WithAlpha(0.25f);
            Bounds = new Bounds(Vector3.zero, Vector3.one);

            Draggable.Damping = 0.2f;
            Draggable.StateChanged += () =>
            {
                bool anyRotationDragging = XYRotation.IsDragging || XZRotation.IsDragging || YZRotation.IsDragging; 
                bool isDragging = Draggable.IsDragging;
                bool isActive = isDragging || (Draggable.IsHovering & !anyRotationDragging);
                Frame.Color = isActive ? Color.white : Color.white.WithAlpha(0.25f);
                Frame.EmissiveColor = isDragging ? Color.blue : Color.black;
                Frame.ColumnWidth = isActive ? 0.010f : 0.005f;
            };

            XYRotation.Damping = 0.1f;
            XZRotation.Damping = 0.1f;
            YZRotation.Damping = 0.1f;

            XYRotation.StateChanged += () => UpdateMarker(XYResource, XYRotation);
            XZRotation.StateChanged += () => UpdateMarker(XZResource, XZRotation);
            YZRotation.StateChanged += () => UpdateMarker(YZResource, YZRotation);

            RotationConstraint = RotationConstraintType.XZ;
        }

        static void UpdateMarker(ISupportsColor resource, IScreenDraggable draggable)
        {
            bool isDragging = draggable.IsDragging;
            bool isActive = isDragging || draggable.IsHovering;
            resource.Color = isActive ? Color.white : Color.white.WithAlpha(0.25f);
            resource.EmissiveColor = isDragging ? Color.blue : Color.black;
        }

        public int Layer { get; set; }

        public void Suspend()
        {
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        Bounds? IDisplay.Bounds => Bounds;

        public void SplitForRecycle()
        {
            frame.ReturnToPool();
        }
    }
}