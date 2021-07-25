using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Msgs;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Resource that constructs a mesh from vertices, triangles, normals, etc.
    /// </summary>
    public sealed class MeshTrianglesResource : MeshMarkerResource
    {
        const int MaxVerticesShort = 65000;

        public bool FlipWinding { get; set; }

        [CanBeNull] Mesh mesh;
        Bounds localBounds;

        public Bounds LocalBounds
        {
            get => localBounds;
            private set
            {
                localBounds = value;
                if (!HasBoxCollider)
                {
                    return;
                }

                BoxCollider.center = localBounds.center;
                BoxCollider.size = localBounds.size;
            }
        }

        void OnDestroy()
        {
            if (mesh != null)
            {
                Destroy(mesh);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Color = Color;
            LocalBounds = LocalBounds;
        }

        [NotNull]
        Mesh EnsureOwnMesh(int numPointsNeeded)
        {
            var indexFormat = numPointsNeeded >= MaxVerticesShort
                ? UnityEngine.Rendering.IndexFormat.UInt32
                : UnityEngine.Rendering.IndexFormat.UInt16;

            if (mesh != null)
            {
                if (mesh.indexFormat != indexFormat)
                {
                    mesh.indexFormat = indexFormat;
                }

                return mesh;
            }

            Mesh tmpMesh = new Mesh
            {
                indexFormat = indexFormat,
                name = "MeshTrianglesResource Mesh"
            };


            mesh = tmpMesh;
            GetComponent<MeshFilter>().sharedMesh = tmpMesh;
            return tmpMesh;
        }

        public void Clear()
        {
            EnsureOwnMesh(0).Clear();
        }

        public void Set(in Rent<Vector3> points, in Rent<Color> colors = default)
        {
            if (points.Length % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Length}", nameof(points));
            }

            if (colors.Length != 0 && colors.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            Mesh ownMesh = EnsureOwnMesh(points.Length);

            ownMesh.Clear();
            ownMesh.SetVertices(points);
            if (colors.Length != 0)
            {
                ownMesh.SetColors(colors);
            }

            using (var triangles = new Rent<int>(points.Length))
            {
                if (FlipWinding)
                {
                    for (int i = 0; i < triangles.Length; i += 3)
                    {
                        triangles.Array[i] = i;
                        triangles.Array[i + 1] = i + 2;
                        triangles.Array[i + 2] = i + 1;
                    }
                }
                else
                {
                    for (int i = 0; i < triangles.Length; i++)
                    {
                        triangles.Array[i] = i;
                    }
                }

                ownMesh.SetTriangles(triangles);
            }

            ownMesh.RecalculateNormals();

            LocalBounds = ownMesh.bounds;
        }

        public void Set(
            in Rent<Vector3> points,
            in Rent<Vector3> normals,
            in Rent<Vector4> tangents,
            in Rent<Vector3> diffuseTexCoords,
            in Rent<Vector3> bumpTexCoords,
            in Rent<int> triangles,
            in Rent<Color32> colors = default)
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

            Mesh ownMesh = EnsureOwnMesh(points.Length);

            ownMesh.Clear();
            ownMesh.SetVertices(points);
            if (normals.Length != 0)
            {
                ownMesh.SetNormals(normals);
            }

            if (diffuseTexCoords.Length != 0)
            {
                ownMesh.SetUVs(0, diffuseTexCoords);
            }

            if (bumpTexCoords.Length != 0)
            {
                ownMesh.SetUVs(1, bumpTexCoords);
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
            
            LocalBounds = ownMesh.bounds;
        }

        public override void Suspend()
        {
            base.Suspend();
            if (mesh != null)
            {
                mesh.Clear();
            }
        }
    }
}