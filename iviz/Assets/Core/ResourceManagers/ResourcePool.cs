﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Repository from where objects can be rented and returned to, in order reuse them between modules instead
    /// of destroying and re-creating them.
    /// </summary>
    public sealed class ResourcePool : MonoBehaviour
    {
        const bool CheckDuplicates = !Settings.IsStandalone;
        const int TimeToDestroyInSec = 60;

        [SerializeField] AssetHolder? assetHolder;
        [SerializeField] AppAssetHolder? appAssetHolder;
        [SerializeField] WidgetAssetHolder? widgetAssetHolder;
        [SerializeField] AudioAssetHolder? audioAssetHolder;

        public static AssetHolder AssetHolder =>
            Instance.assetHolder.AssertNotNull(nameof(Instance.assetHolder));

        public static AppAssetHolder AppAssetHolder =>
            Instance.appAssetHolder.AssertNotNull(nameof(Instance.appAssetHolder));

        public static WidgetAssetHolder WidgetAssetHolder =>
            Instance.widgetAssetHolder.AssertNotNull(nameof(Instance.widgetAssetHolder));

        public static AudioAssetHolder AudioAssetHolder =>
            Instance.audioAssetHolder.AssertNotNull(nameof(Instance.audioAssetHolder));

        static ResourcePool? instance;

        static ResourcePool Instance => TryGetInstance(out var resourcePool)
            ? resourcePool
            : throw new MissingAssetFieldException("Cannot find resource pool!");

        static bool TryGetInstance([NotNullWhen(true)] out ResourcePool? resourcePool)
        {
            if (instance != null)
            {
                resourcePool = instance;
                return true;
            }

            var obj = GameObject.Find("Resource Pool");
            if (obj == null || !obj.TryGetComponent(out ResourcePool rp))
            {
                resourcePool = null;
                return false;
            }

            instance = resourcePool = rp;
            return true;
        }

        readonly List<GameObject> objectsToDestroy = new();
        readonly HashSet<int> disposedObjectIds = new();
        readonly Dictionary<int, Queue<ObjectWithExpirationTime>> disposedObjectPool = new();

        readonly List<IRecyclable> recyclableBuffer = new();

        /// <summary>
        /// Transform of the node where disposed objects are stored.
        /// </summary>
        public static Transform Transform => Instance.transform;

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

        public static void ClearResources() => instance = null;

        /// <summary>
        /// Rents an object of the given resource type. If no object of the type exists in the pool, a new one
        /// is instantiated.
        /// </summary>
        /// <param name="resource">The resource identifier.</param>
        /// <param name="parent">Parent transform to attach the rented object.</param>
        /// <param name="enable">Whether the object should be enabled before returning.</param>
        /// <returns>The rented object.</returns>
        public static GameObject Rent(ResourceKey<GameObject> resource, Transform? parent = null, bool enable = true)
        {
            ThrowHelper.ThrowIfNull(resource, nameof(resource));
            return Instance.Get(resource, parent, enable);
        }

        /// <summary>
        /// Rents an object of the given resource type, and obtains the Unity component of type T.
        /// If no object of the type exists in the pool, a new one is instantiated.
        /// </summary>
        /// <param name="resource">The resource identifier.</param>
        /// <param name="parent">Parent transform to attach the rented object.</param>
        /// <param name="enable">Whether the object should be enabled before returning.</param>
        /// <typeparam name="T">The component type.</typeparam>
        /// <returns>The rented object.</returns>
        public static T Rent<T>(ResourceKey<GameObject> resource, Transform? parent = null, bool enable = true)
            where T : MonoBehaviour
        {
            ThrowHelper.ThrowIfNull(resource, nameof(resource));
            var rentedObject = Rent(resource, parent, enable);
            if (rentedObject.TryGetComponent<T>(out var result))
            {
                return result;
            }

            Return(resource, rentedObject);
            throw new ResourceNotFoundException($"Cannot find component of type '{typeof(T).Name}' in rented object");
        }

        /// <summary>
        /// Rents an object of the given display type, performing a lookup of the resource type that corresponds
        /// to the display type T. If no object of the type exists in the pool, a new one is instantiated.
        /// </summary>
        /// <param name="parent">Parent transform to attach the rented object.</param>
        /// <typeparam name="T">The display type.</typeparam>
        /// <returns>The rented object.</returns>
        public static T RentDisplay<T>(Transform? parent = null) where T : MonoBehaviour, IDisplay
        {
            if (!Resource.Displays.TryGetResource(typeof(T), out var info))
            {
                throw new ResourceNotFoundException($"Cannot find unique display type for type '{typeof(T).Name}'");
            }

            return Rent<T>(info, parent);
        }

        /// <summary>
        /// Rents an object of the given display type, but only if the argument is empty.
        /// The rented object will be stored in the argument.
        /// </summary>
        /// <param name="t">The argument to check. The rented object will be written here.</param>
        /// <param name="parent">Parent transform to attach the rented object.</param>
        /// <typeparam name="T">The display type.</typeparam>
        /// <returns>The rented object.</returns>
        public static T RentChecked<T>(ref T? t, Transform? parent = null) where T : MonoBehaviour, IDisplay
        {
            return t != null ? t : (t = RentDisplay<T>(parent));
        }

        public static T RentChecked<T>(ref T? t, ResourceKey<GameObject> resource, Transform? parent = null)
            where T : MonoBehaviour, IDisplay
        {
            return t != null ? t : (t = Rent<T>(resource, parent));
        }

        /// <summary>
        /// Returns a rented object to the pool. 
        /// </summary>
        /// <param name="resource">The resource identifier.</param>
        /// <param name="gameObject">The object to return.</param>
        public static void Return(ResourceKey<GameObject> resource, GameObject gameObject)
        {
            ThrowHelper.ThrowIfNull(resource, nameof(resource));
            ThrowHelper.ThrowIfNull(gameObject, nameof(gameObject));

            // do double check here. we may be too early or during shutdown,
            // so there's no guarantee that the resource pool exists
            if (!TryGetInstance(out var resourcePool))
            {
                Destroy(gameObject);
                return;
            }

            resourcePool.Add(resource, gameObject);
        }

        internal static void ReturnDisplay(IDisplay display)
        {
            ThrowHelper.ThrowIfNull(display, nameof(display));

            if (display is not MonoBehaviour behaviour)
            {
                ThrowHelper.ThrowArgument(
                    $"Argument to {nameof(ReturnDisplay)} must be an object that inherits from MonoBehavior",
                    nameof(display));
                return; // unreachable
            }

            if (!Resource.Displays.TryGetResource(display.GetType(), out var info))
            {
                throw new ResourceNotFoundException("Cannot find unique display type for resource");
            }

            Return(info, behaviour.gameObject);
        }

        void CheckForDead()
        {
            float now = GameThread.GameTime;
            objectsToDestroy.Clear();

            foreach (var queue in disposedObjectPool.Values)
            {
                while (queue.Count != 0 && queue.Peek().ExpirationTime < now)
                {
                    objectsToDestroy.Add(queue.Dequeue().GameObject);
                }
            }

            foreach (var deadObject in objectsToDestroy)
            {
                deadObject.GetComponents(recyclableBuffer);
                foreach (var recyclable in recyclableBuffer)
                {
                    recyclable.SplitForRecycle();
                }

                recyclableBuffer.Clear();
                Destroy(deadObject);
            }
        }

        GameObject Get(ResourceKey<GameObject> resource, Transform? parent, bool enable)
        {
            if (!disposedObjectPool.TryGetValue(resource.Id, out var instanceQueue) || instanceQueue.Count == 0)
            {
                return Instantiate(resource.Object, parent);
            }

            var newObject = instanceQueue.Dequeue().GameObject;
            newObject.transform.SetParentLocal(parent);
            if (enable)
            {
                newObject.SetActive(true);
            }

            if (CheckDuplicates)
            {
                disposedObjectIds.Remove(newObject.GetInstanceID());
            }

            return newObject;
        }

        void Add(ResourceKey<GameObject> resource, GameObject obj)
        {
            if (CheckDuplicates)
            {
                if (disposedObjectIds.Contains(obj.GetInstanceID()))
                {
                    Debug.LogWarning($"{this}: Attempting to return object {obj} " +
                                     $"[ type={resource.Name} id={obj.GetInstanceID().ToString()} ] multiple times!");
                    return;
                }
            }

            if (disposedObjectPool.TryGetValue(resource.Id, out var instanceQueue))
            {
                instanceQueue.Enqueue(new ObjectWithExpirationTime(obj));
            }
            else
            {
                var newQueue = new Queue<ObjectWithExpirationTime>(16);
                newQueue.Enqueue(new ObjectWithExpirationTime(obj));
                disposedObjectPool[resource.Id] = newQueue;
            }

            obj.SetActive(false);
            obj.name = resource.Name;
            obj.transform.SetParentLocal(transform);
            obj.transform.localPosition = resource.Object.transform.localPosition;
            obj.transform.localRotation = resource.Object.transform.localRotation;
            obj.transform.localScale = resource.Object.transform.localScale;

            if (CheckDuplicates)
            {
                disposedObjectIds.Add(obj.GetInstanceID());
            }
        }

        public override string ToString() => $"[{nameof(ResourcePool)}]";

        readonly struct ObjectWithExpirationTime
        {
            public readonly GameObject GameObject;
            public readonly float ExpirationTime;

            public ObjectWithExpirationTime(GameObject o)
            {
                GameObject = o;
                ExpirationTime = GameThread.GameTime + TimeToDestroyInSec;
            }
        }
    }
}