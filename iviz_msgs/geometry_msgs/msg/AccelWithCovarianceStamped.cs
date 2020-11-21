/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/AccelWithCovarianceStamped")]
    public sealed class AccelWithCovarianceStamped : IDeserializable<AccelWithCovarianceStamped>, IMessage
    {
        // This represents an estimated accel with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "accel")] public AccelWithCovariance Accel { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AccelWithCovarianceStamped()
        {
            Header = new StdMsgs.Header();
            Accel = new AccelWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AccelWithCovarianceStamped(StdMsgs.Header Header, AccelWithCovariance Accel)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACr1VTY/bRgy9C9B/ILCH7Ba2C2SLPSzQQ9Gi7R4KBE3QTxQLWqKkaaQZlTNaW/n1fRzJ" +
                "XgfJoQXaGDYkj8hH8vGRuqI3nYukMqpE8SkSe5KY3MBJauKqkp4OLnUwaUTFV0JVCFo7DwNqlAeBS03w" +
                "gBsP464svheuRanLl7L4ykB+BsbX4YnVsWFk4LIoiy//409Z/PD6u3uKqX4cYhs/X3Ipiyt6nZAna02D" +
                "JK45MTUBSbq2E9328oRCcwEoOz9N8ygR1VwtFOHbihflvp9pirBKAVQMw+RdZVycGTgBmKvzxDSyJldN" +
                "PesH3GV8+0X5a8rsPnxzDysfpZqSQ1IzMCoVjs63eAjjyfl0+9I84PjmELb4Ly0IP2dAqeNkGcvR+mrJ" +
                "cry3MJ8tNe4AD5IEgepI1/nsEX/jDSEOspAxVB1dI/1Xc+qCB6JQ7t6+F0OuwANgX5jTi5tLaEv9njz7" +
                "cMJfIJ+D/BNc/wxsZW07NK83CuLUgkdYjhqeXA3b/ZxRqt5BwNS7vbLOZWFuS1CAfJuFmqyRuTe4coyh" +
                "clnmJvCyiEktQO7Lo6v/R3W2EiBCnReJfmQ+zqo7NRDpmhXklxxYA3+NCsobGczm+ZzgpomhhRmiWqfu" +
                "ec6u6Mdw2A78JzR/nvYFLDSZv7vjHYR3nlAsAHXHJRGhoO5sDzmDoSQabQ6g78Ydpd7y8XKRZNOs7gdE" +
                "UMzgJke5cGaVrMnr44bmDb3bkIY1BO/DlOgXMswPjn/9+PFv+RiKafrA6e6L32/v/rgo6NO28981cK/h" +
                "rdghVorDDobQBSK3vcq+zYvDdohto5+kSkFvabW5OFgtP1mda+BzpZfvEHrKD98vc5cX3UNeTMFjsQ3C" +
                "GFjUfHaFZ+0UviYeEx7eOkFlA1aoDiDRh2QgA78FqGBJmDuPI9CwsZV97BeCM5N0Lbt2t6FDB3azlQ34" +
                "spnzMncVqWsddrm5IhRUv3ozrQVCt81LzFjfL1kv0aBlQzlp8GZHDw3NYaKD1YQbXd8igfZIcs0sb7kU" +
                "wiZPzorxPq2vAmQAamLkFhvRx4Q3WJ7oVdeEoTzdYs2dbt+Vxd/KT+N8zQcAAA==";
                
    }
}
