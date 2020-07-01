using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitMiracle.LibJpeg;
using Iviz.Bridge.Client;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.RoslibSharp;
using Newtonsoft.Json.Serialization;

namespace iviz_test
{
    class Program
    {
        static void Main()
        {
            RosClient client = new RosClient(
                //"http://192.168.0.73:11311",
                "http://141.3.59.5:11311",
                null,
                //"http://192.168.0.157:7613"
                "http://141.3.59.19:7621"
            );
            Iviz.Msgs.NavMsgs.Path path = new Iviz.Msgs.NavMsgs.Path();
            path.Header = new Header(0, new time(DateTime.Now), "map");
            path.Poses = new[]
            {
                new PoseStamped(path.Header, new Pose(new Point(0, 0, 0), new Quaternion(0, 0, 0, 1) )), 
                new PoseStamped(path.Header, new Pose(new Point(1, 0, 0), new Quaternion(0, 0, 0, 1) )), 
                new PoseStamped(path.Header, new Pose(new Point(2, 0, 0), new Quaternion(0, 0, 0, 1) )), 
            };

            client.Advertise<Iviz.Msgs.NavMsgs.Path>("/my_path", out RosPublisher publisher);
            while (true)
            {
                Console.WriteLine("publishing...");
                publisher.Publish(path);
                Thread.Sleep(1000);
            }
        }
        static void Main_N()
        { 
            RosClient client = new RosClient(
                //"http://192.168.0.73:11311",
                "http://141.3.59.5:11311",
                null,
                //"http://192.168.0.157:7613"
                "http://141.3.59.19:7621"
                );
            
            /*
            Console.WriteLine(client.GetSystemState());
            client.Subscribe<TFMessage>("/tf", Callback);
            */


            client.Advertise<TFMessage>("/tf", out RosPublisher publisherTf);
            client.Advertise<MarkerArray>("/test_markers", out RosPublisher publisherMarkers);

            TransformStamped[] tfs = new TransformStamped[1];
            tfs[0] = new TransformStamped
            (
                Header: new Header(0, new time(DateTime.Now), "map"),
                ChildFrameId: "ar_marker",
                Transform: new Transform
                (
                    Translation: new Vector3
                    (
                        X: 0,
                        Y: 0,
                        Z: 0.725f
                    ),
                    Rotation: new Quaternion
                    (
                        X: 0,
                        Y: 0,
                        Z: 0,
                        W: 1
                    )
                )
            );
            TFMessage tf = new TFMessage
            {
                Transforms = tfs
            };

            Marker marker = new Marker()
            {
                Header = new Header(0, new time(DateTime.Now), "ar_marker"),
                Ns = "iviz",
                Id = 0,
                Type = Marker.CYLINDER,
                Action = Marker.ADD,
                Pose = new Pose(new Point(0, 0, -0.0125f), new Quaternion(0, 0, 0, 1)),
                Scale = new Vector3(0.90f, 0.90f, 0.025f),
                Color = new ColorRGBA(1, 1, 1, 1),
                Lifetime = new duration(),
                FrameLocked = true,
            };
            MarkerArray array = new MarkerArray(new[] { marker });

            while(true)
            {
                publisherTf.Publish(tf);
                publisherMarkers.Publish(array);
                Thread.Sleep(1000);
            }
            Console.In.Read();
        }

        static void Main_Old4()
        {
            Stream stream = File.Open("/Users/akzeac/Downloads/001-0.jpg", FileMode.Open);
            var image = new JpegImage(stream);
            Console.WriteLine(image.BitsPerComponent);
            Console.WriteLine(image.Colorspace);

            const int bmpHeaderLength = 54;
            byte[] buffer = new byte[image.Width * image.Height * 3 + bmpHeaderLength];
            Stream stream2 = new MemoryStream(buffer);
            image.WriteBitmap(stream2);
            Console.WriteLine(image.Width + " " + image.Height + " " + buffer.Length + " " + stream2.Position);
            Console.In.Read();
        }

        static void Main_Old5()
        {
            RosClient client = new RosClient(
                //"http://192.168.0.73:11311",
                "http://141.3.59.5:11311",
                null,
                //"http://192.168.0.157:7613"
                "http://141.3.59.19:7614"
                );

            Console.WriteLine(client.GetSystemState());
            client.Subscribe<TFMessage>("/tf", Callback);
            Console.In.Read();
        }

        static void Main_Old(string[] args)
        {
            BridgeClient client = new BridgeClient("ws://192.168.0.157:8080");

            TransformStamped[] tfs = new TransformStamped[1];
            tfs[0] = new TransformStamped
            (
                Header: new Iviz.Msgs.StdMsgs.Header(),
                ChildFrameId: "",
                Transform: new Transform
                (
                    Translation: new Vector3
                    (
                        X: 0,
                        Y: 0,
                        Z: 1
                    ),
                    Rotation: new Quaternion
                    (
                        X: 0,
                        Y: 0,
                        Z: 0,
                        W: 1
                    )
                )
            );
            TFMessage tf = new TFMessage
            {
                Transforms = tfs
            };

            BridgePublisher<TFMessage> publisher = client.Advertise<TFMessage>("/tf");

            while (true)
            {
                publisher.Publish(tf);
                //Console.WriteLine(">> " + tf.ToJsonString());
                Thread.Sleep(1000);
            }
        }

        static void Main_Old_2(string[] args)
        {
            RosClient client = new RosClient(
                //"http://192.168.0.73:11311",
                "http://141.3.59.5:11311",
                null,
                //"http://192.168.0.157:7614"
                "http://141.3.59.35:7614"
                );

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
            /*
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
            */

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

            */
            TransformStamped[] tfs = new TransformStamped[1];
            tfs[0] = new TransformStamped
            (
                Header: new Iviz.Msgs.StdMsgs.Header(),
                ChildFrameId: "",
                Transform: new Transform
                (
                    Translation: new Vector3
                    (
                        X: 0,
                        Y: 0,
                        Z: 1
                    ),
                    Rotation: new Quaternion
                    (
                        X: 0,
                        Y: 0,
                        Z: 0,
                        W: 1
                    )
                )
            );
            TFMessage tf = new TFMessage
            {
                Transforms = tfs
            };
            /*
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
            //client.Subscribe<TFMessage>("/tf", Callback);
            client.Advertise<TFMessage>("/tf", out RosPublisher publisher);

            //client.Subscribe<Marker>("/hololens/environment", Callback);


            while (true)
            {
                publisher.Publish(tf);
                //Console.WriteLine(">> " + tf.ToJsonString());
                Thread.Sleep(1000);
            }

            Console.Read();
            client.Close();

        }

        static void Callback(Iviz.Msgs.StdMsgs.Int32 value)
        {
            Console.WriteLine("<< " + value.ToJsonString());
        }

        static void Callback(TFMessage value)
        {
            Console.WriteLine("<< " + value.ToJsonString());
        }

        static void Callback(Marker value)
        {
            Console.WriteLine("<< " + value.ToJsonString());
        }
    }
}
