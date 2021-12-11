#nullable enable

using System;
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
    public sealed class TfFrameHighlighter : IAnimatable
    {
        readonly AxisFrameResource axisResource;
        readonly Tooltip tooltip;
        readonly FrameNode node;

        public CancellationToken Token => default;
        public float Duration => 1;

        public TfFrameHighlighter(TfFrame frame)
        {
            node = FrameNode.Instantiate("Frame Highlighter");

            axisResource = ResourcePool.RentDisplay<AxisFrameResource>(node.Transform);
            axisResource.ShadowsEnabled = false;
            axisResource.Emissive = 1;
            axisResource.OverrideMaterial(Resource.Materials.TransparentLitAlwaysVisible.Object);
            axisResource.Tint = Color.white;

            tooltip = ResourcePool.RentDisplay<Tooltip>(node.Transform);
            tooltip.Color = Resource.Colors.HighlighterBackground;
            tooltip.CaptionColor = Color.white;
            tooltip.Caption = "";

            if (!frame.IsAlive) // ?
            {
                node.Visible = false;
                return;
            }
            
            node.AttachTo(frame);
        }

        public void Update(float t)
        {
            var nodePosition = node.Transform.position;
            float distanceToCam = Settings.MainCameraTransform.InverseTransformPoint(nodePosition).z;

            float size = 0.25f * Mathf.Abs(distanceToCam);
            float clampedSize = Mathf.Max(size, 2);

            float baseFrameSize = TfListener.Instance.FrameSize;
            float frameSize = baseFrameSize * clampedSize;
            float labelSize = baseFrameSize * Mathf.Max(size * 0.375f / 2, 0.15f);

            float alpha = Mathf.Sqrt(1 - t);
            var color = Color.white.WithAlpha(alpha);

            axisResource.AxisLength = frameSize;
            axisResource.Tint = color;

            tooltip.Scale = labelSize;
            tooltip.Transform.position = nodePosition + (1.2f * axisResource.AxisLength + 5 * labelSize) * Vector3.up;
            tooltip.CaptionColor = color;
            tooltip.Color = Resource.Colors.HighlighterBackground.WithAlpha(alpha);
            if (t == 0)
            {
                tooltip.PointToCamera();
            }

            using var description = BuilderPool.Rent();
            description.Append("<b>").Append(node.ParentId).Append("</b>\n");
            RosUtils.FormatPose(node.Transform.AsPose(), description, RosUtils.PoseFormat.OnlyPosition);
            tooltip.SetCaption(description);
        }

        public void Dispose()
        {
            axisResource.ReturnToPool();
            tooltip.ReturnToPool();
            node.DestroySelf();
        }
    }
}