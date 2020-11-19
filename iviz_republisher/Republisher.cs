using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Republisher
{
    internal class Republisher : IAsyncDisposable
    {
        readonly string[] topicsAtoB;
        readonly string[] topicsBtoA;

        readonly RosClient clientA;
        readonly RosClient clientB;

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
                //"/PointCloudToROSProvider/point_cloud",
                "/ArVizToRViz/arviz",
                "/tf",
                //"/grasping/update",
                //"/grasping/update_full",
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
                return;
            }

            var topicsInA = await clientA.GetSystemPublishedTopicsAsync();
            var topicsInB = await clientB.GetSystemPublishedTopicsAsync();

            string[] topicTypesAtoB = topicsAtoB
                .Select(topic =>
                    topicsInA.FirstOrDefault(info => info.Topic == topic)?.Type ??
                    topicsInB.FirstOrDefault(info => info.Topic == topic)?.Type)
                .ToArray();

            string[] topicTypesBtoA = topicsBtoA
                .Select(topic =>
                    topicsInB.FirstOrDefault(info => info.Topic == topic)?.Type ??
                    topicsInA.FirstOrDefault(info => info.Topic == topic)?.Type)
                .ToArray();

            if (topicTypesAtoB.Any(type => type == null))
            {
                Console.WriteLine("EE At least one topic type in A->B is missing");
                return;
            }

            if (topicTypesBtoA.Any(type => type == null))
            {
                Console.WriteLine("EE At least one topic type in B->A is missing");
                return;
            }

            Type[] typesAtoB = topicTypesAtoB.Select(type => BuiltIns.TryGetTypeFromMessageName(type)).ToArray();
            Type[] typesBtoA = topicTypesBtoA.Select(type => BuiltIns.TryGetTypeFromMessageName(type)).ToArray();

            if (typesAtoB.Any(type => type == null))
            {
                Console.WriteLine("EE At least one C# type in A->B could not be resolved");
                return;
            }

            if (typesBtoA.Any(type => type == null))
            {
                Console.WriteLine("EE At least one C# type in B->A could not be resolved");
                return;
            }

            var tasksA = topicsAtoB.Zip(typesAtoB,
                (topic, type) => Republish(clientA, clientB, topic, type));

            var tasksB = topicsBtoA.Zip(typesBtoA,
                (topic, type) => Republish(clientB, clientA, topic, type));

            Task[] tasks = tasksA.Concat(tasksB).ToArray();
            await Task.WhenAll(tasks);
        }

        static async Task Republish(IRosClient sourceClient, IRosClient destClient, string topic, Type msgType)
        {
            await using IRosChannelReader reader = RosChannelReader.CreateInstance(msgType);
            await using IRosChannelWriter writer = RosChannelWriter.CreateInstance(msgType);

            try
            {
                Console.WriteLine($"** Starting reader for '{topic}'...");
                await reader.StartAsync(sourceClient, topic);
                Console.WriteLine($"** Starting writer for '{topic}'...");
                await writer.StartAsync(destClient, topic);
                Console.WriteLine($"** Transferring '{topic}'!");
                await writer.WriteAllAsync(reader.ReadAllAsync(CancellationToken.None));
                Console.WriteLine($"** Circuit for '{topic}' closed.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"EE Circuit for topic '{topic}' stopped!");
                Console.WriteLine(e);
            }
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