using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.OctomapMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.VisualizationMsgs;
using JetBrains.Annotations;
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
        static MaterialsType materials;
        static ColormapsType colormaps;
        static DisplaysType displays;
        static ControllersType controllers;
        static WidgetsType widgets;
        static TexturedMaterialsType texturedMaterials;
        static FontInfo fontInfo;
        static InternalResourceManager internals;
        static ExternalResourceManager externals;

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
                {JointState.RosMessageType, ModuleType.JointState},
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
                {Path.RosMessageType, ModuleType.Path},
                {PoseArray.RosMessageType, ModuleType.Path},
                {PolygonStamped.RosMessageType, ModuleType.Path},
                {Polygon.RosMessageType, ModuleType.Path},
                {GridMap.RosMessageType, ModuleType.GridMap},
                {Octomap.RosMessageType, ModuleType.Octomap},
                {OctomapWithPose.RosMessageType, ModuleType.Octomap},
                {Dialog.RosMessageType, ModuleType.GuiDialog},
                {GuiArray.RosMessageType, ModuleType.GuiDialog},
            }.AsReadOnly();

        [NotNull] public static MaterialsType Materials => materials ?? (materials = new MaterialsType());
        [NotNull] public static ColormapsType Colormaps => colormaps ?? (colormaps = new ColormapsType());
        [NotNull] public static DisplaysType Displays => displays ?? (displays = new DisplaysType());
        [NotNull] public static ControllersType Controllers => controllers ?? (controllers = new ControllersType());
        [NotNull] public static WidgetsType Widgets => widgets ?? (widgets = new WidgetsType());
        public static ColorScheme Colors { get; } = new ColorScheme();

        [NotNull]
        public static TexturedMaterialsType TexturedMaterials =>
            texturedMaterials ?? (texturedMaterials = new TexturedMaterialsType());

        [NotNull] public static FontInfo Font => fontInfo ?? (fontInfo = new FontInfo());

        [NotNull]
        public static InternalResourceManager Internal => internals ?? (internals = new InternalResourceManager());

        [NotNull]
        public static ExternalResourceManager External => externals ?? (externals = new ExternalResourceManager());

        public static bool IsRobotSaved([NotNull] string robotName)
        {
            return Internal.ContainsRobot(robotName) ||
                   External.ContainsRobot(robotName);
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<string> GetRobotNames()
        {
            return Internal.GetRobotNames().Concat(External.GetRobotNames());
        }

        public static async ValueTask<(bool result, string robotDescription)> TryGetRobotAsync(
            [NotNull] string robotName,
            CancellationToken token = default)
        {
            return Internal.TryGetRobot(robotName, out string robotDescription)
                ? (true, robotDescription)
                : await External.TryGetRobotAsync(robotName, token);
        }

        [ContractAnnotation("=> false, info:null; => true, info:notnull")]
        public static bool TryGetResource([NotNull] string uriString, out GameObjectInfo info)
        {
            return Internal.TryGet(uriString, out info) || External.TryGetGameObject(uriString, out info);
        }

        [ItemCanBeNull]
        public static async ValueTask<GameObjectInfo> GetGameObjectResourceAsync([NotNull] string uriString,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            return Internal.TryGet(uriString, out GameObjectInfo info)
                ? info
                : await External.TryGetGameObjectAsync(uriString, provider, token);
        }

        [ItemCanBeNull]
        internal static async ValueTask<Info<Texture2D>> GetTextureResourceAsync([NotNull] string uriString,
            [CanBeNull] IExternalServiceProvider provider, CancellationToken token)
        {
            return Internal.TryGet(uriString, out Info<Texture2D> info)
                ? info
                : await External.TryGetTextureAsync(uriString, provider, token);
        }

        public static void ClearResources()
        {
            internals = null;
            externals = null;
            texturedMaterials = null;
        }
    }
}