/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class PlaceLocation : IDeserializable<PlaceLocation>, IMessage
    {
        // A name for this grasp
        [DataMember (Name = "id")] public string Id;
        // The internal posture of the hand for the grasp
        // positions and efforts are used
        [DataMember (Name = "post_place_posture")] public TrajectoryMsgs.JointTrajectory PostPlacePosture;
        // The position of the end-effector for the grasp relative to a reference frame 
        // (that is always specified elsewhere, not in this message)
        [DataMember (Name = "place_pose")] public GeometryMsgs.PoseStamped PlacePose;
        // The approach motion
        [DataMember (Name = "pre_place_approach")] public GripperTranslation PrePlaceApproach;
        // The retreat motion
        [DataMember (Name = "post_place_retreat")] public GripperTranslation PostPlaceRetreat;
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of grasping
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects;
    
        /// Constructor for empty message.
        public PlaceLocation()
        {
            Id = "";
            PostPlacePosture = new TrajectoryMsgs.JointTrajectory();
            PlacePose = new GeometryMsgs.PoseStamped();
            PrePlaceApproach = new GripperTranslation();
            PostPlaceRetreat = new GripperTranslation();
            AllowedTouchObjects = System.Array.Empty<string>();
        }
        
        /// Constructor with buffer.
        public PlaceLocation(ref ReadBuffer b)
        {
            b.DeserializeString(out Id);
            PostPlacePosture = new TrajectoryMsgs.JointTrajectory(ref b);
            PlacePose = new GeometryMsgs.PoseStamped(ref b);
            PrePlaceApproach = new GripperTranslation(ref b);
            PostPlaceRetreat = new GripperTranslation(ref b);
            b.DeserializeStringArray(out AllowedTouchObjects);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PlaceLocation(ref b);
        
        public PlaceLocation RosDeserialize(ref ReadBuffer b) => new PlaceLocation(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            PostPlacePosture.RosSerialize(ref b);
            PlacePose.RosSerialize(ref b);
            PrePlaceApproach.RosSerialize(ref b);
            PostPlaceRetreat.RosSerialize(ref b);
            b.SerializeArray(AllowedTouchObjects);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (PostPlacePosture is null) BuiltIns.ThrowNullReference();
            PostPlacePosture.RosValidate();
            if (PlacePose is null) BuiltIns.ThrowNullReference();
            PlacePose.RosValidate();
            if (PrePlaceApproach is null) BuiltIns.ThrowNullReference();
            PrePlaceApproach.RosValidate();
            if (PostPlaceRetreat is null) BuiltIns.ThrowNullReference();
            PostPlaceRetreat.RosValidate();
            if (AllowedTouchObjects is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < AllowedTouchObjects.Length; i++)
            {
                if (AllowedTouchObjects[i] is null) BuiltIns.ThrowNullReference(nameof(AllowedTouchObjects), i);
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.GetStringSize(Id);
                size += PostPlacePosture.RosMessageLength;
                size += PlacePose.RosMessageLength;
                size += PrePlaceApproach.RosMessageLength;
                size += PostPlaceRetreat.RosMessageLength;
                size += BuiltIns.GetArraySize(AllowedTouchObjects);
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/PlaceLocation";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "f3dbcaca40fb29ede2af78b3e1831128";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71X32/cNgx+918hIA9NhssVaIc9BOhDgW5dBgzI0GIvRWDoLPpOjS25knyX21+/j5R/" +
                "Xdo1A7YkCHC2RVL8yI8UdabeKqdbUrUPKu1sVNugY1fEFKzbKmuK4kx93JGyLlFwulGdj6kPpHwNeVI7" +
                "7cygTIPuGcvYZL2LilepxnrCM7T6SKZIQX+mKvlwLNu4jS9/87D+cfooW5Rdoysqh91GL0bD4+7kzCXM" +
                "i96pFypQo5Pdk0peabzVFMhVABoYLuydp51OCoh1c9DHqGJHla0tweEm0mEH+ZVyHiIuR6alGPWWLoot" +
                "+ZbS6P2Nj/Qh6baD5uT05LHuuuB1tVOtZ8eL98F2HQWgdZEdBJYu0IB2FB6VA3YhOPkd3TlUgzDragSo" +
                "43Xkq7Excbj8JiZdNRSV4D5w6hCdSK12yVZAifi12are+D6JHZOlK1jccCj7akfmZddH/mn9HpglPKQq" +
                "34cotJD4gz0DiT7dIsKNP5ApRb/0G050LIo3//Nf8fuH91fqEXYVv5I2FNROfmYfP7NYybUQiwcqN/wK" +
                "kY5/n8zvmEx2ODuI8INVzuhgwLykjU5aKL6zW3DzsqE9NVDKxJPVdOworoU7oCvXMjkKCP5R6o4rofJt" +
                "2ztb6YRsWmBd6kMTydSq0wGM6BsdIO+DsY7FpXDYOv4jfemlmq7fXUHGRap6LjbsZF0FGkbuHtfvVNEj" +
                "ZK9fsUJx9vHgL7mPbBH+aXM1liHdoxAi+6njFfb4IYNbwzaCQ9jFRHUu30q8xguFTeACdR7ldQ7Pb45p" +
                "5zMb9zpYvWmIDVeIAKy+YKUXFwvLTkw77fxoPluc9/g3Zt1klzFdckNsGH3stwggBFHTe2sgujnmSmks" +
                "uYTC3AQNPrJW3rI4+0Wak3QbyQh3pxh9ZZEAow427cbOLNkouT8/DRsfqSIpCUD+mVtbWrZufJ9aKZIK" +
                "l5Ht6UD4tFJgCQAlrOJFVxU14Kgs3t7Coj+VzofH7dTcF3uBzuhJdM/cI8PMfNs0iza+101P+dgZelTk" +
                "6ILQ8EhH+SJVLydgZKEHKNeAXhR143X66UdpAINji28znMXHE1iL7xlNYfq8JIwp6+DbEgTAwhMl8x8P" +
                "LI6Z4nfh1uKUfFj3+SgYa/ZBDxUD+dB7Lv/F8UDcMVBLepwJ2AnOYx0I2e5wLK645fFnM6zn+YHh+GBH" +
                "3bUqhNGTQPFHr3ngEbuz3HMBzMUlbRyNKWnrMl2X848eiu0E7sg1dT89Haenv57H/Tl0I4YpUVFGkzme" +
                "p87z25c57jyPrItHEI1Ph6fCxkOOTRnZ1wMYEBqqreM+w71pGszmYVRUVvkEBsDOVnd9J22OxzaVdLyL" +
                "MMMKdI/iavIw3tg6caPngMnAhJxz0pOcPxAYh0UWks4oC/VgmOcvPpKwYGyA+mJqXvj5YJb9U/re63Gc" +
                "nTQnUxTxyZwgNRgwtRuph7N+ECqnhUG5tbNwPvXbHrPphmSCwAnJlmueHRjIhgCF5bjW80jPQ8KeULwp" +
                "Uduh509bwvRiu6chwvci9S2iq0HkP7XW0cZefp8V2jcxZT9Oy3bNE+G1zHDeYQJsCTcKPp0nTShOVFrz" +
                "zSZIclfKJmU8agfXLNho9R1fSDBQyaWt62DstKrwGSrntN6uVwpXNJelpE7YCxl4cZkJdmsHls63GrkI" +
                "DuBWKtWvkBkMDOJz3iyTLfjcnC7W6rpWR9+rAwPCQxjmbBk8Rr+k7JL3UuIjX7/u5+P1EbGLCcl9tLP9" +
                "DS7wPx6bDwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
