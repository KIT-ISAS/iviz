/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexColorsStamped : IHasSerializer<MeshVertexColorsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_vertex_colors")] public MeshMsgs.MeshVertexColors MeshVertexColors;
    
        public MeshVertexColorsStamped()
        {
            Uuid = "";
            MeshVertexColors = new MeshMsgs.MeshVertexColors();
        }
        
        public MeshVertexColorsStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshVertexColors MeshVertexColors)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshVertexColors = MeshVertexColors;
        }
        
        public MeshVertexColorsStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            MeshVertexColors = new MeshMsgs.MeshVertexColors(ref b);
        }
        
        public MeshVertexColorsStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            Uuid = b.DeserializeString();
            MeshVertexColors = new MeshMsgs.MeshVertexColors(ref b);
        }
        
        public MeshVertexColorsStamped RosDeserialize(ref ReadBuffer b) => new MeshVertexColorsStamped(ref b);
        
        public MeshVertexColorsStamped RosDeserialize(ref ReadBuffer2 b) => new MeshVertexColorsStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshVertexColors.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Uuid);
            MeshVertexColors.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference(nameof(Uuid));
            if (MeshVertexColors is null) BuiltIns.ThrowNullReference(nameof(MeshVertexColors));
            MeshVertexColors.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Uuid);
                size += MeshVertexColors.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uuid);
            size = MeshVertexColors.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshVertexColorsStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9e3527729bbf26fabb162c58762b6739";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71Ty2ocMRC86ysa9mA7sA5Jbgs5+EEcHwzBNrkYs/RIvTMCjTTRY+35+5S0eGICITkk" +
                "GQQjtaqqn1rRjaSBznKOtitZ6jFxLyplsx1Tn95+FjYSaWg/mKP1PZVijRrBPGCqxleJWZ4vggsxUbva" +
                "N8tWN5NSH//yp27urjb0U5xqRXeZveFoEERmw5lpFxC/7QeJayd7cSDxOImhdpvnSdIpiPeDTYTVi5fI" +
                "zs1UEkA5kA7jWLzVjAJli+Re88G0npgmjtnq4jgCH6KxvsJ3kUep6lhJvhXxWuj6cgOMT6JLtghohoKO" +
                "wqnW9vqSVLE+f3hfCbSih9uQ3j2q1f1TWMMuPdqxREF54FyjlucponeIitMGzt4csjyFE1RJ4M4kOm62" +
                "LY7phOANscgU9EDHSOHLnIfgISi052i5c1KFNUoB1aNKOjp5peybtGcfXuQPij98/ImsX3RrTusBzXO1" +
                "DKn0qCSAUwx7awDt5iainRWfydkucpxVZR1cqtWnWmyAwGqtwZ9TCtqiE4aebB5eBri1ZYsh/kdj+eu3" +
                "gUR/9+Qa8Pbq/Ozhkf7vI1o8q50LXEcwLrt+2XXLjpX6DmravalCBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshVertexColorsStamped> CreateSerializer() => new Serializer();
        public Deserializer<MeshVertexColorsStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshVertexColorsStamped>
        {
            public override void RosSerialize(MeshVertexColorsStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshVertexColorsStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshVertexColorsStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshVertexColorsStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshVertexColorsStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshVertexColorsStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshVertexColorsStamped msg) => msg = new MeshVertexColorsStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshVertexColorsStamped msg) => msg = new MeshVertexColorsStamped(ref b);
        }
    }
}
