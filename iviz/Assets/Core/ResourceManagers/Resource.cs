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
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.VisualizationMsgs;
using UnityEngine;
using GameObjectInfo = Iviz.Resources.Info<UnityEngine.GameObject>;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.Resources
{
    /// <summary>
    /// Class that simplifies access to assets.
    /// </summary>
    public static class Resource
    {
        static MaterialsType? materials;
        static ColormapsType? colormaps;
        static DisplaysType? displays;
        static ExtrasType? controllers;
        static WidgetsType? widgets;
        static TexturedMaterialsType? texturedMaterials;
        static FontInfo? fontInfo;
        static InternalResourceManager? internals;
        static ExternalResourceManager? externals;

        /// <summary>
        /// Dictionary that describes which module handles which ROS message type.
        /// </summary>
        public static ReadOnlyDictionary<string, ModuleType> ResourceByRosMessageType { get; }
            = new Dictionary<string, ModuleType>
            {
                {PointCloud2.RosMessageType, ModuleType.PointCloud},
                {Image.RosMessageType, ModuleType.Image},
                {CompressedImage.RosMessageType, ModuleType.Image},
                {Marker.RosMessageType, ModuleType.Marker},
                {MarkerArray.RosMessageType, ModuleType.Marker},
                {InteractiveMarkerUpdate.RosMessageType, ModuleType.InteractiveMarker},
                //{ JointState.RosMessageType, ModuleType.JointState },
                {LaserScan.RosMessageType, ModuleType.LaserScan},
                {PoseStamped.RosMessageType, ModuleType.Magnitude},
                {Pose.RosMessageType, ModuleType.Magnitude},
                {PointStamped.RosMessageType, ModuleType.Magnitude},
                {Point.RosMessageType, ModuleType.Magnitude},
                {WrenchStamped.RosMessageType, ModuleType.Magnitude},
                {Wrench.RosMessageType, ModuleType.Magnitude},
                {Odometry.RosMessageType, ModuleType.Magnitude},
                {TwistStamped.RosMessageType, ModuleType.Magnitude},
                {Twist.RosMessageType, ModuleType.Magnitude},
                {OccupancyGrid.RosMessageType, ModuleType.OccupancyGrid},
                //{ Path.RosMessageType, ModuleType.Path },
                //{ PoseArray.RosMessageType, ModuleType.Path },
                //{ PolygonStamped.RosMessageType, ModuleType.Path },
                //{ Polygon.RosMessageType, ModuleType.Path },
                {GridMap.RosMessageType, ModuleType.GridMap},
                //{ Octomap.RosMessageType, ModuleType.Octomap },
                //{ OctomapWithPose.RosMessageType, ModuleType.Octomap },
                //{ Dialog.RosMessageType, ModuleType.GuiDialog },
                {DialogArray.RosMessageType, ModuleType.GuiDialog},
            }.AsReadOnly();

        public static readonly ColorSchema Colors = new();

        public static MaterialsType Materials => materials ??= new MaterialsType();
        public static ColormapsType Colormaps => colormaps ??= new ColormapsType();
        public static DisplaysType Displays => displays ??= new DisplaysType();
        public static ExtrasType Extras => controllers ??= new ExtrasType();
        public static WidgetsType Widgets => widgets ??= new WidgetsType();

        public static TexturedMaterialsType TexturedMaterials => texturedMaterials ??= new TexturedMaterialsType();
        public static FontInfo Font => fontInfo ??= new FontInfo();
        public static InternalResourceManager Internal => internals ??= new InternalResourceManager();
        public static ExternalResourceManager External => externals ??= new ExternalResourceManager();

        public static bool IsRobotSaved(string robotName) =>
            Internal.ContainsRobot(robotName) ||
            External.ContainsRobot(robotName);

        public static IEnumerable<string> GetRobotNames() => Internal.GetRobotNames().Concat(External.GetRobotNames());

        public static ValueTask<(bool result, string? robotDescription)> TryGetRobotAsync(string robotName,
            CancellationToken token = default) =>
            Internal.TryGetRobot(robotName, out string? robotDescription)
                ? new ValueTask<(bool, string?)>((true, robotDescription))
                : External.TryGetRobotAsync(robotName, token);

        public static bool TryGetResource(string uriString, [NotNullWhen(true)] out GameObjectInfo? info) =>
            Internal.TryGet(uriString, out info) || External.TryGetGameObject(uriString, out info);

        public static ValueTask<GameObjectInfo?> GetGameObjectResourceAsync(
            string uriString,
            IExternalServiceProvider? provider,
            CancellationToken token) =>
            Internal.TryGet(uriString, out GameObjectInfo? info)
                ? new ValueTask<GameObjectInfo?>(info)
                : External.TryGetGameObjectAsync(uriString, provider, token);

        internal static ValueTask<Info<Texture2D>?> GetTextureResourceAsync(
            string uriString,
            IExternalServiceProvider? provider,
            CancellationToken token) =>
            Internal.TryGet(uriString, out Info<Texture2D>? info)
                ? new ValueTask<Info<Texture2D>?>(info)
                : External.TryGetTextureAsync(uriString, provider, token);

        public static void ClearResources()
        {
            internals = null;
            externals = null;
            texturedMaterials = null;
        }
    }
}