using System;
using System.Threading.Tasks;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.RemoteLib;
using Iviz.Roslib;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Iviz.UtilsTests
{
    [Category("Iviz")]
    public class IvizTests
    {
        static readonly Uri CallerUri = new Uri("http://localhost:7615");
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
            var remoteController = new RemoteController(client, IvizId);
            const string requestedId = "test_robot";
            string moduleId = remoteController.AddModule(AddModuleType.Robot, requestedId);

            Assert.True(moduleId == requestedId);

            var config = new RemoteLib.RobotConfiguration
            {
                SavedRobotName = "[Fraunhofer IOSB] BoB",
                AttachedToTf = false,
                Tint = ColorRGBA.White,
            };

            remoteController.UpdateModule(moduleId, config);
        }
    }
}