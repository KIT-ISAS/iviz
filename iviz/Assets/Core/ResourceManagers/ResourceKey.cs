#nullable enable

using System;
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
        public T Object { get; }

        /// <summary>
        /// Returns the instance id of the resource.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Name of this resource.
        /// </summary>
        public string Name => Object.name;

        /// <summary>
        /// Constructs a unique identifier from an existing instance
        /// </summary>
        /// <param name="baseObject">Loaded instance of this type, if already available.</param>
        /// <param name="objectName">>Optional descriptive name for this object, in case of an error.</param>
        public ResourceKey(T baseObject, string? objectName = null) 
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException(objectName ?? nameof(baseObject));
            }

            Object = baseObject;
            Id = Object.GetInstanceID();
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

        public override int GetHashCode() => Id.GetHashCode();
    }

    public static class ResourceKey
    {
        public static T Instantiate<T>(this ResourceKey<GameObject> o, Transform? parent = null)
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
}