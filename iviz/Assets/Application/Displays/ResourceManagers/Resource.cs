using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Reflection;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.VisualizationMsgs;
using GameObjectInfo = Iviz.Resources.Resource.Info<UnityEngine.GameObject>;
using MaterialInfo = Iviz.Resources.Resource.Info<UnityEngine.Material>;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.NavMsgs;
using System.Text;
using Iviz.Displays;
using Iviz.Msgs.GridMapMsgs;
using JetBrains.Annotations;
using Transform = UnityEngine.Transform;

namespace Iviz.Resources
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }


    public static class Resource
    {
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
            [Obsolete] LegacyRobot,
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
            Robot,
        }

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
                    {Msgs.GeometryMsgs.Pose.RosMessageType, Module.Magnitude},
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
                    {GridMap.RosMessageType, Module.GridMap},
                }
            );

        public class ColorScheme
        {
            public Color EnabledFontColor { get; }
            public Color DisabledFontColor { get; }
            public Color EnabledPanelColor { get; }
            public Color DisabledPanelColor { get; }

            public ColorScheme()
            {
                EnabledFontColor = new Color(0.196f, 0.196f, 0.196f, 1.0f);
                DisabledFontColor = EnabledFontColor * 3;

                EnabledPanelColor = new Color(0.88f, 0.99f, 1f, 0.373f);
                DisabledPanelColor = new Color(0.777f, 0.777f, 0.777f, 0.373f);
            }
        }

        /// <summary>
        /// Unique identifier for the type of a Unity resource.
        /// Basically a wrapper around the instance id of the resource that only gets loaded when the asset is needed.
        /// Required by the Resource Pool.
        /// </summary>
        /// <typeparam name="T">Unity object type, such as a GameObject or a Texture.</typeparam>
        public sealed class Info<T> : IEquatable<Info<T>> where T : UnityEngine.Object
        {
            readonly string resourceName;

            T baseObject;

            /// <summary>
            /// Returns or loads a resource of this type.  
            /// </summary>
            public T Object
            {
                get
                {
                    if (!(baseObject is null))
                    {
                        return baseObject;
                    }

                    baseObject = UnityEngine.Resources.Load<T>(resourceName);
                    if (baseObject is null)
                    {
                        throw new ArgumentException("Cannot find resource '" + resourceName + "'",
                            nameof(resourceName));
                    }

                    return baseObject;
                }
            }

            int id = 0;

            /// <summary>
            /// Returns the instance id of the resource.
            /// </summary>
            public int Id => (id != 0) ? id : (id = Object.GetInstanceID());

            /// <summary>
            /// Constructs a unique identifier from a resource path, and loads it from memory.
            /// </summary>
            /// <param name="resourceName">Path to the resource.</param>
            public Info([NotNull] string resourceName)
            {
                this.resourceName = resourceName ?? throw new ArgumentNullException(nameof(resourceName));
            }

            /// <summary>
            /// Constructs a unique identifier from a resource path, and sets the resource to a previously loaded instance.
            /// </summary>
            /// <param name="resourceName">Path to the resource.</param>
            /// <param name="baseObject">Previously loaded instance.</param>
            public Info(string resourceName, T baseObject)
            {
                this.resourceName = resourceName;
                this.baseObject = baseObject;
            }

            /// <summary>
            /// Name of this resource.
            /// </summary>
            public string Name => Object.name;

            public override string ToString()
            {
                return Object.ToString();
            }

            /// <summary>
            /// Instantiates a clone of the resource.
            /// </summary>
            /// <param name="parent">If not null, sets the clone parent to this.</param>
            /// <returns>An instantiated clone.</returns>
            public T Instantiate(Transform parent = null)
            {
                if (Object == null)
                {
                    throw new ResourceNotFoundException();
                }

                return UnityEngine.Object.Instantiate(Object, parent);
            }

            public bool Equals(Info<T> other)
            {
                return !(other is null) && Id == other.Id;
            }

            public override bool Equals(object obj)
            {
                return !(obj is null) && obj is Info<T> info && Id == info.Id;
            }

            public static bool operator ==(Info<T> a, Info<T> b)
            {
                return ReferenceEquals(a, b) || (!(a is null) && a.Equals(b));
            }

            public static bool operator !=(Info<T> a, Info<T> b)
            {
                return !(a == b);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }
        }

        public class MaterialsType
        {
            public MaterialInfo Lit { get; }
            public MaterialInfo SimpleLit { get; }
            public MaterialInfo TexturedLit { get; }
            public MaterialInfo TransparentLit { get; }
            public MaterialInfo TransparentTexturedLit { get; }
            public MaterialInfo ImagePreview { get; }
            public MaterialInfo PointCloud { get; }
            public MaterialInfo PointCloudWithColormap { get; }
            public MaterialInfo MeshList { get; }
            public MaterialInfo DepthImageProjector { get; }
            public MaterialInfo Grid { get; }
            public MaterialInfo GridInterior { get; }
            public MaterialInfo GridMap { get; }

            public MaterialInfo Line { get; }
            public MaterialInfo TransparentLine { get; }
            public MaterialInfo LineWithColormap { get; }
            public MaterialInfo TransparentLineWithColormap { get; }

            public MaterialInfo LitOcclusionOnly { get; }
            public MaterialInfo MeshListOcclusionOnly { get; }

            public MaterialsType()
            {
                SimpleLit = new MaterialInfo("Materials/SimpleWhite");
                Lit = new MaterialInfo("Materials/White");
                TexturedLit = new MaterialInfo("Materials/Textured Lit");
                TransparentLit = new MaterialInfo("Materials/Transparent Lit");
                TransparentTexturedLit = new MaterialInfo("Materials/Transparent Textured Lit");
                ImagePreview = new MaterialInfo("Materials/ImagePreview");
                MeshList = new MaterialInfo("Materials/MeshList Material");
                Grid = new MaterialInfo("Materials/Grid");
                GridInterior = new MaterialInfo("Materials/Grid Interior");
                GridMap = new MaterialInfo("Materials/GridMap");
                DepthImageProjector = new MaterialInfo("Materials/DepthImage Material");

                PointCloud = new MaterialInfo("Materials/PointCloud Material");
                PointCloudWithColormap = new MaterialInfo("Materials/PointCloud Material with Colormap");

                Line = new MaterialInfo("Materials/Line Material");
                TransparentLine = new MaterialInfo("Materials/Transparent Line Material");
                LineWithColormap = new MaterialInfo("Materials/Line Material with Colormap");
                TransparentLineWithColormap = new MaterialInfo("Materials/Transparent Line Material with Colormap");

                LitOcclusionOnly = new MaterialInfo("Materials/White OcclusionOnly");
                MeshListOcclusionOnly = new MaterialInfo("Materials/MeshList OcclusionOnly");
            }
        }

        public class TexturedMaterialsType
        {
            readonly Dictionary<Texture, Material> materialsByTexture = new Dictionary<Texture, Material>();
            readonly Dictionary<Texture, Material> materialsByTextureAlpha = new Dictionary<Texture, Material>();

            public Material Get([NotNull] Texture texture)
            {
                if (texture is null)
                {
                    throw new ArgumentNullException(nameof(texture));
                }

                if (materialsByTexture.TryGetValue(texture, out Material material))
                {
                    return material;
                }

                material = Materials.TexturedLit.Instantiate();
                material.mainTexture = texture;
                material.name = Materials.TexturedLit.Name + " - " + materialsByTexture.Count;
                materialsByTexture[texture] = material;
                return material;
            }

            public Material GetAlpha([NotNull] Texture texture)
            {
                if (texture is null)
                {
                    throw new ArgumentNullException(nameof(texture));
                }

                if (materialsByTextureAlpha.TryGetValue(texture, out Material material))
                {
                    return material;
                }

                material = Materials.TransparentTexturedLit.Instantiate();
                material.mainTexture = texture;
                material.name = Materials.TransparentTexturedLit.Name + " - " + materialsByTextureAlpha.Count;
                materialsByTextureAlpha[texture] = material;
                return material;
            }
        }

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
                    textures[(ColormapId) i] = UnityEngine.Resources.Load<Texture2D>("Colormaps/" + Names[i]);
                }

                Textures = new ReadOnlyDictionary<ColormapId, Texture2D>(textures);
            }
        }

        public class DisplaysType
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
            public GameObjectInfo AxisFrame { get; }
            public GameObjectInfo AngleAxis { get; }
            public GameObjectInfo Trail { get; }
            public GameObjectInfo InteractiveControl { get; }
            public GameObjectInfo GridMap { get; }

            public ReadOnlyDictionary<Type, GameObjectInfo> ResourceByType { get; }

            public DisplaysType()
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
                Square = new GameObjectInfo("Displays/Plane");
                Line = new GameObjectInfo("Displays/Line");
                Grid = new GameObjectInfo("Displays/Grid");
                Axis = new GameObjectInfo("Displays/Axis");
                DepthImageResource = new GameObjectInfo("Displays/DepthImageResource");
                OccupancyGridResource = new GameObjectInfo("Displays/OccupancyGridResource");
                RadialScanResource = new GameObjectInfo("Displays/RadialScanResource");
                ARMarkerResource = new GameObjectInfo("Displays/ARMarkerResource");
                AxisFrame = new GameObjectInfo("Displays/AxisFrameResource");
                AngleAxis = new GameObjectInfo("Displays/AngleAxis");
                Trail = new GameObjectInfo("Displays/Trail");
                InteractiveControl = new GameObjectInfo("Displays/InteractiveControl");
                GridMap = new GameObjectInfo("Displays/GridMap");

                ResourceByType = new ReadOnlyDictionary<Type, GameObjectInfo>(CreateTypeDictionary());
            }

            Dictionary<Type, GameObjectInfo> CreateTypeDictionary()
            {
                Dictionary<Type, GameObjectInfo> resourceByType = new Dictionary<Type, GameObjectInfo>();
                PropertyInfo[] properties = GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (!typeof(GameObjectInfo).IsAssignableFrom(property.PropertyType))
                    {
                        continue;
                    }

                    GameObjectInfo info = (GameObjectInfo) property.GetValue(this);
                    IDisplay display = info.Object.GetComponent<IDisplay>();
                    Type type = display?.GetType();
                    string name = type?.FullName;
                    if (name is null)
                    {
                        continue;
                    }

                    if (resourceByType.ContainsKey(type))
                    {
                        resourceByType[type] = null; // not unique! invalidate
                        continue;
                    }

                    resourceByType[type] = info;
                }

                return resourceByType;
            }

            public bool TryGetResource(Type type, out GameObjectInfo info)
            {
                return ResourceByType.TryGetValue(type, out info) && info != null;
            }
        }

        public class ControllersType
        {
            public GameObjectInfo AR { get; }

            public ControllersType()
            {
                AR = new GameObjectInfo("Listeners/AR");
            }
        }

        public class WidgetsType
        {
            public GameObjectInfo DisplayButton { get; }
            public GameObjectInfo TopicsButton { get; }
            public GameObjectInfo ItemListPanel { get; }
            public GameObjectInfo ConnectionPanel { get; }
            public GameObjectInfo ImagePanel { get; }
            public GameObjectInfo TFPanel { get; }
            public GameObjectInfo SaveAsPanel { get; }
            public GameObjectInfo AddTopicPanel { get; }

            public GameObjectInfo HeadTitle { get; }
            public GameObjectInfo SectionTitle { get; }
            public GameObjectInfo Toggle { get; }
            public GameObjectInfo Slider { get; }
            public GameObjectInfo Input { get; }
            public GameObjectInfo ShortInput { get; }
            public GameObjectInfo NumberInput { get; }
            public GameObjectInfo Dropdown { get; }
            public GameObjectInfo ColorPicker { get; }
            public GameObjectInfo ImagePreview { get; }
            public GameObjectInfo CloseButton { get; }
            public GameObjectInfo TrashButton { get; }
            public GameObjectInfo DataLabel { get; }
            public GameObjectInfo ToggleButton { get; }
            public GameObjectInfo Vector3 { get; }
            public GameObjectInfo Sender { get; }
            public GameObjectInfo Listener { get; }
            public GameObjectInfo Frame { get; }
            public GameObjectInfo Vector3Slider { get; }
            public GameObjectInfo InputWithHints { get; }

            public WidgetsType()
            {
                DisplayButton = new GameObjectInfo("Widgets/Display Button");
                TopicsButton = new GameObjectInfo("Widgets/Topics Button");
                ItemListPanel = new GameObjectInfo("Widgets/Item List Panel");
                ConnectionPanel = new GameObjectInfo("Widgets/Connection Panel");
                ImagePanel = new GameObjectInfo("Widgets/Image Panel");
                TFPanel = new GameObjectInfo("Widgets/TF Tree Panel");
                SaveAsPanel = new GameObjectInfo("Widgets/Save As Panel");
                AddTopicPanel = new GameObjectInfo("Widgets/Add Topic Panel");

                HeadTitle = new GameObjectInfo("Widgets/Head Title");
                SectionTitle = new GameObjectInfo("Widgets/Section Title");
                Toggle = new GameObjectInfo("Widgets/Toggle");
                Slider = new GameObjectInfo("Widgets/Slider");
                Input = new GameObjectInfo("Widgets/Input Field");
                ShortInput = new GameObjectInfo("Widgets/Short Input Field");
                NumberInput = new GameObjectInfo("Widgets/Number Input Field");
                ColorPicker = new GameObjectInfo("Widgets/ColorPicker");
                ImagePreview = new GameObjectInfo("Widgets/Image Preview");
                Dropdown = new GameObjectInfo("Widgets/Dropdown");
                CloseButton = new GameObjectInfo("Widgets/Close Button");
                TrashButton = new GameObjectInfo("Widgets/Trash Button");
                DataLabel = new GameObjectInfo("Widgets/Data Label");
                ToggleButton = new GameObjectInfo("Widgets/Toggle Button");
                Vector3 = new GameObjectInfo("Widgets/Vector3");
                Sender = new GameObjectInfo("Widgets/Sender");
                Listener = new GameObjectInfo("Widgets/Listener");
                Frame = new GameObjectInfo("Widgets/Frame");
                Vector3Slider = new GameObjectInfo("Widgets/Vector3 Slider");
                InputWithHints = new GameObjectInfo("Widgets/Input Field With Hints");
            }
        }

        public class RobotsType
        {
            public ReadOnlyCollection<string> Names { get; } =
                new ReadOnlyCollection<string>(new[]
                    {
                        "edu.iviz.dummybot",
                        "com.clearpath.husky",
                        "com.willowgarage.pr2",
                        "edu.fraunhofer.iosb.bob",
                        "edu.fraunhofer.iosb.crayler",
                        "edu.kit.h2t.armar6",
                        "edu.kit.ipr.gammabot",
                        "edu.fraunhofer.iosb.e2",
                        "test_car"
                    }
                );

            public ReadOnlyDictionary<string, GameObjectInfo> Objects { get; }

            public RobotsType()
            {
                Dictionary<string, GameObjectInfo> objects = Names.ToDictionary(
                    name => name,
                    name => new GameObjectInfo("Robots/" + name));
                Objects = new ReadOnlyDictionary<string, GameObjectInfo>(objects);
            }
        }

        public class FontInfo
        {
            readonly Font font;
            readonly int dotWidth;
            readonly int arrowWidth;
            readonly Dictionary<char, int> charWidths = new Dictionary<char, int>();

            public FontInfo()
            {
                font = UnityEngine.Resources.Load<Font>("Fonts/Montserrat Real NonDynamic");
                dotWidth = CharWidth('.') * 3; // ...
                arrowWidth = CharWidth('→') + CharWidth(' ');
            }

            public string Split(string s, int maxWidth, int maxLines = 2)
            {
                int usableWidth = maxWidth - dotWidth;
                StringBuilder str = new StringBuilder();
                int usedWidth = 0;
                int numLines = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    int charWidth = CharWidth(s[i]);
                    if (usedWidth + charWidth > usableWidth)
                    {
                        if (i >= s.Length - 2)
                        {
                            str.Append(s[i]);
                            continue;
                        }

                        if (numLines != maxLines - 1)
                        {
                            str.Append("...\n→ ").Append(s[i]);
                            usedWidth = arrowWidth;
                            numLines = 1;
                        }
                        else
                        {
                            str.Append("...");
                            return str.ToString();
                        }
                    }
                    else
                    {
                        str.Append(s[i]);
                        usedWidth += charWidth;
                    }
                }

                return str.ToString();
            }

            int CharWidth(char c)
            {
                if (charWidths.TryGetValue(c, out int width))
                {
                    return width;
                }

                font.GetCharacterInfo(c, out CharacterInfo ci, 12);
                charWidths[c] = ci.advance;
                return ci.advance;
            }
        }

        public const int ClickableLayer = 8;

        static MaterialsType materials;
        public static MaterialsType Materials => materials ?? (materials = new MaterialsType());

        static ColormapsType colormaps;
        public static ColormapsType Colormaps => colormaps ?? (colormaps = new ColormapsType());

        static DisplaysType displays;
        public static DisplaysType Displays => displays ?? (displays = new DisplaysType());

        static ControllersType controllers;
        public static ControllersType Controllers => controllers ?? (controllers = new ControllersType());

        static WidgetsType widgets;
        public static WidgetsType Widgets => widgets ?? (widgets = new WidgetsType());

        static RobotsType robots;
        public static RobotsType Robots => robots ?? (robots = new RobotsType());

        public static ColorScheme Colors { get; } = new ColorScheme();

        static TexturedMaterialsType texturedMaterials;

        public static TexturedMaterialsType TexturedMaterials =>
            texturedMaterials ?? (texturedMaterials = new TexturedMaterialsType());

        static FontInfo fontInfo;
        public static FontInfo Font => fontInfo ?? (fontInfo = new FontInfo());

        static InternalResourceManager internals;

        static InternalResourceManager Internal =>
            internals ?? (internals = new InternalResourceManager());

        static ExternalResourceManager external;

        public static ExternalResourceManager External =>
            external ?? (external = new ExternalResourceManager());

        public static bool ContainsRobot(string robotName)
        {
            return Internal.ContainsRobot(robotName) ||
                   External.ContainsRobot(robotName);
        }
        
        public static IEnumerable<string> GetRobotNames()
        {
            return Internal.GetRobotNames().Concat(External.GetRobotNames());
        }

        public static bool TryGetRobot(string robotName, out string robotDescription)
        {
            return Internal.TryGetRobot(robotName, out robotDescription) ||
                   External.TryGetRobot(robotName, out robotDescription);
        }
        
        public static bool TryGetResource(Uri uri, out GameObjectInfo info)
        {
            return Internal.TryGet(uri, out info) ||
                   External.TryGet(uri, out info);
        }

        public static bool TryGetResource(Uri uri, out Info<Texture2D> info)
        {
            return Internal.TryGet(uri, out info) ||
                   External.TryGet(uri, out info);
        }
    }
}