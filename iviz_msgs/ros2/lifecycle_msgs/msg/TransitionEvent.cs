/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.LifecycleMsgs
{
    [DataContract]
    public sealed class TransitionEvent : IDeserializableRos2<TransitionEvent>, IMessageRos2
    {
        // The time point at which this event occurred.
        [DataMember (Name = "timestamp")] public ulong Timestamp;
        // The id and label of this transition event.
        [DataMember (Name = "transition")] public Transition Transition;
        // The starting state from which this event transitioned.
        [DataMember (Name = "start_state")] public State StartState;
        // The end state of this transition event.
        [DataMember (Name = "goal_state")] public State GoalState;
    
        /// Constructor for empty message.
        public TransitionEvent()
        {
            Transition = new Transition();
            StartState = new State();
            GoalState = new State();
        }
        
        /// Explicit constructor.
        public TransitionEvent(ulong Timestamp, Transition Transition, State StartState, State GoalState)
        {
            this.Timestamp = Timestamp;
            this.Transition = Transition;
            this.StartState = StartState;
            this.GoalState = GoalState;
        }
        
        /// Constructor with buffer.
        public TransitionEvent(ref ReadBuffer2 b)
        {
            b.Deserialize(out Timestamp);
            Transition = new Transition(ref b);
            StartState = new State(ref b);
            GoalState = new State(ref b);
        }
        
        public TransitionEvent RosDeserialize(ref ReadBuffer2 b) => new TransitionEvent(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Timestamp);
            Transition.RosSerialize(ref b);
            StartState.RosSerialize(ref b);
            GoalState.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Transition is null) BuiltIns.ThrowNullReference();
            Transition.RosValidate();
            if (StartState is null) BuiltIns.ThrowNullReference();
            StartState.RosValidate();
            if (GoalState is null) BuiltIns.ThrowNullReference();
            GoalState.RosValidate();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Timestamp);
            Transition.AddRosMessageLength(ref c);
            StartState.AddRosMessageLength(ref c);
            GoalState.AddRosMessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "lifecycle_msgs/TransitionEvent";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
