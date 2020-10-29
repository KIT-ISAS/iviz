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
                p.Create(packageDir, package);
            }

            p.ResolveAll();

            foreach(ClassInfo classInfo in p.messages.Values)
            {
                string packageDir = ivizMsgPaths + "/" + classInfo.RosPackage + "/msg/";
                Directory.CreateDirectory(packageDir);
                string text = classInfo.ToCString();
                File.WriteAllText(packageDir + classInfo.RosName + ".cs", text);
            }

            foreach (ServiceInfo classInfo in p.services.Values)
            {
                string packageDir = ivizMsgPaths + "/" + classInfo.RosPackage + "/srv/";
                Directory.CreateDirectory(packageDir);
                string text = classInfo.ToCString();
                File.WriteAllText(packageDir + classInfo.Name + ".cs", text);
            }
        }

    }
}
