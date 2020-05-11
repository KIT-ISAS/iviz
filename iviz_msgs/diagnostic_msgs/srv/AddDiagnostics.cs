using System.Runtime.Serialization;

namespace Iviz.Msgs.diagnostic_msgs
{
    public sealed class AddDiagnostics : IService
    {
        /// <summary> Request message. </summary>
        public AddDiagnosticsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public AddDiagnosticsResponse Response { get; set; }
        
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
        
        public IService Create() => new AddDiagnostics();
        
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
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "diagnostic_msgs/AddDiagnostics";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "e6ac9bbde83d0d3186523c3687aecaee";
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
        public string load_namespace { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AddDiagnosticsRequest()
        {
            load_namespace = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddDiagnosticsRequest(string load_namespace)
        {
            this.load_namespace = load_namespace ?? throw new System.ArgumentNullException(nameof(load_namespace));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddDiagnosticsRequest(Buffer b)
        {
            this.load_namespace = b.DeserializeString();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new AddDiagnosticsRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.load_namespace);
        }
        
        public void Validate()
        {
            if (load_namespace is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(load_namespace);
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
        public bool success { get; set; }
        
        // Message with additional information about the success or failure
        public string message { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AddDiagnosticsResponse()
        {
            message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddDiagnosticsResponse(bool success, string message)
        {
            this.success = success;
            this.message = message ?? throw new System.ArgumentNullException(nameof(message));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddDiagnosticsResponse(Buffer b)
        {
            this.success = b.Deserialize<bool>();
            this.message = b.DeserializeString();
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new AddDiagnosticsResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.success);
            b.Serialize(this.message);
        }
        
        public void Validate()
        {
            if (message is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(message);
                return size;
            }
        }
    }
}
