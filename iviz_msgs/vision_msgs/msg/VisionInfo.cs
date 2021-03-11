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
            Method = string.Empty;
            DatabaseLocation = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public VisionInfo(in StdMsgs.Header Header, string Method, string DatabaseLocation, int DatabaseVersion)
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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACq1WTW/cNhC9C/B/GMAH28V6C7Q3A730O0DcBk1S9GZQ0khiQZEqSe16++v7ZkjtOqkP" +
                "PTQIElvivHnz5s1Q1/QuhoPtOdHM2dxbP4Q4m2yDJ9OGNZOhg02rcbTYhZ31vG+um2v6MFkJScmMTInj" +
                "AQiGkp2tM5GWNS4hMeWAdz6F+DSnMX35nZk5mjfIsaMW2NanzKYHHFEYqGXrR8qWewmcTOyPJvKObKbI" +
                "S2RA5USvUUwLd3awnSJ1YV7WzFGIy6Ez8UL6ZXzK5pQQAB7GZ7oNEZkc3h3YnSpYeXdH4QDIbGcQMr6n" +
                "FIQX8I4GRQyrq8X28vho80RsugnZegt9IaDCoYjV5T19eyJnEee14kB5QnWbnmlHaW1TF23LEQmscwjs" +
                "GKwUBIeFV+ZnkZCOk0WiZW2BOEG7WvYGRtBQUrSMw5AFQmbu0UX6QQh+JhKlKayu3+BQTKLf9Yi07QIK" +
                "QHkVjh4/LrbbKTOwMTQb7yHV5gWcvPR93+DcxwSWaALk+mtl30GD5mf4AEGT/ieHfkGMmEKKfb2RlSnq" +
                "gkeNW1HlZLQlMxsRtjbF+GKwNScYnVa4dd+kHEV6mH4KveR7G7riiePEIhjSykT0JhuSf1qDBknWHCLk" +
                "AwWWpoR5Rs9Rj9vibdJ0BsJ7+uPxLdVUeCWgv/36nhYTUZ1YVCcnlmmQt/xsOpQwL46BmxWxwImxXjgX" +
                "TBwPmdal2qfWJZPJF8JVIyX96eTkjO6XSeucSZiLPu3VEsX39dmGMBtNhJJMDjP6XdeCO6kKo7d/m9ah" +
                "gEK8tEiReh6stxtpsaCXXzT0YEZpG3CdC0dUnyPCaXD8bFvrbD6p0IpTkWVca9uVI7yorlvsMzvoOZ51" +
                "A+gYIiZx3lFo/2QIWyMuXu3RhQ7i7Ki3w4DGYwssHCD/TaLBdBu6/FgPf4bMuRPVqU7J2aRJdlaIuY68" +
                "4qDjM2ZmIL9iIGxHb77HrJf5FYEhTqgTDqnOXdy6+unmEhsqqJx94VCsp9FYv3vVH2c1zwFnNWFYjCwq" +
                "V+sqnyGGGRwvHvGwLcSMsuM0sCrZubX/jN+KmgBUhcfewIaTyC64gKUGSBCSLaAQxUzwkASfp3Mj+bQN" +
                "lwzq47+mEhMk0te1AF6y5orZOvETfi1EdSPLCldBziuv5RE3EfhITkOejxvktoDOqW5BGmtlM5BuYnmO" +
                "c8ErWI4QX+c9quQ0B1jLFv7prnhFurnxLKurw2ShCXPAqGhn2+2G4KIWrtE11hX3kv15O8jDLUJ3fqlH" +
                "HifZpds22zew2ddfXcSttV413/zPf66ax/c/PcCqfbn+y46/AuP3uFJ73O+XFSvXwWRH7N57x7h95Wqe" +
                "F+igb/Np4VTGQNqaaJQqdYVA47LEsIlXLzqztviTeESWJWFitt0q11IXQuytl+ODbGNBx996JTFs/6CX" +
                "P3dr+RwobjIqKmaiWYuMCGiuPxwDPp0yj/UTQZOfryN+lo8X4WnSA3J8UYrbAxvqYHv6PtGtPnvCr+kO" +
                "g+hBAfsDfr0F83cnXFPFbgcTrW5asToUAOqNBN3cvUAW2g+YVo+PsAJfEC85/gusoBRcqel+Qs/guBGz" +
                "PUJAHFzKt6OaVUfBWVmgzrbRxFOjk6Ypm+sfRePLrpJoLJTQWTSg1++lbei1G0+2b66afwDXc6rrngoA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
