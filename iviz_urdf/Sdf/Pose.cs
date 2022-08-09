using System;
using System.Globalization;
using System.Xml;
using Iviz.Urdf;

namespace Iviz.Sdf
{
    public sealed class Pose
    {
        public string? RelativeTo { get; }
        public Vector3d Position { get; } = Vector3d.Zero;
        public Vector3d Orientation { get; } = Vector3d.Zero;

        Pose()
        {
        }

        internal Pose(XmlNode node)
        {
            RelativeTo = node.Attributes?["relative_to"]?.Value;

            if (node.InnerText is null)
            {
                throw new MalformedSdfException();
            }

            string[] elems = node.InnerText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 6)
            {
                throw new MalformedSdfException(node);
            }

            double x = double.Parse(elems[0], NumberStyles.Any, Utils.Culture);
            double y = double.Parse(elems[1], NumberStyles.Any, Utils.Culture);
            double z = double.Parse(elems[2], NumberStyles.Any, Utils.Culture);
            double rr = double.Parse(elems[3], NumberStyles.Any, Utils.Culture);
            double rp = double.Parse(elems[4], NumberStyles.Any, Utils.Culture);
            double ry = double.Parse(elems[5], NumberStyles.Any, Utils.Culture);

            Position = new Vector3d(x, y, z);
            Orientation = new Vector3d(rr, rp, ry);
        }

        public static readonly Pose Identity = new Pose();
    }
}