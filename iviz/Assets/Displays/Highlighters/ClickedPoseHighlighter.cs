#nullable enable

using System.Threading;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public sealed class ClickedPoseHighlighter : IAnimatable
    {
        readonly FrameNode node;
        readonly MeshMarkerDisplay reticle;
        readonly Tooltip tooltip;

        public CancellationToken Token { get; }
        public float Duration { get; }

        public ClickedPoseHighlighter(in Pose unityPose, string? customText = null, float duration = 1,
            CancellationToken token = default)
        {
            float labelSize = Tooltip.GetRecommendedSize(unityPose.position);
            float frameSize = labelSize * 10;

            Duration = duration;
            Token = token;

            node = new FrameNode("Clicked Pose Highlighter", TfModule.FixedFrame);
            node.Transform.SetPose(unityPose);

            reticle = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Reticle, node.Transform);
            reticle.EnableShadows = false;
            reticle.Color = Color.white.WithAlpha(0.3f);
            reticle.EmissiveColor = Color.white;
            reticle.Layer = LayerType.IgnoreRaycast;

            var localPose = new Pose(0.002f * Vector3.up, Quaternions.Rotate270AroundX);
            reticle.Transform.SetLocalPose(localPose);
            reticle.Transform.localScale = frameSize * Vector3.one;

            tooltip = ResourcePool.RentDisplay<Tooltip>(node.Transform);
            tooltip.CaptionColor = Color.white;
            tooltip.Color = Resource.Colors.TooltipBackground;
            tooltip.Scale = labelSize;
            tooltip.Transform.localPosition = 2f * (frameSize * 0.3f + labelSize) * Vector3.up;
            tooltip.PointToCamera();

            if (customText != null)
            {
                tooltip.Caption = customText;
                return;
            }

            using var description = BuilderPool.Rent();
            RosUtils.FormatPose(TfModule.RelativeToFixedFrame(unityPose), description,
                RosUtils.PoseFormat.OnlyPosition, 2);
            tooltip.SetCaption(description);
        }

        public void Update(float t)
        {
            float alpha = 1 - t * t;
            tooltip.CaptionColor = Color.white.WithAlpha(alpha);
            tooltip.Color = Resource.Colors.TooltipBackground.WithAlpha(alpha);
            reticle.Color = Color.white.WithAlpha(0.3f * alpha);
        }

        public void Dispose()
        {
            reticle.ReturnToPool(Resource.Displays.Reticle);
            tooltip.ReturnToPool();
            node.Dispose();
        }
    }
}