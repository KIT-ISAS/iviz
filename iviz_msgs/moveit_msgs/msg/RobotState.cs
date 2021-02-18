/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/RobotState")]
    public sealed class RobotState : IDeserializable<RobotState>, IMessage
    {
        // This message contains information about the robot state, i.e. the positions of its joints and links
        [DataMember (Name = "joint_state")] public SensorMsgs.JointState JointState { get; set; }
        // Joints that may have multiple DOF are specified here
        [DataMember (Name = "multi_dof_joint_state")] public SensorMsgs.MultiDOFJointState MultiDofJointState { get; set; }
        // Attached collision objects (attached to some link on the robot)
        [DataMember (Name = "attached_collision_objects")] public AttachedCollisionObject[] AttachedCollisionObjects { get; set; }
        // Flag indicating whether this scene is to be interpreted as a diff with respect to some other scene
        // This is mostly important for handling the attached bodies (whether or not to clear the attached bodies
        // of a moveit::core::RobotState before updating it with this message)
        [DataMember (Name = "is_diff")] public bool IsDiff { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RobotState()
        {
            JointState = new SensorMsgs.JointState();
            MultiDofJointState = new SensorMsgs.MultiDOFJointState();
            AttachedCollisionObjects = System.Array.Empty<AttachedCollisionObject>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public RobotState(SensorMsgs.JointState JointState, SensorMsgs.MultiDOFJointState MultiDofJointState, AttachedCollisionObject[] AttachedCollisionObjects, bool IsDiff)
        {
            this.JointState = JointState;
            this.MultiDofJointState = MultiDofJointState;
            this.AttachedCollisionObjects = AttachedCollisionObjects;
            this.IsDiff = IsDiff;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public RobotState(ref Buffer b)
        {
            JointState = new SensorMsgs.JointState(ref b);
            MultiDofJointState = new SensorMsgs.MultiDOFJointState(ref b);
            AttachedCollisionObjects = b.DeserializeArray<AttachedCollisionObject>();
            for (int i = 0; i < AttachedCollisionObjects.Length; i++)
            {
                AttachedCollisionObjects[i] = new AttachedCollisionObject(ref b);
            }
            IsDiff = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RobotState(ref b);
        }
        
        RobotState IDeserializable<RobotState>.RosDeserialize(ref Buffer b)
        {
            return new RobotState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            JointState.RosSerialize(ref b);
            MultiDofJointState.RosSerialize(ref b);
            b.SerializeArray(AttachedCollisionObjects, 0);
            b.Serialize(IsDiff);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (JointState is null) throw new System.NullReferenceException(nameof(JointState));
            JointState.RosValidate();
            if (MultiDofJointState is null) throw new System.NullReferenceException(nameof(MultiDofJointState));
            MultiDofJointState.RosValidate();
            if (AttachedCollisionObjects is null) throw new System.NullReferenceException(nameof(AttachedCollisionObjects));
            for (int i = 0; i < AttachedCollisionObjects.Length; i++)
            {
                if (AttachedCollisionObjects[i] is null) throw new System.NullReferenceException($"{nameof(AttachedCollisionObjects)}[{i}]");
                AttachedCollisionObjects[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += JointState.RosMessageLength;
                size += MultiDofJointState.RosMessageLength;
                foreach (var i in AttachedCollisionObjects)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/RobotState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "217a2e8e5547f4162b13a37db9cb4da4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+0abW/bNvq7gPwHYvngZHPdrumGnQ/5kDbpmmJtsiR76YrCoCXaZiOJHknF8Q733+95" +
                "ISnJcdcNuAQ44HqHxZLI5/2d3BVXC+1EpZyTcyVyU3upayd0PTO2kl6bWsipabzwCyWsmRovnJdeDYUe" +
                "qRG9XRqncaETZia0d+Kj0TX8kXUhSl1fu53MqdoZO6nc3D1+jV8vEQYvnBC8nWwn2xWveadfSC8quRYL" +
                "eaNE1ZReL0sljs9eCmmVcEuV65lWhVgoq/rQ3+BiWNjBQvsnhZlN7uA78l7mCwCUm7LUDrk1048qBxr2" +
                "ZPzmjXCmUsSLgBVJEvs7WQTwIu4/o+3vP4i4fZJATwJoRv2ylHMQc6FzkHI9F6uFAsAWoIM+XK5qJeAH" +
                "4J7Cj9oru7TKAzUSBCsKPZuJlfYLYRVKwyciDQGh/YiFtIsKNs6Xa6GrpbFe1l6AekG6dVEibuQosTs1" +
                "hVbAfyQIFtaGEOSlknbbYsQEupeA5kZpPx7nxqrx+AKFxDqYKkCoRLMsmFvtmXzfsT4Q59SYEsidIH87" +
                "WXb4X/6Xvbn8fiy2G2NHWDL5AxniwpSFE0C4RCEUyuVWTxXJgUyJWXfK4w9v7O8NO5IFxYOI2BtGItsl" +
                "FJ1NCmTIn8WeVTembPC9FUurHfpevo/UFGqmaxT1egwAxJc9nyOUC5WgyAIBVPvDdumNKk2u/fru0seO" +
                "Fj92++Sr7RY1A2155h7lsVyW6G267gJ4W+Hut/sjYuyk5QV2NLUGKaDBFar27KvTNQWHWlYqCGKhZIHG" +
                "GtzZEXSvK7Qv8AedLzr4SGpOrMDjwejBwApVjMRRWd5ZA9CBUgMOFNVIYYRdKW5FFKRBIIdwjoJ++tHQ" +
                "aeddsO0YhqS1cu2GhAHdiNS4lNb3JUzEoNqJ17mRZXDoSl4r3hTWA+9oYWaJGpXlSPyyULVQo/lIrE1j" +
                "YzwlLmoDAIN+pHOgWYlRITiTqoa4ReSyFuCsN6qrTqJbqGrp18EaUXrMDeu2w7tbmKYsguSinJz+AwI/" +
                "sAyCZDgdr8FVpgadrwALsJlsIJHZEQ5aQSIaBG1BLx6AsQZHWZa9YuNgG8ky5y3EDYirZD+z0kj/7TN4" +
                "jI7QeRUNvvOKJXBvAcUXHE2YZmADIkpdSFuAOL2kyEHxVs8hoD4qFVCInFZL0BzHlfUSuW6FOYf4bWUJ" +
                "0mscp6DcVBWINCc5gr329rPJS7JCnTclhOncgJ3rGpfPrCT7xmVOgVLqXInT4zEZuMobr2/IV+vcKukw" +
                "PJ8ei6wBPR08xQ3Z7tXKPMIkNMcEFZGn+KBuITc5R8kJY9SXzNwIYGO0BSxg3Xv0bgKPEG4ACZCglgac" +
                "YA8oP1/7RcisN9JqOS0p++WSIugANw32O5BrAl3L2kTwDLHF8VfA1gku8vQopUPXzEGAsHBpzY0uOHaR" +
                "nUIYBOMt9dRKu84oVBHKbPelpUCC6iONYNjsu2cwYdbGRBcPkd7uVkPA7IVCdQEjMmYQzkhooiHUkGem" +
                "gFeouVWKwuAMfhQGogzAmUF+M6tYPwB3Te4bS5mtxcdh9dQHgTQVWjPajYzZAu3WrZ1XFccBtySFgsmD" +
                "X1hZO6xEec9c+TYbAVhZmoD9GlIkpUyRL6CAHYmXGJhvQTUlRCxwjFLW0sbUJSne/XRx/JIy7AEWlnu3" +
                "EDrh/3KFBoH5EGzHKf6I8RRjXsfQu9SxIOGP1QCF92J+6X0HqLwiQgPDvVEWzWMq82tkuEfD/5PqwybV" +
                "lYW4uPjLSTUu/19Kqp/KqdwX4XaXzRW0Ed6uOYBcRROGVcmc7yxagUJxAf7d+PYLiQk+srzuK+h9guoo" +
                "SRtDXnCHFFamyq8U2IVfmTsZk/SHAQ+cSeZgy9nPIE9jD3h/yV79YwMbbI0BwBoOqQ/DZCBmC4sSSiD8" +
                "tkG/SIGYLKpS2AeCTaWd1FiizQAPI3QwS13bELu1woA8oA+kKAauhlmG3B/D8ToGQ5YJvoYte+hsQ2xs" +
                "a16FqYJqFapuIFZbPdfFZhilwB+YGwo/ewomDS5FNDMyUCEAidLeH4nTGTnoChki547N2lQluij5e2OG" +
                "WFEFEH2BnpMTRV/VNaQkWYDWQxkpbtOvVFqKPx5E1a2NbdM2hGWrUzrv6Ryffm8NFIX8WYbir9UD+SoF" +
                "jcBWTLCu7Vr7/EytuVbIJJmYw5kMziQw5cp6ToUvJg0IdtFXw5L2Oax7GO44/G3RGqiC1dMyNwSnAuIp" +
                "9SCDmHL/GosErH3kGcR9McgzHmbvE+OvkJ433rIfTzvTI8pV0Bvp2zgnQaellInDtp1YMuMDpScanZEo" +
                "06SSxlFQeEIl4BYS2iiSFLSFioRM3zcIARgcG7ojPlzHSHchnMicnQl1hgwT0NqE6eAIohZO6ZSHag9j" +
                "Uxzs7W6DGKd8XK4lXpI4GEOxk23KywRqd+LgiEdMNFMNFWZ3FhcnlzgilViaBzJMky8QBCAv1ExCxSUe" +
                "JeKYFCzOS2j+ijXXZ1AeMLVhw05bKxC4SZjrIliQVg5lLCgjUEbVJBTcOY+Mel0p6IV6BNYMpiJmldJQ" +
                "hTASOwg7pBAIcQrkD10857W8NNSmSmsacosAZn8Yh2OEpFY5BnO7JnRWldzdImCqqxg1KhKApmkyJIyP" +
                "hGbdGRFepZcgEiRwEpC02lkpaO9TubqhFcihM3Fdm1W90wZY2nBvY86un971z6NQBQ55wDCjaiFMmWNL" +
                "R060068aW37BBwKvQZB7ZEoEDvT4BtCf4og8+LAu2q1R6eulYgvBfD2VjnpHklLrUvxjgi3GvKZRD7PE" +
                "nFwhCITTAm9H+SEUYxO0paqPvhz2aRuCBrrP5midivXOqUMriUtTgiAiphxnt5XGgQqeemA8Ympp2Xn8" +
                "hnOrzrrNMsT1FkyCGhDbG+UWG5DxFSyvwpetsPBjF8xzdBtUCrbFOOtXWCSEgJf4HIppOPehZbFt5WID" +
                "xNCwD4L+ikJzl0Ui3O/Rd457kR9C9Slm8WOXwqOicF3bCjrAdTWNaPCAgwq/ziKw3RttGgclorqFqgJZ" +
                "0J6DOMcjUPZ0DfX90fHx4RPGdEHRt4dsZk3Fg9P6RltTV1gbY8ttsfPaU9C1ryF4kZfQQZUHT3cbRqKL" +
                "/YDs4uTN2c8nh18HzpZLjGVY5daJOxqIhABMpLs4bP9zjmNVzpsit6CPDqvn5ydvjw+fpmDdot2OkRAN" +
                "IXiugkMEvVOHsIcrogpjm1s1zuOKUs08t7D7iAsinjMligwkHGNKG3QL5UCgRSCTRHTARJ4tlU29AMCF" +
                "R6xc01oTv99b7Px80Ml2//Y/cfb89cmLKxyu/v3N4R8L6MWfH9NSXKXxyozyYwh0EOVwoIWtrlM8gMFy" +
                "E1SpLM5Q5nzel0YMfPAE9kLHexu1yLVKB0pdJGN6wyDaOZWNtjWHeFaLYpqyAoBpYRbTLkEhKdOk7fXl" +
                "2dvHuani+O3d0ZsfBIMYiaNk0BCJk0d0ulSM5lE27YgxVAIx9YzECdUaut6iffIsmv8Ycw1lzrUaiy/+" +
                "NUBBD8aDF1gSHT8fDMXAGuPhzcL75fjxY2hhZAlC94N/fxGYtFRs1Yanf3UIm6zFUBWhkjpywMpT+wFs" +
                "0jl13NdKhdn7rATXneoS+qRRP7f2TBdPY1mOsfE+fs5GQlCQLwwEATUPzsjMGpAVhj6eo7oxnVsDjYFh" +
                "ehYEaSySFPglCgJebgpi/M0/vnsWlmCi5mEDLLxL9iBiu/zxBwH6cwoPYpO++sgvfy9fxSUBPKETg9Xc" +
                "HXwbXuHh91h88+zgKT/DBotLNFbLcQ2UCitji833WNwgQxFLPM0PnytTNCUuoEGDN8tBsnE09/ua9X+q" +
                "wgCajtl9p+YWGsslWt5Q5Gso0anqy3HQGqaVsW+yKh02g5nFKSUURtNYLwAwTAiY/8k3uf5+MoT/jTI6" +
                "MfpOPD/79fDr8Pvy/NXJxcnh0/D44t0Pp2+PTy4OD+KLs7cnh8+yYLoxblEWQprCKnyfxUWFhnTs4nWT" +
                "dml72NeuiHtw1IXkdzd0lo15cIydD92NYCFwQkdx3cbwNWj3DDj5ZcFG8SswTqRyE/LrULzjs4DfujSj" +
                "kKn3UvXcp2F1LyrhxBZCZ+IPhD5qZTv59fBJ5+ldkjU+/Qai7pLE8g9U0QQN1Y6BFP6GUwWHZRIHGYrP" +
                "zLeVhW6QhNAssQWNenqdXBwdn/50CfR0cUYlE0xUMJ9uslTYdGimQYPIWEvSSU5A9ZuQUJCMRDuB7MGd" +
                "vDo5/f7VldhD2OFhv+WJr6J0JN7ytOh1aNEXxB76wj7jw6gX8TB3AQ8/dPB8CgtOJqPsWH2hr9mO84Wp" +
                "ebgQP8H+tjfY9Ek8NdKWWukRu4xetjZEMsX92LGivTfLYTgj+yoINdvwxCC/ZFIbzINxdTz1zuJWMLjw" +
                "fkLc3X6Buld75xATi9XNgRppCwsG/s5XZlDanYnpSGQ8+U1XCToj/c66h2JQ12kc2htyda/8SNZxn93P" +
                "zHXvPwVhKxoTT4dU7D4xQEBXzs5ntaznUE/8sxNhb2TZKGzUZnjFwHQuFQKPeEwKxY97/yFDHFcBAJ1J" +
                "BVhZCB5hFBh3xA7tGu+e0QKiZovM6eIIb3ogUUU2togssjVwLVF8H+P9AdOpbic0VnwQaqmH33pzgE/W" +
                "1TAMA9phQZooyFvxFU4SvxL5H/CfQhyKJ6gsKcaHYOBq9v7JBxxOpsev8TFPj0/xsUiPBx/S+cX7Zx/o" +
                "3X0J4DODwI3T1K2HqRtboqGR896b4j5Dd4wwdLmgXRsiSntvQGlqB5Mjvh/GQxn4Cg8yz1UZGnH3AbVk" +
                "+qv50tWHNIXv4OJUpm7x8pEqRrEOTcOTEA0w+8WpBM4W8ZKBxU6mf+ZNMWKDyxGwnm25J+buXhQDdjov" +
                "e2zdvUJWNHE0Abl/gpMivE+Md8v+A9npvjlLLQAA";
                
    }
}
