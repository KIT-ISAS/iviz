using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Roslib;
using Iviz.XmlRpc;

namespace Iviz.Roslib.MarkerHelper
{
    public sealed class RosMarker : IDisposable
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
    {
        static readonly Marker InvalidMarker = new Marker();

        readonly string ns;
        readonly List<Marker> markers = new List<Marker>();
        readonly RosChannelWriter<MarkerArray> publisher = new RosChannelWriter<MarkerArray> {LatchingEnabled = true};
        bool disposed;

        public ReadOnlyCollection<Marker> Markers { get; }

        public string Topic => publisher.Topic;

        public RosMarker(string ns)
        {
            this.ns = ns;
            Markers = new ReadOnlyCollection<Marker>(markers);
        }

        public RosMarker(IRosClient client, string topic = "markers", string ns = "Marker") : this(ns)
        {
            Start(client, topic);
        }

        public override string ToString()
        {
            return $"[RosMarkerHelper ns={ns}]";
        }

        public void Start(IRosClient client, string topic = "markers")
        {
            publisher.Start(client, topic);
        }

        public async Task StartAsync(IRosClient client, string topic = "markers")
        {
            await publisher.StartAsync(client, topic);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            if (publisher.IsAlive)
            {
                Clear();
                ApplyChanges();
            }

            publisher.Dispose();
        }

        public async Task DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            if (publisher.IsAlive)
            {
                Clear();
                await ApplyChangesAsync().AwaitNoThrow(this);
            }

            await publisher.DisposeAsync();
        }

#if !NETSTANDARD2_0
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await DisposeAsync();
        }
