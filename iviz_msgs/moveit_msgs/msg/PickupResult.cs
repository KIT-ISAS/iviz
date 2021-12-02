/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PickupResult : IDeserializable<PickupResult>, IResult<PickupActionResult>
    {
        // The overall result of the pickup attempt
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
        // The trajectory that moved group produced for execution
        [DataMember (Name = "trajectory_stages")] public RobotTrajectory[] TrajectoryStages;
        [DataMember (Name = "trajectory_descriptions")] public string[] TrajectoryDescriptions;
        // The performed grasp, if attempt was successful
        [DataMember (Name = "grasp")] public Grasp Grasp;
        // The amount of time in seconds it took to complete the plan
        [DataMember (Name = "planning_time")] public double PlanningTime;
    
        /// Constructor for empty message.
        public PickupResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new RobotState();
            TrajectoryStages = System.Array.Empty<RobotTrajectory>();
            TrajectoryDescriptions = System.Array.Empty<string>();
            Grasp = new Grasp();
        }
        
        /// Explicit constructor.
        public PickupResult(MoveItErrorCodes ErrorCode, RobotState TrajectoryStart, RobotTrajectory[] TrajectoryStages, string[] TrajectoryDescriptions, Grasp Grasp, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.TrajectoryStart = TrajectoryStart;
            this.TrajectoryStages = TrajectoryStages;
            this.TrajectoryDescriptions = TrajectoryDescriptions;
            this.Grasp = Grasp;
            this.PlanningTime = PlanningTime;
        }
        
        /// Constructor with buffer.
        internal PickupResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            TrajectoryStart = new RobotState(ref b);
            TrajectoryStages = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < TrajectoryStages.Length; i++)
            {
                TrajectoryStages[i] = new RobotTrajectory(ref b);
            }
            TrajectoryDescriptions = b.DeserializeStringArray();
            Grasp = new Grasp(ref b);
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PickupResult(ref b);
        
        PickupResult IDeserializable<PickupResult>.RosDeserialize(ref Buffer b) => new PickupResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            b.SerializeArray(TrajectoryStages);
            b.SerializeArray(TrajectoryDescriptions);
            Grasp.RosSerialize(ref b);
            b.Serialize(PlanningTime);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (TrajectoryStages is null) throw new System.NullReferenceException(nameof(TrajectoryStages));
            for (int i = 0; i < TrajectoryStages.Length; i++)
            {
                if (TrajectoryStages[i] is null) throw new System.NullReferenceException($"{nameof(TrajectoryStages)}[{i}]");
                TrajectoryStages[i].RosValidate();
            }
            if (TrajectoryDescriptions is null) throw new System.NullReferenceException(nameof(TrajectoryDescriptions));
            for (int i = 0; i < TrajectoryDescriptions.Length; i++)
            {
                if (TrajectoryDescriptions[i] is null) throw new System.NullReferenceException($"{nameof(TrajectoryDescriptions)}[{i}]");
            }
            if (Grasp is null) throw new System.NullReferenceException(nameof(Grasp));
            Grasp.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += TrajectoryStart.RosMessageLength;
                size += BuiltIns.GetArraySize(TrajectoryStages);
                size += BuiltIns.GetArraySize(TrajectoryDescriptions);
                size += Grasp.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c1e72234397d9de0761966243e264774";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbXPbRpL+zl8xZVWdpESiHMnxZnWnD7RE2UwkUhFpbxyXCwUCQxIrEMMAoChl6/77" +
                "Pd09AwwhKnZqT0pd1TkpmwRm+m26e/qNrdaludW9spvnJj81sS6Upo9BhM+tVuvajE05LMNSqzIP/6mj" +
                "0uT3QVGGeenejqrnnz43Fk110WoVZZ5k0/V3wBPlyaJMTIYVrbd5WCzUlP7Gt0lqwvL1K7VIwyzD1qBM" +
                "5rp18r/8p3U5fHus5uA+KYN5MS0OmpJoJVl5dKhuw7TV2lJ4mYdpqsZ6Ft4mJrdvh+9PT7vD4cl39vt5" +
                "p3fx/rp78nf607IPry46/X6v/zagt92zk323utf/0LnonQWXg1Fv0A9o3cn+oX3pPQzsws6oexa8+Rh0" +
                "+x9614P+Zbc/Ck7fdfpvuyf7R3bb6aA/uh5cVLhe2efv+503F91gNAg6P7/vXXeDYbc/HFwHANo52f/e" +
                "rhr1LoFi8H50sv/aUX/d7V5eAfPJ/t9IEu5c1H+omyTT87BMokLl+relLkpRHye74ahzPQrw96gLFoLT" +
                "wcVFbwimIIGXG5Z86A0u8O8wuOqM3mF1fzi67vT6oyHWf+eE+XbQuWgCO/Tf/RGUI3+h98ptorN51Wqc" +
                "ztvrwfuroN+5hJS/+775sgEJS143llwP3gwsi3j7t8bbi17/Jwf8h8a7wZsfu6cj9xb6tKWK+6LU83Ux" +
                "n19jQQAC+sPzwfVl4JRw/9ApWiUsqEv39CfSRejDB6wjpcBCJ0GPVvqb3zmhWYXp9c8H1TsIa8tXgzW6" +
                "+oOg91MwHFy8J02GiuIQn96Oa58F0kazpFBzXRRwRioyWRkmWaGSbGJyothkKhybZanKmVY5bVTwW6Xe" +
                "U0lbt/npwhQJOyplJiopC/VPA+4KFWaxSpPspmgVOivgMhn5j/RSHCavIzcIQkAJvykAMizVPLxXcCNa" +
                "zZdpmSxSrc4G5yrMtSoWOkomiY7VTOd6DfQlrcU6DwVvD2IzCRrIOmUZRjNAiUyaJgXxacbkfQu1E7p3" +
                "pVGFmWvmQmFFJYPdltt/6rYPeDe8uNuNG8K+CixkwnuehlNIN04iCBcuYjXTgJoDNI6hiHSmFT4A8Rgf" +
                "slLni1yXICWEPFWcTCZqlZQzOBOSQ1lRaBgI73dnSsdqijK9V8l8YfIyzEqFQ4VYsxgMTZmbitWxiRPc" +
                "bTuOHizMDMOPUh3mmxaTz5+AKlGt4+PI5Pr42LsQxxr4tFouYuE1KYX40lO53dbYmBTEBsTcU2n/ZgX0" +
                "JBVWJsDqNzNpXCiQHZIE5DLGgZAQWIOE8UKX9AEXNjw7206OI4d8xADaqrXFKLxNGgKU12on17cmXdLz" +
                "XC3ypGAHsUvUxHoChwE53x8DgPpmzcwYJb47KGFMAOa7e/XSW52aKCnvHy49KHjxQbHL5llv0ROcVSnc" +
                "kzwWi5RsLMl8AP057e7vtpmxbs0LdiyzBFIgbYt1VoqFjvENBpWFCE9EEDMdxqSo1ojJ2iFWhC9QLthC" +
                "Aog1PpZaoVawcyg81CvWcVt1EGM01wA6KDUwHneM7DzEjNxWQsEnCHIYp7DRdIBFUoBm0WznfMI8D++L" +
                "PcZANsTHuECMty5hJoaOnXmdmpAUm6iYhzdaNtn14J00zHCEF6Zt9Y+ZzpRuT9vq3ixz50KZi8wAoD2f" +
                "sChwssASO1PS8z3aoqIwU7BU4ro+TqZb6fmivLfaSNITbuRsPd6LmVmmcKsOBsupSH6HrwfLEKTA8ayG" +
                "VpkMZ74CFrBZ6UBFpicc0oKKaAg6x7mUdF/zCbYR174T5RAd8eJi1h8b9OKrMwTvkVN475FI4MkcShmL" +
                "NxGawQY8ShaHeQxxliF7Dna2yRTedD/VoJA4nS9wcuJX7hfEdS3MKXw3BdD3alnI3ROZ+RwixU0h+rq2" +
                "X1Q+ZC1MomUKHx0Z6HmS0fJJDpkRdBIwBZ5ZpFXv7JgVXEfLMgFBsM4synVYkHPunanWUqISbGhtjVZm" +
                "ny6gKV1ODnnlH/Qd7qWC6AwL8lHfCHNtwCZvCyzQ7h1+FuAr3A2QgAS9MDCCHVB+dV/O7JV6G+ZJOIaZ" +
                "AXAECQDqNm3a3vUgE9nHUIXMOPACscbxNWCzCi7xtF/dhcVyCgFi4SI3t/Bg7LtYT+EGobxpMs7D/L7F" +
                "ropRtrbOScZiQnwi5DbXzdOqsJxGkMTPcb09DILA7LWm4wIjEtHBCcmNRCpqXQ1bZuXwYj3NNXwvVk7w" +
                "ITbwMoAzwf1mVi54AHfLqFzCO2NZjU/cak/ukaJYzkmbSW9Cd1uQ3townf1AseADhcrDLvIwKyj4lD1T" +
                "Xda3EcCGqbHYq5haRTPErG11To75DkeTwmOFnIXhTO3VhfgJaN9fn53zDXtE4eTOHVwn/g9XpBB0H0J3" +
                "Ci0vyZ+Sz/MU3adOBIl/8gRQZC/dL2vvAVVWOGhQXGTJpB7jMLohhtdo+P9L9Xkv1VUOvzj76kvVLf+/" +
                "dKk+dqdKOkTbi9ZUI4UoUXViBzJyKiwVKfn8YNEKURItoH8b7/7BYsJLkddTOb1HqHaSzJ3LszFm5VbG" +
                "ulxp6EW5Mg9uTD4/cniIUMMInqz1gQtyR7I/Fav+eYkNeUYOIDfiUp+HSUvMBhZDhED0rkG/qhwxa9Rc" +
                "UxIInap2clJJOgMe2mRgOedsyO5LFRvIA0kgezGYGt0yHFOTO4Y6+jKhx9iyQ8a2R0ltJqvoquBYhaMb" +
                "+Oo8mSZx042y47fM7alycgiVhkkxzYIMRwggTtq7bdWbsIGuiCE2bpesUZpm6eLLvzRmjyIqC2JdoFds" +
                "RM5WUfgoYSftusJ6V32qQkv1+7Mcda1jm04bV3hOcYmIb+3M6dtvtYKSkL/IkPu0eiZbZadh2XIXbFFn" +
                "rev8jHNzA3XCQZGKFVSMoYIEXblhNuXAly4NODtnq3ZJ/d2uex7uxP1tODUchRxPzdwejArE89VDDFJQ" +
                "/3UsMrD6q9QgnqN4+Ejhy17Pjadix2OvdMR3FXKj5M7VScho+cqkKpuLmOkz306ombEcq8okF6IQdSIM" +
                "KGYhcigWE3JCfKJwlt4/oE38gl/Xo2WMcAueBPDYjui4iFcGmRlbEWzDYVFxTpcI9Mgt2Wre1iZ4rrYn" +
                "GUTFRiUIQRC3moISoC1XL5LKEldPbWDp199cpZLqoaDIrCwNZsmKB8SoIIWI5dV+RZiQQSF5ipQvvpdS" +
                "B4ICodRu8JpRBCyQ+i3dIhMVIXLFEViquCqDGDuSKtFaIorT4LRAzoNuH2GSb545wahYAWh7acCpaYgd" +
                "ebvcZFGKPJ+uj9ws2RAslN09Vw5jHJmOyH3n94wt1wjXaBtleBRICWI6PsB0RWOv0VaXBOs2HYRB1AUW" +
                "hTuUlUYyXwWnjcPAjTlRN5lZVWUJu/45bPKhLXZsxMdXX8yiqarJLn1jm2kGiMIqNN6yaQW4w9rDsHB6" +
                "0gjcdcZKia3sc+eM2oYoBd3K4xC3r7HSqaxH/g2oODfNuJ4jvAgLI4JAYBzkukxvvS0VDzcE7s5mZVvC" +
                "ukgroSnNwjmH4143wQlgaFLw79BEVJudg8BbCIs9jtDJq67cKypL1cuaQUax9p70ivq+W+pSF7N1qPQE" +
                "a+fyYiMceleDeEPGQYdA6S4V8FHssVFBUXO3p8a2hcPLXDoqQQSYX4ql4cTimM8C3pVQ7Pq0XdFWYoQx" +
                "PcIkvaup68SU1nuKIVKvGqTcsOBgzlsEHUX/eFkg7NN3iBSIfMSWcpmyw2m3xvcI2TtnZycvCc01O9U1" +
                "TJPcUAUB+VV2m+Qmm1OwS4VpeIh7SAlpOCpFYgrcbyphzEVDJ5J4VzBddy8HH7roYRNPiwX5KYpZnTbb" +
                "8oZ1rEy0zQS/xKuLsWWT4xOnUDN5ddXtn50cWidc49yMjrHswSuurObbo+Zgf4dWuHNzGet8iaY0VqR6" +
                "Uko2StUQeLPCpCQriNZ5jNqboiUCScZCIsvmiAgcLFDCdCE9YOIrBaBuoXGvn8opftmptLb+9B8lTWaq" +
                "kP75zfYPCef0j5ur7DS57TDhC886MrgxqklRtoqogCtyFDHiCHVOZZCp9OuqKoH0jqAn1J5bCypudNUQ" +
                "8jEc8xPZXzdvOJNkdYHHylQ8ds4eUBzAeOyTYi9YLpP9OBz0D1C+drWzj53LCyUA0MCpVBhutjIAL8Uk" +
                "R+2kUtcH5VJ3F0pbdTlqoLbPg0NnO+LajTE3iFdu9LF68a9tkvD28fYpRTZnb7b31HZuTIkns7JcHB8c" +
                "IP0IU0i73P7vF8Iibg0oOyJBLtxl1jPK6dnohg7HkwJFjkm5jU0Jgn1YwY3Wtmw+SWGq4yRFimOvp036" +
                "Sl1UEaJLmc/eiG4wEOKK7N5ilpIXKdcSciIXJwVQLspTQdQyy31EBnOsKgHwMxIBnjVFcPz93394JSvo" +
                "6pUKAdY9pHjbYhr+fIG+KUIEap5W57SGePhb+s6tENiMSm2vpsXRa3lCrepj9f2ro0P+itU5LUD4bFZ2" +
                "Ba79Feo2jccUoRAjDoHrusvbuYmXKb3nqkBpFttOoaHaT1WWfyxaAEVnYqZjg/pvsSBN21PRPUJrDtqg" +
                "blrZwqLLcqAWri8MtXIFRUQ4YxcCABg5fLrS2RIlcH65h/9QAqDmzg/qzeAXXGPyeXj1roshmEP79fQj" +
                "Jm3Outdw5fbBoN894fmVkeef+JYhmuwqidKcS0B/BGmFHQapl9Z9uXqF20NVKSLf3+AtO5YaL6UrPMMg" +
                "QpCrmsR15zzVdr1nWy43HriwOSEYZ1Ile/hlT32Usv2vPs0kZE6YdDZFsGgpavogSpsq/iD0di3b4BdE" +
                "JPW3j5Ws6duvdIt7JIn8LVVc7KJjJ7eJf21XHd5T6IRHI1csfKOTnyyJBJvmiAY5OgRucN05670fUoTk" +
                "4XSHzDDpgKURKVIR1eHyA9cMXXjITReL6lcVIuBoq7pYuAY3eNftvX03UjsE237ZrXmSmRFP4jVPs7X0" +
                "ytmC2iFb2BV85OccHuHO4pEvHp7HsFAR0clOjs8mJ5tx4sqWYoB7RdN9VZzftElq8CQ5p8BcS0WPcVHr" +
                "EMuU9lOySfq+XOzZdta3VqjOSBvCrFSqwTzFo7WlPlhcC4YWPo2Le5gEcPKZP+g3UjDarH3xaVF4IO9l" +
                "uoWk7RU3UcKWIm3V9feq796652IQpLjS3lpJyp/OQS3E9Tlrdr9Qgn36K4hSS3fxeKRSOkkOAtm1GB+a" +
                "5tkUEcR/eh4WI76YXcWxTmgawHgjf+CROpoIdopPn1uEY2QBcPvIwiIEXuHO7XC5FyK/JQVJPL4ze5BZ" +
                "QpY84yGbnklUjo0NInNsIcqriJLRiU9HQqe+C7gO+CzUcl6+sckvTXBYmuT3df5fFQnCO/Utlf++VdHv" +
                "+CtWJ4oz6lAdn0DB9eTTy89UUay+fkdfo+rrIX2Nq69Hn6tWw6dXn/nZUwngCzW8Rl1rY9+zscUpGhtv" +
                "8RfR7TwMD9fVa61HqefmUPujtK8yxE97rn+Ct/gSRhFKoZJtF5/plMz6apmP+lzVzD1ccpXpO5oTojqE" +
                "jUOrsoj1BnT7uaoDVQdpyA5Ni6LRnmYf0eCyDdZbG0a6ioczXTRoWj9cY+vhtFe8dOUH3P0B1YDcTzGe" +
                "aaraU8AvFZlFE8vHN6xN8ngbmzPNHohn0tlHKFuf8bTRK48os+rYgapGy93Ozmi5XHhOjYesNuilO90N" +
                "RetNIYed39yHpFynqYa1IyOcbnq1Odmz60xQVtRTRx4IbulgBi1dxsSFnQvxYpnioNbhg3XN3bJzxtZa" +
                "2H5s0mEfVdA8S5JMYG2TGJ1bRiVxyhubFsjAEF5udIOPnOZf4w7/iBh3Js1jpZqIUzF/gGyHggqjXtNv" +
                "B3a/bsqlKvvYsmlYd6Vr/+dUE+HnlOpP/rTSI3MynjN7gAKa4+nGv4dnXcv+Un/Iv1nbMF8tMTNaQ/VP" +
                "3MRa+edtYhcrHpqCcMiD0KCWdI29FiNK8PtVqxCHLJur6vieNUwKe2ZmhRUU1Cww3UkKlOD9FLj5gy4j" +
                "zi49KquipCUXcMm5PazbhuqFIGYjeYExwzoRGPS9mjmNzGMcDV4P4aEd9aXZ9kdnxeou7XxM+eVG9B5u" +
                "O8C5ubTsdEWyb+mKrIuPUjH6KQKvJqjcw2lOs7XF1XIvsOrtyq8SH7QCud1o+0huRpTTfHJhTrPhsvdl" +
                "/5YUQOrEgqILPv0v3aOAEVQk+/3ar6LAYfcQSxMa4cSfIGIjAc1fbKwJfY0Ap4B2/I9lXxV3XkDtca9x" +
                "L/7FJlh7rKuSW8mslINgFZcHE75Jsm8ebCXE9wsMLMhG4K9MzwcjlRP6ORGqKvAgKWb7KF9kwPZnPpVo" +
                "d3wEu3ZcYKynYdbe0DQc2qH2Wuuc+DBynkCV8Q5mOw6lcs6zy0t4uaJoqCAPVNY/hiJ6MQpgz518wIup" +
                "MfELabG1q9Rb8CIfIugOdeUpquE4bl1Somq5hcxubM/UdlnewqGgvzXyhuJq1XQAHQK04WHf5Qb44YRK" +
                "cM4b0l0/pmlFMlDUrSGMnboL52YQdjcix2lZ7BZbE/ncOMy2TyeDe9UMhee/qBhA+lm7F3JI4iTXJzAq" +
                "naZJJp1OpPX8KGD61ZLUGWrH5WZlKsBSTROoLqZ0JId8FEuyYe4QwwCpHAe5+aPECA0RNKDUbmeNfHQk" +
                "yNswSXlckFggi6EaE+FqPypabkX7ouX6Bep48+VcLg5qQ/PMF03xUl9rlqSWDRLCzn+dvOSflyUFod4V" +
                "lTw6JCCBBRDInBePp1eDzVWtxoxhixHqK/bWtD97KjBjgxJj9PA2qMbcbSeJzJyaQTo+WCwxchAf8HSS" +
                "S+siDFaKA3BE1xGknTsKZE6o+oXj08QVj7oNvo/ou5vucDnE5jTD/Y6lkUAwAHE9zxEXNfWJSii2qbE+" +
                "VFvbE2+xwzw4HFF5TulJC+E5Coxp0e8zqp9A8Oc0mXCpuZ4aMBOuYLGuY4HzS84o5cXEAqYDt4pdOypr" +
                "Uf5A9MYpZefYq50VKJkdWOOUbxWcW2UDdlFQvXAGBu7dM1s+tPML3jzbhNwMMVJfTTT6K16pUIjBeAKN" +
                "hh2gQpXZJZmH7mkU4Y8ktXGs2014/jvq7WDIiHir9T/GuYXa1EIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
