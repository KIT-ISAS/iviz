using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Nito.AsyncEx;
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
        const string StrServiceFailedWithMessage = "Model Loader Service failed to load '{0}'. Reason: {1}";

        const string StrCallServiceFailed =
            "ExternalResourceManager: Call Service failed! Are you sure iviz is connected and the Iviz.Model.Service program is running?";

        const string StrResourceFailedWithError = "ExternalResourceManager: Loading resource {0} failed with error {1}";

        const string StrLocalResourceFailedWithError =
            "ExternalResourceManager: Loading local resource '{0}' failed with error {1}";

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

        CancellationTokenSource runningTs = new CancellationTokenSource();

        public int ResourceCount => resourceFiles.Models.Count + resourceFiles.Textures.Count +
                                    resourceFiles.Scenes.Count + resourceFiles.RobotDescriptions.Count;

        GameObject Node { get; }

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
                Logger.Error($"{this}: Error creating directories", e);
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
                Logger.Error($"{this}: Error reading config file", e);
            }
        }

        public async Task ClearModelCacheAsync(CancellationToken token = default)
        {
            runningTs.Cancel();
            runningTs = new CancellationTokenSource();

            var allFiles = resourceFiles.Models.Values
                .Concat(resourceFiles.Scenes.Values)
                .Concat(resourceFiles.Textures.Values)
                .Concat(resourceFiles.RobotDescriptions.Values);

            Logger.Debug($"{this}: Removing all files in {Settings.SavedRobotsPath}");
            Logger.Debug($"{this}: Removing all files in {Settings.ResourcesPath}");

            foreach (string path in allFiles)
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    Logger.Error($"ExternalResourceManager: Failed to delete file '{path}' :", e);
                }
            }

            resourceFiles.Models.Clear();
            resourceFiles.Scenes.Clear();
            resourceFiles.Textures.Clear();
            resourceFiles.RobotDescriptions.Clear();

            loadedModels.Clear();
            loadedScenes.Clear();
            loadedTextures.Clear();

            await WriteResourceFileAsync(token);
        }

        async Task WriteResourceFileAsync(CancellationToken token)
        {
            await FileUtils.WriteAllTextAsync(Settings.ResourcesFilePath,
                JsonConvert.SerializeObject(resourceFiles, Formatting.Indented), token).AwaitNoThrow(this);
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

        public async Task<(bool result, string robotDescription)> TryGetRobotAsync([NotNull] string robotName,
            CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(robotName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotName));
            }

            if (!resourceFiles.RobotDescriptions.TryGetValue(robotName, out string localPath))
            {
                return default;
            }

            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token))
            {
                string absolutePath = $"{Settings.SavedRobotsPath}/{localPath}";
                if (!File.Exists(absolutePath))
                {
                    Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                    resourceFiles.RobotDescriptions.Remove(robotName);
                    await WriteResourceFileAsync(tokenSource.Token);
                    return default;
                }

                try
                {
                    return (true, await FileUtils.ReadAllTextAsync(absolutePath, tokenSource.Token));
                }
                catch (IOException e)
                {
                    Logger.Error($"ExternalResourceManager: Failed to read robot '{robotName}' :", e);
                    return default;
                }
            }
        }

        public async void AddRobotResourceAsync([NotNull] string robotName, [NotNull] string robotDescription,
            CancellationToken token = default)
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

            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token))
            {
                await FileUtils.WriteAllTextAsync($"{Settings.SavedRobotsPath}/{localPath}", robotDescription,
                    tokenSource.Token);
                Logger.Debug($"Saving to {Settings.SavedRobotsPath}/{localPath}");

                resourceFiles.RobotDescriptions[robotName] = localPath;
                await WriteResourceFileAsync(tokenSource.Token);
            }
        }

        public async void RemoveRobotResource([NotNull] string robotName, CancellationToken token = default)
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
            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token))
            {
                await WriteResourceFileAsync(tokenSource.Token);
            }
        }

        #endregion

        [ContractAnnotation("=> false, resource:null; => true, resource:notnull")]
        public bool TryGetGameObject([NotNull] string uriString, out Info<GameObject> resource)
        {
            return TryGetModel(uriString, out resource) || TryGetScene(uriString, out resource);
        }

        [NotNull, ItemCanBeNull]
        public async Task<Info<GameObject>> TryGetGameObjectAsync([NotNull] string uriString,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token = default)
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

            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token))
            {
                if (fileType == ".SDF" || fileType == ".WORLD")
                {
                    return await TryGetSceneAsync(uriString, provider, tokenSource.Token);
                }

                using (await mutex.LockAsync(tokenSource.Token))
                {
                    var resource = await TryGetModelAsync(uriString, provider, tokenSource.Token);
                    if (resource == null)
                    {
                        temporaryBlacklist[uriString] = currentTime;
                    }

                    return resource;
                }
            }
        }

        [ContractAnnotation("=> false, resource:null; => true, resource:notnull")]
        bool TryGetModel([NotNull] string uriString, out Info<GameObject> resource)
        {
            return loadedModels.TryGetValue(uriString, out resource);
        }

        [NotNull, ItemCanBeNull]
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
                await WriteResourceFileAsync(token);
            }

            return provider == null
                ? null
                : await TryGetModelFromServerAsync(uriString, provider, token);
        }

        [NotNull, ItemCanBeNull]
        async Task<Info<GameObject>> TryGetModelFromServerAsync([NotNull] string uriString,
            [NotNull] IExternalServiceProvider provider, CancellationToken token)
        {
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
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                temporaryBlacklist[uriString] = Time.time;
                throw;
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                Logger.Error(string.Format(StrServiceFailedWithMessage, uriString, msg.Response.Message));
            }
            else
            {
                Logger.Debug(StrCallServiceFailed);
            }

            return null;
        }

        [ContractAnnotation("=> false, resource:null; => true, resource:notnull")]
        bool TryGetScene([NotNull] string uriString, out Info<GameObject> resource)
        {
            return loadedScenes.TryGetValue(uriString, out resource);
        }

        [NotNull, ItemCanBeNull]
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
                await WriteResourceFileAsync(token);
            }

            return provider == null
                ? null
                : await TryGetSceneFromServerAsync(uriString, provider, token);
        }

        [NotNull, ItemCanBeNull]
        async Task<Info<GameObject>> TryGetSceneFromServerAsync([NotNull] string uriString,
            [NotNull] IExternalServiceProvider provider, CancellationToken token)
        {
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
                Logger.Error(string.Format(StrServiceFailedWithMessage, uriString, msg.Response.Message));
            }
            else
            {
                Logger.Debug(StrCallServiceFailed);
            }

            return null;
        }

        [NotNull, ItemCanBeNull]
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


            using (var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token))
            {
                if (resourceFiles.Textures.TryGetValue(uriString, out string localPath))
                {
                    if (File.Exists($"{Settings.ResourcesPath}/{localPath}"))
                    {
                        return await LoadLocalTextureAsync(uriString, localPath, tokenSource.Token);
                    }

                    Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                    resourceFiles.Textures.Remove(uriString);
                    await WriteResourceFileAsync(tokenSource.Token);
                }

                return provider == null
                    ? null
                    : await TryGetTextureFromServerAsync(uriString, provider, tokenSource.Token, currentTime);
            }
        }

        [NotNull, ItemCanBeNull]
        async Task<Info<Texture2D>> TryGetTextureFromServerAsync([NotNull] string uriString,
            [NotNull] IExternalServiceProvider provider, CancellationToken token, float currentTime)
        {
            GetModelTexture msg = new GetModelTexture
            {
                Request =
                {
                    Uri = uriString
                }
            };

            if (await provider.CallServiceAsync(TextureServiceName, msg, token) && msg.Response.Success)
            {
                return await ProcessTextureResponseAsync(uriString, msg.Response, token);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                Logger.Error(string.Format(StrServiceFailedWithMessage, uriString, msg.Response.Message));
            }
            else
            {
                Logger.Debug(StrCallServiceFailed);
            }

            temporaryBlacklist.Add(uriString, currentTime);
            return null;
        }


        [NotNull, ItemCanBeNull]
        async Task<Info<GameObject>> LoadLocalModelAsync([NotNull] string uriString, [NotNull] string localPath,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            byte[] buffer;

            try
            {
                buffer = await FileUtils.ReadAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat(StrResourceFailedWithError, uriString, e);
                return null;
            }

            Model msg = Msgs.Buffer.Deserialize(modelGenerator, buffer, buffer.Length);

            GameObject obj = await CreateModelObjectAsync(uriString, msg, provider, token);

            Info<GameObject> resource = new Info<GameObject>(obj);
            loadedModels[uriString] = resource;

            return resource;
        }

        [NotNull, ItemCanBeNull]
        async Task<Info<Texture2D>> LoadLocalTextureAsync([NotNull] string uriString, [NotNull] string localPath,
            CancellationToken token)
        {
            byte[] buffer;

            try
            {
                buffer = await FileUtils.ReadAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat(StrLocalResourceFailedWithError, uriString, e);
                return null;
            }

            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            texture.LoadImage(buffer);
            if (!Application.isEditor)
            {
                texture.Compress(true);
            }

            texture.name = uriString;

            Info<Texture2D> resource = new Info<Texture2D>(texture);
            loadedTextures[uriString] = resource;

            return resource;
        }

        [NotNull, ItemCanBeNull]
        async Task<Info<GameObject>> LoadLocalSceneAsync([NotNull] string uriString, [NotNull] string localPath,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            byte[] buffer;

            try
            {
                buffer = await FileUtils.ReadAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat(StrLocalResourceFailedWithError, uriString, e);
                return null;
            }

            Scene msg = Msgs.Buffer.Deserialize(sceneGenerator, buffer, buffer.Length);
            GameObject obj = await CreateSceneNodeAsync(msg, provider, token);

            Info<GameObject> resource = new Info<GameObject>(obj);
            loadedScenes[uriString] = resource;

            return resource;
        }

        [NotNull, ItemCanBeNull]
        async Task<Info<GameObject>> ProcessModelResponseAsync([NotNull] string uriString,
            [NotNull] GetModelResourceResponse msg, [NotNull] IExternalServiceProvider provider,
            CancellationToken token)
        {
            try
            {
                Debug.Log("ExternalResourceManager: Processing " + uriString);
                GameObject obj = await CreateModelObjectAsync(uriString, msg.Model, provider, token);
                Debug.Log("ExternalResourceManager: Finished " + uriString);

                Info<GameObject> info = new Info<GameObject>(obj);
                loadedModels[uriString] = info;

                string localPath = SanitizeForFilename(uriString);

                byte[] buffer = new byte[msg.Model.RosMessageLength];
                Msgs.Buffer.Serialize(msg.Model, buffer);
                await FileUtils.WriteAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", buffer, token);
                Logger.Debug($"Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Models[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Error processing model response: ", e);
                return null;
            }
        }

        [NotNull, ItemCanBeNull]
        async Task<Info<Texture2D>> ProcessTextureResponseAsync([NotNull] string uriString,
            [NotNull] GetModelTextureResponse msg, CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                Texture2D texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
                texture.LoadImage(msg.Image.Data);
                texture.name = uriString;

                Info<Texture2D> info = new Info<Texture2D>(texture);
                loadedTextures[uriString] = info;

                string localPath = SanitizeForFilename(uriString);

                byte[] buffer = msg.Image.Data;

                await FileUtils.WriteAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", buffer, token);
                Logger.Debug($"Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Textures[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Error processing texture response: ", e);
                return null;
            }
        }

        [ItemCanBeNull]
        async Task<Info<GameObject>> ProcessSceneResponseAsync([NotNull] string uriString,
            [NotNull] GetSdfResponse msg,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            try
            {
                Debug.Log("ExternalResourceManager: Processing " + uriString);
                GameObject node = await CreateSceneNodeAsync(msg.Scene, provider, token);

                Info<GameObject> info = new Info<GameObject>(node);

                loadedScenes[uriString] = info;

                string localPath = SanitizeForFilename(uriString);

                byte[] buffer = new byte[msg.Scene.RosMessageLength];
                Msgs.Buffer.Serialize(msg.Scene, buffer);
                await FileUtils.WriteAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", buffer, token);
                Logger.Debug($"Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Scenes[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (OperationCanceledException)
            {
                throw;
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

        enum SdfLightType
        {
            Point,
            Directional,
            Spot
        }        
        
        [NotNull]
        [ItemNotNull]
        async Task<GameObject> CreateSceneNodeAsync([NotNull] Scene scene,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            GameObject node = new GameObject(scene.Name);
            if (Node != null)
            {
                node.transform.SetParent(Node.transform, false);
            }

            //Logger.Debug(scene.ToJsonString());

            foreach (Include include in scene.Includes)
            {
                token.ThrowIfCancellationRequested();
                GameObject child = new GameObject("Include");

                Matrix4x4 m = new Matrix4x4();
                for (int i = 0; i < 16; i++)
                {
                    m[i] = include.Pose.M[i];
                }

                var childTransform = child.transform;
                childTransform.SetParent(node.transform, false);
                childTransform.localRotation = m.rotation.Ros2Unity();
                childTransform.localPosition = ((Vector3) m.GetColumn(3)).Ros2Unity();
                childTransform.localScale = m.lossyScale;

                Info<GameObject> includeResource =
                    await Resource.GetGameObjectResourceAsync(include.Uri, provider, token);
                if (includeResource == null)
                {
                    Logger.Debug("ExternalResourceManager: Failed to retrieve model '" + include.Uri + "'");
                    continue;
                }

                includeResource.Instantiate(child.transform);
            }
            
            foreach (Msgs.IvizMsgs.Light source in scene.Lights)
            {
                GameObject lightObject = new GameObject("Light:" + source.Name);
                lightObject.transform.parent = node.transform;
                UnityEngine.Light light = lightObject.AddComponent<UnityEngine.Light>();
                light.color = source.Diffuse.ToColor32();
                light.lightmapBakeType = LightmapBakeType.Mixed;
                light.shadows = source.CastShadows ? LightShadows.Soft : LightShadows.None;
                lightObject.transform.localPosition = source.Position.ToVector3();
                light.range = source.Range != 0 ? source.Range : 20;
                switch ((SdfLightType) source.Type)
                {
                    default:
                        light.type = LightType.Point;
                        break;
                    case SdfLightType.Spot:
                        light.type = LightType.Spot;
                        light.transform.LookAt(light.transform.position + source.Direction.ToVector3());
                        light.spotAngle = source.OuterAngle * Mathf.Rad2Deg;
                        light.innerSpotAngle = source.InnerAngle * Mathf.Rad2Deg;
                        break;
                    case SdfLightType.Directional:
                        light.type = LightType.Directional;
                        light.transform.LookAt(light.transform.position + source.Direction.ToVector3());
                        break;
                }
            }
            

            return node;
        }
    }
}