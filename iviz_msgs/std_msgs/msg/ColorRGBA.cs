using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ColorRGBA : IMessage
    {
        [DataMember] public float r { get; }
        [DataMember] public float g { get; }
        [DataMember] public float b { get; }
        [DataMember] public float a { get; }
    
        /// <summary> Explicit constructor. </summary>
        public ColorRGBA(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ColorRGBA(Buffer b)
        {
            this = b.Deserialize<ColorRGBA>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new ColorRGBA(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 16;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/ColorRGBA";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a29a96539573343b1310c73607334b00";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSKOJKg7LS4awkOCuRiwsAZHVNWikAAAA=";
                
    }
}
