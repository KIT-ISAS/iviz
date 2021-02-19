using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Roslib;
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

        static Settings()
        {
            AotHelper.EnsureType<StringEnumConverter>();
        }

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
        static string persistentDataPath;
        static string savedFolder;
        static string simpleConfigurationPath;
        static string resourcesPath;
        static string savedRobotsPath;
        static string resourcesFilePath;


        [NotNull]
        public static string PersistentDataPath =>
            persistentDataPath ?? (persistentDataPath = Application.persistentDataPath);

        [NotNull] public static string SavedFolder => savedFolder ?? (savedFolder = PersistentDataPath + "/saved");

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
                : mainCamera = (GameObject.FindWithTag("MainCamera") ?? GameObject.Find("MainCamera"))
                    .GetComponent<Camera>();
            set
            {
                mainCamera = value != null ? value : throw new NullReferenceException("Camera cannot be null!");
                mainCameraTransform = value.transform;
            }
        }

        [CanBeNull] public static ISettingsManager SettingsManager { get; set; }
    }
}