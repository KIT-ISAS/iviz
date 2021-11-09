#nullable enable

using System;
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
        public const bool IsMobile =
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID || UNITY_WSA)
            true;
#else
            false;
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
        public const bool IsPhone =
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            true;
#else
            false;
#endif

        public const bool IsIphone =
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
        public const bool IsVR = false;
#else
        public static bool IsHololens { get; set; }
        public static bool IsVR { get; set; }

#endif

        static string? persistentDataPath;

        static string? savedFolder;
        static string? bagsFolder;
        static string? simpleConfigurationPath;
        static string? resourcesPath;
        static string? savedRobotsPath;
        static string? resourcesFilePath;

        static string PersistentDataPath => persistentDataPath ??= Application.persistentDataPath;
        public static string SavedFolder => savedFolder ??= PersistentDataPath + "/saved";
        public static string BagsFolder => bagsFolder ??= PersistentDataPath + "/bags";

        public static string SimpleConfigurationPath =>
            simpleConfigurationPath ??= $"{PersistentDataPath}/connection.json";

        public static string ResourcesPath => resourcesPath ??= $"{PersistentDataPath}/resources";
        public static string SavedRobotsPath => savedRobotsPath ??= $"{PersistentDataPath}/robots";
        public static string ResourcesFilePath => resourcesFilePath ??= $"{PersistentDataPath}/resources.json";

        static Camera? mainCamera;
        static Transform? mainCameraTransform;

        public static Transform MainCameraTransform => mainCameraTransform != null
            ? mainCameraTransform
            : (mainCameraTransform = MainCamera.transform);

        public static Camera MainCamera
        {
            get => mainCamera != null
                ? mainCamera
                : mainCamera = (GameObject.FindWithTag("MainCamera").CheckedNull()
                                ?? GameObject.Find("MainCamera").CheckedNull()
                                ?? throw new NullReferenceException("Failed to find camera!"))
                    .GetComponent<Camera>();
            set
            {
                mainCamera = value.CheckedNull() ?? throw new NullReferenceException("Camera cannot be null!");
                mainCameraTransform = value.transform;
            }
        }

        public static Camera? ARCamera { get; set; }
        public static event Action<QualityType>? QualityTypeChanged;

        public static void RaiseQualityTypeChanged(QualityType newQualityType)
        {
            QualityTypeChanged?.Invoke(newQualityType);
        }

        public static ISettingsManager? SettingsManager { get; set; }
        public static IScreenCaptureManager? ScreenCaptureManager { get; set; }

        static bool? supportsComputeBuffersHelper;

        public static bool SupportsComputeBuffers => supportsComputeBuffersHelper ??
                                                     (supportsComputeBuffersHelper =
                                                         !IsHololens &&
                                                         SystemInfo.supportsComputeShaders &&
                                                         SystemInfo.maxComputeBufferInputsVertex > 0).Value;

        static bool? supportsR16;

        public static bool SupportsR16 => supportsR16 ??
                                          (supportsR16 = SystemInfo.SupportsTextureFormat(TextureFormat.R16)).Value;

        static bool? supportsRGB24;

        public static bool SupportsRGB24 => supportsRGB24 ??
                                            (supportsRGB24 = SystemInfo.SupportsTextureFormat(TextureFormat.RGB24))
                                            .Value;

        static Settings()
        {
            AotHelper.EnsureType<StringEnumConverter>();
        }
    }
}