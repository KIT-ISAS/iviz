using System;
using System.Collections.Generic;
using System.IO;

namespace Iviz.MsgsGen
{
    public class PackageInfo
    {
        public readonly Dictionary<string, ClassInfo> messages = new Dictionary<string, ClassInfo>();
        public readonly Dictionary<string, ServiceInfo> services = new Dictionary<string, ServiceInfo>();

        static void GetFilesWithExtension(string path, List<string> msgs, string ext)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (Path.GetExtension(file) == ext && Path.GetFileName(file)[0] != '.')
                {
                    msgs.Add(file);
                }
            }
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                GetFilesWithExtension(dir, msgs, ext);
            }
        }

        static void GetMessages(string path, List<string> msgs)
        {
            GetFilesWithExtension(path, msgs, ".msg");
        }

        static void GetServices(string path, List<string> srvs)
        {
            GetFilesWithExtension(path, srvs, ".srv");
        }


        public void Create(string path, string package)
        {
            List<string> msgs = new List<string>();
            GetMessages(path, msgs);

            foreach (string msg in msgs)
            {
                ClassInfo classInfo = new ClassInfo(package, msg);
                messages.Add(classInfo.rosPackage + "/" + classInfo.name, classInfo);
            }

            List<string> srvs = new List<string>();
            GetServices(path, srvs);

            foreach (string srv in srvs)
            {
                ServiceInfo classInfo = new ServiceInfo(package, srv);
                services.Add(classInfo.rosPackage + "/" + classInfo.name, classInfo);
            }
        }

        public bool TryGet(string name, string package, out ClassInfo classInfo)
        {
            return
                messages.TryGetValue(name, out classInfo) ||
                messages.TryGetValue(package + "/" + name, out classInfo);
        }

        public void ResolveAll()
        {
            foreach (var classInfo in messages.Values)
            {
                classInfo.ResolveClasses(this);
            }
            foreach (var classInfo in messages.Values)
            {
                classInfo.CheckFixedSize();
            }
            foreach (var classInfo in services.Values)
            {
                classInfo.ResolveClasses(this);
                classInfo.CheckFixedSize();
            }
        }

        public void ToCString()
        {
            foreach (var classInfo in messages.Values)
            {
                Console.WriteLine(classInfo.ToCString());
            }
        }
    }
}
