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
                "H4sIAAAAAAAACq1Wy27kNhC8G/A/NLAH28HYAZKbgVzyXmCdLOLdIDeDoloSA4pU+Jjx5OtTTVKaseND" +
                "DjH8GoldrK6ubvIdfQx+b3qONHNSt8YNPswqGe9IdT4nUrQ3MStLi1nYGsd3lxfv8E2fJiNBMaqRKXLY" +
                "A0NRNLOxKtCSw+IjU/J456IPT3Mc45ffqZmDeo9ddtQB3biYWPWCR+QH6ti4kZLhXiInFfqDCrwjkyjw" +
                "EhhYKdJbLOPC2gxGVyjt5yUnDkJeVm3kK+1zgJjUMSIATJRLdO0DtrJ4t2d7XNHqyxvye2AmM4OScj1F" +
                "L8wAeFDIY8i25dvL44NJE7HSE7brDUSGihUPeWSb7ujbI1mDQFeS9pQmJLhqGncUcxd1MB0H7GCsRaBm" +
                "8KooWC3MEj+LjnSYDLZacgfICfq1zFc0go6yR8dYDGUgZuJeikk/CMdXQlGcfLb9iod8Iv1elkjxTqhA" +
                "lFf+4PDvYvSucgMfRbNyDnKtlsDSU/mxsaz8HMEUtYBof2V2GkJcXvwMRyBuKn/qul8QKP6QnN8uaeOL" +
                "9OBYZTOSnVSpzcxKBG7VUa6ZLacI31OGdUEmpiBFQBNMHnaUNR+8rg45TCzaYWvpkV4lRfKrUyiW7Jx8" +
                "gJKgwVIgP88wANKya7yJdUuFIjj64+EDtd3wTlB/+/WRFhWQoji2tFKo7SFv+Vlp5DEvlgGcCmTDE5+d" +
                "ORlcLA+J8tLMtCYnzconzk2qwvtlL6UEK9Te01ZFNEof74o/WiO0hyvErMpWyEolP6P6bVjYY1FidOZv" +
                "1VnkULnXUlWongfjzMpbLOnkQ4ndq1HqB2Br/QEKpIB4Giw/m85Yk45F7QrUsKWFmwEKS3izmHAxz2wh" +
                "6riJB9TRBzTnvCPf/clQt0WcebdHLTQE2lFvhgH1x2hY2KMIV5EGpVd4+bctfgXNSRfpqfXNZtgoo8yH" +
                "1MZAAULhZ3TRQC6jQ4ym99+j/2tLi8jQx69dD7m2Wm7FfTnRxJAFVhafeRVTa1TG7d70yUnSLWKTFNZF" +
                "GyP7YuJCaQh+Bs2TVxwMDEWDzL4SuMqpbe5fMczIC0hNfkwTTD4J1d56DDtggpIMhopRXQUzSfSpW1ee" +
                "T2ur1cZ9+FeXoqGkBG1UgJxMwOo7LdbCx8a2zGsZ8EWXbRp2POKoAinZVpHjw4q5TqVtr2swx6zZvFTG" +
                "tLzAQu8KWgooQhkAoUhPs4fLTM0h3jTbSFlXqnWiaTQaijF7NE4pcbceIFxFw0mbQ5t85/xP80KeriHl" +
                "SKgpyeMoM3adcOAAz3391Unjlu/lxTf/89flxcPjT/dwbV/vCHX8C+VHHLo97gCnuStHxWRGDORbyzig" +
                "5fSeFyhR3qbjwrG1hBQ30ih5lpkCnetcw3zOTrTmUucXABJap4YKyegsx5b2PvTGyfpBZnTBl592ZDGa" +
                "4L5cEVjnemuotlJFWXTI5UWuWiICgZ8OHvesxGO7ShQG22nFz3LNEbIq3ss2X9Qc7wAPkTBVXR/pujx7" +
                "wsd4g84UFpgpeqJr0P94xCFWjbdXwZQJLLaHDoC9kqCrm3NooX6P/nW4sVX8Cnna5L/gCkoDlrRuJxQP" +
                "5hvR7SN0xMql3jWLb0tbWCNz1ZouqIC7Vum7silAfhSxTxNM4jFlvDaoRF8uV9scKHV5MuXc/gf4IjFc" +
                "0goAAA==";
                
    }
}
