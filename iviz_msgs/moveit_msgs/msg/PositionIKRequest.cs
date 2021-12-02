/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
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
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new PositionIKRequest(ref b);
        
        PositionIKRequest IDeserializable<PositionIKRequest>.RosDeserialize(ref Buffer b) => new PositionIKRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(GroupName);
            RobotState.RosSerialize(ref b);
            Constraints.RosSerialize(ref b);
            b.Serialize(AvoidCollisions);
            b.Serialize(IkLinkName);
            PoseStamped.RosSerialize(ref b);
            b.SerializeArray(IkLinkNames);
            b.SerializeArray(PoseStampedVector);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PositionIKRequest";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9936dc239c90af180ec94a51596c96f2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cbXPbRpL+zl8xZX+QtKFpr+3d2lPKH2xL2TgXW15LySZxuVggOSSxAgEGACUxV/ff" +
                "73m65w0gFSd1J91e7TkpWwRnevq9e7obemhemvdVk7d5VZo3/25q+/PGNq1Z2abJFnYweGgultaU2cqa" +
                "am5a/Lyoq83aXC/z6dJc50VhJtZsGjszbWWm1Wq9aS0gYaMdLUbmQZ0vlu04q1cPhqaqzQP81Dwwj0xj" +
                "ucw0azvN5/k0EwzmWLHaFG2+LuwjOagB+KK6Brg3Jb5duXV1tRJkzj+cfLWDRbZpKy6cZkWxNTPb2nqV" +
                "l9bhXOTlpQE8LGyq4kqwAOSh2ZQFqDb55ZhLxkJz3pisaCqPpp0NmrbOy4VyQdaQRy/Nh2pStedtBuKn" +
                "VdnkTctVYNkyL9vHIHZm1o7PjZBJ7HGyckypysp0EcByHWiQtRX+qs0/KoADjqU8q3noyLyczWRPVphG" +
                "MMgTVq3r6iqf4XhsF3oA1zNKqdpyVy34dlFMj3xMnjSm6hxMWBdLwNRj3353fkHq2wz46SNPgcMbZ3o5" +
                "TbaeBSKFWiUSFWgkileAkeSi37/M2u42iBcCggq0SzPNSoIXxm3Ay01DmoKaeK5Pq7q2zboqZ0GQg1V1" +
                "ZfN2vGoWzeNElELnWEhRMTdW0KGI2zrbwWm1IboTu/2S9M3sPIMyD/EtebS7FU/tat1uB6+Th8kCnvlV" +
                "DkpAmFK8EZnKkdlVlc+4vACTKLORedU/E//PwR47mFRVoTvGcQPBH569v3hz9u7lt0c7hi6GQqY5W7cm" +
                "gwaphMg5MfI3c1NWbbSPYdwqsLxtQiUt2D5T080IZpKXqqIA0/EtspHG4EU34jFZuTVXWQH3JHiEEwXF" +
                "1GibIXWJ5MtyMiFflFUdjTdd7V0chLxaq5V2ODAE+bbsKZ1TUwrfqm461HpGG801U9qo1XsQM/lcDh47" +
                "LMZXdtpWtIll1gjltrArS1iB7tFgYauVbeutqi3cuD1PiPCwSOBb+lR1qGad1SAbPvE2+Tcp+c0/hwY4" +
                "yX381JX0HgoOHdlHwoQeLXsoEPoORcZORsLzFcRCOyvh8qyxQMXO5yKUIx/aeJoLZ7pvxVDn3T8PhvNY" +
                "yulJFAxkPSjsXB85DazqGVQr5UODeOyYBt8IdeE3jfBGf9Yt8Cw43cUE4datmgEG7tMzqkh2k682EEyB" +
                "cMsDcxxD3jBIZcV0U4ioBrNNrTLjgmrTpnvLzWqiJGBT1rZ0bo05hHKrK65BO2TPaNh8aQqbXQklT4Jr" +
                "dt7L2Uc0cRd2RHWxvaYNxlhyNICVPXsaThwMBi/+h/8M3p7/9djsjxI+CLqcyUfAphOGswl4FWOnhseh" +
                "yUd2pKSFyAvu5eCacx3UFtHdQWPLpqr18G/4pYYoWRdDlHzjgtIq20KXwWOfUpmTs696zpNZQQe0uAqs" +
                "S46Q7eNZNR/3DnvZttl0CSghpiD4/QMqBaFn/jvJs1bOI6T5w9HA73/tt5/Jbiip3x2j1dhBlphYZHDi" +
                "iN9MG6FXsF5JVTTOTi0slsYiCgKEbb2u4fAQRsFPM8vnc80XmAUAZMBQ8x3Z72VKsVZNizQyX62rus3g" +
                "6amscAyzwmcXgdRJNcvhcuhNBBQW0jUyr4Gy1/sWq+fLnGodHyM5scfHSQoysTgPer6eKa15q8i3icod" +
                "aXzPmzGJuyvt36+ACaeyYAKifsuqQH4CtDNyYGabaZ1DIOLDhDQh3KVF8EK4eYjt1BA5+KMGMEJsiBFa" +
                "N1kw0EXdw9peMSuiK0SqmzeS9x8RG/gSJP1MNI8BwPyhY2bezXooGXwMrh5Hw7j0CveOad5ud5c+bmTx" +
                "4+ZIzDNuQYiAjij15Md6XdDGnGd2AN6tuPvd0UgIO420YMemzMEFatsMwV4tFIkk/YHkKsqIpc3E7Tsj" +
                "1lAg/hrnaqiO5wnXGPmgRLWFeuEugCtDmqC4NYCea7jzYhTnoWbkt/KIEIV4ppLRd4CMgOLKsuh8srrO" +
                "ts1QTqANiRjh0lX+XWQodqF1UeFWo8a8yi6tbnLrQTs1rFrr5Wdk/s4oLrF5W23CZUmoKCuGbZVP1jSQ" +
                "LE6ZeVOyqyG3yA1Cw1IiTsFbE3WnjeSeUqOyTWhvltWmgFv1MIRPTf4LfD1IBiMVTmI1EsdLyPwap4DM" +
                "oAMBzYQ5LqNyaYXcZKYtgKkER4h8X6tyqI4MYt4k+jMvqqz983PNAsQQkkde4ZNHyoE7cyjtTL2J4gwy" +
                "4FHKWVbPwM42E88hzhbJk60fFRYYhjxd/cp2TaojMxfw3bVc+5OSxAosRaRQfe3sV5XPRAtzpjhM7aHn" +
                "zEuhX0yUCZ0MZmGknOIScHIsCm6nyCCBEKyznNY2kwTnzYkZbDQZwYbBw4vr6hED0ILByR8e/IO9QVxq" +
                "iGfW0Ef9QYkbATa9LU6Bdms+O8ZHuBscAhTsuoIRHALz99t26ULqVVbn2QRmBsAsfADqATcdHCWQifYx" +
                "VKGsPHiFGM/4LWDLAJc0PQqxsNkswEAsDDUHd8mfwg1CeYt8Umf1diCuSo4cPPyKPFYTEonQbXbN01/a" +
                "RBrjfHZn2virSRCI/WApLhCiGR2cUKxxOFcjlhkc3swuaqtXkDl+QOo71OIEU2yfPIC6zbTdwDtjWTxP" +
                "3eobjSNNs1lRm+Xa76MF9bbZNkh5k4qGOErYBTLthsmn7lnYNkYjgM2Kyp1+iRApIdNMl8hZR+YrOuYb" +
                "iKaAx4JhFFkJmbrQhfwJx36HG4ZE2GdMJw9v4Drxf3ZNhdDLDa/Q8qWryKSKnmKnjMQ/dQ4oupfxpfM9" +
                "oOoKDw2Ki/yf6jHJppckuIPD/wfV+w2q1zX84vI3B1W//P9SUL0tpup1SCsR3Sv3hVdhrArqvLPoGlkS" +
                "F/Df3nd/FzbhS+XXXTm9W7D2nKy9y3M5ZnArE9teW5bFrqudiCnyo8NDhppN4ckG30uV4Znud3WEv22w" +
                "oS7pAOpKXer9EOmQ2UNihhSI3/XwN8ERi0atLC+B0KmwUy6V1BnQIGWhWu5suN23ZlaBH7gEiheDqTHK" +
                "SE5Ndwx1THkiRR64WRqbKzvKKoYKyVUku4GvRjUpn/XdqDh+RxxqcPOnWnkTnPUwiBBAPLePpKpKA70m" +
                "QWLc/rLGa5rDS4J/W1XojxBxAdEvL9GIvK2i8NHCTiB1l0aam/BTSC3NL/ci6qhj+6SNEF4zL1H2dWTO" +
                "Tz9HBSWTP0uQ/+n6nmxVnIYjywfYJt5au/RM6uoS6gRBUcUaFmNYkGDIzcqFJL4MGnB23lbdkvjZrbsf" +
                "6tT97ZEaRKHiicQNYVSsDDL0kEAm9b+NRAEWP2oN4j6Kh7cUvlx47j0NFfRQOpJYhbtRfuPrJDRaCZms" +
                "svmMOe1xCB9DZVIKUcg62TFcZrhD+d4jfvK12B3c1C+kdT1fWB88ZH9G/B/dZG1Jq4AsK1cRHMFhaRMM" +
                "iR7dkqvmPdwHz9f29AYRu7GeEXrAbNBnlAINHR2tLGnlP3Tpwnm+Usl6aCh6A4dqI4oX+3doVnvEkqYw" +
                "rnyzrZY6kBQopm5DTBAEmPQrpG4JLk2RubI96vsROBw59lSrRJ2LKKQh1wKVB6OPEimRZ0UYgRR2QTRo" +
                "wKmlvQrcvHDPZ/hAO0H7iArlSLs0/ozSTum+662cVqPZJPdZ6aNZf7D0eaVbJUVjBAg+rrzJyt3lIjxk" +
                "3x3Yjd0RXijXVpokLtnsCQMRc24uy+o6lCXc+vuwyV1bfOkyPgl9rs/oq8n++iY2008QlVRovCPTMfBQ" +
                "tEdgQXpvcfYbFMF9T1IaddIHcnJGbUOVglF5kmlbUrgTrEf/HbM4tyilnqO0KAkXhEAwHnIs0ztvy+Lh" +
                "nsTd26xuy0UXXdu1XziXdDzpJngGnFcF6PfHTFmbXQHBKzBLPI7iKave+69YlorLdntY6ffUK+3/vbXN" +
                "sguVT7B2pV/shcPvIohXNA6Zf8B1lwV8FHtcVpA0XIdm4lo4ssxfRzWJAPHa48LDLM5j8IijFLf33EpC" +
                "5KRbiOR3ETuMd7gLTXAAHolSyi5sWEgylyyCjl7l1aZB2mdvkCkQfeSWGkzF4YwGky1S9pcnJy+e8JgP" +
                "4lQ7J4UxG1te5XVVsgONn1sku4B7iHpcjUqRmoL0m1oYc9PTiRz9OTnpw+nbs+9PX/xRaFqv6aeYs3pt" +
                "duUN51gF6TDo8uu0+hxbN3k6IYVI5Pv3p+9OXjx1Tjieuf84OWUIr3jtNN+JWpL9Q2nPOrn5G6sMfmAF" +
                "e7p6G2U1BN4MAwPkFVjrPUb0pmiJgJMzRVF484wInq1RwvQpPWDiIxNQv7DyX9+VU/y8Uxk8/N1/zNmr" +
                "b05fX7BC+vs3uz9kzutfb66K05S2g44AOUcGN8aaFG+ryAqkIhfHIMDmhfbrQpVAe0fQE7bnOknFpQ0N" +
                "ofSEY3mi+2PzRm6Soi7wWKWZTbyzBxQPcDZJUXEBVspk35yfvXvMUQVXO/vx5dtvjQJAAyeoMNxsMIDk" +
                "iklH7bnSmXjCuT6gjMypZA1s++wIXexIajdVdYl85dIemwf/cUAOHxwfvGZmc/LqYGgO6qpq8WTZtuvj" +
                "x49x/cgKcLs9+E832KBjZ8gEpXBXOs+o0nPZDYWTcIGZY94eYFOOZB9WcGmtK5vPC5jqJC9wxXHhaZ++" +
                "souqTPRX5pNXqhsChFTR7t3JWvKicm3AJ7o4LYBKUZ4FUUes9BEFzLEJDJBnZAGe9Vlw/Kd/+8tzXcHQ" +
                "qxUCrNvF+MCddP63b9E3RYrA5mmQU+fg85+Lr/0KhS1HmYPrRfPsz/qErepj86fnz57KR6yuuQDpc3Xt" +
                "ViDsX6Nu03vMDIWE+AN8112/XVWzTcHvpSrQVusDr9BQ7bsqy9+WLQCjEzXTSYX6b7Ompg3NdIvUWpI2" +
                "qJsMA7Kw6G85UAvfF4Za+YIiMpyJTwEAjA6fIV0sURPnJ0P8hxIAmzt/Ma/OfkAY05/P3399+uEUoUU/" +
                "vv7x2zfvTk4/wJW7B2fvTl8899bu/ZNEGeLkVmmW5l0C+iO4VrhhkLg09uXiCr+HVSk3cRo2JMuOtcYr" +
                "s4Ws9igTNFSTXTfeUx3EPQca3GTgwt0JQbigqreHH4bmRy3b/5TiTCbLhcmWCySLDqO+D+K1KdAHpo8i" +
                "b8c/ICOJn34MvOannxjFE5SU/w4rKXZR7HSb+Nd11eE9FU94NLpipRud/HxDFNw1RzXI46Fwxx9enrz5" +
                "7pwZUnKmF7LApIC1EalcUdWR8oPUDH16KE0Xd9RPBhNTmG2IxcIO3PHXp2/++vWFOSRs9+Eo0qQzIwnH" +
                "I03LzvXK24I5pC0c6Xn0c/4cpc6dox+Sc247hUVEzzsVn7uc7D8TIVuLAf4r7I95ft8m2eDJa7kC64gd" +
                "emlRh4Sn3M/LJvV9sx66dtYXjqneSHvMDCrVI575aLTUncWRMVx4Ny5u9xIgl896p98os6m92pdIi+mB" +
                "fq/TLeR2UtxECVuLtKHrn1Tfk3X3RSBQ8aW9Tkkqnc5BLcT3OSO5nynB3n0I4tXSB54EVV4n6SD8uDqC" +
                "ImqOyCC+TDysG16WkU5qehz5A43saCLZaT5+GvCMCwdA2kcOFg9ICnd+h797IfPDFCgXCDZ7eC4zHrrp" +
                "nljlydjDMk8WsryAlI5OfHymeNqbsdQB7wVbuZfvbfJrExyWFl8n0Pt/KBJkN+YLlv++MNNf8NfMvDBy" +
                "o87M8QsouJ1/fPKJFcXw8Y/8OA0fn/LjLHx89im0Gj4+/yTP7ooBn6nh9epae/uevS1e0cR4m/8lvL2H" +
                "keG6uNZ5lDg3h9ofr33BED8Off8E3+JDNp2iFKq37eYTpVR1V+t81KdQM0/O0lBmbzgnxDqEy0NDWSR5" +
                "lcFXHeTdC5noduPdsT0tPqJH5QikD/aMdDW7M10cNI0PO2TtTnt1JrzHrAFx4re+pyJsfPnltrHqLLjZ" +
                "VaWvOPnqUPLqjGd4+rqNL9I0IK8Jr45kOg7hUo9Yz4eAWJN1Fxxto4gEIo7BFDrv7Pj36TrrvHC6S89i" +
                "7O2sTmJyd8P3KN/q7bez/io87i6/e4H1OKIVGv2wJ6SHXtVECxRS3vYtEbmCeX5HF+PDnnzt9sKuc1Qf" +
                "paX0MRyBPg2qbehbT+1Y3hocxuO/SL5DuejKfgp5Q38Usr9yz3N9J1GyNG1QyIAaAjP0KfRroiTMISpC" +
                "FcdwUTiFVHHr9OPkWt/QyfNUfc1rdG40T/jF1pXc79DdkxcU/ST6Ub9Lcvfi3tXtW83Uvc/RCfpBHJHU" +
                "3fEavdDqaxLSDexzUzIolu32NTq1JDSfs/vHWcbQU5TZlKPoqrMaVT8XEqpknXtHKDgVbOglU2GcRUCg" +
                "UE9V1SM9AhxK93ON+zA3A99z+F5XxkUoCy0kF/9n0657UK8uU2KFB5M5yiYI9NlJ51XM5tZG025i7KXC" +
                "txEELicaM5ZtMQ7zW/tSTsKuJZYO3PcutSwDIvT/5kbWm9g5kutBgDZ0razQhghJBPsns9A6DzUsR+dh" +
                "toC4kcY0e0jsNsluJ8od/VmC0o7a3SvK3sD5e1xRd/jn8+7I9Wo6u1xVIvFUUTKiWHGEqMez267f/03X" +
                "54c2pRDySK5XxtY1XYYPXUk/M9h3NpE3i+34ZsyN47B4d8X2syt+6a/4V/Rj+9K00IMP9Gppcr6R5JMT" +
                "HZIGxVxOR/pmeTPVpqPGm6OdSZHwnoJov6yXcdNObS4LgGVqhb8NQVpiqK6xWaqNIijjgs0gV0SWiXgC" +
                "fuuQE+cS8eNpBIW7L5ZuRCGjRYSuVDXRIXf1wXE70SD0dxD6cVIQ8vwRAO/OLgBcx75WwIDvmfLtK/fK" +
                "KagV/WjiSGrAPMyje9ahpYutAhaVYA81yQb8K4NxEpq8uMrRCI5lYuVK8oszoozFQx/KLBV4gCbJ1s0u" +
                "H4Xfe0DFb9A6MutNLe5yFMy+U1MVMUoEc9PPAOB1hLcTshHmHZNTTUYUSOrPU4Bf+pulSkga/mESqLNS" +
                "Xh6dLuFDpJF2bYsiSEijkOeSvMCh2V1UyejChvqul3bUfuWV9ZBMofrJUrsfPVB2e81sq+usduMQXqYB" +
                "ZVX6rK9iiWbp7NYawoH2yMiLFnVWfMVeOhK9NyBkCFfXPNcF8gqHvsFTijOTDEMxSg7WJE7uKma9BY9y" +
                "qQEkb/87sWYFRtSpveaZ6DV6ASP3PjMRHsuhHXniXA3PXUtU3Y0q4tujKiDkSO7AOLbcC14AEMO+Y7u4" +
                "Bp3RGu15HyWsC31iHYTmyHHbERJ9Fngc56Q9MwSYxC31hyhtYre8NltvrM7cJAM1uzbHQoE2dPXXubhf" +
                "fNFswMfb1c297ePV7ZT9eTbMF8uoT0wldixO9a2nYt4mOBBjmhVIYL3PTjPOTPshsN3MQU7ZO1HlfIzy" +
                "G0jK0YCZ2BLXimdykC6hSnxX2f9+BSqO8F2FOmUc2Ie6Yx/HceR3H8igCucG5YQnLpnUXoycp3lF8oat" +
                "GxhnaVq+cp7Y+Uu4XDDEU5acLtR7Bx0Uw8/gz/c6AK/UakJyDe+/pOCDhM/YIs5q25kM1JDZ/Kb3u0L8" +
                "hOkTF3ExMKL4aAYM1nPMdC33WNsceYhswqAhztERH+zcyfvBv88fP9UTUujAi1NR6kIBO3EW4WVmoZij" +
                "RY5YFwYTwez4AfcrekIUyfyvH1EHjnkiGQy3NR5zkvTJUPDT9wOehKnddkf+SWiGd/CBCCiNuW6slf9g" +
                "536XRMEGdxsQyZcSqt4OsfiCd58uib6RsBvnfXDY0SGvXMxI3eRHVWLWVSGzCd0LLmku4AKbql2idKSF" +
                "96rwS5gkRgauuaSjxx4ZBdME40mXVbI95VWKbupsqzAV3TMYn6H4ykZIfn5AI+IpevT4549D9JzZlnDN" +
                "7dN352cf0EvvPYitdvfghzDY4BymyCmc/S+T3C9uCyTCAH72fhz3M3lPuP9WlCqjfwe4d8kUABqL/gsh" +
                "EhnNkk0AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
