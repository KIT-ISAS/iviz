/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupGoal")]
    public sealed class PickupGoal : IDeserializable<PickupGoal>, IGoal<PickupActionGoal>
    {
        // An action for picking up an object
        // The name of the object to pick up (as known in the planning scene)
        [DataMember (Name = "target_name")] public string TargetName { get; set; }
        // which group should be used to plan for pickup
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // which end-effector to be used for pickup (ideally descending from the group above)
        [DataMember (Name = "end_effector")] public string EndEffector { get; set; }
        // a list of possible grasps to be used. At least one grasp must be filled in
        [DataMember (Name = "possible_grasps")] public Grasp[] PossibleGrasps { get; set; }
        // the name that the support surface (e.g. table) has in the collision map
        // can be left empty if no name is available
        [DataMember (Name = "support_surface_name")] public string SupportSurfaceName { get; set; }
        // whether collisions between the gripper and the support surface should be acceptable
        // during move from pre-grasp to grasp and during lift. Collisions when moving to the
        // pre-grasp location are still not allowed even if this is set to true.
        [DataMember (Name = "allow_gripper_support_collision")] public bool AllowGripperSupportCollision { get; set; }
        // The names of the links the object to be attached is allowed to touch;
        // If this is left empty, it defaults to the links in the used end-effector
        [DataMember (Name = "attached_object_touch_links")] public string[] AttachedObjectTouchLinks { get; set; }
        // Optionally notify the pick action that it should approach the object further,
        // as much as possible (this minimizing the distance to the object before the grasp)
        // along the approach direction; Note: this option changes the grasping poses
        // supplied in possible_grasps[] such that they are closer to the object when possible.
        [DataMember (Name = "minimize_object_distance")] public bool MinimizeObjectDistance { get; set; }
        // Optional constraints to be imposed on every point in the motion plan
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints { get; set; }
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId { get; set; }
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of grasping;
        // CAREFUL: If the object name 'all' is used, collisions with all objects are disabled during the approach & lift.
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects { get; set; }
        // The maximum amount of time the motion planner is allowed to plan for
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime { get; set; }
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupGoal()
        {
            TargetName = string.Empty;
            GroupName = string.Empty;
            EndEffector = string.Empty;
            PossibleGrasps = System.Array.Empty<Grasp>();
            SupportSurfaceName = string.Empty;
            AttachedObjectTouchLinks = System.Array.Empty<string>();
            PathConstraints = new Constraints();
            PlannerId = string.Empty;
            AllowedTouchObjects = System.Array.Empty<string>();
            PlanningOptions = new PlanningOptions();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupGoal(string TargetName, string GroupName, string EndEffector, Grasp[] PossibleGrasps, string SupportSurfaceName, bool AllowGripperSupportCollision, string[] AttachedObjectTouchLinks, bool MinimizeObjectDistance, Constraints PathConstraints, string PlannerId, string[] AllowedTouchObjects, double AllowedPlanningTime, PlanningOptions PlanningOptions)
        {
            this.TargetName = TargetName;
            this.GroupName = GroupName;
            this.EndEffector = EndEffector;
            this.PossibleGrasps = PossibleGrasps;
            this.SupportSurfaceName = SupportSurfaceName;
            this.AllowGripperSupportCollision = AllowGripperSupportCollision;
            this.AttachedObjectTouchLinks = AttachedObjectTouchLinks;
            this.MinimizeObjectDistance = MinimizeObjectDistance;
            this.PathConstraints = PathConstraints;
            this.PlannerId = PlannerId;
            this.AllowedTouchObjects = AllowedTouchObjects;
            this.AllowedPlanningTime = AllowedPlanningTime;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupGoal(ref Buffer b)
        {
            TargetName = b.DeserializeString();
            GroupName = b.DeserializeString();
            EndEffector = b.DeserializeString();
            PossibleGrasps = b.DeserializeArray<Grasp>();
            for (int i = 0; i < PossibleGrasps.Length; i++)
            {
                PossibleGrasps[i] = new Grasp(ref b);
            }
            SupportSurfaceName = b.DeserializeString();
            AllowGripperSupportCollision = b.Deserialize<bool>();
            AttachedObjectTouchLinks = b.DeserializeStringArray();
            MinimizeObjectDistance = b.Deserialize<bool>();
            PathConstraints = new Constraints(ref b);
            PlannerId = b.DeserializeString();
            AllowedTouchObjects = b.DeserializeStringArray();
            AllowedPlanningTime = b.Deserialize<double>();
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupGoal(ref b);
        }
        
        PickupGoal IDeserializable<PickupGoal>.RosDeserialize(ref Buffer b)
        {
            return new PickupGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(TargetName);
            b.Serialize(GroupName);
            b.Serialize(EndEffector);
            b.SerializeArray(PossibleGrasps, 0);
            b.Serialize(SupportSurfaceName);
            b.Serialize(AllowGripperSupportCollision);
            b.SerializeArray(AttachedObjectTouchLinks, 0);
            b.Serialize(MinimizeObjectDistance);
            PathConstraints.RosSerialize(ref b);
            b.Serialize(PlannerId);
            b.SerializeArray(AllowedTouchObjects, 0);
            b.Serialize(AllowedPlanningTime);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (TargetName is null) throw new System.NullReferenceException(nameof(TargetName));
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (EndEffector is null) throw new System.NullReferenceException(nameof(EndEffector));
            if (PossibleGrasps is null) throw new System.NullReferenceException(nameof(PossibleGrasps));
            for (int i = 0; i < PossibleGrasps.Length; i++)
            {
                if (PossibleGrasps[i] is null) throw new System.NullReferenceException($"{nameof(PossibleGrasps)}[{i}]");
                PossibleGrasps[i].RosValidate();
            }
            if (SupportSurfaceName is null) throw new System.NullReferenceException(nameof(SupportSurfaceName));
            if (AttachedObjectTouchLinks is null) throw new System.NullReferenceException(nameof(AttachedObjectTouchLinks));
            for (int i = 0; i < AttachedObjectTouchLinks.Length; i++)
            {
                if (AttachedObjectTouchLinks[i] is null) throw new System.NullReferenceException($"{nameof(AttachedObjectTouchLinks)}[{i}]");
            }
            if (PathConstraints is null) throw new System.NullReferenceException(nameof(PathConstraints));
            PathConstraints.RosValidate();
            if (PlannerId is null) throw new System.NullReferenceException(nameof(PlannerId));
            if (AllowedTouchObjects is null) throw new System.NullReferenceException(nameof(AllowedTouchObjects));
            for (int i = 0; i < AllowedTouchObjects.Length; i++)
            {
                if (AllowedTouchObjects[i] is null) throw new System.NullReferenceException($"{nameof(AllowedTouchObjects)}[{i}]");
            }
            if (PlanningOptions is null) throw new System.NullReferenceException(nameof(PlanningOptions));
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 42;
                size += BuiltIns.UTF8.GetByteCount(TargetName);
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += BuiltIns.UTF8.GetByteCount(EndEffector);
                foreach (var i in PossibleGrasps)
                {
                    size += i.RosMessageLength;
                }
                size += BuiltIns.UTF8.GetByteCount(SupportSurfaceName);
                size += 4 * AttachedObjectTouchLinks.Length;
                foreach (string s in AttachedObjectTouchLinks)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += PathConstraints.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(PlannerId);
                size += 4 * AllowedTouchObjects.Length;
                foreach (string s in AllowedTouchObjects)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += PlanningOptions.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "458c6ab3761d73e99b070063f7b74c2a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08a28bR5LfCeg/DGzgJCU07djeRU5ZLeBYSuIgtryWNy/DIJqcJjnRcJqZnhFFH+6/" +
                "Xz27e4aUHd+tuXfY212sxZnu6qrq6nr3DAYD39RFNc8aU89tM67M0g7Cw3nt2lXvma3ysZ3N7LRxNTz9" +
                "tjZ+9eZttnLeF5PSjuf4wA/iBN+uVq5uxr6tZ2ZqFdxgMHGuzExZujVMKlYrW4917NSVZeELV0U4sIZp" +
                "GjNd2HzsJr/B+uPGtdPFuCyqK1pPIC6LqlgW76yOygvfmGqKaz51FQAzRdX4bGWaBawTHiQYr0pTVYBN" +
                "kQ+66yOusDyvy+Bx3qx0pvnz4/Ce5sOUcVMQqS/l98WqAZJ8Ft47fjAYnP6D/zN4fvntSbZ017Zoxks/" +
                "9/dpnwZ3s9eLwmdL672Z2wzIb4B4n5kst34Km4D4ZG4GD2gfs2Zhmmzt2jLPJjZrvc0PAMi6aBYwZGXq" +
                "ppi2palRKu6pVGSNk9kGYBGXhllRTcs2R+4u3BpGIBizWtUOdjQrYACKAP1hm+ko6+KZO+uzyjWKMADe" +
                "IICimrl6aQhpM3FtA0jd4aVXDjb1TnZkUDILGnHxAsixgtHxCAH8BOTZa1tncwsbDn97GJIs7BdE+sJc" +
                "W9jd2pp8AzgsJ0XFjNhGIFndM5+2xyAWc+uWtqmJDOA3PurzEBZatY3l4Qh2DKRYgNfBEehAGE8yPFfZ" +
                "DKfiW5pwoBJd5Dzq9QLnN7auTImMadra6vILU+Uy32ar2t4TEIBfVW4CG0FYapUEODq/EbobFrLvkezX" +
                "4SFCGQfMca2PxCJgkCwOr4FJoCQ+BpFbkAiisWsHOkioRBae2YMb4WYIB3/egZNgq4ZU0Z1dwIYkvWba" +
                "tKAjNikIFWWcmn1WVJ9tzcWlN6tiKjMBg3AeUzjwN8JZAo0ZqbwyW9fwL0OeWCDHRg4fpSsc0xgPg+am" +
                "AnlS2WRevoQFLhuzXNk8EcPIROtBzcHRyWG73cRMirJoNoiRb6dTkNGeTA4z+OlhgcwBNjXhbI0XEUDl" +
                "cGfuXH4HdAFwG7BR7cpr/w4cBPhx+aBE8qIGanA34ew05soq0cC7KzwEQRsdgNUiewMSUvmSj2aUVYUY" +
                "16iBG9Y0O5YwMxDjoC0XBploKzq6pUWeHPGSKDhqvo53rw8bJwjIctvrL50uDpNRq2TrBaxW2xI4SFY8" +
                "6LevmOWJ8iF9xWq0srgxBg5GR8iB5d6Ws2E2ASV1K+ShSn2q2NZFWaJEBsgjQpzB4hlNkTa0Je1KMALq" +
                "2hoUamY8mRQ9QLX1bQkGwbstWoid16YoDbgcRASdIYObAauNbucw2F7wQTocxrWW5qZYtks2MLBdAHJq" +
                "EWeABqwoSqEFWXH0l9MH+ApOGS5/LCL66CFCGQuEMUHgBVDwyLLCmSzxSAKBbgJHdFqSwUETa9nIeLs0" +
                "FRjVbavBgHIePgWQeP7REbH5/VXr8R+09znbB7Ttbc2aQRE/+IAnc/Cp/JAPaOfBd2BXYeMW9E9E8jcc" +
                "Ru6iH/SmvMSf5HWy6/Zp8PZNzggzgrABoAer3NQ5qKzG5KYxJHuLYg6a7F4JnkQJk1hV0ttms7J+pG4X" +
                "qkDyM1CT66EAVbFsK1DvaOnBX+zMJxej62lNnavBj8Lhsxp4g9Dhf97+3lrwcrNnZycoxd5O26YAhDbo" +
                "etVyip+dZYMWWAayChMGd1+v3T20xHNgf1icRQyQtTegFL2nk3kCa3zGxI0ANjDHwiq5z47o2Rh++uMM" +
                "FgEU7MqBPj4CzF9umoVjebw2dUGnFQCjLQOohzjp8DiBXBHoylROwTPEuMYfAVsFuEjTPTR4JUcic2Ag" +
                "DAT9fl3kMHTCtnhaFmC+4WxOalBdA5zFSw7ufoM8Zv1DO4KKx3s3LcjioYOnPhbtBsUMn0YaP3CK6EgA" +
                "yedoC5vEB8PnmV/ZaTErQN1ARADCGj2qN8MMpAQIauAt/DBgtEv0hfHl27fo/nVHs/f1NpiOZC0QZ9BK" +
                "9gZlz+YomU/ALOjaOWxW2Vr220RLeeQuCDQaUXat6NSTM4saP+tROQLSQ8DFYScjljyL5CQPO2Qlz5ma" +
                "Qd7yK5KY8ax2yzEIALz4RJt5q4tFrjz+5uChtjNb07nun3s2BnpmezqUAKCbtj/8CfHaosaAs2TUrdaQ" +
                "ZVZb2O0VGN8heUcO1Qq/ZxccyXF1oXNH2YAkOgwY/K1F814R3DhuXwTy4SI1HqLmphdCGDlsHXKD/3oT" +
                "/tqEv97tB/3IOqUhbJQn7yTys4s8/vo98h1dktHgAxTpX+v9pDX6bh5QmNsZBOiY1WgS9y+6ujRlyBYY" +
                "CGRnlNQcOofg2Psrj+4WzrA3Bl15+rssZk0nkIBNx11vyADBAI0c1GFugnuKkMkFU4czhhLi7Sao9iOw" +
                "H0n3PdIgLExNoFkPD/MOvZr3iu6pjBrHN8H9LeJ4Nv/LFvzUCaWIPJhKBD5DJwIJisEkzpfQASzLNUQM" +
                "EOTY5arBoDx4xUWVLLkXge9ybJfQZzLkf6RmFca1ZEP3SNpOmhiP7hEeoXf4jPw5yuNAsA0qCix1mInZ" +
                "PxUpCttq2t8hRuCaecMoHeNdGO8pLgJRB2DdEwaPYcqRHc1HQ44faRQdGcSCnF+IbepiXoisxiAHYeqm" +
                "DLNm9pBjSsKZFwN5AyC1Y0V1PMqezbKNa7M1EgR/1OJzkxOieNEJbJyj4y4gduj2kPEDvd7A5n5Qy32a" +
                "rU5VW5Ksfk/eVuNJSQ1oajlLMtuU6URHLHkWzrcHZnpyzlBNmWzuIEhlFiZHHxw3TiGKr4vuGf4k7ywi" +
                "GoK2ZCUc9lKMZGekWs7+4ItojDrjEyPVn/JjAYqJ8k6dGdfhcXfCHjavxxjYgPBjh9vAPi9zHfURxXxT" +
                "W1OuG4L/Kg+cj0Fx1N40Qqaj9bGUBvDZm7DKPXiL3i9ouPEE3OP1MGLwefLOTICGtzHnpoPik97YXS9o" +
                "Ac1Kry0Ex2Q1ZyZJq7IbJdzJjnILOsaiNkNVcg3HcIl1IDZHjjOFHZHOnpbgQVKa/J2tHWk1n5WYbtSp" +
                "zXFEjrHYw75vS/qtZ5ftNngqHf9RNiXSSvn9jtlJ8oJu4iTB22cpWTRPhRaRHByWCA7OB/fF24ZCasSG" +
                "AJHVO47RHdUFxbF1ybi1pTguKByYcIvforVFgjHmNSMO165sNcLehX92MPga5RvW+JGHxlHj2s6DH/S/" +
                "S9b2omW6jAEenJHni54Aswo29tGZsge3GtPyDnS52kCwxqu6WBbICElFkp1vV5xdkc1x4lNnRwaNTovR" +
                "AJDsF2ZlGZVLhPpSQaF6D2A79RZ0yzG5XGCWggpNYXUUJ4jcDTmP2wFmCpNKEAL4GSjLPC9CZjwAHOKZ" +
                "W1iv2dKYh0BfI6d/gvpEqRFqj8wcNn6ICYkdhD4HmIAKg34vabL6h8nCgUrRp0pl3rZTUWgm7mYIPEL3" +
                "D0L0DRzyHKMkOAk200wOQmGKWB6Ih5S4BoKLOrISgCHjLVb5MH9Yk4l6MIT/gmuFKcgvs68vfj79Qv6+" +
                "fPnd+avz04fy8+kvPzx7cXb+6vSRPrh4cX76eCDMxrSqxk2Ek4zC5wMdlIPTXnkqGnaGxtxPHKFz8Ggj" +
                "+umEZNhJZjG7RsoBnU4NNLkaktsbTWsdxjmHQHxtNrjCN6JTgXBCdUi/fh5mvwwpzPg1xdlIua601Rwi" +
                "FMFo6mpw2leOuIxpX3DQAn3A9FHk7fjn0wfJr18Cr/HXr8DqFCXmv2BFPjduOyVqKgwRQIdiuMx4gpM/" +
                "F30BNsjkRYsoiDPCEjTq7Ov41ZOzZ3+/BHzSNXWTCSZuMKfLmSssOmhqKLXLzh9KUumIcBzza2ZuCk+l" +
                "WYlZOnDH350/+/a719kRwpYfx5EmLrwnHI80LUiFB57LWciO8Cwc83ro0ek6TJ2swz+SdW5bBWMZ5R1v" +
                "nwmV3B1rPsUNQU7pKyyLd5VncibRfS9qqhhwJa4pVlGGiKdUVodNQnlvV0PmbPa5MHXQO4nCvyBSPeJB" +
                "uJKTujU4MgYHfnIVhzpaFVviXaGeHVCtnWMmMH+mmoMh/yo5wZKjJv+D7EloPYCzfW2xDmP9m7cDXOO1" +
                "AABdEmANRDi52B9mbNtWwmZHKEoZbJ60J1YpGTtYpmQd+ogUF5DePGI87c0YGPcpsU09np3B4ce42N1k" +
                "5x9zszW7ls7UGnX0wKN7QT5STJv2rf7uRPY/xqsPdWY8xPdo6zJb19RQJPGZj5hGp9VMwC1sGzu+GePM" +
                "cRi9Y8jmw0PebQ35F/XQd2UmZJsTitm6zlrKvCzxEQb+MX3BybG88NPsKAZlx1u1YS4IH+h5oAmYYfMd" +
                "+2IC5A3bgvUCizRoIahLhXs9MEkXxBzTfq4myM8FPXKYI4a4HMKabDIY29aaAmdhDg182PZQt1NeI5mO" +
                "eBD4F7DzJ4GChEcE4sXFawDPzRnU59kuMe+u3RuaZcYunGaNjTgR+dg/ofzDpqya4YJHo2CToJftqLh0" +
                "cF5KqlhdF3ad9H4Ja7hq14v9KfI4wlW5NQrcKu4oPNZuSjoAvgWnctXWFAKMEkXQcQ9oN8mKSFfixAZZ" +
                "wRRdwcn5JCPDYTdD6UYqKcyvtPLKW4XSKz5YfyQsBzu1AK1icc7almXcKY6xlFmUOueURhTPqNiGaW3l" +
                "fQ1nIXWgHWff1G6Z8F3ltHFrU+e+s7sBbT4Cpi9vqZSRk0nlmxtpZmupYWJpqg072SPyWwVlSW/zmMc8" +
                "YJixSBguAGIXEZp73xd1zllQri5bbYBRRR6PLHkfusGmXJsNynL2iKQcHFwYye0biPOY1u3tLKzNIWj3" +
                "cLIsR3kZ6STeqiLXRWNZoGffAEKMbYX7SVcbAeQ6g/Ee4ug8HYjiUTp3JZUGzOk3nc3ipt6kEBFYQtBW" +
                "2vXVem6hRGUCmsRys5omAMDwbZ9CquUhAkCXK68l11QAWPveZkdGPZG982vSKK6dL6Jwod+xdQqHu/Sb" +
                "nhHwmkCmloZPz8ROTRvP2Q4fg5aRMhWa7kYTF6p6mOvagd+vXMJgUlkC6grEippEsckBRBBlaBilZmq4" +
                "TLKFvDLRgjZrsdXxmsoMpeN60APJnnC0QQuy94GGoROXkHNMr0RJqyatsBZUKnHJ+sQA1d2xcCrVrtlO" +
                "jRDkm08U5aTTYplUPdlsiH8XsebDbqhjERmOb6TvMRxSbXl8IDZ57QQjDsaA/VggXlH8YP2xgtTWUAyx" +
                "xRjK0rfAf1ncf8hLpOABs5VFvxs16/Eo1R7KbqZZmizTJsdkd7bUAgOKFsYIUFHsDhbl3m14jKCPHgwJ" +
                "Qy7HPcClvCrfrhQktjtP2nqxZxIHjmmgKjM89zqR7KSHSBcoLTfsIqVzSAOUGAl3yRSAd3c4A2o2toRJ" +
                "pQzdV2m1dFWF3ZFa7u7bndRhELPHAphIH1KDOUXlYk0mNLBOXZMek7JiRjlbPF09hhGALsdSnFMd7DIK" +
                "z4xcJOj0HdAGaaY/ukk/Z6fZw2H2C/zzxTD7Ff55cKD5nPMXlxevxr+e9p/8cvpF78nPpw/1iWhS2rJe" +
                "98K/YEzQuw4Uw8xiNuOOUK67xssYWmPxU4s5eA3yAH2FdUlvwsUiGjhGgJIjnzFHZ6WZyxEl0SUDKtkK" +
                "aRHnbmxqC+JGPqqgI+QgvZSA9HzugmcvjR8yCQUMVA9dyMK5Y8wpfhQu1BymhP8bThTIFc2SDmacSX6F" +
                "ZJizI+S0CJyHCIn8dHDvvbVLOR2ILUgTWvFbW8pDyzUiZKvronbVEgyHkIRLjnnJLlHhvHPa6Po9JEV6" +
                "REXfQhLX5VWpVe1yAqKBrjUz3H8V1k/UTWlnDfroD4aaBDFt2cT3kswl7ocbXfmmAm90Oq6x93ZWzOmu" +
                "DLubCcFjXThQjjtI+muWjiOrI5uacqblhuLtznvwSZQFTDNe0dvuRYhSOUoop+kScOClKy9d2bjlVYLB" +
                "kNxoEhBK9PPNAZNVoAlJyCtrc7SDADcwcfSAbcxu4sCyxD6slM/Il+vC7OKrMqKr1r2Z2XE4QGOkKREv" +
                "OYXgMDpujqWuDApWcg64w9Qh354JPqGWaUKEQpAQH7pilvO1qnBya0vNr8EdoKNqK+Q5X5AjH7OtpqyH" +
                "0EeTgwEeFkCONmlbbHkEHQEVpEyljN/1BIxM2xKYzjnbguuRa1NQnKW2fRdYrGtLH3mi6WWRHAzJZq/6" +
                "nnQ0EFRJbNxV61u9Nchh8AA5AoP9aODpK/xxiX/z87E8Vz4p6DScB0JZ/tl6FJ5L9bIag6FRMY2YhFPk" +
                "0vAdrrxdlehIUGpnlh3tCkliOwC1EfTDq9fa8yUx1pu32ay4sfmYm+dDSxhvPdGvKiFcCQaZAtRvDgZP" +
                "+M1TffGcnoe7LWHCWCfQSS9LDmdXSGQ1h5V+gJ8v+RfgQwlVedmb4qemtDLhEv/W4fRC/BkJh6VD1A8T" +
                "xMMjey3eqAOHZ2mo9zSlb0WRFtVVKexyJbn98d4Bc3spx5MKjEmLP706GFzQck9xMrZP8W1ohhV7hzrL" +
                "9vyJn1wNQcAa/1+SL2Sy2Z3ETV4vbMM6NhUvTZtBkADhVqMJcXJv+jITLwLqKSA1UnhxXvZwPOOZurWO" +
                "sPsKb3IyQd+OIGhsks4uytthSPIb3wVGDUvXLOHosTscG8X4RHNrV3Kiv+eZUjnasEAswb4UGNefXXxD" +
                "kWCsO2AVtAv9OQ6GgckqNH+cu9l4a70gu1simx1pmidsGp0IaQgiTsBhVwDhSLIIplf346kMt872JVnx" +
                "FhbmP/BilMYBfItLrwmRw6bkTlyOR+pIEYKBlHoCN720pt41WK50G5Gyk5MpeBwnJ4nulgbqdpUztWDK" +
                "CP30Xvfxno7CbmFMmGXCeSBBXLgy96HTlj8bIFkdEiUmXXqNIJz7veWDVDu6nMWnAZuSxWSFSdRawZ2Q" +
                "R7W9pkISNeXXhcezNz1Oc0mTDd5Iyz7rnDk1fQrF5JQ3PR7GoXI5aLM99L6nwfchasCzGqfwFaEYWa/A" +
                "DMZ7lgLgBRUiXhyPiLDzSEtBXUTABRQ4iGIbPquTDSkH+hgGM4Krjl01zu5Op6uIoRLXMAVfW3LjavJZ" +
                "sUenP2ZAVwgxg6fbyP06dJR0qmZj6ToWrjmS/elqQ1/4xotsqxoi4+OHtAKlHZH09Bpvggz1oiOt1GXM" +
                "B5pK4jRJxlOhxYdK5ij7CT1sbCrnJm/Rp0RF5TAu5P3pXcsjKzikBnFKPlvpUgrDyWiio7gRaUTuMTX9" +
                "byx0vgMR+OSLd5Zus+OdN4KTnBqy7thHszaUsgoyENBMmENumyLNPT5TLBPyDo4Gg969g3hHluRn+zrc" +
                "9m24zfadtz0olG37A1S92rokxjoApee35MMZQcRyO68ttznhvYbcwb5Skhs9PdXYnEqVzwjE9ViQ+3UJ" +
                "04Sua6qh+o2HmCHprvJccg0OKc+Z2yaef/IMnax+BUqJlBTWxzD51ykWGfKLTa3KwpCE/f3V2Tek0x6h" +
                "KT+6AWGF/5n1cahUYm6dXkrlIL2Qm2LHjGQ3dxhviXffD+7KCIUGB5oaOEAZmSnVdzo4/L8a268aW+MF" +
                "pMUfVmM6/P+SGrtNi6U3/W8JGKnFSqPD/qA1bCgOwH97734iNsFL5td+7mgFrJWTvcJTVCuhNLF2W1fP" +
                "fO8i1yDcN0suXaXXYvVq1J6IJG4LgaqZfHSwuhdJJ7W74tKRI42BjZ2G6y2mmlN7AJ42kBIlUobE3zJu" +
                "P9Sx3OzYP+4S6d1o9haQpzOLBFLG+Q+RSMDiT3aX9xH03hKpiV7rPQ2lyBDoyAfAKHcTby+RrsG48Jbu" +
                "tO3b09IkqX3l2ChDffRa+eghAjDkllMSjeI4XvTus5lm2mnPqBSAQCsngeyoyEOFEtM6IQa9uwtiqJyR" +
                "nQu0BHbwCvnBoM8v/cyQpuUkGuKvLLFpTsNGDbIxmk+KAPRxFqpfbkJi+V5AjlGhsoF+lyykyOOE5Jsv" +
                "ydfyQlJZ75gxZmSG6Q50vtVapp/pCsU5uQFNF0mX9E02JYfKwnwJVPoFwje1sF2AUrScRA8XqY9DwxEt" +
                "0v0+UfgIkeas4ieVPAANiY8PfQQst4jg9lfA1p1u796uDLGqcVW5dfVPqRVun88nYj6HsX8sJETUF5a7" +
                "JLe0sxZ5aDZhRh6RKOl1+Oew/DPM5uz4dpxuOt6/IAnBPID2FRKX4pGShCP6ZnNuwJUWXnr+GkEgnAg8" +
                "Zp30cpLd+thKgrZ2eBS1Xr6p7e42mSRBFjlxees1qD9+r+mPX1J6LteFbr9I9MGbQQhGL3xRPIFpKYtt" +
                "xqLwAp3cE6Xp+NB3FO5QcNuaqdK2KVyke9EJU8FEDy11G7H4MsXwSS5deFE/bJewR1rQkkEgu9eFaz34" +
                "lfYGvAokgQtYVKzhhpHJBhyjJ2dn2HpwQGEktSqmcEIrUFKxzTBWqdFlPcIvPW4a+ToD5VSb6UIgRCEp" +
                "8ANttNir8+cXP55jYwNRtsKGG4oLw/cfOJIUBUyoe80LvZ/iUDanSUot7EdC6suX5y/OsIlClHVcdveK" +
                "tNCQK5l0IPTeG3KBGoh0CzU+0EvgVOgk3596UQq6JIgsAw53P/7GKThqlxI0iUWPGMmLla3Dbf6J1Q++" +
                "hbFO338y3flhpTO4+9H/yS6+/v786Wv8etbHT5b/MIOevr+ioPfa8LNiaB9F0YGWoy47iBG85cgV3U3Y" +
                "Sr4MMOfUdIjN5K6moX6Ovi9yZUPuM13khJ4wiBjg1ypbeGupyvJJsAoAJrkgMUkREqNMKYrvLy9e3Mcq" +
                "s+Qtfnny/IeMQUCcHwQaNHE4Ecl3JlCbK286N99wYTU9o+ycfI2i2rH7dLJC82lZXNmT7M5/HCKjD08O" +
                "n6JLdPb14TA7rJ1r4MmiaVYn9+/jfc0SmN4c/ucdIZJr9pXjtEml5VHaRfGK6KNLkQ98Ve4QJhXcpHRl" +
                "rXxcbVbC0eXuxlHXtnZEFwsHzEf9dMbZ1ywk4SuAqAhkac44kJi1dU2qjxNQnhrqMSMlBNPvjCCdZIEL" +
                "/BAZAQ/7jDj5079/+ViGoKHmHi4YuI32oa52+bcfMtg/b7FmEParu/jl7+V3OkTA03LZ4XruH/1ZHmGd" +
                "5iT70+NHD/k3TKhxSIHeso4BV2ENEXT/OTo3SJCuooUneb10eVviAGopadzqMMg4ivunv4FFdnZnWpTT" +
                "hnYoBjsa9GD1zU32OXr7n2fTd/B/ObbqDaif5eQUtsnO3jzAL7hNws8v8Oc0/HyIP/Pw89Hb+G21x2/p" +
                "2Z4TJr0v9sS0QpqLJTO/9aEedu5G4TN8d9X32Bo5XRSlthzgwH6KMFYIySeWjwTCqL+YbFHb2ekdOR3r" +
                "4qoY1c6PXD2/38zu/LWZ/eW++SuI4vQKP82Mcy6tpWxA7qbtMuzuTPr5U0OwlSATMeyim3EMFNr29VIn" +
                "DuKng9cxax0SUfvIKezsxxDtpncugQPgf4TP+3EHGLdnhEiVxnA6MP1gdF5cFzmmC/B9Eeff3iFyN/Nw" +
                "UvA+u98s2c8fcojQ/e5gZ8E+Gef4MmDF10N3fnmg305BuXb9pC5+LqP0LjZ1pO1izBRuGgvJ58ipUacJ" +
                "bJtc7aQE013HL/msTEENV8zro+oLcA2511grmWht2QcI34y9BfvYWxgvPm2vn2QgtNXfhLiayQE0qITO" +
                "iCROt35ZhBxSIJAc5omDrYIZlMN5SAFeB18aLuV24aVcXeJvBzNS0uLGOasqe/LiLHVEE7kTEOOOOGAR" +
                "fuudSsE/4VSROGK5oNuQEHcDQhr+3jXqT+rZywMV4fceEE/aqgZ3k4/T2/elCLUtK35irJPhC98l0v6s" +
                "PdFB3V4fQQU2hH2YCmkb28e16dgK1s3kFKwtuC631jB4wy1nO1NAqgZUB/CwPKSrXP3q26+f6JtP/WXm" +
                "sGD42mAd/pqHvybhL7P3hk9qouMmvm5P1Vba+M3b7JbuqNdJqyDp1PSrfzGHE5dAX/tgIFNEBPjHT6AF" +
                "6euw8vLTReLvWZzSmI/OqNMRe3jBOeNcL5gd6kqH8bW1/Xrh9v1VR+N2Zv2kDs1+0o7klVzTUahSiRCA" +
                "+JXhXQQoVftk2n+bWdRTRz1aeHUGPAKZekQFXSxf3XfTabsC+3uMZoTabemJqaYbZcXRaNLcH6GnUJT2" +
                "mHvSGBCu8bQ0eKEmuqFOb0LK9KhCBhTj0AcCUESxOXt5HC6Y43VIiy4iT6tcHj/fzBc3Wy5t3lUyIAQs" +
                "QMu+C+EST+UPPXJvA31xZbTgIoXhLvd1XTQqOB77zr9E6w7nZTD4LxlaHzIOaQAA";
                
    }
}
