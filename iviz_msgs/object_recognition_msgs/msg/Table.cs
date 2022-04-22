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
        public Table(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            ConvexHull = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Table(ref b);
        
        public Table RosDeserialize(ref ReadBuffer b) => new Table(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
            b.SerializeStructArray(ConvexHull);
        }
        
        public void RosValidate()
        {
            if (ConvexHull is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 60 + Header.RosMessageLength + 24 * ConvexHull.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/Table";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "39efebc7d51e44bd2d72f2df6c7823a2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UwW4TMRC9+ytGyqEtokECxKGIA1IF9AAU0RtC1WR3smvYtbe2N8n263ljJ2lDD3CA" +
                "RpF27Z15b97zeGZ04ZY+9JFSy4mYho4dB0q86IRajrQQcVRLkipJTTmmsSvsdb7iZL0z5oNwLYHa/DBm" +
                "Rlet0OCj5MhIkx8BL5QCu6hshSzxTynffP5ceR9q6zgJxSkm6YHklyVTy3lKa5vavPbBNtZR9L2sWwlC" +
                "WOzjsgYhdnXeA8ot8cZGcqDmbkeXo0wjwEhhuu5jE59datFa+VaFImseNSOj+CSyLX3PVXsI/PT5imST" +
                "BIzLMeBj0LBcEnAq71ayoXbsutfYAiD+P8YIKyMtYTYeazlaQdYiSljBZpvmWxu3Su/78NAoBbQu2lq2" +
                "Ru4JVcdXSZo/eOsSCGGCdc3vgQcMD1xB5rfv2/DrgvvmH//Mx6/vzyimunCWntLqEw6SQ00oiGtOrAqo" +
                "tQ1cPu1kJR2SuB/gWv6apkFica843YiTwF030RgRhNOvfN+PzlZqYLK9HOQjE37jInBItho7nM89v5eB" +
                "e1F0/KPcjOIqoYvzM/UmSjUmNDyYrKuCcFSfL87JjPDvxXNNMLOrtT/FUhptkh156SoUK5shSNQ6OZ6B" +
                "40kRNwc2zBGw1JGO8941lvGEQIISZPBVS8eo/HJKrS+3YcXB5oYBcAUHgHqkSUcn95Bdhnbs/A6+IN5x" +
                "/A2s2+OqplM0f92p+jg2MBCBQ/Ar9GdNi6m0XmfFJersInCYjGYVSjN7px6Xi5JPBE+O0VeWdQLpDDAx" +
                "BUXPp3Ft6//VjQ+HAwS+pSB6SCg/z79yt2KeQcuACREHrjCs0GW6XW+/2xyrQwlXepc7J5Mv1z7AfBmh" +
                "MriMexf3WAJRyu7moBcSY6iUWbmrH1q4TJJDuWbZeU6vXtJm/zbt324fp/w763Ya9geFDjrw87B4Xd3c" +
                "+a4Tcm7+oGj3tjbmF2wrPuFEBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
