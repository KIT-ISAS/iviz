using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class UpdateFilename : IService
    {
        /// Request message.
        [DataMember] public UpdateFilenameRequest Request;
        
        /// Response message.
        [DataMember] public UpdateFilenameResponse Response;
        
        /// Empty constructor.
        public UpdateFilename()
        {
            Request = new UpdateFilenameRequest();
            Response = new UpdateFilenameResponse();
        }
        
        /// Setter constructor.
        public UpdateFilename(UpdateFilenameRequest request)
        {
            Request = request;
            Response = new UpdateFilenameResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (UpdateFilenameRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (UpdateFilenameResponse)value;
        }
        
        public const string ServiceType = "pcl_msgs/UpdateFilename";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "93a4bc4c60dc17e2a69e3fcaaa25d69d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateFilenameRequest : IRequest<UpdateFilename, UpdateFilenameResponse>, IDeserializable<UpdateFilenameRequest>
    {
        [DataMember (Name = "filename")] public string Filename;
    
        public UpdateFilenameRequest()
        {
            Filename = "";
        }
        
        public UpdateFilenameRequest(string Filename)
        {
            this.Filename = Filename;
        }
        
        public UpdateFilenameRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Filename);
        }
        
        public UpdateFilenameRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Filename);
        }
        
        public UpdateFilenameRequest RosDeserialize(ref ReadBuffer b) => new UpdateFilenameRequest(ref b);
        
        public UpdateFilenameRequest RosDeserialize(ref ReadBuffer2 b) => new UpdateFilenameRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Filename);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Filename);
        }
        
        public void RosValidate()
        {
            if (Filename is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Filename);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Filename);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateFilenameResponse : IResponse, IDeserializable<UpdateFilenameResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        public UpdateFilenameResponse()
        {
        }
        
        public UpdateFilenameResponse(bool Success)
        {
            this.Success = Success;
        }
        
        public UpdateFilenameResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
        }
        
        public UpdateFilenameResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
        }
        
        public UpdateFilenameResponse RosDeserialize(ref ReadBuffer b) => new UpdateFilenameResponse(ref b);
        
        public UpdateFilenameResponse RosDeserialize(ref ReadBuffer2 b) => new UpdateFilenameResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 1;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
