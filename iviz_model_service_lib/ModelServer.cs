using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Assimp;
using Assimp.Configs;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.SensorMsgs;


namespace Iviz.ModelService
{
    public sealed class ModelServer : IDisposable
    {
        public const string ModelServiceName = "/iviz/get_model_resource";
        public const string TextureServiceName = "/iviz/get_model_texture";
        public const string FileServiceName = "/iviz/get_file";
        public const string SdfServiceName = "/iviz/get_sdf";

        readonly AssimpContext importer = new AssimpContext();
        readonly Dictionary<string, List<string>> packagePaths = new Dictionary<string, List<string>>();

        public int NumPackages => packagePaths.Count;

        public ModelServer()
        {
            Logger.Log("** Used package paths:");
            string packagePath = Environment.GetEnvironmentVariable("ROS_PACKAGE_PATH");
            if (packagePath is null)
            {
                Logger.LogError("EE Cannot retrieve environment variable ROS_PACKAGE_PATH");
            }
            else
            {
                string[] paths = packagePath.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string path in paths)
                {
                    string pathNormalized = path.Trim();
                    if (!Directory.Exists(pathNormalized))
                    {
                        Logger.Log("** Ignoring '" + pathNormalized + "'");
                        continue;
                    }

                    string folderName = new DirectoryInfo(pathNormalized).Name;
                    CheckPath(folderName, pathNormalized);
                }
            }

            if (packagePaths.Count == 0)
            {
                Logger.Log("EE Empty list of package paths. Nothing to do.");
            }
        }

        void CheckPath(string folderName, string path)
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

        void AddPath(string package, string path)
        {
            if (!packagePaths.TryGetValue(package, out List<string> paths))
            {
                paths = new List<string>();
                packagePaths[package] = paths;
            }

            paths.Add(path);
            Logger.Log("++ " + package);
        }

        string ResolvePath(Uri uri)
        {
            return ResolvePath(uri, out string _);
        }

        string ResolvePath(Uri uri, out string outPackagePath)
        {
            outPackagePath = null;

            string package = uri.Host;
            if (!packagePaths.TryGetValue(package, out List<string> paths))
            {
                Logger.LogError("EE Failed to find package '" + package + "'.");
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

                Logger.LogError("EE Rejecting resource request '" + uri + "' for path traversal.");
                return null;
            }

            Logger.LogError("EE Failed to find resource '" + uri + "'.");
            return null;
        }

        public void ModelCallback(GetModelResource msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }
            
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
 
            Logger.Log("** Requesting " + modelPath);

            Model model;
            try
            {
                model = LoadModel(modelPath);
                model.Filename = uri.ToString();
            }
            catch (AssimpException e)
            {
                Logger.LogError("EE Assimp exception loading '" + modelPath + "':");
                Logger.LogError(e);

                msg.Response.Success = false;
                msg.Response.Message = "Failed to load model";
                return;
            }

            msg.Response.Success = true;
            msg.Response.Message = "";
            msg.Response.Model = model;

            Logger.Log(">> " + uri);
        }

        public void TextureCallback(GetModelTexture msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }
            
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
                Logger.LogError("EE Failed to read '" + texturePath + "': " + e.Message);
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

            Logger.Log(">> " + uri);
        }

        Model LoadModel(string fileName)
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

            Assimp.Scene scene = importer.ImportFile(fileName,
                PostProcessPreset.TargetRealTimeMaximumQuality | PostProcessPreset.ConvertToLeftHanded);
            Model msg = new Model
            {
                Meshes = new Msgs.IvizMsgs.Mesh[scene.Meshes.Count],
                OrientationHint = orientationHint
            };

            List<Triangle> faces = new List<Triangle>();
            for (int i = 0; i < scene.MeshCount; i++)
            {
                Assimp.Mesh srcMesh = scene.Meshes[i];

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
                        default:
                            Logger.LogDebug("ModelService: Got mesh face with " + face.IndexCount + " vertices!");
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

                msg.Meshes[i] = dstMesh;
            }

            msg.Materials = new Msgs.IvizMsgs.Material[scene.MaterialCount];
            for (int i = 0; i < scene.MaterialCount; i++)
            {
                Assimp.Material srcMaterial = scene.Materials[i];
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
            ProcessNode(scene.RootNode, nodes, new Dictionary<Assimp.Node, int>());

            msg.Nodes = nodes.ToArray();

            return msg;
        }

        static void ProcessNode(Assimp.Node node, List<Msgs.IvizMsgs.Node> nodes, Dictionary<Assimp.Node, int> ids)
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

            foreach (Assimp.Node child in node.Children)
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
        
        public void FileCallback(GetFile msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }
            
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
                Logger.LogError("EE Failed to read '" + filePath + "': " + e.Message);
                msg.Response.Message = e.Message;
                return;
            }

            msg.Response.Success = true;
            msg.Response.Message = "";
            msg.Response.Bytes = data;

            Logger.Log(">> " + uri);
        }

        public void SdfCallback(GetSdf msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }
            
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
                Logger.LogError("EE Failed to find resource path for '" + modelPath + "'");
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
                Logger.LogError("EE Failed to read '" + modelPath + "': " + e.Message);
                msg.Response.Message = e.Message;
                return;
            }

            Dictionary<string, string> modelPaths = Sdf.SdfFile.CreateModelPaths(packagePath);

            Sdf.SdfFile file;
            try
            {
                file = Sdf.SdfFile.Create(data).ResolveIncludes(modelPaths);
            }
            catch (Exception e) when (e is IOException || e is Sdf.MalformedSdfException)
            {
                Logger.LogError("EE Failed to parse '" + modelPath + "': " + e.Message);
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

        static void ResolveIncludes(Sdf.SdfFile file, ICollection<Include> includes)
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

        static void ResolveIncludes(Sdf.World world, ICollection<Include> includes)
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

            foreach (Sdf.Link link in model.Links)
            {
                Matrix4x4 linkPose = Multiply(pose, ToPose(link.Pose));
                foreach (Sdf.Visual visual in link.Visuals)
                {
                    ResolveIncludes(visual, includes, linkPose);
                }
            }
            
            foreach (Sdf.Model innerModel in model.Models)
            {
                ResolveIncludes(innerModel, includes, pose);
            }
        }

        static void ResolveIncludes(Sdf.Visual visual, ICollection<Include> includes, in Matrix4x4 inPose)
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
                    Pose = ToMatrix(pose),
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
                    Pose = ToMatrix(pose),
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
                    Pose = ToMatrix(pose),

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
                    Pose = ToMatrix(pose),
                    Material = includeMaterial
                });                
            }
        }

        static Matrix4x4 ToPose(Sdf.Pose pose)
        {
            if (pose is null)
            {
                return Matrix4x4.Identity;
            }

            Matrix4x4 result = Matrix4x4.FromEulerAnglesXYZ(
                (float) -pose.Orientation.X,
                (float) -pose.Orientation.Y,
                (float) -pose.Orientation.Z
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

        bool disposed;
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            importer.Dispose();
        }
    }
}
