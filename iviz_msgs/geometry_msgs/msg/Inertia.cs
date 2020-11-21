/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Inertia")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Inertia : IMessage, System.IEquatable<Inertia>, IDeserializable<Inertia>
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
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Inertia(ref b);
        }
        
        readonly Inertia IDeserializable<Inertia>.RosDeserialize(ref Buffer b)
        {
            return new Inertia(ref b);
        }
        
        public override readonly int GetHashCode() => (M, Com, Ixx, Ixy, Ixz, Iyy, Iyz, Izz).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Inertia s && Equals(s);
        
        public readonly bool Equals(Inertia o) => (M, Com, Ixx, Ixy, Ixz, Iyy, Iyz, Izz) == (o.M, o.Com, o.Ixx, o.Ixy, o.Ixz, o.Iyy, o.Iyz, o.Izz);
        
        public static bool operator==(in Inertia a, in Inertia b) => a.Equals(b);
        
        public static bool operator!=(in Inertia a, in Inertia b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 80;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Inertia";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1d26e4bb6c83ff141c5cf0d883c2b0fe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1STUvDQBC9B/IfHvSiECNU8SD05EF6KAgWL6KybSbp0uxO2d3aJvTHO5ukGsGjCwM7" +
                "H+/tm5mdYKG8x+u2ekuTsmYV7m5h0iRNJnggG8iBS5iuxkhJRWwouObD+Mpfv9A6sLvBmgfI3JILWmFJ" +
                "1rOLtFfmfSq4CeI5QR+PYo1Yi1OHwKwLS6iJNoTP1W0X0m0XPusTkrHTjJ125DTjTDPOtOKkyeyfT5os" +
                "nh/v8eeMYlPLjfZwtHPkZbQeCp9dEtqidETwO7WmHN1cAqSYbd3AkLIBgX+ggiy0E6xmmwstOSrZUQYd" +
                "UDB5WA6RxKitkMouKMLVbidsCsEp62sVwTEsmAvKqzzDYUO2r9K2ksJIUZGsVK/hdKWLHipPmW+0wtBg" +
                "hlBOcdB13avuXwsbiiyOQ4e4zDEv0fAeh9iTXBwKFUQTYyUiB2VqVUfFnGEfpfccv8f6xFoIDHmvKpIB" +
                "+kCqyONWz0se/ZHRP5DFfwHcg+JT9AIAAA==";
                
    }
}
