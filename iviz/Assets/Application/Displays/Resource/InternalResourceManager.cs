using System;
using System.Collections.Generic;
using UnityEngine;

namespace Iviz.Resources
{
    public class InternalResourceManager
    {
        readonly Dictionary<Uri, Resource.Info<GameObject>> files = new Dictionary<Uri, Resource.Info<GameObject>>
        {
            [new Uri("package://iviz/cube")] = Resource.Displays.Cube,
            [new Uri("package://iviz/cylinder")] = Resource.Displays.Cylinder,
            [new Uri("package://iviz/sphere")] = Resource.Displays.Sphere,
            [new Uri("package://iviz/rightHand")] = new Resource.Info<GameObject>("Markers/RightHand")
        };

        public bool TryGet(Uri uri, out Resource.Info<GameObject> info)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
                
            if (files.TryGetValue(uri, out info))
            {
                return true;
            }

            string path = "Package/" + uri.Host + Uri.UnescapeDataString(uri.AbsolutePath);
            GameObject resource = UnityEngine.Resources.Load<GameObject>(path);
            if (resource is null)
            {
                return false;
            }

            info = new Resource.Info<GameObject>(path, resource);
            files[uri] = info;
            return true;
        }
    }
}