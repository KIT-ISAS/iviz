/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.UniqueIdentifierMsgs
{
    [DataContract]
    public sealed class UUID : IDeserializable<UUID>, IHasSerializer<UUID>, IMessage
    {
        // A universally unique identifier (UUID).
        //
        //  http://en.wikipedia.org/wiki/Universally_unique_identifier
        //  http://tools.ietf.org/html/rfc4122.html
        [DataMember (Name = "uuid")] public byte[/*16*/] Uuid;
    
        public UUID()
        {
            Uuid = new byte[16];
        }
        
        public UUID(byte[] Uuid)
        {
            this.Uuid = Uuid;
        }
        
        public UUID(ref ReadBuffer b)
        {
            unsafe
            {
                var array = new byte[16];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 16 * 1);
                Uuid = array;
            }
        }
        
        public UUID(ref ReadBuffer2 b)
        {
            unsafe
            {
                var array = new byte[16];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 16 * 1);
                Uuid = array;
            }
        }
        
        public UUID RosDeserialize(ref ReadBuffer b) => new UUID(ref b);
        
        public UUID RosDeserialize(ref ReadBuffer2 b) => new UUID(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Uuid, 16);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Uuid, 16);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (Uuid.Length != 16) BuiltIns.ThrowInvalidSizeForFixedArray(Uuid.Length, 16);
        }
    
        public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 16;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c + Ros2FixedMessageLength;
        
    
        public const string MessageType = "unique_identifier_msgs/UUID";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "fec2a93b6f5367ee8112c9c0b41ff310";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE02MsQrCMBBA9/uKgyy6JLSIiJvg0g/IJFKKudjDmNT0ovj3Eh3q9t7wnsIDlshPyvMQ" +
                "wrvyoxCyoyjsmTKurO2Oaw0KFOIoMu2NoahffOOJHA865aupZuzy6X+ffvn81ZJSmDWT+G87yj2Y7C+b" +
                "pm11FYDCUXanZnvGUtgBfACu5uuRpgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<UUID> CreateSerializer() => new Serializer();
        public Deserializer<UUID> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<UUID>
        {
            public override void RosSerialize(UUID msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(UUID msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(UUID msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(UUID msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<UUID>
        {
            public override void RosDeserialize(ref ReadBuffer b, out UUID msg) => msg = new UUID(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out UUID msg) => msg = new UUID(ref b);
        }
    }
}
