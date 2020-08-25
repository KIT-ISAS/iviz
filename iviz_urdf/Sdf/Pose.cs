using System;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Pose
    {
        public string RelativeTo { get;}
        public Vector3 Position { get; } = Vector3.Zero;
        public Vector3 Orientation { get; } = Vector3.Zero;

        Pose() {}

        internal Pose(XmlNode node)
        {
            RelativeTo = node.Attributes?["relative_to"]?.Value;

            string[] elems = node.Value.Split(Vector3.Separator, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 6)
            {
                throw new MalformedSdfException(node);
            }

            double x = double.Parse(elems[0], Utils.Culture);
            double y = double.Parse(elems[1], Utils.Culture);
            double z = double.Parse(elems[2], Utils.Culture);
            double rr = double.Parse(elems[3], Utils.Culture);
            double rp = double.Parse(elems[4], Utils.Culture);
            double ry = double.Parse(elems[5], Utils.Culture);

            Position = new Vector3(x, y, z);
            Orientation = new Vector3(rr, rp, ry);
        }
        
        public static readonly Pose Identity = new Pose();        
    }
}