/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupResult : IDeserializable<MoveGroupResult>, IResult<MoveGroupActionResult>
    {
        // An error code reflecting what went wrong
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public MoveitMsgs.RobotState TrajectoryStart;
        // The trajectory that moved group produced for execution
        [DataMember (Name = "planned_trajectory")] public MoveitMsgs.RobotTrajectory PlannedTrajectory;
        // The trace of the trajectory recorded during execution
        [DataMember (Name = "executed_trajectory")] public MoveitMsgs.RobotTrajectory ExecutedTrajectory;
        // The amount of time it took to complete the motion plan
        [DataMember (Name = "planning_time")] public double PlanningTime;
    
        /// Constructor for empty message.
        public MoveGroupResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new MoveitMsgs.RobotState();
            PlannedTrajectory = new MoveitMsgs.RobotTrajectory();
            ExecutedTrajectory = new MoveitMsgs.RobotTrajectory();
        }
        
        /// Explicit constructor.
        public MoveGroupResult(MoveItErrorCodes ErrorCode, MoveitMsgs.RobotState TrajectoryStart, MoveitMsgs.RobotTrajectory PlannedTrajectory, MoveitMsgs.RobotTrajectory ExecutedTrajectory, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.TrajectoryStart = TrajectoryStart;
            this.PlannedTrajectory = PlannedTrajectory;
            this.ExecutedTrajectory = ExecutedTrajectory;
            this.PlanningTime = PlanningTime;
        }
        
        /// Constructor with buffer.
        internal MoveGroupResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            TrajectoryStart = new MoveitMsgs.RobotState(ref b);
            PlannedTrajectory = new MoveitMsgs.RobotTrajectory(ref b);
            ExecutedTrajectory = new MoveitMsgs.RobotTrajectory(ref b);
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupResult(ref b);
        
        MoveGroupResult IDeserializable<MoveGroupResult>.RosDeserialize(ref Buffer b) => new MoveGroupResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            PlannedTrajectory.RosSerialize(ref b);
            ExecutedTrajectory.RosSerialize(ref b);
            b.Serialize(PlanningTime);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (PlannedTrajectory is null) throw new System.NullReferenceException(nameof(PlannedTrajectory));
            PlannedTrajectory.RosValidate();
            if (ExecutedTrajectory is null) throw new System.NullReferenceException(nameof(ExecutedTrajectory));
            ExecutedTrajectory.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += TrajectoryStart.RosMessageLength;
                size += PlannedTrajectory.RosMessageLength;
                size += ExecutedTrajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "34098589d402fee7ae9c3fd413e5a6c6";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/byLX+rl8xqIHa7trKxs6mW1/4g2LLiXZtySvJ6WaDgKDIkcSa4mg5lGVvcf/7" +
                "fc45MyRFKy9Fr11c4KbFRiTPnDnvbzNpta7Mne4V3Tw3+ZmJtVWafgYRfrdarQW+JkWwsDP7YmgmphgV" +
                "YaFVkYf/0FFh8ofAFmFebIMclzBqmYZZpuOgWvaVBfpeR6uiuWKamrB4/UrQJdksKJIFiDz9X/7Tuhq9" +
                "PVF18poyaiVZcXyk7sK01dpR+JiHaaomeh7eJSZ3X0c3Z2fd0ej0pXu+6PQub4bd07/Rn5Z7eX3Z6fd7" +
                "/bcBfe2enx566F7/feeydx5cDca9QT8guNPDI/ex9jJwgJ1x9zx48yHo9t/3hoP+Vbc/Ds7edfpvu6eH" +
                "x27Z2aA/Hg4uy71eufc3/c6by24wHgSdX256w24w6vZHg2EApJ3Twx8c1Lh3hS0GN+PTw9ee+mG3e3WN" +
                "nU8P/0qS8IpRf1a3SaYXYZFEVuX695W2hRiWl91o3BmOA/x33AULwdng8rI3AlOQwPdbQN73Bpf4exRc" +
                "d8bvAN0fjYedXn88AvxLL8y3g85lE9lR/duXsBzXAWuf/CLSzatWQztvh4Ob66DfuYKUX/7Q/NjABJDX" +
                "DZDh4M3AsYivf218vez1f/bIf2x8G7z5qXs29l9hTzvKPthCLzbFfDEEQAAC+qOLwfAq8EZ4eOQNrRQW" +
                "zKV79jPZIuzhPeDIKADoJVijlf7L37zQnMH0+heD8huEtVM3gw26+oOg93MwGlzekCXDRF8+hx9XEQyk" +
                "jeeJVQttbTjTKjJZESaZVUk2NTlRbDIVTsyqUMVcq5wWKoS6Qh+opK3b/HZpbEKAVpmpSgqr/mHAnVVh" +
                "Fqs0yW5ty+rMIpjy5j/RRwmfDEeRE4SAEv5igTIs1CJ8UAgjWi1WaZEsU63OBxcqzLWySx0l00THaq5z" +
                "vYH6imABV9uClwexmQaNzTpFEUZzYIlMmiaW+DQTirFW7YX+W2GUNQvNXChAlDLYb/n1Z375gFd//KT8" +
                "auQO9ylwmGnfizScQbpxEkG4CBHruQbWHKihBhvpTCv8wMYT/MgKnS9zjQygQshTxcl0qtZJMUcwITkU" +
                "JYWGkfB6r1NSq7FF+qCSxdLkRZgVCkqFWLMYDM2Ym5LViYkTZL09Tw8AM8P4o1SH+TZgivlTUCWmdXIS" +
                "mVyfnNTS40RjP61Wy1h4TQohvqiZ3H5rYkwKYgNi7qmsf7sB1iQVli7A5jc3aWwVyA5JAkh1UZ5AISQE" +
                "tiBh3OqCfiAtI7Kz7+RQOeQjDtBWrR3eorZIQ4DyWe3l+s6kSO8k62WeWA4Q+0RNrKcIGJDzwwkQqL9s" +
                "uBlviWePJYwJwWL/oAK906mJkuLhMegLy8Av7D67Z7VET6GrQrgneSyXKflYktUR9Be0ur/fZsa6FS9Y" +
                "scoSSIGsLdZZIR46wRMcKgtRn4gg5jqMyVCdE5O3Q6yoX2Bc8IUEGKv9WGpWreHnMHiYV6zjtuqgxmjC" +
                "ADsoNXAer0YOHuJGfiltwRoEObynsNEMgDaxoFks2wefMM/DB3vAO5APsRqXqPg2JczEkNqZ15kJybCJ" +
                "ikV4q2WRgwfvZGFmSRoN07b6+1xnSrdnbfVgVrkPocxFZoDQ6Se0FprFLrF3Jb04oCUqCjMFTyWuK3Uy" +
                "3UovlsWDs0aSnnAjuq3xbudmlSKsehwsJ5v8gVgPliFIwVPzGoIyGXS+xi5gs7SBksyacMgKSqIh6Bx6" +
                "KShfswbbKGzfiXGIjbRatsgRNRBR2X5c1YtH7wi1V97ga69EAk8WUIpYoonQDDYQUbI4zGOIswg5cnCw" +
                "TWaIpoepBoXE6WIJzUlceVgS15UwZ4jdVEA/qJWV3BOZxQIiRaYQe91YLyYfshUm0SpFjI4M7DzJCHya" +
                "Q2aEnQRMhWcWadU7P2EDp6YiAUHwzizKdWgpOPfOVWslVQkWtHbGa3NICWhGyclvXsYHfY+8ZInO0FKM" +
                "+osw1wZuirbYBda9x+8CPCLcYBOQoJcGTrAHyq8firlLqXdhnoQTuBkQR5AAsO7Sot39GmYi+wSmkBmP" +
                "XjBWe3wL2qzESzwdlrnQrmYQIACXublDBOPYxXaKMAjjTZNJHqL74lDFW7Z2LkjG4kKsEQqbm+7pTFi0" +
                "ESTxc6S3x0UQmB1qUhcYkYoOQUgyEpmoCzXsmWXAi/Us14i9gJziR2wQZYBnivxm1r54AHerqFghOgOs" +
                "2k/Cak/yiLWrBVkz2U3oswXZrSvTOQ7YJSsUJg+/yMPMUvEpa2a6qLIR0IapcbuXNbWK5qhZ2+qCAvM9" +
                "VJMiYoXchUGnLnWhfsK2N8PzC86wx1RO7t0jdOL/4ZoMgvIhbMdq+UjxlGJezdDr1Ikg8VeeAIuspfyy" +
                "8R1YBcJjg+GiSybzmITRLTG8QcP/J9XnTarrHHFx/s1J1YP/X0qqn8up0g7RctuaabQQBYZYHEDG3oQB" +
                "VZrzI6A1qiQCoL8b3/7OYsJHkddTBb3PUO0lmfuQ52rMMqxMdLHWsItibR5lTNYfBTxUqGGESNZ6z2O3" +
                "Y1mfilf/ssKCPKMAkBsJqc/DpCNmC4shSiD61qBflYGYLWqhqQmETZUruakkmwEPbXKwnHs2dPeFig3k" +
                "gSaQoxhcjbIM19QUjmGOdZnQayzZI2c7oKY2EyhKFVyrcHWDWJ0nsyRuhlEO/I65A1VMj2DScCmmWTaD" +
                "CoHES3u/rXpTdtA1McTO7Zs1atMcXZz8C2MOqKJyKDYFes1O5H0Vg48CftKuRqz35a+ytFR/PIuqKxvb" +
                "pm2k8JzqEhHfhs7p6ffKQEnIX2XI/1o/k69y0HBs+QRrq651k59Jbm5hTlAUmZilYQwNJCjlhtmMC19K" +
                "Ggh23lcdSPXs4J6HOwl/W7QGVYh6KuYO4FQgnlMPMUhF/bexyMiqR5lBPMfw8DODL5eeG2/Fjye10RHn" +
                "KvRGyb2fk5DTcsqkKZuvmOk3ZyfMzFiO5WSSB1GoOlEG2HmIHorFhJ4Qv6icpe+PaJO4UJ/rERhvuINI" +
                "AnzsR6Qu4pVRZsZNBNsIWDSc0wUKPQpLbpq3sw2fn+1JB1GyUQpCNohbTUEJ0pafF8lkiaenrrCsz9/8" +
                "pJLmoaDIrB0NZsWGh40xQQpRy6vDkjAhg0ryFC1f/CCjDhQFQqlbUBUIjCyQ+S1lkamKULlCBY4qnsqg" +
                "xo5kSrTRiEIb3BaIPij7CJOceRaEo2QFqF3SQFDTEDv6dslkUYo+n9JHblbsCA7L/oEfh/EemY4ofOO0" +
                "jHbLNco1WkYdHhVSsjGpDzj90Lh2bleNBGvnbjFaeDDvtvBKWWs082Vx2lAGMuZU3WZmXY4lHPxz+ORj" +
                "X+y4io9TX8yiKafJvn1jn2kWiMIqLN6x6QS4x9bDuKA9OQjc985Kja2s83rGbEOMgrLyJET2NU46pffI" +
                "3wEN52YZz3OEF2FhTBgIjcdcjeldtKXh4ZbC3fusLEvYFgkSltIcnHM5XjtN8AIYmRT8+20ims0uQOAd" +
                "hMURR+hkqGv/icZSFVizyLAb38muAISdrrSdb2KlN4BdyIeteOhbheINOQcpgdpdGuBj2OOqAltxd6Am" +
                "7giHwXw7KkUEmF+Jp0Fjccy6QHSlLfbrtF3TUmKEd/oMk/Stoq4TU1tfMwyRenlAygcWXMzVgGCjOD9e" +
                "WZR9+h6VApGP2lKSKQecdmvygJK9c35++j1tM+SgurHTNDc0QUB/ld0luckWVOzSYBoR4gFSQhuOSZG4" +
                "Ap83FXBm27CJJN6XnYbdq8H7Ls6wiaflkuIU1azemt14wwVWJtp1gl/j1dfYssjzCS1UTF5fd/vnp0cu" +
                "CFd7bt+OdzlAVFw7y3eq5mJ/jyC83nzHuljhUBoQqZ4W0o3SNATRzJqUZAXR+ohRRVMciUCSsZDIsjkm" +
                "AgdLjDB9SQ+ceKQC1AMa//mpguLXg0pr51/+o+SQmSak//pi94eEc/blw1UOmnzsMOWE5wIZwhjNpKhb" +
                "RVXAEzmqGKFCndMYZCbndeWUQM6OYCd0PLdRVNzq8kCovsMJv5H11eENd5JsLohYmYonPtgDi0cYT+qk" +
                "uATLY7KfRoP+C4yv/ezsQ+fqUgkCHOCUJowwWzpArcWkQO2lUs0HJan7hNJWXa4a6NjnkdLZj3h2Y8wt" +
                "6pVbfaL+9M9dkvDuye4ZVTbnb3YP1G5uTIE386JYnrx4gfYjTCHtYve//yQsImvA2FEJ8uAuc5FRtOeq" +
                "G1JOTQpUOSbFLhYlKPbhBbdau7H5NIWrTpIULY5LT9vslU5RRYi+ZT5/I7bBSIgr8nu3s4y8yLhWkBOF" +
                "OBmA8lCeBqKOWT5HZDQnqhQAvyMR4F1TBCc//O3HVwJBqVcmBIB7TPGu22n0yyXOTVEi0OFpqaeNjUe/" +
                "p+88hODmrdTuemaPX8sbOqo+UT+8Oj7iR0DnBIDy2awdBNL+GnObxmuqUIgRv4E/dZevCxOvUvrOU4HC" +
                "LHe9QcO0n2os/7lqARSdi5tODOa/dkmWdqCiB5TWXLTB3LRyg0Xf5cAs/LkwzMoPFFHhTHwJAGQU8Cml" +
                "sydK4fz9Af6HEQAd7vyo3gx+RRqT36Prd11cgjlyj2cfcNPmvDtEKHcvBv3uKd9fGdfiE2cZoslBSZXm" +
                "QwLOR9BWuMsgFWh1LldB+DU0lSLy6wtqYCcy46V2he8wiBAkVZO47n2k2q3W7Epy4wsXricE40yqdA+/" +
                "HqgPMrb/rU4zCZkbJp3NUCw6ipoxiNqmkj8IvV3JNvgVFUn19KGUNT39Rlm8RpLI31HFwy5SO4VN/O1O" +
                "1RE9hU5ENArFwjdO8pMVkeDaHLEgT4fgDYad897NiCqk2p5eyYyTFCwHkSIVMR0eP/DM0JeHfOjitvpN" +
                "hSg42qoaFm7gDd51e2/fjdUe4XYP+xVPcmekJvGKp/lGe+V9Qe2RL+zLfhTn/D7CndtHHmr7fG4XGiJ6" +
                "2Yn6XHOyfU+kbBkG+E90u6+s85s+SQc8Sc4tMM9Scca4rGyIZUrrqdkke18tD9xx1ndOqN5JG8IsTarB" +
                "PNWjlac+Aq4EQ4BPE+IeNwHcfOaPzhupGG3OvlhbVB7Id7ndQtKuDTcxwpYhbXnqX5u+1+Cei0GQ4kd7" +
                "GyOp+u0czEL8OWfF7ldGsE+fgqi19ImnRiq1kxQg0F2L8+HQPJuhgvivWoTFFV/cXYVap3QbwNSu/IFH" +
                "OtFEsWM/fmrRHmOHgI+PHC7aoDa48yt874XKb0VFEl/fmT/qLCFLvuMhi55JVJ6NLSLzbKHKK4mSqxMf" +
                "j4VOfR/wHPBZqOW+fOshvxyCw9Okv6/6/3JIEN6r72j8952K/sB/YnWquKMO1ckpDFxPP37/iSaK5eNL" +
                "eozKxyN6jMvH40/lUcPHV5/43VMJ4CszvMZca+u5Z2OJNzR2XvsfottHGL5cV8G6iFLdm8Psj9q+0hE/" +
                "HvjzE3zFQxhFGIVKt20/kZbMJrTcj/pUzsxre0kq8//4oO3r0HIs4qIBZT8/daDpIF2yw6GFbRxPc4xo" +
                "cNkG660tV7rs4ztddNG0ernB1uPbXvHKjx+Q+wOaAfl/mPE0yvzCP9746pBZLLH4/IKNmzy1hc07zfV/" +
                "HPI0bH4jZZt3PF31yleU2XTcharGkbu7O6MlufA9Nb5ktcUuvXa3DK23lRzu/uYhJOVPmipce3KF099e" +
                "bd7s2fcuKBDVraMaCj7SwR20dBUTF+5eSK2WsS8qG36xabk77p6x8xb2H9d0uFcltponSSewsUiczoPR" +
                "SJz6xqYHMjKUl1vD4Ge0+Z8Jh18ixuukqVaaiXgTq18g26OiwqjX9G8H9r/tlkvLj33c2JTuVbhT6Sr+" +
                "edNE+Tmj+VP9ttJn7snUgtmjLWA5Ndv49/bZtLIvxMP/ARitSzXyNgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
