using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            DepthImageProjector
        }

        public class Info
        {
            public int Id { get; }
            public GameObject GameObject { get; }

            public Info(GameObject resource)
            {
                GameObject = resource;
                Id = resource.GetInstanceID();
            }

            public string Name => GameObject.name;

            public override string ToString()
            {
                return GameObject.ToString();
            }
        }

        public static class Materials
        {
            public static Material Lit { get; private set; }
            public static Material SimpleLit { get; private set; }
            public static Material TexturedLit { get; private set; }
            public static Material ImagePreview { get; private set; }
            public static Material PointCloud { get; private set; }
            public static Material MeshList { get; private set; }

            public static void Initialize()
            {
                if (Lit != null)
                {
                    return;
                }
                SimpleLit = Resources.Load<Material>("BaseMaterials/SimpleWhite");
                Lit = Resources.Load<Material>("BaseMaterials/White");
                TexturedLit = Resources.Load<Material>("BaseMaterials/Textured Lit");
                ImagePreview = Resources.Load<Material>("BaseMaterials/ImagePreview");
                PointCloud = Resources.Load<Material>("Displays/PointCloud Material");
                MeshList = Resources.Load<Material>("Displays/MeshList Material");
            }
        }

        public static class Colormaps
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public enum Id
            {
                lines, pink, copper, bone, gray, winter, autumn, summer, spring, cool, hot, hsv, jet, parula
            };

            public static string[] Names { get; } = new string[]
            {
                "lines", "pink", "copper", "bone", "gray", "winter", "autumn", "summer", "spring", "cool", "hot", "hsv", "jet", "parula"
            };

            public static Dictionary<Id, Texture2D> Textures { get; } = new Dictionary<Id, Texture2D>();

            public static void Initialize()
            {
                if (Textures.Count != 0)
                {
                    return;
                }
                for (int i = 0; i < Names.Length; i++)
                {
                    Textures[(Id)i] = Resources.Load<Texture2D>("Colormaps/" + Names[i]);
                }
            }
        }

        public static class Markers
        {
            public static Info Cube { get; private set; }
            public static Info Cylinder { get; private set; }
            public static Info Sphere { get; private set; }
            public static Info Text { get; private set; }
            public static Info LineStrip { get; private set; }
            public static Info LineConnector { get; private set; }
            public static Info NamedBoundary { get; private set; }
            public static Info Arrow { get; private set; }
            public static Info SphereSimple { get; private set; }
            public static Info MeshList { get; private set; }
            public static Info PointList { get; private set; }

            public static Dictionary<string, Info> Generic { get; private set; }

            public static void Initialize()
            {
                if (Cube != null)
                {
                    return;
                }

                Cube = new Info(Resources.Load<GameObject>("Markers/Cube"));
                Cylinder = new Info(Resources.Load<GameObject>("Markers/Cylinder"));
                Sphere = new Info(Resources.Load<GameObject>("Markers/Sphere"));
                Text = new Info(Resources.Load<GameObject>("Markers/Text"));
                LineStrip = new Info(Resources.Load<GameObject>("Markers/LineStrip"));
                LineConnector = new Info(Resources.Load<GameObject>("Markers/LineConnector"));
                NamedBoundary = new Info(Resources.Load<GameObject>("Markers/NamedBoundary"));
                Arrow = new Info(Resources.Load<GameObject>("Markers/Arrow"));
                SphereSimple = new Info(Resources.Load<GameObject>("Spheres/sphere-LOD1"));
                MeshList = new Info(Resources.Load<GameObject>("Markers/MeshList"));
                PointList = new Info(Resources.Load<GameObject>("Markers/PointList"));

                Generic = new Dictionary<string, Info>()
                {
                    ["Cube"] = Cube,
                    ["Cylinder"] = Cylinder,
                    ["Sphere"] = Sphere,
                    ["RightHand"] = new Info(Resources.Load<GameObject>("Markers/RightHand")),
                };
            }
        }

        public static class Displays
        {
            public static Info TFFrame { get; private set; }
            public static Info PointCloud { get; private set; }
            public static Info Grid { get; private set; }
            public static Info TF { get; private set; }
            public static Info Image { get; private set; }
            public static Info Robot { get; private set; }
            public static Info MarkerObject { get; private set; }
            public static Info Marker { get; private set; }
            public static Info InteractiveMarkerControlObject { get; private set; }
            public static Info InteractiveMarkerObject { get; private set; }
            public static Info InteractiveMarker { get; private set; }
            public static Info JointState { get; private set; }
            public static Info DepthImageProjector { get; private set; }

            public static void Initialize()
            {
                if (TFFrame != null)
                {
                    return;
                }

                TFFrame = new Info(Resources.Load<GameObject>("Displays/TFFrame"));
                PointCloud = new Info(Resources.Load<GameObject>("Displays/PointCloud"));
                Grid = new Info(Resources.Load<GameObject>("Displays/Grid"));
                TF = new Info(Resources.Load<GameObject>("Displays/TF"));
                Image = new Info(Resources.Load<GameObject>("Displays/Image"));
                Robot = new Info(Resources.Load<GameObject>("Displays/Robot"));
                MarkerObject = new Info(Resources.Load<GameObject>("Displays/MarkerObject"));
                Marker = new Info(Resources.Load<GameObject>("Displays/Marker"));
                InteractiveMarkerControlObject = new Info(Resources.Load<GameObject>("Displays/InteractiveMarkerControlObject"));
                InteractiveMarkerObject = new Info(Resources.Load<GameObject>("Displays/InteractiveMarkerObject"));
                InteractiveMarker = new Info(Resources.Load<GameObject>("Displays/InteractiveMarker"));
                JointState = new Info(Resources.Load<GameObject>("Displays/JointState"));
                DepthImageProjector = new Info(Resources.Load<GameObject>("Displays/DepthImageProjector"));
            }
        }

        public static class Widgets
        {
            public static Info DisplayButton { get; private set; }
            public static Info TopicsButton { get; private set; }

            public static void Initialize()
            {
                if (DisplayButton != null)
                {
                    return;
                }

                DisplayButton = new Info(Resources.Load<GameObject>("Widgets/Display Button"));
                TopicsButton = new Info(Resources.Load<GameObject>("Widgets/Topics Button"));
            }
        }

        public static class Robots
        {
            public static string[] Names { get; } = new string[]
            {
                "edu.iviz.dummybot",
                "com.clearpath.husky",
                "com.robotis.turtlebot3.burger",
                "com.robotis.turtlebot3.waffle",
                "com.willowgarage.pr2",
                "edu.fraunhofer.iosb.bob",
                "edu.kit.h2t.armar6"
            };

            static Dictionary<string, Info> Objects { get; } = new Dictionary<string, Info>();

            public static Info GetObject(string name)
            {
                // robots are huge so they are only loaded on demand
                if (!Objects.TryGetValue(name, out Info info))
                {
                    info = new Info(Resources.Load<GameObject>("Robots/" + name));
                    Objects.Add(name, info);
                }
                return info;
            }
        }

        public const int ClickableLayer = 8;
    }
}