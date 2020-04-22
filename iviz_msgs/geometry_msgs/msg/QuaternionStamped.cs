
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class QuaternionStamped : IMessage
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        
        public std_msgs.Header header;
        public Quaternion quaternion;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/QuaternionStamped";
    
        public IMessage Create() => new QuaternionStamped();
    
        public int GetLength()
        {
            int size = 32;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public QuaternionStamped()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            quaternion.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            quaternion.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "e57f1e547e0e1fd13504588ffc8334e2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACrVTTYvbQAy9D+Q/CHLY3UJSaEsPgd5KPw6Flt17UDyyPWDPeDVysu6v7xu76xR6aA+t" +
                "MVjjkZ6enqQtPbQhk8qgkiVaJo6UNMBkCynSJViL61pUYiVUpaQ+RDahWrkXuHuy0Es27oe9c5+EvSi1" +
                "88d9G+GpsQA9rqbbuHf/+Nm4L/cfD5TNH/vc5JcLi43b0r2BIaunXow9G1OdQC80reiuk7N0NFMXT/Ot" +
                "TYPkvfupC95Goih33URjhpMlaND3YwxVEWEt/TkekSES08BqoRo71t80K+h4szyOs6af3x/gE7NUowUQ" +
                "moBQqXAOscEluTFEe/2qBLjtwyXtcJQGIq/JyVq2QlaeSh8LT84H5HixFLcHNtQRZPGZbud/RxzzHSEJ" +
                "KMiQqpZuwfzrZC26Za3QmTXwqZMCXEEBoN6UoJu7X5AL7QNFjukZfkG85vgb2Ljilpp2LXrWlerz2EBA" +
                "OA6azsHD9TTNIFVXRpS6cFLWyZWoJaXbfpjn0kr75o4EzHTOqQpogJ/n2WXTgj534xj8/xvIRhLmTqdl" +
                "Kq/bUCbzD4sH0WoVVDUw5MTpukBlhHvsWt0ltrdv6Gm1ptX6vloXlPcDgp9iTecDAAA=";
                
    }
}
