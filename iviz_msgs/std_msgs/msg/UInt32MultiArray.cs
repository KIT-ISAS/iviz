
namespace Iviz.Msgs.std_msgs
{
    public sealed class UInt32MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public uint[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt32MultiArray";
    
        public IMessage Create() => new UInt32MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 4 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt32MultiArray()
        {
            layout = new MultiArrayLayout();
            data = new uint[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            layout.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            layout.Serialize(ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4d6a180abc9be191b96a7eda6c8a233d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6VmJbQZKbdb9+R7b8kfZ1" +
                "zBgs389zjq40oh8ZC8uUaX0g4cilTI9l5tTaGPH+IN516Shna8WOSXKiCuWULijRJhqR1HGZc+FEZcMr" +
                "soxyny58up1G0adilIVv/YzIHjlWiYpDkYSkcCJERaUq3HKx2TbhVHvbZ0RVpyYtigbR6h8/g+jx6fsd" +
                "WSdfcruzXz8yGkCIn5Ct4w2h4kwYtiRoxwUbFdfeK6kglwVPkXXABQochXEqLpFVE3TvR54S3TfxKGWY" +
                "tJFsWFJidE5ozYZybR3ynSZVFOH/TPa2BEREeyi2bhVrXHQ0+shAwDYoXqF40UliubdVRyGlKnbEGftt" +
                "ByjnsRSu0x/l4xjzoo0lm+oyk7R+eF7/eqJXppNRznEBqATsuT0HYZ1RklFBFLKZCpCteF55Xr3YRBnP" +
                "c0R4O+Ev1GQ/OVzSqgKz6XP44pNf6hab+Xaszi2L7XgPy2EbjTwFYAEIYeSElldxKiBtRjfXs9/X32ak" +
                "cn8YTsqlIAJsOEFvwBnrTBsKwRZVThV70O64CHtXNUDnzWw7zcQr6gLuMGW1S92wc1n1h6H5itCxZ63Q" +
                "wrocA83Yo1nR7WJ+M5sRXRTacYgMYpKytC+hXFUOalfYL0PBeR/BSUmXDjtPCwCNetYzAPjObxeNe9Ev" +
                "F3QYdr624LJna8tVsnzeScMJY5Iw3v5m8pIbfZrQHgvoXebFpJqWg/+vO07/6xVw30zkIPJccDaCBDgt" +
                "9Qqi79Qbhr4d3uaIBUH8FRh250MgXfiDgpuASly79rJNrFXzifXqc+og+gtbagn62wUAAA==";
                
    }
}
