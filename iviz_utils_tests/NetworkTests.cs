using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;
using NUnit.Framework;
using Iviz.XmlRpc;
using String = Iviz.Msgs.StdMsgs.String;

namespace Iviz.UtilsTests;

[Category("Network")]
public class NetworkTests
{
    static readonly Uri CallerUri = new Uri("http://localhost:7633");

    static readonly Uri OtherCallerUri = new Uri("http://localhost:7614");

    //static readonly Uri MasterUri = new Uri("http://141.3.59.5:11311");
    static readonly Uri MasterUri = new Uri("http://192.168.0.220:11311");
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
    public async Task TestXmlRpcGetUriAsync()
    {
        var args = new XmlRpcArg[] { CallerId };

        using var source = new CancellationTokenSource();
        source.CancelAfter(3000);

        var response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "getUri", args, source.Token);

        Assert.IsTrue(response.TryGetArray(out var responseArray));
        Assert.IsTrue(responseArray.Length == 3);
        Assert.IsTrue(responseArray[0].TryGet(out int successCode) && successCode == 1);
        Assert.IsTrue(responseArray[1].TryGet(out string _));
        Assert.IsTrue(responseArray[2].TryGet(out string retMasterUri) &&
                      Uri.TryCreate(retMasterUri, UriKind.Absolute, out _));
    }

    [Test]
    public void TestXmlRpcGetUriTimeout()
    {
        var args = new XmlRpcArg[] { CallerId };

        using var source = new CancellationTokenSource();
        source.Cancel();

        Assert.CatchAsync<OperationCanceledException>(async () =>
            await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "getUri", args, source.Token));
    }

    [Test]
    public void TestRosClientConnection()
    {
        using var client = new RosClient(MasterUri, CallerId, CallerUri);
        var rosNodeClient = new RosNodeClient(CallerId, OtherCallerUri, CallerUri);
        Uri testMasterUri = TaskUtils.RunSync(rosNodeClient.GetMasterUriAsync).Uri;
        Assert.True(testMasterUri == MasterUri);
        Assert.Catch<RosUriBindingException>(() => _ = new RosClient(MasterUri, CallerId, CallerUri));
    }
    
    
    [Test]
    public async Task TestRosClientGetTopicsAsync()
    {
        await using var client = new RosClient(MasterUri, CallerId, CallerUri);
        var topics = await client.GetSystemTopicsAsync();
        Assert.IsNotEmpty(topics);
    }
    

    /*
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

        var rosMasterClient = new RosMasterClient(MasterUri, CallerId, CallerUri);
        var newSystemState = TaskUtils.RunSync(rosMasterClient.GetSystemStateAsync);
        Assert.False(newSystemState.Publishers.Any(tuple =>
            tuple.Topic == topicName && tuple.Members.Contains(CallerId)));
    }
    */

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

    /*
    [Test]
    public void TestChannels()
    {
        const string topicName = "/my_test_topic_xyz";
        using IRosClient client = new RosClient(MasterUri, CallerId, CallerUri);
        using var publisher = new RosChannelWriter<String>(client, topicName, true);

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
    */

    [Test]
    public async Task TestChannelsAsync()
    {
        const string topicName = "/my_test_topic_xyz";
        await using var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri);
        await using var publisher = await client.CreateWriterAsync<String>(topicName, true);

        var systemState = await client.GetSystemStateAsync();
        Assert.True(
            systemState.Publishers.Any(tuple => tuple.Topic == topicName && tuple.Members.Contains(CallerId)));

        const string msgText = "test message";
        publisher.Write(new String(msgText));

        await using var subscriber = await client.CreateReaderAsync<String>(topicName);

        using var source = new CancellationTokenSource(5000);

        await foreach (var msg in subscriber.ReadAllAsync(source.Token))
        {
            if (msg.Data == msgText)
            {
                break;
            }
        }
    }

    /*
    [Test]
    public async Task TestGenericChannelReaderAsync()
    {
        const string topicName = "/my_test_topic_xyz";
        await using var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri);
        await using var publisher = await client.CreateWriterAsync<String>(topicName);
        publisher.LatchingEnabled = true;

        var systemState = await client.GetSystemStateAsync();
        Assert.True(
            systemState.Publishers.Any(tuple => tuple.Topic == topicName && tuple.Members.Contains(CallerId)));

        const string msgText = "test message";
        publisher.Write(new String(msgText));

        // reader has no message type, it will use whatever the publisher sends during handshake
        await using var subscriber = await client.CreateReaderAsync(topicName);

        using var source = new CancellationTokenSource(5000);

        await foreach (var msg in subscriber.ReadAllAsync(source.Token))
        {
            if (msg is not DynamicMessage dynamic)
            {
                throw new Exception("Message is not dynamic");
            }

            if (dynamic.Fields.FirstOrDefault(property => property is ("data", _)) is not (_, IField<string> field))
            {
                throw new Exception("Data property is missing");
            }

            if (field.Value != msgText)
            {
                throw new Exception("Data property is incorrect");
            }

            break;
        }
    }
    */

    [Test]
    public async Task TestChannelsWithEmptyMessageAsync()
    {
        const string topicName = "/my_test_topic_xyz_empty";
        await using var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri);
        await using var publisher = await client.CreateWriterAsync<Empty>(topicName, true);

        var systemState = await client.GetSystemStateAsync();
        Assert.True(
            systemState.Publishers.Any(tuple => tuple.Topic == topicName && tuple.Members.Contains(CallerId)));

        publisher.Write(Empty.Singleton);

        await using var subscriber = await client.CreateReaderAsync<Empty>(topicName);

        using var source = new CancellationTokenSource(5000);

        await foreach (var _ in subscriber.ReadAllAsync(source.Token))
        {
            break;
        }
    }

    [Test]
    public async Task TestRosClientParametersAsync()
    {
        await using var client = await RosClient.CreateAsync(MasterUri, CallerId, CallerUri);

        const string inValueStr = "test";
        Assert.True(await client.Parameters.SetParameterAsync("/iviz_utils_tests/a", inValueStr));
        var value = await client.Parameters.GetParameterAsync("/iviz_utils_tests/a");
        Assert.True(value.TryGet(out string valueStr) && valueStr == inValueStr);
        Assert.True(await client.Parameters.DeleteParameterAsync("/iviz_utils_tests/a"));

        const int inValueInt = 1;
        Assert.True(await client.Parameters.SetParameterAsync("/iviz_utils_tests/a", inValueInt));
        value = await client.Parameters.GetParameterAsync("/iviz_utils_tests/a");
        Assert.True(value.TryGet(out int valueInt) && valueInt == inValueInt);
        Assert.True(await client.Parameters.DeleteParameterAsync("/iviz_utils_tests/a"));

        string[] inValueArray = { "a", "b" };
        Assert.True(await client.Parameters.SetParameterAsync("/iviz_utils_tests/a", inValueArray));
        value = await client.Parameters.GetParameterAsync("/iviz_utils_tests/a");
        Assert.True(value.TryGetArray(out var valueArray)
                    && valueArray.Length == inValueArray.Length
                    && valueArray[0].TryGet(out string valueStr0) && valueStr0 == inValueArray[0]
                    && valueArray[1].TryGet(out string valueStr1) && valueStr1 == inValueArray[1]);


        string[] inEmptyValueArray = Array.Empty<string>();
        Assert.True(await client.Parameters.SetParameterAsync("/iviz_utils_tests/a", inEmptyValueArray));
        value = await client.Parameters.GetParameterAsync("/iviz_utils_tests/a");
        Assert.True(value.TryGetArray(out var emptyValueArray)
                    && emptyValueArray.Length == 0);
        Assert.True(await client.Parameters.DeleteParameterAsync("/iviz_utils_tests/a"));
    }
}