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
                if (classInfo.ForceSkip)
                {
                    continue;
                }
                string packageDir = ivizMsgPaths + "/" + classInfo.rosPackage + "/msg/";
                Directory.CreateDirectory(packageDir);
                string text = classInfo.ToCString();
                File.WriteAllText(packageDir + classInfo.name + ".cs", text);
            }

            foreach (ServiceInfo classInfo in p.services.Values)
            {
                string packageDir = ivizMsgPaths + "/" + classInfo.rosPackage + "/srv/";
                Directory.CreateDirectory(packageDir);
                string text = classInfo.ToCString();
                File.WriteAllText(packageDir + classInfo.name + ".cs", text);
            }
        }

    }
}
