using Iviz.App.ARDialogs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public class ClickedPoseHighlighter : DisplayWrapperResource
    {
        const float HighlightDuration = 1.0f;

        MeshMarkerResource flatSphere;
        Tooltip tooltip;

        float? highlightFrameStart;

        protected override IDisplay Display => flatSphere;

        void Awake()
        {
            Transform.parent = null;

            flatSphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, Transform);
            flatSphere.ShadowsEnabled = false;
            flatSphere.EmissiveColor = Color.white;
            //resource.OverrideMaterial(Resource.Materials.TransparentLitAlwaysVisible.Object);
            flatSphere.Layer = LayerType.IgnoreRaycast;

            tooltip = ResourcePool.RentDisplay<Tooltip>(Transform);
            tooltip.Layer = LayerType.IgnoreRaycast;
            tooltip.UseAnimation = false;
        }

        public void HighlightPose(in Pose absolutePose)
        {
            highlightFrameStart = Time.time;
            flatSphere.Tint = Color.white.WithAlpha(0.3f);
            tooltip.CaptionColor = Color.white;
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground;

            float distanceToCam = Settings.MainCameraTransform
                .InverseTransformDirection(absolutePose.position - Settings.MainCameraTransform.position).z;
            float size = 0.2f * distanceToCam;

            float baseFrameSize = TfListener.Instance.FrameSize;
            float frameSize = baseFrameSize * Mathf.Max(1, size);
            float labelSize = baseFrameSize * size * 0.375f / 2;

            //float frameSize = TfListener.Instance.FrameSize;
            flatSphere.Transform.localScale = new Vector3(frameSize, frameSize * 0.1f, frameSize);

            //float size = Mathf.Min(distanceToCam, MaxDistanceForScale) / MaxDistanceForScale * LabelScaleAtMaxDistance;
            tooltip.Scale = labelSize;

            Transform.SetPose(absolutePose);
            tooltip.Transform.localPosition = 2f * (frameSize * 0.3f + labelSize) * Vector3.up;
            tooltip.PointToCamera();

            var (pX, pY, pZ) = TfListener.RelativePositionToFixedFrame(Transform.position).Unity2RosVector3();
            string px = pX.ToString("#,0.0", UnityUtils.Culture);
            string py = pY.ToString("#,0.0", UnityUtils.Culture);
            string pz = pZ.ToString("#,0.0", UnityUtils.Culture);

            tooltip.Caption = $"{px}, {py}, {pz}";
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
                highlightFrameStart = null;
                this.ReturnToPool();
                return;
            }

            float alpha = Mathf.Sqrt(srcAlpha);
            tooltip.CaptionColor = Color.white.WithAlpha(alpha);
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground.WithAlpha(alpha);
            flatSphere.Tint = Color.white.WithAlpha(0.3f * alpha);
        }

        public override void Suspend()
        {
            base.Suspend();
            highlightFrameStart = null;
        }

        public override void SplitForRecycle()
        {
            flatSphere.ShadowsEnabled = true;
            flatSphere.EmissiveColor = Color.black;
            flatSphere.OverrideMaterial(null);
            flatSphere.ReturnToPool(Resource.Displays.Sphere);
            tooltip.ReturnToPool();
        }
    }
}