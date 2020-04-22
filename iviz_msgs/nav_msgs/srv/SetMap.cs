namespace Iviz.Msgs.nav_msgs
{
    public class SetMap : IService
    {
        public sealed class Request : IRequest
        {
            // Set a new map together with an initial pose
            public nav_msgs.OccupancyGrid map;
            public geometry_msgs.PoseWithCovarianceStamped initial_pose;
        
            public int GetLength()
            {
                int size = 0;
                size += map.GetLength();
                size += initial_pose.GetLength();
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                map = new nav_msgs.OccupancyGrid();
                initial_pose = new geometry_msgs.PoseWithCovarianceStamped();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                map.Deserialize(ref ptr, end);
                initial_pose.Deserialize(ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                map.Serialize(ref ptr, end);
                initial_pose.Serialize(ref ptr, end);
            }
        
            public Response Call(IServiceCaller caller)
            {
                SetMap s = new SetMap(this);
                caller.Call(s);
                return s.response;
            }
        }

        public sealed class Response : IResponse
        {
            public bool success;
            
        
            public int GetLength() => 1;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out success, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(success, ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "nav_msgs/SetMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "c36922319011e63ed7784112ad4fdd32";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACsVW227jNhB9J5B/GCAPmxSW17tbBEWAPhQNus1D0LTZohfDMGhpLLGVSC1JxdZ+fQ9J" +
            "SXYuLfrQTQMhFqm5njkz5CndsSdJmnfUyJa8KdlXbGmnfEVSk9LKK1lTaxwLLe/XjSvd6x/yvGulzvv3" +
            "VhVBUZRsGva2T99vIf0LLHxr7qVVEOQ7L5uWi9HeOtrLskxsjKnJdXnOzglxIr7+j/9OxM3d+0t6PvQT" +
            "cUofKuXIcmvZsfYOYLzNrqgcEpshYtpVKq+IJf7lXNfH0gCLWms2cqNq5XsyW5g0o4+5EN+zLIBnlX6E" +
            "OL1hL6+kl7Q1NqoH+G5kO+0rvTUiBha/UYHNGIY1u6yRf0DNWBibkfPSeqXLVK2zxWxxPieaMoSNQ2iK" +
            "kZrlYCc4tVKXTMvF7M1isYLSz/pPbXaot6PszVwo7b9arqLrz1cT54tUk4RRKAZoogtpAT3QKEaUKlWC" +
            "k1nN9wyuDEyKX33fspuPVcRTsmYr67qnzkHIG8pN03Ra5dIzedXwA31oAhBJbQAy72ppIQ90lQ7iWysb" +
            "DtbxOP7YMYhM11eXkNGO884rBNTDQm5ZulCI6ysSHcB79zYoiNMPO5NhySVqPzlHAaQPwfI+8CjEKd0l" +
            "fHyRkpvDNtBheCkcncW9NZbunOAEIXBrQMUzRH7b+8qkisZO29SosKMcCMDqq6D06vzIcgg7NIM2o/lk" +
            "8eDj35jVk92QU1ahZnXI3nUlAIQgaHevCohu+mgkrxW6hWq1sdL2Imgll+L0u4AxhKAVK4Jf6ZzJFQpQ" +
            "RGIL522wHquxVsULDImjdpxGRGVq5IMy57FDbSO9AkZyYzqfkqyklblnqxwGg9nGzQfjZuzqmD8okObK" +
            "MANoJx3VBp1QJICwtw7rdVgdzQNQxtRddL5sXoeBtBJbCAbSHb5BAVkAwAKjYRmk3GqkZtwcBCpGc/nH" +
            "Eml3cGqsKkGKIaMQwrKZER4rizA7xt6Lc4Vlne2MBVZhxA9KMBQHZxxR4wiCofkz58bg7rMV+anHUOJv" +
            "DkM91RWBxwQQ7NYy6NrKnGdhmoTtYviuEgc01laNunMStwY4TgLixw5stjraPciJF8sRwUw8Rtt7qfRw" +
            "dI0pIB1MwRj1g4wTsy6+pP301k9vn14qgwN+z57YD1B9GH9YfTygH/oWh/I/JzW+7V6uQH9zXZqyHU+K" +
            "mCxOEYXpAxJGhqarGgDZso0n1OMTLPJzOn0eXUnEU+fpsve/5f78pexpM6bEO2jYQGgfbltxgMTwYeSn" +
            "6b70tLcD+S/2F8BqyhqQWrU/zLxJHJcDoAgKuXCpQCxbtecik/vjGFPnQ/s6sDFez2KDHXRx+wqH/Nl+" +
            "Rv2MPmF8msFBOkN+pWDxyfZvz2//HrfPR7Iu312sjpJB9f4CouBFaNsLAAA=";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SetMap()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetMap(Request request)
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
