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
        [DataMember (Name = "workspace_parameters")] public WorkspaceParameters WorkspaceParameters { get; set; }
        // Starting state updates. If certain joints should be considered
        // at positions other than the current ones, these positions should
        // be set here
        [DataMember (Name = "start_state")] public RobotState StartState { get; set; }
        // The possible goal states for the model to plan for. Each element of
        // the array defines a goal region. The goal is achieved
        // if the constraints for a particular region are satisfied
        [DataMember (Name = "goal_constraints")] public Constraints[] GoalConstraints { get; set; }
        // No state at any point along the path in the produced motion plan will violate these constraints (this applies to all points, not just waypoints)
        [DataMember (Name = "path_constraints")] public Constraints PathConstraints { get; set; }
        // The constraints the resulting trajectory must satisfy
        [DataMember (Name = "trajectory_constraints")] public TrajectoryConstraints TrajectoryConstraints { get; set; }
        // The name of the motion planner to use. If no name is specified,
        // a default motion planner will be used
        [DataMember (Name = "planner_id")] public string PlannerId { get; set; }
        // The name of the group of joints on which this planner is operating
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // The number of times this plan is to be computed. Shortest solution
        // will be reported.
        [DataMember (Name = "num_planning_attempts")] public int NumPlanningAttempts { get; set; }
        // The maximum amount of time the motion planner is allowed to plan for (in seconds)
        [DataMember (Name = "allowed_planning_time")] public double AllowedPlanningTime { get; set; }
        // Scaling factors for optionally reducing the maximum joint velocities and
        // accelerations.  Allowed values are in (0,1].  The maximum joint velocity and
        // acceleration specified in the robot model are multiplied by thier respective
        // factors.  If either are outside their valid ranges (importantly, this
        // includes being set to 0.0), the factor is set to the default value of 1.0
        // internally (i.e., maximum joint velocity or maximum joint acceleration).
        [DataMember (Name = "max_velocity_scaling_factor")] public double MaxVelocityScalingFactor { get; set; }
        [DataMember (Name = "max_acceleration_scaling_factor")] public double MaxAccelerationScalingFactor { get; set; }
    
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
        public MotionPlanRequest(ref Buffer b)
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
        
        public void Dispose()
        {
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
                foreach (var i in GoalConstraints)
                {
                    size += i.RosMessageLength;
                }
                size += PathConstraints.RosMessageLength;
                size += TrajectoryConstraints.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(PlannerId);
                size += BuiltIns.UTF8.GetByteCount(GroupName);
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
                "H4sIAAAAAAAAE+0823IbuZXv/ApU/CBxh6blS1JZpfxgW3LsqbHlWMqML+VigWyQxKjZ4DS6RdFb++97" +
                "LjgAuknFntpIu1tZJzUiu4ED4Nxv4D11sbReeVNf2ZlRM1c12lZeNUujCjO3lW2sq9Tc1Uqr2vzWGt+o" +
                "xtH7lcN3g3tqXeqqMjCiKuiFa5t12yjbqHXtrmxh/ABGvdO1XpnG1J7A4cCNqy/9WsPCzVI39Ehg+aVr" +
                "y4JGKNgQABn8IqMzSBHCZB0f4mLnja4bWy2Ub3RjVLsu4I8fq9dzNTM1nlH96mzVeFloSofHdWpTAADY" +
                "ztp5Or1XDnaGO9YV7XHW1rWpGuUq40f4xJtsMAMEEADSm0bBVDN476auOae9eNzahPaFO71Y0mRvp6VR" +
                "C6dL3nJC0soVpkScI2rw6Vid6tlSmdKsaBdzgIIDdV3rLVMNpmsGVpsF7GpMy9ADoDbMtuaKjmnnfCLY" +
                "eFNrQgjTeo34m7WlrgMIAA971431cwtTX6QZn78Q5EkGBA/21gXkAyp1tYVDwhulSwdUIUrrZgmk5c+1" +
                "K9qZKQJP8VE3tizVlXUlAmEs5/s8bJBz9XpdWjgu4EfDcFoEiFK5Rv3aAq9u9JafDfMt0+L9DV/0EIEb" +
                "q41vS2IkePqrmTWu3qoVAmZUbAcX8XkOP43et0oFnAp0y6Qo8j2co/WGGLVyPBDlc21mFvE+QtZEGmvY" +
                "Vn8uIQy4DgAUA1gTtx3eTWyxb/FF7do1fgnCAMA2SwvMRbgVuPDRrU2tEQ8Cl2ZOEFaE266mMBgh2xVS" +
                "REDgfDgWSdgKFIMpxup86eoGdYl3ZRvUiGy/Nmt8WYwHsKfHjxDwhLYC605005jVOqFypa/tql0pvXIt" +
                "CQOtvg+zyCxl6TbAZZkwqUNgQW+ASAWwyLx0uvnTExmYlkWgpFdmusTjzzUSl4XFrXEdmLKFrQMb28Dg" +
                "sjXCrboypZuBikDRrEjDzGYgwohVYJCxUs/C5q502eIgEDfY2uHR6OEXeHtxE8DtHnCJX0TAatQ/QZUg" +
                "5BVyNUoOaL4tksqgnOO0xl4ZABcOCCsDJxpLChAngm5HJYlAbY17tYWqdbWAHR/aFRJOV025HRH5UcFU" +
                "s7IFCwCEJXVsyHwcjY+GpDnDOsTjJloW4W9CBdL04fiIYIF6Z0Qf2rEZj27CCADsvsmRMxxHMsOgiUya" +
                "eCbthHfUGZNP748bDJ7+k/8N3pz/9RhodQVon6z8wj/YY/mI/QFrIGleLzLLrQmRgLNkEFEhzFtQ/Shl" +
                "8Jb0Gcg9ovoK5A/k5VCrqbseIruIAhCm6coNbmssi9sI2lVAlM3SBHEjEUEquKktDRkTj3sKlooBazDf" +
                "pizHKFcnZLWYI+qgoGozB9NZgXcgphC2CAetKzz/K6PBVoN1xT9RG1hQFkB1HiRaDqaNQLuAuQksLnzG" +
                "+/DiK4AZNoOFcYA10NuE+Z9JiT9GwBMG2lc8//ylgOFkqdthLd8UvCTjkN2lqtA12F/TaHCVNKF8aRcg" +
                "9vdL8BXIK1mtgQfobbNdgzeVscHCwH5JMtH44KFB16/ays7IeKNByOeTMHe9jJlzdWErHE4cgNCJXcHn" +
                "RB54fXJM1tnMWlRRsBKoltpoj+h8faIGLVsLmDC4d7Fx91FbLNCiyuLsZMJmzfUaqIP71P4Y1vg3PtwY" +
                "YB+LLVCH9GwCX/1QwSKwBbBLIBloMN5tm6VjzXqla6vRdwPAoBlKgHqAkw6GGeSKQFe6cgKeIaY1vgds" +
                "FeHime6DO1qQMfLtQpMKDe520OmgFEDBg/Yr7bTW9XZAlpGWHNx7SVJGNpooglLuPWhCIEBBHCyWnqhB" +
                "/sPtcONeKRDWAl8ASGXQN9Gg39lWgOGuDZwENeIYmeQ1kZV00MqA/UH+izPRX7I1GrbgB4NecbUZYXxS" +
                "OLBM4CsCjJW+RH8dcEy+JLiVYFnRj6t8yUYVHsOUQzNegOUhXUejEEfE0SQDdqZquwCjSDNhoVWcrFU4" +
                "HFi++SN2d2jPvBgQDIDUrglWCg3v1rXgwcIZ4EMdRI88KdkXsUjj3AjlLoDoIvQdGUAxE2AhGhB60Lli" +
                "367jp2389PUObFqKiG40ZbZK+NNT8Dy6mrRBGoIbwBFECtbmQCYvHi3GpCAkl36AtHU1L/4jvuRwjMal" +
                "cOxHF3x/UBUrCKeW+ip6S0adnL3kOCj6VxTd5aDf4FgYly1B0yeFm096iz1rGojFAMrMlaX1eE43xbgB" +
                "1IGWd0BwDwSlUyiXuXPDgcx/IdPPaDYEZDJ7EiFPAmRc92WpF4DdArUzsi8wc4hw0Q+bAR8nn52cLhCl" +
                "htQlCdN8vmPiaIccJtP8zDSsnG9QW4tzyKZFdBcFrnLUqSvQPz6U/cBADOTQmpRG1/sGw0JAcB1Y6/gY" +
                "LKc5Ps7C7SlJO2cAcEEQYdp8k7HccDB1DkPjCR7u1szuXgbMMKWjCBD7LV1Z+Cj04EPPajtlH4kDazp4" +
                "8PZAr4ChJNmpHZkLFgDUj8FjiZMMpg/YMT6sDXqA+LwG82E9ittsiLvhRALaErSRYMpyMRN/R6Dogpzu" +
                "4SgNTd54f+gDT4MfgGFF8UxTzBxo1UQ7zbF9jGECgLcrnP0W3Hg82Gk6C3qjlQUsILcVoPlZQsEWoj6g" +
                "UJURwT5jFGKO9Mk4wrrJ/WWoIRuzATkHhgf2KjB+hWBtZww7NQ6ER8hIyoPFSKaqkOfyZH4t+Tl7FKC3" +
                "nj1mnZQP5Xf8iFZAGSIyogfVxTBthsxinvNB1x0tHE0K4+HsyGESv47VL2jU0L6xvQkqlE5ROQAY6NNz" +
                "FBDWakS2agZBNUjqlcnJyXkpjNq3gRsRe3wapm129pCOWwoMwpO3X0HXw5EBkQwnkxrKOFL4Aatg+kR4" +
                "IG4zQw4lP2TT6LujY4BuJlEQzGI/rmAnCDQq8U8wkPBVBCF7JAyfPWIM3IVC2TU7cKr34gNpkVnWAcg9" +
                "gbiEi8hihVmAZ0WMhy5W4YCumAxwGACKugaUtLOmrUmXpPWYkdkVA9RDVFmwJGuRT/T4/RYckBVj3q/J" +
                "FSbXKLpLPGdhmiT/mN2IacNLUEqkpNRsCV7CWL1EUbgGp7YEHtEUf+palIUmDvv7+5OXpNMeowE/hBBt" +
                "C//XG3SlOWkNfjm/RA5GLstChHx3jEj4U1uAwnNRojvvyZvEEQINBPrKYEpLTfXsEg/c2cP/q7G7VWMb" +
                "zCosv1uNyfD/S2rsJi3GDihO9730w4WwMIyK7LwzaAMExQH4t/fuF0ITvGR83U24GHe9J2AkcYhqZWqa" +
                "jQG+aDZuJ9fgezHlYCDJmCz+G/yt1Zh/RAUgUdrdHDItvC8sBlmtbdTxnYPgt9/SrhEP34z85NPmjghI" +
                "nBSOJVrXJ+exe55p7S4NHpJicY8xEcYFqId1taA8EuUax5GAYUj6HsbdzelYJvZQDUjB5EmHG4E9x5xt" +
                "Qx5vg4rqO49IwNJXDgXuIi99Q/wZdHbvaazvxAiOFJgG/X0t4QpnSy2d+lISUPhZKk2Ex06VWoMrgkXT" +
                "pV5zaYVyq6l8urM3NmR5eI3DaMF7r+cIj+QIyYVnJZCVC4H52BahVjGieqME1ff2wZMQm613PEZW6MIF" +
                "sJbaRRQDlURzCPAoiZHq5HE9SRhgWiLL0zeuJcabbmM15X7c2ETqirqswUJss9J3NiFZDQI24TQK+nip" +
                "iM67Iq8CHK8ZB2udvC5Qg3xFpgem6fiQlKJbIYx4FAAdsmug1AygHdxnTvnNSkd5Xl27lgQhQAlFJFmj" +
                "MjO0v/WWVqtNyenhUHUICyP5AKbkbrJKbYrMU1EXkIG7m4QlhCgbYxfL6LH0iDHCsvpl5TZV0qY0/i5k" +
                "clcWnwU3YMS5+TmlUENSR3x6kpn9NRXg+HDMgMBD4h6CBdR7A2u/boYirKnOLHTerg0zBWYyptpT2EDY" +
                "idLDfyfoXC6444TPwke4QAgIJhXpJVsWtC0VVXe9OZFZnmbroBlQUvaVaPKkniDg3GGBU5aZYYpkZbH+" +
                "4AekcXifNOqdvMLoMA3rZ2N95/2EEQ8rvTF+2YWKT2Dsil/shYPvEojnKBxS9Mc8mkHjH7RZaiFQ0zb1" +
                "2hiJUdiJgMO3LGlAsaKw7FIT4ob53t7hVDwIrXTDIfFd2t2zovA5GwWsx3oh5Q0p650NAh69sq714Aeb" +
                "a/AUcPu2Ye3MCmc8mG7Bj3t2cvL0aEAxL0pDZ6V57VackKiubO0qapvBwKpG//rQQGy2BdVEokBp3waE" +
                "2fd4whZDXun96Zuzn0+fPqQzrdeopzCCreK5KOYNipU27WObyz88qxQjeJKcE6iQDvnu3enbk6ePghJO" +
                "a+5fjlYZgVbcBM4PpKaqyCH1fwS6SRhDnS0wojTzhkOUITcJeVcirgC1ojGSNi2Mt9g0RVsk3DzGDZ6t" +
                "pRuBLS58RQdUBjp5fVtK8dtKZXDvd/9TZ89/PH1xgQXH3z85/EPkvPjHNQ5SmhQ2z8ngBUUGagwTFRjC" +
                "eMOBdVblb9yC0+YxdOQULvAJZsk7TsWliXnZfIVjesLzU/KhFoZagMaqVDEVZQ9QBGAxzbcSDCzlTn48" +
                "P3v7ADt/QkLl47M3PykGMFbPIguDmo0CkNXiUFELVlLSiI26GJSxOiWvwVZ7iE5yRAG9c5fgr1yaY/WH" +
                "/zhADB8cH7xAz+bk+cFIHdTONfBk2TTr4wcPIPzQJWC7OfjPP/ARa/KYKsfZnCpoRqZe8G6QOBkW0HO0" +
                "zQFMwt5KkIJLY0IVel6CqE5taSUJYPbxKxYzGIlSWzx5zrxBQPBUKPdhZc6DIHOF/sSQFaMaN2bJwmEp" +
                "nU9gjlVEAD1DFMCzPgqO//jvf37CI9D0cikVxu3u+CCsdP63nxSQzRusYUQ6dRY+/618JSMYNi2lDjYL" +
                "//hP/AQrRsfqj08eP6KvMLrGARbd3DACzP4GgvneY/RQ8CCygBS/+O3KFW2J76l82rj1gTA0sPZt5Wpv" +
                "8hZS8wv1jvg1ctpIzbbgWpPTNsNEWcg2SZRTm1ieAbaSLBN4OFNxAQAYKnw06SSJ7DgfjeB/4wH1SvxZ" +
                "PT/7AGaMP5+/e3X6/hRMC3998fGn129PTt+DKg8Pzt6ePn0i0i76iawM7imMYi9NVIIFQ+ulJpuGpvR4" +
                "GhG7aowmA5lPyIYdc+KPWiqxlCjNrjgW0XUtmuogzTlg4zYIrIlv4eC0VY4ePozUR87lfsr3jEimgMlU" +
                "iyYmG/s6CMMmn3X/jBNuJx/AI0nfPkZc47dPaMWzLTH+w64oD4hkR7UJf0NW2KP3w0qFVHFoUdWFbX1q" +
                "A2YOGnfoOnn/7OT138/RQ8rWFCITTCQw9/UwVph1KP1AzRXiHlImPiz1SWlwOLg7kbsqOnAnr05f//XV" +
                "hTpE2OHLMJ2JS7cZxtOZlp3wSmRBHaIsDHk91HOyDp8urMNfsnVuWgW7LQR3TL4QnOxf84WrOBkgr2B+" +
                "8vP7MolZf1tTCMzN141dJx4inOJ8DDa5G28Uahw/BKQOepIY8BdZqnd49EeTpO4MTojBgbej4naDAAo+" +
                "650iFDqj/dwXUQvdA37PRWbEdpbcHKsBd7PE4luWks3G3dUBbRUzl52UVF4k16HvvXPcb6Rgb98EYWgp" +
                "hifbKoaTqCAgumbhq62uFuBB/CXTsKE/mbozqZM6dt7AGbHMBc6O//xlgGtcBABUUwiwBkF5hMSdzJDY" +
                "69JIayrtZg/OqdTKk+4IVXKMPSiTYx34tCnuRPz8mPdprieUB7yT3VJcvrfyy5VRMwrxfYr/Y5JAX6sf" +
                "MP33g5p9hf8U6qmiiFqr46fA4Gb++egLZhTj14f4dRa/PsKvRfz6+EssNXx+8oWe3RYCvpHD6+W19hbD" +
                "elOE0fjuyP/QvkXDUHE4u3/CGiXVfUN7fhTEz6PstgF86dw0+IJUct3R3KbwJebMs7XYlJlrbLvFPETw" +
                "Q2NapHtbIZY6sUhcY+jSrVmSjuidcgxHH+zprPC7rRXY75Uedo6123RRtJJ+ANs/wRzQhNqv7yYJm+78" +
                "3NyoL2o2v6iCspndGBKE5/eSJEkTL2OFK1tUI5eLNjGfT1c0pALM7QPM7GmPURQ6V5XeBTp0xglxukPP" +
                "ku3tjM5scnfCz9aH6Lcz/io+7g6/fYL1MMIZGv6yx6THWtWUExSU3paSCIVggu+kYsTs0eswVy7ioYP9" +
                "OS5xH94iX1czM5kC429Gafkfsnd6Cgf4Ev2GfkdSf+Se5wSd8pihQJGuNqV6TaKEOixM5Roy/lgTh6hT" +
                "ujo5v8ENoDn7qhcleHXkJ3w1taP4zkNI5X1qCB32qyS3T+5d3r5RTJFmRc/oR3Kko+72XHBAy93KVA3s" +
                "Y5M8KEzb7St0ckpoPsfq32FgQoJCDQvDpKp1vTBNMAkuG7cxpJTz6zg3XDhhEBMCMeElZQPhdtCNO1cD" +
                "qTn8zCPToAlfG/3fx113wF5dpKQMjxaEAkEfnzBqQnnX31ho2nWMhSpOEryHlCNsMQ4afm9dahCvHqP5" +
                "zhvGekEtpgHB9H93Iet1qhxReBChjUIpK5YhohOB9ZMils5jDiveTVsAuUfoTewesVsku/lQYelvHiiv" +
                "qN0+o+w1nL9HFXWbf76tjkKtpjMrZCUyTZUoQ4yVWogGNzUmdcPv/6bqk04+SoTcp/BKmbpGlSGmK6tn" +
                "pku7U7pMbCbXE5w4iYN3R2y/OeJrf8S/oh7b56bFGnw8b7oFSt2Zlrg18+W4wbewfsZFR7Y3w51OkXjt" +
                "j7ifxlMPYic3pyPgLefRNlwSs2sqlnKhyOEVrMjY3CaNgN+EzZFySfvD1RAUxL4wtK0lPmYGjlUpN+XO" +
                "Z9bBaTpuA6G/BaIfZwkhwQ8BeHt2AcC57WsFO8Cro9k1ZThtw3wd+xTjzmOTsqAOf96gZrC2iVAzb0Bu" +
                "7qT2WMTFlTWbLE3MWAGm3nWGSEMfUi8V4EBPy21oaB3KzwsQ4/sWO3fbmtTlOIp9J6dKZCQLln5oQ3gE" +
                "oxPLP3yRnFN2RhhIrs9zgH+RyFKutV9J1ro/ku5wzZZ4QV3uG0cKsRUSLMmt4+5PX0QVNuJeZa6o7TEd" +
                "5+F6a3SmPKmMl9J6wOgWzmzcRtehHUJoGrfMTK/7LJZxlgm3Y2oH3KNT08YKf2eDKhK9tni6rchjnvCA" +
                "Ed/F1pyZBGVGHobv83b47Q+MVdR6CziyRZRQvp0fyKrLDTYQwMDHxNcOSxB8Ixc3PPHyMzCRnvGqeVcS" +
                "mXcTi0h5lAkEPlJYMN3v7BkvAJDMfkA7qQbu0RrvuaQQx8U6Md8Yxe7rpkMk1FmA43ShVJBBwNbSbNP6" +
                "li5D0+21ujXcc5M11OzKHCYKuKALFugq+NwWoAIeb2a3cAVE2O30ijSHaxfLxE/oSuxI3GifFhOZwIYY" +
                "5VeahGVqZrpNUrXHc6BV9nZUBR3D+JZfuRncy2UJx5JmCpAugZXwyiB37Wji1lFilpnme647Ww/ow3Yc" +
                "cJOXmhpVsG+QVjgKziTXYmg99iuyi27hZi2mpulV0MRBX1ZY6C/lZNnqdHpR0JEx5LLyfK8CEKZmEaIw" +
                "vN+5LkZCPLa0Z5ZtTQ01iGx8M6JdRqGUDtOjYHE3LuyHPWBAPbaZrimONX4oELEIU5oGW0fE2IWV94N/" +
                "Zx884hVy6LAv7IpiFToc58oi3imkE2NrUThsMIMZYXb0QPilqmhFdIAZFLhbc2O4qeExdpIejWh/fJH6" +
                "KHbtNjv0z0xz0fttEBg34cx/lHOZRVbQQ2wDhyy37PrkM0jiS4x9ukeUQsKunRfjsMNDwlzokYbOD1dV" +
                "1PXLZcy+ccl9gWDYmO0ypsOzYFwVfyGGbGTEWnA6euihVjD5CZcOqmh6jqt8u7mydbEruicw4qFIZiM6" +
                "Px/UU/VopD7Cn4cj9YnKEqG4ffr2/Oz95NPT3oNUag8PPsTGhqAwiU5x7X8Z5/5GQ0IIwO+ix+WnV/pX" +
                "ZZgZ5Sc1ekEmAWBbdDv7z4OTvT+8JaxHd7WAozq/5+ViXjy7T5hqHb3fNOtkoP8LPW4OfChPAAA=";
                
    }
}
