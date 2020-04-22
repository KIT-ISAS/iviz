namespace Iviz.Msgs.nav_msgs
{
    public class GetMap : IService
    {
        public sealed class Request : IRequest
        {
            // Get the map as a nav_msgs/OccupancyGrid
        
            public int GetLength() => 0;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public Response Call(IServiceCaller caller)
            {
                GetMap s = new GetMap(this);
                caller.Call(s);
                return s.response;
            }
        }

        public sealed class Response : IResponse
        {
            public nav_msgs.OccupancyGrid map;
        
            public int GetLength()
            {
                int size = 0;
                size += map.GetLength();
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                map = new nav_msgs.OccupancyGrid();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                map.Deserialize(ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                map.Serialize(ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "nav_msgs/GetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACr1VTW/bRhC9L+D/MIAOsQtRUdKgKAz0UMCom4NRF0lOgiGMliNyW3KX2V1aYX993y5F" +
            "ymqToIc6BmGRy/l6b2YeF3QrkWIt1HJHHIjJ8uO2DVV4+ZvWfcdWD7felKooCvX5V8lVXaif/ue/C3X3" +
            "7vb6C+VcqAW9r00gL52XIDam0l8XN1QdK1qSsXSoja5JGP+0NM1T64S5827HO9OYOJDbI6SbcqyU+lW4" +
            "FE/1+KPU4k4i33Bk2js/UabuuJvPjd07lQsb6SxxmMvw7lC0/AfcnEewJYXIPhpb0cHEmi7Xy/XVimhG" +
            "iBin0owAmpcUJyX1bCuhzXr5ar1+gNMH+6d1B0vgoni1UsbGHzcPOfXz9STEcuzJyFFqxrvItmQP6sFG" +
            "ObFUm6oWXzTyKE0C3XZS5tooDp2E1dRFXJVY8dw0A/UBRtGRdm3bW6M5CkXTypk/PEEIU5eI1H3DHvZg" +
            "19hkvvfcSoqOK8jHXqwWentzDRsbRPfRoKABEbQXDqkRb29I9SDv+9fJQS3eH1yBR6nQ+zk5GsAxFSuf" +
            "0hylOjlcI8d3I7gVYoMdQZYy0GU+2+IxXBGSoATpHEbxEpXfD7F2Y0cf2RveNehwIA0GEPVFcnpx9SRy" +
            "Kjstg3VT+DHiKcd/CWvnuAlTUaNnTUIf+goEwhBj92hKmO6GHEQ3BttCjdl59oNKXmNKtfglcQwjeOWO" +
            "4JdDcNqgAWUebBWiT9FzN7bQkOcXiSfrOEtE7RrgQZt13lDfcjTgiHeuH6VP1+xZR/EmQBjcPh+eq99x" +
            "qzN+jMCoK5NsHqCbjcMmlCNBONum5216eqIHGBnX9Dn5pn2ZBOlB7WGYhu70Dg5AAQJLSMMmWYWHaTTz" +
            "4dGgFixX/KfFeHpM6rypMBRHRKmETbskXJ7LpB3T7mVdEW6Kg/PgqsOMHZ0QKAtnlqhJghBopSpxWHU/" +
            "jLzfZ5ec7tma/O+MqcU/n0R97CsKzwBQ7N4LxrVjLcukJum4PL434wxYPHsz+a5I3TvwOBuo33tMs7c5" +
            "7slOfTOMKGaeY6x9ZGOPn64JAuBABXPVZ4hVnqwf3tCn+W6Y7/76VghO/H32i33G6nn96enjif20t/go" +
            "fx3UdHcAvL8B9AgLWdoIAAA=";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetMap()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetMap(Request request)
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
