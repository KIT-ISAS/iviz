
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.MeshMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;
using NUnit.Framework;
using UInt16 = Iviz.Msgs.StdMsgs.UInt16;

namespace Iviz.UtilsTests;

[Category("Message2")]
public class Message2Tests
{
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

        byte[] logArray = log.SerializeToArrayRos2();
        //Assert.AreEqual(BaseUtils.GetMd5Hash(logArray), "2da79d032c7392f78627aff94eda903a");

        var otherLog = log.DeserializeRos2(logArray);
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

        byte[] covArray = cov.SerializeToArrayRos2();
        //Assert.AreEqual(BaseUtils.GetMd5Hash(covArray), "35ca07d018d5627c247bc4f9cbaccefc");

        var otherCov = cov.DeserializeRos2(covArray);
        Assert.AreEqual(cov.Twist.Twist.Angular, otherCov.Twist.Twist.Angular);
        Assert.AreEqual(cov.Twist.Twist.Linear, otherCov.Twist.Twist.Linear);
        CollectionAssert.AreEqual(cov.Twist.Covariance, otherCov.Twist.Covariance);        
    }
    
    [Test]
    public void TestMessageLengths()
    {
        Assert.IsTrue(new String("").Ros2MessageLength == sizeof(int) + 1);
        Assert.IsTrue(new String("abcd").Ros2MessageLength == sizeof(int) + "abcd".Length + 1);
        Assert.IsTrue(new String("abcd").AddRos2MessageLength(1) == 4 + sizeof(int) + "abcd".Length + 1);
        
        
        var h = new Header();
        h.RosValidate();
        Assert.IsTrue(h.Ros2MessageLength == 13);

        Assert.IsTrue(Color32.Ros2FixedMessageLength == 4);
        {
            var c = new Color32();
            Assert.IsTrue(c.AddRos2MessageLength(1) == Color32.Ros2FixedMessageLength + 1);
            Assert.IsTrue(c.AddRos2MessageLength(2) == Color32.Ros2FixedMessageLength + 2);
            Assert.IsTrue(c.AddRos2MessageLength(4) == Color32.Ros2FixedMessageLength + 4);
            Assert.IsTrue(c.AddRos2MessageLength(8) == Color32.Ros2FixedMessageLength + 8);
        }
        
        Assert.IsTrue(UInt16.Ros2FixedMessageLength == sizeof(ushort));
        {
            var c = new UInt16();
            Assert.IsTrue(c.AddRos2MessageLength(1) == UInt16.Ros2FixedMessageLength + 2);
            Assert.IsTrue(c.AddRos2MessageLength(2) == UInt16.Ros2FixedMessageLength + 2);
            Assert.IsTrue(c.AddRos2MessageLength(4) == UInt16.Ros2FixedMessageLength + 4);
            Assert.IsTrue(c.AddRos2MessageLength(8) == UInt16.Ros2FixedMessageLength + 8);
        }

        Assert.IsTrue(ColorRGBA.Ros2FixedMessageLength == 4 * sizeof(float));
        {
            var c = new ColorRGBA();
            Assert.IsTrue(c.AddRos2MessageLength(1) == ColorRGBA.Ros2FixedMessageLength + 4);
            Assert.IsTrue(c.AddRos2MessageLength(2) == ColorRGBA.Ros2FixedMessageLength + 4);
            Assert.IsTrue(c.AddRos2MessageLength(4) == ColorRGBA.Ros2FixedMessageLength + 4);
            Assert.IsTrue(c.AddRos2MessageLength(8) == ColorRGBA.Ros2FixedMessageLength + 8);
        }

        Assert.IsTrue(Point.Ros2FixedMessageLength == 3 * sizeof(double));
        {
            var c = new Point();
            Assert.IsTrue(c.AddRos2MessageLength(1) == Point.Ros2FixedMessageLength + 8);
            Assert.IsTrue(c.AddRos2MessageLength(2) == Point.Ros2FixedMessageLength + 8);
            Assert.IsTrue(c.AddRos2MessageLength(4) == Point.Ros2FixedMessageLength + 8);
            Assert.IsTrue(c.AddRos2MessageLength(8) == Point.Ros2FixedMessageLength + 8);
        }

        Assert.IsTrue(MeshMaterial.Ros2FixedMessageLength ==
                      sizeof(uint) + ColorRGBA.Ros2FixedMessageLength + sizeof(bool));

        var mms = new MeshMaterial[] { new(), new() };
        Assert.IsTrue(WriteBuffer2.AddLength(0, mms) == 4 + 2 * (MeshMaterial.Ros2FixedMessageLength + 3) - 3);
        Assert.IsTrue(WriteBuffer2.AddLength(1, mms) == 4 + 4 + 2 * (MeshMaterial.Ros2FixedMessageLength + 3) - 3);
    }

    /*
    [Test]
    public void TestMessageConsistencyN()
    {
        object k = new object();
        GCHandle handle = GCHandle.Alloc(k, GCHandleType.Pinned);
        Thread.Sleep(1000);
        handle.Free();
    }
    */
}