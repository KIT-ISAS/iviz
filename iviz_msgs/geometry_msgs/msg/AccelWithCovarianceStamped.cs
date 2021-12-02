/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AccelWithCovarianceStamped : IDeserializable<AccelWithCovarianceStamped>, IMessage
    {
        // This represents an estimated accel with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public AccelWithCovariance Accel;
    
        /// Constructor for empty message.
        public AccelWithCovarianceStamped()
        {
            Accel = new AccelWithCovariance();
        }
        
        /// Explicit constructor.
        public AccelWithCovarianceStamped(in StdMsgs.Header Header, AccelWithCovariance Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// Constructor with buffer.
        internal AccelWithCovarianceStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new AccelWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AccelWithCovarianceStamped(ref b);
        
        AccelWithCovarianceStamped IDeserializable<AccelWithCovarianceStamped>.RosDeserialize(ref Buffer b) => new AccelWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Accel is null) throw new System.NullReferenceException(nameof(Accel));
            Accel.RosValidate();
        }
    
        public int RosMessageLength => 336 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelWithCovarianceStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "96adb295225031ec8d57fb4251b0a886";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/UQAy951dY2kNbtLtIFPVQiQMCAT0gIVrxKVR5EycZmswEz6S74dfzZpJNW7UH" +
                "kIBVpE0m9rP9/Ows6KI2nlQ6FS82eGJL4oNpOUhBnOfS0NaEGialqNhcKHdOC2NhQKVyK3ApCB5w47Zb" +
                "Z2+EC1Gq01/2PEJ8BMILd81qOCIk2Cx79pd/2dvz16fkQ3HZ+so/HvPIFnQekCFrQa0ELjgwlQ75maoW" +
                "XTVyjRJT6ig4vQ1DJ36dTdzgqsSKctMM1HsYBQcO2ra3Jo8kzKXv/eFpLDF1rMHkfcN6j7OIjsvLjz5x" +
                "evbyFDbWS94Hg4QGIOQq7I2t8JKy3thw/CQ6ZIuLrVvhUSqwPAenUHOIycou9jLmyf4UMR6Nxa2BDXIE" +
                "UQpPh+nsEo/+iBAEKUjn8poOkfm7IdTOAlAotWzTSATOwQBQD6LTwdEt5Jj2KVm2bg8/It7E+B1YO+PG" +
                "mlY1etbE6n1fgUAYduquTQHTzZBA8sZAsdSYjbIOWfQaQ2aLV0mXIbYvdQT/7L3LTVJ11HPmg0b01I1L" +
                "U/wrNVbioDodRkk+MAx7me3bhkyjEfQWDNgCb6UKCusYjKZJ7OGlgaGAYZ2N87WfqAW9d9tVy9+h73mm" +
                "RyBXJtJOdicQ2jyJGHM1u5SDkFMzm0O7ICaI+qh5iLk0OylWvLu9LZJplPIZ8BXDtkwxbvmyStTg4W5J" +
                "w5J+LkndFIA3rg/0iSLivePPDx9/ScdHWdk4DidPvx6ffLtVzH9s4R81baPuSuIh9obBhoWqBYqOW5Nt" +
                "ldZD3BTYOB8kD06PaTK5eZ7s/k+FU9TsgW8DXad3dwtcx012lnaPs9hcrTDGEsXOnnAsjMI1yiVKDR8T" +
                "p7IEHVQ4sGddAEbLV4AULAKCN3cdwLCNla1vRmITg3Qo62q9pG0NVpNVHOS0dtOiNjmpqQz2dPREIIh8" +
                "cmaaioNQyycYp6YZcx6DQbwA2YvuaE1nJQ2up20sCDc6fR8cbZDilFfaY8G5ZRqUEeIuoe8ceg9avOcK" +
                "K8/6gC8TRneSMe3mu2G++5n9AoCGDIabBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
