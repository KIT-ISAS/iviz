/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/GripperTranslation")]
    public sealed class GripperTranslation : IDeserializable<GripperTranslation>, IMessage
    {
        // defines a translation for the gripper, used in pickup or place tasks
        // for example for lifting an object off a table or approaching the table for placing
        // the direction of the translation
        [DataMember (Name = "direction")] public GeometryMsgs.Vector3Stamped Direction { get; set; }
        // the desired translation distance
        [DataMember (Name = "desired_distance")] public float DesiredDistance { get; set; }
        // the min distance that must be considered feasible before the
        // grasp is even attempted
        [DataMember (Name = "min_distance")] public float MinDistance { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GripperTranslation()
        {
            Direction = new GeometryMsgs.Vector3Stamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GripperTranslation(GeometryMsgs.Vector3Stamped Direction, float DesiredDistance, float MinDistance)
        {
            this.Direction = Direction;
            this.DesiredDistance = DesiredDistance;
            this.MinDistance = MinDistance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GripperTranslation(ref Buffer b)
        {
            Direction = new GeometryMsgs.Vector3Stamped(ref b);
            DesiredDistance = b.Deserialize<float>();
            MinDistance = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GripperTranslation(ref b);
        }
        
        GripperTranslation IDeserializable<GripperTranslation>.RosDeserialize(ref Buffer b)
        {
            return new GripperTranslation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Direction.RosSerialize(ref b);
            b.Serialize(DesiredDistance);
            b.Serialize(MinDistance);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Direction is null) throw new System.NullReferenceException(nameof(Direction));
            Direction.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Direction.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/GripperTranslation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b53bc0ad0f717cdec3b0e42dec300121";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VwW7UQAy9R+o/WOqBFrWLBIhDJW4I6AGpUhHXyptxkqHJTJhx2oav53mym20FQhyA" +
                "1UrJztjPfvaz95icND5IJiZNHHLP6mOgJibSTqhNfhwlndGUxZEPNPr6dhoJ12PPtZByvs1H1XHxkAce" +
                "xl7Ke+8b9aElDhS3X6VWik1jUXgLCxjwOKbIdWdGFmq5aHbIOD2qDNeunE8AsLxis9geUj2qWomDaJpv" +
                "htzmF19gGdOra0UqSHl1fYQmGYfuCV/ns3Ko5ahq+sj66uXe6uZws/cf/MEeB6w0TFlpK1THkL0TA2+E" +
                "szdCWwElsxPzbxPnkXwmuZNArCrDqOIOYQH+KGT19i9/qk/XHy7odxVDkp87JJhkTJIlqEljZ0L3Xjvc" +
                "NKBo5OsYk/OBFX1LPAiajbL6QbJhVR+FUQzqyqPaY9yV57+iltUtrJbgYANewXFyBMrsWLlorPNtJ+m8" +
                "Rx96ynux2K3Oo+TNvgz4thIkcd/PyxBoBO9hmIKvjfhKd+8PTwiEaeSkvp56Tj/VydDxzfJtKnW8fHdR" +
                "tCP1pB4JzUCokykIs3H5jqrJBxMHHKrjz/fxHD+lRWnX4IsOTVcP1jbLk/MFYjxfyG2AjeIIorhMJ+Xs" +
                "Bj/zKSEIUpAx1h2dIPOrWTtMhCn9jpMvUwngGhUA6jNzenb6CDkU6MAh7uEXxEOMP4ENK65xOu/Qs97Y" +
                "56lFAWGIZXGH2XK0nQtI3XuIE2tmmzjNlXktIavj90WLau0rHcGTc461RwNc0XCVNRl66caNd/910H45" +
                "YctUmHKaJGAyYrluTCSXpa0xQBSDMBhDf6snHNcFtwEq5tK2zRl5JRex1UNUYAx8C0hBjc0bixdgT/c9" +
                "juFyIpt2c0b3ne0msyoL3LIoM+BrSr71u82JQMPqvK6IM9LmJWrc90vOSzA0DCApanE43dBlQ3Oc6N4I" +
                "4SXtRi/aFt3nVSSiMZY/nx3E04JeRQwCypIzt1BTyIqh31TLMn3zmh7Wt3l9+179AABrk5L0BgAA";
                
    }
}
