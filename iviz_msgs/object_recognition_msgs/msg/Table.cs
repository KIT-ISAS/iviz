/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Table : IDeserializable<Table>, IMessage
    {
        // Informs that a planar table has been detected at a given location
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The pose gives you the transform that take you to the coordinate system
        // of the table, with the origin somewhere in the table plane and the 
        // z axis normal to the plane
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // There is no guarantee that the table does NOT extend further than the
        // convex hull; this is just as far as we've observed it.
        // The origin of the table coordinate system is inside the convex hull
        // Set of points forming the convex hull of the table
        [DataMember (Name = "convex_hull")] public GeometryMsgs.Point[] ConvexHull;
    
        /// Constructor for empty message.
        public Table()
        {
            ConvexHull = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// Explicit constructor.
        public Table(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, GeometryMsgs.Point[] ConvexHull)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.ConvexHull = ConvexHull;
        }
        
        /// Constructor with buffer.
        internal Table(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            ConvexHull = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Table(ref b);
        
        Table IDeserializable<Table>.RosDeserialize(ref Buffer b) => new Table(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Pose);
            b.SerializeStructArray(ConvexHull);
        }
        
        public void RosValidate()
        {
            if (ConvexHull is null) throw new System.NullReferenceException(nameof(ConvexHull));
        }
    
        public int RosMessageLength => 60 + Header.RosMessageLength + 24 * ConvexHull.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/Table";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "39efebc7d51e44bd2d72f2df6c7823a2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW8TMRC9+1eMlENbRIMEiEMQB6QK6AEoojeEosl6smvYtbe2N8n21/Ns55Me4ACN" +
                "Iu3aO/PmvefxTOjaLp3vAsWGIzH1LVv2FHnRCjUcaCFiSUuUKoqmHFObFfZaV3E0zir1QViLpyY/lJrQ" +
                "bSPUuyA5MtDoBsALRc82pGqlWOSfUr65/LlyzmtjOQqFMUTpgOSWJTPReUprE5u8dt7UxlJwnawb8UJY" +
                "7OOyBiG2Ou8B5Z54YwJZlOZ2Vy5HqVqAEf0470Idnt0k0on5VkVCTnlUDwzyUWRLfV9LOwj89PmWZBMF" +
                "FZeDx0cY2HCmBJzK2ZVsqBna9jW2AIj/jyHAykBLmI3HWs5WkLUI4lew2cTp1sat0mMfHhqVAI0NRid6" +
                "clww6fgqMeX3ztiIgjDB2Pr3wJMKD1xB5rfv2/B5wX3zj3/q49f3MwpRl5qlpxL7iINkrwmEWHPkpIAa" +
                "U8Ply1ZW0iKJux6u5a9x7CUU94rTtVjx3LYjDQFBOP3Kdd1gDdoXek0nJ/nIhN+4COyjqYYW53Pk99Jz" +
                "Jwkd/yB3g9hK6PpqlrwJUg0RDY9KxlZeOCSfr69IDfDvxfOUoCa3a3eJpdSpSXbFS1eBrGx6LyHx5DBD" +
                "jSdF3BTYMEdQRQc6z3tzLMMFoQgoSO+qhs7B/GaMjSu3YcXe5IYBcAUHgHqWks4ujpAT7RlZtm4HXxAP" +
                "Nf4G1u5xk6ZLNL9uk/ow1DAQgb13K/SnpsVYWq81YiO1ZuHZjypllZJq8i55XC5KPhE8OQRXGRyAzjNA" +
                "hegTej6NudH/qxsfDgcIfEte0iGBfp5/5W5hbsClpceECD1XGFbosrStt99Njk1DCVd6lzsllS/XPkB9" +
                "GaDS24x7iHssgaCyuznohcgYKmVW7vhDC65GpnwiVy1bx/HVS9rs38b92/3j0D9Yt9OwPyh00Imfp+TT" +
                "6u7ge5qQU/UHRbu3tVK/AGwrPuFEBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
