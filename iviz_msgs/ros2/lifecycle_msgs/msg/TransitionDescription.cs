/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.LifecycleMsgs
{
    [DataContract]
    public sealed class TransitionDescription : IDeserializable<TransitionDescription>, IMessageRos2
    {
        // The transition id and label of this description.
        [DataMember (Name = "transition")] public Transition Transition;
        // The current state from which this transition transitions.
        [DataMember (Name = "start_state")] public State StartState;
        // The desired target state of this transition.
        [DataMember (Name = "goal_state")] public State GoalState;
    
        /// Constructor for empty message.
        public TransitionDescription()
        {
            Transition = new Transition();
            StartState = new State();
            GoalState = new State();
        }
        
        /// Explicit constructor.
        public TransitionDescription(Transition Transition, State StartState, State GoalState)
        {
            this.Transition = Transition;
            this.StartState = StartState;
            this.GoalState = GoalState;
        }
        
        /// Constructor with buffer.
        public TransitionDescription(ref ReadBuffer2 b)
        {
            Transition = new Transition(ref b);
            StartState = new State(ref b);
            GoalState = new State(ref b);
        }
        
        public TransitionDescription RosDeserialize(ref ReadBuffer2 b) => new TransitionDescription(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
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
    
        public void GetRosMessageLength(ref int c)
        {
            Transition.GetRosMessageLength(ref c);
            StartState.GetRosMessageLength(ref c);
            GoalState.GetRosMessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "lifecycle_msgs/TransitionDescription";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
