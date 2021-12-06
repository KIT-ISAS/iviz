using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class ChangeDriftDimensions : IService
    {
        /// Request message.
        [DataMember] public ChangeDriftDimensionsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ChangeDriftDimensionsResponse Response { get; set; }
        
        /// Empty constructor.
        public ChangeDriftDimensions()
        {
            Request = new ChangeDriftDimensionsRequest();
            Response = new ChangeDriftDimensionsResponse();
        }
        
        /// Setter constructor.
        public ChangeDriftDimensions(ChangeDriftDimensionsRequest request)
        {
            Request = request;
            Response = new ChangeDriftDimensionsResponse();
        }
        
        IService IService.Create() => new ChangeDriftDimensions();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ChangeDriftDimensionsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ChangeDriftDimensionsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/ChangeDriftDimensions";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "0d34c8d563fea2efff65829c37132a15";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ChangeDriftDimensionsRequest : IRequest<ChangeDriftDimensions, ChangeDriftDimensionsResponse>, IDeserializable<ChangeDriftDimensionsRequest>
    {
        // For use with moveit_jog_arm Cartesian planner
        //
        // Allow the robot to drift along these dimensions in a smooth but unregulated way.
        // Give 'true' to enable drift in the direction, 'false' to disable.
        // For example, may allow wrist rotation by drift_x_rotation == true.
        [DataMember (Name = "drift_x_translation")] public bool DriftXTranslation;
        [DataMember (Name = "drift_y_translation")] public bool DriftYTranslation;
        [DataMember (Name = "drift_z_translation")] public bool DriftZTranslation;
        [DataMember (Name = "drift_x_rotation")] public bool DriftXRotation;
        [DataMember (Name = "drift_y_rotation")] public bool DriftYRotation;
        [DataMember (Name = "drift_z_rotation")] public bool DriftZRotation;
        // Not implemented as of Jan 2020 (for now assumed to be the identity matrix). In the future it will allow us to transform
        // from the jog control frame to a unique drift frame, so the robot can drift along off-principal axes
        [DataMember (Name = "transform_jog_frame_to_drift_frame")] public GeometryMsgs.Transform TransformJogFrameToDriftFrame;
    
        /// Constructor for empty message.
        public ChangeDriftDimensionsRequest()
        {
        }
        
        /// Explicit constructor.
        public ChangeDriftDimensionsRequest(bool DriftXTranslation, bool DriftYTranslation, bool DriftZTranslation, bool DriftXRotation, bool DriftYRotation, bool DriftZRotation, in GeometryMsgs.Transform TransformJogFrameToDriftFrame)
        {
            this.DriftXTranslation = DriftXTranslation;
            this.DriftYTranslation = DriftYTranslation;
            this.DriftZTranslation = DriftZTranslation;
            this.DriftXRotation = DriftXRotation;
            this.DriftYRotation = DriftYRotation;
            this.DriftZRotation = DriftZRotation;
            this.TransformJogFrameToDriftFrame = TransformJogFrameToDriftFrame;
        }
        
        /// Constructor with buffer.
        internal ChangeDriftDimensionsRequest(ref ReadBuffer b)
        {
            DriftXTranslation = b.Deserialize<bool>();
            DriftYTranslation = b.Deserialize<bool>();
            DriftZTranslation = b.Deserialize<bool>();
            DriftXRotation = b.Deserialize<bool>();
            DriftYRotation = b.Deserialize<bool>();
            DriftZRotation = b.Deserialize<bool>();
            b.Deserialize(out TransformJogFrameToDriftFrame);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ChangeDriftDimensionsRequest(ref b);
        
        public ChangeDriftDimensionsRequest RosDeserialize(ref ReadBuffer b) => new ChangeDriftDimensionsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(DriftXTranslation);
            b.Serialize(DriftYTranslation);
            b.Serialize(DriftZTranslation);
            b.Serialize(DriftXRotation);
            b.Serialize(DriftYRotation);
            b.Serialize(DriftZRotation);
            b.Serialize(in TransformJogFrameToDriftFrame);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 62;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ChangeDriftDimensionsResponse : IResponse, IDeserializable<ChangeDriftDimensionsResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        /// Constructor for empty message.
        public ChangeDriftDimensionsResponse()
        {
        }
        
        /// Explicit constructor.
        public ChangeDriftDimensionsResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// Constructor with buffer.
        internal ChangeDriftDimensionsResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ChangeDriftDimensionsResponse(ref b);
        
        public ChangeDriftDimensionsResponse RosDeserialize(ref ReadBuffer b) => new ChangeDriftDimensionsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
