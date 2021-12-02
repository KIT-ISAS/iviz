/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MotionPlanDetailedResponse : IDeserializable<MotionPlanDetailedResponse>, IMessage
    {
        // The representation of a solution to a planning problem, including intermediate data
        // The starting state considered for the robot solution path
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
        // The group used for planning (usually the same as in the request)
        [DataMember (Name = "group_name")] public string GroupName;
        // Multiple solution paths are reported, each reflecting intermediate steps in the trajectory processing
        // The list of reported trajectories
        [DataMember (Name = "trajectory")] public RobotTrajectory[] Trajectory;
        // Description of the reported trajectories (name of processing step)
        [DataMember (Name = "description")] public string[] Description;
        // The amount of time spent computing a particular step in motion plan computation 
        [DataMember (Name = "processing_time")] public double[] ProcessingTime;
        // Status at the end of this plan
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public MotionPlanDetailedResponse()
        {
            TrajectoryStart = new RobotState();
            GroupName = string.Empty;
            Trajectory = System.Array.Empty<RobotTrajectory>();
            Description = System.Array.Empty<string>();
            ProcessingTime = System.Array.Empty<double>();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public MotionPlanDetailedResponse(RobotState TrajectoryStart, string GroupName, RobotTrajectory[] Trajectory, string[] Description, double[] ProcessingTime, MoveItErrorCodes ErrorCode)
        {
            this.TrajectoryStart = TrajectoryStart;
            this.GroupName = GroupName;
            this.Trajectory = Trajectory;
            this.Description = Description;
            this.ProcessingTime = ProcessingTime;
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        internal MotionPlanDetailedResponse(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new MotionPlanDetailedResponse(ref b);
        
        MotionPlanDetailedResponse IDeserializable<MotionPlanDetailedResponse>.RosDeserialize(ref Buffer b) => new MotionPlanDetailedResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            TrajectoryStart.RosSerialize(ref b);
            b.Serialize(GroupName);
            b.SerializeArray(Trajectory);
            b.SerializeArray(Description);
            b.SerializeStructArray(ProcessingTime);
            ErrorCode.RosSerialize(ref b);
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
                size += BuiltIns.GetStringSize(GroupName);
                size += BuiltIns.GetArraySize(Trajectory);
                size += BuiltIns.GetArraySize(Description);
                size += 8 * ProcessingTime.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionPlanDetailedResponse";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7b84c374bb2e37bdc0eba664f7636a8f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRhL+rl+xOANnu5WVJk7TVgd/UGw5UWtLriSnTYOAoMiVxJriqiRl2T3cf79n" +
                "ZnZJipKbFHd2ccDlDo1Izs7O+9tu9tR4rlWql6nOdJL7eWQSZabKV5mJV/yUGzwtYz9JomSmlqmZxHrR" +
                "VFESxKuQXkVJrtOFDiM/1yr0c7/R2GO0We6nOUHgBz4FJsmiUKc6VFOTqpw2NhOTl1st/XzeGNK7Ea/I" +
                "U/9XHeQmvfcYl0M8S81qqVaZxVQQd7DKVn4c3zPuzF9o5WcgT7bSv610lh82sjwlWMbhJQAirJerOI+W" +
                "sd6kJVN+ytIxaa7DptJ+MMfjNAZRW5xnuV4Wu5WUk8QCnWWAd+THUZaTkB3iEjrSmfA/LtZ/+FhBRhjO" +
                "dBak0dJpSnjbgUgdEHMEUlLARDoRAHNY4nLE+QuzSpi8PMLybAmzgOoWS4gFCGAKpNRgFfspYyOOF0ZE" +
                "Bj1YUDGkxjQ2fv7qJXYqafAIL+1GOl5BxDnzoJNQ2IkyRtS4NLe6l3fT1KSnBoQqTT+9AL8bjZP/8p/G" +
                "5ehNG3zc6ij3Ftkse1aaIQsGVC1Avz9jO879KCFdw/oWwqo/Mau8atO0El7S0i1+uzRZRIAZMRnlmfrV" +
                "wHrAPdiOo+Qma8D/MvDHm39PH8UHGI7MH4SAEv6SASXEtvDv1dy/1WrhrPdscM4mC60F0TSCRczhbxuo" +
                "2dIBV9mCl3uhmXq1zTp5DosHlsDEMFq2uAkZGIzLd98QHzIDSyEuFMULJ4PDhlt/6pYPeDXMwa2GOu0n" +
                "z2Kmfc9jn5wrjAKfjW4918BKIQNqyAKdaIUf2HiixQcRvsj84e2+CqPpVK2jnDyV5JAXFBpGwuudTkmt" +
                "JssRMqIF+ZAPY6eQModewNCMuSlYnZiQHcvRA8DEMP4g1vCHHcDYiMOpmFa7HZhUt9uVGDfR2E+r1RKR" +
                "k2NKLsTnFZM7bEyMiUGsR8w9lvXvNsCKpPzCBdj85iYOMw74JAEJJVAIx15mTfKIlmBiUsRf9p0UKod8" +
                "xAFaqrFXpgtZxGGWP6uDVN9SRMZ7xPk0ysjdgkOiJtTTKCE537eBQH2x4WYuMjosPoJLqhaHzRL0Vscm" +
                "iPL7bdBnGQM/yw7ZPcslegpd5cI9yWO5jMnHbMy3CPoLWt0/bDFj3ZIXrFglEaRA1hYirIqHTvAEh+JM" +
                "JIKYax9psnBi8nYt0Rj7rucRMJb7sdQytYafw+BhXqEOW6oTx1swwA5KDZzHqZGDh7iRW+rCMWdP2lPY" +
                "qAfADCmMQ5lfBh8/Tf37rMk7kA+xGilbbEqYiSG1Syo3Phk2UbHwb7QssvDgnSzMcH7y45b6aa4TpVuz" +
                "lro3q9SFUOYiMUBo9eNnGTSLXULnSlSwYIkKkKDgqcR1qU6mW+nFMr+31kjSE25EtxXes7lZxQirDgfL" +
                "KYt+R6wHyxCk4Kl4DUGZBDpfYxewWdhAQWZFOJyyHdEQdAq95EAmGmw1Go23YhxiI40ylbP9VPKtdYTK" +
                "K2fwlVcigUcLKHko0URolpSfhH4aQpy5z5GDg200QzQ9ijUoJE4XS2hO4sr9krguhTlD7E65xOPqjyKv" +
                "WSwgUmQKsdeN9WLyGzVLYGDnUULg0xQyI+wkYCoPk0Cr3lmbDVwHKHhAELwzCVLtc/nUO1ONFfR0/IIW" +
                "NPbGa3NECWhGycltXsQHfUdlNdHpZxSjvhDmWsBN0Ra7wLoP+J2HR4QbbAISUM3BCQ5A+dV9Prcp9dZP" +
                "Ix+lNyEOIAFg3adF+4cVzER2G6aQGIdeMJZ7fA7apMBLPB0VuTBbzSBAqs9Sc4sIxrGL7RRhEMYbR5PU" +
                "R5EqhSNt2dg7JxmLC7FGKGxuuqcryFkbXhQ+RXrbLoLA7HCrC5KMRCZqQw17ZhHwQj1LNWIvIKf4ERpE" +
                "GeCZIr+ZtSsewN0qyFeIzgAr95Ow2pM8kmUrdBFiN77LFmS32T1K7IXEgWzJCuV2DHV+klHxKWtmOi+z" +
                "EdD6sbG73yBFcspUwRw1a0udU2C+g2piRCzp66BTm7qkW7oenp1zhj2mcvLgDqET//fXZBCUD2E7mZaP" +
                "FE8p5lUMvUqdCBJ/pRGwyFrKLxvfgVUgHDYY7q3mdmbiBzfE8AYN/0+qT5tU1yni4vyzk6oD/19Kqg/l" +
                "VGmHaHnWmGm0EDkmERxAxs6EpT2X31tAa1RJBEB/1779xGLCR5HXYwW9B6h2kiwGP7bGLMLKROdrDbvI" +
                "12YrY7L+KOChQvUDRLLGO545HMv6WLz6xxUWpAkFgNRISH0aJi0xO1j0UQLRtxr9qgjEbFELTU0gbKpY" +
                "yU0l2Qx4aJGDpdyzobvPVWggDzSBHMXgapRluKamcAxzrMqEXmPJATlbk5raRKBkrgIMXN0gVqfRLArr" +
                "YZQDv2WuqfLpC5g0XIppls2gQiBx0j5sqd6UHXRNDLFzu2aN2jRLFyf/3JgmVVQWxaZAr9iJnK9i8JHD" +
                "T6B1W0aqu+JXUVqq359E1aWN7dI2UjjmYC6db+icnn4rDZSE/EmG3K/1E/kqBw3LlkuwWdm1bvIzSc0N" +
                "zAmKIhPLaBhDAwlKuX4y48KXkgaCnfNVC1I+W7in4U7C3w6tQRWinpK5JpwKxHPqIQapqP88FhlZ+Sgz" +
                "iKcYHj4w+LLpufZW/HhSGR1xrkJvFN25OQk5LadMmrK5ipl+uwk2y7GYTPIgClUnyoBs7qOHYjGhJ8Qv" +
                "N3vfok3iQnWuR2C84R4iCfCxH5G6iFdGmRg7EWwhYNFwTuco9Cgs2Wne3i58brYnHUTBRiEI2SBs1AUl" +
                "SIvjBZks8fTUFpbV+ZubVNI8FBSZtaXBrNjwsDEmSD5qeXVUECZkUEkeo+UL76tnFuWCskBgZJ7MbymL" +
                "TFWAyhUqsFTxVAY1diBToo1GFNrgtkD0QdlHmOTMsyAcBStAbZMGghoNytG3SyYLYvT5lD5wmsGOYLEc" +
                "Nt04jPdINA3f0ZvxbqlGucYHEntSSMnGpD7gdEPjyuFLORIszyUgDKLOs1s4paw1mvmiOK0pAxlzqm4S" +
                "sy7GEhb+KXxy2xc7tuJrlkdJxTTZtW/sM/UCUViFxVs2rQDlwIVxQXtyflGcN1FjK+ucnjHbEKOgrDzx" +
                "kX2NlU7hPfK3R8O5WcLzHOFFWBgTBkLjMJdjehttaXi4o3B3PivLIrZFgoSl1AfnXI5XThOcAEYmBv9u" +
                "m4BmswsQeAthccQROhnqyn3iY6ACrF5kZBvfya4AREdzOptvYqU3gF3Ih5146FuJ4jU5B59eot2lAT6G" +
                "PbYqyErummpij3AYzLWjUkSA+ZV4GjQWhqwLRFfa4rBK2xUtJUZ4pweYpG8ldZ2Q2vqKYYjUi1NNPrDg" +
                "Yq4CBBu9jcwqQ9mn71ApEPmoLSWZcsBpNSb3KNk7Z2cnX9E2Qw6qGztNU7Owh2+3UWqSBRW7NJhGhLiH" +
                "lNCGY1IkrsDnTTmcOavZRBQeyk7D7uXgXffkOfO0xLkhx9siJNnxhg2sTHRxXPrHvLoaWxY5PqGFksmr" +
                "q27/7OSFDcLlnru3412aiIpra/lW1VzsHxCE05vrWBcrHNkCItbTXLpRmoYgmuG8mGQF0bqIUUZTHIlA" +
                "kqGQyLI5JgIHS4wwXUkPnHikAtQBGvf5sYLip4NKY+9P/1GD1993T8c0If3zi+0fEs7pHx+uctDkY4cp" +
                "JzwbyBDGaCZF3SqqAp7IUcUIFeqUxiAzOa8rpgRydgQ7oeO5jaLiRhcHQtUd2vxG1peHN9xJsrkgYiUq" +
                "nLhgDywOYTipkmITLI/Jvh8N+s/ouNzOzt53Li+UIMABTmHCCLOFA1RaTArUTirlfFCSuksoLdXlqoGO" +
                "fbaUzn7EsxtjblCv3Oi2+ts/90nC++39U6pszl7vN9V+akyON/M8X7afPUP74ceQdr7/r78Ji8gaMHZU" +
                "gjy4S2xkFO3Z6oaUU5ECVY5Rvo9FEYp9eMGNxkUCjg+4W3EXTaIYLY5NT7vslU5RRYiuZT57LbbBSIgr" +
                "8nu7s4y8yLhWkBOFOBmA8lCeBqKWWT5HZDRtVQiA35EI8K4ugvbX3337UiAo9cqEAHDbFO/bnUY/XuDc" +
                "FCUCHZ4WetrYePRb/NZBCG7eSu2vZ9nxK3lDR9Vt9fXL4xf8COiUAFA+m7WFQNpfY25Te00VCjHiNnCn" +
                "7vJ1YcJVTN95KpCb5b4zaJj2Y43lH6oW+KoLu+nEYP6bLcnSmiq4R2nNRRvMTSs7WHRdDszCnQvDrNxA" +
                "ERXOxJUAQEYBn1I6e6IUzl818T+MAOhw51v1evAz0pj8Hl297Q67SC3yePr+otc/6w4Ryu2LQb978tJ5" +
                "u4tPnGWIJgslVZoLCTgfQVthL4OUoOW5XAnh1tBUisivLqiAtWXGS+0K32EQIUiqJnHduUi1X67Zl+TG" +
                "Fy5sTwjGmVTpHn5uqvcytv+lSjMJmRsmncxQLFqK6jGI2qaCPwi9VcrW+xkVSfn0vpA1Pf1CWbxCksjf" +
                "UsXDLlI7hU38bU/VET2FTkQ0CsXCN07yI9wvMlPb5ogFOToErzfsnPWuR1QhVfZ0SmacpGA5iBSpiOnw" +
                "+IFnhq485EMXu9UvykfB0VLlsHADr/e223vzdqwOCLd9OCx5kjsjFYmXPM032ivnC+qAfOFQ9qM45/YR" +
                "7uw+8lDZ56FdaIjoZCfqs83J7j2RsmUY4D5hfVnn132SDniilFtgnqXijHFZ2hDLlNZTs0n2vlo27XHW" +
                "l1aozklrwixMqsY81aOlp24Bl4IhwMcJcdtNADef27cuqRitz75YW1QeyHe53ULSrgw3McKWIW1x6l+Z" +
                "vlfgnopBkOJGexsjqertHMxC3Dlnye4nRrCPn4KotXSJp0IqtZMUINzNTSRFzBxRQfyjEmFv/Rg3TOl+" +
                "KN0GMJUrf+CRTjRR7GQfPjZoj7FFwMdHFhdtUBncuRWu90Llh0uvBMDU7JA53/GQRU8kKsfGDpE5tlDl" +
                "FUTJ1YkPx0KnvvN4Dvgk1HJfvvOQXw7B4WnS35f9fzEk8O/UlzT++1IFv+M/oTpR3FH7qn0CA9fTD199" +
                "pIli8ficHoPi8QU9hsXj8cfiqOHDy4/87rEE8IkZXm2utfPcs7bEGRo7b/YX0e0iDF+uq161lrPw4t4c" +
                "Zn/U9hWO+KHpzk/wFQ9+EGAUKt129pG0ZDah5X7Ux2JmXtlLUpm+o3tCNIewdWgxFrHRgLKfmzrwdWxc" +
                "ssOhRVY7nuYYUeOyBdYbO650Zdt3uuiiaflyg63t217hyo0fkPs9mgG52/VPdKu6YoCfGjKLJeYPL9i4" +
                "yVNZWL/TXL1D/zhsfiZlm3c8bfXKV5TZdOyFqtqRu707oyW58D01vmS1wy6ddncMrXeVHPb+5hEk5U6a" +
                "SlwHcoXT3V6t3+w5dC4oEOWtowoKPtKRfytCXNh7IZVaJntW2vCzTcvds/eMrbew/9imw74qsFU8STqB" +
                "jUXidA6MRuLUN9Y9kJGhvNwZBh/Q5l8TDv+IGKeTulppJuJMrHqB7ICKCqNe0b8dOPy8Wy7F2MeOTf3y" +
                "VLqMf840UX7OaP5Uva30wD2ZSjDb2gKWU7GN/2yfTSv7S+Nh/R+5NORyKZIHiQAf6bIrBIBJRmRS+3V0" +
                "fXraHVHDIs/nnd7FNWYU39Gfhn15ddHp93v9Nx597Z6dHDnoXv9d56J35l0Oxr1B3yO4k6MX9mPlpWcB" +
                "O+Pumff6vdftv+sNB/3Lbn/snb7t9N90T46O7TK0UePh4KLY66V9f93vvL7oeuOB1/nxuod2e9TtjwZD" +
                "D0g7J0dfW6hx7xJbDK7HJ0evHPXDbvfyCjufHH1DkigOCP5eXmnM3L/pkn8Z5GQ3GneGYw//HXfBgnc6" +
                "uLjojcAUJPDVDpB3vcEF/h55V53xW0D3R+Nhp9cfjwCPjlAWvBl0LurIXlS//RGW4ypg5ZNbRLrBAGlT" +
                "O2+Gg+srr9+5hJSff13/WMMEkFc1kOHg9cCyeHL0/JvaV3TIPzjk39a+ySDffYU9YZYj11A3xHw+BIAH" +
                "Avqj88HwklVPRnj0whlaISyYS/f0B7JF2MM7wJFRANBJsEIr/Ze/OaFZg+n1zwfFN562Vcxgg67+wOv9" +
                "4I0GF9dkyTBRKPHfP5X1ReY4AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
