using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class ResourcePool : MonoBehaviour
    {
        const int TimeToDestroyInSec = 60;

        static ResourcePool Instance;
        readonly HashSet<int> destroyedObjects = new HashSet<int>();
        readonly List<GameObject> objectsToDestroy = new List<GameObject>();
        readonly Dictionary<int, Queue<ObjectWithDeadline>> pool = new Dictionary<int, Queue<ObjectWithDeadline>>();

        void Awake()
        {
            Instance = this;
            GameThread.EverySecond += CheckForDead;
        }

        void OnDestroy()
        {
            Instance = null;
        }

        [NotNull]
        public static GameObject GetOrCreate([NotNull] Info<GameObject> resource, [CanBeNull] Transform parent = null,
            bool enable = true)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (Instance != null)
            {
                return Instance.Get(resource, parent, enable);
            }

            GameObject obj = resource.Instantiate(parent);
            obj.SetActive(enable);
            return obj;
        }

        [NotNull]
        public static T GetOrCreate<T>([NotNull] Info<GameObject> resource, [CanBeNull] Transform parent = null, bool enable = true)
            where T : MonoBehaviour
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            return GetOrCreate(resource, parent, enable).GetComponent<T>();
        }

        public static void Dispose([NotNull] Info<GameObject> resource, [NotNull] GameObject instance)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (Instance == null)
            {
                Destroy(instance);
            }
            else
            {
                Instance.Add(resource, instance);
            }
        }

        [NotNull]
        public static T GetOrCreateDisplay<T>([CanBeNull] Transform parent = null) where T : MonoBehaviour, IDisplay
        {
            if (!Resource.Displays.TryGetResource(typeof(T), out var info))
            {
                throw new ResourceNotFoundException("Cannot find unique display type for type " + nameof(T));
            }

            return GetOrCreate<T>(info, parent);
        }

        public static void DisposeDisplay<T>([NotNull] T resource) where T : MonoBehaviour, IDisplay
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (!Resource.Displays.TryGetResource(typeof(T), out var info))
            {
                throw new ResourceNotFoundException("Cannot find unique display type for resource");
            }

            Dispose(info, resource.gameObject);
        }

        public static bool TryDisposeDisplay([NotNull] IDisplay resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (!(resource is MonoBehaviour behaviour))
            {
                throw new ArgumentException("Invalid resource type");
            }

            if (!Resource.Displays.TryGetResource(resource.GetType(), out Info<GameObject> info))
            {
                Debug.Log($"ResourcePool: Could not find display for '{behaviour.gameObject.name}'");
                Destroy(behaviour.gameObject);
                return false;
            }

            Dispose(info, behaviour.gameObject);
            return true;
        }

        void CheckForDead()
        {
            float now = Time.time;
            objectsToDestroy.Clear();

            foreach (var entry in pool)
            {
                while (entry.Value.Any() && entry.Value.Peek().ExpirationTime < now)
                {
                    objectsToDestroy.Add(entry.Value.Dequeue().GameObject);
                }
            }

            foreach (var deadObject in objectsToDestroy)
            {
                var recyclable = deadObject.GetComponent<IRecyclable>();
                recyclable?.SplitForRecycle();
                Destroy(deadObject);
            }
        }

        GameObject Get([NotNull] Info<GameObject> resource, [CanBeNull] Transform parent, bool enable)
        {
            if (!pool.TryGetValue(resource.Id, out var instances) || !instances.Any())
            {
                return Instantiate(resource.Object, parent);
            }

            GameObject obj = instances.Dequeue().GameObject;
            obj.transform.SetParentLocal(parent);
            if (enable)
            {
                obj.SetActive(true);
            }

            destroyedObjects.Remove(obj.GetInstanceID());
            return obj;
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
                                 $"[ type={resource.Object.name} id {obj.GetInstanceID()} ] multiple times!");
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

        class ObjectWithDeadline
        {
            public ObjectWithDeadline([NotNull] GameObject o)
            {
                GameObject = o;
                ExpirationTime = Time.time + TimeToDestroyInSec;
            }

            public float ExpirationTime { get; }
            [NotNull] public GameObject GameObject { get; }
        }
    }
}