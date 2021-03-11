/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/QuaternionStamped")]
    public sealed class QuaternionStamped : IDeserializable<QuaternionStamped>, IMessage
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "quaternion")] public Quaternion Quaternion { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public QuaternionStamped()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public QuaternionStamped(in StdMsgs.Header Header, in Quaternion Quaternion)
        {
            this.Header = Header;
            this.Quaternion = Quaternion;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public QuaternionStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Quaternion = new Quaternion(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new QuaternionStamped(ref b);
        }
        
        QuaternionStamped IDeserializable<QuaternionStamped>.RosDeserialize(ref Buffer b)
        {
            return new QuaternionStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Quaternion.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 32;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/QuaternionStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e57f1e547e0e1fd13504588ffc8334e2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTYvbQAy9D+Q/CHLY3UJSaEsPgd5KPw6Flt17UDyyPWDPeDVysu6v7xu76xR6aA+t" +
                "MVjjkZ6enqQtPbQhk8qgkiVaJo6UNMBkCynSJViL61pUYiVUpaQ+RDahWrkXuHuy0Es27oe9c5+EvSi1" +
                "88d9G+GpsQA9rqbbuHf/+Nm4L/cfD5TNH/vc5JcLi43b0r2BIaunXow9G1OdQC80reiuk7N0NFMXT/Ot" +
                "TYPkvfupC95Goih33URjhpMlaND3YwxVEWEt/TkekSES08BqoRo71t80K+h4szyOs6af3x/gE7NUowUQ" +
                "moBQqXAOscEluTFEe/2qBLjtwyXtcJQGIq/JyVq2QlaeSh8LT84H5HixFLcHNtQRZPGZbud/RxzzHSEJ" +
                "KMiQqpZuwfzrZC26Za3QmTXwqZMCXEEBoN6UoJu7X5AL7QNFjukZfkG85vgb2Ljilpp2LXrWlerz2EBA" +
                "OA6azsHD9TTNIFVXRpS6cFLWyZWoJaXbfpjn0kr75o4EzHTOqQpogJ/n2WXTgj534xj8/xvIRhLmTqdl" +
                "Kq/bUCbzD4sH0WoVVDUw5MTpukBlhHvsWt0ltrdv6Gm1ptX6vloXlPcDgp9iTecDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
