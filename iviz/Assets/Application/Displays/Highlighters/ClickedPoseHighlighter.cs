#nullable enable

using System.Threading;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using Animator = Iviz.Core.Animator;

namespace Iviz.Displays
{
    public sealed class ClickedPoseHighlighter
    {
        readonly FrameNode node;
        readonly MeshMarkerResource flatSphere;
        readonly Tooltip tooltip;
        readonly CancellationTokenSource tokenSource;

        public ClickedPoseHighlighter()
        {
            node = FrameNode.Instantiate("[Clicked Pose Highlighter]");
            flatSphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, node.Transform);
            flatSphere.ShadowsEnabled = false;
            flatSphere.Color = Color.white.WithAlpha(0.3f);
            flatSphere.EmissiveColor = Color.white;
            flatSphere.Layer = LayerType.IgnoreRaycast;

            tooltip = ResourcePool.RentDisplay<Tooltip>(node.Transform);
            tooltip.CaptionColor = Color.white;
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground;
            tooltip.Layer = LayerType.IgnoreRaycast;
            
            tokenSource = new CancellationTokenSource();
        }

        public void Highlight(in Pose absolutePose)
        {
            float distanceToCam = Settings.MainCameraTransform
                .InverseTransformDirection(absolutePose.position - Settings.MainCameraTransform.position).z;
            float size = 0.2f * distanceToCam;

            float baseFrameSize = TfListener.Instance.FrameSize;
            float frameSize = baseFrameSize * Mathf.Max(1, size);
            float labelSize = baseFrameSize * size * 0.375f / 2;

            node.Transform.SetPose(absolutePose);

            flatSphere.Transform.localScale = new Vector3(frameSize, frameSize * 0.1f, frameSize);

            tooltip.Scale = labelSize;
            tooltip.Transform.localPosition = 2f * (frameSize * 0.3f + labelSize) * Vector3.up;
            tooltip.PointToCamera();

            var (pX, pY, pZ) = TfListener.RelativePositionToFixedFrame(node.Transform.position).Unity2RosVector3();
            string px = pX.ToString("#,0.0", UnityUtils.Culture);
            string py = pY.ToString("#,0.0", UnityUtils.Culture);
            string pz = pZ.ToString("#,0.0", UnityUtils.Culture);

            tooltip.Caption = $"{px}, {py}, {pz}";

            Animator.Spawn(tokenSource.Token, 1.0f, UpdateFrame);
        }

        void UpdateFrame(float t)
        {
            if (t >= 1)
            {
                tokenSource.Cancel();
                flatSphere.ReturnToPool(Resource.Displays.Sphere);
                tooltip.ReturnToPool();
                node.DestroySelf();
                return;
            }

            float alpha = Mathf.Sqrt(1 - t);
            tooltip.CaptionColor = Color.white.WithAlpha(alpha);
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground.WithAlpha(alpha);
            flatSphere.Color = Color.white.WithAlpha(0.3f * alpha);
        }
    }
}