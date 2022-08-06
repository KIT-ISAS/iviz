using System;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.MeshMsgs;
using Iviz.Msgs.StdMsgs;
using NUnit.Framework;
using UInt16 = Iviz.Msgs.StdMsgs.UInt16;

namespace Iviz.UtilsTests;

[Category("Message2")]
public class Message2Tests
{
    [Test]
    public void TestMessageConsistency()
    {
        WriteBuffer2.AddLength(0, "a");
        Assert.IsTrue(WriteBuffer2.AddLength(0, "") == sizeof(int) + 1);
        Assert.IsTrue(WriteBuffer2.AddLength(0, "abcd") == sizeof(int) + "abcd".Length + 1);
        Assert.IsTrue(WriteBuffer2.AddLength(1, "abcd") == 4 + sizeof(int) + "abcd".Length + 1);
        
        
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
}