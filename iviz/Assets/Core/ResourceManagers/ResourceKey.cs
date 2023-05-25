#nullable enable

using System;
using Iviz.Core;
using Iviz.Msgs;
using UnityEngine;

namespace Iviz.Resources
{
    /// <summary>
    /// Unique identifier for the type of a Unity resource, used by the Resource Pool as a key.
    /// </summary>
    /// <typeparam name="T">Unity object type, such as GameObject or Texture.</typeparam>
    public sealed class ResourceKey<T> : IEquatable<ResourceKey<T>> where T : UnityEngine.Object
    {
        /// <summary>
        /// Returns or loads a resource of this type.  
        /// </summary>
        public readonly T Object;

        /// <summary>
        /// Returns the instance id of the resource.
        /// </summary>
        public int Id => Object.GetHashCode();

        /// <summary>
        /// Name of this resource.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Constructs a unique identifier from an existing instance
        /// </summary>
        /// <param name="baseObject">Loaded instance of this type, if already available.</param>
        /// <param name="objectName">>Optional descriptive name for this object, in case of an error.</param>
        public ResourceKey(T baseObject, string? objectName = null)
        {
            if (baseObject == null)
            {
                ThrowHelper.ThrowArgumentNull(nameof(baseObject),
                    $"Resource key was not set for asset '{objectName ?? "unnamed"}'");
            }

            Object = baseObject;
            Name = baseObject.name;
        }

        public override string ToString() => Object.ToString();

        /// <summary>
        /// Instantiates a clone of the resource.
        /// </summary>
        /// <param name="parent">Sets the clone's parent to this transform.</param>
        /// <returns>An instantiated clone.</returns>
        public T Instantiate(Transform? parent = null) => UnityEngine.Object.Instantiate(Object, parent);

        public bool Equals(ResourceKey<T>? other) => other != null && Id == other.Id;

        public override bool Equals(object obj) => obj is ResourceKey<T> info && Id == info.Id;

        public static bool operator ==(ResourceKey<T>? a, ResourceKey<T>? b) =>
            ReferenceEquals(a, b) || (a is not null && a.Equals(b));

        public static bool operator !=(ResourceKey<T>? a, ResourceKey<T>? b) => !(a == b);

        public override int GetHashCode() => Object.GetHashCode();
    }

    public static class ResourceKey
    {
        public static T Instantiate<T>(this ResourceKey<GameObject> resourceKey, Transform? parent = null)
        {
            ThrowHelper.ThrowIfNull(resourceKey, nameof(resourceKey));
            if (!resourceKey.Instantiate(parent).TryGetComponent(out T component))
            {
                ThrowHelper.ThrowMissingAssetField($"{resourceKey} does not have a component of type {typeof(T).Name}");
            }

            return component;
        }
    }
}