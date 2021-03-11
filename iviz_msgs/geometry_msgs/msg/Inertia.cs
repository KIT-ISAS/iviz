/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Inertia")]
    public sealed class Inertia : IDeserializable<Inertia>, IMessage
    {
        // Mass [kg]
        [DataMember (Name = "m")] public double M { get; set; }
        // Center of mass [m]
        [DataMember (Name = "com")] public GeometryMsgs.Vector3 Com { get; set; }
        // Inertia Tensor [kg-m^2]
        //     | ixx ixy ixz |
        // I = | ixy iyy iyz |
        //     | ixz iyz izz |
        [DataMember (Name = "ixx")] public double Ixx { get; set; }
        [DataMember (Name = "ixy")] public double Ixy { get; set; }
        [DataMember (Name = "ixz")] public double Ixz { get; set; }
        [DataMember (Name = "iyy")] public double Iyy { get; set; }
        [DataMember (Name = "iyz")] public double Iyz { get; set; }
        [DataMember (Name = "izz")] public double Izz { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Inertia()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Inertia(double M, in GeometryMsgs.Vector3 Com, double Ixx, double Ixy, double Ixz, double Iyy, double Iyz, double Izz)
        {
            this.M = M;
            this.Com = Com;
            this.Ixx = Ixx;
            this.Ixy = Ixy;
            this.Ixz = Ixz;
            this.Iyy = Iyy;
            this.Iyz = Iyz;
            this.Izz = Izz;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Inertia(ref Buffer b)
        {
            M = b.Deserialize<double>();
            Com = new GeometryMsgs.Vector3(ref b);
            Ixx = b.Deserialize<double>();
            Ixy = b.Deserialize<double>();
            Ixz = b.Deserialize<double>();
            Iyy = b.Deserialize<double>();
            Iyz = b.Deserialize<double>();
            Izz = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Inertia(ref b);
        }
        
        Inertia IDeserializable<Inertia>.RosDeserialize(ref Buffer b)
        {
            return new Inertia(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(M);
            Com.RosSerialize(ref b);
            b.Serialize(Ixx);
            b.Serialize(Ixy);
            b.Serialize(Ixz);
            b.Serialize(Iyy);
            b.Serialize(Iyz);
            b.Serialize(Izz);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 80;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Inertia";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1d26e4bb6c83ff141c5cf0d883c2b0fe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1STUsDMRS8B/ofBnpRqCtU8SD05EF6KAgWL1Ildt9uQzd5S5La7tIf78tuvwSPBgKP" +
                "ycxk3kuGmOkQ8L4uF6qoWMeHe1ilhngiF8mDC9iOYBeqJLYUffNpQxlu32gZ2d9hyR1/6shHozEnF9gn" +
                "wxv7MV7ISVp7mN1OdiO7xT7xMelQQZq0e/TIbTvEtAk9xhKHi7q5qNtz3VzgzQXetmqgJv+8Bmr2+vyI" +
                "P+cykG7mKxPgqfYUZJoBGt/dIYxD4YkQar2kDGkcEcJlVzWwpF1E5LNShLnxIjXsMnElTwV7GsFE5EwB" +
                "jqN4WL0WS5k/JbWuazHTiF67UOmkTbBIrigrsxG2K3I9y7hSiOJQkryiWcKb0uS9Ui6yJ7HGobsRYjHG" +
                "1lRVn7m/LK5ITDzHTnCdYVqg4Q22qSEpPHIdJRHjSyIecumvKuXlETYpeGfxe6IvbERvKQRdkswuRNJ5" +
                "pk6Pe/4W58dvB+oHH8CTFtsCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
