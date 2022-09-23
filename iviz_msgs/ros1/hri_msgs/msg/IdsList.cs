/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class IdsList : IHasSerializer<IdsList>, IMessage
    {
        // This message encodes a list of ROS4HRI IDs (eg face ids, body ids, person
        // ids...).
        // It is for instance published on /humans/faces/tracked to access the list of
        // currently detected faces.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "ids")] public string[] Ids;
    
        public IdsList()
        {
            Ids = EmptyArray<string>.Value;
        }
        
        public IdsList(in StdMsgs.Header Header, string[] Ids)
        {
            this.Header = Header;
            this.Ids = Ids;
        }
        
        public IdsList(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Ids = b.DeserializeStringArray();
        }
        
        public IdsList(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            Ids = b.DeserializeStringArray();
        }
        
        public IdsList RosDeserialize(ref ReadBuffer b) => new IdsList(ref b);
        
        public IdsList RosDeserialize(ref ReadBuffer2 b) => new IdsList(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Ids);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Ids);
        }
        
        public void RosValidate()
        {
            if (Ids is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Ids.Length; i++)
            {
                if (Ids[i] is null) BuiltIns.ThrowNullReference(nameof(Ids), i);
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetArraySize(Ids);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Ids);
            return size;
        }
    
        public const string MessageType = "hri_msgs/IdsList";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "84a63f55b5676f78b625e8a8bb809fe5";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61Sy27bMBC88ysW0CFxUcvo42Sgt6CJD0ULO7cgMNbkWiIikSqXcqq/71Bq0hx7qEBA" +
                "fOzOzs5sRfetV+pFlRshCTY6UWLqvGaKZ9p/P3y+2+9od6N0LQ2d2Qp5p+/pFN207AZJGoOpyqmu61WN" +
                "7S4TcM8xkQ+aOSBrGE9AbcVRDLRpx56DbgqebnJi+4SHHIktLpRyKy8cgGbHlCTkbiInWWxG6JxYmzth" +
                "J4na+Wc0Jx+ah8fCxJgv//kz3w63W9Lsjr02ullKg90B/TlODjJmdpx57rv1TStp3clFOiRxP4D1/Jqn" +
                "oVCvFu2xGgmSuEN7oy4q2Nj3Y/CWs1D2sOdtflE6wKOBU/Z27DghPibnQwk/J+6loGOp/BylaL+72SIm" +
                "qNgxexCCc8EmYYVceCQz+pA/fSwJVNHDPuqHR1PdP8c17qWBwq8sYA3P5sqvIcEqsGLdoti7pcsaRaCS" +
                "oJzDzMx3Rxx1RagGLjJE29I1Wvgx5RazULy+cPJ86qQAW0gB1KuSdLV6gxxm6MAhvsAviH9r/AtseMUt" +
                "Pa1bmNcVGXRsoCQChxQv3iH0NM0gtvMYPozjKXGaTMlaSprqaxEbQciarcGfVaP1XGb02ef2z0wuthy9" +
                "M+Y37aP+bnQDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<IdsList> CreateSerializer() => new Serializer();
        public Deserializer<IdsList> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<IdsList>
        {
            public override void RosSerialize(IdsList msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(IdsList msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(IdsList msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(IdsList msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(IdsList msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<IdsList>
        {
            public override void RosDeserialize(ref ReadBuffer b, out IdsList msg) => msg = new IdsList(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out IdsList msg) => msg = new IdsList(ref b);
        }
    }
}
