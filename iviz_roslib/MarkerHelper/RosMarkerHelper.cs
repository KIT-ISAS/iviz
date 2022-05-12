using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.Roslib.MarkerHelper;

public static class RosMarkerHelper
{
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

    public static Marker CreateArrow(string ns = "", int id = 0, Point a = default, Point b = default,
        double width = 1, ColorRGBA? color = null, string frameId = "")
    {
        return new Marker
        {
            Header = (0, frameId),
            Ns = ns,
            Id = id,
            Type = Marker.ARROW,
            Action = Marker.ADD,
            Pose = Pose.Identity,
            Scale = (width, 1, 1),
            Color = color ?? ColorRGBA.White,
            FrameLocked = true,
            Points = new[] { a, b }
        };
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
    public static Marker CreateLines(string ns = "", int id = 0, Point[]? lines = null, ColorRGBA[]? colors = null,
        in ColorRGBA? color = null,
        in Pose? pose = null, double width = 1, string frameId = "")
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
            Scale = width * Vector3.One,
            Color = color ?? ColorRGBA.White,
            Points = lines ?? Array.Empty<Point>(),
            Colors = colors ?? Array.Empty<ColorRGBA>(),
            FrameLocked = true
        };
    }

    public static Marker CreateLineStrip(string ns = "", int id = 0, Point[]? lines = null,
        ColorRGBA[]? colors = null,
        ColorRGBA? color = null,
        Pose? pose = null, double width = 1, string frameId = "")
    {
        if (colors != null && lines != null && colors.Length != lines.Length)
        {
            throw new ArgumentException("Number of points and colors must be equal", nameof(colors));
        }

        return new Marker
        {
            Header = (0, frameId),
            Ns = ns,
            Id = id,
            Type = Marker.LINE_STRIP,
            Action = Marker.ADD,
            Pose = pose ?? Pose.Identity,
            Scale = width * Vector3.One,
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
            Ns = ns,
            Id = id,
            Type = (byte)meshType,
            Action = Marker.ADD,
            Pose = pose ?? Pose.Identity,
            Scale = scale ?? Vector3.One,
            Color = color ?? ColorRGBA.White,
            Points = positions ?? Array.Empty<Point>(),
            Colors = colors ?? Array.Empty<ColorRGBA>(),
            FrameLocked = true
        };
    }

    public static Marker CreateTriangleList(string ns = "", int id = 0, Point[]? positions = null,
        ColorRGBA[]? colors = null, ColorRGBA? color = null, Pose? pose = null,
        Vector3? scale = null, string frameId = "")
    {
        if (colors != null && positions != null && colors.Length != positions.Length)
        {
            throw new ArgumentException("Number of points and colors must be equal", nameof(colors));
        }

        return new Marker
        {
            Header = (0, frameId),
            Ns = ns,
            Id = id,
            Type = Marker.TRIANGLE_LIST,
            Action = Marker.ADD,
            Pose = pose ?? Pose.Identity,
            Scale = scale ?? Vector3.One,
            Color = color ?? ColorRGBA.White,
            Points = positions ?? Array.Empty<Point>(),
            Colors = colors ?? Array.Empty<ColorRGBA>(),
            FrameLocked = true
        };
    }
    
    public static Marker CreatePointList(string ns = "", int id = 0, Point[]? positions = null,
        ColorRGBA[]? colors = null, ColorRGBA? color = null, Pose? pose = null,
        double scale = 1, string frameId = "")
    {
        if (colors != null && positions != null && colors.Length != positions.Length)
        {
            throw new ArgumentException("Number of points and colors must be equal", nameof(colors));
        }

        return new Marker
        {
            Header = (0, frameId),
            Ns = ns,
            Id = id,
            Type = Marker.POINTS,
            Action = Marker.ADD,
            Pose = pose ?? Pose.Identity,
            Scale = Vector3.One.WithX(scale),
            Color = color ?? ColorRGBA.White,
            Points = positions ?? Array.Empty<Point>(),
            Colors = colors ?? Array.Empty<ColorRGBA>(),
            FrameLocked = true
        };
    }

    public static Marker CreateResource(string resource, string ns = "", int id = 0, Pose? pose = null,
        Vector3? scale = null, string frameId = "")
    {
        return new Marker
        {
            Header = (0, frameId),
            Ns = ns,
            Id = id,
            Type = Marker.MESH_RESOURCE,
            Action = Marker.ADD,
            Pose = pose ?? Pose.Identity,
            Scale = scale ?? Vector3.One,
            Color = ColorRGBA.White,
            MeshResource = resource,
            FrameLocked = true
        };
    }

    public static Marker CreateDelete(string ns = "", int id = 0)
    {
        return new Marker
        {
            Ns = ns,
            Id = id,
            Action = Marker.DELETE,
        };
    }
    
    public static Marker CreateDeleteAll()
    {
        return new Marker
        {
            Action = Marker.DELETEALL,
        };
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

public class RosInteractiveMarkerHelper
{
    public static InteractiveMarker Create(string name, Pose? pose = null, string description = "", float scale = 1,
        string frameId = "", params InteractiveMarkerControl[] controls)
    {
        return new InteractiveMarker
        {
            Header = frameId,
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
            InteractionMode = (byte)mode,
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

        public static implicit operator MenuEntry((string title, uint id) p) => new(p.title, p.id);

        public static implicit operator MenuEntry((string title, uint id, uint parentId) p) =>
            new(p.title, p.id, p.parentId);
    }


    public static InteractiveMarker CreateMenu(string name = "", Pose? pose = null,
        float scale = 1, string frameId = "", Marker? controlMarker = null, params MenuEntry[] entries)
    {
        return new InteractiveMarker
        {
            Header = frameId,
            Name = name,
            Pose = pose ?? Pose.Identity,
            Scale = scale,
            Controls =
                new[]
                {
                    CreateControl(mode: RosInteractionMode.Menu,
                        markers: controlMarker ?? RosMarkerHelper.CreateSphere())
                },
            MenuEntries = entries.Select(entry => new Msgs.VisualizationMsgs.MenuEntry
                { Id = entry.Id, ParentId = entry.ParentId, Title = entry.Title }).ToArray()
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
            new(p.name, p.pose);

        public static implicit operator PoseUpdate((string name, Pose pose, string parent) p) =>
            new(p.name, p.pose, p.parent);
    }

    public static InteractiveMarkerUpdate CreatePoseUpdate(params PoseUpdate[] args)
    {
        return new InteractiveMarkerUpdate
        {
            Type = InteractiveMarkerUpdate.UPDATE,
            Poses = args.Select(tuple => new InteractiveMarkerPose
                    { Header = tuple.Parent, Name = tuple.Name, Pose = tuple.Pose })
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
        var erase = new InteractiveMarkerUpdate
        {
            Type = InteractiveMarkerUpdate.UPDATE,
            Erases = args
        };
        return erase;
    }
}