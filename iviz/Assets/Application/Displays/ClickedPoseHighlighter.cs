using System.Threading;
using Iviz.App.ARDialogs;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public class ClickedPoseHighlighter : MonoBehaviour, IDisplay, IRecyclable
    {
        MeshMarkerResource flatSphere;
        Tooltip tooltip;
        [CanBeNull] CancellationTokenSource tokenSource;

        Transform mTransform;
        [NotNull] Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        void Awake()
        {
            Transform.parent = null;

            flatSphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, Transform);
            flatSphere.ShadowsEnabled = false;
            flatSphere.EmissiveColor = Color.white;

            tooltip = ResourcePool.RentDisplay<Tooltip>(Transform);
            
            Layer = LayerType.IgnoreRaycast;
        }

        public void HighlightPose(in Pose absolutePose)
        {
            //highlightFrameStart = Time.time;
            flatSphere.Color = Color.white.WithAlpha(0.3f);
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

            tokenSource = new CancellationTokenSource();
            Animator.Spawn(tokenSource.Token, 1.0f, UpdateFrame);
        }

        void UpdateFrame(float t)
        {
            if (t >= 1)
            {
                this.ReturnToPool();
                return;
            }

            float alpha = Mathf.Sqrt(1 - t);
            tooltip.CaptionColor = Color.white.WithAlpha(alpha);
            tooltip.BackgroundColor = Resource.Colors.HighlighterBackground.WithAlpha(alpha);
            flatSphere.Color = Color.white.WithAlpha(0.3f * alpha);
        }

        public void Suspend()
        {
            tokenSource?.Cancel();
            tokenSource = null;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public int Layer
        {
            get => gameObject.layer;
            set
            {
                gameObject.layer = value;
                flatSphere.Layer = value;
                tooltip.Layer = value;
            } 
        }
        
        public Bounds? Bounds => flatSphere.Bounds;
        
        public void SplitForRecycle()
        {
            flatSphere.ReturnToPool(Resource.Displays.Sphere);
            tooltip.ReturnToPool();
        }
    }
}