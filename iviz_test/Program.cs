using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Roslib;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using Uri = System.Uri;
using Vector3 = Iviz.Msgs.GeometryMsgs.Vector3;

namespace iviz_test
{
    public sealed class RosMarkerHelper : IDisposable
    {
        readonly string ns;
        readonly List<Marker?> markers = new List<Marker?>();
        bool disposed;

        RosPublisher<MarkerArray>? publisher;
        string? publisherId;

        public RosMarkerHelper(string ns = "Marker")
        {
            this.ns = ns;
        }

        public RosMarkerHelper(RosClient client, string topic = "/markers", string @namespace = "Marker") : this(
            @namespace)
        {
            Start(client, topic);
        }

        public void Start(RosClient client, string topic = "/markers")
        {
            if (publisherId != null)
            {
                throw new InvalidOperationException("Helper has already been started");
            }

            publisherId = client.Advertise(topic, out publisher);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            if (publisher != null && publisherId != null)
            {
                publisher?.Unadvertise(publisherId);
            }
        }

        int GetFreeId()
        {
            int index = markers.FindIndex(marker => marker == null);
            if (index != -1)
            {
                return index;
            }

            markers.Add(null);
            return markers.Count - 1;
        }

        public int CreateArrow(in Pose pose, in ColorRGBA color, in Vector3 scale, string frameId = "map",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateArrow(ns, id, pose, color, scale, frameId);
            return id;
        }

        public static Marker CreateArrow(string ns, int id, in Pose pose, in ColorRGBA color, in Vector3 scale,
            string frameId)
        {
            return new Marker
            {
                Header = new Header
                {
                    FrameId = frameId ?? ""
                },
                Ns = ns,
                Id = id,
                Type = Marker.ARROW,
                Action = Marker.ADD,
                Pose = pose,
                Scale = scale,
                Color = color,
                FrameLocked = true,
            };
        }

        public int CreateArrow(in Point a, in Point b, in ColorRGBA color, double scale = -1, string frameId = "map",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateArrow(ns, id, a, b, color, scale, frameId);
            return id;
        }

        public static Marker CreateArrow(string ns, int id, in Point a, in Point b, in ColorRGBA color, double scale,
            string frameId)
        {
            return new Marker
            {
                Header = new Header
                {
                    FrameId = frameId ?? ""
                },
                Ns = ns,
                Id = id,
                Type = Marker.ARROW,
                Action = Marker.ADD,
                Pose = Pose.Identity,
                Scale = Vector3.One,
                Color = color,
                FrameLocked = true,
                Points = new[] {a, b}
            };
        }

        public int CreateCube(in Point position, in ColorRGBA color, in Vector3 scale, string frameId = "map",
            int replaceId = -1)
        {
            return CreateCube(new Pose(position, Quaternion.Identity), color, scale, frameId, replaceId);
        }

        public int CreateCube(in Pose pose, in ColorRGBA color, in Vector3 scale, string frameId = "map",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateCube(ns, id, pose, scale, color, frameId);
            return id;
        }

        public static Marker CreateCube(string ns, int id, in Pose pose, in Vector3 scale, in ColorRGBA color,
            string frameId)
        {
            if (ns == null)
            {
                throw new ArgumentNullException(nameof(ns));
            }

            return new Marker
            {
                Header = new Header
                {
                    FrameId = frameId ?? ""
                },
                Ns = ns,
                Id = id,
                Type = Marker.CUBE,
                Action = Marker.ADD,
                Pose = pose,
                Scale = scale,
                Color = color,
                FrameLocked = true
            };
        }

        public int CreateSphere(in Point position, in ColorRGBA color, in Vector3 scale, string frameId = "map",
            int replaceId = -1)
        {
            return CreateSphere(new Pose(position, Quaternion.Identity), color, scale, frameId, replaceId);
        }

        public int CreateSphere(in Pose pose, in ColorRGBA color, in Vector3 scale, string frameId = "map",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateSphere(ns, id, pose, scale, color, frameId);
            return id;
        }

