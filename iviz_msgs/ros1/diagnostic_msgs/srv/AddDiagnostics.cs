using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class AddDiagnostics : IService
    {
        /// Request message.
        [DataMember] public AddDiagnosticsRequest Request;
        
        /// Response message.
        [DataMember] public AddDiagnosticsResponse Response;
        
        /// Empty constructor.
        public AddDiagnostics()
        {
            Request = new AddDiagnosticsRequest();
            Response = new AddDiagnosticsResponse();
        }
        
        /// Setter constructor.
        public AddDiagnostics(AddDiagnosticsRequest request)
        {
            Request = request;
            Response = new AddDiagnosticsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (AddDiagnosticsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (AddDiagnosticsResponse)value;
        }
        
        public const string ServiceType = "diagnostic_msgs/AddDiagnostics";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e6ac9bbde83d0d3186523c3687aecaee";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddDiagnosticsRequest : IRequest<AddDiagnostics, AddDiagnosticsResponse>, IDeserializable<AddDiagnosticsRequest>
    {
        // This service is used as part of the process for loading analyzers at runtime,
        // and should be used by a loader script or program, not as a standalone service.
        // Information about dynamic addition of analyzers can be found at
        // http://wiki.ros.org/diagnostics/Tutorials/Adding%20Analyzers%20at%20Runtime
        // The load_namespace parameter defines the namespace where parameters for the
        // initialization of analyzers in the diagnostic aggregator have been loaded. The
        // value should be a global name (i.e. /my/name/space), not a relative
        // (my/name/space) or private (~my/name/space) name. Analyzers will not be added
        // if a non-global name is used. The call will also fail if the namespace
        // contains parameters that follow a namespace structure that does not conform to
        // that expected by the analyzer definitions. See
        // http://wiki.ros.org/diagnostics/Tutorials/Configuring%20Diagnostic%20Aggregators
        // and http://wiki.ros.org/diagnostics/Tutorials/Using%20the%20GenericAnalyzer
        // for examples of the structure of yaml files which are expected to have been
        // loaded into the namespace.
        [DataMember (Name = "load_namespace")] public string LoadNamespace;
    
        public AddDiagnosticsRequest()
        {
            LoadNamespace = "";
        }
        
        public AddDiagnosticsRequest(string LoadNamespace)
        {
            this.LoadNamespace = LoadNamespace;
        }
        
        public AddDiagnosticsRequest(ref ReadBuffer b)
        {
            LoadNamespace = b.DeserializeString();
        }
        
        public AddDiagnosticsRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            LoadNamespace = b.DeserializeString();
        }
        
        public AddDiagnosticsRequest RosDeserialize(ref ReadBuffer b) => new AddDiagnosticsRequest(ref b);
        
        public AddDiagnosticsRequest RosDeserialize(ref ReadBuffer2 b) => new AddDiagnosticsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(LoadNamespace);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(LoadNamespace);
        }
        
        public void RosValidate()
        {
            if (LoadNamespace is null) BuiltIns.ThrowNullReference(nameof(LoadNamespace));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(LoadNamespace);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, LoadNamespace);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddDiagnosticsResponse : IResponse, IDeserializable<AddDiagnosticsResponse>
    {
        // True if diagnostic aggregator was updated with new diagnostics, False
        // otherwise. A false return value means that either there is a bond in the
        // aggregator which already used the requested namespace, or the initialization
        // of analyzers failed.
        [DataMember (Name = "success")] public bool Success;
        // Message with additional information about the success or failure
        [DataMember (Name = "message")] public string Message;
    
        public AddDiagnosticsResponse()
        {
            Message = "";
        }
        
        public AddDiagnosticsResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public AddDiagnosticsResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
        }
        
        public AddDiagnosticsResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
        }
        
        public AddDiagnosticsResponse RosDeserialize(ref ReadBuffer b) => new AddDiagnosticsResponse(ref b);
        
        public AddDiagnosticsResponse RosDeserialize(ref ReadBuffer2 b) => new AddDiagnosticsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Align4();
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference(nameof(Message));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(Message);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Success
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Message);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
