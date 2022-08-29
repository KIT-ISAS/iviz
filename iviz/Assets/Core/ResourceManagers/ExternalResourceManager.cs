#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Roslib;
using Iviz.Tools;
using Newtonsoft.Json;
using Nito.AsyncEx;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ExternalResourceManager
    {
        const int BlacklistDurationInMs = 5;

        const string ModelServiceName = "/iviz/get_model_resource";
        const string TextureServiceName = "/iviz/get_model_texture";
        const string FileServiceName = "/iviz/get_file";
        const string SceneServiceName = "/iviz/get_sdf";

        const string StrMissingFileRemoving = nameof(ExternalResourceManager) + ": Missing file '{0}'. Removing.";
        const string StrServiceFailedWithMessage = "[Model Loader]: Failed to load '{0}'. Reason: {1}";

        const string StrCallServiceFailed = "[Model Loader]: Call Service failed! " +
                                            "Are you sure iviz is connected and the Iviz.Model.Service program is running?";

        const int TimeoutInMs = 10000;
        const int Md5SumLength = 32;


        [DataContract]
        sealed class ResourceFiles
        {
            [DataMember] int Version { get; set; }
            [DataMember] public Dictionary<string, string> Models { get; set; } = new();
            [DataMember] public Dictionary<string, string> Textures { get; set; } = new();
            [DataMember] public Dictionary<string, string> Scenes { get; set; } = new();
            [DataMember] public Dictionary<string, string> RobotDescriptions { get; set; } = new();
        }

        readonly ResourceFiles resourceFiles = new();
        readonly Dictionary<string, ResourceKey<GameObject>> loadedModels = new();
        readonly Dictionary<string, ResourceKey<Texture2D>> loadedTextures = new();
        readonly Dictionary<string, ResourceKey<GameObject>> loadedScenes = new();
        readonly Dictionary<string, float> blacklistTimestamps = new();
        readonly Dictionary<string, AsyncLock> modelLocks = new();
        readonly GameObject? node;

        CancellationTokenSource runningTs = new();

        public int ResourceCount => resourceFiles.Models.Count + resourceFiles.Textures.Count +
                                    resourceFiles.Scenes.Count + resourceFiles.RobotDescriptions.Count;

        public ReadOnlyCollection<string> GetListOfModels() => resourceFiles.Models.Keys.ToList().AsReadOnly();

        public ExternalResourceManager(bool createNode = true)
        {
            if (createNode)
            {
                node = new GameObject("External Resources");
                node.SetActive(false);
            }

            try
            {
                Directory.CreateDirectory(Settings.ResourcesPath);
                Directory.CreateDirectory(Settings.SavedRobotsPath);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Error creating directories", e);
            }

            if (!File.Exists(Settings.ResourcesFilePath))
            {
                RosLogger.Debug($"{ToString()}: Failed to find file {Settings.ResourcesFilePath}");
                return;
            }

            RosLogger.Debug($"{ToString()}: Using resource file {Settings.ResourcesFilePath}");

            try
            {
                string text = File.ReadAllText(Settings.ResourcesFilePath);
                resourceFiles = JsonUtils.DeserializeObject<ResourceFiles>(text);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Error reading config file", e);
            }
        }

        public Task ClearModelCacheAsync(CancellationToken token = default)
        {
            runningTs.Cancel();
            runningTs = new CancellationTokenSource();

            var allFiles = resourceFiles.Models.Values
                .Concat(resourceFiles.Scenes.Values)
                .Concat(resourceFiles.Textures.Values)
                .Concat(resourceFiles.RobotDescriptions.Values);

            RosLogger.Debug($"{ToString()}: Removing all files in {Settings.SavedRobotsPath}");
            RosLogger.Debug($"{ToString()}: Removing all files in {Settings.ResourcesPath}");

            foreach (string path in allFiles)
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{ToString()}: Failed to delete file '{path}' :", e);
                }
            }

            resourceFiles.Models.Clear();
            resourceFiles.Scenes.Clear();
            resourceFiles.Textures.Clear();
            resourceFiles.RobotDescriptions.Clear();

            loadedModels.Clear();
            loadedScenes.Clear();
            loadedTextures.Clear();

            return WriteResourceFileAsync(token);
        }

        Task WriteResourceFileAsync(CancellationToken token)
        {
            return FileUtils.WriteAllTextAsync(Settings.ResourcesFilePath,
                BuiltIns.ToJsonString(resourceFiles), token).AwaitNoThrow(this);
        }

        public static string SanitizeFilename(string input)
        {
            ThrowHelper.ThrowIfNull(input, nameof(input));
            return Uri.EscapeDataString(input);
        }

        #region RobotStuff

        public Dictionary<string, string>.KeyCollection GetRobotNames() => resourceFiles.RobotDescriptions.Keys;

        public bool ContainsRobot(string robotName)
        {
            ThrowHelper.ThrowIfNull(robotName, nameof(robotName));
            return resourceFiles.RobotDescriptions.ContainsKey(robotName);
        }

        public async ValueTask<(bool result, string robotDescription)> TryGetRobotAsync(string robotName,
            CancellationToken token = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(robotName, nameof(robotName));

            if (!resourceFiles.RobotDescriptions.TryGetValue(robotName, out string localPath))
            {
                return (false, "Robot not found");
            }

            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);
            string absolutePath = $"{Settings.SavedRobotsPath}/{localPath}";
            if (!File.Exists(absolutePath))
            {
                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.RobotDescriptions.Remove(robotName);
                await WriteResourceFileAsync(tokenSource.Token);
                return (false, "Robot resource file is missing");
            }

            try
            {
                return (true, await FileUtils.ReadAllTextAsync(absolutePath, tokenSource.Token));
            }
            catch (IOException e)
            {
                RosLogger.Error($"{ToString()}: Failed to read robot '{robotName}' :", e);
                return (false, "Cannot read robot resource file");
            }
        }

        public async ValueTask AddRobotResourceAsync(string robotName, string robotDescription,
            CancellationToken token = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(robotName, nameof(robotName));
            ThrowHelper.ThrowIfNullOrEmpty(robotDescription, nameof(robotDescription));

            string localPath = SanitizeFilename(robotName);

            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);
            await FileUtils.WriteAllTextAsync($"{Settings.SavedRobotsPath}/{localPath}", robotDescription,
                tokenSource.Token);
            RosLogger.Debug($"{ToString()}: Saving to {Settings.SavedRobotsPath}/{localPath}");

            resourceFiles.RobotDescriptions[robotName] = localPath;
            await WriteResourceFileAsync(tokenSource.Token);
        }

        public async ValueTask RemoveRobotResourceAsync(string robotName, CancellationToken token = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(robotName, nameof(robotName));

            if (!resourceFiles.RobotDescriptions.TryGetValue(robotName, out string localPath))
            {
                return;
            }

            string absolutePath = $"{Settings.ResourcesPath}/{localPath}";
            try
            {
                File.Delete(absolutePath);
                RosLogger.Debug($"Removing '{localPath}'");
            }
            catch (IOException)
            {
                RosLogger.Warn($"{ToString()}: Failed to delete robot file '" + localPath + "'");
            }

            resourceFiles.RobotDescriptions.Remove(robotName);

            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);
            await WriteResourceFileAsync(tokenSource.Token);
        }

        #endregion

        public bool TryGetGameObject(string uriString, [NotNullWhen(true)] out ResourceKey<GameObject>? resource)
        {
            return TryGetModel(uriString, out resource) || TryGetScene(uriString, out resource);
        }

        public ValueTask<ResourceKey<GameObject>?> TryGetGameObjectAsync(string uriString,
            IServiceProvider? provider, CancellationToken token = default)
        {
            ThrowHelper.ThrowIfNull(uriString, nameof(uriString));

            if (TryGetGameObject(uriString, out var resource))
            {
                return resource.AsTaskResultMaybeNull();
            }

            float currentTime = Time.time;
            if (blacklistTimestamps.TryGetValue(uriString, out float blacklistTime))
            {
                if (currentTime < blacklistTime)
                {
                    return default;
                }

                blacklistTimestamps.Remove(uriString);
            }

            if (!Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                RosLogger.Warn($"{ToString()}:  Uri '{uriString}' is not a valid uri!");
                blacklistTimestamps.Add(uriString, float.MaxValue);
                return default;
            }

            return GetGameObjectAsync(uriString, uri, provider, token);
        }

        async ValueTask<ResourceKey<GameObject>?> GetGameObjectAsync(string uriString, Uri uri,
            IServiceProvider? provider, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
            string fileType = Path.GetExtension(uriPath).ToUpperInvariant();

            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);
            
            if (fileType is ".SDF" or ".WORLD")
            {
                return await GetSceneAsync(uriString, provider, tokenSource.Token);
                //RosLogger.Error($"{ToString()}: SDF parsing is disabled");
                //return null;
            }


            // multiple displays may ask for the same resource at the same time
            // we use a lock to let one pass and make the other wait
            if (!modelLocks.TryGetValue(uriString, out var modelLock))
            {
                modelLock = new AsyncLock();
                modelLocks[uriString] = modelLock;
            }

            try
            {
                using (await modelLock.LockAsync(tokenSource.Token))
                {
                    var newResource = await GetModelAsync(uriString, provider, tokenSource.Token);
                    if (newResource == null)
                    {
                        blacklistTimestamps[uriString] = Time.time + BlacklistDurationInMs;
                    }

                    return newResource;
                }
            }
            finally
            {
                modelLocks.Remove(uriString);
            }
        }

        bool TryGetModel(string uriString, [NotNullWhen(true)] out ResourceKey<GameObject>? resource)
        {
            return loadedModels.TryGetValue(uriString, out resource);
        }

        async ValueTask<ResourceKey<GameObject>?> GetModelAsync(string uriString, IServiceProvider? provider,
            CancellationToken token)
        {
            // we just passed a mutex, the quick paths may have just become valid
            if (blacklistTimestamps.ContainsKey(uriString))
            {
                return null;
            }

            if (TryGetGameObject(uriString, out var resource))
            {
                return resource;
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
                : await GetModelFromServerAsync(uriString, provider, token);
        }

        async ValueTask<ResourceKey<GameObject>?> GetModelFromServerAsync(string uriString,
            IServiceProvider provider, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var msg = new GetModelResource { Request = { Uri = uriString } };
            bool hasService = await provider.CallModelServiceAsync(ModelServiceName, msg, TimeoutInMs, token);
            if (!hasService)
            {
                //RosLogger.Debug($"{ToString()}: Call to model service failed.");
                return null;
            }

            if (msg.Response.Success)
            {
                return await ProcessModelResponseAsync(uriString, msg.Response, provider, token);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                RosLogger.Error(string.Format(StrServiceFailedWithMessage, uriString, msg.Response.Message));
            }

            return null;
        }

        bool TryGetScene(string uriString, [NotNullWhen(true)] out ResourceKey<GameObject>? resource)
        {
            return loadedScenes.TryGetValue(uriString, out resource);
        }

        async ValueTask<ResourceKey<GameObject>?> GetSceneAsync(string uriString, IServiceProvider? provider,
            CancellationToken token)
        {
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
                : await GetSceneFromServerAsync(uriString, provider, token);
        }

        async ValueTask<ResourceKey<GameObject>?> GetSceneFromServerAsync(string uriString,
            IServiceProvider provider, CancellationToken token)
        {
            var msg = new GetSdf { Request = { Uri = uriString } };
            if (await provider.CallModelServiceAsync(SceneServiceName, msg, TimeoutInMs, token) && msg.Response.Success)
            {
                return await ProcessSceneResponseAsync(uriString, msg.Response, provider, token);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                RosLogger.Error(string.Format(StrServiceFailedWithMessage, uriString, msg.Response.Message));
            }
            else
            {
                RosLogger.Debug(StrCallServiceFailed);
            }

            return null;
        }

        public ValueTask<ResourceKey<Texture2D>?> TryGetTextureAsync(string uriString, IServiceProvider? provider,
            CancellationToken token)
        {
            ThrowHelper.ThrowIfNull(uriString, nameof(uriString));

            if (loadedTextures.TryGetValue(uriString, out var existingTexture))
            {
                return existingTexture.AsTaskResultMaybeNull();
            }

            float currentTime = Time.time;
            if (blacklistTimestamps.TryGetValue(uriString, out float blacklistTime))
            {
                if (currentTime < blacklistTime)
                {
                    return default;
                }

                blacklistTimestamps.Remove(uriString);
            }

            return GetTextureAsync(uriString, provider, token);
        }

        async ValueTask<ResourceKey<Texture2D>?> GetTextureAsync(string uriString, IServiceProvider? provider,
            CancellationToken token)
        {
            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);
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
                : await GetTextureFromServerAsync(uriString, provider, tokenSource.Token);
        }

        async ValueTask<ResourceKey<Texture2D>?> GetTextureFromServerAsync(string uriString,
            IServiceProvider provider, CancellationToken token)
        {
            var msg = new GetModelTexture { Request = { Uri = uriString } };
            bool hasService = await provider.CallModelServiceAsync(TextureServiceName, msg, TimeoutInMs, token);
            if (!hasService)
            {
                return null;
            }

            if (msg.Response.Success)
            {
                return await ProcessTextureResponseAsync(uriString, msg.Response, token);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                RosLogger.Error(string.Format(StrServiceFailedWithMessage, uriString, msg.Response.Message));
            }

            return null;
        }

        public ValueTask<Model?> GetModelFromFileAsync(string uriString, CancellationToken token = default)
        {
            if (resourceFiles.Models.TryGetValue(uriString, out string localPath)
                && File.Exists($"{Settings.ResourcesPath}/{localPath}"))
            {
                return ReadModelFromFileAsync(uriString, localPath, token);
            }

            return default; // completed, null
        }

        async ValueTask<Model?> ReadModelFromFileAsync(string uriString, string localPath, CancellationToken token)
        {
            using var buffer = await FileUtils.ReadAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", token);
            if (buffer.Length < Md5SumLength ||
                BuiltIns.UTF8.GetString(buffer[..Md5SumLength]) != Model.Md5Sum)
            {
                RosLogger.Warn($"{ToString()}: Resource {uriString} is out of date");
                return null;
            }

            return RosUtils.DeserializeMessage<Model>(buffer[Md5SumLength..]);
        }

        async ValueTask<ResourceKey<GameObject>?> LoadLocalModelAsync(string uriString, string localPath,
            IServiceProvider? provider, CancellationToken token)
        {
            GameObject obj;
            try
            {
                var msg = await ReadModelFromFileAsync(uriString, localPath, token);
                if (msg == null)
                {
                    return null;
                }

                obj = await CreateModelObjectAsync(uriString, msg, provider, token);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{ToString()}: Loading resource {uriString} failed with error", e);
                return null;
            }

            var resource = new ResourceKey<GameObject>(obj);
            loadedModels[uriString] = resource;

            return resource;
        }

        async ValueTask<ResourceKey<Texture2D>?> LoadLocalTextureAsync(string uriString, string localPath,
            CancellationToken token)
        {
            Texture2D texture;

            try
            {
                using var buffer = await FileUtils.ReadAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", token);
                texture = new Texture2D(1, 1, TextureFormat.RGB24, true);
                texture.LoadImage(buffer.Array);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{ToString()}: Loading resource {uriString} failed with error", e);
                return null;
            }

            texture.name = uriString;

            var resource = new ResourceKey<Texture2D>(texture);
            loadedTextures[uriString] = resource;

            return resource;
        }

        async ValueTask<ResourceKey<GameObject>?> LoadLocalSceneAsync(string uriString, string localPath,
            IServiceProvider? provider, CancellationToken token)
        {
            GameObject obj;

            try
            {
                Scene msg;

                using (var buffer = await FileUtils.ReadAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", token))
                {
                    if (buffer.Length < Md5SumLength ||
                        BuiltIns.UTF8.GetString(buffer[..Md5SumLength]) != Scene.Md5Sum)
                    {
                        RosLogger.Warn($"{ToString()}: Resource {uriString} is out of date");
                        return null;
                    }

                    msg = RosUtils.DeserializeMessage<Scene>(buffer[Md5SumLength..]);
                }

                obj = await CreateSceneNodeAsync(msg, provider, token);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{ToString()}: Loading resource {uriString} failed with error", e);
                return null;
            }

            var resource = new ResourceKey<GameObject>(obj);
            loadedScenes[uriString] = resource;

            return resource;
        }

        async ValueTask<ResourceKey<GameObject>?> ProcessModelResponseAsync(string uriString,
            GetModelResourceResponse msg, IServiceProvider provider,
            CancellationToken token)
        {
            try
            {
                var obj = await CreateModelObjectAsync(uriString, msg.Model, provider, token);
                var info = new ResourceKey<GameObject>(obj);
                loadedModels[uriString] = info;

                string localPath = SanitizeFilename(uriString);

                using (var buffer = new Rent<byte>(msg.Model.RosMessageLength + Md5SumLength))
                {
                    BuiltIns.UTF8.GetBytes(Model.Md5Sum.AsSpan(), buffer[..Md5SumLength]);
                    msg.Model.SerializeTo(buffer[Md5SumLength..]);
                    await FileUtils.WriteAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", buffer, token);
                }

                RosLogger.Debug($"{ToString()}: Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Models[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{ToString()}: Error processing model response: ", e);
                return null;
            }
        }

        async ValueTask<ResourceKey<Texture2D>?> ProcessTextureResponseAsync(string uriString,
            GetModelTextureResponse msg,
            CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                var texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
                texture.LoadImage(msg.Image.Data);
                texture.name = uriString;

                var info = new ResourceKey<Texture2D>(texture);
                loadedTextures[uriString] = info;

                string localPath = SanitizeFilename(uriString);

                await FileUtils.WriteAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", msg.Image.Data, token);
                RosLogger.Debug($"{ToString()}: Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Textures[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{ToString()}: Error processing texture response: ", e);
                return null;
            }
        }

        async ValueTask<ResourceKey<GameObject>?> ProcessSceneResponseAsync(string uriString,
            GetSdfResponse msg, IServiceProvider? provider, CancellationToken token)
        {
            try
            {
                Debug.Log($"{ToString()}: Processing {uriString}");
                var sceneNode = await CreateSceneNodeAsync(msg.Scene, provider, token);
                var info = new ResourceKey<GameObject>(sceneNode);

                loadedScenes[uriString] = info;

                string localPath = SanitizeFilename(uriString);

                using (var buffer = new Rent<byte>(msg.Scene.RosMessageLength + Md5SumLength))
                {
                    BuiltIns.UTF8.GetBytes(Scene.Md5Sum.AsSpan(), buffer[..Md5SumLength]);
                    msg.Scene.SerializeTo(buffer[Md5SumLength..]);
                    await FileUtils.WriteAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", buffer, token);
                    RosLogger.Debug($"{ToString()}: Saving to {Settings.ResourcesPath}/{localPath}");
                }

                resourceFiles.Scenes[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{ToString()}: Error processing scene response: ", e);
                return null;
            }
        }

        async ValueTask<GameObject> CreateModelObjectAsync(string uriString, Model msg,
            IServiceProvider? provider, CancellationToken token)
        {
            var model = (await SceneModel.CreateAsync(uriString, msg, provider, token)).gameObject;
            if (node != null)
            {
                model.transform.SetParent(node.transform, false);
            }

            return model;
        }

        enum SdfLightType
        {
            Point,
            Directional,
            Spot
        }

        async ValueTask<GameObject> CreateSceneNodeAsync(Scene scene, IServiceProvider? provider,
            CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var sceneNode = new GameObject(scene.Name);
            if (node != null)
            {
                sceneNode.transform.SetParent(node.transform, false);
            }

            foreach (var include in scene.Includes)
            {
                token.ThrowIfCancellationRequested();
                var child = new GameObject("Include");

                var m = new Matrix4x4();
                foreach (int i in ..16)
                {
                    m[i] = include.Pose.M[i];
                }

                var childTransform = child.transform;
                childTransform.SetParent(sceneNode.transform, false);
                childTransform.localRotation = m.rotation.Ros2Unity();
                childTransform.localPosition = ((Vector3)m.GetColumn(3)).Ros2Unity();
                childTransform.localScale = m.lossyScale;

                var includeResource = await Resource.GetGameObjectResourceAsync(include.Uri, provider, token);
                if (includeResource == null)
                {
                    RosLogger.Debug($"{nameof(ExternalResourceManager)}: Failed to retrieve model '{include.Uri}'");
                    continue;
                }

                includeResource.Instantiate(child.transform);
            }

            foreach (var source in scene.Lights)
            {
                var lightObject = new GameObject("Light:" + source.Name);
                lightObject.transform.parent = sceneNode.transform;
                var light = lightObject.AddComponent<UnityEngine.Light>();
                light.color = source.Diffuse.ToColor32();
                light.shadows = source.CastShadows ? LightShadows.Soft : LightShadows.None;
                lightObject.transform.localPosition = source.Position.Ros2Unity();
                light.range = source.Range != 0 ? source.Range : 20;
                switch ((SdfLightType)source.Type)
                {
                    default:
                        light.type = LightType.Point;
                        break;
                    case SdfLightType.Spot:
                        light.type = LightType.Spot;
                        light.transform.LookAt(light.transform.position + source.Direction.Ros2Unity());
                        light.spotAngle = source.OuterAngle * Mathf.Rad2Deg;
                        light.innerSpotAngle = source.InnerAngle * Mathf.Rad2Deg;
                        break;
                    case SdfLightType.Directional:
                        light.type = LightType.Directional;
                        light.transform.LookAt(light.transform.position + source.Direction.Ros2Unity());
                        break;
                }
            }


            return sceneNode;
        }

        public override string ToString() => $"[{nameof(ExternalResourceManager)}]";
    }
}