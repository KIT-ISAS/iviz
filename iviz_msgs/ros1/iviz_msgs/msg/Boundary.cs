/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Boundary : IHasSerializer<Boundary>, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        public const byte TYPE_SIMPLE = 0;
        public const byte TYPE_CIRCLE_HIGHLIGHT = 1;
        public const byte TYPE_SQUARE_HIGHLIGHT = 2;
        public const byte BEHAVIOR_NONE = 0;
        public const byte BEHAVIOR_COLLIDER = 1;
        public const byte BEHAVIOR_NOTIFY_COLLISION = 2;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "behavior")] public byte Behavior;
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale;
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        [DataMember (Name = "second_color")] public StdMsgs.ColorRGBA SecondColor;
        [DataMember (Name = "caption")] public string Caption;
    
        public Boundary()
        {
            Id = "";
            Caption = "";
        }
        
        public Boundary(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            Id = b.DeserializeString();
            b.Deserialize(out Type);
            b.Deserialize(out Behavior);
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            Caption = b.DeserializeString();
        }
        
        public Boundary(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            b.Align4();
            Id = b.DeserializeString();
            b.Deserialize(out Type);
            b.Deserialize(out Behavior);
            b.Align8();
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            Caption = b.DeserializeString();
        }
        
        public Boundary RosDeserialize(ref ReadBuffer b) => new Boundary(ref b);
        
        public Boundary RosDeserialize(ref ReadBuffer2 b) => new Boundary(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Behavior);
            b.Serialize(in Pose);
            b.Serialize(in Scale);
            b.Serialize(in Color);
            b.Serialize(in SecondColor);
            b.Serialize(Caption);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Align4();
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Behavior);
            b.Align8();
            b.Serialize(in Pose);
            b.Serialize(in Scale);
            b.Serialize(in Color);
            b.Serialize(in SecondColor);
            b.Serialize(Caption);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 123;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetStringSize(Caption);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Action
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size += 1; // Type
            size += 1; // Behavior
            size = WriteBuffer2.Align8(size);
            size += 56; // Pose
            size += 24; // Scale
            size += 16; // Color
            size += 16; // SecondColor
            size = WriteBuffer2.AddLength(size, Caption);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/Boundary";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "62b95500e71c4021f7c13dd16ceb7a04";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71V227bRhB951cM4IfYha02TlEEBvKgSIpNQLYUWTEQFIWwIkfkouQus7uUwn59zy4p" +
                "SkoMpA+NBV1GszNn58yNtVTuLQ1Hy3j2sBqOx/SOfovqY+Vicj97mkD/+jn9cDrF0XXUnS0/zyerx/h+" +
                "Pp0cIQXtKF6MppPVXXx7N8VneYTYen38NFycnvew7yd3w6d4tlg9zB6OgXv9aDadxuPJ4gj0yGcZf/jc" +
                "mjwi8hY4si5dlTazv96xSNlQHn46Z5E4qRVsjFQZybRTu6biTlxzLrZSmyhjXbIzTQs215apwtc3+idO" +
                "nDZvyCai4MPdI11os7h9P6TES88dWE60Slf78xBQIqoQX/Tuf35F94+3N/RNaqIzenRCpcKkBEoiFU7Q" +
                "RiNlMsvZXBW85QJOoqw4pXDqE2UHcFzm0hLeGSs2oigaqi2MnAbhsqyVTIRjcrLkE394SkWCKmGcTOpC" +
                "GNhrk0rlzTdGlOzR8bb8pWaVMMXjG9gopKt2EgE1QEgMC+sTFo8p1O3NtXegM/pzoe3rv6Kz5U5fQc8Z" +
                "OqCPglwunI+av1aGrQ9Y2Btc9kvLcoBLbrrCWDoPuhX+2gvCbYiFK53kdA4K88blWgGQaSuMFOuCPTC6" +
                "oADqK+/06uIIWQVoJZTew7eIhzv+C6zqcT2nqxzFK3wabJ0hkzCsjN7KFKbrJoAkhWTlqJBrI0wTea/2" +
                "yujsg082jOAVSoNfYa1OJCqR0k66fN+VoSwrDMtPasvvJw0Eh2TYFwnhCz8TpDdh/nz/bAyDRiUSvvTt" +
                "5tVpdy6DLfJC2si974CiuUY39AbRxxosjQq4B7uXIohQ9iOEXnBCKhuq1ccPLpiREPIJ3WhTaOH++J2+" +
                "9lLTS/+8TPiH1O059IVCB53k8zR4/+/LIe9YNOUg+gGjvbR7GW7dNn+OGG3D2Smlgd9UcVgpWmEzlSxQ" +
                "MizB3hOOqTSctG24xFplEEffSkepZktK+14oxd+AZMy39xZVBTBsWyOULdpUQg2Xcx5kg0va5axaKz+f" +
                "Ya2GRSwTMjKTaevpM9w7C+rIXZLbXGO+i6KNub0M7QcQo9vCXQwo3lCja9p5QhBMt/81HpB9XGE9Oa0v" +
                "/fLvIJ7pdaTFWpH5BrAOT54fVv3nlPr7Z3B7JZ4dppeyXlr3koiifwET9zhvTwkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Boundary> CreateSerializer() => new Serializer();
        public Deserializer<Boundary> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Boundary>
        {
            public override void RosSerialize(Boundary msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Boundary msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Boundary msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Boundary msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Boundary msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Boundary>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Boundary msg) => msg = new Boundary(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Boundary msg) => msg = new Boundary(ref b);
        }
    }
}
