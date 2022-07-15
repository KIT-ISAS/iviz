/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.LifecycleMsgs
{
    [DataContract]
    public sealed class Transition : IDeserializable<Transition>, IMessageRos2
    {
        // Default values for transitions as described in:
        // http://design.ros2.org/articles/node_lifecycle.html
        // Reserved [0-9], publicly available transitions.
        // When a node is in one of these primary states, these transitions can be
        // invoked.
        // This transition will instantiate the node, but will not run any code beyond
        // the constructor.
        public const byte TRANSITION_CREATE = 0;
        // The node’s onConfigure callback will be called to allow the node to load its
        // configuration and conduct any required setup.
        public const byte TRANSITION_CONFIGURE = 1;
        // The node’s callback onCleanup will be called in this transition to allow the
        // node to load its configuration and conduct any required setup.
        public const byte TRANSITION_CLEANUP = 2;
        // The node's callback onActivate will be executed to do any final preparations
        // to start executing.
        public const byte TRANSITION_ACTIVATE = 3;
        // The node's callback onDeactivate will be executed to do any cleanup to start
        // executing, and reverse the onActivate changes.
        public const byte TRANSITION_DEACTIVATE = 4;
        // This signals shutdown during an unconfigured state, the node's callback
        // onShutdown will be executed to do any cleanup necessary before destruction.
        public const byte TRANSITION_UNCONFIGURED_SHUTDOWN = 5;
        // This signals shutdown during an inactive state, the node's callback onShutdown
        // will be executed to do any cleanup necessary before destruction.
        public const byte TRANSITION_INACTIVE_SHUTDOWN = 6;
        // This signals shutdown during an active state, the node's callback onShutdown
        // will be executed to do any cleanup necessary before destruction.
        public const byte TRANSITION_ACTIVE_SHUTDOWN = 7;
        // This transition will simply cause the deallocation of the node.
        public const byte TRANSITION_DESTROY = 8;
        // Reserved [10-69], private transitions
        // These transitions are not publicly available and cannot be invoked by a user.
        // The following transitions are implicitly invoked based on the callback
        // feedback of the intermediate transition states.
        public const byte TRANSITION_ON_CONFIGURE_SUCCESS = 10;
        public const byte TRANSITION_ON_CONFIGURE_FAILURE = 11;
        public const byte TRANSITION_ON_CONFIGURE_ERROR = 12;
        public const byte TRANSITION_ON_CLEANUP_SUCCESS = 20;
        public const byte TRANSITION_ON_CLEANUP_FAILURE = 21;
        public const byte TRANSITION_ON_CLEANUP_ERROR = 22;
        public const byte TRANSITION_ON_ACTIVATE_SUCCESS = 30;
        public const byte TRANSITION_ON_ACTIVATE_FAILURE = 31;
        public const byte TRANSITION_ON_ACTIVATE_ERROR = 32;
        public const byte TRANSITION_ON_DEACTIVATE_SUCCESS = 40;
        public const byte TRANSITION_ON_DEACTIVATE_FAILURE = 41;
        public const byte TRANSITION_ON_DEACTIVATE_ERROR = 42;
        public const byte TRANSITION_ON_SHUTDOWN_SUCCESS = 50;
        public const byte TRANSITION_ON_SHUTDOWN_FAILURE = 51;
        public const byte TRANSITION_ON_SHUTDOWN_ERROR = 52;
        public const byte TRANSITION_ON_ERROR_SUCCESS = 60;
        public const byte TRANSITION_ON_ERROR_FAILURE = 61;
        public const byte TRANSITION_ON_ERROR_ERROR = 62;
        // Reserved [90-99]. Transition callback success values.
        // These return values ought to be set as a return value for each callback.
        // Depending on which return value, the transition will be executed correctly or
        // fallback/error callbacks will be triggered.
        // The transition callback successfully performed its required functionality.
        public const byte TRANSITION_CALLBACK_SUCCESS = 97;
        // The transition callback failed to perform its required functionality.
        public const byte TRANSITION_CALLBACK_FAILURE = 98;
        // The transition callback encountered an error that requires special cleanup, if
        // possible.
        public const byte TRANSITION_CALLBACK_ERROR = 99;
        //#
        //# Fields
        //#
        // The transition id from above definitions.
        [DataMember (Name = "id")] public byte Id;
        // A text label of the transition.
        [DataMember (Name = "label")] public string Label;
    
        /// Constructor for empty message.
        public Transition()
        {
            Label = "";
        }
        
        /// Explicit constructor.
        public Transition(byte Id, string Label)
        {
            this.Id = Id;
            this.Label = Label;
        }
        
        /// Constructor with buffer.
        public Transition(ref ReadBuffer2 b)
        {
            b.Deserialize(out Id);
            b.DeserializeString(out Label);
        }
        
        public Transition RosDeserialize(ref ReadBuffer2 b) => new Transition(ref b);
    
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
        public const string MessageType = "lifecycle_msgs/Transition";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
