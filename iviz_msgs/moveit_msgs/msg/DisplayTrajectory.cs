/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/DisplayTrajectory")]
    public sealed class DisplayTrajectory : IDeserializable<DisplayTrajectory>, IMessage
    {
        // The model id for which this path has been generated
        [DataMember (Name = "model_id")] public string ModelId { get; set; }
        // The representation of the path contains position values for all the joints that are moving along the path; a sequence of trajectories may be specified
        [DataMember (Name = "trajectory")] public RobotTrajectory[] Trajectory { get; set; }
        // The robot state is used to obtain positions for all/some of the joints of the robot. 
        // It is used by the path display node to determine the positions of the joints that are not specified in the joint path message above. 
        // If the robot state message contains joint position information for joints that are also mentioned in the joint path message, the positions in the joint path message will overwrite the positions specified in the robot state message. 
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public DisplayTrajectory()
        {
            ModelId = string.Empty;
            Trajectory = System.Array.Empty<RobotTrajectory>();
            TrajectoryStart = new RobotState();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DisplayTrajectory(string ModelId, RobotTrajectory[] Trajectory, RobotState TrajectoryStart)
        {
            this.ModelId = ModelId;
            this.Trajectory = Trajectory;
            this.TrajectoryStart = TrajectoryStart;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DisplayTrajectory(ref Buffer b)
        {
            ModelId = b.DeserializeString();
            Trajectory = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < Trajectory.Length; i++)
            {
                Trajectory[i] = new RobotTrajectory(ref b);
            }
            TrajectoryStart = new RobotState(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DisplayTrajectory(ref b);
        }
        
        DisplayTrajectory IDeserializable<DisplayTrajectory>.RosDeserialize(ref Buffer b)
        {
            return new DisplayTrajectory(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ModelId);
            b.SerializeArray(Trajectory, 0);
            TrajectoryStart.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ModelId is null) throw new System.NullReferenceException(nameof(ModelId));
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            for (int i = 0; i < Trajectory.Length; i++)
            {
                if (Trajectory[i] is null) throw new System.NullReferenceException($"{nameof(Trajectory)}[{i}]");
                Trajectory[i].RosValidate();
            }
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(ModelId);
                foreach (var i in Trajectory)
                {
                    size += i.RosMessageLength;
                }
                size += TrajectoryStart.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/DisplayTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c3c039261ab9e8a11457dac56b6316c8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+1bW28bxxV+J6D/MIgfJDU05VhO0LLQg23JsYL4EtttkxgGMdwdkhstd5idpSim6H/v" +
                "+c6ZmZ1dUrGL1ioK1DVi7u7Mud9nek+9Wxi1tLkpVZGrma3VZlFkC9UsCqdWulmohXZqakyl5qYytW5M" +
                "fjBwTV1Uc9k3KejFweAeQ6rNqjbOVI1uClspOyNARuBklt4WFUG1ruCv17pcG8dIdVnyyl9sUTWOfupG" +
                "6RqUXQORLi39N4D6s9LKmV/XpsoMo6j1LyZrbF0QtKXeErnKrUxWzAoQ+8ZObfMurNm+/9Bu2CaUY5Vy" +
                "RLhRxPramVw1VtkpiI40R2pPnF2awJ+n2j8xpJEC4MsmwppuW1HkhVuVRGdF8gOS3DSmXhaVkSURVxd8" +
                "FEoFQgN/iqiLiwT80jin50bpqb02npCENM9kWBX14iEE7RQVsboUPYLpPhW6dJaAVFjwe2QMe0zdTu+m" +
                "ICsgkutNXTR9WewwvIcX8MrafstvWzVPaFndHAwGZ//hP4MXb78dw0pN0UyWbu5OetY2SIjg79+B7/a7" +
                "yGGSWmR/x4t12RTnr571dy7xfpLb2WQXxmdi9CPMDJ4bnZtaLfgfHyXI34S+SpOaBr0tr/FIS1ZsXZ+L" +
                "btfkQrAQSA5BFlLlus7JdBqd60azjS+K+cLU90tzTfGQbGa5Invjr812ZdyIYwV5NP2VaFiW2xgqMrtc" +
                "rqsiY9MriNd0P+0ku9Vk8HVTZOtS17Te1nlRYfmsJtkAOv2Nke3yfAzndCZbNwURtCUIWW20Q0S8PFeD" +
                "NYns9CE2DO6929j79GjmJP6IXJyViDU3CMugU7sx4fiDMDci2CQcQ1hyp4743YQe3bEiJESCWVnKBUdE" +
                "+etts7DieNe6LvS05DCZkQQI6iE2HR4nkCsGXenKBvACscXxKWCrCBc83V+Qzkpw79ZzEiAyVE0JIm/D" +
                "a1YWFJFUWUxrDeejXYJycO8ZZCx5jTVC/2rnbFYgpVHsaRYhrbE2KK39l7yIXYJYvtDIw62/s4vEOEhK" +
                "JZJJ2zFEvh8qshJiqKGv9KCzzJTI2Pj44QNBtN3VZkY233xgy4fwElxkzpREzQ1sz+SwzMcUm9sY7FM3" +
                "8oAPxw7SJYMmirRLIjx7PRb1uBwR64PBrLS6+eYRBwBPWPKuZSd52WEreS/cDPK1fGKLmcxqu5Tgf1fK" +
                "vCVa+ypDImNaYIhMvfX2IoIie5dcZ2am5qDAFr1HYYFt14vB0NyeqkxL9rhP2cPrKYF1ZGB5Ym7wEnyr" +
                "XKwGjoNtyoq2NktAcConhy/XObggQ6mp3iFkUc0nrXJPuiq9J2Fr4c2IDas01ZwqBf8qQktMbMjC6mwS" +
                "awzL4O6KgXdNk4GNBvuz1S3avJus9Ymm1YkXfbVmJPxgYoki1dF6Bev7RhG848HcUDXbBDzvwiopleW3" +
                "G4gJk8cDJkUHHfxzmwSGYJpUjM+LqlPBqj6aTeGarpfvoKg67v7v4ela2Z0HiltkHEqK6KMu+JLX1NQ0" +
                "G7RfzcbuBAgOrLPaoN/RGVUQg7+yTZzK/pIZHPywpg11BV5rKzHgbpj0xOxhEbaDbz36VeyZbEX1ztJo" +
                "RCbb7qSNeVHTVuJhJLZCQqIeo2hUbkke1BsRjKW+IpCGygfs1qtVGa2/9Eq32HJkRvPRkFpeki+v4mYT" +
                "VHB5V2QK5pX3AiDDVJ45am9mD33rApoFGamQgARpH4/QgG3tWm3AEP2ofVXJaTbQxdVPY+0QycGD6AqU" +
                "XT32S9S0NRTlRzGNqpv4axt//XYnqm5tbJ+2K/hpzD8dnePp19ZAIeSPMhR+be7IVxFAAluhlHZt9Ovy" +
                "M63tlQGTbGKOatHKULGK7KSrOVf+aAKomQi+6pe0z37dnbWr3C4HBnfmAukgQE/tuum33+R9I/LdndkF" +
                "uA+pl7gnLq/cwQBuaeuk8pVmXbIuw5OhzHfJvAFDnYW+NpLeVuQmlLa4+mwzAmJBF3onXwqWfs+c4Hvc" +
                "NJRACVBmy7JwXChNkWKpU9HhGwo2DH7Ai7LJIOL4YBAAPA37X/F2ZB7/ZRJBTzxoQf2s1HMSc47eERGI" +
                "4hGX9tysuIxCEUKiBAs0eTWZYMPNHMfD2YzbF/I4SKOJRFoGwvsPks51aV2DZnK5onoZ8Yg739BacX0V" +
                "2J3aHJ3GUSCIFmL4hGa3hFHvWQxMUmCylY3HGcXn8TgZy0w5ZKv1KhduKQ4z+U1ifSTOqbVUQroJ+Pts" +
                "A439xpgIS0d/kKLUlrmLsTs3LquLqYyqZBLFrDvTcEVia2rl2ZFqyw2teAPSnO8H4iYuuaV8OarNtS3X" +
                "eE+hoi4cfC87BjW5mRUVd7vo4qnZTn2uWwQd1ToHgOXxsF0aQ9bO0hPHi0+o9YevtlukrYqTBKS3nanj" +
                "0csldr88HjFjFy0vaHSqgqQAg8sxLGRfpW4dwQE1drcxartbLoLQ7BDeMJIO+FhqTm1QKVIxgDo/H3GH" +
                "2l8jYxdbteNODiPiSmErUMSmAThHXj/daOgoC7ikeUIY4uaBeg9ggBtJ50ThvSthJoarG/A6t7r0Ds2F" +
                "Cm/y64l3WJhdQaO6HKm/oTZBmSJlg4+nzEVlCaDXT2+UAVjLIZccKKXJWa9Nqk7poMxy1Wy9NUJ6wo3o" +
                "NuHdLey6zL3kgpxc8RsFfmIZcwKGk3gNV+aohjaaZwnRBiKZvTYsEk2CrlHfYRDGGqRioN/Sxk6N7Wd3" +
                "hLA7QdjuzgnuIKDs5h/i6s1OLy4xIBmxsyyiieVmTsUFGx6qjNySXgnOjCKK3YSITSJZZ8265ljS4hND" +
                "loqaRL9eIoXxAD/4J2aSbkt15FIk71Y8rOMKt+1BeM/cNK3/E9i28b+ioMRBSmULKhlG6hlc4UYvif4h" +
                "hp6lrnQdgoVmC/vLm/NnHNNOkcqPbshY6a/eYLogYw9LRTB/9M1gOsTcGUqQJ5FV3Az9Xt30vnNTgBUB" +
                "Gjn0tanhL1OdXYHhDg3/D2N3G8Y2GG8tPjmMheX/S2HstiiWzps+aRCzf7zRcJfS/fY3FhN9FHndTbsk" +
                "SPe0gUS99HtttzSkgEl2xgpHxwRD/7SeiYG1j1Jr3UXHdEuZ752i91YGA9OkSmYL0eQgN6EehGWxoaKp" +
                "iGfreGCj4BaBRRk7Mi67KdyT/7mFXhnpryj9+dP0ZpcQgiHRIm1lsE6Q3rucASJ359AZGGaglfVd0KjI" +
                "8YlK2yGf1scG5t4+iKGbkSAZeYniEAz5waAvL+upDcfyvpTm3tHH9bTnCB2aHEhTQvRk2HW2AAhCTjWz" +
                "pjin7kfihBSkxLImZ9xKVCSnFGr9hoPWQxncxPev/jg9o+SBmwFCGcdwSnOZlMadcz7SC2dm0QxmW8Iq" +
                "z7WWgBHZAWw/k6JwZ0j+VK3IoCwrLR/86dqu2S08mON4vs5IKpMh3NVbRlebUs4LD/zg26OGIglo7Jo/" +
                "dkidGxA48Uha7WxMMV/EJNHTylAVM3VV2U110E5seMOdHMTv+udjH3uHcvgy4/Gj76ZDIcVOdNCN1S2/" +
                "5AOeVy/IIzYlBkd6fEHoLzEK8D6c3owJSt+ujFgImsipdlyxsZRal5IfEyT2ecUlrbAknLwDCMBpgbcj" +
                "Cx+KUXrsyaXBl/2+ovZBA+7THyFwikymK60k3tqSBBEwZehRlwWOqDHdQTwSannZ6/AN9Xmyrj/XdJ0F" +
                "E68GYHth3KIHGa9o+dJ/2QsLH1MwT+A2UAqKUcw0DKaOPuBFPodq6udbvCwUizK9JDGsxQdJf3leSG3D" +
                "Ijzu0Pcae8EPo7qNWXxMKXyc5y61La8DrKv40BuDnFG4yuMXke1eF3btqCgxN1QAgIWikSAu8YiUPd1S" +
                "pfL4/PzsgWB6w9G3gwznH9IgVtdFbStc7FEodGvUO0eGauUtBS/2Eh7INeTprmckRX7skb25ePHqrxdn" +
                "X3nOVivEMjQVVeSO2xAfgJn0eDXo9zkOY37ZFLglfSSsvn598fL87GEM1i3a/RgZ0ZCC58Y7hNc7Hzkc" +
                "YUVQYSgul2vXYEVpZo0UjsfARRHP2RIi8zfXukE3N44EmnsyWUSnQuSrVTjjkjRNjyj24lobvn+22Pnx" +
                "oDO49y//Ua+efHfx9B2uq/zrm/0fEdDT3x9Hc1zlpmbG+dEHOopyaCNxduaMtD0oN5e4cIfOZS5zzVjY" +
                "y4CN7IXHmL1a5MrEwVmKZMxvBETbHdbBtuYUzyqVT2NWIDAtzHyaEuSTMve337199fIks8vQ9P70+MX3" +
                "SkBQkxgNmiJx9Ijk2AvRPMimbex9JRBSz0hdcK1RVHu0z57FXZe1V1TmXJmx+uLvhxD04fjwKUqi8yeH" +
                "Q3VYW9vQm0XTrMYnJ6XNdElCbw7/8cVBPM8tQKD03JUPm6JFXxVBSYkcUHkWzSFtKjJuc6+M8beZZiW5" +
                "7rQoC2rVurm1Y7qYOoscw0ne+RMxEoYCvhAIPGppV9nM1iQrhD6ZXrgxz+eJRs8wPyuGNFZRCvISgqCX" +
                "fUGMv/7THx/5JUjUcnpJC3fJPgzY3v7wvSL9OYOBc9RXF/nbX8vnYYkHz+jU4WbuTr/xrzDkH6uvH50+" +
                "lGfaUGNJgWo5rKFSYWPrvP8exQ0YCljCqYX/vLT5usQCPrls7Oow2jjM/XNN2G6rMIimc3Hfqb2hxnIF" +
                "yxuqbEslOld9GcYb4SqT75tqE4fqZGZhNkCF0TTUCwQMCQH5n31T6u8HQ/ofNfW4g/dH9eTVj2df+d9v" +
                "Xz+/eHNx9tA/Pv3p+8uX5xdvzk7Di1cvL84e+asOMW5xFgJNfhXeD8KivKB07NIrwbK0HWq2K8IenJ2D" +
                "/HRDsmws4xp0PnwGJEKQhA5x3YTwddjuOZTkN/A2iq/EOJMqTciPQ/WTTOB+TmmGkLn3kks8nqJOVMKc" +
                "hEJn5I+EPmplO/nx7EHy9FOUNZ5+JlGnJIn8PVU8vYHaEUjp3ypeyB76IMPxWfiudV6sQYJvlsSCRh29" +
                "Tt48Pr/8y1uiJ8UZlMwwoWC5LypSEdPhmQbfbAi1JM9PPaqflaaCZKTaKw0duJPnF5ffPn+njgDbPxy3" +
                "PMmRWyLxlqdFp0MLvqCO4AvHgg9RL+AR7jweeUjw3IYFVx2C7ER9vq/Zj/OprWS4ED7R/rY36PskZrVF" +
                "za30SFymWLU2xDLFfnSssPf1augn0196oQ56nujlF02qxzwZV+KpO4tbwWDh5wlxu/3CLdf4UKz2B2qs" +
                "LRQMyW07lnZyBWOkBq87t/3TO0LJurtiUC6w7Q650qNNHW4kpux+5KLI509BaEVD4klIRfeJAEFduThf" +
                "XehqTvXEn5MI6y/R8uVOWHrn/yOBwwkqftz7DwPgeOcB8CTYwxr44OFHgWFH6NCucMbOC5iaPTLnAzLZ" +
                "dEeiCmzsEVlg69C1RMkN9/enQqe5mfBY8U6o5R5+73mdnGeZoR8GtMOCOFHQN+pLTBK/VNlv9J9cnakH" +
                "UJZW4zMycDN7/wDXsafx8Ss8ZvHxIR7z+Hj6ob0o/egDvxsM/glfgIUbujUAAA==";
                
    }
}
