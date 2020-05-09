using Iviz.Msgs;
using Iviz.Msgs.sensor_msgs;
using Iviz.Msgs.std_msgs;
using Iviz.RoslibSharp;
using Microsoft.Kinect;
using OpenCvSharp;
using System;
using System.Threading;

namespace iviz_kinect
{
    class Program
    {
        static byte[] colorBuffer;
        static ushort[] depthBuffer;
        static RosClient client;
        static RosPublisher publisher;
        static uint frameId;
        static DateTime lastFrame;

        static KinectSensor kinect = KinectSensor.GetDefault();
        const string path = @"D:\AnthroPraktikum_SS20\";
        static int seq = 0;
        static void Main(string[] args)
        {
            //ColorFrameReader colorReader = kinect.ColorFrameSource.OpenReader();
            //colorReader.FrameArrived += ColorReader_FrameArrived_2;
            DepthFrameReader depthReader = kinect.DepthFrameSource.OpenReader();
            depthReader.FrameArrived += DepthReader_FrameArrived_2;

            kinect.Open();
            while (true) Thread.Sleep(1000);
        }

        static void ColorReader_FrameArrived_2(object sender, ColorFrameArrivedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastFrame).TotalMilliseconds < 1000)
            {
                return;
            }
            lastFrame = now;
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame == null)
                {
                    return;
                }

                FrameDescription description = colorFrame.FrameDescription;
                int length = description.Width * description.Height * 4;
                if (colorBuffer == null || colorBuffer.Length != length)
                {
                    colorBuffer = new byte[length];
                }
                colorFrame.CopyConvertedFrameDataToArray(colorBuffer, ColorImageFormat.Rgba);

                using (Mat mat = new Mat(new Size(description.Width, description.Height), MatType.CV_8UC4))
                {
                    mat.SetArray(colorBuffer);
                    mat.SaveImage(path + "color_" + seq + ".png");
                }
            }

        }

        static void DepthReader_FrameArrived_2(object sender, DepthFrameArrivedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastFrame).TotalMilliseconds < 1000)
            {
                return;
            }
            lastFrame = now;
            using (DepthFrame depthFrame = e.FrameReference.AcquireFrame())
            {
                if (depthFrame == null)
                {
                    return;
                }

                FrameDescription description = depthFrame.FrameDescription;
                int length = description.Width * description.Height;
                if (depthBuffer == null || depthBuffer.Length != length)
                {
                    depthBuffer = new ushort[length];
                }
                depthFrame.CopyFrameDataToArray(depthBuffer);

                using (Mat mat = new Mat(new Size(description.Width, description.Height), MatType.CV_16U))
                {
                    mat.SetArray(depthBuffer);
                    mat.SaveImage(path + "depth_" + seq + ".png");
                }
                seq++;
            }

            var coordinateMapper = kinect.CoordinateMapper;
            var calibrationData = coordinateMapper.GetDepthCameraIntrinsics();
            Console.WriteLine(calibrationData.FocalLengthX + ", 0, " + calibrationData.PrincipalPointX);
            Console.WriteLine("0, " + calibrationData.FocalLengthY + ", " + calibrationData.PrincipalPointY);

        }

        static void Main_Old(string[] args)
        {
            string masterUrl = args.Length > 0 ? args[0] : "http://141.3.59.5:11311";
            string localId = args.Length > 1 ? args[1] : "/KinectNode4";
            string localUrl = args.Length > 2 ? args[2] : "http://141.3.59.11:7613";

            client = new RosClient(masterUrl, localId, localUrl);

            client.Advertise<Image>("/kinect/image_raw", out publisher);

            KinectSensor kinect = null;
            lastFrame = DateTime.MinValue;

            while (true)
            {
                if (publisher.NumSubscribers != 0 && kinect == null)
                {
                    kinect = KinectSensor.GetDefault();
                    ColorFrameReader colorReader = kinect.ColorFrameSource.OpenReader();
                    colorReader.FrameArrived += ColorReader_FrameArrived;
                    kinect.Open();
                    Console.WriteLine("*** " + DateTime.Now + ": I have subscribers! Turning on.");
                }
                else if (publisher.NumSubscribers == 0 && kinect != null)
                {
                    kinect.Close();
                    kinect = null;
                    Console.WriteLine("*** " + DateTime.Now + ": No subscribers! Turning off.");
                }
                publisher.Cleanup();
                Thread.Sleep(1000);
            }
        }

        static void ColorReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastFrame).TotalMilliseconds < 250)
            {
                return;
            }
            lastFrame = now;
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame == null)
                {
                    return;
                }

                FrameDescription description = colorFrame.FrameDescription;
                int length = description.Width * description.Height * 4;
                if (colorBuffer == null || colorBuffer.Length != length)
                {
                    colorBuffer = new byte[length];
                }

                colorFrame.CopyConvertedFrameDataToArray(colorBuffer, ColorImageFormat.Rgba);

                Image image = new Image()
                {
                    header = new Header()
                    {
                        seq = frameId++,
                        stamp = new time(DateTime.Now),
                        frame_id = "/base_link"
                    },
                    height = (uint)description.Height,
                    width = (uint)description.Width,
                    encoding = "rgba8",
                    is_bigendian = 0,
                    step = (uint)(description.Width * 4),
                    data = colorBuffer
                };

                publisher.Publish(image);
            }

        }
    }
}
