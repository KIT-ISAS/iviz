using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;
using Iviz.Roslib.XmlRpc;
using Iviz.RosMaster;
using Iviz.XmlRpc;
using NUnit.Framework;
using String = Iviz.Msgs.StdMsgs.String;

namespace Iviz.UtilsTests
{
    [Category("RosMaster")]
    public class RosMasterTests
    {
        static readonly Uri CallerUri = new Uri("http://localhost:7613");
        static readonly Uri MasterUri = new Uri("http://localhost:11312");
        const string CallerId = "/iviz_util_tests";
        RosMasterServer rosMasterServer;
        
        
        [SetUp]
        public void Setup()
        {
            rosMasterServer ??= new RosMasterServer(MasterUri, startInBackground: true);
        }
        
        
        [Test]
        public async Task TestXmlRpcGetUriAsync()
        {

            var args = new XmlRpcArg[] {CallerId};

            using var source = new CancellationTokenSource();
            source.CancelAfter(3000);

            var response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "getUri", args, source.Token);

            Assert.IsTrue(response.TryGetArray(out var responseArray));
            Assert.IsTrue(responseArray.Length == 3);
            Assert.IsTrue(responseArray[0].TryGetInteger(out int successCode) && successCode == 1);
            Assert.IsTrue(responseArray[1].TryGetString(out _));
            Assert.IsTrue(responseArray[2].TryGetString(out string retMasterUri) &&
                          Uri.TryCreate(retMasterUri, UriKind.Absolute, out _));
        }

        [Test]
        public async Task TestRosClientAdvertisementAsync()
        {
            const string topicName = "/my_topic";
            await using (var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri))
            {
                await client.AdvertiseAsync<String>(topicName);
                var systemState = await client.GetSystemStateAsync();
                Assert.True(systemState.Publishers.Any(tuple =>
                    tuple.Topic == topicName && tuple.Members.Contains(CallerId)));
            }

            var newSystemState = await new RosMasterClient(MasterUri, CallerId, CallerUri).GetSystemStateAsync();
            Assert.False(newSystemState.Publishers.Any(tuple =>
                tuple.Topic == topicName && tuple.Members.Contains(CallerId)));
        }

        [Test]
        public async Task TestChannelsAsync()
        {
            const string topicName = "/my_test_topic_xyz";
            await using var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri);
            await using var publisher = await RosChannelWriter.CreateAsync<String>(client, topicName);
            publisher.LatchingEnabled = true;

            var systemState = await client.GetSystemStateAsync();
            Assert.True(
                systemState.Publishers.Any(tuple => tuple.Topic == topicName && tuple.Members.Contains(CallerId)));

            const string msgText = "test message";
            publisher.Write(new String(msgText));

            await using var subscriber = await RosChannelReader.CreateAsync<String>(client, topicName);

            using var source = new CancellationTokenSource(5000);

            await foreach (var msg in subscriber.ReadAllAsync(source.Token))
            {
                if (msg.Data == msgText)
                {
                    break;
                }
            }
        }

        [Test]
        public async Task TestGenericChannelReaderAsync()
        {
            const string topicName = "/my_test_topic_xyz";
            await using var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri);
            await using var publisher = await RosChannelWriter.CreateAsync<String>(client, topicName);
            publisher.LatchingEnabled = true;

            var systemState = await client.GetSystemStateAsync();
            Assert.True(
                systemState.Publishers.Any(tuple => tuple.Topic == topicName && tuple.Members.Contains(CallerId)));

            const string msgText = "test message";
            publisher.Write(new String(msgText));

            // reader has no message type, it will use whatever the publisher sends during handshake
            await using var subscriber = await RosChannelReader.CreateAsync(client, topicName);

            using var source = new CancellationTokenSource(5000);

            await foreach (var msg in subscriber.ReadAllAsync(source.Token))
            {
                if (msg is String {Data: msgText})
                {
                    break;
                }
            }
        }

        [Test]
        public async Task TestChannelsWithEmptyMessageAsync()
        {
            const string topicName = "/my_test_topic_xyz_empty";
            await using var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri);
            await using var publisher = await RosChannelWriter.CreateAsync<Empty>(client, topicName);
            publisher.LatchingEnabled = true;

            var systemState = await client.GetSystemStateAsync();
            Assert.True(
                systemState.Publishers.Any(tuple => tuple.Topic == topicName && tuple.Members.Contains(CallerId)));

            publisher.Write(Empty.Singleton);

            await using var subscriber = await RosChannelReader.CreateAsync<Empty>(client, topicName);

            using var source = new CancellationTokenSource(5000);

            await subscriber.ReadAsync(source.Token);
        }
        
        [Test]
        public async Task TestRosClientParametersAsync()
        {
            await using var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri);

            const string inValueStr = "test";
            Assert.True(await client.Parameters.SetParameterAsync("/iviz_utils_tests/a", inValueStr));
            (bool success, XmlRpcValue value) = await client.Parameters.GetParameterAsync("/iviz_utils_tests/a");
            Assert.True(success && value.TryGetString(out string valueStr) && valueStr == inValueStr);
            Assert.True(await client.Parameters.DeleteParameterAsync("/iviz_utils_tests/a"));

            const int inValueInt = 1;
            Assert.True(await client.Parameters.SetParameterAsync("/iviz_utils_tests/a", inValueInt));
            (success, value) = await client.Parameters.GetParameterAsync("/iviz_utils_tests/a");
            Assert.True(success && value.TryGetInteger(out int valueInt) && valueInt == inValueInt);
            Assert.True(await client.Parameters.DeleteParameterAsync("/iviz_utils_tests/a"));

            string[] inValueArray = {"a", "b"};
            Assert.True(await client.Parameters.SetParameterAsync("/iviz_utils_tests/a", inValueArray));
            (success, value) = await client.Parameters.GetParameterAsync("/iviz_utils_tests/a");
            Assert.True(success && value.TryGetArray(out var valueArray)
                                && valueArray.Length == inValueArray.Length
                                && valueArray[0].TryGetString(out string valueStr0) && valueStr0 == inValueArray[0]
                                && valueArray[1].TryGetString(out string valueStr1) && valueStr1 == inValueArray[1]);


            string[] inEmptyValueArray = Array.Empty<string>();
            Assert.True(await client.Parameters.SetParameterAsync("/iviz_utils_tests/a", inEmptyValueArray));
            (success, value) = await client.Parameters.GetParameterAsync("/iviz_utils_tests/a");
            Assert.True(success && value.TryGetArray(out var emptyValueArray)
                                && emptyValueArray.Length == 0);
            Assert.True(await client.Parameters.DeleteParameterAsync("/iviz_utils_tests/a"));
        }
        
    }
}