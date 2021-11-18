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
        [DataMember (Name = "group_name")] public string GroupName;
        // A RobotState consisting of hint/seed positions for the IK computation and positions 
        // for all the other joints in the robot. Additional state information provided here is 
        // used to specify starting positions for other joints/links on the robot.  
        // This state MUST contain state for all joints to be used by the IK solver
        // to compute IK. The list of joints that the IK solver deals with can be 
        // found using the SRDF for the corresponding group
        [DataMember (Name = "robot_state")] public MoveitMsgs.RobotState RobotState;
        // A set of constraints that the IK must obey; by default, this set of constraints is empty
        [DataMember (Name = "constraints")] public Constraints Constraints;
        // Find an IK solution that avoids collisions. By default, this is false
        [DataMember (Name = "avoid_collisions")] public bool AvoidCollisions;
        // (OPTIONAL) The name of the link for which we are computing IK
        // If not specified, the link name will be inferred from a combination 
        // of the group name and the SRDF. If any values are specified for ik_link_names,
        // this value is ignored
        [DataMember (Name = "ik_link_name")] public string IkLinkName;
        // The stamped pose of the link, when the IK solver computes the joint values
        // for all the joints in a group. This value is ignored if pose_stamped_vector
        // has any elements specified.
        [DataMember (Name = "pose_stamped")] public GeometryMsgs.PoseStamped PoseStamped;
        // Multi-group parameters
        // (OPTIONAL) The names of the links for which we are computing IK
        // If not specified, the link name will be inferred from a combination 
        // of the group name and the SRDF
        [DataMember (Name = "ik_link_names")] public string[] IkLinkNames;
        // (OPTIONAL) The (stamped) poses of the links we are computing IK for (when a group has more than one end effector)
        // e.g. The "arms" group might consist of both the "right_arm" and the "left_arm"
        // The order of the groups referred to is the same as the order setup in the SRDF
        [DataMember (Name = "pose_stamped_vector")] public GeometryMsgs.PoseStamped[] PoseStampedVector;
        // Maximum allowed time for IK calculation
        [DataMember (Name = "timeout")] public duration Timeout;
        // Maximum number of IK attempts (if using random seeds; leave as 0 for the default value specified on the param server to be used)
        [DataMember (Name = "attempts")] public int Attempts;
    
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
        internal PositionIKRequest(ref Buffer b)
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
                size += BuiltIns.GetStringSize(GroupName);
                size += RobotState.RosMessageLength;
                size += Constraints.RosMessageLength;
                size += BuiltIns.GetStringSize(IkLinkName);
                size += PoseStamped.RosMessageLength;
                size += BuiltIns.GetArraySize(IkLinkNames);
                size += BuiltIns.GetArraySize(PoseStampedVector);
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
                "H4sIAAAAAAAACu1cbW8bR5L+TiD/oWF/kLShacf2LvYU+INtyRvnYstrKdlkDYMYkk1yVsMZZmYoiTnc" +
                "f7/nqeq3GVJxgjvr9rDnBLY47K6u96quqtF989y8q5q8zavSvP53U9ufN7Zpzco2Tbawg8F9c7G0psxW" +
                "1lRz0+LnRV1t1uZ6mU+X5jovCjOxZtPYmWkrM61W601rAQkb7WgxMvfqfLFsx1m9ujc0VW3u4afmnnlg" +
                "GstlplnbaT7Pp5lgMMeK1aZo83VhH8hBDcAX1TXAvS7x7cqtq6uVIHP+/uTVDhbZpq24cJoVxdbMbGvr" +
                "VV5ah3ORl5cG8LCwqYorwQKQh2ZTFqDa5JdjLhkLzXljsqKpPJp2NmjaOi8XygVZQx49N++rSdWetxmI" +
                "n1ZlkzctV4Fly7xsH4LYmVk7PjdCJrHHycoxpSor00UAy3WgQdZW+Ks2/6gADjiW8qzmoSPzfDaTPVlh" +
                "GsEgT1i1rqurfIbjsV3oAVzPKKVqy1214NtFMT3yIXnSmKpzMGFdLAFTj33z/fkFqW8z4KePPAUOb5zp" +
                "5TTZehaIFGqVSFSgkSheAUaSi37/Mmu72yBeCAgq0C7NNCsJXhi3AS83DWkKauK5Pq3q2jbrqpwFQQ5W" +
                "1ZXN2/GqWTQPE1EKnWMhRcXcWEGHIm7rbAen1YboTuz2a9I3s/MMyjzEt+TR7lY8tat1ux28TB4mC3jm" +
                "qxyUgDCleCMylSOzqyqfcXkBJlFmI/Oifyb+n4M9djCpqkJ3jOMGgj88e3fx+uzt8++OdgxdDIVMc7Zu" +
                "TQYNUgmRc2Lkr+emrNpoH8O4VWB524RKWrB9pqabEcwkL1VFAabjW2QjjcGLbsRjsnJrrrIC7knwCCcK" +
                "iqnRNkPqEsmX5WRCviirOhpvutq7OAh5tVYr7XBgCPJt2VM6p6YUvlXddKj1jDaaa6a0Uav3IGbyuRw8" +
                "dliMr+y0rWgTy6wRym1hV5awAt2jwcJWK9vWW1VbuHF7nhDhYZHAN/Sp6lDNOqtBNnzibfJvUvKbfw4N" +
                "cJL78LEr6T0UHDqyj4QJPVr2UCD0HYqMnYyE5yuIhXZWwuVZY4GKnc9FKEc+tPE0F85034qhzrt/Hgzn" +
                "sZTTkygYyLpX2Lk+chpY1TOoVsqHBvHYMQ2+EerCbxrhjf6sW+BZcLqLCcKtWzUDDNynZ1SR7CZfbSCY" +
                "AuGWB+Y4hrxhkMqK6aYQUQ1mm1plxgXVpk33lpvVREnApqxt6dwacwjlVldcg3bIntGw+doUNrsSSh4F" +
                "1+y8l7OPaOIu7IjqYntNG4yx5GgAK3vyOJw4GHwxePY//OeLwZvzvxyb/XHiCx8HXdrkg2DTicTZBOyK" +
                "4VMj5NDkIztS6kLwBQNzMM55DyqMqO+gsWVT1Xr6t/xSo5Ssi1FKvnFxaZVtoc5gs8+qzMnZq57/ZGLQ" +
                "AS3eAuuSI2T7eFbNx73DnrdtNl0CSggriH//gFZB7pn/TlKtlXMKaQpxNPD7X/rtZ7Ibeup3x4A1dpAl" +
                "LBYZ/DhCODNHqBYMWLIVDbVTC6OlvYiOAGFbr2v4PERS8NPM8vlcUwYmAgAZMNSUR/Z7mVKsVdMik8xX" +
                "66puMzh76it8w6zwCUYgdVLNcngdOhQBhYX0jkxtoO/1vsXq/DKnW8fHyE/s8XGShUwszoOqr2dKa94q" +
                "8m2ickca4vNmTOI+nwHsV8FgAMyXgxWIBi6rAlkKMM/IhJltpnUOmYgnE+qEdpccwRfh/iHmU0PqYJHa" +
                "wAgRIsZp3WTBQxd7D2t7xdyIDhEJb95I9n9EbOBRkPoz3TwGAPOHjqV5Z+uhZPA0uIAcDePSK9w+pnm7" +
                "3V36sJHFD5sjsdC4BYECaqLUkx/rdUEzc/7ZAXi74u63RyMh7DTSgh2bMgcXqHAzhHw1UqSTdAmSsSgj" +
                "ljYT5+/sWAOCeG2cqwE7nidcY/yDHtUWGoYbAS4OaZri1gB6rkHPi1H8h1qS38ojQizimUpG3wcyDoo3" +
                "y6L/yeo62zZDOYFmJGKEY1f5d5Gh2IXWRYW7jdrzKru0usmtB+3UsGqtV6CR+RtjuUTobbUJVyahoqwY" +
                "vFU+WdNAsjhl5q3JrobcIvcIDU6JOAVvTdedNpJ7So3KNqG9WVabAp7VwxA+NfkvcPcgGYxUOInVSDQv" +
                "IfNrnAIygw4ENBPmuLzKJRdyn5m2AKYSHA0Gg29UOVRHBjF7Ev2ZF1XW/ump5gJiCMkjr/DJI+XAZ/Qp" +
                "7UwdimJNZwKvUs6yegaOtpk4D3G5yKJs/aCwQDIk7OpatmsSHvm5gAev5f6f1CZW4CrihapsZ79qfSaK" +
                "mDPXYY4PVWeCChVjxkzo5DErJOUUt4GTY9FxO0UqCYRgoOW0tplkOq9PzGCjWQk2DO5fXFcPGIYWDFH+" +
                "8OAi7A2iU0M8s4Zu6g9K3Aiw6XFxChRcE9sxPsLj4BCgYNcV7OAQmL/btksXWK+yOs8msDQAZgUEUA+4" +
                "6eAogUy0j6ENZeXBK8R4xm8BWwa4pOlBiIjNZgEGYmEoPrjb/hSeEPpb5JM6q7cD8VZy5OD+K/JYrUgk" +
                "Qs/ZtVB/exNpjPPZ3QS53WSI+vneUmKgRVM7uKJY73AOR+wzuL2ZXdRWryNz/IA0eKiFCqbbPosAgZtp" +
                "u4GPxrJ4oDrX1xpNmmazokJLCcDHDKpus22Q/ibVDXGXMA1k3Q2zUN2zsG2MSQCbFZU7/RKBUgKnmS6R" +
                "vI7MK7rnG0ingN+CbRRZCbG6AIZECsd+j9uGxNknzCsPb+BA8X92TZ3Qiw6v0/Klq86kup5ip4zEP3UO" +
                "KLqXUabzPaDqCg8Nuou7ADVkkk0vSXAHh/8PrXcbWq9ruMblbw6tfvn/pdB6W2TVe5FWJbrX7wuvwlgV" +
                "1Hln0TVyJS7gv73v/iZswpfKr8/n927BO2T3tfd6LtkMnmVi22vLKtl1tRM3RYT0eUhVsymc2eAHKTo8" +
                "0f2urPDXDTbUJX1AXalXvSs6HTr7qMyQDvHLHgkmuGPRq5XlnRCaFXbKHZOaAzKkUFTLFQ6X/dbMKrAE" +
                "d0LxZTA4BhvJr+mUoZQpW6TsA2dLk3OFSFnFgCFJi6Q58NioL+WzvjMV9++oQ1Vu/lhrcYKzHgYpAohn" +
                "+JHUWWmm1yRITNxf3Hhlc3hJFtBWFTomRFxA9AtONCVvsaiDtLAWCN6llOYm/BTSTPPLHUk7KtpegSOW" +
                "18xRlIMdsfPTz1FNyedP0RR+ur4zo6X/CJT5YNvEe2yXpEldXUKpIC4qWsMKDasUDL9ZuZA8mAEEjs8b" +
                "rVsSP7t1d0WgOsN9soNAVEiRviGsi0VDRiLSyDT/t1EpwOJHLUzcTV3xlpKYkmxN73Gor4eqkkQvXJjy" +
                "G18/oQFLEGUBzqfRaQdEWBmKllKjQh7KfuIyw8XKdybxk6/U9rAY3FcfkZb8fNl9cJ/dG/GFdJm1JbEC" +
                "sqxcsXAE56UtMqR+dFGu0Hd/Hzxf9tNrRezVekboAbNBn1EKNPR7tOKkfYHQwwvn+SImS6WhJA4cqg0C" +
                "8P2ku4dWtkcsaRnjHjjbagkEaYJi6jbElEGASTdDSprg0hS5LJunvluBw5F1T7V61LmdQhpyUVB5MBIp" +
                "kRKFVoQRSGGPRAMIvFvaycB1DPd/hhI0G7TLqFCOtIfjzyjtlK683sppNVpRcsmVLpv1B0sXWHpZUk9G" +
                "sODjyput3GYuwkN25YHd2B3hhXJtpYXi0s+eMBA95+ayrK5DucKtvxuz3GOOz10aKJHQNSJ9rdnf6cRs" +
                "+lmjUguld5Q6Hh6KAgksCPANDn+NErlvWkonTxpFTtSoeaheMEhPMu1bCoOCAem/Y9btFqWUepQYpeGC" +
                "EAjGQ45FfOd0WVfck817s9Vtuaij68v2y+qSoye9Bs+A86oA/f6YKcu2KyB4BWaJ01E8ZdU7/xUrVnHZ" +
                "bpMr/Z6qpQ3CN7ZZdqHyCdau9Iu9cPhdBPGC9iEDErgDs7yPIpDLEJKO7NBMXINHlvk7qiYUIF6bYHiY" +
                "xYENHnGU4vaOW0mInHQLkfwuYof5D3fLCT7AI1FKOYbtDMntkkXQ0au82jTIAu0Ncgaij1RTQ6r4nNFg" +
                "skUS//zk5NkjHvNe/GrnpDCHY8urvK5Ktqjxc4vcF3APUaerUUFSU5BuVAt7bno6kaOBJye9P31z9sPp" +
                "s6+EpvWarooprNdmV/NwvlWQDpMwv06rT7l1k6cTUohEvnt3+vbk2WPnh+OZ+4+TU4ZwjNdO852oJfc/" +
                "lP6tk5u/xspkCFaw6atXVJZI4NAwUUBegbXeY0SHim4JODlTFIU3T4jg2RqlTZ/hAyY+Mhn1Cyv/9efz" +
                "i592K3CPv/uPOXvx7enLCxZPf/9m94f8efnr3Vfxm9KU0DEh58vgyVir4hUWuYFU6uKoBDi90IZeqB5o" +
                "Zwmqwv5dJ7W4tKFdlJ5wLE90f2ztyN1SNAZOqzSziff3gOIBziYpKi7MSvns2/Oztw85zuBqaj89f/Od" +
                "UQBo7wQthqcNNpBcOumrPVc6U1E418eUkTmV3IFNoR2piylJTaeqLpG1XNpjc+8/Dsjhg+ODl8xvTl4c" +
                "DM1BXVUtnizbdn388CGuIlkBbrcH/+mGH3Q0DfmgFPRK5xxVei7HoXASLjB/zNsDbMqR9cMQLq11FfV5" +
                "AWud5AWuOy5C7VNYtlmVif4SffJCdUOAkCqavjtZS2FUrg34RC+nhVGp17NQ6oiVLqOAOTaBAfKMLMCz" +
                "PguO//hvf36qKxh9tWaAdbsYH7iTzv/6HbqqyBLYWg1y6hx8/nPxjV+hsOUoc3C9aJ78SZ+wl31s/vj0" +
                "yWP5iNU1FyCJrq7dCkT+axRzeo+ZpJAQf4Bvy+u3q2q2Kfi91Anaan3gFRqq/fkq9relDEzTTtRSJxVK" +
                "w82ayjY00y1ybEndoHEyM8iao7/uQDN84xia5WuNyHMmPhEAMLp9BnYxRs2gHw3xH4oCbP382bw4+xHB" +
                "TH8+f/fN6ftTBBj9+PKn716/PTl9D4fuHpy9PX321Bu8d1ESa4iTW6W5mvcK6J7gfuEGRuLS2LiLK/we" +
                "lqrcYGrYkCw71vKvjCCyBKRM0IBNdt14Z3UQ9xxoiJOhDHc5BOGCql4jfhyan7Si//cUZzJZbk62XCBl" +
                "dBj13RDvT4E+MH0UeTv+EXlJ/PRT4DU//Z2xPEFJ+e+wkgoYxU7PiX9d2x0OVPGEU6M3VrrR6s83RMHd" +
                "d1SDPB4Kd/z++cnr78+ZJyVneiELTApY25TKFVUdKUVIIdEnidKPcUf93WCwCsMPsYLYgTv+5vT1X765" +
                "MIeE7T4cRZp0riTheKRp2blneVswh7SFIz2Prs6fo9S5c/RDcs5tp7Cy6Hmn4nNXlP1nImprVcB/hf0x" +
                "2+/bJHs/eS13YZ3EQ5st6pDwlPt566S+b9ZD1+n60jHVG2mPmUGlesQzK42WurM4MgYL76gSxsuA3kLr" +
                "nW6kTLH2SmEiMCYJ+r1OwJDhScUTpW0t3obJgKQwn6y7OxqBTCj2dSpU6RAPSiO+ERop/kRp9i5iES+a" +
                "IQIl2PJ2SU/hx9sRIFGIRDbxdeJq3bCzjIBS5eN8IMhk1xOJT/Ph44CHXDgA0mJysHhAUsrzO/xVDFkg" +
                "pka5QLDZw3eZBtFNd8YtT8g+rnnKkPQFvHTI4sMTRdXejKU4eEcIy119/ziAtsthdfElBC0KhMpBdmO+" +
                "ZFnwSzP9BX/NzDMj1+zMHD+Dptv5h0cfWWkMH7/ix2n4+JgfZ+Hjk4+hF/Hh6Ud59vl48Inq3he9etfe" +
                "Jmlvj9c4MeTmfw314HBkJC8udg4mTtuhLsjrYDDKD0PfY8G3+JBNpyiU6kW8+UhZVd3VOlX1MVTUk7M0" +
                "vtkbjhaxROGS01AxSV6D8AUJeW9DpsHdaHhsZ4u/6JE5Au2DPYNgze4kGCdU48MOWbszYp3p8DHLQxwV" +
                "ru+sRBtenbl9JDsLXndV6RtSvnaUvHnjeZ6+reNLOA0obMKbJ5lOULiUJBb8ISNWbN3dR/ssIoSIZDCI" +
                "zis//nW8zjovn+7SsxiQO6uTQN3d8AOKu3ox7qy/Co87y+9EZj2eUG7h0544H/pZEy1fSP3bt03kduZZ" +
                "Hn2ND4TytdsL685RnpS204dwBHo5KMehzz21Y3nvcBiP/zL5DsWkK/sxJBP9Mcr+yj3P9a1Gyd60iSGT" +
                "bQjVUKnQ04nCMIeoF1Uc4UVlFYLFhdRPo2v1QwfXUw02L9Hd0czhF1tXcvVDB1BecfSD7Ee9TsqdSHxX" +
                "wW83VvdSSCcNCBKJ1O7O5eh1V1+0kKZhn6GSVrGut68fqjWj+ZxNQs5BhtajTLQcRZ+d1SgLuthQJevc" +
                "i0bBtWBDL8MKQzACAsV8aqse6RHgTLufidyHuRn4vsQPujIuQt1oQYX8Z1Owu/EpXbYkJSDM8yinINMn" +
                "J51XOptb+1G7CbMXDN9nELichsxY2sUQzW9tXzkhu85ZOrLfu/WyVIg04Df3u17HBpNcGwK0oet4hW5F" +
                "SCjYZpmFJnsocjk6D7MFJI6UptlDYreXdjtR7uhPEpQ03u5EV/bG0N/lkLojQ592Sq6r09nlKheJv4rC" +
                "Ed2Kg0c9tt1yP//vOkA/8ynFkgdy7TK2ruk4fAxLOp/ByrOJvKRsxzdjbhyHxbsrtp9c8Ut/xb+mN9uX" +
                "tPlhoIRkrWDON5KLcgJEUqKY2uk44Cxvptqh1MBztDNZEl52EAOQ9TKw2inhZQGwTLnwdytI8wxFOHZW" +
                "taUEfVywbeRqzTJUT8BvHHLiYiJ+PI2gcCfG0o3oZDSK0L+qJjomr544bicahP4Wcj9OKkaePwLg7dkF" +
                "gOuk2AoY8K1VvsXlXmAFtaIiTZxoDZiHiXbPOvR/sVXAomDsoSZpgX/7MM5SkxdXObrGsZqsXEl+DUcU" +
                "svjpQ5m9Ag/QTtm66eej8FsUqPsNmkxmvanFaY6C5XdKryJGiWNufhoAvI7wskI2wsJjoqpZiQJJvXoK" +
                "8Gt/11QJyXRAmBzqrJT3UKdLuBFpuV3boggS0ljkuSRvgWiaF1UyerGhvjOmvbdfeQE+ZFWNeI1Xfk5B" +
                "2e01s62us9rNTniZBpRV6bO+iiWapbNeawgH2iPzMVrsWfGFfWlc9N6hkAFeXfNUF8hLIPoaUCn+TPIM" +
                "xSg5WLM5ubeY9RY8yqUqkPwuASfWrLjmtAEWPhG9Rstg5N6OJsJjObQjT5yrQbpriaq7UUV8I1UFhEzJ" +
                "HRhHnnvxCwBi8HdsF9egM12jPW+0hHWho6xD1BxXbjtCos8Cj+OMtWeGAJPQpf4QhU/sljdw643VAZ1k" +
                "+mbX5tg10Nav/nIY92s0mg34eLu6uReGvLqdspPP1vpiGfWJ2cSOxam+9VTM2wSnZ0yzAgmsA9ppxnlr" +
                "PzG2mzzIKXvHr5yPUX4DSTkaMBNb4lrxTA7SJVSJrz3739ZAxRG+q1CnjAP7UHfs4+yO/CYFmWrhnKGc" +
                "8MillNqykfM0tUje1HXD5ixcy1fOEzt/CZcLhnjKktOFeu+gg2L4+f35XgfglVpNSK7k/XccfJDwSVvE" +
                "WW07k+kbMpvf9H7ziJ9IfeQiLkZLFB/Ng8F6jqWu5UJrmyMPkY0atM45ZOKDnTt5P/h3+cPHekIKHXhx" +
                "hEpdKGAnziK8FC0Ucw7JEevCYCKYHT/gfuFPiCKZ/2Um6sAxfCTj5LbGY06ePhoKfvpuwaMw5dvuyD8J" +
                "zfAOPhABpTHXjWXdINi53yVRsMENB0TyhYaqt0MsvuANqEuigru/J8774LCjQ165mJS6GZGqxGysQmav" +
                "uhdc0lzABTZVu0TpSAtvV+FXOkmMDFxzSUePPTI3pgnGoy6rZHvKqxTd1NlWYYq6ZzA+Q/EljpD8/IgG" +
                "xWO08vHPV0O0ptmucD3w07fnZ+/Rcu89iB159+DHMP/gHKbIKZz9L5TfL24LJdrQ5QPvynFLk/eN++9V" +
                "qT76d4l7V00BIOHoi8F/AXQIl6zkTQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
