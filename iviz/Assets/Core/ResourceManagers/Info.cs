using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Resources
{
    /// <summary>
    /// Unique identifier for the type of a Unity resource.
    /// Basically a wrapper around the instance id of the resource that only gets loaded when the asset is needed.
    /// Required by the Resource Pool.
    /// </summary>
    /// <typeparam name="T">Unity object type, such as a GameObject or a Texture.</typeparam>
    public sealed class Info<T> : IEquatable<Info<T>> where T : UnityEngine.Object
    {
        readonly string resourceName;

        T baseObject;

        /// <summary>
        /// Returns or loads a resource of this type.  
        /// </summary>
        public T Object
        {
            get
            {
                if (!(baseObject is null))
                {
                    return baseObject;
                }

                baseObject = UnityEngine.Resources.Load<T>(resourceName);
                if (baseObject == null)
                {
                    throw new ArgumentException($"Cannot find resource '{resourceName}'", nameof(resourceName));
                }

                return baseObject;
            }
        }

        int id = 0;

        /// <summary>
        /// Returns the instance id of the resource.
        /// </summary>
        public int Id => (id != 0) ? id : (id = Object.GetInstanceID());

        /// <summary>
        /// Constructs a unique identifier from a resource path, and loads it from memory.
        /// </summary>
        /// <param name="resourceName">Path to the resource.</param>
        public Info([NotNull] string resourceName)
        {
            this.resourceName = resourceName ?? throw new ArgumentNullException(nameof(resourceName));
        }

        /// <summary>
        /// Constructs a unique identifier from a resource path, and sets the resource to a previously loaded instance.
        /// </summary>
        /// <param name="resourceName">Path to the resource.</param>
        /// <param name="baseObject">Previously loaded instance.</param>
        public Info(string resourceName, T baseObject)
        {
            this.resourceName = resourceName;
            this.baseObject = baseObject;
        }

        /// <summary>
        /// Name of this resource.
        /// </summary>
        public string Name => Object.name;

        public override string ToString()
        {
            return Object.ToString();
        }

        /// <summary>
        /// Instantiates a clone of the resource.
        /// </summary>
        /// <param name="parent">If not null, sets the clone parent to this.</param>
        /// <returns>An instantiated clone.</returns>
        public T Instantiate(Transform parent = null)
        {
            if (Object == null)
            {
                throw new ResourceNotFoundException("Tried to instantiate from a resource that could not be found.");
            }

            return UnityEngine.Object.Instantiate(Object, parent);
        }

        public bool Equals(Info<T> other)
        {
            return other != null && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Info<T> info && Id == info.Id;
        }

        public static bool operator ==(Info<T> a, Info<T> b)
        {
            return ReferenceEquals(a, b) || (!(a is null) && a.Equals(b));
        }

        public static bool operator !=(Info<T> a, Info<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}