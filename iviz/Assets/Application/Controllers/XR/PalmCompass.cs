#nullable enable

using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Controllers.XR
{
    public class PalmCompass : MonoBehaviour, IDisplay, IRecyclable
    {
        [SerializeField] MeshMarkerDisplay? ring;
        AxisFrameDisplay? frame;
        Tooltip? tooltip;

        Transform? mTransform;
        public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        MeshMarkerDisplay Ring => ring.AssertNotNull(nameof(ring));

        AxisFrameDisplay Frame =>
            frame != null ? frame : (frame = ResourcePool.RentDisplay<AxisFrameDisplay>(Transform));

        Tooltip Tooltip => tooltip != null ? tooltip : (tooltip = ResourcePool.RentDisplay<Tooltip>(Transform));

        void Awake()
        {
            Ring.EnableShadows = false;
            Frame.Transform.localPosition = new Vector3(0, 0.15f, 0);
            Frame.EnableShadows = false;
            Tooltip.Transform.localPosition = new Vector3(0, 0.55f, 0);
            Tooltip.Transform.localScale = Vector3.one * 0.02f;
            Tooltip.Color = Resource.Colors.TooltipBackground;
            Tooltip.Caption = "";
        }

        void LateUpdate()
        {
            float scale = Transform.lossyScale.x;
            Frame.Transform.rotation = Quaternions.Rotate90AroundY;
            Tooltip.Transform.position = Transform.position + new Vector3(0, 0.55f * scale, 0);
            using var description = BuilderPool.Rent();
            RosUtils.FormatPose(TfModule.RelativeToFixedFrame(Transform.AsPose()), description,
                RosUtils.PoseFormat.OnlyPosition, 2);
            Tooltip.SetCaption(description);
            Tooltip.PointToCamera();
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