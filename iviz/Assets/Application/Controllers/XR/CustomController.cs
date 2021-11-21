#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers
{
    /// <summary>
    /// A simplified version of <see cref="XRMainController"/> for devices that do not have a <see cref="XRNode"/>.
    /// Instead we just wait for a device that "looks like" what we need.  
    /// </summary>
    public abstract class CustomController : XRBaseController
    {
        readonly List<InputFeatureUsage> cachedUsages = new();
        InputDevice? device;

        protected static bool HasFlag(InputDeviceCharacteristics full, InputDeviceCharacteristics flag) => (full & flag) == flag;
        protected static bool HasFlag(InputTrackingState full, InputTrackingState flag) => (full & flag) == flag;
        protected abstract bool MatchesDevice(InputDeviceCharacteristics characteristics,
            List<InputFeatureUsage> usages);

        public bool HasDevice => device != null;
        
        protected bool TryGetDevice(out InputDevice outDevice)
        {
            if (device != null)
            {
                outDevice = device.Value;
                return true;
            }

            outDevice = default;
            return false;
        }
        
        protected override void Awake()
        {
            base.Awake();
            InputDevices.deviceConnected += OnDeviceConnected;
            InputDevices.deviceDisconnected += OnDeviceDisconnected;
        }

        void OnDestroy()
        {
            InputDevices.deviceConnected -= OnDeviceConnected;
            InputDevices.deviceDisconnected -= OnDeviceDisconnected;
        }

        void OnDeviceConnected(InputDevice newDevice)
        {
            cachedUsages.Clear();
            newDevice.TryGetFeatureUsages(cachedUsages);
            
            /*
            Debug.Log(newDevice.name);
            foreach (var featureUsage in cachedUsages)
            {
                Debug.Log("*****    " + featureUsage.name);
            }
            */
            
            if (MatchesDevice(newDevice.characteristics, cachedUsages))
            {
                device = newDevice;
            }
        }

        void OnDeviceDisconnected(InputDevice deadDevice)
        {
            if (deadDevice == device)
            {
                device = null;
            }
        }
    }
}