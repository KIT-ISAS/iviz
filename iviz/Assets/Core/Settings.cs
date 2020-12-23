using System;
using JetBrains.Annotations;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using UnityEngine;

namespace Iviz.Core
{
    public static class Settings
    {
        static Settings()
        {
            AotHelper.EnsureType<StringEnumConverter>();
        }

        /// <summary>
        /// Does this device support a way to move the root node?
        /// </summary>
        public const bool IsRootMovable =
#if UNITY_IOS || UNITY_ANDROID || UNITY_WSA
            true;
#else
            false;
#endif

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

        /// <summary>
        /// Is this being run in a Hololens?
        /// </summary>
        public const bool IsHololens =
#if UNITY_WSA
#if UNITY_EDITOR
            false;
#else
            true;
#endif
#else
            false;
#endif
        [NotNull] public static string PersistentDataPath => Application.persistentDataPath;
        [NotNull] public static string SavedFolder => PersistentDataPath + "/saved";
        [NotNull] public static string SimpleConfigurationPath => PersistentDataPath + "/connection.json";
        [NotNull] public static string ResourcesPath => PersistentDataPath + "/resources";
        [NotNull] public static string SavedRobotsPath => PersistentDataPath + "/robots";
        [NotNull] public static string ResourcesFilePath => PersistentDataPath + "/resources.json";

        [CanBeNull] static Camera mainCamera;

        [NotNull]
        public static Camera MainCamera
        {
            get => mainCamera.SafeNull() ??
                   (mainCamera = (GameObject.FindWithTag("MainCamera") ?? GameObject.Find("MainCamera"))
                       .GetComponent<Camera>());
            set => mainCamera = value.SafeNull() ?? throw new NullReferenceException("Camera cannot be null!");
        }
    }
}