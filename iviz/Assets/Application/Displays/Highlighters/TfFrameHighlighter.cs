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
using Animator = Iviz.Core.Animator;

namespace Iviz.Displays
{
    public sealed class TfFrameHighlighter
    {
        readonly AxisFrameResource axisResource;
        readonly Tooltip tooltip;
        readonly FrameNode node;
        readonly CancellationTokenSource tokenSource;

        public TfFrameHighlighter()
        {
            node = FrameNode.Instantiate("Frame Highlighter");

            axisResource = ResourcePool.RentDisplay<AxisFrameResource>(node.Transform);
            axisResource.ShadowsEnabled = false;
            axisResource.Emissive = 1;
            axisResource.OverrideMaterial(Resource.Materials.TransparentLitAlwaysVisible.Object);
            axisResource.Tint = Color.white;

            tooltip = ResourcePool.RentDisplay<Tooltip>(node.Transform);
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground;
            tooltip.CaptionColor = Color.white;
            tooltip.Caption = "";

            tokenSource = new CancellationTokenSource();
        }

        public void Highlight(TfFrame frame)
        {
            node.AttachTo(frame);

            float distanceToCam = Settings.MainCameraTransform.InverseTransformPoint(node.Transform.position).z;

            float size = 0.25f * Mathf.Abs(distanceToCam);
            float clampedSize = Mathf.Max(size, 2);

            float baseFrameSize = TfListener.Instance.FrameSize;
            float frameSize = baseFrameSize * clampedSize;
            float labelSize = baseFrameSize * Mathf.Max(size * 0.375f / 2, 0.15f);

            axisResource.AxisLength = frameSize;
            tooltip.Scale = labelSize;
            tooltip.Transform.localPosition = (1.2f * axisResource.AxisLength + 5 * labelSize) * Vector3.up;
            tooltip.PointToCamera();
            
            Animator.Spawn(tokenSource.Token, 2.0f, UpdateFrame);
        }

        void UpdateFrame(float t)
        {
            if (t >= 1)
            {
                tokenSource.Cancel();
                axisResource.ReturnToPool();
                tooltip.ReturnToPool();
                node.DestroySelf();
                return;
            }

            float alpha = Mathf.Sqrt(1 - t);
            var color = Color.white.WithAlpha(alpha);
            axisResource.Tint = color;
            tooltip.CaptionColor = color;
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground.WithAlpha(alpha);

            var (pX, pY, pZ) = TfListener.RelativePositionToFixedFrame(node.Transform.position).Unity2RosVector3();
            string px = pX.ToString("#,0.##", UnityUtils.Culture);
            string py = pY.ToString("#,0.##", UnityUtils.Culture);
            string pz = pZ.ToString("#,0.##", UnityUtils.Culture);

            var str = BuilderPool.Rent();
            try
            {
                str.Append("<b>").Append(node.ParentId).Append("</b>\n");
                str.Append(px).Append(", ").Append(py).Append(", ").Append(pz);
                tooltip.SetCaption(str);
            }
            finally
            {
                BuilderPool.Return(str);
            }
        }
    }    
    /*
    public sealed class TfFrameHighlighter : MonoBehaviour, IDisplay, IRecyclable
    {
        [SerializeField] AxisFrameResource axisResource;
        [SerializeField] Tooltip tooltip;
        [SerializeField] FrameNode node;

        [CanBeNull] CancellationTokenSource tokenSource;

        Transform mTransform;
        [NotNull] Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        void Awake()
        {
            node = FrameNode.Instantiate("Frame Highlighter");
            node.Transform.SetParentLocal(Transform);

            axisResource = ResourcePool.RentDisplay<AxisFrameResource>(node.Transform);
            axisResource.ShadowsEnabled = false;
            axisResource.Emissive = 1;
            axisResource.OverrideMaterial(Resource.Materials.TransparentLitAlwaysVisible.Object);

            tooltip = ResourcePool.RentDisplay<Tooltip>(node.Transform);

            Layer = LayerType.IgnoreRaycast;
        }

        public void HighlightFrame([NotNull] TfFrame frame)
        {
            node.AttachTo(frame);
            axisResource.Tint = Color.white;
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground;
            tooltip.CaptionColor = Color.white;
            tooltip.Caption = "";

            float distanceToCam = Settings.MainCameraTransform.InverseTransformPoint(node.Transform.position).z;

            float size = 0.25f * Mathf.Abs(distanceToCam);
            float clampedSize = Mathf.Max(size, 2);

            float baseFrameSize = TfListener.Instance.FrameSize;
            float frameSize = baseFrameSize * clampedSize;
            float labelSize = baseFrameSize * Mathf.Max(size * 0.375f / 2, 0.15f);

            axisResource.AxisLength = frameSize;
            tooltip.Scale = labelSize;
            tooltip.Transform.localPosition = (1.2f * axisResource.AxisLength + 5 * labelSize) * Vector3.up;
            tooltip.PointToCamera();
            
            tokenSource = new CancellationTokenSource();
            Animator.Spawn(tokenSource.Token, 2.0f, UpdateFrame);
        }

        void UpdateFrame(float t)
        {
            if (t >= 1)
            {
                this.ReturnToPool();
                return;
            }

            float alpha = Mathf.Sqrt(1 - t);
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
                str.Append("<b>").Append(node.ParentId).Append("</b>\n");
                str.Append(px).Append(", ").Append(py).Append(", ").Append(pz);
                tooltip.SetCaption(str);
            }
            finally
            {
                BuilderPool.Return(str);
            }
        }

        public Bounds? Bounds => axisResource.Bounds;

        public int Layer
        {
            get => gameObject.layer;
            set
            {
                gameObject.layer = value;
                axisResource.Layer = value;
                tooltip.Layer = value;
            } 
        }

        public void Suspend()
        {
            node.Parent = null;
            node.Transform.SetParentLocal(Transform);
            
            tokenSource?.Cancel();
            tokenSource = null;            
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void SplitForRecycle()
        {
            axisResource.ShadowsEnabled = true;
            axisResource.Emissive = 0;
            axisResource.OverrideMaterial(null);
            axisResource.ReturnToPool();
            tooltip.ReturnToPool();
        }
    }
    */
}