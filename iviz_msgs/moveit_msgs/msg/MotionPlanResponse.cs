/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MotionPlanResponse : IDeserializable<MotionPlanResponse>, IMessage
    {
        // The representation of a solution to a planning problem
        // The corresponding robot state
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
        // The group used for planning (usually the same as in the request)
        [DataMember (Name = "group_name")] public string GroupName;
        // A solution trajectory, if found
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory;
        // Planning time (seconds)
        [DataMember (Name = "planning_time")] public double PlanningTime;
        // Error code - encodes the overall reason for failure
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public MotionPlanResponse()
        {
            TrajectoryStart = new RobotState();
            GroupName = string.Empty;
            Trajectory = new RobotTrajectory();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public MotionPlanResponse(RobotState TrajectoryStart, string GroupName, RobotTrajectory Trajectory, double PlanningTime, MoveItErrorCodes ErrorCode)
        {
            this.TrajectoryStart = TrajectoryStart;
            this.GroupName = GroupName;
            this.Trajectory = Trajectory;
            this.PlanningTime = PlanningTime;
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        internal MotionPlanResponse(ref Buffer b)
        {
            TrajectoryStart = new RobotState(ref b);
            GroupName = b.DeserializeString();
            Trajectory = new RobotTrajectory(ref b);
            PlanningTime = b.Deserialize<double>();
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MotionPlanResponse(ref b);
        
        MotionPlanResponse IDeserializable<MotionPlanResponse>.RosDeserialize(ref Buffer b) => new MotionPlanResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            TrajectoryStart.RosSerialize(ref b);
            b.Serialize(GroupName);
            Trajectory.RosSerialize(ref b);
            b.Serialize(PlanningTime);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            Trajectory.RosValidate();
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += TrajectoryStart.RosMessageLength;
                size += BuiltIns.GetStringSize(GroupName);
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionPlanResponse";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e493d20ab41424c48f671e152c70fc74";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRhL+rl+xOANnq5WVJk7TVgd/UGw5UWtLriSnTYOAoMiVxJriqiRl2T3cf79n" +
                "ZnZJipabFHd2ccC5RUxyZ2fnfWdm13tqstAq1atUZzrJ/TwyiTIz5avMxGt+yw3eVrGfJFEyV6vUTGO9" +
                "bDT2eGZgUsxcmSSkQYyZXGVAoxsjeh7To8pT/1cd5Ca98zCW5m7yPDXrlVpnOlQzk5ZrHKyztR/HdyoH" +
                "UOYvtfIzFYESJvW3tc7yZiPLU4JlHF4CIMLarZBdLNpS0QwLrJNQiJoUIxUgmn3pCMgjrHmQ6QB8Zc3G" +
                "LDZ+/uplQaBH4zShl6agOzChVodKJ/SQMZXmRqfgANT6GWgh7mZ+FK9T3bjAWD/nmScMr+nRo7mNxvF/" +
                "+adxMX7TUUssGeXeMptnz0q1sBKiTC11lvlzUiX0HyUkadC7FFPwp2adi+BL3UKgbd3mryuTRQSYkdFE" +
                "eaZ+NVGCX34SqjhKrrMGzCoDf7z49zQoNsFwZA4gBJTwCMnOz9XSv1ML/0ar5TrOo1Ws1enwTPkpbGGl" +
                "g2gWwV4WGqKsor4gWMBVluDpXmhmXm2xbp77wQJYAhPHUcYmPyU7yNSB78Zg9pmBGRAXiuzJyaDZcPNP" +
                "3PQhz/7wUbnZUKcd8ixmWvcs9ueQbhgFEC7MbLPQwJoCNdSQBTrRCg9YeIqHJNcpvDIHKbB+X4XRbKY2" +
                "Ub6AUZEc8oJCw0h4vtMpqdVkOVwoWq5MmvtJzka4gF5itnBwU7A6NWEEQzxw9AAwMYw/iLWf7gLGQhwl" +
                "xLQ6HcQB3elUfH6qsZ5W61UovEa5EJ9XTK7ZmBoTg1iPmHss699tgBVJ+YULsPktTBxmCmT7JAF4aJBG" +
                "UAjHImZNwqPO6QGhA/GIfSeFyiEfcYC2auzZKFdM0hCgDKuDVN9QoMJ3xL00ysjdgiZRE+pZlJCc7zpA" +
                "oL7YcjNeEu8Oix8SgmWzVYLe6NgEUX53H/RZxsDPsia7ZzlFz6CrXLgneaxWMfmYjbgWwWBJswfNNjPW" +
                "K3nBjHUSQQpkbSE2EfHQKd7gUByZRRAL7YdkqNaJJVJyqMW6m0UEjOV6LLVMbeDnMHiYV6jDtuoiptZh" +
                "gB2UGjiPUyMHD3EjN5WWKHYTWlPYqAfALMpAs1i2Cz5+mvp3WYtXIB9iNa6wjW1LmIkhtcvWZnwybKJi" +
                "6V9rmWThwTtZmFmRRv24rX5a6ETp9ryt7sw6dSGUuUgMEFr9+FkGzWKV0LmSXrZoigr8RMFTietSnUy3" +
                "0stVfmetkaQn3IhuK7xnC7OOEVYdDpZTFv2OWA+WIUjBU/Ea3uYS6HyDVcBmYQMFmRXhkBUUREvSEORA" +
                "JhpsNxqNt2IcYiMNu7sjorL92A0Yr84RKp+cwVc+iQQeLaDkoUQToRlsIKIkoZ+GEGfuc+TgYBvNEU0P" +
                "Yw0KidPlCpqTuHK3Iq5LYc4RuylhuJNsiCKvWS4hUuwUYq9b88XkfbbCKFjHiNGBgZ1HCYHPUsiMsJOA" +
                "KV1KAq36px02cB0gPQJB8M4koPSEgnP/VDXW0NPRC5rQ2JtszCFtQHPanNziRXzQt5QtEp1+RjHqC2Gu" +
                "DdwUbTlpQvZE3zy8ItxgEZCgVwZOcADKL+/yhd1Sb/w08pFREuIAEgDWfZq036xgJrI7MIXEOPSCsVzj" +
                "c9AmBV7i6bDYC7P1HAIEIHLbG0Qwjl1spwiDMN44mqY+MkQOVbxkY++MZCwuxBqhsLntni5BZW14UfgU" +
                "29v9JAjMju4l97IjkYnaUMOeWQS8UM9TjdgLyBkeQoMoAzwz7G9m45IHcLcOciS0BFauJ2G1L/tIlq2X" +
                "ZM1kN77bLchus7ss18tK8cCBEn6R+klGyafMmeu83I2A1o+NXf0aWyRvmSpYIGdtqzMKzLdQTYyIJeUK" +
                "dGq3LqkerkanZ7zDHlE6eXCL0In//Q0ZBO2HsJ1MyyDFU4p5FUOvUieCxK80AhaZS/vL1jiwCoTDBsNF" +
                "VUDmMfWDa2J4i4b/b6pPu6luUsTFxWdvqg78f2lTfWhPlXKIpmeNuUYJkaMy5wAycSYMqMKc7wFtkCUR" +
                "AP2ujf3EYsKgyOuxgt4DVDtJFv0Mm2MWYWWq842GXeQbc2/HZP1RwEOG6geIZI133Bo4kvmxePWPa0xI" +
                "EwoAqZGQ+jRMWmJ2sOgjBaKxGv2qCMRsUUtNRSBsqpjJRSXZDHhok4OlXLOhus9VaCAPFIEcxeBqtMtw" +
                "Tk3hGOZYlQl9xpQDcrYWFbWJQNFWwbkKZzeI1Wk0j8J6GOXAb5lrqXz2AiYNl2KaZTGoEEictJtt1Z+x" +
                "g26IIXZuV6xRmWbp4s0/N6ZFGZVFsS3QS3Yi56tofOTwE2jddXtui6citVS/P4mqSxvbpW1s4SnlJSK+" +
                "LZ3T22+lgZKQP8mQe9o8ka9y0LBsuQ02K6vWbX6mqbmGOUFRZGIZNWOoIUFbrp/MOfGlTQPBzvmqBSnf" +
                "LdzTcCfhb4fWoApRT8lcC04F4nnrIQYpqf88FhlZ+So9iKdoHj7Q+LLbc+2r+PG00jrivQq1UXTr+iTk" +
                "tLxlUpfNZcz07Dq6LMeiM8mNKGSdSAOyhY8aisWEmhBPlM7S+D3aJC5U+3oExgvuIZIAH/sRqYt4ZZSJ" +
                "sR3BNgIWNed0jkSPwpLt5u3twud6e1JBFGwUgpAFwkZdUILUdcVtZ4m7pzaxrPbfXKeS+qGgyGwsDWbN" +
                "hoeF0UHykcujJe0IEzIoJY9R8oV30upAUiCU2gllgsDIPOnf0i4yUwEyV6jAUsVdGeTYgXSJtgpRaIPL" +
                "AtEH7T7CJO88S8JRsALUdtNAUNMQO+p22cmCGHU+bR/o7rMjWCzNlmuH8RqJDih8o5FPq6Ua6RpNowqP" +
                "EilZmNQHnK5pXDmMKFuClSOBECU8mLdLOKVsNIr5IjmtKYMPGK4TsynaEhb+KXzyvi92bcbXKo9Wim6y" +
                "K9/YZ+oJorAKi7dsWgEesPUwLmhPzi+K8xcqbGWe0zN6G2IUtCtPfey+xkqn8B757VFzbp5wP0d4ERYm" +
                "hIHQlIdMrk1voy01D3ck7s5nZVrEtkiQsJR645zT8cppghPA2MTg3y0TUG92CQJvICyOOEInQ126IWpL" +
                "lWD1JCPbGie7AhBWutDZYhsrfQHsUgZ24qGxEsVrcg4+lEO5Sw18NHtsVpCV3LXU1B7hMJgrRyWJAPNr" +
                "8TRoLAxZF4iutESzShsdjTEjvNIDTNJYSV03pLK+Yhgi9eKUjw8sOJmrAMFGbyKzzpD26VtkCkQ+ckvZ" +
                "TDngtBvTO6Ts3dPT469omREH1a2VZqmhDgLqq+QmSk2ypGSXGtOIEHeQEspwdIrEFfi8KYczZzWbiMKm" +
                "rDTqXQzf9Y6fM0+rFcUpylmdNdv2hg2sTHRxWPnHvLocWyY5PqGFksnLy97g9PiFDcLlmruX41VaiIob" +
                "a/lW1ZzsHxCE05urWJfrLCeIWM9yqUapG4JohmNUkhVE6yJGGU1xJAJJhkIiy+aICByu0MJ0KT1w4pUS" +
                "UAdo3PBjBcVPB5XG3p/+UcPX3/dOJtQh/fOT7Q8J5+SPD1c5aPKxA59Tu0CGMEY9KapWkRVwR44yRqhQ" +
                "p9QGmct5XdElkLMj2Akdz20lFde6OBCqrtDhLzK/PLzhSpLNBRErUeHUBXtgcQjDaZUUu8Fym+z78XDw" +
                "DO1r1zt73704V4IABziFCSPMFg5QKTEpUDupbF0uwLpuQ2mrHmcNdOxzT+nsR9y7MeYa+cq17qi//XOf" +
                "JLzf2T+hzOb09X5L7afG5PiyyPNV59kzlB9+DGnn+//6m7CIXQPGjkyQG3eJjYyiPZvdkHIqUqDMMcr3" +
                "MSlCsg8vuNbats1nMVx1GsUocez2tMte6RRVhOhK5tPXYhuMhLgiv7crS8uLjGsNOVGIkwYoN+WpIWqZ" +
                "5XNERtNRhQD4G4kA3+oi6Hz93bcvBYK2XukQAO4+xft2pfGP5zg3RYpAh6eFnrYWHv8Wv3UQgpuXUvub" +
                "eXb0Sr7QUXVHff3y6AW/AjolAKTPZmMhsO1v0LepfaYMhRhxC7hTdxldmnAd0zh3BXKz2ncGDdN+rLb8" +
                "Q9kCKDoVN50a9H+zFVlaSwV3SK05aYO5aWUbi67KgVm4c2GYlWsoIsOZuhQAyCjg05bOniiJ81ct/IcW" +
                "AB3ufKteD3/GNibP48u3vVEPW4u8nrw/7w9OeyOEcvthOOgdv3Te7uIT7zJEk4WSLM2FBJyPoKywl0FK" +
                "0PJcroRwc6grReRXJ1TAOtLjpXKF7zCIEGSrJnHduki1X87Zl82NL1zYmhCMM6lSPfzcUu+lbf9LlWYS" +
                "MhdMOpkjWbQU1WMQlU0FfxB6u5St9zMykvLtfSFrevuFdvEKSSJ/SxU3u0jtFDbx256qI3oKnYhoFIqF" +
                "b5zkR2siwZY5YkGODsHrjbqn/asxZUiVNZ2SGScpWA4iRSpiOtx+4J6hSw/50MUu9YvykXC0Vdks3MLr" +
                "ve3137ydqAPCbV+aJU9yZ6Qi8ZKnxVZ55XxBHZAvNGU9inNuHeHOriMvlXUeWoWaiE52oj5bnOxeE1u2" +
                "NAPcEOaXeX7dJ+mAJ0q5BOZeKs4YV6UNsUxpPhWbZO/rVcseZ31pheqctCbMwqRqzFM+WnrqPeBSMAT4" +
                "OCHufhHAxef9y4SUjNZ7X6wtSg9kXG63kLQrzU20sKVJW5z6V7rvFbinYhCkuNbeVkuqejsHvRB3zlmy" +
                "+4kW7ONvQVRauo2nQiqVkxQgUF2L8+HQPJkjg/hHJcLe+DFuXEKtM7oNYCpX/sAjnWgi2ck+fGzQGhOL" +
                "gI+PLC5aoNK4czNc7YXMD5dACYCp2SFzvuMhk55IVI6NHSJzbCHLK4iSqxMfjoROfetxH/BJqOW6fOch" +
                "vxyCw9Okvi/r/6JJ4N+qL6n996UKfsc/oTpWXFH7qnMMA9ezD199pI5i8fqcXoPi9QW9hsXr0cfiqOHD" +
                "y4/87bEE8IkeXq2vtfPcszbFGRo7b/YX0e0iDF+uK2FtRCnvzaH3R2Vf4YgfWu78BKN48YMArVCptrOP" +
                "pCWzDS33oz4WPfPKWrKV6Vu6J0R9CJuHFm0RGw1o93NdB+oO0iU7HFpkteNpjhE1LttgvbHjSld2/04X" +
                "XTQtP26xdf+2V7h27Qfs/R71gNxt8ye6VV0xwE81mcUS84cnbN3kqUys32muXmB/HDY/k7LtO542e+Ur" +
                "ymw69kJV7cjd3p3RsrnwPTW+ZLXDLp12dzStd6Uc9v7mISTlTppKXAdyhdPdXq3f7Gk6FxSI8tZRBQUf" +
                "6eAOWrwOiQt7L6SSy2TPSht+tm25e/aesfUW9h9bdNhPBbaKJ0klsDVJnM6BUUuc6sa6BzIypJc7w+AD" +
                "2vxrwuEfEeN0Ulcr9USciVUvkB1QUmHUK/rbgebn3XIp2j62beqXp9Jl/HOmifRzTv2n6m2lB+7JVILZ" +
                "vSVgORXb+M/W2bayvzQe1v/IpSGXS7F5kAjcX8dMNToZkUnt6Pjq5KQ3poJF3s+6/fMr9Ci+o5+G/Xh5" +
                "3h0M+oM3Ho32To8PHXR/8K573j/1LoaT/nDgEdzx4Qs7WPnoWcDupHfqvX7v9Qbv+qPh4KI3mHgnb7uD" +
                "N73jwyM7DWXUZDQ8L9Z6ab9fDbqvz3veZOh1f7zqo9we9wbj4cgD0u7x4dcWatK/wBLDq8nx4StH/ajX" +
                "u7jEyseH35AkigOCv5dXGjP3N07yl0FOduNJdzTx8O+kBxa8k+H5eX8MpiCBr3aAvOsPz/F77F12J28B" +
                "PRhPRt3+YDIGPCpCmfBm2D2vI3tRHfsjLEdVwMqQm0S6QQNpWztvRsOrS2/QvYCUn39dH6xhAsirGsho" +
                "+HpoWTw+fP5NbRQV8g8O+be1MWnku1HYE3o5cg11S8xnIwB4IGAwPhuOLlj1ZISHL5yhFcKCufROfiBb" +
                "hD28AxwZBQCdBCu00r885oRmDaY/OBsWY9xtq5jBFl2Dodf/wRsPz6/IkmGiUOK/AaV6cu29NwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
