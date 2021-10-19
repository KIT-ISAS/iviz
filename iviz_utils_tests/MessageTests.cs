using System;
using System.Linq;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsGen;
using Iviz.MsgsGen.Dynamic;
using Iviz.Tools;
using NUnit.Framework;
using Buffer = Iviz.Msgs.Buffer;

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
        public void TestFindingMessage()
        {
            Assert.NotNull(BuiltIns.TryGetTypeFromMessageName("std_msgs/Header"));
            Assert.Null(BuiltIns.TryGetTypeFromMessageName("std_msgs/NonExistingMessage"));
            
            // these two are equivalent
            Assert.NotNull(BuiltIns.TryGetTypeFromMessageName("dummy_namespace/DummyMessage", "Iviz.UtilsTests"));
            Assert.NotNull(BuiltIns.TryGetTypeFromMessageName("DummyNamespace/DummyMessage", "Iviz.UtilsTests"));
        }

        [Test]
        public void TestDynamicMessage()
        {
            const string definition =
                "uint32 seq\n" +
                "time stamp\n" +
                "string frame_id";

            DynamicMessage message =
                DynamicMessage.CreateFromDependencyString("iviz_test/Header", definition);

            Assert.True(message.Fields.Count == 3);

            Assert.True(message.Fields[0].TrySet((uint) 1));
            Assert.False(message.Fields[0].TrySet(0)); // int, not uint
            Assert.False(message.Fields[0].TrySet("a"));

            time now = new time(DateTime.Now);
            Assert.True(message.Fields[1].TrySet(now));
            Assert.False(message.Fields[1].TrySet("a"));

            Assert.True(message.Fields[2].TrySet("map"));
            Assert.False(message.Fields[2].TrySet(0));

            var realHeader = new Header(1, now, "map");

            Assert.True(message.RosInstanceMd5Sum == Header.RosMd5Sum);
            Assert.True(message.RosMessageLength == realHeader.RosMessageLength);

            byte[] messageBytes = message.SerializeToArray();
            byte[] otherMessageBytes = realHeader.SerializeToArray();

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

            DynamicMessage message =
                DynamicMessage.CreateFromDependencyString("iviz_test/TransformStamped", fullDependencies);

            var realTransformStamped = new TransformStamped();

            Assert.True(message.RosInstanceMd5Sum == TransformStamped.RosMd5Sum);
            Assert.True(message.RosMessageLength == realTransformStamped.RosMessageLength);

            byte[] messageBytes = message.SerializeToArray();
            byte[] otherMessageBytes = realTransformStamped.SerializeToArray();

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

            var rent2 = new UniqueRef<string>(200, true);
            Assert.True(rent2.Array != null && rent2.Array.Length >= rent2.Length);

            string[] rent2Array = rent2.Array;

            rent2.Dispose();

            Assert.Catch<IndexOutOfRangeException>(() =>
            {
                string _ = rent2[0];
            });

            Assert.Catch<ObjectDisposedException>(() =>
            {
                var _ = rent2.Array;
            });

            Assert.True(rent2Array[0] == null);
        }
    }

    namespace DummyNamespace
    {
        public sealed class DummyMessage : IMessage
        {
            public void RosSerialize(ref Buffer b) => throw new NotImplementedException();
            public int RosMessageLength => 0;
            public void RosValidate() => throw new NotImplementedException();
            public ISerializable RosDeserialize(ref Buffer b) => throw new NotImplementedException();
            public string RosType => "";
        }
    }
}