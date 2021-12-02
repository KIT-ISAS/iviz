/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryActionGoal : IDeserializable<ExecuteTrajectoryActionGoal>, IActionGoal<ExecuteTrajectoryGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ExecuteTrajectoryGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public ExecuteTrajectoryActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ExecuteTrajectoryGoal();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ExecuteTrajectoryGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal ExecuteTrajectoryActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ExecuteTrajectoryGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteTrajectoryActionGoal(ref b);
        
        ExecuteTrajectoryActionGoal IDeserializable<ExecuteTrajectoryActionGoal>.RosDeserialize(ref Buffer b) => new ExecuteTrajectoryActionGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "36f350977c67bc94e8cd408452bad0f0";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVYS2/cNhC+61cQ8CHrYr0GkiIHAz0UcB4uEDRtjFwCY8GVRhJjiVRIatfbX99vSL1W" +
                "XjcFWm+MBSSRM8N5fPOg35PMyIoyPBKZemV0pTbr2hXu8p2R1c21KPBYqyx580Bp6+nWyq+UemP3vB92" +
                "k1/+57/kw6d3V8L5LCryPqp3Jj55qTNpM1GTl5n0UuQG2quiJHtR0ZYqMMm6oUyEXb9vyK3AeFsqJ/Ar" +
                "SJOVVbUXrQORNyI1dd1qlUpPwquaDvjBqbSQopHWq7StpAW9sZnSTJ5bWRNLx8/Rt5Z0SuLm+go02rGv" +
                "FBTaQ0JqSTqlC2yKpFXav3rJDMnZ7c5c4JMKxGA4XPhSelaWHhpLjvWU7gpn/BSNW0E2nEM4JXNiEdbW" +
                "+HTnAodABWpMWooFNP+496XREEhiK62Sm4pYcAoPQOoLZnpxPpHMal8JLbXpxUeJ4xn/Rqwe5LJNFyVi" +
                "VrH1ri3gQBA21mxVBtLNPghJK0XaCwDPSrtPmCsemZy9ZR+DCFwhInhK50yqEIBM7JQvE+ctSw/RYJw+" +
                "ExqPJkeAVqescKVpqwwfxrLKEU8CsdyVCgEJRnC6iJ10wjJgHIxgAN2EeAdIwiVSd4chyHYLaOxK0kJ5" +
                "AUPJMWiBC6obL+BwcLNMF1GzIxw9iBYbQn5ABZGS9RKRY42m/u30V1kfE7gX6iEsZvSzyImyjUzvoVkG" +
                "DoCyrTxy0DlZUAiCcA2lKldpNLDTwK066ZwgkQBK1a3z0Ewg60C16uPHkXum0NVmS8rHuB2tYknyp9kY" +
                "Py7C1f3rKZSanZ6Mp8f93wyqxES7r/y99k8zfECA1PXvb+eMNa+vM5OvH4l4JjO/Y0py2IEiFr7cdRZq" +
                "pLRLZiwf+RMkDT/dD9I7KAFYv5Gc2aODg1ID2FHDkRwwrzFOcUa7L0uBpoC88tjFh0xTqtCSwubdHSSa" +
                "Q2rKkcL+LjS6UFMmGDWcRhQBHerIr1U1SbStrJCFQqICcOpzneJiiv4FjVCCeCX4WQQ/M9HMyhVMT5K8" +
                "MtK//jm4vFNssjaaM1k8MGuyHq1JsjZuhXq0zq2p16hH2DhRMJ9Ij64aRiwOFRlujj7tmtVsAOCiGDYs" +
                "5ai+3C5DgT0SsN5sN0M9Rw7s3O3RB6NnTI5KHNL1AunaxWkia0GMvAg3boq8px0OrQP/eY/NSCErgxI7" +
                "U2enABf096rN2AoAxaLy47AhzJdjcC8PQ3oW+03ZwSgAqyJdoBV0S4O0CcSWwVkHTBGNPRl3dx6n5tAM" +
                "wlbJ8frwRDR/TJ34J2X6mMzDmsL5PcQmgRSLFsOgEa8F5J0nBRmMvf05tz0VLBw4YGWAMDKeZaI6yD4/" +
                "95PC0EPTWFUg6aHH6O/5MTvl/GGWPzqCx5URG//tnEOUnbxQPOHj/gYx5GgE8BipDfkdYUjzO/OoQITC" +
                "mmNig2dkioEn+Rww8SryV8HA5I8WDFazrdbEGnAaIztljpjI2OG9mf5iGFgxDmOkIMmVyYycYMyUBSts" +
                "CBOgDWPokkfYzMAf2nAq1PIeIgm3hTBtNk01oD/6hJfBsqBVsVrGIThQ8bQY7mbhNoeZk+GVzQpgkCk6" +
                "45bC5y9jvQs6x8MQQp5mO2+fr8RNLvamxfwLG/Biu0tkaLO9XuGy441ZcnPoRBw6NKT6MB0rjUlcojv3" +
                "XVA8DG/74e2vk4R6xNixaCNBcb/o+89BzPnr2whQdvJ3Dcq7t92JcpULSG9Wf3N2Y/U7tGdjzT3fqXSA" +
                "mMPVUxPuptydpC7CRZ/v/PjfQZ+rHcn43dElyd8gek0kRBEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
