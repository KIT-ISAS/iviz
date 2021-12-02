/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PositionConstraint : IDeserializable<PositionConstraint>, IMessage
    {
        // This message contains the definition of a position constraint.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The robot link this constraint refers to
        [DataMember (Name = "link_name")] public string LinkName;
        // The offset (in the link frame) for the target point on the link we are planning for
        [DataMember (Name = "target_point_offset")] public GeometryMsgs.Vector3 TargetPointOffset;
        // The volume this constraint refers to 
        [DataMember (Name = "constraint_region")] public BoundingVolume ConstraintRegion;
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight;
    
        /// Constructor for empty message.
        public PositionConstraint()
        {
            LinkName = string.Empty;
            ConstraintRegion = new BoundingVolume();
        }
        
        /// Explicit constructor.
        public PositionConstraint(in StdMsgs.Header Header, string LinkName, in GeometryMsgs.Vector3 TargetPointOffset, BoundingVolume ConstraintRegion, double Weight)
        {
            this.Header = Header;
            this.LinkName = LinkName;
            this.TargetPointOffset = TargetPointOffset;
            this.ConstraintRegion = ConstraintRegion;
            this.Weight = Weight;
        }
        
        /// Constructor with buffer.
        internal PositionConstraint(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            LinkName = b.DeserializeString();
            b.Deserialize(out TargetPointOffset);
            ConstraintRegion = new BoundingVolume(ref b);
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PositionConstraint(ref b);
        
        PositionConstraint IDeserializable<PositionConstraint>.RosDeserialize(ref Buffer b) => new PositionConstraint(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(LinkName);
            b.Serialize(ref TargetPointOffset);
            ConstraintRegion.RosSerialize(ref b);
            b.Serialize(Weight);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
            if (ConstraintRegion is null) throw new System.NullReferenceException(nameof(ConstraintRegion));
            ConstraintRegion.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(LinkName);
                size += ConstraintRegion.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PositionConstraint";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c83edf208d87d3aa3169f47775a58e6a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1X227bRhB951cs4AdLLaOmcVAULvyQxE4soElc2w1yQSCsyBG5CMVldpeyla/vmV0u" +
                "STl204faiQHxsnNm5syVe+KyVFasyVpZkMh07aSqrXAliZxWqlZO6VrolZCi0Tbc4ZR1BufcLElOSeZk" +
                "ROl/kmQPgCSMXmonKlV/ARLwBwlhaEUGCnSCJ6ou/KlFLdcUhfVqZcmJiaq9GR5lZXBgKlba+GdOmgJH" +
                "Gs2IenTuioQ0JJpK1jWDQyApSK/Jme1ibQv7yzvKnDYHHcTCQyyCymjARlftmu62XCTPdVvnwH8XTg6H" +
                "FoYKUMRIz2CMKkrnzZCstDN/F3WSU60dWcBX0qkNCbVutHGyzmCCFhq+mZGAnYkXlbZ4hpffyGgETyJi" +
                "FULYi7ppsqq0dL897YxIkqP/+V/y+uLVobAuD7SGNIDbF1CfS5PDLCdz6aT3uoQRZB5VtKEKQnLdUC78" +
                "W7dtyM488yAGfwXVZGRVbUVrcQheZnq9bmuVSQdGFHJ1LA9JJAqyUxqnsraSTJY2iA4f93nD6Piz9LUl" +
                "ZnV+fOgJpaxlwqFJ1ZkhaTlU82ORtOD54AkLJHuXV/oRbqlgxqNyRFE6NpauGwPiYYy0h9DxU3BuBmyQ" +
                "Q9CSWzHxzxa4tVMBJTCBGp2VPsXPtq7sMngjjZLLChmADAEDQN1nof3pCJnNPhS1rHWED4iDjv8CW/e4" +
                "7NOjEjFD/RTCtgUIxMHG6I3KcXS59SBZpQjZWqmlkWabsFRQmey9ZI5DXvuI4FdaqzOFAOTiSrkylrqP" +
                "xkLl95WNt1Z6TC1DHCo4AfPExr/jzFkZgieNzGjGSTL3YdU1koLrynH+9ZIQzJWBKEp8xq0CLUEbSoVy" +
                "IteoYVQyMNbyCyAJHLO0bBqAIdENypRLnMOiWWRCs2KWiquS6nCKOfIZ7WtAZcKoQuVBEorWvbAUnXOp" +
                "cKsn4Liqgs1BGQIGEKOdF5jOxHwltroVV+wQLkxXelosqbfLp4jTOuW66yB2CT3z7TbOCowJh6LHCIit" +
                "5rq/2vZX3+4p1Gu9IeWCXbvdGGYf8+TCJIidHGE+OA49mScLppnVFYjt3APRjVFrDLcNYuhL24ew5Tof" +
                "xoEOE5HERHIKoSOB28SWsqFgxwWDnkWkT59HqHG0YIZCBRRclQoNgOFHqnl0VZr7XH5jap1Bbgy48ECM" +
                "Okfu5HkYzDBxQEs5UiVQM1lznG1DmVqp0FIhwj+sf9mxF/2cyAJzJkUN3+Lia0DCjoD8L051qn/oEJ+L" +
                "vtxPotwZnyFPlvo6BT1czqnItmiEGGW40njHSYPi8ijBn5AEnr5SYlzDXWUGFgHGnKNbAA5jwfAL8TjF" +
                "f5QKT5bfxfO3749+7a4vzk5Pzk+OnnS3Lz78OX9zfHJ+dBAfvH1zcvQ0Us3TkvOXOfY2daf4eRIP5WjO" +
                "tUXo7O7RriZB+3AiynCzY/PHAqNjh4IkQuvXEG4igYSwJzJd11xjfLM/yOzDeSO3rOFlt7fBcW9q6u/e" +
                "p+ID8gz0fBzbzCT7hY7qwpXRokwbNOEGEw9m8jTHdOr9A+mzgdvF+6PHo7sPPdd89xFUj00K/HdW+R7K" +
                "YcfKAVXc8jENeA8JdqJpF12TwJIrc9WyCTxFmTOfQdGOgLs4f3Y8//sC9ox1xiB7TA5w2IICKyF1eJ31" +
                "E1sbHrycSZX2jvOZj0JeK2yCwwzawV2cnsxfnV6KCWN3N9PBJ4CAtxHjg0+l3xZ7zrtaEBOuhWnQB+le" +
                "T/Cu0xNuRnru0sKzKXIXwicxce7U+YIDwkzFV5C/0TRHNYlOlynjF0E/orHgNEMOeU5ZnkcZ53vbpIFZ" +
                "8XNHaizSG2T2KXXDeSTXqFK/OzwQwwfvp8V931n9qOvXlrA2gANutLs7TxqixQtseB8GCbMdEs/LYjMK" +
                "sz8eSP5q0dMNj4fxuYdyEKbErW7ni7W3v/teZZN33P3BqvIg5g/U3baZ7vC5azzffR1454Xwh8tX/xX4" +
                "AOOVd4M4VEdh4BHPza9SNjQWfI3UBT5X/xhNj42sWv8NjE9sv8Z0kcQXYY1lnT/tyH76nLCOyw4Ac8xF" +
                "LFbAaPjObmXVS3y/zHlrbskngEWhB6IqunELZdGtfTsYFb5JPx0EO+l6AeKCtf8AU1aPtsQRAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
