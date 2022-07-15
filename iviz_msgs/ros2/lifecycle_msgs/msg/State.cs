/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.LifecycleMsgs
{
    [DataContract]
    public sealed class State : IDeserializable<State>, IMessageRos2
    {
        // Primary state definitions as depicted in:
        // http://design.ros2.org/articles/node_lifecycle.html
        // These are the primary states. State changes can only be requested when the
        // node is in one of these states.
        // Indicates state has not yet been set.
        public const byte PRIMARY_STATE_UNKNOWN = 0;
        // This is the life cycle state the node is in immediately after being
        // instantiated.
        public const byte PRIMARY_STATE_UNCONFIGURED = 1;
        // This state represents a node that is not currently performing any processing.
        public const byte PRIMARY_STATE_INACTIVE = 2;
        // This is the main state of the node’s life cycle. While in this state, the node
        // performs any processing, responds to service requests, reads and processes
        // data, produces output, etc.
        public const byte PRIMARY_STATE_ACTIVE = 3;
        // The finalized state is the state in which the node ends immediately before
        // being destroyed.
        public const byte PRIMARY_STATE_FINALIZED = 4;
        // Temporary intermediate states. When a transition is requested, the node
        // changes its state into one of these states.
        // In this transition state the node’s onConfigure callback will be called to
        // allow the node to load its configuration and conduct any required setup.
        public const byte TRANSITION_STATE_CONFIGURING = 10;
        // In this transition state the node’s callback onCleanup will be called to clear
        // all state and return the node to a functionally equivalent state as when
        // first created.
        public const byte TRANSITION_STATE_CLEANINGUP = 11;
        // In this transition state the callback onShutdown will be executed to do any
        // cleanup necessary before destruction.
        public const byte TRANSITION_STATE_SHUTTINGDOWN = 12;
        // In this transition state the callback onActivate will be executed to do any
        // final preparations to start executing.
        public const byte TRANSITION_STATE_ACTIVATING = 13;
        // In this transition state the callback onDeactivate will be executed to do any
        // cleanup to start executing, and reverse the onActivate changes.
        public const byte TRANSITION_STATE_DEACTIVATING = 14;
        // This transition state is where any error may be cleaned up.
        public const byte TRANSITION_STATE_ERRORPROCESSING = 15;
        // The state id value from the above definitions.
        [DataMember (Name = "id")] public byte Id;
        // A text label of the state.
        [DataMember (Name = "label")] public string Label;
    
        /// Constructor for empty message.
        public State()
        {
            Label = "";
        }
        
        /// Explicit constructor.
        public State(byte Id, string Label)
        {
            this.Id = Id;
            this.Label = Label;
        }
        
        /// Constructor with buffer.
        public State(ref ReadBuffer2 b)
        {
            b.Deserialize(out Id);
            b.DeserializeString(out Label);
        }
        
        public State RosDeserialize(ref ReadBuffer2 b) => new State(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Id);
            b.Serialize(Label);
        }
        
        public void RosValidate()
        {
            if (Label is null) BuiltIns.ThrowNullReference();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Id);
            WriteBuffer2.Advance(ref c, Label);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "lifecycle_msgs/State";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
