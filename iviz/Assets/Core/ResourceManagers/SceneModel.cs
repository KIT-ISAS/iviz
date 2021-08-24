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
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Buffer = System.Buffer;
using Color32 = UnityEngine.Color32;
using Object = UnityEngine.Object;
using Texture = UnityEngine.Texture;

namespace Iviz.Displays
{
    public static class SceneModel
    {
        [ItemNotNull]
        public static async ValueTask<AggregatedMeshMarkerResource> CreateAsync([NotNull] string uriString,
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
        static async ValueTask<AggregatedMeshMarkerResource> CreateImpl(string uriString, [NotNull] Model msg,
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
                obj.EnsureComponent<BoxCollider>();

                obj.transform.SetParent(root.transform, false);

                MeshTrianglesResource meshResource = obj.AddComponent<MeshTrianglesResource>();

                meshResource.Name = mesh.Name;

                Iviz.Msgs.IvizMsgs.Color32[] meshColors = mesh.ColorChannels.Length != 0
                    ? mesh.ColorChannels[0].Colors
                    : Array.Empty<Iviz.Msgs.IvizMsgs.Color32>();

                var material = msg.Materials[(int) mesh.MaterialIndex];
                Msgs.IvizMsgs.Texture diffuseTexture =
                    material.Textures.FirstOrDefault(texture => texture.Type == Msgs.IvizMsgs.Texture.TYPE_DIFFUSE);
                Msgs.IvizMsgs.Texture bumpTexture =
                    material.Textures.FirstOrDefault(texture => texture.Type == Msgs.IvizMsgs.Texture.TYPE_NORMALS);

                Vector3f[] meshDiffuseTexCoords =
                    diffuseTexture != null && diffuseTexture.UvIndex < mesh.TexCoords.Length
                        ? mesh.TexCoords[diffuseTexture.UvIndex].Coords
                        : Array.Empty<Vector3f>();
                Vector3f[] meshBumpTexCoords =
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
                        Core.Logger.Warn($"SceneModel: Failed to retrieve diffuse texture " +
                                         $"'{diffuseTexture.Path}' required by {uriString}");
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
                        Core.Logger.Warn($"SceneModel: Failed to retrieve normal texture " +
                                         $"'{bumpTexture.Path}' required by {uriString}");
                    }
                }

                meshResource.Mesh.name = mesh.Name;

                children.Add(meshResource);
                templateMeshes.Add(meshResource);
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

            BoxCollider ammCollider = root.EnsureComponent<BoxCollider>();

            Bounds? ammBounds = amm.Children
                .Select(resource => TransformBoundsUntil(resource.LocalBounds, resource.Transform, root.transform))
                .CombineBounds();
            
            if (ammBounds != null)
            {
                ammCollider.center = ammBounds.Value.center;
                ammCollider.size = ammBounds.Value.size;
            }

            return amm;
        }

        [ItemCanBeNull]
        static ValueTask<Info<Texture2D>> GetTextureResourceAsync([NotNull] string uriString,
            [NotNull] string localPath, IExternalServiceProvider provider, CancellationToken token)
        {
            Uri uri = new Uri(uriString);
            string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
            string directoryName = Path.GetDirectoryName(uriPath);
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


        static Vector3 Assimp2Unity(in Vector3f vector3) =>
            new Vector3(vector3.X, vector3.Y, vector3.Z);

        static void MemCopy<TA, TB>([NotNull] TA[] src, [NotNull] TB[] dst, int sizeToCopy)
            where TA : unmanaged
            where TB : unmanaged
        {
            Buffer.BlockCopy(src, 0, dst, 0, sizeToCopy);
        }
    }
}