/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceGoal")]
    public sealed class PlaceGoal : IDeserializable<PlaceGoal>, IGoal<PlaceActionGoal>
    {
        // An action for placing an object
        // which group to be used to plan for grasping
        [DataMember (Name = "group_name")] public string GroupName;
        // the name of the attached object to place
        [DataMember (Name = "attached_object_name")] public string AttachedObjectName;
        // a list of possible transformations for placing the object
        [DataMember (Name = "place_locations")] public PlaceLocation[] PlaceLocations;
        // if the user prefers setting the eef pose (same as in pick) rather than 
        // the location of the object, this flag should be set to true
        [DataMember (Name = "place_eef")] public bool PlaceEef;
        // the name that the support surface (e.g. table) has in the collision world
        // can be left empty if no name is available
        [DataMember (Name = "support_surface_name")] public string SupportSurfaceName;
        // whether collisions between the gripper and the support surface should be acceptable
        // during move from pre-place to place and during retreat. Collisions when moving to the
        // pre-place location are still not allowed even if this is set to true.
        [DataMember (Name = "allow_gripper_support_collision")] public bool AllowGripperSupportCollision;
        // Optional constraints to be imposed on every point in the motion plan
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints;
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId;
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of placing;
        // CAREFUL: If the object name 'all' is used, collisions with all objects are disabled during the approach & retreat.
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects;
        // The maximum amount of time the motion planner is allowed to plan for
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceGoal()
        {
            GroupName = string.Empty;
            AttachedObjectName = string.Empty;
            PlaceLocations = System.Array.Empty<PlaceLocation>();
            SupportSurfaceName = string.Empty;
            PathConstraints = new Constraints();
            PlannerId = string.Empty;
            AllowedTouchObjects = System.Array.Empty<string>();
            PlanningOptions = new PlanningOptions();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceGoal(string GroupName, string AttachedObjectName, PlaceLocation[] PlaceLocations, bool PlaceEef, string SupportSurfaceName, bool AllowGripperSupportCollision, Constraints PathConstraints, string PlannerId, string[] AllowedTouchObjects, double AllowedPlanningTime, PlanningOptions PlanningOptions)
        {
            this.GroupName = GroupName;
            this.AttachedObjectName = AttachedObjectName;
            this.PlaceLocations = PlaceLocations;
            this.PlaceEef = PlaceEef;
            this.SupportSurfaceName = SupportSurfaceName;
            this.AllowGripperSupportCollision = AllowGripperSupportCollision;
            this.PathConstraints = PathConstraints;
            this.PlannerId = PlannerId;
            this.AllowedTouchObjects = AllowedTouchObjects;
            this.AllowedPlanningTime = AllowedPlanningTime;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PlaceGoal(ref Buffer b)
        {
            GroupName = b.DeserializeString();
            AttachedObjectName = b.DeserializeString();
            PlaceLocations = b.DeserializeArray<PlaceLocation>();
            for (int i = 0; i < PlaceLocations.Length; i++)
            {
                PlaceLocations[i] = new PlaceLocation(ref b);
            }
            PlaceEef = b.Deserialize<bool>();
            SupportSurfaceName = b.DeserializeString();
            AllowGripperSupportCollision = b.Deserialize<bool>();
            PathConstraints = new Constraints(ref b);
            PlannerId = b.DeserializeString();
            AllowedTouchObjects = b.DeserializeStringArray();
            AllowedPlanningTime = b.Deserialize<double>();
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceGoal(ref b);
        }
        
        PlaceGoal IDeserializable<PlaceGoal>.RosDeserialize(ref Buffer b)
        {
            return new PlaceGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(GroupName);
            b.Serialize(AttachedObjectName);
            b.SerializeArray(PlaceLocations, 0);
            b.Serialize(PlaceEef);
            b.Serialize(SupportSurfaceName);
            b.Serialize(AllowGripperSupportCollision);
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
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (AttachedObjectName is null) throw new System.NullReferenceException(nameof(AttachedObjectName));
            if (PlaceLocations is null) throw new System.NullReferenceException(nameof(PlaceLocations));
            for (int i = 0; i < PlaceLocations.Length; i++)
            {
                if (PlaceLocations[i] is null) throw new System.NullReferenceException($"{nameof(PlaceLocations)}[{i}]");
                PlaceLocations[i].RosValidate();
            }
            if (SupportSurfaceName is null) throw new System.NullReferenceException(nameof(SupportSurfaceName));
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
                int size = 34;
                size += BuiltIns.GetStringSize(GroupName);
                size += BuiltIns.GetStringSize(AttachedObjectName);
                size += BuiltIns.GetArraySize(PlaceLocations);
                size += BuiltIns.GetStringSize(SupportSurfaceName);
                size += PathConstraints.RosMessageLength;
                size += BuiltIns.GetStringSize(PlannerId);
                size += BuiltIns.GetArraySize(AllowedTouchObjects);
                size += PlanningOptions.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e3f3e956e536ccd313fd8f23023f0a94";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08a3Mbx5Hf8Su2zKojGUOQLCopHx2lShZpWy7rEVHxS6VCDXYHwIaLnfXOLkHo6v77" +
                "9XNmFgAl+XJk7iqXpCJid6Znuqff3bOj0ch3bVkvskXr+mZam5UdhWem60y+tMXUzf5u807fvqpMbn9w" +
                "uelKV799lzX4e1rJAw8jRjPnKnlu7XwUQfq+aVzbTX3fzvGtgJQZpqrcerpoy6ax7VTH5q6qSg+gYdhT" +
                "WKBrTVl3PmtMt4SX4UGyDCxd1wCiLEbxKWyVFgCEOtfnS0EL580rZ7o/PQrvaT5MmXaloky/XzaEYhbe" +
                "u0Zwfvw//J/R84tvT7OVu7JlN135hb8/IPvoIHuSIfGyuWuzbll6OEHjGyUAIn6QvVnaDChj29rAcTjf" +
                "9a3N3BzG22xp6kImW5l7gGNKxhDf2jm8B0IbmNV7W4yA0Egx1254S987gP4mPKQlpnzsspruQgHr6rYu" +
                "7gF4mjfcRdbaClC8slnnMgO/5ra1dQ6ItoguwDvqlqbLAGNTrc3GZ76xeTkvLWy48na9hPHjrHYwpGbK" +
                "rKz3ZmGPRwvrVrbT3b9y3l50ZtXAzLDpsGPTNK0D9oczIIJ/y1wJ2Na+ojPImtYKtjpYJ7ewioVNfmBu" +
                "JJUMxrkGCEQcBecFPN8hudzMgxhW1meE9xqPDqjj7crUXZkDlkC/FUM1M9d3BKfg0TlAnCEpexTk+03v" +
                "8R9kq4LJY7Pc9a0ntiD6A/d8VF5uh9k/wl2j76wpbJst6Z+4x7/jMFIkfrQ15RX+RA3lWD/czr59V/CG" +
                "eYNAfuCqujBtAZzXmcJ0hlh8WS6AN+9V9spWMIkZj952m8b6CfEOsCvKsgXtBcTfkNyhJORuterrEqQf" +
                "ThOU0mA+zITDNKARW+CIvjItjHdtUdY4nAQHocP/vP2tJ2l6dnaaoe60eY/CBiuVdQ5s6FF7PDvLRj2Q" +
                "7OQhThgdvFm7e6hHFkD+sHimYmivQRA87tP4U1jjD4zcBGADcSysUvjsiJ5N4ac/zmAR2IJtHIjXEez8" +
                "1aZbOubGK9OWZlZZBJwDBQDqIU46PE4g1wS6NrVT8AwxrvEpYOsAF3G6hwqxYhu1AALCQJDpq7KAobMN" +
                "S0pV2roDwZy1BvgRZ/GSo4NvSDmRtqETQe3kvctLOIAiW5fdUjUznQYZptvhxo9IEYkEoHyOqq1LVTc8" +
                "D6oUDhW2DKcdDMLbcQZcAgh18BZ+mDy3FfAovXz3DiC64Wg2Hu+Cck/WAnYGnWSvkfdsgZz5pKoSNX5l" +
                "qt6y2REd5ZG6wNCwI+PpCUk9WUCPg7awnADqwaqTApCNJc8iOsnDAVrJc8ZmVPT8ijhmOm/dagoMAC9u" +
                "6TBvNFhk//E38VZiJbflnk2ByuyWDiUAbPTuav+08daixgBZMuoT4CbwHOethdNuwCyOUeXh40Les/+A" +
                "6Li21LmTbEQcHQaM/tobdHgIbhx3VwiycJEaB8XUgV/K7Jr6P0aEbYBucEGvw1+b8Nf7u9l+JJ3iEA7K" +
                "k2sS6TncPP76LdId/ZHJ6CMY6V/ru/Cddx0wwLCw87JGPYO6KThm0RmlKWO2wIBgU+aXfUNqDt22rDP+" +
                "0gMYnGCvQbgqdsarct5R9AQEI4cJzhwPvSP7AwPUWcRBpBnpxVwAo/+FJgleFGUL0xOvOdnnli/7I+m9" +
                "E3Vnw8wAynp4VAwwLcDBNLWyHth6GTQNL2TyqoyD2eqvevBNZ5Y8CLCQCHmOvgMiMrOACo5DWWeXHp2E" +
                "K1tjQGlXDej8sCSATpa7HUb4EKX2MXomQ/4h1aowrujfO0VtL068j6HYTtAjfEY+nKvBA1xZiCjQOoeZ" +
                "MDGw0gQjm5YOd5yVXVY4kB0IswDGylxiQAIOFQVtTQPAhlIFj2HKkZ0sJuMMQrSaR5Gc4C7I4YVgpi0X" +
                "pXBpjGooEBTkxlk3fwgnAw4D7ZkXY2ZrHSun40n2bJ5tXJ+tESH4oxU/mxwP3ReJXeccibjy664+1/AR" +
                "aOc7ONyParbbOepUnSVZED1s3WSwOiYEkByEhpxFlqRM1PdKHgXZ9kBLT/4YqiaTLRwEpUzBROzBV0Nf" +
                "VpxbTueQMxb3GGK0Qa7mlVjEwTg1k8OhL6PdGYxO7NFwwo8lqKKyKrvNYPxVeDwcfvsHtkURoHr4scc9" +
                "YN+WSY06iGK73LZ4sBlE+HWh9I6xr+pqei1z0cZYivR99jYscQ/eoosLKm06Ax94PY7Lf568MzNA4F3g" +
                "6+Bg6YOtkXueE/QReXxrC8EvmcW5SXI+7CYJVbKjwoI+sT6mgMoVZgDZ7rjMUUiSsm/2tAIPscWX723r" +
                "SIP5rAJZCFO74+hq0CbuIlW3w9s3iikbZ3BEBu6hHEdEFZTO0L5oosnNHEak9eUONclwwQJOmQVHBV7B" +
                "yeCXeNtRqIzbIChk2Y5j1Gbahe3EYXXJuLWl+CwoFZiw3yUREFMCMeUldQNXruo1bN6382z0NbIzwP+R" +
                "R8ZB09YuxL/538Vdd8BeQ6IABc7Ik0Urz2SCAz05Y9LgAQNTeQdqWq0b2NmmLVclEkGyimTB+4ZzJXIq" +
                "Tjzk7MigOenRtz8e+aVpLO/jAoG+UkiovgPUJO+LHnYHNr/EhAM+iksjB1HdAByx3VAxBUipWYL6DJRh" +
                "UZTqGERoYxSupfWa84z5BPQfCvonaEjkFcHzyCzguMeYWNhF8TmAhH0w5A8gJUt/FCEcp7jcDqPceD6R" +
                "T2buegzk4UR5vgF5LjDQAdan/DolYxAK48NMQOSjxDOgW7aRigAMaW6xzIApwJYM0IMx/BeUFmYRv8y+" +
                "fvnz4y/k74tX352/Pn/8UH4+/eWHZy/Ozl8/PtEHL1+cP36kpMbMqEY/tCcZhc9HOqgAH7z2VLYYDI3p" +
                "mzhC56As4/bTCcmw08xigoy0AfqQGit2VFEp7LVmpg7jnENAvjUbXOEbUZ+AOG11TL9+Hme/jClq+DXd" +
                "s+FoCRRLvYCAQ3aUuxZ88MYRlTFzCw5XwA+IPom0nf78+EHy65dAa/z1K5A63RLTX3ZFLjQeO+VaavT4" +
                "QWlixMv7BJ99IUoCbI0pyh63IK4Gc9BkcK7T10/Onv3tAvaTrqmHTDDxgDnjzVRh1kGrQtlZduqQkypH" +
                "iOOYXzNzXYJCjiHIAO70u/Nn3373JjtC2PLjOOKEycl5SvGI05KUdqC5yEJ2hLJwzOuhs6brMHayDv9I" +
                "1rlpFQxNlHZ8fMbbm9d8igeClNJXWJgbKs1EJtEdL1tK+k9YZMom8hDRlAp7cEjI730zZspmnwtRR1uS" +
                "KPQLLLWFPDBXIqk7gyNhcOCtqzjUz6rYEg8K1SwyoIZAYPNMvQDL/VUiwZJmJm+DTEkofoJsX1kspVj/" +
                "9t0I13gjAECXBFjqcoPL0UNopDN2DSrtZk9kSUlonnRHpFI09pBM0Tr0cVNcA3p7wvu011Mg3G3uNnVy" +
                "9gZ9v8eNHuYrP+5Ka4osnSVylHjZ0asgpyhmPUc35VKHaeh/0G0P1WEU3Xt0YJltW9SzGnYlBfHY1zAD" +
                "B7Dv7PR6ihOnYfDuiM1HR7zfHvGv6IPvSzHI4Sb4sjGd95Q4WeEjDOFjHoJTW0Xpc+xq0HDreKeaG0q4" +
                "xP00HtNjfmBNTAC8Yc2/XmJVBe1BSe4qDnaYYQuMjTk71yLg57I5cozj/nA1BDXbZDC0bzVnzQysKUDq" +
                "UWj7nJdIpuM2EPoLOPTTpDCi9CEAL16+AeCAT06p5nLVrzBNvgIuwz81PezB1HVra+tk56HVQUmHjR8t" +
                "gwXfRaEmkSxbTHHeQEYqKi9dlXadODZMFa6wbQXyFF0c4aIN0MDMwH/yS9dXxTHCpVoBMr7vwXts+pZc" +
                "/UkQ+4EXQMdIxoIhIADlEcyslZxPj4kVDqQZSBqLpAC/0gIpnxCyrPhZ2yNhLTigJegQi3PWtqrCCXEE" +
                "pVSiZDdnJiJLRhU2TksgH+ixCYkAbrL5pnWrhNzKmZ1bm7bwgzMNW2amN9sslnAW+ZBUYQHuoeJ7Ty0N" +
                "K1Nv2IeekFsq25VkNI95xAPGGfGB4QodKDMy5n6btzkBQXm2rNkAjcoiSCi5Fnqs0qYEA0+Irx06zdxd" +
                "gRue0qKD84R1ObQcSiLzbmSRiczhA4L4XhaM6fst4wUAYsgqZCfVwH1+k1AOMN5DaFyk45AnKucupSCA" +
                "qfducEios4DGsV6gxCBgDVcNUR/21NiCagN0BpzArO9CSA+WbVfm0IHF5QEpV11JvqgEqEDHm9mN9x3Y" +
                "7fyKNIfrF8vIT+hK7EjceJ8WU5kARwj4aGVIWGY2N32Uqj2eA60idSQ0y51mIVTHML1hk7T0VjkRxpJm" +
                "EkiXwEorBx4tNh4A1yHjjCOz5IbLGDtbF/JZUFrgkUIQT1WAynG55oEkQjh6oPXYr0DdP4gzyNmlV6KJ" +
                "RV/WWKmpFLNkdcJeFXSsZkotar5XAShTswhRCjmtZEk9ki2DeGxxzyzbII+2xd5CejOmXQahpNIRIPNA" +
                "LO7ayX44sgLSY8W2oWDA+mOFiEFeZTtslFJjJyvvB/+qvP+QV0ihw74aiz40qtDjSaoslNSMMXZ0CbJi" +
                "BpOD2dEDDCdaESMwRYE7WJP7QOExNlA+GNP+uE72AFfyqmaH55+YZtAOaohgS1McN+VYIsi5ziIr6CFg" +
                "BSSrDbs+6QyS+AoD2iGKGprs2nk1Djs8pMyFHql0PLq6xi5FCby3jUvqC4hhY7ZLmA5xwZyg0q8lGxmo" +
                "Jk7HFnmyck7JVpSoIaloekqrdLupsnUZxVfU4rdd/qdz0ax8cH5+zh5nD8fZL/DPF+PsV/hHA/GL8xcX" +
                "L19Pf3289SAmh+TBzyEVJwqTzmnQQPAv59xvtXvHPON8zr2YXP4M5xGqID63mC/XKO04NI5f0IvQNk7j" +
                "pgiPUtpzpuW8MgsRRuJUso6SYeCWldZ2fUvd2dI+RzVsBBt4lXKGnmUs+OfSeCGTkKWy0KNfTzEL+Dv2" +
                "Qf1YivG/wTyBWi+4KZtahnEiuQuSEM6OkMDCYx4iHHS2wUX31q5EFHCjwEFooNOeZoR5ZUCXIKra4Iy7" +
                "sfVV2bp6hRk+QgbXm/J6KTpBqDnBc/UBZCIqrIBvQIYL4qq26n41A2ZAB5nJ7L/S1RONUtl5h372g7Em" +
                "K0xfdfG9JF2J6jCdfIZiA3F/mU9b7HKdlwtsqGfHMUF1qqsKznhqpJ/m6SgyJ3KQKU167tvVTh9BKUcv" +
                "Q5FnbPG2xW75P3LhJOJMsyVegEmwCrc+4zHXyfpj8oWJJygV36LZM1kNmo5Yura2QOsGYAP1Jg/YeuzH" +
                "DIxGaHhK6Ys0uSrNPoIqEQYq25u5nQZhmSJCo4gfbQ58P8fNp9QCQaFGwQFymEh9Mol7pzUUjS8IEO5l" +
                "1fSU8q6LRERbS72lwb6TWNoaie2Jiugt9nXOugb9LRYD8JYAbih07HApv6/lhg+xTiZsxa8GHEXmagWE" +
                "5kRqyZXBtSkpPFJTvQ8m1pWlPzsqcVmhABOxuUtVTgoY0Kkllh2q7GH/CpIVvDgOnOAMOjt6jX9f4J/8" +
                "eMqPhT4KNI27AUNmdbYJeH5Oby0IDBqkqb0kAiK/hMYDRzUVegSUd5lnR/tCiViDp9r9Vkj0RnupJC56" +
                "+y6bl9e2mHIjemi1wsMmtFXsw7UrYCHY9fXoCb94qs+f0+NwRySMn8p4FOaq4sizQfTqhR/9AL9e8Q/Y" +
                "CeU25d1gvM8NZthx9AX+qWPpOfkkErNKj6UfJ/sNj+yVeJEOnJaVoebNFK2G4iKqamKM5Cry02PfPhN4" +
                "JfJH1b2kRZ5ejV7SYk9xLrYk8U05BqUFgsGSQ5fgJ9eCy77G/6ecCFledgLxRNdL27HiTBlJc1jg0ENc" +
                "1GkumtyTbQbxDlmTgDCnk4oovTgfty+AUXJuTN7v3GBKBInkDPToBGK7LumUoiwaBhDUAsX31ZBD/Ejc" +
                "2Nh3xWLLrVJBbL/naVKr2TATrMBilBh4n738huK1mO3HuuMA9HMcC+OSJWj6tHDz6dZigVl3eDQ70uRL" +
                "OCzif2m1IRocj3R+ED1mOryvoFc0o/SFW1p3wk7xzhJmJvAakTrufOdJL9WQx6WozlyBAnSk+4GBlA4C" +
                "17qypt03mGu4Rljr9DQHz+H0NFHL0nbcNwXjCvaJNj+8AXgX3L+fARNKmSACxH5LVxU+NKgW1udtKdkW" +
                "4iBGXBp5IPj6rWfZaR3dY2IBwF5esUZhErUwcDPhUWuvqHpD/ett6VHc8uM0xzPbYIY++8NAzNSqKRRT" +
                "UArzeByHyj2aze7Q+54G3wd3H8UzTuHbNDEAbsDIxQuJAuAFVQFeHE8IsfOIS0mNOkAF5DaIOTuW0NmG" +
                "9AGZUyYE1/eGGps9mEHvDkMlqmEavLXkkrXkemIvzPaYEd22w8SaHiP3xZAY6VTNjtLNJVxzIuczVIC+" +
                "9J0XzlblQ3bGj2kFygYi6ph5H1KYNkMt3IgrdeeyMFPpmSbJeCpz+FA7nGQ/oaOMvdjcGy0qlLCoHQZ0" +
                "fD5bN9jI4I2pr5qSwVa6gcJwso/o+22EG5F6jM32XVxNVmlHEdPJl++xKaWl62EEJ5EasuPYr7I2lFMK" +
                "PBC2mRCHPDLdNPfS5BhW8QlORjs14HCdlPhn9+bY7sWxze71sDtQKLtmB7B6vXOfinUAco8cLtEisFhh" +
                "F63ldiK8DlC4FRU95g59OVXXnOWUK+NxPWbk7TqB6ULjMhUv/cZDGJB0MXmudQZ/k+csbBflnxxAJ6tf" +
                "glIiJYU1KszPDao2hrxe06qyMMRhf3t99g3ptBM04EfXwKzwP7PWlF1HBVF6Ken89O5qujsmJDuy43id" +
                "evh+dCAjFBoINDVKgDIyOZVbBnv4fzV2t2psjfd2lp+sxnT4/yU1dpMWSy/F3xAPUitTCP62Bq3hQHEA" +
                "/rv17iciE7xket3N1aawa6XkVkkoqpVQPFi7nRtbfuv+0yhc00pv1iWtO3qj6I6QJGoLgqqZfHSwhncu" +
                "Z6275LqOI42BDZSGKyKmXlCNHqUNuESRlCHxt4y7G+yYb/acH/dobF3+9RY2TzKLCFKu+JNQJGDxJ7vL" +
                "dxHn3hCjiV7behrKhCHKISE3nJqJF4BI12BAuK8ZbPeWsXQiavM2NqlQq7rWKXb2JveEkhAUh9GCB8/m" +
                "miKn46L0PYKsnQSvk7IIpUNM3GjgebAPXihtkYULaARC8ALFaJtQDFQTbRIEUaAvFjkNFTWoxtA9Sd3T" +
                "10uwrrgJaeF7YWO8Dcr1t6BFN2l2O06ImpU/hcKpBk4J680s3hVZXrohXOy0cekneEL1TC4I05VLrOdG" +
                "VLBMy7clpXIfvpaDhXvKs3L2O1wzPg5tPrRGbXO0Ue2GVmttxd/2kJSULIzHBzA1v/Gx7/sUFne3/W2f" +
                "9aCHeuswxliHuKzduv4n1PF2ZfGJmMpxbNUKiQ/1e/l6xt4e0bIIvR5MwCPiHr0i/hzWftYd736ESc8Z" +
                "bzMQU2C0r317RJ0gPZJBRAdswd2s0g9Lz98gBL78oM2GmlHSyz1259MjyYalu6Js9RJLa/e3pySJLyXA" +
                "xY13iD71UtCn3vF5ztdtbryH89GrNQeZ3pGiOAFzTRZbdUWbBey4+UiT6KHJJ9xB4L4wU6f9SbjE4I4Q" +
                "JnQtfwetvhFJfBd396SQDreoAHbryBOtN8kg4NGr0vUefEV7DZ4Cbp/rS1RUoRaN2QZ8nSdnZ3gBA+NC" +
                "6v9LgYSmm6R6mmHw0aIPemSxv6mTTxNQarTLlwIh8kRZHPNKr8+fv/zxHDv9AacGW1soygtfPuC4UBQr" +
                "bdprlufDuIbKNU1SPOEUIpKvXp2/OMPLLaSE45r7l6NVxlxbJM7XW2KIP/Xp6Lmpq6/3oKn0SG48hpEl" +
                "3aVDWgFpVWNEbSotSbxFos0JbvBlY9twl31GfUDorOpAp69vSyl+XKmMDn73f7KXX39//vQNfi3q90+W" +
                "/yBxnn64DqBXwPAjWmjwRJGBGqMGNnDzveXgEz1GOEJun19wajmEV3KX0WDUPnQqLm3IXaYrnNITnh8D" +
                "9FYZCm/31FkxU2UPUEKfyCzdihhYyi98f/HyxX0s90rS4Zcnz3/IGAAE6YGFQc0GAUi+rYCKWqmyfT1M" +
                "DcokOyevoaz3HDrJUWjkrMpLe5p99h+HSOHD08On6NmcfX04zg5b5zp4suy65vT+fbzRWAG1u8P//IxR" +
                "5LJ57TjjUWvdkk5PvBv6tFCkAt8mO4RJJXcDXVornxCbVyCq3DE4GdjLAb9iwp+JqN+KOPuaeSN86A7l" +
                "XlbmXAEyVw90QhXHmSP6QBlmkgRZSnkTmNMsEICeIQng2TYJTv/4718+4hFoerlJCsbt7vhQVrr46w8Z" +
                "HJu3mOcP5zRY+OK36jsdwbBpqexwvfAnf+InWFU5zf746OQh/YTRLQ4o0c2VEWD21xDwbj1GDwUR0QW0" +
                "QMRvV67oK3xPTRydaw6VoYG1b/9SEtnOvRlMzvDZsdjgaKODITfX2efoon+e5e/h/wrqfqMOktPHcDh2" +
                "/vYBfpdsFn5+gT/z8PMh/izCz5N38Ythj97RszvObWx9kyZmANK0KRnwnU/RsJc2CR+XO1CXYmdkviwr" +
                "Lf7jwO1sXqzkkVcrn76DUX822bK188efiUisy8ty0jo/ce3ifjf/7C/d/M/3zV+ADfNLAEQpvQsI6DFw" +
                "L1zer8LpzqULPlX4O7ks4cLhdjOOXkLDu95zxEH8dPQmJphDzuguwv+9zRGizvQaIlAA/Ivw0TruuKJx" +
                "IbykIZK406ADdPxVWWBkj2/LOPnGVo2DzIOU4PVuv1mxuz5mP3/4Jb10tW0MzvFd2BFfltxzAX+7wYHy" +
                "4XDmtpqP6SsRlXehxSLtzGJicH9WyA9HCk2ShqtdPLVFEWxzGz9R05iSOpyYwkf1F+DwUb+uVhrRoLKN" +
                "Dx8/vWHnsXcvXAvaXT1JFWiHvAmBMKMCm6DyNm0j8aD1UxrkYwJ25ADPHJwQTKA0y0MKzga7peFcCRcq" +
                "yt0e0BNIUtqS9JJxQqnOnrw4S/3LwGgCYJqyAFbHd17pyd+9DBEHYh5/2CYQzwFCk/xS+k65Ma5QHPTn" +
                "HWw7aWkaHSTfZLY3Zu60HSp+LSvNvIUv7mhf1N2gQH1Wn4oA9mF9FAFp1rr97Sc9WMOES8magYtkaw1h" +
                "N9zqtZumUYFXcacxRcglufb1t18/kRe3/THhsF74Vl4b/lqEv2bhL3Pn3ZTUu8Z9c8Ompu0k7tt32d72" +
                "pDdJZx5pzvSLdTHhEuGj8zySGXLy/OMnUHb0MVN5eWtx9AfWpuziyRm1FWJnLDhdnHwFy0KN3TC+tXZ/" +
                "YjG9nOlo3N60nJSC5VPou5kmucwS+na5HiAA8Zu4+xD4ZxDtv00sammjNim8awI2X6YeUU0VK0j3XZ73" +
                "DRjZYzQY1NJKT0ydb5QUR5NZd3+CzkBZaVcYA6KERGXwBkp0L53eEJTpQ83x2tLFeGRQbHleHYfb1XhN" +
                "0KLrx9NqV8SPDfNtxl7v2AgaENWVoFnfhzCIp/InCrm9gD4uMlly0cBw0/i6LTtlHI+93F+iGUdpGf0X" +
                "lMDwYONhAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
