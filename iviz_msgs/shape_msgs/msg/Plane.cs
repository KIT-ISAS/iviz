
namespace Iviz.Msgs.shape_msgs
{
    public sealed class Plane : IMessage
    {
        // Representation of a plane, using the plane equation ax + by + cz + d = 0
        
        // a := coef[0]
        // b := coef[1]
        // c := coef[2]
        // d := coef[3]
        
        public double[/*4*/] coef;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "shape_msgs/Plane";
    
        public IMessage Create() => new Plane();
    
        public int GetLength() => 32;
    
        /// <summary> Constructor for empty message. </summary>
        public Plane()
        {
            coef = new double[4];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out coef, ref ptr, end, 4);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(coef, ref ptr, end, 4);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "2c1b92ed8f31492f8e73f6a4a44ca796";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAClNWCEotKEotTs0rSSzJzM9TyE9TSFQoyEnMS9VRKC3OzEtXKMlIhQgopBaWQhQlViho" +
                "KyRVAonkKiCRomCrYMDFpQzUaWWrkJyfmhZtEAvkJsG5hiBuMpxrBOKmwLnGsVxcaTn5iSVmJtEmsWAx" +
                "Ll4uAHsFt+abAAAA";
                
    }
}
