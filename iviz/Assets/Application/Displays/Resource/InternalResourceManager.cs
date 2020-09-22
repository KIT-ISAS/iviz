using System;
using System.Collections.Generic;
using UnityEngine;

namespace Iviz.Resources
{
    public class InternalResourceManager
    {
        readonly Dictionary<Uri, Resource.Info<GameObject>> gameObjects = new Dictionary<Uri, Resource.Info<GameObject>>
        {
            [new Uri("package://iviz_internal/cube")] = Resource.Displays.Cube,
            [new Uri("package://iviz_internal/cylinder")] = Resource.Displays.Cylinder,
            [new Uri("package://iviz_internal/sphere")] = Resource.Displays.Sphere,
        };

        readonly Dictionary<Uri, Resource.Info<Texture2D>> textures = new Dictionary<Uri, Resource.Info<Texture2D>>();

        public IEnumerable<string> GetRobotNames() => Array.Empty<string>();
        
        public bool ContainsRobot(string robotName)
        {
            return false;
        }

        public bool TryGetRobot(string robotName, out string robotDescription)
        {
            robotDescription = null;
            return false;
        }
        
        public bool TryGet(Uri uri, out Resource.Info<GameObject> info)
        {
            return TryGet(uri, gameObjects, out info);
        }
        
        public bool TryGet(Uri uri, out Resource.Info<Texture2D> info)
        {
            return TryGet(uri, textures, out info);
        } 
        
        static bool TryGet<T>(Uri uri, Dictionary<Uri, Resource.Info<T>> repository, out Resource.Info<T> info) where T : UnityEngine.Object
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
                
            if (repository.TryGetValue(uri, out info))
            {
                return true;
            }

            string path = "Package/" + uri.Host + Uri.UnescapeDataString(uri.AbsolutePath);
            T resource = UnityEngine.Resources.Load<T>(path);
            if (resource is null)
            {
                return false;
            }

            info = new Resource.Info<T>(path, resource);
            repository[uri] = info;
            return true;
        } 
    }
}