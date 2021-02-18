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
                "H4sIAAAAAAAAE+1cW28byZV+J6D/0IgfJGVoWracIKtADx5LzngwthxLmfEFBlFkF8keNbs4Xd2iOIv9" +
                "73u+c+rWTWrsAWIli+zsIha7q05Vnfut+kF2tShsZnV9U0x1NjVVo4rKZs1CZ7meFVXRFKbKZqbOVFbr" +
                "X1ptm6wx/H5p8G5v8CBblaqqNA2pcn5j2mbVNlnRZKva3BS5tnsDjHujarXUja4tQ8TQtamv7UrR2s1C" +
                "NfzIQ7ML05Y5j8hoTwRmb/CTH56ACiDGq/BQ1rtsVN0U1TyzjWp01q5y+seOspezbKprnDT72RRVY/1a" +
                "E0YBlqp1Dgi0pZWxjASbGdoddq0q3ue0rWtdNZmptB3iidXJYIEIGATU6iajuXSAt2ZimkvejsXuxrw1" +
                "2e7VggHYYlLqbG5UKfuOyFqaXJdAP1CEp6PsXE0XmS71kncyAxiMVHWtNkJBmq8EWq3ntLURr8MPiPI0" +
                "vdA3cthiJuei7Te1YrwI4VdA47QtVe1gEHzav2oKOysw93mc8vETwx4nUOR0r40jA+FUVRs6Kb3LVGmI" +
                "Pkx21SyIzvJ3bfJ2qnPHY3LedVGW2U1hSgARdKdbPWjAyWq1Kgs6MiFJ0XBehKhTmSb7uSXeXauNPDvs" +
                "7JpX397zVQ8d2FutbVsyV9HTn/W0MfUmWwK2IGSzN7gKL9Il4vDdC1XEu0TCRLaCKNBpWquZcSsjAyG1" +
                "Kz0tQIAhsyrIrWhr/cmMN2JCgkCkonWxd/dyXOS7NzCvTbvCDycgBG+9KIjXGMseNP1pVrpWQEcAzVPH" +
                "AJaAbpcTGg/gxRLk8VAAgk7HgrckpaHzUXa5MHUDRWNN2Xod409R6xXe5qO9AW3s+AlAj3k/tPZYNY1e" +
                "rlKsLtVtsWyXmVqalkWEd7ALyeCesjRrYrtExLID4kmriWI5eGZWGtX8+akfGVcGVKd1pqoEHmYKxBYZ" +
                "MissRZM2dABi7cIxvd8dYzm70aWZkv6AyFaif6ZTEm4gmDhmlGXP3AZvVNliFIkhbe/gaPj4E729ugvi" +
                "Zhe8yD9e7GooJ6dlAHoJRoc8kWbcgGYaCgDTmuJGA547I61NrKkLVpCYSRYAWhRQixq7LfKsVtWc9nxQ" +
                "LEFBVTXlZsiMwLqnmpYtGQoiMStszWbmaHR0yKrVLcRcr4MF8gzP2ABpH4+OBBjZAMH2QTHSo+FdWCGI" +
                "3Tcpfg5Hkdw0auxnja0QeCx76g5KAWwN3BsMTv/J/w1eXf7thEh2Q8gfL+3cPtphIlkUCHUkeVbNEzOv" +
                "GJuEuGg5oSZmLdkGljp6zaqOdAEQfkMCScJzoLKJuT0E23il4JmnK0TY12jPL18E4KYi0qwX2gkfSwto" +
                "YSZFqdncWOzKWzMBrcjU67IciZSdsW0T3qid4qr1jIxsRc6Et5i0TTptXbFL8J1WZNjJEOOfREMUpEKI" +
                "A2ScV4A0c0hKh2yS43jPdLIZ610LMtgkCXNtCH2k2ZkEP7KaPwbksUDd1kdfYTViP7/aV+Iz2+SypqBS" +
                "XKwqVzVZat0ocq8Uo35RzEkTPCzJs2AnZrkifuC3zWZFHljCEXNNG2ZRhX3CsckQLNuqmLKZh7VI57N0" +
                "d12SqTF1XlQYzpwA6My65K2CF16enbAR19MWaotWImVTa2WB0Jdn2aAVQ0ITBg+u1uYh1MccVtcvLr4p" +
                "bVbfrog+2KeyJ7TGH+VwI4J94o1EdsDPxvTTHma0CG2BbBZJCSzJm02zMKJtb1RdKLh6BJgURUlQ9zFp" +
                "/zCBXDHoSlXGgxeIcY0vAVsFuDjTQ/JgczZRtp0rVqrOT3d6njQEKX1Sh2UxqVW9GbDJ5CUHD16wtLEB" +
                "Z4pA4q0lzUgEyJmHvSPA1CAP42tx404x8KxFfgKRSsN3UaTwxXqQRa81nQTqcQQmeclkZXW01GSSwH9h" +
                "Jlyqooaxc14z6RdT6yECm9yQrSKvkmAs1TU8fMIxe53kgJK5hbdX2VIMLT2mKQd6NCdTxGqPRwFHzNEs" +
                "A8U0q4s52UmeSQstw2SVucORLZw9EVeI9yyLEcEISG0aZ7ZgizemJV+XzkB/1E702M3y+2IWaYwZQu4c" +
                "iC5C37BF9DaDzEVDQj8aBHN3G/7ahL9+vQcDF0OoO+1aUUX8qQk5I11d2oCG5BdIrBHjuxmRyXqPF8Es" +
                "Cck1GQ4Q19Sy+vd4KwEcD0wDuO+NixJIWywp/lqom+BE6ezs4oXETcHtkpAwhf4Kg2lgsgrPH+dmNt5a" +
                "71nTUPxGgKamLAuL05oJYgxSCsq/I7JbIiufJTOJo0ferAfw3M+/4OkUw/np4wB67EDL0i9KNSc051DT" +
                "4GPiahcdw0WbEkNHz57dMZKphvUmS9VstmXteJMSYvP81G1YGttAb3vPUYyM12Ic8PrjTkwO//nAb4gG" +
                "IviDXSm1qncNxkpEe+W47OSErKg+OUlC9QkLviQQsCJJM2+/SbiP0DkxBkH1GOf7ejZ4JzMmyFJBHpgR" +
                "F6bMbdAA5GJP62IijpPE43x05weSkiGryYJUG7YdIg1Qls6BCZM0Ug/iNh/UGq4hntdkSwoL2ZseYjeS" +
                "g4BhgcEku5bKnHd/PBSVs0t+OIxDo6/eH/rI8uBHZGUhq3GKnhG1mmC0JSUQghwH4PUSs18fjvhg5/Es" +
                "8FKrgrAAhsvJDIiskmGEckBY6xAhfmQQZ8kOsKWkdaNfLFBdJmdNEk9MTwyWI9KlcG5rjHg4hgTIk5HV" +
                "iIiSn5q5XJllW1yw07NDG9rCiietohri1JAd8goQIyYj3KkuhnkzbCPTdBF8epg7nuTG09nBYT7EHWU/" +
                "wcLB2InxcfqUT1EZAujo0/MaAGs5ZMM1pdCbhPVGp+SUlBai+43jRmBPTiO0Tc7u8nkLD4PxZItfSfHT" +
                "kQmRAieRGs5bclhCqyDf4nkgbDNBDmdK/KbhysNLgM/JFCQb2Y01Bs4jIr3K/OOsJf30gpA88gyfPBIM" +
                "3IdC2bY/dKq33iFSXmZFB4B7HHEZF4HFcj0nN4sZD/5WboiuSBYYRIZeYxNK2mnT1qxL4nrCyOKXEeop" +
                "3MxFkpWXT7j/dkPeyFIwb1fsF7OfFHwnmTPXTZR/pD9CtvGalBIrqWy6IJdhlL2AKNySh1sSjyiOS1Xt" +
                "lYViDvvH27MXrNOOYcoPKGLb0P+rNfxqSX2Tky4vwcHgsiReSHcniKR/6oKgyFxIdOc9u5YY4aGRQN9o" +
                "5L6yiZpe48CdPfy/GrtfNbZGqmHxxWrMD/+/pMbu0mLiiWK67SUjrjwL06jAzluD1kRQDMC/vXc/MZro" +
                "peDrfmLHsOsd0SOLQ1ArE92sNfFFszZbiQfbCzAHA5+aSYLBwd9bhewkFIAP2e7nkHHhXTEyyWpdBB3f" +
                "OQh+/RJ3DTx8Ngz0f63viYDMSe5YXuva6Dx2zzOpzbXGITkwtwiNEBpAD6tqzkklzkGOAgHdkPjbjbuf" +
                "04lM7KAakULIEw83JHuObG7DHm8DRfWFR2Rg8aeEAveRsb4jCnU6u/c0FIJCEMcKTJH+vvXhiiRPCz71" +
                "dahL4UcsSzEqO/VuRd4Iaq4LtZICDGdbY/W1txGCIcYsjbQxThZ98HIGiCxMoBkOzEAr44L0UZG7esaQ" +
                "a5Uhvn6wC6IPtsWGh7MkdTGswLXYLr6M261PQLtIj1MbsegelvQJBGQqkkx+Y9rpgqvZm1B2eRg2N/Yl" +
                "SVXWZCs2SRU9mbAXDQiDG7v0CsAmJXnZGbsY5IVNJXLrZHyJLuw4CmWQwJOjcvJuCRjhOIDtEm+k4jTh" +
                "n5xpyQZOS8MpYFWblsXCgXEVJ79IpaewxvWGl6t1KZljX5twS4OQBDQkdZJib4zUY12YUIINjt0ikTpr" +
                "XcwXwYfpUWWIEv11ZdZVLDrJhHupLG3L5zPnGgwleT/jHKtL9ng/n4XozuILyYA7q0PkAbMSgyM6vqLl" +
                "XyJT5WQ4LVd7om9WWjgEOY6JshxQMJaiSMkfY/idc+lpkSPJSa4AAnDSqr/PqDlVzEXZbVfPy7KbV9RO" +
                "aUB8dtVz0uRfxMSlQYHUrzRFCmVZoFiB5CP0keyWh73x7xA+JuP6yVvbGTB2ZMBqr7Rd9CDjEQ1fujc7" +
                "YeFlCuZbiI3vJUDKTcNJcAov9iZkkzb29Wgfy4izQWhoRQaJfnleiOvNKDzs7O8N5uI8vNRdh8XLdIfP" +
                "8tymvOVoEGqOnGfkdHkyiHj3pjCtJZ9Z35JXgSMUjShx0UdE7MmGnL5nZ2enR7LSW9a+ncVmtVlK/qK6" +
                "KWpTcYcO4rAa7viBplBuQ8qLpYTzxQ1Juu0xSZEfusXenr+6+PH89LE72WoFXYaYtwqn4yjZKWDeug39" +
                "NL95Yl/LkEn+tESP5Khv3py/Pjt9EpR1XHb3irzQkJTn2gmEozvXVQ64w8SR0Mc+3EJDI0o9aySuOXRN" +
                "SdaUQBlh2OuUqHRzbQvu1eJtMoqOZZMXK9/nIGaafsJzDWONf//VdOfnlc7gwe/+L7v49vvz51coXP7+" +
                "ye4/QdDz366WsF7lmHvG9tEpOtJyyHIg/rFaovKkeaAxc0m7h7hT8r/EL5xl7/ki1zrkddNFTviJgIjJ" +
                "i9rz1pz0WZXlk2AVCEyEmU/SDTmjzOmX7y8vXj9Cm5HLybx/9uqHTECMsmeBoUkTB4lIanvQ5h43Me/k" +
                "PAFvekbZOfsaRbWD+ixZnBQw5prcnGt9kv3hv/eB6P2T/edwic6+3R9m+7UxDT1ZNM3q5NEjCmFUSUhv" +
                "9v/nD+6QNTtblZGUUOXUplDReUUgUoIHeJ5Fs0+T0OdJEnGttatrz0oS3UlRUpw06trWDuuiKCJ49OXK" +
                "s2+FSRgKzgVF4JaWbAqzmWuTdMk1e8LlI9qjOzD/zhjSSRawIA+BCHrYR8TJn/7rL0/dEBhqKdHSwO1t" +
                "7/vVLv/+Q0b0sxr1kECv7uKXv5Tf+SEOPC+X7a/n9vjP7hFqUCfZn54eP5HfNKHGkALesh9DrsLa1Hn/" +
                "OZwbHMiv4otq7vXS5G2JAVyebcxqP/A42P1rJYDv8jBimw33p9gVOG+YTTfkorPXN0X2zaWwfNxU61Dz" +
                "ITbzqStyjCbeXyBgMAiw/yyb4n8fDen/RgPuxvhL9u3Fu9PH7u/LN9+dvz0/feJ+Pn//w8vXZ+dvT4/9" +
                "g4vX56dPB451vd5iK4Q9uVF4PvCD8oLMsfVV3zg05tzjCD8HDQLYfjohGXYi2URu7USJ0jffYizQdevV" +
                "136csy/Gb+B4FG/p4LxVCULeDbP3kiD+kO4ZSObYS1fzJmQwO1oJaTx0XicdRqOI2/G706Pk1/uAa/z6" +
                "QKhOtyT4d7vi5CLIDkVK/7pUs4WbJEqG9bPrlVV50WILLlgSDhp16Dp+++zs5T8uaT/pmp7IDBMEls4h" +
                "wYqwDuc0uH3D+5Kc3ndLfcgUOSTSFCl9Gx244+/OX/7tu6vsALDdj8N4JqkIJxiPZ1p0IjQvC9kBZOFQ" +
                "1oPW8+vI6dw68iNZ565V0M/hcSfkc3HN7jWfm0qSC/4VzY+xQV8mUUooag6lpRm8KVaRhxinmI+IVXr/" +
                "hq5w8o1D6qAniQ5/gaV6hyfmSiR1a3BEDAZ+HRW3HS9w9FpvVbbgrPYTakwtOAzyXirXwHaSMR1lA+mX" +
                "CRW9JM+bjLuvAxZVSId2klxp5V25HvzOcT+T1/36JgihqDc8yVYRfUJBUFQuwlcXqpqTP/HXRMO6tmju" +
                "A+Um7tDbQ2dE7YycH/vx0wBrXDkAXKhwsAZOebhUoJ/hI7Rr7RtheTc7cM71W5l0T6jyx9iBMn+sfRs3" +
                "Jb2OH49ln/p2zGnFe9ktx/A7y8lSbtVDlwyIyYKQUVC32TfIJH6TTX+l/8mz0+wIxFLZySkxuJ59PPqE" +
                "5GT4+Rg/p+HnE/zMw8/jT6F+8fHpJ372tRDwmURgr8S2s8LWm+IZTe6x/Iv27TUMV5yTizCiUWIx2d0J" +
                "CIL4cZjccqAfnQsOn0Al0x0tvQ+fQhY+WUtMmb5FY6/OR94PDcmT7iWJUD9F5blGJNMthLKO6J1yREcf" +
                "7GjXsNv9GnSc5GHnWNudHHnrUxNk+8fIFI25xft+srjh7tGd/ZMqqNn0jgxkM7m5xMEiMJ7ekPJZnHA5" +
                "zF0h48q7v+gTagN8MQShuwtyfHWGyRA3GuShd2/qjSNHZ6SnUX/wRTTCnfGJce5P+bGwLjDuzLgJj7sT" +
                "7oF4PcRIFkd+7DDvoRg2kfQFZ8l9mYXDsYD5qG8kkyIhQ1t5h9HfE4S//TGs8pDegs2rqR5PSA7Ww7iD" +
                "b5J3akJn+BSLFn5QfNIbu+sFL+DSn67sEa9ZxVpQJEl2kOvKNOwQoPhOkajvIJUciHSbpiydPS/J02Pf" +
                "4VddG475LIVZ1sbm08N+8eUe6L7N6XfKLiiX9zyBQJR4Vr5Gc0dRRhqluerYRym7Vsjz3VFWlezRbIYq" +
                "44FjSgbEHRKHUY2req4bZy5MMm6tWWGn94LuuvAiMMYMYyxrxj24q0p37j/bG/gCxo8yNI4ayy3Xf0de" +
                "uxct00VMzAIpj1Ui7PGZR48rKNs7a1jb7rMnjvHp4QNOLLaIlg6/vOSVXJqGnU/b1XrRL1KHDVL/X14l" +
                "exlLUhxKBIBDVyYLJY3gcKAck4eqfch3hVtzcyL8EJ7HjoP2KnC/cTS3+uePlRbs7oFrdhrY36Omui1I" +
                "X6aqXP2nM9OlMhItFknEfBabmfqY2x20/3M0o+8s5BzKQ47MMl3X0CXexiV10+Sq8YTvQevx7Rgzx2H0" +
                "jiGbzw/5dWvIf6iW2+Xdhep/OHG8tMpNowWzb+ICSt9xXtiplDXFKB1u9azI1cRwqYUncG9kJ72nAuSN" +
                "pOLWUm0rVlySleqTwT2xwObSvs2QX7ntsdKJO8RygEXxM41tax9jCzOHWpeZSEu2aOg4HfsYyRccGimY" +
                "XPW4gkG8vrgi8NKRtqQ94JJrcr+aDtwIi4cWyrD5vdBA7fGHLzbUArdoAtjEcfA3i2LrLvBxU+h18hUM" +
                "hxpi7m3/ibX3AXd5ER7UpNy4bttD/60EFgDboq24rVmNjhJF0MnOMjXZysUPiXheQZhTyDc9Eq9WXBeB" +
                "0tX2Kcy/+jDV38y/8Snw/ki+bzZd4Iq9vygdKSV2yiPL35buftcjKLahtFNLtW6XZbl093GD+2WdDnnh" +
                "Ox4E755PG7NWtWvE8NQN2xYRUH1+S7lMu0s8tSFOUrFnZImviHCNo9e9zzcsZcxTGTB0V8mVJDtJv7E7" +
                "Yvus7r5ugngnW20IUUUeRVa+MuAIrMo1mhZo5DFzual1+CIF9jy28bM3gbLhvnxXOIWXI7+EKqyQirwq" +
                "t2i8mdqzbwQh+gcO+6wvpH2MAfavVISBoSItl13RK950iAVVRqiOd2EDShjayvf8tLbli9x8365utbT+" +
                "JH0921LIFWIpHZN5unH+ekFggc67ec/dWYm8d37DGsW080VkLvgdW1I43KXfvIygKSezSyXSM9FT1UY5" +
                "2+Fj8DI7+7yc6hGs+6/68CczIqtiMKssB+qa2ApXHaV3SDHvDiPXTJXc1N3avEciWoLIw14obpRBfyMv" +
                "ceQ8UCn28ILifSTX89zlYOS++ZVT0l6TVmgtKP3hkvUZAV53Bw7xF65nOzVC4G+RKI7r+x333oJ4/y7u" +
                "WoRdcT8PEI43Q9lnEFLfFHvkbPLauB2J20zoR2PsisNibQ89SBR6St2gbcUbQ7f0HfDfFI+eyBIpeNoZ" +
                "urNEsx6OUu0RbkPymbm/yZ3XmcmEOltqwX2rK1gY5YA6xW5W0tOua3rMfa9HQ96h3Ag/Cs3GzRYXJLY7" +
                "73/1hAaOeaBXZpB7P5HtpKXIiE5absRFSuewBigROXWP6QA+2OEMeLOxxUyey+C+up4TU1XcrEyP5Wsl" +
                "XbuTOgzO7AkDJtyH0yAuC9/AYRMaUOddkx6SuC3Nf6OmizAG0MVYuudUB5vQ1N0TH+/K+GxJdJPeZafZ" +
                "k2H2nv55PMw+oA6y58vp568vL96OP5z2n7xH12DnyTt08skTp0mZZGED/1ExwZ1WhlGA317D+6/L9C/+" +
                "CGv6r4X0SjoMAIbqPmKanV8e8zzIN8+ItTpfNDMhH5/cjoxFlv633boJ7/8Fnyn8dT9QAAA=";
                
    }
}
