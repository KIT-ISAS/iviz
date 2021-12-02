/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Grasp : IDeserializable<Grasp>, IMessage
    {
        // This message contains a description of a grasp that would be used
        // with a particular end-effector to grasp an object, including how to
        // approach it, grip it, etc.  This message does not contain any
        // information about a "grasp point" (a position ON the object).
        // Whatever generates this message should have already combined
        // information about grasp points with information about the geometry
        // of the end-effector to compute the grasp_pose in this message.
        // A name for this grasp
        [DataMember (Name = "id")] public string Id;
        // The internal posture of the hand for the pre-grasp
        // only positions are used
        [DataMember (Name = "pre_grasp_posture")] public TrajectoryMsgs.JointTrajectory PreGraspPosture;
        // The internal posture of the hand for the grasp
        // positions and efforts are used
        [DataMember (Name = "grasp_posture")] public TrajectoryMsgs.JointTrajectory GraspPosture;
        // The position of the end-effector for the grasp.  This is the pose of
        // the "parent_link" of the end-effector, not actually the pose of any
        // link *in* the end-effector.  Typically this would be the pose of the
        // most distal wrist link before the hand (end-effector) links began.
        [DataMember (Name = "grasp_pose")] public GeometryMsgs.PoseStamped GraspPose;
        // The estimated probability of success for this grasp, or some other
        // measure of how "good" it is.
        [DataMember (Name = "grasp_quality")] public double GraspQuality;
        // The approach direction to take before picking an object
        [DataMember (Name = "pre_grasp_approach")] public GripperTranslation PreGraspApproach;
        // The retreat direction to take after a grasp has been completed (object is attached)
        [DataMember (Name = "post_grasp_retreat")] public GripperTranslation PostGraspRetreat;
        // The retreat motion to perform when releasing the object; this information
        // is not necessary for the grasp itself, but when releasing the object,
        // the information will be necessary. The grasp used to perform a pickup
        // is returned as part of the result, so this information is available for 
        // later use.
        [DataMember (Name = "post_place_retreat")] public GripperTranslation PostPlaceRetreat;
        // the maximum contact force to use while grasping (<=0 to disable)
        [DataMember (Name = "max_contact_force")] public float MaxContactForce;
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of grasping
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects;
    
        /// Constructor for empty message.
        public Grasp()
        {
            Id = string.Empty;
            PreGraspPosture = new TrajectoryMsgs.JointTrajectory();
            GraspPosture = new TrajectoryMsgs.JointTrajectory();
            GraspPose = new GeometryMsgs.PoseStamped();
            PreGraspApproach = new GripperTranslation();
            PostGraspRetreat = new GripperTranslation();
            PostPlaceRetreat = new GripperTranslation();
            AllowedTouchObjects = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public Grasp(string Id, TrajectoryMsgs.JointTrajectory PreGraspPosture, TrajectoryMsgs.JointTrajectory GraspPosture, GeometryMsgs.PoseStamped GraspPose, double GraspQuality, GripperTranslation PreGraspApproach, GripperTranslation PostGraspRetreat, GripperTranslation PostPlaceRetreat, float MaxContactForce, string[] AllowedTouchObjects)
        {
            this.Id = Id;
            this.PreGraspPosture = PreGraspPosture;
            this.GraspPosture = GraspPosture;
            this.GraspPose = GraspPose;
            this.GraspQuality = GraspQuality;
            this.PreGraspApproach = PreGraspApproach;
            this.PostGraspRetreat = PostGraspRetreat;
            this.PostPlaceRetreat = PostPlaceRetreat;
            this.MaxContactForce = MaxContactForce;
            this.AllowedTouchObjects = AllowedTouchObjects;
        }
        
        /// Constructor with buffer.
        internal Grasp(ref Buffer b)
        {
            Id = b.DeserializeString();
            PreGraspPosture = new TrajectoryMsgs.JointTrajectory(ref b);
            GraspPosture = new TrajectoryMsgs.JointTrajectory(ref b);
            GraspPose = new GeometryMsgs.PoseStamped(ref b);
            GraspQuality = b.Deserialize<double>();
            PreGraspApproach = new GripperTranslation(ref b);
            PostGraspRetreat = new GripperTranslation(ref b);
            PostPlaceRetreat = new GripperTranslation(ref b);
            MaxContactForce = b.Deserialize<float>();
            AllowedTouchObjects = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Grasp(ref b);
        
        Grasp IDeserializable<Grasp>.RosDeserialize(ref Buffer b) => new Grasp(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            PreGraspPosture.RosSerialize(ref b);
            GraspPosture.RosSerialize(ref b);
            GraspPose.RosSerialize(ref b);
            b.Serialize(GraspQuality);
            PreGraspApproach.RosSerialize(ref b);
            PostGraspRetreat.RosSerialize(ref b);
            PostPlaceRetreat.RosSerialize(ref b);
            b.Serialize(MaxContactForce);
            b.SerializeArray(AllowedTouchObjects);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (PreGraspPosture is null) throw new System.NullReferenceException(nameof(PreGraspPosture));
            PreGraspPosture.RosValidate();
            if (GraspPosture is null) throw new System.NullReferenceException(nameof(GraspPosture));
            GraspPosture.RosValidate();
            if (GraspPose is null) throw new System.NullReferenceException(nameof(GraspPose));
            GraspPose.RosValidate();
            if (PreGraspApproach is null) throw new System.NullReferenceException(nameof(PreGraspApproach));
            PreGraspApproach.RosValidate();
            if (PostGraspRetreat is null) throw new System.NullReferenceException(nameof(PostGraspRetreat));
            PostGraspRetreat.RosValidate();
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
                int size = 20;
                size += BuiltIns.GetStringSize(Id);
                size += PreGraspPosture.RosMessageLength;
                size += GraspPosture.RosMessageLength;
                size += GraspPose.RosMessageLength;
                size += PreGraspApproach.RosMessageLength;
                size += PostGraspRetreat.RosMessageLength;
                size += PostPlaceRetreat.RosMessageLength;
                size += BuiltIns.GetArraySize(AllowedTouchObjects);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/Grasp";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e26c8fb64f589c33c5d5e54bd7b5e4cb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVY32/bNhB+119BJA9NCscdumEP2fpQoFuXAds6NNgeisCgpZPMhiJVkrLj/fX7jhRl" +
                "2U7bFVuyIIAl8ni/ePfdnU7F9Up50ZL3siFRWhOkMl5IUZEvneqCskbYGguNk74TYSWD2NheV2JJovdU" +
                "Fadio8IKFJ10QZW9lk6QqS6orqkM1olgh8MSrJbvsTgTypS6r5RpxMpuQAEusuucleVKKOw3kB0fKJRz" +
                "sa9lZckLY0NWF3y3OK9MbV0ro8ZyafsAlU6S4M4qE07EGXS0XkWK336FLTTocz7H+T9hGq3JiYYMOTx7" +
                "UEzE+lU0eyXXJKR2JKstNGiXykQnHIufyPbJR8c0rENDtqXg2AZ4mlcO3QcxXR8oUTPXBewgsNvTcF6A" +
                "w0thZEsCctJeJC98cOxrVTHJNbhAJ3JGanZI6B1lyStpquEwic7RRToPzYzejt5DgLjh9oOT7EHrtovW" +
                "N/7Zz2zu9bjIPBajyizpizTI0ieCsQvXWBe+QIl7FRhD4T6n7ymQAxD/0S3se1uDDb+dIOzJhIVW5vbk" +
                "Pl6zGKuyDL3U8OGEwxC4fFI8Vebp0VEWvO1UORyE/DH1pmzwDDYtzBOV8gE+3Tj8JsZLgi20c+3ZVMB5" +
                "pPEgaqSZFzkSkxffgP3bINuOqknUZfeRDwqhjD2k7VIulVZhy9r4viwRkAchOBN49WAvLFRxrC9JP9w7" +
                "Y8BJY211gpyHm+dFra0M334zyP0A14F7Fj0iRaUczOA7RJIEeUvZWvjslgN+RJziNQClI4egMF6nDNyF" +
                "ZmaYBTg4gQB0x/xljagd0XAl2XVkYoJqYmecJXkcKzIE8KTq/F7huK1B+iDtUHhrs2QcZdwQmxVEOdJw" +
                "HBu3w6/vkqMn8MKAlEDSEN+GRBbsxTQc7UnXM7EECn2U8WwI8ilwbZTWHIEj43lUO3HldJyqLONV9F3S" +
                "B6b1DnAp4DeuFjldHPleA+y9PTIkOnItlZZLnWCNMwZx51jW/KOu7bQsaepaltPKO9X2bSocuCSwK5Ea" +
                "llnBCUoPZrATzr5/8RVvIaNY9HkKya+fM5PFwGARGTB3DrRYLJF8mnMPltklcrHUsY5w1aRUOzy10qBQ" +
                "HleDyKdK1CU4cprbnkPoWdd7/mntGt6LuM/VuncJALLSA86/u0GB0nZD1SKeX6TL9EXx4j/+K355+/pS" +
                "fAZ+i59QK3Ffq/iz0/E9ky24XPni4MgbfgVJKp4PpbcPVVI4KQj3A+5MJV0FcAqykkHGiFupBpB1odEd" +
                "aBxKiBh3w7YjoNXpWB9S78BwnTMB0ND2BhjO9VvB1un52Dfst06ltQ6NEZPXDr5h7vj39KEng2i9enXJ" +
                "8eup7IOCQlvupdyQuVevRNHDZQhTHChOrzf2ggttA/ePwlOAQVm6AwR61lP6S8h4moybgzecQ5BSeXEW" +
                "1xZ49ecCQqACdRbgewbN32zDyqZoXEunYo6CMRcscH3Ch56cTziz2pfoUIzN7BPHnYx/wtaMfNmmCy5r" +
                "KGMNKk8DB4IQaL5WFUiXqd6WWqFEIzGXDnhV8Kkksjj9kX2cUCfeCMON97ZUsbRx15abp3gbC26h/pcs" +
                "iikBk3/gwrejTSkifEelqhWwhqAybnvsmN7NBKIEBgXs4kWiOmvub3nz5oZbu33q1F3djOViIgvhDEyi" +
                "O449qjgyX6IWZNkVLkv3lPqyAaM8excBzUUzdU8x62OTyjgvDqycw/QiV/8IAINik7WdOZPFPbMm68ma" +
                "ourTVoyYRe1su0AAYOOBLvOjnVRs0fk9TQSOakL7iBQ4zPtUCnLOHmBoZJC6scfSPyruiBEDuZScidKT" +
                "B5HaEW67Q9GdxW4Iy9Wwn7psNsc6lc/OBYzgSMgExe89V3UT+e7oHsvAlFwRxscpOBxMCTw+ssp75o6t" +
                "6t34tB2f/noc9XeuyzaMF8UT09Sf+8rz24ed37kfwRT5aYvy0+ahbOMmR4Vk2XF7BwsrqjF081eKMGn7" +
                "du1tPDJLFRgGpg40whw3hWjk/a0HGz5Ad0iuobHUqg57QwPunC89xPoDgjwm5B45bdQDY+6/hj5zNzcM" +
                "De5Ez4Mh64+Ie1/nOWs8ObIij6Vqz9I45AEzxpZ0IFqMG7nfhfV5LVX9tkdvCgznDgIVkjnX3DuwIbtJ" +
                "EceHIQH1ZI3ZAKMMtR0wf9cFKzMR9zCB8ClP3RfoYiD5V9Caeazj76Oadq9NSY/9tAV4noqr2MPF7zIY" +
                "pQFLqM7jSf6Cl0MpzmcuXi4+vIXx+xnP4DzTgh4YjtOIbzDbzyos48gZzZv5LM2JkSrmCWsRG14MM041" +
                "aojS3VTDPPOlzESon6fhMeqchKVgczaB0/lcXNVia3uxYYPw4IY+OzYeWa+YdsHamOI5Xo/xfPxyBywP" +
                "uNzPItvfFcqX7Q4VAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
