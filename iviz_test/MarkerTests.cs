using System;
using System.Threading;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;
using Iviz.Roslib.MarkerHelper;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using Uri = System.Uri;
using Vector3 = Iviz.Msgs.GeometryMsgs.Vector3;

namespace iviz_test
{
    static class MarkerTests
    {
        static Uri RosMasterUri => RosClient.EnvironmentMasterUri ??
                                   throw new NullReferenceException("Please set the ROS_MASTER_URI variable!");

        public static void MarkerListTests()
        {
            Console.WriteLine("** Starting Marker test!");

            Logger.Log = Console.WriteLine;
            Logger.LogDebug = Console.WriteLine;
            Logger.LogError = Console.WriteLine;

            using RosClient client = new RosClient(RosMasterUri, callerUri: RosClient.TryGetCallerUri(7632));
            using RosMarker helper = new RosMarker(client);

            helper.CreateArrow(Pose.Identity, ColorRGBA.Red, new Vector3(2, 1, 10));
            
            helper.CreateArrow(Point.One, Point.Zero, ColorRGBA.Red);
            helper.CreateCube(Pose.Identity.WithPosition((1, 0, 0)), ColorRGBA.Magenta, 0.5 * Vector3.One);
            helper.CreateSphere(Pose.Identity.WithPosition((2, 0, 0)), ColorRGBA.Blue, 0.5 * Vector3.One);
            helper.CreateCylinder(new Point(3, 0, 0), ColorRGBA.Yellow, 0.5 * Vector3.One);
            helper.CreateTextViewFacing("Cube", new Point(1, 0, 0.5), ColorRGBA.Yellow, 0.1);
            helper.CreateTextViewFacing("Sphere", new Point(2, 0, 0.5), ColorRGBA.Blue, 0.1);

            Point[] points =
            {
                new Point(-3, -1, 0.5),
                new Point(3, -1, 0.5),
                new Point(3, 1, 0.5),
                new Point(-3, 1, 0.5),
            };

            helper.CreateLines(points, Pose.Identity, ColorRGBA.Red, 0.05f);

            ColorRGBA[] colors =
            {
                ColorRGBA.Red,
                ColorRGBA.Green,
                ColorRGBA.Blue,
                ColorRGBA.White,
            };

            helper.CreateLines(points.Select(x => x + 0.2 * Point.UnitZ).ToArray(), colors,
                Pose.Identity, 0.05f);

            Point[] points2 =
            {
                new Point(-3, -1, 0),
                new Point(3, -1, 0),
                new Point(3, 1, 0),
                new Point(-3, 1, 0),
                new Point(-3, -1, 0),
            };

            helper.CreateLineStrip(points2, Pose.Identity, ColorRGBA.Green, 0.05f);

            ColorRGBA[] colors2 =
            {
                ColorRGBA.Red,
                ColorRGBA.Green,
                ColorRGBA.Blue,
                ColorRGBA.White,
                ColorRGBA.Cyan,
            };

            helper.CreateLineStrip(points2.Select(x => x + 0.2 * Point.UnitZ).ToArray(), colors2,
                Pose.Identity, 0.05f);
                

            while (true)
            {
                helper.ApplyChanges();
                Thread.Sleep(1000);
            }
        }
    }
}