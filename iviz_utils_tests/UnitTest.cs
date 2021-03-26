using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsGen;
using Iviz.MsgsGen.Dynamic;
using Iviz.Roslib;
using Iviz.Roslib.XmlRpc;
using NUnit.Framework;
using Iviz.XmlRpc;
using String = Iviz.Msgs.StdMsgs.String;

namespace iviz_utils_tests
{
    public class Tests
    {
        static readonly Uri CallerUri = new Uri("http://localhost:7613");
        static readonly Uri OtherCallerUri = new Uri("http://localhost:7614");
        static readonly Uri MasterUri = new Uri("http://141.3.59.5:11311");
        const string CallerId = "/iviz_util_tests";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestHttpRequest()
        {
            using var source = new CancellationTokenSource();
            source.CancelAfter(3000);

            HttpRequest request;
            using (request = new HttpRequest(CallerUri, MasterUri))
            {
                await request.StartAsync(source.Token);
            }

            Assert.IsFalse(request.IsAlive);
        }

        [Test]
        public void TestXmlRpcGetUri()
        {
            var args = new XmlRpcArg[] {CallerId};

            using var source = new CancellationTokenSource();
            source.CancelAfter(3000);

            var response = XmlRpcService.MethodCall(MasterUri, CallerUri, "getUri", args, source.Token);

            Assert.IsTrue(response.TryGetArray(out var responseArray));
            Assert.IsTrue(responseArray.Length == 3);
            Assert.IsTrue(responseArray[0].TryGetInteger(out int successCode) && successCode == 1);
            Assert.IsTrue(responseArray[1].TryGetString(out _));
            Assert.IsTrue(responseArray[2].TryGetString(out string retMasterUri) &&
                          Uri.TryCreate(retMasterUri, UriKind.Absolute, out _));
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
        public void TestXmlRpcGetUriTimeout()
        {
            var args = new XmlRpcArg[] {CallerId};

            using var source = new CancellationTokenSource();
            source.Cancel();

            Assert.Catch<OperationCanceledException>(() =>
                XmlRpcService.MethodCall(MasterUri, CallerUri, "getUri", args, source.Token));

            Assert.CatchAsync<OperationCanceledException>(async () =>
                await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "getUri", args, source.Token));
        }

        [Test]
        public void TestRosClientConnection()
        {
            using var _ = new RosClient(MasterUri, CallerId, CallerUri);
            Uri testMasterUri = new NodeClient(CallerId, OtherCallerUri, CallerUri).GetMasterUri().Uri;
            Assert.True(testMasterUri == MasterUri);
            Assert.Catch<RosUriBindingException>(() => new RosClient(MasterUri, CallerId, CallerUri));
        }

        [Test]
        public void TestRosClientAdvertisement()
        {
            const string topicName = "/my_topic";
            using (var client = new RosClient(MasterUri, CallerId, CallerUri))
            {
                client.Advertise(topicName, out RosPublisher<String> _);
                var systemState = client.GetSystemState();
                Assert.True(systemState.Publishers.Any(tuple =>
                    tuple.Topic == topicName && tuple.Members.Contains(CallerId)));
            }

            var newSystemState = new RosMasterApi(MasterUri, CallerId, CallerUri).GetSystemState();
            Assert.False(newSystemState.Publishers.Any(tuple =>
                tuple.Topic == topicName && tuple.Members.Contains(CallerId)));
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

            var newSystemState = await new RosMasterApi(MasterUri, CallerId, CallerUri).GetSystemStateAsync();
            Assert.False(newSystemState.Publishers.Any(tuple =>
                tuple.Topic == topicName && tuple.Members.Contains(CallerId)));
        }

        [Test]
        public void TestChannels()
        {
            const string topicName = "/my_test_topic_xyz";
            using var client = new RosClient(MasterUri, CallerId, CallerUri);
            using var publisher = new RosChannelWriter<String>(client, topicName);
            publisher.LatchingEnabled = true;

            var systemState = client.GetSystemState();
            Assert.True(
                systemState.Publishers.Any(tuple => tuple.Topic == topicName && tuple.Members.Contains(CallerId)));

            const string msgText = "test message";
            publisher.Write(new String(msgText));

            using var subscriber = new RosChannelReader<String>(client, topicName);

            using var source = new CancellationTokenSource(5000);

            foreach (var msg in subscriber.ReadAll(source.Token))
            {
                if (msg.Data == msgText)
                {
                    break;
                }
            }
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

            await foreach (var msg in subscriber.ReadAllAsync(source.Token))
            {
                break;
            }
        }

        [Test]
        public void TestDynamicMessage()
        {
            const string fakeHeaderDefinition = "uint32 seq\ntime stamp\nstring frame_id";

            ClassInfo clazz = new ClassInfo(null, "iviz_test/Header", fakeHeaderDefinition);
            DynamicMessage message = new DynamicMessage(clazz);

            Assert.True(message.Fields.Count == 3);

            Assert.True(message.Fields[0].TrySet((uint) 0));
            Assert.False(message.Fields[0].TrySet(0)); // int, not uint
            Assert.False(message.Fields[0].TrySet("a"));

            Assert.True(message.Fields[1].TrySet(new time(DateTime.Now)));
            Assert.False(message.Fields[1].TrySet("a"));

            Assert.True(message.Fields[2].TrySet("map"));
            Assert.False(message.Fields[2].TrySet(0));

            Assert.True(message.RosInstanceMd5Sum == Iviz.Msgs.StdMsgs.Header.RosMd5Sum);
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
}