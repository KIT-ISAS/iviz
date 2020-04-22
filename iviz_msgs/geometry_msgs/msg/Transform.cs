
namespace Iviz.Msgs.geometry_msgs
{
    public struct Transform : IMessage
    {
        // This represents the transform between two coordinate frames in free space.
        
        public Vector3 translation;
        public Quaternion rotation;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Transform";
    
        public IMessage Create() => new Transform();
    
        public int GetLength() => 56;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ac9eff44abf714214112b05d54a3cf9b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1Sy0rEQBC8B/YfCvaiECKoeBA8yx4ERfEqs0knO5hMx55eY/x6O5s18QVexJwqk65K" +
                "V9UscbfxEUKtUKSgEbohqLgQS5YGa9KOKEA7Rs4shQ9OCaW4hiJ8MESE2LqcsiS5p1xZTkZ+7dRzSG62" +
                "RpBgEMI6ni2Siz9+FsnV7eU5KuKGVPqHJlbxaL/OIll+c+nwvPv4xQJsdKWwWQ51j4ZcUCjPTCMWXoxq" +
                "NjJTJSHLiVJ4RcEWSWA1jcY9miSFaFkyXNuamPsYy3BslAPKqixFt7GId1M+VDZoChUFEp9DfOWLuZCJ" +
                "7LB3l0LLY3S+rsedx59ZiybyHvhhhlWJnrfoBkMGBIVT24it4Wkvt66HfTnFdlh8J/E50Wv2xrfqo6vI" +
                "sotKrrDiy5qdnp3iZUL9hF7/qe35ov1YeACLNzgm+Kn24e1pvqZDzr95mlBnl/kNSp54Z0UDAAA=";
                
    }
}
