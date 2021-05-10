using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    public enum FeedbackType
    {
        Expired,
        ButtonClick,
        MenuEntryClick,
        PositionChanged,
        AngleChanged,
        ScaleChanged,
    } 
    
    [DataContract]
    public sealed class Feedback : RosMessageWrapper<Feedback>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Feedback";
        
        [DataMember, NotNull] public string VizId { get; set; } = "";
        [DataMember] public string Id { get; set; } = "";
        [DataMember] public FeedbackType FeedbackType { get; set; }
        [DataMember] public int EntryId { get; set; }
        [DataMember] public Vector3 Position { get; set; }
        [DataMember] public Quaternion Orientation { get; set; }
        [DataMember] public double Scale { get; set; }
    }
}