#endif

        int GetFreeId()
        {
            int index = markers.FindIndex(marker => marker == InvalidMarker);
            if (index != -1)
            {
                return index;
            }

            markers.Add(InvalidMarker);
            return markers.Count - 1;
        }

        public int CreateArrow(in Pose pose, in ColorRGBA color, in Vector3 scale, string frameId = "",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateArrow(ns, id, pose, color, scale, frameId);
            return id;
        }

        public static Marker CreateArrow(string ns = "", int id = 0, Pose? pose = null, ColorRGBA? color = null,
            Vector3? scale = null, string frameId = "")
        {
            return new Marker
            {
                Header = (0, frameId),
                Ns = ns,
                Id = id,
                Type = Marker.ARROW,
                Action = Marker.ADD,
                Pose = pose ?? Pose.Identity,
                Scale = scale ?? Vector3.One,
                Color = color ?? ColorRGBA.White,
                FrameLocked = true,
            };
        }

        public int CreateArrow(in Point a, in Point b, in ColorRGBA color, string frameId = "",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateArrow(ns, id, a, b, color, frameId);
            return id;
        }

        public static Marker CreateArrow(string ns = "", int id = 0, Point a = default, Point b = default,
            ColorRGBA? color = null, string frameId = "")
        {
            return new Marker
            {
                Header = (0, frameId),
                Ns = ns,
                Id = id,
                Type = Marker.ARROW,
                Action = Marker.ADD,
                Pose = Pose.Identity,
                Scale = Vector3.One,
                Color = color ?? ColorRGBA.White,
                FrameLocked = true,
                Points = new[] {a, b}
            };
        }

        public int CreateCube(Pose? pose = null, ColorRGBA? color = null, Vector3? scale = null, string frameId = "",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateCube(ns, id, pose, scale, color, frameId);
            return id;
        }

        public static Marker CreateCube(string ns = "", int id = 0, Pose? pose = null, Vector3? scale = null,
            ColorRGBA? color = null, string frameId = "")
        {
            return new Marker
            {
                Header = (0, frameId),
                Ns = ns,
                Id = id,
                Type = Marker.CUBE,
                Action = Marker.ADD,
                Pose = pose ?? Pose.Identity,
                Scale = scale ?? Vector3.One,
                Color = color ?? ColorRGBA.White,
                FrameLocked = true
            };
        }

        public int CreateSphere(Pose? pose = null, ColorRGBA? color = null, Vector3? scale = null, string frameId = "",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateSphere(ns, id, pose, scale, color, frameId);
            return id;
        }

        public static Marker CreateSphere(string ns = "", int id = 0, Pose? pose = null, Vector3? scale = null,
            ColorRGBA? color = null, string frameId = "")
        {
            return new Marker
            {
                Header = (0, frameId),
                Ns = ns,
                Id = id,
                Type = Marker.SPHERE,
                Action = Marker.ADD,
                Pose = pose ?? Pose.Identity,
                Scale = scale ?? Vector3.One,
                Color = color ?? ColorRGBA.White,
                FrameLocked = true
            };
        }

        public int CreateCylinder(in Point position, in ColorRGBA color, in Vector3 scale, string frameId = "",
            int replaceId = -1)
        {
            return CreateCylinder(new Pose(position, Quaternion.Identity), color, scale, frameId, replaceId);
        }

        public int CreateCylinder(in Pose pose, in ColorRGBA color, in Vector3 scale, string frameId = "",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateCylinder(ns, id, pose, scale, color, frameId);
            return id;
        }

        public static Marker CreateCylinder(string ns = "", int id = 0, Pose? pose = null, Vector3? scale = null,
            ColorRGBA? color = null, string frameId = "")
        {
            return new Marker
            {
                Header = (0, frameId),
                Ns = ns,
                Id = id,
                Type = Marker.CYLINDER,
                Action = Marker.ADD,
                Pose = pose ?? Pose.Identity,
                Scale = scale ?? Vector3.One,
                Color = color ?? ColorRGBA.White,
                FrameLocked = true
            };
        }

        public int CreateTextViewFacing(string text, Point? position = null, ColorRGBA? color = null, double scale = 1,
            string frameId = "",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            Marker marker = CreateTextViewFacing(ns, id, text, position, color, scale, frameId);
            markers[id] = marker;
            return id;
        }

        public static Marker CreateTextViewFacing(string ns = "", int id = 0, string text = "",
            Point? position = null, ColorRGBA? color = null, double scale = 1, string frameId = "")
        {
            return new Marker
            {
                Header = (0, frameId),
                Ns = ns,
                Id = id,
                Type = Marker.TEXT_VIEW_FACING,
                Action = Marker.ADD,
                Pose = Pose.Identity.WithPosition(position ?? Point.Zero),
                Scale = scale * Vector3.One,
                Color = color ?? ColorRGBA.White,
                FrameLocked = true,
                Text = text
            };
        }

        public int CreateLines(Point[] lines, in Pose? pose = null, in ColorRGBA? color = null, double scale = 1,
            string frameId = "",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateLines(ns, id, lines, null, color, pose, scale, frameId);
            return id;
        }

        public int CreateLines(Point[] lines, ColorRGBA[] colors, in Pose? pose = null, double scale = 1,
            string frameId = "", int replaceId = -1)
        {
            if (colors == null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateLines(ns, id, lines, colors, ColorRGBA.White, pose, scale, frameId);
            return id;
        }

        public static Marker CreateLines(string ns = "", int id = 0, Point[]? lines = null, ColorRGBA[]? colors = null,
            in ColorRGBA? color = null,
            in Pose? pose = null, double scale = 1, string frameId = "")
        {
            if (lines != null && lines.Length % 2 != 0)
            {
                throw new ArgumentException("Number of points must be even", nameof(lines));
            }

            if (colors != null && lines != null && colors.Length != lines.Length)
            {
                throw new ArgumentException("Number of points and colors must be equal", nameof(colors));
            }

            return new Marker
            {
                Header = (0, frameId),
                Ns = ns,
                Id = id,
                Type = Marker.LINE_LIST,
                Action = Marker.ADD,
                Pose = pose ?? Pose.Identity,
                Scale = scale * Vector3.One,
                Color = color ?? ColorRGBA.White,
                Points = lines ?? Array.Empty<Point>(),
                Colors = colors ?? Array.Empty<ColorRGBA>(),
                FrameLocked = true
            };
        }

        public int CreateLineStrip(Point[] lines, Pose? pose = null, ColorRGBA? color = null, double scale = 1,
            string frameId = "",
            int replaceId = -1)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateLineStrip(ns, id, lines, null, color, pose, scale, frameId);
            return id;
        }

        public int CreateLineStrip(Point[] lines, ColorRGBA[] colors, Pose? pose = null, double scale = 1,
            string frameId = "", int replaceId = -1)
        {
            if (colors == null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateLineStrip(ns, id, lines, colors, ColorRGBA.White, pose, scale, frameId);
            return id;
        }

        public static Marker CreateLineStrip(string ns = "", int id = 0, Point[]? lines = null,
            ColorRGBA[]? colors = null,
            ColorRGBA? color = null,
            Pose? pose = null, double scale = 1, string frameId = "")
        {
            if (colors != null && lines != null && colors.Length != lines.Length)
            {
                throw new ArgumentException("Number of points and colors must be equal", nameof(colors));
            }

            return new Marker
            {
                Header = (0, frameId),
                Ns = ns ?? "",
                Id = id,
                Type = Marker.LINE_STRIP,
                Action = Marker.ADD,
                Pose = pose ?? Pose.Identity,
                Scale = scale * Vector3.One,
                Color = color ?? ColorRGBA.White,
                Points = lines ?? Array.Empty<Point>(),
                Colors = colors ?? Array.Empty<ColorRGBA>(),
                FrameLocked = true
            };
        }

        public enum MeshType
        {
            Cube = 6,
            Sphere = 7,
        }

        public int CreateMeshList(Point[] positions, MeshType meshType, ColorRGBA? color = null, Pose? pose = null,
            Vector3? scale = null, string frameId = "", int replaceId = -1)
        {
            if (positions == null)
            {
                throw new ArgumentNullException(nameof(positions));
            }

            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateMeshList(ns, id, positions, null, color, meshType, pose, scale, frameId);
            return id;
        }

        public static Marker CreateMeshList(string ns = "", int id = 0, Point[]? positions = null,
            ColorRGBA[]? colors = null, ColorRGBA? color = null, MeshType meshType = MeshType.Cube, Pose? pose = null,
            Vector3? scale = null, string frameId = "")
        {
            if (colors != null && positions != null && colors.Length != positions.Length)
            {
                throw new ArgumentException("Number of points and colors must be equal", nameof(colors));
            }

            return new Marker
            {
                Header = (0, frameId),
                Ns = ns ?? "",
                Id = id,
                Type = (byte) meshType,
                Action = Marker.ADD,
                Pose = pose ?? Pose.Identity,
                Scale = scale ?? Vector3.One,
                Color = color ?? ColorRGBA.White,
                Points = positions ?? Array.Empty<Point>(),
                Colors = colors ?? Array.Empty<ColorRGBA>(),
                FrameLocked = true
            };
        }


        public void Erase(int id)
        {
            if (id < 0 || id >= markers.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            markers[id] = new Marker
            {
                Ns = ns,
                Id = id,
                Action = Marker.DELETE,
            };
        }

        public void Clear()
        {
            for (int id = 0; id < markers.Count; id++)
            {
                if (markers[id] == InvalidMarker)
                {
                    continue;
                }

                markers[id] = new Marker
                {
                    Ns = ns,
                    Id = id,
                    Action = Marker.DELETE,
                };
            }
        }

        public void SetPose(int id, in Pose pose, Header? header = null)
        {
            if (id < 0 || id >= markers.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            markers[id].Pose = pose;
            if (header != null)
            {
                markers[id].Header = header;
            }
        }

        public int Size => markers.Count(marker => marker != InvalidMarker);

        public void ApplyChanges()
        {
            if (!publisher.IsAlive)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            Marker[] toSend = markers.Where(marker => marker != InvalidMarker).ToArray();
            MarkerArray array = new MarkerArray(toSend);

            for (int id = 0; id < markers.Count; id++)
            {
                if (markers[id] != InvalidMarker && markers[id].Action == Marker.DELETE)
                {
                    markers[id] = InvalidMarker;
                }
            }

            publisher.Write(array);
        }

        public async Task ApplyChangesAsync()
        {
            if (!publisher.IsAlive)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            Marker[] toSend = markers.Where(marker => marker != InvalidMarker).ToArray();
            MarkerArray array = new MarkerArray(toSend);

            for (int id = 0; id < markers.Count; id++)
            {
                if (markers[id] != InvalidMarker && markers[id].Action == Marker.DELETE)
                {
                    markers[id] = InvalidMarker;
                }
            }

            await publisher.WriteAsync(array);
        }
    }

    public enum RosInteractionMode
    {
        /// NONE: This control is only meant for visualization; no context menu.
        None = InteractiveMarkerControl.NONE,

        /// MENU: Like NONE, but right-click menu is active.
        Menu = InteractiveMarkerControl.MENU,

        /// BUTTON: Element can be left-clicked.
        Button = InteractiveMarkerControl.BUTTON,

        /// MOVE_AXIS: Translate along local x-axis.
        MoveAxis = InteractiveMarkerControl.MOVE_AXIS,

        /// MOVE_PLANE: Translate in local y-z plane.
        MovePlane = InteractiveMarkerControl.MOVE_PLANE,

        /// ROTATE_AXIS: Rotate around local x-axis.
        RotateAxis = InteractiveMarkerControl.ROTATE_AXIS,

        /// MOVE_ROTATE: Combines MOVE_PLANE and ROTATE_AXIS.
        MoveRotate = InteractiveMarkerControl.MOVE_ROTATE,

        /// MOVE_3D: Translate freely in 3D space.
        Move3D = InteractiveMarkerControl.MOVE_3D,

        /// ROTATE_3D: Rotate freely in 3D space about the origin of parent frame.
        Rotate3D = InteractiveMarkerControl.ROTATE_3D,

        /// MOVE_ROTATE_3D: Full 6-DOF freedom of translation and rotation about the cursor origin.
        MoveRotate3D = InteractiveMarkerControl.MOVE_ROTATE_3D
    }

    public enum RosEventType
    {
        /// KEEP_ALIVE: sent while dragging to keep up control of the marker
        KeepAlive = InteractiveMarkerFeedback.KEEP_ALIVE,

        /// POSE_UPDATE: the pose has been changed using one of the controls
        PoseUpdate = InteractiveMarkerFeedback.POSE_UPDATE,

        /// MENU_SELECT: a menu entry has been selected
        MenuSelect = InteractiveMarkerFeedback.MENU_SELECT,

        /// BUTTON_CLICK: a button control has been clicked
        ButtonClick = InteractiveMarkerFeedback.BUTTON_CLICK,
        MouseDown = InteractiveMarkerFeedback.MOUSE_DOWN,
        MouseUp = InteractiveMarkerFeedback.MOUSE_UP
    }


    public class RosInteractiveMarker
    {
        public static InteractiveMarker Create(string name, Pose? pose = null, string description = "", float scale = 1,
            string frameId = "", params InteractiveMarkerControl[] controls)
        {
            return new InteractiveMarker
            {
                Header = {FrameId = frameId},
                Name = name,
                Description = description,
                Pose = pose ?? Pose.Identity,
                Scale = scale,
                Controls = controls
            };
        }

        public static InteractiveMarkerControl CreateControl(string name = "", Quaternion? orientation = null,
            RosInteractionMode mode = RosInteractionMode.None, params Marker[] markers)
        {
            return new InteractiveMarkerControl
            {
                Name = name,
                Orientation = orientation ?? Quaternion.Identity,
                InteractionMode = (byte) mode,
                Markers = markers
            };
        }

        public class MenuEntry
        {
            public string Title { get; }
            public uint Id { get; }
            public uint ParentId { get; }

            MenuEntry(string title, uint id, uint parentId = 0)
            {
                Title = title ?? throw new ArgumentNullException(nameof(title));
                Id = id;
                ParentId = parentId;

                if (id == 0)
                {
                    throw new ArgumentException("Id cannot be 0");
                }
            }

            public static implicit operator MenuEntry((string title, uint id) p) => new MenuEntry(p.title, p.id);

            public static implicit operator MenuEntry((string title, uint id, uint parentId) p) =>
                new MenuEntry(p.title, p.id, p.parentId);
        }


        public static InteractiveMarker CreateMenu(string name = "", Pose? pose = null,
            float scale = 1, string frameId = "", Marker? controlMarker = null, params MenuEntry[] entries)
        {
            return new InteractiveMarker
            {
                Header = {FrameId = frameId},
                Name = name,
                Pose = pose ?? Pose.Identity,
                Scale = scale,
                Controls =
                    new[]
                    {
                        CreateControl(mode: RosInteractionMode.Menu,
                            markers: controlMarker ?? RosMarker.CreateSphere())
                    },
                MenuEntries = entries.Select(entry => new Iviz.Msgs.VisualizationMsgs.MenuEntry
                    {Id = entry.Id, ParentId = entry.ParentId, Title = entry.Title}).ToArray()
            };
        }

        public static InteractiveMarkerUpdate CreateMarkerUpdate(params InteractiveMarker[] args)
        {
            return new InteractiveMarkerUpdate
            {
                Type = InteractiveMarkerUpdate.UPDATE,
                Markers = args
            };
        }

        public class PoseUpdate
        {
            public string Name { get; }
            public Pose Pose { get; }
            public string Parent { get; }

            PoseUpdate(string name, Pose pose, string parent = "")
            {
                Name = name;
                Pose = pose;
                Parent = parent;
            }

            public static implicit operator PoseUpdate((string name, Pose pose) p) =>
                new PoseUpdate(p.name, p.pose);

            public static implicit operator PoseUpdate((string name, Pose pose, string parent) p) =>
                new PoseUpdate(p.name, p.pose, p.parent);
        }

        public static InteractiveMarkerUpdate CreatePoseUpdate(params PoseUpdate[] args)
        {
            return new InteractiveMarkerUpdate
            {
                Type = InteractiveMarkerUpdate.UPDATE,
                Poses = args.Select(tuple => new InteractiveMarkerPose
                        {Header = {FrameId = tuple.Parent}, Name = tuple.Name, Pose = tuple.Pose})
                    .ToArray()
            };
        }

        public static InteractiveMarkerInit CreateInit(params InteractiveMarker[] markers)
        {
            return new InteractiveMarkerInit
            {
                Markers = markers
            };
        }

        public static InteractiveMarkerUpdate CreateMarkerErase(params string[] args)
        {
            return new InteractiveMarkerUpdate
            {
                Type = InteractiveMarkerUpdate.UPDATE,
                Erases = args
            };
        }
    }
}