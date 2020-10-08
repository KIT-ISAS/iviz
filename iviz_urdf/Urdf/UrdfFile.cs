using System;
using System.IO;
using System.Xml;

namespace Iviz.Urdf
{
    public static class UrdfFile
    {
        public static Robot CreateFromXml(string xmlData)
        {
            if (string.IsNullOrEmpty(xmlData))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlData));
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlData);
            return Robot.Create(document);
        }

        public static Robot CreateFromFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));
            }

            XmlDocument document = new XmlDocument();
            document.Load(path);
            return Robot.Create(document);
        }

        public static Robot CreateFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            XmlDocument document = new XmlDocument();
            document.Load(stream);
            return Robot.Create(document);
        }
    
    }
}