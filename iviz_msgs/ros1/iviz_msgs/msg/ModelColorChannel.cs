/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelColorChannel : IDeserializable<ModelColorChannel>, IMessage
    {
        [DataMember (Name = "colors")] public Color32[] Colors;
    
        public ModelColorChannel()
        {
            Colors = System.Array.Empty<Color32>();
        }
        
        public ModelColorChannel(Color32[] Colors)
        {
            this.Colors = Colors;
        }
        
        public ModelColorChannel(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Colors);
        }
        
        public ModelColorChannel(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Colors);
        }
        
        public ModelColorChannel RosDeserialize(ref ReadBuffer b) => new ModelColorChannel(ref b);
        
        public ModelColorChannel RosDeserialize(ref ReadBuffer2 b) => new ModelColorChannel(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Colors);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Colors);
        }
        
        public void RosValidate()
        {
            if (Colors is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Colors.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Colors length
            c += 4 * Colors.Length;
            return c;
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
    }
}
