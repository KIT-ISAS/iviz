
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class TransformStamped : IMessage
    {
        // This expresses a transform from coordinate frame header.frame_id
        // to the coordinate frame child_frame_id
        //
        // This message is mostly used by the 
        // <a href="http://wiki.ros.org/tf">tf</a> package. 
        // See its documentation for more information.
        
        public std_msgs.Header header;
        public string child_frame_id; // the frame id of the child frame
        public Transform transform;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/TransformStamped";
    
        public IMessage Create() => new TransformStamped();
    
        public int GetLength()
        {
            int size = 60;
            size += header.GetLength();
            size += child_frame_id.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TransformStamped()
        {
            header = new std_msgs.Header();
            child_frame_id = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out child_frame_id, ref ptr, end);
            transform.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(child_frame_id, ref ptr, end);
            transform.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b5764a33bfeb3588febc2682852579b0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
