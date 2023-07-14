/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class TFMessage : IHasSerializer<TFMessage>, IMessage
    {
        [DataMember (Name = "transforms")] public GeometryMsgs.TransformStamped[] Transforms;
    
        public TFMessage()
        {
            Transforms = EmptyArray<GeometryMsgs.TransformStamped>.Value;
        }
        
        public TFMessage(GeometryMsgs.TransformStamped[] Transforms)
        {
            this.Transforms = Transforms;
        }
        
        public TFMessage(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.TransformStamped[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.TransformStamped>.Value;
                else
                {
                    array = new GeometryMsgs.TransformStamped[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new GeometryMsgs.TransformStamped(ref b);
                    }
                }
                Transforms = array;
            }
        }
        
        public TFMessage(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                GeometryMsgs.TransformStamped[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.TransformStamped>.Value;
                else
                {
                    array = new GeometryMsgs.TransformStamped[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new GeometryMsgs.TransformStamped(ref b);
                    }
                }
                Transforms = array;
            }
        }
        
        public TFMessage RosDeserialize(ref ReadBuffer b) => new TFMessage(ref b);
        
        public TFMessage RosDeserialize(ref ReadBuffer2 b) => new TFMessage(ref b);
    
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
            b.Align4();
            b.Serialize(Transforms.Length);
            foreach (var t in Transforms)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Transforms, nameof(Transforms));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Transforms) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Transforms.Length
            foreach (var msg in Transforms) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "tf2_msgs/TFMessage";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "94810edda583a504dfda3829e70d7eec";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
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
    
        public Serializer<TFMessage> CreateSerializer() => new Serializer();
        public Deserializer<TFMessage> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TFMessage>
        {
            public override void RosSerialize(TFMessage msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TFMessage msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TFMessage msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TFMessage msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(TFMessage msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TFMessage>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TFMessage msg) => msg = new TFMessage(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TFMessage msg) => msg = new TFMessage(ref b);
        }
    }
}
