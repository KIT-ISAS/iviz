using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Iviz.Resources;
using Iviz.Displays;

namespace Iviz.App
{
    public sealed class ResourcePool : MonoBehaviour
    {
        const int TimeToDestroy = 60;

        static ResourcePool Instance;

        public static GameObject GetOrCreate(Resource.Info<GameObject> resource, Transform parent = null, bool enable = true)
        {
            return Instance is null ? 
                resource.Instantiate(parent) : 
                Instance.GetImpl(resource, parent, enable);
        }

        public static T GetOrCreate<T>(Resource.Info<GameObject> resource, Transform parent = null, bool enable = true) where T : MonoBehaviour
        {
            if (resource is null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            T t = GetOrCreate(resource, parent, enable).GetComponent<T>();
            //Debug.Log("2) " + parent + " -> " + t.transform.parent);
            return t;
        }

        public static void Dispose(Resource.Info<GameObject> resource, GameObject instance)
        {
            Instance?.AddImpl(resource, instance);
        }

        class ObjectWithDeadline
        {
            public float Expiration { get; }
            public GameObject GameObject { get; }

            public ObjectWithDeadline(GameObject o)
            {
                GameObject = o;
                Expiration = Time.time + TimeToDestroy;
            }
        }

        readonly Dictionary<int, Queue<ObjectWithDeadline>> pool = new Dictionary<int, Queue<ObjectWithDeadline>>();
        readonly List<GameObject> objectsToDestroy = new List<GameObject>();

        void Awake()
        {
            Instance = this;
            GameThread.EverySecond += CheckDead;
        }

        void CheckDead()
        {
            float now = Time.time;
            objectsToDestroy.Clear();

            foreach (var entry in pool)
            {
                while (entry.Value.Any() && entry.Value.Peek().Expiration < now)
                {
                    objectsToDestroy.Add(entry.Value.Dequeue().GameObject);
                }
            }

            objectsToDestroy.ForEach(deadObject =>
            {
                IRecyclable recyclable = deadObject.GetComponent<IRecyclable>();
                recyclable?.Recycle();
                Debug.Log("ResourcePool: Destroying object of type '" + deadObject.name + "'");
                Destroy(deadObject);
            });
        }

        void OnDestroy()
        {
            Instance = null;
        }

        readonly HashSet<int> destroyedObjects = new HashSet<int>();

        GameObject GetImpl(Resource.Info<GameObject> resource, Transform parent, bool enable)
        {
            if (!pool.TryGetValue(resource.Id, out Queue<ObjectWithDeadline> instances) || !instances.Any())
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
            //Debug.Log("State: " + string.Join(",", destroyedObjects));
            return obj;
            //gameObject.transform.SetParentLocal(parent);
        }

        void AddImpl(Resource.Info<GameObject> resource, GameObject obj)
        {
            //Debug.Log("Adding " + resource.GameObject.name + " " + gameObject.GetInstanceID());
            if (obj is null)
            {
                Debug.LogWarning("ResourcePool: Attempted to dispose null object of type '" + resource + "'");
                return;
            }

            if (destroyedObjects.Contains(obj.GetInstanceID()))
            {
                Debug.LogWarning($"ResourcePool: Attempting to dispose of object {obj} " +
                    $"[ type={resource.Object.name} id {obj.GetInstanceID()} ] multiple times!");
                //Debug.Log("** State: " + string.Join(",", destroyedObjects));
                return;
            }

            if (pool.TryGetValue(resource.Id, out Queue<ObjectWithDeadline> objects))
            {
                objects.Enqueue(new ObjectWithDeadline(obj));
            }
            else
            {
                Queue<ObjectWithDeadline> queue = new Queue<ObjectWithDeadline>();
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
            //Debug.Log("State: " + string.Join(",", destroyedObjects));
        }
    }
}