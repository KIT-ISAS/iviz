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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/Grasp";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e26c8fb64f589c33c5d5e54bd7b5e4cb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVY32/bNhB+F5D/gUgemhSOO7TDHrL1oUC3rgO2dWiwPRSBQUsnmw1FqiRlx/vr9x0p" +
                "SrKdriu2ZkEAS+TxfvHuuzudieu18qIh7+WKRGlNkMp4IUVFvnSqDcoaYWssrJz0rQhrGcTWdroSSxKd" +
                "p6o4E1sV1qBopQuq7LR0gkx1SXVNZbBOBNsflmC1fI/FmVCm1F2lzEqs7RYU4CLb1llZroXC/gqy4wOF" +
                "ci72tawseWFsyOqC7w7nlamta2TUWC5tF6DSaRLcWmXCqTiHjtarSPHrL7CFen0u5jj/B0yjDTmxIkMO" +
                "zx4UE7F+Hc1eyw0JqR3JagcNmqUy0QnH4ieyffLRMQ3rsCLbUHBsAzzNK4fug5i2C5SomesCdhDY7Wk4" +
                "L8DhhTCyIQE5aS+SFz449rWqmOQaXKATOSM1OyR0jrLktTRVf5hE6+gynYdmRu8G7yFAXH/7wUn2oHW7" +
                "ReNX/slPbO71sMg8FoPKLOmzNMjSJ4KxC9dYFz5DiXsVGELhPqfvKZADEP/RLex7W4MNv50i7MmEhVbm" +
                "9vQ+XrMYq7IMndTw4YRDH7h8UjxW5vHRURa8a1XZH4T8IfWmbPAMNg3ME5XyAT7dOvwmxkuCLTS69nwq" +
                "4CLSeBCtpJkXORKTF9+A/dsgm5aqSdRl95EPCqGMPaTtUi6VVmHH2viuLBGQByE4E3j1YC8sVHGsL0nf" +
                "3ztjwOnK2uoUOQ83z4taWxm++bqX+wGuA/csekCKSjmYwXeIJAnylrK18NktB/yAOMUrAEpLDkFhvE4Z" +
                "OIZmZpgFODiBAHTH/GWNqB3QcC3ZdWRigmpiZ5wneRwrMgTwpOriXuG4rV56L+1QeGOzZBxl3BDbNUQ5" +
                "0nAcGzfi17fJ0RN4YUBKIGmIb0MiC/ZiGo72pOuZWAKFPsp41gf5FLi2SmuOwIHxPKqduHI6TlWW8Sq6" +
                "NukD0zoHuBTwG1eLnC6OfKcB9t4eGRIduZFKy6VOsMYZg7hzLGv+Ude2WpY0dS3LaeSdaromFQ5cEtiV" +
                "SA3LrOAEpXsz2Ann3z3/ireQUSz6IoXks6fMZNEzWEQGzJ0DLRZLJJ/m3INldolcLHWsI1w1KdUOT400" +
                "KJTH1SDyqRJ1CY6c5rbjEHrSdp5/GruB9yLuc7XuXAKArHSP8+9uUKC03VK1iOcX6TJ9cVI8/4//Toqf" +
                "3766Ep8A4JPiR5RLXNk6/oxqvme6BVcsXxycecOvIEn188up7kOVdE4qnuASAHqmkq4CRAVZySBj3K3V" +
                "CsB1qdEjaJxKuBh3w64lYNbZUCVSB8GgnfMBANF0BkjOVVzB3On52D3sN1CltQ7tEZPXDu5h7vj39KEj" +
                "g5h9/fKKo9hT2QUFhXbcUbk+f1+/FEUHryFYcaA4u97aSy63K9zAIDyFGZSlOwChZz2lv4KMx8m4OXjD" +
                "OwQplRfncW2BV38hIAQqUGsBwefQ/M0urG2KyY10KmYqGHPZAtdHfOjRxYQzq32FPsXYzD5xHGX8E7Zm" +
                "4Ms2XXJxQzFbof6s4EAQAtM3qgLpMlXdUisUaqTn0gG1Cj6VRBZnP7CPE/bEG2HQ8d6WKhY47t1yCxVv" +
                "Y4FG6v/KpZgXHKXfcwUciVOiCN9SqWoF0CFojQsfWqd3M4FAgU0Bu3iRKNOaG13evLnhHm+fOrVZN0Pd" +
                "mMhCRAOc6I7DjyoOzhcoCll2hfvSHaUGrQcrzw5GTHP1TG1UzP3YrTLgiwMz57C9yG1AhIFescnaaM5k" +
                "cc+syXqypqi6tBWDZlE72ywQA9j4Yvf50aaKL/GF4IU0HTiqCa0kEuEw+1NZyJl7AKaRQezMHtCEpLsj" +
                "hg4kVXIpKlGeS2pHuPMWNXgWmyMsV/1+arrZIutUPjsXsIPjIRMUv3Vc5E3kO9I9nI19lkVIH+bicDA3" +
                "8EDJWu9ZPDSvd8PTbnj686EsGP03mDFcF49RU6/u689vH0bvc5OC0fLvjcpP2y9nHvc+KiTjjrs+NrKi" +
                "GsM4f70Ik3ZwbHvjmVmqybAxdaYR9bhZRIPvbz3Y8AG6Q6L1DadWddgbJnDzfPUhViQQ5PEh985po+4Z" +
                "c1/W95/jPNE3vhM9D4av3yMMPsvz13ByYEUeS9WepXH4A34MrWpPtBg2ch8M6/Na6gOaDj0rIJ17CtRM" +
                "5lxzN8GGjBMkjvfDA8rLBjMDRhxqWpSAsTtWZhT3QKG+76t7w130NP8KaTOPTfx9YOvuNyupsp+/wNIz" +
                "8Tr2dvGrDQZtQBRK9nCSv+/lgIrTm4tXjM9yYfi6xhM6T7ygB6QHy1EOZvu5hWUcOaf5aj5LU2SkitnC" +
                "WsRGGKOOUyvVx+o48zDPfC8zEeqnabSMOidhKeScTSh1MReva7GzndiyQXhwff8du5GsV0y+YG1M9By1" +
                "x+A+fNcDrgfc76cg7qT4CzSVqg8tFQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
