using System.Runtime.Serialization;

namespace Iviz.Msgs.actionlib_msgs
{
    public sealed class GoalStatusArray : IMessage
    {
        // Stores the statuses for goals that are currently being tracked
        // by an action server
        public std_msgs.Header header;
        public GoalStatus[] status_list;
        
    
        /// <summary> Constructor for empty message. </summary>
        public GoalStatusArray()
        {
            header = new std_msgs.Header();
            status_list = System.Array.Empty<GoalStatus>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.DeserializeArray(out status_list, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.SerializeArray(status_list, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += header.RosMessageLength;
                for (int i = 0; i < status_list.Length; i++)
                {
                    size += status_list[i].RosMessageLength;
                }
                return size;
            }
        }
    
        public IMessage Create() => new GoalStatusArray();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "actionlib_msgs/GoalStatusArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "8b2b82f13216d0a8ea88bd3af735e619";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WwXLbNhC98yswo0PsTq22SZumntFBlVRXHSfxWGovnY4HBFYkWpBUAdCy/j5vQYqi" +
                "HKnRIalGtoYS8Pbh7dvFDsQiVI68CDkJH2SoPR5WlRNZJS1/LYOQjoSqnaMy2K1IyZSZCE6qf0gnA5Fu" +
                "hSyFVMFUpfDkHsklv5LU5EQeP5IbQC0i9p9/tUEerPEhSZLRZ34lbxc314ihHwqf+W8aHiCJ8KWWTouC" +
                "gtQyyHjG3GQ5uStLj2SZWLEmLeKvYbsmP8TGZW68wDujkpy0OD8U0iJUQlVFUZdGyUAimIIO9mOngShi" +
                "LV0wqrbSYX3ltCl5+crJghgdb0//1lQqEvPpNdaUnlQdDAhtgaAcSc9yz6ciqU0ZXr3kDclguamu8EgZ" +
                "VO6CN9kCWXpaI6fMU/prxPiqOdwQ2BCHEEV7cRG/e8CjvxQIAgq0rlQuLsD8bhtypJNd8SidkaklBlZQ" +
                "AKgveNOLyx5yGaFLWVY7+AZxH+Mc2LLD5TNd5ciZ5dP7OoOAWLh21aPRWArTMYiyBqYU1qROum3Cu5qQ" +
                "yeAX1hiLsCtmBJ/S+0oZJECLjQl54oNj9JiNB6O/lBub0gDHxpP7aoiFgcxyqXF8TvCbtkDah7vZu+n8" +
                "3Y3YvUbiW/xnW1LcJnLpxZYCGzIl1kc1iW8FOizLBnM8Wc7/mIke5neHmJyRZ+V+FvDd/Wz29m45m3bA" +
                "Lw+BHSmCtWFLpBz24G/gfo8Wswpwsgl8escJoqdYB2WWiP94DfAHk0QVGsOhKteWGMEEv0MB0YsluQLV" +
                "Z7kVBLpsKS9+n0xms2mP8qtDyhsgS5UbYtq+VqzCquY+cEyIU2HGP7+/3+vCYb4/Eiat4tF1HW255340" +
                "kq7pk9KwK3yFMlhJY2t08RP07me/zSY9fiPxw8f0HP1NKpxwQCyoqg7P7fL1pzmmpCR6asTsgtXok0GC" +
                "KXcIdGpTPkpr9KkDtM7rKmUkXv8PzuusV1YhFuHefF3yOoUn49vbfSWPxI/nEkwJVxUdZXiOusjJx9k6" +
                "JF2ujCv4UuPrI/S7QGRC+uAQfZu8+QyHOE9mNsVB+TUB+No44Ynb94tlH2okfoqA425YaW8PIAmNrDEI" +
                "tRNPJwGjDJspwMPgVkfd0jNqzzN2xWqzpBuD4x8blZLB2NpqE+cRXohScFy33WUFMu1FxTUm9tdH3KIp" +
                "rbOMZWwXBXr6coPVkatsPk0aBzQjSCuS56kynifeyZB0kxvMFvE+7rWU6A7SPAvN4+hSt3fMc52wn0r2" +
                "D07J42qFHkNUrJEra7G7N65uCKE76J31YEly3FIio/6o0PJHd2nHC7Ri0NseZmFFpFPMvOxG7MB8VduA" +
                "cdJ7mVGTGr8mZVZG7YohMvDDFp1nvWYBSBV1LAr0OYNVw13yeAhJPgA0lujVlAsAAA==";
                
    }
}
