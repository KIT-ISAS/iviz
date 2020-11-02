using System;
using System.IO;

namespace Iviz.MsgsGen
{
    static class Program
    {
        static void Main()
        {
            string rosBasePath;
            string ivizMsgPaths;

            string debugRosBasePath = Path.GetFullPath("../../../../ros_msgs");
            string debugIvizMsgPaths = Path.GetFullPath("../../../../iviz_msgs");

            string releaseRosBasePath = Path.GetFullPath("../ros_msgs");
            string releaseIvizMsgPaths = Path.GetFullPath("../iviz_msgs");

            Console.WriteLine("** Starting iviz_msgs_gen...");

            if (Directory.Exists(debugRosBasePath) && Directory.Exists(debugIvizMsgPaths))
            {
                // running from an IDE
                rosBasePath = debugRosBasePath;
                ivizMsgPaths = debugIvizMsgPaths;
            }
            else if (Directory.Exists(releaseRosBasePath) && Directory.Exists(releaseIvizMsgPaths))
            {
                // running from the Publish folder
                rosBasePath = releaseRosBasePath;
                ivizMsgPaths = releaseIvizMsgPaths;
            }
            else
            {
                Console.WriteLine(
                    "EE Failed to find the iviz_msgs and ros_msgs folders. They should be in the ../ folder from where you run this.");
                return;
            }

            PackageInfo p = new PackageInfo();

            string[] packages = Directory.GetDirectories(rosBasePath);
            foreach (string packageDir in packages)
            {
                string package = Path.GetFileName(packageDir);
                p.AddPackagePath(packageDir, package);
            }

            p.ResolveAll();

            foreach (ClassInfo classInfo in p.Messages.Values)
            {
                string dstPackageDir = $"{ivizMsgPaths}/{classInfo.RosPackage}/msg/";
                Directory.CreateDirectory(dstPackageDir);
                string text = classInfo.ToCString();
                File.WriteAllText($"{dstPackageDir}{classInfo.Name}.cs", text);
            }

            foreach (ServiceInfo classInfo in p.Services.Values)
            {
                string packageDir = $"{ivizMsgPaths}/{classInfo.RosPackage}/srv/";
                Directory.CreateDirectory(packageDir);
                string text = classInfo.ToCString();
                File.WriteAllText($"{packageDir}{classInfo.Name}.cs", text);
            }
            
            Console.WriteLine("** Done!");
        }
    }
}