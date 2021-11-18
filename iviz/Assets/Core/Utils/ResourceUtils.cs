#nullable enable

using System;
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

        public static void ReturnToPool(this IDisplay? resource, Info<GameObject> info)
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
            ResourcePool.Return(info, behaviour.gameObject);
        }

        public static ReadOnlyDictionary<T, TU> AsReadOnly<T, TU>(this Dictionary<T, TU> t)
        {
            return new ReadOnlyDictionary<T, TU>(t);
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static T EnsureComponent<T>(this GameObject gameObject) where T : Component =>
            gameObject.TryGetComponent(out T comp) ? comp : gameObject.AddComponent<T>();

        public static T Instantiate<T>(this Info<GameObject> o, Transform? parent = null)
        {
            var component = o.Instantiate(parent).GetComponent<T>();
            if (component == null)
            {
                throw new NullReferenceException("While instantiating " + o + " the component " +
                                                 typeof(T).Name + " was not found.");
            }

            return component;
        }
    }

    public class MissingAssetFieldException : Exception
    {
        public MissingAssetFieldException(string message) : base(message)
        {
        }
    }    
}