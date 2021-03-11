/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceGoal")]
    public sealed class PlaceGoal : IDeserializable<PlaceGoal>, IGoal<PlaceActionGoal>
    {
        // An action for placing an object
        // which group to be used to plan for grasping
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // the name of the attached object to place
        [DataMember (Name = "attached_object_name")] public string AttachedObjectName { get; set; }
        // a list of possible transformations for placing the object
        [DataMember (Name = "place_locations")] public PlaceLocation[] PlaceLocations { get; set; }
        // if the user prefers setting the eef pose (same as in pick) rather than 
        // the location of the object, this flag should be set to true
        [DataMember (Name = "place_eef")] public bool PlaceEef { get; set; }
        // the name that the support surface (e.g. table) has in the collision world
        // can be left empty if no name is available
        [DataMember (Name = "support_surface_name")] public string SupportSurfaceName { get; set; }
        // whether collisions between the gripper and the support surface should be acceptable
        // during move from pre-place to place and during retreat. Collisions when moving to the
        // pre-place location are still not allowed even if this is set to true.
        [DataMember (Name = "allow_gripper_support_collision")] public bool AllowGripperSupportCollision { get; set; }
        // Optional constraints to be imposed on every point in the motion plan
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints { get; set; }
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId { get; set; }
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of placing;
        // CAREFUL: If the object name 'all' is used, collisions with all objects are disabled during the approach & retreat.
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects { get; set; }
        // The maximum amount of time the motion planner is allowed to plan for
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime { get; set; }
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions { get; set; }
    
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
        public PlaceGoal(ref Buffer b)
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
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += BuiltIns.UTF8.GetByteCount(AttachedObjectName);
                foreach (var i in PlaceLocations)
                {
                    size += i.RosMessageLength;
                }
                size += BuiltIns.UTF8.GetByteCount(SupportSurfaceName);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e3f3e956e536ccd313fd8f23023f0a94";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cbW8byZH+TsD/YbACTlJWpr2WE+xp4wC2pd31Ym05lrMvWRjEkNOkJhrOcGeGoujD" +
                "/fd7nqp+myFleS9n7h1ySRCLM93VXdX1XtUzGAyats7LWTKrq+ViVKZzM/DP0rZNJ5cmG1Xjf5hJ696+" +
                "LtKJ+b6apG1elb+8Sxb8PSrsgwYjBuOqKuxzY6aDALJZLhZV3Y6aZT3lWwvSzkiLolqNZnW+WJh65MZO" +
                "qqLIG4DGsOdYoK3TvGybZJG2l3jpH0TLYOmyBIg8G4Sn2KosAITaajm5tGhx3rSo0vZPj/17mY8pozZ3" +
                "KMvv84WgmPj3lT4Y3Bs8+R/+z73By4tvTpJ5dW3ydjRvZs2DDuHvDfaSpwnpl0yrOmkv8waHmDYLRwPi" +
                "vpe8vTQJiGPqMsWJVE27rE1STTHeJJdpmdnJxs7d45hckeRbM8V70DrFrGVjsgFoTaJV9Vr39F0F6G/9" +
                "Q1mC5MPZ2tXcLhxgt7ops/sAL/O6u0hqUwDHa5O0VZLi19TUppwA0ZroAt5Be5m2CTBOi1W6bpJmYSb5" +
                "NDfYcNGY1SXGHyVlhSGlUmZumiadmcPBzFRz07rdv64ac9Gm8wVm+k37HaeLRV1BAnAI3PjgG2VMYFs2" +
                "3CBwWdSYIhPdYDe5xioGm/zA3EAqO5hzUxBImArnBbZvSa5q3EASC9MAGYBc8ehAncbM07LNJ8AS9Jsr" +
                "1HRcLVuBk+noCSCOSUqwvMkeLJYN/yFfZUoek0yqZd0IWwj9wT13icwn4/c7+Ove4FuTZqZOLuWfsM1/" +
                "cJyok2bQm/OaP6mn+O8n3HrTZrpn3SIFFLxVZmmdgf/aNEvbVBj9Mp+BQ+8X5toUmKXsJ2/b9cI0Q+Eg" +
                "MC0l2kCN4QjWIn2Uh0k1ny/LHEoAZwrt1JmPmTjSFKqxBl8si7TG+KrO8pLDRXwIHf9rzK9LkakXpycY" +
                "UzZmsqTIYaW8nIAZG+qQF6fJYAmqHT/ihMHe21V1n9pkhhPwiyubYbPmBuLQcJ9pc4I1/qDIDQEb1DFY" +
                "JWuSA3k2ws/mMMEi2IJZVBCyA+z89bq9rJQnr9M6T8cFtFcDFi4KQN3npP3DCDK3fQIlWFYOvEIMa3wM" +
                "2NLDJU73qRYLNVYzEBADIdnXeYah47XKS5GbsoV4juu0Xg84S5cc7H0tKkp0jpwIdVTTVJMcB5Alq7y9" +
                "dPpZToMW6veSJZELcukZdVwYrILidSrOFbvGgXvL8MtRAkYBTi3e4kc6mZgCbCov370DxKo7Wq3IO6/l" +
                "o7XA0VBO5obsZzIy59OiiPT5dVosjdofq6waEhg8jR2lVIhGZV9MIWSmTHpoDoG7t/CiBuzGomcBnehh" +
                "B63ouWIzyJb6SphmNK2r+Qg8gBef7DxvtV3qC/CBcFhkMfvSr2bBSW5PmQoAMYA7REH3XhuqDgiVkhSW" +
                "iPvgaU5rgzNfwEoeUffxcWbfqztBjKo6d3OHCfAgP7gBg78ugX5dCtwwbnc4WikTlQ4l1cJZVb6NPSLo" +
                "bNl1B2Pvl974v9b+r/e7wiDQz6Phj4teYkzV7v7569dAfTopw8EdSLm/VrvxqTf9MiKZmWleUu1QVXmH" +
                "LTipMudIbTJwXOSTq+VCtB7duaRNm6sGYDjB3EDQYG/4d5FPWwmsQDNxpHDyPPpWLBIGOCeSg0RRyoup" +
                "BUy/jEYKL7K8xvTIm4722fNxfxA1eOzcXD/TgzINHmUdTDM4nin0h54GrL8dNPIv7OQ5sHfP1A+YL+Gz" +
                "QqXTp4DNJOQpvQkiMjZAheMMpqurT7fh2kCK29bMFzABfkmADsvtiNW7tNrK7okd809pWgfjWv7dMXbb" +
                "0dKtdOUXunQveSG+XVXCM5wbxBs02X4mJnqGGjLuqeWIj5K8TbIKEoQgDDDm6RXDFThaEtItFgDWlS08" +
                "xpQDM5wNjxIEcKWOEmnhLsQRRqhT57Pc8mqIeSRMtNgdJe30EQ4HXoTsWRdTlqsr1VKHw+TFNFlXy2RF" +
                "hPBHbf1v8UbcvkT42qoSQXdcu6ncXXAJ2jUtzvcuFbcTvRYlSvx5u316I5T6CFOjVJ/XEOm1051PFj3y" +
                "Qt6AnI34adRRaTKrELUqESP5hw/HZIT1ezXlI05a2KSP4Dr5nNfWQHbGOavZHXoebFBndGSbuhN+QEpp" +
                "nBd5u+6Mv/aPO8N3cmY9mvDc/K8tDoO6vUptKiOJ/Cam5tkmyAKUmSN5CI6d3pbXdi7tDXQwLFmT/OKX" +
                "uI+39H6h20ZjuMero7D859E75BuuzTvP3d7rcg96I7c8F+jc2FNkNhAai4lEdjDkhdRxsmRJDjIDrQLd" +
                "4tNEOdzC2tqgKqkkWok5OHlewG0EpCp5b+pK9FiTIJ/S+KntYfA8ZBO7yehtMPjtwqq2Gn5Jx2e0JxKw" +
                "hfbp2hqXj6rGyIchrr3aIKgYMSxQOX7hKM8unAw3pTGgvOVDgSJW7jDEdGk9wxD1YqtoHLJVjN68asGE" +
                "7R6KBYEUHLlVl3QbuK6KpYurt+08GTwjRwP+DzoyDEJybWbdnf9VDLYbndIlC7nrVHxbWnylFM70+FSp" +
                "wzMGXzUV9LWzdLC5izqfg82uXf5RrDn83TY6mMr6zMkBgvI0QYYKZnbQXKYLoxu5INDXDhL1uIcaZYjp" +
                "cyPBeZkzI8FHYWkykRQZ4CNuhpExQEniCtQXUIlZpiKCLQZoR5SvS0C12dGQcKAvkck/Xk+SXSyeB+kM" +
                "J46UR7MFxZcAiX0o5A8gZZe+EyGOs7h8uqTlbScUscq4ujkChTSrPllDqqFXGI3jnUvYEIyipHwgFJQs" +
                "NTDO60BIACPZYZTFN0hrsUQPj/BfqC4mG79Mnp3/9OQL+/fF62/P3pw9eWR/Pv/5+xevTs/ePDl2D85f" +
                "nT157KjNBKoLiWRPdhSfD9ygDF45nBPWODpDQ4onjHBzKNHcfjwhGnaSGCbRRCfQpXQBJMeSXDcue7Uf" +
                "5uwD+Tpdc4WvrRIF4rLVI/n101HyM1gN5Pl7vGcSWbSrKWcIQeyOJlUNl3yBJCjDRCR44Xx5/ED0YaDt" +
                "6KcnD6NfP3ta89ffQep4S0p/uyvxqHnskokpGQBAdTIM1n3ChZ9ZPQGLk2b5kluwPodykNuHwh29eXr6" +
                "4m8X2E+8pjtkgckD1sS4UkVZh7ZFkrjq4JGTikoQ55i/J+lNDrUcIpIO3NG3Zy+++fZtckDY9sdhwIkJ" +
                "zGlM8YDTpahuT3MrC8kBZeFQ16PX5tZR7Ow6+iNa57ZVGKk42unxpVqd2b4mnAeNNt0rVvG6ejOSSbrm" +
                "eS21AQnYEKMuAg8JTaUKiEMivy8XR0rZ5HNLVCekPWJ6luohD+aKJHVjcCAMBu5Cy1FJe90WuVJUtuRB" +
                "FxHB8qXlDCb8q0iIbTZa3A4xKL5YCvG+huudT5ARfzfgIm8tAKgTD8u53/A9loiU3IxNsyq72RJrSq5a" +
                "J+2MWg6RbVRzmO03YV9aMPrlWLdqbkag3afdcOzwbA0Ef5NX3U1o3u1ZuwRaPMsKVOR0Bw9DHKSQFu0d" +
                "8y3Z6n/Wi/c1ZcrwfTmzxNQ1Fa4LxKIyemiIGMMZRGVmdDPixJEfvDlifeeI9/0R/5ou+bbMgzJoHLZY" +
                "wzpdSkJlzkeM60N+QrNeyJBO2A7hArDDjQKwr/qKAMh4Zs6ajmVJPeC1WoHVJesvtA2sYCLNwCiQyTfP" +
                "20znVTUBv7SbEz857I+rERSKphi6FJ4MQuGyg9LcUC+RC5dwIkznNgj9Fc79JKqfOPoIgFfnbwEc+IAN" +
                "kDDO58s58+hzMBr/dPnjBmavXRlkFcPOfY+EIx07RmoFCz/GQY1iW7We1pGDmBRSiLrOzSpycpQqWo7r" +
                "xfYSbBxwUWT5x8gurqFqq2WRHRKuFBPI+w0aLZLFshbPf+glv+MRyDGK1VAIBOB4hBk3khESHrItGlor" +
                "kDg0iQF+5QqqekLkWetz9UdiLRwQCuVQpZizMkXhT0gDKkclSYVrriKwZNBiR3GN5APNOT41oN05X6Pa" +
                "GpHbcWZbrdBs0XTO1G9ZmT7ts1jEWeJPSgkG3CP1+qV0QaDLZq3+9FBcVLtdm6fWMY91wBEDIu2HKEWf" +
                "iVXXHUULa0pCkm/JYg0a5VL6lo2Ij+GO1fY3YeCx8DU82eFAGzK44ZEs2jlPrKuRZlcSlXcDi+hy7oAQ" +
                "7tsFQ2a/Z78AIESwluyiGrRHcOgrBWh3QKScxePIE0VVXdlaAbPybeeQqLNA41BKcMQQYGK6VB/Ca8Js" +
                "qg3oDJzAeNn6CB/GbVPm6MxyeSBVFRKNSmNNswQdb2c33bdntzNWqNBCspxdBn6iN7EhccpvPRZzMgF3" +
                "CHw0BwqAOTaTdBmkaovzIKvYKhMtsyigWMcovbFJWbpXb8RY0UwW0hVYaY6GMxw4RCcVbhW666FOaAe2" +
                "bd2SD816dE0R0Et1gKaVKzy0eRGNJGQ9dS2o+zsxh3i98spqYqsvoXJBEIdZtLpg7xR0KHfaMtV0qwJw" +
                "TK0iJHnluMhlC5ZqGazTFvassg15NDWbEuXNkezSC6VUlYDMQ2txV5Xdj0ZZID1LuuhaBRea5tBBZMBX" +
                "mJa9Vc7Y2ZW3g3+dP3ikK8TQsa8FjkFVKGBHysKRWjFmE5hF1prB6GA29IDCCVYktTCtAq+wpjaQ4jE7" +
                "Lx8eyf60hPaQKyFr6Ng+Pv/INEM7OEOELY04biTjBl7O3Syxgg2CVyDJul3VmyESXzC47aKo4Pa22Hln" +
                "HDZ4yDEXnVLbKlmhaxjtjTYI7xuX2Bewhk3ZLmI64sIUoaNfLTbSU806HT3yJDmSrupgPOySSqbHtIq3" +
                "GytblE/JG9IV2O8PkHNxeXrv/PyUPEkeIcOEf744QsbkSeKC8ouzVxfnb5AJ6j0IiSL74CeflrMKU86p" +
                "02HwL+jf95rF74W0I5o9pINTK6P+SHxppEFuC8ziYrVD33d+IS9817mMA52nU0lyT5Wc0yKdWXkUZhUD" +
                "abMNPA1klQwasqWz23bcSYWbYD27SgqxUTHzLrptzrCTyFWJb/EvR0wK/oZ9SPOWw/jfMM9CZct649qN" +
                "OVE8BpsfTg5IYctmaBzN6W/DS2+MmVtp4EbBRLTRcT80YV6nUCdE1TVHa//5dV5XJdJvrSLD9Ua6XoyO" +
                "l2tN9lx/AJmAiurgW5DRWrnTXOVyPgYz0EdWMjdfudUjpVKYKc6hQaLcpSzSZdGG9zYHK1THdHEbsjWi" +
                "/3yCAhhYbprP2IyvvmOE6sitanHmqYmKmsajxKLYg4xpIuo6pZMVK+MJHQ2HvGLLyxqbnQGBC8V/dFlI" +
                "zcDZSVhFG6Z5zGW0Pmo4TOySJyQzX9PypUkJZScsXRqDnl2xU556w4dqQLZjBrvhm6Ji+pIm13m6jaCO" +
                "CB2t3aRTM/LCgkaCRgqZFj/ZHNw/OJ/MG0p3hEQb6CglIn6idNFEHp4rqbgQQwBxL/MFm2f1woYXUYgy" +
                "21G9iRexNCWJTYmRNO90WQobp+JyqRjAYQJcX/fY4FJ9LwzvWCexbKWvOhwlFmsOQmtSlZ3SjCRSYlZ6" +
                "a70NJovNtqs76HG7QgYrsd6tNhcVzDSNXLmJxV60cbe7hZSFL6fhE46hNYM3/BvxBQy0PB7pY0siBzSO" +
                "voGkcruaBR4hDLddR2HIIJfji+Ig8U5kPJhqUdAvkOzLNDnYFlCE2rzU9HuBkfRGUqHa6Ag56Gl+g2ZA" +
                "7WD3vVg8b0HbSb6/uAUuwq5vBk/1xXP3/KU89ldM/PiRHU95BjyJPxdEr5w1g+/x67X+wE4kyWnfdcY3" +
                "k5QJd46+4J9urDwXz8RGrrYVE6F72K9/JL2J4gfDdZmn0uMZo7WQ6EjqnIyUqkK89dDwrwSeWxGUel/U" +
                "WC+vBueyGIhS1WxY0rt2CsrVCzpLdr2CH6sajvuK/y+ZETG+6gryRJGiENepy0gukwW3HtGR6A6mRsRD" +
                "6TNIA16w/pdyumgJ5HHF/9iJDAbZuT2Rv3EHKpIlETVo0yGCvLiPStJpjCSkQUpvvJFJmoH1Z0Nblkqu" +
                "NlJ5yZU3vnqzVj6Yw27kjMBPz7+WwC1k/lmM7IB+ybEYFy0h00dZNR31FvP8usGmyCm6d+68RARsF47Q" +
                "4HDg5nvpU77jRQd3zzMIoL8auROOCvedmKLgFSTnwet9KXchR/wuh+q4yihDB24/GCh5ITjYhUGScctg" +
                "LeymlrdOTlAzNycnkWa2DcrLBQyyOKStbr57h3A3ArCdBb0AUJ14KRAOvKwK5D5dHysSHJM6t5kXYSLF" +
                "3fb4IBD7daniU+PUQSKVAbb8WpvkJ0lrg3YbHtSGGR8+R096nSObhaTuYZzvGa+ZrU/+0JE0Z9sclJTl" +
                "smR+eBSG2js4682hD1jlT+YP4PdTQsMUvYkTgmG0+lLMbCxjAbySisCrQ3iYTOUFXOi5lTmoQIZD/Nmq" +
                "kKJcQZUgRlUJoeW+rt5WV6bT1qNQhWpMiYOP6JvV4oOyR6Y/hjECCAP5cceo/TIiSW6qy5TKrSeuqWj0" +
                "dWCDQodoszToH7E2sGFcQTKDRJ1Z+C6FZTPS6U1cpYNX5Vnq0TLJjpeSB1ax4ekw+ZEeM1u2tYXaalHB" +
                "oqwY2en59C7Aidk7kvZrSQwb2yXkh4uVpBO4ttxI6ik2/Qu9LnHlOo2UTk3+ns0q6DoyFk4kNWLN2ceC" +
                "zDrR9DzgtxkRR/wyt2ntsZkwvtITRHGmXxL2F1KFfzZvnW1eOltvXi3biU7ZtD3ULW82LmKpGiAD2fMV" +
                "cnguy8ysNtppxIsDWYWjlVQ0nTqntDXpaa+ehwWVl/tlA3C8a26WWmazbhASRA1Owp32+gCNvs5BVi6o" +
                "APEEK7v6FfSS6CmWrJiu6xRxEGbCn4LBsPoCdgvL/u3N6dei1o5pxg/QALfG/9KVy+ChGoAsiby02f34" +
                "9mu8OyWkerTaJSXb7b4HVB3hoEGmpXsC+iidSPWls4f/12S71WQrXvK5/GhN5ob/X9Jktymy+Gb9LYGh" +
                "tDj5KLA3aAXTxAH8t/fuRyETXiq9dnUPyu97200ose1es/hyAko9/RteTe+y1MBf64ov40X9PO760c7w" +
                "JMk9jk4/NcHT6l7WHNfVlRZ7eBcLKUeoTahFaiwUIKRwT5kDrzg87ZDw247bFYLKP9tOUXs3eteHG4P9" +
                "i/ASR0kgfxSWAiz8VNd5N5HvLUGbaxfqPfYVRB/3iMCnmq8JF4ZE7zBE3NYqtnlP2XYruh5v9q9IU7ur" +
                "X/R2Mdiz94qioJTDZME9JEFt6lxOTNL6BFmiyCPTh2hGcFVFZnNcKLq3DZ6veonf7tHwhNAFskGfUArU" +
                "Zd9sTCShv7XOcfDowmwG81FKX76IwpLj2qeL7/uN6TakBlBDo67jrHeYELSsfl5Fkw+aKnY3uXRXEk/I" +
                "7WKJbzrtNO6zPr6wZi8Xy0VNlnoDKqzg6h1LW9T3X+BhTV/yr5oV91eUD30HkKyB+ijtFRoZuBqqdfql" +
                "EJunsgvz+ADTZTzu+mZQZri7/veCtGTnLHbvMJDMmSZXZbUqf5cS3xZxfGotp70VTur4bIhzg/Uux9Ym" +
                "UjC96wRRGh4IA7kb5i+x+AskcTa+7eSOmvcehC8Y/7uuPiGQFyCbWWRkOdN2V9sxK8/fEoJek3CtiC7N" +
                "5G4CUfg3HSAntjotF3a0bedbm1eibJgjwMWtF44+9gbRx14IYj9zF2r30s5d93AAwt2okrCBCSjDXl6r" +
                "0Dx22prkkuu+BcjfVtCuMTyMsvZconOhiIleQURWugVJvgu7e5rZ/regAzZLzFITiwaBR6/zatnAdTTo" +
                "LsD+XOlJ6i3SwDFew+95enrKqxoME6U7MAbiW3Kiwir+btENALgHyKTXa15Snbl8aQt5bno8kWeHutKb" +
                "s5fnP5zx8gBwWrDxRYI+/+EEDROtbpVNW4/6Llx9UVsmOTxxCgHJ16/PXp3yGozo4bDm9uVklSMtOwrn" +
                "uytlxF+6eNy5Oc/f3Z6WqqR49Ywq2QOCzmhes6+ky6qrUG3Dkm5RaHPMDZ6jLchfggdMdi/BcXUDK/f6" +
                "0+nFu9UK1ONv/k9y/uy7s+dv+QWq3z7Z/of0ef7h+oC7L8YPc9HsWV0GTSYdbvD64RtIcoOuI05RW+xn" +
                "mnL2AZe9+4iy6V7PtbgyPqEZr3AiT3R+SD7K5xeEY6C00LE0dvoeUHwXyTjeijWzknH47uL81QMWg20a" +
                "4uenL79PFAASkJ6LoWm9DETfZaCudlTp3yVzNmWYnInvwLTlxqmLKPlOzyK/MifJZ/+xTwrvn+w/p39z" +
                "+mz/KNmvq6rFk8u2XZw8eMAbkAWo3e7/52eKohbV4Q9KDqR0JU05PevjyIeKAhX06tk+JuXaLnRljP0s" +
                "2bSAtGpLoesO3MKwLAQoEd13Jk6fKW/4T+hR9O3Kmj0gcy1BJ2o5zSXJR8+YW7LISh5cwJwkngDyjCTA" +
                "sz4JTv74718+1hG0vtpFhXGbO963K1389Xvk/eElMPnvz6mz8MWvxbduhMKWpZL91aw5/pM+YbXlJPnj" +
                "4+NH8hOjaw6AE12t7AhYfhQZs95jOilExC3gCkf6FuXpZcH30uLRVot9x9Bg7V1cXxIDuj2tqWk/KG61" +
                "xMFSe3Oe3iSf01f/PJm8x/9l0iEnLSYnT3A+ZvrLQ37rbOx/fsGfE//zEX9m/ufxu/AVssfv5NnOEx79" +
                "D9uEnECcThVLvvE9G3XXhv6zdXvOt9gYObnMwQlhYL9eEep84t7aj+ph1J/T5LI20yefWcFY5Vf5sK6a" +
                "YVXPHrTTz/7STv/8IP0LmHFyBUCS6rtAfM84PqsmyB+7A6aakO6gSO1v5LgsL3a3m2gk4/vi3dVIDtKn" +
                "A0/OQLMdZQO29k+4ZIC7uQgiwNfwn8PTxizpp/DRpgyxOT0XgEDZX+cZA32+zcPkW9s5kDyErPBSeLOe" +
                "q+t+pD5/9xt98Wp9FM74zu9I71duubnfb4KQVDmO3RRTLMk9Nva6ea+BS4mhbVy+CBYoJNdrrIO4iafr" +
                "ZISRBkFCKxuCHNBZKXxQonu2lM5eV4ekZVVj77+vesvOQ4ufv0C0uXqUOXC99FjHf0WXqGATUv+WbUTe" +
                "tPsMh/ibwE6cYVS7LxNMkKzLIwnUOruV4Voqt1S0t4CgKkhS2ZJtOdP8Upk8fXUa+ZqB0SyAUcwCLJ9v" +
                "vLIn/7uIkfAgpajXShCOApHKBFUkOQ5tocscGu7nTnYe9T5hv9HHn212bdvNT9scFT68Fafj/Gd7bAfV" +
                "zrCQnqyPxoFNW3fioJ1du7lkHFq2nPYN3jWx0a4AKCYVRqb/MHYzfeOE34m+jLGfERbwb7559tS++PQf" +
                "LvYr3vNf4av9XzP/19j/lf4OPZjS8Eaqb/RB9bO8kM6tHU1vo34+0aXxh/BCOibAp189sDMsA+iPH6H+" +
                "5Kup9uUnjLI/sLqmH49PpR+RXbVwxjRBC3MjTeGYgDrI9sxjfLcTq7Besi1vZ5tg7CfYN1NR9i6M7/nV" +
                "moEFyE/wbsPg96Hbf59e0gsnzVW8rQJfwE49kDIsa00PqslkuYDxPaQVkXZYeYKLLEgEKDUOhuP2wZBO" +
                "Ql64djIFJBmLAqFW7HlqBoOpb53e1SJveLFRO77ZMT3HuvZ+Ni8aogfcTSvRK+w/Y6z3IWUagVg0EPbl" +
                "ULTvfZCkU/X7h9qRIJ8qGUorv0TEdCJWNa672bENW8G/pHmnzNwb/Bc8UPF8YmIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
