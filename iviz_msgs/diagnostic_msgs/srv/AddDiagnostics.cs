using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract (Name = "diagnostic_msgs/AddDiagnostics")]
    public sealed class AddDiagnostics : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public AddDiagnosticsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public AddDiagnosticsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public AddDiagnostics()
        {
            Request = new AddDiagnosticsRequest();
            Response = new AddDiagnosticsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public AddDiagnostics(AddDiagnosticsRequest request)
        {
            Request = request;
            Response = new AddDiagnosticsResponse();
        }
        
        IService IService.Create() => new AddDiagnostics();
        
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "diagnostic_msgs/AddDiagnostics";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "e6ac9bbde83d0d3186523c3687aecaee";
    }

    public sealed class AddDiagnosticsRequest : IRequest
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
        [DataMember (Name = "load_namespace")] public string LoadNamespace { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AddDiagnosticsRequest()
        {
            LoadNamespace = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddDiagnosticsRequest(string LoadNamespace)
        {
            this.LoadNamespace = LoadNamespace;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddDiagnosticsRequest(Buffer b)
        {
            LoadNamespace = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new AddDiagnosticsRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.LoadNamespace);
        }
        
        public void Validate()
        {
            if (LoadNamespace is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(LoadNamespace);
                return size;
            }
        }
    }

    public sealed class AddDiagnosticsResponse : IResponse
    {
        // True if diagnostic aggregator was updated with new diagnostics, False
        // otherwise. A false return value means that either there is a bond in the
        // aggregator which already used the requested namespace, or the initialization
        // of analyzers failed.
        [DataMember (Name = "success")] public bool Success { get; set; }
        // Message with additional information about the success or failure
        [DataMember (Name = "message")] public string Message { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AddDiagnosticsResponse()
        {
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddDiagnosticsResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddDiagnosticsResponse(Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new AddDiagnosticsResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.Success);
            b.Serialize(this.Message);
        }
        
        public void Validate()
        {
            if (Message is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
