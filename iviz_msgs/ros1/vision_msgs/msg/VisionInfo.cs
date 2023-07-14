/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class VisionInfo : IHasSerializer<VisionInfo>, IMessage
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
        // The database should store information attached to numeric ids. Each
        //   numeric id should map to an atomic, visually recognizable element. This
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
    
        public VisionInfo()
        {
            Method = "";
            DatabaseLocation = "";
        }
        
        public VisionInfo(in StdMsgs.Header Header, string Method, string DatabaseLocation, int DatabaseVersion)
        {
            this.Header = Header;
            this.Method = Method;
            this.DatabaseLocation = DatabaseLocation;
            this.DatabaseVersion = DatabaseVersion;
        }
        
        public VisionInfo(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Method = b.DeserializeString();
            DatabaseLocation = b.DeserializeString();
            b.Deserialize(out DatabaseVersion);
        }
        
        public VisionInfo(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            Method = b.DeserializeString();
            b.Align4();
            DatabaseLocation = b.DeserializeString();
            b.Align4();
            b.Deserialize(out DatabaseVersion);
        }
        
        public VisionInfo RosDeserialize(ref ReadBuffer b) => new VisionInfo(ref b);
        
        public VisionInfo RosDeserialize(ref ReadBuffer2 b) => new VisionInfo(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Method);
            b.Serialize(DatabaseLocation);
            b.Serialize(DatabaseVersion);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Method);
            b.Align4();
            b.Serialize(DatabaseLocation);
            b.Align4();
            b.Serialize(DatabaseVersion);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Method, nameof(Method));
            BuiltIns.ThrowIfNull(DatabaseLocation, nameof(DatabaseLocation));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Method);
                size += WriteBuffer.GetStringSize(DatabaseLocation);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Method);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, DatabaseLocation);
            size = WriteBuffer2.Align4(size);
            size += 4; // DatabaseVersion
            return size;
        }
    
        public const string MessageType = "vision_msgs/VisionInfo";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "eee36f8dc558754ceb4ef619179d8b34";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61WTW/jNhC9+1cM4EOSwnHR9rZAL/1eYNMudtNFgaIIKGkssaBIlR923F/fN0NJTtIc" +
                "emhgJLHIeZx58+ZRW3ofw9F2nGjkbG6tP4Q4mmyDJ9OEksnQ0aZiHE12Ymc97zfbzZbuByshKZmeKXE8" +
                "AsFQsqN1JtJU4hQSUw5Y8ynEhzH16fNvzcjRvMUZO2qAbX3KbDrAEYUDNWx9T9lyJ4GDid3JRN6RzRR5" +
                "igyonOi1FNPErT3YVpHaME4lc5TEZdOaeE36aXzK5pwQgDyMz3QdIk5yWDuyO89gde2GwhGQ2Y5IyPiO" +
                "UpC8gHcyKOJQ3FxsJ49PNg/Eph1wWmfBLwhUOBRRXN7TN2dyFnFeKw6UB1S38Jl2lEqT2mgbjjjAOofA" +
                "lpGVgmCz5JX5USik02Bx0FQaIA7gbi57ASNwKEc0jM2gBURm7tBF+l4SfEESpSEU1y1wKCbRJ90ibbuA" +
                "5qBL4eTx72TbnWaGbAyNxntQtWgBOy9932+w79eELNEE0PVXYd+Cg81P0AGCBv0jm35GjIhCin29kXOm" +
                "qAsaNa6gysFoS0Y2QuzcFOOrwEpOEDoVqHW/STkK9RD9EDo5711oqyZOAwthA+tEdCYbkl+NQYPk1Bwi" +
                "6EMKLE0J44ieox63xNukxxkQ7+m3u3c0H4UlAf3wy0eaTER1IlGdnFinQVb50bQoYZwcAzcrYoUTYT1R" +
                "LjJxfMhUplk+c11bTWxNeOZIk34+OTmj+3XSfEF7bEu2S3sVhZ54ebqgjGaqfCI6jOj5bA3urEz03v5t" +
                "GociavK1TYrV8cF6uyQuMvTyRUOPpi8qUHwLJzCQI8Lp4PjRNtbZfFayFWdGlpGdW986kxKnqrzJPrID" +
                "p/3KHUD7EDGN445C8yeD3DniotcOnWhB0I46ezig+XCCiQNacJXoYNoFXf6dN79A5tzuFe/Tc6Em8a0Q" +
                "8zz2ioOuj0mEvfD79jvMe51hIRjkhHnKQdXayaWzz91LpKigsveJSmFRvbF+96pGVjbXgJVNiDap+FS+" +
                "ms8hhhE5CoIyR96IBWJ64XMaODPZutK9yK+gJgDNxMM74HIS2QYXonoIEhInUIgqJmhIgtcJXZJ8WAZM" +
                "hvXuX5OJKRLqZ2tAXmJ1VWxtVNFwTVRdWWxcCVltr+EetxHykTMNeT4tkIsJrUddI2lYyyIgdWN5jn3B" +
                "K1iOIF9nPirlNIZOrifNP91Urdyri9c8q321mCw0YQwYFe1ss9wSXNnCVVribHNPs18dQh4uEer7tR55" +
                "nMRPF0fbbyCzr768kDvXuvn6f/7Z3H388Q2E2tUXgOrySPcj7tQOF/zFY+U+GGwP8711fJQxzmacQIKu" +
                "5vPEab+8d+DTS4nqHyVVFxMrLl5IZu3vs3hEVocwMdu2yL3UhhA762X7QexY0PGZ7ySG5t/o7c9tqe8D" +
                "VUpGGcVAbErlEAG0pd8/hPTFH5vt/Sncir/188uCZrFeTPworzGSsElvcNhntco9DgFL8FDfJbrWZw/4" +
                "mm4wjpILXASqvUYJ78+4sKrojiZa9VsRPKgA6pUEXd08QfYK7Y0PC3xFvJzxX2D9iis13Q5onhMaUumN" +
                "TtlU3yJVsjoQzoqNOttEE88bnTc9crP9Qci+OJZEw1ZCa9GJTt+cltHXtjxYXM//ADX5t7qnCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<VisionInfo> CreateSerializer() => new Serializer();
        public Deserializer<VisionInfo> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<VisionInfo>
        {
            public override void RosSerialize(VisionInfo msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(VisionInfo msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(VisionInfo msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(VisionInfo msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(VisionInfo msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<VisionInfo>
        {
            public override void RosDeserialize(ref ReadBuffer b, out VisionInfo msg) => msg = new VisionInfo(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out VisionInfo msg) => msg = new VisionInfo(ref b);
        }
    }
}
