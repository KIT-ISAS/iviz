/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt32MultiArray")]
    public sealed class UInt32MultiArray : IDeserializable<UInt32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public uint[] Data; // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public UInt32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<uint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt32MultiArray(MultiArrayLayout Layout, uint[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal UInt32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt32MultiArray(ref b);
        }
        
        UInt32MultiArray IDeserializable<UInt32MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new UInt32MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 4 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt32MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4d6a180abc9be191b96a7eda6c8a233d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
