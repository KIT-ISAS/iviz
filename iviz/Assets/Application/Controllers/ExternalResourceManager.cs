using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
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

        [DataContract]
        public class ResourceFiles
        {
            [DataMember] public Dictionary<Uri, string> Models { get; set; }
            [DataMember] public Dictionary<Uri, string> Textures { get; set; }

            public ResourceFiles()
            {
                Models = new Dictionary<Uri, string>();
                Textures = new Dictionary<Uri, string>();
            }
        }

        readonly ResourceFiles resourceFiles = new ResourceFiles();

        readonly Dictionary<Uri, Resource.Info<GameObject>> loadedModels =
            new Dictionary<Uri, Resource.Info<GameObject>>();

        readonly Dictionary<Uri, Resource.Info<Texture2D>> loadedTextures =
            new Dictionary<Uri, Resource.Info<Texture2D>>();

        readonly GameObject node;
        readonly Model generator = new Model();

        string ResourceFolder { get; }
        string ResourceFile { get; }

        public ExternalResourceManager()
        {
            ResourceFolder = ModuleListPanel.PersistentDataPath + "/resources";
            ResourceFile = ModuleListPanel.PersistentDataPath + "/resources.json";            
            
            node = new GameObject("External Resources");
            node.transform.parent = TFListener.ListenersFrame?.transform;
            node.SetActive(false);

            if (!File.Exists(ResourceFile))
            {
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

            try
            {
                Directory.CreateDirectory(ResourceFolder);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }


        public bool TryGet([NotNull] Uri uri, out Resource.Info<GameObject> resource)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            
            Debug.Log("ExternalResourceManager: Requesting " + uri);
            
            if (loadedModels.TryGetValue(uri, out resource))
            {
                return true;
            }

            if (resourceFiles.Models.TryGetValue(uri, out string localPath))
            {
                if (File.Exists($"{ResourceFolder}/{localPath}"))
                {
                    resource = LoadLocalModel(uri, localPath);
                    return resource != null;
                }

                Debug.LogWarning($"ExternalResourceManager: Missing file '{localPath}'. Removing.");
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
            if (!ConnectionManager.Connection.CallService(ModelServiceName, msg))
            {
                return false;
            }

            resource = ProcessModelResponse(uri, msg.Response);
            return resource != null;
        }

        public bool TryGet([NotNull] Uri uri, out Resource.Info<Texture2D> resource)
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

                Debug.LogWarning($"ExternalResourceManager: Missing file '{localPath}'. Removing.");
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
            if (!ConnectionManager.Connection.CallService(TextureServiceName, msg))
            {
                return false;
            }

            resource = ProcessTextureResponse(uri, msg.Response);
            return resource != null;
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
                Debug.LogWarning("ExternalResourceManager: Loading model " + uri + " failed with error " + e);
                return null;
            }

            Model msg = Msgs.Buffer.Deserialize(generator, buffer, buffer.Length);
            GameObject obj = CreateModelObject(uri, msg);
            obj.name = uri.ToString();

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

        Resource.Info<GameObject> ProcessModelResponse(Uri uri, GetModelResourceResponse msg)
        {
            try
            {
                GameObject obj = CreateModelObject(uri, msg.Model);
                obj.name = uri.ToString();

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

        void WriteResourceFile()
        {
            File.WriteAllText(ResourceFile, JsonConvert.SerializeObject(resourceFiles, Formatting.Indented));
        }

        static string GetMd5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(input);
            byte[] result = md5.ComputeHash(textToHash);
            return BitConverter.ToString(result).Replace("-", "");
        }

        GameObject CreateModelObject(Uri uri, Model msg)
        {
            GameObject model = SceneModel.Create(uri, msg);
            model.transform.SetParent(node.transform, false);
            return model;
        }
    }
}