using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.MoveitMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.RemoteLib;
using Iviz.Roslib;
using Iviz.Roslib.MarkerHelper;
using Iviz.Tools;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Iviz.UtilsTests;

[Category("Iviz")]
public class IvizTests
{
    static readonly Uri CallerUri = new Uri("http://localhost:7616");
    static readonly Uri MasterUri = new Uri("http://localhost:11311");
    const string CallerId = "/iviz_util_tests";
    const string IvizId = "/iviz_osxeditor";

    RosClient client;


    [SetUp]
    public void Setup()
    {
        client ??= new RosClient(MasterUri, CallerId, CallerUri);
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
                    new Header(s++, time.Now(), ""), "frame_" + i, Transform.Identity.WithTranslation(p)
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
        var writer = client.CreateWriter<OccupancyGrid>("/occupancy_grid");

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
        var writer = client.CreateWriter<Twist>("/magnitude");

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
                Linear = Quaternion.AngleAxis(i / 2.0 * Math.PI / 180, Vector3.UnitZ) * ((Vector3.UnitX + Vector3.UnitZ) * i * 0.01)
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
        var writer = client.CreateWriter<MarkerArray>("/markers", true);

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/markers");

        var markers = new List<Marker>();
        markers.Add(RosMarkerHelper.CreateArrow(id: 1, a: (0, 0, 0), b: (0, 0, 1), width: 0.2, color: ColorRGBA.Green));
        
        markers.Add(RosMarkerHelper.CreateArrow(id: 2, pose: Pose.Identity.WithPosition(1, 0, 0), color: ColorRGBA.Blue, scale: (1, 0.1, 0.1)));

        markers.Add(RosMarkerHelper.CreateArrow(id: 2, pose: Pose.Identity.WithPosition(1, 0, 0), color: ColorRGBA.Yellow, scale: (1, 0.25, 0.25)));

        markers.Add(RosMarkerHelper.CreateCube(id: 3, pose: Pose.Identity.WithPosition(0, 1, 0), color: ColorRGBA.Red, scale: (1, 0.5, 0.25) ));

        markers.Add(RosMarkerHelper.CreateSphere(id: 4, pose: Pose.Identity.WithPosition(1, 1, 0), color: ColorRGBA.Green, scale: (1, 0.5, 0.25) ));

        markers.Add(RosMarkerHelper.CreateCube(id: 5, pose: Pose.Identity.WithPosition(2, 1, 0), color: ColorRGBA.Blue, scale: (2, 1, 0.5) ));

        markers.Add(RosMarkerHelper.CreateCylinder(id: 5, pose: Pose.Identity.WithPosition(2, 1, 0), color: ColorRGBA.Blue, scale: (1, 0.5, 0.5) ));

        markers.Add(RosMarkerHelper.CreateTextViewFacing(id: 6, position: (0, 2, 0), color: ColorRGBA.Blue, scale: 2, text: "lol"));

        markers.Add(RosMarkerHelper.CreateTextViewFacing(id: 6, position: (0, 2, 0), color: ColorRGBA.Green, scale: 0.25, text: "LOL"));

        markers.Add(RosMarkerHelper.CreateTextViewFacing(id: 7, position: (1, 2, 0), color: ColorRGBA.Red, scale: 0.5, text: "ABCD"));

        markers.Add(RosMarkerHelper.CreateTextViewFacing(id: 7, position: (1, 2, 0), color: ColorRGBA.Red, scale: 0.5, text: "ABCD\neee"));

        //----------------
        
        var points = new List<Point>();
        for (int i = 0; i < 2 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
        }

        markers.Add(RosMarkerHelper.CreateLineStrip(id: 8,pose: Pose.Identity.WithPosition(0, 3, 0), color: ColorRGBA.Blue, lines: points.ToArray(), width: 0.01));

        points.Clear();
        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
        }

        markers.Add(RosMarkerHelper.CreateLineStrip(id: 8,pose: Pose.Identity.WithPosition(0, 3, 0), color: ColorRGBA.Red, lines: points.ToArray(), width: 0.01));

        var colors = new List<ColorRGBA>();
        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            var color = new ColorRGBA(0, MathF.Cos(angle) * 0.5f + 0.5f, 0, 1);
            colors.Add(color);
        }
        
        markers.Add(RosMarkerHelper.CreateLineStrip(id: 9,pose: Pose.Identity.WithPosition(2, 3, 0), colors: colors.ToArray(), lines: points.ToArray(), width: 0.01));
        markers.Add(RosMarkerHelper.CreateLineStrip(id: 10,pose: Pose.Identity.WithPosition(2, 3, 0), colors: colors.ToArray(), lines: points.ToArray(), width: 0.01));

        colors.Clear();
        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            var color = new ColorRGBA(0, 0, 1, MathF.Cos(angle) * 0.5f + 0.5f);
            colors.Add(color);
        }

        markers.Add(RosMarkerHelper.CreateLineStrip(id: 10,pose: Pose.Identity.WithPosition(4, 3, 0), colors: colors.ToArray(), lines: points.ToArray(), width: 0.01));


        //----------------
        
        points.Clear();
        points.Add((1, 0,0));

        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
            points.Add(point);
        }

        points.RemoveAt(points.Count - 1);
        
        markers.Add(RosMarkerHelper.CreateLines(id: 11,pose: Pose.Identity.WithPosition(0, 5, 0), color: ColorRGBA.Red, lines: points.ToArray(), width: 0.01));
        
        colors.Clear();
        colors.Add((1, 0,0));
        for (int i = 0; i < 5 * 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            var color = new ColorRGBA(0, MathF.Cos(angle) * 0.5f + 0.5f, 0, 1);
            colors.Add(color);
            colors.Add(color);
        }
        
        colors.RemoveAt(colors.Count - 1);
        
        markers.Add(RosMarkerHelper.CreateLines(id: 12,pose: Pose.Identity.WithPosition(2, 5, 0), colors: colors.ToArray(), lines: points.ToArray(), width: 0.01));

        //----------------

        markers.Add(RosMarkerHelper.CreatePointList(id: 13,pose: Pose.Identity.WithPosition(0, 7, 0), positions: points.ToArray(), scale: 0.05));

        markers.Add(RosMarkerHelper.CreateMeshList(id: 14,pose: Pose.Identity.WithPosition(2, 7, 0), positions: points.ToArray(), scale: (0.05, 0.05, 0.05)));

        //----------------

        points.Clear();
        points.Add((0,0,0));
        for (int i = 0; i < 360; i += 9)
        {
            float angle = i * 180 / MathF.PI;
            Point point = (MathF.Cos(angle), MathF.Sin(angle), i * 0.001);
            points.Add(point);
        }

        var indices = new List<Point>();
        for (int i = 0; i < 360/9; i++)
        {
            indices.Add(points[0]);
            indices.Add(points[i]);
            indices.Add(points[i+1]);
        }
        
        markers.Add(RosMarkerHelper.CreateTriangleList(id: 15,pose: Pose.Identity.WithPosition(0, 9, 0), positions: indices.ToArray(), scale: (1, 1, 5)));

        indices[^1] *= float.PositiveInfinity; 
        markers.Add(RosMarkerHelper.CreateTriangleList(id: 16,pose: Pose.Identity.WithPosition(2, 9, 0), positions: indices.ToArray(), scale: (1, 1, 5)));

        writer.Write(new MarkerArray(markers.ToArray()));
        
        Thread.Sleep(1000);
    }    
}