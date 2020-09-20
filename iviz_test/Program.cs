using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using BitMiracle.LibJpeg;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Roslib;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Int64 = System.Int64;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using Vector3 = Iviz.Msgs.GeometryMsgs.Vector3;

namespace iviz_test
{
    class Program
    {
        static void Main()
        {
            Uri masterUri = RosClient.EnvironmentMasterUri ?? 
                            new Uri("http://192.168.0.220:11311");
            string callerId = "/iviz_test";

            RosClient client = new RosClient(masterUri, callerId);

            client.Advertise<PoseStamped>("/test_topic", out RosPublisher publisher);

            PoseStamped msg = new PoseStamped
            {
                Header = new Header { FrameId = "/map", Stamp = time.Now() },
                Pose = new Pose
                {
                    Orientation = new Quaternion(0, 0, 0, 1),
                    Position = new Point(0, 0, 2)
                }
            };

            for (int i = 0; i < 20; i++)
            {
                publisher.Publish(msg);
                Thread.Sleep(1000);
            }

            client.Close();
            
            /*
            string data =
                File.ReadAllText("/Users/akzeac/Shared/aws-robomaker-hospital-world/worlds/hospital.world");
            SdfFile sdfFile = SdfFile.Create(data);

            var modelPaths = SdfFile.CreateModelPaths("/Users/akzeac/Shared/aws-robomaker-hospital-world/");
            SdfFile newSdfFile = sdfFile.ResolveIncludes(modelPaths);
            */



            //Console.WriteLine(JsonConvert.SerializeObject(newSdfFile, Formatting.Indented));

        }
        
        /*
        static void CheckModelPath(string folderName, string path, IDictionary<string, List<string>> modelPaths)
        {
            if (File.Exists(path + "/model.config"))
            {
                AddModelPath(folderName, path, modelPaths);
                return;
            }

            foreach (string subFolderPath in Directory.GetDirectories(path))
            {
                string subFolder = Path.GetFileName(subFolderPath);
                CheckModelPath(subFolder, subFolderPath, modelPaths);
            }
        }

        static void AddModelPath(string package, string path, IDictionary<string, List<string>> modelPaths)
        {
            if (!modelPaths.TryGetValue(package, out List<string> paths))
            {
                paths = new List<string>();
                modelPaths[package.ToLower()] = paths;
            }
            paths.Add(path);
            //Console.WriteLine("++ " + package);
        }
        */
        
