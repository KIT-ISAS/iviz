using System;
using System.Threading.Tasks;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Iviz.UtilsTests
{
    [Category("Iviz"), Ignore("")]
    public class IvizTests
    {
        static readonly Uri CallerUri = new Uri("http://localhost:7613");
        static readonly Uri MasterUri = new Uri("http://localhost:11312");
        const string CallerId = "/iviz_util_tests";
        const string IvizId = "/iviz_osxeditor";

        RosClient client;


        [SetUp]
        public void Setup()
        {
            client ??= new RosClient(MasterUri, CallerId, CallerUri);
        }
        
        [Test]
        public async Task TestAddModule()
        {
            var addModuleResponse = await client.CallServiceAsync(IvizId + "/add_module",
                new AddModuleRequest(nameof(ModuleType.Robot), "test_robot"));
            Assert.True(addModuleResponse.Success);

            string moduleId = addModuleResponse.Id;
            Assert.True(moduleId == addModuleResponse.Id);

            SimpleRobotConfiguration config = new()
            {
                SavedRobotName = "panda",
                AttachedToTf = true,
                Tint = ColorRGBA.Yellow,
            };

            var updateModuleResponse = await client.CallServiceAsync(IvizId + "/update_module",
                new UpdateModuleRequest()
                {
                    Id = moduleId,
                    Config = JsonConvert.SerializeObject(config),
                    Fields = new[]
                    {
                        nameof(SimpleRobotConfiguration.SavedRobotName),
                        nameof(SimpleRobotConfiguration.AttachedToTf),
                        nameof(SimpleRobotConfiguration.Tint),
                    }
                });
            
            Assert.True(updateModuleResponse.Success);
        }

        public enum ModuleType
        {
            Invalid = 0,
            Grid,
            TF,
            PointCloud,
            Image,
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
            Octomap
        }

        public record SimpleRobotConfiguration
        {
            public string SourceParameter { get; set; }
            public string SavedRobotName { get; set; }
            public string FramePrefix { get; set; }
            public string FrameSuffix { get; set; }
            public bool AttachedToTf { get; set; }
            public bool RenderAsOcclusionOnly { get; set; }
            public SerializableColor Tint { get; set; }
            public float Metallic { get; set; }
            public float Smoothness { get; set; }
            public string Id { get; set; }
            public bool Visible { get; set; }
            public ModuleType ModuleType => ModuleType.Robot;
        }

        public record SerializableColor
        {
            public float R { get; set; }
            public float G { get; set; }
            public float B { get; set; }
            public float A { get; set; }

            public static implicit operator SerializableColor(in ColorRGBA color) => new()
                {R = color.R, G = color.G, B = color.B, A = color.A};
        }
    }
}