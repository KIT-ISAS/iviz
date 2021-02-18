/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionPlanResponse")]
    public sealed class MotionPlanResponse : IDeserializable<MotionPlanResponse>, IMessage
    {
        // The representation of a solution to a planning problem
        // The corresponding robot state
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart { get; set; }
        // The group used for planning (usually the same as in the request)
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // A solution trajectory, if found
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory { get; set; }
        // Planning time (seconds)
        [DataMember (Name = "planning_time")] public double PlanningTime { get; set; }
        // Error code - encodes the overall reason for failure
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionPlanResponse()
        {
            TrajectoryStart = new RobotState();
            GroupName = string.Empty;
            Trajectory = new RobotTrajectory();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionPlanResponse(RobotState TrajectoryStart, string GroupName, RobotTrajectory Trajectory, double PlanningTime, MoveItErrorCodes ErrorCode)
        {
            this.TrajectoryStart = TrajectoryStart;
            this.GroupName = GroupName;
            this.Trajectory = Trajectory;
            this.PlanningTime = PlanningTime;
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionPlanResponse(ref Buffer b)
        {
            TrajectoryStart = new RobotState(ref b);
            GroupName = b.DeserializeString();
            Trajectory = new RobotTrajectory(ref b);
            PlanningTime = b.Deserialize<double>();
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionPlanResponse(ref b);
        }
        
        MotionPlanResponse IDeserializable<MotionPlanResponse>.RosDeserialize(ref Buffer b)
        {
            return new MotionPlanResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            TrajectoryStart.RosSerialize(ref b);
            b.Serialize(GroupName);
            Trajectory.RosSerialize(ref b);
            b.Serialize(PlanningTime);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionPlanResponse";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e493d20ab41424c48f671e152c70fc74";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+0ba28bx/E7Af2HRQVUUkLRtuQ4CQt+oCXKZiKRCkm5cQzjsLxbklcdb5nboyim6H/v" +
                "PHb3HqLiFK1UFKhbxLy72XnP7Mzsel9MFkpkapUpo9Jc5rFOhZ4JKYxO1vSUa3haJTJN43QuVpmeJmq5" +
                "19hr7NPaUGewdqXTCD/DV50LA4jUXmOED2P8LfJM/k2Fuc62AXzM8mL9PNPrlVgbFYmZzgpCh2uzlkmy" +
                "FTkAGblUQhoRp/SYqV/XyuRHew2TZwhMSIIUoBhxt8S+p9wU8QxorNPIsjbxn0pQjODasZHHQPnQqBAE" +
                "NEBwlmiZv3nt+QwQgNf0sgwECHWkxLFQKf4wxK6+UxmIAmxLAxyhmDMZJ+sMFl7Bx35OS89ogcKfAS7e" +
                "azQ6/+E/javxu7ZYAs04D5Zmbl4UNiJ7xEYslTFyjoYFf4hTVDpwvGTXkFO9ztkGhaVBsS3VorcrbWIE" +
                "NOhEcW7E33Scwl8yjUQSp7cGTKZSAxIS9R/wK3sIAQbWc1CdP/DKfCFzsZRbsZB3SizXSR6vEiXOhxdC" +
                "ZuAZKxXGsxi8Z6FQn2XsVwgMgCUqtD6I9Cx4QK+b5zJcAKJQJ0lsKBCm6BRGHEr3DYLBaPAIlEXotNAE" +
                "uIZDcObWD2n5p8/CLQ886sCiZtIXiZyDmqM4BC2D020WChBngB3sYUKVKgE/gPYUfqS5yiBcc+AGIkKK" +
                "KJ7NxCbOFwLjEJB6JjUhofV7zrpoYG1yiKt4udJZLtOcHHIBFkrI4UEiL+5URzH45KFjCABTTQTCRMls" +
                "FzBSogTCXtZuQ4JQ7XYpF0wVEFRivYpY2jhn9vOS94E6p1onwG6A8j1ZKOx2xpKypI8HcsSFTiIjgHGJ" +
                "SoB4DbN4qjhHkXCcO1WOPyCdQJ6iQMrA8KAijoaWaOzb7OcXKdAhfxaHmbrD7KVQ3assNhh74RFyE6lZ" +
                "nKKqt21AIL6qxByRXCiPRUaIYHnULEDvVKLDON8+BH1hCPiFOaJYLZaoGVgrZ+lRH6tVgtFmM7FFMFji" +
                "6sFRiwTrFbLAinUagxbQ4SLYYThWp1tKDpiwrSIWSkborDacOXFS8gW6m0UcLkr0SGtGbCDiwenBwSIV" +
                "tUQXUmwdBrADpxoCyJmR0giHkluKJPwugzRb1j7VbGhikxvr2y4NySyTW9MkChhGZMYVbHBVDRMzaHbe" +
                "8rRMbEAv5a3iRRYeZEcP0yu0qExa4q8LlQrVmrfEVq8zl09JilQDQmsfaQxYVmJWsMGklk1cIkKZCgjW" +
                "O1U2J/Et1HKVb603ovZYGrZtSXaz0OsksppzejLxb5D4QWRQJOMpRQ3teinYfANUQEzvA57NknLQCzzT" +
                "XE+EOSBjC7YajcZ7dg72kYbd9CGvkv/YHRkeXSCUXjmHL71iDTxZQskjzibMM4gBGSWNZBaBOnNJmYPy" +
                "bTyHhHqcKOAQJV2uwHKcV7YrlLpQ5hzyd0alEFVJmHz1cgkqDUmP4K+V9ezykrwwDteJxJoE/DxOEXyW" +
                "SfJvBDNYRqWhEv3zNjm4CqFmuqNYTUOsVjA9989FYw12Oj3BBY39yUYf4yY0xw3KEff5Qd1jKWloc8Ic" +
                "9RUL1wLcmG2pjIJ6Ct8F8AjpBogAC2qlIQgOgfPrbb6wO+udzGIJ5SYiDiVl0ANcdHBUwpwS6lSm2qFn" +
                "jAWNP4I29XhRpmO/HZr1HBQIgFD43sUR5y7yU0iD4LxJPM1ktm1QqiKSjf2LjBIJmo8sgmmzGp6ubiVr" +
                "BHH0HNvbw2oIhB09qPx5R0IXtamGItMnvEjNM6UoDc7gR6QhywCeGexveuPqB5BuHeZQ3yJYQY/Taj+3" +
                "Clkv0ZvRb6TbLdBvzdbkalnqKwy3IFCipwYrUV4zV3mxGwFamWhL/Ra2SNoyRbiAArYlLjAx34NpEshY" +
                "3MvIzG1d3FXcjM4vaIc9xcLy8B5SJ/xfbtAhcD8E3zGKP2I+xZxXcvQyd6xI+CuLAQuvxf2l8h2wMoTD" +
                "Bo4LTQK6x1SGtyhwhYf/b6rPu6luMsiLiz+8qTrw/6VN9bE9lfsiXG4acwVtRA49OyWQiXNhgPLu/ABo" +
                "AwZFAPy79u2vpCb4yPp6qqT3CNdOk37YYcPBp5WpyjcK/CLf6Ac7JtkPEx4EkwzBlxsfaFxwyusTjuqf" +
                "1rAgSzEBZJpT6vMIaZnZIaKEEgi/1fgXPhGTRy0V9oHgU34lNZboMyBDCwMso66tid1apEEf0AdSFoNQ" +
                "w12Gwh/T8dYlQ9YJvoYlhxhsTWxsU4bCrYJqFapuIFdn8TyO6mmUEr8Vriny2Qm4NIQU8czEwISAxGn7" +
                "qCX6MwrQDQpEwe2atanyfNHmn2vdxIrKoqgq9JqCyMVqnMKWJCOwuhv/3PtfvrQUvz2LqQsf22VtSMtZ" +
                "7Lfzis3x6dfCQVHJXxTI/do8U6xS0rBiuQ3WFF1rVZ5ppm8VCkkuZnAmgzMJ3HJlOqfCFzcNSHYuVi1I" +
                "8Wzhnkc6Tn87rAamYPMUwjUhqIB52npQQNxy/5iIhKx45BnEc0wSHxl/2e259pbjeFqaHtFeBb1RfO/m" +
                "JBi0tGXisM2PevGhmPSSKv2kksZRUHhCJWAWEtoo0hS0hYqUTN9rjAAOzg3lER/CMdF9SCcy5GBCm6HA" +
                "hDTVdjrYgqyFUzqVQ7WHuckN9vZ3YXRTPi7XvCxeHUwh2mvU9aUtt25sbkdMNFO1FWZ5FucmlzgilVia" +
                "Wzb0OlwgCiAeqZmEiksce+aYFSzOE2j+oi3XZ1AeMLd2wV5RKxC6wM51ES1oK4QyFoxhOaNqEgrukEdG" +
                "la4U7EI9AlsGtyIWlbahJeLw4iBuu4VAilOgf+jieV8LE01tqsxwsk8ZkNAcNd1wjIikKsRknm2JXKYS" +
                "7m4RMdVVTBoNCUj9NLl0bFGMCEvHBpFCBgNLpLDORkF778vVmlXoHOI21Zu0OE7gBc8y8X8Yn11bBTaL" +
                "Yxg/ZXYtHQXRXrVqLOSFGLCyWkUekisROrAjH3IUxzVxVCx1Rt+uFHsI7tdTaah3JC0VIcU/Amwx5imN" +
                "elgklmSCKBBP+XDKjfJtKsYmaEdV72LZroszmzQwfOqjdSrWS6cOhSbGOgFFOEohzm6XMQ5U8NQD8xFz" +
                "S2DX7hvOrUpw9TLEVAACawakdqXMooYZXwH40n7ZiQs/ltG8xbChkz1oi3HWr7BIsAnPy9kUU3vuQ2Cu" +
                "beViA9Sw5hgE+0VRzF0WqfCowh+eqpE8ROoxYfFjmcNuFJmyb1kb+JNCOuCgwq8EBL57F+u1gRJR3UNV" +
                "gSLEOSdxzkdg7OkW6vvu+XnnJVMaUfatEJtlesmD0/QuznS6xNoYW+4MO69DBV37FpIXRQkdVOUQ6abm" +
                "JHF0ZImNelfDD73OKyvZaoW5DKvc1EtHAxGbgIl1f+z5+xK7qpwXOWnBHiVRr697g/POiU/WBdndFIlQ" +
                "E5LnxgaEtTt1CIcI4Uzo2tzl2uQIkahZzi3sEdKCjGd0gioDDbucUiTdSBlQaGTZJBWdMpPDlcp8LwB4" +
                "4RErVw+r3fcny51fTjqN/X/5jxi+/aF3NsHh6r++2P5hBZ39/jEt5VUar9DJt0t0kOVwoIWtrlE8gMFy" +
                "E0ypMpyhzPm8z48Y+OAJ/IWO92q1yK3yB0plIm16wyiKOVXmfGsO+SwV0dTvCoCmwBlNywzZTZkmbT+M" +
                "h4MXoV668dvH7tWlYBQt0fUODZnYR0SpS8Vs7nRTubqAhN3W0xI9qjXidIf1KbJo/qP1LZQ5t6ot/vT3" +
                "A1T0QfvgDEui87cHTXGQaZ3Dm0Wer9ovXkALIxNQen7wjz9ZITMqtlLN07/Upk22oq2K0EglPWDlGecH" +
                "sCgOqeO+VcrO3mcJhO40TqBPalX31orr4mks69E13udv2UkIC8qFicCS5sEZudkadIWpj+eopk3n1sCj" +
                "FZieBWFqC68FfomKgJd1RbS/+f671xYEN2oeNgDgQ7YPHLXxT5cC7GcUHsR6e1WJj39N3jsQi57IiYPN" +
                "3Jy+sa/w8Lstvnl9esLPsCBDkBirZQcDpcJGZ1H9PRY3KJCj4k7z7eeljtYJAtCgIderA+/j6O5PNet/" +
                "rMIAns45fKf6HhrLFXpeU4RbKNGp6gtx0Gqnla5vypQ/bAY3c1NKKIymrl4AZLgh4P5Pscn198sm/K/V" +
                "oBOj78Tb4c+dV/b3+Pp9b9TrnNjHs4+X/cF5b9Q5dS+Gg17ndcO6rstbtAshTxYK3zccUBTDdmzcdZMC" +
                "tDjsKyDcGhx1IfvlBSWwNg+OsfOhuxGsBN7QUV33Ln0dFGsOePNrWB/FryA4scpNyM9N8ZHPAn4p84xK" +
                "pt5LpfPcD6srWQkntnGkvHyg9Fah2+DnzsvS00eva3z6BVRdZon1b7miCRqaHRMp/G1PFQyWSZxkKD+z" +
                "3JmM4jWyYJsl9qBWxa7BqHvevxkDP2WazsiEEw3Mp5usFXYdmmnQINLVknSSY0n9IiQUJC1RTCAreIP3" +
                "vf679xNxiLjtw1EhE19FKWm8kGlR6dBcLIhDjIUjpodZz9Fh6SwdfijReYwKTiad7th8tq/ZTfNMpzxc" +
                "cJ9gfdEb1GMST43ijFrpFodMvCp8iHSK67FjRX9fr5r2jOxrq9RGLRKt/rxL1YQH5ypF6gPgQjEI+DQp" +
                "7mG/QN3rw+uLWKzWB2pkLSwY+DtfmUFtlyamLdHgya+/SlAa6ZfgnkvAOPXj0MqQq3zlR7KNq+J+Ya77" +
                "9FsQtqJu4ymxit0nJgjoyjn4slimc6gn/lLKsHcyWSts1GZ4xUCXLhWCjHhMCsWP+fS5gTQmFgGdSVlc" +
                "DZs87CjQrXAd2i3ePSMA4maHzuniCC96JlU5MXaozIl1YAqm+D7Gp1PmU90HNFZ8Fm6ph995c4BP1lXT" +
                "DgOKYYGfKMh78TVOEr8W4W/wn0h0xEs0lhTtDji4mn16+RmHk/7xFT6G/vEEHyP/ePrZn198ev2Z3j2V" +
                "Ar4wCKydpu48TK0tcY5GwftkhvsC3y7D0OWCAtZmlOLegIqpHfSB+KnpDmXgKzzIMFSJbcTNZ7SSrkLz" +
                "pavPfgpfosVbmbrHy0cqark61A9PbDbA3c9NJXC2iJcMMuxkqmfelCNqUrZA9MaOe2Lm4UUxEKf0siLW" +
                "wytk0dqNJmDvD3BSxJfbn+3edskBvzSpZk8sX3Ovr6jcDyqtrF+ZLuN4Jq99hLXqLRdbv9LtZ3Iee0+r" +
                "dpJvr+Qo3l7o+hvdiNnhmc6+pn5ZYnfRYW+wHIOq3AFWgeuQL7G4+zv1C0NHLggZorjMVEJBZ0RxGibr" +
                "SNGola6blKoZ86Lw4hdV392315dtvFAE2bbDvvLYSrHEvUBlEYedA8NhurC7bTkGCVnrkVslj1jzv5MQ" +
                "f48ZZ5O6WXFI4lysfC/tEMsKLd7gP044+mOXZ/wgyA5WZXHYXWRA55oa70jUrnk/cv2mlM4ekEgree3f" +
                "o1P1sv9qRqz/S5oG31mF7YOniu5f4UzVQt7FOtuzAOObs7PeeIwzeX5x0e1f3ox6ne/xD67l19eX3cGg" +
                "P3gX4PfeeefYL+gPPnQv++fB1XDSHw4CBOwcn7ivpbeBhexOeufB249Bb/ChPxoOrnqDSXD2vjt41+sc" +
                "n7p10FFNRsNLT+61+3Az6L697AWTYdD96aYPrfe4NxgPRwGg7XaOv3Fgk/4VUBneTDrHb7wMo17v6nqC" +
                "6L5lrfgzhT8X9yaN+wdW/I+RjNfUpDuaBPDfSQ8kCc6Gl5f9McgGqni5C+ZDf3gJf4+D6+7kPYAPxpNR" +
                "tz+YjGHBq0Kx74bdyzq+k8rH30N0WoEsfXOr0FKvC2rOWO9Gw5vrYNC9Ap2/+ubB1xoygHlThxkN3w6t" +
                "qPD52/pnaJ5/dPi/q3/k8b/7/D3bwt58rSr9YgQwAbAxGF8MR1eB887jk1eFp1jFgRP1zn5EHwUf+QCA" +
                "6CgA6bVZYhn/Sx+9Aq0b9QcXQ//xNXNWco0qd4Nh0P8xGA8vbyZkuFNgqvFPM8LYDFU4AAA=";
                
    }
}
