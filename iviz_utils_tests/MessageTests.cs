using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.RosgraphMsgs;
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
            var log = new Log(header, 1, "name", "message", "file", "function", 2000, new[] { "topic1, topic2" });

            byte[] logArray = log.SerializeToArrayRos1();
            Assert.AreEqual(BaseUtils.GetMd5Hash(logArray), "2da79d032c7392f78627aff94eda903a");

            double[] eye = new double[36];
            foreach (int i in 1..6)
            {
                eye[i * 7] = 1;
            }

            var cov = new TwistWithCovarianceStamped(header,
                new TwistWithCovariance(new Twist(Vector3.UnitX, Vector3.UnitZ), eye));

            byte[] covArray = cov.SerializeToArrayRos1();
            Assert.AreEqual(BaseUtils.GetMd5Hash(covArray), "35ca07d018d5627c247bc4f9cbaccefc");

            Assert.AreEqual(TwistWithCovarianceStamped.Md5Sum, "8927a1a12fb2607ceea095b2dc440a96");
            Assert.AreEqual(Log.Md5Sum, "acffd30cd6b6de30f120938c17c593fb");

            Assert.AreEqual(new TwistWithCovarianceStamped().RosDependenciesBase64,
                "H4sIAAAAAAAAE71VTW/bMAy9+1cQyKHNkGRAO+RQYKcN23oYUKzFPjEUjM3YWm3Jo+Qm3q/fk5y4KdpD" +
                "D1sDA7Fl8pF8fKQndFUZTyqtihcbPLEl8cE0HKSgsDE+0MaECiZrUbG5UO6cFsbCgNbKjcAFlqaBGzft" +
                "IvsgXIhSlf6yqwjxBQhv3C2r4YiQYLPs9T/+ZR8v35+RD8V140v/csgjm9BlQIasBTUSuODAtHbIz5SV" +
                "6LyWW6kppY6C09vQt+IXcEzc4CrFinJd99T5yIoDB03TWZNHEsbS9/7wNJaYWtZg8q5mfcBZRMfl5XeX" +
                "OD1/ewYb6yXvgkFCPRByFfbGlnhJWWdsOD2JDtnkauPmeJQSLI/BKVQcYrKyjb2MebI/Q4wXQ3ELYIMc" +
                "QZTC03E6u8ajnxKCIAVpXV7RMTK/6EPlLACFUstWtUTgHAwA9Sg6HU0PkG2CtmzdHn5AvIvxFFg74saa" +
                "5hV6VsfqfVeCQBi26m5NAdNVn0Dy2kCxVJuVsvZZ9BpCZpN3SZchti91BP/svctNUnXUc+aDRvTUjWtT" +
                "/C81luKgOu0HST4yDHuZ7dvmCe1HpiFKAOkJimoZbKYp7OChgdH9fpENs7Wfpgl9cpt5w7+g7XGeORhQ" +
                "7taJsOV2CZGNU4gRV7NN8YWcmtEcugUpQdRHvUPIa7OVYs7bw02RTKOMz4GvGLRZinHgyypRf8fbGfUz" +
                "+jMjdbsAvHJdoK8UER8cf3v8+Hs6nmbr2nFYvvpxuvx5UMwztu/JDVupuxGLQ+wLg80KNQuUHLcl2zKt" +
                "hbghsGk+Sx6cntLO5O55Z/c81e2i7us7/CagxPjufoGLuMHO085xFhurEcY4otjRE46FUbhGqUSZ4SPi" +
                "VGaggwoH5qyLdDZ8A0jBAoje3LYAwxZWtr4eJJAYpGNZlIsZbSqwmqziAKd1mxa0yUlNaYrBE4Ga0Zlp" +
                "VxxEuj7BKNX1kPMQDMIFyF5w0wWdr6l3HW1iQbjR3XfB0UrGvNL+Cs7N0pAMEPcJvXDoPWjxnkusOusD" +
                "vkgY252EaTve9ePdn+wv5KrBh5MHAAA=");
            Assert.AreEqual(new Log().RosDependenciesBase64,
                "H4sIAAAAAAAAE61TXWvbMBR996+4oIe2g7S0G2ME8pCRpgts7Ugz9lBKUKwbWyBLniQn87/fkdxkGevD" +
                "HhYMkn3POffrRIhCCHrkHXsdezK4GCqdDVHaGBArNn1kmt1+/HY3uSaheNNVA2yILO7nD5MbIlGxZS/N" +
                "aez7dHk/eYfYXnqr7R+82+XyYTn5QIK9d/40Mp+upp8n1+9JbGWU5qpEZbo8Kotc8VyzUbm+TywVe6rz" +
                "MSgMwBB9ymllwySGw20p1kzWKT6Em1Ah2nAIsmI6fN1qk0j5SIxDvEwqW++aI7CzZdTOJvDh+jqh0za+" +
                "vSGjbVLOx+tAMUg/PVN0rS4D0PmSWwggyXjsgtpuY3SoORR/s54WlXWen4ti8p9/xZfHuzGFqNYYX7ga" +
                "VlDAR3CNkl6hqygVtkdb7LbWVc1+NHgLxmpaVpSjsW85XIK4qnUgPC8mMj11AaDo4MWm6Sz2j71GjQGc" +
                "8sHUliS10sMinZEeeOeVtgm+9RhYUscT+EfHtoRfZ+Psby67qFFQD4XSswxpm4sZHfYEQiFWezfCK1cw" +
                "2DH5sAEUyz9bj+2hGBnGyPFmaO4S2hgOI4sKdJ6/rfEaLghJUAK3rqzpHJV/7WP9Ypid9FpuYDcIw+wG" +
                "qmeJdHZxomyztJXWHeQHxd85/kXWHnVTT6MaOzOp+9BVGCCArXc7rQDd9FmkNJpthGc3Xvq+SKwhZSHm" +
                "acYAgZU3glOG4EqNBSja61gf/ykJudaqKH4BX5ZnFnQEAAA=");
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
            public void AddRos2MessageLength(ref int offset) => throw new RosInvalidMessageForVersion();
        }
    }
}