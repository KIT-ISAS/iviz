/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/AccelWithCovarianceStamped")]
    public sealed class AccelWithCovarianceStamped : IDeserializable<AccelWithCovarianceStamped>, IMessage
    {
        // This represents an estimated accel with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "accel")] public AccelWithCovariance Accel { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AccelWithCovarianceStamped()
        {
            Accel = new AccelWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AccelWithCovarianceStamped(in StdMsgs.Header Header, AccelWithCovariance Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AccelWithCovarianceStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Accel = new AccelWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AccelWithCovarianceStamped(ref b);
        }
        
        AccelWithCovarianceStamped IDeserializable<AccelWithCovarianceStamped>.RosDeserialize(ref Buffer b)
        {
            return new AccelWithCovarianceStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Accel is null) throw new System.NullReferenceException(nameof(Accel));
            Accel.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 336;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelWithCovarianceStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "96adb295225031ec8d57fb4251b0a886";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
