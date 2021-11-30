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
        readonly CancellationTokenSource tokenSource;

        public CancellationToken Token => tokenSource.Token;
        public float Duration => 1;

        public ClickedPoseHighlighter(in Pose unityPose)
        {
            float labelSize = Tooltip.GetRecommendedSize(unityPose.position);
            float frameSize = labelSize * 10;

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
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground;
            tooltip.Layer = LayerType.IgnoreRaycast;
            tooltip.Scale = labelSize;
            tooltip.Transform.localPosition = 2f * (frameSize * 0.3f + labelSize) * Vector3.up;
            tooltip.PointToCamera();

            tokenSource = new CancellationTokenSource();

            using var description = BuilderPool.Rent();
            RosUtils.FormatPose(unityPose, description, RosUtils.PoseFormat.OnlyPosition, 2);
            tooltip.SetCaption(description);
        }

        public void Update(float t)
        {
            float alpha = Mathf.Sqrt(1 - t);
            tooltip.CaptionColor = Color.white.WithAlpha(alpha);
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground.WithAlpha(alpha);
            reticle.Color = Color.white.WithAlpha(0.3f * alpha);
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            reticle.ReturnToPool(Resource.Displays.Reticle);
            tooltip.ReturnToPool();
            node.DestroySelf();
        }
    }
}