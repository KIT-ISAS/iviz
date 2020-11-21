/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract (Name = "nav_msgs/OccupancyGrid")]
    public sealed class OccupancyGrid : IDeserializable<OccupancyGrid>, IMessage
    {
        // This represents a 2-D grid map, in which each cell represents the probability of
        // occupancy.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        //MetaData for the map
        [DataMember (Name = "info")] public MapMetaData Info { get; set; }
        // The map data, in row-major order, starting with (0,0).  Occupancy
        // probabilities are in the range [0,100].  Unknown is -1.
        [DataMember (Name = "data")] public sbyte[] Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public OccupancyGrid()
        {
            Header = new StdMsgs.Header();
            Info = new MapMetaData();
            Data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public OccupancyGrid(StdMsgs.Header Header, MapMetaData Info, sbyte[] Data)
        {
            this.Header = Header;
            this.Info = Info;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public OccupancyGrid(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Info = new MapMetaData(ref b);
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new OccupancyGrid(ref b);
        }
        
        OccupancyGrid IDeserializable<OccupancyGrid>.RosDeserialize(ref Buffer b)
        {
            return new OccupancyGrid(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Info.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Info is null) throw new System.NullReferenceException(nameof(Info));
            Info.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 80;
                size += Header.RosMessageLength;
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/OccupancyGrid";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3381f2d731d4076ec5c71b0759edbe4e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/bOBC9G/B/GCCHJIXtuB8oFgH2sECwbQ9FU6Q9GYExpsYSdyVSJam47q/vG8qy" +
                "nPbQHjYbOLZEzcebN29GZ/SpspGCtEGiuBSJ6cX8hspgC2q4nZF1tKusqUgYX0bq+tQ6VUJt8Bve2Nqm" +
                "PfntdHJG3piuZWf2i+lkOnkrXEigqv/Rk7P3kviGE9PWhxwDqaaT99weH1i39dkUAPNjKnCc4QS/mzf8" +
                "Dzx9QMQZxcQhWVfSzqaKLpaz5eWC6MMAQoOMGK2gxiAaSBMHdqXQajl7vlzew+uz+9f5nSOQMn8O+Nal" +
                "P1b3ObnC+fM//kPRd2+uUUGxbmIZr3quFPFdYldwQBdASTFwVdmykjCv5UFqrbtppcjgKO1biYueMIDH" +
                "pxQnget6T12EVfJkfNN0zhpOQsk28iiAuoIUplbZNF3NAQ6g2Dq13wZuJMfX/yhfOnFG6N3NNaxcFNMl" +
                "C1B7xDBBOGo/3t3AuAOFL1+oBxw/7fwc91JCCUcEaAQnRSxfVVgKluO1pnnW17hAeJAkSFREushna9zG" +
                "S0IeoJDWQ5wXgH+7T5XvW/vAwfKmRqsjGfCAsOfqdH55GlqhX5Nj54f4fcgxye/EdWNgLWteoXm1UhC7" +
                "EjzCEgp8sAVsN/scxdQWE0S13QQO0Ki69UkR5G8lG2bwy73BL8fojUUniizz6SSmoAlyX9a2eEJ1On7o" +
                "1XkyoEehVb5GTei3yTMbGk4WRPHGd6kvtOLAJkmwEQvDb/PhcTjfYNGMc55JgBb6jXNYDLTjSLXHYMAy" +
                "W+BwrQdrvTvdEVCPr7sMYNVc6bK6n062MFUFjg/VBbWAyAILY6V28f6o1Hw6mFSCiUs/2fTHQ2ofbAmN" +
                "HGpTIKtmRvgELnSpDBOZF45wPd/5ANZaSO7gpJHybs3ba1hOiISJK8VjB4R934Pb7JQTPlnDf86o+P4a" +
                "F3/fY0DPJQDtNgjU27KRmW4ZPS4Oz22vB4f7YAffBRbDrQeXR4vp5GMHdQeXI4+WT6jrH8sEnKOssQoS" +
                "W3d4ww1VoCLsx4z7UdEHkb1+RV/HSwz1cPntf6tiJPFYyunL/RG1j2vQuy9jC3SU8+v7l5UNlzu1/g5Q" +
                "HqfNUQgAAA==";
                
    }
}
