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
                "H4sIAAAAAAAACu1dbY/bRpL+LsD/gbCBG00ylh3bu8hN1gs4nkniIPZ4Pd68whAosSVxhyIVkhpZPtx/" +
                "v+ep6jdSGju5PSt32MsFmxHZXd1VXe9VzRsMBk1b5+U8adN6btpxmS7NwD+c19V61XtmymxsZjMzbasa" +
                "T7+u02b1y5tkVTVNPinMeM4HzSBMaNarVVW342Zdz9KpceAGg0lVFUlaFNUGk/LVytRjN3ZaFUXe5FUZ" +
                "4GCNtG3T6cJk42ryD6w/bqv1dDEu8vJK1rMQl3mZL/N3xo3K8qZNyynXfFqVAJbmZdskq7RdYB3/INrx" +
                "qkjLErvJs0F3fe4Vy+u6Cp7zZkWVtn9+5N/LfEwZt7mg+tL+vli1QAlLu/eVPhjcGjz+H/7n1uD55den" +
                "ybK6Nnk7Xjbz5p6c1K3BneT1Im+SpWmadG4SUKAF/k2SJplppjgHbimpZnggR5m0i7RNNtW6yJKJSdaN" +
                "yQBjk7cLjFildZtP10Vaky/uOr5I2spOTgFK6HSS5OW0WGek76LaYASgpKtVXeFIkxzvyQPyh2mno6S7" +
                "y6wyTVJWrdsu4G4xPy9nVb1MZcfppFq32NJtXXhV4VBvJ0PssWpyGXHxArgYu5/jEeb/ANTMtamTucF5" +
                "4+8GI6Jlm4WgvUivDQ63Nmm2xQ6Wk7wUIuwuH63dKI12x3APc1MtTVsTB1CaT/rkwzKrdWt0NKGOgYcB" +
                "uM4ORwNAeJJQpBKso+9kuONl8jCPnFNbU5dpQYK069q4lRdpmdnJJlnV5q7Ox87KYuupBwap7elDYEjB" +
                "qt4qY31LdF/7h4Qx9lvmSr9rB271aGG8BWmgF37HJvZuwLPCPqJ3NuAYEP8KWUj7agYw/HUbbG/KVjTP" +
                "7X2wToRX02m7hkrYxhAs43Jm8klefrIzlQtvV/nUTsT6XvRiMPgbYJZALxH9ViSbGv9VwBMDXEwg7TBe" +
                "4FjGNBg0T8vRwHGiUvElwF+26XJlsojrHPlMA4UGKclwxNUkneRF3m65m2Y9nYIheyx4kuBnA/BJha3U" +
                "3K9JYQUEAeqA2/Oqym5D5kHmkdeiuu6vIB2gu6W9psjyGmjwDCEkbXplHLag2RUZ3mscmCYxKmCKsilU" +
                "AgNrOoBugRpEMFB0u/DTGbjWa8NFStKZUgS0MCTGUNcjrzgLdbx3cZyWXd2u1l98WbmVMZV6I9kssFRt" +
                "ChBOrLTXX18ooSP1QoWkSrI0PI0UUtDhaRC6McXsJJlAC90I+MQyeay4NnlRkAM94JFsW6FSHOMtp3IU" +
                "65XuB6ita6jLBHSjtXDiUptmXUDZN9UOIkLI6zQvUvgTggIlBnxXc63RjaSFWYV7EZGW6yzTt/lyvVTD" +
                "gUMCuClEoyIoECEvLBokwvAvj+/zFSSKSx8rSz58QCBjC2AsAAidjCbGEsIHZ0UwqyaQxWkhdoRW06jt" +
                "aMwyLWEod62BwMl09BQQKeb0Lkx2b7Vu+B+a8Ez1Pq31ulYF4Db9Ie/ko7kWH1DAtwbfwFziyBbyn7DN" +
                "f3CcOIHNoDfnJX+KLykO2UfbetNmumfdIn0iKL0yS+sMKqpNs7RNhe8W+RyK624BH6HALNWL8rbdrgx0" +
                "lnWmqPHEg6DSdvIABbFcl9DktOJwBDvzxXvoOlDTqqrhHnH4rAZ5CB3/NubXtYH7mjw7OyUXN2a6bnNs" +
                "aEuPqrby++wsGaxBNTArJgzuvN5Ud2lu5zgBv7iyGTZr3kIRNtxn2pxijU8UuRFggzoGq2RNMpRnY/xs" +
                "jhMsgi2YVQUVPMTOX27bRaU8eZ3WuUgqANNsAeoRJx0dR5C57VP4KWXlwCvEsMZvAVt6uMTpLo1boSHG" +
                "HATEQOj06zzD0Ila3WmRw1BDPCc1tNaAs3TJwZ2vSGPVPXIiVDpNU01zMXD03ZwLJafBYOCPkiWRC3Lp" +
                "OS1gGKyCkjQrM81nOZQOfH3wa3CdfjlJwCjAqcVb/Ehhpgs6unz55g19vO5odbPeeLsRrQWOhnIyb8l+" +
                "JiNzPoFRcGtnOK9ibdRBs8qqIYHB07Se6kaJ7Iu3SoWf9NAcAXcfTGlIqRuLngV0oocdtKLnis0gW+sr" +
                "YZrxrK6WY/AAXny087zRqeIhPkn4QKOD2swMXEkIQl/61Sw4ye0pUwEgntkBUdC914aqA0KlJIUlcnHJ" +
                "rDY48xVs8Ik4R3ic2ffqdBOjqs7d3FECPMgPbsDgb2sa+VLghnGHw9FKmah0Hxe3vbiBASV33cHYO69v" +
                "/V9b/9e7Q2EQ6OfR8MfFMCqmanf//PVroD6dFISW70fK/bU5VPqi7/URyczMEIwze9FG7mBwe2XOidpk" +
                "4KieqWg9Ootw8Bvkju7IBPMWgmYdziKftZ1gAifPo2/FImGACx+c76wvZhYw/TLrf4Z4wjq+0T57wdf3" +
                "ogYfuvjLz/SgTINHWQdTn9xyrqodFGW9rB8M7N0z9QOWa/isUOn0KWAzCXlGb4KIhAgS023wAPNyjZgB" +
                "IY5ZrmACgnecl2G5A7F6l1Z72T2xY/4pTetgXGu+87DY7UdLt9KVX+jSO8kz8e0ka4NAGyoKJtvPZH7P" +
                "MZREb7UcMdJyrc+uMUJnxIvxUOmYDS4HsK5s4TGmDM1oPjrRKFJGibRwF+III9Sp83lueTXEPITpzuUk" +
                "aWcPNLSUPetiynJ1pVrqeJQ8myXbap1siBD+qK3/Ld6I25cIX1tVIuiOa3eVu8/rQa+3ON8PqbiD6LUo" +
                "J/2+5KyLMG2KwKWQRXrtdOeTRY+8kDcgZyN+GnUUkhkVolYlYiT/8OGYL7R+rybqxUkLm/QRXCd1/tIa" +
                "yM44ZzW7Qy+CDeqMjmxTd8L3KARoqqkz/to/7gw/yJn1aMJz87/2OAzq9iq1qYwk8puaWjLZyAKUmSN5" +
                "CI6d3pbXdi7tDXQwLFmT/OKXuIu39H6h28YTuMebk7D8p9E75BuuzRvP3d7rcg96I/c8F+iacd4YhMZi" +
                "IlHTCdlTdZwsWZJhZqBVoFuQZ8LBIhGSwy2srQ2qNC3Y4eDkaQG3URLg70xdiR5rEuRTGj+1PQ6eh2zi" +
                "ICe+y+A3C6vaavglHZ/RnkjAFtqna2tcMrCaVDaR2yeoGDEsUDl+4SjPLpwMN6UxoLzlQ4EiVu44xHRS" +
                "6bNebBWNQ7aK0ZtXLZiw30NxxUIBMdYl3Qauq2Lt4up9O08GX5KjAf97HRkGIXU3t+7O/yoGO4xO6ZKF" +
                "3HUmvi0tvlIKZ/rwTKnDM2bevYK+dpYONndVo/JJOtj8o1hz+LttdDBIbyrcIYLyNEGGCmZ20CzSldGN" +
                "XBLoSweJetxDjeoo9LmZRc6ZkZDCkV+aTIQonXmUPeWFGKBUFwTqM6jELFMRYS7ZQzuhfC0A1WZHQ8KB" +
                "vkQm//F6kuxi8Rymc5w4Uh7NHhSfAyT2oZDfg5Rd+oMIcZzF5eMlLW86oYhVJtVbZNRXdO4Qg28h1dAr" +
                "jMbxziVsCEZRUj4QCkqWGhjndSAkgJHsMMriG6S1WKL7J/g/qC4mGz9Pvrz48fFn9u/Ll9+cvzp//MD+" +
                "fPrTd89enJ2/evzQPbh4cf74kaM2E6guJJI92VF8PnCDMnjlcE5YBOwMDSmeMMLNoURz+/GEaNhpYphE" +
                "E51Al9IFkFryyMxbl706CnOOgHydSi3qK6tEgbhs9UR+/XiS/ARWA3l+jvdMIot2NeUcIYjd0bSq4ZKv" +
                "kARlmIgEL5wvjx+IPgq0Hf/4+H706ydPa/76GaSOt6T0t7sSj5rHLpmYkgEAVCfDYN0nXPi51ROwOGmW" +
                "r7kF63MoB7l9KNzxqydnz/5+if3Ea7pDFpg8YE2MK1WUdWhbJImrDh45qagEcY75OUF9Bmo5RCQduONv" +
                "zp99/c3rZEjY9sdxwEnL5xHFA04LUd2e5lYWkiFl4VjXo9fm1lHs7Dr6I1rnplUYqTja6fGlvjy7Z004" +
                "Dxptulcsc3f1ZiSTdM3zWmoDWm5r0R/heUhoKmVyHBL5fY16q1A2+dQS1Qlpj5iepXrIg7kiSd0ZHAiD" +
                "gYfQclTSXrdFrhSVLXnQRUSwfGk5hwn/IhJim40Wt0MMiu8mgHij4wNFF2TE3wy4yGsLAOrEw3Lut9bw" +
                "/Yxdsyq72RNrSq5aJx2MWg6RfVRzmB01YV9aMPrloW7VvB2Ddh93w7HDszcQ/F1edTeh+WHP2iXQ4lmu" +
                "HB2c7uBhiIMU0qK9Y74hW/3PevG+pkwZvitnlpi6lq4gG4g1YY+h92wCZxCVmfHbMSeO/eDdEdsPjnjX" +
                "H/Gv6ZLvyzwog8ZhizWss7UkVJZ8xLg+5Cc064UM6TQZhgDseKcA7Ku+IgAynpmzpmNZUg94q1Zgs2D9" +
                "hbZB+k+0k4PJN8/bTOehsQiAn9vNiZ8c9sfVCApFUwxdaz+o52Hfe8fmhnqNXLiEE2E6t0HoL3Dup1H9" +
                "xNFHALy4eA3g2n0hHZroyQD7uPYMlz9mc027YX9N2LnvkXCkY3tVrWDhxzioUWyr1tM6chAT9E+BFte5" +
                "2UROjlJFy3G92F6CjSEX1UYn+FLaCnjsWiCF99H2tEhW61o8/5GX/I5HIMcoVsM2EwKA4xFm3EhGSHjI" +
                "tmhorUDi0CQG+IUrqOoJkWetz9UfibVwQCiUQ5VizsYUhT8hDagclSQVrrmKwJJBi53ENZL3dI351IC2" +
                "jX2FamtEbseZbbVBs0XTOVO/ZWX6tM9iEWeJPyklGHCP1OvX0gWBLput+tMjcVHtdm2eWsc80gEnDIi0" +
                "HwKJdHYG0arrjqKFNSUhybdktQWNcil9y0bEx3DHmhabdEvuTR4KX8OTHQ20IYMbHsuinfPEuhppdiVR" +
                "eTewiC7nDgjhvl0wZPZ79gsAQgRryR61po18pQDtDoiUs3gceaKoqitbK2BWvu0ckrbeRqUERwwBpr3D" +
                "og+185FqAzrDaM+Zi/Bh3HZljs4slwdSVSHRqDTWNGvQ8WZ20317djtnhQotJOv5IvATvYkdiVN+67GY" +
                "kwm4Q+CjJVAAzImZpusgVXucB1nFVplomUUBxTpG6e1a5Hv1RowVzWQhXYGVpK+TjQrgOjKO0F0PdUo7" +
                "sG/rlnxo9aRrioBeqgM0rVzhvs2LaCQh66lrQd3fiTnE65VXVhNbfQmVC4I4zKLVBXunoEO505apZnsV" +
                "gGNqFSHJK8dFLluwVMtgnbawZ5XtVBoOSWy+0a5FL5SuYfG+tbibyu5HoyyQniXdlUQFpjl2EF1PJ2Nn" +
                "a+zsyvvBv8zvPdAVYujY1wrHoCoUsCNl4UitGGuHZNylGB3Mjh5QOMGKpBamVeAV1tQOazwG5OH9E9mf" +
                "ltDucyVkDR3bx+cfmWZoB2eI2PPIcWMZN/By7maJFWwQvAJJ1u2q3gyR+ILBbRdFBXdnj513xmGHhxxz" +
                "0Sm1rZJViV5UhSw9cl3jEvsC1rAp20VMR1yYInT0q8VGeqpZp6NHniRH0lUdjPtdUsn0mFbxdmNli/Ip" +
                "eUO6Avv9AXIuLk/vnZ8fk8fJA2SY8J/PTpAxeZy4oPzy/MXlxStkgnoPQqLIPvjRp+WswpRz6nQY/Av6" +
                "9717ObdC2hHNHtLBqZXRcDvClUYa5LbALC5WO/ZXfC7lhb/gI+NA59lMktwzJeesSOdWHoVZxUDabIM2" +
                "cmvXtPTsaMedVLgJ1rOrpBAbFTPvotvmDDuJXJXopShOHTMp+Dv2Ic1bDuN/wzwLlbdKGtduzIniMdj8" +
                "cDIkhS2boXE0p78NL70xZmmlgRsFE9FG39j17Zqj9XrEdV5XJdJvrSLD9ca6XoyOl2tN9ly/B5mAiurg" +
                "G5DRWrnTXOV6OQEz0EdWMjdfuNUjpVKYGc6hQaLcpSzSddGG9zYHK1THdHEbsi2i/3yKAhhYbpbPeWVF" +
                "fccI1bFb1eLMUxMVNYtHiUWxBxnTRNR12ux0xcPRcMgrtrwXt9sZELhQ/EeXhdQMnJ2EVbRhmsdcRuuj" +
                "hsPELnlCMvPS0J8mJZSdsHRpDHp2xU556o3uqwHZjxnshm+KiulLmlzn6T6COiJ0tHaTzszYCwsaCRop" +
                "ZFr8ZHNw/+B8Mm8o3RESbaCjVK5CuonSRRN5eK6k4kIMAcS9yJ2uTG80eRGFKLMd1Zt4EUtTkth6H40O" +
                "47oUNpZLMVam4TABrq977HCpvheGd6yTWLbSVx2OEou1BKE1qcpOaUYSKTErvbXeB5PFZtvVHfS4XSGD" +
                "ldgeVpuLCmaaRm7FxWIv2rjb3ULKwpfT8AnH0JrBK/6N+AIGWh6P9bElkQMaR99AUrldzQKPEIbbrqMw" +
                "ZJDL8UVxkHgnenEqW68K+gWSfZklw30BRajNS02/FxhJbyQVqo2OkIOe5W/RDKgd7L4Xi+ctaDvJ99dt" +
                "wUXY9dvBE33x1D1/Lo/9FRM/fmzHU54BT+LPFdEr583gO/x6qT+wE0ly2ned8c00ZcKdoy/5pxsrz8Uz" +
                "sZGrbcVE6B726x9Jb6L4wXBdlqn0eMZorSQ6kjonI6WqEG89NPwrgZdWBKXeFzXWy6vBhSwGolQ1G5b0" +
                "erGCcvWCzpJdr+CHqobjvuH/SmZEjK+6gjxRpCjEdeoykstkwa1HdCS6Q67q0kPpM0i4amc5XbQE8rji" +
                "fxxEBoPs3JzI338jNpJAaNMRgry4j0rSaYwkpEFKr4TqvW/rz4a2LJVcbaTykitvfPVmq3ywhN3IGYGf" +
                "XXwlgVvI/LMY2QH9nGMxLlpCpo+zajbuLeb5dYdNkVN079x5iQjYLhyhwfHAzffSp3wXX4APAuhvoR+E" +
                "o8J9J6YoeAXJefB6X8pdyBG/y6E6qTLK0NDtBwMlLwQHuzBIMu4ZrIXd1PLW6Slq5ub0NNLMtkF5vYJB" +
                "Foe01c3Hl6OPDyQA+1nQCwDViZcC4cBFVSD36fpY9dq9zbwIEynutscHgdivaxWfGqcOEqkMsOXX2iQ/" +
                "SVobtNtwWBtmfPgcPel1jmwWkrrHcb5nsmW2PtE7yP2r0Q5KynJZsjw+CUPtHZzt7tB7rPIny3vw+ymh" +
                "YYrexAnBMFp9KWY2lrEAXkhF4AVv5jOVF3Ch51bmoAIZDvFnq0KKcgVVghhVJYSW+7p6W12ZTluPQhWq" +
                "MSUOPqJvVosPyh6Z/hjGCCAM5Mcdo/bLiCS5qS5TKreeuKai0deBDQodos3SoH/E2sCGcQXJDBL1+LJs" +
                "tBnp9JbLt+zgVXmWerRMsuOl5IFVbHg6Sn6gx8yWbW2htlpUsCj5IQZ7Pr0LcGL2TqT9WhLDxnYJ+eFi" +
                "JekEbi03knqKTf9LBZ1vKXg6Nfk7Nqug68hYOJHUiDVnHwsy63Jj1/GA32ZEHPHL3Ka1x2bK+EpPEMWZ" +
                "fknYX0gV/tm9dbZ76Wy7e7XsIDpl1/ZQt7zauYilaoAMZM9XyOG5LDPz2minES8OZBWOVlLRdOqc0tak" +
                "p72jHxZUXu6XDcDxrrlZapnNtkFIEDU4CXfa6wNyN1zmICsXVIB4gpVd/Qp6SfQUS1ZM13WKOAgz4U/B" +
                "YFh9AbuFZf/+6uwrUWsPacaHaIDb4t904zJ4qAYgSyIvbXY/vv0a704JqR6tdknJdrvvAVVHOGiQaeme" +
                "gD5Kp1J96ezh/zXZYTXZhpd8Fr9Zk7nh/5c02U2KLL5Zf0NgKC1OPgrsDdrANHEA/9t794OQCS+VXoe6" +
                "B+X3ve8mlNh2r1l8OQGlnv4Nr6Z3WWrgr3XFl/Gifh53/ehgeJLkHkenn5rgaXUva07q6kqLPbyLhZQj" +
                "1CbUIjUWChBSuKfMgVccnnZI+G3HHQpB5Z99p6i9G73rw43B/kV4iaMkkH8TlvptDv9TXefDRL43BG2u" +
                "Xaj32FcQfdxjP6gl+ZpwYUj0DkPEfa1iu/eUbbei6/Fm/4o0tbv6RW8Xgzv2XlEUlHKYLHgHSVCbOpcT" +
                "k7Q+QZYo8sj0EZoRXFWR2RwXit7ZB89XvcRv92h4QugC2aBPKPstHxfpaEykXzBS6xwHjy7MZjAfpfTl" +
                "iygsOW59uviu35huQ2oA7gNfPusdJgQt2/nonKSK3U0u3ZXEE3K7WOKbTjuN++6VL6zZy8VyUZOl3oAK" +
                "K7h6x9IW9f13qljTl/yrZsX9FeVj3wEka3Q/AuS/9GPzVOGTRQ1guozHhz6qlRnurv9VLS3ZOYvdOwwk" +
                "c2bJVVltyj+kxLdHHJ9Yy2lvhZM6Phvi3GC9y7G3iRRM7zpBlIZDYSB3w/w5Fn+GJM7O59fcUfPeg/AF" +
                "43/X1ScE8gJkM4uMLOfa7mo7ZuX5a0LQaxKuFdGlmdxNIAr/rgPkxFan5cKOtu18b/NKlA1zBLi88cLR" +
                "b71B9FsvBLGfuQu1e2nnQ/dwAMLdqJKwgQkow15eq9A8dtqa5JLrvgXI31bQrjE8jLL2XKJzoYiJXkFE" +
                "VroBSb4Lu3uS2f63oAN2S8xSE4sGgUev82rdwHU06C7A/lzpSeot0sAx2cLveXJ2xqsaDBOlOzAG4lty" +
                "osIq/m7RDQC4Q34QcctLqnOXL20hz02PJ/LsWFd6df784vtzXh4ATis2vkjQ5z+coGGi1a2yaetRfwhX" +
                "X9SWSQ5PnEJA8uXL8xdnvAYjejisuX85WeVEy47C+e5KGfGXLh53bs7zd7enpSopXj2jSvaAoDOa1+wr" +
                "6bLqKlTbsKRbFNo85AYv0BbkL8EDpv1cmhtYudcfTy9+WK1APf7uf5KLL789f/qaX6D6/ZPtP6TP0/fX" +
                "B9x9MX6Yi2bP6jJoMulwg9cP30CSG3QdcYraYj/XlLMPuOzdR5RN7/RciyvjE5rxCqfyROeH5KN8fkE4" +
                "BkoLHUsTp+8BxXeRTOKtWDMrGYdvLy9e3GMx2KYhfnry/LtEASAB6bkYmtbLQPRdBupqR5X+XTJnU0bJ" +
                "ufgOTFvunLqIku/0LPIrc5rc/o8jUvjo9Ogp/ZuzL49OkqO6qlo8WbTt6vTePd6ALEDt9ug/byuKWlSH" +
                "Pyg5kNKVNOX0rI8jHyoKVNCrZ0eYlGu70JUx9rNkswLSqi2FrjtwD8OyEKBEdN+ZOPtSecN/Qo+ib1fW" +
                "7AGZaw06UctpLkk+esbckkVW8uAC5jTxBJBnJAGe9Ulw+qd///yRjqD11S4qjNvd8ZFd6fJv3yHvDy+B" +
                "yX9/Tp2FL38tvnEjFLYslRxt5s3DP+sTVltOkz89evhAfmJ0zQFwoquNHQHLjyJj1ntMJ4WIuAVc4Ujf" +
                "ojy9LvheWjzaanXkGBqsfYjrS2JA96c1Ne0Hxa2WOFhqb87Tt8mn9NU/Tabv8D+ZdMhJi8npY5yPmf1y" +
                "n986m/ifn/Hn1P98wJ+Z//nwTfgK2aM38uzgCY/+h21CTiBOp4ol3/mejbprI//ZujvOt9gZOV3k4IQw" +
                "sF+vCHU+cW/tR/Uw6i9psqjN7PFtKxib/Cof1VUzqur5vXZ2+6/t7C/30r+CGadX/Ewx51wivmccn1VT" +
                "5I/dAVNNSHdQpPZ3clyWF7vbTTSS8X3x7mokB+nTgSdnoNmBsgF7+ydcMsDdXAQR4Gv4z+FpY5b0U/ho" +
                "U4bYnJ4LQKDsr/OMgT7f5mHyje0cSB5CVngpvNku1XU/UZ+/+42+eLU+Cud853ek9yv33NzvN0FIqtx9" +
                "eJZfmCgae92818ClxNA2Ll8ECxSS6zXWQdzF03UywkiDIKGVDUEO6KwUHpboni2ls9fVIWlZ1dj776ve" +
                "sPPQ4ucvEO2uHmUOXC891vHfhyYq2ITUv2UbkTftPsMh/iawE2cY1e5FggmSdXkggVpntzJcS+WWivYW" +
                "kH5cV7dkW840v1QmT16cRb5mYDQLYByzAMvnO6/syf8hYiQ8SCnqtRKEo0Ckop+Bps6UFrrMoeF+HmTn" +
                "Ue8T9ht9n91m1/bd/LTNUeHDW3E6zn+2x3ZQHQwL6cn6zTiwaeuDOGhn12EuGYeWLad9g3dNbLQrAIpJ" +
                "hZHpP4zdTd844XeiL2PsZ4QF/Kuvv3xiX3z8Dxf7FW/5r/DV/q+5/2vi/0r/gB5MaXgj1Xf6oPpZXkjn" +
                "3o6m11E/n+jS+EN4IR0T4NOvHtgZlgH0xw9Qf/LVVPvyI0bZ71ld048Pz6QfkV21cMY0QQtzI03hmIA6" +
                "yP7MY3y3E6uwXrIvb2ebYNQv2pOKsndhfM+v1gwsQH6Cdx8Gfwzd/vv0kl44aa7ibRX4AnbqUMqwrDXd" +
                "q6bT9QrG95hWRNph5QkusiARoNQYjibtvRGdBHwn3raTKSDJWBQItWLPUzMYTH3r9K4WecWLjdrxzY7p" +
                "Jda197N50RA94G5aiV5h/xljvQ8p0wjEooGwL4eifeeDJJ2q3z/UjgT5VMlIWvklIqYTgf8fEUw1ydiG" +
                "reCf07xTZm4N/gvWxofWC2gAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
