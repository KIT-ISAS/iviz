/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point32 : IMessage, System.IEquatable<Point32>, IDeserializable<Point32>
    {
        // This contains the position of a point in free space(with 32 bits of precision).
        // It is recommeded to use Point wherever possible instead of Point32.  
        // 
        // This recommendation is to promote interoperability.  
        //
        // This message is designed to take up less space when sending
        // lots of points at once, as in the case of a PointCloud.  
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
        [DataMember (Name = "z")] public float Z;
    
        /// Explicit constructor.
        public Point32(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Point32(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b) => new Point32(ref b);
        
        readonly Point32 IDeserializable<Point32>.RosDeserialize(ref Buffer b) => new Point32(ref b);
        
        public override readonly int GetHashCode() => (X, Y, Z).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Point32 s && Equals(s);
        
        public readonly bool Equals(Point32 o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        
        public static bool operator==(in Point32 a, in Point32 b) => a.Equals(b);
        
        public static bool operator!=(in Point32 a, in Point32 b) => !a.Equals(b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 12;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Point32";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACj1QMW7DMAzc/YoDsrRAkSF5QqduHfoB2aJtorIoiHTS9PWllDSABoq8O97xgK+VFZNk" +
                "C5wVthKKKBtLhswI/uNs4Iy5EkFLmOjlyrbifMLIpg1VKk2sTnk9Dgd8OFzhLdk2ihRhgl0Jn13pulKl" +
                "C9W2RnlM5NpqFGIT6pDz6Qi4jr9u7qGUY+iuvOOCpcom1shGVQrVMHJiu3XqP3Mj1bA4SBFJecl3Mxa+" +
                "CXtB8vE9UXOVob6D8+LsJI9gzY8iGCRP9Iag7RLtSFPwRP1A3fN7kj223cOcJHgE/Dyr27P6Hf4Ad4CM" +
                "MHABAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
