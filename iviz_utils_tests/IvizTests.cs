using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.RemoteLib;
using Iviz.Roslib;
using Iviz.Roslib.MarkerHelper;
using Iviz.Roslib2;
using Iviz.Tools;
using JetBrains.Annotations;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Path = Iviz.Msgs.NavMsgs.Path;
using Point = Iviz.Msgs.GeometryMsgs.Point;

namespace Iviz.UtilsTests;

[Category("Iviz")]
public class IvizTests
{
    static readonly Uri CallerUri = new Uri("http://localhost:7616");
    static readonly Uri MasterUri = new Uri("http://localhost:11311");

    //static readonly Uri CallerUri = new Uri("http://192.168.0.157:7616");
    //static readonly Uri MasterUri = new Uri("http://192.168.0.220:11311");

    //static readonly Uri CallerUri = new Uri("http://141.3.59.19:7616");
    //static readonly Uri MasterUri = new Uri("http://141.3.59.5:11311");

    const string CallerId = "iviz_util_tests";

    const string IvizId = "iviz_osxeditor";
    //const string IvizId = "/iviz_iphoneplayer";

    IRosClient client;


    [SetUp]
    public void Setup()
    {
        //client ??= new RosClient(MasterUri, CallerId, CallerUri);
        
        const string ivizRootPath = "../../../../";
        const string ros2LibPath = ivizRootPath +
                                   "iviz_ros2_rcl/lib/iviz_ros2_rcl_macos.bundle/Contents/MacOS/iviz_ros2_rcl_macos";
        Ros2Client.RemapRclWrapperLibrary(ros2LibPath);
        client ??= new Ros2Client(CallerId);
    }

    [Test]
    public void TestAddModule()
    {
        var ivizController = new IvizController(client, IvizId);
        const string requestedId = "test_robot";
        string moduleId = ivizController.AddModule(AddModuleType.Robot, requestedId);

        Assert.True(moduleId == requestedId);

        var config = new RemoteLib.RobotConfiguration
        {
            SavedRobotName = "[Fraunhofer IOSB] BoB",
            AttachedToTf = false,
            Tint = ColorRGBA.White,
        };

        ivizController.UpdateModule(moduleId, config);
    }

    [Test]
    public void TestTf2()
    {
        //var ivizController = new IvizController(client, IvizId);
        //ivizController.ResetModule("/tf");

        var writer = client.CreateWriter<TFMessage>("/tf");
        writer.WaitForAnySubscriber();
        Thread.Sleep(100);

        uint s = 0;
        foreach (int _ in ..10000)
        {
            foreach (int __ in ..100)
            {
                int i = Random.Shared.Next(0, 10000);
                var p = new Vector3(Random.Shared.NextDouble(), Random.Shared.NextDouble(), 0);
                var transform = new TransformStamped(
                    new Header(s++, time.Now(), ""), "frame_" + i + "äää", Transform.Identity.WithTranslation(p)
                );
                var message = new TFMessage(new[] { transform });
                writer.Write(message);
            }

            Thread.Sleep(1);
        }
    }

    [Test]
    public void TestOccupancyGrid()
    {
        using var writer = client.CreateWriter<OccupancyGrid>("/occupancy_grid");

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/occupancy_grid");

        writer.WaitForAnySubscriber();
        Thread.Sleep(100);
        uint s = 0;
        const int w = 100;
        const int h = 100;

        sbyte[] bytes = new sbyte[w * h];


        var msg = new OccupancyGrid
        {
            Info = new MapMetaData
            {
                Height = h,
                Origin = Pose.Identity,
                Resolution = 0.05f,
                Width = w
            },
            Header = new Header(s++, time.Now(), ""),
            Data = bytes
        };

        foreach (int i in ..2000)
        {
            foreach (int v in ..h)
            {
                foreach (int u in ..w)
                {
                    bytes[v * w + u] = (sbyte)(MathF.Sin((i + u + v) / 50f) * 50 + 48);
                }
            }

            writer.Write(msg);
            Thread.Sleep(5);
        }
    }

