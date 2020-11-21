/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract (Name = "octomap_msgs/Octomap")]
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
            Header = new StdMsgs.Header();
            Id = "";
            Data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Octomap(StdMsgs.Header Header, bool Binary, string Id, double Resolution, sbyte[] Data)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACq1TwWrbQBC9C/QPAznELokNTSnF0ENpSJtDaWlyC8WMd8fSwmpX3V05Vb++byTbobce" +
                "KhDSSvPezLw3c0Ef6OaWOu7JBdq5wGmkfUwdlyviTF9NSSJ19VnYSqJ2etRVXV3QneeGSiQrIRYhPqEX" +
                "MXhwALaOxgy9E7ukmGg/eE/TFw5mxJsy02K1K+tVLLR3XpZ1tYvRH5nmNB8950zOUtxTaYVMDIVdEHti" +
                "qKtckgsNYmbEd8nRD8XFQAs01S1P0Nyx95LLCRmilVxXex+5vH1D6YybeY79ZEmOvfvNEyOoZvQVDXmq" +
                "5iAp409etapGgkLEwdJzclBljkUSF8q7px9kubCyv//PV119efi0oVzststNXs9+aRcPBdVwstRJYU2v" +
                "9lLrmlbStZeDeKC46yHo9LeMveSVIh9bB+EzNRIkQblRO7bapIldNwRnGB0W18lfBAqF7Ew9p+LM4DkB" +
                "EJOFnIjfJ+5k4tc7y89BghG6v92omFkMHEBRIzgMxMzq7P0tggdIePNaEQA+PsdrnKXBUJ4rgMlctGL5" +
                "1cNMLZbzRtO8mntcgR4iCRLZTIvp2xbHvCTkQRXSR9NOU/NtLC3s1rE5MAZg50WZjU6QpUsFXWJcX6i1" +
                "9A0FDvHEP1O+JPkXXmU5Emtb1y3M8ypBHhroiMg+xYOziN2N8z54J6GQd7s07YzC5qQguVOxEQbc5A2e" +
                "2KZoHJzAiLrSnrdn8mU779AfrWmrqBYEAAA=";
                
    }
}
