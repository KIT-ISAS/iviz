/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Include")]
    public sealed class Include : IDeserializable<Include>, IMessage
    {
        // Reference to an external asset
        [DataMember (Name = "uri")] public string Uri; // Uri of the asset
        [DataMember (Name = "pose")] public Matrix4 Pose; // Pose of the asset
        [DataMember (Name = "material")] public Material Material;
        [DataMember (Name = "package")] public string Package; // If uri has a model scheme, this indicates the package to search
    
        /// <summary> Constructor for empty message. </summary>
        public Include()
        {
            Uri = string.Empty;
            Pose = new Matrix4();
            Material = new Material();
            Package = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Include(string Uri, Matrix4 Pose, Material Material, string Package)
        {
            this.Uri = Uri;
            this.Pose = Pose;
            this.Material = Material;
            this.Package = Package;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Include(ref Buffer b)
        {
            Uri = b.DeserializeString();
            Pose = new Matrix4(ref b);
            Material = new Material(ref b);
            Package = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Include(ref b);
        }
        
        Include IDeserializable<Include>.RosDeserialize(ref Buffer b)
        {
            return new Include(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uri);
            Pose.RosSerialize(ref b);
            Material.RosSerialize(ref b);
            b.Serialize(Package);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException(nameof(Uri));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
            if (Material is null) throw new System.NullReferenceException(nameof(Material));
            Material.RosValidate();
            if (Package is null) throw new System.NullReferenceException(nameof(Package));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 72;
                size += BuiltIns.GetStringSize(Uri);
                size += Material.RosMessageLength;
                size += BuiltIns.GetStringSize(Package);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Include";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "89c6a6240009410a08d4bbcad467b364";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UyW7bMBA9R4D/gUCuBdo4SZsWyEGWaJuoNmhJagSBwMiUzVYbSMpJ+vUdbQ6NXIvo" +
                "IJJvhsM3j5w5RyHLmWBVxpCqEa0Qe1FMVLRAVEqmDEMqwasdagVHZ2fnKIGxzpHas9HBpeDwcoWaWrLO" +
                "IYDxnQcTHCKW42SK2dDsD931u0jen7CnElFU1ltWIJntWck+QSAuEa+2PIPtso87buwYS0ZFtjeQMTNu" +
                "//M3M9xo9QPxA/+blnInP4+pzoy8qKm6nD9cfH1EJTpHon6G5H7X4qNo9DLOjJZX6gYtHOzZqY2XZuLE" +
                "6BZ9OcFN2yYxucNguDjeZkVLZlh1UYvLOaLlE2eVOq63PM9b+WZnJZeSH9iUNqpBf65ej+untmxSmdEC" +
                "Qh9BuecVr5iU75EUWLBqp/ZHk2B5wTIFOULYgf1Twapt2j0FI4Y32Qr28IjUMJMfIfOY/qSyGMfdxG8c" +
                "6UdwGRWYuMSbAKee72HttnvMJstlEg13rcFRgK3EMUPA5zpuuguCve7NXOowdkkUDU/mSsfXmKzWnff1" +
                "KY/QNZ0I4K8nZ66JRzwcdYZvusEPTIvEG4BvTqlHgWNa2B0IfddtTneuawZdXif5hnjpYCsmvteZTnJO" +
                "vJ+ef9/jc2M0QIiAeKt0Gfpumtxp6k2WKFjjUNdvMlgbh3g21iWcTAv/l6bghEIynq7ghL/xup5o+UHq" +
                "QumSwNlolACF2tWoABAlizg0rVhjAahN7oiNNQ6dp+v78XqMMHHocLLycN8VdAb3oRmk3U87v8csx3R7" +
                "2XXQJWHo60r0qI0t0+lJvPV3KHFwgAKH/s1eRm/12rBxWtKm6ZrG4NQe0sHv2Fj6HpDTTNVT+dUNE1Tx" +
                "uhrXz4I2fZdI23fIAUrzH8C1RtbhBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
