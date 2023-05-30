#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.MarkerDetection;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class ARMarkerManager
    {
        readonly Dictionary<(ARMarkerType, string), ARSeenMarker> seenMarkers = new();
        readonly Dictionary<(ARMarkerType, string), float> activeMarkerHighlighters = new();

        const float MaxDetectionDistanceInM = 1f;
        const float MaxSnapDistanceInM = 0.1f;

        public bool TryGetMarkerNearby(Vector3 position, out Pose pose)
        {
            if (seenMarkers.Count == 0)
            {
                pose = default;
                return false;
            }

            var (minDistanceSq, closestMarker) =
                seenMarkers.Values.Min(marker => ((marker.UnityPose.position - position).sqrMagnitude, marker));

            if (closestMarker.LastSeen < GameThread.GameTime - 3)
            {
                pose = default;
                return false;
            }

            if (minDistanceSq > MaxSnapDistanceInM * MaxSnapDistanceInM)
            {
                pose = default;
                return false;
            }

            var markerRotation = closestMarker.UnityPose.rotation;
            var dirX = (markerRotation * Vector3.right).WithY(0);

            var newRotation = dirX.sqrMagnitude.ApproximatelyZero()
                ? Quaternion.identity
                : Quaternion.LookRotation(dirX, Vector3.up);

            pose = closestMarker.UnityPose.WithRotation(newRotation);
            return true;
        }


        public void Process(XRMarker rosMarker)
        {
            var key = ((ARMarkerType)rosMarker.Type, rosMarker.Code);
            ProcessUnknownSize(rosMarker);

            var rosCameraPosition = rosMarker.CameraPose.Position;
            var rosMarkerPose = rosMarker.PoseRelativeToCamera;
            double distanceMarkerToCameraSq = (rosCameraPosition - rosMarkerPose.Position).SquaredNorm;
            if (distanceMarkerToCameraSq > MaxDetectionDistanceInM * MaxDetectionDistanceInM)
            {
                RosLogger.Debug($"{this}: Detected far marker at distance " +
                                $"{distanceMarkerToCameraSq.ToString(BuiltIns.Culture)}, " +
                                "discarding.");
                return;
            }

            if (seenMarkers.TryGetValue(key, out var knownMarker))
            {
                knownMarker.SizeInMm = (float)rosMarker.MarkerSizeInMm;
                knownMarker.UnityPose = ARController.GetAbsoluteMarkerPose(rosMarker);
                knownMarker.LastSeen = GameThread.GameTime;
                return;
            }

            var seenMarker = new ARSeenMarker
            {
                Type = (ARMarkerType)rosMarker.Type,
                Code = rosMarker.Code,
                SizeInMm = (float)rosMarker.MarkerSizeInMm,
                UnityPose = ARController.GetAbsoluteMarkerPose(rosMarker),
                LastSeen = GameThread.GameTime
            };

            seenMarkers[(seenMarker.Type, seenMarker.Code)] = seenMarker;
        }

        public void Highlight(XRMarker rosMarker)
        {
            var key = ((ARMarkerType)rosMarker.Type, rosMarker.Code);
            if (activeMarkerHighlighters.TryGetValue(key, out float existingExpirationTime)
                && GameThread.GameTime < existingExpirationTime)
            {
                return;
            }

            ARMarkerHighlighter.Highlight(rosMarker);

            const float markerLifetimeInSec = 5;
            float expirationTime = GameThread.GameTime + markerLifetimeInSec;
            activeMarkerHighlighters[key] = expirationTime;
        }

        void ProcessUnknownSize(XRMarker marker)
        {
            if (!TryDetectMarkerSize(marker))
            {
                return;
            }

            try
            {
                marker.PoseRelativeToCamera = SolvePnp(marker);
            }
            catch (CvMarkerException)
            {
                RosLogger.Info($"{this}: OpenCV error while processing image");
            }
        }


        static bool TryDetectMarkerSize(XRMarker marker)
        {
            const float distanceMultiplier = 0.15f;

            var relativeCameraPose = marker.CameraPose.FromCameraFrame().Ros2Unity();
            var (cameraPosition, cameraRotation) = TfModule.FixedFrameToAbsolute(relativeCameraPose);
            var intrinsic = new Intrinsic(marker.CameraIntrinsic);

            Span<Vector3> dirs = stackalloc Vector3[4];
            for (int i = 0; i < 4; i++)
            {
                var (cornerX, cornerY, _) = marker.Corners[i];
                var (dirX, dirY, dirZ) = intrinsic.Unproject(cornerX, cornerY);
                dirs[i] = new Vector3(dirX, -dirY, dirZ); // ros y-down to unity y-up
            }

            var centerDir = (dirs[0] + dirs[1] + dirs[2] + dirs[3]) / 4;
            var hitInfo = new ClickHitInfo(new Ray(cameraPosition, cameraRotation * centerDir.Normalized()));
            if (!hitInfo.TryGetARRaycastResults(out var arHits))
            {
                return false;
            }

            Span<Ray> rays = stackalloc[]
            {
                new Ray(cameraPosition, cameraRotation * dirs[0].Normalized()),
                new Ray(cameraPosition, cameraRotation * dirs[1].Normalized()),
                new Ray(cameraPosition, cameraRotation * dirs[2].Normalized()),
                new Ray(cameraPosition, cameraRotation * dirs[3].Normalized())
            };

            Span<Vector3> hitPoints = stackalloc Vector3[4];
            foreach (var (hitPosition, hitNormal) in arHits)
            {
                var planeAsRay = new Ray(hitPosition, hitNormal);

                UnityUtils.PlaneIntersection(planeAsRay, rays[0], out hitPoints[0], out _);
                UnityUtils.PlaneIntersection(planeAsRay, rays[1], out hitPoints[1], out _);
                UnityUtils.PlaneIntersection(planeAsRay, rays[2], out hitPoints[2], out _);
                UnityUtils.PlaneIntersection(planeAsRay, rays[3], out hitPoints[3], out _);

                float4 sizes;
                sizes.x = (hitPoints[1] - hitPoints[0]).Magnitude();
                sizes.y = (hitPoints[2] - hitPoints[1]).Magnitude();
                sizes.z = (hitPoints[3] - hitPoints[2]).Magnitude();
                sizes.w = (hitPoints[0] - hitPoints[3]).Magnitude();

                //float averageSize = (sizes.x + sizes.y + sizes.z + sizes.w) / 4;
                float averageSize = math.csum(sizes) / 4;
                float distanceThreshold = averageSize * distanceMultiplier;

                /*                    
                if (Mathf.Abs(sizes[0] - averageSize) > distanceThreshold ||
                    Mathf.Abs(sizes[1] - averageSize) > distanceThreshold ||
                    Mathf.Abs(sizes[2] - averageSize) > distanceThreshold ||
                    Mathf.Abs(sizes[3] - averageSize) > distanceThreshold)
                    */
                if (math.any(math.abs(sizes - averageSize) > distanceThreshold))
                {
                    continue;
                }

                marker.MarkerSizeInMm = averageSize * 1000;
                return true;
            }

            return false;
        }


        static Msgs.GeometryMsgs.Pose SolvePnp(XRMarker marker)
        {
            float sizeInMm = (float)marker.MarkerSizeInMm;
            float sizeInM = sizeInMm / 1000f;
            var intrinsic = new Intrinsic(marker.CameraIntrinsic);
            var corners = marker.Corners;
            ReadOnlySpan<UnityEngine.Vector2> imageCorners = stackalloc[]
            {
                ToVector2(corners[0]),
                ToVector2(corners[1]),
                ToVector2(corners[2]),
                ToVector2(corners[3])
            };

            return MarkerDetector.SolvePnp(imageCorners, intrinsic, sizeInM);

            static UnityEngine.Vector2 ToVector2(in Msgs.GeometryMsgs.Vector3 v) => new((float)v.X, (float)v.Y);
        }

        public override string ToString() => $"[{nameof(ARMarkerManager)}]";
    }
}