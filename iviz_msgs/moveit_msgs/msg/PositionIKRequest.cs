/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PositionIKRequest")]
    public sealed class PositionIKRequest : IDeserializable<PositionIKRequest>, IMessage
    {
        // A Position IK request message
        // The name of the group which will be used to compute IK
        // e.g. "right_arm", or "arms" - see IK specification for multiple-groups below
        // Information from the SRDF will be used to automatically determine which link 
        // to solve IK for, unless ik_link_name is also specified
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // A RobotState consisting of hint/seed positions for the IK computation and positions 
        // for all the other joints in the robot. Additional state information provided here is 
        // used to specify starting positions for other joints/links on the robot.  
        // This state MUST contain state for all joints to be used by the IK solver
        // to compute IK. The list of joints that the IK solver deals with can be 
        // found using the SRDF for the corresponding group
        [DataMember (Name = "robot_state")] public MoveitMsgs.RobotState RobotState { get; set; }
        // A set of constraints that the IK must obey; by default, this set of constraints is empty
        [DataMember (Name = "constraints")] public Constraints Constraints { get; set; }
        // Find an IK solution that avoids collisions. By default, this is false
        [DataMember (Name = "avoid_collisions")] public bool AvoidCollisions { get; set; }
        // (OPTIONAL) The name of the link for which we are computing IK
        // If not specified, the link name will be inferred from a combination 
        // of the group name and the SRDF. If any values are specified for ik_link_names,
        // this value is ignored
        [DataMember (Name = "ik_link_name")] public string IkLinkName { get; set; }
        // The stamped pose of the link, when the IK solver computes the joint values
        // for all the joints in a group. This value is ignored if pose_stamped_vector
        // has any elements specified.
        [DataMember (Name = "pose_stamped")] public GeometryMsgs.PoseStamped PoseStamped { get; set; }
        // Multi-group parameters
        // (OPTIONAL) The names of the links for which we are computing IK
        // If not specified, the link name will be inferred from a combination 
        // of the group name and the SRDF
        [DataMember (Name = "ik_link_names")] public string[] IkLinkNames { get; set; }
        // (OPTIONAL) The (stamped) poses of the links we are computing IK for (when a group has more than one end effector)
        // e.g. The "arms" group might consist of both the "right_arm" and the "left_arm"
        // The order of the groups referred to is the same as the order setup in the SRDF
        [DataMember (Name = "pose_stamped_vector")] public GeometryMsgs.PoseStamped[] PoseStampedVector { get; set; }
        // Maximum allowed time for IK calculation
        [DataMember (Name = "timeout")] public duration Timeout { get; set; }
        // Maximum number of IK attempts (if using random seeds; leave as 0 for the default value specified on the param server to be used)
        [DataMember (Name = "attempts")] public int Attempts { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PositionIKRequest()
        {
            GroupName = string.Empty;
            RobotState = new MoveitMsgs.RobotState();
            Constraints = new Constraints();
            IkLinkName = string.Empty;
            PoseStamped = new GeometryMsgs.PoseStamped();
            IkLinkNames = System.Array.Empty<string>();
            PoseStampedVector = System.Array.Empty<GeometryMsgs.PoseStamped>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PositionIKRequest(string GroupName, MoveitMsgs.RobotState RobotState, Constraints Constraints, bool AvoidCollisions, string IkLinkName, GeometryMsgs.PoseStamped PoseStamped, string[] IkLinkNames, GeometryMsgs.PoseStamped[] PoseStampedVector, duration Timeout, int Attempts)
        {
            this.GroupName = GroupName;
            this.RobotState = RobotState;
            this.Constraints = Constraints;
            this.AvoidCollisions = AvoidCollisions;
            this.IkLinkName = IkLinkName;
            this.PoseStamped = PoseStamped;
            this.IkLinkNames = IkLinkNames;
            this.PoseStampedVector = PoseStampedVector;
            this.Timeout = Timeout;
            this.Attempts = Attempts;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PositionIKRequest(ref Buffer b)
        {
            GroupName = b.DeserializeString();
            RobotState = new MoveitMsgs.RobotState(ref b);
            Constraints = new Constraints(ref b);
            AvoidCollisions = b.Deserialize<bool>();
            IkLinkName = b.DeserializeString();
            PoseStamped = new GeometryMsgs.PoseStamped(ref b);
            IkLinkNames = b.DeserializeStringArray();
            PoseStampedVector = b.DeserializeArray<GeometryMsgs.PoseStamped>();
            for (int i = 0; i < PoseStampedVector.Length; i++)
            {
                PoseStampedVector[i] = new GeometryMsgs.PoseStamped(ref b);
            }
            Timeout = b.Deserialize<duration>();
            Attempts = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PositionIKRequest(ref b);
        }
        
        PositionIKRequest IDeserializable<PositionIKRequest>.RosDeserialize(ref Buffer b)
        {
            return new PositionIKRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(GroupName);
            RobotState.RosSerialize(ref b);
            Constraints.RosSerialize(ref b);
            b.Serialize(AvoidCollisions);
            b.Serialize(IkLinkName);
            PoseStamped.RosSerialize(ref b);
            b.SerializeArray(IkLinkNames, 0);
            b.SerializeArray(PoseStampedVector, 0);
            b.Serialize(Timeout);
            b.Serialize(Attempts);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (RobotState is null) throw new System.NullReferenceException(nameof(RobotState));
            RobotState.RosValidate();
            if (Constraints is null) throw new System.NullReferenceException(nameof(Constraints));
            Constraints.RosValidate();
            if (IkLinkName is null) throw new System.NullReferenceException(nameof(IkLinkName));
            if (PoseStamped is null) throw new System.NullReferenceException(nameof(PoseStamped));
            PoseStamped.RosValidate();
            if (IkLinkNames is null) throw new System.NullReferenceException(nameof(IkLinkNames));
            for (int i = 0; i < IkLinkNames.Length; i++)
            {
                if (IkLinkNames[i] is null) throw new System.NullReferenceException($"{nameof(IkLinkNames)}[{i}]");
            }
            if (PoseStampedVector is null) throw new System.NullReferenceException(nameof(PoseStampedVector));
            for (int i = 0; i < PoseStampedVector.Length; i++)
            {
                if (PoseStampedVector[i] is null) throw new System.NullReferenceException($"{nameof(PoseStampedVector)}[{i}]");
                PoseStampedVector[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 29;
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += RobotState.RosMessageLength;
                size += Constraints.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(IkLinkName);
                size += PoseStamped.RosMessageLength;
                size += 4 * IkLinkNames.Length;
                foreach (string s in IkLinkNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                foreach (var i in PoseStampedVector)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PositionIKRequest";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9936dc239c90af180ec94a51596c96f2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08a28bR5LfCeg/NOwPEjc0rVjexZ4CfZAteaNsbHktJRvHMIjmTJOcaDjNTM+IYg73" +
                "369e/ZghFTvAWtjDnm1Y4kx3dde7uqqaj9Wpemtd0RS2Uhd/V7X5tTWuUUvjnJ6bvcHe4LG6XhhV6aVR" +
                "dqYa+H1e23al1osiW6h1UZZqalTrTK4aqzK7XLWNAVg404znY/WoLuaLZqLr5aORsrV6BL+5R+qJcgbH" +
                "KbcyWTErMk2bmMGIZVs2xao0T2glB/BLu0Z4FxW8XsrA2i5pO1fvzl5t7UO3jcWBmS7LjcpNY+plURnZ" +
                "dVlUNwoBwkhny1vaB4AeqbYqAXVV3ExwzITQLpzSpbN+oybfG7imLqo5U4IGMaVO1Ts7tc1Vo4EEma1c" +
                "4RocB4RbFFXzFDDO1Uro7QhXxAAWZ7oxZrpKByFcHAiI0GAL/9XqFwvwYJ8VPatx1bE6zXOapEvlaAtF" +
                "Qq9VbW+LHNaH6YQTAvbkYtQ2OK2mHXc3ma75FAnjlO2srFhOACov/PqHq2skQKNhh/zI4yA7h0U9u6Yb" +
                "TwXiRS2MiaI0JhEsgZhISQ9goZvuPGAz8AlEoVmoTFcIn4nXAkFbh2gFefGkz2xdG7eyVR74uTdY2ltT" +
                "NJOlm7unCUMJ1wlh49ntDG0JWd3Uemtfyxa3PDWbbxDJ3Mw0SPYI3iKhtqfCU7NcNZu9wcvkaTKCl31V" +
                "AD66EsRb4i6tqm9tkeOEEmiFzBurF/1l4d8MqAQYTK0tecokzuAVDi7fXl9cvjn9fril/KQ6SD3Rf6N0" +
                "bYRXSEJW/IuZqmwTNWYU5xIwr64goAYYkLM2a4QzLSoWWITTsTg0E5XDc3GM6+hqo251CWaLdhKWpE2m" +
                "euxGJFhIBBqPpCjmla0ThU7HR+MHLF+uWHM7dBgBEUzVE0IRW0ePSVZle31FjiqsGcExa1B/c6qY0coT" +
                "2cbk1mSNJS1ZaEf4m9IsDQIL2I/3BnNjl6apNyzHYOXNVYKHh8ZYvkaLy+ZWrXQN2IPBvF8YXEoF9+8i" +
                "Dp6LHz52+b4TjQPBf0jU6CG0Aw1C8oD4Lewi6i+BQ6h8FRhEowzsxsxmxJ9hcIC4nDg9nrhEh+j9A64M" +
                "dmVByye+MqD2qDQzfuTl0dY5CFpKDAeeW0gHlrNg4XNEIP6dp4DNgeXFaTDJ7pUSoOJOqSNx0XfFsl2i" +
                "KNs1rlks2cCjJ9Nl1pbEs71B3tbMPRxh26Y7vWqXU0YE5ummQePn1AHIO1vrGkgAcoBe032jSqNvCZ/D" +
                "YL3FtInKRNUX50SSDNNr1MvocIAzoHlHz8KSuKvByb/4z+D11d+O1W5X4p2lhFneU7qOw9ZTIFh0suxG" +
                "R6oYmzFjF1w0ELAAwolBQbkhMQZ9MJWzNa/+Hb5lR0YDU0f2XeJSl3oDkg2U9lGYOrt81TOtGEJ0oZMB" +
                "gYHJKjR/ktvZZGu906bR2QIABccDXvIXEDDgvvbvKDZbiplIww3gnwfw0s+/pOkgs3569GkTAS3Os9Rg" +
                "5sHdY7wJMgYaTcEN++TMVGR6WVhg16Ze1WALc5Q7rfJiNuP4AoMGABo2yRESzQ+hEDLYugbiz2K5snWj" +
                "wRWg5IK1yEsfjQR0pzYvwA4d+A3BQLSZGAiB5Ne7BotJ1CJlx8cQzZjj4yRkmZoZGqh2lTO2RcPbbxLp" +
                "G0ooULgJ4vfFVGG3MCbE0kEfSBAXtoRgBjaukQi5cVldTA1bNkKOUJcwCkwTnFxIkWpgPJCItWGsBo+j" +
                "F+dJBmgonvmgNrcYQqGBhAi5cHRmGOJuwLbAgQGj02MAoP7U0TlvfD0UnSOA5XAUh97CoSUrms320KeO" +
                "Bj91Q9LVOAVcB4gJY4/0WK1K1Dax1wLgzRJnvxmOCbHziAvMaKsCqIACl0M4wLoKgScaB/SEQoiF0eQM" +
                "RJ3ZQZAJh3XZj8f1iGroEUGMapOhH8nhpJHGMDIGoBfsBj0byYywKvmpSoJj8k245lj407WG6BidyLY3" +
                "Q7qu9caNaAVUI2IjmPimS2HaDLKdcJ1bXYpCL/WN4UkyHnBHCbMrPjON1T/Ru5PL3tg2nLEIi8qiN2f+" +
                "aOeAsxqtgiiTWY5wCp062E0l7KR9c2Av0ojUY2yYtwnubmHbMhfKeTq54jdDR2cgJMNJtIa8ewU8X8Mq" +
                "gGaQgbDNhDgSbUm0QYefrAFgzMHxYDD4loWDZWQQIyqSn1lpdfOX5xwakCIkj7zAJ4+YAl/MoDQ5WxPe" +
                "M6ABFqXKdZ0DORtNloPsLcRUpn5SGthhiOXZrmxWiHUk5hzsd00pgyShsQSSZkRHkNfOfBZ5TVJYYNSD" +
                "4T/IOcasIF8YRyN0JDAmVqoMDgpnxyTgJoPI8pZ0tcpqoynguThTg5ZjE5gweHy9tk/QCc3RQfnFg30w" +
                "d+CbnCPnhDbqT4zcGGCjtYVVQLo5zp3ARzA3sAhswawsKMEB7PztplmIZ73VdaGnJXk/TJoA1H2ctD9M" +
                "IFcEutKV9eAZYlzjc8BWAS7i9CS4Q9fOgYAwMKQqJDOQgRkE4S2Laa3rzYBMFS05ePyqJkOC7COOoNns" +
                "qqc/2hE3JkX+EO5tOxoCZN8ZZBcgor0HiYkRMTWkmcHg5WZeGz6azOAXCIVHis6QGHX7+AGwa7Omrcmz" +
                "xfXYrF40QpB2idJMOQLvLVBu3cZBBJwkQchQgl5A5O0wEuU5c9NEbwRgdWll9RtwkeQyVbaAAHasXqFh" +
                "vgPWlGCxQDFKXenauy5N9u4HOHeQhz3CwPLgDkwn/NNrFAg+8+Axm15KFicV9HR3TEj4URcAheeif+m8" +
                "B6g8wkMDwYXzAIrHVGc3iHBnD//vVB/Wqa5rsIuLz3aqfvj/Jad6n0/lcxHlKHrn8GsvwjAqiPPWoDUw" +
                "FAfgz967fxKZ4CXT60sZvXt27SlZe5Mn6hDMytQ0a4Ops7Xd8pjEPzR4oEw6A1ke/EiphyOez6mFwT9a" +
                "mFBXaABqyyb1YZCUzexAUSvOkfT2r4IhJolaGjwHgkyFmXSwRJkBHChbVNOpbYSntdwCPeAcSFYMVA29" +
                "DKk/muONN4alJFksTjlAZZPUJI1CV0GxCkU3YKvrYl7kfTNKhl+QG6lm9oyzcrRnXgxYCEA8tYeUfUUF" +
                "XSNCpNz+sDY1YV/k/BtrRxhRCYh+zgmVyOtqUYFL0jlwXcJIdRd+C6Gl+u1BWB1lbBe3wSzXRXDnHZ7j" +
                "p1+jgCKRP4mQ/239QLpKRkPQ8g7WxVNrF59pbW8MIkki5jAngzkJTYWIOQW+6DTA2HldlSHxs4x7GOzY" +
                "/O3gGrCC2RORG4FSYaawoaN2gz7pM1EkYPEj5yAeIpN4T/pL3HPvaciuh+wR+So4GxV3Pk+CSksuE5Nt" +
                "oRrSLYUQKUOmktJREHhirXGhV8b5sqWJdc3eRgAG24Y0xeeT7vASizkZKxPyDBEmoJWV7OAYrBZXzkZU" +
                "RQmJvce7IPosH4drsZzrycEr5Fhs69LLym5D+YdTTFwaCOW9sKTPXGKKNCTEYRu2zRYIIlb+1JOwuaSy" +
                "DIe/fMPxGYQHvFuZkFQ0CNxE8rpSU8kgjMUCqy9awAYg4M44ZdQ5lQJf6IzAnEFXxKiSG1oijIAO1UrY" +
                "hYCJSysacA6zdEzVNZVUtc/aDkc+OUaLVCZDY15vaLnalHy65eqb8UtTrZjqW5xNBofxCy2zSVKE1+Eh" +
                "1vBhgxNZJHJnbaiaIuFnjysjLJ7dVHZd7UUDSxMeJOO/rZ+nEgWOOMFANUqfZfZHOlKivW7UGPEFHRBc" +
                "hZAHJEoEDvj4Gpa/wBS5r2jmcapn+mZlWELQX081FzWJSlGl+JcJHjHmFaV6GCXG5BpBIJwIPKbyxRTj" +
                "IWhHVO91WeYVtRgNVJ9+ap2C9aTqEClxZUsghF8pw9ztssCEClY90B7xbmnYW/8O81bJuO3SVzpgImyg" +
                "gpVxix5kfATDl/JmJyx8mYJ5gWpD3RVwLMZcv8EgQQxeUhydSt2HhvljKwcbxncywEMd2z1wkWFnf29x" +
                "LuJDS92HLL5Md3ia5y6VLeEBjqsoRYMFDgr8kkEgu7eFbR2EiOYOogpEoWjYiLM9AmZPNxDfn56dnRzy" +
                "Su/I+nYWCx09protalthSVvhkbvGk9eBgVP7BowXaQkVqhrQdNcTkgLLe7TYu/PXlz+en3wtmK1WaMsw" +
                "yq0CdpQQEQNMWw8dNb+PsY/KeZLHFviRoPr27fmbs5NnwVjHZXevSAuNwHiuRSGE73RCOKBKr7DQH3Op" +
                "uwRGYH2Yj7BUcwaL52yJJAMKe5sSjW5uXEEtD7RNItERb/JyZepwFgC48BEj1zDW+vdfzHZ+2ugMHv/h" +
                "P+ryxXfnL68xufrHJ8sfJtDL3y/Tkl2l9Aq3HImhAyuHCS086jrDCZjYYgGknnO9L6QYuPAE8kLlvV4s" +
                "cmNCQSld5JieMIiYp6q9bM3BnlUqnwavAGAizHyabkicMmXavru6fPMUuyAk/fb+9PX3ikGM1WkQaLDE" +
                "QSOSUypac0+bTp8VLuxdz1idU6xRVDu4T5pF+R9rbyDMuTHH6tF/7yOh94/3X2JIdPZif6T2a2sbeLJo" +
                "mtXx06dwhNElEL3Z/x/fM8Etb5Xl7F8lZpO5KFERMimhA0aeRbMPk4qMTtw3xkjufVaC6k6LEs5J465v" +
                "7YguVmOZjv7gffaChYSgIF5oCGRpTpyRmLVAKzR9nEd1x1S3hj0KwvRZEaRjFajAD5EQ8LBPiOM//9df" +
                "n8sQdNScbICB29ve96td/eN7BfxzBguxgV/dxa9+Lb/1QwQ8Laf213N39Bd5hMXvY/Xn50fP+DNMqHFI" +
                "gdGyHwOhwtrWef85BjeIkF/FV/Pl9dLmbYkDKNHQ2NV+kHEU9y+V678vwoA9nbH6Tu0dHCxXKHkjlW0g" +
                "RKeoL8NEq2Qr/bmpNqHYDGLms5QQGE19vADA0CGg/yfd5Pj7cAR/xwOqGP1Vvbj86eRr+f3q7bfn785P" +
                "nsnHl++/v3hzdv7u5Mg/uHxzfvJ8IKLr7RZ5IdyTjMLnAz8oL8AdO99uEofGYl8c4edgqkvaX8OEZNgx" +
                "J46pvxFTSEwEduhIrjtvvvbjnH12fgORUXwLiNNW+RDy00i951rAz+medc0Z29JU8yYkqztWCTO2YDoD" +
                "fkD0caTt5KeTw+TT+0Br/PQzkDrdEtNfdkUZNGQ7GlL4KVUFh2ESGxmyz4x3rfOixS3IYYklaNzh6+Td" +
                "6dnFD1ewn3RNz2SCiQzm6iZThUWHchqUiPSxJFVyZKmflYaAZKxiBrIDd/Lt+cXfvr1WBwhbPgwjTtyK" +
                "klA84rTonNC8LqgD1IUhr4dWz6/D2Mk6/CFZ575VMDPpacfs07FZc3vNl7bi5IJ/BfPj2aCvk1g1Kmo6" +
                "SnM7X1OsogwRTXE+nlhR3tvVSGpkXwlRBz1NFPoFkeohD8KVaOrW4EgYHPhlTNz2eYFOr/VWEZOaYnsJ" +
                "NeIWBgz8vght7UnGdKwGnPkNrQRJSj8Z91AIFlVIh3aSXGnLj2Yed9H9RF73y7sgPIp6x5NsFU+faCB8" +
                "3zx4RV3NIZ74JrGw0jlN3aMo6bGpEHDEMikEP+7DxwGucS0AqCYlsAZiPCQV6Gf4E9oN9p7RANrNDppT" +
                "4whPeiBSeTR2kMyjte/iprgf48MR79PcTSit+CC7pTP8zs4BrqybUXKtgZMFIaOg79RXmEn8SmW/wX+5" +
                "OlGHyCytjk9AwM3sw+FHTE6Gj1/jxyx8fIYf8/Dx6GOoX3x4/pGefSkCfCIR2Kum7iym9qZ4QSPl/WKM" +
                "+8S+vYWh5oI4VixK7BswBR0HgyJ+GPmiDLyFDzrLTCkHcfcRuWS7o7np6mPIwidrsSszd9h8ZPKxj0ND" +
                "8iS5R+GzEnT1g5rHdXqlofKF2h6WY0B9sKNPzG03igE6ycMOWtstZJ1G8glmiiZ0V+lhsrjhAs69jds6" +
                "mNml5etWPoGUXN+hwyJSPL3047M4DvBz4eKK5iYLiT1ibQA4xBldOeT46gyxIW406EPv8pC/69cZ6XnU" +
                "H3wZnXBnfOKc+1N+LJwcjDszbsPj7oQHYF6PMJzF4Q873Hsohk05fUFZcl9moeNYoHy0N5xJ4SNDW/mA" +
                "EdS8MLeGKlYfwipP4C2KeZWZCV1rHMUdfJW801PA4WMsWvhB8Ulv7K4Xcm+Sr6px2YM64TRVc0ItKLJE" +
                "HeSmsg0FBNhTACdR37rOORBuc09FWr0sIdKj2OE3U1s68znFlyh91/uwX3x5AL5vS/q9utssJNjvxHnC" +
                "lIjrGCl5T1GGb2hQ1bFPUgqtMM93T1mVs0ezGVYZD0Qo+ZIdpqKG0Yzrem4acRc2GSeXlYLBmeEtnZ09" +
                "KwJjQjAmvGbcAzbC+17KXftXewNfwPiRh8ZRk9rMSTr//WTtQaxMlzAxC6Q9VYGxR2e9W6Pu3hrWdvjs" +
                "mWN9eviAEostnpaGn1/y8ryWklvamdg7/WLqsMHU/+dXyS5iSYqOEgHgSMpkoaQRAg4sx+Shah/yXYLt" +
                "gZ4D40cYeexAtFeB+x3UZPVPo5UW7B5AanY62D9iprotSJ9nqqT+05kpqYzEikUWkZzFZqY+5XYf2v81" +
                "ltE3kVIO5QmdzJSpa7Ql3se59Fa+V3w9pXvRZnI3wZmTMHrHkM2nh/y2NeQ/1Mrtiu5C9T9gzMnNWVty" +
                "f3DBXwIQQ0DuNMwLl3FZk53ScKtnha9PhNt0NIHaYDvpPR0gbzgVt+ZqW7GikixXnyx2PAYx5079Md9E" +
                "5e2R0Yk7xOUQFpyfYWxb+zM2C3Ooddkpd9+zhY7TcR8E/g1w/jhgkNCIQLy5vAbw3JG2hD3glVi8GCa3" +
                "YwHhhkU8dMuGze+FXnlPvxHsqGa4RRPAJoGDv9IYu7SRHreFWcNPH8AKaZJvBYmcJut9QF1eQAc9LTfS" +
                "WD0M3+SACuBa7CBvazKj48QQdLKzxE3yctKcDSC8rOAxB4lZ4vc3hKiWQxeG0rX2Kcxv/DGVWUU9BqE7" +
                "qTOSLrpmC7AqVKRbm7KMnGI/5YlVy/cfdMQzGrYRd85zte737t2H8MuJDXnlOx6Y7l5OG7vWtTRieO6G" +
                "bbMK6L68pVJm5PZgbUGSdOwZWeJ3BVCNo3dRg3qFecxzHjDir0ehm0YV2TcKR1xf1Dnuo/OOWm2AUEUe" +
                "VVauZTODdbnGpgUYeURSbmvUEb7shHue0Lo9zsLa7Ma7ysmyHOUlVGGZVRBVyaKxx7rn3wBCjA+E+mQv" +
                "uH2MAPZvz4SBoSLNbdvYIN10mIWmDEgdu7oDSQjayvf8tK6ly2Z00bduDbf+JH0921pIFWIuHfM318i3" +
                "erjW/O53Psj1pCh757dkUWw7X0ThwrhjSwtHu+yb1xFsylFuqVl7pibTbdSzHTEGLbOzz0tMD1Md9klr" +
                "+y/NEFHFwWSyBNQNiBXesfZfGIEyNIpSk2nuOd/avCcitgTRNzlQowz2N9IShxKBcrGHFuToI7kXLG3u" +
                "mPumV2KkvSWtsLWg9Mgl6xMBvO0OEuKvDsx2WoQg36xRdK7vX67wHsTHd3HXrOya+nmQ4Pim/10ovin2" +
                "UHzy2sqOOGwG8mNj7IqOxcYNPUgs9JSmwbYV7wxl6Xvgvy2ePuMlUvCwM+zOYss67HwPSriGTThTf5Pg" +
                "K24y4c6WWZCvJAoeRvsvV2HDblfc025qeEx9r4cj2iHfbTgMzcbNlhQkvhuNhfdSsKkJDpzQQG/MUO/9" +
                "RPKTDk5GgGm54RApnUMWoMSTUxdNAfh4RzDg3caWMHkpw/BVek5sVVGzMjxGYH2/kwYM4vZYABPpQ2zw" +
                "XBa+fIpcaCCdD016RKK2NA5DDnsEIwBdiqV7Tm2wDU3dPfXxoYzPlsQw6Sd1op6N1Hv48fVI/Yx1kD1f" +
                "Tj9/c3X5bvLzSf/Je+wa7Dz5CTv5+IlYUmJZ2MB/1JngXi9DJMDP3sLDAY9uPffveLFo+hvNvZIOAUBH" +
                "NRj8LzuJ8LagTgAA";
                
    }
}
