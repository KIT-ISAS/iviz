using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public class PoseHighlighter : DisplayWrapperResource
    {
        const float HighlightDuration = 1.0f;

        MeshMarkerResource resource;
        TextMarkerResource label;

        float? highlightFrameStart;

        protected override IDisplay Display => resource;

        void Awake()
        {
            Transform.parent = null;

            resource = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, Transform);
            resource.CastsShadows = false;
            resource.EmissiveColor = Color.white;
            //resource.OverrideMaterial(Resource.Materials.TransparentLitAlwaysVisible.Object);
            resource.Layer = LayerType.IgnoreRaycast;

            label = ResourcePool.RentDisplay<TextMarkerResource>(Transform);
            label.AlwaysVisible = true;
            label.Layer = LayerType.IgnoreRaycast;
        }


        public void HighlightPose(in Pose absolutePose)
        {
            highlightFrameStart = Time.time;
            resource.Tint = Color.white.WithAlpha(0.3f);
            label.Color = Color.white;

            float distanceToCam = Settings.MainCameraTransform
                .InverseTransformDirection(absolutePose.position - Settings.MainCameraTransform.position).z;
            float size = 0.2f * distanceToCam;

            float baseFrameSize = TfListener.Instance.FrameSize;
            float frameSize = baseFrameSize * Mathf.Max(1, size);
            float labelSize = baseFrameSize * size;

            //float frameSize = TfListener.Instance.FrameSize;
            resource.Transform.localScale = new Vector3(frameSize, frameSize * 0.1f, frameSize);

            //float size = Mathf.Min(distanceToCam, MaxDistanceForScale) / MaxDistanceForScale * LabelScaleAtMaxDistance;
            label.ElementSize = labelSize;

            Transform.SetPose(absolutePose);
            label.BillboardOffset = (frameSize * 0.3f + labelSize) * Vector3.up;

            var (pX, pY, pZ) = TfListener.RelativePositionToFixedFrame(Transform.position).Unity2RosVector3();
            string px = pX.ToString("#,0.0", UnityUtils.Culture);
            string py = pY.ToString("#,0.0", UnityUtils.Culture);
            string pz = pZ.ToString("#,0.0", UnityUtils.Culture);

            label.Text = $"{px}, {py}, {pz}";
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
                highlightFrameStart = null;
                this.ReturnToPool();
                return;
            }

            alpha = Mathf.Sqrt(alpha);
            label.Color = Color.white.WithAlpha(alpha);
            resource.Tint = Color.white.WithAlpha(0.3f * alpha);
        }

        public override void Suspend()
        {
            base.Suspend();
            highlightFrameStart = null;
        }

        public override void SplitForRecycle()
        {
            resource.CastsShadows = true;
            resource.EmissiveColor = Color.black;
            resource.OverrideMaterial(null);
            resource.ReturnToPool(Resource.Displays.Sphere);
            label.ReturnToPool();
        }
    }
}