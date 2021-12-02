/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
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
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new MotionPlanRequest(ref b);
        
        MotionPlanRequest IDeserializable<MotionPlanRequest>.RosDeserialize(ref Buffer b) => new MotionPlanRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            WorkspaceParameters.RosSerialize(ref b);
            StartState.RosSerialize(ref b);
            b.SerializeArray(GoalConstraints);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionPlanRequest";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c3bec13a525a6ae66e0fc57b768fdca6";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cbXPbRpL+zl+BWn+QtFFox/Zu7WnLHxxL2TgVW15LmzeXiwUSQwoRiGEAUBRzdf/9" +
                "nqd73gBS56RupburvWzVWgBmenp6+r17+Ci7vCrbrDXNTTkz2czWXV7WbdZdmaww87Iuu9LW2dw2WZ41" +
                "5pe1abuss/J9aflt9ChbVXldG4yoC/lg191q3WVll60ae1MWph1h1Lu8yZemM00r4DhwY5vrdpVj4e4q" +
                "7+SVh9Ve2XVVyIgMCAHI6Hs/OoEUIExW4SUXu+jypivrRdZ2eWey9arAP+04ez3PZqbhHrOfbVl3rV9o" +
                "KpvnOo0pAADorGwru28zC8yIcV4LjrN105i6y2xt2mO+aU0yWAECBEC2pssw1Yze26ntLgQXYNR0E8GL" +
                "mF5y07Zty2llsoXNK0U5EmlpC1OR5iQN346zs3x2lZnKLAWLOaBwYN40+VZPDdNzBdaYBbAayzLyAqeN" +
                "2aW5kW2Wc90REO+aXAiiZw1yduVsXeWNAwHwwD3vynZeYuqrOOPDR4E8SYBwY2+tIz5ImddbbBJfsryy" +
                "OBU56by7wtHq340t1jNTOJ7SrW7KqspuSlsRiFI5xfOwI+fmq1VVYrugT47hsggOpbZd9vMavLrJt/ru" +
                "KEVZFh8iTBKlCxCxxrTrShgJb382s84222xJwEqK7egyvE/hx9H7VqnBqTi3RIoC32Mf69YIo9ZWB1I+" +
                "V2ZWku7HZE2ecQ60hnOFYOA6AChGWJNou2+Tsti3+KKx6xUfnDAA2OaqBHMJbT1c/GlXpsGG64WHKzMn" +
                "hBXgrpdTDCbkcskT8SA4H9sSCVtCMZhinF1c2QZMDjLaau3UiEe/MSt+LMYj4PTsKQFPBBWsO8m7zixX" +
                "kZTL/LZcrpdZvrRrEQZZfR9lySxVZTfgskSYskOwYGtwSAVYZF7ZvPvzcz8wLkugoldmecXtz3MergqL" +
                "XXEdTNkCdbCxcEuCmtA2uzGVnUFFUDRr0TCzGUSYVAWDjLPspUPuJq+gZUXcgNrhk+MvPuLr5V0At3vA" +
                "RX7xAtZQ/zhVQshLcjUlB5pvy6MCgcDrmNaVNwbg3AaxMjjRlKIAORG6nUqSQMuGuJZF1uT1Ahgflkse" +
                "XF531ZZqsWypYOpZtYYFwMGKOoZCBPWfjJ8cieZ06wiP6ydneYS/hRQ80y/GTwQW1LsS+rAcm/HxXRQB" +
                "wP6XlDhH43DMGDTxkyatHu1EMeqNSacPx41GL/7J/43eXPztBGd1A7JPlu2ifbzH8gn7g2qQtDZfJJY7" +
                "F0KCZtEgUiHM11D9lDJ8FX0GuSepbyB/kJfDPJva2yOyi1cAnmn6ckO0xn7xMoC2NQ5lc2WcuImI8BTs" +
                "tIRZozGBDplnzlIp4Bzm21TVmHJ1KlZLOYJoc3Bj5jCdNbwDbwqBIjbaQJrb0dcmh62GdeU/QRuUUBY4" +
                "dR3ktRymHUO7wNw4Fvd8pniIQRZdaVszWhgLqkFvC+W/EyX+jIAnCnSoeP75S4Hh/FL3w1ptV+iSSkN1" +
                "l+oib2B/TZfDVcqF5FflAmL/eQVfQbyS5Qo8IF+77QreVMIGCwN8RTJpfLhp6Prlui5nYrxpENL5Isx9" +
                "L2NmbVOUNYcLBxC6sCt8TvLA69MTsc5mBnsBhLZULY3JW5Lz9Wk2Wqu1wITRo8uN/ZzaYkGL6hdXJxPI" +
                "mtsVTod45u0J1vijbm4M2CfeFmSH8m6Cx/YowyJAAXYJkkGD8W7bXUHNigDlTZnTdwNgaIYKUA846eAo" +
                "gUy0T2B5a+vBK8S4xm8BWwe43NPncEcLMUbtegEC0tqqu+10OpQCFDy0X1VOm7zZjsQyypKjR1+JlImN" +
                "lhOhlLctNCEOoBAO9pZeTkP8h/vhxr1S4FkLvgCOCpugYruRb+SceWOwE2rEMZnktRyr6KClgf0h/4WZ" +
                "9JfKhobN+cHQK7Yxx4xPCgvLBF8RMJb5Nf110Fh8SbiVsKz04+q2UqOK15hyaMYLWB7RdTKKNBKOFhko" +
                "Z1lTLmAUZSYWWobJeeY2B8s3f6rujuCsi+HAAKSxnbNSNLxbu4YHiz3gj8aJnnhSHi9hkc7aY8qdA9En" +
                "6DsxgN5MwEJ0EHroXG/fbsNf2/DXrw9g02JEdKcpK+tIv3wKz6OvSTueIdwAjSBisDbHMbXeo2VMCiG5" +
                "bkc8W9vo4t/wo4ZjMi6GY/KlVVWxRDh1ld8Eb8lkp+dfaRwU/CuJ7lLQbzgW45IlZPqksPPJYLGXXYdY" +
                "DFBmtqrKlvu0U8YNUAe5/4YDb3Ggsgs66IEGRyM//5Wffi6zEZD52bAk7tPEQea6X1X5AtQtqJ3JvmBm" +
                "F+HSD5uBj6PPLk4XRIlqARabwjSf75g4wVDDZJmfmIalbeEQZsE5VNPidZcErn6rU1vQPz70+GAgAzla" +
                "k8pAxe0ZjIVw4LljrZMTWE5zcpKE21ORds0AcEGIsCDfJSx3NJpay9B4ws3dm9ndy4AJpfIgAsJ+V7aC" +
                "ofBCDx961pQUfRBBA2vZuPP2oFdgKEV2Ghw56KMCQP3oPJYwyTB9oI7xYWPoAfJ9A/NRthS3GfxALCwu" +
                "GW0JbSRMWSpm3t/xUPJCnO6j4zg0euPDoY9bGfwYhpXiGaeYOc6qC3ZaY/sQwzgAb5ec/RZuPDcmqRD9" +
                "Qm+0LkEFclsBza8SCltIfSChqhJCfcYgxBrpi3HEutH9VaguG7OBnIPhwV6wr2MGaztj1KlBUigcoygP" +
                "FSM/lUvICYr5xZq6jaECbMtWPeY8Kh/J7yC3wRUoQ3KM9KD6FBZkxCymOR+67rRwMsmNx97JYT5+HWff" +
                "06jRvqm9cSpUdlFbAHTnM3AUCGt5LLZqhqAakspdx+PUvBSj9q3jRlJPd6Nnm+zdpeOUcp5ObfkrdD22" +
                "DEIqnERqJOMo4QdWYfrE80BAMyGOJD880vTd6RjQzZQThFkcxhXqBEGjCv84A4lHLwjJK8/wySulwEMo" +
                "lF2zg1299z6Q2lAcu+oAco87XKFFYLHCLOBZCePRxSoszpXJAGgUu/HqGiRZz7o1kwHzLK6njKyuGEiP" +
                "qJLZYCb/vHzS42+3cEAkaqL1EFdYXKPgLumchemi/DO7EdKG11BKoqSy2RW8hHH2FUXhFk5tBR7JJf6E" +
                "qXDKAhYLy/7j/elXotOe0YAfIkRDgmKbb+hKa9Iafrl+JAeTy5IQIcVOCYl/mhJQdC4luvddvEmO8NAg" +
                "0DfIOlMZ5bNrbriHw/+rsYdVYxtmFa5+sxrzw/8vqbG7tJg6oJzeDtIPl56FMSqw886gDewSB/Dfwbfv" +
                "hUz4qPR6mHAxYL0nYBSrHtTK1HQbA77oNnYn19AOYsrRyCdjkvhv9Pc1JjQ1FYCP0h5mk3HhfWEx9HrD" +
                "MF9VU28jfPolYk06fDLy839tHugAhZPctrzWbaPz2N/PtLHXOEPwMGPxljER4wLqYeSgJY8kucZxOEA3" +
                "JD67cQ+zO5WJPaeGo9DjiZs7hj1nzpb6iBukO/nbtijA4qOGAg+Rl74j/nQ6e/A21HdCBCcKDKnG8taH" +
                "K5otBakY7PoEFP/2lSahY69KjSz8mkXTqxwpSSETc6uxfLqDm/rjaXjNYbLgI6RcAE/kiMfFvQpIlOI0" +
                "fB4js6O1Clh/5m9cUP1oHzwfYmsQEraRFLq4AGupfUIpUJ9odgGeJDFinTys5xMGTEskefrOroXxsLCv" +
                "pnweEFM0JLGPDGqxTUrfyYRoNQTYRNMo9PFiEV2xkuAIjhcruMO8Lk5DfEU9D6bpdJOSolsSRtgKQLvs" +
                "GpSaAdnhPmvKb1bB3WaeDVVHEQQHxRWR/Bq1mdH+okTL1RrUbCQ97KoObmEeH2D63E1SqY2ReSzqghjE" +
                "buKW8IeyMciNB49lcBhIS82z69puQnTgxj+ETO7K4kvnBkiOsBDShKSO9+lFZvbXVMDxbpuOgIfCPQIL" +
                "p/cGa79GLsoJa6wz+3NGqUCZgpmMaQ5H2zrqBOnRfyeMkRfacaJ70S1cEgLBxCK9z5Y5bStF1V1vzsus" +
                "TkO5UjUDJWVfiSZN6nkCXFgWOP0yM6ZIlkDwBsQSjaN4yqh3/hOjwzhsmI1te9/JV9oe88a0V32ofIOx" +
                "S/2wFw6/RRBfUjh80Z95NNROnFeQthBkU5dJlWE+RlEnAptfq6ThxIpCzgLalUscpbi941RuRFa6Y5P8" +
                "FrF7WTDWSxhDqR7qhZI3lKx3Mgg8ig6QdQs/2NzCUyD6yOCpMRWFMx5Nt/DjXp6evnjCZd6LUu2tNG8s" +
                "w0o43fVN2dha2maYH4KGQCkZ5a0GhRcVBUn7dhBmhZAU0osjXen92Zvz785efCF7Wq2opxjBem52Ma9T" +
                "rIK0Cw8+tVdfjNBJfp84hbjJd+/O3p6+eOqUcFxz/3KyCnphzMZxvjtqqYqgfQZhlTs3H8ZIZwtGVGbe" +
                "aYjCEBnaDG0apBVI6zVG1KbITIKShaIotHlGBM+1ZURrH4CJRzqgfqDrKLk/H/rTSmX06Hf/l51/+c3Z" +
                "q0sWHH//ZPcfifPqv65xiNKU7N9cDJ5TZFBjTFQwhIFX0A6q/J1daNo8hI6awgWfMEvecyquTcjLpiuc" +
                "yBudH3OoUnITdoHGqrNi6pU9oHiAxTRFxRlYyZ18c3H+9jE7f1xC5ceXb77NFADyqIGFoWaDACS1OCpq" +
                "T5WYNFKj7g0KGuLEa2D2defQRY4koLf2Gv7KtTnJ/vDvB6TwwcnBK3o2p18eHGcHjbUd3lx13erk8WOE" +
                "H3kFancH//EH3SL7cYieZnNqpxn19Jx3w8NJqEDPsewOMIm9lZCCa2NcFXpeQVTRG4EQx5mnffzKYoYS" +
                "0dcWT79U3hAg3BXl3q2seRAyl+tPdFkxqXEzS+Y2K+l8AXOSBQLIO5IA74YkOPnTv/3luY6g6dVSKsbt" +
                "YnzgVrr4+7coX8BFYA0jnFNv4Ytfqq/9CIUtS2UHm0X77M/6hhWjk+xPz589lUeMbjgA7rPduBEw++gB" +
                "LQav6aFwI34BX/zSr2iDWlf8LuXTzq4OPEODte8rV3uXtxCbX6R3pF2R046z2RautThtYDeTuWyTj3LA" +
                "Fr48A7byWSZ4OFPvAgAYFT5NukiiOs5PjvE/pADYK/GX7MvzH2DG9O+Ld1+fvT+DadHHVz9++/rt6dl7" +
                "qHL34vzt2YvnXtq9fhIrQ5zcKPXSvEpAdQNhhavJxqExPR5HhK4alO+JfjohGXaiiT9pqWQp0Te7cizJ" +
                "des11UGcc6DGTeqeLibExgVVjR5+OM5+1FzuTynOJLIETKZewFl0GA11EMOmsD8QfRxpO/kBHkl8+jHQ" +
                "mk8/0YonKCn9HVaSB+SxU23iX1fcgvZUPKHRqIpdi2pelGui4MIc5SCPh8KdvH95+vofF/SQkjX9IQtM" +
                "HrD29ShVlHUk/SDNFd49lEy8W+qnDE1Q7BEMXRU9uJOvz17/7evL7JCw3cNR3JOWbhOKxz1d9cIrLwvZ" +
                "IWXhSNejnvPr6O7cOvqQrHPXKuy28LTT43PByf41YbI1GeA/YX7084cyyax/2UgIrM3XKLBEHhKacj6D" +
                "Te3GO3Y1js8cUb2QDogZWGqwefqjUVJ3BkfCcOD9qLjdIECCz2anCEVndJj7ktOie6DftchMaifJTaTd" +
                "tZslFN+SlGwy7qE2CFR8aq+XkkqL5MiF+OJX3O4nUrD3b4IYWnrDk6DKcJIKAtG1Ch960OoFPIi/JhrW" +
                "9SdLd6Z0UofOG+yRZS44O+2HjyOucekASE3BweICSeLOz/CxFzw/15oq2OyhuZRaddIDkcpvYw/J/Lbg" +
                "5QWktBPxwzPF09xOJA/4INhKXL638quVUUiaxvcx/g9Jgvw2+4zpv8+y2a/4vyJ7kUlEnWcnL8DgZv7h" +
                "yUdmFMPjF3ychcenfCzC47OPodTw4flHeXdfBPhEDm+Q19pbDBtM8Yymd0f+h/D2GkZ6XJL7J6pRYvuK" +
                "a88PgvjhOLltgIfeTYOPPCXbH61tCh9DzjxZS02ZuWXbLfMQzg8NaZH+bYVQ6mSvC4oW7aBmKTpisMsx" +
                "tj7a01nR7rZWsN8rvuxta7fpolj79ANs/4Q5IDbeNQ+UhI13fu5u1PdqNr2oQtlMbgx5gqf3knySJlzG" +
                "cle2pEbuL9qEfL5c0fAVYO1iUWaPOAZR6F1VeufOoTfOH05/6Hm0vb3RiU3uT/gO6VuNfnvjb8Lr/vD7" +
                "P7ABRTRDow97THqoVU01QSHpbV8SkRDM0zuqGG/25LOb6y/i0cH+EJZAnQbZNhSeZ2YyBeNvjuPynyXf" +
                "kC66MR+D3zDsSBqO3PNeoEse0xUo4tWmWK+JJ5EdIiNk2Q2HxClOFVGn7+rU/IY2gKbsm71C5Ub9hF9N" +
                "YyW+Q3UPshAbQo+GVZL7P+5d3r5TTN19pJ7RD8cRt7rbc6EBrXYrSzVwSE3xoJi221fo1JTQfM7qH68G" +
                "hJqiNCwcRVWdN8j6OZNgk3EbNgwmKWdMuOPCiYJAop6sqkt6BNztoDsxz0a+5vCdjoyDJnpt9H8fdz0A" +
                "e/WJEjM8uScoDvTZqZLGlXfbOwtNu46xPxU2BQtctrnlTNva+ui31qXcCbuSWNr3OghqmQaE6f/NhazX" +
                "sXIk4UGAduxKWaEMEZwI1k+KUDoPOaxwN22B44Yb0+7ZYr9Idvem3NKf3FBaUbt/RtlrOH+PKuo3/3xa" +
                "HblaTW+Wy0okmiqejDBWbCEa0Oyu8Pu/qfp8J58kQj6X8CozTUOV4U1XUs+Ml3ancpnYTG4nnDgJg3dH" +
                "bD854tfhiH9FPbbPTQs1+LDfeAtUujPxim5Q9OW0wbco25kWHdXeHO10ioRrf8L9Ml56EHu5uTwAlq4V" +
                "+F9aEkN2jcVSLRSBGRcsBrkksrRJE/Abh5wol4gfVyMoxL4YuhaGjBIRqlJ2qp3PqoPjdKJB6G9x6CdJ" +
                "QsjTRwC8Pb8EcG37WgIDXh1Nriljt8IfbexTDJiHJmVPOv68QaNgkQn2UBNvwN/cie2xpMVNiUJwTBMr" +
                "VcDUu86QaOhD6aUCDVAk2bqG1iP/8wLC+C1KR9lq3Yi6HAex7+VU5RjFgsUf2vA8wuiEZIR4R+dUnREF" +
                "kurzFOBffWTpr7WLdfF9LL2leYcLjeI19Ki7bxxOSK2Qp5K/ddz/6Yugwo71yoVW1PaYjgt3vTU4U8h+" +
                "MtXuWw+U3J4zO7vJG9cO4c80oKxMnw9ZLOEs7d1a4XDAPdLyokmdJX9nQyoSg7Z4ua2oY57rAOnr14s2" +
                "tSgz8TAUo2Rh99sfjFWy1RY0KiUHIIjo7Xx3rHmFvmVyb/ZM+Bq1AP/7DUR4Iov2zjNcNe9LovJuZBFf" +
                "HtUDgo/kFoz3OwfGCwCi2XdkF9WgPVrjPZcUwrhQJ9Ybo+y+7nqHRJ0FGscLpZ4YAkzslupDpDYxW26v" +
                "NWujPTdJQ82uzDFRoAVdWCCp50lfeLsGHe9mN3cFxLPbGevzLJgvriI/0ZXYkTjltwGLeZlgQ0zWLrEF" +
                "5vvMLOflUt8Etus5yCp7O6qcjlF6+1+5AcxEljhWNJODdA1W4pVB7drJhVuF7nqoM9qBfag78rEdB24y" +
                "SqKSSaFdlR+ccM6k1mJkPfUrkotu7mYtU9PyyWlipy+hckEQv7Nkddm9V9CBMfxl5fleBeCZWkVIwvBh" +
                "57o3Et5jizirbOfSUENi8wvv3QnHKETfYfrEWVw0jCg+6gGD9GwzXUkca9ojD5FFGBTE2TrijZ1beT/4" +
                "d+Xjp7pCCh14sStKVShgJ8oi3CmUHbO1yG3WmcHkYHb0gPulqmBFcgfTKXD0E0ljuGnwmp2kT44FP71I" +
                "/SR07XY755+Y5mLw2yAYN9HMf5BzP0usYIvYBpvk7W07mCESXzH26W/RFxJ27bw3Djs85JmLHqnr/LA1" +
                "el0VMovQA+OS+gLOsCnbJUzHvTCuCr8QIzYyUM05HQPySCuY/wmXHqlkekqrFN1U2drQFT0QGO+h+MxG" +
                "cH5+QCHiKWr0+OeLY9ScWZZwxe2ztxfn71FLH7yIpXb34ofQ2OAUppxTWPtfxrm/05AIAfjs9bj/6ZXh" +
                "VZlMmNH/pMYgyBQAaovuB/80ONn7w1ue9eSuFjiq93te0rolefHkPmGsdQx+06yXgf5PPW4OfChPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
