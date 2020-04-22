namespace Iviz.Msgs.grid_map_msgs
{
    public class GetGridMapInfo : IService
    {
        public sealed class Request : IRequest
        {
        
            public int GetLength() => 0;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        }

        public sealed class Response : IResponse
        {
            
            // Grid map info
            public grid_map_msgs.GridMapInfo info;
        
            public int GetLength()
            {
                int size = 0;
                size += info.GetLength();
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                info = new grid_map_msgs.GridMapInfo();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                info.Deserialize(ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                info.Serialize(ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string MessageType = "grid_map_msgs/GetGridMapInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "a0be1719725f7fd7b490db4d64321ff2";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACr1UwWrcMBC9C/YfBDkkKfUG2tLDQg+F0jTQQNr0FspGkca2QJYcSU7W/fq+kbPeLKWk" +
            "hyaLWVvSzJt582ZUVZUQB/I0WiM71Uvr6yAarNZYrbvUpBM+O1f9GU6m44X48J9/C3F+ebqSf427QIpf" +
            "SBmK8ijbjqTyRtZRdXQsHvbb8mIu3ykFN2QbvAy1zC0VXHnVnWhy7udS1C6o/P6djLMhu30l3+QWDOWm" +
            "MjaSLghX3SMHV0zWm33z8Qnzkc0vQqK9dLjYmnxG6sDg7cJHGqqtJ8Ob1xOn6wm1odBRjuNUnILX4+/Z" +
            "xEjZTKGmArMElxl1VxHJU1ZGZSXrgMrbpqVYObojBy/V9Ui/nOaxp7SE44/WJomnIU9ROTfKIcEoB6lD" +
            "1w3eapVJsrJ7/vBEGZTsVcxWD05F2IdorGfzUi9Gx5PodiCvSZ59WsHGJ9IQFgmNQNCRVLK+waEUg/X5" +
            "7Rt2EAc/7kOFJTUQYQ4OLVTmZGnTo0M4T5VWiPFqIrcENqpDiGKSPCp7ayzTsUQQpEB90K08QuYXY27D" +
            "JO6dilbdOGJgjQoA9ZCdDo8fIXPaK+mVD1v4CXEX419g/YzLnKoWmjlmn4YGBYRhH8OdNTC9GQuIdhaN" +
            "KJ29iSqOogxYCSkOPpeezCxfUQRvlVLQFgIYeW9zK1KOjF7UWFvzjLfDHwPAPfkRY8w6gYHazjzPBXdO" +
            "HQlMeqXpNTcab5uHc1ts+RoJ0W59l1JcBDTEbCC+DSAafcHd2b0cRySz2M4POiIr61PRbKYAOhiQkvUe" +
            "Y7G9hTbz1zh//XopBrv6zTRmudBKe1Xdz59Xt7vq46LpluIJUtuve9D7DRkqgEDZBgAA";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetGridMapInfo()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetGridMapInfo(Request request)
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
