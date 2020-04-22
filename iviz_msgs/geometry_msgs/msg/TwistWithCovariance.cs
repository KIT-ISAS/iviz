
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class TwistWithCovariance : IMessage
    {
        // This expresses velocity in free space with uncertainty.
        
        public Twist twist;
        
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        public double[/*36*/] covariance;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/TwistWithCovariance";
    
        public IMessage Create() => new TwistWithCovariance();
    
        public int GetLength() => 336;
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovariance()
        {
            twist = new Twist();
            covariance = new double[36];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            twist.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out covariance, ref ptr, end, 36);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            twist.Serialize(ref ptr, end);
            BuiltIns.Serialize(covariance, ref ptr, end, 36);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1fe8a28e6890a4cc3ae4c3ca5c7d82e6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1TTWvbQBC9L/g/PMglAUWFpvgQ6LnkUCitKf2glLE0kreRdszsOJby6zuyVSemPfRQ" +
                "KpAYzc578/X2AqtNzOBhq5wzZzxwJ1W0ETGhUWbkLVWMfbQNdqliNYrJxjKE1T5mg03fEC7wXvbXPf0Q" +
                "hfJExsnIoiRIA9swlsMSlTyQRnIa9GQaB8et/Ew0nsK3pNSzsWbsMoPQxIHraxq8zHPm0tF3zq81a3HI" +
                "8QxLyrd+fjkUGAs8FlCZE9BadoZPmBh/c3/+s/vLwX0Vmk7Ilq++3iy/PWsmLMLrf/wswtsPb27Rsng/" +
                "On7vc5tfHCa+CH+7s7XKPSd3miBaRhcTk4JS7W+769z2gVkuw0euTPQGc8jT/xz3vxqc855aPO3b9+ld" +
                "TofnPZaYNGDwWEndiJ4puSblCenAOqpDJ8FMYlNuRLnwiaAWH14Sc46e7p2SkyvOBLTdOhnBlFLujkJw" +
                "t0MuuWzLAvuND/YQFVPrgc7QcmKNFTS2sT4iPZHLfAYT5u5cqs1Lv1Bdd6z5mMzl6yS/ZHdV4q7BKDvs" +
                "p4bcUNRkXpFg7SXOddG6m+qV4nBVjhTnE30nvn4fS87Uss8uG1Ptl3cWMoaTNZ6sx0X4Cf1kjn0XBAAA";
                
    }
}
