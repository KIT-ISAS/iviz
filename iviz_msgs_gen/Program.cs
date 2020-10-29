using System;
using System.IO;

namespace Iviz.MsgsGen
{
    class Program
    {
        static void Main()
        {
            string rosBasePath = Path.GetFullPath("../../../../ros_msgs");
            string ivizMsgPaths = Path.GetFullPath("../../../../iviz_msgs");

            PackageInfo p = new PackageInfo();

            string[] packages = Directory.GetDirectories(rosBasePath);
            foreach(string packageDir in packages)
            {
                string package = Path.GetFileName(packageDir);
                p.AddPackagePath(packageDir, package);
            }

            p.ResolveAll();

            foreach(ClassInfo classInfo in p.Messages.Values)
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
        }

    }
}
