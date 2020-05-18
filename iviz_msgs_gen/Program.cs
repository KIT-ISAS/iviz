using System.IO;

namespace Iviz.MsgsGen
{
    class Program
    {
        static void Main(string[] args)
        {
            //string RosBasePath = args.Length > 0 ? args[0] : @"D:\Ant\msgs";
            //string IvizMsgPaths = args.Length > 1 ? args[1] : @"D:\Ant\iviz\iviz_msgs"; 

            string RosBasePath = args.Length > 0 ? args[0] : @"/Users/akzeac/Downloads/msgs";
            string IvizMsgPaths = args.Length > 1 ? args[1] : @"/Users/akzeac/Documents/iviz/iviz_msgs"; 

            PackageInfo p = new PackageInfo();

            string[] packages = Directory.GetDirectories(RosBasePath);
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
                string packageDir = IvizMsgPaths + "/" + classInfo.rosPackage + "/msg/";
                Directory.CreateDirectory(packageDir);
                string text = classInfo.ToCString();
                File.WriteAllText(packageDir + classInfo.name + ".cs", text);
            }

            foreach (ServiceInfo classInfo in p.services.Values)
            {
                string packageDir = IvizMsgPaths + "/" + classInfo.rosPackage + "/srv/";
                Directory.CreateDirectory(packageDir);
                string text = classInfo.ToCString();
                File.WriteAllText(packageDir + classInfo.name + ".cs", text);
            }
        }

    }
}
