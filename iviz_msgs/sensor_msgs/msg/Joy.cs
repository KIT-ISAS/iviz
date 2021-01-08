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
                "H4sIAAAAAAAACq1Ty27bMBC8C9A/LOBDkgJOgfZmoLeij0OBosmtKIw1uZKYiKRKUk70952l7Drooeih" +
                "gi1b5OzMcma1oW8yxVQylUEoFy5CsSOmh7jk4sxjJn4W3IKlw1xKDPm2bT4JW0k0rD+Xa0PFeQGLn8iF" +
                "SnnCuFVAt+sfy4V1MYkRdxRLXYq+7pyF26YbI5e3b77/WFt4qQJcXfPCeU7iJeAEleLSedu4sJafGv+D" +
                "4Lz6Nw5qm7Z595+vtvly93EHs+3e5z6/Xt1smw3dFfjMyaKnwtWiLsJm1w+StqMcZdSI/AS/6m5ZJtE8" +
                "NnQ/wEx8egmSeBwXmjNQJZKJ3s/BGU32dzpnAi1FUEwTJ5x3HjmhICbrguK7xF4qv36z/JwlGKHP73dA" +
                "hSxmLggPYi6YBBtd6LEJ8Fyt1woU3j/FLZ6lxxxc5qMMXLRjeZ6SZG2W805lXq1nvAU9TBII2UzXdW2P" +
                "x3xD0EEXmFsz0DXa/7qUIa7jduTk+DCKMhv4ANorLbq6eUmtre8ocIhn/pXyIvIvvMpyItZjbQeEN6oF" +
                "ee7hI5BTikdngT0slcWMDlNGozskTkvb1NehioLkg5oNGOrObwfnHI1DEpaeXBnaJpekAjWXvbM6nb8A" +
                "HEsLCsIDAAA=";
                
    }
}
