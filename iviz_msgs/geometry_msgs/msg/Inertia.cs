
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Inertia : IMessage
    {
        // Mass [kg]
        public double m;
        
        // Center of mass [m]
        public geometry_msgs.Vector3 com;
        
        // Inertia Tensor [kg-m^2]
        //     | ixx ixy ixz |
        // I = | ixy iyy iyz |
        //     | ixz iyz izz |
        public double ixx;
        public double ixy;
        public double ixz;
        public double iyy;
        public double iyz;
        public double izz;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Inertia";
    
        public IMessage Create() => new Inertia();
    
        public int GetLength() => 80;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out m, ref ptr, end);
            com.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out ixx, ref ptr, end);
            BuiltIns.Deserialize(out ixy, ref ptr, end);
            BuiltIns.Deserialize(out ixz, ref ptr, end);
            BuiltIns.Deserialize(out iyy, ref ptr, end);
            BuiltIns.Deserialize(out iyz, ref ptr, end);
            BuiltIns.Deserialize(out izz, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(m, ref ptr, end);
            com.Serialize(ref ptr, end);
            BuiltIns.Serialize(ixx, ref ptr, end);
            BuiltIns.Serialize(ixy, ref ptr, end);
            BuiltIns.Serialize(ixz, ref ptr, end);
            BuiltIns.Serialize(iyy, ref ptr, end);
            BuiltIns.Serialize(iyz, ref ptr, end);
            BuiltIns.Serialize(izz, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1d26e4bb6c83ff141c5cf0d883c2b0fe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1STUsDMRS8B/ofBnpRqCtU8SD05EF6KAgWL1Ildt9uQzd5S5La7tIf78tuvwSPBgKP" +
                "ycxk3kuGmOkQ8L4uF6qoWMeHe1ilhngiF8mDC9iOYBeqJLYUffNpQxlu32gZ2d9hyR1/6shHozEnF9gn" +
                "wxv7MV7ISVp7mN1OdiO7xT7xMelQQZq0e/TIbTvEtAk9xhKHi7q5qNtz3VzgzQXetmqgJv+8Bmr2+vyI" +
                "P+cykG7mKxPgqfYUZJoBGt/dIYxD4YkQar2kDGkcEcJlVzWwpF1E5LNShLnxIjXsMnElTwV7GsFE5EwB" +
                "jqN4WL0WS5k/JbWuazHTiF67UOmkTbBIrigrsxG2K3I9y7hSiOJQkryiWcKb0uS9Ui6yJ7HGobsRYjHG" +
                "1lRVn7m/LK5ITDzHTnCdYVqg4Q22qSEpPHIdJRHjSyIecumvKuXlETYpeGfxe6IvbERvKQRdkswuRNJ5" +
                "pk6Pe/4W58dvB+oHH8CTFtsCAAA=";
                
    }
}
