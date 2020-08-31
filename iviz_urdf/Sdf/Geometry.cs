using System.Collections.Generic;
using System.IO;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Geometry
    {
        public Empty Empty { get; }
        public Box Box { get; }
        public Cylinder Cylinder { get; }
        public Mesh Mesh { get; }
        public Plane Plane { get; }
        public Sphere Sphere { get; }
        
        internal bool HasUri { get; }
        
        internal Geometry(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "empty": 
                        Empty = Empty.Instance;
                        break;
                    case "box":
                        Box = new Box(child);
                        break;
                    case "cylinder":
                        Cylinder = new Cylinder(child);
                        break;
                    case "mesh":
                        Mesh = new Mesh(child);
                        break;
                    case "plane":
                        Plane = new Plane(child);
                        break;
                    case "sphere":
                        Sphere = new Sphere(child);
                        break;
                }
            }

            HasUri = Mesh != null;
        }
        
        Geometry(Geometry source, IReadOnlyDictionary<string, string> modelPaths)
        {
            Empty = source.Empty;
            Box = source.Box;
            Cylinder = source.Cylinder;
            Plane = source.Plane;
            Sphere = source.Sphere;

            System.Uri meshUri = source.Mesh.Uri.ToUri();
            string modelPackage = meshUri.Host;
            string modelPath = modelPaths[modelPackage];
            
            string basePath = modelPaths[""];
            
            System.Uri resolvedUri;
            if (modelPath.StartsWith(basePath))
            {
                string packageName = new DirectoryInfo(basePath).Name;
                string relativePath = "/" + modelPath.Substring(basePath.Length);
                resolvedUri = new System.Uri($"package://{packageName}{relativePath}{meshUri.AbsolutePath}");
            }
            else
            {
                // if this happens, then its unfortunate, I cannot tell from here which package this belongs to
                // file scheme is a security risk and will probably get rejected
                resolvedUri = new System.Uri($"file://{modelPath}{meshUri.AbsolutePath}");
            }
            
            Mesh = new Mesh(new Uri(resolvedUri), source.Mesh.Scale);
        }
        
        internal Geometry ResolveUris(IReadOnlyDictionary<string, string> modelPaths)
        {
            return HasUri ? new Geometry(this, modelPaths) : this;
        }         
    }
}