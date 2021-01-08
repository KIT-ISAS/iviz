/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PointStamped")]
    public sealed class PointStamped : IDeserializable<PointStamped>, IMessage
    {
        // This represents a Point with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "point")] public Point Point { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PointStamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointStamped(StdMsgs.Header Header, in Point Point)
        {
            this.Header = Header;
            this.Point = Point;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PointStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Point = new Point(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PointStamped(ref b);
        }
        
        PointStamped IDeserializable<PointStamped>.RosDeserialize(ref Buffer b)
        {
            return new PointStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Point.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PointStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTWvcQAy9G/wfBDkkKWwKbelhobfSj0MhkNwXZUa2BfaMO5I3cX99pTG7W+ilh8bY" +
                "nrEtPb33JF/B48ACheZCQkkFEO4zJ4Vn1sHed1QoBYKQc4mcUAm6ghMBpgjKE4niNLfNN8JIBYa6tM2G" +
                "Mfu9bdrm038+2ubHw9c9iMbDJL283aq3zRU8qBHDEmEixYiK0GWjxf1AZTfSkUaojClC/arrTHLnmdUI" +
                "O3tKVHAcV1jEojSb9mlaEgcXf5Z8AvBUTmbbjEU5LCOWv8yq+H4J/Vyqnd8/7y0qCYVF2UithhEKoXDq" +
                "7aMFL2bd+3eeYYmPz3lnz9SbxWcGoAOqM6YX756TRdl7mTebxjuDN5PICkWBm/ruYI9yC1bHWNCcwwA3" +
                "Rv9+1SEnQyQ4YmF8GsmRg/lgsNeedH37J7RT30PClE/4G+SlyL/gpguwy9oN1rzRLZClNx8tci75yNFi" +
                "n9aKEka2MYWRnwqWtW08bStqIF/qZKo3svbGVhTJga0TsU5024gWL1D7cuD4itPZU7YhLOs2ovWPOM+Z" +
                "WabISaqmOQsrm0258zmqv4551xUyaTMGaptuzKgfP8DLZWviT9tfruI3+17QM8sDAAA=";
                
    }
}
