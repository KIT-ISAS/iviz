/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [Preserve, DataContract (Name = "octomap_msgs/Octomap")]
    public sealed class Octomap : IDeserializable<Octomap>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Flag to denote a binary (only free/occupied) or full occupancy octree (.bt/.ot file)
        [DataMember (Name = "binary")] public bool Binary;
        // Class id of the contained octree 
        [DataMember (Name = "id")] public string Id;
        // Resolution (in m) of the smallest octree nodes
        [DataMember (Name = "resolution")] public double Resolution;
        // binary serialization of octree, use conversions.h to read and write octrees
        [DataMember (Name = "data")] public sbyte[] Data;
    
        /// <summary> Constructor for empty message. </summary>
        public Octomap()
        {
            Id = string.Empty;
            Data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Octomap(in StdMsgs.Header Header, bool Binary, string Id, double Resolution, sbyte[] Data)
        {
            this.Header = Header;
            this.Binary = Binary;
            this.Id = Id;
            this.Resolution = Resolution;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Octomap(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Binary = b.Deserialize<bool>();
            Id = b.DeserializeString();
            Resolution = b.Deserialize<double>();
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Octomap(ref b);
        }
        
        Octomap IDeserializable<Octomap>.RosDeserialize(ref Buffer b)
        {
            return new Octomap(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Binary);
            b.Serialize(Id);
            b.Serialize(Resolution);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Id);
                size += Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "octomap_msgs/Octomap";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9a45536b45c5e409cd49f04bb2d9999f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TTW8TMRC9W8p/GCmHJqhNJYoQisQBURV6QCDaW4WiiT3ZteS1F9ubsvx63uw2BW4c" +
                "iCLt17w3894bL+kdXV1Txz35SHsfOY90SLnjek5c6LOtWcR8FHaSqZ0uxizpJnBDNZGTmKoQn6CrFAMI" +
                "gLlM1g69F7emlOkwhEDTG452xJ3S0mqzr5ebVOngg6zNPqXwRKQ93gcuhbyjdKDaCtkUK/so7gQ3pWYf" +
                "G5Ro+VcpKQzVp0graOnWJ1zpOAQp9QSLyUkxh5C4vn5F+RmmJE8yimTPwf/kiQ48M/SchjLNcZRc8KVs" +
                "WjUhwxbi6Ogxe5gx1xbjY33z8I0cVzYL8/Y//xbm092HLZXqdl1pyuUc0QIa7ipm4eyok8raXQOl1jet" +
                "5IsgRwlAcdfDyOlrHXspGwDvWw+/CzUSJcOzUeU6VWhT1w3RW4a86jv5Cw8k/GbqOVdvh8AZ9Sk7WIny" +
                "Q+ZOlB3/It8HiVbo9nqrNhaxcB4DjWCwsLFonLfXZAaYd/VSAWZ5/5gu8CgNNvC5OZLlqsPKjx4R6pxc" +
                "tujxYha3ATfcEXRxhVbTux0ey5rQxApJn2w7bcqXsbZIWVflyMh9H0SJrW6NozMFna3/YNaxtxQ5phP9" +
                "zPi7x7/QKsvMq5ouWmQWVH0ZGhiIwj6no3co3Y/z+gcvsVLw+6wHRFFzS7O8UY9RBNSUCK44Osl6BICt" +
                "9LU9HZUpjR0OzML8AgqgOGH7AwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
