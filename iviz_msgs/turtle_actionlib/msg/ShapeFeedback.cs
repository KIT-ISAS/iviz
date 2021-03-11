/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeFeedback")]
    public sealed class ShapeFeedback : IDeserializable<ShapeFeedback>, IFeedback<ShapeActionFeedback>
    {
        //feedback
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeFeedback()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeFeedback(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        ShapeFeedback IDeserializable<ShapeFeedback>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly ShapeFeedback Singleton = new ShapeFeedback();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
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
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuPi5QIAl8utpwMAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
