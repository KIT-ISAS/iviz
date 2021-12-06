/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeFeedback : IDeserializable<ShapeFeedback>, IFeedback<ShapeActionFeedback>
    {
        //feedback
    
        /// Constructor for empty message.
        public ShapeFeedback()
        {
        }
        
        /// Constructor with buffer.
        internal ShapeFeedback(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public ShapeFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly ShapeFeedback Singleton = new ShapeFeedback();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = BuiltIns.EmptyMd5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
