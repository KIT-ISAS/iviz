/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Joy")]
    public sealed class Joy : IDeserializable<Joy>, IMessage
    {
        // Reports the state of a joysticks axes and buttons.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp in the header is the time the data is received from the joystick
        [DataMember (Name = "axes")] public float[] Axes { get; set; } // the axes measurements from a joystick
        [DataMember (Name = "buttons")] public int[] Buttons { get; set; } // the buttons measurements from a joystick 
    
        /// <summary> Constructor for empty message. </summary>
        public Joy()
        {
            Header = new StdMsgs.Header();
            Axes = System.Array.Empty<float>();
            Buttons = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Joy(StdMsgs.Header Header, float[] Axes, int[] Buttons)
        {
            this.Header = Header;
            this.Axes = Axes;
            this.Buttons = Buttons;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Joy(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Axes = b.DeserializeStructArray<float>();
            Buttons = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Joy(ref b);
        }
        
        Joy IDeserializable<Joy>.RosDeserialize(ref Buffer b)
        {
            return new Joy(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Axes, 0);
            b.SerializeStructArray(Buttons, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Axes is null) throw new System.NullReferenceException(nameof(Axes));
            if (Buttons is null) throw new System.NullReferenceException(nameof(Buttons));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += 4 * Axes.Length;
                size += 4 * Buttons.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Joy";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5a9ea5f83505693b71e785041e67a8bb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
