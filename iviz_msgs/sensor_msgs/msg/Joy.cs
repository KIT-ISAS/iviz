
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class Joy : IMessage
    {
        // Reports the state of a joysticks axes and buttons.
        public std_msgs.Header header; // timestamp in the header is the time the data is received from the joystick
        public float[] axes; // the axes measurements from a joystick
        public int[] buttons; // the buttons measurements from a joystick 
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Joy";
    
        public IMessage Create() => new Joy();
    
        public int GetLength()
        {
            int size = 8;
            size += header.GetLength();
            size += 4 * axes.Length;
            size += 4 * buttons.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Joy()
        {
            header = new std_msgs.Header();
            axes = System.Array.Empty<0>();
            buttons = System.Array.Empty<0>();
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "5a9ea5f83505693b71e785041e67a8bb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1Ty4rcMBC8C+YfGnzY3cBsILkN5BbyOARCdm8hDD1S21aihyPJs+u/T0leZ5YcQg4x" +
                "ZjyWqqtLVe2OvsgUU8lURqFcuAjFnpi+xyUXq39k4kfBTzB0mkuJId+qD8JGEo3r43J1VKwXkPiJbGiM" +
                "Txi78tft9sdw4bqYRIs9i6E+Rd92tr6qd5HL61dfv60CnjcBrK154Twn8RKgvzFcdCsb1uon1X/Ub6t/" +
                "oyC1U2/+87VTn+7eH2C0Ofo85JerlTvV0V2Bx5wMJBVu/vQRHtthlLR3chZX4/ETzGq7ZZkEWXR0P8JI" +
                "3IMESezcQnMGqETS0fs5WF1D/Z3MVo9KZMQ0ccJhZ8cJ+JiMDRXeJ/ZS2XFn+TlL0EIf3x6ACVn0XJAa" +
                "OtmgExy0YcAmqbmZXgtUd/8Q93iVAfFfxqKMXKpYeZyS5KqT8wE9XqyHuwU33BF0MZmu29oRr/mG0EQL" +
                "YVj1SNdQ/nkpY1yH7MzJ8slJJdZwAKxXtejq5hlzlX2gwCFu9Cvjpce/0FaWlbeeaT8iM1dPn+cBBgI4" +
                "pXi2BtDT0ki0s5gtcvaUOC2qfQGtpereVY8BQtX2PXDOUVsEYOjBllHlkip7S+NoDQbyFyuQrCWxAwAA";
                
    }
}