    [Test]
    public void TestTf3()
    {
        var ivizController = new IvizController(client, IvizId);
        ivizController.UpdateModule("/tf", new TFConfiguration
        {
            FrameLabelsVisible = false,
            Interactable = true
        });

        Assert.Throws<RemoteException>(() =>
            ivizController.UpdateModule("/tf", new MarkerConfiguration()));
    }

    [Test]
    public void TestMagnitude()
    {
        using var writer = client.CreateWriter<Twist>("/magnitude");

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/magnitude");
        ivizController.UpdateModule("/magnitude", new MagnitudeConfiguration
        {
            VectorScale = 0.1f,
            TrailVisible = true
        });

        foreach (int i in ..2000)
        {
            var t = new Twist
            {
                Angular = new Vector3(0, 0, i / 10.0 * Math.PI / 180),
                Linear = Quaternion.AngleAxis(i / 2.0 * Math.PI / 180, Vector3.UnitZ) *
                         ((Vector3.UnitX + Vector3.UnitZ) * i * 0.01)
            };

            ivizController.UpdateModule("/magnitude", new MagnitudeConfiguration
            {
                VectorColor = ColorRGBA.Blue
            });

            writer.Write(t);
            Thread.Sleep(5);
        }
    }

    [Test]
    public void TestMarkers()
    {
        using var writer = client.CreateWriter<MarkerArray>("/markers", true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/markers");

        var markers = new List<Marker>();
        markers.Add(RosMarkerHelper.CreateArrow(id: 1, a: (0, 0, 0), b: (0, 0, 1), width: 0.2, color: ColorRGBA.Green));

        markers.Add(RosMarkerHelper.CreateArrow(id: 2, pose: Pose.Identity.WithPosition(1, 0, 0), color: ColorRGBA.Blue,
            scale: (1, 0.1, 0.1)));

        markers.Add(RosMarkerHelper.CreateArrow(id: 2, pose: Pose.Identity.WithPosition(1, 0, 0),
            color: ColorRGBA.Yellow, scale: (1, 0.25, 0.25)));

        markers.Add(RosMarkerHelper.CreateCube(id: 3, pose: Pose.Identity.WithPosition(0, 1, 0), color: ColorRGBA.Red,
            scale: (1, 0.5, 0.25)));

        markers.Add(RosMarkerHelper.CreateSphere(id: 4, pose: Pose.Identity.WithPosition(1, 1, 0),
            color: ColorRGBA.Green, scale: (1, 0.5, 0.25)));

        markers.Add(RosMarkerHelper.CreateCube(id: 5, pose: Pose.Identity.WithPosition(2, 1, 0), color: ColorRGBA.Blue,
            scale: (2, 1, 0.5)));

        markers.Add(RosMarkerHelper.CreateCylinder(id: 5, pose: Pose.Identity.WithPosition(2, 1, 0),
            color: ColorRGBA.Blue, scale: (1, 0.5, 0.5)));

        var marker = RosMarkerHelper.CreateCylinder(id: 66, pose: Pose.Identity.WithPosition(3, 1, 0),
            color: ColorRGBA.Grey, scale: (0.5, 0.5, 0.5));
        marker.Lifetime = TimeSpan.FromSeconds(3);
        markers.Add(marker);

        markers.Add(RosMarkerHelper.CreateTextViewFacing(id: 6, position: (0, 2, 0), color: ColorRGBA.Blue, scale: 2,
            text: "lol"));

        markers.Add(RosMarkerHelper.CreateTextViewFacing(id: 6, position: (0, 2, 0), color: ColorRGBA.Green,
            scale: 0.25, text: "LOL"));

        markers.Add(RosMarkerHelper.CreateTextViewFacing(id: 7, position: (1, 2, 0), color: ColorRGBA.Red, scale: 0.5,
            text: "ABCD"));

        markers.Add(RosMarkerHelper.CreateTextViewFacing(id: 7, position: (1, 2, 0), color: ColorRGBA.Red, scale: 0.5,
            text: "ABCD\neee"));

        //----------------

        var points = new List<Point>();
        for (int i = 0; i < 2 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
        }

        markers.Add(RosMarkerHelper.CreateLineStrip(id: 8, pose: Pose.Identity.WithPosition(0, 3, 0),
            color: ColorRGBA.Blue, lines: points.ToArray(), width: 0.01));

        points.Clear();
        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
        }

        markers.Add(RosMarkerHelper.CreateLineStrip(id: 8, pose: Pose.Identity.WithPosition(0, 3, 0),
            color: ColorRGBA.Red, lines: points.ToArray(), width: 0.01));

        var colors = new List<ColorRGBA>();
        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            var color = new ColorRGBA(0, MathF.Cos(angle) * 0.5f + 0.5f, 0, 1);
            colors.Add(color);
        }

