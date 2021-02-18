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
                "H4sIAAAAAAAAE+0bbW/bxvm7AP+HwwzMdisriZ2mrQZ/UGw5UWtLriRnTYOAOJEniTPFU3mkZXXYf9/z" +
                "cnckJbnpsNnDgKVFYpLPPfe8v915X4znSmRqmSmj0lzmsU6FngopjE4Keso1PC0TmaZxOhPLTE8StWiK" +
                "OA2TIsJXcZqrbKGiWOZKRDKXe429xj4hNrnMcoSBH+BjqFMTRypTkZjqTOS4tZ7ovNxsKfP5XmOIL0e0" +
                "JM/k31SY62wdELIS9yzTxVIUxiLzFB4WppBJsib0Ri6UkAZo5N3Ur4Uy+dFew+QZAhOSIAUoRnxdJHm8" +
                "TFSdIiNkRlLSWa6iplAynMPjNAHKtiRgcrX0G5bko+RCZQzAlzwksclR3A51CR8rY+Uw9ig+fa7gYyQX" +
                "yoRZvHRqYx534BKHyCOClGQQpV4UgDwqkZUkyoUuUiIyjwGDWYKZgCIXy4JYB9NAFYdFIjNCiJwvNIsO" +
                "VGJB2bD2GtNEy/zNa9ispCNAxLwh6rwAaefEiUojZio2hGuvca3vVS/vZpnOzjWQKxT+GITw816jcfYf" +
                "/tO4Hr1rAzP3Ks6DhZmZF6VhknSArgXwIGdk2rmMU1Q8WOOC+ZUTXeRVM8eV4Dot1aK3S21iBDTIZpwb" +
                "8TcNpgT8A+NJnN6BCYBXGuCQdv8Bv7JbEGBA+FhyP/DKfA6yW8i1mMt7JRbOmi8Gl2TCoL0wnsZgHHPw" +
                "wjp2Mn0ArOxC64NIT4Ot/Tp5Dk4AiEKdgBWT/U3Q3MDUpPsGocPohSJehE5LSYDVOQTnbv2AloNluOWB" +
                "Rx1Y1Lz1ZSLR5aI4lGSCq7kCxBnbiQlVqgT8AHtPFHsmBDf0BwgDUkTxdCpWcY7+i9LIPZGakND6Padd" +
                "VLA2OQSTeIFeJcH2MdjMQUMJ7o0ceXYnOiJXcwQBYKppgzBRMtsFjDtRuGUra7dDnal2uxIAJwo2VKJY" +
                "RsxtnDP5ecX6QJwTrRMgN0D+nswVdhtjRVjS+wMZ4lwnkaGUgELg8DJRHJiJOc40isOLziA4kyNloHgQ" +
                "EXtDSzT2y3TCiygA02dxmKl7jNUKxb3MYoO+Fx4hNZGaximKet0GBOKrms+5cOmwyAgRLI6aJei9SnQY" +
                "5+tt0BeGgF+YI/LVcomagrZy5h7lsVwm6G02G1gE/QWu7h+1iLFuyQusKNIYpIAGF0GgZV+drCk4YAS3" +
                "gpgrGaGxWnc2nGswPsO+q3kcziv7kdSMWCnKYWBgkYpaopMkWzCAHSjV4EBOjRRG2JXcUhedKbXini2r" +
                "n3o0NJDajLVtF4Zklsm1adIO6EakRswfdQkTMah2zvNaJtahF/JO8SILD7yjhWnKWTJpib/OVSpUa9YS" +
                "a11kLp4SF6kGhFY/0hjQrMSoYJ0JSxpYIkJIWeCs96qqTqJbqMUyX1trROkxN6zbCu9mroskspJzcjLx" +
                "bxD4gWUQJOOpeA1C6RR0voJdgE1vA57MinAojzuiQdAZ6CUHZKzBVqPReM/GwTbSKNM72U8lAVtHqLxy" +
                "Bl95xRJ4soCSRxxNmGauANJIZhGIM5cUOSjexjMIqMeJAgqR08USNMdxZb1ErkthziB+Z1T/UWmIwVcv" +
                "FiDSkOQI9lpbzyZfq2JCDXYepwg+zSTZN4IZrB3TUIneRZsMXIVQAt2Tr6ZhpiTVVL0L0ShAT6cnuKCx" +
                "P17pY0xCM0xQbnMfH9QDFt6GkhPGqK+YuRbgxmgLu4B1H9K7AB4h3MAmQAKUeOAEh0D5zTqf28x6L7NY" +
                "QnGOiENJEfQAFx0cVTCnhDqVqXboGWO5xx9Bm3q8yNOxT4emmIEAsVzL9H0ccewiO4UwCMabxJNMZusG" +
                "l5K4ZWP/MqNAguojjWDYrLunK9ZJG0EcPUd6266GgNnhVp/EGQlN1IYa8kwf8CI1y5SiMDiFHyINUQbw" +
                "TCG/6ZWrH4C7IsyLjDJbuR+H1V5uBVIs0JrRbqTLFmi3Zg1F94LjgFmSQqlhg+I/NViJ8pqZystsBGhl" +
                "ou3ud5AiKWWKcA4FbEtcYmB+ANUkELG485OZS13cSt0OLy4pw55iYXn4AKET/pcrNAjMh2A7RvFHjKcY" +
                "8yqGXqWOBQn/ZDFg4bWYX2rfAStDOGxguPeKepyJDO+Q4RoN/0+qz5tUVxnExfkfTqoO/H8pqT6WU7kv" +
                "wuWmMVPQRuTZmgPI2Jkwd+388xbQChSKAPjvxre/kpjgI8vrqYLeI1Q7SfrRkHHzDBtWJipfKbCLfKW3" +
                "MibpDwMeOJMMwZYbH2gQccrrE/bqnwpYkKUYADLNIfV5mLTE7GBRQgmE3zboFz4Qk0UtFPaBYFN+JTWW" +
                "aDPAQwsdLKOurYndWqRBHtAHUhQDV8MsQ+6P4XjtgiHLBF/DkkN0tiY2tilD8aQFMFB1A7E6i2dxtBlG" +
                "KfBb5poin56ASYNLEc28GagQkDhpH7VEb0oOukKGyLldszZRni5K/rnWTayoLIq6QG/IiZyvximkJBmB" +
                "1m0ZKR78T760FL89i6pLG9ulbQjLWezTeU3n+PRraaAo5C8y5H5aPZOvUtCwbLkEa8qutc7PJNN3Cpkk" +
                "EzM4k8GZBKZcmc6o8MWkAcHO+aoFKZ8t3PNwx+Fvh9ZAFayekrkmOBUQT6kHGcSU+8dYJGTlI88gnmOS" +
                "+Mj4y6bnjbfsx5PK9IhyFfRG8YObk6DTUsrEYZufb+NDOd4mUfpJJY2joPCESsDMJbRRJCloC5Xx4/kN" +
                "QgAHx4bqiC+fK7vpPoQTGbIzoc6QYUKaajsdbEHUwimdyqHaw9jkBnv7uzC6KR+Xa54XLw7eIdprbMpL" +
                "W2r9OQSPmGimaivM6izOTS5xRCqxNLdk6CKcIwrYPFJTCRWXOPbEMSlYnCfQ/EXr6ulGuaAyXid0gZ3r" +
                "IlqQVghlLCjDUkbVJBTcIY+Mal0p6IV6BNYMpiJmldLQAnF4dhC3TSEQ4nCKDl0857Uw0dSmykwX5BYW" +
                "zVHTDcdok1ThbB46NdouUwl3t4iY6ireGhUJSP00uXJWU44Iy+MLEAkSGNhNSu2sFLT3vlzd0Ark0Km4" +
                "S/Uq3SsDLC14lon/tn92bBXYLM+e/JTZtXTkRHv1qrHkN44cr1aQfDhD6ECPfMhRnlHFUbnUKX29VGwh" +
                "mK8n0lDvSFIqXYp/CLDFmKU06mGWmJMxokA8JfJylG9DMTZBO6p658t2XZzZoIHuszlap2K9cupQSmKk" +
                "ExCE2ynE2e0ixoEKnnpgPGJqCezGfaODoxJuswwxNYDAqoEO9pSZb2DGVwC+sF924sKPVTRv0W3oHBTa" +
                "Ypz1KywSbMDzfDbFxJ77EJhrW7nYADEU7IOgvyiKucsiER7V6LvBtcgPbfUYs/ixSmEnikzVtqwO/PEo" +
                "HXBQ4VcBAtu9j3VhoERUD1BVIAtxzkGc4xEoe7KG+r5zcXH2kncaUvStbTbN9MIe3d3HmU4XWBtjy51h" +
                "53WooGtfQ/AiL6GDqhw83WwYSRwd2c2G3evBh+7ZK8vZcomxDKvc1HNHAxEbgIl0f/T6+xy7qpwXOW5B" +
                "HxVWb266/YuzEx+sy21370gbNSF4rqxDWL1Th3CIEE6Frs1dFCZHiERNc25hj3AviHhGJygykLCLKWXQ" +
                "jZQBgUaWTBLRKRM5WKrM9wKAFx6xcvWw2n1/stj55aDT2P+X/4jB2x+652Mcrv7ri+0fFtD57x/TUlyl" +
                "8cqU8qMNdBDlcKCFra5RPIDBchNUqTKcocz4vM+PGPjgSeCJ9f5WLXKn/IFSdZM2vWEU5Zwqc7Y1g3iW" +
                "imjiswKgKXFGkypBNinTpO2H0aD/As/g7fjtY+f6SjCKluh4g4ZI7D2i0qViNHeyKUeMthJwqaclulRr" +
                "xOkO7ZNn0fxH6zsoc+5UW/zp7wco6IP2wTmWRBdvD5riINM6hzfzPF+2X7yAFkYmIPT84B9/skxmVGyl" +
                "mqd/qQ2brEVbFaGSKnLAyjPOD2BRHFLHfaeUnb1PE3DdSZxAn9Sq59aa6eJpLMvRNd4Xb9lICAvyhYHA" +
                "bs2DMzKzAmSFoY/nqKZN59ZAo2WYngVhagsvBX6JgoCXm4Jof/P9d68tCCZqHjYA4DbZB2630U9XAvRn" +
                "FB7Een3VNx/9mrx3IBY9bScOVjNz+sa+wsPvtvjm9ekJP8OCDEFirJYdDJQKK51Fm++xuEGG3C7uNN9+" +
                "XuioSBCABg25Xh54G0dzf6pZ/2MVBl2qIfed6AdoLJdoeU0RrqFEp6ovxEGrnVa6vilT/rAZzMxNKaEw" +
                "mrh6AZBhQsD8T77J9ffLJvzXatCJ0Xfi7eDns1f259HN++6we3ZiH88/XvX6F93h2al7Meh3z143rOm6" +
                "uEVZCGmyUPi+4YCiGNKxcddNStDysK+EcGtw1IXkVxdUwNo8OMbOh+5GsBA4oaO4Hlz4OijXHHDya1gb" +
                "xa/AOJHKTcjPTfGRzwJ+qdKMQqbeS6Wz3A+ra1EJJ7ZxpDx/IPRWKdvg57OXlaePXtb49AuIukoSy99S" +
                "RRM0VDsGUvjXnioYLJM4yFB8Zr4zGcUFkmCbJbagVk2vwbBz0bsdAT3VPZ2SCScqmE83WSpsOjTToEGk" +
                "qyXpJMdu9YuQUJC0RDmBrOEN3nd7796PxSHitg9HJU98FaUi8ZKnea1Dc74gDtEXjng/jHpuH+bO7sMP" +
                "lX0e2wUnk052rD7b1+ze81ynPFxwn2B92Rts+iSeGsUZtdItdpl4WdoQyRTXY8eK9l4sm/aM7Gsr1MaG" +
                "J1r5eZPaYB6Mq+KpW8ClYBDwaULcdr9A3ev2ZU8sVjcHaqQtLBj4O1+ZQWlXJqYt0eDJr79KUBnpV+Ce" +
                "i8E49ePQ2pCreuVHso7r7H5hrvv0KQhbUZd4KqRi94kBwl0Thawo0xnUE3+pRNh7mRQKG7UpXjHQlUuF" +
                "wCMek0LxYz59buAeY4uAzqQsroYNHnYU6Fa4Du0O754RAFGzQ+Z0cYQXPZOoHBs7RObYOjAlUXwf49Mp" +
                "06keAhorPgu11MPvvDnAJ+uqaYcB5bDATxTkg/gaJ4lfi/A3+CsSZ+IlKkuK9hkYuJp+evkZh5P+8RU+" +
                "hv7xBB8j/3j62Z9ffHr9md49lQC+MAjcOE3deZi6scQZGjnvkynuC3S7CEOXC6o3u/mA3d8bUDG1g94R" +
                "PzXdoQx8hQcZhiqxjbj5jFrSdWi+dPXZT+Ere3EqUw94+UhFLVeH+uGJjQaY/dxUgi5+ZyhwaTbOvClG" +
                "bHDZAtYbO+6Jme2LYsBO5WWNre0rZFHhRhOQ+wOcFPGN/me7t10xwC9NqtkSq5ftN1fU7gdVVm5ema7i" +
                "eCarfYS0+i0XW7/S7WcyHntPa+Mk317JUZxe6Pob3YjZYZlOv2bzssTuosPeYDkGUbkDrBLXIV9icfd3" +
                "Ni8MHTknZIjyMlMFBZ0R8S+pKBq10nWTSjVjXpRW/KJuu/v2+rL1F/Ig23bYVx5bxZe4F6gtYrdzYDhM" +
                "FzbbVn2QkLUeuVXyiDb/OwHx94hxOtlUKw5JnIlV76UdYlmhxRv85YSjP3Z5xg+C7GBVlofdZQR0pqnx" +
                "jsTGNe9Hrt9UwtnWFmktrv17+9St7L8aETd/k6bBd1YhffBUET7jLVoQwVzexzrbswCj2/Pz7miEM3l+" +
                "cdnpXd0Ou2ff4x9cy69vrjr9fq//LsDv3YuzY7+g1//QuepdBNeDcW/QDxDw7PjEfa28DSxkZ9y9CN5+" +
                "DLr9D73hoH/d7Y+D8/ed/rvu2fGpWwcd1Xg4uPLbvXYfbvudt1fdYDwIOj/d9qD1HnX7o8EwALSds+Nv" +
                "HNi4dw27DG7HZ8dvPA/Dbvf6ZozovmWp+DOFP5f3Jo37rTL+ZSTjJTXuDMcB/D3uAifB+eDqqjcC3kAU" +
                "L3fBfOgNruDfUXDTGb8H8P5oPOz0+uMRLHhVCvbdoHO1ie+k9vH3EJ3WICvf3CrU1OtyN6esd8PB7U3Q" +
                "71yDzF99s/V1AxnAvNmEGQ7eDiyr8Pnbzc/QPP/o8H+3+ZHH/+7z96wLe/O1LvTLIcAEQEZ/dDkYXgfO" +
                "Oo9PXpWWYgUHRtQ9/xFtFGzkAwCioQCkl2aFZPybPnoBWjPq9S8H/uNrpqxiGnXq+oOg92MwGlzdjklx" +
                "p0BU459lLsC1gzkAAA==";
                
    }
}
