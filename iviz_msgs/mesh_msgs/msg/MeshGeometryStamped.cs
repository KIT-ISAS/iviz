/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshGeometryStamped : IDeserializable<MeshGeometryStamped>, IMessage
    {
        // Mesh Geometry Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_geometry")] public MeshMsgs.MeshGeometry MeshGeometry;
    
        /// Constructor for empty message.
        public MeshGeometryStamped()
        {
            Uuid = string.Empty;
            MeshGeometry = new MeshMsgs.MeshGeometry();
        }
        
        /// Explicit constructor.
        public MeshGeometryStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshGeometry MeshGeometry)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshGeometry = MeshGeometry;
        }
        
        /// Constructor with buffer.
        internal MeshGeometryStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Uuid = b.DeserializeString();
            MeshGeometry = new MeshMsgs.MeshGeometry(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshGeometryStamped(ref b);
        
        MeshGeometryStamped IDeserializable<MeshGeometryStamped>.RosDeserialize(ref Buffer b) => new MeshGeometryStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshGeometry.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (MeshGeometry is null) throw new System.NullReferenceException(nameof(MeshGeometry));
            MeshGeometry.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Uuid);
                size += MeshGeometry.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshGeometryStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "2d62dc21b3d9b8f528e4ee7f76a77fb7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTwWrbQBC971cM+OAk4BSSkoOhN9PUh0AgvoVixtqRtLDaVXdXjtWv71spVkxIQw+1" +
                "EEgrzXvzZt7MjB4k1nQvvpEU+nyKXImKSW+bWMUvP4S1BKqHBz4H4yrqOqNVA+AYkykmhuFz9XpS6tt/" +
                "vtTD0/2S3slTM3pK7DQHjfyJNSem0kO2qWoJCyt7sQBx04qm4W/qW4nXAG5qEwl3JU4CW9tTFxGUPBW+" +
                "aTpnCk5CyaCuUzyQxhFTyyGZorMcEO+DNi6Hl4Ebyey4o/zqxBVC69USMS5K0SUDQT0YiiAcc0vXK1Kd" +
                "cen2JgPUbPPiFzhKheZPySnVnLJYObQBTkEMxyVyXI3FXYMbzRFk0ZEuhm9bHOMlIQkkSOuLmi6g/LFP" +
                "tXcgFNpzMLyzkokLdACs8wyaX54wZ9lLcuz8kX5kfMvxL7Ru4s01LWp4ZnP1savQQAS2we+NRuiuH0gK" +
                "a8QlsmYXGOOUUWNKNfuee4wgoAZH8OQYfWFggKYXk+rjuA5ubDGyZ5rGjzdB/W23jssxQh49bH7+SXvJ" +
                "gyTxk99y2DofGrbxZPk26LKrrKydznCElpxpzlTrB+qOS4SxSGxcHIxrfTTJYBR8mbckx+WFKYPAwBYK" +
                "VWk9p7uvdJje+unt9/mtetc3FLGS0rgT0RieIWIe38wZV/T5djLEvMLVHxHN1jZKBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
