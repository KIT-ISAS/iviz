#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Assimp;
using Assimp.Configs;
using Assimp.Unmanaged;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Tools;
using Model = Iviz.Msgs.IvizMsgs.Model;
using Uri = System.Uri;

namespace Iviz.ModelService;

public sealed class ModelServer : IDisposable
{
    public const string ModelServiceName = "/iviz/get_model_resource";
    public const string TextureServiceName = "/iviz/get_model_texture";
    public const string FileServiceName = "/iviz/get_file";
    public const string SdfServiceName = "/iviz/get_sdf";

    readonly bool verbose;
    readonly AssimpContext importer = new();
    readonly Dictionary<string, HashSet<string>> packagePaths = new();
    bool disposed;

    public int NumPackages => packagePaths.Count;
    public bool IsFileSchemaEnabled { get; set; }

    static void LogError(string msg)
    {
        Logger.LogError(msg);
        Console.Error.WriteLine("EE " + msg);
    }

    static void Log(string msg)
    {
        Logger.Log(msg);
        Console.WriteLine("** " + msg);
    }

    static void LogUp(Uri uri)
    {
        Console.WriteLine(">> " + uri);
    }

    public ModelServer(string? additionalPaths = null, bool enableFileSchema = false, bool verbose = false)
    {
        this.verbose = verbose;
        string? packagePath = Environment.GetEnvironmentVariable("ROS_PACKAGE_PATH");
        if (packagePath is null && additionalPaths is null)
        {
            LogError("Environment variable ROS_PACKAGE_PATH is not set, and no other source of packages was provided.");
            return;
        }

        var paths = new List<string>();

        if (verbose)
        {
            Log("Adding the following package paths:");
        }

        if (packagePath != null)
        {
            string[] newPaths = packagePath.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (verbose)
            {
                foreach (string path in newPaths)
                {
                    Log("    " + path);
                }
            }

            paths.AddRange(newPaths);
        }

        if (additionalPaths != null)
        {
            if (verbose)
            {
                Log("Adding additional package paths:");
            }

            string[] newPaths = additionalPaths.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (verbose)
            {
                foreach (string path in newPaths)
                {
                    Log("    " + path);
                }
            }

            paths.AddRange(newPaths);
        }

        if (verbose)
        {
            Log("** Resolving subfolders:");
        }

        foreach (string path in paths)
        {
            string pathNormalized = path.Trim();
            if (!Directory.Exists(pathNormalized))
            {
                continue;
            }

            string folderName = new DirectoryInfo(pathNormalized).Name;
            CheckPath(folderName, pathNormalized);
        }

        if (packagePaths.Count == 0)
        {
            LogError("No packages were found in the given paths were found.");
        }

        IsFileSchemaEnabled = enableFileSchema;
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
        if (!packagePaths.TryGetValue(package, out var paths))
        {
            paths = new HashSet<string>();
            packagePaths[package] = paths;
        }

        if (verbose)
        {
            Log("    " + path);
        }

        paths.Add(path);
    }

    string? ResolvePath(Uri uri)
    {
        return ResolvePath(uri, out _);
    }

    string? ResolvePath(Uri uri, out string? outPackagePath)
    {
        string package = uri.Host;
        if (!packagePaths.TryGetValue(package, out var paths))
        {
            LogError($"Failed to resolve uri '{uri}'. Reason: Package '{package}' not found.");

            outPackagePath = null;
            return null;
        }

        string subPath = Uri.UnescapeDataString(uri.AbsolutePath);

        bool errorMessageShown = false;
        foreach (string packagePath in paths)
        {
            string path = packagePath + "/" + subPath;

            if (!File.Exists(path))
            {
                LogError($"Failed to resolve uri '{uri}'. Reason: File '{path}' does not exist.");
                errorMessageShown = true;
                continue;
            }

            if (Path.GetFullPath(path).StartsWith(packagePath, false, BuiltIns.Culture))
            {
                outPackagePath = packagePath;
                return path;
            }

            LogError($"Failed to resolve uri '{uri}'. Reason: Resolution requires path traversal.");

            outPackagePath = null;
            return null;
        }

        if (!errorMessageShown)
        {
            LogError($"Failed to resolve uri '{uri}'.");
        }

        outPackagePath = null;
        return null;
    }

    public void ModelCallback(GetModelResource msg)
    {
        if (msg == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(msg));
        }

