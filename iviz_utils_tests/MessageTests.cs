using System;
using System.Linq;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsGen;
using Iviz.MsgsGen.Dynamic;
using Iviz.Tools;
using NUnit.Framework;

namespace Iviz.UtilsTests
{
    [Category("Message")]
    public class MessageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestMessageConsistency()
        {
            var header = new Header(1, new time(10, 20), "abcd");
            var log = new Msgs.RosgraphMsgs.Log{
                Header = header, 
                Level = 1, 
                Name = "name", 
                Msg = "message", 
                File = "file", 
                Function = "function",
                Line = 2000, 
                Topics = new[] { "topic1, topic2" }
            };
            
            byte[] logArray = log.SerializeToArrayRos1();
            Assert.AreEqual(BaseUtils.GetMd5Hash(logArray), "2da79d032c7392f78627aff94eda903a");

            var otherLog = log.DeserializeRos1(logArray);
            Assert.AreEqual(log.Name, otherLog.Name);
            Assert.AreEqual(log.Msg, otherLog.Msg);
            Assert.AreEqual("", otherLog.Function); // marked as ignore!
            Assert.AreEqual(log.Level, otherLog.Level);

            double[] eye = new double[36];
            foreach (int i in 1..6)
            {
                eye[i * 7] = 1;
            }

            var cov = new TwistWithCovarianceStamped(header,
                new TwistWithCovariance(new Twist(Vector3.UnitX, Vector3.UnitZ), eye));

            byte[] covArray = cov.SerializeToArrayRos1();
            Assert.AreEqual(BaseUtils.GetMd5Hash(covArray), "35ca07d018d5627c247bc4f9cbaccefc");

            var otherCov = cov.DeserializeRos1(covArray);
            Assert.AreEqual(cov.Twist.Twist.Angular, otherCov.Twist.Twist.Angular);
            Assert.AreEqual(cov.Twist.Twist.Linear, otherCov.Twist.Twist.Linear);
            CollectionAssert.AreEqual(cov.Twist.Covariance, otherCov.Twist.Covariance);
            
            Assert.AreEqual(TwistWithCovarianceStamped.Md5Sum, "8927a1a12fb2607ceea095b2dc440a96");
            Assert.AreEqual(Msgs.RosgraphMsgs.Log.Md5Sum, "acffd30cd6b6de30f120938c17c593fb");
            Assert.AreEqual(Msgs.RclInterfaces.Log.Md5Sum, "2e550226619c297add0debbcdcf7d29b");
            
            Assert.AreEqual(Model.Md5Sum, "bd9be904104258bcedbf207e42db2852");
            Assert.AreEqual(ModelTexture.Md5Sum, "0c05ed3d1750fc865d4edeecbd425ef0");
        }


        [Test]
        public void TestFindingMessage()
        {
            Assert.NotNull(BuiltIns.TryGetGeneratorFromMessageName("std_msgs/Header"));
            Assert.Null(BuiltIns.TryGetGeneratorFromMessageName("std_msgs/NonExistingMessage"));

            // these two are equivalent
            Assert.NotNull(BuiltIns.TryGetGeneratorFromMessageName("dummy_namespace/DummyMessage", "Iviz.UtilsTests"));
            Assert.NotNull(BuiltIns.TryGetGeneratorFromMessageName("DummyNamespace/DummyMessage", "Iviz.UtilsTests"));
        }

