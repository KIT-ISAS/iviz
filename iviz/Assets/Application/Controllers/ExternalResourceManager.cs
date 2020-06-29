using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Iviz.App.Listeners;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.RoslibSharp;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App.Resources
{

    public class ExternalResourceManager
    {
        readonly Dictionary<Uri, string> resourceFiles = new Dictionary<Uri, string>();
        readonly Dictionary<Uri, Resource.Info<GameObject>> loadedObjects = new Dictionary<Uri, Resource.Info<GameObject>>();
        readonly GameObject node;
        readonly Msgs.IvizMsgs.Model generator = new Msgs.IvizMsgs.Model();

        static readonly HashSet<char> InvalidPathCharacters = new HashSet<char>(Path.GetInvalidFileNameChars());

        public ExternalResourceManager()
        {
            InvalidPathCharacters.Add(':');

            node = new GameObject("External Resources");
            node.transform.parent = TFListener.ListenersFrame.transform;
            node.SetActive(false);

            string path = Application.persistentDataPath + "/resources.json";
            if (File.Exists(path))
            {
                try
                {
                    string text = File.ReadAllText(path);
                    resourceFiles = JsonConvert.DeserializeObject<Dictionary<Uri, string>>(text);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            string callerId = ConnectionManager.Connection.MyId;

            AdvertiseService<Msgs.IvizMsgs.SetModel>("set_model", SetModelCallback);
        }

        void AdvertiseService<T>(string service, Action<T> callback) where T : Msgs.IService, new()
        {
            ConnectionManager.AdvertiseService(service, callback);
            //Logger.Internal($"Offering <b>{service}</b> <i>[{Msgs.BuiltIns.GetServiceType(typeof(T))}]</i>.");
        }

        public bool TryGetResource(Uri uri, out Resource.Info<GameObject> resource)
        {
            if (loadedObjects.TryGetValue(uri, out resource))
            {
                return true;
            }

            if (!resourceFiles.TryGetValue(uri, out string path))
            {
                resource = null;
                return false;
            }

            if (!File.Exists(path))
            {
                Debug.LogWarning($"ExternalResourceManager: Missing file '{path}'. Removing.");
                resourceFiles.Remove(uri);
                resource = null;
                return false;
            }
            resource = LoadModel(uri, path);
            return resource != null;
        }

        Resource.Info<GameObject> LoadModel(Uri uri, string path)
        {
            byte[] buffer;

            try
            {
                buffer = File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }

            Msgs.IvizMsgs.Model msg = Msgs.Buffer.Deserialize(generator, buffer, buffer.Length);
            GameObject obj = CreateModel(msg);
            obj.name = uri.ToString();

            Resource.Info<GameObject> resource = new Resource.Info<GameObject>(uri.ToString(), obj);
            loadedObjects[uri] = resource;

            return resource;
        }


        void SetModelCallback(Msgs.IvizMsgs.SetModel srv)
        {
            if (!Uri.TryCreate(srv.Request.Uri, UriKind.Absolute, out Uri uri) || uri.Scheme != "package")
            {
                srv.Response.Success = false;
                srv.Response.Message = $"Provided uri '{srv.Request.Uri}' is not a valid resource uri";
                return;
            }

            GameThread.RunOnce(() => SaveModel(uri, srv));
            lock (srv)
            {
                Monitor.Wait(srv);
            }
        }

        void SaveModel(Uri uri, Msgs.IvizMsgs.SetModel srv)
        {
            try
            {
                GameObject obj = CreateModel(srv.Request.Model);
                obj.name = uri.ToString();

                loadedObjects[uri] = new Resource.Info<GameObject>(uri.ToString(), obj);

                string path = SanitizePathFile(srv.Request.Uri);

                byte[] buffer = new byte[srv.Request.Model.RosMessageLength];
                Msgs.Buffer.Serialize(srv.Request.Model, buffer);

                File.WriteAllBytes($"{Application.persistentDataPath}/{path}", buffer);
                Debug.Log($"ExternalResourceManager: ++ {uri} | {Application.persistentDataPath}/{path} | ({buffer.Length} bytes)");
                Logger.Internal($"Added external resource <i>{uri}</i>");

                resourceFiles[uri] = path;

                File.WriteAllText(
                    Application.persistentDataPath + "/resources.json",
                    JsonConvert.SerializeObject(resourceFiles, Formatting.Indented));

                srv.Response.Success = true;
            }
            catch (Exception e)
            {
                srv.Response.Success = false;
                srv.Response.Message = e.Message;
                Logger.Error(e);
            }
            lock (srv)
            {
                Monitor.Pulse(srv);
            }
        }

        static string SanitizePathFile(string path)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < path.Length; i++)
            {
                str.Append(InvalidPathCharacters.Contains(path[i]) ? '_' : path[i]);
            }
            return str.ToString();
        }

        GameObject CreateModel(Msgs.IvizMsgs.Model msg)
        {
            GameObject model = new GameObject();
            model.transform.parent = node.transform;

            BoxCollider c = model.AddComponent<BoxCollider>();

            Vector3 min = new Vector3(msg.Bounds.Minx, msg.Bounds.Miny, msg.Bounds.Minz);
            Vector3 max = new Vector3(msg.Bounds.Maxx, msg.Bounds.Maxy, msg.Bounds.Maxz);
            c.center = (min + max) / 2;
            c.size = max - min;

            List<MeshTrianglesResource> children = new List<MeshTrianglesResource>();

            AggregatedMeshMarker amm = model.AddComponent<AggregatedMeshMarker>();
            amm.Children = new ReadOnlyCollection<MeshTrianglesResource>(children);

            foreach (var mesh in msg.Meshes)
            {
                GameObject obj = new GameObject();
                obj.transform.parent = model.transform;

                obj.AddComponent<MeshRenderer>();
                obj.AddComponent<MeshFilter>();
                obj.AddComponent<BoxCollider>();

                MeshTrianglesResource r = obj.AddComponent<MeshTrianglesResource>();
                r.Name = mesh.Name;

                Vector3[] vertices = new Vector3[mesh.Vertices.Length];
                MemCopy(mesh.Vertices, vertices, vertices.Length * 3 * sizeof(float));

                Vector3[] normals = new Vector3[mesh.Normals.Length];
                MemCopy(mesh.Normals, normals, normals.Length * 3 * sizeof(float));

                Color32[] colors = new Color32[mesh.Colors.Length];
                MemCopy(mesh.Colors, colors, colors.Length * 4);

                int[] triangles = new int[mesh.Faces.Length * 3];
                MemCopy(mesh.Faces, triangles, triangles.Length * 4);

                var material = msg.Materials[mesh.MaterialIndex];
                r.Color = new Color32(material.Diffuse.R, material.Diffuse.G, material.Diffuse.B, material.Diffuse.A);

                r.Set(vertices, normals, triangles, colors);

                children.Add(r);
            }

            return model;
        }

        static void MemCopy<A, B>(A[] src, B[] dst, int bytes)
            where A : unmanaged
            where B : unmanaged
        {
            unsafe
            {
                fixed (A* a = src)
                fixed (B* b = dst)
                {
                    Buffer.MemoryCopy(a, b, bytes, bytes);
                }
            }
        }
    }
}