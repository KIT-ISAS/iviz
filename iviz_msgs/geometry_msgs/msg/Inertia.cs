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
                "H4sIAAAAAAAACq1STUvDQBC9B/IfHvSiECNU8SD05EF6KAgWL6KybSbp0uxO2d3aJvTHO5ukGsGjCwM7" +
                "H+/tm5mdYKG8x+u2ekuTsmYV7m5h0iRNJnggG8iBS5iuxkhJRWwouObD+Mpfv9A6sLvBmgfI3JILWmFJ" +
                "1rOLtFfmfSq4CeI5QR+PYo1Yi1OHwKwLS6iJNoTP1W0X0m0XPusTkrHTjJ125DTjTDPOtOKkyeyfT5os" +
                "nh/v8eeMYlPLjfZwtHPkZbQeCp9dEtqidETwO7WmHN1cAqSYbd3AkLIBgX+ggiy0E6xmmwstOSrZUQYd" +
                "UDB5WA6RxKitkMouKMLVbidsCsEp62sVwTEsmAvKqzzDYUO2r9K2ksJIUZGsVK/hdKWLHipPmW+0wtBg" +
                "hlBOcdB13avuXwsbiiyOQ4e4zDEv0fAeh9iTXBwKFUQTYyUiB2VqVUfFnGEfpfccv8f6xFoIDHmvKpIB" +
                "+kCqyONWz0se/ZHRP5DFfwHcg+JT9AIAAA==";
                
    }
}
