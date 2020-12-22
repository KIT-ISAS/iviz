using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Roslib;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Nito.AsyncEx;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Displays
{
    public interface IExternalResourceListener
    {
        void OnResourceArrived(Info<GameObject> resource);
    }

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

        readonly AsyncLock mutex = new AsyncLock();


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

        #region RobotStuff

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

        #endregion

        [ItemCanBeNull]
        public async Task<Info<GameObject>> TryGetGameObjectAsync([NotNull] string uriString,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
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
                    return null;
                }

                temporaryBlacklist.Remove(uriString);
            }

            if (!Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                Logger.Warn($"[ExternalResourceManager]: Uri '{uriString}' is not a valid uri!");
                temporaryBlacklist.Add(uriString, float.MaxValue);
                return null;
            }

            string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
            string fileType = Path.GetExtension(uriPath).ToUpperInvariant();

            using (await mutex.LockAsync(token))
            {
                var resource =
                    fileType == ".SDF" || fileType == ".WORLD"
                        ? await TryGetSceneAsync(uriString, provider, token)
                        : await TryGetModelAsync(uriString, provider, token);

                if (resource == null)
                {
                    temporaryBlacklist[uriString] = currentTime;
                }

                return resource;
            }
        }

        async Task<Info<GameObject>> TryGetModelAsync([NotNull] string uriString,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            if (loadedModels.TryGetValue(uriString, out Info<GameObject> resource))
            {
                return resource;
            }

            if (temporaryBlacklist.ContainsKey(uriString))
            {
                return null;
            }
            
            if (resourceFiles.Models.TryGetValue(uriString, out string localPath))
            {
                if (File.Exists($"{Settings.ResourcesPath}/{localPath}"))
                {
                    return await LoadLocalModelAsync(uriString, localPath, provider, token);
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Models.Remove(uriString);
                WriteResourceFile();
            }

            if (provider == null)
            {
                return null;
            }

            token.ThrowIfCancellationRequested();

            GetModelResource msg = new GetModelResource
            {
                Request =
                {
                    Uri = uriString
                }
            };

            try
            {
                bool hasClient = await provider.CallServiceAsync(ModelServiceName, msg, token);
                if (!hasClient)
                {
                    Debug.LogWarning("ExternalResourceManager: Call service failed, no connection");
                    return null;
                }

                if (msg.Response.Success)
                {
                    return await ProcessModelResponseAsync(uriString, msg.Response, provider, token);
                }
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    temporaryBlacklist[uriString] = Time.time;
                }

                throw;
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

        async Task<Info<GameObject>> TryGetSceneAsync([NotNull] string uriString,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            if (loadedScenes.TryGetValue(uriString, out Info<GameObject> resource))
            {
                return resource;
            }

            if (resourceFiles.Scenes.TryGetValue(uriString, out string localPath))
            {
                if (File.Exists($"{Settings.ResourcesPath}/{localPath}"))
                {
                    return await LoadLocalSceneAsync(uriString, localPath, provider, token);
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Scenes.Remove(uriString);
                WriteResourceFile();
            }

            if (provider == null)
            {
                return null;
            }

            token.ThrowIfCancellationRequested();

            GetSdf msg = new GetSdf
            {
                Request =
                {
                    Uri = uriString
                }
            };

            if (await provider.CallServiceAsync(SceneServiceName, msg, token) && msg.Response.Success)
            {
                return await ProcessSceneResponseAsync(uriString, msg.Response, provider, token);
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

        [ItemCanBeNull]
        public async Task<Info<Texture2D>> TryGetTextureAsync([NotNull] string uriString,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
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
                    return null;
                }

                temporaryBlacklist.Remove(uriString);
            }

            if (loadedTextures.TryGetValue(uriString, out var resource))
            {
                return resource;
            }

            if (resourceFiles.Textures.TryGetValue(uriString, out string localPath))
            {
                if (File.Exists($"{Settings.ResourcesPath}/{localPath}"))
                {
                    return LoadLocalTexture(uriString, localPath);
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Textures.Remove(uriString);
                WriteResourceFile();
            }

            if (provider == null)
            {
                return null;
            }

            token.ThrowIfCancellationRequested();

            GetModelTexture msg = new GetModelTexture()
            {
                Request =
                {
                    Uri = uriString
                }
            };

            if (await provider.CallServiceAsync(TextureServiceName, msg, token) && msg.Response.Success)
            {
                return ProcessTextureResponse(uriString, msg.Response);
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
            return null;
        }

        [ItemCanBeNull]
        async Task<Info<GameObject>> LoadLocalModelAsync([NotNull] string uriString, [NotNull] string localPath,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
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
            GameObject obj = await CreateModelObjectAsync(uriString, msg, provider, token);

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
            if (!Application.isEditor)
            {
                texture.Compress(true);
            }

            texture.name = uriString;

            Info<Texture2D> resource = new Info<Texture2D>(uriString, texture);
            loadedTextures[uriString] = resource;

            return resource;
        }

        [ItemCanBeNull]
        async Task<Info<GameObject>> LoadLocalSceneAsync([NotNull] string uriString, [NotNull] string localPath,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
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
            GameObject obj = await CreateSceneNodeAsync(msg, provider, token);

            Info<GameObject> resource = new Info<GameObject>(uriString, obj);
            loadedScenes[uriString] = resource;

            return resource;
        }

        [ItemCanBeNull]
        async Task<Info<GameObject>> ProcessModelResponseAsync([NotNull] string uriString,
            [NotNull] GetModelResourceResponse msg, [NotNull] IExternalServiceProvider provider,
            CancellationToken token)
        {
            try
            {
                GameObject obj = await CreateModelObjectAsync(uriString, msg.Model, provider, token);

                Info<GameObject> info = new Info<GameObject>(uriString, obj);
                loadedModels[uriString] = info;

                string localPath = SanitizeForFilename(uriString);

                byte[] buffer = new byte[msg.Model.RosMessageLength];
                Msgs.Buffer.Serialize(msg.Model, buffer);
                File.WriteAllBytes($"{Settings.ResourcesPath}/{localPath}", buffer);
                Logger.Debug($"Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Models[uriString] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Error processing model response: ", e);
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

                resourceFiles.Textures[uriString] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Error processing texture response: ", e);
                return null;
            }
        }

        [ItemCanBeNull]
        async Task<Info<GameObject>> ProcessSceneResponseAsync([NotNull] string uriString, [NotNull] GetSdfResponse msg,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            try
            {
                GameObject node = await CreateSceneNodeAsync(msg.Scene, provider, token);

                Info<GameObject> info = new Info<GameObject>(uriString, node);

                loadedScenes[uriString] = info;

                string localPath = SanitizeForFilename(uriString);

                byte[] buffer = new byte[msg.Scene.RosMessageLength];
                Msgs.Buffer.Serialize(msg.Scene, buffer);
                File.WriteAllBytes($"{Settings.ResourcesPath}/{localPath}", buffer);
                Logger.Debug($"Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Scenes[uriString] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Error processing scene response: ", e);
                return null;
            }
        }

        [NotNull]
        [ItemNotNull]
        async Task<GameObject> CreateModelObjectAsync([NotNull] string uriString, [NotNull] Model msg,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            GameObject model = (await SceneModel.CreateAsync(uriString, msg, provider, token)).gameObject;
            if (Node != null)
            {
                model.transform.SetParent(Node.transform, false);
            }

            return model;
        }

        [NotNull]
        [ItemNotNull]
        async Task<GameObject> CreateSceneNodeAsync([NotNull] Scene scene,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            GameObject node = new GameObject(scene.Name);
            if (Node != null)
            {
                node.transform.SetParent(Node.SafeNull()?.transform, false);
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
                child.transform.localPosition = ((Vector3) m.GetColumn(3)).Ros2Unity();
                child.transform.localScale = m.lossyScale;

                Info<GameObject> includeResource =
                    await Resource.GetGameObjectResourceAsync(include.Uri, provider, token);
                if (includeResource == null)
                {
                    Logger.Debug("ExternalResourceManager: Failed to retrieve model '" + include.Uri + "'");
                    continue;
                }

                includeResource.Instantiate(child.transform);
            }

            return node;
        }
    }
}