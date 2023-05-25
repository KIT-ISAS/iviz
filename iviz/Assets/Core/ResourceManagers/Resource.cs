#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.GridMapMsgs;
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

        public static ColorSchema Colors => Instance.colors; // ColorSchema contains only constants, no refs
        public static MaterialsType Materials => Instance.materials ??= new MaterialsType();
        public static ColormapsType Colormaps => Instance.colormaps ??= new ColormapsType();
        public static DisplaysType Displays => Instance.displays ??= new DisplaysType();
        public static WidgetsType Widgets => Instance.widgets ??= new WidgetsType();
        public static AudioType Audio => Instance.audio ??= new AudioType();
        public static TexturedMaterialsType TexturedMaterials => Instance.texturedMaterials ??= new();
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
            ServiceProvider? provider,
            CancellationToken token) =>
            Internal.TryGet(uriString, out ResourceKey<GameObject>? info)
                ? info.AsTaskResultMaybeNull()
                : External.TryGetGameObjectAsync(uriString, provider, token);

        internal static ValueTask<ResourceKey<Texture2D>?> GetTextureResourceAsync(
            string uriString,
            ServiceProvider? provider,
            CancellationToken token) =>
            Internal.TryGet(uriString, out ResourceKey<Texture2D>? info)
                ? info.AsTaskResultMaybeNull()
                : External.TryGetTextureAsync(uriString, provider, token);

        public static void ClearResources() => instance = null;

        sealed class ResourceCore
        {
            public readonly ColorSchema colors = new();
            public MaterialsType? materials;
            public ColormapsType? colormaps;
            public DisplaysType? displays;
            public WidgetsType? widgets;
            public AudioType? audio;
            public TexturedMaterialsType? texturedMaterials;
            public FontInfo? fontInfo;
            public InternalResourceManager? internals;
            public ExternalResourceManager? externals;

            /// <summary>
            /// Dictionary that describes which module handles which ROS message type.
            /// </summary>
            public readonly Dictionary<string, ModuleType> resourceByRosMessageType = new()
            {
                { PointCloud2.MessageType, ModuleType.PointCloud },
                { Image.MessageType, ModuleType.Image },
                { CompressedImage.MessageType, ModuleType.Image },
                { Marker.MessageType, ModuleType.Marker },
                { MarkerArray.MessageType, ModuleType.Marker },
                { InteractiveMarkerUpdate.MessageType, ModuleType.InteractiveMarker },
                { LaserScan.MessageType, ModuleType.LaserScan },
                { PoseStamped.MessageType, ModuleType.Magnitude },
                //{ Pose.MessageType, ModuleType.Magnitude },
                { PointStamped.MessageType, ModuleType.Magnitude },
                //{ Point.MessageType, ModuleType.Magnitude },
                { WrenchStamped.MessageType, ModuleType.Magnitude },
                //{ Wrench.MessageType, ModuleType.Magnitude },
                { Odometry.MessageType, ModuleType.Magnitude },
                { TwistStamped.MessageType, ModuleType.Magnitude },
                { Twist.MessageType, ModuleType.Magnitude },
                { OccupancyGrid.MessageType, ModuleType.OccupancyGrid },
                { GridMap.MessageType, ModuleType.GridMap },

                { Widget.MessageType, ModuleType.VizWidget },
                { WidgetArray.MessageType, ModuleType.VizWidget },
                { Dialog.MessageType, ModuleType.VizWidget },
                { DialogArray.MessageType, ModuleType.VizWidget },
                { RobotPreview.MessageType, ModuleType.VizWidget },
                { RobotPreviewArray.MessageType, ModuleType.VizWidget },
                { Boundary.MessageType, ModuleType.VizWidget },
                { BoundaryArray.MessageType, ModuleType.VizWidget },
                { DetectionBox.MessageType, ModuleType.VizWidget },
                { DetectionBoxArray.MessageType, ModuleType.VizWidget },

                // these are already implemented, but need refinement
                //{ JointState.MessageType, ModuleType.JointState },
                //{ Path.MessageType, ModuleType.Path },
                //{ PoseArray.MessageType, ModuleType.Path },
                //{ PolygonStamped.MessageType, ModuleType.Path },
                //{ Polygon.MessageType, ModuleType.Path },
                //{ Octomap.MessageType, ModuleType.Octomap },
                //{ OctomapWithPose.MessageType, ModuleType.Octomap },
            };
        }
    }
}