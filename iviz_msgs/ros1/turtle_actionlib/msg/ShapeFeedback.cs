/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract]
    public sealed class ShapeFeedback : IDeserializable<ShapeFeedback>, IHasSerializer<ShapeFeedback>, IMessage, IFeedback<ShapeActionFeedback>
    {
        //feedback
    
        public ShapeFeedback()
        {
        }
        
        public ShapeFeedback(ref ReadBuffer b)
        {
        }
        
        public ShapeFeedback(ref ReadBuffer2 b)
        {
        }
        
        public ShapeFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public ShapeFeedback RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static ShapeFeedback? singleton;
        public static ShapeFeedback Singleton => singleton ??= new ShapeFeedback();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public int AddRos2MessageLength(int c) => c;
    
        public const string MessageType = "turtle_actionlib/ShapeFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ShapeFeedback> CreateSerializer() => new Serializer();
        public Deserializer<ShapeFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ShapeFeedback>
        {
            public override void RosSerialize(ShapeFeedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ShapeFeedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ShapeFeedback msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ShapeFeedback msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<ShapeFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ShapeFeedback msg) => msg = new ShapeFeedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ShapeFeedback msg) => msg = new ShapeFeedback(ref b);
        }
    }
}
