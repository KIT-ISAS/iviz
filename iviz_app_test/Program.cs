using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Rosbag;
using Iviz.Roslib;

namespace iviz_app_test
{
    class Program
    {
        static async Task Main()
        {
            string path = "/Users/akzeac/Shared/ISAS_IPR.bag";
            using var file = new RosbagFile(path);

            var topics = new HashSet<string>
            {
                "/tf",
                "/tf_static",
                "/map",
                "/sick_visionary_t_driver/points",
            };

            var client = await RosClient.CreateAsync();

            var msg = file.GetAllMessages().First();
            time referenceTime = msg.Time;

            var messages = file.GetAllMessagesWhere(connection => topics.Contains(connection.Topic));

            var publishers = new Dictionary<string, IRosChannelWriter>();

            foreach (var message in messages)
            {
                var innerMessage = message.Message;
                if (message.Topic == null)
                {
                    continue;
                }

                if (!publishers.TryGetValue(message.Topic, out var publisher))
                {
                    publisher = await client.CreateWriterForMessageAsync(innerMessage, message.Topic);
                    await (innerMessage is DynamicMessage dynamicMessage
                        ? publisher.StartAsync(client, message.Topic, dynamicMessage)
                        : publisher.StartAsync(client, message.Topic));
                    publishers[message.Topic] = publisher;
                }

                publisher.Write(innerMessage);
            }
        }
    }
}