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
        
        /// Constructor with buffer.
        public XRHandState(ref ReadBuffer b)
        {
            b.Deserialize(out IsValid);
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Palm);
            Thumb = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Index = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Middle = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Ring = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Little = b.DeserializeStructArray<GeometryMsgs.Transform>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new XRHandState(ref b);
        
        public XRHandState RosDeserialize(ref ReadBuffer b) => new XRHandState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(in Palm);
            b.SerializeStructArray(Thumb);
            b.SerializeStructArray(Index);
            b.SerializeStructArray(Middle);
            b.SerializeStructArray(Ring);
            b.SerializeStructArray(Little);
        }
        
        public void RosValidate()
        {
            if (Thumb is null) BuiltIns.ThrowNullReference(nameof(Thumb));
            if (Index is null) BuiltIns.ThrowNullReference(nameof(Index));
            if (Middle is null) BuiltIns.ThrowNullReference(nameof(Middle));
            if (Ring is null) BuiltIns.ThrowNullReference(nameof(Ring));
            if (Little is null) BuiltIns.ThrowNullReference(nameof(Little));
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
                "H4sIAAAAAAAAE71UwW7TQBC9+ytG6qEtSoNUEIdK3BDQA1JRKy4IRRPvxF6x3nV3x0nN1/PWTpwW2sAB" +
                "GkXy2jPvzcybmV2G4MimxZqdNcVHYSOR6uFRVBIa0dgvmlSllzeRfVqF2FDLrnnK+PUbad01ywN2643c" +
                "HbA31hgnBxyi9dUBs7OqwBfF23/8Kz5df7igpGaMOYpVHNG1sjccDSEhNqxMyIRqW9USz5ysxQHETSuG" +
                "Bqv2raQ5gDe1TRCfKvES2bmeugQnDVSGpum8LVmF1DbyAA+k9cToQ1Rbdo4j/EM01mf3VeRGMjv+SW47" +
                "8aXQ5bsL+PgkZacWCfVgKKNwgpQwUtFZr6/OM6A4utmEM7xKhVGYgqOtrDlZuWujpJwnpwvEeDEWNwc3" +
                "xBFEMYlOhm8LvKZTQhCkIG0oazpB5le91sGDUGjN0fLSSSYuoQBYjzPo+PQesx+oPfuwox8Z9zH+htZP" +
                "vLmmsxo9c7n61FUQEI5tDGtr4LrsB5LSWfGKcVpGjn2RUWPI4uh91hhOQA0dwZNTCqVFAwxtrNZF0jym" +
                "YzcWWK3/NI1PrMFuuKLkZqGMNJSk0w4vRTciUGsTfhuelMdrFQXltlxiloovUmqIr0a8Y7XBF587AKLH" +
                "kWLQ8duzFLlN5pESmdaD7Zf88yZcDrMbPCa/EUZbsWQTEkBjI6CoYQ5WiQKRZEZWyQTo4YOCo+HvoBQM" +
                "UkZz24KM72uSPwNyIvNqPqNNDX0HrzwIw9oOi25L3GCVNftuTGCmbXEz0tU5Bsm5MecxGFoIkp3ap3O6" +
                "XFEfOtrkgnCI2/sloL1TXsMeaAizfLlsKR4KehWw7ZAlJa6wMj4pbjZ0feUC65vXdDed+un041lavZ+x" +
                "x7rtKcS8oqN8D3qe3273A5pF/mNBu9OmKH4CmywSUBYHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
