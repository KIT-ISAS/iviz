/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class WrenchStamped : IDeserializable<WrenchStamped>, IMessage
    {
        // A wrench with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "wrench")] public Wrench Wrench;
    
        public WrenchStamped()
        {
            Wrench = new Wrench();
        }
        
        public WrenchStamped(in StdMsgs.Header Header, Wrench Wrench)
        {
            this.Header = Header;
            this.Wrench = Wrench;
        }
        
        public WrenchStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Wrench = new Wrench(ref b);
        }
        
        public WrenchStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Wrench = new Wrench(ref b);
        }
        
        public WrenchStamped RosDeserialize(ref ReadBuffer b) => new WrenchStamped(ref b);
        
        public WrenchStamped RosDeserialize(ref ReadBuffer2 b) => new WrenchStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Wrench.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Wrench.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Wrench is null) BuiltIns.ThrowNullReference();
            Wrench.RosValidate();
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 48; // Wrench
            return c;
        }
    
        public const string MessageType = "geometry_msgs/WrenchStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d78d3cb249ce23087ade7e7d0c40cfa7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTW/UMBC9+1eMtIe2aBtEizhU4oCEgB6QKlrBAaFqNp5NLBI72JPdhl/Ps7NNqeDA" +
                "AVhFmw/7vZl588YrekX7KL5uae+0pShbya9CdQjROs8qtI3cC7G3pK6XpNwP5p2wlUhtuZlPB4pyM+bl" +
                "X/6Z99dvLyipve1Tk57Osc2KrhVJcbTUi7JlZdoG5OSaVuJpJzvpqGQrlsqqToOkCsCb1iXC1YiXyF03" +
                "0ZiwSQPK7vvRuzrXvVR7jwfSeWIaOKqrx47jLzJldlxJvo1FxsvXF9jjk9SjOiQ0gaGOwsn5BotkRuf1" +
                "/CwDaEWfP4T07ItZ3ezDKb5LA4mXLEhb1py13A1RUk6Y0wWCPZmrrBAEKgnC2UTH5dstXtMJIRpykSGg" +
                "Scco4WrSNngQCu04Ot50kolrSAHWoww6OvmJ2Rdqzz7c08+MDzH+hNYvvLmm0xbN67IMaWygJDYOMeyc" +
                "xdbNVEjqzolX6twmcpxMRs0hzepN8aTmPpbW4M4phdqhE7Z42SSNmb205dbZf2XLRgLsF6fZm/Mk3Fss" +
                "Su4UakjZmRAMIm2joIqBa1mjWbBSyRjdDtle2AlNBHrkeWPfFJdlw8G4H6XWEM9pJnt4xT/s9n8KPAT9" +
                "TYVMu7L2uMgqj8Nl8W3wsH8vjJZi0hYkgNZFQF3wFVhxAKE+qOOUbJBEPig4ev4KSoGJMpqHAWQY6cg+" +
                "dZyx+TMgx1I11Zr2rfh5VzZBmd0y7a6m6BpnZyQC9QuY6VDcmnR7BhN13ZzzHAyOBEkMWgAnFV1uaQoj" +
                "7XNBeIiHQybQRpa8ygxoCOt8whwoHgt6FdB7yJISN9kgSXG8VcZsu8D64jndLU/T8vTd/ACyd0KauQUA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
