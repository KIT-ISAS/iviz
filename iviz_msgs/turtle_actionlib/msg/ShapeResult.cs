/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeResult")]
    public sealed class ShapeResult : IDeserializable<ShapeResult>, IResult<ShapeActionResult>
    {
        //result definition
        [DataMember (Name = "interior_angle")] public float InteriorAngle { get; set; }
        [DataMember (Name = "apothem")] public float Apothem { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeResult()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeResult(float InteriorAngle, float Apothem)
        {
            this.InteriorAngle = InteriorAngle;
            this.Apothem = Apothem;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeResult(ref Buffer b)
        {
            InteriorAngle = b.Deserialize<float>();
            Apothem = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeResult(ref b);
        }
        
        ShapeResult IDeserializable<ShapeResult>.RosDeserialize(ref Buffer b)
        {
            return new ShapeResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(InteriorAngle);
            b.Serialize(Apothem);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b06c6e2225f820dbc644270387cd1a7c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuNKy8lPLDE2UsjMK0ktyswvik/MS89J5YIJJxbkl2Sk5vJyAQDg6GOSKQAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
