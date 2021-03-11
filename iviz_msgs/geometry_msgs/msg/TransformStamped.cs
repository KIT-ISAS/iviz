/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/TransformStamped")]
    [StructLayout(LayoutKind.Sequential)]
    public struct TransformStamped : IMessage, System.IEquatable<TransformStamped>, IDeserializable<TransformStamped>
    {
        // This expresses a transform from coordinate frame header.frame_id
        // to the coordinate frame child_frame_id
        //
        // This message is mostly used by the 
        // <a href="http://wiki.ros.org/tf">tf</a> package. 
        // See its documentation for more information.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "child_frame_id")] public string? ChildFrameId; // the frame id of the child frame
        [DataMember (Name = "transform")] public Transform Transform;
    
        /// <summary> Explicit constructor. </summary>
        public TransformStamped(in StdMsgs.Header Header, string ChildFrameId, in Transform Transform)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Transform = Transform;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TransformStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            ChildFrameId = b.DeserializeString();
            Transform = new Transform(ref b);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new TransformStamped(ref b);
        }
        
        readonly TransformStamped IDeserializable<TransformStamped>.RosDeserialize(ref Buffer b)
        {
            return new TransformStamped(ref b);
        }
        
        public override readonly int GetHashCode() => (Header, ChildFrameId, Transform).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is TransformStamped s && Equals(s);
        
        public readonly bool Equals(TransformStamped o) => (Header, ChildFrameId, Transform) == (o.Header, o.ChildFrameId, o.Transform);
        
        public static bool operator==(in TransformStamped a, in TransformStamped b) => a.Equals(b);
        
        public static bool operator!=(in TransformStamped a, in TransformStamped b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId ?? string.Empty);
            Transform.RosSerialize(ref b);
        }
        
        public readonly void Dispose()
        {
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength
        {
            get {
                int size = 60;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(ChildFrameId ?? string.Empty);
                return size;
            }
        }
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TransformStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b5764a33bfeb3588febc2682852579b0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VwW7TQBC9W8o/jJpDW9Q6EkUcosIJATkggVpxrab22F7V3jW746bm63m7TpyWIsEB" +
                "iCJl1955s2/em8mSrhsTSB56LyFIICb1bEPlfEeVdx0VzvnSWFbBnjuhRrgUn6fNjSmzJakjbeT5yaIx" +
                "bXlzOJjtsnVIxbVQXLqg7UhDkJJuxwSDU5dMjZfqzVGj2q9Xq625M7l3IXe+Xml19FaryxW/pZ6LOwDl" +
                "MeZKAKiBSlcMnVhlNc4SeCCHxysbKaWHeZZ9TBx2VLKg3tj6p+vSMt1mYoKtqyaS8dD0NLueKzXXLFtk" +
                "b/7yZ5F9uvqwpqDlTRfqsJruvoiUlW3JvkRBlUtWTnQbUzfiz1u5lxZR3PWobXqrYy8h36uAby1WPLd7" +
                "AaBj4bpusKaIIqqBUI/jEWksHNKzV1MMLftnmkd0fIN8G8QWQpt3a5yxQYpBDS40AqHwwiEWfPOOssFY" +
                "vXgZA7Ll9dadYys1pJmTo+qs9MijJXFYI8eLiVwObFRHkKUMdJKe3WAbTglJcAXpXdHQCW7+edQGnogy" +
                "3rM3fNsmDxaoAFCPY9Dx6SPkeO01WbZuDz8hHnL8CaydcSOn8waatZF9GGoUEAd77+5NeWiAojXwL7Xm" +
                "1rMfsxg1pcyW75MbNcqXFMEvh+AKAwFK2hpt9maeu+6fGbIWB9/5cXLl3AuLvb+8RL3AJCRWh7FyK7oV" +
                "QcG27pl/YEs0rUcvBzQ37JR9lUKdv5ji29TA2ZcBAd7GBvdu6vT/xXN3nV+xZLpPL3+iEPthkxzsLPzf" +
                "CUNctNocicDSeITG2QRUwejDyDrDOMM0Q0msU2B0fAdIgZ1iNPc9wPhxWeJjhJxIXudntG1Q4nQq2iE1" +
                "b2p3U5A3NQbaLMgczLRjd0ZavYSd2na685QMKgJkX/DTnDYVjW6gbSSEhd9NGQeF53ulblDnzuKI2UE8" +
                "rehnh54//CfYoBhwEL5qHevrV/Qwr8Z59f0/qX0w2i8Ft+R87NWpgk9kj7tvB5vGOv+O07zawsw/ABIw" +
                "D1iZBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
