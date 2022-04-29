/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct TransformStamped : IMessage, IDeserializable<TransformStamped>, System.IEquatable<TransformStamped>
    {
        // This expresses a transform from coordinate frame header.frame_id
        // to the coordinate frame child_frame_id
        //
        // This message is mostly used by the 
        // <a href="http://wiki.ros.org/tf">tf</a> package. 
        // See its documentation for more information.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> The frame id of the child frame </summary>
        [DataMember (Name = "child_frame_id")] public string ChildFrameId;
        [DataMember (Name = "transform")] public Transform Transform;
    
        /// Explicit constructor.
        public TransformStamped(in StdMsgs.Header Header, string ChildFrameId, in Transform Transform)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Transform = Transform;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TransformStamped(ref ReadBuffer b)
        {
            Deserialize(ref b, out this);
        }
        
        public static void Deserialize(ref ReadBuffer b, out TransformStamped h)
        {
            StdMsgs.Header.Deserialize(ref b, out h.Header);
            b.DeserializeString(out h.ChildFrameId);
            b.Deserialize(out h.Transform);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TransformStamped(ref b);
        
        public readonly TransformStamped RosDeserialize(ref ReadBuffer b) => new TransformStamped(ref b);
        
        public readonly override int GetHashCode() => (Header, ChildFrameId, Transform).GetHashCode();
        public readonly override bool Equals(object? o) => o is TransformStamped s && Equals(s);
        public readonly bool Equals(TransformStamped o) => (Header, ChildFrameId, Transform) == (o.Header, o.ChildFrameId, o.Transform);
        public static bool operator==(in TransformStamped a, in TransformStamped b) => a.Equals(b);
        public static bool operator!=(in TransformStamped a, in TransformStamped b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId ?? "");
            b.Serialize(in Transform);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 60 + Header.RosMessageLength + BuiltIns.GetStringSize(ChildFrameId);
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TransformStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b5764a33bfeb3588febc2682852579b0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTU/cMBC951eMugeggqwEVQ8IOKG2HCpRgXpFQzJJLBI72BOW9Nf32dnN8iW1h5ZV" +
                "pLUdz5uP92ayoOvGBJLH3ksIEohJPdtQOd9R5V1HhXO+NJZVsOdOqBEuxedpc2PKbEHqSBt5fbNoTFve" +
                "bC/iavLWwRXXQnHpgrYjDUFKuh0TDG6dMDVeqtMPjWp/vFyuzJ3JvQu58/VSqw9nWp0s+Yx6Lu4AlEeb" +
                "KwGgBipdMXRildU4S8gDPjxe2ZhSOsyz7FvKYZ1KFtQbW78IlxYpmikTbF01JRkvTafZ9VypuWZZdvqP" +
                "f9n3q6/HFLS86UIdllPkMV9lW7IvUU3lkpVTro2pG/EHrTxICyPuehQ2vdWxl5BvKMBTixXP7ab6ILFw" +
                "XTdYU0QG1YClp/awNBby6NmrKYaW/SvCIzqeIPeD2ELo4vwYd2yQYlCDgEYgFF44xGpfnFM2GKtHh9Eg" +
                "W1yv3AG2UoOX2TlKzkpPBFoSh2P4+DgllwMbxRF4KQPtprMbbMMewQlCkN4VDe0i8stRGwgicvjA3vBt" +
                "mwRYoAJA3YlGO3tPkG2CtmzdBn5C3Pr4G1g748acDhpw1sbsw1CjgLjYe/dgyq36i9ZAvNSaW89+zKLV" +
                "5DJbfElS1EhfYgT/HIIrDAgoaWW02Sh5brn/pMZaHFTnx0mScxtsxOUlkoU0QkppO1BuRVciqNbKvRJP" +
                "iPKqPLo4oK2hpeynFOr80WTfptbNfgww8Da2tndTj79Pkutg3kiR6SG9exF/7ISLpF1nofxOGLSiyWZL" +
                "GJbGwzSOJKAKJh4m1T6mGIYY6mGdAqPjO0AKhBStue8Bxk9rEo9hsit5ne/TqkF9060ohNS2qdFNQd7U" +
                "mGMzG7Mx0zq5fdLqEEJq2ynmyRkoBMim2ns5XVQ0uoFWMSEs/Hq+ONA7x5X6QJ3bj8NlDfG8oJcO3b79" +
                "FNigmGxgvWod6+dP9Divxnn1612o3mrsLbYtOW/m78szzuPufivQWOQ/JrRZrbLsN7aLdoyMBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
