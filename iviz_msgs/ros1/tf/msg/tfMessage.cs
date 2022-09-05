/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf
{
    [DataContract]
    public sealed class tfMessage : IDeserializable<tfMessage>, IHasSerializer<tfMessage>, IMessage
    {
        [DataMember (Name = "transforms")] public GeometryMsgs.TransformStamped[] Transforms;
    
        public tfMessage()
        {
            Transforms = EmptyArray<GeometryMsgs.TransformStamped>.Value;
        }
        
        public tfMessage(GeometryMsgs.TransformStamped[] Transforms)
        {
            this.Transforms = Transforms;
        }
        
        public tfMessage(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.TransformStamped>.Value
                    : new GeometryMsgs.TransformStamped[n];
                for (int i = 0; i < n; i++)
                {
                    GeometryMsgs.TransformStamped.Deserialize(ref b, out array[i]);
                }
                Transforms = array;
            }
        }
        
        public tfMessage(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.TransformStamped>.Value
                    : new GeometryMsgs.TransformStamped[n];
                for (int i = 0; i < n; i++)
                {
                    GeometryMsgs.TransformStamped.Deserialize(ref b, out array[i]);
                }
                Transforms = array;
            }
        }
        
        public tfMessage RosDeserialize(ref ReadBuffer b) => new tfMessage(ref b);
        
        public tfMessage RosDeserialize(ref ReadBuffer2 b) => new tfMessage(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Transforms.Length);
            foreach (var t in Transforms)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Transforms.Length);
            foreach (var t in Transforms)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Transforms is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Transforms);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Transforms.Length
            for (int i = 0; i < Transforms.Length; i++)
            {
                c = Transforms[i].AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "tf/tfMessage";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "94810edda583a504dfda3829e70d7eec";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/UMBC951eM6IEWtVnxIQ5V4YSAHpCAVlwQqqbJJLGa2MGedAm/nmdnkxRaAQfo" +
                "KtLajufNvHkzk1pcJ+rHiy7UYXPu2YbK+e5Mueul/PyFdD4KWfbiH/+yd2dvjqn+bQjZHp03JpB8672E" +
                "IIF4jYkq7zoqnPOlsayCPXdCjXApPk+bCxMh1JE2cvtm0Zi2vFgvzt46uOJaKC5d0HakIUhJl2OCwa0T" +
                "psZL9eJBo9ofbzZbc2Vy70LufL3R6sFLrU42/JJ6Lq4AlEebMwGgBipdMXRildU4S+ABHx6vbKSUDvMs" +
                "e5s47KhkQb2x9S/h0l6KZmKCrasmkvHSdJot2Vxz9r9kDFpOCk6RR77KtmRfIpvKJSsnro2pG/FHrVxL" +
                "C6MkMqW3OvYS8lkCPLVY8dzO2YeIheu6wZoiKqgGKt20h6WxKI+evZpiaNnfEjyi4wnydRBbCJ2+OsYd" +
                "G6QY1CCgEQiFFw4x26evKBuM1adPogGy/fmjC4+/ZHvnW3eEc6kh0BIFcs9KNyq1JA7HcPZoYpnDCbIk" +
                "cFcG2k9nF9iGA4I3xCK9KxraB4X3ozaojCjmNXvDl22qxAKpAOrDaPTw4AayTdCWrZvhJ8TVx9/A2gU3" +
                "cjpqIF4b0xCGGpnExd67a1OubVC0BlVMrbn07McsWk0us73XqSY16pikwT+H4AoDJUraGm3mkl56736n" +
                "y1xlXqJYoBESpXWyXIpuRZCtrbtVRSHWWeXRzgH9jaLKPkmhzj+d7NvUw9mHAQbexh73bmr2+yG5C+YO" +
                "ikzX6d0v8ceWOE216yxaoBOGrOi2xRKGpfEwjbMJqILRh5F1iHGGaYZ8WKfA6PgKkIJCitbc9wDjmzmJ" +
                "xzDZl7zOD2nbIL/pViyE1L+p401B3tQYaIsaizHTjtwhafUEhdS2U8yTM0gIkDnbBzmdVjS6gbaREBZ+" +
                "N2gc5F3iSn2gzh3GKbOD+Dmh7x26ff0m2KAYcVC9ah3r82f0bVmNy+r7vUi91thdalty3iwfmp80j7uv" +
                "a4HGJP+R0LzaZtkPSzp7rTcIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<tfMessage> CreateSerializer() => new Serializer();
        public Deserializer<tfMessage> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<tfMessage>
        {
            public override void RosSerialize(tfMessage msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(tfMessage msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(tfMessage msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(tfMessage msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<tfMessage>
        {
            public override void RosDeserialize(ref ReadBuffer b, out tfMessage msg) => msg = new tfMessage(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out tfMessage msg) => msg = new tfMessage(ref b);
        }
    }
}
