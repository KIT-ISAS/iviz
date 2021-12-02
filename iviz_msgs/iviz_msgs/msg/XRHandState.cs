/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public XRHandState()
        {
            Thumb = System.Array.Empty<GeometryMsgs.Transform>();
            Index = System.Array.Empty<GeometryMsgs.Transform>();
            Middle = System.Array.Empty<GeometryMsgs.Transform>();
            Ring = System.Array.Empty<GeometryMsgs.Transform>();
            Little = System.Array.Empty<GeometryMsgs.Transform>();
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new XRHandState(ref b);
        
        XRHandState IDeserializable<XRHandState>.RosDeserialize(ref Buffer b) => new XRHandState(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(ref Palm);
            b.SerializeStructArray(Thumb);
            b.SerializeStructArray(Index);
            b.SerializeStructArray(Middle);
            b.SerializeStructArray(Ring);
            b.SerializeStructArray(Little);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/XRHandState";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7e63e355743ca3360c1e27ce5a4ea185";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW7TQBC9+ytG6qEtSoNUEIdI3BDQA1JRKy4IVRPvxF6x3nV3x0nN1/PWTpwW2sAB" +
                "GkXy2jPvzcybmV2G4MimmzU7a4qPwkYi1cOjqCQ0orG/aVKVXl5H9mkVYkMtu+Yp49dvpHXXLA/YrTdy" +
                "d8DeWGOcHHCI1lcHzM6qAl8Ub//xr/h09WFBSc0YcxSrOKIrZW84GkJCbFiZkAnVtqolnjlZiwOIm1YM" +
                "DVbtW0lzAK9rmyA+VeIlsnM9dQlOGqgMTdN5W7IKqW3kAR5I64nRh6i27BxH+IdorM/uq8iNZHb8k9x2" +
                "4kuhi3cL+PgkZacWCfVgKKNwgpQwUtFZr6/OM6A4ut6EM7xKhVGYgqOtrDlZuWujpJwnpwVivBiLm4Mb" +
                "4giimEQnw7cbvKZTQhCkIG0oazpB5pe91sGDUGjN0fLSSSYuoQBYjzPo+PQec057QZ592NGPjPsYf0Pr" +
                "J95c01mNnrlcfeoqCAjHNoa1NXBd9gNJ6ax4xTgtI8e+yKgxZHH0PmsMJ6CGjuDJKYXSogGGNlbrImke" +
                "07EbN1it/zSNT6zBbrii5GahjDSUpNMOL0U3IlBrE34bHsykx0lQbsslZqn4IqWG+GrEO1YbfPG5AyB6" +
                "HCkGHb89S5HbZB4pkWk92H7JP2/CxTC7wWPyG2G0FUs2IQE0NgKKGuZglSgQSWZklUyAHj4oOBr+DkrB" +
                "IGU0ty3I+L4m+TMgJzKv5jPa1NB38MqDMKztsOi2xA1WWbPvxgRm2hY3I12dY5CcG3Meg6GFINmpfTqn" +
                "ixX1oaNNLgiHuL1fAto75TXsgYYwy5fLluKhoJcB2w5ZUuIKK+OT4mZD11cusL55TXfTqZ9OP56l1fsZ" +
                "e6zbnkLMKzrK96Dn+e12P6BZ5D8WtDttiuInmywSUBYHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
