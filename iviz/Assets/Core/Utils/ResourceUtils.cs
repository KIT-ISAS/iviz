#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Core
{
    public static class ResourceUtils
    {
        public static void ReturnToPool(this IDisplay? resource)
        {
            if (resource == null)
            {
                return;
            }

            resource.Suspend();
            ResourcePool.ReturnDisplay(resource);
        }

        public static void ReturnToPool(this IDisplay? resource, ResourceKey<GameObject> resourceKey)
        {
            if (resource == null)
            {
                return;
            }
            
            if (resource is not MonoBehaviour behaviour)
            {
                throw new ArgumentException("Argument is not a MonoBehavior");
            }            

            resource.Suspend();
            ResourcePool.Return(resourceKey, behaviour.gameObject);
        }
    }

    public class MissingAssetFieldException : Exception
    {
        public MissingAssetFieldException(string message) : base(message)
        {
        }
    }    
}