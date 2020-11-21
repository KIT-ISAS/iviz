/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Polygon")]
    public sealed class Polygon : IDeserializable<Polygon>, IMessage
    {
        //A specification of a polygon where the first and last points are assumed to be connected
        [DataMember (Name = "points")] public Point32[] Points { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Polygon()
        {
            Points = System.Array.Empty<Point32>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Polygon(Point32[] Points)
        {
            this.Points = Points;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Polygon(ref Buffer b)
        {
            Points = b.DeserializeStructArray<Point32>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Polygon(ref b);
        }
        
        Polygon IDeserializable<Polygon>.RosDeserialize(ref Buffer b)
        {
            return new Polygon(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Points, 0);
        }
        
        public void RosValidate()
        {
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 12 * Points.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Polygon";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cd60a26494a087f577976f0329fa120e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1RwUrEMBC9F/oPA3tRkBV2b4IH8SAeBEFvIpJNp+1gmgmZqWv9eidt2fUDzGmSvHnz" +
                "3pvNHUhCTy15p8QRuAUHicPU2eXYY0bQHqGlLAouNhCcFYkpqoCzXycyDtiAMhwQPMeIXrGpq+eC2e/e" +
                "3ld0XdXV7T+funp6ebiBDnlAzdPHIJ1cr4PragOvPUnRpI6izEYSC/01alCgCG1GtCScx4sjaQ/7HRzI" +
                "HBoqZctHrOVyWygfDS9gbzyY7cX4KAjz1CWxL8xljtAhoJGLomsK0ypsC1CITvpWrtgsG7AXo0yZB9bS" +
                "rpg5YXYHCqTT0nzqHVDEdQYTaFCoi4sgdZ8IY4Jg34utoiyC2BSKXWkPvNpbV6nA0eOV7bPkUaLyzmzN" +
                "Mc3C7wOPzTy+rtrAzpzA97mczuVPXf0CAf7SilgCAAA=";
                
    }
}
