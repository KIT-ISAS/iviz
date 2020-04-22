
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class AccelWithCovariance : IMessage
    {
        // This expresses acceleration in free space with uncertainty.
        
        public Accel accel;
        
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        public double[/*36*/] covariance;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/AccelWithCovariance";
    
        public IMessage Create() => new AccelWithCovariance();
    
        public int GetLength() => 336;
    
        /// <summary> Constructor for empty message. </summary>
        public AccelWithCovariance()
        {
            accel = new Accel();
            covariance = new double[36];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            accel.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out covariance, ref ptr, end, 36);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            accel.Serialize(ref ptr, end);
            BuiltIns.Serialize(covariance, ref ptr, end, 36);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ad5a718d699c6be72a02b8d6a139f334";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1TTWvcQAy9D+x/eJBLAo4LTdlDoIecSg6F0obSD0rR2rJ3GntkNLNZO7++8kfdLO2l" +
                "ELqwIGv0nvQ0b85wt/cR3HfKMXIEFQU3rJS8BPiASpkROyoYR5/2OISCNZEPaciduxmrZ4xzZ3gvx8uW" +
                "fohCeSTkkGYiqZD2jG2/RSEPpJ6MBi0l9b3h7uxM1K/lHSm1nFgjDpFBqHzP5SX1Nuopc27oW+PXkjWb" +
                "ejzBkvK1nZ/3GYYMjxlUlga0k0PCJ4yMf6Q//z39ZUpfuKoRSttXX6+2356IcRv3+pl/G/f2w5tr1Cym" +
                "R4fvbazji2njG/cv97ZTuecxmQQ+RTQ+MCkolPavD43FtrQUc/eRiyR6haXk9/dS979ELn1Xmeudm048" +
                "TIenGnOMPkiwWgnNgJYpJJjeFWnA0qtBR9OMhlOuRDmzjaAUW2CQZBwt3RslB3NdElDXGRkhKYXYzLu1" +
                "tEHOOa/zDMe9LXaq8qG2QmOoObD6AuprX85Ia2RWX8CERZ3ZtXppj6pp5pnnZmZhI/llvYsctxUGOeA4" +
                "CrJAUVKyiQQ7G3GZi3bNOK9k03OZKU43+k7s+m0tMVLNtruYmEp7wIuZ0a/RsEaPG/cTZs8JRh8EAAA=";
                
    }
}
