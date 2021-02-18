/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/Grasp")]
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
        [DataMember (Name = "id")] public string Id { get; set; }
        // The internal posture of the hand for the pre-grasp
        // only positions are used
        [DataMember (Name = "pre_grasp_posture")] public TrajectoryMsgs.JointTrajectory PreGraspPosture { get; set; }
        // The internal posture of the hand for the grasp
        // positions and efforts are used
        [DataMember (Name = "grasp_posture")] public TrajectoryMsgs.JointTrajectory GraspPosture { get; set; }
        // The position of the end-effector for the grasp.  This is the pose of
        // the "parent_link" of the end-effector, not actually the pose of any
        // link *in* the end-effector.  Typically this would be the pose of the
        // most distal wrist link before the hand (end-effector) links began.
        [DataMember (Name = "grasp_pose")] public GeometryMsgs.PoseStamped GraspPose { get; set; }
        // The estimated probability of success for this grasp, or some other
        // measure of how "good" it is.
        [DataMember (Name = "grasp_quality")] public double GraspQuality { get; set; }
        // The approach direction to take before picking an object
        [DataMember (Name = "pre_grasp_approach")] public GripperTranslation PreGraspApproach { get; set; }
        // The retreat direction to take after a grasp has been completed (object is attached)
        [DataMember (Name = "post_grasp_retreat")] public GripperTranslation PostGraspRetreat { get; set; }
        // The retreat motion to perform when releasing the object; this information
        // is not necessary for the grasp itself, but when releasing the object,
        // the information will be necessary. The grasp used to perform a pickup
        // is returned as part of the result, so this information is available for 
        // later use.
        [DataMember (Name = "post_place_retreat")] public GripperTranslation PostPlaceRetreat { get; set; }
        // the maximum contact force to use while grasping (<=0 to disable)
        [DataMember (Name = "max_contact_force")] public float MaxContactForce { get; set; }
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of grasping
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
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
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        public Grasp(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Grasp(ref b);
        }
        
        Grasp IDeserializable<Grasp>.RosDeserialize(ref Buffer b)
        {
            return new Grasp(ref b);
        }
    
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
            b.SerializeArray(AllowedTouchObjects, 0);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += PreGraspPosture.RosMessageLength;
                size += GraspPosture.RosMessageLength;
                size += GraspPose.RosMessageLength;
                size += PreGraspApproach.RosMessageLength;
                size += PostGraspRetreat.RosMessageLength;
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
        [Preserve] public const string RosMessageType = "moveit_msgs/Grasp";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e26c8fb64f589c33c5d5e54bd7b5e4cb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VY247bNhB9F5B/IDYP2Q28TtEUfUibhwBp0xRomyKL9iFYGLQ0splIpEJS9rpf3zND" +
                "UZL3gjRos10sYF3IMxfO5YweqoutCaqlEPSGVOls1MYGpVVFofSmi8ZZ5Wo82HgdOhW3Oqq965tKrUn1" +
                "gaoHxUO1N3GLJZ320ZR9o70iW51TXVMZnVfRDbs1sNbv8XChjC2bvjJ2o7ZujxUMo7vOO11ulcGCDaTL" +
                "BcVyqY71rBwFZV3MCgP4wADG1s63WpTWa9dHKHWSRHfO2HiiTqGlC0ZW/PYrzKFBo7MlA/wJ82hHXm3I" +
                "ksd1wJKZ4LAV07d6R0o3nnR1gA7t2tjkiJsKzKSH5Keba1iLDbmWohcz4G9+dN2HENT1kdJyhl3BFALe" +
                "kY6wgzFeKKtbUjVv5bey4UERomefmyqtutjy/kje6oYdE3tPWfxW22rYT6rzdD5AQD/bHEY3Ilh8joTo" +
                "9XtR97BqwyY8+ZnNvhgfMspq1JxlfaYWowYz4XgNJzkfP0eRO5QYQ+O2EzhSIkekCck9fBCuZhy+PUEm" +
                "kI2rxtgPJ7eBLSR6dRl73cCXM4gcyrxVPTb28Y29LPrQmXLYCQ3GfJzj4JpxWtioKhMiXLv3+E3Ia4I5" +
                "NHn4dC7hTNYELNpoi3jKsZl8+QYC3kbddlTNwnByIoVoEN54i2xe67VpTDywRqEvS8TotZhcKNwGCFAO" +
                "2njRmXQYQoCLw8nGueoEtQDehjZ143T89ptB9kd4EPiT+LGIVMbDGj5N5E7UHygbDd994CQYq9GD4hWK" +
                "TUceEWJDk1JzitWMOMnw8AbpeIsIXSOMx2q51exEspK6DbFPTpNIDhwdI1CpOrtdPg5uUGAQd1N+67Jw" +
                "bOaqovZbSPPUwINs4lTfvksunxUfqVepjFrig9FIjKMgh8sDNfVCrVGk7kRe5KifF7a9aRqOyBF5KYon" +
                "WM7RudJajqTvBo1gXe9RUBW8xy0lJ5Cn0DdoCMHdsEXcudOm0esmFT3JIc2HAWnLuz3cNbqkYw+zrFZf" +
                "mbZvU4PBcQGyJNYZaHCFaQZb2BWn3z//il8hy1j82RCiT79mlNWAsBKEJIADTzorcrLhlISBbo0ULRtp" +
                "ONxiKTWZQK22aKo3u0YCqtLyEpCc/67ngHrS9YF/WreDF6U/cG/vfaoMWfHcDd5dopc1bk/VSgBW6VzD" +
                "g6J4/h//Fb+8ffVMfaI6Fz+hr+LgtvIzKfmel624rYXi2pY3fIslqc9+Kb1DrJLCSUEcAOqgrbSvULKi" +
                "rnTUEntbs0ElO2/AJBpsSqVS3sZDR2EpWZyaR+IZXMlzUqBUtL1FeedOb2DrfL9QjGOmVTrnwaN4ee3h" +
                "G0bHf6CPPVnE7OuXzziKA5V9NFDowNTLD1n8+qUqergMsYoNxcOLvTvnTryB+0fhKcSgLF2hKIYgmfkM" +
                "Mh4n45bAhnMIUqqgTuXZCrfhTEEIVKDOoR6fQvM3h7h1KR532hvJVgBzLwPqI9706GyGbAXaausyfEKc" +
                "ZPwTWDvisk3n3PAatj70GzgQC1Hfd6bC0nXqxWVj0L6Rm2uP0lXwriSyePgj+zjVHzkRLjwhuNJIx2OC" +
                "lzmWnMbKVP9TFklKwOQfuBfGGQfj5yp0VJraoNyQ4bY7Map3C4UogUERb3Gj0bQb5sL88vKS6d/x6sS+" +
                "LsfWMZOFcEZVoiuOPao4Ml+gLWTZFQ6r6SnxtqFKBfYuApqbaKJWkvVCZrniq2tWLmF6kSmBFIBBsdmz" +
                "yZzZwyOzZs+TNUXVp1cSMavau3aFAMCLL3SYd1IsofJ8n4YHTzV5yevreZ+aQc7ZazVUAJim3Z/+orgn" +
                "rhjIJZ1pdR5Zak847Q7NdyHsyHFZSe8TBWdznDd571IVEtHjguL3ntu7Fdxp3X0ZmJJLyvg4NcdrI4Qe" +
                "ku3I3JG/Xo1Xh/Hqr/tRf3JdtmE8qCDsZPLnsfJ893HyO1OSZfEJi/LV/kvZxjTHxGTZTZoHCyuqMaDz" +
                "V404o38T1ZUti9SBYWAio1LmmByC2IcPgekW76ArzVRerhtTx6NBAofOpx6lAWFBnhwyYY4jPWVkoWCZ" +
                "cE6jxMB2Z6pen8D+kNr3NA9h49YZGgU8rI7slSHQMgfN9HRYtZrejPTXTOtT+2978NS1fCIKaJUMXjOJ" +
                "YIOmYZL3D6MDOssOEwOGHGq7yEP5yIqNnYm8l4A/9thtQa+GJf+qzGaMnfzeq2m32pT0OE7hJbPD18Ln" +
                "5DsOhm2UKHTqcSd//cshJWObl/Nd8ASev7zxlM7zLtYHmYsQ6gA7zjA8xpZTWm6WizQ/yipJGdZCyC9m" +
                "G282ZojVachhzHwoCxXrr9NMKTonYYg3gHiXCtXZUr2u1cH1as8G4cIPnFtISNZLMjA6J+k+QNxS28cv" +
                "fqjrEYf7ySr3N5rBZShKFQAA";
                
    }
}
