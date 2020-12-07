using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.VisualizationMsgs;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ColormapId
        {
            lines,
            pink,
            copper,
            bone,
            gray,
            winter,
            autumn,
            summer,
            spring,
            cool,
            hot,
            hsv,
            jet,
            parula
        }

        /// <summary>
        /// Module type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ModuleType
        {
            Invalid = 0,
            Grid,
            TF,
            PointCloud,
            Image,
            Marker,
            InteractiveMarker,
            JointState,
            DepthCloud,
            LaserScan,
            AugmentedReality,
            Magnitude,
            OccupancyGrid,
            Joystick,
            Path,
            GridMap,
            Robot
        }


        static MaterialsType materials;
        static ColormapsType colormaps;
        static DisplaysType displays;
        static ControllersType controllers;
        static WidgetsType widgets;
        static TexturedMaterialsType texturedMaterials;
        static FontInfo fontInfo;
        static InternalResourceManager internals;
        static ExternalResourceManager external;

        /// <summary>
        /// Dictionary that describes which module handles which ROS message type.
        /// </summary>
        public static ReadOnlyDictionary<string, ModuleType> ResourceByRosMessageType { get; }
            = new ReadOnlyDictionary<string, ModuleType>(new Dictionary<string, ModuleType>
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
                    {GridMap.RosMessageType, ModuleType.GridMap}
                }
            );

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
        [NotNull] public static InternalResourceManager Internal => internals ?? (internals = new InternalResourceManager());
        [NotNull] public static ExternalResourceManager External => external ?? (external = new ExternalResourceManager());

        public static bool ContainsRobot([NotNull] string robotName)
        {
            return Internal.ContainsRobot(robotName) ||
                   External.ContainsRobot(robotName);
        }

        [NotNull]
        public static IEnumerable<string> GetRobotNames()
        {
            return Internal.GetRobotNames().Concat(External.GetRobotNames());
        }

        [ContractAnnotation("=> false, robotDescription:null; => true, robotDescription:notnull")]
        public static bool TryGetRobot([NotNull] string robotName, out string robotDescription)
        {
            return Internal.TryGetRobot(robotName, out robotDescription) ||
                   External.TryGetRobot(robotName, out robotDescription);
        }

        [ContractAnnotation("=> false, info:null; => true, info:notnull")]
        public static bool TryGetResource([NotNull] string uriString, out GameObjectInfo info,
            [CanBeNull] IExternalServiceProvider provider)
        {
            //
            return Internal.TryGet(uriString, out info) ||
                   External.TryGet(uriString, out info, null);
        }

        [ContractAnnotation("=> false, info:null; => true, info:notnull")]
        public static bool TryGetResource([NotNull] string uriString, out Info<Texture2D> info,
            [CanBeNull] IExternalServiceProvider provider)
        {
            return Internal.TryGet(uriString, out info) ||
                   External.TryGet(uriString, out info, Settings.IsHololens ? null : provider);
        }
    }
}