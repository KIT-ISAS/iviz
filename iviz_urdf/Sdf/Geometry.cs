using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Geometry
    {
        public Empty? Empty { get; }
        public Box? Box { get; }
        public Cylinder? Cylinder { get; }
        public Mesh? Mesh { get; }
        public Plane? Plane { get; }
        public Sphere? Sphere { get; }
        
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

            if (source.Mesh == null)
            {
                throw new InvalidOperationException("Internal error: Mesh should not be null");
            }
            
            System.Uri meshUri = source.Mesh.Uri.ToUri();
            string modelPackage = meshUri.Host;
            string modelPath = modelPaths[modelPackage.ToUpperInvariant()];
            string modelRelativePath = System.Uri.UnescapeDataString(meshUri.AbsolutePath);
            
            string basePath = modelPaths[""];
            
            System.Uri resolvedUri;
            if (modelPath.StartsWith(basePath, false, Utils.Culture))
            {
                string packageName = new DirectoryInfo(basePath).Name;
                string packageToModelPath = "/" + modelPath.Substring(basePath.Length);
                resolvedUri = new System.Uri($"package://{packageName}{packageToModelPath}{modelRelativePath}");
            }
            else
            {
                // if this happens, then its unfortunate, we cannot tell from here which package this belongs to.
                // we send a 'file' scheme but it will probably get rejected for being a security risk
                resolvedUri = new System.Uri($"file://{modelPath}{modelRelativePath}");
            }
            
            Mesh = new Mesh(new Uri(resolvedUri), source.Mesh.Scale);
        }
        
        internal Geometry ResolveUris(IReadOnlyDictionary<string, string> modelPaths)
        {
            return HasUri ? new Geometry(this, modelPaths) : this;
        }         
    }
}