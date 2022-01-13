#nullable enable

using System;
using System.Linq;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizMsgs;
using Iviz.Tools;
using TMPro;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class ARMarkerHighlighter : MeshMarkerHolderResource
    {
        static readonly Quaternion TableRosToUnity =
            Quaternion.AngleAxis(-90, Vector3.up) * Quaternion.AngleAxis(90, Vector3.right);

        static readonly Color BaseColor = new(0.78f, 0.98f, 1);

        [SerializeField] Transform? topLeft;
        [SerializeField] Transform? topRight;
        [SerializeField] Transform? bottomRight;
        [SerializeField] Transform? bottomLeft;
        [SerializeField] TextMeshPro? text;

        Transform TopLeft => topLeft.AssertNotNull(nameof(topLeft));
        Transform TopRight => topRight.AssertNotNull(nameof(topRight));
        Transform BottomLeft => bottomLeft.AssertNotNull(nameof(bottomLeft));
        Transform BottomRight => bottomRight.AssertNotNull(nameof(bottomRight));
        TextMeshPro Text => text.AssertNotNull(nameof(text));

        public override void Suspend()
        {
            base.Suspend();
            var scale = new Vector3(2, 2, 1);
            TopLeft.localScale = scale;
            TopRight.localScale = scale;
            BottomRight.localScale = scale;
            BottomLeft.localScale = scale;
            Text.transform.localScale = 0.05f * Vector3.one;

            TopLeft.localPosition = new Vector3(-0.5f, 0.5f, 0);
            TopRight.localPosition = new Vector3(0.5f, 0.5f, 0);
            BottomRight.localPosition = new Vector3(0.5f, -0.5f, 0);
            BottomLeft.localPosition = new Vector3(-0.5f, -0.5f, 0);
        }

        public static void Highlight(ARMarker marker, float highlightTimeInSec)
        {
            if (marker.MarkerSizeInMm != 0)
            {
                var pose = ARController.GetAbsoluteMarkerPose(marker);
                Highlight(pose, (float) marker.MarkerSizeInMm, marker.Code, highlightTimeInSec);
            }
            else
            {
                Highlight(marker.Corners, marker.Code, marker.CameraIntrinsic, highlightTimeInSec);
            }
            
        }

        static void Highlight(Msgs.GeometryMsgs.Vector3[] cornersLocal, string code, double[] intrinsicArray,
            float highlightTimeInSec)
        {
            const float cameraZ = 0.05f;
            const float cornerScale = 0.005f;
            const float highlightScale = cornerScale * 1.05f;

            if (cornersLocal.Length == 0)
            {
                throw new ArgumentException("Cannot highlight marker with no corners.");
            }

            var intrinsic = new Intrinsic(intrinsicArray);
            var cornersWorld = cornersLocal
                .Select(corner => intrinsic.Unproject(corner.X, corner.Y) * cameraZ)
                .ToArray();

            float minX = cornersWorld.Min(corner => corner.X);
            float maxX = cornersWorld.Max(corner => corner.X);
            float minY = cornersWorld.Min(corner => -corner.Y);
            float maxY = cornersWorld.Max(corner => -corner.Y);

            var center = new Vector3((maxX + minX) / 2, (maxY + minY) / 2, cameraZ);
            float sizeX = maxX - center.x;
            float sizeY = maxY - center.y;
            float scaleX = sizeX / cornerScale;
            float scaleY = sizeY / cornerScale;

            var highlighter = ResourcePool.RentDisplay<ARMarkerHighlighter>();

            var mTransform = highlighter.transform;
            mTransform.parent = Settings.ARCamera != null ? Settings.ARCamera.transform : null;
            mTransform.localRotation = Quaternion.identity;
            mTransform.localPosition = center;
            mTransform.localScale = highlightScale * Vector3.one;

            highlighter.TopLeft.localPosition = new Vector3(-scaleX, scaleY, 0);
            highlighter.TopRight.localPosition = new Vector3(scaleX, scaleY, 0);
            highlighter.BottomRight.localPosition = new Vector3(scaleX, -scaleY, 0);
            highlighter.BottomLeft.localPosition = new Vector3(-scaleX, -scaleY, 0);

            highlighter.Text.transform.localPosition = new Vector3(0, scaleY, 0);
            highlighter.Text.text = code;

            highlighter.EmissiveColor = (BaseColor * 0.5f).WithAlpha(1);

            void Update(float t)
            {
                highlighter.Color = BaseColor.WithAlpha(Mathf.Sqrt(1 - t));
            }

            void Dispose()
            {
                highlighter.ReturnToPool();
            }

            FAnimator.Spawn(default, highlightTimeInSec, Update, Dispose);
        }

        static void Highlight(in Pose pose, float markerSizeInMm, string code, float highlightTimeInSec)
        {
            var highlighter = ResourcePool.RentDisplay<ARMarkerHighlighter>();
            float markerSizeInM = markerSizeInMm * 0.001f;
            var (position, rotation) = TfListener.RelativeToFixedFrame(pose);

            var mTransform = highlighter.transform;
            mTransform.parent = TfListener.FixedFrame.Transform;
            mTransform.localRotation = rotation * TableRosToUnity;
            mTransform.localPosition = position + rotation * (Vector3.up * 0.01f);
            mTransform.localScale = markerSizeInM * Vector3.one;

            var scale = new Vector3(2, 2, 1) * 0.25f;
            highlighter.TopLeft.localScale = scale;
            highlighter.TopRight.localScale = scale;
            highlighter.BottomRight.localScale = scale;
            highlighter.BottomLeft.localScale = scale;
            highlighter.Text.transform.localScale = 0.025f * Vector3.one;

            highlighter.Text.text = code;
            highlighter.EmissiveColor = Color.blue;
            highlighter.Color = BaseColor;
            
            void Update(float t)
            {
                var color = BaseColor.WithAlpha(Mathf.Sqrt(1 - t));
                highlighter.Color = color;
                highlighter.Text.color = color;
            }

            void Dispose()
            {
                highlighter.ReturnToPool();
            }

            FAnimator.Spawn(default, highlightTimeInSec, Update, Dispose);
        }
    }
}