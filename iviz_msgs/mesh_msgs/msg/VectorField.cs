/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class VectorField : IDeserializable<VectorField>, IMessage
    {
        [DataMember (Name = "positions")] public GeometryMsgs.Point[] Positions;
        [DataMember (Name = "vectors")] public GeometryMsgs.Vector3[] Vectors;
    
        /// Constructor for empty message.
        public VectorField()
        {
            Positions = System.Array.Empty<GeometryMsgs.Point>();
            Vectors = System.Array.Empty<GeometryMsgs.Vector3>();
        }
        
        /// Explicit constructor.
        public VectorField(GeometryMsgs.Point[] Positions, GeometryMsgs.Vector3[] Vectors)
        {
            this.Positions = Positions;
            this.Vectors = Vectors;
        }
        
        /// Constructor with buffer.
        internal VectorField(ref Buffer b)
        {
            Positions = b.DeserializeStructArray<GeometryMsgs.Point>();
            Vectors = b.DeserializeStructArray<GeometryMsgs.Vector3>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new VectorField(ref b);
        
        VectorField IDeserializable<VectorField>.RosDeserialize(ref Buffer b) => new VectorField(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Positions);
            b.SerializeStructArray(Vectors);
        }
        
        public void RosValidate()
        {
            if (Positions is null) throw new System.NullReferenceException(nameof(Positions));
            if (Vectors is null) throw new System.NullReferenceException(nameof(Vectors));
        }
    
        public int RosMessageLength => 8 + 24 * Positions.Length + 24 * Vectors.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/VectorField";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9da8d62df10048ede4a91e419a35679d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr2SwUrEQBBE7/MVBV4UQgQVD4Jn2YMgKF5EZDbpzA4m02G61zV+vZ1kjSwIXsScOklX" +
                "zatKAnFHmoeXToKc3nFM+vSMniVq5CQuHLx/pEo5n9vG2zSJc9d/fLnb+5sr/IDljvCwiYKKk/qYBLqh" +
                "BRTcwNud7SEmNJkI0vuKXNOy18sLvC/TsEwf/4O/b+0rQKY+k1BSMeS5x0PmEra6siACTu2AjrzFUv5W" +
                "mrCO2aQWvTRXytRwpgJRUTMJEo99df7VLCkJjWrf92bmodknaf1Umz02yTGVoSyw21Cat2IKtmgOgRLl" +
                "WCHHEOtZaQd1i9hjH66ANmfYxbadmefD7BOZSWadBCclVg0G3mI3BrIho/ZqRIy1Ie65/LodebnAdgSf" +
                "LH74H6wWER/IuhMlX5ful2/9CT2+7Q/rAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