        if (!Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out var uri))
        {
            msg.Response.Success = false;
            msg.Response.Message = "Failed to parse uri from requested string";
            LogError($"Failed to resolve uri '{msg.Request.Uri}'. Reason: Invalid uri.");
            return;
        }

        LogUp(uri);

        string? modelPath;
        switch (uri.Scheme)
        {
            case "package":
            {
                modelPath = ResolvePath(uri);
                if (modelPath is null)
                {
                    msg.Response.Success = false;
                    msg.Response.Message = "Could not find resource with the given path";
                    return;
                }

                break;
            }
            case "file" when !IsFileSchemaEnabled:
                msg.Response.Success = false;
                msg.Response.Message = "File schema is disabled";
                LogError($"Failed to resolve uri '{uri}'. Reason: File scheme is disabled.");
                return;
            case "file":
            {
                modelPath = Uri.UnescapeDataString(uri.AbsolutePath);
                if (!File.Exists(modelPath))
                {
                    msg.Response.Success = false;
                    msg.Response.Message = $"File '{modelPath}' does not exist";
                    LogError($"Failed to resolve uri '{uri}'. Reason: File '{modelPath}' does not exist.");
                    return;
                }

                break;
            }
            default:
                msg.Response.Success = false;
                msg.Response.Message = "Only 'package' or 'file' scheme is supported";
                LogError($"Failed to resolve uri '{uri}'. Reason: Unknown scheme '{uri.Scheme}'.");
                return;
        }

        Model model;
        try
        {
            model = LoadModel(modelPath);
            model.Filename = uri.ToString();
        }
        catch (XmlException e)
        {
            LogError($"Failed to access uri '{uri}'. Reason: XML error while reading '{modelPath}'. {e.Message}");
            msg.Response.Success = false;
            msg.Response.Message = "Failed to load model. Reason: XML error. " + e.Message;
            return;
        }
        catch (AssimpException e)
        {
            LogError($"Failed to access uri '{uri}'. Reason: Failed to read path '{modelPath}'. {e.Message}");

            msg.Response.Success = false;
            msg.Response.Message = "Failed to load model. Reason: Assimp error. " + e.Message;
            return;
        }

        msg.Response.Success = true;
        msg.Response.Message = "";
        msg.Response.Model = model;
    }

    public void TextureCallback(GetModelTexture msg)
    {
        if (msg == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(msg));
        }

        // TODO: force conversion to either png or jpg

        if (!Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out var uri))
        {
            msg.Response.Success = false;
            msg.Response.Message = "Failed to parse uri from requested string";
            LogError($"Failed to resolve uri '{msg.Request.Uri}'. Reason: Invalid uri.");
            return;
        }

        LogUp(uri);

        string? texturePath;
        switch (uri.Scheme)
        {
            case "package":
            {
                texturePath = ResolvePath(uri);
                if (string.IsNullOrWhiteSpace(texturePath))
                {
                    msg.Response.Success = false;
                    msg.Response.Message = "Could not find resource with the given path";
                    return;
                }

                break;
            }
            case "file" when !IsFileSchemaEnabled:
                msg.Response.Success = false;
                msg.Response.Message = "File schema is disabled";
                LogError($"Failed to resolve uri '{uri}'. Reason: File scheme is disabled.");
                return;
            case "file":
            {
                texturePath = Uri.UnescapeDataString(uri.AbsolutePath);
                if (!File.Exists(texturePath))
                {
                    msg.Response.Success = false;
                    msg.Response.Message = $"File '{texturePath}' does not exist";
                    LogError($"Failed to resolve uri '{uri}'. Reason: File '{texturePath}' does not exist.");
                    return;
                }

                break;
            }
            default:
                msg.Response.Success = false;
                msg.Response.Message = "Only 'package' or 'file' scheme is supported";
                LogError($"Failed to resolve uri '{uri}'. Reason: Unknown scheme '{uri.Scheme}'.");
                return;
        }

        byte[] data;

        try
        {
            data = File.ReadAllBytes(texturePath);
        }
        catch (IOException e)
        {
            LogError($"Failed to resolve uri '{uri}'. Reason: Failed to read path '{texturePath}'. {e.Message}");
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
    }

    Model LoadModel(string fileName)
    {
        string orientationHint = "";
        if (fileName.EndsWith(".DAE", true, BuiltIns.Culture))
        {
            var doc = new XmlDocument();
            doc.Load(fileName);
            var nodeList = doc.GetElementsByTagName("up_axis");
            if (nodeList.Count != 0 && nodeList[0] is { } node)
            {
                orientationHint = node.InnerText ?? "";
            }
        }
        
        importer.SetConfig(new IntegerPropertyConfig(
            AiConfigs.AI_CONFIG_PP_RVC_FLAGS,
            (int)ExcludeComponent.Normals
        ));
        
        importer.SetConfig(new NormalSmoothingAngleConfig(60));

        var scene = importer.ImportFile(fileName,
            PostProcessSteps.RemoveComponent |
            PostProcessPreset.TargetRealTimeMaximumQuality |
            PostProcessPreset.ConvertToLeftHanded |
            PostProcessSteps.PreTransformVertices);
        
        var msg = new Model
        {
            Meshes = new ModelMesh[scene.Meshes.Count],
            OrientationHint = orientationHint
        };

        var faces = new List<Triangle>();
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
                            (uint)face.Indices[0],
                            (uint)face.Indices[1],
                            (uint)face.Indices[2]
                        ));
                        break;
                    case 4:
                        faces.Add(new Triangle(
                            (uint)face.Indices[0],
                            (uint)face.Indices[1],
                            (uint)face.Indices[2]
                        ));
                        faces.Add(new Triangle(
                            (uint)face.Indices[0],
                            (uint)face.Indices[2],
                            (uint)face.Indices[3]
                        ));
                        break;
                    default:
                        Logger.LogDebugFormat("{0}: Got mesh face with {1} vertices!", this, face.IndexCount);
                        break;
                }
            }

            var dstMesh = new ModelMesh
            {
                Name = srcMesh.Name ?? "[mesh]",
                Vertices = srcMesh.Vertices.Select(ToVector3).ToArray(),
                Normals = srcMesh.Normals.Select(ToVector3).ToArray(),
                Tangents = srcMesh.Tangents.Select(ToVector3).ToArray(),
                BiTangents = srcMesh.BiTangents.Select(ToVector3).ToArray(),
                TexCoords = srcMesh.TextureCoordinateChannels.Select(ToTexCoords).ToArray(),
                ColorChannels = srcMesh.VertexColorChannels.Select(ToColorChannel).ToArray(),
                Faces = faces.ToArray(),
                MaterialIndex = (uint)srcMesh.MaterialIndex,
            };

            msg.Meshes[i] = dstMesh;
        }

        msg.Materials = new ModelMaterial[scene.MaterialCount];

        for (int i = 0; i < scene.MaterialCount; i++)
        {
            Material srcMaterial = scene.Materials[i];
            msg.Materials[i] = new ModelMaterial
            {
                Name = srcMaterial.Name ?? "[material]",
                Ambient = ToColor(srcMaterial.ColorAmbient),
                Diffuse = ToColor(srcMaterial.ColorDiffuse),
                Emissive = ToColor(srcMaterial.ColorEmissive),
                Opacity = srcMaterial.Opacity,
                BumpScaling = srcMaterial.BumpScaling,
                Shininess = srcMaterial.Shininess,
                ShininessStrength = srcMaterial.ShininessStrength,
                Reflectivity = srcMaterial.Reflectivity,
                BlendMode = (byte)srcMaterial.BlendMode,
                Textures = srcMaterial.GetAllMaterialTextures().Select(ToTexture).ToArray()
            };
        }

        var nodes = new List<ModelNode>();
        ProcessNode(scene.RootNode, nodes, new Dictionary<Node, int>());

        msg.Nodes = nodes.ToArray();

        return msg;
    }

    static void ProcessNode(Node node, List<ModelNode> nodes, Dictionary<Node, int> ids)
    {
        if (node.Children.Count == 0 && node.MeshIndices.Count == 0)
        {
            return;
        }

        ids[node] = ids.Count;
        int parentId = node.Parent is null ? -1 : ids[node.Parent];

        nodes.Add(new ModelNode(
            node.Name,
            parentId,
            ToMatrix(node.Transform),
            node.MeshIndices.ToArray()
        ));

        foreach (var child in node.Children)
        {
            ProcessNode(child, nodes, ids);
        }
    }

    static Point32 ToVector3(Vector3D v) => new(v.X, v.Y, v.Z);

    static Point32 ToVector3(Sdf.Vector3d v) => new((float)v.X, (float)v.Y, (float)v.Z);

    static Point32 ToVector3UV(Vector3D v) => new(v.X, 1 - v.Y, v.Z);

    static Color32 ToColor(Color4D color)
    {
        int r = (int)(Math.Max(Math.Min(color.R, 1), 0) * 255);
        int g = (int)(Math.Max(Math.Min(color.G, 1), 0) * 255);
        int b = (int)(Math.Max(Math.Min(color.B, 1), 0) * 255);
        int a = (int)(Math.Max(Math.Min(color.A, 1), 0) * 255);
        return new Color32((byte)r, (byte)g, (byte)b, (byte)a);
    }

    static ModelColorChannel ToColorChannel(List<Color4D> colorChannel) => new(colorChannel.Select(ToColor).ToArray());

    static ModelTexCoords ToTexCoords(List<Vector3D> texCoords) => new(texCoords.Select(ToVector3UV).ToArray());

    static ModelTexture ToTexture(TextureSlot texture)
    {
        return new ModelTexture
        {
            Path = texture.FilePath,
            Index = texture.TextureIndex,
            BlendFactor = texture.BlendFactor,
            Mapping = (byte)texture.Mapping,
            Operation = (byte)texture.Operation,
            Type = (byte)texture.TextureType,
            UvIndex = texture.UVIndex,
            WrapModeU = (byte)texture.WrapModeU,
            WrapModeV = (byte)texture.WrapModeV
        };
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
            BuiltIns.ThrowArgumentNull(nameof(msg));
        }

        if (!Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out var uri))
        {
            msg.Response.Message = "Failed to parse uri from requested string";
            return;
        }

        string? filePath = ResolvePath(uri);
        if (string.IsNullOrWhiteSpace(filePath))
        {
            msg.Response.Message = "Could not find resource with the given path";
            return;
        }

        byte[] data;
        try
        {
            data = File.ReadAllBytes(filePath);
        }
        catch (IOException e)
        {
            LogError("Failed to read '" + filePath + "'. Reason: " + e.Message);
            msg.Response.Message = e.Message;
            return;
        }

        msg.Response.Success = true;
        msg.Response.Message = "";
        msg.Response.Bytes = data;

        LogUp(uri);
    }

    public void SdfCallback(GetSdf msg)
    {
        if (msg == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(msg));
        }

        if (!Uri.TryCreate(msg.Request.Uri, UriKind.Absolute, out var uri))
        {
            msg.Response.Message = "Failed to parse uri from requested string";
            return;
        }

        if (uri.Scheme != "package")
        {
            msg.Response.Message = "Only 'package' scheme is supported";
            return;
        }

        string? modelPath = ResolvePath(uri, out string? packagePath);
        if (string.IsNullOrWhiteSpace(modelPath) || string.IsNullOrWhiteSpace(packagePath))
        {
            LogError("Failed to find resource path for '" + modelPath + "'");
            msg.Response.Message = "Could not find resource with the given path";
            return;
        }

        string data;
        try
        {
            data = File.ReadAllText(modelPath);
        }
        catch (IOException e)
        {
            LogError("Failed to read '" + modelPath + "'. Reason: " + e.Message);
            msg.Response.Message = e.Message;
            return;
        }

        Dictionary<string, string> modelPaths = Sdf.SdfFile.CreateModelPaths(packagePath);

        Sdf.SdfFile file;
        try
        {
            var srcFile = Sdf.SdfFile.CreateFromXml(data);
            file = srcFile.ResolveIncludes(modelPaths);
        }
        catch (Exception e) when (e is IOException or XmlException or Sdf.MalformedSdfException)
        {
            LogError("Failed to parse '" + modelPath + "'. Reason: " + e.Message);
            msg.Response.Message = e.Message;
            return;
        }

        List<SceneInclude> includes = new List<SceneInclude>();
        ResolveIncludes(file, includes);

        msg.Response.Success = true;
        msg.Response.Scene = new Msgs.IvizMsgs.Scene
        {
            Name = file.Worlds.Count != 0 && file.Worlds[0].Name is { } worldName ? worldName : "sdf",
            Filename = uri.ToString(),
            Includes = includes.ToArray(),
            Lights = file.Lights.Select(ToLight).ToArray()
        };

        LogUp(uri);
    }

    static SceneLight ToLight(Sdf.Light light)
    {
        return new SceneLight
        {
            Name = light.Name ?? "",
            Type = (byte)light.Type,
            CastShadows = light.CastShadows,
            Diffuse = ToColor(light.Diffuse),
            Range = 0,
            Position = ToVector3(light.Pose.Position),
            Direction = ToVector3(light.Direction),
            InnerAngle = (float)light.Spot.InnerAngle,
            OuterAngle = (float)light.Spot.OuterAngle
        };
    }

    static void ResolveIncludes(Sdf.SdfFile file, ICollection<SceneInclude> includes)
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

    static void ResolveIncludes(Sdf.World world, ICollection<SceneInclude> includes)
    {
        foreach (Sdf.Model model in world.Models)
        {
            ResolveIncludes(model, includes, Matrix4x4.Identity);
        }
    }

    static void ResolveIncludes(Sdf.Model model, ICollection<SceneInclude> includes, in Matrix4x4 inPose)
    {
        if (model.IsInvalid)
        {
            return;
        }

        var pose = Multiply(inPose, Multiply(ToPose(model.IncludePose), ToPose(model.Pose)));

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

    static void ResolveIncludes(Sdf.Visual visual, ICollection<SceneInclude> includes, in Matrix4x4 inPose)
    {
        if (visual.Geometry.Empty != null)
        {
            return;
        }

        Matrix4x4 pose = Multiply(inPose, ToPose(visual.Pose));

        ModelMaterial includeMaterial;
        if (visual.Material != null)
        {
            includeMaterial = new ModelMaterial
            {
                Name = visual.Name + "_material",
                Diffuse = ToColor(visual.Material.Diffuse),
                Emissive = ToColor(visual.Material.Emissive),
            };
        }
        else
        {
            includeMaterial = new ModelMaterial();
        }

        if (visual.Geometry.Box != null)
        {
            Vector3D diag = new(
                (float)visual.Geometry.Box.Scale.X,
                (float)visual.Geometry.Box.Scale.Y,
                (float)visual.Geometry.Box.Scale.Z
            );
            pose = Multiply(pose, Matrix4x4.FromScaling(diag));

            includes.Add(new SceneInclude
            {
                Uri = "package://iviz_internal/cube",
                Pose = ToMatrix(pose),
                Material = includeMaterial
            });
        }
        else if (visual.Geometry.Cylinder != null)
        {
            Vector3D diag = new(
                (float)visual.Geometry.Cylinder.Radius,
                (float)visual.Geometry.Cylinder.Radius,
                (float)visual.Geometry.Cylinder.Length
            );
            pose = Multiply(pose, Matrix4x4.FromScaling(diag));

            includes.Add(new SceneInclude
            {
                Uri = "package://iviz_internal/cylinder",
                Pose = ToMatrix(pose),
                Material = includeMaterial
            });
        }
        else if (visual.Geometry.Sphere != null)
        {
            Vector3D diag = new((float)visual.Geometry.Sphere.Radius);
            pose = Multiply(pose, Matrix4x4.FromScaling(diag));

            includes.Add(new SceneInclude
            {
                Uri = "package://iviz_internal/cylinder",
                Pose = ToMatrix(pose),
            });
        }
        else if (visual.Geometry.Mesh != null)
        {
            Vector3D diag = new(
                (float)visual.Geometry.Mesh.Scale.X,
                (float)visual.Geometry.Mesh.Scale.Y,
                (float)visual.Geometry.Mesh.Scale.Z
            );
            pose = Multiply(pose, Matrix4x4.FromScaling(diag));

            includes.Add(new SceneInclude
            {
                Uri = visual.Geometry.Mesh.Uri.Value,
                Pose = ToMatrix(pose),
                Material = includeMaterial
            });
        }
    }

    static Matrix4x4 ToPose(Sdf.Pose? pose)
    {
        if (pose is null)
        {
            return Matrix4x4.Identity;
        }

        Matrix4x4 result = Matrix4x4.FromEulerAnglesXYZ(
            (float)-pose.Orientation.X,
            (float)-pose.Orientation.Y,
            (float)-pose.Orientation.Z
        );

        result.A4 = (float)pose.Position.X;
        result.B4 = (float)pose.Position.Y;
        result.C4 = (float)pose.Position.Z;

        return result;
    }

    static Matrix4x4 Multiply(in Matrix4x4 a, in Matrix4x4 b)
    {
        return b * a; // assimp inverts natural order of multiplication!
    }


    static Color32 ToColor(Sdf.Color color)
    {
        int r = (int)(Math.Max(Math.Min(color.R, 1), 0) * 255);
        int g = (int)(Math.Max(Math.Min(color.G, 1), 0) * 255);
        int b = (int)(Math.Max(Math.Min(color.B, 1), 0) * 255);

        int a = (int)(Math.Max(Math.Min(color.A, 1), 0) * 255);
        return new Color32((byte)r, (byte)g, (byte)b, (byte)a);
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        importer.Dispose();
    }
}