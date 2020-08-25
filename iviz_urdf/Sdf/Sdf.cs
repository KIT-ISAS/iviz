using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Sdf
    {
        public ReadOnlyCollection<World> Worlds { get; }
        public ReadOnlyCollection<Model> Models { get; }
        public ReadOnlyCollection<Light> Lights { get; }

        public Sdf(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            
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
        }
    }
}