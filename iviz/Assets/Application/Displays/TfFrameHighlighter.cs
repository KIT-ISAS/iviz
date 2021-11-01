using System.Text;
using Iviz.App.ARDialogs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Iviz.Displays
{
    public class TfFrameHighlighter : DisplayWrapperResource
    {
        const float HighlightDuration = 2.0f;

        [SerializeField] AxisFrameResource axisResource;
        [SerializeField] Tooltip tooltip;

        [SerializeField] FrameNode node;
        float? highlightFrameStart;

        protected override IDisplay Display => axisResource;

        void Awake()
        {
            node = FrameNode.Instantiate("Frame Highlighter");
            node.Transform.SetParentLocal(Transform);

            axisResource = ResourcePool.RentDisplay<AxisFrameResource>(node.Transform);
            axisResource.CastsShadows = false;
            axisResource.Emissive = 1;
            axisResource.OverrideMaterial(Resource.Materials.TransparentLitAlwaysVisible.Object);
            axisResource.Layer = LayerType.IgnoreRaycast;

            tooltip = ResourcePool.RentDisplay<Tooltip>(node.Transform);
            tooltip.Layer = LayerType.IgnoreRaycast;
            tooltip.UseAnimation = false;
        }

        public void HighlightFrame([NotNull] string frameId)
        {
            node.AttachTo(frameId);
            highlightFrameStart = Time.time;
            axisResource.Tint = Color.white;
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground;
            tooltip.CaptionColor = Color.white;
            tooltip.Caption = "";

            float distanceToCam = Settings.MainCameraTransform.InverseTransformPoint(node.Transform.position).z;

            float size = 0.25f * Mathf.Abs(distanceToCam);
            float clampedSize = Mathf.Max(size, 2);

            float baseFrameSize = TfListener.Instance.FrameSize;
            float frameSize = baseFrameSize * clampedSize;
            float labelSize = baseFrameSize * Mathf.Max(size * 0.375f / 2, 0.25f);

            axisResource.AxisLength = frameSize;
            tooltip.Scale = labelSize;
            tooltip.Transform.localPosition = (1.2f * axisResource.AxisLength + 5 * labelSize) * Vector3.up;
            tooltip.PointToCamera();
        }

        void Update()
        {
            if (highlightFrameStart == null)
            {
                return;
            }

            float srcAlpha = 1 - (Time.time - highlightFrameStart.Value) / HighlightDuration;

            if (srcAlpha < 0)
            {
                this.ReturnToPool();
                return;
            }

            float alpha = Mathf.Sqrt(srcAlpha);
            var color = Color.white.WithAlpha(alpha);
            axisResource.Tint = color;
            tooltip.CaptionColor = color;
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground.WithAlpha(alpha);

            var (pX, pY, pZ) = TfListener.RelativePositionToFixedFrame(node.Transform.position).Unity2RosVector3();
            string px = pX.ToString("#,0.##", UnityUtils.Culture);
            string py = pY.ToString("#,0.##", UnityUtils.Culture);
            string pz = pZ.ToString("#,0.##", UnityUtils.Culture);

            StringBuilder str = BuilderPool.Rent();
            try
            {
                str.Append("<font=Bold>").Append(node.ParentId).Append("</font>\n");
                str.Append(px).Append(", ").Append(py).Append(", ").Append(pz);
                tooltip.SetCaption(str);
            }
            finally
            {
                BuilderPool.Return(str);
            }
        }

        public override void Suspend()
        {
            node.Parent = null;
            node.Transform.SetParentLocal(Transform);
            highlightFrameStart = null;
        }

        public override void SplitForRecycle()
        {
            axisResource.CastsShadows = true;
            axisResource.Emissive = 0;
            axisResource.OverrideMaterial(null);
            tooltip.ReturnToPool();
            base.SplitForRecycle();
        }
    }
}