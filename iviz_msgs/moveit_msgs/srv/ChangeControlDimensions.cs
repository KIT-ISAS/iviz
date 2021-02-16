using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/ChangeControlDimensions")]
    public sealed class ChangeControlDimensions : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ChangeControlDimensionsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ChangeControlDimensionsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ChangeControlDimensions()
        {
            Request = new ChangeControlDimensionsRequest();
            Response = new ChangeControlDimensionsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ChangeControlDimensions(ChangeControlDimensionsRequest request)
        {
            Request = request;
            Response = new ChangeControlDimensionsResponse();
        }
        
        IService IService.Create() => new ChangeControlDimensions();
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/ChangeControlDimensions";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "35b43a24f32e4654e4afa7596399fc3c";
    }

    [DataContract]
    public sealed class ChangeControlDimensionsRequest : IRequest<ChangeControlDimensions, ChangeControlDimensionsResponse>, IDeserializable<ChangeControlDimensionsRequest>
    {
        // For use with moveit_jog_arm Cartesian planner
        //
        // Turn on/off jogging along these dimensions.
        // Give 'true' to enable jogging in the direction, 'false' to disable
        [DataMember (Name = "control_x_translation")] public bool ControlXTranslation { get; set; }
        [DataMember (Name = "control_y_translation")] public bool ControlYTranslation { get; set; }
        [DataMember (Name = "control_z_translation")] public bool ControlZTranslation { get; set; }
        [DataMember (Name = "control_x_rotation")] public bool ControlXRotation { get; set; }
        [DataMember (Name = "control_y_rotation")] public bool ControlYRotation { get; set; }
        [DataMember (Name = "control_z_rotation")] public bool ControlZRotation { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ChangeControlDimensionsRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public ChangeControlDimensionsRequest(bool ControlXTranslation, bool ControlYTranslation, bool ControlZTranslation, bool ControlXRotation, bool ControlYRotation, bool ControlZRotation)
        {
            this.ControlXTranslation = ControlXTranslation;
            this.ControlYTranslation = ControlYTranslation;
            this.ControlZTranslation = ControlZTranslation;
            this.ControlXRotation = ControlXRotation;
            this.ControlYRotation = ControlYRotation;
            this.ControlZRotation = ControlZRotation;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ChangeControlDimensionsRequest(ref Buffer b)
        {
            ControlXTranslation = b.Deserialize<bool>();
            ControlYTranslation = b.Deserialize<bool>();
            ControlZTranslation = b.Deserialize<bool>();
            ControlXRotation = b.Deserialize<bool>();
            ControlYRotation = b.Deserialize<bool>();
            ControlZRotation = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ChangeControlDimensionsRequest(ref b);
        }
        
        ChangeControlDimensionsRequest IDeserializable<ChangeControlDimensionsRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
    }

    [DataContract]
    public sealed class ChangeControlDimensionsResponse : IResponse, IDeserializable<ChangeControlDimensionsResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ChangeControlDimensionsResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public ChangeControlDimensionsResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ChangeControlDimensionsResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ChangeControlDimensionsResponse(ref b);
        }
        
        ChangeControlDimensionsResponse IDeserializable<ChangeControlDimensionsResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
