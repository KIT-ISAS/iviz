/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class UInt8MultiArray : IDeserializable<UInt8MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public byte[] Data; // array of data
    
        /// Constructor for empty message.
        public UInt8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<byte>();
        }
        
        /// Explicit constructor.
        public UInt8MultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal UInt8MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new UInt8MultiArray(ref b);
        
        UInt8MultiArray IDeserializable<UInt8MultiArray>.RosDeserialize(ref Buffer b) => new UInt8MultiArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/UInt8MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "82373f1612381bb6ee473b5cd6f5d89c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9NlmT5KKUt5CEw2EsLgw7GCKGo1nWsxLaCJDfrfv2O5M+0exwT" +
                "Btv385wjXQ3pW8bCMmVaH0k4cinTY5k5tTFGvD2IN106ytlasWeSnKhCOaULSrSJhiR1XOZcOBFseESW" +
                "Ue7ThU+3syj6UIyy+l2tIdkTxypRcV0kISmcqKOiUhXudrtrorGCt11DCp2atCiK1v94RY9PX+/JOvmc" +
                "2739/J4PVPgOzTrSUCnOhGFLgvZcsFFx5Z1KBa0sSIqsQy1Q4CSMU3GJrIqdezvxjOhLE49ShkkbyYYl" +
                "JUbnhM5sKNfWA3CaVFHU/xeatyUgIdpDrk0rV+Oik9EnBgK2Qe7VMqB41kliubdPJyGlKvbEGfs9Byjn" +
                "sRSuEx/l4xiHRRtLNtVlJmnz8GPz84lemM5GOccFoBKw5/YShHVGSUYFUcjmSIBs4Dn1vHqxiTKe55Dw" +
                "dMJfqclhchzROoDZ9jl88snPVYvtYjdWl5blbnyA5biLhp4CsACEMHJCq2mcCkib0c31/Nf17ZxU7ifh" +
                "rFwKIsCG8XkFzlhn2lAdbFHlHNiDdsdF2PvQAJ23890sEy+oC7iDlNU+dYPOZdVvhuZrQseeNaCFdTUG" +
                "mrFHs6a75eJmPie6KrTjOrIWk5SlQwnlQjmoHbCP6oKLPoKzki4ddJ4WABr1rBcA8F7cLRv3sl+u1mHQ" +
                "+dqCq56tLRdk+biThhPGScLx9teSl9zo84QO+IDeZV5Mwmk5+v+q4+w/zn87W5EngsGo+WNUqi8ovlev" +
                "OPHtyW3mq1bDX3711rwLpCs/JbgGqMSFa0dtYiWZT6y+/tLjDzEj9RXUBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
