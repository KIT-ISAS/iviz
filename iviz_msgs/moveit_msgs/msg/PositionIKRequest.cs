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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PositionIKRequest";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9936dc239c90af180ec94a51596c96f2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08bXMTR5rf9Su64IOtjRAE2K09p/hgsNk4GzCLnSwJRalampY08Wha6Z6xrFzdf7/n" +
                "pZ/unpEcSN3ZtVd7QGFrpt+e99fWQ3Ws3llfNqWt1dnflTO/tsY3amW81wszGDxUl0ujar0yys5VA78v" +
                "nG3XarMsZ0u1KatKTY1qvSlUY9XMrtZtY2AlmGjGi7F64MrFsplot3owUtapB/Cbf6AeKW9wmPJrMyvn" +
                "5UzTCeYwYtVWTbmuzCPayMPyld3Acmc1vF2Fcc6u6DAX709e75xCt43FgTNdVVtVmMa4VVmbcOaqrK8U" +
                "rAcDva2u6RSw8ki1dQVQq/JqgkMmBHPpla68lWOaYuAbV9YLxgKNQRwdq/d2apuLRgPwM1v70jc4ClC2" +
                "LOvmMQBbqHXAsycw8fSwM2OModJ1PgiWxXEAA4218J9Tv1hYDs5Y0zOHm47VcVHQHF0pTycoM1Stnb0u" +
                "C9gephM8sK4giqHa4ixH5+0eMd/yMeLEK9vZWBF7wJq87ZsfLi4R+kbD+fiRQBDODXsKnaZbQQFRwTFF" +
                "EgONifEqQCRiUeYvddOdBuQFAgELNEs10zUuT4hrAZetR5gimwjWZ9Y549e2LiIhByt7bcpmsvIL/zgj" +
                "JcE5IVCYzN7QcZDEjdM7Z1q1eNyp2X6D8BVmroGZR/AWcbQ7FZ6a1brZDl5lD7MBuOfrEiDRdYC4JZrS" +
                "lvralgUOrwBJSLOxetnfE/7NAT1mMLW24hmTNAGXPzx/d3l2/vb4++GOoJOgINKCrBulnQkUQsyRkJ/N" +
                "VW2bJB+jNJXWEtkEljSA9oJFV+My07JmFoVlOrqFJqIwCOnGuI2ut+paV6Ce6BxxRzpiLrR+hLyE4NNw" +
                "REK5qK1LwpuPFhUHRF6tWUo7GBgB+KbuMV1gU0+PiTfD0XpCm8RVM2xjlpf+wVQ5p40n4RSTazNrLMrE" +
                "UnuC3FRmZXCtCPd4sDB2ZRq3ZbYFNW4uMiBkLQTwDepUVqhqrR2ADTrxNvr7HHz/r8EBgXIfP3UpvQeC" +
                "wwD2kJDQg2UPBATfIdE40IhwvgKyoJzVoPKMMnAUM58TUYZi2nC3YM543gpNnah/3BiUx5J2z6xgBOtB" +
                "Zeb8KHCgdQWwVo4HD/Y4IA10Y8ns5gk3/DtPAc0CuwebQNi6lTMAgfv4DFlE35SrdoWsaze4Ybli9Y1G" +
                "SleztiJSDYrWMc1wgG2bfG7drqYMAkzSTYPKzatDYG5WxQ5gB9qjNfTfqMroa4LkSVTNQXsF+UgiHswO" +
                "sS5MdyiDyZYMByBlz57GHQeDwYv/5T+DNxd/O1L7rYQYweAziQX0HTOsp4CrZDvZPI5UOTZjBi1aXsBe" +
                "CVgLqgO5hXh34E3trePNv8OXbKJoXDJR32WGcqW3wMuAY3Gp1Mn5657yRK+gszSpChiXbUHTJ4WdT3qb" +
                "HTeNni1hlWhTwPj9AiwFRNfyjvysVdAIuf8wHMj8VzL9nGYDk8rsZK0mYWWyiZUGJQ72G91G4CuQXnJV" +
                "2M7OTE26lRkEDmzc2oHCK5DXtCrK+Zz9BfQCYMl4QvZ3aL7QFMlqfQNuZLlaW9do0PTIrKAYikq8iwjq" +
                "1BYlqJxDOQ8MRNWIfg0wu9s3mDWfDqx1dATOiTk6ylyQqZmjKmrXBcNaNnz4JmO5Idv30k8QuLvi/v0M" +
                "mGFKRxEg9lvaCvwTOLZGDBTGz1w5NazDCDQCPLhFoIUg8iDZcUBywA8LwBhsQ7LQPMkAAoPVPXTmGr0i" +
                "VIXg6pae/P4hngZ0CTj96GgewQLqTx0xEzUrq+gCF1gNR2noNcQds7LZ7g597GnwYz8k8UxTwEQAjzD0" +
                "iI/1ukIZC5o5LPB2hbPfDscE2GmCBWa0dQlYQG4rwNizhIIjifqAfBVGxNJoUvtBiNkUkL6GfdlUp/0I" +
                "a2j5gImcmaHFKCBkyB2UMAZWL9ncCRlJebAYyVQVnF2yQrjnONCnqwDRAvrA2aJ8tHN660e0A8oQkRFU" +
                "etPFMB1mrAKsC6urIMwrfWV4UhgPsCOH2TUHP2P1T7TiZJu3to3BEkFRWzTbTB/tPVBWo0YIomRWI5xC" +
                "EQSbpYycdG521AM3IvYYGqZtBrtf2rYqAuYET778zVD0C4jkdTKpITteA803sAuAGXkgHjNDTvCogltB" +
                "kcysgcWYgmOwfN8yczCPDJLfRPwzr6xu/vKcvQAShOyRMHz2iDFwZwqlKVib8JkBDNAodaFdAehsNGkO" +
                "UrbgPBn3qDJwwuins17ZrhHqhMwF6G5HYX+WklgBSmeER+DXznxmeU1cWKKLg6498Dn6pcBf6Cjj6ohg" +
                "TIzUMwgCTo6Iwc0MPMhrktV65owmB+fsRA1adkZgwuDh5cY+QgO0QOMkm0f9YG7ALnlPhgl11J8YuDGs" +
                "jdoWdgHuZn92Ah9B3cAmcASztiAEh3Dyd9tmGUzqtXalnlZk+TDxAase4KSDYbZyTUvXurayPK+Y9viS" +
                "Zeu4LsL0KNpC3y4AgTAw5hxCkD8DNQjMW5VTp912QKqKthw8fO1IkSD5iCKoNrviKUEbUWNSFvdh3nad" +
                "IAD2vUFyASBaLEjKcQRVQ5IZFV5hFs5wCDKHX8D1HXFyAl1scR4AunbWtI4sW9qP1epZExDSrpCbKewX" +
                "a4F867ceXN4so0GKEuQCPG2PzifPWZgmWSNYVlc27H4FJpJMppotwWcdq9eomG+ANBVoLBCMStfaienS" +
                "pO9+gAiDLOwzdCcPb0B1wj+9QYbg4AZDaHoZMjI5o+enY0TCD1fCKjwX7UvnPazKI2Q1YFzw/5E9pnp2" +
                "hQB3zvD/RvV+jerGgV5cfrFRleH/l4zqbTaVwyHORHRD7kthYRgV2Xln0AYIigPwZ+/dPwlN8JLxdVdK" +
                "75ZTCyadqLwgDlGtTE2zMZgW29gdi0n0Q4UHwqRnwMuDHynL8IznhzzCP1qY4GpUAM6ySr0fIMNh9oCo" +
                "FadDeudXURETR60MBoHAU3EmBZXIMwADpYUcxWwjjNUKC/iAIJC0GIgaWhkSf1THW1GGVUipWJxyiMIW" +
                "0o40Ck0F+Srk3YCuduWiLPpqlBR/AG6kmvlTzrzRmXkzICEsItgeUlYVBXSDAJFwS7A2NfFcZPwba0fo" +
                "UYUl+uklFCKR1bIGk6QLoHpwI9VN/C26luq3eyF14rF91Aa17Mpozjs0x0+/JgZFJH8WIPltc0+ySkoj" +
                "gCUG1qeotQvP1Nkrg0ASi3lMxmBCQlNtYUGOLxoNUHYiq2FI+hzG3Q90rP72UA1IweRJwI1AqDAz2FCo" +
                "3aBN+kIQabH0kXMQ95E8vCXxFcxz72nMoMfUEdkqiI3KG8mToNCSycQsm3jMeY2D8Bgzk5SIAq8TK4ZL" +
                "vTZeao8mFSd3zsZ6Ic/rSWJ98BDrMzOWIyQXwkpL1jZkBMegsLgINqLqiGTzHu5bT3J77Kilaqwggjco" +
                "Bn1E8aKxosOZJc78xypd3E8ylZgPjUlvOINtifFS/U49igfLisIQ8hVb9srAKeCThgnJQaDFJpy/5VLJ" +
                "DDxXLI9KPQI2Bx97xlmiTiAK1KCwgOmB1oeBJMuzwjUiKFgFYaMBSi2vVUDkZSkw1Y4qoloStMORpMNo" +
                "j9rMUH27Le3mTMXxLNXRjGxMdV6qVlHSGAzEL7TJNksJXsaHWHeH003CFkKUjaEiSXA2e8QYYR3sqrab" +
                "OmlTGn8fMrkri8fB4xtxMoHqjJJNlvCNZKbvIDKowPEBzIDAQ+IeWguo9wb2PmuGsSZZyDyh83ZtmCnQ" +
                "Kk81lyUJO1F6+OcE44hFTfkchoVBuMQVcBlZOaXpg7bFOGeP4y4yy9NKFzQDSko/cU7ueFZNEARc2Arg" +
                "l21mmJtdlZgw8QPSOHxOGvVOXmFaKg3brWHl7yeMeCw+Gb/sropPYOyKX+xdB9+lJV6icFD/A4S7mMA3" +
                "aPyDNssKm9NQwqFhEo6yE2Gk3QAe6tSPgVsM87O9w6kICO10C5D4Lp3uuCh8zkYB6ziqprQLFizImcsG" +
                "AY9el7b14PaZG/AU8Phlw9qZFc54MN2Cy358cvLiyYDSGygNnZ1im42pr0tna6xAK4yhHYZShwbC8C2o" +
                "JhIFqjc1IMy+xxNlMeSd3p++Of/x9MXXBNN6jXoKfdY6wkXpjaBY6dCx0eX3YRUfmycJnECFBOS7d6dv" +
                "T148DUo47bl/O9plBFpxEzg/kJqc/UMqzwa6ScRKjR8wAmu6HI1iNgS0mbcV4gpQKxojadPC+BK7EuiI" +
                "hJtneMDztXHRpYc14SM6oDLQyuu7UoqfVyqDh3/4jzp/+d3pq0vMkP7xyeEPIufV7xdXSWlShoRbgIIi" +
                "AzWGOSmMVr3hHEpqgwA0L7heF7MEXDsCPsHyXMepuDKxIJTvcERPeH7KMzlhqAVorFoVU1H2sIosWEzz" +
                "owQDS2my7y7O3z7GVoWQO/vp+M33ihcYq+PIwqBmowBkISYqasFKp+MJ9hWDMlan5DWU9R6ikxxR7sba" +
                "K/BXrsyRevCfB4jhg6ODV+jZnLw8GKkDZ20DT5ZNsz56/BjCD10BtpuD/wqNDdx2VltO3NVBMzL1gneD" +
                "xMmwgJ5j2RzApHJGwfKVMSFtPq9AVKdlVUq+x+zjV6yiMhIlZD55ybxBiyBUKPdhZ055IXO1gCdUcZwA" +
                "paQ8JkQDsFRHpGWOVEQAPUMUwLM+Co7+/B9/fc4j0PRyhgDG7Z74IOx08Y/vFZDNGyyeRjp1Nr74tfpW" +
                "RvDatJU62Cz8s7/wEyxVH6k/P3/2lD7CaIcDSnRzwwgw+xvrit5j9FAQENlAqu78dmWLtsL3lBVo7PpA" +
                "GBpY+67S8rd5C3CiExbTqb2BGHCNnDZSsy241uS0zTAnGhKLEuU4E+vCwFaSUAQPZyouACyGCh9NOkki" +
                "O85PRvB3PKDizl/Vy/MPYMb494t3356+PwXTwh9f/fT92duT0/egysOD87enL56LtIt+IiuDZwqj2EsT" +
                "lVCCofXSDJKGprpcGiFzMCsVOk7jhGzYEed4qbcQsz2MBDbViK4b0VQHac4BG7dBYE18C4DTUTl6+DBS" +
                "P3Ha/uf8zNpxcrUy9aKJeeW+DsKwKcIHSB8n3E4+gEeSPv0UcY2ffkYrnh2J8R9ORckuJDuqTfgZCgAe" +
                "vR9WKqSKGW6ni7LFI4Qwhzlo3KHr5P3xydkPF+ghZXsKkWlNJDAXIhkrzDqUfqCcobiHVHQJW/2sNDgc" +
                "Y5WShZ11J9+env3t20t1iGuHD8MEE/eMZBhPMC074ZXIgjpEWRjyfqjnZB+GLuzDH7J9btsFk4iCOyaf" +
                "Tj2Tu3u+sjUnA+QVzE9+fl8mscBTOgqBucWuKdeJhwinOB+DTeT3dj0K5ayvAlIHPUkM+Iss1QMe/dEk" +
                "qTuDE2Jw4N2ouN0ggIJPt1NvpN7UXu6LqIXuAb8vYyd5ltwcqwEnaWPVP8u+Z+PuC8CyjpnLTkoq787R" +
                "TOMuuJ9Jwd69CcLQUgxPdlQMJ1FBSLs6GEVdL8CD+CbTsKF5mVo6kdNTyx/AiBVNcHb8x08D3OMyLEDl" +
                "o7DWICiPkLiTGRJ7XWGTGA2g0+zBOfV48KR7QpWAsQdlAtaBT4fi1omPz/ic5mZCecB7OS3F5XuL/FwE" +
                "N6PsOgHH/zFJoG/UV5j++0rNfoP/CvVCUUSt1dELYHAz//jkE2YU48ev8eMsfnyKH4v48dmnWGr4+PwT" +
                "PbsrBHwmh9fLa+2te/amCKOR8N4Z4T5zbtEw1AeQxgaNkkr8pqSwLwrix5HUT+AtfNCzmalCtO0/IZVs" +
                "dzT3R32KOfNsLzZl5gb7hDAPEfzQmBbJrjJI1oHuXlBHt85vFtRSU+1BOQbQB3tauvxuTxc2mqaHHbB2" +
                "u706Hd4TzAFN6ILQ/SRh0+WX29qqdVSzK8tXnCQ7lF2dEYTn120kSeMBPB+vjmhuhwiuR8rnA4EwJxsC" +
                "HC6jEAXSGaModO7syH26zjghTnfoebK9ndGZTe5O+LH0IfrtjL+Oj7vD755gPYxwhoY/7DHpsVY15QQF" +
                "pbelJEIhmOA7qRgxe/Q6zAW5Ls21oZLSx7jFI3iLfF3PzIRuDY7S9l9l7/QUAPgU/YZ+K2R/5J7nfCeR" +
                "vDQuUFCDmqaSS6zXJEqow8LUtiHjj6V+iDqlnZzzG9x5nrOvelWBV0d+wm/GWYrvvOILitKJPuxXSe6e" +
                "3Lu8fauYNsvg13dcukCOBOpuew0HtHxNgqqBfWySB4Vpu32FTk4JzedY/TsMTMg32TC9NEyqWruFaYJJ" +
                "sNm4cEcoKhWYMNjbQRKWmNASE95SDoBN6dLXuO/kaiA1hx95ZBo0cWZBvvi/GnfdA3t1kZIyPFoQCgR9" +
                "dtK5iulvLTTtOsZCFSsJ3kPKEbYYBw2/tC4VKBxKYnlvYC+oxTQgmP4vLmSdpcoRhQdxtVEoZcUyRHQi" +
                "sH5SxNJ5zGEFOA/1Asg9Qm9iF8Rukex2oMLWnwUor6jdPaPsNZx/RBV1m38+r45CraYzK2QlMk2VKEOM" +
                "lVqIBrc1JnXD7/+h6pOmTUqEPKLwShnnUGWI6crqmVG+9ZRuFpvJzQQnTuLg3RHbz474rT/i31GP7XPT" +
                "Yg0+wsupyXlbcSNuyRfnky/HLX1F6WdcdGR7M9zpFIn3FIj7aTy1m3ZyczouvOU82oZLYuWaiqVcKLLY" +
                "WRgZmzviceE34XCkXNL5cDdcCmJfGNo6iY+ZgWNVyk65yZ11cJqOx8DV3wLRj7KEkOCHFnh7fgmLc9vX" +
                "Ck6A90zx9lW4cgrQNszXsSU1njz2owvqRnAcx8uWTVw18wbkymDqhEZcXJdmk6WJGSvZF2ckGpOGPqRe" +
                "KsCBnlbb0Ls8jN97gIzvW2zSbh2py3EU+05OlchIFix0P8MCwiMYnSAaQbyTc8rOCC+S6/N8wW8ksmQK" +
                "UcE/dgJ1RtLl0dkSdAgV0jamqiKF2AoJllz4yoAOSyYVNuK2dK6o/c6V9ehMeVIZr6X1gNEtnNnYjXah" +
                "HUJoGo/MTK/7LJZxlgnX8pwF7tGpaWOFV+ypItG7AUFNuDzmOQ+gKxx8g6cmZUYehu/zNjtxFKuo9RZw" +
                "VBZRQsPtZiarrjbYQAADnxFfWyxB8BUiPPCENu3QE/Zl89yVRObdxCJSHmUCgY8UNkxtyz3jBQsksx/Q" +
                "TqqBe7TGg937KHFcrBNzIzS2HDcdIqHOAhynPmlBBi22lmab1rd0e4uuzbrWcM9N1lCzK3OYKOCCLn+d" +
                "S/jiC9+a3/uGhHDbR9jt9Jo0h20Xy8RP6ErsSNxonxYTmcCGGOVXmoRlama6TVK1x3OgXfZ2VAUdw/iG" +
                "Q9LWg4e5LOFY0kxhpStgJbyrLN+vgIwzSswy09y+vXP0gD5sx6HvPqBGFewbpB2eBGeSazG0H/sV2Q3b" +
                "0DCOqWl6FTRx0Jc1FvorgSzbnaAXBR0ZQ3rw53sVgDA1ixCF4f1LCmIkxGNLZ2bZ1tRQg8jGN73vCpEO" +
                "0yfB4m5sOA97wIB6bDNdUxxr/FBWxCJMZRpsHRFjF3bev/y78vFT3iFfHc6FXVGsQoedrwqJl5kJYmwt" +
                "CsAGM5gRZkcPhK/oiVZEy9ePsAK3a24MNw4eYyfpkxGdj+8HPIldu80O/TPTDNpBDBEcaYLjJpz5j3Iu" +
                "s8gKeohtAMhqy65PPoMkvsLYpwuiFBJ27bwYhx0eEuZCjzR0fti6pq5fLmP2jUvuCwTDxmyXMR3CgnFV" +
                "/BImspERa8Hp6KGHWsHYwXjSRRVNz3GVHzdXtjZ2RfcERjwUyWxE5+eDeqGejtRP8OPrkfqZyhKhuH36" +
                "9uL8/eTnF70HqdQeHnyIjQ1BYRKd4t7/Ns79rYaEEICfRY9DfEb3hPu3opgZ5Q5wL8ikBdgW/TchEhnN" +
                "kk0AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
