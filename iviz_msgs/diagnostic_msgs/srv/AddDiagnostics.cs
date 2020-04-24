namespace Iviz.Msgs.diagnostic_msgs
{
    public class AddDiagnostics : IService
    {
        public sealed class Request : IRequest
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
            public string load_namespace;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                load_namespace = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out load_namespace, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(load_namespace, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += load_namespace.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            
            // True if diagnostic aggregator was updated with new diagnostics, False
            // otherwise. A false return value means that either there is a bond in the
            // aggregator which already used the requested namespace, or the initialization
            // of analyzers failed.
            public bool success;
            
            // Message with additional information about the success or failure
            public string message;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                message = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out success, ref ptr, end);
                BuiltIns.Deserialize(out message, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(success, ref ptr, end);
                BuiltIns.Serialize(message, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 5;
                size += message.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "diagnostic_msgs/AddDiagnostics";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "e6ac9bbde83d0d3186523c3687aecaee";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public AddDiagnostics()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public AddDiagnostics(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new AddDiagnostics();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
