#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Common;
using Iviz.Core;
using Iviz.Tools;
using Unity.Mathematics;
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
        bool flipWinding;

        public bool FlipWinding
        {
            get => flipWinding;
            set
            {
                if (flipWinding == value)
                {
                    return;
                }

                flipWinding = value;
                if (mesh == null || mesh.GetIndexCount(0) == 0)
                {
                    return;
                }

                FlipMeshWinding(mesh);
            }
        }

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

        Mesh EnsureOwnMesh()
        {
            if (mesh != null)
            {
                return mesh;
            }

            var newMesh = new Mesh
            {
                name = nameof(MeshTrianglesDisplay) + " Mesh"
            };


            mesh = newMesh;
            gameObject.AssertHasComponent<MeshFilter>(nameof(gameObject)).sharedMesh = newMesh;
            return newMesh;
        }

        static void FlipMeshWinding(Mesh mesh)
        {
            var meshTopology = mesh.GetTopology(0);
            if (meshTopology is not (MeshTopology.Triangles or MeshTopology.Quads))
            {
                return;
            }
            
            int[] array = mesh.GetIndices(0); // allocates! use sparingly
            switch (meshTopology)
            {
                case MeshTopology.Triangles:
                    var array3 = MemoryMarshal.Cast<int, uint3>(array);
                    int length3 = array3.Length;
                    for (int i = 0; i < length3; i++)
                    {
                        ref uint3 triangle = ref array3[i];
                        (triangle.y, triangle.z) = (triangle.z, triangle.y);
                    }

                    break;
                case MeshTopology.Quads:
                    var array4 = MemoryMarshal.Cast<int, uint4>(array);
                    int length4 = array4.Length;
                    for (int i = 0; i < length4; i++)
                    {
                        ref uint4 quad = ref array4[i];
                        (quad.y, quad.w) = (quad.w, quad.y);
                    }

                    break;
            }

            mesh.SetIndices(array, meshTopology, 0);
        }

        public void Clear()
        {
            EnsureOwnMesh().Clear();
        }

        public void Set(Rent<Vector3> points, ReadOnlySpan<Color> colors = default)
        {
            int pointsLength = points.Length;
            if (pointsLength % 3 != 0)
            {
                ThrowHelper.ThrowArgument($"Invalid point size {pointsLength.ToString()}", nameof(points));
            }

            if (colors.Length != 0 && colors.Length != pointsLength)
            {
                ThrowHelper.ThrowArgument("Inconsistent color size!", nameof(colors));
            }

            var ownMesh = EnsureOwnMesh();

            ownMesh.Clear();

            if (pointsLength == 0)
            {
                Collider.SetLocalBounds(ownMesh.bounds);
                BoundsChanged?.Invoke();
                return;
            }

            ownMesh.SetVertices(points);
            if (colors.Length != 0)
            {
                ownMesh.SetColors(colors);
            }

            using (var indices = new Rent<int>(pointsLength))
            {
                if (FlipWinding)
                {
                    IndicesUtils.FillIndicesFlipped(indices);
                }
                else
                {
                    IndicesUtils.FillIndices(indices);
                }

                ownMesh.SetTriangles(indices);
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
                ThrowHelper.ThrowArgument($"Invalid triangle list {points.Length.ToString()}", nameof(points));
            }

            if (normals.Length != 0 && normals.Length != points.Length)
            {
                ThrowHelper.ThrowArgument("Inconsistent normals size!", nameof(normals));
            }

            if (colors.Length != 0 && colors.Length != points.Length)
            {
                ThrowHelper.ThrowArgument("Inconsistent color size!", nameof(colors));
            }

            var ownMesh = EnsureOwnMesh();

            ownMesh.Clear();

            if (points.Length == 0 || triangles.Length == 0)
            {
                Collider.SetLocalBounds(ownMesh.bounds);
                BoundsChanged?.Invoke();
                return;
            }

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