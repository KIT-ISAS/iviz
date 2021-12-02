/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class VisionInfo : IDeserializable<VisionInfo>, IMessage
    {
        // Provides meta-information about a visual pipeline.
        //
        // This message serves a similar purpose to sensor_msgs/CameraInfo, but instead
        //   of being tied to hardware, it represents information about a specific
        //   computer vision pipeline. This information stays constant (or relatively
        //   constant) over time, and so it is wasteful to send it with each individual
        //   result. By listening to these messages, subscribers will receive
        //   the context in which published vision messages are to be interpreted.
        // Each vision pipeline should publish its VisionInfo messages to its own topic,
        //   in a manner similar to CameraInfo.
        // Used for sequencing
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Name of the vision pipeline. This should be a value that is meaningful to an
        //   outside user.
        [DataMember (Name = "method")] public string Method;
        // Location where the metadata database is stored. The recommended location is
        //   as an XML string on the ROS parameter server, but the exact implementation
        //   and information is left up to the user.
        // The database should store information attached to class ids. Each
        //   class id should map to an atomic, visually recognizable element. This
        //   definition is intentionally vague to allow extreme flexibility. The
        //   elements could be classes in a pixel segmentation algorithm, object classes
        //   in a detector, different people's faces in a face detection algorithm, etc.
        //   Vision pipelines report results in terms of numeric IDs, which map into
        //   this  database.
        // The information stored in this database is, again, left up to the user. The
        //   database could be as simple as a map from ID to class name, or it could
        //   include information such as object meshes or colors to use for
        //   visualization.
        [DataMember (Name = "database_location")] public string DatabaseLocation;
        // Metadata database version. This counter is incremented
        //   each time the pipeline begins using a new version of the database (useful
        //   in the case of online training or user modifications).
        //   The counter value can be monitored by listeners to ensure that the pipeline
        //   and the listener are using the same metadata.
        [DataMember (Name = "database_version")] public int DatabaseVersion;
    
        /// Constructor for empty message.
        public VisionInfo()
        {
            Method = string.Empty;
            DatabaseLocation = string.Empty;
        }
        
        /// Explicit constructor.
        public VisionInfo(in StdMsgs.Header Header, string Method, string DatabaseLocation, int DatabaseVersion)
        {
            this.Header = Header;
            this.Method = Method;
            this.DatabaseLocation = DatabaseLocation;
            this.DatabaseVersion = DatabaseVersion;
        }
        
        /// Constructor with buffer.
        internal VisionInfo(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Method = b.DeserializeString();
            DatabaseLocation = b.DeserializeString();
            DatabaseVersion = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new VisionInfo(ref b);
        
        VisionInfo IDeserializable<VisionInfo>.RosDeserialize(ref Buffer b) => new VisionInfo(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Method);
            b.Serialize(DatabaseLocation);
            b.Serialize(DatabaseVersion);
        }
        
        public void RosValidate()
        {
            if (Method is null) throw new System.NullReferenceException(nameof(Method));
            if (DatabaseLocation is null) throw new System.NullReferenceException(nameof(DatabaseLocation));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Method);
                size += BuiltIns.GetStringSize(DatabaseLocation);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/VisionInfo";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "eee36f8dc558754ceb4ef619179d8b34";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1WTW/cNhC9768YwAfbxXoLtDcDvfQ7QNwGTVL0ZlDSSGJBkSpJ7Xr76/tmSGlt14ce" +
                "GgSJLXHevHnzZqgr+hDD0XacaOJs7qzvQ5xMtsGTacKSydDRpsU4mu3Mzno+7K52V/RptBKSkhmYEscj" +
                "EAwlO1lnIs1LnENiygHvfArxcUpD+vI7M3E075BjTw2wrU+ZTQc4otBTw9YPlC13Ejia2J1M5D3ZTJHn" +
                "yIDKid6imGZubW9bRWrDNC+ZoxCXQxvxQvp5fMrmnBAAHsZnugkRmRzeHdmdK1h5d0vhCMhsJxAyvqMU" +
                "hBfwTgZF9IurxXby+GTzSGzaEdk6C30hoMKhiMXlA317JmcR57XiQHlEdaueaU9paVIbbcMRCaxzCGwZ" +
                "rBQEh4VX5ieRkE6jRaJ5aYA4Qrta9gpG0FBSNIzDkAVCZu7QRfpBCL4SidIYFtetcCgm0e96RNp2AQWg" +
                "vAonjx9n2+6VGdgYmoz3kGr1Ak5e+n7Y4dznBJZoAuT6a2HfQoPdz/ABgkb9Tw79ghgxhRT7diMrU9QF" +
                "jxq3oMrRaEsmNiJsbYrxxWBLTjA6LXDrYZdyFOlh+jF0ku99aIsnTiOLYEgrE9GZbEj+aQwaJFlziJAP" +
                "FFiaEqYJPUc9bo23SdMZCO/pj4f3VFPhlYD+9utHmk1EdWJRnZxYpkHe8pNpUcI0OwZuVsQCJ8Z65lww" +
                "cdxnWuZqn1qXTCZfCFeNlPTLyckZ3S+T1jqTMBddOqgliu/rsxVhMpoIJZkcJvS7rgV3VhUGb/82jUMB" +
                "hXhpkSJ13FtvV9JiQS+/aOjRDNI24DoXTqg+R4RT7/jJNtbZfFahFaciy7jWtitHeFFdN9sndtBz2HQD" +
                "6BAiJnHaU2j+ZAhbIy5e7dCFFuLsqbN9j8ZjC8wcIP91ot60K7r8WA+/QubciupUp2QzaZKdFWKuI684" +
                "6PiEmenJLxgI29K77zHrZX5FYIgT6oRDqq2La1dfbi6xoYLK2WcOxXoajPX7N/2xqbkFbGrCsBhZVK7W" +
                "VT59DBM4XjziYVuIGWXHaWBVsnVL94rfgpoAVIXH3sCGk8g2uIClBkgQki2gEMVM8JAEb9O5knxch0sG" +
                "9eFfU4kJEunrWgAvWXPFbK34Cb8WorqRZYWrINvKa3jATQQ+ktOQ59MKuS6gLdUNSGOtrAbSTSzPcS54" +
                "BcsR4uu8R5WcpgBr2cI/3RavSDdXnmV1tZgsNGEKGBXtbLPeEFzUwjW6xLrinrPftoM8XCN055d65HGS" +
                "Xbpus8MONvv6q4u4tdbdN//zn93Dx5/uYdSuXP5lw4PuR9ynHS73y36Vu2C0AxbvnWNcvXIvTzNE0Lf5" +
                "PHMqMyA9TTRIibo/IHDZYFjDixeRWfv7Ih6RZUOYmG27yJ3UhhA76+V4L6tY0PG33kcMz9/rzc/tUr4F" +
                "ipWMKoqB2C1FQwTsrj6dAr6bMg/1+0CTb3cRP8mXi/A06R45vijFHYANcbA6fZfoRp894td0iyn0oIDl" +
                "AbPegPmHM+6o4rWjiVbXrPgcCgD1WoKub58hC+17jKrHF1iBL4iXHP8FVlAKrtR0N6JnsNuAwR4gIA7O" +
                "5cNRnapz4KxsT2ebaOJ5p2OmKXdXP4rGl0Ul0dgmobVoQKcfS+vEazceLW7kfwCw+3+3mgoAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
