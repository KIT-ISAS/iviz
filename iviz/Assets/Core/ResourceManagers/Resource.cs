#nullable enable

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Tools;
using UnityEngine;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.Resources
{
    /// <summary>
    /// Class that simplifies access to assets.
    /// </summary>
    public static class Resource
    {
        static ResourceCore? instance;
        static ResourceCore Instance => instance ??= new ResourceCore();

        public static ColorSchema Colors { get; } = new(); // ColorSchema contains only constants, no refs
        public static MaterialsType Materials => Instance.materials ??= new MaterialsType();
        public static ColormapsType Colormaps => Instance.colormaps ??= new ColormapsType();
        public static DisplaysType Displays => Instance.displays ??= new DisplaysType();
        public static WidgetsType Widgets => Instance.widgets ??= new WidgetsType();
        public static AudioType Audio => Instance.audio ??= new AudioType();
        public static TexturedMaterialsType TexturedMaterials => Instance.textureds ??= new TexturedMaterialsType();
        public static FontInfo Font => Instance.fontInfo ??= new FontInfo();
        public static InternalResourceManager Internal => Instance.internals ??= new InternalResourceManager();
        public static ExternalResourceManager External => Instance.externals ??= new ExternalResourceManager();

        public static bool TryGetResourceByRosMessageType(string type, out ModuleType module) =>
            Instance.resourceByRosMessageType.TryGetValue(type, out module);

        public static bool IsRobotSaved(string robotName) =>
            Internal.ContainsRobot(robotName) ||
            External.ContainsRobot(robotName);

        public static IEnumerable<string> GetRobotNames() => Internal.GetRobotNames().Concat(External.GetRobotNames());

        public static ValueTask<(bool result, string robotDescription)> TryGetRobotAsync(string robotName,
            CancellationToken token = default) =>
            Internal.TryGetRobot(robotName, out string? robotDescription)
                ? (true, robotDescription).AsTaskResult()
                : External.TryGetRobotAsync(robotName, token);

        public static bool TryGetResource(string uriString, [NotNullWhen(true)] out ResourceKey<GameObject>? info) =>
            Internal.TryGet(uriString, out info) || External.TryGetGameObject(uriString, out info);

        public static ValueTask<ResourceKey<GameObject>?> GetGameObjectResourceAsync(
            string uriString,
            IServiceProvider? provider,
            CancellationToken token) =>
            Internal.TryGet(uriString, out ResourceKey<GameObject>? info)
                ? info.AsTaskResultMaybeNull()
                : External.TryGetGameObjectAsync(uriString, provider, token);

        internal static ValueTask<ResourceKey<Texture2D>?> GetTextureResourceAsync(
            string uriString,
            IServiceProvider? provider,
            CancellationToken token) =>
            Internal.TryGet(uriString, out ResourceKey<Texture2D>? info)
                ? info.AsTaskResultMaybeNull()
                : External.TryGetTextureAsync(uriString, provider, token);

        public static void ClearResources() => instance = null;

        sealed class ResourceCore
        {
            public MaterialsType? materials;
            public ColormapsType? colormaps;
            public DisplaysType? displays;
            public WidgetsType? widgets;
            public AudioType? audio;
            public TexturedMaterialsType? textureds;
            public FontInfo? fontInfo;
            public InternalResourceManager? internals;
            public ExternalResourceManager? externals;

            /// <summary>
            /// Dictionary that describes which module handles which ROS message type.
            /// </summary>
            public readonly Dictionary<string, ModuleType> resourceByRosMessageType = new()
            {
                { PointCloud2.RosMessageType, ModuleType.PointCloud },
                { Image.RosMessageType, ModuleType.Image },
                { CompressedImage.RosMessageType, ModuleType.Image },
                { Marker.RosMessageType, ModuleType.Marker },
                { MarkerArray.RosMessageType, ModuleType.Marker },
                { InteractiveMarkerUpdate.RosMessageType, ModuleType.InteractiveMarker },
                { LaserScan.RosMessageType, ModuleType.LaserScan },
                { PoseStamped.RosMessageType, ModuleType.Magnitude },
                { Pose.RosMessageType, ModuleType.Magnitude },
                { PointStamped.RosMessageType, ModuleType.Magnitude },
                { Point.RosMessageType, ModuleType.Magnitude },
                { WrenchStamped.RosMessageType, ModuleType.Magnitude },
                { Wrench.RosMessageType, ModuleType.Magnitude },
                { Odometry.RosMessageType, ModuleType.Magnitude },
                { TwistStamped.RosMessageType, ModuleType.Magnitude },
                { Twist.RosMessageType, ModuleType.Magnitude },
                { OccupancyGrid.RosMessageType, ModuleType.OccupancyGrid },
                { GridMap.RosMessageType, ModuleType.GridMap },
                { WidgetArray.RosMessageType, ModuleType.GuiWidget },
                
                // these are already implemented, but need refinement
                //{ JointState.RosMessageType, ModuleType.JointState },
                //{ Path.RosMessageType, ModuleType.Path },
                //{ PoseArray.RosMessageType, ModuleType.Path },
                //{ PolygonStamped.RosMessageType, ModuleType.Path },
                //{ Polygon.RosMessageType, ModuleType.Path },
                //{ Octomap.RosMessageType, ModuleType.Octomap },
                //{ OctomapWithPose.RosMessageType, ModuleType.Octomap },
            };
        }
    }
}