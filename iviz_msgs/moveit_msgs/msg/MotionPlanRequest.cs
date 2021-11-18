/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionPlanRequest")]
    public sealed class MotionPlanRequest : IDeserializable<MotionPlanRequest>, IMessage
    {
        // This service contains the definition for a request to the motion
        // planner and the output it provides
        // Parameters for the workspace that the planner should work inside
        [DataMember (Name = "workspace_parameters")] public WorkspaceParameters WorkspaceParameters;
        // Starting state updates. If certain joints should be considered
        // at positions other than the current ones, these positions should
        // be set here
        [DataMember (Name = "start_state")] public RobotState StartState;
        // The possible goal states for the model to plan for. Each element of
        // the array defines a goal region. The goal is achieved
        // if the constraints for a particular region are satisfied
        [DataMember (Name = "goal_constraints")] public Constraints[] GoalConstraints;
        // No state at any point along the path in the produced motion plan will violate these constraints (this applies to all points, not just waypoints)
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints;
        // The constraints the resulting trajectory must satisfy
        [DataMember (Name = "trajectory_constraints")] public TrajectoryConstraints TrajectoryConstraints;
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId;
        // The name of the group of joints on which this planner is operating
        [DataMember (Name = "group_name")] public string GroupName;
        // The number of times this plan is to be computed. Shortest solution
        // will be reported.
        [DataMember (Name = "num_planning_attempts")] public int NumPlanningAttempts;
        // The maximum amount of time the motion planner is allowed to plan for (in seconds)
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime;
        // Scaling factors for optionally reducing the maximum joint velocities and
        // accelerations.  Allowed values are in (0,1].  The maximum joint velocity and
        // acceleration specified in the robot model are multiplied by thier respective
        // factors.  If either are outside their valid ranges (importantly, this
        // includes being set to 0.0), the factor is set to the default value of 1.0
        // internally (i.e., maximum joint velocity or maximum joint acceleration).
        [DataMember (Name = "max_velocity_scaling_factor")] public double MaxVelocityScalingFactor;
        [DataMember (Name = "max_acceleration_scaling_factor")] public double MaxAccelerationScalingFactor;
    
        /// <summary> Constructor for empty message. </summary>
        public MotionPlanRequest()
        {
            WorkspaceParameters = new WorkspaceParameters();
            StartState = new RobotState();
            GoalConstraints = System.Array.Empty<Constraints>();
            PathConstraints = new Constraints();
            TrajectoryConstraints = new TrajectoryConstraints();
            PlannerId = string.Empty;
            GroupName = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionPlanRequest(WorkspaceParameters WorkspaceParameters, RobotState StartState, Constraints[] GoalConstraints, Constraints PathConstraints, TrajectoryConstraints TrajectoryConstraints, string PlannerId, string GroupName, int NumPlanningAttempts, double AllowedPlanningTime, double MaxVelocityScalingFactor, double MaxAccelerationScalingFactor)
        {
            this.WorkspaceParameters = WorkspaceParameters;
            this.StartState = StartState;
            this.GoalConstraints = GoalConstraints;
            this.PathConstraints = PathConstraints;
            this.TrajectoryConstraints = TrajectoryConstraints;
            this.PlannerId = PlannerId;
            this.GroupName = GroupName;
            this.NumPlanningAttempts = NumPlanningAttempts;
            this.AllowedPlanningTime = AllowedPlanningTime;
            this.MaxVelocityScalingFactor = MaxVelocityScalingFactor;
            this.MaxAccelerationScalingFactor = MaxAccelerationScalingFactor;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MotionPlanRequest(ref Buffer b)
        {
            WorkspaceParameters = new WorkspaceParameters(ref b);
            StartState = new RobotState(ref b);
            GoalConstraints = b.DeserializeArray<Constraints>();
            for (int i = 0; i < GoalConstraints.Length; i++)
            {
                GoalConstraints[i] = new Constraints(ref b);
            }
            PathConstraints = new Constraints(ref b);
            TrajectoryConstraints = new TrajectoryConstraints(ref b);
            PlannerId = b.DeserializeString();
            GroupName = b.DeserializeString();
            NumPlanningAttempts = b.Deserialize<int>();
            AllowedPlanningTime = b.Deserialize<double>();
            MaxVelocityScalingFactor = b.Deserialize<double>();
            MaxAccelerationScalingFactor = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionPlanRequest(ref b);
        }
        
        MotionPlanRequest IDeserializable<MotionPlanRequest>.RosDeserialize(ref Buffer b)
        {
            return new MotionPlanRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            WorkspaceParameters.RosSerialize(ref b);
            StartState.RosSerialize(ref b);
            b.SerializeArray(GoalConstraints, 0);
            PathConstraints.RosSerialize(ref b);
            TrajectoryConstraints.RosSerialize(ref b);
            b.Serialize(PlannerId);
            b.Serialize(GroupName);
            b.Serialize(NumPlanningAttempts);
            b.Serialize(AllowedPlanningTime);
            b.Serialize(MaxVelocityScalingFactor);
            b.Serialize(MaxAccelerationScalingFactor);
        }
        
        public void RosValidate()
        {
            if (WorkspaceParameters is null) throw new System.NullReferenceException(nameof(WorkspaceParameters));
            WorkspaceParameters.RosValidate();
            if (StartState is null) throw new System.NullReferenceException(nameof(StartState));
            StartState.RosValidate();
            if (GoalConstraints is null) throw new System.NullReferenceException(nameof(GoalConstraints));
            for (int i = 0; i < GoalConstraints.Length; i++)
            {
                if (GoalConstraints[i] is null) throw new System.NullReferenceException($"{nameof(GoalConstraints)}[{i}]");
                GoalConstraints[i].RosValidate();
            }
            if (PathConstraints is null) throw new System.NullReferenceException(nameof(PathConstraints));
            PathConstraints.RosValidate();
            if (TrajectoryConstraints is null) throw new System.NullReferenceException(nameof(TrajectoryConstraints));
            TrajectoryConstraints.RosValidate();
            if (PlannerId is null) throw new System.NullReferenceException(nameof(PlannerId));
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 40;
                size += WorkspaceParameters.RosMessageLength;
                size += StartState.RosMessageLength;
                size += BuiltIns.GetArraySize(GoalConstraints);
                size += PathConstraints.RosMessageLength;
                size += TrajectoryConstraints.RosMessageLength;
                size += BuiltIns.GetStringSize(PlannerId);
                size += BuiltIns.GetStringSize(GroupName);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionPlanRequest";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c3bec13a525a6ae66e0fc57b768fdca6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cbW8bR5L+TiD/YbD+IOmi0I7tXexp4Q+OJW8cxJbX0ubNMIghp0lNNJxmZoaimMP9" +
                "93ueqn6bIbVOcGvdHfYcICJnuqurq+u9qvkgu7wq26w1zU05M9nM1l1e1m3WXZmsMPOyLrvS1tncNlme" +
                "NeaXtWm7rLPyfmn5bvQgW1V5XRuMqAt5Ydfdat1lZZetGntTFqYdYdTbvMmXpjNNK+A4cGOb63aVY+Hu" +
                "Ku/kkYfVXtl1VciIDAgByOh7PzqBFCBMVuEhF7vo8qYr60XWdnlnsvWqwJ92nL2aZzPTcI/Zz7asu9Yv" +
                "NJXNc53GFAAAdFa2ld23mQVmxDivBcfZumlM3WW2Nu0xn7QmGawAAQIgW9NlmGpG7+zUdheCCzBquong" +
                "RUwvuWnbtuW0MtnC5pWiHIm0tIWpSHOShk/H2Vk+u8pMZZaCxRxQODBvmnyrp4bpuQJrzAJYjWUZeYDT" +
                "xuzS3Mg2y7nuCIh3TS4E0bMGObtytq7yxoEAeOCed2U7LzH1RZzx/oNAniRAuLE31hEfpMzrLTaJN1le" +
                "WZyKnHTeXeFo9XNji/XMFI6ndKubsqqym9JWBKJUTvE87Mi5+WpVldgu6JNjuCyCQ6ltl/28Bq9u8q0+" +
                "O0pRlsWHCJNE6QJErDHtuhJGwtOfzayzzTZbErCSYju6DM9T+HH0vlVqcCrOLZGiwPfYx7o1wqi11YGU" +
                "z5WZlaT7MVmTZ5wDreFcIRi4DgCKEdYk2u7dpCz2Lb5o7HrFL04YAGxzVYK5hLYeLj7alWmw4Xrh4crM" +
                "CWEFuOvlFIMJuVzyRDwIzse2RMKWUAymGGcXV7YBk4OMtlo7NeLRb8yKL4vxCDg9eUzAE0EF607yrjPL" +
                "VSTlMr8tl+tlli/tWoRBVt9HWTJLVdkNuCwRpuwQLNgaHFIBFplXNu/+9NQPjMsSqOiVWV5x+/Och6vC" +
                "YldcB1O2QB1sLNySoCa0zW5MZWdQERTNWjTMbAYRJlXBIOMse+6Qu8kraFkRN6B2+Oj4yw94e3kXwO0e" +
                "cJFfvIA11D9OlRDyklxNyYHm2/KoQCDwOqZ15Y0BOLdBrAxONKUoQE6EbqeSJNCyIa5lkTV5vQDGh+WS" +
                "B5fXXbWlWixbKph6Vq1hAXCwoo6hEEH9R+NHR6I53TrC4/rKWR7hbyEFz/TL8SOBBfWuhD4sx2Z8fBdF" +
                "ALD/JiXO0TgcMwZN/KRJq0c7UYx6Y9Lpw3Gjz0bP/sn/Phu9vvjrCU7rBoSfLNtF+3CP7ftMJACEg7C1" +
                "+SIx3rnQEmSLNpE6Yb6G9qeg4a2oNIg+qX0DEYTIHObZ1N4ekWO8DvB80xcd4jX2i5cBtK1xLpsr4yRO" +
                "pIQHYaclLBvtCdTIPHPGSgHnsOCmqsYUrVMxXMoURJuDGzOH9azhIHhrCBSx0QYC3Y6+NjnMNQws/wSF" +
                "UEJf4OB1kFd0mHYMBQOL47jcs5riITZZ1KVtzWhhLKgG1S2k/070+BMCnijQoe755y8FnnNLfTLuartC" +
                "F1Uqkpvgn9RF3sAKmy6Hw5QL1a/KBYT/iwoeg/gmyxXYQN522xV8qoQTFgYoi3zSBHHf0PjLdV3OxITT" +
                "LKTzRaT7vsbM2qYoaw4XJiB04Vh4nmSDV6cnYqPNDFYDCG2pYBqTt6Toq9NstFabgQmjB5cb+wV1xoJ2" +
                "1S+uriaQNbcrHBDxzNsTrPFvurkxYJ94i5AdyrMJvrZHGRYBCrBOEA6ajbfb7grKVmQob8qcHhwAQz9U" +
                "gHrASQdHCWSifQL7W1sPXiHGNX4L2DrA5Z6+gFNaiElq1wsQkDZXnW6n2aEXoOahA6ty2uTNdiT2UZYc" +
                "PXgpgiaWWk6Egt620Ic4gEKY2Nt7OQ16EZ+MIfeKQlBycApwWtgH1duNvCTzzBuDzVAxjsknr+RkRRMt" +
                "DQwRWTDMpONUNrRwziGGdrGNOWagUliYKDiNgLHMr+m4g8ziVMK/hImlQ1e3lVpXPMaUQzNewASJxpNR" +
                "JJMwtYhBOcuacgHrKDOx0DJMzjO3O5jA+WP1ewRnXQxnBiCN7Zy5ogXe2jVcWewBHxonfeJSebyESzpr" +
                "jyl6DkSfom/FEnpjATvRQfCheb2huw2ftuHTr/di3GJwdLdNK+tIwnwKL6SvUjseI1wCjSZi4DbHSbXe" +
                "u2V8ClG5bkc8Xtvo6t/wpYZmMi6GZvKmVYWxRGh1ld8Ez8lkp+cvNSYKvpZEeino1xyLcckSMn1S2Plk" +
                "sNjzrkNcBigzW1Vly33aKWMIKIXcv8OZtzhT2QWd9UCDo5Gf/8JPP5fZCM78bJgU92riIHPdl1W+AHUL" +
                "6mhyMPjZRbv0yWZg5ei/iwMGaaJygOmmPM3nO7ZOMNSQWeYnBmJpWziHWXAU1cB4DSZBrN/q1Bb0lQ89" +
                "PhjIoI42pTJQdHsGYyEceO546+QEJtScnCSh91QEXrMBXBBSLMh3CcsdjabWMkyecHOf0P7uZcEgANTE" +
                "QQqEA69sBYvhRR8u9awpqQBAB42zZe/O84N2gcUU8Wlw6iCRygC1pPNewiTDbIL6yYeNoTfI5w3sSNlS" +
                "4mbwCbGwuGc0KjSWsGmppHnfx0PJC/HBj47j0OicD4c+bGXwQ1hYSmicYuY4ri4YbA31Q0jjALxZcvYb" +
                "ePXcmGRG9A0907oEFchwBfS/CimMIlWCRK5KCPUfgxxr4C9WEutGV1ihuuTMBqIOngeHwdCOGbvtjFHv" +
                "BjmicIyiP1SS/FQuIScodhhr6jaGOrAtW/We86h/JN2DVAdXoBjJMdKV6lNYkBHjmKaA6MbTzskkNx57" +
                "J4f5cHacfU/TRiunVsdpUdlFbQHQnc/AYyCs5bFYrBlibAgrdx2PU9NUDOK3jhtJPd2Nnm2yd5edU8p5" +
                "OrXlr1D32DIIqXASqZEEpIQiWIXZFM8DAc2EOJIL8UjTj6d7QH9TThDGcRhjqDcEpSr848wkvnpBSB55" +
                "hk8eKQXuR6fs2h7qlnfeGVJLipNXNUAGcucr5AhcVpgFXCzhPfpahcXRMj0ApWI3XmmDKutZt2Z6YJ7F" +
                "BZWX1ScD9RFkMj/MdKAXUXr/7RaeiARRtCHiFouPFPwmnbMwXVQBzHeEROI19JLoqWx2BV9hnL2kNNzC" +
                "wa3AJrmEozAYTl/AbmHZv787fSlq7QnN+CEiNqQstvmGbrWmseGj60syMRktCRdS7JSQ+NOUgKJzKdS9" +
                "9+JWcoSHBpm+QR6a+iifXXPDPRz+X5PdrybbMMlw9Zs1mR/+f0mT3aXI1A3l9HaQjbj0LIxRgZ13Bm1g" +
                "mjiAfwfvvhcy4aXS675Cx4D3vuBRbHvQLFPTbQxYo9vYndRDO4gvRyOfnkliwdHf1pjQ1NQBPmK7r33G" +
                "pfdGydDuDQN/VVC9vfDbLxFxkuJjgWD4tLm3YyRHhZ159dtGR7K/pWljr3GSYGZG5y1DJIYJVMhIT0ty" +
                "SXKQ43CMbkj87sbd1wZVPPadHQ5EDynu7xjGnelc6ibukd7lb9ulAItfNTK4n6z1HTGpbtlkg8ehABTC" +
                "OtFnyEKWtz6A0VwqqMUI2Oem+NmXooSUvTI20vRrVlWvcmQrhVLMvMb66gCL0QP10NOYm8NkwQdIxQCe" +
                "CBRPjJsVkKjVaUw9RsZHixlwBpjXcZH2g33wfNytYUnYRlIJ4wIstvYJpUB9GtqFfJLZiIX0sJ7PIjBX" +
                "kWTxO7uGSkaVehvKLV8ExBQNSfsjuVpsk9p4MiEaEQE20dwKXb5YZVesJFyCH8YS7zDli9MQ11HPg+k7" +
                "3aSk7paEEbYC0C7rBu1mQHY41JoKnFVwwJl/Q1lSZMFBcVUmv0ZtZjTHqOFytQZFHckcu5qEW5jHB5g+" +
                "oZOUcmOwHqu+IAaxm7gl/KFsDNLmwYEZHAZyVfPsurabEC+48fcjlnvE8blzDCR9WAh1QrLHe/kiNvuL" +
                "LmB6t1NHw0NhIIGFA3yNxV8hR+XkNdai/VGjkKB8wfTGNIfrbR2BggDp3wkD54V2pehmdA+XhEAwsZDv" +
                "s2hO6Urhdde/82Kr01DSVOVAYdlXw0mTfZ4AF5ZFUL/MjHmTJRC8AbFE6SieMuqtf8WQMQ4bJmrb3nuy" +
                "lrbQvDbtVR8qn2DsUl/shcN3EcRXlA/fGMD8GiorzkNI2wyyqcuwyjAftahDgc2vVdhwYkUhZwEFyyWO" +
                "Utzecio3IivdsUm+i9g9Lxj9JYyhVA8FRcknSkI8GQQeRZfIuoVnbG7hMxB9ZPbUpIrOGY+mW7h1z09P" +
                "nz3iMu9Er/ZWmjeWgSbc8PqmbGwtrTVMGkFJoNyM4leDsoyKgqSDO8izQkiK7cWRrvTu7PX5d2fPvpQ9" +
                "rVZUVYxpPTe7KNjpVkHaBQwf26uvU+gkv0+cQtzk27dnb06fPXZ6OK65fzlZBf0yZuM43x21FEzQYoNA" +
                "y52bD2yk+wUjKjPvNGhh0AyFhlYO0gqk9RojKlSkK0HJQlEU2jwhgufaVqJlEcDEVzqjfqDrOvmULvXH" +
                "1QrU4+/+l51/9c3Zi0tWJH//ZPeP9Hnxj8sfojclKzgXs+d0GTQZsxcMauAbtINOgM4uNKMe4klN7YJV" +
                "mEDvuRbXJuRr0xVO5InOj7lVKcgJx0Bp1Vkx9foeUDzAYpqi4sysJFS+uTh/85ANQi7L8uPz199mCgD5" +
                "1cDF0LRBBpJKHXW1p0rMJKlp9zYFfXPiOzAru3PqIkoS5Vt7Da/l2pxkf/iPA1L44OTgBf2b068OjrOD" +
                "xtoOT666bnXy8CFCkbwCtbuD//yDbpFtO0RPUzy1U456es7H4eEkVKD/WHYHmMQWTAjCtTGuTD2vIK3o" +
                "n0C44yzUPoZlnUOJ6CuPp18pbwgQ7oqi71bW5AiZy7UxulSZFMGZOnOblTS/gDnJAgHkGUmAZ0MSnPzx" +
                "3//8VEfQ+mqhFeN2MT5wK1387VuUNeAlsLYRzqm38MUv1dd+hMKWpbKDzaJ98id9wmLSSfbHp08ey1eM" +
                "bjgATrTduBGw/GgVLQaP6aRwI34BXxfTt+iWWld8L8XVzq4OPEODtT9dDvcul+Gz2CMjLSbtisx2nM22" +
                "8LHFdQPHmcxloXy4A87wlRtwls8+wc+ZekcAwKj2adhFGNWDfnSM/5AUYD/Fn7Ovzn+AMdPPF2+/Pnt3" +
                "BgOjX1/8+O2rN6dn76DQ3YPzN2fPnnqB9ypKbA1xcqPUV/NaAYUPxBeuYhuHxsx5HBGab1DfJ/rphGTY" +
                "iSYEpfmShUbfFsuxJNetV1YHcc6BmjipirrgEBsXVDWM+OE4+1FzvD+lOJPIEjmZegGX0WE0VEOMn8L+" +
                "QPRxpO3kB/gl8duPgdb89hNteYKS0t9hJflBHjs1J/66uhcUqOIJpUZt7JpZ86JcEwUX7ygHeTwU7uTd" +
                "89NXf7+gn5Ss6Q9ZYPKAtfdHqaKsI6kI6b7wTqJk6N1SP2XolWI3YWi76MGdfH326q9fX2aHhO2+HMU9" +
                "aWE3oXjc01UvzvKykB1SFo50Pao6v47uzq2jX5J17lqF7Riednp8LkTZvyastmYF/CvMj97+UCZZDSgb" +
                "iYW1TRuFl8hDQlPOZ9SpTXvHrvbxuSOqF9IBMQNLDTZPrzRK6s7gSBgMvKdMGIMBjUKbnfoUvdJhKkwO" +
                "jE6CvtcSNAmeZDyRkdeOl1CaS1K1ybj72yOQCcm+XoYqraIjNeJLY3HHH0nN3octYqAZLFCCLaNLagoE" +
                "2yqFaFirF/Am/pKoWtfSLN2c0nwdGnSwTdbB4Pi07z+MuMilAyBFBweLCySpPD/Dh2LwAl0rq2Czh+5S" +
                "jtVJ90Ytv5F9VPM7g9MX8NLOxfdPFFVzO5Hk4D0hLLH6/gKxFlAhdRr0x6RAyBzkt9nnTAt+ns1+xf+K" +
                "7FkmYXaenTwDp5v5+0cfmGkMX7/k11n4+phfi/D1yYdQi3j/9IM8+3Q0+Eh277NBvmtv2Wwwx3Oc3jv5" +
                "H0M9KBzpiUmur6iCie0urrs/COX74+SyAr70Lip84FnZ/mhta/gQMurJWmrfzC37dZmicM5pyJj0LzuE" +
                "uih7Y1DVaAcFTtEXg22OsffRnk6MdrcVgy1i8WFvW7tNGsXaZybgEEyYHmKvXnNvKdpwaegf9fl7rZte" +
                "daGQJneOPM3Tm00+hROuc7lLX1JT91d1QsJfLnn4irE2vijLRySDQPQuO711R9Eb58+nP/Q8GuTe6MRQ" +
                "9yd8h+SuBsa98TfhcW/4vZzZgCY8t/Btj50P9ayppi8k/+3LJhKdeZJHXeMNobx2c/1tPvre78MSqOUg" +
                "HYdC9cxMpmD/zXFc/vPkHZJJN+ZDcCaGfUzDkXueC3RJdLoiRrwfFWs68TCyQ+SLLHvokFnFwSIg9e2g" +
                "mv3QztGUg7MXqO6o5/CraayEfqgAQhxiJ2m8u6VI3MuJ7zL43cLq7jX13IBwInG3u50aGu5qp7MUDYcE" +
                "FbeKeb199VDNGc3nLBLyckEoPUqPw1HU2XmDtKCzDTYZt2GnYZKWxoQ7bq0oCCTzya26pEfAXTG6E/Ns" +
                "5OsS3+nIOGii10//1zHY/eiUPlmSFFDuaYozfXKq1HGF4PbOetSuw+wPhg3FApf9cTlTu7Y++q3lK3fI" +
                "rnKW9swOol6mCuEG/OZ616tYYJKwIUA7dhWvUK0IDgXLLEUosockV7jjtsCJw6Vp92yxX0u7e1Nu6Y9u" +
                "KCm83Quv7LWhv0sh9VuGPq6UXFWnN8tlLhJ9FQ9HeCs2Hg3Idkd8/t9VgL4LUJIlX0jYlZmmoeLwNiyp" +
                "fMYrwFO5mmwmtxNOnITBuyO2Hx3x63DEv6Y22+e0+WagZMvxTqk0d+IRXaLo2ml/cFG2M61QquE52uks" +
                "CTcIRQBkvLQw9lJ4eQAsXS7wxbR4hiQcK6taUgI/Llg2crlmabMm4NcOOVExET+uRlCIiTF0LTwZhSLU" +
                "r+xUG6dVE8fpRIPQ3+DcT5KMkaePAHhzfgng2im2BAa8iJrce8ZuhUXa2OMYMA89zp50/L2ERsEiYeyh" +
                "Jm6Bv/4Tu2tJi5sSVeOYTVaqgK933SLR04fSewUaoJyydf2wR/73CoT3WxSZstW6EaU5DpLfS73KMYod" +
                "i7/c4XmEwQrJCAmPjqp6JQok1eopwL/4WNPfkxcb45teekvzIhj6zGuoUnd7OZyQ2iJPJX+Huf9bGkGL" +
                "HeulDa297TEgF+6mbPCqWtEaL32fgpLbc2ZnN3njeif8mQaUlenzIYslnKW9XiscDrhH+mM02bPkD3dI" +
                "4WLQVS+3HnXMUx0g1wL0qk4t+kz8DMUoWdj9mAjjlmy1BY1KyQoIInrd3x1rXqHtmdybPRG+RsnA/yAE" +
                "EZ7Ior3zDBfX+5KovBtZxBdS9YDgKbkF4z3Rgf0CgGj8HdlFNWhP13jPHYcwLlSU9eYpm7e73iFRZ4HG" +
                "8WKqJ4YAE9Ol+hCJT8yWK3DN2miDTtJ9sytzrBpo6RdGSMp+0lberkHHu9nNXSHx7HbGSj5L64uryE/0" +
                "JnYkTvltwGJeJtg9k7VLbIF5QDPLeUnVd4ztOg+yyt72K6djlN7+Z3MAM5EljhXN5CBdg5V471BbfHLh" +
                "VqG7HuqMdmAf6o587N2Bs4zKqSRWaFrlFyycS6klG1lPXYvkqpy7ocvEtbxymtjpS6hcEMTvLFlddu8V" +
                "dGAMf+l5vlcBeKZWEZKQfNj17o2Ed9oizirbuXTfkNh8w5t7wjEK0XekPnIWF60lio/6wSA921JXEtCa" +
                "9shDZKEGpXM2mXhj51beD/5t+fCxrpBCB15soVIVCtiJsgi3EmXH7ENym3VmMDmYHT3gfvoqWJHcwXQK" +
                "HM1H0k5uGjxm5+mjY8FPL2Q/Cl2+3c75J6a5GPzYCMZNZNwoyLmfJVawRYSDTfIWuB3MEImvGAH1t6jg" +
                "Huyx89447PCQZy46pa5HxNbojVXIrFUPjEvqCzjDpmyXMB33wugq/OSM2MhANed0DMgjfWP+N2F6pJLp" +
                "Ka1SdFNla0MX9UBgvIfiUxzB+fkBBYrHKOXjz5fHKE2zXOFq4GdvLs7foeQ+eBAr8u7BD6H/wSlMOaew" +
                "9r+Qf3+nKdGCLh94Ve5/y2V40yYTfvQ/0DEINQWAmKN7CVH2/pyXj1H0whf4qvczYdLqJcny5FJirIEM" +
                "fiqtn5b+L/pl9oyATwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
