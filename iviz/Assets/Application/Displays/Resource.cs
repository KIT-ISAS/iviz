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
using Iviz.Msgs.NavMsgs;
using System.Text;
using Iviz.App.Listeners;
using Iviz.App.Resources;

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
            Magnitude,
            OccupancyGrid,
            Joystick,
            Path
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
            { PoseStamped.RosMessageType, Module.Magnitude },
            { PointStamped.RosMessageType, Module.Magnitude },
            { WrenchStamped.RosMessageType, Module.Magnitude },
            { Odometry.RosMessageType, Module.Magnitude },
            { TwistStamped.RosMessageType, Module.Magnitude },
            { OccupancyGrid.RosMessageType, Module.OccupancyGrid },
            { Path.RosMessageType, Module.Path },
            { PoseArray.RosMessageType, Module.Path },
        };

        public static ReadOnlyDictionary<string, Module> ResourceByRosMessageType { get; }
            = new ReadOnlyDictionary<string, Module>(resourceByRosMessageType);

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

            public Info(string resourceName, T baseObject)
            {
                this.resourceName = resourceName;
                this.baseObject = baseObject;
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
            public MaterialInfo MeshList { get; }
            public MaterialInfo DepthImageProjector { get; }
            public MaterialInfo Grid { get; }

            public MaterialInfo Line { get; }
            public MaterialInfo TransparentLine { get; }

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
                PointCloud = new MaterialInfo("Materials/PointCloud Material");
                MeshList = new MaterialInfo("Materials/MeshList Material");
                Grid = new MaterialInfo("Materials/Grid");
                DepthImageProjector = new MaterialInfo("Materials/DepthImage Material");

                Line = new MaterialInfo("Materials/Line Material");
                TransparentLine = new MaterialInfo("Materials/Transparent Line Material");

                LitOcclusionOnly = new MaterialInfo("Materials/White OcclusionOnly");
                MeshListOcclusionOnly = new MaterialInfo("Materials/MeshList OcclusionOnly");
            }
        }

        public class TexturedMaterialsType
        {
            readonly Dictionary<Texture, Material> materialsByTexture = new Dictionary<Texture, Material>();
            readonly Dictionary<Texture, Material> materialsByTextureAlpha = new Dictionary<Texture, Material>();

            public Material Get(Texture texture)
            {
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

            public Material GetAlpha(Texture texture)
            {
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
            public GameObjectInfo AxisFrameResource { get; }

            public ReadOnlyDictionary<Uri, GameObjectInfo> Generic { get; }

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
                AxisFrameResource = new GameObjectInfo("Displays/AxisFrameResource");

                Generic = new ReadOnlyDictionary<Uri, GameObjectInfo>(
                    new Dictionary<Uri, GameObjectInfo>()
                    {
                        [CreateUri("Cube")] = Cube,
                        [CreateUri("Cylinder")] = Cylinder,
                        [CreateUri("Sphere")] = Sphere,
                        [CreateUri("RightHand")] = new GameObjectInfo("Displays/RightHand"),
                    });
            }

            static Uri CreateUri(string name)
            {
                return new Uri("package://iviz/" + name);
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
            public GameObjectInfo Frame { get; }

            public WidgetsType()
            {
                DisplayButton = new GameObjectInfo("Widgets/Display Button");
                TopicsButton = new GameObjectInfo("Widgets/Topics Button");
                ItemListPanel = new GameObjectInfo("Widgets/Item List Panel");
                ConnectionPanel = new GameObjectInfo("Widgets/Connection Panel");
                ImagePanel = new GameObjectInfo("Widgets/Image Panel");
                TFPanel = new GameObjectInfo("Widgets/TF Tree Panel");
                SaveAsPanel = new GameObjectInfo("Widgets/Save As Panel");

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
                Frame = new GameObjectInfo("Widgets/Frame");
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

        public class FontInfo
        {
            readonly Font font;
            readonly int dotWidth;
            readonly int arrowWidth;
            readonly Dictionary<char, int> charWidths = new Dictionary<char, int>();

            public FontInfo()
            {
                font = UnityEngine.Resources.Load<Font>("Fonts/Montserrat Real NonDynamic");
                dotWidth = CharWidth('.') * 3;
                arrowWidth = CharWidth('→') + CharWidth(' ');
            }

            public string Split(string s, int maxWidth)
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
                        if (numLines == 0)
                        {
                            str.Append("...\n→ ").Append(s[i]);
                            usedWidth = arrowWidth;
                            numLines = 1;
                        }
                        else if (numLines == 1)
                        {
                            str.Append("...");
                            return str.ToString();
                        }
                    } else
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

        static FontInfo font;
        public static FontInfo Font => font ?? (font = new FontInfo());

        static ExternalResourceManager external;
        public static ExternalResourceManager External => external ?? (external = new ExternalResourceManager());
    }
}