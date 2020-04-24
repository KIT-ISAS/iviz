namespace Iviz.Msgs.sensor_msgs
{
    public sealed class Joy : IMessage
    {
        // Reports the state of a joysticks axes and buttons.
        public std_msgs.Header header; // timestamp in the header is the time the data is received from the joystick
        public float[] axes; // the axes measurements from a joystick
        public int[] buttons; // the buttons measurements from a joystick 
    
        /// <summary> Constructor for empty message. </summary>
        public Joy()
        {
            header = new std_msgs.Header();
            axes = System.Array.Empty<float>();
            buttons = System.Array.Empty<int>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out axes, ref ptr, end, 0);
            BuiltIns.Deserialize(out buttons, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(axes, ref ptr, end, 0);
            BuiltIns.Serialize(buttons, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 8;
            size += header.GetLength();
            size += 4 * axes.Length;
            size += 4 * buttons.Length;
            return size;
        }
    
        public IMessage Create() => new Joy();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "sensor_msgs/Joy";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "5a9ea5f83505693b71e785041e67a8bb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
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
