/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract]
    public sealed class Octomap : IDeserializableRos1<Octomap>, IDeserializableRos2<Octomap>, IMessageRos1, IMessageRos2
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
    
        /// Constructor for empty message.
        public Octomap()
        {
            Id = "";
            Data = System.Array.Empty<sbyte>();
        }
        
        /// Constructor with buffer.
        public Octomap(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Binary);
            b.DeserializeString(out Id);
            b.Deserialize(out Resolution);
            b.DeserializeStructArray(out Data);
        }
        
        /// Constructor with buffer.
        public Octomap(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Binary);
            b.DeserializeString(out Id);
            b.Deserialize(out Resolution);
            b.DeserializeStructArray(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Octomap(ref b);
        
        public Octomap RosDeserialize(ref ReadBuffer b) => new Octomap(ref b);
        
        public Octomap RosDeserialize(ref ReadBuffer2 b) => new Octomap(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Binary);
            b.Serialize(Id);
            b.Serialize(Resolution);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Binary);
            b.Serialize(Id);
            b.Serialize(Resolution);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += Data.Length;
                return size;
            }
        }
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Binary);
            WriteBuffer2.AddLength(ref c, Id);
            WriteBuffer2.AddLength(ref c, Resolution);
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "octomap_msgs/Octomap";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9a45536b45c5e409cd49f04bb2d9999f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TTW8TMRC9+1eMlEMT1KQSRQhF4oCoCj0gEO2tQtWsPdm15LUX25uy/Hre7DYFbhyI" +
                "Iu3XvDfz3huv6B1dXlHPA/lIjY+cJzqk3HM9Jy702dYsYj4KO8nUzRdjVnQduKWayElMVYhP0HWKAQTA" +
                "XCRrx8GL21DKdBhDoPkNRzvhTmlpvWvqxS5VOvggG9OkFJ6ItMf7wKWQd5QOVDshm2JlH8Wd4KbU7GOL" +
                "Ei3/KiWFsfoUaQ0t/eaEKz2HIKWeYDE5KeYQEtfXryg/w5TkSUaR7Dn4nzzTgWeBntNY5jmOkgu+lF2n" +
                "JmTYQhwdPWYPM5baYnysb+6/kePKxrz9zz/z6fbDnkp1D31py8USEATcVgzC2VEvlbW1pkmdbzvJ2yBH" +
                "CQBxP8DF+WudBik7AO86D7MLtRIlw7BJtTqVZ1Pfj9Fbhrbqe/kLDyTMZho4V2/HwBn1KTv4iPJD5l6U" +
                "Hf8i30eJVujmaq8eFrGwHQNNYLDwsGiWN1dkRjh3+VIBZnX3mLZ4lBbr99wcsXLVYeXHgPx0Ti579Hix" +
                "iNuBG+YIurhC6/ndAx7LhtAEI8iQbDevyZepdohY9+TICL0JosRWV8bRmYLONn8wx5k6ckwn+oXxd49/" +
                "oY3PvKpp2yGzoOrL2MJAFA45Hb1DaTMtux+8xErBN1lPh6KWlmZ1rR6jCKg5EVxxbpL1CAAr6Wt3Oidz" +
                "Gg96Wn4BbmC2U/cDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
