using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Iviz.Sdf
{
    /*
    public sealed class Joint
    {
        public string Name { get; }
        public JointType Type { get; }
        public string Parent { get; }
        public string Child { get; }
        public Pose Pose { get; } = Pose.Identity;
    }
    */


    public sealed class World
    {
        static readonly Vector3 DefaultGravity = new Vector3(0, 0, -9.8);
        
        public string Name { get; }
        public Wind Wind { get; }
        public ReadOnlyCollection<Include> Includes { get; }
        public Vector3 Gravity { get; } = DefaultGravity;
        public ReadOnlyCollection<Light> Lights { get; }
        public ReadOnlyCollection<Frame> Frames { get; }
        public ReadOnlyCollection<Model> Models { get; }

        internal World(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;
            
            List<Include> includes = new List<Include>();
            List<Model> models = new List<Model>();
            List<Frame> frames = new List<Frame>();
            List<Light> lights = new List<Light>();

            Includes = includes.AsReadOnly();
            Models = models.AsReadOnly();
            Frames = frames.AsReadOnly();
            Lights = lights.AsReadOnly();

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "wind":
                        Wind = new Wind(child);
                        break;
                    case "gravity":
                        Gravity = new Vector3(child);
                        break;
                    case "include":
                        includes.Add(new Include(child));
                        break;
                    case "model":
                        models.Add(new Model(child));
                        break;
                    case "frame":
                        frames.Add(new Frame(child));
                        break;
                    case "light":
                        lights.Add(new Light(child));
                        break;
                }
            }
        }
    }
}