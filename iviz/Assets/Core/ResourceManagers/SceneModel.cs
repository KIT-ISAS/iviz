#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Tools;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Color32 = UnityEngine.Color32;
using Object = UnityEngine.Object;

namespace Iviz.Displays
{
    public static class SceneModel
    {
        public static async ValueTask<MeshMarkerHolderResource> CreateAsync(string uriString,
            Model msg, IExternalServiceProvider? provider, CancellationToken token)
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            if (!Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
            {
                throw new ArgumentException($"Uri string '{uriString}' is not a valid URI!");
            }

            if (msg is null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            token.ThrowIfCancellationRequested();
            var root = new GameObject($"Root:{uriString} [{msg.OrientationHint}]");

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

        static async ValueTask<MeshMarkerHolderResource> CreateImpl(string uriString, Model msg,
            IExternalServiceProvider? provider, CancellationToken token, GameObject root)
        {
            root.transform.localRotation =
                msg.OrientationHint.ToUpperInvariant() == "Z_UP"
                    ? Quaternion.Euler(0, -90, 0)
                    : Quaternion.Euler(90, 0, 90);

            var marker = root.AddComponent<MeshMarkerHolderResource>();
            var children = new List<MeshTrianglesResource>();
            var templateMeshes = new List<MeshTrianglesResource>();

            foreach (var mesh in msg.Meshes)
            {
                token.ThrowIfCancellationRequested();
                var obj = new GameObject();
                obj.AddComponent<MeshRenderer>();
                obj.AddComponent<MeshFilter>();
                obj.EnsureComponent<BoxCollider>();

                obj.transform.SetParent(root.transform, false);

                MeshTrianglesResource meshResource = obj.AddComponent<MeshTrianglesResource>();

                meshResource.Name = mesh.Name;

                var meshColors = mesh.ColorChannels.Length != 0
                    ? mesh.ColorChannels[0].Colors
                    : Array.Empty<Iviz.Msgs.IvizMsgs.Color32>();

                var material = msg.Materials[(int)mesh.MaterialIndex];
                var diffuseTexture =
                    material.Textures.FirstOrDefault(texture => texture.Type == Msgs.IvizMsgs.Texture.TYPE_DIFFUSE);
                var bumpTexture =
                    material.Textures.FirstOrDefault(texture => texture.Type == Msgs.IvizMsgs.Texture.TYPE_NORMALS);

                var meshDiffuseTexCoords =
                    diffuseTexture != null && diffuseTexture.UvIndex < mesh.TexCoords.Length
                        ? mesh.TexCoords[diffuseTexture.UvIndex].Coords
                        : Array.Empty<Vector3f>();
                var meshBumpTexCoords =
                    bumpTexture != null && bumpTexture.UvIndex < mesh.TexCoords.Length
                        ? mesh.TexCoords[bumpTexture.UvIndex].Coords
                        : Array.Empty<Vector3f>();

                using (var vertices = new Rent<Vector3>(mesh.Vertices.Length))
                using (var normals = new Rent<Vector3>(mesh.Normals.Length))
                using (var colors = new Rent<Color32>(meshColors.Length))
                using (var diffuseTexCoords = new Rent<Vector3>(meshDiffuseTexCoords.Length))
                using (var bumpTexCoords = new Rent<Vector3>(meshBumpTexCoords.Length))
                using (var tangents = new Rent<Vector4>(mesh.Tangents.Length))
                using (var triangles = new Rent<int>(mesh.Faces.Length * 3))
                {
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        vertices.Array[i] = Assimp2Unity(mesh.Vertices[i]);
                    }

                    MemCopy(mesh.Normals, normals.Array, normals.Length * 3 * sizeof(float));
                    MemCopy(meshColors, colors.Array, colors.Length * sizeof(int));
                    MemCopy(meshDiffuseTexCoords, diffuseTexCoords.Array, diffuseTexCoords.Length * 3 * sizeof(float));
                    MemCopy(meshBumpTexCoords, bumpTexCoords.Array, bumpTexCoords.Length * 3 * sizeof(float));
                    MemCopy(mesh.Faces, triangles.Array, triangles.Length * sizeof(int));

                    for (int i = 0; i < mesh.Tangents.Length; i++)
                    {
                        (float x, float y, float z) = mesh.Tangents[i];
                        tangents.Array[i] = new Vector4(x, y, z, -1);
                    }

                    meshResource.Set(vertices, normals, tangents, diffuseTexCoords, bumpTexCoords, triangles, colors);
                }

                meshResource.Color = material.Diffuse.ToColor32();
                meshResource.EmissiveColor = material.Emissive.ToColor32();

                if (diffuseTexture != null && diffuseTexture.Path.Length != 0)
                {
                    var textureInfo = await GetTextureResourceAsync(uriString, diffuseTexture.Path, provider, token);
                    if (textureInfo != null)
                    {
                        meshResource.DiffuseTexture = textureInfo.Object;
                    }
                    else
                    {
                        RosLogger.Warn($"SceneModel: Failed to retrieve diffuse texture " +
                                       $"'{diffuseTexture.Path}' required by '{uriString}'");
                    }
                }

                if (bumpTexture != null && bumpTexture.Path.Length != 0)
                {
                    var textureInfo = await GetTextureResourceAsync(uriString, bumpTexture.Path, provider, token);
                    if (textureInfo != null)
                    {
                        meshResource.BumpTexture = textureInfo.Object;
                    }
                    else
                    {
                        RosLogger.Warn($"SceneModel: Failed to retrieve normal texture " +
                                       $"'{bumpTexture.Path}' required by '{uriString}'");
                    }
                }

                meshResource.Mesh.name = mesh.Name;

                children.Add(meshResource);
                templateMeshes.Add(meshResource);
            }

            var nodes = new List<GameObject>();
            bool[] used = new bool[templateMeshes.Count];

            foreach (var node in msg.Nodes)
            {
                token.ThrowIfCancellationRequested();
                var nodeObject = new GameObject($"Node:{node.Name}");
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
                        var newMesh = Object.Instantiate(
                            templateMeshes[meshId].gameObject,
                            nodeObject.transform,
                            false);
                        children.Add(newMesh.GetComponent<MeshTrianglesResource>());
                    }
                }
            }

