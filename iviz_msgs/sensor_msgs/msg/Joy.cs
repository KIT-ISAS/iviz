using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class Joy : IMessage
    {
        // Reports the state of a joysticks axes and buttons.
        public std_msgs.Header header { get; set; } // timestamp in the header is the time the data is received from the joystick
        public float[] axes { get; set; } // the axes measurements from a joystick
        public int[] buttons { get; set; } // the buttons measurements from a joystick 
    
        /// <summary> Constructor for empty message. </summary>
        public Joy()
        {
            header = new std_msgs.Header();
            axes = System.Array.Empty<float>();
            buttons = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Joy(std_msgs.Header header, float[] axes, int[] buttons)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.axes = axes ?? throw new System.ArgumentNullException(nameof(axes));
            this.buttons = buttons ?? throw new System.ArgumentNullException(nameof(buttons));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Joy(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.axes = BuiltIns.DeserializeStructArray<float>(b, 0);
            this.buttons = BuiltIns.DeserializeStructArray<int>(b, 0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Joy(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            BuiltIns.Serialize(this.axes, b, 0);
            BuiltIns.Serialize(this.buttons, b, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (axes is null) throw new System.NullReferenceException();
            if (buttons is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += header.RosMessageLength;
                size += 4 * axes.Length;
                size += 4 * buttons.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/Joy";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "5a9ea5f83505693b71e785041e67a8bb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61TwW7UMBC9+ytGyqEt0hYJbitxQ1AOSIj2htBq1p4kpokd7Mm2+XuenaZbcUA9YFlJ" +
                "bM+8eX5v0tB3mWLSTNoLZWUVii0x/YpLVm/vM/Gj4BEcHWfVGPK1uRF2kqhfX+fRkPpRADJO5ENFfIrx" +
                "K345rh+OlctmEiv+JI7aFMd6stU17RBZ37/78XMl8LIIwureKJznJKME8K8IZ97GhzX7ifVf+dvuvyDI" +
                "mA//eZivt5/3kNkdxtzlt6uQpqFbhcCcHPgoV3HaCIF910vaDXKSgaqsUKqe6jIJjGjoroeKmJ0ESTwM" +
                "C80ZQRrJxnGcg7fF0WdbtnxkwiCmiRNuOg+cEB+T86GEt4lHKeiYWX7PEqzQl497xIQsdlZYhko+2AT5" +
                "fOhwSGauipcE09w9xB2W0sH7c09oz1rIyuOUJBeenPeo8Wa93DWwIY6gist0WfcOWOYrQhFQQKfani7B" +
                "/NuifVw77MTJ83GQAmyhAFAvStLF1QvkUKEDh7jBr4jnGq+BDc+45U67Hp4N5fZ57rj2+JTiyTuEHpcK" +
                "YgePxqLBHxOnxdT2ryVN86lojCBkbT8D5xythwGOHrz2Jmsq6NWNg3fG/AFW3XPdrQMAAA==";
                
    }
}