        public static Marker CreateSphere(string ns, int id, in Pose pose, in Vector3 scale, in ColorRGBA color,
            string frameId)
        {
            if (ns == null)
            {
                throw new ArgumentNullException(nameof(ns));
            }

            return new Marker
            {
                Header = new Header
                {
                    FrameId = frameId ?? ""
                },
                Ns = ns,
                Id = id,
                Type = Marker.SPHERE,
                Action = Marker.ADD,
                Pose = pose,
                Scale = scale,
                Color = color,
                FrameLocked = true
            };
        }

        public int CreateCylinder(in Point position, in ColorRGBA color, in Vector3 scale, string frameId = "map",
            int replaceId = -1)
        {
            return CreateCylinder(new Pose(position, Quaternion.Identity), color, scale, frameId, replaceId);
        }

        public int CreateCylinder(in Pose pose, in ColorRGBA color, in Vector3 scale, string frameId = "map",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateCylinder(ns, id, pose, scale, color, frameId);
            return id;
        }

        public static Marker CreateCylinder(string ns, int id, in Pose pose, in Vector3 scale, in ColorRGBA color,
            string frameId)
        {
            if (ns == null)
            {
                throw new ArgumentNullException(nameof(ns));
            }

            return new Marker
            {
                Header = new Header
                {
                    FrameId = frameId ?? ""
                },
                Ns = ns,
                Id = id,
                Type = Marker.CYLINDER,
                Action = Marker.ADD,
                Pose = pose,
                Scale = scale,
                Color = color,
                FrameLocked = true
            };
        }

        public int CreateTextViewFacing(string text, in Point position, in ColorRGBA color, double scale,
            string frameId = "map",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            Marker marker = new Marker
            {
                Header = new Header
                {
                    FrameId = frameId ?? ""
                },
                Ns = ns,
                Id = id,
                Type = Marker.TEXT_VIEW_FACING,
                Action = Marker.ADD,
                Pose = new Pose(position, Quaternion.Identity),
                Scale = scale * Vector3.One,
                Color = color,
                FrameLocked = true,
                Text = text
            };

            markers[id] = marker;
            return id;
        }

        public int CreateLines(Point[] lines, in Pose pose, in ColorRGBA color, double scale, string frameId = "map",
            int replaceId = -1)
        {
            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateLines(ns, id, lines, null, color, pose, scale, frameId);
            return id;
        }

        public int CreateLines(Point[] lines, ColorRGBA[] colors, in Pose pose, double scale,
            string frameId = "map", int replaceId = -1)
        {
            if (colors == null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateLines(ns, id, lines, colors, ColorRGBA.White, pose, scale, frameId);
            return id;
        }

        public static Marker CreateLines(string ns, int id, Point[] lines, ColorRGBA[]? colors, in ColorRGBA color,
            in Pose pose, double scale, string frameId)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            if (lines.Length % 2 != 0)
            {
                throw new ArgumentException("Number of points must be even", nameof(lines));
            }

            if (colors != null && colors.Length != lines.Length)
            {
                throw new ArgumentException("Number of points and colors must be equal", nameof(colors));
            }

            return new Marker
            {
                Header = new Header
                {
                    FrameId = frameId ?? ""
                },
                Ns = ns,
                Id = id,
                Type = Marker.LINE_LIST,
                Action = Marker.ADD,
                Pose = pose,
                Scale = scale * Vector3.One,
                Color = color,
                Points = lines,
                Colors = colors ?? Array.Empty<ColorRGBA>(),
                FrameLocked = true
            };
        }

        public int CreateLineStrip(Point[] lines, in Pose pose, in ColorRGBA color, double scale,
            string frameId = "map",
            int replaceId = -1)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateLines(ns, id, lines, null, color, pose, scale, frameId);
            return id;
        }

