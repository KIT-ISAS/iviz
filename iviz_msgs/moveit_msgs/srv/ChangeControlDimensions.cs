using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class ChangeControlDimensions : IService
    {
        /// Request message.
        [DataMember] public ChangeControlDimensionsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ChangeControlDimensionsResponse Response { get; set; }
        
        /// Empty constructor.
        public ChangeControlDimensions()
        {
            Request = new ChangeControlDimensionsRequest();
            Response = new ChangeControlDimensionsResponse();
        }
        
        /// Setter constructor.
        public ChangeControlDimensions(ChangeControlDimensionsRequest request)
        {
            Request = request;
            Response = new ChangeControlDimensionsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ChangeControlDimensionsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ChangeControlDimensionsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/ChangeControlDimensions";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "35b43a24f32e4654e4afa7596399fc3c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ChangeControlDimensionsRequest : IRequest<ChangeControlDimensions, ChangeControlDimensionsResponse>, IDeserializable<ChangeControlDimensionsRequest>
    {
        // For use with moveit_jog_arm Cartesian planner
        //
        // Turn on/off jogging along these dimensions.
        // Give 'true' to enable jogging in the direction, 'false' to disable
        [DataMember (Name = "control_x_translation")] public bool ControlXTranslation;
        [DataMember (Name = "control_y_translation")] public bool ControlYTranslation;
        [DataMember (Name = "control_z_translation")] public bool ControlZTranslation;
        [DataMember (Name = "control_x_rotation")] public bool ControlXRotation;
        [DataMember (Name = "control_y_rotation")] public bool ControlYRotation;
        [DataMember (Name = "control_z_rotation")] public bool ControlZRotation;
    
        /// Constructor for empty message.
        public ChangeControlDimensionsRequest()
        {
        }
        
        /// Explicit constructor.
        public ChangeControlDimensionsRequest(bool ControlXTranslation, bool ControlYTranslation, bool ControlZTranslation, bool ControlXRotation, bool ControlYRotation, bool ControlZRotation)
        {
            this.ControlXTranslation = ControlXTranslation;
            this.ControlYTranslation = ControlYTranslation;
            this.ControlZTranslation = ControlZTranslation;
            this.ControlXRotation = ControlXRotation;
            this.ControlYRotation = ControlYRotation;
            this.ControlZRotation = ControlZRotation;
        }
        
        /// Constructor with buffer.
        public ChangeControlDimensionsRequest(ref ReadBuffer b)
        {
            ControlXTranslation = b.Deserialize<bool>();
            ControlYTranslation = b.Deserialize<bool>();
            ControlZTranslation = b.Deserialize<bool>();
            ControlXRotation = b.Deserialize<bool>();
            ControlYRotation = b.Deserialize<bool>();
            ControlZRotation = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ChangeControlDimensionsRequest(ref b);
        
        public ChangeControlDimensionsRequest RosDeserialize(ref ReadBuffer b) => new ChangeControlDimensionsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ControlXTranslation);
            b.Serialize(ControlYTranslation);
            b.Serialize(ControlZTranslation);
            b.Serialize(ControlXRotation);
            b.Serialize(ControlYRotation);
            b.Serialize(ControlZRotation);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 6;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ChangeControlDimensionsResponse : IResponse, IDeserializable<ChangeControlDimensionsResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        /// Constructor for empty message.
        public ChangeControlDimensionsResponse()
        {
        }
        
        /// Explicit constructor.
        public ChangeControlDimensionsResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// Constructor with buffer.
        public ChangeControlDimensionsResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ChangeControlDimensionsResponse(ref b);
        
        public ChangeControlDimensionsResponse RosDeserialize(ref ReadBuffer b) => new ChangeControlDimensionsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
