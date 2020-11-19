using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Republisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Uri masterUriA = new Uri("http://192.168.0.1:11311");
            Uri masterUriB = new Uri("http://192.168.0.2:11311");

            Uri callerUriA = new Uri("http://192.168.0.11:7623");
            Uri callerUriB = new Uri("http://192.168.0.12:7623");

            string[] topicsAtoB =
            {
                "/ros_out"
            };

            string[] topicsBtoA =
            {
                "/abcd"
            };

            await using RosClient clientA = new RosClient(masterUriA, callerUri: callerUriA);
            await using RosClient clientB = new RosClient(masterUriB, callerUri: callerUriB);

            var topicsInA = await clientA.GetSystemPublishedTopicsAsync();
            var topicsInB = await clientB.GetSystemPublishedTopicsAsync();

            string[] topicTypesAtoB = topicsAtoB
                .Select(topic =>
                    topicsInA.FirstOrDefault(info => info.Topic == topic)?.Topic ??
                    topicsInB.FirstOrDefault(info => info.Topic == topic)?.Topic)
                .ToArray();

            string[] topicTypesBtoA = topicsBtoA
                .Select(topic =>
                    topicsInB.FirstOrDefault(info => info.Topic == topic)?.Topic ??
                    topicsInA.FirstOrDefault(info => info.Topic == topic)?.Topic)
                .ToArray();

            if (topicTypesAtoB.Any(type => type == null))
            {
                Console.WriteLine("EE At least one topic type in A->B is missing");
            }

            if (topicTypesBtoA.Any(type => type == null))
            {
                Console.WriteLine("EE At least one topic type in B->A is missing");
            }

            Type[] typesAtoB = topicTypesAtoB.Select(type => BuiltIns.TryGetTypeFromMessageName(type)).ToArray();
            Type[] typesBtoA = topicTypesBtoA.Select(type => BuiltIns.TryGetTypeFromMessageName(type)).ToArray();
            
            
        }

        async Task Republish(IRosClient sourceClient, IRosClient destClient, string topic, Type msgType)
        {
            IRosChannelReader reader = RosChannelReader.CreateInstance(msgType);
            await reader.StartAsync(sourceClient, topic);

            (string id, IRosPublisher publisher) = await destClient.AdvertiseAsync(topic, msgType);

            await foreach (var message in reader.ReadAllAsync(CancellationToken.None))
            {
                publisher.Publish(message);
            }

            await publisher.UnadvertiseAsync(id);
        }
    }
}