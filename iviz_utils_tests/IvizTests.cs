using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.RemoteLib;
using Iviz.Roslib;
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
                    bytes[v * w + u] = (sbyte) (MathF.Sin((i + u + v) / 50f) * 50 + 48);
                }
            }
                
            writer.Write(msg);
            Thread.Sleep(5);
        }
    }

    [Test]
    public void TestMagnitude()
    {
        var writer = client.CreateWriter<Twist>("/magnitude");

        var ivizController = new IvizController(client, IvizId);
        ivizController.AddModuleFromTopic("/magnitude");

        foreach (int i in ..2000)
        {
            var t = new Twist
            {
                Angular = new Vector3(0, 0, i/10.0 * Math.PI / 180),
                //Angular = float.NaN * Vector3.One,
                Linear = Vector3.Zero
            };
        
            writer.Write(t);
            Thread.Sleep(5);
        }
    }

}