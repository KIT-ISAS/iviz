/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt32MultiArray : IDeserializableRos1<UInt32MultiArray>, IMessageRos1
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public uint[] Data;
    
        /// Constructor for empty message.
        public UInt32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<uint>();
        }
        
        /// Explicit constructor.
        public UInt32MultiArray(MultiArrayLayout Layout, uint[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt32MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UInt32MultiArray(ref b);
        
        public UInt32MultiArray RosDeserialize(ref ReadBuffer b) => new UInt32MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) BuiltIns.ThrowNullReference();
            Layout.RosValidate();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 4 * Data.Length;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt32MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4d6a180abc9be191b96a7eda6c8a233d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5Ucpa6ENgsJcWBh2MEkJQrXOsRJaCJDfr/vp9sh3bafc4" +
                "ZgyW73R33/fpTkP6rll4Jm3tnkSgkDM9ljqopXPi7UG82TJQwd6LLZPkTBkVlDWUWZcMSdq0LNgEUdnw" +
                "Cq2piOEihvtJknxIRrr51s+Q/IFTlam0SZKRFEE0u5JSmbCYr9bUPpWXuvCq0iksSZL7f/wkj0/f7sgH" +
                "uSn81n9+zwcq/IBmHWmolGrh2JOgLRt2Kq29V1JBKw+SQneoBRIchAsqLRFVswtvB54QfT3tRyrHZJ1k" +
                "x5IyZwtCZXZUWB8BBEvKmOb/TPM2BRREeci1bOU6uejg7IGBgH0jd4ViY7PMc++cDkJKZbbEmuOZ+9gu" +
                "wGJCJz7SpymaxTpPPrellrR8+Ll8fqIXpqNTIbABVAL2wp+D8MEpycggjDy1BMhWPK8ir97eTLnIc0h4" +
                "O+Ev1Hg33l/SfQVm1efwKQZv6hKr2Xqkzi3z9WgHy36dDCMFYAEI4eSYFldpLiCtppvr6a/rL1NSRZyE" +
                "owo5iAAbxucVOFOrraNms0eWY8UetDsuwt9VBVB5NV1PtHhBXsAd5Ky2eRh0Lq9+M0UXKvasFVpYFyOg" +
                "GUU093Q7n91Mp0QXxgZudjZikvK0K6FclQ5qV9gvm4SzPoKjkiEfdJ4WAAr1rGcA8J3dzk/ueT9do8Og" +
                "87UJFz1bm66S5eNJOs4YnYT2jtdSlNzZ45h2WEDvsjDjqlv28b+uOPmP89/OVhKJYDAa/hiVegXFt+oV" +
                "Hd927mm+GjXi5dcczbuNdBGnBNcAlbhw/WUbWEsWA+vVX2r8AfgORpHUBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
