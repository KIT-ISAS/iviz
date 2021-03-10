/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [Preserve, DataContract (Name = "octomap_msgs/Octomap")]
    public sealed class Octomap : IDeserializable<Octomap>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Flag to denote a binary (only free/occupied) or full occupancy octree (.bt/.ot file)
        [DataMember (Name = "binary")] public bool Binary { get; set; }
        // Class id of the contained octree 
        [DataMember (Name = "id")] public string Id { get; set; }
        // Resolution (in m) of the smallest octree nodes
        [DataMember (Name = "resolution")] public double Resolution { get; set; }
        // binary serialization of octree, use conversions.h to read and write octrees
        [DataMember (Name = "data")] public sbyte[] Data { get; set; }
    
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
        public Octomap(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += 1 * Data.Length;
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
