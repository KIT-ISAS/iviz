﻿#nullable enable

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
using Iviz.Tools;
using Newtonsoft.Json;
using Nito.AsyncEx;
using UnityEngine;

namespace Iviz.Displays
{
    public class ExternalResourceManager
    {
        const int BlacklistDurationInMs = 5;

        const string ModelServiceName = "/iviz/get_model_resource";
        const string TextureServiceName = "/iviz/get_model_texture";
        const string FileServiceName = "/iviz/get_file";
        const string SceneServiceName = "/iviz/get_sdf";

        const string StrMissingFileRemoving = "ExternalResourceManager: Missing file '{0}'. Removing.";
        const string StrServiceFailedWithMessage = "Model Loader Service failed to load '{0}'. Reason: {1}";

        const string StrCallServiceFailed = "ExternalResourceManager: Call Service failed! " +
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
        readonly Dictionary<string, float> temporaryBlacklist = new();
        readonly AsyncLock mutex = new();
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
                RosLogger.Error($"{this}: Error creating directories", e);
            }

            if (!File.Exists(Settings.ResourcesFilePath))
            {
                RosLogger.Debug($"{this}: Failed to find file {Settings.ResourcesFilePath}");
                return;
            }

            RosLogger.Debug($"{this}: Using resource file {Settings.ResourcesFilePath}");

            try
            {
                string text = File.ReadAllText(Settings.ResourcesFilePath);
                resourceFiles = JsonConvert.DeserializeObject<ResourceFiles>(text);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error reading config file", e);
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

            RosLogger.Debug($"{this}: Removing all files in {Settings.SavedRobotsPath}");
            RosLogger.Debug($"{this}: Removing all files in {Settings.ResourcesPath}");

            foreach (string path in allFiles)
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Failed to delete file '{path}' :", e);
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
            return Uri.EscapeDataString(input ?? throw new ArgumentNullException(nameof(input)));
        }

        #region RobotStuff

        public Dictionary<string, string>.KeyCollection GetRobotNames() => resourceFiles.RobotDescriptions.Keys;

        public bool ContainsRobot(string robotName)
        {
            return resourceFiles.RobotDescriptions.ContainsKey(robotName ??
                                                               throw new ArgumentNullException(nameof(robotName)));
        }

