using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using Assimp;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Roslib;
using Iviz.Sdf;
using Color = Iviz.Msgs.IvizMsgs.Color;
using Include = Iviz.Msgs.IvizMsgs.Include;
using Material = Assimp.Material;
using Mesh = Assimp.Mesh;
using Model = Iviz.Msgs.IvizMsgs.Model;
using Node = Assimp.Node;
using Texture = Iviz.Msgs.IvizMsgs.Texture;
using Uri = System.Uri;
using Vector2 = Iviz.Msgs.IvizMsgs.Vector2;
using Vector3 = Iviz.Msgs.IvizMsgs.Vector3;

namespace Iviz.ModelService
{
    public static class ModelService
    {
        const string ModelServiceName = "/iviz/get_model_resource";
        const string TextureServiceName = "/iviz/get_model_texture";
        const string FileServiceName = "/iviz/get_file";
        const string SdfServiceName = "/iviz/get_sdf";

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

            using RosClient client = new RosClient(masterUri, "/iviz_model_loader");

            Console.WriteLine("** Used package paths:");
            string packagePath = Environment.GetEnvironmentVariable("ROS_PACKAGE_PATH");
            if (packagePath is null)
            {
                Console.Error.WriteLine("EE Cannot retrieve environment variable ROS_PACKAGE_PATH");
            }
            else
            {
                string[] paths = packagePath.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string path in paths)
                {
                    if (!Directory.Exists(path))
                    {
                        Console.WriteLine("** Ignoring '" + path + "'");
                        continue;
                    }
                    
                    CheckPath("", path);
                }
            }

