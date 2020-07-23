using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Iviz.App.Listeners;
using Iviz.Displays;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.MeshMsgs;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Texture = UnityEngine.Texture;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.App.Resources
{
    public enum ResourceGetResult
    {
        Success,
        Failed,
        Retrieving
    }

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

        public ExternalResourceManager()
        {
            node = new GameObject("External Resources");
            node.transform.parent = TFListener.ListenersFrame?.transform;
            node.SetActive(false);

            string path = UnityEngine.Application.persistentDataPath + "/resources.json";
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                string text = File.ReadAllText(path);
                resourceFiles = JsonConvert.DeserializeObject<ResourceFiles>(text);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }


        public bool TryGet(Uri uri, out Resource.Info<GameObject> resource)
        {
            if (loadedModels.TryGetValue(uri, out resource))
            {
                return true;
            }

            if (resourceFiles.Models.TryGetValue(uri, out string localPath))
            {
                if (false && File.Exists($"{UnityEngine.Application.persistentDataPath}/{localPath}"))
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
        
        public bool TryGet(Uri uri, out Resource.Info<Texture2D> resource)
        {
            if (loadedTextures.TryGetValue(uri, out resource))
            {
                return true;
            }

            if (resourceFiles.Textures.TryGetValue(uri, out string localPath))
            {
                if (false && File.Exists($"{UnityEngine.Application.persistentDataPath}/{localPath}"))
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
                buffer = File.ReadAllBytes($"{UnityEngine.Application.persistentDataPath}/{localPath}");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }

            Msgs.IvizMsgs.Model msg = Msgs.Buffer.Deserialize(generator, buffer, buffer.Length);
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
                buffer = File.ReadAllBytes($"{UnityEngine.Application.persistentDataPath}/{localPath}");
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
                File.WriteAllBytes($"{UnityEngine.Application.persistentDataPath}/{localPath}", buffer);
                Debug.Log($"Saving to {UnityEngine.Application.persistentDataPath}/{localPath}");
                Logger.Internal($"Added external resource <i>{uri}</i>");
                //Debug.Log($"Added external resource <i>{uri}</i>");

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
                File.WriteAllBytes($"{UnityEngine.Application.persistentDataPath}/{localPath}", buffer);
                Debug.Log($"Saving to {UnityEngine.Application.persistentDataPath}/{localPath}");
                Logger.Internal($"Added external resource <i>{uri}</i>");
                //Debug.Log($"Added external resource <i>{uri}</i>");

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
            File.WriteAllText(
                UnityEngine.Application.persistentDataPath + "/resources.json",
                JsonConvert.SerializeObject(resourceFiles, Formatting.Indented));
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
            return BitConverter.ToString(result);
        }

        GameObject CreateModelObject(Uri uri, Model msg)
        {
            GameObject model = new SceneModel(uri, msg).Root;
            model.transform.SetParent(node.transform, false);
            return model;
        }

    }
}