        [Test]
        public void TestDynamicMessage()
        {
            const string definition =
                "uint32 seq\n" +
                "time stamp\n" +
                "string frame_id";

            var message =
                DynamicMessage.CreateFromDependencyString("iviz_test/Header", definition);

            Assert.True(message.Fields.Count == 3);

            Assert.True(message.Fields[0].TrySet((uint)1));
            Assert.False(message.Fields[0].TrySet(0)); // int, not uint
            Assert.False(message.Fields[0].TrySet("a"));

            time now = new time(DateTime.Now);
            Assert.True(message.Fields[1].TrySet(now));
            Assert.False(message.Fields[1].TrySet("a"));

            Assert.True(message.Fields[2].TrySet("map"));
            Assert.False(message.Fields[2].TrySet(0));

            var realHeader = new Header(1, now, "map");

            Assert.True(message.RosMd5Sum == new Header().RosMd5Sum);
            Assert.True(message.RosMessageLength == realHeader.RosMessageLength);

            byte[] messageBytes = message.SerializeToArrayRos1();
            byte[] otherMessageBytes = realHeader.SerializeToArrayRos1();

            Assert.True(messageBytes.SequenceEqual(otherMessageBytes));
        }

        [Test]
        public void TestDynamicMessageWrongDefinition()
        {
            const string wrongDefinition =
                "float seq      #no float type!\n" +
                "time stamp                    \n" +
                "string frame_id               \n";

            Assert.Catch<MessageDependencyException>(() =>
                DynamicMessage.CreateFromDependencyString("iviz_test/Header", wrongDefinition));
        }

        [Test]
        public void TestDynamicMessageWithDependencies()
        {
            const string fullDependencies = @"
            Header header
            string child_frame_id # the frame id of the child frame
            geometry_msgs/Transform transform
            ================================================================================
            MSG: std_msgs/Header
            uint32 seq
            time stamp
            string frame_id
            ================================================================================
            MSG: geometry_msgs/Transform
            Vector3 translation
            Quaternion rotation
            ================================================================================
            MSG: geometry_msgs/Vector3
            float64 x
            float64 y
            float64 z
            ================================================================================
            MSG: geometry_msgs/Quaternion
            float64 x
            float64 y
            float64 z
            float64 w
            ";

            var message =
                DynamicMessage.CreateFromDependencyString("iviz_test/TransformStamped", fullDependencies);

            var realTransformStamped = new TransformStamped();

            Assert.True(message.RosMd5Sum == new TransformStamped().RosMd5Sum);
            Assert.True(message.RosMessageLength == realTransformStamped.RosMessageLength);

            byte[] messageBytes = message.SerializeToArrayRos1();
            byte[] otherMessageBytes = realTransformStamped.SerializeToArrayRos1();

            Assert.True(messageBytes.SequenceEqual(otherMessageBytes));
        }

        [Test]
        public void RentTest()
        {
            using (var rent = new Rent<byte>(100))
            {
                Assert.True(rent.Array != null && rent.Array.Length >= rent.Length);
                Assert.Catch<IndexOutOfRangeException>(() =>
                {
                    int _ = rent[100];
                });
            }

            using (var rent = new Rent<byte>(1000))
            {
                Assert.True(rent.Array != null && rent.Array.Length >= rent.Length);
            }

            using (var rent = new Rent<byte>(0))
            {
                Assert.True(rent.Array != null && rent.Array.Length == 0);
            }

            using (var rent = Rent.Empty<byte>())
            {
                Assert.True(rent.Array != null && rent.Array.Length == 0);
            }
        }
    }

    namespace DummyNamespace
    {
        public sealed class DummyMessage : IMessage
        {
            public void RosSerialize(ref WriteBuffer b) => throw new NotImplementedException();
            public int RosMessageLength => 0;
            public void RosValidate() => throw new NotImplementedException();
            public ISerializableRos1 RosDeserializeBase(ref ReadBuffer b) => throw new NotImplementedException();
            public string RosMessageType => "";
            public string RosMd5Sum => "";
            public string RosDependenciesBase64 => "";

            public void Dispose()
            {
            }

            public void RosSerialize(ref WriteBuffer2 b) => throw new RosInvalidMessageForVersion();
            public int Ros2MessageLength => throw new RosInvalidMessageForVersion();
            public int AddRos2MessageLength(int offset) => throw new RosInvalidMessageForVersion();
        }
    }
}