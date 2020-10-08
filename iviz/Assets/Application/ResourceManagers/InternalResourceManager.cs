using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Resources
{
    public class InternalResourceManager
    {
        readonly Dictionary<Uri, Info<GameObject>> gameObjects = new Dictionary<Uri, Info<GameObject>>
        {
            [new Uri("package://iviz_internal/cube")] = Resource.Displays.Cube,
            [new Uri("package://iviz_internal/cylinder")] = Resource.Displays.Cylinder,
            [new Uri("package://iviz_internal/sphere")] = Resource.Displays.Sphere,
        };

        readonly Dictionary<Uri, Info<Texture2D>> textures = new Dictionary<Uri, Info<Texture2D>>();
        readonly ReadOnlyDictionary<string, string> robotDescriptions;

        public IEnumerable<string> GetRobotNames() => robotDescriptions.Keys;

        public InternalResourceManager()
        {
            string robotsFile = UnityEngine.Resources.Load<TextAsset>("Package/iviz/resources")?.text;
            if (string.IsNullOrEmpty(robotsFile))
            {
                Debug.Log("InternalResourceManager: Empty resource file!");
                robotDescriptions = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
                return;
            }

            Dictionary<string, string> robots = JsonConvert.DeserializeObject<Dictionary<string, string>>(robotsFile);
            Dictionary<string, string> tmpRobotDescriptions = new Dictionary<string, string>();
            foreach (var pair in robots)
            {
                string robotDescription = UnityEngine.Resources.Load<TextAsset>("Package/iviz/robots/" + pair.Value)?.text;
                if (string.IsNullOrEmpty(robotDescription))
                {
                    Debug.Log("InternalResourceManager: Empty or null description file " + pair.Value + "!");
                    continue;
                }

                tmpRobotDescriptions[pair.Key] = robotDescription;
            }
            
            robotDescriptions = new ReadOnlyDictionary<string, string>(tmpRobotDescriptions);
        }
        
        public bool ContainsRobot(string robotName)
        {
            if (robotName == null)
            {
                throw new ArgumentNullException(nameof(robotName));
            }

            return robotDescriptions.ContainsKey(robotName);
        }

        public bool TryGetRobot(string robotName, out string robotDescription)
        {
            if (robotName == null)
            {
                throw new ArgumentNullException(nameof(robotName));
            }

            return robotDescriptions.TryGetValue(robotName, out robotDescription);
        }
        
        public bool TryGet(Uri uri, out Info<GameObject> info)
        {
            return TryGet(uri, gameObjects, out info);
        }
        
        public bool TryGet(Uri uri, out Info<Texture2D> info)
        {
            return TryGet(uri, textures, out info);
        } 
        
        static bool TryGet<T>(Uri uri, Dictionary<Uri, Info<T>> repository, out Info<T> info) where T : UnityEngine.Object
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
                
            if (repository.TryGetValue(uri, out info))
            {
                return true;
            }

            string path = $"Package/{uri.Host}{Uri.UnescapeDataString(uri.AbsolutePath)}";
            T resource = UnityEngine.Resources.Load<T>(path);
            if (resource == null)
            {
                return false;
            }

            info = new Info<T>(path, resource);
            repository[uri] = info;
            return true;
        } 
    }
}