        public int CreateLineStrip(Point[] lines, ColorRGBA[] colors, in Pose pose, double scale,
            string frameId = "map",
            int replaceId = -1)
        {
            if (colors == null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            int id = replaceId != -1 ? replaceId : GetFreeId();
            markers[id] = CreateLines(ns, id, lines, colors, ColorRGBA.White, pose, scale, frameId);
            return id;
        }

        public static Marker CreateLineStrip(string ns, int id, Point[] lines, ColorRGBA[]? colors, in ColorRGBA color,
            in Pose pose, double scale, string frameId)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            if (colors != null && colors.Length != lines.Length)
            {
                throw new ArgumentException("Number of points and colors must be equal", nameof(colors));
            }

            return new Marker
            {
                Header = new Header
                {
                    FrameId = frameId ?? ""
                },
                Ns = ns,
                Id = id,
                Type = Marker.LINE_STRIP,
                Action = Marker.ADD,
                Pose = pose,
                Scale = scale * Vector3.One,
                Color = color,
                Points = lines,
                Colors = colors ?? Array.Empty<ColorRGBA>(),
                FrameLocked = true
            };
        }


        public void RemoveMarker(int id)
        {
            if (id < 0 || id >= markers.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            markers[id] = null;
        }

        public void Flush()
        {
            if (publisher == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            Marker[] toSend = markers.Where(marker => marker != null).ToArray()!;
            MarkerArray array = new MarkerArray(toSend);

            publisher.Publish(array);
        }
    }

    static class Program
    {
        static Uri RosMasterUri => RosClient.EnvironmentMasterUri ??
                                   throw new NullReferenceException("Please set the ROS_MASTER_URI variable!");

        public static void Main()
        {
            Console.WriteLine("** Starting Marker test!");

            Logger.Log = Console.WriteLine;
            Logger.LogDebug = Console.WriteLine;
            Logger.LogError = Console.WriteLine;

            using RosClient client = new RosClient(RosMasterUri, callerUri: RosClient.TryGetCallerUri(7632));
            using RosMarkerHelper helper = new RosMarkerHelper(client);

            helper.CreateArrow(Pose.Identity, ColorRGBA.Red, new Vector3(2, 1, 10));

            /*
            helper.CreateArrow(Point.One, Point.Zero, ColorRGBA.Red);
            helper.CreateCube(new Point(1, 0, 0), ColorRGBA.Magenta, 0.5 * Vector3.One);
            helper.CreateSphere(new Point(2, 0, 0), ColorRGBA.Blue, 0.5 * Vector3.One);
            helper.CreateCylinder(new Point(3, 0, 0), ColorRGBA.Yellow, 0.5 * Vector3.One);
            helper.CreateTextViewFacing("Cube", new Point(1, 0, 0.5), ColorRGBA.Yellow, 0.1);
            helper.CreateTextViewFacing("Sphere", new Point(2, 0, 0.5), ColorRGBA.Blue, 0.1);

            Point[] points =
            {
                new Point(-3, -1, 0.5),
                new Point(3, -1, 0.5),
                new Point(3, 1, 0.5),
                new Point(-3, 1, 0.5),
            };

            helper.CreateLines(points, Pose.Identity, ColorRGBA.Red, 0.05f);

            ColorRGBA[] colors =
            {
                ColorRGBA.Red,
                ColorRGBA.Green,
                ColorRGBA.Blue,
                ColorRGBA.White,
            };

            helper.CreateLines(points.Select(x => x + 0.2 * Point.UnitZ).ToArray(), colors,
                Pose.Identity, 0.05f);

            Point[] points2 =
            {
                new Point(-3, -1, 0),
                new Point(3, -1, 0),
                new Point(3, 1, 0),
                new Point(-3, 1, 0),
                new Point(-3, -1, 0),
            };

            helper.CreateLineStrip(points2, Pose.Identity, ColorRGBA.Green, 0.05f);

            ColorRGBA[] colors2 =
            {
                ColorRGBA.Red,
                ColorRGBA.Green,
                ColorRGBA.Blue,
                ColorRGBA.White,
                ColorRGBA.Cyan,
            };

            helper.CreateLineStrip(points2.Select(x => x + 0.2 * Point.UnitZ).ToArray(), colors2,
                Pose.Identity, 0.05f);
                */

            while (true)
            {
                helper.Flush();
                Thread.Sleep(1000);
            }
        }
    }
}