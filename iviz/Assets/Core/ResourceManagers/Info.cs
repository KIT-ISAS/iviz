#nullable enable

using System;
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
        readonly string? resourceName;
        T? baseObject;
        int id;

        /// <summary>
        /// Returns or loads a resource of this type.  
        /// </summary>
        public T Object
        {
            get
            {
                if (baseObject is not null)
                {
                    return baseObject;
                }

                if (resourceName == null)
                {
                    throw new ResourceNotFoundException($"Cannot find resource [empty]");
                }

                baseObject = UnityEngine.Resources.Load<T>(resourceName);
                if (baseObject == null)
                {
                    throw new ResourceNotFoundException($"Cannot find resource '{resourceName}'");
                }

                return baseObject;
            }
        }

        /// <summary>
        /// Returns the instance id of the resource.
        /// </summary>
        public int Id => (id != 0) ? id : (id = Object.GetInstanceID());

        /// <summary>
        /// Constructs a unique identifier from a resource path. The instance will be loaded the first time it is needed.
        /// </summary>
        /// <param name="resourceName">Path to the resource.</param>
        public Info(string resourceName) : this(resourceName, null)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }
        }

        /// <summary>
        /// Constructs a unique identifier from an existing instance
        /// </summary>
        /// <param name="baseObject">Loaded instance of this type, if already available.</param>
        /// <param name="objectName">>Optional descriptive name for this object, in case of an error.</param>
        public Info(T baseObject, string? objectName = null) : this(null, baseObject)
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException(objectName ?? nameof(baseObject));
            }
        }

        Info(string? resourceName, T? baseObject) =>
            (this.resourceName, this.baseObject) = (resourceName, baseObject);


        /// <summary>
        /// Name of this resource.
        /// </summary>
        public string Name => Object.name;

        public override string ToString() => Object.ToString();

        /// <summary>
        /// Instantiates a clone of the resource.
        /// </summary>
        /// <param name="parent">Sets the clone's parent to this transform.</param>
        /// <returns>An instantiated clone.</returns>
        public T Instantiate(Transform? parent = null) => UnityEngine.Object.Instantiate(Object, parent);

        public bool Equals(Info<T>? other) => other != null && Id == other.Id;

        public override bool Equals(object obj) => obj is Info<T> info && Id == info.Id;

        public static bool operator ==(Info<T>? a, Info<T>? b) =>
            ReferenceEquals(a, b) || (a is not null && a.Equals(b));

        public static bool operator !=(Info<T>? a, Info<T>? b) => !(a == b);

        public override int GetHashCode() => Id.GetHashCode();
    }
}