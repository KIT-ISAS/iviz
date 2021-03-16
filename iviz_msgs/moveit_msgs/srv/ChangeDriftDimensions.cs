using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/ChangeDriftDimensions")]
    public sealed class ChangeDriftDimensions : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ChangeDriftDimensionsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ChangeDriftDimensionsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ChangeDriftDimensions()
        {
            Request = new ChangeDriftDimensionsRequest();
            Response = new ChangeDriftDimensionsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/ChangeDriftDimensions";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
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
        [DataMember (Name = "drift_x_translation")] public bool DriftXTranslation { get; set; }
        [DataMember (Name = "drift_y_translation")] public bool DriftYTranslation { get; set; }
        [DataMember (Name = "drift_z_translation")] public bool DriftZTranslation { get; set; }
        [DataMember (Name = "drift_x_rotation")] public bool DriftXRotation { get; set; }
        [DataMember (Name = "drift_y_rotation")] public bool DriftYRotation { get; set; }
        [DataMember (Name = "drift_z_rotation")] public bool DriftZRotation { get; set; }
        // Not implemented as of Jan 2020 (for now assumed to be the identity matrix). In the future it will allow us to transform
        // from the jog control frame to a unique drift frame, so the robot can drift along off-principal axes
        [DataMember (Name = "transform_jog_frame_to_drift_frame")] public GeometryMsgs.Transform TransformJogFrameToDriftFrame { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ChangeDriftDimensionsRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        public ChangeDriftDimensionsRequest(ref Buffer b)
        {
            DriftXTranslation = b.Deserialize<bool>();
            DriftYTranslation = b.Deserialize<bool>();
            DriftZTranslation = b.Deserialize<bool>();
            DriftXRotation = b.Deserialize<bool>();
            DriftYRotation = b.Deserialize<bool>();
            DriftZRotation = b.Deserialize<bool>();
            TransformJogFrameToDriftFrame = new GeometryMsgs.Transform(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ChangeDriftDimensionsRequest(ref b);
        }
        
        ChangeDriftDimensionsRequest IDeserializable<ChangeDriftDimensionsRequest>.RosDeserialize(ref Buffer b)
        {
            return new ChangeDriftDimensionsRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(DriftXTranslation);
            b.Serialize(DriftYTranslation);
            b.Serialize(DriftZTranslation);
            b.Serialize(DriftXRotation);
            b.Serialize(DriftYRotation);
            b.Serialize(DriftZRotation);
            TransformJogFrameToDriftFrame.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 62;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ChangeDriftDimensionsResponse : IResponse, IDeserializable<ChangeDriftDimensionsResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ChangeDriftDimensionsResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public ChangeDriftDimensionsResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ChangeDriftDimensionsResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ChangeDriftDimensionsResponse(ref b);
        }
        
        ChangeDriftDimensionsResponse IDeserializable<ChangeDriftDimensionsResponse>.RosDeserialize(ref Buffer b)
        {
            return new ChangeDriftDimensionsResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
        }
        
        public void Dispose()
        {
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
