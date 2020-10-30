using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public enum Module
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


        public const int ClickableLayer = 8;

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
        public static ReadOnlyDictionary<string, Module> ResourceByRosMessageType { get; }
            = new ReadOnlyDictionary<string, Module>(new Dictionary<string, Module>
                {
                    {PointCloud2.RosMessageType, Module.PointCloud},
                    {Image.RosMessageType, Module.Image},
                    {CompressedImage.RosMessageType, Module.Image},
                    {Marker.RosMessageType, Module.Marker},
                    {MarkerArray.RosMessageType, Module.Marker},
                    {InteractiveMarkerUpdate.RosMessageType, Module.InteractiveMarker},
                    {JointState.RosMessageType, Module.JointState},
                    {LaserScan.RosMessageType, Module.LaserScan},
                    {PoseStamped.RosMessageType, Module.Magnitude},
                    {Pose.RosMessageType, Module.Magnitude},
                    {PointStamped.RosMessageType, Module.Magnitude},
                    {Point.RosMessageType, Module.Magnitude},
                    {WrenchStamped.RosMessageType, Module.Magnitude},
                    {Wrench.RosMessageType, Module.Magnitude},
                    {Odometry.RosMessageType, Module.Magnitude},
                    {TwistStamped.RosMessageType, Module.Magnitude},
                    {Twist.RosMessageType, Module.Magnitude},
                    {OccupancyGrid.RosMessageType, Module.OccupancyGrid},
                    {Path.RosMessageType, Module.Path},
                    {PoseArray.RosMessageType, Module.Path},
                    {PolygonStamped.RosMessageType, Module.Path},
                    {Polygon.RosMessageType, Module.Path},
                    {GridMap.RosMessageType, Module.GridMap}
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
        public static bool TryGetResource([NotNull] Uri uri, out GameObjectInfo info,
            [CanBeNull] IExternalServiceProvider provider)
        {
            return Internal.TryGet(uri, out info) ||
                   External.TryGet(uri, out info, provider);
        }

        [ContractAnnotation("=> false, info:null; => true, info:notnull")]
        public static bool TryGetResource([NotNull] Uri uri, out Info<Texture2D> info,
            [CanBeNull] IExternalServiceProvider provider)
        {
            return Internal.TryGet(uri, out info) ||
                   External.TryGet(uri, out info, provider);
        }
    }
}