        public async ValueTask<(bool result, string robotDescription)> TryGetRobotAsync(string robotName,
            CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(robotName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotName));
            }

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
                RosLogger.Error($"{this}: Failed to read robot '{robotName}' :", e);
                return (false, "Cannot read robot resource file");
            }
        }

        public async void AddRobotResourceAsync(string robotName, string robotDescription,
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

            string localPath = SanitizeFilename(robotName);

            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);
            await FileUtils.WriteAllTextAsync($"{Settings.SavedRobotsPath}/{localPath}", robotDescription,
                tokenSource.Token);
            RosLogger.Debug($"{this}: Saving to {Settings.SavedRobotsPath}/{localPath}");

            resourceFiles.RobotDescriptions[robotName] = localPath;
            await WriteResourceFileAsync(tokenSource.Token);
        }

        public async void RemoveRobotResource(string robotName, CancellationToken token = default)
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
                RosLogger.Debug($"Removing '{localPath}'");
            }
            catch (IOException)
            {
                RosLogger.Warn($"{this}: Failed to delete robot file '" + localPath + "'");
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

        public async ValueTask<ResourceKey<GameObject>?> TryGetGameObjectAsync(string uriString,
            IExternalServiceProvider? provider, CancellationToken token = default)
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            float currentTime = Time.time;
            if (temporaryBlacklist.TryGetValue(uriString, out float insertionTime))
            {
                if (currentTime < insertionTime + BlacklistDurationInMs)
                {
                    return null;
                }

                temporaryBlacklist.Remove(uriString);
            }

            if (!Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                RosLogger.Warn($"{this}:  Uri '{uriString}' is not a valid uri!");
                temporaryBlacklist.Add(uriString, float.MaxValue);
                return null;
            }

            string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
            string fileType = Path.GetExtension(uriPath).ToUpperInvariant();

            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);
            if (fileType is ".SDF" or ".WORLD")
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

        bool TryGetModel(string uriString, [NotNullWhen(true)] out ResourceKey<GameObject>? resource)
        {
            if (!loadedModels.TryGetValue(uriString, out var resourceCandidate))
            {
                resource = null;
                return false;
            }

            if (resourceCandidate != null)
            {
                resource = resourceCandidate;
                return true;
            }

            resource = null;
            loadedModels.Remove(uriString);
            return false;
        }

        async ValueTask<ResourceKey<GameObject>?> TryGetModelAsync(string uriString, IExternalServiceProvider? provider,
            CancellationToken token)
        {
            if (loadedModels.TryGetValue(uriString, out ResourceKey<GameObject> resource))
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

        async ValueTask<ResourceKey<GameObject>?> TryGetModelFromServerAsync(string uriString,
            IExternalServiceProvider provider, CancellationToken token)
        {
            var msg = new GetModelResource { Request = { Uri = uriString } };
            try
            {
                bool hasClient = await provider.CallServiceAsync(ModelServiceName, msg, TimeoutInMs, token);
                if (!hasClient)
                {
                    RosLogger.Debug($"{this}: Call to model service failed. Reason: Not connected.");
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
                RosLogger.Error(string.Format(StrServiceFailedWithMessage, uriString, msg.Response.Message));
            }
            else
            {
                RosLogger.Debug(StrCallServiceFailed);
            }

            return null;
        }

        bool TryGetScene(string uriString, [NotNullWhen(true)] out ResourceKey<GameObject>? resource)
        {
            return loadedScenes.TryGetValue(uriString, out resource);
        }

        async ValueTask<ResourceKey<GameObject>?> TryGetSceneAsync(string uriString, IExternalServiceProvider? provider,
            CancellationToken token)
        {
            if (loadedScenes.TryGetValue(uriString, out ResourceKey<GameObject> resource))
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

        async ValueTask<ResourceKey<GameObject>?> TryGetSceneFromServerAsync(string uriString,
            IExternalServiceProvider provider, CancellationToken token)
        {
            var msg = new GetSdf { Request = { Uri = uriString } };
            if (await provider.CallServiceAsync(SceneServiceName, msg, TimeoutInMs, token) && msg.Response.Success)
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

        public async ValueTask<ResourceKey<Texture2D>?> TryGetTextureAsync(string uriString,
            IExternalServiceProvider? provider, CancellationToken token)
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
                : await TryGetTextureFromServerAsync(uriString, provider, tokenSource.Token, currentTime);
        }

        async ValueTask<ResourceKey<Texture2D>?> TryGetTextureFromServerAsync(string uriString,
            IExternalServiceProvider provider, CancellationToken token, float currentTime)
        {
            var msg = new GetModelTexture { Request = { Uri = uriString } };
            if (await provider.CallServiceAsync(TextureServiceName, msg, TimeoutInMs, token) && msg.Response.Success)
            {
                return await ProcessTextureResponseAsync(uriString, msg.Response, token);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                RosLogger.Error(string.Format(StrServiceFailedWithMessage, uriString, msg.Response.Message));
            }
            else
            {
                RosLogger.Debug(StrCallServiceFailed);
            }

            temporaryBlacklist.Add(uriString, currentTime);
            return null;
        }

        public ValueTask<Model?> TryGetModelFromFileAsync(string uriString, CancellationToken token = default)
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
                BuiltIns.UTF8.GetString(buffer.Array, 0, Md5SumLength) != Model.RosMd5Sum)
            {
                RosLogger.Warn($"{this}: Resource {uriString} is out of date");
                return null;
            }

            return BuiltIns.DeserializeMessage<Model>(buffer[Md5SumLength..]);
        }

        async ValueTask<ResourceKey<GameObject>?> LoadLocalModelAsync(string uriString, string localPath,
            IExternalServiceProvider? provider, CancellationToken token)
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
                RosLogger.Error($"{this}: Loading resource {uriString} failed with error", e);
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
                RosLogger.Error($"{this}: Loading resource {uriString} failed with error", e);
                return null;
            }

            texture.Compress(true);

            texture.name = uriString;

            var resource = new ResourceKey<Texture2D>(texture);
            loadedTextures[uriString] = resource;

            return resource;
        }

        async ValueTask<ResourceKey<GameObject>?> LoadLocalSceneAsync(string uriString, string localPath,
            IExternalServiceProvider? provider, CancellationToken token)
        {
            GameObject obj;

            try
            {
                using var buffer = await FileUtils.ReadAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", token);
                if (buffer.Length < Md5SumLength ||
                    BuiltIns.UTF8.GetString(buffer.Array, 0, Md5SumLength) != Scene.RosMd5Sum)
                {
                    RosLogger.Warn($"{this}: Resource {uriString} is out of date");
                    return null;
                }

                var msg = BuiltIns.DeserializeMessage<Scene>(buffer[Md5SumLength..]);
                obj = await CreateSceneNodeAsync(msg, provider, token);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{this}: Loading resource {uriString} failed with error", e);
                return null;
            }

            var resource = new ResourceKey<GameObject>(obj);
            loadedScenes[uriString] = resource;

            return resource;
        }

        async ValueTask<ResourceKey<GameObject>?> ProcessModelResponseAsync(string uriString,
            GetModelResourceResponse msg, IExternalServiceProvider provider,
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
                    BuiltIns.UTF8.GetBytes(Model.RosMd5Sum, 0, Md5SumLength, buffer.Array, 0);
                    msg.Model.SerializeTo(buffer[Md5SumLength..]);
                    await FileUtils.WriteAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", buffer, token);
                }

                RosLogger.Debug($"{this}: Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Models[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{this}: Error processing model response: ", e);
                return null;
            }
        }

        async ValueTask<ResourceKey<Texture2D>?> ProcessTextureResponseAsync(string uriString, GetModelTextureResponse msg,
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
                RosLogger.Debug($"{this}: Saving to {Settings.ResourcesPath}/{localPath}");

                resourceFiles.Textures[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{this}: Error processing texture response: ", e);
                return null;
            }
        }

        async ValueTask<ResourceKey<GameObject>?> ProcessSceneResponseAsync(string uriString,
            GetSdfResponse msg, IExternalServiceProvider? provider, CancellationToken token)
        {
            try
            {
                Debug.Log($"{this}: Processing {uriString}");
                var sceneNode = await CreateSceneNodeAsync(msg.Scene, provider, token);
                var info = new ResourceKey<GameObject>(sceneNode);

                loadedScenes[uriString] = info;

                string localPath = SanitizeFilename(uriString);

                using (var buffer = new Rent<byte>(msg.Scene.RosMessageLength + Md5SumLength))
                {
                    BuiltIns.UTF8.GetBytes(Scene.RosMd5Sum, 0, Md5SumLength, buffer.Array, 0);
                    msg.Scene.SerializeTo(buffer[Md5SumLength..]);
                    await FileUtils.WriteAllBytesAsync($"{Settings.ResourcesPath}/{localPath}", buffer, token);
                    RosLogger.Debug($"{this}: Saving to {Settings.ResourcesPath}/{localPath}");
                }

                resourceFiles.Scenes[uriString] = localPath;
                await WriteResourceFileAsync(token);

                return info;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{this}: Error processing scene response: ", e);
                return null;
            }
        }

        async ValueTask<GameObject> CreateModelObjectAsync(string uriString, Model msg,
            IExternalServiceProvider? provider, CancellationToken token)
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

        async ValueTask<GameObject> CreateSceneNodeAsync(Scene scene, IExternalServiceProvider? provider,
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
                    RosLogger.Debug($"ExternalResourceManager: Failed to retrieve model '{include.Uri}'");
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

        public override string ToString() => "ExternalResourceManager";
    }
}