#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ResourcePool : MonoBehaviour
    {
        const int TimeToDestroyInSec = 60;

        static ResourcePool? instance;
        readonly HashSet<int> destroyedObjects = new();
        readonly List<GameObject> objectsToDestroy = new();
        readonly Dictionary<int, Queue<ObjectWithDeadline>> pool = new();

        void Awake()
        {
            instance = this;
            GameThread.EverySecond += CheckForDead;
        }

        void OnDestroy()
        {
            GameThread.EverySecond -= CheckForDead;
            instance = null;
        }

        public static GameObject Rent(Info<GameObject> resource, Transform? parent = null, bool enable = true)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (instance != null)
            {
                return instance.Get(resource, parent, enable);
            }

            GameObject obj = resource.Instantiate(parent);
            obj.SetActive(enable);
            return obj;
        }

        public static T Rent<T>(Info<GameObject> resource, Transform? parent = null, bool enable = true)
            where T : MonoBehaviour
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            return Rent(resource, parent, enable).GetComponent<T>();
        }

        public static T RentDisplay<T>(Transform? parent = null) where T : MonoBehaviour, IDisplay
        {
            if (!Resource.Displays.TryGetResource(typeof(T), out var info))
            {
                throw new ResourceNotFoundException("Cannot find unique display type for type " + nameof(T));
            }

            return Rent<T>(info, parent);
        }

        public static void Return(Info<GameObject> resource, GameObject gameObject)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (instance == null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance.Add(resource, gameObject);
            }
        }

        internal static void ReturnDisplay(IDisplay resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (resource is not MonoBehaviour behaviour)
            {
                throw new ArgumentException("Argument is not a MonoBehavior");
            }

            if (!Resource.Displays.TryGetResource(resource.GetType(), out var info))
            {
                throw new ResourceNotFoundException("Cannot find unique display type for resource");
            }

            Return(info, behaviour.gameObject);
        }

        void CheckForDead()
        {
            float now = Time.time;
            objectsToDestroy.Clear();

            foreach (var (_, queue) in pool)
            {
                while (queue.Count != 0 && queue.Peek().ExpirationTime < now)
                {
                    objectsToDestroy.Add(queue.Dequeue().GameObject);
                }
            }

            foreach (var deadObject in objectsToDestroy)
            {
                var recyclables = deadObject.GetComponents<IRecyclable>();
                foreach (var recyclable in recyclables)
                {
                    recyclable.SplitForRecycle();
                }

                Destroy(deadObject);
            }
        }

        GameObject Get(Info<GameObject> resource, Transform? parent, bool enable)
        {
            if (!pool.TryGetValue(resource.Id, out var instances) || instances.Count == 0)
            {
                return Instantiate(resource.Object, parent);
            }

            var newObject = instances.Dequeue().GameObject;
            newObject.transform.SetParentLocal(parent);
            if (enable)
            {
                newObject.SetActive(true);
            }

            destroyedObjects.Remove(newObject.GetInstanceID());
            return newObject;
        }

        void Add(Info<GameObject> resource, GameObject obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("ResourcePool: Attempted to dispose null object of type '" + resource + "'");
                return;
            }

            if (destroyedObjects.Contains(obj.GetInstanceID()))
            {
                Debug.LogWarning($"ResourcePool: Attempting to dispose of object {obj} " +
                                 $"[ type={resource.Object.name} id {obj.GetInstanceID().ToString()} ] multiple times!");
                return;
            }

            if (pool.TryGetValue(resource.Id, out var objects))
            {
                objects.Enqueue(new ObjectWithDeadline(obj));
            }
            else
            {
                var queue = new Queue<ObjectWithDeadline>();
                queue.Enqueue(new ObjectWithDeadline(obj));
                pool[resource.Id] = queue;
            }

            obj.SetActive(false);
            obj.name = resource.Name;
            obj.transform.SetParentLocal(transform);
            obj.transform.localPosition = resource.Object.transform.localPosition;
            obj.transform.localRotation = resource.Object.transform.localRotation;
            obj.transform.localScale = resource.Object.transform.localScale;
            destroyedObjects.Add(obj.GetInstanceID());
        }

        readonly struct ObjectWithDeadline
        {
            public ObjectWithDeadline(GameObject o)
            {
                GameObject = o;
                ExpirationTime = Time.time + TimeToDestroyInSec;
            }

            public float ExpirationTime { get; }
            public GameObject GameObject { get; }
        }
    }
}