using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using Assimp;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Roslib;
using Material = Assimp.Material;
using Mesh = Assimp.Mesh;
using Node = Assimp.Node;

namespace Iviz.ModelService
{
    public static class ModelService
    {
        const string ModelServiceName = "/iviz/get_model_resource";
        const string TextureServiceName = "/iviz/get_model_texture"; 
        
        static readonly AssimpContext Importer = new AssimpContext();
        
        static readonly Dictionary<string, List<string>> PackagePaths = new Dictionary<string, List<string>>();

        static void Main()
        {
            Uri masterUri = RosClient.EnvironmentMasterUri;
            if (masterUri is null)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to determine master uri");
                return;
            }
            
            RosClient client;
            try
            {
                client = new RosClient(masterUri, "/iviz_model_loader");
            }
            catch (ConnectionException)
            {
                Console.Error.WriteLine("EE Fatal error: Failed to connect to the ROS master");
                return;
            }

            Console.WriteLine("** Used package paths:");
            string packagePath = Environment.GetEnvironmentVariable("ROS_PACKAGE_PATH");
            if (packagePath is null)
            {
                Console.Error.WriteLine("EE Cannot retrieve environment variable ROS_PACKAGE_PATH");
            }
            else
            {
                string[] paths = packagePath.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var path in paths)
                {
                    CheckPath(null, path);
                }
            }

            client.AdvertiseService<GetModelResource>(ModelServiceName, ModelCallback);
            client.AdvertiseService<GetModelTexture>(TextureServiceName, TextureCallback);

            WaitForCancel();

