using System;
using System.IO;
using System.Xml;

namespace Iviz.Urdf
{
    public static class UrdfFile
    {
        /// <summary>
        /// Parses a URDF definition from a text string
        /// </summary>
        /// <param name="xmlData">A string containing the URDF definition</param>
        /// <returns>A description of the robot</returns>
        /// <exception cref="ArgumentException">Thrown if the argument is null</exception>
        /// <exception cref="XmlException">Thrown if the text was not a valid XML document</exception>
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

        /// <summary>
        /// Parses a URDF definition from a text file
        /// </summary>
        /// <param name="path">A path to the file containing the URDF definition</param>
        /// <returns>A description of the robot</returns>
        /// <exception cref="ArgumentException">Thrown if the argument is null</exception>
        /// <exception cref="XmlException">Thrown if the text was not a valid XML document</exception>
        /// <exception cref="FileNotFoundException">Thrown if the path was not found</exception>
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

        /// <summary>
        /// Parses a URDF definition from a stream
        /// </summary>
        /// <param name="stream">A stream containing the URDF definition</param>
        /// <returns>A description of the robot</returns>
        /// <exception cref="ArgumentException">Thrown if the argument is null</exception>
        /// <exception cref="XmlException">Thrown if the stream did not contain a valid XML document</exception>
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