/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Joy : IDeserializable<Joy>, IMessage
    {
        // Reports the state of a joysticks axes and buttons.
        /// timestamp in the header is the time the data is received from the joystick
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// the axes measurements from a joystick
        [DataMember (Name = "axes")] public float[] Axes;
        /// the buttons measurements from a joystick
        [DataMember (Name = "buttons")] public int[] Buttons;
    
        /// Constructor for empty message.
        public Joy()
        {
            Axes = System.Array.Empty<float>();
            Buttons = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public Joy(in StdMsgs.Header Header, float[] Axes, int[] Buttons)
        {
            this.Header = Header;
            this.Axes = Axes;
            this.Buttons = Buttons;
        }
        
        /// Constructor with buffer.
        internal Joy(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Axes = b.DeserializeStructArray<float>();
            Buttons = b.DeserializeStructArray<int>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Joy(ref b);
        
        public Joy RosDeserialize(ref ReadBuffer b) => new Joy(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Axes);
            b.SerializeStructArray(Buttons);
        }
        
        public void RosValidate()
        {
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/Joy";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "5a9ea5f83505693b71e785041e67a8bb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61TwW7UMBC9+ytGyqEt0hYJbitxQ1AOSIj2htBq1p4kpokd7Mm2+XuenaZbcUA9YFlJ" +
                "bM+8eX5v0tB3mWLSTNoLZWUVii0x/YpLVm/vM/Gj4BEcHWfVGPK1uRF2kqhfX+fRkPpRADJO5ENFfIrx" +
                "K345rh+OlctmEiv+JI7aFMd6stU17RBZ37/78XMl8LIIwureKJznJKME8K8IZ97GhzX7ifVf+dvuvyDI" +
                "mA//eZivt5/3kNkdxtzlt6uQpqFbhcCcHPgoV3HaCIF910vaDXKSgaqsUKqe6jIJjGjoroeKmJ0ESTwM" +
                "C80ZQRrJxnGcg7fF0WdbtnxkwiCmiRNuOg+cEB+T86GEt4lHKeiYWX7PEqzQl497xIQsdlZYhko+2AT5" +
                "fOhwSGauipcE09w9xB2W0sH7c09oz1rIyuOUJBeenPeo8Wa93DWwIY6gist0WfcOWOYrQhFQQKfani7B" +
                "/NuifVw77MTJ83GQAmyhAFAvStLF1QvkUKEDh7jBr4jnGq+BDc+45U67Hp4N5fZ57rj2+JTiyTuEHpcK" +
                "YgePxqLBHxOnxdT2ryVN86lojCBkbT8D5xythwGOHrz2Jmsq6NWNg3fG/AFW3XPdrQMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
