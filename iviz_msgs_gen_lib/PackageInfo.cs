using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Iviz.MsgsGen
{
    public sealed class PackageInfo
    {
        readonly Dictionary<string, ClassInfo> messages = new Dictionary<string, ClassInfo>();
        readonly Dictionary<string, ServiceInfo> services = new Dictionary<string, ServiceInfo>();
        readonly HashSet<string> packages = new HashSet<string>();

        public PackageInfo()
        {
            Messages = new ReadOnlyDictionary<string, ClassInfo>(messages);
            Services = new ReadOnlyDictionary<string, ServiceInfo>(services);
        }

        public IReadOnlyDictionary<string, ClassInfo> Messages { get; }
        public IReadOnlyDictionary<string, ServiceInfo> Services { get; }

        static void GetFilesWithExtension(string path, ICollection<string> msgs, string ext)
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
            foreach (string dir in dirs) GetFilesWithExtension(dir, msgs, ext);
        }

        static void CollectMsgFiles(string path, ICollection<string> msgs)
        {
            GetFilesWithExtension(path, msgs, ".msg");
        }

        static void CollectSrvFiles(string path, ICollection<string> srvs)
        {
            GetFilesWithExtension(path, srvs, ".srv");
        }

        public void AddPackagePath(string packagePath, string packageName)
        {
            if (string.IsNullOrEmpty(packagePath))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(packagePath));
            }

            if (string.IsNullOrEmpty(packageName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(packageName));
            }

            if (packages.Contains(packageName))
            {
                throw new ArgumentException($"Package '{packageName}' has already been added!", nameof(packageName));
            }

            packages.Add(packageName);

            List<string> msgs = new List<string>();
            CollectMsgFiles(packagePath, msgs);

            foreach (string msg in msgs)
            {
                ClassInfo classInfo = new ClassInfo(packageName, msg);
                messages.Add(classInfo.FullRosName, classInfo);
            }

            List<string> srvs = new List<string>();
            CollectSrvFiles(packagePath, srvs);

            foreach (string srv in srvs)
            {
                ServiceInfo classInfo = new ServiceInfo(packageName, srv);
                services.Add(classInfo.FullRosName, classInfo);
            }
        }

        internal bool TryGet(string name, string package, out ClassInfo classInfo)
        {
            return
                messages.TryGetValue(name, out classInfo) ||
                messages.TryGetValue($"{package}/{name}", out classInfo);
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
    }
}