            client.AdvertiseService<GetModelResource>(ModelServiceName, ModelCallback);
            client.AdvertiseService<GetModelTexture>(TextureServiceName, TextureCallback);
            client.AdvertiseService<GetFile>(FileServiceName, FileCallback);
            client.AdvertiseService<GetSdf>(SdfServiceName, SdfCallback);

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
            return ResolvePath(uri, out string _);
        }

        static string ResolvePath(Uri uri, out string outPackagePath)
        {
            outPackagePath = null;

            string package = uri.Host;
            if (!PackagePaths.TryGetValue(package, out List<string> paths))
            {
                Console.Error.WriteLine("EE Failed to find package '" + package + "'.");
                return null;
            }

            string subPath = Uri.UnescapeDataString(uri.AbsolutePath);

            foreach (string packagePath in paths)
            {
                string path = packagePath + "/" + subPath;

                if (!File.Exists(path))
                {
                    continue;
                }

                if (Path.GetFullPath(path).StartsWith(packagePath, false, BuiltIns.Culture))
                {
                    outPackagePath = packagePath;
                    return path;
                }

                Console.Error.WriteLine("EE Rejecting resource request '" + uri + "' for path traversal.");
                return null;
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

            if (uri.Scheme != "package")
            {
                msg.Response.Success = false;
                msg.Response.Message = "Only 'package' scheme is supported";
                return;
            }

            string modelPath = ResolvePath(uri);
            if (modelPath is null)
            {
                msg.Response.Success = false;
                msg.Response.Message = "Failed to find resource path";
                return;
            }
 
            Console.WriteLine("** Requesting " + modelPath);

            Model model;
            try
            {
                model = LoadModel(modelPath);
                model.Filename = uri.ToString();
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
            // TODO: force conversion to either png or jpg

            bool success = Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out Uri uri);
            if (!success)
            {
                msg.Response.Success = false;
                msg.Response.Message = "Failed to parse uri from requested string";
                return;
            }

            if (uri.Scheme != "package")
            {
                msg.Response.Success = false;
                msg.Response.Message = "Only 'package' scheme is supported";
                return;
            }

            string texturePath = ResolvePath(uri);
            if (string.IsNullOrWhiteSpace(texturePath))
            {
                msg.Response.Success = false;
                msg.Response.Message = "Failed to find resource path";
                return;
            }

            byte[] data;

            try
            {
                data = File.ReadAllBytes(texturePath);
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("EE Failed to read '" + texturePath + "': " + e.Message);
                msg.Response.Success = false;
                msg.Response.Message = e.Message;
                return;
            }


            msg.Response.Success = true;
            msg.Response.Message = "";
            msg.Response.Image = new CompressedImage
            {
                Format = Path.GetExtension(texturePath).Replace(".", ""),
                Data = data
            };

            Console.WriteLine(">> " + uri);
        }


        static Model LoadModel(string fileName)
        {
            string orientationHint = "";
            if (fileName.EndsWith(".DAE", true, BuiltIns.Culture))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                var nodeList = doc.GetElementsByTagName("up_axis");
                if (nodeList.Count != 0)
                {
                    orientationHint = nodeList[0].InnerText ?? "";
                }
            }

            Assimp.Scene scene = Importer.ImportFile(fileName,
                PostProcessPreset.TargetRealTimeMaximumQuality | PostProcessPreset.ConvertToLeftHanded);
            Model msg = new Model
            {
                Meshes = new Msgs.IvizMsgs.Mesh[scene.Meshes.Count],
                OrientationHint = orientationHint
            };

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
                    DiffuseTexture: new Texture
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
        
        static Matrix4 ToMatrixLH(in Matrix4x4 v)
        {
            return new Matrix4(new[]
            {
                v.A1, -v.B1, -v.C1, -v.D1,
                -v.A2, v.B2, v.C2, v.D2,
                -v.A3, v.B3, v.C3, v.D3,
                -v.A4, v.B4, v.C4, v.D4,
            });
        }

        static void FileCallback(GetFile msg)
        {
            bool success = Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out Uri uri);
            if (!success)
            {
                msg.Response.Message = "Failed to parse uri from requested string";
                return;
            }

            string filePath = ResolvePath(uri);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                msg.Response.Message = "Failed to find resource path";
                return;
            }

            byte[] data;
            try
            {
                data = File.ReadAllBytes(filePath);
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("EE Failed to read '" + filePath + "': " + e.Message);
                msg.Response.Message = e.Message;
                return;
            }

            msg.Response.Success = true;
            msg.Response.Message = "";
            msg.Response.Bytes = data;

            Console.WriteLine(">> " + uri);
        }

        static void SdfCallback(GetSdf msg)
        {
            bool success = Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out Uri uri);
            if (!success)
            {
                msg.Response.Message = "Failed to parse uri from requested string";
                return;
            }

            if (uri.Scheme != "package")
            {
                msg.Response.Message = "Only 'package' scheme is supported";
                return;
            }

            string modelPath = ResolvePath(uri, out string packagePath);
            if (string.IsNullOrWhiteSpace(modelPath))
            {
                Console.Error.WriteLine("EE Failed to find resource path for '" + modelPath + "'");
                msg.Response.Message = "Failed to find resource path";
                return;
            }

            string data;
            try
            {
                data = File.ReadAllText(modelPath);
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("EE Failed to read '" + modelPath + "': " + e.Message);
                msg.Response.Message = e.Message;
                return;
            }

            Console.WriteLine("package path: " + packagePath);
            Dictionary<string, string> modelPaths = SdfFile.CreateModelPaths(packagePath);

            SdfFile file;
            try
            {
                file = SdfFile.Create(data).ResolveIncludes(modelPaths);
            }
            catch (Exception e) when (e is IOException || e is MalformedSdfException)
            {
                Console.Error.WriteLine("EE Failed to parse '" + modelPath + "': " + e.Message);
                msg.Response.Message = e.Message;
                return;
            }

            List<Include> includes = new List<Include>();
            ResolveIncludes(file, includes);

            msg.Response.Success = true;
            msg.Response.Scene = new Msgs.IvizMsgs.Scene
            {
                Name = file.Worlds.Count != 0 ? file.Worlds[0].Name : "sdf",
                Filename = uri.ToString(),
                Includes = includes.ToArray()
            };
        }

        static void ResolveIncludes(SdfFile file, ICollection<Include> includes)
        {
            if (file.Worlds.Count != 0)
            {
                ResolveIncludes(file.Worlds[0], includes);
                return;
            }

            foreach (Sdf.Model model in file.Models)
            {
                ResolveIncludes(model, includes, Matrix4x4.Identity);
            }
        }

        static void ResolveIncludes(World world, ICollection<Include> includes)
        {
            foreach (Sdf.Model model in world.Models)
            {
                ResolveIncludes(model, includes, Matrix4x4.Identity);
            }
        }

        static void ResolveIncludes(Sdf.Model model, ICollection<Include> includes, in Matrix4x4 inPose)
        {
            if (model.IsInvalid)
            {
                return;
            }
            
            Matrix4x4 pose = Multiply(inPose, Multiply(ToPose(model.IncludePose), ToPose(model.Pose)));

            foreach (Link link in model.Links)
            {
                Matrix4x4 linkPose = Multiply(pose, ToPose(link.Pose));
                foreach (Visual visual in link.Visuals)
                {
                    ResolveIncludes(visual, includes, linkPose);
                }
            }
            
            foreach (Sdf.Model innerModel in model.Models)
            {
                ResolveIncludes(innerModel, includes, pose);
            }
        }

        static void ResolveIncludes(Visual visual, ICollection<Include> includes, in Matrix4x4 inPose)
        {
            if (visual.Geometry.Empty != null)
            {
                return;
            }

            Matrix4x4 pose = Multiply(inPose, ToPose(visual.Pose));

            Msgs.IvizMsgs.Material includeMaterial;
            if (visual.Material != null)
            {
                includeMaterial = new Msgs.IvizMsgs.Material
                {
                    Name = visual.Name + "_material",
                    Diffuse = ToColor(visual.Material.Diffuse),
                    Emissive = ToColor(visual.Material.Emissive),
                };
            }
            else
            {
                includeMaterial = new Msgs.IvizMsgs.Material();
            }

            if (visual.Geometry.Box != null)
            {
                Vector3D diag = new Vector3D(
                    (float) visual.Geometry.Box.Scale.X,
                    (float) visual.Geometry.Box.Scale.Y,
                    (float) visual.Geometry.Box.Scale.Z
                );
                pose = Multiply(pose, Matrix4x4.FromScaling(diag));

                includes.Add(new Include
                {
                    Uri = "package://iviz_internal/cube",
                    Pose = ToMatrixLH(pose),
                    Material = includeMaterial
                });
            }
            else if (visual.Geometry.Cylinder != null)
            {
                Vector3D diag = new Vector3D(
                    (float) visual.Geometry.Cylinder.Radius,
                    (float) visual.Geometry.Cylinder.Radius,
                    (float) visual.Geometry.Cylinder.Length
                );
                pose = Multiply(pose, Matrix4x4.FromScaling(diag));

                includes.Add(new Include
                {
                    Uri = "package://iviz_internal/cylinder",
                    Pose = ToMatrixLH(pose),
                    Material = includeMaterial
                });
            }
            else if (visual.Geometry.Sphere != null)
            {
                Vector3D diag = new Vector3D((float) visual.Geometry.Sphere.Radius);
                pose = Multiply(pose, Matrix4x4.FromScaling(diag));

                includes.Add(new Include
                {
                    Uri = "package://iviz_internal/cylinder",
                    Pose = ToMatrixLH(pose),

                });
            } else if (visual.Geometry.Mesh != null)
            {
                Vector3D diag = new Vector3D(
                    (float) visual.Geometry.Mesh.Scale.X,
                    (float) visual.Geometry.Mesh.Scale.Y,
                    (float) visual.Geometry.Mesh.Scale.Z
                );
                pose = Multiply(pose, Matrix4x4.FromScaling(diag));

                includes.Add(new Include
                {
                    Uri = visual.Geometry.Mesh.Uri.Value,
                    Pose = ToMatrixLH(pose),
                    Material = includeMaterial
                });                
            }
        }

        static Matrix4x4 ToPose(Pose pose)
        {
            if (pose is null)
            {
                return Matrix4x4.Identity;
            }

            Matrix4x4 result = Matrix4x4.FromEulerAnglesXYZ(
                (float) pose.Orientation.X,
                (float) pose.Orientation.Y,
                (float) pose.Orientation.Z
            );

            result.A4 = (float) pose.Position.X;
            result.B4 = (float) pose.Position.Y;
            result.C4 = (float) pose.Position.Z;
            
            return result;
        }

        static Matrix4x4 Multiply(in Matrix4x4 a, in Matrix4x4 b)
        {
            return b * a; // assimp inverts natural order of multiplication!
        }


        static Color ToColor(Sdf.Color color)
        {
            int r = (int) (Math.Max(Math.Min(color.R, 1), 0) * 255);
            int g = (int) (Math.Max(Math.Min(color.G, 1), 0) * 255);
            int b = (int) (Math.Max(Math.Min(color.B, 1), 0) * 255);

            int a = (int) (Math.Max(Math.Min(color.A, 1), 0) * 255);
            return new Color((byte) r, (byte) g, (byte) b, (byte) a);
        }
    }
}
