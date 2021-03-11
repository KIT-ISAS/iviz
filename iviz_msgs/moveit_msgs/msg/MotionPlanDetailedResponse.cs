/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionPlanDetailedResponse")]
    public sealed class MotionPlanDetailedResponse : IDeserializable<MotionPlanDetailedResponse>, IMessage
    {
        // The representation of a solution to a planning problem, including intermediate data
        // The starting state considered for the robot solution path
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart { get; set; }
        // The group used for planning (usually the same as in the request)
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // Multiple solution paths are reported, each reflecting intermediate steps in the trajectory processing
        // The list of reported trajectories
        [DataMember (Name = "trajectory")] public RobotTrajectory[] Trajectory { get; set; }
        // Description of the reported trajectories (name of processing step)
        [DataMember (Name = "description")] public string[] Description { get; set; }
        // The amount of time spent computing a particular step in motion plan computation 
        [DataMember (Name = "processing_time")] public double[] ProcessingTime { get; set; }
        // Status at the end of this plan
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionPlanDetailedResponse()
        {
            TrajectoryStart = new RobotState();
            GroupName = string.Empty;
            Trajectory = System.Array.Empty<RobotTrajectory>();
            Description = System.Array.Empty<string>();
            ProcessingTime = System.Array.Empty<double>();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionPlanDetailedResponse(RobotState TrajectoryStart, string GroupName, RobotTrajectory[] Trajectory, string[] Description, double[] ProcessingTime, MoveItErrorCodes ErrorCode)
        {
            this.TrajectoryStart = TrajectoryStart;
            this.GroupName = GroupName;
            this.Trajectory = Trajectory;
            this.Description = Description;
            this.ProcessingTime = ProcessingTime;
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionPlanDetailedResponse(ref Buffer b)
        {
            TrajectoryStart = new RobotState(ref b);
            GroupName = b.DeserializeString();
            Trajectory = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < Trajectory.Length; i++)
            {
                Trajectory[i] = new RobotTrajectory(ref b);
            }
            Description = b.DeserializeStringArray();
            ProcessingTime = b.DeserializeStructArray<double>();
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionPlanDetailedResponse(ref b);
        }
        
        MotionPlanDetailedResponse IDeserializable<MotionPlanDetailedResponse>.RosDeserialize(ref Buffer b)
        {
            return new MotionPlanDetailedResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            TrajectoryStart.RosSerialize(ref b);
            b.Serialize(GroupName);
            b.SerializeArray(Trajectory, 0);
            b.SerializeArray(Description, 0);
            b.SerializeStructArray(ProcessingTime, 0);
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
            for (int i = 0; i < Trajectory.Length; i++)
            {
                if (Trajectory[i] is null) throw new System.NullReferenceException($"{nameof(Trajectory)}[{i}]");
                Trajectory[i].RosValidate();
            }
            if (Description is null) throw new System.NullReferenceException(nameof(Description));
            for (int i = 0; i < Description.Length; i++)
            {
                if (Description[i] is null) throw new System.NullReferenceException($"{nameof(Description)}[{i}]");
            }
            if (ProcessingTime is null) throw new System.NullReferenceException(nameof(ProcessingTime));
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += TrajectoryStart.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                foreach (var i in Trajectory)
                {
                    size += i.RosMessageLength;
                }
                size += 4 * Description.Length;
                foreach (string s in Description)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 8 * ProcessingTime.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionPlanDetailedResponse";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7b84c374bb2e37bdc0eba664f7636a8f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRrb+LiD/YbAG1vZWVto4zXa98AfFlhO1tuRKcrZpEBAUOZK4pjgqh7LsLu5/" +
                "v885Z4akaKXp4t64WGDTIjbJM2fO+9tM9tRkoVWuV7m2OivCIjGZMjMVKmvSNT8VBk+rNMyyJJurVW6m" +
                "qV62VZJF6TqmV0lW6Hyp4yQstIrDImy19hitLcK8IAj8gk+RyWwS61zHamZyVdDGZmqKaqtVWCxaI3o3" +
                "5hVFHv5TR4XJHwLG5RHPc7NeqbV1mEriDtZ2HabpA+O24VKr0II82Ur/sta2OGzZIidYxhFkACKsV+u0" +
                "SFap3qbFqjBn6Zi80HFb6TBa4HGWgqhHnNtCr8rdKspJYpG2FvCe/DSxBQnZI66gE22F/0m5/sPHGjLC" +
                "cK5tlCcrrynhbQcidUDMEUhFARPpRQDMcYXLExcuzTpj8ooEy+0KZgHVLVcQCxDAFEip0ToNc8ZGHC+N" +
                "iAx6cKBiSK1ZasLi1UvsVNEQEF7ajXS8hogL5kFnsbCTWEbUujJ3ul/08tzkZwaEKk2/BhF+bz1rnf4/" +
                "/3nWuhq/OQEndzopgqWd2+eVIT5j2YCwJVgI52zKRZhkpG4Y4FK4DadmXdTNmpbCUTq6w29XxiYEaInP" +
                "pLDqnwYGBAGA8zTJbm0LLmjBIu/+PX0UN2A48oCC5cZfLFBCcsvwQS3CO62W3oDPhxdstVBclMwSGMUC" +
                "LreFmo0dcLUteHkQm1nQ2KxbFDB6YIlMCrtlo5uSjcG+Qv8NIcIaGAtxoShkeBkctvz6M798yKthEX41" +
                "NOo+BQ4z7XuRhuRfcRKFbHebhQZWihpQg410phV+wcZTLW6ICEYeAIcPVZzMZmqTFOSsJIeipNAwEl7v" +
                "dUpqNbZA1EiW5EYh7J2iygJ6AUNz5qZkdWpi9i1PDwAzw/ijVMMldgBjI46oYlsnJ5HJ9clJLcxNNfbT" +
                "ar1C8OSwUgjxRc3kDltTY1IQGxBzX84Bdptg6QD4Pyy9gC1wYdLYctgnIUhAgU44AjN3kk20hBSTIwqz" +
                "++TQOkQkPtBRrb0qacgiDrb8WR3k+o7iMt4j2ueJJY+LDomaWM+SjET9cAIE6i9bnubjo8cSIsTkannY" +
                "rkDvdGqipHh4DPrcMvBze8geWi3RM6irEO5JHqtVSm7mIr9DMFjS6sFhhxnrVbxgxTpLIAUyuBjBVZx0" +
                "iif4FOcjEcRCh0iWpR+Tw2uJydh3s0iAsdqPpWbVBq4Om4eFxTruqG6aPoIBdlBq4D9ejRw/xJP8Uh+U" +
                "OYfSnsJGMwZaJDKOZmEVf8I8Dx9sm3cgN2I1Us7YljATQ2qXhG5Csm2iYhnealnk4ME7WZjhLBWmHfWP" +
                "hc6U7sw76sGscx9FmYvMAKHTT2gtNItdYu9NVLZgiYqQpuCsxHWlTqZb6eWqeHDWSNITbkS3Nd7twqxT" +
                "RFaPg+Vkk18R7sEyBCl4al5DUCaDzjfYBWyWNlCSWRMOJ25PNASdQy8FkIkGO61W660Yh9hIq0robD+1" +
                "rOscofbKG3ztlUjgC8aUIpaAIlRTMEFUyeIwjyHRIuTgwSE3mSOmHqUaRBKzyxWUJ6HlYUWMV/KcI4Ln" +
                "XOtxGUjx1yyXkCryhZjs1nqx+q3iJTIw9SQj8FkOsRF2kjHViVmkVf/8hG1cR6h8QBAcNItyHXId1T9X" +
                "rTVUdfyCFrT2JhtzRGloTinKb16GCH1P9TXRGVoKU38R5jrATREXu8DAD/hdgEdEHGwCElDWwQ8OQPn1" +
                "Q7FwifUuzJMQNTghjiABYN2nRfuHNcxE9gmsITMevWCs9vg9aLMSL/F0VGZEu55DgFSo5eYOQYzDF5sq" +
                "IiHsN02meYhqVSpI2rK1d0EyFi9ijVDk3PZQX5mzNoIkfpok97gYIvscPeqIJC+RlbqAw/5Zhr1Yz3ON" +
                "CAzIGX6JDWIN8MyQ5czGVxFgcB0Va8RogFUbSnDtSzaxdo2OQkwn9DmDTNc+oNxeSjSwK9Ypt2ao+TNL" +
                "VaismeuiyklAG6bG7X6LRMmJU0ULFK8ddUHh+R7aSRG3pMeDWl0Ck87pZnR+wXn2mOrKg3sEUPwfbsgm" +
                "KCvCfKyWjxRVKfLVbL1OnQgSP/IEWGQtZZmt78AqEB4bbPdOc2szDaNbYniLhv+m1qdNrZscoXHxu1Or" +
                "B/9PSq2fyqzSF9Fy25pr9BIFphIcQSbehKVVl98fAW1QKxEA/Wx8+weLCR9FXl8u7n2C7rK6L+dArtgs" +
                "I8tUFxsN0yg25lHeZBVSzEOpGkYIZq13PII4lvWpOPaPayzIM4oBuZGo+lR8OnJ2cRmiHKKPDRZUGY7Z" +
                "rpaaekJYVrmSe0yyHLDRITfLuYVDs1+o2EAk6Ak5lsHhKNlwfU1BGUZZFwu9xpIDcrk29biZQMmkBRi4" +
                "zEHEzpN5EjeDKYd/x11bFbMXMGw4FtMsm0GLQOIFfthR/Rm76YYYYhf3jRu1bI4urgIKY9pUWjkU2xK9" +
                "ZlfyHos5SAFvgeJdSanuy9/KMlP9+kTargxtp8KRyzEc83l9S+309EtlpiTnz/FU/rZ5Mqel+FFy5pOt" +
                "rfrYbZamubmFUUFdZGiWJjQ0paD0G2ZzroMpgSDwead1INWzg3sqBiUY7tIdFCJKqvhrw7tAP2ci4pHK" +
                "/N/HJSOrHmUw8TRzxU+MxIRlrRqvxaentakSZy80TMm9n5+QA3MSpQGcL6Ppdz/fZlGWQ0ueUaEORWFg" +
                "FyEaK5YUekX85ifzDSpaexIj6iM/AuMN9xBVgI8dijRGzDLKzLhhYQfBi+Z2ukDpRyHKDfr2duHzYz9p" +
                "K0o2SkHIBnGrKShBWh4+yMSJB6uu1KyP5vwQk0aloMhsHA1mjQS8RxtjshSiuldHJWFCBhXpKfrA+KF+" +
                "olEtqEoGRhbIaJcyykxFqGWhAkcVT2tQdUcyPdrqTqENbhREH5SJhEnOQkvCUbIC1C6BILrRGB39vGS1" +
                "KEX/T6kEZx3sCw7LYduPyXiPTNNoHg0b75ZrFHB8XLEnpZVsTOoDTj9Prh3NVLPC6tQCwiDqAreFV8pG" +
                "o8Mvy9WGMpA9Z+o2M5tyXOHgn8Ytd7hj15WB7eqsqZw1+56O3aZZNQq3MHrHqZOhnMgwLihQDjjKAyk0" +
                "vG6dVzVmHmIXlKSnIZKxcQIqHUh+BjS3m2c86hFmhIcJYSA0HnM1xHdBl+aKO6p577ayLGFzJEgYS3Os" +
                "zjV67azBC2BsUvDvt4lobLsEgXcQFgcdoZOhrv0nPicqwZo1h936TqYFIDq703axjZXeAHYpH3bioW8V" +
                "itfkH3y8iR6YxvsYArkKwVbctdXUHfAwmO9RpaAA82txNmgsjlkXCLC0xWGdtmtaSozwTp9gkr5V1HVj" +
                "6vVrhiFSL489+TiDa7saEGz0LjFriypQ36NmIPJRakpK5ZjTaU0fUMR3z89Pv6ZtRhxXt3aa5WbpTufu" +
                "ktxkS6p9aWaNIPEAKaE3xwRJXIFPowr4s23YRBIfyk6j3tXwXe/0G+ZphYNFDrllVHIzDxdbmejyPPW3" +
                "efUltyzyfEILFZPX173B+ekLF4erPXdvx7u0ERg3zvKdqrn2PyAIrzffxi7XONMFRKpnhbSoNCJBQMOB" +
                "MskKovURowqoOC2BJGMhkWVzTAQOVxht+gofOPFIxagHNP7zl4uLnw8rCI//9h81fP1972xCw9N/f7H7" +
                "Q/I5++3TV46bfCgx47TnYhkiGc2qqIVFbcCTOiodoUWd03hkLgd65fRATpZgKnR+t1Va3OryuKi+wwm/" +
                "kfXV0Q73lmwxCFqZiqc+3gOLRxhP66S4NMvjs+/Hw8FzOlJ3M7X33atLJQhwvFNaMSJt6QO1ppNitZdK" +
                "NTeU1O5zSkf1uHagQ6FHWmdX4pmOMbeoWm71ifrTv/ZJwvsn+2dU35y/3m+r/dyYAm8WRbE6ef4crUiY" +
                "QtrF/v/8SVhE4oC9ox7kgV7mgqNoz9U4pJyaFKh+TIp9LEpQ9cMRbjUuG3CIwP2L+2SapGh3XIbaZbB0" +
                "zCpC9E30+WuxDUZCXJHru51lFEbGtYacKMrJYJTn9TQodczyKSOjOVGlAPgdiQDvmiI4+fZv370UCMq+" +
                "MjMA3GOK991O4x8vcaqKKoGOVks9bW08/iV96yEEN2+l9jdze/xK3tBZ9on69uXxC34EdE4AKKLNxkEg" +
                "828wzGm8piKFGPEb+GN5+bo08Tql7zwnKMxq3xs0TPvLTew/VTJQmXYunjo1GA3bFRlbW0UPqLG5dIPF" +
                "aeVmjr7dgWX4g2NYlp81os6Z+kIAyCjsU2JnZ5QK+us2/sNQgI5+vlOvhz8hmcnv4+u3vVEPCUYez95f" +
                "9gfnvRECunsxHPROX3qH9yGKcw3R5KCkVvNRAacn6C/chZEKtDq4qyD8GhpVEfn1BTWwExn/Ut/C9xxE" +
                "CJKwSVz3PljtV2v2JcXxpQzXHIJxJlXaiJ/a6r1M9H+u00xC5s5JZ3OUjI6iZhii/qnkD0LvVLINfkJd" +
                "Uj29L2VNTz9TLq+RJPJ3VPEEjNROkRM/3bE7AqjQiaBG0Vj4xlF/gmtIZub6HbEgT4fgDUbd8/7NmOqk" +
                "2p5eyYyTFCzHlCIVMR0eRfAg0ReJfB7jtvpZhSg7OqqaIG7hDd72+m/eTtQB4XYPhxVPcq+kJvGKp8VW" +
                "n+V9QR2QLxzKfhTq/D7CndtHHmr7fGoXmix62Yn6XIuye09kbZkK+E9YX1X7TZ+ks58k516YB6w4gVxV" +
                "NsQypfXUdZK9r1dtd9L1lROqd9KGMEuTajBPVWnlqY+AK8EA8IkmYdQMSBf6+H4mVaXNURgrjIoE+S43" +
                "YEjgtYknRtsyvC1vBtQG8zW4p+MRxJTDvq0JVf0SD0Yj/iC04vgzo9mnyEXUaJYZqEYtdZcUKfxNTyRI" +
                "DCJRTfy9FmrvwhQ3Uuk+KV0aMLX7gWCTTj1R+NgPH1u0ycQh4CMmh4s2qI3y/ArfiqEKxCVZAmBqdsid" +
                "b4PIoieTlmdkl9Q8Zyj6SrrkksWHYyFV3wc8HHwigrlX330dQI7L4XXS9FdDgXJyEN6rr2gs+JWKfsVf" +
                "sTpV3GaH6uQUlq5nH77+SJPG8vEbeozKxxf0GJePxx/Ls4gPLz/yuy8ng89M95415l07D0kba7zFsSPb" +
                "P4z0MuDwlbz6NW05Oy9v22EuSO1g6ZQf2v6MBV/xEEYRBqXSiNuPpCuzDS23qj6WE/XaXpLf9D1dLaIR" +
                "hStOy4mJiwyUEv1Agq9y42oeTjVs4zib40WDzQ54b+24CGYf3wSjG6rVyy22Ht8Ri9d+MoGCIKDxkLuZ" +
                "/3Q3sutm+LkptNhj7f58c8HW7Z/awuZ96BqKJ7PcT9D2bPt+qCts+YYzG5C7idU4pXc3brSkG77gxrez" +
                "dlin1/GOqfauUsTd/TyCsPxpVIXrQK5/+puvzftAh94RBaK6q1RDwcc+8q9NiAt3m6RW49jnlSU/37bf" +
                "PXdH2fkMe5HrR9yrElvNn6RJ2FokrufBaGZOLWXTDxkZKs+d8fAT6vyj4uJvkVPGx6ZmaWjirax+8+yA" +
                "Kg2jXtG/Pjj8fddjyrmQG62G1RF2FQi9daIyndOAqn7N6RMXbGpR7dEWMJ6aefzf9tk2tD84MDb/rcyz" +
                "ltxNRSIhKeAr3ZWFDDDqSEzuvo5vzs56Y+po5Pmi27+8wRDjb/Sn5V5eX3YHg/7gTUBfe+enRx66P3jX" +
                "veyfB1fDSX84CAju9OiF+1h7GTjA7qR3Hrx+H/QG7/qj4eCqN5gEZ2+7gze906Njtwx91mQ0vCz3eune" +
                "3wy6ry97wWQYdH+86aMfH/cG4+EoANLu6dG3DmrSv8IWw5vJ6dErT/2o17u6xs6nR38lSZTnCH+urkNa" +
                "/2/D5F8YWS+dSXc0CfD3pAcWgrPh5WV/DKYgga93gLzrDy/xcxxcdydvAT0YT0bd/mAyBjx6S1nwZti9" +
                "bCJ7Uf/2W1iO64C1T34R6QYTpm3tvBkNb66DQfcKUv7m2+bHBiaAvGqAjIavh47F06Nv/tr4ihb6B4/8" +
                "u8Y3Gfb7r7AnDHvkCuuWmC9GAAhAwGB8MRxdserJCI9eeEMrhQVz6Z39QLYIe3gHODIKAHoJ1milv/mb" +
                "F5ozmP7gYlh+43FczQy26BoMg/4PwXh4eUOWDBOlvv9/AcA7F5AvOQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
