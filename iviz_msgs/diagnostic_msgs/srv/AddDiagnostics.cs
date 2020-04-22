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
        
            public int GetLength()
            {
                int size = 4;
                size += load_namespace.Length;
                return size;
            }
        
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
        
            public Response Call(IServiceCaller caller)
            {
                AddDiagnostics s = new AddDiagnostics(this);
                caller.Call(s);
                return s.response;
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
        
            public int GetLength()
            {
                int size = 5;
                size += message.Length;
                return size;
            }
        
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
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "diagnostic_msgs/AddDiagnostics";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "e6ac9bbde83d0d3186523c3687aecaee";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACpVUTY/TMBC9V+I/jLRCWqQ2QRy5rUAgDlygnNE0niQWjh1sp9nugd/OG6efLAe4pO7Y" +
            "fvPmzRvf0ba3iZLEvW2EsJySGOJEI8dMoaXcC40xNJIStSGSC2ys74g9u8OTxEScKU4+20HWqzvEDaU+" +
            "TM7QTha03YG43JNIqYl2BHBU0C7ysCYfsiZkShmX2QUvJ0IVAD95pB042+CJd2HKZA6eB9sQG2NLGDQv" +
            "dBr2mrkNE5hwBkKf8/i2rmf7w1YxpCrErjaWOx9Stk2qt1MO0bJL9QMQfffyzeuHExzWnPH5slS4AtwW" +
            "img138FC0sjQDWJhnVGfkdZ6SUW2y/7cS7w6tSiJI0CzHjWws0/8vBTrC86FK3HXRekYfKnnvaBQ8Yu0" +
            "plJiANyzm+SqBUydCzt2hQ7d20oqqodDrX/rQu/VsQcUxYHFXlHub08sDbN7zoD49ceeris6S0azda4g" +
            "anYDalom6kLMb67JHO1WmKNvuFWuohOBWrZOb90ICaAm+MzWp2s1cw8PtsG5MGuWs+wpx6nJE6QvJ0xA" +
            "Y5QXMNRTlAMAy5Y8jtLkxaua8dSDpZ/FZamiryL/Zad3yGO7KS6een8+owY7NzIdh+bfYb+lBRBE8f0o" +
            "XqJtTvIDTc0ljzyMDvUeR/giBQIHHhy1Vrfn3jY9MeJnCXK4eAtoi7vgRcRvmlGtAKpPwe0wrDabTRmT" +
            "CBuigX9374yJn0YDPxk0PffkZb46mtb0AaWq3AFJ42yTWoxaDcKnqMQfnT4Iww1LFwGEnumnmItpF6Ds" +
            "MkYq81X+pW4Xhc1heaa0uCg/J0lK6lzPWr2ve7eTqsyuZ1X9CiuvdiE4SlOjD6bK8Bm/3OEJ0CJPLxYG" +
            "wD571kqblouaUgHRr5PIYKM4qxer388qDeS0BQAA";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public AddDiagnostics()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public AddDiagnostics(Request request)
        {
            this.request = request;
        }
        
        public IResponse CreateResponse() => new Response();
        
        public IRequest GetRequest() => request;
        
        public void SetResponse(IResponse response)
        {
            this.response = (Response)response;
        }
    }

}
