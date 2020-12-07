using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Roslib;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Displays
{
    public class ExternalResourceManager
    {
        const string ModelServiceName = "/iviz/get_model_resource";
        const string TextureServiceName = "/iviz/get_model_texture";
        const string FileServiceName = "/iviz/get_file";
        const string SceneServiceName = "/iviz/get_sdf";

        const string StrMissingFileRemoving = "ExternalResourceManager: Missing file '{0}'. Removing.";
        const string StrServiceFailedWithMessage = "ExternalResourceManager: Call Service failed with message '{0}'";

        const string StrCallServiceFailed =
            "ExternalResourceManager: Call Service failed! Are you sure iviz is connected and the Iviz.Model.Service program is running?";

        const string StrResourceFailedWithError = "ExternalResourceManager: Loading resource {0} failed with error {1}";

        [DataContract]
        public class ResourceFiles
        {
            [DataMember] int Version { get; set; }
            [DataMember] public Dictionary<string, string> Models { get; set; }
            [DataMember] public Dictionary<string, string> Textures { get; set; }
            [DataMember] public Dictionary<string, string> Scenes { get; set; }
            [DataMember] public Dictionary<string, string> RobotDescriptions { get; set; }

            public ResourceFiles()
            {
                Models = new Dictionary<string, string>();
                Textures = new Dictionary<string, string>();
                Scenes = new Dictionary<string, string>();
                RobotDescriptions = new Dictionary<string, string>();
            }
        }

        readonly ResourceFiles resourceFiles = new ResourceFiles();

        readonly Dictionary<string, Info<GameObject>> loadedModels =
            new Dictionary<string, Info<GameObject>>();

        readonly Dictionary<string, Info<Texture2D>> loadedTextures =
            new Dictionary<string, Info<Texture2D>>();

        readonly Dictionary<string, Info<GameObject>> loadedScenes =
            new Dictionary<string, Info<GameObject>>();

        readonly Dictionary<string, float> temporaryBlacklist = new Dictionary<string, float>();

        public GameObject Node { get; }

        readonly Model modelGenerator = new Model();
        readonly Scene sceneGenerator = new Scene();

        [NotNull]
        public ReadOnlyCollection<string> GetListOfModels() =>
            new ReadOnlyCollection<string>(resourceFiles.Models.Keys.ToList());

        public ExternalResourceManager(bool createNode = true)
        {
            if (createNode)
            {
                Node = new GameObject("External Resources");
                Node.SetActive(false);
            }

            try
            {
                Directory.CreateDirectory(Settings.ResourcesPath);
                Directory.CreateDirectory(Settings.SavedRobotsPath);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            if (!File.Exists(Settings.ResourcesFilePath))
            {
                Logger.Debug("ExternalResourceManager: Failed to find file " + Settings.ResourcesFilePath);
                return;
            }

            Logger.Debug("ExternalResourceManager: Using resource file " + Settings.ResourcesFilePath);

            try
            {
                string text = File.ReadAllText(Settings.ResourcesFilePath);
                resourceFiles = JsonConvert.DeserializeObject<ResourceFiles>(text);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        void WriteResourceFile()
        {
            try
            {
                File.WriteAllText(Settings.ResourcesFilePath,
                    JsonConvert.SerializeObject(resourceFiles, Formatting.Indented));
            }
            catch (IOException e)
            {
                Logger.Warn("ExternalResourceManager: Failed to write resource file! " + e);
            }
        }

        [NotNull]
        public static string SanitizeForFilename([NotNull] string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return Uri.EscapeDataString(input);
        }

        [NotNull]
        public IEnumerable<string> GetRobotNames() => resourceFiles.RobotDescriptions.Keys.ToArray();

        public bool ContainsRobot([NotNull] string robotName)
        {
            if (robotName == null)
            {
                throw new ArgumentNullException(nameof(robotName));
            }

            return resourceFiles.RobotDescriptions.ContainsKey(robotName);
        }

        [ContractAnnotation("=> false, robotDescription:null; => true, robotDescription:notnull")]
        public bool TryGetRobot([NotNull] string robotName, out string robotDescription)
        {
            if (string.IsNullOrEmpty(robotName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotName));
            }

            if (!resourceFiles.RobotDescriptions.TryGetValue(robotName, out string localPath))
            {
                robotDescription = null;
                return false;
            }

            string absolutePath = $"{Settings.SavedRobotsPath}/{localPath}";
            if (!File.Exists(absolutePath))
            {
                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.RobotDescriptions.Remove(robotName);
                WriteResourceFile();
                robotDescription = null;
                return false;
            }

            try
            {
                robotDescription = File.ReadAllText(absolutePath);
                return true;
            }
            catch (IOException e)
            {
                Logger.Debug("ExternalResourceManager: Failed to read robot '" + robotName + "' : " + e);
                robotDescription = null;
                return false;
            }
        }

        public void AddRobotResource([NotNull] string robotName, [NotNull] string robotDescription)
        {
            if (string.IsNullOrEmpty(robotName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotName));
            }

            if (string.IsNullOrEmpty(robotDescription))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotDescription));
            }

            string localPath = SanitizeForFilename(robotName);

            File.WriteAllText($"{Settings.SavedRobotsPath}/{localPath}", robotDescription);
            Logger.Debug($"Saving to {Settings.SavedRobotsPath}/{localPath}");

            resourceFiles.RobotDescriptions[robotName] = localPath;
            WriteResourceFile();
        }

        public void RemoveRobotResource([NotNull] string robotName)
        {
            if (string.IsNullOrEmpty(robotName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotName));
            }

            if (!resourceFiles.RobotDescriptions.TryGetValue(robotName, out string localPath))
            {
                return;
            }

            string absolutePath = $"{Settings.ResourcesPath}/{localPath}";
            try
            {
                File.Delete(absolutePath);
                Logger.Debug($"Removing '{localPath}'");
            }
            catch (IOException)
            {
                Logger.Warn("ExternalResourceManager: Failed to delete robot file '" + localPath + "'");
            }

            resourceFiles.RobotDescriptions.Remove(robotName);
            WriteResourceFile();
        }

        [ContractAnnotation("=> false, resource:null; => true, resource:notnull")]
        public bool TryGet([NotNull] string uriString, out Info<GameObject> resource,
            [CanBeNull] IExternalServiceProvider provider)
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            float currentTime = Time.time;
            if (temporaryBlacklist.TryGetValue(uriString, out float insertionTime))
            {
                if (currentTime < insertionTime + 30)
                {
                    resource = null;
                    return false;
                }

                temporaryBlacklist.Remove(uriString);
            }

            
            if (!Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                Logger.Warn($"[ExternalResourceManager]: Uri '{uriString}' is not a valid uri!");
                temporaryBlacklist.Add(uriString, float.MaxValue);
                resource = null;
                return false;
            }            
            
            //Logger.Debug("ExternalResourceManager: Requesting " + uri);

            string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
            string fileType = Path.GetExtension(uriPath).ToUpperInvariant();

            if (fileType == ".SDF" || fileType == ".WORLD")
            {
                resource = TryGetScene(uriString, provider);
            }
            else
            {
                resource = TryGetModel(uriString, provider);
            }

            if (resource == null)
            {
                temporaryBlacklist.Add(uriString, currentTime);
                //Logger.Debug("ExternalResourceManager: Resource is null!");
            }

            return resource != null;
        }

        Info<GameObject> TryGetModel([NotNull] string uriString, [CanBeNull] IExternalServiceProvider provider)
        {
            if (loadedModels.TryGetValue(uriString, out Info<GameObject> resource))
            {
                return resource;
            }

            if (resourceFiles.Models.TryGetValue(uriString, out string localPath))
            {
                if (File.Exists($"{Settings.ResourcesPath}/{localPath}"))
                {
                    return LoadLocalModel(uriString, localPath, provider);
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Models.Remove(uriString);
                WriteResourceFile();
            }

            GetModelResource msg = new GetModelResource
            {
                Request =
                {
                    Uri = uriString
                }
            };

            if (provider != null &&
                provider.CallService(ModelServiceName, msg) &&
                msg.Response.Success)
            {
                return ProcessModelResponse(uriString, msg.Response, provider);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                //Debug.LogWarningFormat(StrServiceFailedWithMessage, msg.Response.Message);
            }
            else
            {
                //Logger.Debug(StrCallServiceFailed);
            }

            return null;
        }

        Info<GameObject> TryGetScene([NotNull] string uriString, [CanBeNull] IExternalServiceProvider provider)
        {
            if (loadedScenes.TryGetValue(uriString, out Info<GameObject> resource))
            {
                return resource;
            }

            if (resourceFiles.Scenes.TryGetValue(uriString, out string localPath))
            {
                if (File.Exists($"{Settings.ResourcesPath}/{localPath}"))
                {
                    return LoadLocalScene(uriString, localPath, provider);
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Scenes.Remove(uriString);
                WriteResourceFile();
            }

            GetSdf msg = new GetSdf
            {
                Request =
                {
                    Uri = uriString
                }
            };

            if (provider != null &&
                provider.CallService(SceneServiceName, msg) &&
                msg.Response.Success)
            {
                return ProcessSceneResponse(uriString, msg.Response, provider);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                Debug.LogWarningFormat(StrServiceFailedWithMessage, msg.Response.Message);
            }
            else
            {
                Logger.Debug(StrCallServiceFailed);
            }

            return null;
        }

        [ContractAnnotation("=> false, resource:null; => true, resource:notnull")]
        public bool TryGet([NotNull] string uriString, out Info<Texture2D> resource,
            [CanBeNull] IExternalServiceProvider provider)
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            float currentTime = Time.time;
            if (temporaryBlacklist.TryGetValue(uriString, out float insertionTime))
            {
                if (currentTime < insertionTime + 30)
                {
                    resource = null;
                    return false;
                }

                temporaryBlacklist.Remove(uriString);
            }            
            
            if (loadedTextures.TryGetValue(uriString, out resource))
            {
                return true;
            }

            if (resourceFiles.Textures.TryGetValue(uriString, out string localPath))
            {
                if (File.Exists($"{Settings.ResourcesPath}/{localPath}"))
                {
                    resource = LoadLocalTexture(uriString, localPath);
                    return resource != null;
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Textures.Remove(uriString);
                WriteResourceFile();
            }

            GetModelTexture msg = new GetModelTexture()
            {
                Request =
                {
                    Uri = uriString
                }
            };

            if (provider != null &&
                provider.CallService(TextureServiceName, msg) &&
                msg.Response.Success)
            {
                resource = ProcessTextureResponse(uriString, msg.Response);
                return resource != null;
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                Debug.LogWarningFormat(StrServiceFailedWithMessage, msg.Response.Message);
            }
            else
            {
                Logger.Debug(StrCallServiceFailed);
            }

            temporaryBlacklist.Add(uriString, currentTime);
            return false;
        }

        [CanBeNull]
        Info<GameObject> LoadLocalModel([NotNull] string uriString, [NotNull] string localPath,
            [CanBeNull] IExternalServiceProvider provider)
        {
            byte[] buffer;

            try
            {
                buffer = File.ReadAllBytes($"{Settings.ResourcesPath}/{localPath}");
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat(StrResourceFailedWithError, uriString, e);
                return null;
            }

            Model msg = Msgs.Buffer.Deserialize(modelGenerator, buffer, buffer.Length);
            GameObject obj = CreateModelObject(uriString, msg, provider);

            Info<GameObject> resource = new Info<GameObject>(uriString, obj);
            loadedModels[uriString] = resource;

            return resource;
        }

        [CanBeNull]
        Info<Texture2D> LoadLocalTexture([NotNull] string uriString, [NotNull] string localPath)
        {
            byte[] buffer;

            try
            {
                buffer = File.ReadAllBytes($"{Settings.ResourcesPath}/{localPath}");
            }
            catch (Exception e)
            {
                Logger.Debug(e);
                return null;
            }

            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            texture.LoadImage(buffer);
            if (!UnityEngine.Application.isEditor)
            {
                texture.Compress(true);
            }

            texture.name = uriString;

            Info<Texture2D> resource = new Info<Texture2D>(uriString, texture);
            loadedTextures[uriString] = resource;

            return resource;
        }

        [CanBeNull]
        Info<GameObject> LoadLocalScene([NotNull] string uriString, [NotNull] string localPath,
            [CanBeNull] IExternalServiceProvider provider)
        {
            byte[] buffer;

            try
            {
                buffer = File.ReadAllBytes($"{Settings.ResourcesPath}/{localPath}");
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat(StrResourceFailedWithError, uriString, e);
                return null;
            }

            Scene msg = Msgs.Buffer.Deserialize(sceneGenerator, buffer, buffer.Length);
            GameObject obj = CreateSceneNode(msg, provider);

            Info<GameObject> resource = new Info<GameObject>(uriString, obj);
            loadedScenes[uriString] = resource;

            return resource;
        }

        [CanBeNull]
        Info<GameObject> ProcessModelResponse([NotNull] string uriString, [NotNull] GetModelResourceResponse msg,
            [CanBeNull] IExternalServiceProvider provider)
        {
            try
            {
                GameObject obj = CreateModelObject(uriString, msg.Model, provider);

                Info<GameObject> info = new Info<GameObject>(uriString, obj);
                loadedModels[uriString] = info;

                string localPath = SanitizeForFilename(uriString);

                byte[] buffer = new byte[msg.Model.RosMessageLength];
                Msgs.Buffer.Serialize(msg.Model, buffer);
                File.WriteAllBytes($"{Settings.ResourcesPath}/{localPath}", buffer);
                Logger.Debug($"Saving to {Settings.ResourcesPath}/{localPath}");
                //Logger.Internal($"Added external model <i>{uri}</i>");

                resourceFiles.Models[uriString] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        [CanBeNull]
        Info<Texture2D> ProcessTextureResponse([NotNull] string uriString, [NotNull] GetModelTextureResponse msg)
        {
            try
            {
                Texture2D texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
                texture.LoadImage(msg.Image.Data);
                texture.name = uriString;

                Info<Texture2D> info = new Info<Texture2D>(uriString, texture);
                loadedTextures[uriString] = info;

                string localPath = SanitizeForFilename(uriString);

                byte[] buffer = msg.Image.Data;
                File.WriteAllBytes($"{Settings.ResourcesPath}/{localPath}", buffer);
                Logger.Debug($"Saving to {Settings.ResourcesPath}/{localPath}");
                //Logger.Internal($"Added external texture <i>{uri}</i>");

                resourceFiles.Textures[uriString] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        [CanBeNull]
        Info<GameObject> ProcessSceneResponse([NotNull] string uriString, [NotNull] GetSdfResponse msg,
            [CanBeNull] IExternalServiceProvider provider)
        {
            try
            {
                GameObject node = CreateSceneNode(msg.Scene, provider);

                Info<GameObject> info = new Info<GameObject>(uriString, node);

                loadedScenes[uriString] = info;

                string localPath = SanitizeForFilename(uriString);

                byte[] buffer = new byte[msg.Scene.RosMessageLength];
                Msgs.Buffer.Serialize(msg.Scene, buffer);
                File.WriteAllBytes($"{Settings.ResourcesPath}/{localPath}", buffer);
                Logger.Debug($"Saving to {Settings.ResourcesPath}/{localPath}");
                //Logger.Internal($"Added external scene <i>{uri}</i>");

                resourceFiles.Scenes[uriString] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        [NotNull]
        GameObject CreateModelObject([NotNull] string uriString, [NotNull] Model msg,
            [CanBeNull] IExternalServiceProvider provider)
        {
            GameObject model = SceneModel.Create(uriString, msg, provider).gameObject;
            if (Node != null)
            {
                model.transform.SetParent(Node.transform, false);
            }

            return model;
        }

        [NotNull]
        GameObject CreateSceneNode([NotNull] Scene scene, [CanBeNull] IExternalServiceProvider provider)
        {
            GameObject node = new GameObject(scene.Name);
            if (Node != null)
            {
                node.transform.SetParent(Node?.transform, false);
            }

            //Logger.Debug(scene.ToJsonString());

            foreach (Include include in scene.Includes)
            {
                GameObject child = new GameObject("Include");

                Matrix4x4 m = new Matrix4x4();
                for (int i = 0; i < 16; i++)
                {
                    m[i] = include.Pose.M[i];
                }

                child.transform.SetParent(node.transform, false);
                child.transform.localRotation = m.rotation.Ros2Unity();
                child.transform.localPosition = ((UnityEngine.Vector3) m.GetColumn(3)).Ros2Unity();
                child.transform.localScale = m.lossyScale;

                if (!Resource.TryGetResource(include.Uri, out Info<GameObject> includeResource, provider))
                {
                    Logger.Debug("ExternalResourceManager: Failed to retrieve resource '" + include.Uri + "'");
                    continue;
                }

                includeResource.Instantiate(child.transform);
            }

            return node;
        }
    }
}