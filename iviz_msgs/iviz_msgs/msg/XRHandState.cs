/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/XRHandState")]
    public sealed class XRHandState : IDeserializable<XRHandState>, IMessage
    {
        [DataMember (Name = "is_valid")] public bool IsValid;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "palm")] public GeometryMsgs.Transform Palm;
        [DataMember (Name = "thumb")] public GeometryMsgs.Transform[] Thumb;
        [DataMember (Name = "index")] public GeometryMsgs.Transform[] Index;
        [DataMember (Name = "middle")] public GeometryMsgs.Transform[] Middle;
        [DataMember (Name = "ring")] public GeometryMsgs.Transform[] Ring;
        [DataMember (Name = "little")] public GeometryMsgs.Transform[] Little;
    
        /// <summary> Constructor for empty message. </summary>
        public XRHandState()
        {
            Thumb = System.Array.Empty<GeometryMsgs.Transform>();
            Index = System.Array.Empty<GeometryMsgs.Transform>();
            Middle = System.Array.Empty<GeometryMsgs.Transform>();
            Ring = System.Array.Empty<GeometryMsgs.Transform>();
            Little = System.Array.Empty<GeometryMsgs.Transform>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public XRHandState(bool IsValid, in StdMsgs.Header Header, in GeometryMsgs.Transform Palm, GeometryMsgs.Transform[] Thumb, GeometryMsgs.Transform[] Index, GeometryMsgs.Transform[] Middle, GeometryMsgs.Transform[] Ring, GeometryMsgs.Transform[] Little)
        {
            this.IsValid = IsValid;
            this.Header = Header;
            this.Palm = Palm;
            this.Thumb = Thumb;
            this.Index = Index;
            this.Middle = Middle;
            this.Ring = Ring;
            this.Little = Little;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal XRHandState(ref Buffer b)
        {
            IsValid = b.Deserialize<bool>();
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Palm);
            Thumb = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Index = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Middle = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Ring = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Little = b.DeserializeStructArray<GeometryMsgs.Transform>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new XRHandState(ref b);
        }
        
        XRHandState IDeserializable<XRHandState>.RosDeserialize(ref Buffer b)
        {
            return new XRHandState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(Palm);
            b.SerializeStructArray(Thumb, 0);
            b.SerializeStructArray(Index, 0);
            b.SerializeStructArray(Middle, 0);
            b.SerializeStructArray(Ring, 0);
            b.SerializeStructArray(Little, 0);
        }
        
        public void RosValidate()
        {
            if (Thumb is null) throw new System.NullReferenceException(nameof(Thumb));
            if (Index is null) throw new System.NullReferenceException(nameof(Index));
            if (Middle is null) throw new System.NullReferenceException(nameof(Middle));
            if (Ring is null) throw new System.NullReferenceException(nameof(Ring));
            if (Little is null) throw new System.NullReferenceException(nameof(Little));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 77;
                size += Header.RosMessageLength;
                size += 56 * Thumb.Length;
                size += 56 * Index.Length;
                size += 56 * Middle.Length;
                size += 56 * Ring.Length;
                size += 56 * Little.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/XRHandState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7e63e355743ca3360c1e27ce5a4ea185";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW7UMBC9R9p/GKmHtqhdpII4rMQNAT0gFbXigtBqNp5NLBw7tSe7DV/Pc9JmW2gX" +
                "DtDVSnHiec8zb954FYIjm5YbdtYUH4WNRKqHR1FJaERjv2xSlV5eRfZpHWJDLbvmqc2v30jrrlnt2bfe" +
                "yM2e/cYa42RPQLS+2rPtrCrwxax4+49/s+LT5YcFJTXjqaNcs+KALpW94WgIObFhZUIyVNuqlnjqZCMO" +
                "KG5aMTTsat9KmgN4VdsE/akSL5Gd66lLCNJAZWiaztuSVUhtIw/wQFpPjFZEtWXnOCI+RGN9Dl9HbiSz" +
                "45/kuhNfCp2/WyDGJyk7tUioB0MZhRPUxCYVnfX66iwDioOrbTjFq1Rww3Q4Osuak5WbNkrKeXJa4IwX" +
                "Y3FzcEMdwSkm0dHwbYnXdEw4BClIG8qajpD5Ra918CAU2nC0vHKSiUsoANbDDDo8vsec016QZx/u6EfG" +
                "3Rl/Q+sn3lzTaY2euVx96ioIiMA2ho01CF31A0nprHiFo1aRY19k1HhkcfA+a4wgoIaO4MkphdKiAYa2" +
                "VusiaXbq2I0lpuu/GfKJWZjd+StK7hcqSUNVOk3ySnQrAsG24Tf/wJYeK0HFLZewU/FFSg3x1Yh3rDb4" +
                "4nMHQPRYUgw6fnumOm/TeaxKps2w+UsJeR7OBwcHD/83wmguRm1CAmhsBBRlzMEqUaCTnJBVMgGS+KDg" +
                "aPg7KAV2ymhuW5DxfVnyZ0COZF7NT2hbQ+IhKtthGN5h3G2Jq6yyZteQCcx0W90J6foMdnJuzHk8DF0E" +
                "yZ3gx3M6X1MfOtrmgrCIt7dMQIenvIZp0BBO8hVzS/FQ0YuAmYcsKXGFwfFJccGh8WsXWN+8pptp1U+r" +
                "H8/U7Z3RHm24pxDzrI4KPmh7frve2TTr/KeaptUWZv4Jc8wTBSMHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
