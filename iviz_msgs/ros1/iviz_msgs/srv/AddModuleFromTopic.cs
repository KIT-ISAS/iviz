using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class AddModuleFromTopic : IService
    {
        /// Request message.
        [DataMember] public AddModuleFromTopicRequest Request;
        
        /// Response message.
        [DataMember] public AddModuleFromTopicResponse Response;
        
        /// Empty constructor.
        public AddModuleFromTopic()
        {
            Request = new AddModuleFromTopicRequest();
            Response = new AddModuleFromTopicResponse();
        }
        
        /// Setter constructor.
        public AddModuleFromTopic(AddModuleFromTopicRequest request)
        {
            Request = request;
            Response = new AddModuleFromTopicResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (AddModuleFromTopicRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (AddModuleFromTopicResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/AddModuleFromTopic";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "68ab9eda5fc795e020e1e72fec9f4815";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddModuleFromTopicRequest : IRequest<AddModuleFromTopic, AddModuleFromTopicResponse>, IDeserializable<AddModuleFromTopicRequest>
    {
        // Adds a module
        /// <summary> Name of the topic </summary>
        [DataMember (Name = "topic")] public string Topic;
        /// <summary> Requested id to identify this module, or empty to autogenerate </summary>
        [DataMember (Name = "id")] public string Id;
    
        public AddModuleFromTopicRequest()
        {
            Topic = "";
            Id = "";
        }
        
        public AddModuleFromTopicRequest(string Topic, string Id)
        {
            this.Topic = Topic;
            this.Id = Id;
        }
        
        public AddModuleFromTopicRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Topic);
            b.DeserializeString(out Id);
        }
        
        public AddModuleFromTopicRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Topic);
            b.DeserializeString(out Id);
        }
        
        public AddModuleFromTopicRequest RosDeserialize(ref ReadBuffer b) => new AddModuleFromTopicRequest(ref b);
        
        public AddModuleFromTopicRequest RosDeserialize(ref ReadBuffer2 b) => new AddModuleFromTopicRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Topic);
            b.Serialize(Id);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Topic);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Topic is null) BuiltIns.ThrowNullReference();
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(Topic) + WriteBuffer.GetStringSize(Id);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.AddLength(c, Topic);
            c = WriteBuffer2.AddLength(c, Id);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddModuleFromTopicResponse : IResponse, IDeserializable<AddModuleFromTopicResponse>
    {
        /// <summary> Whether the retrieval succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
        /// <summary> An id identifying this module </summary>
        [DataMember (Name = "id")] public string Id;
    
        public AddModuleFromTopicResponse()
        {
            Message = "";
            Id = "";
        }
        
        public AddModuleFromTopicResponse(bool Success, string Message, string Id)
        {
            this.Success = Success;
            this.Message = Message;
            this.Id = Id;
        }
        
        public AddModuleFromTopicResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
            b.DeserializeString(out Id);
        }
        
        public AddModuleFromTopicResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
            b.DeserializeString(out Id);
        }
        
        public AddModuleFromTopicResponse RosDeserialize(ref ReadBuffer b) => new AddModuleFromTopicResponse(ref b);
        
        public AddModuleFromTopicResponse RosDeserialize(ref ReadBuffer2 b) => new AddModuleFromTopicResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Id);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 9 + WriteBuffer.GetStringSize(Message) + WriteBuffer.GetStringSize(Id);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c += 1;  // Success
            c = WriteBuffer2.AddLength(c, Message);
            c = WriteBuffer2.AddLength(c, Id);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
