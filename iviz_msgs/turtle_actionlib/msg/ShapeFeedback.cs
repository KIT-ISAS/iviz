/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract (Name = "turtle_actionlib/ShapeFeedback")]
    public sealed class ShapeFeedback : IDeserializable<ShapeFeedback>, IFeedback<ShapeActionFeedback>
    {
        //feedback
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeFeedback()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ShapeFeedback(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeFeedback(ref b);
        }
        
        ShapeFeedback IDeserializable<ShapeFeedback>.RosDeserialize(ref Buffer b)
        {
            return new ShapeFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
    }
}
