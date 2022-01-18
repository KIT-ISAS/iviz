#nullable enable

using System;
using Iviz.Common;
using Iviz.Controllers.XR;
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
#else
        public static bool IsMobile => IsHololens;
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

        /// <summary>
        /// Is this being run in a Hololens?
        /// </summary>
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        public const bool IsHololens = false;
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

            if (!GameObject.Find("iviz").TryGetComponent<XRStatusInfo>(out var info))
            {
                return false;
            }

            isHololens = info.IsHololens;
            isXR = info.IsXREnabled;
            return true;
        }

        public static bool IsHololens =>
            TryReadXRInfo() && (isHololens ??
                                throw new InvalidOperationException("Could not check if we are running in a Hololens"));

        public static bool IsXR =>
            TryReadXRInfo() && (isXR ?? throw new InvalidOperationException("Could not check if we are running in XR"));

#endif

        public static bool SupportsAR => IsPhone;

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

        static bool? supportsComputeBuffersHelper;
        static bool? supportsR16;
        static bool? supportsRGB24;

        static string PersistentDataPath => persistentDataPath ??= Application.persistentDataPath;
        public static string SavedFolder => savedFolder ??= PersistentDataPath + "/saved";
        public static string BagsFolder => bagsFolder ??= PersistentDataPath + "/bags";

        public static string SimpleConfigurationPath =>
            simpleConfigurationPath ??= $"{PersistentDataPath}/connection.json";

        public static string XRStartConfigurationPath => xrStartConfigurationPath ??= $"{PersistentDataPath}/xr_start.json";

        public static string ResourcesPath => resourcesPath ??= $"{PersistentDataPath}/resources";
        public static string SavedRobotsPath => savedRobotsPath ??= $"{PersistentDataPath}/robots";
        public static string ResourcesFilePath => resourcesFilePath ??= $"{PersistentDataPath}/resources.json";

        public static GameObject FindMainCamera() =>
            GameObject.FindWithTag("MainCamera").CheckedNull()
            ?? GameObject.Find("MainCamera").CheckedNull()
            ?? throw new MissingAssetFieldException("Failed to find camera!");

        public static Camera MainCamera
        {
            get => mainCamera != null
                ? mainCamera
                : mainCamera = FindMainCamera().GetComponent<Camera>().AssertNotNull(nameof(mainCamera));
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

        public static Camera? ARCamera { get; set; }
        public static event Action<QualityType>? QualityTypeChanged;

        public static void RaiseQualityTypeChanged(QualityType newQualityType)
        {
            QualityTypeChanged?.Invoke(newQualityType);
        }

        public static ISettingsManager SettingsManager =>
            (UnityEngine.Object?)settingsManager != null
                ? settingsManager
                : settingsManager = FindMainCamera().GetComponent<ISettingsManager>()
                                    ?? throw new MissingAssetFieldException("Failed to find SettingsManager!");

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

        static Settings()
        {
            AotHelper.EnsureType<StringEnumConverter>();
        }
    }
}