/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Inertia : IDeserializable<Inertia>, IMessage
    {
        // Mass [kg]
        [DataMember (Name = "m")] public double M;
        // Center of mass [m]
        [DataMember (Name = "com")] public GeometryMsgs.Vector3 Com;
        // Inertia Tensor [kg-m^2]
        //     | ixx ixy ixz |
        // I = | ixy iyy iyz |
        //     | ixz iyz izz |
        [DataMember (Name = "ixx")] public double Ixx;
        [DataMember (Name = "ixy")] public double Ixy;
        [DataMember (Name = "ixz")] public double Ixz;
        [DataMember (Name = "iyy")] public double Iyy;
        [DataMember (Name = "iyz")] public double Iyz;
        [DataMember (Name = "izz")] public double Izz;
    
        /// Constructor for empty message.
        public Inertia()
        {
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal Inertia(ref Buffer b)
        {
            M = b.Deserialize<double>();
            b.Deserialize(out Com);
            Ixx = b.Deserialize<double>();
            Ixy = b.Deserialize<double>();
            Ixz = b.Deserialize<double>();
            Iyy = b.Deserialize<double>();
            Iyz = b.Deserialize<double>();
            Izz = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Inertia(ref b);
        
        Inertia IDeserializable<Inertia>.RosDeserialize(ref Buffer b) => new Inertia(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(M);
            b.Serialize(ref Com);
            b.Serialize(Ixx);
            b.Serialize(Ixy);
            b.Serialize(Ixz);
            b.Serialize(Iyy);
            b.Serialize(Iyz);
            b.Serialize(Izz);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 80;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Inertia";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1d26e4bb6c83ff141c5cf0d883c2b0fe";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1SwUoDMRS85ysGvCisK1TxIPTkQXooCBYvUiXtvt2GbvJKktru0o/3ZbftVvBoIPCY" +
                "zEzmveQKUx0CPtbVXJU16/j4AKvUFZ7JRfLgErYj2LmqiC1F33zZUIW7d1pG9vdYcsefOPLRaMzIBfbJ" +
                "8NZ+juZyktYBZr+X3chucUh8jDtUkCbtHj1x2w4xbUJPscThom4u6naomwu8ucDbVqnxPy81fXt5wp9T" +
                "kVZmKxPgaeMpyCgDNL67MxiH0hMhbPSScqRZRAiXXd3AknYRkQelCAvjRWrY5eJKnkr2lMFEFEwBjqN4" +
                "WL0WSxk+JbXebMRMI3rtQq2TNsEiuaa8yjPsVuR6lnGVEMWhInlCs4Q3lSl6pVxkz2KNY3MZYjnCztR1" +
                "n7m/LK5ITDzHTnCTY1Ki4S12qSEpPAodJRFjIRGPufSiTnk5wzYF7yx+D/SVjegthaArktmFSLrI1fll" +
                "hz8xvHyrfgBfqLHq1wIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
