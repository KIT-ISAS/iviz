using System;
using System.IO;
using System.Text;
using System.Threading;
using Iviz.Msgs.geometry_msgs;
using Iviz.Msgs.rosapi;
using Iviz.Msgs.rosbridge_library;
using Iviz.Msgs.tf2_msgs;
using Iviz.RoslibSharp;

namespace iviz_test
{
    class Program
    {
        static void Main(string[] args)
        {
            RosClient client = new RosClient(
                "http://192.168.0.73:11311",
                null,
                "http://192.168.0.157:7614");

            /*
            AddTwoInts service = new AddTwoInts();
            Console.WriteLine(service.ToJsonString());
            */

            /*
            Topics topics = new Topics();
            client.CallService("/rosapi/topics", topics);
            Console.WriteLine(topics.ToJsonString());

            client.Close();
            */
            /*
            client.AdvertiseService<AddTwoInts>("/add", x =>
            {
                x.response = new AddTwoInts.Response()
                {
                    sum = x.request.a + x.request.b
                };
                throw new ArgumentException();
            });

            while (true)
            {
                Thread.Sleep(1000);
            }
            */

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            Point point = new Point();

            TransformStamped[] tfs = new TransformStamped[1];
            tfs[0] = new TransformStamped
            {
                transform = new Transform
                {
                    translation = new Vector3
                    {
                        x = 0,
                        y = 0,
                        z = 1
                    },
                    rotation = new Quaternion
                    {
                        x = 0,
                        y = 0,
                        z = 0,
                        w = 1
                    }
                }
            };
            TFMessage tf = new TFMessage
            {
                transforms = tfs
            };


            Console.WriteLine(tf.ToJsonString());

            /*
            string json = sb.ToString();

            JsonTextReader reader = new JsonTextReader(new StringReader(json));

            TFMessage tf2 = new TFMessage();

            Console.WriteLine(tf2.ToJsonString());
            */

            /*
            RosClient client = new RosClient("http://192.168.0.73:11311", null, "http://192.168.0.157:7615");
            //client.Subscribe<Iviz.Msgs.std_msgs.Int32>("/client_count", Callback);
            //Console.WriteLine(client.GetSystemState());


            client.Advertise<TFMessage>("/tf", out RosPublisher publisher);


            TransformStamped[] tfs = new TransformStamped[1];
            tfs[0] = new TransformStamped
            {
                transform = new Transform
                {
                    translation = new Vector3{
                        x = 0,
                        y = 0,
                        z = 1
                    },
                    rotation = new Quaternion
                    {
                        x = 0,
                        y = 0,
                        z = 0,
                        w = 1
                    }
                }
            };
            TFMessage tf = new TFMessage
            {
                transforms = tfs
            };

            client.Subscribe<TFMessage>("/tf", Callback);

            while (true)
            {
                publisher.Publish(tf);
                //Console.WriteLine(">> " + tf.ToJsonString());
                Thread.Sleep(1000);
            }

            Console.Read();
            client.Close();
            */

        }

        static void Callback(Iviz.Msgs.std_msgs.Int32 value)
        {
            Console.WriteLine("<< " + value.ToJsonString());
        }

        static void Callback(TFMessage value)
        {
            Console.WriteLine("<< " + value.ToJsonString());
        }

    }
}
