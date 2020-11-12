using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Core
{
    public static class Settings
    {

        /// <summary>
        /// Is this being run on an Android, IOS, or Hololens device?
        /// </summary>
        public const bool IsMobile =
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
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
#if !UNITY_EDITOR && UNITY_WSA
            // bug: this will activate with any UWP device, not only Hololens! but what else? 
            true;
#else
            false;
#endif                
        
        [NotNull] public static string PlatformName => UnityEngine.Application.platform.ToString().ToLower();
        [NotNull] public static string PersistentDataPath => UnityEngine.Application.persistentDataPath;
        [NotNull] public static string SavedFolder => PersistentDataPath + "/saved";
        [NotNull] public static string SimpleConfigurationPath => PersistentDataPath + "/connection.json";
        [NotNull] public static string ResourcesPath => PersistentDataPath + "/resources";
        [NotNull] public static string SavedRobotsPath => PersistentDataPath + "/robots";
        [NotNull] public static string ResourcesFilePath => PersistentDataPath + "/resources.json";
        public static Camera MainCamera { get; set; }
    }
}