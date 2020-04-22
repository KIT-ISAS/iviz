namespace Iviz.Msgs.grid_map_msgs
{
    public class GetGridMap : IService
    {
        public sealed class Request : IRequest
        {
            // Frame id of the submap position request.
            public string frame_id;
            
            // Requested submap position in x-direction [m].
            public double position_x;
            
            // Requested submap position in y-direction [m].
            public double position_y;
            
            // Requested submap length in x-direction [m].
            public double length_x;
            
            // Requested submap width in y-direction [m].
            public double length_y;
            
            // Requested layers. If empty, get all layers.
            public string[] layers;
            
        
            public int GetLength()
            {
                int size = 44;
                size += frame_id.Length;
                for (int i = 0; i < layers.Length; i++)
                {
                    size += layers[i].Length;
                }
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                frame_id = "";
                layers = System.Array.Empty<0>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out frame_id, ref ptr, end);
                BuiltIns.Deserialize(out position_x, ref ptr, end);
                BuiltIns.Deserialize(out position_y, ref ptr, end);
                BuiltIns.Deserialize(out length_x, ref ptr, end);
                BuiltIns.Deserialize(out length_y, ref ptr, end);
                BuiltIns.Deserialize(out layers, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(frame_id, ref ptr, end);
                BuiltIns.Serialize(position_x, ref ptr, end);
                BuiltIns.Serialize(position_y, ref ptr, end);
                BuiltIns.Serialize(length_x, ref ptr, end);
                BuiltIns.Serialize(length_y, ref ptr, end);
                BuiltIns.Serialize(layers, ref ptr, end, 0);
            }
        
            public Response Call(IServiceCaller caller)
            {
                GetGridMap s = new GetGridMap(this);
                caller.Call(s);
                return s.response;
            }
        }

        public sealed class Response : IResponse
        {
            
            // Submap
            public grid_map_msgs.GridMap map;
        
            public int GetLength()
            {
                int size = 0;
                size += map.GetLength();
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                map = new grid_map_msgs.GridMap();
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
        public const string ServiceType = "grid_map_msgs/GetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "802c2cbc7b10fada2d44db75ddb8c738";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACr1XbU/bSBD+bon/MCIfSrgkhVChKxIf0KH2kFqJOyqdTgiFjb1OFmyvu7smuL/+ntm1" +
            "HYfQoyddiSLF3p2dfWbmmZcM6IMRuSSVkE7JLSXZap6LkkptlVO6ICO/VtK6SWSdUcWCUpafqSSKBvRn" +
            "2JPJ1ilV0OM4UUbG/vU6v5lEaaaFO37XCc0eI6KXtNQvaqmfRZLJYuGW/4ojiHwXxUolQUH9goInADJR" +
            "S2MndJGSzEtXj2ghHYksa3caT17fNAtRNB6PWceVvzhaGJXM8DDL7cK+/Yi3z4DDOzvR6f/82Yk+X308" +
            "oWev3AEkfuSraSlFIk3UbF0UqYZrUh31Zbw5VIAfzxnZE5wLq+K+OO3pkv0rsuGEvoCFPQmLk4l00uSq" +
            "kLRaqnjZbICLOqfb8HJLhYT3naa5pAeRgaEDjp42wE2pNiQolohCw/NFC6Z3oofaA5g9gz0RTrBcEnz1" +
            "gblwNP1cZU6dGSNqnGURzwm9IuuEccCRyEfaS2QqIEgHw0lUqcIdHpOuYNnMS828FB/8TWdVXrx4VhXF" +
            "k7OvSxBmAZPkd88N2nMKlUQUSagRw6hZb6jjk8TCMp9I/Shc5285Mr3MMp0gH/v0H3J5Q/yHMvdSW7lF" +
            "ilgWiAvr4GVvD0iYgoEJL94Gm26D1oXUuXSmDs7x+lCd5E8LRke+4GAOwZWD34UBeOkE888zfqkWS2nG" +
            "mXyQGbMpLwHf77q65CQdINmUJXwXElxCkaqpsiGNYp3nVaFi4SRxZDfOh9wSVIJ7Kq4yYSCPVFMFi3t/" +
            "sXZ8LRfGIpZ0cX4CmcLKGIEFoBoaYiORZ+gqF+fkWX005QPR4MtKj/EqFwhCdzliIZAPluRjCYYwTmFP" +
            "cMd+MG4C3fCOxC0JSopfm+HVDgmXAIIsNYrHHpBf1m6pQ3AfhFFinqEHWorhAWh9w4feDHuaGfYJalWh" +
            "W/VB4/qOH1FbdHrZpvESMcvYelst4EAIlkY/qASi89oriTMFIlKm5kaYOvIJ5q+MBqFvOw6fjwh+hbU6" +
            "VghAgublllsd++dVh60EYE6eIY05TrBAtDnPecHMSY2EJaWI5YiJxst+AulaP5cRbVR7dkLRpQYhOoHo" +
            "jwqGmsLrXcu9no0As9PmDxjhhCqsj1lnAsxBgnjUGxZ3Veixe6q7p2+vZcHaf50ZXbhApQ2vbuLnt69r" +
            "76PQ5JPoBaPap9XPM+/7LZkNvMxQaSRlWt8TiggHai3wSdTow6id1opFU+hDDGEcjx86rvI1jcFOzBE5" +
            "Hxd8HIU02lLGM4r/DR8UwlLGKuV62pDDp22QCv45mmJ4aD9+t/sMyN/UHotewY1PLQo0kT274agYlR/j" +
            "mwj9AyOb30XbhbusH+jWwAUU9PpF14cmROetPFQZGWY2FAQ/3/kJiXJtHddNHjuL5n3D7Z0KHsBUDo+d" +
            "dR5rt7i+lhIIpG27DaOY6TS1mNE7X5ciQSdbkMwkhx2gHGNBInf+h/o4Bl80xlC71FWW0Nmnv87+vuJh" +
            "cmWUc9KnDc+tdhMEl+VEQgPXuIYVXP3YzjHb1ZNNlWE7fSddO35Pje5G90M69WCu+zb8wodn4Yrrw5t9" +
            "tbkyvdm/w8r9TTTwFdo2c8OIjsYx+lGBQeH43cHju18PSOWcDNxJYAiwIYMegDPWGUaLRpiH85W3Hmav" +
            "bfFtmRNH5dcHN5NMzKEXcHeXEiOJ211vWfVNwuenhBt7qx4tVo/2gWaf0ZzS++nh8cEB0V6hMWMEycaZ" +
            "3P3uKnjOq4O3PXZutCx22Efg/9btrnc6ALiot7oBAL+H76ft9rSvrvHD7nqvU3jUW+vUebdsR9LIlP/Q" +
            "gN5cmdjlRq9GdIeH2P8ZGHm23PN7uHHyqiXgvGXkTjtVNC5AtoQnOH2Bsa5Yk7cb6IJDuAQ20Xki6Ecn" +
            "rgSEedPZYXcweI0PhqftozvRP/qdBgfCEAAA";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetGridMap()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetGridMap(Request request)
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
