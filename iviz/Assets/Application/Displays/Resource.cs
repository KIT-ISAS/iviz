using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;
using System;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.VisualizationMsgs;

using GameObjectInfo = Iviz.Resources.Resource.Info<UnityEngine.GameObject>;
using MaterialInfo = Iviz.Resources.Resource.Info<UnityEngine.Material>;
using Iviz.Msgs.GeometryMsgs;
using Iviz.App;
using Iviz.Msgs.NavMsgs;

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
            LaserScan,
            AR,
            Odometry,
            OccupancyGrid,
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
            { PoseStamped.RosMessageType, Module.Odometry },
            { PointStamped.RosMessageType, Module.Odometry },
            { WrenchStamped.RosMessageType, Module.Odometry },
            { TwistStamped.RosMessageType, Module.Odometry },
            { OccupancyGrid.RosMessageType, Module.OccupancyGrid },
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

        public class Info<T> where T : UnityEngine.Object
        {
            readonly string resourceName;

            T baseObject;
            public T Object
            {
                get
                {
                    if (baseObject != null)
                    {
                        return baseObject;
                    }
                    baseObject = UnityEngine.Resources.Load<T>(resourceName);
                    if (baseObject is null)
                    {
                        throw new ArgumentException("Cannot find resource '" + resourceName + "'", nameof(resourceName));
                    }
                    return baseObject;
                }
            }

            int id = 0;
            public int Id => (id != 0) ? id : (id = Object.GetInstanceID());

            public Info(string resourceName)
            {
                if (resourceName is null)
                {
                    throw new ArgumentNullException(nameof(resourceName));
                }

                this.resourceName = resourceName;
            }

            public string Name => Object.name;

            public override string ToString()
            {
                return Object.ToString();
            }

            public T Instantiate(UnityEngine.Transform parent = null)
            {
                if (Object is null)
                {
                    throw new NullReferenceException();
                }
                return UnityEngine.Object.Instantiate(Object, parent);
            }
        }

        public class MaterialsType
        {
            public MaterialInfo Lit { get; }
            public MaterialInfo SimpleLit { get; }
            public MaterialInfo TexturedLit { get; }
            public MaterialInfo TransparentLit { get; }
            public MaterialInfo ImagePreview { get; }
            public MaterialInfo PointCloud { get; }
            public MaterialInfo MeshList { get; }
            public MaterialInfo DepthImageProjector { get; }
            public MaterialInfo Grid { get; }
            public MaterialInfo Line { get; }

            public MaterialsType()
            {
                SimpleLit = new MaterialInfo("Materials/SimpleWhite");
                Lit = new MaterialInfo("Materials/White");
                TexturedLit = new MaterialInfo("Materials/Textured Lit");
                TransparentLit = new MaterialInfo("Materials/Transparent Lit");
                ImagePreview = new MaterialInfo("Materials/ImagePreview");
                PointCloud = new MaterialInfo("Materials/PointCloud Material");
                MeshList = new MaterialInfo("Materials/MeshList Material");
                Grid = new MaterialInfo("Materials/Grid");
                Line = new MaterialInfo("Materials/Line Material");
                DepthImageProjector = new MaterialInfo("Materials/DepthImage Material");
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
            public GameObjectInfo Cube { get; }
            public GameObjectInfo Cylinder { get; }
            public GameObjectInfo Sphere { get; }
            public GameObjectInfo Text { get; }
            public GameObjectInfo LineConnector { get; }
            public GameObjectInfo NamedBoundary { get; }
            public GameObjectInfo Arrow { get; }
            public GameObjectInfo SphereSimple { get; }
            public GameObjectInfo MeshList { get; }
            public GameObjectInfo PointList { get; }
            public GameObjectInfo MeshTriangles { get; }
            public GameObjectInfo TFFrame { get; }
            public GameObjectInfo Image { get; }
            public GameObjectInfo Square { get; }
            public GameObjectInfo Line { get; }
            public GameObjectInfo Grid { get; }
            public GameObjectInfo Axis { get; }
            public GameObjectInfo DepthImageResource { get; }
            public GameObjectInfo OccupancyGridResource { get; }
            public GameObjectInfo RadialScanResource { get; }
            public GameObjectInfo ARMarkerResource { get; }
            public GameObjectInfo AxisFrameResource { get; }

            public ReadOnlyDictionary<string, GameObjectInfo> Generic { get; }

            public MarkersType()
            {
                Cube = new GameObjectInfo("Displays/Cube");
                Cylinder = new GameObjectInfo("Displays/Cylinder");
                Sphere = new GameObjectInfo("Displays/Sphere");
                Text = new GameObjectInfo("Displays/Text");
                LineConnector = new GameObjectInfo("Displays/LineConnector");
                NamedBoundary = new GameObjectInfo("Displays/NamedBoundary");
                Arrow = new GameObjectInfo("Displays/Arrow");
                SphereSimple = new GameObjectInfo("Spheres/sphere-LOD1");
                MeshList = new GameObjectInfo("Displays/MeshList");
                PointList = new GameObjectInfo("Displays/PointList");
                MeshTriangles = new GameObjectInfo("Displays/MeshTriangles");
                TFFrame = new GameObjectInfo("Displays/TFFrame");
                Image = new GameObjectInfo("Displays/ImageResource");
                Square = new GameObjectInfo("Displays/Square");
                Line = new GameObjectInfo("Displays/Line");
                Grid = new GameObjectInfo("Displays/Grid");
                Axis = new GameObjectInfo("Displays/Axis");
                DepthImageResource = new GameObjectInfo("Displays/DepthImageResource");
                OccupancyGridResource = new GameObjectInfo("Displays/OccupancyGridResource");
                ARMarkerResource = new GameObjectInfo("Displays/ARMarkerResource");
                AxisFrameResource = new GameObjectInfo("Displays/AxisFrameResource");

                Generic = new ReadOnlyDictionary<string, GameObjectInfo>(
                    new Dictionary<string, GameObjectInfo>()
                    {
                        ["Cube"] = Cube,
                        ["Cylinder"] = Cylinder,
                        ["Sphere"] = Sphere,
                        ["RightHand"] = new GameObjectInfo("Displays/RightHand"),
                    });
            }
        }

        public class ListenersType
        {
            public GameObjectInfo AR { get; }

            public ListenersType()
            {
                AR = new GameObjectInfo("Listeners/AR");
            }

            public T Instantiate<T>(UnityEngine.Transform parent = null) where T: MonoBehaviour
            {
                if (!typeof(IController).IsAssignableFrom(typeof(T)))
                {
                    throw new ArgumentException(nameof(T));
                }
                GameObject gameObject = new GameObject();
                gameObject.transform.parent = parent;
                gameObject.name = typeof(T).Name;
                return gameObject.AddComponent<T>();
            }
        }

        public class WidgetsType
        {
            public GameObjectInfo DisplayButton { get; }
            public GameObjectInfo TopicsButton { get; }
            public GameObjectInfo ItemListPanel { get; }
            public GameObjectInfo ConnectionPanel { get; }

            public GameObjectInfo HeadTitle { get; }
            public GameObjectInfo SectionTitle { get; }
            public GameObjectInfo ToggleWidget { get; }
            public GameObjectInfo SliderWidget { get; }
            public GameObjectInfo InputWidget { get; }
            public GameObjectInfo ShortInputWidget { get; }
            public GameObjectInfo NumberInputWidget { get; }
            public GameObjectInfo Dropdown { get; }
            public GameObjectInfo ColorPicker { get; }
            public GameObjectInfo ImagePreview { get; }
            public GameObjectInfo CloseButton { get; }
            public GameObjectInfo TrashButton { get; }
            public GameObjectInfo DataLabel { get; }
            public GameObjectInfo HideButton { get; }
            public GameObjectInfo Vector3 { get; }
            public GameObjectInfo Sender { get; }
            public GameObjectInfo Listener { get; }

            public WidgetsType()
            {
                DisplayButton = new GameObjectInfo("Widgets/Display Button");
                TopicsButton = new GameObjectInfo("Widgets/Topics Button");
                ItemListPanel = new GameObjectInfo("Widgets/Item List Panel");
                ConnectionPanel = new GameObjectInfo("Widgets/Connection Panel");

                HeadTitle = new GameObjectInfo("Widgets/Head Title");
                SectionTitle = new GameObjectInfo("Widgets/Section Title");
                ToggleWidget = new GameObjectInfo("Widgets/Toggle");
                SliderWidget = new GameObjectInfo("Widgets/Slider");
                InputWidget = new GameObjectInfo("Widgets/Input Field");
                ShortInputWidget = new GameObjectInfo("Widgets/Short Input Field");
                NumberInputWidget = new GameObjectInfo("Widgets/Number Input Field");
                ColorPicker = new GameObjectInfo("Widgets/ColorPicker");
                ImagePreview = new GameObjectInfo("Widgets/Image Preview");
                Dropdown = new GameObjectInfo("Widgets/Dropdown");
                CloseButton = new GameObjectInfo("Widgets/Close Button");
                TrashButton = new GameObjectInfo("Widgets/Trash Button");
                DataLabel = new GameObjectInfo("Widgets/Data Label");
                HideButton = new GameObjectInfo("Widgets/Hide Button");
                Vector3 = new GameObjectInfo("Widgets/Vector3");
                Sender = new GameObjectInfo("Widgets/Sender");
                Listener = new GameObjectInfo("Widgets/Listener");
            }
        }

        public class RobotsType
        {
            static readonly string[] names =
                {
                "edu.iviz.dummybot",
                "com.clearpath.husky",
                "com.willowgarage.pr2",
                "edu.fraunhofer.iosb.bob",
                "edu.fraunhofer.iosb.crayler",
                "edu.kit.h2t.armar6",
                "edu.kit.ipr.gammabot"
                };

            public ReadOnlyCollection<string> Names { get; }
            public ReadOnlyDictionary<string, GameObjectInfo> Objects { get; }

            public RobotsType()
            {
                Names = new ReadOnlyCollection<string>(names);

                Dictionary<string, GameObjectInfo> objects = new Dictionary<string, GameObjectInfo>();
                foreach (string name in names)
                {
                    objects.Add(name, new GameObjectInfo("Robots/" + name));
                }
                Objects = new ReadOnlyDictionary<string, GameObjectInfo>(objects);
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