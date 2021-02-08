using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitTest
{
    [DataContract (Name = "moveit_test/ExecuteTrajectory")]
    public sealed class ExecuteTrajectory : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ExecuteTrajectoryRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ExecuteTrajectoryResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ExecuteTrajectory()
        {
            Request = ExecuteTrajectoryRequest.Singleton;
            Response = new ExecuteTrajectoryResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ExecuteTrajectory(ExecuteTrajectoryRequest request)
        {
            Request = request;
            Response = new ExecuteTrajectoryResponse();
        }
        
        IService IService.Create() => new ExecuteTrajectory();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ExecuteTrajectoryRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ExecuteTrajectoryResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_test/ExecuteTrajectory";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "358e233cde0c8a8bcfea4ce193f8fc15";
    }

    [DataContract]
    public sealed class ExecuteTrajectoryRequest : IRequest<ExecuteTrajectory, ExecuteTrajectoryResponse>, IDeserializable<ExecuteTrajectoryRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        ExecuteTrajectoryRequest IDeserializable<ExecuteTrajectoryRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly ExecuteTrajectoryRequest Singleton = new ExecuteTrajectoryRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class ExecuteTrajectoryResponse : IResponse, IDeserializable<ExecuteTrajectoryResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryResponse(ref b);
        }
        
        ExecuteTrajectoryResponse IDeserializable<ExecuteTrajectoryResponse>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryResponse(ref b);
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
