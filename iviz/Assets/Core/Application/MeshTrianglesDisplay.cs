#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    /// <summary>
    /// Resource that constructs a mesh from vertices, triangles, normals, etc.
    /// </summary>
    public sealed class MeshTrianglesDisplay : MeshMarkerDisplay, ISupportsDynamicBounds
    {
        Mesh? mesh;

        public bool FlipWinding { get; set; }
        public override Bounds? Bounds => mesh != null && mesh.vertexCount != 0 ? Collider.GetLocalBounds() : null;
        public event Action? BoundsChanged;

        public string MeshName
        {
            set
            {
                if (mesh != null)
                {
                    mesh.name = value;
                }
            }
        }

        Mesh EnsureOwnMesh(int numPointsNeeded)
        {
            var indexFormat = numPointsNeeded <= UnityUtils.MeshUInt16Threshold
                ? IndexFormat.UInt16
                : IndexFormat.UInt32;

            if (mesh != null)
            {
                if (mesh.indexFormat != indexFormat)
                {
                    mesh.indexFormat = indexFormat;
                }

                return mesh;
            }

            var newMesh = new Mesh
            {
                indexFormat = indexFormat,
                name = "MeshTrianglesResource Mesh"
            };


            mesh = newMesh;
            gameObject.AssertHasComponent<MeshFilter>(nameof(gameObject)).sharedMesh = newMesh;
            return newMesh;
        }

        public void Clear()
        {
            EnsureOwnMesh(0).Clear();
        }

        public void Set(ReadOnlySpan<Vector3> points, ReadOnlySpan<Color> colors = default)
        {
            if (points.Length % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Length}", nameof(points));
            }

            if (colors.Length != 0 && colors.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            var ownMesh = EnsureOwnMesh(points.Length);

            ownMesh.Clear();
            ownMesh.SetVertices(points);
            if (colors.Length != 0)
            {
                ownMesh.SetColors(colors);
            }

            using (var triangles = new Rent<int>(points.Length))
            {
                var tArray = triangles.AsSpan();
                if (FlipWinding)
                {
                    for (int i = 0; i < triangles.Length; i += 3)
                    {
                        tArray[i] = i;
                        tArray[i + 1] = i + 2;
                        tArray[i + 2] = i + 1;
                    }
                }
                else
                {
                    for (int i = 0; i < triangles.Length; i++)
                    {
                        tArray[i] = i;
                    }
                }

                ownMesh.SetTriangles(triangles);
            }

            ownMesh.RecalculateNormals();
            Collider.SetLocalBounds(ownMesh.bounds);
            BoundsChanged?.Invoke();
        }

        public void Set(
            ReadOnlySpan<Vector3> points,
            ReadOnlySpan<Vector3> normals,
            ReadOnlySpan<Vector4> tangents,
            ReadOnlySpan<Vector3> diffuseTexCoords,
            ReadOnlySpan<Vector3> bumpTexCoords,
            ReadOnlySpan<int> triangles,
            ReadOnlySpan<Color32> colors = default)
        {
            if (triangles.Length % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Length}", nameof(points));
            }

            if (normals.Length != 0 && normals.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent normals size!", nameof(normals));
            }

            if (colors.Length != 0 && colors.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            var ownMesh = EnsureOwnMesh(points.Length);

            ownMesh.Clear();
            ownMesh.SetVertices(points);
            if (normals.Length != 0)
            {
                ownMesh.SetNormals(normals);
            }

            if (diffuseTexCoords.Length != 0)
            {
                ownMesh.SetUVs(diffuseTexCoords);
            }

            if (bumpTexCoords.Length != 0)
            {
                ownMesh.SetUVs(bumpTexCoords, 1);
            }

            if (colors.Length != 0)
            {
                ownMesh.SetColors(colors);
            }

            ownMesh.SetTriangles(triangles);

            if (normals.Length == 0)
            {
                ownMesh.RecalculateNormals();
            }

            if (tangents.Length != 0)
            {
                ownMesh.SetTangents(tangents);
            }
            else
            {
                ownMesh.RecalculateTangents();
            }

            Collider.SetLocalBounds(ownMesh.bounds);
            BoundsChanged?.Invoke();
        }

        public override void Suspend()
        {
            base.Suspend();
            BoundsChanged = null;
            if (mesh != null)
            {
                mesh.Clear();
            }
        }

        void OnDestroy()
        {
            if (mesh != null)
            {
                Destroy(mesh);
            }
        }
    }
}