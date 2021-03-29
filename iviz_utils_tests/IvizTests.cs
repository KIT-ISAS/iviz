using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Iviz.UtilsTests
{
    [Category("Iviz")]
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
            public string SourceParameter { get; init; }
            public string SavedRobotName { get; init; }
            public string FramePrefix { get; init; }
            public string FrameSuffix { get; init; }
            public bool AttachedToTf { get; init; }
            public bool RenderAsOcclusionOnly { get; init; }
            public SerializableColor Tint { get; init; }
            public float Metallic { get; init; }
            public float Smoothness { get; init; }
            public string Id { get; init; }
            public bool Visible { get; init; }
            public ModuleType ModuleType => ModuleType.Robot;
        }

        public record SerializableColor
        {
            public float R { get; init; }
            public float G { get; init; }
            public float B { get; init; }
            public float A { get; init; }

            public static implicit operator SerializableColor(in ColorRGBA color) => new()
                {R = color.R, G = color.G, B = color.B, A = color.A};
        }
    }
}