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
                "H4sIAAAAAAAAE61WTW/rNhC861cQ8CFJ4bhAewvQS78f8NI+NOlDbwElrSQWFKnyw4776zu7pOQkzaGH" +
                "BkYSi9zh7uzsUDv1Kfij6SmqmZK+NW7wYdbJeKd063NSWh1NzNqqxSxkjaNDs2t26nEyHBKjHklFCkcg" +
                "aBXNbKwOaslh8ZFU8lhz0YenOY7xy+/0TEF/wBl71QLbuJhI94BTyg+qJeNGlQz1HDjp0J90oL0ySQVa" +
                "AgEqRfVeinGhzgymE6TOz0tOFDhx3rQlXpJ+GR+TPkcEIA/tkrr2ASdZrB3JnitYWbtR/gjIZGYkpF2v" +
                "oue8gHfSKGLIthbb8+OTSZMi3U04rTfgFwQKHIrINh3Ut2dlDeKcVOxVmlDdymfcq5jb2AXTUsABxloE" +
                "doSsBASbOa9Ez0yhOk0GBy25BeIE7mrZK5gCh3xES9gMWkBkoh5dVD9wgm9IUnHy2fYrHIqJ6rNs4bZd" +
                "QJOXJX9y+Hcx3V4yQzZazdo5ULVqATsvfT802Pd7RJZoAuj6K5PrwEHzM3SAoEn+8KZfEMOi4GLfb2TN" +
                "FHVBo9pmVDlpaclMmomtTdGuCCynCKGrDLUempgCUw/RT77n8z76rmjiNBETNpFMRK+TVvyr1WgQn5p8" +
                "AH1Igbgpfp7Rc9Rj13gT5TgN4p364/6jqkdhiUF/+/VBLTqgOpaoTE4o08Cr9Kw7lDAvloCbBLHAsbBe" +
                "KBeZWBqSykuVT61rJ4ltCVeOJOnXk5MSul8mrbM6Yi76eBBJFN3XZyvCrJfCJSL9jH5XW7BnYWF05m/d" +
                "WhRQEi8tEqSeBuPMmjRL0PEXCT3qMYs48c2fUH0KCFeDpWfTGmvSWYgWnIrM41rbLjlSLKpbzDNZ8Dlu" +
                "vAF09AGTOO+Vb/8kEFsjLlrt0YUO5OxVb4YBjYcLLORB/1VUg+5WdP63bn6DTKk7CN7n1yKN7Fk+pDry" +
                "goOOz5FF7TIGwnTqw/eY9TK/TDDI8XXCQdXWxbWrr52LZSigvPeFQmFPozZu/64+Nja3gI1NCDaK8ES6" +
                "ks8Q/IwcLxpxmu0PkwuPk8DKZGdz/ya/jJoAVImHb8DhOLLz1gfxDyTELiAQRUzQEAdv07km+bQOFw/q" +
                "/b+mEhPE1FdbQF5sc0VsXRDRUElUHJktXAjZLK+lETcR8uEztXJ0WiFXA9qOukbSsJVVQOLE/Bz7vBOw" +
                "FEC+zHsQytXse76aJP94U7TyKA5e8izW1WGy0ITZY1Sks+16Q1BhC9doDtXiXma/uQM/XCPE80s9/Diy" +
                "l65udmggs6+/upBba22++Z9/mvuHn+4g1L5c/sXhke4D7tMel/vFX/kumMwI4721dOQxTnpeQIKspvNC" +
                "8bC+c+AzconiHzlWB4MNZ8ckk/T3VTwii0PokEyX+U7qvA+9cbx9YCtmdHzqfUTQ/J3c/NTl8i5QpKSF" +
                "UQxEkwuHCGh2jyd/y7Y21vcDOXy7i+iZ31w4Tx3vcMYXpbgDsEEOrNP1UV3Lsyd8jTeYQk4B5gGxXiPz" +
                "T2fcUUVrRx2M2CzrHAwA9YqDrm5eIDuBdtr5Fb4gXs74L7Buw+Wabif0zHL1MY9ahmspL46iVJkDa9g9" +
                "rWmDDudGxkyObHY/MscXo+JouInvDBrQy8vSOvHSjSeDG/kfsPt/t5oKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
