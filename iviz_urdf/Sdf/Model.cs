using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Model
    {
        public string Name { get; }
        public string CanonicalLink { get; }
        public bool Static { get; }
        public bool SelfCollide { get; }
        public bool AllowAutoDisable { get; }
        public ReadOnlyCollection<Include> Includes { get; }
        public ReadOnlyCollection<Model> Models { get; }
        public bool EnableWind { get; }
        public ReadOnlyCollection<Frame> Frames { get; }
        public Pose Pose { get; } = Pose.Identity;
        public Pose IncludePose { get; }
        public ReadOnlyCollection<Link> Links { get; }

        internal bool HasIncludes { get; }

        internal Model(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;
            CanonicalLink = node.Attributes?["canonical_link"]?.Value;

            List<Include> includes = new List<Include>();
            List<Model> models = new List<Model>();
            List<Frame> frames = new List<Frame>();
            List<Link> links = new List<Link>();

            Includes = includes.AsReadOnly();
            Models = models.AsReadOnly();
            Frames = frames.AsReadOnly();
            Links = links.AsReadOnly();

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "static":
                        Static = BoolElement.ValueOf(child);
                        break;
                    case "self_collide":
                        SelfCollide = BoolElement.ValueOf(child);
                        break;
                    case "allow_auto_disable":
                        AllowAutoDisable = BoolElement.ValueOf(child);
                        break;
                    case "include":
                        includes.Add(new Include(child));
                        break;
                    case "model":
                        models.Add(new Model(child));
                        break;
                    case "enable_wind":
                        EnableWind = BoolElement.ValueOf(child);
                        break;
                    case "frame":
                        frames.Add(new Frame(child));
                        break;
                    case "pose":
                        Pose = new Pose(child);
                        break;
                    case "link":
                        links.Add(new Link(child));
                        break;
                }
            }

            HasIncludes = 
                Includes.Count != 0 || 
                Models.Any(model => model.HasIncludes) ||
                Links.Any(link => link.HasUri);
        }

        Model(Model source, IReadOnlyDictionary<string, string> modelPaths)
        {
            Name = source.Name;
            CanonicalLink = source.CanonicalLink;
            Static = source.Static;
            SelfCollide = source.SelfCollide;
            AllowAutoDisable = source.AllowAutoDisable;
            EnableWind = source.EnableWind;
            Frames = source.Frames;
            Pose = source.Pose;
            Links = source.Links.Select(link => link.ResolveUris(modelPaths)).ToList().AsReadOnly();

            var resolvedModels = source.Models.Select(model => model.ResolveIncludes(modelPaths));
            var resolvedIncludes = source.Includes.Select(include => new Model(include, modelPaths));

            Models = resolvedModels.Concat(resolvedIncludes).ToList().AsReadOnly();
            Includes = new List<Include>().AsReadOnly();
        }

        internal Model(Include include, IReadOnlyDictionary<string, string> modelPaths)
        {
            System.Uri uri = include.Uri.ToUri();

            if (!modelPaths.TryGetValue(uri.Host.ToUpperInvariant(), out string path))
            {
                Console.Error.WriteLine("Model: Failed to find path '" + uri.Host + "'");
                return;
            }

            string sdfPath = path + "/model.sdf";
            if (!File.Exists(sdfPath))
            {
                throw new FileNotFoundException(sdfPath);
            }

            string sdfText = File.ReadAllText(sdfPath);
            SdfFile sdfFile = SdfFile.Create(sdfText);
            if (sdfFile.Models.Count == 0)
            {
                Console.WriteLine("Warning: Included model file " + sdfPath + " with no models!");
                return;
            }

            Model source = sdfFile.Models[0].ResolveIncludes(modelPaths);

            Name = include.Name ?? source.Name;
            CanonicalLink = source.CanonicalLink;
            Static = include.Static ?? source.Static;
            SelfCollide = source.SelfCollide;
            AllowAutoDisable = source.AllowAutoDisable;
            EnableWind = source.EnableWind;
            Frames = source.Frames;
            Pose = source.Pose;
            IncludePose = include.Pose;
            Links = Name is null ? 
                source.Links : 
                source.Links.Select(link => new Link(link, Name + "::" + link.Name)).ToList().AsReadOnly();
            Models = source.Models;
            Includes = source.Includes; // should be empty
        }

        internal Model ResolveIncludes(IReadOnlyDictionary<string, string> modelPaths)
        {
            return HasIncludes ? new Model(this, modelPaths) : this;
        }

        public bool IsInvalid => Models is null || Links is null;
    }
}