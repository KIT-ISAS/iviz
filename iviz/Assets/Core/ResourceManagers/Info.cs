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
        [CanBeNull] readonly string resourceName;
        [CanBeNull] T baseObject;

        /// <summary>
        /// Returns or loads a resource of this type.  
        /// </summary>
        [NotNull]
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
                    throw new InvalidOperationException($"Cannot find resource '{resourceName}'");
                }

                return baseObject;
            }
        }

        int id;

        /// <summary>
        /// Returns the instance id of the resource.
        /// </summary>
        public int Id => (id != 0) ? id : (id = Object.GetInstanceID());

        /// <summary>
        /// Constructs a unique identifier from a resource path. The instance will be loaded the first time it is needed.
        /// </summary>
        /// <param name="resourceName">Path to the resource.</param>
        public Info([NotNull] string resourceName) : this(resourceName, null)
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
        public Info([NotNull] T baseObject) : this(null, baseObject)
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException(nameof(baseObject));
            }
        }

        Info([CanBeNull] string resourceName, [CanBeNull] T baseObject) =>
            (this.resourceName, this.baseObject) = (resourceName, baseObject);
        
        
        /// <summary>
        /// Name of this resource.
        /// </summary>
        [NotNull]
        public string Name => Object.name;

        public override string ToString() => Object == null ? "[null]" : Object.ToString();

        /// <summary>
        /// Instantiates a clone of the resource.
        /// </summary>
        /// <param name="parent">If not null, sets the clone parent to this.</param>
        /// <returns>An instantiated clone.</returns>
        public T Instantiate([CanBeNull] Transform parent = null) =>
            Object != null
                ? UnityEngine.Object.Instantiate(Object, parent)
                : throw new ResourceNotFoundException("Tried to instantiate from a resource that could not be found.");

        public bool Equals(Info<T> other) => other != null && Id == other.Id;

        public override bool Equals(object obj) => obj != null && obj is Info<T> info && Id == info.Id;

        public static bool operator ==([CanBeNull] Info<T> a, [CanBeNull] Info<T> b) =>
            ReferenceEquals(a, b) || (!(a is null) && a.Equals(b));

        public static bool operator !=([CanBeNull] Info<T> a, [CanBeNull] Info<T> b) => !(a == b);

        public override int GetHashCode() => Id.GetHashCode();
    }
}