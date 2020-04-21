using System;
using System.Collections.Generic;
using System.IO;

namespace Iviz.MsgsGen
{
    public class PackageInfo
    {
        public readonly Dictionary<string, ClassInfo> classes = new Dictionary<string, ClassInfo>();

        static void GetMessages(string path, List<string> msgs)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (Path.GetExtension(file) == ".msg")
                {
                    msgs.Add(file);
                }
            }
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                GetMessages(dir, msgs);
            }
        }

        public void Create(string path, string package)
        {
            List<string> msgs = new List<string>();
            GetMessages(path, msgs);

            foreach (string msg in msgs)
            {
                ClassInfo classInfo = new ClassInfo(package, msg);
                classes.Add(classInfo.package + "/" + classInfo.name, classInfo);
            }
        }

        public bool TryGet(string name, string package, out ClassInfo classInfo)
        {
            return
                classes.TryGetValue(name, out classInfo) ||
                classes.TryGetValue(package + "/" + name, out classInfo);
        }

        public void ResolveAll()
        {
            foreach (var classInfo in classes.Values)
            {
                classInfo.ResolveClasses(this);
            }
            foreach (var classInfo in classes.Values)
            {
                classInfo.CheckFixedSize();
            }
        }

        public void ToCString()
        {
            foreach (var classInfo in classes.Values)
            {
                Console.WriteLine(classInfo.ToCString());
            }
        }
    }
}