            marker.Children = children;
            marker.UpdateBounds();

            /*
            var parentCollider = root.EnsureComponent<BoxCollider>();

            var markerChildren = children
                .Select(resource => TransformBoundsUntil(resource.Bounds, resource.Transform, root.transform));
            var nullableRootBounds = markerChildren.CombineBounds();

            if (nullableRootBounds is { } rootBounds)
            {
                parentCollider.center = rootBounds.center;
                parentCollider.size = rootBounds.size;
            }
            */

            return marker;
        }

        static ValueTask<Info<Texture2D>?> GetTextureResourceAsync(string uriString, string localPath,
            IExternalServiceProvider? provider, CancellationToken token)
        {
            var uri = new Uri(uriString);
            string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
            string? directoryName = Path.GetDirectoryName(uriPath);
            if (!string.IsNullOrEmpty(directoryName) && Path.DirectorySeparatorChar == '\\')
            {
                directoryName = directoryName.Replace('\\', '/'); // windows!
            }

            string textureUri = $"{uri.Scheme}://{uri.Host}{directoryName}/{localPath}";
            return Resource.GetTextureResourceAsync(textureUri, provider, token);
        }

        static Bounds? TransformBoundsUntil(Bounds? bounds, Transform transform, Transform endTransform)
        {
            while (transform != endTransform)
            {
                bounds = bounds.TransformBound(transform);
                transform = transform.parent;
            }

            return bounds;
        }

        static Vector3 Assimp2Unity(in Vector3f vector3) => new(vector3.X, vector3.Y, vector3.Z);

        static unsafe void MemCopy<TA, TB>(TA[] src, TB[] dst, int sizeToCopy)
            where TA : unmanaged
            where TB : unmanaged
        {
            if (dst.Length * sizeof(TB) < sizeToCopy || src.Length * sizeof(TA) < sizeToCopy)
            {
                throw new ArgumentException("Potential buffer overflow");
            }

            fixed (void* srcPtr = src, dstPtr = dst)
            {
                UnsafeUtility.MemCpy(dstPtr, srcPtr, sizeToCopy);
            }
        }
    }
}