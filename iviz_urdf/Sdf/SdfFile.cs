using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class SdfFile
    {
        public ReadOnlyCollection<World> Worlds { get; }
        public ReadOnlyCollection<Model> Models { get; }
        public ReadOnlyCollection<Light> Lights { get; }

        bool HasIncludes { get; } 
            
        SdfFile(XmlNode node)
        {
            List<Model> models = new List<Model>();
            List<World> worlds = new List<World>();
            List<Light> lights = new List<Light>();

            Models = models.AsReadOnly();
            Lights = lights.AsReadOnly();            
            Worlds = worlds.AsReadOnly();
            
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "world":
                        worlds.Add(new World(child));
                        break;
                    case "model":
                        models.Add(new Model(child));
                        break;
                    case "light":
                        lights.Add(new Light(child));
                        break;
                }
            }

            HasIncludes = Worlds.Any(world => world.HasIncludes) || Models.Any(model => model.HasIncludes);
        }

        SdfFile(SdfFile source, IReadOnlyDictionary<string, string> modelPaths)
        {
            Worlds = source.Worlds.Select(world => world.ResolveIncludes(modelPaths)).ToList().AsReadOnly();
            Models = source.Models.Select(model => model.ResolveIncludes(modelPaths)).ToList().AsReadOnly();
            Lights = source.Lights;
        }

        static SdfFile Create(XmlDocument document)
        {
            XmlNode root = document.FirstChild;
            while (root != null && root.Name != "sdf")
            {
                root = root.NextSibling;
            }

            if (root is null)
            {
                throw new MalformedSdfException("Sdf has no root node");
            }

            return new SdfFile(root);
        }        
        
        
        public static SdfFile CreateFromXml(string xmlData)
        {
            if (string.IsNullOrEmpty(xmlData))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlData));
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlData);

            return Create(document);
        }

        public static SdfFile CreateFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            XmlDocument document = new XmlDocument();
            document.Load(path);

            return Create(document);
        }
        
        public static SdfFile CreateFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            XmlDocument document = new XmlDocument();
            document.Load(stream);
            
            return Create(document);
        }        
        
        public SdfFile ResolveIncludes(IReadOnlyDictionary<string, string> modelPaths)
        {
            if (modelPaths is null)
            {
                throw new ArgumentNullException(nameof(modelPaths));
            }
            
            return HasIncludes ? new SdfFile(this, modelPaths) : this;
        }
        
        static void CheckModelPath(string folderName, string path, IDictionary<string, string> modelPaths)
        {
            if (File.Exists(path + "/model.config"))
            {
                AddModelPath(folderName, path, modelPaths);
                return;
            }

            foreach (string subFolderPath in Directory.GetDirectories(path))
            {
                string subFolder = Path.GetFileName(subFolderPath);
                CheckModelPath(subFolder, subFolderPath, modelPaths);
            }
        }

        static void AddModelPath(string package, string path, IDictionary<string, string> modelPaths)
        {
            if (modelPaths.TryGetValue(package, out string oldPackagePath))
            {
                Console.WriteLine("Warning: Duplicate path for package '" + package + "':");
                Console.WriteLine("\t" + oldPackagePath);
                Console.WriteLine("\t" + path);
            }
            
            modelPaths[package.ToUpperInvariant()] = path;
        }

        public static Dictionary<string, string> CreateModelPaths(string packagePath)
        {
            if (string.IsNullOrEmpty(packagePath))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(packagePath));
            }

            Dictionary<string, string> modelPaths = new Dictionary<string, string>
            {
                [""] = packagePath,
            };
            
            CheckModelPath("", packagePath, modelPaths);
            return modelPaths;
        }
    }
}