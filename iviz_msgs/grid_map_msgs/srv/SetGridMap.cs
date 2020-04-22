namespace Iviz.Msgs.grid_map_msgs
{
    public class SetGridMap : IService
    {
        public sealed class Request : IRequest
        {
            // map
            public grid_map_msgs.GridMap map;
            
        
            /// <summary> Full ROS name of the parent service. </summary>
            public const string MessageType = SetGridMap.MessageType;
        
            /// <summary> MD5 hash of a compact representation of the parent service. </summary>
            public const string Md5Sum = SetGridMap.Md5Sum;
        
            /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
            public const string DependenciesBase64 = SetGridMap.DependenciesBase64;
        
            public IResponse CreateResponse() => new Response();
        
            public bool IsResponseType<T>()
            {
                return typeof(T).Equals(typeof(Response));
            }
        
            public int GetLength()
            {
                int size = 0;
                size += map.GetLength();
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
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

        public sealed class Response : IResponse
        {
        
            public int GetLength() => 0;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string MessageType = "grid_map_msgs/SetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "4f8e24cfd42bc1470fe765b7516ff7e5";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACr1XUU8bORB+X4n/MCIPBY6kECp0ReIBXdVepVbirpVOJ4SC2Z1sDLv21nYI6a+/b+zd" +
            "zab01Hu4EiGya8+Mv5n5ZjwZUa2arHS6mOFhVvvSv3yHt4+qiTvZeDzOdrLz//mzk3389O6MvnvuTjYi" +
            "eZTzacGqYJe1W+/N3JLGv2woU6k1OzKqZj/JfHDalFfXadVvCd4qr/OhOO3ZJmhrVLU/oc8LHkp4aBYc" +
            "2NXaMK0WOl+0GzR3tqab9HJDhrmgYOmW6UFVuoCeNmQdcNPcOlKUc1WRnVPACWUHZqAxQB0BzL6DvVBB" +
            "iVyRYvW2siqcTD8uq6AvnFNr6IqI6PxpV+SDcgE4Cn6kvYLnCoJ0tD/JltqE41OyS3g2i1KzKCWKv9lq" +
            "WZsf6mpjvtF9XoIIC4Qkv0du0F7QNZMyBfKCpO5n7XpLHQkIe3gmid7KwlX9UjJzPcnmEs3TV+R6QVH7" +
            "wKYMC0nm47jQjvNo4aoeKFRRZPa4Lb7+gfhaxC+t5yekyNkgL2JDlqM/IOEcDCxk8Sb5dJOslmxrDm6d" +
            "ghPtNfj305LRky8FWFLwKSDuygE8ByX8i4xf6HLBblzxA1fCproB/Lgb1o0U6QjFpj3hr2RwSVXVmpY+" +
            "lVFu63ppdK4Ck2R2Sz/VlqIG3NP5slIO8ig1bUQ8xkus48/zlyWbnOn9mzPIGM85EgtAa1jIHaPOTIlN" +
            "iqw+mYpCNvq8smO8cokk9IcjFwr14IkfGzBEcCp/hjMOknMT2EZ0GKcUaClxbYZXv084BBC4sWgee0B+" +
            "uQ4Lm5L7oJxWtxWL4RwRgNUXovRif2BZYJ+hVxnbmU8WN2f8F7Omtys+jRfIWSXe+2WJAEKwcfZBFxC9" +
            "XUcjeaVBRKr0rVNuncUCi0dmo7eRk0HSFzOCb+W9zTUSUNBKh0XbzFI2ZuiHP687PCkA4eQFyljyBA9U" +
            "V/NSF8KcuWN40qicD4Vosly0+zrKShuxTne6E8ouLQjRC2R/LOGoM9HuRu75fASYna5+wIigtPExZ70L" +
            "cAcFElFvedx3ocf+ad0/fX0uDzbx693o0wUqbUV1G7+8fdlEH42mnmQ/cKp7Wv089/79ShYHLyt0GqbK" +
            "2ntCE5FEbQQ+qDXuYfRO71XZNvqUQzgn44fNl/WGxmAn5oha1JWoo5FmT4zJjBK/0weNsOFcz6WftuSI" +
            "ZZukUnxOphgeuk/c7T8jiid1atkzhPFbjxJNeOA3ApWj82N8U+n+wMgWd3HtIlw+DnQb4AoGBvdFfw9N" +
            "iN508jDlOM1saAhxvosTEtXWB+mbMnaa9n0r7L0JGcB0jYhd9BHrtqS/NgwE7LvbRlDM7HzueZCqRhW4" +
            "yUriiiXtABUECwq5jz/M5zn4YjGG+oVdVgVdfPjr4u9PMkyunA6BY9nI3Oq3QUhbLhgWpMe1rJDuJ36O" +
            "xa+B7Fw78TPepJvA7+nDu8P7fTqPYK6GPvwiyrN0xNXx9YHeXpleH9xh5f46G8UO7du54ZBOxjnuI4NB" +
            "4fTV0eOrX49I11IMcpPAEWBDBT0AZ24rjBatsAznq+g93N74Eq9lKRxdXx1dTyp1C7uAu7tgjCRhd7Pl" +
            "9VdGzM8JJw5WI1qsnhwAzYGgOafX0+PToyOiPWMxYyTJNphy+90tEbloDtGO2OWiFbHjIYKVLsJid7PT" +
            "A8BBg9UtAPg+fj3ttqdDc20cdjd7vcGTwVpvLoblaSYdz+UHDegtnUlC7uzqkO7wkMcfA4eRLffynk6c" +
            "PGsLeNMxcqebKtoQoFrSE4JeYqwzG/L2A10KiLTANjvfCMbRSToBYd4Mfr9XTFETxfT0VHUn+wf9gqTY" +
            "OQ8AAA==";
            
    }

}
