/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeResult : IDeserializable<ShapeResult>, IResult<ShapeActionResult>
    {
        //result definition
        [DataMember (Name = "interior_angle")] public float InteriorAngle;
        [DataMember (Name = "apothem")] public float Apothem;
    
        /// Constructor for empty message.
        public ShapeResult()
        {
        }
        
        /// Explicit constructor.
        public ShapeResult(float InteriorAngle, float Apothem)
        {
            this.InteriorAngle = InteriorAngle;
            this.Apothem = Apothem;
        }
        
        /// Constructor with buffer.
        internal ShapeResult(ref Buffer b)
        {
            InteriorAngle = b.Deserialize<float>();
            Apothem = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ShapeResult(ref b);
        
        ShapeResult IDeserializable<ShapeResult>.RosDeserialize(ref Buffer b) => new ShapeResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(InteriorAngle);
            b.Serialize(Apothem);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b06c6e2225f820dbc644270387cd1a7c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuNKy8lPLDE2UsjMK0ktyswvik/MS89J5YIJJxbkl2Sk5nIBAEOeaCAoAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
