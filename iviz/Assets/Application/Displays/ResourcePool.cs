using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Iviz.Resources;
using Iviz.Displays;

namespace Iviz.App
{
    public class ResourcePool : MonoBehaviour
    {
        const int TimeToDestroy = 60;

        public static ResourcePool Instance { get; private set; }

        public static GameObject GetOrCreate(Resource.Info<GameObject> resource, Transform parent = null, bool enable = true)
        {
            return Instance.GetImpl(resource, parent, enable);
        }

        public static T GetOrCreate<T>(Resource.Info<GameObject> resource, Transform parent = null, bool enable = true) where T : MonoBehaviour
        {
            return GetOrCreate(resource, parent, enable).GetComponent<T>();
        }

        public static void Dispose(Resource.Info<GameObject> resource, GameObject instance)
        {
            if (Instance != null)
            {
                Instance.AddImpl(resource, instance);
            }
        }

        class ObjectWithDeadline
        {
            public readonly DateTime Expiration;
            public readonly GameObject GameObject;

            public ObjectWithDeadline(GameObject o)
            {
                GameObject = o;
                Expiration = DateTime.Now.AddSeconds(TimeToDestroy);
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
            DateTime now = DateTime.Now;
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
            GameObject gameObject;
            if (pool.TryGetValue(resource.Id, out Queue<ObjectWithDeadline> instances) && instances.Any())
            {
                gameObject = instances.Dequeue().GameObject;
                gameObject.transform.SetParentLocal(parent);
                if (enable)
                {
                    gameObject.SetActive(true);
                }
                destroyedObjects.Remove(gameObject.GetInstanceID());
                //Debug.Log("State: " + string.Join(",", destroyedObjects));
                return gameObject;
            }
            return Instantiate(resource.Object, parent);
                //gameObject.transform.SetParentLocal(parent);
        }

        void AddImpl(Resource.Info<GameObject> resource, GameObject gameObject)
        {
            //Debug.Log("Adding " + resource.GameObject.name + " " + gameObject.GetInstanceID());
            if (gameObject == null)
            {
                Debug.LogWarning("ResourcePool: Attempted to dispose null object of type '" + resource + "'");
                return;
            }

            if (destroyedObjects.Contains(gameObject.GetInstanceID()))
            {
                Debug.LogWarning($"ResourcePool: Attempting to dispose of object {gameObject} " +
                    $"[ type={resource.Object.name} id {gameObject.GetInstanceID()} ] multiple times!");
                //Debug.Log("** State: " + string.Join(",", destroyedObjects));
                return;
            }
            
            if (pool.TryGetValue(resource.Id, out Queue<ObjectWithDeadline> objects))
            {
                objects.Enqueue(new ObjectWithDeadline(gameObject));
            }
            else
            {
                Queue<ObjectWithDeadline> queue = new Queue<ObjectWithDeadline>();
                queue.Enqueue(new ObjectWithDeadline(gameObject));
                pool[resource.Id] = queue;
            }
            gameObject.SetActive(false);
            gameObject.name = resource.Name;
            gameObject.transform.SetParentLocal(transform);
            //Debug.Log("Parent of " + gameObject + " is " + gameObject.transform.parent.gameObject);
            gameObject.transform.localPosition = resource.Object.transform.localPosition;
            gameObject.transform.localRotation = resource.Object.transform.localRotation;
            gameObject.transform.localScale = resource.Object.transform.localScale;
            //gameObject.layer = resource.GameObject.layer;
            destroyedObjects.Add(gameObject.GetInstanceID());
            //Debug.Log("State: " + string.Join(",", destroyedObjects));
        }
    }
}