using UnityEngine;

namespace Iviz.Core
{
    public static class Settings
    {

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
#if !UNITY_EDITOR && UNITY_WSA
            // bug: this will activate with any UWP device, not only Hololens! but what else? 
            true;
#else
            false;
#endif                
        
        public static string PlatformName => UnityEngine.Application.platform.ToString().ToLower();
        public static string PersistentDataPath => UnityEngine.Application.persistentDataPath;
        public static string SavedFolder => PersistentDataPath + "/saved";
        public static string SimpleConfigurationPath => PersistentDataPath + "/connection.json";
        public static string ResourcesPath => PersistentDataPath + "/resources";
        public static string SavedRobotsPath => PersistentDataPath + "/robots";
        public static string ResourcesFilePath => PersistentDataPath + "/resources.json";
        public static Camera MainCamera { get; set; }
    }
}