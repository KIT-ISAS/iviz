/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/VisionInfo")]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Name of the vision pipeline. This should be a value that is meaningful to an
        //   outside user.
        [DataMember (Name = "method")] public string Method { get; set; }
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
        [DataMember (Name = "database_location")] public string DatabaseLocation { get; set; }
        // Metadata database version. This counter is incremented
        //   each time the pipeline begins using a new version of the database (useful
        //   in the case of online training or user modifications).
        //   The counter value can be monitored by listeners to ensure that the pipeline
        //   and the listener are using the same metadata.
        [DataMember (Name = "database_version")] public int DatabaseVersion { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public VisionInfo()
        {
            Header = new StdMsgs.Header();
            Method = "";
            DatabaseLocation = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public VisionInfo(StdMsgs.Header Header, string Method, string DatabaseLocation, int DatabaseVersion)
        {
            this.Header = Header;
            this.Method = Method;
            this.DatabaseLocation = DatabaseLocation;
            this.DatabaseVersion = DatabaseVersion;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public VisionInfo(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Method = b.DeserializeString();
            DatabaseLocation = b.DeserializeString();
            DatabaseVersion = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new VisionInfo(ref b);
        }
        
        VisionInfo IDeserializable<VisionInfo>.RosDeserialize(ref Buffer b)
        {
            return new VisionInfo(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Method);
            b.Serialize(DatabaseLocation);
            b.Serialize(DatabaseVersion);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Method is null) throw new System.NullReferenceException(nameof(Method));
            if (DatabaseLocation is null) throw new System.NullReferenceException(nameof(DatabaseLocation));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Method);
                size += BuiltIns.UTF8.GetByteCount(DatabaseLocation);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/VisionInfo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "eee36f8dc558754ceb4ef619179d8b34";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61WTW/jNhC9G9j/QCCHJIXjAu1tgV76vcCmXTTporeAosYSC4pU+WHH/fV9M6RkJ82h" +
                "hwZGEkucx5k3bx55pT7FcLA9JTVR1nfW70OcdLbBK92FkpVWB5uKdmq2Mznrafduc4WPehwtB6WkB1KJ" +
                "4gEYWiU7WaejmkucQyKVA975FOLTlIb05Xd6oqg/YJet6oBufcqke8ZTKuxVR9YPKlvqOXLUsT/qSFtl" +
                "s4o0RwJWTuqtLNNMxu6tqVAmTHPJFDl5XrUmX9O+BEhZnxICkIn2Wd2EiK0c3h3InRa0+vJWhQMws52Q" +
                "kva9SoEzA+BRo459ca3enh8fbR4VaTNiu96CZLBY8VBHcXmnvj0pZxHopeig8ogCF07TVqXSJRNtRxE7" +
                "WOcQaAh5VRSs5swyPTOP6jhabDWXDpAj+GuVL2gKPPIeHWExmAGZmXpupvqBc3xFlEpjKK5f8FBPUp9l" +
                "CTfvjJqDvApHj39na7Y1N+Sj1aS9B12LJLD03H5szCt/T8gUvQBpfxXyBkS82/wMRSBulD913S8IZH1w" +
                "zW+3tOWL8qBY7QqKHbX0ZiLNBLfuaN/EVnKC7lWBdJFMypGbgCEYQ1/3/BhMVchxJOZuJJmRXmet+Fen" +
                "0SzeOYcIJpEGcYPCNEEAKMst8TbVLTWa4NUf9x9V2w3vGPW3Xx/UrCNKZMXKKMU6HvyWnrVBHdPsCMBZ" +
                "IBse6+xCycjF0T6rMjcxLcVdSW5rzo0qyfvlLOUMKdTZM04nDEqfdqKPNgjt4QIx6blyitAwofvNLNxJ" +
                "mBi8/Vt3DjXU3GurKlRPe+vtkjdL0vMXiT3ooYhY8S0cwUCOiFd7R8+2s87mk7BdgRo2j3ATgGRJqYpw" +
                "ts/kQOqwkgfUIUQM57RVofuTwG6LuNBuj14YELRVvd3v0X9Yw0wBTbhOaq/NAs//tsWvoCmbXQX8/FKw" +
                "ia0sxNxsQIDQ+CmxwH3BhFijPnyP+a8jzSSDn7BMPehae7k296WjsSAFlhdfaBWuNWjrt2/q5EzpGrFS" +
                "CukmkaCIWFLaxzAhzbNWvGZbxCzD+yRwodO40r/KsKAuIDX64SZwPg41wYUoroKU2BgqRlUVxMTR52ld" +
                "8nxaRq0O7v2/phQDxS1oVoHk2AGr7kwU+VDLVvyaDV54Wd2wowFHFZLibbXydFwwF1da97pB5vCaVUti" +
                "0/wCC4MXtBzRBDGAKNSrKfR8dkkN6bbJ5lH8vaZaHc1g0NCMKWBwpMXdcoBQJQ0nbYnN+S7zP/sFP11C" +
                "5EioJfHjxB67OBxygOa+/urMcat3883//LO5f/jpPTTb1xtCNX/k+4ATt8cF4Gy6fE6MdoAb3zk68FRn" +
                "Pc2gQd7m00xpt1xM8Bm4RvGTkpqnwZuLZ55JevwiHpHVMHTM1hQ+sUwIsbeel+/Znhkdn3ZWEdT/Xu4G" +
                "ZEq9LlQ9aaEUo7EplUMEbK4ej+GObW5oNwjZfD2k6JlvN5ynTu+xxxe1uB2wQQ681PdJ3cizJ3xNt5hH" +
                "TgFOAsHeIPNPJxxdVW4HHa34LosdDAD1moOuby+QvUB77cMCXxHPe/wXWL/ick13I3rmuPpUBi0TNtf7" +
                "pWhVRsFZ9lJnu6jjaSOjJlturn5kjs+exdHwlWAsGtDLdWoZfOnGk+03m38AMb0kcMEKAAA=";
                
    }
}
