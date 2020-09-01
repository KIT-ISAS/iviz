using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public static class SceneModel
    {
        public static GameObject Create(Uri uri, Msgs.IvizMsgs.Model msg)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (msg is null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            GameObject root = new GameObject("Root:" + uri + " [" + msg.OrientationHint + "]");

            switch (msg.OrientationHint.ToUpperInvariant())
            {
                case "Z_UP":
                    root.transform.localRotation = Quaternion.Euler(0, -90, 0);
                    break;
                default:
                    root.transform.localRotation = Quaternion.Euler(90, 0, 90);
                    break;
            }

            //Debug.Log(JsonConvert.SerializeObject(msg.Nodes, Formatting.Indented));

            AggregatedMeshMarkerResource amm = root.AddComponent<AggregatedMeshMarkerResource>();
            List<MeshTrianglesResource> children = new List<MeshTrianglesResource>();
            List<MeshTrianglesResource> templateMeshes = new List<MeshTrianglesResource>();

            foreach (var mesh in msg.Meshes)
            {
                GameObject obj = new GameObject();

                obj.AddComponent<MeshRenderer>();
                obj.AddComponent<MeshFilter>();
                obj.AddComponent<BoxCollider>();

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
                MemCopy(mesh.Colors, colors, colors.Length * 4);

                Vector2[] texCoords = new Vector2[mesh.TexCoords.Length];
                MemCopy(mesh.TexCoords, texCoords, texCoords.Length * 2 * sizeof(float));

                int[] triangles = new int[mesh.Faces.Length * 3];
                MemCopy(mesh.Faces, triangles, triangles.Length * 4);

                var material = msg.Materials[mesh.MaterialIndex];
                r.Color = new Color32(material.Diffuse.R, material.Diffuse.G, material.Diffuse.B, material.Diffuse.A);
                r.EmissiveColor = new Color32(material.Emissive.R, material.Emissive.G, material.Emissive.B, material.Emissive.A);

                if (material.DiffuseTexture.Path.Length != 0)
                {
                    string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
                    string texturePath = $"{Path.GetDirectoryName(uriPath)}/{material.DiffuseTexture.Path}";
                    Uri textureUri = new Uri($"{uri.Scheme}://{uri.Host}{texturePath}");
                    if (Resource.External.TryGet(textureUri, out Resource.Info<Texture2D> info))
                    {
                        r.Texture = info.Object;
                    }
                    else
                    {
                        Debug.Log("SceneModel: Failed to retrieve texture " + textureUri);
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
                        GameObject newMesh = UnityEngine.Object.Instantiate(templateMeshes[meshId].gameObject,
                            nodeObject.transform, false);
                        children.Add(newMesh.GetComponent<MeshTrianglesResource>());
                    }
                }
            }

            amm.Children = children;

            return root;
        }


        static Vector3 Assimp2Unity(in Iviz.Msgs.IvizMsgs.Vector3 vector3) =>
            new Vector3(vector3.X, vector3.Y, vector3.Z);

        static void MemCopy<A, B>(A[] src, B[] dst, int bytes)
            where A : unmanaged
            where B : unmanaged
        {
            unsafe
            {
                fixed (A* a = src)
                fixed (B* b = dst)
                {
                    Buffer.MemoryCopy(a, b, bytes, bytes);
                }
            }
        }
    }
}