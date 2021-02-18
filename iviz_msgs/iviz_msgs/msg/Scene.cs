/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Scene")]
    public sealed class Scene : IDeserializable<Scene>, IMessage
    {
        // A complete scene 
        [DataMember (Name = "name")] public string Name { get; set; } // Name of the scene
        [DataMember (Name = "filename")] public string Filename { get; set; } // Original filename
        [DataMember (Name = "includes")] public Include[] Includes { get; set; } // List of models to be included
        [DataMember (Name = "lights")] public Light[] Lights { get; set; } // List of lights
    
        /// <summary> Constructor for empty message. </summary>
        public Scene()
        {
            Name = string.Empty;
            Filename = string.Empty;
            Includes = System.Array.Empty<Include>();
            Lights = System.Array.Empty<Light>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Scene(string Name, string Filename, Include[] Includes, Light[] Lights)
        {
            this.Name = Name;
            this.Filename = Filename;
            this.Includes = Includes;
            this.Lights = Lights;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Scene(ref Buffer b)
        {
            Name = b.DeserializeString();
            Filename = b.DeserializeString();
            Includes = b.DeserializeArray<Include>();
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new Include(ref b);
            }
            Lights = b.DeserializeArray<Light>();
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new Light(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Scene(ref b);
        }
        
        Scene IDeserializable<Scene>.RosDeserialize(ref Buffer b)
        {
            return new Scene(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.SerializeArray(Includes, 0);
            b.SerializeArray(Lights, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Filename is null) throw new System.NullReferenceException(nameof(Filename));
            if (Includes is null) throw new System.NullReferenceException(nameof(Includes));
            for (int i = 0; i < Includes.Length; i++)
            {
                if (Includes[i] is null) throw new System.NullReferenceException($"{nameof(Includes)}[{i}]");
                Includes[i].RosValidate();
            }
            if (Lights is null) throw new System.NullReferenceException(nameof(Lights));
            for (int i = 0; i < Lights.Length; i++)
            {
                if (Lights[i] is null) throw new System.NullReferenceException($"{nameof(Lights)}[{i}]");
                Lights[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Filename);
                foreach (var i in Includes)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Lights)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Scene";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5e11da295d41bbfbd413d6274556c4a9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71V227bOBB9jgD/A4G8LtDNpW12gTwoEh0TqxskOW1QFAItUzZ3JVEQqSTt1+9QN1Nt" +
                "H5vYgEWfMxydOSSH58hGuaiakimGZM5qhixLqpbXB1TTip2dnaMAnkgUSB3HkCmg4CWbgsKWH3hNyxm0" +
                "SJ2X3Z59+Yr4MJIQ5nGpdK5K7FkpkRJoxyZ+b3n8cFQwodRPeWbED4hl3f7mj+Un938j/sS/Z5U8yHej" +
                "aOscxaxgLatzpkXSGrEXxVpdIJWSqdmkruVIC93Cc/RoCPApBLxco0bI3qAInj9FsJZDxmocTDkbmv9H" +
                "D/0sUvRvOFKJ6GAaLMGRVewPSMQlWLfnOUyXfd5xolYsGW3zo4Ve37KxUKsoBVVXl18uPnxFFTpHrXiG" +
                "yv4V7dtoGBzseK1u0J2HAzdz8dreeim6RX+uFoTtuiQlDxiYi5W1Mvf7ynJEKdqrS0SrHWe1OgF7XhSd" +
                "NCJYxaXkT4CMpSMBC8DVtxOw66omkzktIf8JlUde85pJ+QsoAzGsPqjjiWtZUbJcQbU69VDIDg7ZPtMb" +
                "YmWlsDW7Vh80NYwg76s7PpowymknXYdZ4DSgbyBmdGB8Y/oY4SwIA2yufA+6ZL3eJuO6G3gSYWfr2TEQ" +
                "lwvC9u8IDvQWulrg2CdJMuyg6wWxweR+o+Pfr5ZqYt/2EsA/LF+8IQEJcKKZjwsmjGyHpI+A3/xQQhJ5" +
                "toP9QdZfC9LTL/ftSBe4rDzGaw87KQkDzS2r3wb/BOGnnrjUx2GgIE1EgvtsHYd+tn0wvZyoJNrgeOHm" +
                "xDiPHglcvDB04u7Cz6afEwxVBQs/J+Ik7/1JXRhlPpxtEnmPpjKA4XCbigBJtndpbDupKQZglzwQF5tS" +
                "dKwfhulmTHJtEuQ+wO5IGEI+xXaU6R9TRg86nu1HppQe9UkchwtfetjFju0NWlana0B3AYiBHgB9nr1M" +
                "M9S3hk3jijZN31yGuO4pG0PnFtR3ioLmSsynVDSspYqLegKeW9r03STrfoae3uAE9zf/bGoUkmDRt10S" +
                "D7u3N2l2NInCcVV/aOKmTzshSpRTqTJ5pHvxLH/R0udWS+sD/H1g2q2rQl/efPBphva81c1YY9MsXtes" +
                "zWBqad4FnTqB+vvqHk4SZwnGLjDupO8g5X+cwfcW9wkAAA==";
                
    }
}
