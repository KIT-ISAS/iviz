using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.Msgs;
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
    }

    public class Screenshot : IDisposable
    {
        public string Format { get; }
        public time Timestamp { get; }
        public int Width { get; }
        public int Height { get; }
        public int Bpp { get; }

        public float Fx { get; }
        public float Cx { get; }
        public float Fy { get; }
        public float Cy { get; }
        public Pose CameraPose { get; }

        public UniqueRef<byte> Bytes { get; }

        public Screenshot(string format, int width, int height, int bpp, float fx, float cx, float fy, float cy,
            in Pose cameraPose, UniqueRef<byte> bytes)
        {
            Format = format;
            Timestamp = time.Now();
            Width = width;
            Height = height;
            Bpp = bpp;
            Fx = fx;
            Cx = cx;
            Fy = fy;
            Cy = cy;
            CameraPose = cameraPose;
            Bytes = bytes;
        }

        public void Dispose()
        {
            Bytes.Dispose();
        }

        public override string ToString()
        {
            return $"[Screenshot width={Width} height={Height} At={CameraPose}]";
        }
    }

    public interface IScreenshotManager
    {
        bool Started { get; }
        IEnumerable<(int width, int height)> GetResolutions();
        Task StartAsync(int width, int height, bool withHolograms);
        Task StopAsync();
        Task<Screenshot> TakeScreenshotColorAsync();
        Task<Screenshot> TakeScreenshotGrey();
    }
}