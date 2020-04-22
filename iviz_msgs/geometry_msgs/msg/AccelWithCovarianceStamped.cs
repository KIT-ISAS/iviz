
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class AccelWithCovarianceStamped : IMessage
    {
        // This represents an estimated accel with reference coordinate frame and timestamp.
        public std_msgs.Header header;
        public AccelWithCovariance accel;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/AccelWithCovarianceStamped";
    
        public IMessage Create() => new AccelWithCovarianceStamped();
    
        public int GetLength()
        {
            int size = 336;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public AccelWithCovarianceStamped()
        {
            header = new std_msgs.Header();
            accel = new AccelWithCovariance();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            accel.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            accel.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "96adb295225031ec8d57fb4251b0a886";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/UQAy9R9r/YKkHWrS7SBT1UIkDAgE9ICFa8SlUeRMnGUhmgmfS3fDreTPJhkXl" +
                "ABJ0FWmTif1sPz87R3RVG08qnYoXGzyxJfHBtBykIM5zaWhrQg2TUlRsLpQ7p4WxMKBSuRW4FAQPuHHb" +
                "rbOXwoUo1ekvexIh3gHhqbthNRwREmy2yB7/498ie3X54px8KK5bX/kHYyaL7IguA5JkLaiVwAUHptIh" +
                "RVPVoqtGblBlyh41p7dh6MSvs4keXJVYUW6agXoPo+BAQ9v21uSRh7n6vT88jSWmjjWYvG9Yb9EW0XF5" +
                "+dYnWi+encPGesn7YJDQAIRchb2xFV5S1hsbTh9Gh+zoautWeJQKRM/BKdQcYrKyi+2MebI/R4z7Y3Fr" +
                "YIMdQZTC03E6u8ajPyEEQQrSubymY2T+egi1swAUSl3bNBKBczAA1HvR6d7JAXJM+5wsW7eHHxF/xvgT" +
                "WDvjxppWNXrWxOp9X4FAGHbqbkwB082QQPLGQLTUmI2yDln0GkNmR8+TNENsX+oI/tl7l5sk7CjpzAeN" +
                "6Kkb16b4f4KsxEF3Ooyq/M1ELPZK23cOyUYrSC4YEAbqShXU1jFITfPYw00DQwTDOhunbJorYL1x21XL" +
                "XyDxebJHIFcm3s52Z9DaPI8YdjW7lIOQUzObQ77gJoj6KHvouTQ7KVa8O9wZyTSq+QL4ioFbphgHvqwS" +
                "ZXi8W9KwpO9LUjcF4I3rA72niHjr+MPvjz+m45OsbByHs0efTs8+HxRzp138u75t1H2VeIjtYbBqoW2B" +
                "ruP6ZFulJRH3BfbOW8mD01OaTH4+T3Z3VeQUdy7z8DtBN+nlrzWu40q7SEvIWaywVhjziXpnTzgWRuEa" +
                "RRMFhw+LU1mCESocCLQuAKPlr4AUbASCN3cdwLCWla1vRm4TiXQs62q9pG0NYpNVnOi0f9PGNjmpqQwW" +
                "dvREIEh9cmaaqoNcy4cYqqYZcx6DQcIA2UvvZE0XJQ2up20sCDc6fSgcbZDilFdaaMG5ZRqXEeJXRl87" +
                "tB+0eM8Vdp/1Ad8oDPAkZtrNd8N8932R/QA9KaQHqAcAAA==";
                
    }
}
