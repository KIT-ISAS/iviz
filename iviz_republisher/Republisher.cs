using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib;
using Nito.AsyncEx;

namespace Iviz.Republisher
{
    class Republisher : IAsyncDisposable
    {
        readonly Uri masterUriA = new Uri("http://141.3.88.138:11311");
        readonly Uri masterUriB = new Uri("http://141.3.59.5:11311");

        readonly Uri callerUriA = new Uri("http://141.3.88.89:7643");
        readonly Uri callerUriB = new Uri("http://141.3.59.11:7644");
        
        readonly string[] topicsAtoB= {
            "/PointCloudToROSProvider/point_cloud",
            "/ArVizToRViz/arviz",
            "/tf",
            "/grasping/update",
            "/grasping/update_full",
            //"/exploration/update",
            //"/exploration/update_full",
        };
        
        readonly string[] topicsBtoA = {
            "/grasping/feedback",
            //"/exploration/feedback",
        };

        readonly RosClient clientA;
        readonly RosClient clientB;

        readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken Token => tokenSource.Token;

        Republisher()
        {
            /*
            Logger.Log = Console.WriteLine;
            Logger.LogDebug = Console.WriteLine;
            Logger.LogError= Console.WriteLine;
            */

            try
            {
                clientA = new RosClient(masterUriA, callerUri: callerUriA);
            }
            catch (RoslibException e)
            {
                Console.WriteLine($"EE Failed to connect to master A with uri '{masterUriA}': {e.Message}");
            }

            try
            {
                clientB = new RosClient(masterUriB, callerUri: callerUriB);
            }
            catch (RoslibException e)
            {
                Console.WriteLine($"EE Failed to connect to master B with uri '{masterUriB}': {e.Message}");
            }
        }

        async Task StartAsync()
        {
            if (clientA == null || clientB == null)
            {
                Console.WriteLine("EE Init failed. Quitting.");
                return;
            }

            try
            {
                await CreateModelServiceBridgeAsync(clientA, clientB);
            }
            catch (RoslibException e)
            {
                Console.WriteLine($"EE Failed to create model service bridge: {e.Message}");
            }

            Task[] publishersAtoB =
                topicsAtoB.Select(topic => Republish(clientA, clientB, topic)).ToArray();
            Task[] publishersBtoA =
                topicsBtoA.Select(topic => Republish(clientB, clientA, topic)).ToArray();

            List<Task> tasks = new List<Task>();
            tasks.AddRange(publishersAtoB);
            tasks.AddRange(publishersBtoA);

            await tasks.WhenAll();
        }

        async Task Republish(IRosClient sourceClient, IRosClient destClient, string topic)
        {
            Console.WriteLine($"** Starting task for '{topic}'...");

            IRosChannelWriter writer = null;
            
            try
            {
                await using var reader = await RosChannelReader.CreateAsync(sourceClient, topic, Token);

                Console.WriteLine($"** Transferring '{topic}'!");
                
                await foreach (var msg in reader.ReadAllAsync(Token))
                {
                    writer ??= await RosChannelWriter.CreateForAsync(msg, destClient, topic, Token);
                    writer.Write(msg);
                }

                Console.WriteLine($"** Circuit for '{topic}' closed.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"EE Circuit for topic '{topic}' stopped!");
                Console.WriteLine(e);
            }
            finally
            {
                if (writer != null)
                {
                    await writer.DisposeAsync();
                }
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
                    await clientWithModels.CallServiceAsync(modelServiceName, srv);
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
            await using var republisher = new Republisher();
            await republisher.StartAsync();
        }
    }
}