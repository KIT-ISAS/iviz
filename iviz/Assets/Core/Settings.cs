#nullable enable

using System;
using Iviz.Common;
using Iviz.Core.XR;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using UnityEngine;

namespace Iviz.Core
{
    public static class Settings
    {
        public const int DefaultFps = -1;

        /// <summary>
        /// Is this being run on an Android, IOS, or Hololens device?
        /// </summary>
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
        public const bool IsMobile = true;
#elif UNITY_WSA || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        public static bool IsMobile => IsHololens;
#else
        public const bool IsMobile = false;
#endif

        public const bool IsStandalone =
#if !UNITY_EDITOR && UNITY_STANDALONE
            true;
#else
            false;
#endif

        /// <summary>
        /// Is this being run on an Android or IOS device? (smartphone or tablet)
        /// </summary>
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        public const bool IsPhone = true;
#else
        public static bool IsPhone => false;
#endif

        public const bool IsIPhone =
#if !UNITY_EDITOR && UNITY_IOS
            true;
#else
            false;
#endif

        public const bool IsAndroid =
#if !UNITY_EDITOR && UNITY_ANDROID
            true;
#else
            false;
#endif

        public const bool IsLinux =
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
            true;
#else
            false;
#endif
        
        public const bool IsMacOS =
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            true;
#else
            false;
#endif        

#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        /// <summary>
        /// Is this being run in a Hololens?
        /// </summary>
        public const bool IsHololens = false;

        /// <summary>
        /// Is this being run on an XR platform? (VR or Hololens)
        /// </summary>
        public const bool IsXR = false;
#else
        static bool? isHololens;

        static bool? isXR;

        static bool TryReadXRInfo()
        {
            if (isHololens != null && isXR != null)
            {
                return true;
            }

            if (GameObject.Find("iviz") is not { } ivizObject
                || !ivizObject.TryGetComponent<XRStatusInfo>(out var info))

            {
                return false;
            }

            isHololens = info.IsHololens;
            isXR = info.IsXREnabled;
            return true;
        }

        /// <summary>
        /// Is this being run in a Hololens?
        /// </summary>
        public static bool IsHololens =>
            TryReadXRInfo() && (isHololens ??
                                throw new InvalidOperationException("Could not check if we are running in a Hololens"));

        /// <summary>
        /// Is this being run on an XR platform? (VR or Hololens)
        /// </summary>
        public static bool IsXR =>
            TryReadXRInfo() && (isXR ?? throw new InvalidOperationException("Could not check if we are running in XR"));
#endif

        public static void ClearResources()
        {
#if !(UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
            isHololens = null;
            isXR = null;
#endif

            mainCamera = null;
            mainCameraTransform = null;
            settingsManager = null;
            inputModule = null;

            supportsR16 = null;
            supportsRGB24 = null;
            supportsComputeBuffersHelper = null;

            VirtualCamera = null;
            ARCamera = null;
            ScreenCaptureManager = null;
        }

        /// <summary>
        /// Does this device support mobile AR? (smartphone, tablet, not Hololens)
        /// </summary>
        public static bool SupportsMobileAR => IsPhone;

        static string? persistentDataPath;
        static string? savedFolder;
        static string? bagsFolder;
        static string? simpleConfigurationPath;
        static string? xrStartConfigurationPath;
        static string? resourcesPath;
        static string? savedRobotsPath;
        static string? resourcesFilePath;

        static Camera? mainCamera;
        static Transform? mainCameraTransform;
        static ISettingsManager? settingsManager;
        static IDragHandler? inputModule;

        static bool? supportsComputeBuffersHelper;
        static bool? supportsR16;
        static bool? supportsRGB24;

        static string PersistentDataPath => persistentDataPath ??= Application.persistentDataPath;
        public static string SavedFolder => savedFolder ??= PersistentDataPath + "/saved";
        public static string BagsFolder => bagsFolder ??= PersistentDataPath + "/bags";

        public static string SimpleConfigurationPath =>
            simpleConfigurationPath ??= $"{PersistentDataPath}/connection.json";

        public static string XRStartConfigurationPath =>
            xrStartConfigurationPath ??= $"{PersistentDataPath}/xr_start.json";

        public static string ResourcesPath => resourcesPath ??= $"{PersistentDataPath}/resources";
        public static string SavedRobotsPath => savedRobotsPath ??= $"{PersistentDataPath}/robots";
        public static string ResourcesFilePath => resourcesFilePath ??= $"{PersistentDataPath}/resources.json";

        public static bool UseSimpleMaterials { get; set; }

        public static GameObject FindMainCamera() =>
            GameObject.FindWithTag("MainCamera").CheckedNull()
            ?? GameObject.Find("MainCamera").CheckedNull()
            ?? throw new MissingAssetFieldException("Failed to find camera!");

        public static Camera MainCamera
        {
            get => mainCamera != null
                ? mainCamera
                : mainCamera = FindMainCamera().AssertHasComponent<Camera>(nameof(mainCamera));

            set
            {
                mainCamera = value.CheckedNull() ?? throw new NullReferenceException("Camera cannot be null!");
                mainCameraTransform = value.transform;
            }
        }

        public static Transform MainCameraTransform =>
            mainCameraTransform != null
                ? mainCameraTransform
                : (mainCameraTransform = MainCamera.transform);

        public static Camera? VirtualCamera { get; set; }
        public static Camera? ARCamera { get; set; }

        public static ISettingsManager SettingsManager =>
            (UnityEngine.Object?)settingsManager != null
                ? settingsManager
                : settingsManager = FindMainCamera().GetComponent<ISettingsManager>()
                                    ?? throw new MissingAssetFieldException("Failed to find SettingsManager!");

        public static bool HasDragHandler => inputModule != null;

        public static IDragHandler DragHandler =>
            (UnityEngine.Object?)inputModule != null
                ? inputModule
                : inputModule = (IDragHandler)SettingsManager;

        public static IScreenCaptureManager? ScreenCaptureManager { get; set; }

        public static bool SupportsComputeBuffers => supportsComputeBuffersHelper ??
                                                     (supportsComputeBuffersHelper =
                                                         !IsHololens &&
                                                         SystemInfo.supportsComputeShaders &&
                                                         SystemInfo.maxComputeBufferInputsVertex > 0).Value;
        public static bool SupportsR16 => supportsR16 ??
                                          (supportsR16 = SystemInfo.SupportsTextureFormat(TextureFormat.R16)).Value;

        public static bool SupportsRGB24 => supportsRGB24 ??
                                            (supportsRGB24 = SystemInfo.SupportsTextureFormat(TextureFormat.RGB24))
                                            .Value;

        /// <summary>
        /// Set when the app is being closed.
        /// This prevents some disposal operations that depend on objects that may have been already destroyed.
        /// </summary>
        public static bool IsShuttingDown { get; set; }
        
        static Settings()
        {
            AotHelper.EnsureType<StringEnumConverter>();
        }
    }

    public interface IDragHandler
    {
        void TryUnsetDraggedObject(IScreenDraggable draggable);
        void TrySetDraggedObject(IScreenDraggable draggable);
        float XRDraggableNearDistance { get; }
    }
}