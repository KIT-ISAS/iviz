using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public class TfFrameHighlighter : DisplayWrapperResource
    {
        const float HighlightDuration = 2.0f;

        AxisFrameResource resource;
        TextMarkerResource label;

        FrameNode node;
        float? highlightFrameStart;

        protected override IDisplay Display => resource;

        void Awake()
        {
            node = FrameNode.Instantiate("Frame Highlighter");
            node.Transform.SetParentLocal(Transform);

            resource = ResourcePool.RentDisplay<AxisFrameResource>(node.Transform);
            resource.CastsShadows = false;
            resource.Emissive = 1;
            resource.OverrideMaterial(Resource.Materials.TransparentLitAlwaysVisible.Object);
            resource.Layer = LayerType.IgnoreRaycast;

            label = ResourcePool.RentDisplay<TextMarkerResource>(node.Transform);
            label.AlwaysVisible = true;
            label.Layer = LayerType.IgnoreRaycast;
        }

        public void HighlightFrame([NotNull] string frameId)
        {
            node.AttachTo(frameId);
            highlightFrameStart = Time.time;
            resource.Tint = Color.white;
            label.Color = Color.white;
            label.Text = frameId;

            float distanceToCam = Settings.MainCameraTransform.InverseTransformPoint(node.Transform.position).z;
            
            float size = 0.25f * distanceToCam;
            float clampedSize = Mathf.Max(size, 2);

            float baseFrameSize = TfListener.Instance.FrameSize;
            float frameSize = baseFrameSize * clampedSize;
            float labelSize = baseFrameSize * size;
            
            resource.AxisLength = frameSize;
            label.ElementSize = labelSize;
            label.BillboardOffset = (1.2f * resource.AxisLength + label.ElementSize) * Vector3.up;
        }

        void Update()
        {
            if (highlightFrameStart == null)
            {
                return;
            }

            float alpha = 1 - (Time.time - highlightFrameStart.Value) / HighlightDuration;
            if (alpha < 0)
            {
                this.ReturnToPool();
                return;
            }

            alpha = Mathf.Sqrt(alpha);
            var color = Color.white.WithAlpha(alpha);
            label.Color = color;
            resource.Tint = color;

            var (pX, pY, pZ) = TfListener.RelativePositionToFixedFrame(node.Transform.position).Unity2RosVector3();
            string px = pX.ToString("#,0.##", UnityUtils.Culture);
            string py = pY.ToString("#,0.##", UnityUtils.Culture);
            string pz = pZ.ToString("#,0.##", UnityUtils.Culture);

            label.Text = $"{node.ParentId}\n{px}, {py}, {pz}";
        }

        public override void Suspend()
        {
            node.Parent = null;
            node.Transform.SetParentLocal(Transform);
            highlightFrameStart = null;
        }

        public override void SplitForRecycle()
        {
            resource.CastsShadows = true;
            resource.Emissive = 0;
            resource.OverrideMaterial(null);
            label.ReturnToPool();
            base.SplitForRecycle();
        }
    }
}