#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

                var meshResource = obj.AddComponent<MeshTrianglesResource>();

                meshResource.gameObject.name = mesh.Name;

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

                var meshTangents = mesh.Tangents;
                using (var tangents = new Rent<Vector4>(meshTangents.Length))
                {
                    var v = new Vector4(0, 0, 0, -1);
                    for (int i = 0; i < meshTangents.Length; i++)
                    {
                        (v.x, v.y, v.z) = meshTangents[i];
                        tangents.Array[i] = v;
                    }

                    meshResource.Set(
                        MemoryMarshal.Cast<Vector3f, Vector3>(mesh.Vertices),
                        MemoryMarshal.Cast<Vector3f, Vector3>(mesh.Normals),
                        tangents,
                        MemoryMarshal.Cast<Vector3f, Vector3>(meshDiffuseTexCoords),
                        MemoryMarshal.Cast<Vector3f, Vector3>(meshBumpTexCoords),
                        MemoryMarshal.Cast<Triangle, int>(mesh.Faces),
                        MemoryMarshal.Cast<Iviz.Msgs.IvizMsgs.Color32, Color32>(meshColors)
                    );
                }

                meshResource.Color = material.Diffuse.ToColor32();
                meshResource.EmissiveColor = material.Emissive.ToColor32();
                meshResource.MeshName = $"{uriString}/{mesh.Name}";

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

                var nodeObjectTransform = nodeObject.transform;
                nodeObjectTransform.SetParent(
                    node.Parent == -1 ? root.transform : nodes[node.Parent].transform,
                    false);

                var m = new Matrix4x4();
                foreach (int i in ..16)
                {
                    m[i] = node.Transform.M[i];
                }

                nodeObjectTransform.localRotation = m.rotation;
                nodeObjectTransform.localPosition = m.GetColumn(3);
                nodeObjectTransform.localScale = m.lossyScale;

                foreach (int meshId in node.Meshes)
                {
                    if (!used[meshId])
                    {
                        templateMeshes[meshId].transform.SetParent(nodeObjectTransform, false);
                        used[meshId] = true;
                    }
                    else
                    {
                        var newMesh = Object.Instantiate(
                            templateMeshes[meshId].gameObject,
                            nodeObjectTransform,
                            false);
                        children.Add(newMesh.GetComponent<MeshTrianglesResource>());
                    }
                }
            }

            marker.Children = children.ToArray();
            marker.UpdateBounds();

            return marker;
        }

        static ValueTask<Info<Texture2D>?> GetTextureResourceAsync(string uriString, string localPath,
            IExternalServiceProvider? provider, CancellationToken token)
        {
            var uri = new Uri(uriString);
            string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
            string directoryName = Path.GetDirectoryName(uriPath) ?? "";
            string validatedName = Path.DirectorySeparatorChar == '\\'
                ? directoryName.Replace('\\', '/') // windows!
                : directoryName;

            string textureUri = $"{uri.Scheme}://{uri.Host}{validatedName}/{localPath}";
            return Resource.GetTextureResourceAsync(textureUri, provider, token);
        }
    }
}