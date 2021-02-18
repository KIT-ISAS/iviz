/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceLocation")]
    public sealed class PlaceLocation : IDeserializable<PlaceLocation>, IMessage
    {
        // A name for this grasp
        [DataMember (Name = "id")] public string Id { get; set; }
        // The internal posture of the hand for the grasp
        // positions and efforts are used
        [DataMember (Name = "post_place_posture")] public TrajectoryMsgs.JointTrajectory PostPlacePosture { get; set; }
        // The position of the end-effector for the grasp relative to a reference frame 
        // (that is always specified elsewhere, not in this message)
        [DataMember (Name = "place_pose")] public GeometryMsgs.PoseStamped PlacePose { get; set; }
        // The approach motion
        [DataMember (Name = "pre_place_approach")] public GripperTranslation PrePlaceApproach { get; set; }
        // The retreat motion
        [DataMember (Name = "post_place_retreat")] public GripperTranslation PostPlaceRetreat { get; set; }
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of grasping
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceLocation()
        {
            Id = string.Empty;
            PostPlacePosture = new TrajectoryMsgs.JointTrajectory();
            PlacePose = new GeometryMsgs.PoseStamped();
            PrePlaceApproach = new GripperTranslation();
            PostPlaceRetreat = new GripperTranslation();
            AllowedTouchObjects = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceLocation(string Id, TrajectoryMsgs.JointTrajectory PostPlacePosture, GeometryMsgs.PoseStamped PlacePose, GripperTranslation PrePlaceApproach, GripperTranslation PostPlaceRetreat, string[] AllowedTouchObjects)
        {
            this.Id = Id;
            this.PostPlacePosture = PostPlacePosture;
            this.PlacePose = PlacePose;
            this.PrePlaceApproach = PrePlaceApproach;
            this.PostPlaceRetreat = PostPlaceRetreat;
            this.AllowedTouchObjects = AllowedTouchObjects;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlaceLocation(ref Buffer b)
        {
            Id = b.DeserializeString();
            PostPlacePosture = new TrajectoryMsgs.JointTrajectory(ref b);
            PlacePose = new GeometryMsgs.PoseStamped(ref b);
            PrePlaceApproach = new GripperTranslation(ref b);
            PostPlaceRetreat = new GripperTranslation(ref b);
            AllowedTouchObjects = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceLocation(ref b);
        }
        
        PlaceLocation IDeserializable<PlaceLocation>.RosDeserialize(ref Buffer b)
        {
            return new PlaceLocation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            PostPlacePosture.RosSerialize(ref b);
            PlacePose.RosSerialize(ref b);
            PrePlaceApproach.RosSerialize(ref b);
            PostPlaceRetreat.RosSerialize(ref b);
            b.SerializeArray(AllowedTouchObjects, 0);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += PostPlacePosture.RosMessageLength;
                size += PlacePose.RosMessageLength;
                size += PrePlaceApproach.RosMessageLength;
                size += PostPlaceRetreat.RosMessageLength;
                size += 4 * AllowedTouchObjects.Length;
                foreach (string s in AllowedTouchObjects)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceLocation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f3dbcaca40fb29ede2af78b3e1831128";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VXTWskNxC9N8x/EPiwdhjPwm7IwZDDwiYbBwIOu+SymEHTqp7RulvqldQznvz6vCr1" +
                "1zhOHEjsGMN0q6tK9epLT2fqnXK6IVX5oNLORrUNOraLIqZg3VZZsygWxZn6tCNlXaLgdK1aH1MXSPkK" +
                "KqR22plenwb1MxayyXoXFX+mCgIJz1DrIsFqCvoLlcmH47qJ2/j6Zw/7n8ZF2WTd1rqkdb/f5Mlge/CA" +
                "nLnEDqJ56okKVOtk96SSVxpvFQVyJfAGRs0Gz9NOJwXkuj7oY1SxpdJWluB0Hemwg8JSOQ8RlyPUUIx6" +
                "SxeLYku+oTQguPGRPibdtFAdHZ95rds2eF3uVOPZ+UXxIdi2pQDQLrKXANQG6kEP0pN+wFYEV/9OfQpa" +
                "L53VNULVsgSyV9uYOHB+E5Mua4pKAnDgRCJOkRrtki0BF5Fssl298V3KhkwWL2Fyw1Htyh2Z120X+afx" +
                "e4CXQJEqfReiVImkAuU0lNXnWwS79gcyazGw9htOe1wUxff/8V/xy8cPV+qJYit+Im0oqJ38TE5+YbE1" +
                "90csHqjc8CtEWv6Nz+V3TCY7nB1EAlBgzuhgUIVJG5201PvOblGnlzXtqYZSrkH5mo4txZVUEEqX+5sc" +
                "BUT/KH3IbVH6pumcLXVCPi2wzvWhiXRq1eqAouhqHSDvg7GOxaWL2Dr+I33tpLWu319BxkUqO+487GRd" +
                "iVqMPFCu36uiQ8jevmGF4uzTwV/yYNki/OPmamhJukdDRPZTxyvs8U0Gt4JtBIewi4nqXNbWeI0XCpvA" +
                "BWo9+uwcnt8c087netzrYPWmJjZcIgKw+oqVXl3MLDsx7bTzg/lscdrjn5h1o13GdMkTsmb0sdsigBBE" +
                "b++tgejmmHultuQSenMTNOqRtfKWxdmPMqlk8khGeFLF6EuLBBh1sGk3DGvJxtqa/6mLpCUA+QeecWk+" +
                "ybE+jlUkFS4j2+MB8XmpUCUAlPAVL7osqUaNysfbW1j0p9L5MLkdJ/1sL5QzphLdc+2R4cp8V9ezkb7X" +
                "dUf5GOqnVOTooqDhkY6yIl0vp2JkoQcoV4BeFFXtdfruWxkAvWOztQnObPEE1mw9oylMlz9Jxayr4Js1" +
                "CgAfnimZf3l2ccwUv0ttzY7Mh32fD4OhZx/MUDHA59/L+S+OB+KJgV7SA0FgJziPVSBku8XZuOSRx8um" +
                "/57JBMPxwQ66K1VIRY8Cxa+dZgYkdie5lwKYm0vGOAZT0tblcp2TId032wncodbU/fh0HJ9+fxn3p9AN" +
                "GMZERWEnUzxPnee3r1PcmZKsiicQDU+H58LGNMemjOzPLAwIDVXW8Zzh2TSys4mZisoyn8AA2Nryrmtl" +
                "zDF3U0nHu8h0izXoHt1VZ4Ze2yrxpOeICWVC0jnrSQ4gCAyskYVkNMqHqrcsFIzt8idjAwzMWPTM1YfU" +
                "9jeZfW8HdjuqzqxRxKI5wWvANDUmxyJnBEd+L7Wevgz6jZ3k8/HfdOCpGxIqgaOSjVdMIhjQhgCJ5Yj1" +
                "M9NnurAntHFK1LSJ7xjDtjA+2/JFCv40Yo8VvepF/tWYHWzs5fdFoT2KKftx2sIrZofXwue8AxtsCBcM" +
                "PqlHTSiOJbXiu06Q/C6VTcp49BGuX7DR6Du+n4BcyW2ubWHstMOwDJVzWm1XS4Wrm8tS0jLshZBf3G2C" +
                "3dq+VqdLjtwQe3BLlao3yAzIg/icN0O9wUjweVBdrNR1pY6+UwcGhIfQc24hIYNf0oHJe2n33sQjs324" +
                "ViJ2MSG5T065PwBv4Av5uw8AAA==";
                
    }
}