        static void Main_Q()
        {
            using RosClient client = new RosClient(
                "http://192.168.0.220:11311",
                //"http://141.3.59.5:11311",
                null,
                "http://192.168.0.157:7619"
                //"http://141.3.59.19:7621"
            );            
            client.Advertise<JointState>("/joints", out RosPublisher publisher2);

            double k = 0.5f;
            while (true)
            {
                Console.WriteLine("publishing...");
                JointState msg = new JointState()
                {
                    
                    Header = new Header(),
                    Name = new []{ "boom_revolute" },
                    Position = new []{ k }
                };

                publisher2.Publish(msg);

                Thread.Sleep(100);
            }
        }
        
        
        static void Main_P()
        {
            RosClient client = new RosClient(
                //"http://192.168.0.73:11311",
                "http://141.3.59.5:11311",
                null,
                //"http://192.168.0.157:7619"
                "http://141.3.59.19:7621"
            );
            InteractiveMarker intMarker = new InteractiveMarker();
            intMarker.Header = new Header()
            {
                FrameId = "map",
                Stamp = time.Now(),
            };
            intMarker.Name = "my_marker";
            intMarker.Scale = 1;
            intMarker.Description = "Simple 1-DOF Control";

            Marker boxMarker = new Marker();
            boxMarker.Header = new Header()
            {
                FrameId = ""
            };
            boxMarker.Action = Marker.ADD;
            boxMarker.Type = Marker.CUBE;
            boxMarker.Scale = new Vector3(0.45f, 0.45, 0.45);
            boxMarker.Color = new ColorRGBA(0.5f, 0.5f, 0.5f, 1);

            InteractiveMarkerControl boxControl = new InteractiveMarkerControl();
            boxControl.AlwaysVisible = true;
            boxControl.Markers = new[] {boxMarker};

            /*
            InteractiveMarkerControl rotateControl = new InteractiveMarkerControl();
            rotateControl.Name = "move_x";
            rotateControl.InteractionMode = InteractiveMarkerControl.MOVE_AXIS;
            */
            InteractiveMarkerControl control = new InteractiveMarkerControl
            {
                Name = "e2_move",
                Orientation = new Quaternion(0, 1, 0, 1),
                InteractionMode = InteractiveMarkerControl.MOVE_PLANE,
                OrientationMode = InteractiveMarkerControl.INHERIT
            };

            InteractiveMarkerControl control2 = new InteractiveMarkerControl
            {
                Name = "e2_rotate",
                Orientation = new Quaternion(0, 1, 0, 1),
                InteractionMode = InteractiveMarkerControl.ROTATE_AXIS,
                OrientationMode = InteractiveMarkerControl.INHERIT
            };

            intMarker.Controls = new[] {boxControl, control, control2};

            client.Advertise<InteractiveMarkerUpdate>("/update", out RosPublisher publisher);
            client.Advertise<TFMessage>("/tf", out RosPublisher publisher2);
            client.Subscribe<InteractiveMarkerFeedback>("/interactive_markers/feedback", Callback);
            while (true)
            {
                Console.WriteLine("publishing...");
                publisher.Publish(new InteractiveMarkerUpdate()
                {
                    Type = InteractiveMarkerUpdate.UPDATE,
                    Markers = new[] {intMarker}
                });

                position = targetPosition.Subtract(position).Normalized().Multiply(v).Add(position);
                if (targetPosition.Subtract(position).Magnitude() > 2)
                {
                    double newAngle = Math.Atan2(targetPosition.Y - position.Y, targetPosition.X - position.X);
                    angle += (newAngle - angle) * v2;
                }
                Quaternion quaternion = VectorStuff.ToQuat(angle);

                TFMessage msg = new TFMessage()
                {
                    Transforms = new[]
                    {
                        new TransformStamped()
                        {
                            Header = new Header()
                            {
                                FrameId = "map",
                                Stamp = time.Now()
                            },
                            ChildFrameId = "e2/base_link",
                            Transform = new Transform()
                            {
                                Translation = new Vector3(position.X, position.Y, position.Z),
                                Rotation = quaternion
                            }
                        }
                    }
                };
                publisher2.Publish(msg);

                Thread.Sleep(10);
            }
        }

        static double angle = 0;
        static Point position = new Point(-20, 20, 0);
        static double v = 0.01f;
        static double v2 = 0.01f;
        static Point targetPosition = new Point(0, 0, 0);


        static void Callback(InteractiveMarkerFeedback f)
        {
            if (f.EventType != InteractiveMarkerFeedback.POSE_UPDATE ||
                f.ControlName.Length < 3 ||
                f.ControlName.Substring(0, 3) != "e2_")
            {
            }

            targetPosition = f.Pose.Position;
        }

        static void Main_D()
        {
            RosClient client = new RosClient(
                "http://192.168.0.73:11311",
                //"http://141.3.59.5:11311",
                null,
                "http://192.168.0.157:7619"
                //"http://141.3.59.19:7621"
            );
            Iviz.Msgs.NavMsgs.Path path = new Iviz.Msgs.NavMsgs.Path();

            List<PoseStamped> list = new List<PoseStamped>();

            path.Header = new Header(0, new time(DateTime.Now), "map");
            for (int i = 0; i < 5; i++)
            {
                float t = i / 5f * 6;
                list.Add(new PoseStamped(path.Header, new Pose(
                    new Point(Math.Sin(t), Math.Cos(t), t), new Quaternion(0, 0, 0, 1))));
            }

            path.Poses = list.ToArray();

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
            MarkerArray array = new MarkerArray(new[] {marker});

            while (true)
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

    public static class VectorStuff
    {
        public static Point Lerp(in Point A, in Point B, double t)
        {
            return new Point(A.X + t * (B.X - A.X), A.Y + t * (B.Y - A.Y), A.Z + t * (B.Z - A.Z));
        }

        public static double Magnitude(this Point A)
        {
            return Math.Sqrt(A.X * A.X + A.Y * A.Y + A.Z * A.Z);
        }

        public static Point Add(this Point A, in Point B)
        {
            return new Point(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        public static Point Subtract(this Point A, in Point B)
        {
            return new Point(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
        }

        public static Point Multiply(this Point A, double t)
        {
            return new Point(A.X * t, A.Y * t, A.Z * t);
        }

        public static Point Normalized(this Point A)
        {
            return A.Multiply(1 / A.Magnitude());
        }

        public static Quaternion ToQuat(double a)
        {
            return new Quaternion(0, 0, Math.Sin(a / 2), Math.Cos(a / 2));
        }
    }
}