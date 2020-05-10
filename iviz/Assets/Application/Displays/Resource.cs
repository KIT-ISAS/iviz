using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;
using Iviz.Msgs.sensor_msgs;
using Iviz.Msgs.visualization_msgs;
using System;

namespace Iviz.App
{
    public class Resource
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Module
        {
            Grid,
            TF,
            PointCloud,
            Image,
            Robot,
            Marker,
            InteractiveMarker,
            JointState,
            DepthImageProjector,
            LaserScan
        }

        static readonly Dictionary<string, Module> resourceByRosMessageType = new Dictionary<string, Module>
        {
            { PointCloud2.RosMessageType, Module.PointCloud },
            { Image.RosMessageType, Module.Image },
            { CompressedImage.RosMessageType, Module.Image },
            { Marker.RosMessageType, Module.Marker },
            { MarkerArray.RosMessageType, Module.Marker },
            { InteractiveMarkerUpdate.RosMessageType, Module.InteractiveMarker },
            { JointState.RosMessageType, Module.JointState },
            { LaserScan.RosMessageType, Module.LaserScan },
        };

        public static ReadOnlyDictionary<string, Module> ResourceByRosMessageType { get; }
            = new ReadOnlyDictionary<string, Module>(resourceByRosMessageType);

        public class ColorScheme
        {
            public Color EnabledFontColor { get; }
            public Color DisabledFontColor { get; }

            public ColorScheme()
            {
                EnabledFontColor = new Color(0.196f, 0.196f, 0.196f);
                DisabledFontColor = EnabledFontColor * 3;
            }
        }

        public class Info
        {
            public int Id { get; }
            public GameObject GameObject { get; }

            public Info(string resourceName)
            {
                if (resourceName is null)
                {
                    throw new ArgumentNullException(nameof(resourceName));
                }

                GameObject = Resources.Load<GameObject>(resourceName);
                if (GameObject == null)
                {
                    throw new ArgumentException("Cannot find resource '" + resourceName + "'", nameof(resourceName));
                }
                Id = GameObject.GetInstanceID();
            }

            public Info(GameObject resource)
            {
                GameObject = resource ?? throw new System.ArgumentNullException(nameof(resource));
                Id = resource.GetInstanceID();
            }

            public string Name => GameObject.name;

            public override string ToString()
            {
                return GameObject.ToString();
            }
        }

        public class MaterialsType
        {
            public Material Lit { get; }
            public Material SimpleLit { get; }
            public Material TexturedLit { get; }
            public Material ImagePreview { get; }
            public Material PointCloud { get; }
            public Material MeshList { get; }
            public Material DepthImageProjector { get; }
            public Material Grid { get; }
            public Material Line { get; }

