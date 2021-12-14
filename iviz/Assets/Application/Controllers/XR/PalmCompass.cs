#nullable enable

using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Controllers.XR
{
    public class PalmCompass : MonoBehaviour, IDisplay, IRecyclable
    {
        [SerializeField] MeshMarkerResource ring;
        AxisFrameResource? frame;
        Tooltip? tooltip;

        Transform? mTransform;
        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        MeshMarkerResource Ring => ring.AssertNotNull(nameof(ring));
        AxisFrameResource Frame =>
            frame != null ? frame : (frame = ResourcePool.RentDisplay<AxisFrameResource>(Transform));

        Tooltip Tooltip => tooltip != null ? tooltip : (tooltip = ResourcePool.RentDisplay<Tooltip>(Transform));

        void Awake()
        {
            Ring.ShadowsEnabled = false;
            Frame.Transform.localPosition = new Vector3(0, 0.15f, 0);
            Frame.ShadowsEnabled = false;
            Tooltip.Transform.localPosition = new Vector3(0, 0.55f, 0);
            Tooltip.Transform.localScale = Vector3.one * 0.02f;
            Tooltip.Color = Resource.Colors.TooltipBackground;
            Tooltip.Caption = "abcd";
        }

        public Bounds? Bounds => Frame.Bounds;

        public int Layer
        {
            set
            {
                Frame.Layer = value;
                Tooltip.Layer = value;
                Ring.Layer = value;
            }
        }

        void Update()
        {
            using var description = BuilderPool.Rent();
            RosUtils.FormatPose(Transform.AsPose(), description, RosUtils.PoseFormat.OnlyPosition);
            Frame.Transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            Tooltip.SetCaption(description);
            Tooltip.Transform.position = Transform.position + new Vector3(0, 0.55f, 0);
        }
        
        public void Suspend()
        {
        }

        public void SplitForRecycle()
        {
            Frame.ReturnToPool();
            Tooltip.ReturnToPool();
        }
    }
}