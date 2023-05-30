/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelColorChannel : IHasSerializer<ModelColorChannel>, IMessage
    {
        [DataMember (Name = "colors")] public Color32[] Colors;
    
        public ModelColorChannel()
        {
            Colors = EmptyArray<Color32>.Value;
        }
        
        public ModelColorChannel(Color32[] Colors)
        {
            this.Colors = Colors;
        }
        
        public ModelColorChannel(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Color32[] array;
                if (n == 0) array = EmptyArray<Color32>.Value;
                else
                {
                    array = new Color32[n];
                    b.DeserializeStructArray(array);
                }
                Colors = array;
            }
        }
        
        public ModelColorChannel(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Color32[] array;
                if (n == 0) array = EmptyArray<Color32>.Value;
                else
                {
                    array = new Color32[n];
                    b.DeserializeStructArray(array);
                }
                Colors = array;
            }
        }
        
        public ModelColorChannel RosDeserialize(ref ReadBuffer b) => new ModelColorChannel(ref b);
        
        public ModelColorChannel RosDeserialize(ref ReadBuffer2 b) => new ModelColorChannel(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Colors.Length);
            b.SerializeStructArray(Colors);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Colors.Length);
            b.SerializeStructArray(Colors);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Colors, nameof(Colors));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 4 * Colors.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Colors.Length
            size += 4 * Colors.Length;
            return size;
        }
    
        public const string MessageType = "iviz_msgs/ModelColorChannel";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "04d8fd1feb40362aeedd2ef19014ebfa";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE3POz8kvMjaKjlVIBrGKubhsqQy4fIPdrRQyyzKr4nOL04v1nSE2cpVm5pVYKBRB6XQo" +
                "nQSlE7m4AEmfKA6bAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ModelColorChannel> CreateSerializer() => new Serializer();
        public Deserializer<ModelColorChannel> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ModelColorChannel>
        {
            public override void RosSerialize(ModelColorChannel msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ModelColorChannel msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ModelColorChannel msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ModelColorChannel msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(ModelColorChannel msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ModelColorChannel>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ModelColorChannel msg) => msg = new ModelColorChannel(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ModelColorChannel msg) => msg = new ModelColorChannel(ref b);
        }
    }
}