            public MaterialsType()
            {
                SimpleLit = Resources.Load<Material>("Materials/SimpleWhite");
                Lit = Resources.Load<Material>("Materials/White");
                TexturedLit = Resources.Load<Material>("Materials/Textured Lit");
                ImagePreview = Resources.Load<Material>("Materials/ImagePreview");
                PointCloud = Resources.Load<Material>("Materials/PointCloud Material");
                MeshList = Resources.Load<Material>("Materials/MeshList Material");
                Grid = Resources.Load<Material>("Materials/Grid");
                Line = Resources.Load<Material>("Materials/Line Material");
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum ColormapId
        {
            lines, pink, copper, bone, gray, winter, autumn, summer, spring, cool, hot, hsv, jet, parula
        };


        public class ColormapsType
        {
            public ReadOnlyDictionary<ColormapId, Texture2D> Textures { get; }

            public ReadOnlyCollection<string> Names { get; }

            public ColormapsType()
            {
                string[] names = 
                {
                    "lines", 
                    "pink", 
                    "copper", 
                    "bone", 
                    "gray", 
                    "winter", 
                    "autumn", 
                    "summer", 
                    "spring", 
                    "cool", 
                    "hot", 
                    "hsv", 
                    "jet", 
                    "parula"
                };
                Names = new ReadOnlyCollection<string>(names);

                Dictionary<ColormapId, Texture2D> textures = new Dictionary<ColormapId, Texture2D>();
                for (int i = 0; i < Names.Count; i++)
                {
                    textures[(ColormapId)i] = Resources.Load<Texture2D>("Colormaps/" + Names[i]);
                }
                Textures = new ReadOnlyDictionary<ColormapId, Texture2D>(textures);
            }
        }

        public class MarkersType
        {
            public Info Cube { get; }
            public Info Cylinder { get; }
            public Info Sphere { get; }
            public Info Text { get; }
            public Info LineStrip { get; }
            public Info LineConnector { get; }
            public Info NamedBoundary { get; }
            public Info Arrow { get; }
            public Info SphereSimple { get; }
            public Info MeshList { get; }
            public Info PointList { get; }
            public Info MeshTriangles { get; }
            public Info TFFrame { get; }
            public Info Image { get; }
            public Info Square { get; }

            public ReadOnlyDictionary<string, Info> Generic { get; }

            public MarkersType()
            {
                Cube = new Info("Displays/Cube");
                Cylinder = new Info("Displays/Cylinder");
                Sphere = new Info("Displays/Sphere");
                Text = new Info("Displays/Text");
                LineStrip = new Info("Displays/LineStrip");
                LineConnector = new Info("Displays/LineConnector");
                NamedBoundary = new Info("Displays/NamedBoundary");
                Arrow = new Info("Displays/Arrow");
                SphereSimple = new Info("Spheres/sphere-LOD1");
                MeshList = new Info("Displays/MeshList");
                PointList = new Info("Displays/PointList");
                MeshTriangles = new Info("Displays/MeshTriangles");
                TFFrame = new Info("Displays/TFFrame");
                Image = new Info("Displays/ImageResource");
                Square = new Info("Displays/Square");

                Generic = new ReadOnlyDictionary<string, Info>(
                    new Dictionary<string, Info>()
                    {
                        ["Cube"] = Cube,
                        ["Cylinder"] = Cylinder,
                        ["Sphere"] = Sphere,
                        ["RightHand"] = new Info("Displays/RightHand"),
                    });
            }
        }

        public class ListenersType
        {
            public Info PointCloud { get; }
            public Info Grid { get; }
            public Info TF { get; }
            public Info Image { get; }
            public Info Robot { get; }
            public Info MarkerObject { get; }
            public Info Marker { get; }
            public Info InteractiveMarkerControlObject { get; }
            public Info InteractiveMarkerObject { get; }
            public Info InteractiveMarker { get; }
            public Info JointState { get; }
            public Info DepthImageProjector { get; }
            public Info LaserScan { get; }

            public ListenersType()
            {
                PointCloud = new Info("Listeners/PointCloud");
                Grid = new Info("Listeners/Grid");
                TF = new Info("Listeners/TF");
                Image = new Info("Listeners/Image");
                Robot = new Info("Listeners/Robot");
                MarkerObject = new Info("Listeners/MarkerObject");
                Marker = new Info("Listeners/Marker");
                InteractiveMarkerControlObject = new Info("Listeners/InteractiveMarkerControlObject");
                InteractiveMarkerObject = new Info("Listeners/InteractiveMarkerObject");
                InteractiveMarker = new Info("Listeners/InteractiveMarker");
                JointState = new Info("Listeners/JointState");
                DepthImageProjector = new Info("Listeners/DepthImageProjector");
                LaserScan = new Info("Listeners/LaserScan");
            }
        }

        public class WidgetsType
        {
            public Info DisplayButton { get; }
            public Info TopicsButton { get; }

            public WidgetsType()
            {
                DisplayButton = new Info("Widgets/Display Button");
                TopicsButton = new Info("Widgets/Topics Button");
            }
        }

        public class RobotsType
        {
            public ReadOnlyCollection<string> Names { get; }

            readonly Dictionary<string, Info> Objects = new Dictionary<string, Info>();

            public RobotsType()
            {
                string[] names =
                {
                "edu.iviz.dummybot",
                "com.clearpath.husky",
                "com.robotis.turtlebot3.burger",
                "com.robotis.turtlebot3.waffle",
                "com.willowgarage.pr2",
                "edu.fraunhofer.iosb.bob",
                "edu.kit.h2t.armar6"
                };

                Names = new ReadOnlyCollection<string>(names);
            }

            public Info GetObject(string name)
            {
                // robots are huge so they are only loaded on demand
                if (!Objects.TryGetValue(name, out Info info))
                {
                    info = new Info("Robots/" + name);
                    Objects.Add(name, info);
                }
                return info;
            }
        }

        public const int ClickableLayer = 8;

        static MaterialsType materials;
        public static MaterialsType Materials => materials ?? (materials = new MaterialsType());

        static ColormapsType colormaps;
        public static ColormapsType Colormaps => colormaps ?? (colormaps = new ColormapsType());

        static MarkersType markers;
        public static MarkersType Markers => markers ?? (markers = new MarkersType());

        static ListenersType listeners;
        public static ListenersType Listeners => listeners ?? (listeners = new ListenersType());

        static WidgetsType widgets;
        public static WidgetsType Widgets => widgets ?? (widgets = new WidgetsType());

        static RobotsType robots;
        public static RobotsType Robots => robots ?? (robots = new RobotsType());

        public static ColorScheme Colors { get; } = new ColorScheme();
    }
}