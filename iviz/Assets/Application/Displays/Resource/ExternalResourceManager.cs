using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Roslib;
using Iviz.Urdf;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Controllers.Logger;

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
        const string StrCallServiceFailed = "ExternalResourceManager: Call Service failed! Are you sure iviz is connected and the iviz_model_service program is running?";
        const string StrResourceFailedWithError = "ExternalResourceManager: Loading resource {0} failed with error {1}";

        [DataContract]
        public class ResourceFiles
        {
            [DataMember] int Version { get; set; }
            [DataMember] public Dictionary<Uri, string> Models { get; set; }
            [DataMember] public Dictionary<Uri, string> Textures { get; set; }
            [DataMember] public Dictionary<Uri, string> Scenes { get; set; }
            [DataMember] public Dictionary<string, string> RobotDescriptions { get; set; }

            public ResourceFiles()
            {
                Models = new Dictionary<Uri, string>();
                Textures = new Dictionary<Uri, string>();
                Scenes = new Dictionary<Uri, string>();
                RobotDescriptions = new Dictionary<string, string>();
            }
        }

        readonly ResourceFiles resourceFiles = new ResourceFiles();

        readonly Dictionary<Uri, Resource.Info<GameObject>> loadedModels =
            new Dictionary<Uri, Resource.Info<GameObject>>();

        readonly Dictionary<Uri, Resource.Info<Texture2D>> loadedTextures =
            new Dictionary<Uri, Resource.Info<Texture2D>>();

        readonly Dictionary<Uri, Resource.Info<GameObject>> loadedScenes =
            new Dictionary<Uri, Resource.Info<GameObject>>();

        public GameObject Node { get; }

        readonly Model modelGenerator = new Model();
        readonly Scene sceneGenerator = new Scene();

        string ResourceFolder { get; }
        string ResourceFile { get; }
        string RobotsFolder { get; }

        public ReadOnlyCollection<Uri> GetListOfModels() =>
            new ReadOnlyCollection<Uri>(resourceFiles.Models.Keys.ToList());

        public ExternalResourceManager(bool createNode = true)
        {
            ResourceFolder = ModuleListPanel.PersistentDataPath + "/resources";
            RobotsFolder = ModuleListPanel.PersistentDataPath + "/robots";
            ResourceFile = ModuleListPanel.PersistentDataPath + "/resources.json";

            if (createNode)
            {
                Node = new GameObject("External Resources");
                Node.SetActive(false);
            }

            try
            {
                Directory.CreateDirectory(ResourceFolder);
                Directory.CreateDirectory(RobotsFolder);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            if (!File.Exists(ResourceFile))
            {
                Debug.Log("ExternalResourceManager: Failed to find file " + ResourceFile);
                return;
            }

            Debug.Log("ExternalResourceManager: Using resource file " + ResourceFile);

            try
            {
                string text = File.ReadAllText(ResourceFile);
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
                File.WriteAllText(ResourceFile, JsonConvert.SerializeObject(resourceFiles, Formatting.Indented));
            }
            catch (IOException e)
            {
                Logger.Warn("ExternalResourceManager: Failed to write resource file! " + e);
            }
        }

        static readonly MD5 MD5 = new MD5CryptoServiceProvider();
        static string GetMd5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            byte[] textToHash = Encoding.Default.GetBytes(input);
            byte[] result = MD5.ComputeHash(textToHash);
            return BitConverter.ToString(result).Replace("-", "");
        }

        public ReadOnlyCollection<string> GetRobotNames() => 
            new ReadOnlyCollection<string>(resourceFiles.RobotDescriptions.Keys.ToArray());

        public bool ContainsRobot(string robotName) => resourceFiles.RobotDescriptions.ContainsKey(robotName); 

        public bool TryGetRobot(string robotName, out string robotDescription)
        {
            if (string.IsNullOrEmpty(robotName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotName));
            }

            robotDescription = null;
            if (!resourceFiles.RobotDescriptions.TryGetValue(robotName, out string localPath))
            {
                return false;
            }

            string absolutePath = $"{RobotsFolder}/{localPath}";
            if (!File.Exists(absolutePath))
            {
                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.RobotDescriptions.Remove(robotName);
                WriteResourceFile();
                return false;
            }

            try
            {
                robotDescription = File.ReadAllText(absolutePath);
                return true;
            }
            catch (IOException e)
            {
                Debug.Log("ExternalResourceManager: Failed to read robot '" + robotName + "' : " + e);
                return false;
            }
        }

        public void AddRobot(string robotName, string robotDescription)
        {
            if (string.IsNullOrEmpty(robotName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotName));
            }

            if (string.IsNullOrEmpty(robotDescription))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotDescription));
            }

            string localPath = GetMd5Hash(robotName);
            
            File.WriteAllText($"{RobotsFolder}/{localPath}", robotDescription);
            Debug.Log($"Saving to {RobotsFolder}/{localPath}");
            Logger.Internal($"Added robot <i>{robotName}</i> to cache");

            resourceFiles.RobotDescriptions[robotName] = localPath;
            WriteResourceFile();
        }
        
        public void RemoveRobot(string robotName)
        {
            if (string.IsNullOrEmpty(robotName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotName));
            }

            if (!resourceFiles.RobotDescriptions.TryGetValue(robotName, out string localPath))
            {
                return;
            }

            string absolutePath = $"{ResourceFolder}/{localPath}";
            try
            {
                File.Delete(absolutePath);
                Logger.Debug("Removing '" + localPath + "'");
            }
            catch (IOException e)
            {
                Logger.Warn("ExternalResourceManager: Failed to delete robot file '" + localPath + "'");
            }

            Logger.Internal($"Removed robot <i>{robotName}</i> from cache");
            resourceFiles.RobotDescriptions.Remove(robotName);
            WriteResourceFile();
        }        

        public bool TryGet(Uri uri, out Resource.Info<GameObject> resource, bool allowServiceCall = true)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            Debug.Log("ExternalResourceManager: Requesting " + uri);

            string uriPath = Uri.UnescapeDataString(uri.AbsolutePath);
            string fileType = Path.GetExtension(uriPath).ToUpperInvariant();

            if (fileType == ".SDF" || fileType == ".WORLD")
            {
                resource = TryGetScene(uri, allowServiceCall);
            }
            else
            {
                resource = TryGetModel(uri, allowServiceCall);
            }

            if (resource is null)
            {
                Debug.Log("ExternalResourceManager: Resource is null!");
            }

            return resource != null;
        }

        Resource.Info<GameObject> TryGetModel(Uri uri, bool allowServiceCall)
        {
            if (loadedModels.TryGetValue(uri, out Resource.Info<GameObject> resource))
            {
                return resource;
            }

            if (resourceFiles.Models.TryGetValue(uri, out string localPath))
            {
                if (File.Exists($"{ResourceFolder}/{localPath}"))
                {
                    return LoadLocalModel(uri, localPath);
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Models.Remove(uri);
                WriteResourceFile();
            }

            GetModelResource msg = new GetModelResource
            {
                Request =
                {
                    Uri = uri.ToString()
                }
            };

            if (allowServiceCall &&
                ConnectionManager.Connection != null &&
                ConnectionManager.Connection.CallService(ModelServiceName, msg) &&
                msg.Response.Success)
            {
                return ProcessModelResponse(uri, msg.Response);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                Debug.LogWarningFormat(StrServiceFailedWithMessage, msg.Response.Message);
            }
            else
            {
                Debug.Log(StrCallServiceFailed);
            }

            return null;
        }

        Resource.Info<GameObject> TryGetScene(Uri uri, bool allowServiceCall)
        {
            if (loadedScenes.TryGetValue(uri, out Resource.Info<GameObject> resource))
            {
                return resource;
            }

            if (resourceFiles.Scenes.TryGetValue(uri, out string localPath))
            {
                if (File.Exists($"{ResourceFolder}/{localPath}"))
                {
                    return LoadLocalScene(uri, localPath);
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Scenes.Remove(uri);
                WriteResourceFile();
            }

            GetSdf msg = new GetSdf
            {
                Request =
                {
                    Uri = uri.ToString()
                }
            };

            if (allowServiceCall &&
                ConnectionManager.Connection != null &&
                ConnectionManager.Connection.CallService(SceneServiceName, msg) &&
                msg.Response.Success)
            {
                return ProcessSceneResponse(uri, msg.Response);
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                Debug.LogWarningFormat(StrServiceFailedWithMessage, msg.Response.Message);
            }
            else
            {
                Debug.Log(StrCallServiceFailed);
            }

            return null;
        }

        public bool TryGet(Uri uri, out Resource.Info<Texture2D> resource, bool allowServiceCall = true)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (loadedTextures.TryGetValue(uri, out resource))
            {
                return true;
            }

            if (resourceFiles.Textures.TryGetValue(uri, out string localPath))
            {
                if (File.Exists($"{ResourceFolder}/{localPath}"))
                {
                    resource = LoadLocalTexture(uri, localPath);
                    return resource != null;
                }

                Debug.LogWarningFormat(StrMissingFileRemoving, localPath);
                resourceFiles.Textures.Remove(uri);
                WriteResourceFile();
            }

            GetModelTexture msg = new GetModelTexture()
            {
                Request =
                {
                    Uri = uri.ToString()
                }
            };

            if (allowServiceCall &&
                ConnectionManager.Connection != null &&
                ConnectionManager.Connection.CallService(TextureServiceName, msg) &&
                msg.Response.Success)
            {
                resource = ProcessTextureResponse(uri, msg.Response);
                return resource != null;
            }

            if (!string.IsNullOrWhiteSpace(msg.Response.Message))
            {
                Debug.LogWarningFormat(StrServiceFailedWithMessage, msg.Response.Message);
            }
            else
            {
                Debug.Log(StrCallServiceFailed);
            }

            return false;
        }

        Resource.Info<GameObject> LoadLocalModel(Uri uri, string localPath)
        {
            byte[] buffer;

            try
            {
                buffer = File.ReadAllBytes($"{ResourceFolder}/{localPath}");
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat(StrResourceFailedWithError, uri, e);
                return null;
            }

            Model msg = Msgs.Buffer.Deserialize(modelGenerator, buffer, buffer.Length);
            GameObject obj = CreateModelObject(uri, msg);

            Resource.Info<GameObject> resource = new Resource.Info<GameObject>(uri.ToString(), obj);
            loadedModels[uri] = resource;

            return resource;
        }

        Resource.Info<Texture2D> LoadLocalTexture(Uri uri, string localPath)
        {
            byte[] buffer;

            try
            {
                buffer = File.ReadAllBytes($"{ResourceFolder}/{localPath}");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }

            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            texture.LoadImage(buffer);
            texture.Compress(true);
            texture.name = uri.ToString();

            Resource.Info<Texture2D> resource = new Resource.Info<Texture2D>(uri.ToString(), texture);
            loadedTextures[uri] = resource;

            return resource;
        }

        Resource.Info<GameObject> LoadLocalScene(Uri uri, string localPath)
        {
            byte[] buffer;

            try
            {
                buffer = File.ReadAllBytes($"{ResourceFolder}/{localPath}");
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat(StrResourceFailedWithError, uri, e);
                return null;
            }

            Scene msg = Msgs.Buffer.Deserialize(sceneGenerator, buffer, buffer.Length);
            GameObject obj = CreateSceneNode(uri, msg);

            Resource.Info<GameObject> resource = new Resource.Info<GameObject>(uri.ToString(), obj);
            loadedScenes[uri] = resource;

            return resource;
        }

        Resource.Info<GameObject> ProcessModelResponse(Uri uri, GetModelResourceResponse msg)
        {
            try
            {
                GameObject obj = CreateModelObject(uri, msg.Model);

                Resource.Info<GameObject> info = new Resource.Info<GameObject>(uri.ToString(), obj);
                loadedModels[uri] = info;

                string localPath = GetMd5Hash(uri.ToString());

                byte[] buffer = new byte[msg.Model.RosMessageLength];
                Msgs.Buffer.Serialize(msg.Model, buffer);
                File.WriteAllBytes($"{ResourceFolder}/{localPath}", buffer);
                Debug.Log($"Saving to {ResourceFolder}/{localPath}");
                Logger.Internal($"Added external model <i>{uri}</i>");

                resourceFiles.Models[uri] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        Resource.Info<Texture2D> ProcessTextureResponse(Uri uri, GetModelTextureResponse msg)
        {
            try
            {
                Texture2D texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
                texture.LoadImage(msg.Image.Data);
                texture.name = uri.ToString();

                Resource.Info<Texture2D> info = new Resource.Info<Texture2D>(uri.ToString(), texture);
                loadedTextures[uri] = info;

                string localPath = GetMd5Hash(uri.ToString());

                byte[] buffer = msg.Image.Data;
                File.WriteAllBytes($"{ResourceFolder}/{localPath}", buffer);
                Debug.Log($"Saving to {ResourceFolder}/{localPath}");
                Logger.Internal($"Added external texture <i>{uri}</i>");

                resourceFiles.Textures[uri] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        Resource.Info<GameObject> ProcessSceneResponse(Uri uri, GetSdfResponse msg)
        {
            try
            {
                GameObject node = CreateSceneNode(uri, msg.Scene);

                Resource.Info<GameObject> info = new Resource.Info<GameObject>(uri.ToString(), node);

                loadedScenes[uri] = info;

                string localPath = GetMd5Hash(uri.ToString());

                byte[] buffer = new byte[msg.Scene.RosMessageLength];
                Msgs.Buffer.Serialize(msg.Scene, buffer);
                File.WriteAllBytes($"{ResourceFolder}/{localPath}", buffer);
                Debug.Log($"Saving to {ResourceFolder}/{localPath}");
                Logger.Internal($"Added external scene <i>{uri}</i>");

                resourceFiles.Scenes[uri] = localPath;
                WriteResourceFile();

                return info;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        GameObject CreateModelObject(Uri uri, Model msg)
        {
            GameObject model = SceneModel.Create(uri, msg).gameObject;
            if (Node != null)
            {
                model.transform.SetParent(Node?.transform, false);
            }

            return model;
        }

        GameObject CreateSceneNode(Uri _, Scene scene)
        {
            GameObject node = new GameObject(scene.Name);
            if (Node != null)
            {
                node.transform.SetParent(Node?.transform, false);
            }

            Debug.Log(scene.ToJsonString());

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

                Uri includeUri = new Uri(include.Uri);
                if (!Resource.TryGetResource(includeUri, out Resource.Info<GameObject> includeResource))
                {
                    Debug.Log("ExternalResourceManager: Failed to retrieve resource '" + includeUri + "'");
                    continue;
                }

                includeResource.Instantiate(child.transform);
            }

            return node;
        }
    }
}