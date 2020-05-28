using UnityEngine;
using System.Collections.Generic;
using System;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.Displays
{
    public class MeshToMarkerHelper
    {
        readonly List<int> indices = new List<int>();
        readonly List<Vector3> vertices = new List<Vector3>();
        readonly HashSet<(int, int)> lines = new HashSet<(int, int)>();

        static readonly Color DefaultInteriorColor = new Color(1, 1, 1, 0.25f);
        static readonly Color DefaultLineColor = new Color(1, 1, 1, 1);

        public string Namespace { get; set; }

        public MeshToMarkerHelper(string Namespace = "mesh_marker")
        {
            this.Namespace = Namespace;
        }

        void AddLine(int a, int b)
        {
            if (a < b)
            {
                lines.Add((a, b));
            }
            else
            {
                lines.Add((b, a));
            }
        }

        uint seq = 0;
        public Marker[] MeshToMarker(Mesh mesh, Pose unityPose,
            Color? interiorColor = null,
            Color? lineColor = null,
            float lineWidth = 0.01f)
        {
            var topology = mesh.GetTopology(0);
            mesh.GetIndices(indices, 0);
            mesh.GetVertices(vertices);
            lines.Clear();

            Msgs.GeometryMsgs.Point[] trianglePoints = Array.Empty<Msgs.GeometryMsgs.Point>();

            if (topology == MeshTopology.Triangles)
            {
                trianglePoints = new Msgs.GeometryMsgs.Point[indices.Count];
                for (int i = 0; i < indices.Count; i += 3)
                {
                    int a = indices[i];
                    int b = indices[i + 1];
                    int c = indices[i + 2];
                    AddLine(a, b);
                    AddLine(b, c);
                    AddLine(c, a);

                    trianglePoints[i] = vertices[a].Unity2RosPoint();
                    trianglePoints[i + 1] = vertices[b].Unity2RosPoint();
                    trianglePoints[i + 2] = vertices[c].Unity2RosPoint();
                }
            }
            else if (topology == MeshTopology.Quads)
            {
                trianglePoints = new Msgs.GeometryMsgs.Point[indices.Count * 6 / 4];

                int k = 0;
                for (int i = 0; i < indices.Count; i += 4)
                {
                    int a = indices[i];
                    int b = indices[i + 1];
                    int c = indices[i + 2];
                    int d = indices[i + 3];
                    AddLine(a, b);
                    AddLine(b, c);
                    AddLine(c, d);
                    AddLine(d, a);

                    trianglePoints[k++] = vertices[a].Unity2RosPoint();
                    trianglePoints[k++] = vertices[b].Unity2RosPoint();
                    trianglePoints[k++] = vertices[c].Unity2RosPoint();

                    trianglePoints[k++] = vertices[a].Unity2RosPoint();
                    trianglePoints[k++] = vertices[c].Unity2RosPoint();
                    trianglePoints[k++] = vertices[d].Unity2RosPoint();
                }
            }

            Msgs.GeometryMsgs.Point[] linePoints = new Msgs.GeometryMsgs.Point[lines.Count * 2];

            int j = 0;
            foreach (var line in lines)
            {
                linePoints[j++] = vertices[line.Item1].Unity2RosPoint();
                linePoints[j++] = vertices[line.Item2].Unity2RosPoint();
            }

            Marker content = new Marker
            {
                Header = RosUtils.CreateHeader(seq++),
                Id = 0,
                Ns = Namespace,
                Type = Marker.TRIANGLE_LIST,
                Action = Marker.ADD,
                Pose = unityPose.Unity2RosPose(),
                Scale = new Msgs.GeometryMsgs.Vector3(1, 1, 1),
                Color = (interiorColor ?? DefaultInteriorColor).ToRos(),
                FrameLocked = true,
                Points = trianglePoints
            };

            Marker grid = new Marker
            {
                Header = RosUtils.CreateHeader(seq++),
                Id = 1,
                Ns = Namespace,
                Type = Marker.LINE_LIST,
                Action = Marker.ADD,
                Pose = unityPose.Unity2RosPose(),
                Scale = new Msgs.GeometryMsgs.Vector3(lineWidth, 1, 1),
                Color = (lineColor ?? DefaultLineColor).ToRos(),
                FrameLocked = true,
                Points = linePoints
            };

            return new Marker[] { content, grid };
        }

        public Marker CreateDeleteAll()
        {
            return new Marker
            {
                Header = RosUtils.CreateHeader(seq++),
                Action = Marker.DELETEALL
            };
        }

    }
}