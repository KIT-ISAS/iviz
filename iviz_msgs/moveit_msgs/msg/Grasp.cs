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
            b.SerializeArray(AllowedTouchObjects, 0);
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
                "H4sIAAAAAAAAE71YS2/bRhC+81cs7EPsQFaKtOghbQ4B0qYp0DZFjPYQBMKKHEqbkLvM7lKy+uv7zSyX" +
                "pGSnadA6hgHxMe+d+WaG5+p6a4JqKQS9IVU6G7WxQWlVUSi96aJxVrkaDzZeh07FrY5q7/qmUmtSfaCq" +
                "OFd7E7eg6LSPpuwb7RXZ6orqmsrovIpuYNYQtX6HhwtlbNn0lbEbtXV7UECK7jrvdLlVBu830C0XFMul" +
                "OraychSUdTGbC7kH8BtbO99qsVivXR9h0llS3Dlj45m6gI0uGKH47Vf4QoM9l0vw/wnXaEdebciSx3UA" +
                "xUxt2IrbW70jpRtPujrAgnZtrAThtvqZ7pBidJuGbdiQayl69gGR5ien4YOaro+UqFnqCn4QxB1ZuCwg" +
                "4ZmyuiVVMyO/E/IiRM+xNhWTXG+ZNZK3uuGAxN5T1rzVthqYSXWerhI/LLPNYYweEsQPpx+9fid2HlZt" +
                "2IRHP7O71+NDlrEaTWZNn2VB1j5TjLcIjfPxM4y404AxFe4K+pEBOQFNSGHh2LsaYvjuDGlPNq4aY9+f" +
                "3SVrIbmqy9jrBjGcSRgSlznVQ2Mf3mJlxYfOlAMj9I+lNxeDa4hp4Z6qTIiI6d7jNwleE3yhKbQXcwWX" +
                "QhNAtNF2WeRMTFF8BfGvo247qmZZl8NHIRqkMt6hbNd6bRoTD2xN6MsSCXmSgguF2wDxysEUz/aSDsO5" +
                "MwacbZyrzlDzCPOyqBun47ffDHo/IHSQnlWPSFEZDzf4DFEkUb+n7C1i9p4TfkSc4gUApSOPpLChSRU4" +
                "pWYWmBV4BIF0vEO+rpG1IxpuNYeOrBRoQxyMi6SPc0XHCJlUXd6pHKc1aB+0nSpvXdYMVsYNtd9ClacG" +
                "gWPnJvz6LgV6Bi8MSAkkLfFpaFTBUU4j0IGaeqHWQKGPCl4MST4Hrr1pGs7AUfBSzE5SuRznJms5ir5L" +
                "9sC13gMuFeLG3SKXi6fQNwD74G45IoHcadPodZNgjStG8ylA1/Kjoe0aXdI8tKyn1Tem7dvUOHBIEFcS" +
                "mwtRCIJpBjc4CBffP/2KX6GiWPVlSsmvH7OQ1SBgJQJYOieaNEsUX8O1B8/cGrVYNtJHuGtS6h2BWm3R" +
                "KG93A5FTJeoSErnMXc8p9KjrA/+0bofoCe5zt+59AoBs9IDzb96iQTVuT9VK+FfpMENRPP2f/4pfXr94" +
                "oj4Bv8VP6JU4r638TDa+Y7IVt6tQnLC84luQpOZ5X3aHWCWDk4EIP+DOVtpXAKeoKx21ZNzWbABZVw2m" +
                "gwZMCRHlbTx0FJZSuKk/pNmB4TpXAqCh7S0wnPu3ga9zfpkbjken0jmPwYjJa4/YsHT8B/rQk0W2vnz+" +
                "hPM3UNlHA4MOPEv5oXJfPldFj5AhTcFQnF/v3RU32g3CPypPCQZj6QYQGILU4xPoeJicW0I2gkPQUgV1" +
                "Ic9WuA2XCkpgAnUO4HsBy18d4talbNxpb6RGIZgbFqQ+YKYHlzPJVkRbbV0WnyROOv6NWDvKZZ+uuK01" +
                "7H3oNwggCIHmO1OBdJ36bdkYtGgU5toDrwrmSiqL8x85xgl15EQYbkJwpZHWxlNbHp7kNFY8Qt1PNn6i" +
                "iqQk4PIP3PjibMDi5yp0VJraAGvIcH+dJqY3C4UsgUMRb3Gj0Z0bnm/55du3PNodU6fp6u3YLma6kM7A" +
                "JLrh3KOKM/MZekHWXeGwmp7SXDZgVODoIqG5aabpSapehlTGeXXi5RKuF7n7CwAMhs2eTe7MHh65NXue" +
                "vCmqPr2SjFnV3rUrJABe3NNhfnSSkhGd79NG4KkmL3V9WvepFeSaPcFQEZCmsS9lvxjuiREDtaTz5JwX" +
                "kdoTTrtD013INOQYVtL7NGWzO86bzLtUhWT0SFD83nNXtyJ3ovtSDqbiEhgft+B4siXoodiO3B1H1Zvx" +
                "6jBe/fVlzJ9Cl30YDyrIaDLF89h4vvswxZ3nkWXxCY/y1f6+fOMhx8Tk2e3xDh5WVGPp5q8UcTb2TeOt" +
                "sCxSB4aDaQIVmOOhEIN8eB8ghhnoRvPkLteNqePR0oAz50OP0n9AkNeEPCPHcSZlwTx/DXPmtDcMA+7M" +
                "zpMl6w/Bva/znjVyjqIo4FF15KkseTanHnr9QLQaX+R510zEqeu3PWbTtXzpCeiQLLnm2YEdmTZFsA9L" +
                "AvrJDrsBVhlqO2D+NAUbO1N3P4nwT5G6K9HVQPKfoDXL2MnvF3XtTp+SHcdlu+SJ8KXMcPJdBqs0YAnd" +
                "eeTkL3g5lWQ/83K4C96v8/cz3sF5pwV9kC0I+Q1hx1WFx2C5oOVmuUh7olBJnbAVMvBimfFmY4YsnbYa" +
                "lpkPZaFi/Tgtj2JzUpaSzbsETpdL9bJWB9erPTuECz/M2TJ4ZLuk7KJzUuI5X2/j+fjlDlgecbifRLa/" +
                "ARXKl+0OFQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
