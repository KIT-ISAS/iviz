using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Color32 = UnityEngine.Color32;
using Object = UnityEngine.Object;

namespace Iviz.Displays
{
    public static class SceneModel
    {
        [NotNull]
        public static async Task<AggregatedMeshMarkerResource> CreateAsync([NotNull] string uriString,
            [NotNull] Model msg, [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            if (msg is null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            token.ThrowIfCancellationRequested();
            GameObject root = new GameObject($"Root:{uriString} [{msg.OrientationHint}]");

            try
            {
                return await CreateImpl(uriString, msg, provider, token, root);
            }
            catch (Exception)
            {
                if (root != null)
                {
                    Object.Destroy(root);
                }

                throw;
            }
        }

        [ItemNotNull]
        static async Task<AggregatedMeshMarkerResource> CreateImpl(string uriString, [NotNull] Model msg,
            IExternalServiceProvider provider, CancellationToken token, [NotNull] GameObject root)
        {
            switch (msg.OrientationHint.ToUpperInvariant())
            {
                case "Z_UP":
                    root.transform.localRotation = Quaternion.Euler(0, -90, 0);
                    break;
                default:
                    root.transform.localRotation = Quaternion.Euler(90, 0, 90);
                    break;
            }

            AggregatedMeshMarkerResource amm = root.AddComponent<AggregatedMeshMarkerResource>();
            List<MeshTrianglesResource> children = new List<MeshTrianglesResource>();
            List<MeshTrianglesResource> templateMeshes = new List<MeshTrianglesResource>();

            foreach (var mesh in msg.Meshes)
            {
                token.ThrowIfCancellationRequested();
                GameObject obj = new GameObject();
                obj.AddComponent<MeshRenderer>();
                obj.AddComponent<MeshFilter>();
                obj.AddComponent<BoxCollider>();
                obj.transform.SetParent(root.transform, false);

                MeshTrianglesResource r = obj.AddComponent<MeshTrianglesResource>();

                r.Name = mesh.Name;

                Vector3[] vertices = new Vector3[mesh.Vertices.Length];
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i] = Assimp2Unity(mesh.Vertices[i]);
                }

                Vector3[] normals = new Vector3[mesh.Normals.Length];
                MemCopy(mesh.Normals, normals, normals.Length * 3 * sizeof(float));

                Color32[] colors = new Color32[mesh.Colors.Length];
                MemCopy(mesh.Colors, colors, colors.Length * sizeof(int));

                Vector2[] texCoords = new Vector2[mesh.TexCoords.Length];
                MemCopy(mesh.TexCoords, texCoords, texCoords.Length * 2 * sizeof(float));

                int[] triangles = new int[mesh.Faces.Length * 3];
                MemCopy(mesh.Faces, triangles, triangles.Length * sizeof(int));

                var material = msg.Materials[mesh.MaterialIndex];
                r.Color = new Color32(material.Diffuse.R, material.Diffuse.G, material.Diffuse.B,
                    material.Diffuse.A);
                r.EmissiveColor = new Color32(material.Emissive.R, material.Emissive.G, material.Emissive.B,
                    material.Emissive.A);

                if (material.DiffuseTexture.Path.Length != 0)
                {
                    Uri uri = new Uri(uriString);
                    string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
                    string directoryName = Path.GetDirectoryName(uriPath);
                    if (!string.IsNullOrEmpty(directoryName) && Path.DirectorySeparatorChar == '\\')
                    {
                        directoryName = directoryName.Replace('\\', '/'); // windows!
                    }

                    string texturePath = $"{directoryName}/{material.DiffuseTexture.Path}";
                    string textureUri = $"{uri.Scheme}://{uri.Host}{texturePath}";
                    
                    var textureInfo = await Resource.GetTextureResourceAsync(textureUri, provider, token);
                    if (textureInfo != null)
                    {
                        r.Texture = textureInfo.Object;
                    }
                    else
                    {
                        Debug.Log($"SceneModel: Failed to retrieve texture {textureUri}");
                    }
                }

                r.Set(vertices, normals, texCoords, triangles, colors);
                r.Mesh.name = mesh.Name;

                children.Add(r);
                templateMeshes.Add(r);
            }

            List<GameObject> nodes = new List<GameObject>();
            bool[] used = new bool[templateMeshes.Count];

            foreach (var node in msg.Nodes)
            {
                token.ThrowIfCancellationRequested();
                GameObject nodeObject = new GameObject($"Node:{node.Name}");
                nodes.Add(nodeObject);

                nodeObject.transform.SetParent(
                    node.Parent == -1 ? root.transform : nodes[node.Parent].transform,
                    false);

                Matrix4x4 m = new Matrix4x4();
                for (int i = 0; i < 16; i++)
                {
                    m[i] = node.Transform.M[i];
                }

                nodeObject.transform.localRotation = m.rotation;
                nodeObject.transform.localPosition = m.GetColumn(3);
                nodeObject.transform.localScale = m.lossyScale;

                foreach (int meshId in node.Meshes)
                {
                    if (!used[meshId])
                    {
                        templateMeshes[meshId].transform.SetParent(nodeObject.transform, false);
                        used[meshId] = true;
                    }
                    else
                    {
                        GameObject newMesh = Object.Instantiate(
                            templateMeshes[meshId].gameObject,
                            nodeObject.transform,
                            false);
                        children.Add(newMesh.GetComponent<MeshTrianglesResource>());
                    }
                }
            }

            amm.Children = children;

            BoxCollider ammCollider = root.AddComponent<BoxCollider>();

            Bounds? ammBounds =
                UnityUtils.CombineBounds(amm.Children.Select(resource =>
                    TransformBoundsUntil(resource.LocalBounds, resource.transform, root.transform)));
            if (ammBounds != null)
            {
                ammCollider.center = ammBounds.Value.center;
                ammCollider.size = ammBounds.Value.size;
            }

            return amm;
        }

        static Bounds? TransformBoundsUntil(Bounds? bounds, Transform transform, Transform endTransform)
        {
            while (transform != endTransform)
            {
                bounds = UnityUtils.TransformBound(bounds, transform);
                transform = transform.parent;
            }

            return bounds;
        }


        static Vector3 Assimp2Unity(in Vector3f vector3) =>
            new Vector3(vector3.X, vector3.Y, vector3.Z);

        static void MemCopy<TA, TB>([NotNull] TA[] src, [NotNull] TB[] dst, int sizeToCopy)
            where TA : unmanaged
            where TB : unmanaged
        {
            unsafe
            {
                if (sizeToCopy > src.Length * sizeof(TA) || sizeToCopy > dst.Length * sizeof(TB))
                {
                    throw new InvalidOperationException("Potential buffer overflow!");
                }

                fixed (TA* a = src)
                fixed (TB* b = dst)
                {
                    Buffer.MemoryCopy(a, b, sizeToCopy, sizeToCopy);
                }
            }
        }
    }
}