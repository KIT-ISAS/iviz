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
            if ((UnityEngine.Object?)display == null)
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
                ThrowHelper.ThrowArgument("Argument is not a MonoBehavior", nameof(display));
                return; // unreachable
            }

            display.Suspend();
            ResourcePool.Return(resourceKey, behaviour.gameObject);
        }
    }

    public class NoModelLoaderServiceException : Exception
    {
        public NoModelLoaderServiceException(Exception e) : base(
            "Failed to reach the iviz model loader service", e)
        {
        }
    }
}