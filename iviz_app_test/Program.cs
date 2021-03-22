using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iviz.Msgs.Tf2Msgs;
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

            /*
            var connections = file.GetAllRecords().Where(record => record.OpCode == OpCode.Connection);
            foreach (var record in connections)
            {
                var connection = record.Connection;
                Console.WriteLine(connection.Topic + " -> " + connection.MessageType);
            }
            */

            var topics = new HashSet<string>
            {
                "/tf",
                "/tf_static",
                "/map",
                "/sick_visionary_t_driver/points",
            };

            var messages = file.GetAllMessagesWhere(connection => topics.Contains(connection.Topic));

            var publishers = new Dictionary<string, IRosChannelWriter>
            {
                ["/tf"] = await RosChannelWriter.CreateAsync<TFMessage>(null, "/tf"),
            };
            
            foreach (var message in messages)
            {
                if (publishers.TryGetValue(message.Topic!, out var publisher))
                {
                    publisher.Write(message.Message);
                }
            }
        }
    }
}