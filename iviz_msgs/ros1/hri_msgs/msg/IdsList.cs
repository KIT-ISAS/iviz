/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class IdsList : IDeserializable<IdsList>, IMessage
    {
        // This message encodes a list of ROS4HRI IDs (eg face ids, body ids, person
        // ids...).
        // It is for instance published on /humans/faces/tracked to access the list of
        // currently detected faces.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "ids")] public string[] Ids;
    
        public IdsList()
        {
            Ids = System.Array.Empty<string>();
        }
        
        public IdsList(in StdMsgs.Header Header, string[] Ids)
        {
            this.Header = Header;
            this.Ids = Ids;
        }
        
        public IdsList(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStringArray(out Ids);
        }
        
        public IdsList(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeStringArray(out Ids);
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
    
        public int RosMessageLength => 4 + Header.RosMessageLength + WriteBuffer.GetArraySize(Ids);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.AddLength(c, Ids);
            return c;
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
    }
}
