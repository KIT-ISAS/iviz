#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Core
{
    public static class ResourceUtils
    {
        /// <summary>
        /// Returns a rented display to the <see cref="ResourcePool"/>.
        /// </summary>
        /// <param name="display">The object to return.</param>
        public static void ReturnToPool(this IDisplay? display)
        {
            if (display == null)
            {
                return;
            }

            display.Suspend();
            ResourcePool.ReturnDisplay(display);
        }

        /// <summary>
        /// Returns a rented display to the <see cref="ResourcePool"/> using the given key as the resource type.
        /// Used for displays for which the resource type cannot be obtained automatically.
        /// </summary>
        /// <param name="display">The object to return.</param>
        /// <param name="resourceKey">The resource type to use.</param>
        public static void ReturnToPool(this IDisplay? display, ResourceKey<GameObject> resourceKey)
        {
            if (display == null)
            {
                return;
            }
            
            if (display is not MonoBehaviour behaviour)
            {
                throw new ArgumentException("Argument is not a MonoBehavior");
            }            

            display.Suspend();
            ResourcePool.Return(resourceKey, behaviour.gameObject);
        }
    }

    /// <summary>
    /// Thrown when an asset is missing but is expected to be there. Only used for internal iviz errors.
    /// </summary>s
    public class MissingAssetFieldException : Exception
    {
        public MissingAssetFieldException(string message) : base(message)
        {
        }
    }

    public class NoModelLoaderServiceException : Exception
    {
        public NoModelLoaderServiceException(string message, Exception e) : base(message, e)
        {
        }
    }
}