/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PickupGoal : IDeserializable<PickupGoal>, IGoal<PickupActionGoal>
    {
        // An action for picking up an object
        // The name of the object to pick up (as known in the planning scene)
        [DataMember (Name = "target_name")] public string TargetName;
        // which group should be used to plan for pickup
        [DataMember (Name = "group_name")] public string GroupName;
        // which end-effector to be used for pickup (ideally descending from the group above)
        [DataMember (Name = "end_effector")] public string EndEffector;
        // a list of possible grasps to be used. At least one grasp must be filled in
        [DataMember (Name = "possible_grasps")] public Grasp[] PossibleGrasps;
        // the name that the support surface (e.g. table) has in the collision map
        // can be left empty if no name is available
        [DataMember (Name = "support_surface_name")] public string SupportSurfaceName;
        // whether collisions between the gripper and the support surface should be acceptable
        // during move from pre-grasp to grasp and during lift. Collisions when moving to the
        // pre-grasp location are still not allowed even if this is set to true.
        [DataMember (Name = "allow_gripper_support_collision")] public bool AllowGripperSupportCollision;
        // The names of the links the object to be attached is allowed to touch;
        // If this is left empty, it defaults to the links in the used end-effector
        [DataMember (Name = "attached_object_touch_links")] public string[] AttachedObjectTouchLinks;
        // Optionally notify the pick action that it should approach the object further,
        // as much as possible (this minimizing the distance to the object before the grasp)
        // along the approach direction; Note: this option changes the grasping poses
        // supplied in possible_grasps[] such that they are closer to the object when possible.
        [DataMember (Name = "minimize_object_distance")] public bool MinimizeObjectDistance;
        // Optional constraints to be imposed on every point in the motion plan
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints;
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId;
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of grasping;
        // CAREFUL: If the object name 'all' is used, collisions with all objects are disabled during the approach & lift.
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects;
        // The maximum amount of time the motion planner is allowed to plan for
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
        /// Constructor for empty message.
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
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal PickupGoal(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new PickupGoal(ref b);
        
        PickupGoal IDeserializable<PickupGoal>.RosDeserialize(ref Buffer b) => new PickupGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(TargetName);
            b.Serialize(GroupName);
            b.Serialize(EndEffector);
            b.SerializeArray(PossibleGrasps);
            b.Serialize(SupportSurfaceName);
            b.Serialize(AllowGripperSupportCollision);
            b.SerializeArray(AttachedObjectTouchLinks);
            b.Serialize(MinimizeObjectDistance);
            PathConstraints.RosSerialize(ref b);
            b.Serialize(PlannerId);
            b.SerializeArray(AllowedTouchObjects);
            b.Serialize(AllowedPlanningTime);
            PlanningOptions.RosSerialize(ref b);
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
                size += BuiltIns.GetStringSize(TargetName);
                size += BuiltIns.GetStringSize(GroupName);
                size += BuiltIns.GetStringSize(EndEffector);
                size += BuiltIns.GetArraySize(PossibleGrasps);
                size += BuiltIns.GetStringSize(SupportSurfaceName);
                size += BuiltIns.GetArraySize(AttachedObjectTouchLinks);
                size += PathConstraints.RosMessageLength;
                size += BuiltIns.GetStringSize(PlannerId);
                size += BuiltIns.GetArraySize(AllowedTouchObjects);
                size += PlanningOptions.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "458c6ab3761d73e99b070063f7b74c2a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbXMbx5H+jl+xJVUdyZiCFMlJ5egoVbJI20pZLxEVv8SlQi2wA3AjYBfeXZCCru6/" +
                "3/N0T8/MLkDJvhyZu8r5XDGxO9PT3dPT77M3Go3arimrRdblzcJ1kypfuVF4uGjqzXrwzFXFxM3nbtbV" +
                "DZ5+3eTt+qe32bpu23K6dJMFH7SjOKHdrNd1003aTTPPZ87AjUbTul5m+XJZX2FSuV67ZmJjZ/VyWbZl" +
                "XUU4WCPvunx24YpJPf071p909WZ2MVmW1TtZz0NclVW5Kj84G1WUbZdXM675tK4ALC+rrs3WeXeBdcKD" +
                "BOP1Mq8qYFMWo/76xBXL67oKnvPmyzrvfv95eC/zMWXSlULqK//75boDSVja3tf6YDR6/D/8z+j5+dcn" +
                "2aq+dGU3WbWL9r7s0+hu9uaibLOVa9t84TKQ34H4NsuzwrUzbALxyeo5Hsg+Zt1F3mVX9WZZZFOXbVpX" +
                "AMZV2V1gxDpvunK2WeYNheKeCUXW1X5yDlDCpOOsrGbLTUHmXtRXGAEo+Xrd1NjPrMR7CoD84brZOOtj" +
                "WdSuzaq6M3QBd4v5ZTWvm1UuGOfTetMBpTu68LrGjt7JDoFj3ZYy4uUL0OI8PkdjzP8epLlL12QLh83G" +
                "3y1GJMu2F0L2RX7psLONy4stMFhNy0qYsLt8snarPNodQxwWrl65riEN4DSfDNmHZdabzuloQp2ADgdw" +
                "PQzHI0B4kvE8ZVhH38lwE2QKMLecUzvXVPmSDOk2jbOVL/Kq8JNdtm7cPZ0PzKrlNnAPAtL43cdpIQfr" +
                "Zqty9WeS+yY8JIxJQJkr/SoMbPVkYbwFa6AUfgUSexEIorCP6T0ETADxr7CFvK/nAMNfdyD2rupE7dzZ" +
                "B+tYZDWfdRvog20KwQsuZ2a/Kavf7Ezlwtt1OfMTsX44eikY/A0wK5CXiXJbZlcN/quApw60uMjaw3SB" +
                "IxnTYtAir8Yjk0Tl4iuAP+/y1doVidQZ+1wLbYZTUmCL62k+LZdltyU27WY2g0AORPA4w88W4LMaqDTE" +
                "1+UwAUIAdcCdRV0Xd3DmweZxUKG67s9gHaDb0kFTFGUDMriHOCRd/s4ZteDZOwp80DiwS2JRIBRVu9QT" +
                "GEXTANoCDZjgoOh24edzSG3Qhhc5WecqOaBLR2Yc6nqUFTNPR3sXx2751f1qw8VXta2MqdQb2dUFlmrc" +
                "EowTEx301xfK6ES9UCGpkqwcdyPHKejJNBjduuX8OJtCC10L+NgLeaq4rsrlkhIYAI8FbYXK45iinMtW" +
                "bNaKD0jbNFCXGfhGa2HHpXHtZgll39Y7hAgjL/NymcOZEBJ4YiB3DdcaX8ta2FT4Fglruc4qf1+uNis1" +
                "HNgkgJvhaNQEBSaUS08GmXD4x8cP+AoniksfqUg+ekggEw9gIgAInYImxhKHD56KUFZPcRZnS7EjtJpO" +
                "bUfrVnkFQ7lrDQROoaNngMhjTtfCFffXm5b/oQUvVO/TWm8aVQCG9Cddk5vxKz6hfkffwFZivy7kPxHH" +
                "v3OYuH/taDDlFX+KF6mu2M3g3XaFIqwIgv1Qd1WRNwWUU5cXeZeLxF2UC6ise0t4B0tMUo0ob7vt2kFb" +
                "eTeKuk58B6prOwlQDatNBR1O+w3/rzdf/Ia+6zSr6waOEYfPG/CG0PFv637eOHit2bPTE8pv62abrgRC" +
                "W/pSjT+5z06z0QYsg5hiwujum6v6Hg3tAuwPi6uAAVn3HiqwJZ55e4I1fqPEjQEbzHFYpWizQ3k2wc/2" +
                "KMMiQMGtayjfQ2D+attd1CqNl3lTyhkFYBosQD3gpIOjBDLRPoGHUtUGXiHGNX4J2CrAJU33aNaWGlks" +
                "wEAMhDa/LAsMnaq9nS1LmGgczGkDfTXiLF1ydPcr8li1juwI1U3b1rNSTBu9NnOeZDckBvinnCI5EiD5" +
                "jIYvjtUjkrVrNyvnJXQNPHwIa/SYfjrOICUgqMNb/MhhnZf0b/ny7Vu6dv3R6l29DeYiWQviDJ3k3lP2" +
                "XEHJfAJbYGsX2Kzlxqlf5nVUS+5CoGk01XuSUy9OKvV8NqByDNJDAKVhpCKWPIvkJA97ZCXPlZpRsdFX" +
                "IjGTeVOvJhAAvLihzbzWkxIXnb81Imjc3MF9xBEYnns1BXZmBzpUAKg3dlv4C+KNo8bAWVJmwvRYIDJv" +
                "HHZ7DaN7LN4QHhf+vXrZJKduSps7zkAEJcEGjP6yoVWvBG4cd1sE6uESNR6i4G4QJTB8JMo9coOr+j78" +
                "tQ1/fbgd9CPrjIawUYyYUn72keevnyPf6Y8givw4RfbX1e2kKYbuHSgs3BxBN7MUXeL2RfdWphyrBQaB" +
                "6oGKmqNTCEe+RYLorkxw73G4vGO5LOddL2jAnnPTO7E/GGBhgvnI+mLuAdP/8n5mjBu8g5vgOQiyvhO9" +
                "98jirDAzgHItHhU9SkMGy1xSPyhJbXl/F9TbM7X6qw18U+hwehCwkIQ8p+9AQmKkiOk+SIA9uURsgFDG" +
                "rdbQ+dELLqtkuZsRhI9xap+gZ37IP6RaDcalz2jeIml7aVI8+scWyvNu9kx8OMnLIJSGWoJ1DjOZwTNR" +
                "kviskc1F4q0L+TPG4IxpMR46HLMh3wDWP1V4jCmHbrwYH2ucKKPknBALcXgRzDTlovRSGqMawrRNOc66" +
                "+UMNHgVnXUyFralVOR2Ns2fzbFtvsisShD8a72eL42F4ybHr6lqOuMnrrj4PmTvo8g6b+0nNdvPqLEk4" +
                "fyT3agGkzwBYelgOrZ9tvlfyKJztFrxsxR+jakKuokZQqhxMjj18Nfqy3rnVJLw4YxHHEKP10uKvvEXs" +
                "jTMz2R/6Mtqd3ujEHvUnfIckv2aSeuMvw+P+8JvfsAFHwPXwY497oL6tspo6SGK7mWskS40IvyqM3zH2" +
                "NV0tr/1c2hjoXVivNvspLHEPb+niQqVNpvCBr47j8p8l75BLuHRvg1wHB8seDEbueS7QNZt85RD8illE" +
                "sSZmRtVN8lzJDgsHfQKtghwSdhVJjhIeYOPtTq0pv574Zk+X8BAluf3BNbVosDZDrqQNU7uj6GoIErew" +
                "3buyfe0xVeMMR6TnHvrtiKRC6fTti2X56mntM7RDborhwgK1CQtHBVnhZPglrQPbvRAKFLFsRzFqk/qd" +
                "d1jrZBzSUIzPglLBhP0uiZUABcRElzQELuvlxsLmfZhnoy8pzoD/nY6Mg5CTW3j/5n+XdN2CePWZAg6c" +
                "iidLK69swoY+OlXWcIOZTa+hps26wc6uGxQzyQSfVRQLDu+2S3YFSUuFe4iYO8+QfYJpHbUX+dopHucE" +
                "+sogUX0HqEl1hB42c8MlEw5SDgpLU4IQhDNHsqdokAKUmoFAfQZlWBR6PpghDtCOebguANXnPGM+gf5D" +
                "If8JGpKy4uk8zBfYbmQ02j0kPgdI4KGQP0KUX/qTBHGc0XIzgnLt/kQ5mdbvkSRf05tDlL3FeYZGYbyN" +
                "d5aMIRSlR4VA2CeJZ5BbNpGLAEaewxCLP5A3YoAeHOP/oLSYRfxD9uXLHx7/1v99/uqbs9dnjx/6n09/" +
                "/PbZi9Oz148f2YOXL84ef26sZmbUoh/ByY/i85ENKuCDwyFhXa83NKZv4gibw7NM9NMJybCTzDFBJtqA" +
                "PqTFilrFKNx7y0wdxDkHIL7Jpbz0lVefIFxQPZZfPxxnP0LOwJ6/pTiTyaJXXbVAwOExmtUNfPA1spuM" +
                "CJG5hcMV6APTx5G3kx8eP0h+/Rh4zV9/A6tTlJT/Hitxobntkmup6PFDaTLiVTzhsy+8koCtyYtyQxS8" +
                "q6ESZHgo3MnrJ6fP/noOfNI1bZMFJjdYM97KFRUdWhXJzqpTR0la1kI4x/wtQ8kFCjmGID24k2/Onn39" +
                "zZvskLD9j6NIk1bEE45Hmi5EaQee+7OQHfIsHOl6dNZsHaXOr6M/knWuW4WhifFOty8PFdc9a8Jr0NjS" +
                "XrFy3VeayZmkO142kvTXClqHlocgQ8JTqXxjkyjvG5RQhbPZZ56pdkgHzAwiNSAewpWc1J3BkTEceOMq" +
                "jvrZFFviQVHNUgAtBILNy6sFLPcXyQn2aWbxNsSUhO4AnG10cKCUglT32xHXeOMBQJcEWOZya00+zNg1" +
                "qILNnshSktA66ZZYZWTsYZmRddBGpLQG9NMjxdO9n4BxN4lt6uTsDfp+jRvdz1d+2pW2FFk6ywrL0cuO" +
                "XoU4RTHrOdjg69LQ/6DbHqrDPLr3ZMMy1zTS3+PDrjbiGFvIpnAAUWyZvJ9w4iQM3h2x/eSID8MR/4o+" +
                "+L4Ug9/chF41pvONJE5WfMQQPuYhNLWFBOgsO4zh1tFONTeUcEX6ZTzTY23PmuQB8FY1/9UFqyq0B9JG" +
                "og0ZzLAFwWbODv1BAPzcIyeOccSPqxEUKqAYutGeziDAoYWOPQrNBqluiR/idKJB6C+w6SdJYcT4IwBe" +
                "vHwD4NpEIV2WaK2A7FiXhaWH2SPTXbFNJmIeWh2MdeySahQsfBeDmkSyajG984YzgjYo8OKydFeJY6Nc" +
                "0QrbIJCX6OKQi2q/Evwn7eg7sk5GEXx0L11k600jrv44HPueFyDbKMbC9wQCgMkIM2tkI453TKxoIK1A" +
                "0lgkBfiFFUh1hyiy3s8ajsRa2CBUvaFHMefKLZdhhzSCMi5JslszE1Ekowo7TksgH2n+CokA7f76CtXT" +
                "hN0mmV19hc6JtrenAWUV+nwoYolkiQ8pFRZIjxTfN9LSgGaZrfrQY3FLPbo+Ga1jPtcBxwyCtLkB2XI2" +
                "+NCYK0bJwpqAkDxbtt6CR6WUsgURcS1sW/PlVb6l9GaPRK7hvY5H2l1BhCeyaG8/sa6Glv2TqLIbRUSX" +
                "sw1CfO8XjOn7gfECgBiyerYnHWbjUA5A7wJC4yIdR5lY1vU7XxBg6r3rbZJ20Cb1AmOGANMWYNGH2sBI" +
                "tQGd4bR1zEJ6WLbdM0cHlsuDqHopEah0ybQb8PF6cVO8g7idsQCFfpDN4iLKE12JnROn8jYQMTsTcIQg" +
                "RyuQAJhTN8s38VTt8RxkFV9HolkWBZTqGOW3tbkPyokYK5rJQ3oHUZL2TDYeQOooOMJ33dQZ7cA+1D37" +
                "0LFJjxRBvFQBaFe5wgOfCNHoQdZTv4K6vxdniLMrr7wm9voSKhcMMcqS1YV6U9CxmulrUfO9CsCEWo+Q" +
                "pJDTSpavR6pl8B5bxFnPdi59g2Q232jzYTiU1nf4wFvcq9rjo5EVWM+K7VqCAdceGURrzWS87I2dX3k/" +
                "+Ffl/Ye6QgodeK2xDapCATtRFsZqpVgbHdNmw2RjdvSAwolWJPcwvQKvsaY2SuMxIB8+OBb8tE72gCsh" +
                "TWhin+5/YpqhHcwQsXWR4yYaS4RzbrPECrYIWEEki3P1YIac+CUD2j6JFprs2nkzDjsyZMJFj9R3PNYV" +
                "WkoVsjS89Y1L6gt4w6ZilwgdaWFO0PjXiI0MXPNOx4A9WYksqzoYD/qskukpr1J0U2WLGillQ1r8huV/" +
                "2RfLygfn54fscfYQWSX857fHyJI8ziwQPz97cf7yNbI/gwcxOeQf/BBScV5hyj71Ggj+5Zz7wc2amGdE" +
                "I4f0Ymr5M95wsCpIi2QWJMWitKNwR+dcXoQbOjIOTJ7PJaU9V17Ol/nCH0aRVLGOPsOgLSva+SzNONo+" +
                "JzVsgg2yKjnDVs9Y8M9944WfRJHK9FYTp06YBfwVeEg/llH8b5jnofJmSGstw5wo7oJPCGeHZLCXMbSA" +
                "lnS24aK3zq38USCikCAa6Gs7t63BWa84XJZNXSHf1ikxXG+i66XkhEOtCZ7LjxATSVEFfA0xWhA3tVVt" +
                "VlMIAx1kZXP7ha2eaJSlm2MfWmTGLVmRo1k9vvdJV+E6povPUGwR95cz1LogcvNywWsn6jgmpE5sVU8z" +
                "d0300zwdJebEb2TKE9HVebvT2Q4vw4hXanmxbbf8H6VQnEdLO2rWzU/CKtr6zG2ukvVRsWEmlzIhqXhp" +
                "ys+zCppORLpyDt23YqQC98YP1HrspwxGIzQ8pfwlTy7LfB9DjQk9ld3mczcJhwXdAq3ULD19ghx8P3ie" +
                "zBVKC4SEGmgPlbuMNlH6ZBL3zmooFl8IIOIi97IKvZUUjiiOMntLg32XY+kqMlvvlNFb3FQixnKxxZ9p" +
                "eEuAGwodO1Kq70XgTXQyL1b6qidRYq5WYLQmUtnzzDAiJ2VVMNX7YLKu7PuzoxL3KxQwEdvbVOWigEGO" +
                "XGtLz7yo4n7/CtkKL04DJ+xB50av+TciC5hmeTzRx54/BjSNu0GhirraBO4fTLZfR2HIIEvtJRGQ+CV6" +
                "86nYrJf0CCTvMs8O94USsQYvtftBSCQ9j9SmPi5C0nlevkeXnzaih1YrbraQbcc+XJaFCAHr96Mn+uKp" +
                "PX8uj8MdkTB+4sfzMAOeRJ5rklct2tG3+PVKfwATyW36d73x7Sxnhp2jz/mnjZXn4pP4mNX3WCJoj/iG" +
                "R9J0KB4wnJZVLs2bKVlriYukqskYqV6Knx779pXBK3/+pLqXtMjLq9FLWQxMqRu2JOnlYAVlBYLekn2X" +
                "4Pu6gct+xf+VnIhYXnUCuaNITojT1Bcky2HBoUdcJIpD7trSPRkKSLwr5yVdVATSt+p83PwBjCfn2uT9" +
                "/vusyfGDHh0jtks7pSSLxgBCWqD0Qqde2fZubOy70mOrrVLh2MqbUKvZqhCsYDFKBt6nL7+SeC1m+1l3" +
                "7IF+zrEYlywh0ydFPZ8MFgvCuiOjSCXaO9sskX/faiM8OBrZ/HD0VOjSu+vx9IVbWrciTvHOEjMTvEZk" +
                "jrveebJLNeJxGanTuuABOjR8MFDSQXCtlw65xT2DtYabe9E6OUF53J2cJGrZtx1v1jDF4op2inx6tfno" +
                "VqR/vwAmnMrDERDxu6iXyHdag6remPfZFpEgJdw38iD4+nmjZ6fBloM/egDYy+utUZgkLQzaTHjYOGZ5" +
                "+Bxt5k2JDBYSuUdpjme6ZYY+0+vDw1vNBiVnfSxbHR3Hof4ezXZ36H1W87PVfbj7PJ5xit6miQEwenh5" +
                "xnwI4wG8kCrAC16qZ/ou0kKHrSrBBUobYs5OTyhKFNQHYk6VEVrf62ts9WB6vTsKVbjGNDiEiC5ZI64n" +
                "e2GGYxgagDE4PLaN2hcjx8imWnZUbi5xTSVjqABbFDdEleVR+YidgfXiCpINJOnpPdcEGWnhlnuz7M7V" +
                "wyylZ5nkx0uZA6v4qHScfU9Hmb3Y2hvtVahQUfEbCn5/BjfYxOAdS1+1JIOd7wYKw8U+0vfbemkk95Sa" +
                "4UcGep9BCHxqyw9sSkF3kfNwklMjdpz9Ksimy2Vbk4GAZsIc8cgMae2lmTGs0h1EQWZYAw7XSUV+dm+O" +
                "7V4c2+5eD7sFhbJrdkDV6537VKoDKD1+c4UXQcQKt2icthPxOkBRY18l90xfztS1Zjn93fq4ngrysE4A" +
                "cbfGZSlettsWYUDSxSSi6S8FyJ1umYM0XDz/4gDWfvV3UEqipFijYn6uV7VBaAk3CqbCKwtYLCz719en" +
                "X4lOe0QDfoguty3+za8sZYf0PzIj8tKn89O7qyl2ykh1ZLUVStDtvwdUHWHQcKClUQLKKJ9JuaWHw/+r" +
                "sdtVY1e8t3Pxi9WYDf+/pMau02Lppfhr4kFpZQrB32DQFewSB/C/g3ffC5vwUvl1O1ebAtbGyUFJKKqV" +
                "UDxAYWd4Y6sd3H8ahWta6c26pHXHbhTdEpHCbU+gaaY2Olj9O5fTpn6ndR3erUKCEQoTCpG6CrUGqdHz" +
                "tEFKjEg/JP72426HOpWbPfunPRqDy7+tA/JyZkmg5Ip/EYn6KY3wU93l24hzr4nRvF4bPA1lwhDl+I9f" +
                "SWomXgASXcOAcF8z2O4tY9+JaM3bbFKRVnWrU+zg5u8JJSEoh8mCd5Hs9Cly2S5J3xNkhUqOTB+j48BK" +
                "h0zcWOB5dx+8UNoSRz2QERihCxSjIaP8d3cstNEgSL82pBY5DRUtqGbonqTu5eslrCtuQ1r4XkBM0ZBc" +
                "v32MK2S344SoWXtfh5OUsN3MUqwkgJAbwhLQ9Hpm7BtVoXrmLwjLlUvWcyMpLNPqbUlfuQ/flGLhXvKs" +
                "mv0O14yPQpuPrNH/YE/4Ko9PScXPC7WAafmNT30Aq3DEbvgFLK3LmZUebAZSN/PsXVVfVf+EOt7uWXzi" +
                "TaW/1k3WhMSH+b16PWNvjygk3no9lIGHIj12Rfw51n6GfM3Od9Jsn3mbQYSC0b717Ql3wunxGUTGkQvt" +
                "ZvX9sPL8DSHo5QdrNrSMkl3u4cnf9XjszOq0UmTRN5PvbU9JEl/GgPNr7xD90ktBv/SOD1uV+1D793A+" +
                "ebXmbmZ3pCROYK7JsVXXa7NAnTYfWRI9NPmEOwjaF4aHSXaeS/TuCDGhK4TIStcQyXcRuyeF73CLCmC3" +
                "jiyFr2QQZPSyrDctfEWH/gHgZ/UlKapIi8Z0C1/nyekpL2AwLpT+vxRIaLpJqqf4u0O9H3AP+eXCLW+c" +
                "Liw12uEwtwOZKIsjXen12fOX352x0x80rdnaIlFe+PKBxoVesQrS3oX+FK2hci2TjE7sQiTy1auzF6e8" +
                "3CJKOK65fzlZ5VhriyL5dkuM9Eufju2bufp2D1pKj+LGM4xklwcan3lbvpY+qr429S1JiqLw5hERfInG" +
                "n3CXHTD9d81sYG2vb0opflqpjO7+6n+yl1/++ezpG34t6tdP9v+QOU8/XgewK2D8iBYNnldkUGPSwAY3" +
                "H16BpDLoMWILtX1+oanlEF75u4wojN4dOBXvXMhdpiucyBOdH/OM8gkFERdoLDQkTU3ZA0roE5mmqHgD" +
                "K/mFP5+/fHGf5V6fdPjxyfNvMwWAXGMQYajZcACSbytQURtXhtfDzKCMszPxGpih3Nl0OUehkXNZvnMn" +
                "2Z3/OCCHD04OntKzOf3y4Dg7aOq6w5OLrluf3L/PG41LcLs7+M87SqKWzeEJSsajsrql7J73buTTQpEL" +
                "epvsAJNK7QZ655z/hNh8iaOqHYPW/LdHXpnwVybatyJOv1TZCB+647n3K2uugMK1AZ+o4jRzJB8oYybJ" +
                "EyspbwFzkgUGyDOyAM+GLDj53b//4XMdQdOrTVIYt4vxgV/p/C/fIsUPF4F5/rBPvYXPf15+YyMUtiyV" +
                "HVwt2ke/1yesqpxkv/v80UP5idENB8B9rq/8CJh9VBKLwWN6KCTEFrACkb5FDXqz5Htp4ujq9YEJNET7" +
                "5i8lie3cm8HUDB9UttrgaKODIc/fZ5/RRf8sm33A/xTS/SYdJCePsTlu/tMDfpdsGn7+lj9n4edD/izC" +
                "z0dv4xfDPn8rz245tzH4Jk3MAKRpUzHgO5+iUS9tHD4ud9dcip2Rs4sSMhAHDosSsZInXq3/9B1G/THP" +
                "Lho3f3zHH4mr8l05bup2XDeL+938zp+6+R/v53+CGM7e8TPCnHOOgJ6Be1HPkCe23aWCkM6fROHv5LK8" +
                "FPbRzTR6CQ3vds+Rg/TpKHAz8uxWwv+9zRFendk1RHAA/kX4aJ12XMm4EF7KEJ+4s6ADOv6yLBjZ820Z" +
                "J1/bqoEMIU4Jr3e325W668fq5/e/pJeuNqTgjO8CRnpZcs8F/GGDg+TD7auw/ErEsvUXxwedWcoM7c8K" +
                "Za7IIbk0453CXTqtRRG2GQyJPWoIbMBn5fBhhZ7YSvp1rdJIg6o2Pnz89BrMY+9euBa0u3qSKrAOeawT" +
                "Pt5MUoCElLcFjcSDtk9piI8J6sQBRjH7IsMESbM8lOCsh60M10q456K/26NfvlWUfC+ZJpSq7MmL09S/" +
                "DILmAUxSEWB1fOeV7fztnyGRQObx+20CcR8QmugHmqktpTGuMBrs5y2gnbQ0je4mn033ibR91zh9y1P8" +
                "WlaaeQtf3LG+qNshQfqsfikB7MP6JAG+Wevm0U96sPoJF5gIkqK1figjPYDM8XHoTprGDrwddxnjP+4r" +
                "0F9//eUT/+KmPyYc1gvfymvCX4vw1zT8ld96N6X0rmnfXL+paZjExXHc2570JunME82ZfrEuJlwifDrP" +
                "Iz/D77z++B7KTj5m6l/eWBz9kbUlu/joVNoK2RkLp0uTr7As0tiN8Shw7E8sppczsQgLIfvScr6jRf2f" +
                "PZkmf5kl9O1qPcAD5Ddx9xHwz2Daf5tZ0tImbVK8awKb76ceSk2VFaT79Wy2WcPIHtFgSEurPME1FMT5" +
                "yorD8bS7P6YzgI+1+64wBSQJiSUiqdS91AQFc9o6va85XvNaorZss+V5hXX97WpeE0QTt02r0O8bPiqs" +
                "txllGoF4MhDVldCsH0IYpFP1E4XaXiAfFxlLL74EvHQW8P+ogWkkGduyl/sPNOM8LaP/AgyYmVCMZwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
