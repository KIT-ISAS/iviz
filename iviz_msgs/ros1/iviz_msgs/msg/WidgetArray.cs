/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class WidgetArray : IHasSerializer<WidgetArray>, IMessage
    {
        [DataMember (Name = "widgets")] public Widget[] Widgets;
    
        public WidgetArray()
        {
            Widgets = EmptyArray<Widget>.Value;
        }
        
        public WidgetArray(Widget[] Widgets)
        {
            this.Widgets = Widgets;
        }
        
        public WidgetArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Widget>.Value
                    : new Widget[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Widget(ref b);
                }
                Widgets = array;
            }
        }
        
        public WidgetArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Widget>.Value
                    : new Widget[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Widget(ref b);
                }
                Widgets = array;
            }
        }
        
        public WidgetArray RosDeserialize(ref ReadBuffer b) => new WidgetArray(ref b);
        
        public WidgetArray RosDeserialize(ref ReadBuffer2 b) => new WidgetArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Widgets.Length);
            foreach (var t in Widgets)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Widgets.Length);
            foreach (var t in Widgets)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Widgets is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Widgets.Length; i++)
            {
                if (Widgets[i] is null) BuiltIns.ThrowNullReference(nameof(Widgets), i);
                Widgets[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Widgets) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Widgets.Length
            foreach (var msg in Widgets) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/WidgetArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7ede0cfb563479b8d3428c913c47d749";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UbU/bMBD+7l9xEh+AaXTjZQxV4kPWdl0noF0aDSGEKje5JpZSO9hOIfz6nZ02bTfQ" +
                "9mFQRc3lXp67e+7sa5GkaG/v4MELhjF2/p9/7HLcb4NYiKfJ3KTmw7XPxEoh7RkEnWgwvJoE3S6cw8dt" +
                "Zdi7HP7skf7wOX1wcUGmI7a0RTej3iQaDi+iwWgDyqvDYRS4yO5g3NmA87bxKBxc9ZeWo+ctx664461E" +
                "YfC914mG4c0y8uRlq4/+tGUPwn6P/noBWU43LaPheLBR6eeXbB7zjDFjk5rUb8gT1JD5F5tWFoHHVihJ" +
                "LlrIFERSa21VIEtRzdHqqo4dKYNQ0N8arqNypcP+lwBiJz1nMBgrmUxq+yxX3J6egIl5juuv2qVWLuuI" +
                "ebFZ1gplqX2l5fuNJrYDY8tlwnUCxANPuOUwU0SfSDPUBzkuMKcgPi8wAW91tJkWBUaZMEBPihI1z/MK" +
                "SkNOVhFT83kpRcwdy2KOW/EUKSRwKLi2Ii5zrslf6URI5z7TfI4OnR6D9yXKGGHQbZOPJIZKK6igihBi" +
                "jdw43gZd8LtxfOQCYAduQ2UO79hO9KAOSI8pbUNTBdiMW1c1PhYajSuYmzYle1d32aIk7eUsDOx53YQ+" +
                "zT5QNqoFCxVnsEctjCqbKUmACAuuBZ/m6IBpxDmh7rqg3f0NZOmhJZdqBV8jrnP8C6xscF1PBxkNL/fr" +
                "U6bEJDkWWi1EQq7TyoPEuUBpIRdTzXXFXFSdku18dWSTE0X50dCbG6NiQZNI6B602Wo5/VgmdHJeaS3/" +
                "PIbUYAAa3ZCofO7OBKiZP5xuf2YaqY2Cx/jerZtTJ0u78L7ECygtVrEtYCNF29A4sB8ldamlx137vVWD" +
                "VMrqCNEuWC6k8dNq6qde6Iz4krfaba6Ux0aqGunpbcpfU7fqoRkUbdAWn9vFu6/7Ne900cxb7C8draSH" +
                "V78Smxu9zkkXim6ktJGmjcQZ+wV0ohF7NQgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<WidgetArray> CreateSerializer() => new Serializer();
        public Deserializer<WidgetArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<WidgetArray>
        {
            public override void RosSerialize(WidgetArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(WidgetArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(WidgetArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(WidgetArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(WidgetArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<WidgetArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out WidgetArray msg) => msg = new WidgetArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out WidgetArray msg) => msg = new WidgetArray(ref b);
        }
    }
}
