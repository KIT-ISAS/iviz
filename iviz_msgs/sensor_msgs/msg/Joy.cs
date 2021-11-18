/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Joy")]
    public sealed class Joy : IDeserializable<Joy>, IMessage
    {
        // Reports the state of a joysticks axes and buttons.
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp in the header is the time the data is received from the joystick
        [DataMember (Name = "axes")] public float[] Axes; // the axes measurements from a joystick
        [DataMember (Name = "buttons")] public int[] Buttons; // the buttons measurements from a joystick 
    
        /// <summary> Constructor for empty message. </summary>
        public Joy()
        {
            Axes = System.Array.Empty<float>();
            Buttons = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Joy(in StdMsgs.Header Header, float[] Axes, int[] Buttons)
        {
            this.Header = Header;
            this.Axes = Axes;
            this.Buttons = Buttons;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Joy(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
                "H4sIAAAAAAAACq1Ty4rcMBC8C+YfGnzY3cBsILkN5BbyOARCdm8hDD1S21aihyPJs+u/T0leZ5YcQg4x" +
                "ZjyWqqtLVe2OvsgUU8lURqFcuAjFnpi+xyUXq39k4kfBTzB0mkuJId+qD8JGEo3r43J1VKwXkPiJbGiM" +
                "Txi78tft9sdw4bqYRIs9i6E+Rd92tr6qd5HL61dfv60CnjcBrK154Twn8RKgvzFcdCsb1uon1X/Ub6t/" +
                "oyC1U2/+87VTn+7eH2C0Ofo85JerlTvV0V2Bx5wMJBVu/vQRHtthlLR3chZX4/ETzGq7ZZkEWXR0P8JI" +
                "3IMESezcQnMGqETS0fs5WF1D/Z3MVo9KZMQ0ccJhZ8cJ+JiMDRXeJ/ZS2XFn+TlL0EIf3x6ACVn0XJAa" +
                "OtmgExy0YcAmqbmZXgtUd/8Q93iVAfFfxqKMXKpYeZyS5KqT8wE9XqyHuwU33BF0MZmu29oRr/mG0EQL" +
                "YVj1SNdQ/nkpY1yH7MzJ8slJJdZwAKxXtejq5hlzlX2gwCFu9Cvjpce/0FaWlbeeaT8iM1dPn+cBBgI4" +
                "pXi2BtDT0ki0s5gtcvaUOC2qfQGtpereVY8BQtX2PXDOUVsEYOjBllHlkip7S+NoDQbyFyuQrCWxAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
