/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            Id = string.Empty;
            PostPlacePosture = new TrajectoryMsgs.JointTrajectory();
            PlacePose = new GeometryMsgs.PoseStamped();
            PrePlaceApproach = new GripperTranslation();
            PostPlaceRetreat = new GripperTranslation();
            AllowedTouchObjects = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public PlaceLocation(string Id, TrajectoryMsgs.JointTrajectory PostPlacePosture, GeometryMsgs.PoseStamped PlacePose, GripperTranslation PrePlaceApproach, GripperTranslation PostPlaceRetreat, string[] AllowedTouchObjects)
        {
            this.Id = Id;
            this.PostPlacePosture = PostPlacePosture;
            this.PlacePose = PlacePose;
            this.PrePlaceApproach = PrePlaceApproach;
            this.PostPlaceRetreat = PostPlaceRetreat;
            this.AllowedTouchObjects = AllowedTouchObjects;
        }
        
        /// Constructor with buffer.
        internal PlaceLocation(ref Buffer b)
        {
            Id = b.DeserializeString();
            PostPlacePosture = new TrajectoryMsgs.JointTrajectory(ref b);
            PlacePose = new GeometryMsgs.PoseStamped(ref b);
            PrePlaceApproach = new GripperTranslation(ref b);
            PostPlaceRetreat = new GripperTranslation(ref b);
            AllowedTouchObjects = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlaceLocation(ref b);
        
        PlaceLocation IDeserializable<PlaceLocation>.RosDeserialize(ref Buffer b) => new PlaceLocation(ref b);
    
        public void RosSerialize(ref Buffer b)
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
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (PostPlacePosture is null) throw new System.NullReferenceException(nameof(PostPlacePosture));
            PostPlacePosture.RosValidate();
            if (PlacePose is null) throw new System.NullReferenceException(nameof(PlacePose));
            PlacePose.RosValidate();
            if (PrePlaceApproach is null) throw new System.NullReferenceException(nameof(PrePlaceApproach));
            PrePlaceApproach.RosValidate();
            if (PostPlaceRetreat is null) throw new System.NullReferenceException(nameof(PostPlaceRetreat));
            PostPlaceRetreat.RosValidate();
            if (AllowedTouchObjects is null) throw new System.NullReferenceException(nameof(AllowedTouchObjects));
            for (int i = 0; i < AllowedTouchObjects.Length; i++)
            {
                if (AllowedTouchObjects[i] is null) throw new System.NullReferenceException($"{nameof(AllowedTouchObjects)}[{i}]");
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
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceLocation";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "f3dbcaca40fb29ede2af78b3e1831128";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVX32/bNhB+119BwA9NBscF2mEPAfZQoFuXAQMyNNhLEQi0eLLZSKRKUna8v37fHSVZ" +
                "TrumQJcsCGBJvDvedz8+HhfqjXK6JVX7oNLWRrUJOnZFTMG6jbKmKBbqZkvKukTB6UZ1PqY+kPI15Elt" +
                "tTODMg26C5axyXoXFa9SjfWEZ2j1kUyRgv5IVfLhULZxE1/+7mH9ZvooW5Rdoysqh91GL0bD4+7kzAXM" +
                "i96pFypQo5PdkUpeabzVFMhVABoYLuydpa1OCoh1s9eHqGJHla0tweEm0n4L+aVyHiIuR6alGPWGzosN" +
                "+ZbS6P21j/Q+6baD5uT05LHuuuB1tVWtZ8eLd8F2HQWgdZEdBJYuQEUUR+FROWAXgpNf0T2GahBmXY0A" +
                "dbyOfDU2Jg6XX8ekq4YiwMDknlOH6ERqtUu2AkrEr81W9dr3SeyYLF3B4ppD2VdbMi+7PvJP63fALOEh" +
                "Vfk+RCkLiT+qZyiiD7eIcOP3ZErRL/2aEx2L4uf/+K/44/27S/VIdRW/kTYU1FZ+jj5+ZLGSeyEWD1Su" +
                "+RUiHf8+md8xmexwdhDhR1U5o4NB5SVtdNJS4lu7QW1eNLSjBkq58GQ1HTqKK6kdlCv3MjkKCP5B+o47" +
                "ofJt2ztb6YRsWmCd60MTydSq0wEV0Tc6QN4HYx2LS+OwdfxH+tRLN129vYSMi1T13GzYyboKZRiZPa7e" +
                "qqJHyF6/YoVicbP3F8wjG4R/2jwXGJylezRCZD91vMQeP2RwK9hGcAi7mKjO5FuJ13iusAlcoM6jvc7g" +
                "+fUhbX2uxp0OVq8b8FZE8TYNrL5gpRfnM8vs9iXoz/nRfLZ43ONbzLrJLmO6YEJsGH3sNwggBNHTO2sg" +
                "uj7kTmksuYTGXAeNemStvGWx+FXISdhGMsLsFKOvLBJg1N6m7cjMko2S+fl/6SJpCUD+hantKJtbZKJS" +
                "JBUuI9vTgfBhqVAlAJSwihddVdSgRmXx9hYW/al0PjxuJ3Kf7YVyBifRPdceGa7MN00zo/GdbnrKx87A" +
                "UZGji4KGR5p5kHLXywmIhnHqAcoVoBdF3XidfvpRCGBwbPbtCGf28QTW7HtGU5g+L0nFlHXwbYkCwMIT" +
                "JfNfDyyOmeJ3qa3ZKfmw7/NRMPbsAw4VA/nQey7/xfFAzBjopRxMHD3sBOexDoRsdzgWl0x5/NkM63l+" +
                "YDg+2FF3pQCCK2EUKP7sgT04sXuUey6AubmExkFMSVuXy3U+/4CnxeUTuGOtqfvp6TA9/f087h9DN2KY" +
                "EsUD4Tyep87z26dj3HkeWRWPIBqf9k+FjYccmzKyzwcwIDRUW8c8w9w0DWbHYVRUlvkEBsDOVnd9JzTH" +
                "Y5tKOt5FmGEFukdz4XTh58bWiYmeAyYDE3LOSU9y/kBgHBZZSJhRFurBMM9ffCRhwdgA9dnUPPPzwSz7" +
                "l/De63GcnTQnUxTxyZwgNRgwNTgjpwJn/SBUTguDcgv047d86rc9ZlNwOE8QOCHZcs2zAwNZE6CwHPd6" +
                "Hul5SNgRmjclajtw/rQlTM+2e5pC+FqkvlToahD5Lmodbezk91mhfRFT9uO0bUGeC3UlM5x3mABbwo2C" +
                "T+dJE4pTKa34ZhMkuUtlkzIevYNrFmy0+o4vJBioFF/aug7GTrsKn6FyRqvNaqlwRUMtsJT0CXshAy8u" +
                "M8Fu7FClx1uNXAQHcEuV6lfIDAYG8Tlvlost+ExO5yt1VauD79WeAeEhDHO2DB6jX9J2yXtp8bFeP+fz" +
                "8fqI2MWE5D7KbP8ALvA/HpsPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
