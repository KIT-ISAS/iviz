#nullable enable

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Iviz.Controllers.Markers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Tools;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed partial class MarkerObject
    {
        static void GenerateLog(StringBuilder description, MarkerObject mObj)
        {
            if (mObj.lastMessage is not { } msg)
            {
                return;
            }

            description.Append("<color=#800000ff>")
                .Append("<link=").Append(mObj.UniqueNodeName).Append(">")
                .Append("<b><u>").Append(mObj.id.Ns.Length != 0 ? mObj.id.Ns : "[]").Append("/").Append(mObj.id.Id)
                .Append("</u></b></link></color>")
                .AppendLine();

            description.Append("Type: <b>");
            description.Append(DescriptionFromType(msg));
            if (msg.Type() == MarkerType.MeshResource)
            {
                description.Append(": ").Append(msg.MeshResource);
            }

            description.Append("</b>").AppendLine();

            if (msg.Lifetime == default)
            {
                description.Append("Expiration: None").AppendLine();
            }
            else
            {
                string timeInSecs = msg.Lifetime.ToTimeSpan().TotalSeconds.ToString("N01");
                description.Append("Expiration: ").Append(timeInSecs).Append(" secs").AppendLine();
            }

            if (msg.Type is < 0 or > (int)MarkerType.TriangleList)
            {
                description.Append(ErrorStr).Append("Unknown marker type ").AppendLine();
                return;
            }

            if (mObj.resource == null)
            {
                if (msg.Type() != MarkerType.MeshResource)
                {
                    return;
                }

                if (mObj.runningTs is { IsCancellationRequested: false })
                {
                    description.Append("Mesh resource is loading...").AppendLine();
                    return;
                }

                description.Append(ErrorStr)
                    .Append("Failed to load mesh resource. Check out the Log messages from [Me].").AppendLine();
                return;
            }

            UpdateTransformLog();

            var markerType = msg.Type();
            switch (markerType)
            {
                case MarkerType.Arrow:
                    CreateArrowLog();
                    break;
                case MarkerType.Cube:
                case MarkerType.Sphere:
                case MarkerType.Cylinder:
                case MarkerType.MeshResource:
                    CreateMeshResourceLog();
                    break;
                case MarkerType.TextViewFacing:
                    CreateTextResourceLog();
                    break;
                case MarkerType.CubeList:
                case MarkerType.SphereList:
                    CreateMeshListLog();
                    break;
                case MarkerType.LineList:
                case MarkerType.LineStrip:
                    CreateLineLog();
                    break;
                case MarkerType.Points:
                    CreatePointsLog();
                    break;
                case MarkerType.TriangleList:
                    CreateTriangleListLog();
                    break;
            }

            void UpdateTransformLog()
            {
                if (string.IsNullOrWhiteSpace(msg.Header.FrameId))
                {
                    description.Append("Frame Locked to: <i>(none)</i>").AppendLine();
                }
                else
                {
                    description.Append("Frame Locked to: <i>").Append(msg.Header.FrameId).Append("</i>").AppendLine();
                }

                if (msg.Pose.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Pose contains NaN values").AppendLine();
                    return;
                }

                var newPose = msg.Pose.Ros2Unity();
                if (!newPose.IsUsable())
                {
                    description.Append(ErrorStr).Append(
                        $"Cannot use ({newPose.position.x}, {newPose.position.y}, {newPose.position.z}) as position. " +
                        "Values too large.");
                }
            }

            void CreateArrowLog()
            {
                switch (msg.Points.Length)
                {
                    case 0:
                        AppendColorLog(msg.Color);
                        AppendScaleLog(msg.Scale);

                        if (msg.Scale.MaxAbsCoeff().ApproximatelyZero())
                        {
                            description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                        }

                        if (msg.Scale.IsInvalid())
                        {
                            description.Append(ErrorStr).Append("Scale value invalid").AppendLine();
                        }

                        break;
                    case 2:
                        float sx = (float)msg.Scale.X;
                        AppendColorLog(msg.Color);
                        AppendScalarLog(msg.Scale.X);

                        if (sx.ApproximatelyZero())
                        {
                            description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                        }
                        else if (sx.IsInvalid())
                        {
                            description.Append(ErrorStr).Append("Scale value invalid").AppendLine();
                        }

                        if (msg.Points[0].IsInvalid() || msg.Points[1].IsInvalid())
                        {
                            description.Append(ErrorStr).Append("Start or end point is invalid").AppendLine();
                        }

                        break;
                    default:
                        description.Append(ErrorStr).Append("Point array must have a length of 0 or 2").AppendLine();
                        break;
                }
            }

            void CreateMeshResourceLog()
            {
                AppendColorLog(msg.Color);
                AppendScaleLog(msg.Scale);

                if (msg.Scale.MaxAbsCoeff().ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                }
                else if (msg.Scale.IsInvalid())
                {
                    description.Append(WarnStr).Append("Scale value has NaN").AppendLine();
                }
            }

            void CreateTextResourceLog()
            {
                description.Append("Text: ").Append(msg.Text.Length).Append(" chars").AppendLine();
                AppendColorLog(msg.Color);
                AppendScalarLog(msg.Scale.Z);

                if (msg.Scale.Z.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                }

                if (msg.Scale.Z.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Scale value of NaN").AppendLine();
                }
            }

            void CreateMeshListLog()
            {
                AppendScaleLog(msg.Scale);
                AppendColorLog(msg.Color);

                if (msg.Points.Length == 0)
                {
                    description.Append("Elements: Empty").AppendLine();
                }
                else
                {
                    description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
                }

                if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
                {
                    description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                        .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                    return;
                }

                if (msg.Points.Length > NativeList.MaxElements)
                {
                    description.Append(ErrorStr).Append("Number of points exceeds maximum of ")
                        .Append(NativeList.MaxElements);
                }

                if (msg.Color.A.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Color field has alpha 0").AppendLine();
                    return;
                }

                if (msg.Color.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Color field is invalid").AppendLine();
                }
            }

            void CreateLineLog()
            {
                float elementScale = Mathf.Abs((float)msg.Scale.X);

                AppendColorLog(msg.Color);
                AppendScalarLog(elementScale);

                if (msg.Points.Length == 0)
                {
                    description.Append("Elements: Empty").AppendLine();
                }
                else
                {
                    description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
                }

                if (elementScale.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Scale value invalid").AppendLine();
                    return;
                }

                if (elementScale.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                    return;
                }

                if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
                {
                    description.Append(ErrorStr)
                        .Append("Color array length ").Append(msg.Colors.Length)
                        .Append(" does not match point array length ").Append(msg.Points.Length)
                        .AppendLine();
                    return;
                }

                if (msg.Points.Length > NativeList.MaxElements)
                {
                    description.Append(ErrorStr).Append("Number of points exceeds maximum of ")
                        .Append(NativeList.MaxElements);
                }

                if (msg.Color.A.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Color field has alpha 0").AppendLine();
                }

                if (msg.Color.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Color field is invalid").AppendLine();
                }
            }

            void CreatePointsLog()
            {
                AppendColorLog(msg.Color);
                AppendScalarLog(msg.Scale.X);

                if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
                {
                    description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                        .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                    return;
                }

                if (msg.Color.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Color field is invalid").AppendLine();
                    return;
                }

                if (msg.Color.A.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Color field has alpha 0").AppendLine();
                    return;
                }

                if (msg.Points.Length == 0)
                {
                    description.Append("Elements: Empty").AppendLine();
                }
                else
                {
                    description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
                }

                if (msg.Points.Length > NativeList.MaxElements)
                {
                    description.Append(ErrorStr).Append("Number of points exceeds maximum of ")
                        .Append(NativeList.MaxElements);
                }
            }

            void CreateTriangleListLog()
            {
                AppendScaleLog(msg.Scale);
                AppendColorLog(msg.Color);

                if (msg.Points.Length == 0)
                {
                    description.Append("Elements: Empty").AppendLine();
                }
                else
                {
                    description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
                }

                if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
                {
                    description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                        .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                    return;
                }

                if (msg.Points.Length > NativeList.MaxElements)
                {
                    description.Append(ErrorStr).Append("Number of points exceeds maximum of ")
                        .Append(NativeList.MaxElements);
                }

                if (msg.Color.A.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Color has alpha 0. Marker will not be visible").AppendLine();
                    return;
                }

                if (msg.Color.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Color has NaN. Marker will not be visible").AppendLine();
                    return;
                }

                if (msg.Points.Length % 3 != 0)
                {
                    description.Append(ErrorStr).Append("Point array length ").Append(msg.Colors.Length)
                        .Append(" needs to be a multiple of 3").AppendLine();
                }

                if (mObj.triangleMeshFailedIndex is { } failedIndex)
                {
                    description.Append(ErrorStr).Append("Index ").Append(failedIndex).Append(" has invalid values!")
                        .AppendLine();
                }
            }

            void AppendScaleLog(in Msgs.GeometryMsgs.Vector3 c)
            {
                description.Append("Scale: [")
                    .Append(c.X.ToString(FloatFormat)).Append(" | ")
                    .Append(c.Y.ToString(FloatFormat)).Append(" | ")
                    .Append(c.Z.ToString(FloatFormat)).Append("]").AppendLine();
            }

            void AppendColorLog(in ColorRGBA c)
            {
                description.Append("Color: ");

                string alpha = c.A.ToString(FloatFormat);

                switch (c.R, c.G, c.B)
                {
                    case (1, 1, 1):
                        description.Append("White | ").Append(alpha);
                        break;
                    case (0, 0, 0):
                        description.Append("Black | ").Append(alpha);
                        break;
                    case (1, 0, 0):
                        description.Append("Red | ").Append(alpha);
                        break;
                    case (0, 1, 0):
                        description.Append("Green | ").Append(alpha);
                        break;
                    case (0, 0, 1):
                        description.Append("Blue | ").Append(alpha);
                        break;
                    case (0.5f, 0.5f, 0.5f):
                        description.Append("Grey | ").Append(alpha);
                        break;
                    default:
                        description
                            .Append(c.R.ToString(FloatFormat)).Append(" | ")
                            .Append(c.G.ToString(FloatFormat)).Append(" | ")
                            .Append(c.B.ToString(FloatFormat)).Append(" | ")
                            .Append(alpha);
                        break;
                }

                description.AppendLine();
            }

            void AppendScalarLog(double c)
            {
                description.Append("Scale: ").Append(c.ToString(FloatFormat)).AppendLine();
            }
        }

        static uint CalculateMarkerHash(Marker msg)
        {
            uint hash = HashCalculator.Compute(msg.Type);
            hash = HashCalculator.Compute(msg.Color, hash);
            hash = HashCalculator.Compute(msg.Points, hash);
            hash = HashCalculator.Compute(msg.Colors, hash);
            return hash;
        }

        static string DescriptionFromType(Marker msg)
        {
            return msg.Type() switch
            {
                MarkerType.Arrow => "Arrow",
                MarkerType.Cylinder => "Cylinder",
                MarkerType.Cube => "Cube",
                MarkerType.Sphere => "Sphere",
                MarkerType.TextViewFacing => "Text_View_Facing",
                MarkerType.LineStrip => "LineStrip",
                MarkerType.LineList => "LineList",
                MarkerType.MeshResource => "MeshResource",
                MarkerType.CubeList => "CubeList",
                MarkerType.SphereList => "SphereList",
                MarkerType.Points => "Points",
                MarkerType.TriangleList => "TriangleList",
                MarkerType.Invalid => "Invalid",
                _ => $"Unknown ({msg.Type.ToString()})"
            };
        }

        static int? CopyPoints(Point[] srcPoints, ColorRGBA[] srcColors, MeshTrianglesDisplay mesh)
        {
            int pointsLength = srcPoints.Length;
            using var points = new Rent<Vector3>(pointsLength);
            var dstPoints = points.Array;

            int invalidIndex = -1;
            ref Point srcPtr = ref srcPoints[0];
            ref Vector3 dstPtr = ref dstPoints[0];

            for (int i = 0; i < pointsLength; i++)
            {
                ref Point srcPtrI = ref Unsafe.Add(ref srcPtr, i);
                ref Vector3 dstPtrI = ref Unsafe.Add(ref dstPtr, i);

                srcPtrI.Ros2Unity(out dstPtrI);
                if (dstPtrI.IsInvalid()) // unlikely but needed!
                {
                    invalidIndex = i;
                }
            }

            if (invalidIndex != -1)
            {
                mesh.Clear();
                return invalidIndex;
            }

            var colors = MemoryMarshal.Cast<ColorRGBA, Color>(srcColors);
            mesh.Set(points, colors);
            return null;
        }
    }
    
    internal static class MarkerUtils
    {
        public static MarkerType Type(this Marker marker) => (MarkerType)marker.Type;
    }
}