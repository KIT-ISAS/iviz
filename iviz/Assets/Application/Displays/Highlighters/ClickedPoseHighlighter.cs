#nullable enable

using System.Threading;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public sealed class ClickedPoseHighlighter : IAnimatable
    {
        readonly FrameNode node;
        readonly MeshMarkerResource reticle;
        readonly Tooltip tooltip;

        public CancellationToken Token => default;
        public float Duration { get; }

        public ClickedPoseHighlighter(in Pose unityPose, string? customText = null, float duration = 1)
        {
            float labelSize = Tooltip.GetRecommendedSize(unityPose.position);
            float frameSize = labelSize * 10;

            Duration = duration;
            
            node = FrameNode.Instantiate("[Clicked Pose Highlighter]");
            node.Transform.SetPose(unityPose);

            reticle = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Reticle, node.Transform);
            reticle.ShadowsEnabled = false;
            reticle.Color = Color.white.WithAlpha(0.3f);
            reticle.EmissiveColor = Color.white;
            reticle.Layer = LayerType.IgnoreRaycast;

            var localPose = new Pose(0.002f * Vector3.up, Quaternion.AngleAxis(90, Vector3.left));
            reticle.Transform.SetLocalPose(localPose);
            reticle.Transform.localScale = frameSize * Vector3.one;

            tooltip = ResourcePool.RentDisplay<Tooltip>(node.Transform);
            tooltip.CaptionColor = Color.white;
            tooltip.Color = Resource.Colors.TooltipBackground;
            tooltip.Layer = LayerType.IgnoreRaycast;
            tooltip.Scale = labelSize;
            tooltip.Transform.localPosition = 2f * (frameSize * 0.3f + labelSize) * Vector3.up;
            tooltip.PointToCamera();

            if (customText == null)
            {
                using var description = BuilderPool.Rent();
                RosUtils.FormatPose(TfListener.RelativeToFixedFrame(unityPose), description,
                    RosUtils.PoseFormat.OnlyPosition, 2);
                tooltip.SetCaption(description);
            }
            else
            {
                tooltip.Caption = customText;
            }
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