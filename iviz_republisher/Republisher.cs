using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib;

namespace Iviz.Republisher
{
    internal class Republisher : IAsyncDisposable
    {
        readonly string[] topicsAtoB;
        readonly string[] topicsBtoA;

        readonly RosClient clientA;
        readonly RosClient clientB;

        ReadOnlyCollection<BriefTopicInfo> topicsInA;
        ReadOnlyCollection<BriefTopicInfo> topicsInB;

        readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken Token => tokenSource.Token;

        Republisher(string[] args)
        {
            /*
            Logger.Log = Console.WriteLine;
            Logger.LogDebug = Console.WriteLine;
            Logger.LogError= Console.WriteLine;
            */


            var masterUriA = new Uri("http://141.3.88.138:11311");
            var masterUriB = new Uri("http://141.3.59.5:11311");

            var callerUriA = new Uri("http://141.3.88.89:7643");
            var callerUriB = new Uri("http://141.3.59.11:7644");

            topicsAtoB = new[]
            {
                "/PointCloudToROSProvider/point_cloud",
                "/ArVizToRViz/arviz",
                "/tf",
                "/grasping/update",
                "/grasping/update_full",
                //"/exploration/update",
                //"/exploration/update_full",
            };

            topicsBtoA = new string[]
            {
                "/grasping/feedback",
                //"/exploration/feedback",
            };

            try
            {
                clientA = new RosClient(masterUriA, callerUri: callerUriA);
            }
            catch (RoslibException e)
            {
                Console.WriteLine($"EE: Failed to connect to master A with uri '{masterUriA}': {e.Message}");
            }

            try
            {
                clientB = new RosClient(masterUriB, callerUri: callerUriB);
            }
            catch (RoslibException e)
            {
                Console.WriteLine($"EE: Failed to connect to master B with uri '{masterUriB}': {e.Message}");
            }
        }

        async Task StartAsync()
        {
            if (clientA == null || clientB == null)
            {
                Console.WriteLine("EE: Init failed. Quitting.");
                return;
            }

            try
            {
                await CreateModelServiceBridgeAsync(clientA, clientB);
            }
            catch (RoslibException e)
            {
                Console.WriteLine($"EE: Failed to create model service bridge: {e.Message}");
            }

            topicsInA = await clientA.GetSystemPublishedTopicsAsync(Token);
            topicsInB = await clientB.GetSystemPublishedTopicsAsync(Token);

            Task probeTask = ProbeTopicTypes();

            Task[] publishersAtoB =
                topicsAtoB.Select(async topic => await Republish(clientA, clientB, topic)).ToArray();
            Task[] publishersBtoA =
                topicsBtoA.Select(async topic => await Republish(clientB, clientA, topic)).ToArray();

            List<Task> tasks = new List<Task> {probeTask};
            tasks.AddRange(publishersAtoB);
            tasks.AddRange(publishersBtoA);

            await Task.WhenAll(tasks);
        }

        async Task ProbeTopicTypes()
        {
            while (true)
            {
                await Task.Delay(1000, Token);

                try
                {
                    topicsInA = await clientA.GetSystemPublishedTopicsAsync(Token);
                }
                catch (TaskCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                try
                {
                    topicsInB = await clientB.GetSystemPublishedTopicsAsync(Token);
                }
                catch (TaskCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        async Task Republish(IRosClient sourceClient, IRosClient destClient, string topic)
        {
            Console.WriteLine($"** Starting task for '{topic}'...");

            string topicType;
            while (true)
            {
                topicType = topicsInA.FirstOrDefault(info => info.Topic == topic)?.Type ??
                            topicsInB.FirstOrDefault(info => info.Topic == topic)?.Type;
                if (topicType != null)
                {
                    break;
                }

                await Task.Delay(1000, Token);
            }

            Type msgType = BuiltIns.TryGetTypeFromMessageName(topicType);
            if (msgType == null)
            {
                Console.WriteLine($"EE C# message type '{topicType}' for topic {topic} could not be resolved!");
                return;
            }

            await using IRosChannelReader reader = RosChannelReader.CreateInstance(msgType);
            await using IRosChannelWriter writer = RosChannelWriter.CreateInstance(msgType);

            try
            {
                Console.WriteLine($"** Transferring '{topic}'!");
                await reader.StartAsync(sourceClient, topic);
                //Console.WriteLine($"** Starting writer for '{topic}'...");
                await writer.StartAsync(destClient, topic);
                //Console.WriteLine($"** Transferring '{topic}'!");
                await writer.WriteAllAsync(reader.ReadAllAsync(Token));
                Console.WriteLine($"** Circuit for '{topic}' closed.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"EE Circuit for topic '{topic}' stopped!");
                Console.WriteLine(e);
            }
        }

        static async Task CreateModelServiceBridgeAsync(IRosClient clientWithModels, IRosClient otherClient)
        {
            const string modelServiceName = "/iviz/get_model_resource";
            const string textureServiceName = "/iviz/get_model_texture";

            async Task CreateModelCallback(GetModelResource srv)
            {
                try
                {
                    //await clientWithModels.CallServiceAsync(modelServiceName, srv);
                    srv.Response.Success = false;
                    srv.Response.Message = "";
                }
                catch (Exception e)
                {
                    srv.Response.Success = false;
                    srv.Response.Message = $"Republisher error: {e.Message}";
                }
            }

            async Task CreateTextureCallback(GetModelTexture srv)
            {
                try
                {
                    await clientWithModels.CallServiceAsync(textureServiceName, srv);
                }
                catch (Exception e)
                {
                    srv.Response.Success = false;
                    srv.Response.Message = $"Republisher error: {e.Message}";
                }
            }

            await otherClient.AdvertiseServiceAsync<GetModelResource>(modelServiceName, CreateModelCallback);
            await otherClient.AdvertiseServiceAsync<GetModelTexture>(textureServiceName, CreateTextureCallback);
        }

        public async ValueTask DisposeAsync()
        {
            if (clientA != null)
            {
                await clientA.DisposeAsync();
            }

            if (clientB != null)
            {
                await clientB.DisposeAsync();
            }
        }

        static async Task Main(string[] args)
        {
            await using Republisher republisher = new Republisher(args);
            await republisher.StartAsync();
        }
    }
}