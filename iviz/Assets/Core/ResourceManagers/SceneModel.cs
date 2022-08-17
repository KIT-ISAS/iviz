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
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Tools;
using Iviz.Urdf;
using UnityEngine;
using Color32 = UnityEngine.Color32;
using Object = UnityEngine.Object;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Displays
{
    public static class SceneModel
    {
        public static async ValueTask<MeshMarkerHolderDisplay> CreateAsync(string uriString,
            Model msg, IServiceProvider? provider, CancellationToken token)
        {
            ThrowHelper.ThrowIfNull(uriString, nameof(uriString));
            ThrowHelper.ThrowIfNull(msg, nameof(msg));

            if (!Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
            {
                throw new ArgumentException($"Uri string '{uriString}' is not a valid URI!");
            }

            token.ThrowIfCancellationRequested();
            var root = new GameObject($"Root:{uriString} [{msg.OrientationHint}]");

            try
            {
                return await CreateImpl(uriString, msg, provider, token, root);
            }
            catch
            {
                if (root != null)
                {
                    Object.Destroy(root);
                }

                throw;
            }
        }

        static async ValueTask<MeshMarkerHolderDisplay> CreateImpl(string uriString, Model msg,
            IServiceProvider? provider, CancellationToken token, GameObject root)
        {
            root.transform.localRotation =
                msg.OrientationHint.ToUpperInvariant() == "Z_UP"
                    ? Quaternion.Euler(0, -90, 0)
                    : Quaternion.Euler(90, 0, 90);

            var marker = root.AddComponent<MeshMarkerHolderDisplay>();
            var children = new List<MeshTrianglesDisplay>();
            var templateMeshes = new List<MeshTrianglesDisplay>();

            foreach (var mesh in msg.Meshes)
            {
                token.ThrowIfCancellationRequested();

                var obj = new GameObject();
                obj.AddComponent<MeshRenderer>();
                obj.AddComponent<MeshFilter>();
                obj.TryAddComponent<BoxCollider>();

                obj.transform.SetParent(root.transform, false);

                var meshResource = obj.AddComponent<MeshTrianglesDisplay>();

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
                        : Array.Empty<Point32>();
                var meshBumpTexCoords =
                    bumpTexture != null && bumpTexture.UvIndex < mesh.TexCoords.Length
                        ? mesh.TexCoords[bumpTexture.UvIndex].Coords
                        : Array.Empty<Point32>();

                if (mesh.Tangents.Length != 0)
                {
                    var meshTangents = mesh.Tangents;
                    using var tangents = new Rent<Vector4>(meshTangents.Length);

                    var tangentsArray = tangents.Array;
                    for (int i = 0; i < meshTangents.Length; i++)
                    {
                        Vector4 v;
                        (v.x, v.y, v.z) = meshTangents[i];
                        v.w = -1;
                        tangentsArray[i] = v;
                    }

                    SetMesh(tangents);
                }
                else
                {
                    SetMesh(Rent.Empty<Vector4>());
                }

                void SetMesh(Rent<Vector4> tangents)
                {
                    try
                    {
                        meshResource.Set(
                            MemoryMarshal.Cast<Point32, Vector3>(mesh.Vertices),
                            MemoryMarshal.Cast<Point32, Vector3>(mesh.Normals),
                            tangents,
                            MemoryMarshal.Cast<Point32, Vector3>(meshDiffuseTexCoords),
                            MemoryMarshal.Cast<Point32, Vector3>(meshBumpTexCoords),
                            MemoryMarshal.Cast<Triangle, int>(mesh.Faces),
                            MemoryMarshal.Cast<Iviz.Msgs.IvizMsgs.Color32, Color32>(meshColors)
                        );
                    }
                    catch (Exception e)
                    {
                        throw new InvalidResourceException($"Failed to load resource '{uriString}'", e);
                    }
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
                        RosLogger.Warn(nameof(SceneModel) + ": Failed to retrieve diffuse texture " +
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
                        RosLogger.Warn(nameof(SceneModel) + ": Failed to retrieve bump texture " +
                                       $"'{bumpTexture.Path}' required by '{uriString}'");
                    }
                }

                children.Add(meshResource);
                templateMeshes.Add(meshResource);
            }

            var nodes = new List<GameObject>();

            using var meshIsBeingUsed = new Rent<bool>(templateMeshes.Count);
            meshIsBeingUsed.AsSpan().Fill(false);

            foreach (var node in msg.Nodes)
            {
                token.ThrowIfCancellationRequested();
                var nodeObject = new GameObject($"Node:{node.Name}");
                nodes.Add(nodeObject);

                var nodeObjectTransform = nodeObject.transform;
                nodeObjectTransform.SetParent(
                    node.Parent == -1 ? root.transform : nodes[node.Parent].transform,
                    false);

                if (node.Transform.M.Length * sizeof(float) != Unsafe.SizeOf<Matrix4x4>())
                {
                    throw new IndexOutOfRangeException("Invalid array size!");
                }
                
                Matrix4x4 m = Unsafe.As<float, Matrix4x4>(ref node.Transform.M[0]);

                nodeObjectTransform.localRotation = m.rotation;
                nodeObjectTransform.localPosition = m.GetColumn(3);
                nodeObjectTransform.localScale = m.lossyScale;

                foreach (int meshId in node.Meshes)
                {
                    if (!meshIsBeingUsed[meshId])
                    {
                        templateMeshes[meshId].transform.SetParent(nodeObjectTransform, false);
                        meshIsBeingUsed[meshId] = true;
                    }
                    else
                    {
                        var newMesh = Object.Instantiate(
                            templateMeshes[meshId].gameObject,
                            nodeObjectTransform,
                            false);
                        children.Add(newMesh.GetComponent<MeshTrianglesDisplay>());
                    }
                }
            }

            for (int i = 0; i < meshIsBeingUsed.Length; i++)
            {
                if (!meshIsBeingUsed[i])
                {
                    templateMeshes[i].Visible = false;
                }
            }

            marker.Children = children.ToArray();
            marker.UpdateBounds();

            return marker;
        }

        static ValueTask<ResourceKey<Texture2D>?> GetTextureResourceAsync(string uriString, string localPath,
            IServiceProvider? provider, CancellationToken token)
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