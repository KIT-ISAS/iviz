using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract]
    public sealed class Empty : IService
    {
        /// Request message.
        [DataMember] public EmptyRequest Request;
        
        /// Response message.
        [DataMember] public EmptyResponse Response;
        
        /// Empty constructor.
        public Empty()
        {
            Request = EmptyRequest.Singleton;
            Response = EmptyResponse.Singleton;
        }
        
        /// Setter constructor.
        public Empty(EmptyRequest request)
        {
            Request = request;
            Response = EmptyResponse.Singleton;
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (EmptyRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (EmptyResponse)value;
        }
        
        public const string ServiceType = "roscpp/Empty";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "d41d8cd98f00b204e9800998ecf8427e";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class EmptyRequest : IRequest<Empty, EmptyResponse>, IDeserializable<EmptyRequest>
    {
    
        public EmptyRequest()
        {
        }
        
        public EmptyRequest(ref ReadBuffer b)
        {
        }
        
        public EmptyRequest(ref ReadBuffer2 b)
        {
        }
        
        public EmptyRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public EmptyRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static EmptyRequest? singleton;
        public static EmptyRequest Singleton => singleton ??= new EmptyRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class EmptyResponse : IResponse, IDeserializable<EmptyResponse>
    {
    
        public EmptyResponse()
        {
        }
        
        public EmptyResponse(ref ReadBuffer b)
        {
        }
        
        public EmptyResponse(ref ReadBuffer2 b)
        {
        }
        
        public EmptyResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public EmptyResponse RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static EmptyResponse? singleton;
        public static EmptyResponse Singleton => singleton ??= new EmptyResponse();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
