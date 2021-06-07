using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using UnityEngine;

namespace Iviz.Core
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum QualityType
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh,
        Ultra,
        Mega
    }

    [DataContract]
    public sealed class SettingsConfiguration : JsonToString
    {
        [DataMember] public QualityType QualityInView { get; set; } = QualityType.Ultra;
        [DataMember] public QualityType QualityInAr { get; set; } = QualityType.Ultra;
        [DataMember] public int NetworkFrameSkip { get; set; } = 1;

        [DataMember]
        public int TargetFps { get; set; } = (Settings.IsMobile || Settings.IsHololens) ? Settings.DefaultFps : 60;

        [DataMember] public SerializableColor BackgroundColor { get; set; } = new Color(0.125f, 0.169f, 0.245f);
        [DataMember] public int SunDirection { get; set; } = 0;
    }

    public interface ISettingsManager
    {
        QualityType QualityInView { get; set; }
        QualityType QualityInAr { get; set; }
        int NetworkFrameSkip { get; set; }
        int TargetFps { get; set; }
        Color BackgroundColor { get; set; }
        int SunDirection { get; set; }
        [NotNull] SettingsConfiguration Config { get; set; }

        bool SupportsView { get; }
        bool SupportsAR { get; }
        [NotNull] IEnumerable<string> QualityLevelsInView { get; }
        [NotNull] IEnumerable<string> QualityLevelsInAR { get; }
    }


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
        public const bool IsHololens =
#if UNITY_WSA
#if UNITY_EDITOR
            true;
#else
            true;
#endif
#else
            false;
#endif

        public static bool IsVR { get; set; }
        public static bool IsVRButtonDown { get; set; }
        
        
        static string persistentDataPath;
        static string savedFolder;
        static string bagsFolder;
        static string simpleConfigurationPath;
        static string resourcesPath;
        static string savedRobotsPath;
        static string resourcesFilePath;


        [NotNull]
        static string PersistentDataPath =>
            persistentDataPath ?? (persistentDataPath = Application.persistentDataPath);

        [NotNull] public static string SavedFolder => savedFolder ?? (savedFolder = PersistentDataPath + "/saved");

        [NotNull] public static string BagsFolder => bagsFolder ?? (bagsFolder = PersistentDataPath + "/bags");

        [NotNull]
        public static string SimpleConfigurationPath =>
            simpleConfigurationPath ?? (simpleConfigurationPath = $"{PersistentDataPath}/connection.json");

        [NotNull]
        public static string ResourcesPath => resourcesPath ?? (resourcesPath = $"{PersistentDataPath}/resources");

        [NotNull]
        public static string SavedRobotsPath => savedRobotsPath ?? (savedRobotsPath = $"{PersistentDataPath}/robots");

        [NotNull]
        public static string ResourcesFilePath =>
            resourcesFilePath ?? (resourcesFilePath = $"{PersistentDataPath}/resources.json");

        [CanBeNull] static Camera mainCamera;
        [CanBeNull] static Transform mainCameraTransform;

        [NotNull]
        public static Transform MainCameraTransform => mainCameraTransform != null
            ? mainCameraTransform
            : (mainCameraTransform = MainCamera.transform);

        [NotNull]
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
        
        [CanBeNull] public static Camera ARCamera { get; set; }
        public static event Action<QualityType> QualityTypeChanged;

        public static void RaiseQualityTypeChanged(QualityType newQualityType)
        {
            QualityTypeChanged?.Invoke(newQualityType);
        }
        
        [CanBeNull] public static ISettingsManager SettingsManager { get; set; }
        [CanBeNull] public static IScreenshotManager ScreenshotManager { get; set; }

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
    
    public interface IScreenshotManager
    {
        bool Started { get; }
        IEnumerable<(int width, int height)> GetResolutions();
        Task StartAsync(int width, int height, bool withHolograms);
        Task StopAsync();
        Task<Screenshot> TakeScreenshotColorAsync();
    }
}