        markers.Add(RosMarkerHelper.CreateLineStrip(id: 9, pose: Pose.Identity.WithPosition(2, 3, 0),
            colors: colors.ToArray(), lines: points.ToArray(), width: 0.01));
        markers.Add(RosMarkerHelper.CreateLineStrip(id: 10, pose: Pose.Identity.WithPosition(2, 3, 0),
            colors: colors.ToArray(), lines: points.ToArray(), width: 0.01));

        colors.Clear();
        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            var color = new ColorRGBA(0, 0, 1, MathF.Cos(angle) * 0.5f + 0.5f);
            colors.Add(color);
        }

        markers.Add(RosMarkerHelper.CreateLineStrip(id: 10, pose: Pose.Identity.WithPosition(4, 3, 0),
            colors: colors.ToArray(), lines: points.ToArray(), width: 0.01));


        //----------------

        points.Clear();
        points.Add((1, 0, 0));

        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
            points.Add(point);
        }

        points.RemoveAt(points.Count - 1);

        markers.Add(RosMarkerHelper.CreateLines(id: 11, pose: Pose.Identity.WithPosition(0, 5, 0), color: ColorRGBA.Red,
            lines: points.ToArray(), width: 0.01));

        colors.Clear();
        colors.Add((1, 0, 0));
        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            var color = new ColorRGBA(0, MathF.Cos(angle) * 0.5f + 0.5f, 0, 1);
            colors.Add(color);
            colors.Add(color);
        }

        colors.RemoveAt(colors.Count - 1);

        markers.Add(RosMarkerHelper.CreateLines(id: 12, pose: Pose.Identity.WithPosition(2, 5, 0),
            colors: colors.ToArray(), lines: points.ToArray(), width: 0.01));

        //----------------

        markers.Add(RosMarkerHelper.CreatePointList(id: 13, pose: Pose.Identity.WithPosition(0, 7, 0),
            positions: points.ToArray(), scale: -0.05));

        markers.Add(RosMarkerHelper.CreateMeshList(id: 14, pose: Pose.Identity.WithPosition(2, 7, 0),
            positions: points.ToArray(), scale: (-0.05, 0.05, 0.05)));

        //----------------

        points.Clear();
        points.Add((0, 0, 0));
        for (int i = 0; i < 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
        }

        var indices = new List<Point>();
        for (int i = 0; i < 360 / 9; i++)
        {
            indices.Add(points[0]);
            indices.Add(points[i]);
            indices.Add(points[i + 1]);
        }

        markers.Add(RosMarkerHelper.CreateTriangleList(id: 15, pose: Pose.Identity.WithPosition(0, 9, 0),
            positions: indices.ToArray(), scale: (1, 1, -5)));

        indices[^1] *= float.PositiveInfinity;
        markers.Add(RosMarkerHelper.CreateTriangleList(id: 16, pose: Pose.Identity.WithPosition(2, 9, 0),
            positions: indices.ToArray(), scale: (1, 1, 5)));

        writer.Write(new MarkerArray(markers.ToArray()));

        Thread.Sleep(1000);
    }

    [Test]
    public void TestInteractiveMarkers()
    {
        using var writer = client.CreateWriter<InteractiveMarkerUpdate>("/interactive_markers/update", true);
        using var writer2 = client.CreateWriter<InteractiveMarkerInit>("/interactive_markers/update_full", true);

        //var ivizController = new IvizController(client, IvizId);
        //ivizController.AddModuleFromTopic("/interactive_markers");


        var marker1 = RosMarkerHelper.CreateCube(scale: 0.5 * Vector3.One);
        var control1 =
            RosInteractiveMarkerHelper.CreateControl(mode: RosInteractionMode.Button, markers: marker1);

        //var marker2 = RosMarkerHelper.CreateCube(scale: 0.5 * Vector3.One, pose: Pose.Identity.WithPosition(2, 0, 0));
        var control2 =
            RosInteractiveMarkerHelper.CreateControl(mode: RosInteractionMode.MoveAxis, markers: Array.Empty<Marker>());

        var imarker =
            RosInteractiveMarkerHelper.Create(frameId: "map", name: "marker", controls: new[] { control1, control2 });
        var update = RosInteractiveMarkerHelper.CreateMarkerUpdate(args: imarker);

        var init = new InteractiveMarkerInit
        {
            Markers = new[] { imarker },
            ServerId = "iviz"
        };

        writer2.Write(init);
        writer.Write(update);

        Thread.Sleep(10000);
    }

    [Test]
    public void TestMarkers2()
    {
        using var writer = client.CreateWriter<MarkerArray>("/markers", true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/markers");
        var markers = new List<Marker>();

        var points = new List<Point>();
        for (int i = 0; i < 2 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
        }

        int o = 0;
        for (int j = 0; j < 32; j++)
        {
            for (int i = 0; i < 32; i++)
            {
                markers.Add(RosMarkerHelper.CreateLineStrip(id: o++, pose: Pose.Identity.WithPosition(i, j, 0),
                    color: ColorRGBA.Blue, lines: points.ToArray(), width: 0.01));
            }
        }

        writer.Write(new MarkerArray(markers.ToArray()));

        Thread.Sleep(1000);
    }

    [Test]
    public void TestImages()
    {
        using var writer = client.CreateWriter<CompressedImage>("/compressed_image", true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/compressed_image");

        var image = new CompressedImage();
        image.Format = "png";


        string path;


        path = "../../../images/grey.png";
        image.Data = File.ReadAllBytes(path);
        writer.Write(image);
        Thread.Sleep(1000);

        path = "../../../images/mask-2.png";
        image.Data = File.ReadAllBytes(path);
        writer.Write(image);
        Thread.Sleep(1000);

        path = "../../../images/mask-2.png";
        image.Data = File.ReadAllBytes(path);
        image.Data.AsSpan()[100..200].Fill(0); // break image
        writer.Write(image);
        Thread.Sleep(1000);

        path = "../../../images/testorig.jpg";
        image.Data = File.ReadAllBytes(path);
        writer.Write(image);
        Thread.Sleep(1000);

        image.Format = "whee";
        writer.Write(image);
        Thread.Sleep(1000);


        path = "../../../images/testorig.jpg";
        image.Data = File.ReadAllBytes(path);
        image.Format = "jpg";
        writer.Write(image);
        Thread.Sleep(1000);


        image.Format = "jpg";
        image.Data.AsSpan()[100..200].Fill(0); // break image
        writer.Write(image);
        Thread.Sleep(1000);
    }


    [Test]
    public void TestGridmap()
    {
        using var writer = client.CreateWriter<GridMap>("/gridmap");

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/gridmap");

        writer.WaitForAnySubscriber();
        Thread.Sleep(100);
        //uint s = 0;
        const int w = 100;
        const int h = 100;

        float[] data = new float[w * h];

        var msg = CreateGridmap(w, h);
        msg.Data[0].Data = data;

        foreach (int i in ..2000)
        {
            foreach (int v in ..h)
            {
                foreach (int u in ..w)
                {
                    data[v * w + u] = MathF.Sin((i + u + v) / 50f);
                }
            }

            writer.Write(msg);
            Thread.Sleep(5);
        }
    }


    [Test]
    public void TestGridmap2()
    {
        using var writer = client.CreateWriter<GridMap>("/gridmap", true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/gridmap");

        writer.WaitForAnySubscriber();
        Thread.Sleep(100);
        const int w = 100;
        const int h = 100;

        float[] data = new float[w * h];

        var msg = CreateGridmap(w, h);
        msg.Layers = new[] { "elevation" };
        msg.Data[0].Data = data;

        data[0] = 10;
        writer.Write(msg);
        Thread.Sleep(3600 * 1000);
    }

    [NotNull]
    static GridMap CreateGridmap(uint w, uint h)
    {
        return new GridMap
        {
            Info = new GridMapInfo
            {
                LengthX = h * 0.05f,
                LengthY = w * 0.05f,
                Pose = Pose.Identity,
                Resolution = 0.05f,
                Header = new Header(0, time.Now(), "map"),
            },
            Data = new Float32MultiArray[]
            {
                new Float32MultiArray
                {
                    Layout = new MultiArrayLayout
                    {
                        Dim = new MultiArrayDimension[]
                        {
                            new MultiArrayDimension
                            {
                                Label = "column_index",
                                Size = h,
                                Stride = w * h
                            },
                            new MultiArrayDimension
                            {
                                Label = "row_index",
                                Size = w,
                                Stride = w
                            }
                        }
                    },
                }
            }
        };
    }

    [Test]
    public void TestGridmap3()
    {
        using var writer = client.CreateWriter<GridMap>("/gridmap");

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/gridmap");

        writer.WaitForAnySubscriber();
        Thread.Sleep(100);

        uint w = 600, h = 600;

        var msg = new GridMap
        {
            Info = new GridMapInfo
            {
                LengthX = h * 0.05f,
                LengthY = w * 0.05f,
                Pose = Pose.Identity,
                Resolution = 0.01f,
                Header = new Header(0, time.Now(), ""),
            },
            Data = new Float32MultiArray[]
            {
                new Float32MultiArray
                {
                    Layout = new MultiArrayLayout
                    {
                        Dim = new MultiArrayDimension[]
                        {
                            new MultiArrayDimension
                            {
                                Label = "column_index",
                                Size = h,
                                Stride = w * h
                            },
                            new MultiArrayDimension
                            {
                                Label = "row_index",
                                Size = w,
                                Stride = w
                            }
                        }
                    },
                }
            }
        };

        writer.Write(msg);
        Thread.Sleep(2000);
    }

    [Test]
    public void TestPointCloud()
    {
        using var writer = client.CreateWriter<PointCloud2>("/pointcloud");

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/pointcloud");

        writer.WaitForAnySubscriber();
        Thread.Sleep(100);
        const int w = 100;
        const int h = 100;

        byte[] data = new byte[w * h * Unsafe.SizeOf<Float4>()];
        var data4 = MemoryMarshal.Cast<byte, Float4>(data);

        var msg = new PointCloud2
        {
            Data = data,
            Fields = new[]
            {
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "x", Offset = 0, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "y", Offset = 4, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "z", Offset = 8, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "i", Offset = 12, },
            },
            Width = w,
            Height = h,
            PointStep = (uint)Unsafe.SizeOf<Float4>(),
            RowStep = (uint)Unsafe.SizeOf<Float4>() * w,
        };
        foreach (int i in ..2000)
        {
            foreach (int v in ..h)
            {
                foreach (int u in ..w)
                {
                    float I = MathF.Sin((i + u + v) / 50f);
                    data4[v * w + u] = new Float4
                    {
                        x = u / 100f,
                        y = v / 100f,
                        z = I,
                        w = I
                    };
                }
            }

            writer.Write(msg);
            Thread.Sleep(5);
        }
    }

    [Test]
    public void TestPointCloud2()
    {
        using var writer = client.CreateWriter<PointCloud2>("/pointcloud");

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/pointcloud");

        writer.WaitForAnySubscriber();
        Thread.Sleep(100);
        const int w = 100;
        const int h = 100;

        byte[] data = new byte[w * h * Unsafe.SizeOf<Float3>()];
        var data4 = MemoryMarshal.Cast<byte, Float3>(data);

        var msg = new PointCloud2
        {
            Data = data,
            Fields = new[]
            {
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "x", Offset = 0, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "y", Offset = 4, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "z", Offset = 8, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "i", Offset = 8, },
            },
            Width = w,
            Height = h,
            PointStep = (uint)Unsafe.SizeOf<Float3>(),
            RowStep = (uint)Unsafe.SizeOf<Float3>() * w,
        };
        foreach (int i in ..2000)
        {
            foreach (int v in ..h)
            {
                foreach (int u in ..w)
                {
                    float I = MathF.Sin((i + u + v) / 50f);
                    data4[v * w + u] = new Float3
                    {
                        x = u / 100f,
                        y = v / 100f,
                        z = I,
                    };
                }
            }

            writer.Write(msg);
            Thread.Sleep(5);
        }
    }

    [Test]
    public void TestPointCloud3()
    {
        using var writer = client.CreateWriter<PointCloud2>("/pointcloud");

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/pointcloud");

        writer.WaitForAnySubscriber();
        Thread.Sleep(100);
        const int w = 100;
        const int h = 100;

        byte[] data = new byte[w * h * Unsafe.SizeOf<Float3>()];
        var data4 = MemoryMarshal.Cast<byte, Float3>(data);

        var msg = new PointCloud2
        {
            Fields = new[]
            {
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "x", Offset = 0, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "y", Offset = 4, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "z", Offset = 8, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "i", Offset = 8, },
            },
            Width = w,
            Height = h,
            PointStep = (uint)Unsafe.SizeOf<Float3>(),
            RowStep = (uint)Unsafe.SizeOf<Float3>() * w,
        };

        Console.WriteLine("Sending!");
        writer.Write(msg);
        Thread.Sleep(2000);

        msg = new PointCloud2
        {
            Data = data,
            Fields = new[]
            {
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "x", Offset = uint.MaxValue, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "y", Offset = 4, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "z", Offset = 8, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "i", Offset = uint.MaxValue, },
            },
            Width = w,
            Height = h,
            PointStep = (uint)Unsafe.SizeOf<Float3>(),
            RowStep = (uint)Unsafe.SizeOf<Float3>() * w,
        };

        Console.WriteLine("Sending!");
        writer.Write(msg);
        Thread.Sleep(2000);

        msg = new PointCloud2
        {
            Data = data,
            Fields = new[]
            {
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "x", Offset = 0, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "y", Offset = 4, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "z", Offset = 8, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "i", Offset = 8, },
            },
            Width = uint.MaxValue,
            Height = h,
            PointStep = (uint)Unsafe.SizeOf<Float3>(),
            RowStep = (uint)Unsafe.SizeOf<Float3>() * w,
        };

        Console.WriteLine("Sending!");
        writer.Write(msg);
        Thread.Sleep(2000);

        msg = new PointCloud2
        {
            Data = data,
            Fields = new[]
            {
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "x", Offset = 0, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "y", Offset = 4, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "z", Offset = 8, },
                new PointField { Count = 1, Datatype = PointField.FLOAT32, Name = "i", Offset = 8, },
            },
            Width = w,
            Height = h,
            PointStep = uint.MaxValue,
            RowStep = uint.MaxValue,
        };

        Console.WriteLine("Sending!");
        writer.Write(msg);
        Thread.Sleep(2000);
    }

    [Test]
    public void TestImage()
    {
        using var writer = client.CreateWriter<Image>("/image", true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/image");

        var image = new Image();
        image.Encoding = "rgb";
        image.Height = 0;
        image.Width = 240;
        image.Step = 240;

        writer.Write(image);
        Thread.Sleep(2000);
    }

    [Test, Ignore("SDF is disabled")]
    public void TestWorld()
    {
        using var writer = client.CreateWriter<Marker>("/markers", true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/markers");

        var marker = RosMarkerHelper.CreateResource("package://aws-robomaker-hospital-world/worlds/hospital.world");

        writer.Write(marker);
        Thread.Sleep(2000);
    }

    [Test]
    public void TestSdf()
    {
        using var writer = client.CreateWriter<Marker>("/markers", latchingEnabled: true);

        //var ivizController = new IvizController(client, IvizId);
        //ivizController.AddModuleFromTopic("/markers");

        var marker = RosMarkerHelper.CreateResource("package://aws-robomaker-hospital-world/worlds/hospital.world");
        writer.Write(marker);
        Thread.Sleep(5000);
    }

    [Test]
    public void TestDialogs()
    {
        using var writer = client.CreateWriter<DialogArray>("/dialogs", latchingEnabled: true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/dialogs");

        var header = new Header(0, time.Now(), "dialogs");

        var dialogArray = new DialogArray
        {
            Dialogs = new[]
            {
                new Dialog
                {
                    Header = header,
                    Id = "1",
                    Type = Dialog.TYPE_SHORT,
                    Caption = "Abcd\nAbcd\nAbcd\nAbcdAbcd\nAbcd\nAbcd\nAbcd",
                    Title = "Title",
                    TfOffset = new Vector3(0, 0, 0),
                    DialogDisplacement = new Vector3(0, 1, 0),
                    BindingType = 1,
                },
                new Dialog
                {
                    Header = header,
                    Id = "2",
                    Type = Dialog.TYPE_SHORT,
                    Caption =
                        "AbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcdAbcd",
                    Title = "Title",
                    TfOffset = new Vector3(1, 0, 1),
                    Scale = 0.5f,
                },
                new Dialog
                {
                    Header = header,
                    Id = "3",
                    Type = Dialog.TYPE_SHORT,
                    Caption =
                        "Abcd",
                    Title = "Tralalala",
                    TfOffset = new Vector3(2, 0, 1)
                },
                new Dialog
                {
                    Header = header,
                    Id = "10",
                    Type = Dialog.TYPE_NOTICE,
                    Caption = "Abcd\nAbcd\nAbcd\nAbcdAbcd\nAbcd\nAbcd\nAbcd",
                    Icon = Dialog.ICON_OK,
                    TfOffset = new Vector3(0, 2, 1),
                    BindingType = 1,
                },
                new Dialog
                {
                    Header = header,
                    Id = "11",
                    Type = Dialog.TYPE_NOTICE,
                    Caption = "EVERYTHING WENT\nWRONG!",
                    Icon = Dialog.ICON_CROSS,
                    Scale = 0.5f,
                    TfOffset = new Vector3(1, 2, 1),
                },
                new Dialog
                {
                    Header = header,
                    Id = "20",
                    Type = Dialog.TYPE_BUTTON,
                    Caption = "Oh no",
                    Icon = Dialog.ICON_QUESTION,
                    TfOffset = new Vector3(0, 4, 1),
                    BindingType = 1,
                },
                new Dialog
                {
                    Header = header,
                    Id = "21",
                    Type = Dialog.TYPE_BUTTON,
                    Caption = "Oh no, but now with longer text!!",
                    Icon = Dialog.ICON_WARN,
                    Scale = 0.5f,
                    TfOffset = new Vector3(1, 4, 1)
                },
                new Dialog
                {
                    Header = header,
                    Id = "22",
                    Type = Dialog.TYPE_BUTTON,
                    Caption = "Oh no in red",
                    Icon = Dialog.ICON_WARN,
                    BackgroundColor = ColorRGBA.Red,
                    TfOffset = new Vector3(2, 4, 1)
                },
                new Dialog
                {
                    Header = header,
                    Id = "23",
                    Type = Dialog.TYPE_BUTTON,
                    Caption = "Oh no transparent",
                    Icon = Dialog.ICON_WARN,
                    BackgroundColor = ColorRGBA.Red.WithAlpha(0.5f),
                    TfOffset = new Vector3(3, 4, 1)
                },
                new Dialog
                {
                    Header = header,
                    Id = "30",
                    Type = Dialog.TYPE_ICON,
                    Title = "does",
                    Caption = "Maybee",
                    Icon = Dialog.ICON_WARN,
                    Buttons = Dialog.BUTTONS_OK,
                    TfOffset = new Vector3(0, 6, 1),
                    BindingType = 1
                },
                new Dialog
                {
                    Header = header,
                    Id = "31",
                    Type = Dialog.TYPE_ICON,
                    Title = "does this work",
                    Caption = "with a much longer text? We need to try it out! Surely nothing can go wrong with this.",
                    Icon = Dialog.ICON_WARN,
                    Buttons = Dialog.BUTTONS_OKCANCEL,
                    TfOffset = new Vector3(1, 6, 1),
                },
                new Dialog
                {
                    Header = header,
                    Id = "32",
                    Type = Dialog.TYPE_ICON,
                    Title = "does this work",
                    Caption = "with a bit shorter text? Maybe?",
                    Icon = Dialog.ICON_WARN,
                    Buttons = Dialog.BUTTONS_OK,
                    TfOffset = new Vector3(3, 6, 1)
                },
                new Dialog
                {
                    Header = header,
                    Id = "40",
                    Type = Dialog.TYPE_PLAIN,
                    Title = "Testing",
                    Caption = "plain now. Will this ever end?",
                    Buttons = Dialog.BUTTONS_OK,
                    TfOffset = new Vector3(0, 8, 1),
                    BindingType = 1,
                },
                new Dialog
                {
                    Header = header,
                    Id = "41",
                    Type = Dialog.TYPE_PLAIN,
                    Title = "Testing",
                    Caption = "What\nIf\nEverything\nGoes\nWrong\n!!!!",
                    Buttons = Dialog.BUTTONS_OK,
                    TfOffset = new Vector3(1, 8, 1),
                    BindingType = 1,
                },
            }
        };

        writer.Write(dialogArray);

        Thread.Sleep(2000);
    }

    [Test]
    public void TestDialogService()
    {
        var header = new Header(0, time.Now(), "dialogs");
        var service = new LaunchDialog();
        service.Request = new LaunchDialogRequest
        {
            Dialog = new Dialog
            {
                Header = header,
                Id = "0",
                Type = Dialog.TYPE_PLAIN,
                Title = "Testing",
                Caption = "services now!",
                Buttons = Dialog.BUTTONS_OK,
                TfOffset = new Vector3(1, 8, 1),
                BindingType = 1,
            }
        };
        client.CallService($"{IvizId}/launch_dialog", service);
        Console.WriteLine(service.Response.Message);
    }

    [Test]
    public void TestRobotPreviews()
    {
        using var writer = client.CreateWriter<RobotPreview>("/previews", latchingEnabled: true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/previews");

        var header = new Header(0, time.Now(), "previews");

        var preview = new RobotPreview
        {
            Header = header,
            Id = "0",
            SavedRobotName = "[Franka Emika] Panda",
        };

        writer.Write(preview);

        Thread.Sleep(2000);
    }

    [Test]
    public void TestBoundaries()
    {
        using var writer = client.CreateWriter<Boundary>("/boundaries", latchingEnabled: true);
        using var reader = client.CreateReader<Feedback>("/boundaries/feedback");
        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/boundaries");

        var header = new Header(0, time.Now(), "boundaries");

        var boundary = new Boundary
        {
            Header = header,
            Id = "0",
            Type = Boundary.TYPE_CIRCLE_HIGHLIGHT,
            Behavior = Boundary.BEHAVIOR_COLLIDER,
            Color = ColorRGBA.White,
            SecondColor = ColorRGBA.Red.WithAlpha(0.5f),
            Scale = Vector3.One
        };

        writer.Write(boundary);
        Thread.Sleep(500);

        var collidable = new Boundary
        {
            Header = header,
            Id = "1",
            Behavior = Boundary.BEHAVIOR_NOTIFY_COLLISION,
            Color = ColorRGBA.White,
            SecondColor = ColorRGBA.Blue.WithAlpha(0.5f),
            Scale = Vector3.One,
            Pose = new Pose().WithPosition(-2, 0, 0)
        };

        writer.Write(collidable);
        Thread.Sleep(500);

        var ts = new CancellationTokenSource();
        var task = Task.Run(() => ReadFeedback(reader, ts.Token), ts.Token);

        for (int i = 0; i < 100; i++)
        {
            float x = ((i / 100f) - 0.5f) * 4;
            var updatedCollidable = new Boundary
            {
                Header = header,
                Id = "1",
                Behavior = Boundary.BEHAVIOR_NOTIFY_COLLISION,
                Color = ColorRGBA.White,
                SecondColor = ColorRGBA.Blue.WithAlpha(0.5f),
                Scale = Vector3.One,
                Pose = new Pose().WithPosition(x, 0, 0)
            };
            writer.Write(updatedCollidable);
            Thread.Sleep(50);
        }

        Thread.Sleep(2000);

        ts.Cancel();

        Assert.IsTrue(task.IsCompletedSuccessfully);

        task.Wait();
    }

    async ValueTask ReadFeedback(RosChannelReader<Feedback> reader, CancellationToken token)
    {
        await foreach (var msg in reader.ReadAllAsync(token))
        {
            //Console.WriteLine(msg);
            if (msg.Type == Feedback.TYPE_COLLIDER_ENTERED) return;
        }

        throw new Exception("No collision detected!");
    }


    [StructLayout(LayoutKind.Sequential)]
    struct Float4
    {
        public float x, y, z, w;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Float3
    {
        public float x, y, z;
    }
}