            client.Close();
        }

        static void CheckPath(string folderName, string path)
        {
            if (File.Exists(path + "/package.xml"))
            {
                AddPath(folderName, path);
                return;
            }

            foreach (string subFolderPath in Directory.GetDirectories(path))
            {
                string subFolder = Path.GetFileName(subFolderPath);
                CheckPath(subFolder, subFolderPath);
            }
        }

        static void AddPath(string package, string path)
        {
            if (!PackagePaths.TryGetValue(package, out List<string> paths))
            {
                paths = new List<string>();
                PackagePaths[package] = paths;
            }
            paths.Add(path);
            Console.WriteLine("++ " + package);
        }

        static void WaitForCancel()
        {
            object o = new object();
            Console.CancelKeyPress += delegate
            {
                lock (o) Monitor.Pulse(o);
            };
            lock (o) Monitor.Wait(o);
        }

        static string ResolvePath(Uri uri)
        {
            string package = uri.Host;
            if (!PackagePaths.TryGetValue(package, out List<string> paths))
            {
                Console.Error.WriteLine("EE Failed to find package '" + package + "'.");
                return null;
            }
            
            string subPath = uri.AbsolutePath;
            foreach (string packagePath in paths)
            {
                string path = packagePath + "/" + subPath;
                if (File.Exists(path))
                {
                    return path;
                }
            }

            Console.Error.WriteLine("EE Failed to find resource '" + uri + "'.");
            return null;
        }
        
        static void ModelCallback(GetModelResource msg)
        {
            bool success = Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out Uri uri);
            if (!success)
            {
                msg.Response.Success = false;
                msg.Response.Message = "Failed to parse uri from requested string";
                return;
            }

            string modelPath = ResolvePath(uri);
            if (modelPath is null)
            {
                msg.Response.Success = false;
                msg.Response.Message = "Failed to find resource path";
                return;
            }

            Model model;
            try
            {
                model = LoadModel(modelPath);
            }
            catch (AssimpException e)
            {
                Console.Error.WriteLine("EE Assimp exception loading '" + modelPath + "':");
                Console.Error.WriteLine(e);

                msg.Response.Success = false;
                msg.Response.Message = "Failed to load model";
                return;
            }            

            msg.Response.Success = true;
            msg.Response.Message = "";
            msg.Response.Model = model;

            Console.WriteLine(">> " + uri);
        }
        
        static void TextureCallback(GetModelTexture msg)
        {
            bool success = Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out Uri uri);
            if (!success)
            {
                msg.Response.Success = false;
                msg.Response.Message = "Failed to parse uri from requested string";
                return;
            }

            string texturePath = ResolvePath(uri);
            if (texturePath is null)
            {
                msg.Response.Success = false;
                msg.Response.Message = "Failed to find resource path";
                return;
            }

            byte[] data = File.ReadAllBytes(texturePath);
         

            msg.Response.Success = true;
            msg.Response.Message = "";
            msg.Response.Image = new CompressedImage()
            {
                Format = Path.GetExtension(texturePath).Replace(".", ""),
                Data = data
            };

            Console.WriteLine(">> " + uri);
        }


        static Model LoadModel(string fileName)
        {
            string orientationHint = "";
            if (fileName.Length > 4 && fileName[^4..].ToLowerInvariant() == ".dae")
            { 
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                var nodeList = doc.GetElementsByTagName("up_axis");
                if (nodeList.Count != 0)
                {
                    orientationHint = nodeList[0].InnerText ?? "";
                }
            }

            Scene scene = Importer.ImportFile(fileName, PostProcessPreset.TargetRealTimeMaximumQuality | PostProcessPreset.ConvertToLeftHanded);
            Model msg = new Model
            {
                Filename = Path.GetFileName(fileName),
                Meshes = new Msgs.IvizMsgs.Mesh[scene.Meshes.Count],
                OrientationHint = orientationHint
            };

            //Console.WriteLine(msg.Filename + " -> " + msg.OrientationHint);

            
            List<Triangle> faces = new List<Triangle>();
            for (int i = 0; i < scene.MeshCount; i++)
            {
                Mesh srcMesh = scene.Meshes[i];

                faces.Clear();
                for (int j = 0; j < srcMesh.FaceCount; j++)
                {
                    Face face = srcMesh.Faces[j];
                    switch (face.IndexCount)
                    {
                        case 3:
                            faces.Add(new Triangle(
                                (uint) face.Indices[0],
                                (uint) face.Indices[1],
                                (uint) face.Indices[2]
                            ));
                            break;
                        case 4:
                            faces.Add(new Triangle(
                                (uint) face.Indices[0],
                                (uint) face.Indices[1],
                                (uint) face.Indices[2]
                            ));
                            faces.Add(new Triangle(
                                (uint) face.Indices[0],
                                (uint) face.Indices[2],
                                (uint) face.Indices[3]
                            ));
                            break;
                    }
                }

                Msgs.IvizMsgs.Mesh dstMesh = new Msgs.IvizMsgs.Mesh
                (
                    Name: srcMesh.Name ?? "[mesh]",
                    Vertices: srcMesh.Vertices.Select(x => ToVector3(x)).ToArray(),
                    Normals: srcMesh.Normals.Select(x => ToVector3(x)).ToArray(),
                    TexCoords: srcMesh.HasTextureCoords(0)
                        ? srcMesh.TextureCoordinateChannels[0].Select(x => ToVector2(x)).ToArray()
                        : Array.Empty<Vector2>(),
                    Colors: srcMesh.HasVertexColors(0)
                        ? srcMesh.VertexColorChannels[0].Select(x => ToColor(x)).ToArray()
                        : Array.Empty<Color>(),
                    Faces: faces.ToArray(),
                    MaterialIndex: (uint) srcMesh.MaterialIndex
                );
                
                //Console.WriteLine(srcMesh.HasTextureCoords(0));

                msg.Meshes[i] = dstMesh;
            }

            msg.Materials = new Msgs.IvizMsgs.Material[scene.MaterialCount];
            for (int i = 0; i < scene.MaterialCount; i++)
            {
                Material srcMaterial = scene.Materials[i];
                msg.Materials[i] = new Msgs.IvizMsgs.Material
                (
                    Name: srcMaterial.Name ?? "[material]",
                    Ambient: ToColor(srcMaterial.ColorAmbient),
                    Diffuse: ToColor(srcMaterial.ColorDiffuse),
                    Emissive: ToColor(srcMaterial.ColorEmissive),
                    DiffuseTexture: new Texture()
                    {
                        Path = srcMaterial.TextureDiffuse.FilePath ?? ""
                    }
                );
            }

            List<Msgs.IvizMsgs.Node> nodes = new List<Msgs.IvizMsgs.Node>();
            ProcessNode(scene.RootNode, nodes, new Dictionary<Node, int>());

            msg.Nodes = nodes.ToArray();

            return msg;
        }

        static void ProcessNode(Node node, List<Msgs.IvizMsgs.Node> nodes, Dictionary<Node, int> ids)
        {
            if (node.Children.Count == 0 && node.MeshIndices.Count == 0)
            {
                return;
            }
            
            //Console.WriteLine(string.Join("\n", node.Metadata));
            
            ids[node] = ids.Count;
            int parentId = node.Parent is null ? -1 : ids[node.Parent];

            nodes.Add(new Msgs.IvizMsgs.Node(
                node.Name,
                parentId,
                ToMatrix(node.Transform),
                node.MeshIndices.ToArray()
            ));
            
            foreach (Node child in node.Children)
            {
                ProcessNode(child, nodes, ids);
            }

        }

        static Vector3 ToVector3(in Vector3D v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        static Vector2 ToVector2(in Vector3D v)
        {
            return new Vector2(v.X, 1 - v.Y);
        }

        static Color ToColor(in Color4D color)
        {
            int r = (int) (Math.Max(Math.Min(color.R, 1), 0) * 255);
            int g = (int) (Math.Max(Math.Min(color.G, 1), 0) * 255);
            int b = (int) (Math.Max(Math.Min(color.B, 1), 0) * 255);

            int a = (int) (Math.Max(Math.Min(color.A, 1), 0) * 255);
            return new Color((byte) r, (byte) g, (byte) b, (byte) a);
        }

        static Matrix4 ToMatrix(in Matrix4x4 v)
        {
            return new Matrix4(new[]
            {
                v.A1, v.B1, v.C1, v.D1,
                v.A2, v.B2, v.C2, v.D2,
                v.A3, v.B3, v.C3, v.D3,
                v.A4, v.B4, v.C4, v.D4,
            });
        }
    }
}