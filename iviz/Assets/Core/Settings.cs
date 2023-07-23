#nullable enable

using System;
using Iviz.Common;
using Iviz.Core.XR;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

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
        //public static bool IsMobile => IsHololens;
        public const bool IsMobile = false;
#elif UNITY_EDITOR
        //public static bool IsMobile => false; // don't set as constant, need for debugging
        public const bool IsMobile = false;
#else
        public const bool IsMobile = false;
#endif


#if !UNITY_EDITOR
        public const bool IsStandalone = true;
#else
        public const bool IsStandalone = false;
#endif

        /// <summary>
        /// Is this being run on an Android or IOS device? (smartphone or tablet)
        /// </summary>
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        public const bool IsPhone = true;
#else
        public const bool IsPhone = false;
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
        
        public const bool IsWSA =
#if UNITY_WSA
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

#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX || UNITY_STANDALONE
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

        static bool TryReadXRInfo(out bool newIsHololens, out bool newIsXR)
        {
            if (GameObject.Find("iviz") is not { } ivizObject
                || !ivizObject.TryGetComponent<XRStatusInfo>(out var info))

            {
                newIsHololens = false;
                newIsXR = false;
                return false;
            }

            isHololens = newIsHololens = info.IsHololens;
            isXR = newIsXR = info.IsXREnabled;
            return true;
        }

        /// <summary>
        /// Is this being run in a Hololens?
        /// </summary>
        public static bool IsHololens =>
            isHololens ?? 
            (TryReadXRInfo(out bool newIsHololens, out _) 
                ? newIsHololens 
                : throw new InvalidOperationException("Could not check if we are running in a Hololens"));

        /// <summary>
        /// Is this being run on an XR platform? (VR or Hololens)
        /// </summary>
        public static bool IsXR =>
            isXR ?? 
            (TryReadXRInfo(out _, out bool newIsXR) 
                ? newIsXR
                : throw new InvalidOperationException("Could not check if we are running in XR"));
#endif

        public static void ClearResources()
        {
#if !(UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX || UNITY_STANDALONE)
            isHololens = null;
            isXR = null;
#endif

            mainCamera = null;
            mainCameraTransform = null;
            settingsManager = null;
            dragHandler = null;

            supportsR16 = null;
            supportsRGB24 = null;
            supportsComputeBuffersHelper = null;

            VirtualCamera = null;
            ARCamera = null;
            ScreenCaptureManager = null;

            // just set these values
            _ = SupportsR16;
            _ = SupportsRGB24;
            _ = MaxTextureSize;
        }

        /// <summary>
        /// Does this device support mobile AR? (smartphone, tablet, not Hololens)
        /// </summary>
        public static bool SupportsMobileAR => IsPhone;

        static string? persistentDataPath;
        static string? savedFolder;
        static string? bagsFolder;
        static string? vncFolder;
        static string? simpleConfigurationPath;
        static string? xrStartConfigurationPath;
        static string? resourcesPath;
        static string? savedRobotsPath;
        static string? resourcesFilePath;
        static string? ros2Folder;

        static Camera? mainCamera;
        static Transform? mainCameraTransform;
        static ISettingsManager? settingsManager;
        static IDragHandler? dragHandler;

        static bool? supportsComputeBuffersHelper;
        static bool? supportsR16;
        static bool? supportsRGB24;
        static int? maxTextureSize;

        static string PersistentDataPath => persistentDataPath ??= Application.persistentDataPath;
        public static string SavedFolder => savedFolder ??= PersistentDataPath + "/saved";
        public static string BagsFolder => bagsFolder ??= PersistentDataPath + "/bags";
        public static string VncFolder => vncFolder ??= PersistentDataPath + "/vnc";
        public static string Ros2Folder => ros2Folder ??= PersistentDataPath + "/ros2";

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

        /// <summary>
        /// Cached transform of the main camera. May refer to different objects depending on the view.
        /// </summary>
        public static Camera MainCamera
        {
            get
            {
#if UNITY_EDITOR
                if (mainCamera != null)
#else
                if (mainCamera is not null)
#endif
                {
                    return mainCamera;
                }

                mainCamera = FindMainCamera().AssertHasComponent<Camera>(nameof(mainCamera));
                mainCameraTransform = mainCamera.transform;
                return mainCamera;
            }

            set
            {
                mainCamera = value.CheckedNull() ?? throw new NullReferenceException("Camera cannot be null!");
                mainCameraTransform = value.transform;
            }
        }

        /// <summary>
        /// Cached transform of the main camera. May refer to different objects depending on the view.
        /// </summary>
        public static Transform MainCameraTransform =>
#if UNITY_EDITOR
            mainCameraTransform != null
                ? mainCameraTransform
                : (mainCameraTransform = MainCamera.transform);
#else
            mainCameraTransform ??= MainCamera.transform;
#endif

        /// <summary>
        /// Cached absolute pose of <see cref="MainCameraTransform"/>. Updated each frame.
        /// </summary>
        public static Pose MainCameraPose;

        /// <summary>
        /// Camera for the birds-eye-view mode.
        /// Always null for Hololens / VR
        /// </summary>
        public static Camera? VirtualCamera { get; set; }

        /// <summary>
        /// Camera for AR. Always null for non-mobile.
        /// </summary>
        public static Camera? ARCamera { get; set; }

        public static ISettingsManager SettingsManager
        {
            get => settingsManager ?? throw new NullReferenceException("No settings manager!");
            set => settingsManager = value;
        }

        public static IDragHandler DragHandler
        {
            get => dragHandler ?? throw new NullReferenceException("No drag handler!");
            set => dragHandler = value;
        }

        public static IScreenCaptureManager? ScreenCaptureManager { get; set; }

        public static bool SupportsComputeBuffers => supportsComputeBuffersHelper ??=
            !IsHololens && SystemInfo.supportsComputeShaders && SystemInfo.maxComputeBufferInputsVertex > 0;

        public static bool SupportsR16 => supportsR16 ??= SystemInfo.SupportsTextureFormat(TextureFormat.R16);
        public static bool SupportsRGB24 => supportsRGB24 ??= SystemInfo.SupportsTextureFormat(TextureFormat.RGB24);
        public static int MaxTextureSize => maxTextureSize ??= Mathf.Min(4096, SystemInfo.maxTextureSize);

        /// <summary>
        /// Set when the app is being closed.
        /// This prevents some disposal operations that depend on objects that may have been already destroyed.
        /// </summary>
#if UNITY_EDITOR
        public static bool IsShuttingDown { get; set; }
#else
        public const bool IsShuttingDown = false;
#endif

        [Preserve]
        public static void InitializeAOT()
        {
            AotHelper.EnsureType<StringEnumConverter>();
        }
    }

    public interface IDragHandler
    {
        bool IsDragging { get; }
        void TryUnsetDraggedObject(IScreenDraggable draggable);
        void TrySetDraggedObject(IScreenDraggable draggable);
        float XRDraggableNearDistance { get; }
    }
}