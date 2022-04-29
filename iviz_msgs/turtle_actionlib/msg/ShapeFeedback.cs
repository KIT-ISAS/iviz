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
        public ShapeFeedback(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public ShapeFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static ShapeFeedback? singleton;
        public static ShapeFeedback Singleton => singleton ??= new ShapeFeedback();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = BuiltIns.EmptyMd5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
