using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Rosbag;
using Iviz.Rosbag.Reader;
using NUnit.Framework;

namespace Iviz.UtilsTests;

public class BagTests
{
    [Test]
    public void TestTopics()
    {
        const string path = "../../../bags/iviz-test.bag";

        var bag = new RosbagFileReader(path);
        var connections = (List<Connection>)bag.GetAllConnections();

        Assert.AreEqual(connections.Count, 5);
        foreach (var connection in connections)
        {
            Assert.NotNull(connection.Topic);
            Assert.NotNull(connection.CallerId);
            Assert.NotNull(connection.Md5Sum);
            Assert.NotNull(connection.MessageDefinition);
            Assert.NotNull(connection.MessageType);
        }

        Assert.True(connections
            .Where(connection => connection.Topic == "/tf")
            .All(connection => connection.MessageType == TFMessage.MessageType));
    }

    [Test]
    public void TestMessages()
    {
        string path = "../../../bags/iviz-test.bag";

        var bag = new RosbagFileReader(path);

        // select the topic, then cast the message
        var anyTf2 = bag.ReadAllMessages().Where(data => data.Topic == "/tf").SelectMessage<TFMessage>();
        Assert.DoesNotThrow(() =>
        {
            foreach (var tf in anyTf2)
            {
            }
        });

        // do not select topic, cast all messages
        var anyImage = bag.ReadAllMessages().SelectMessage<Image>();
        Assert.Throws<RosBufferException>(() =>
        {
            foreach (var image in anyImage)
            {
                // this fails, assumes that all messages are Image
            }
        });
    }
}