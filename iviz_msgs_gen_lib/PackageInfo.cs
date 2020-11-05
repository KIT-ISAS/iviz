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

        static void CollectMsgFiles(string path, ICollection<string> fullMsgPaths)
        {
            GetFilesWithExtension(path, fullMsgPaths, ".msg");
        }

        static void CollectSrvFiles(string path, ICollection<string> fullSrvPaths)
        {
            GetFilesWithExtension(path, fullSrvPaths, ".srv");
        }

        static void CollectActionFiles(string path, ICollection<string> fullActionPaths)
        {
            GetFilesWithExtension(path, fullActionPaths, ".action");
        }

        public void AddAllInPackagePath(string packagePath, string packageName)
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

            List<string> msgFullPaths = new List<string>();
            CollectMsgFiles(packagePath, msgFullPaths);

            foreach (string msgPath in msgFullPaths)
            {
                ClassInfo classInfo = new ClassInfo(packageName, msgPath);
                messages.Add(classInfo.FullRosName, classInfo);
            }

            List<string> srvFullPaths = new List<string>();
            CollectSrvFiles(packagePath, srvFullPaths);

            foreach (string srvPath in srvFullPaths)
            {
                ServiceInfo classInfo = new ServiceInfo(packageName, srvPath);
                services.Add(classInfo.FullRosName, classInfo);
            }

            List<string> actionFullPaths = new List<string>();
            CollectActionFiles(packagePath, actionFullPaths);
            
            foreach (string actionPath in actionFullPaths)
            {
                var actionClasses = ActionGenerator.GenerateFor(packageName, actionPath);
                if (actionClasses == null)
                {
                    continue;
                }

                foreach (var classInfo in actionClasses)
                {
                    messages[classInfo.FullRosName] = classInfo;
                }
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