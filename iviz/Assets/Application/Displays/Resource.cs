using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;
using System;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.Resources
{
    public class Resource
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Module
        {
            Invalid = 0,
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

                GameObject = UnityEngine.Resources.Load<GameObject>(resourceName);
                if (GameObject is null)
                {
                    throw new ArgumentException("Cannot find resource '" + resourceName + "'", nameof(resourceName));
                }
                Id = GameObject.GetInstanceID();
            }

            public Info(GameObject resource)
            {
                GameObject = resource ?? throw new ArgumentNullException(nameof(resource));
                Id = resource.GetInstanceID();
            }

            public string Name => GameObject.name;

            public override string ToString()
            {
                return GameObject.ToString();
            }

            public GameObject Instantiate(Transform parent = null)
            {
                if (GameObject is null)
                {
                    throw new NullReferenceException();
                }
                return UnityEngine.Object.Instantiate(GameObject, parent);
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
                SimpleLit = Load("Materials/SimpleWhite");
                Lit = Load("Materials/White");
                TexturedLit = Load("Materials/Textured Lit");
                ImagePreview = Load("Materials/ImagePreview");
                PointCloud = Load("Materials/PointCloud Material");
                MeshList = Load("Materials/MeshList Material");
                Grid = Load("Materials/Grid");
                Line = Load("Materials/Line Material");
                DepthImageProjector = Load("Materials/DepthImage Material");
            }

            static Material Load(string path)
            {
                return UnityEngine.Resources.Load<Material>(path);
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
                    textures[(ColormapId)i] = UnityEngine.Resources.Load<Texture2D>("Colormaps/" + Names[i]);
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
            public Info Line { get; }
            public Info Grid { get; }

            public ReadOnlyDictionary<string, Info> Generic { get; }

            public MarkersType()
            {
                Cube = new Info("Displays/Cube");
                Cylinder = new Info("Displays/Cylinder");
                Sphere = new Info("Displays/Sphere");
                Text = new Info("Displays/Text");
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
                Line = new Info("Displays/Line");
                Grid = new Info("Displays/Grid");

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
            public Info ItemListPanel { get; }
            public Info ConnectionPanel { get; }

            public Info HeadTitle { get; }
            public Info SectionTitle { get; }
            public Info ToggleWidget { get; }
            public Info SliderWidget { get; }
            public Info InputWidget { get; }
            public Info Dropdown { get; }
            public Info ColorPicker { get; }
            public Info ImagePreview { get; }
            public Info CloseButton { get; }
            public Info TrashButton { get; }
            public Info DataLabel { get; }
            public Info HideButton { get; }

            public WidgetsType()
            {
                DisplayButton = new Info("Widgets/Display Button");
                TopicsButton = new Info("Widgets/Topics Button");
                ItemListPanel = new Info("Widgets/Item List Panel");
                ConnectionPanel = new Info("Widgets/Connection Panel");

                HeadTitle = new Info("Widgets/Head Title");
                SectionTitle = new Info("Widgets/Section Title");
                ToggleWidget = new Info("Widgets/Toggle");
                SliderWidget = new Info("Widgets/Slider");
                InputWidget = new Info("Widgets/Input Field");
                ColorPicker = new Info("Widgets/ColorPicker");
                ImagePreview = new Info("Widgets/Image Preview");
                Dropdown = new Info("Widgets/Dropdown");
                CloseButton = new Info("Widgets/Close Button");
                TrashButton = new Info("Widgets/Trash Button");
                DataLabel = new Info("Widgets/Data Label");
                HideButton = new Info("Widgets/Hide Button");
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