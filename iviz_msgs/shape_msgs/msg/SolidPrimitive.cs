/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [DataContract (Name = "shape_msgs/SolidPrimitive")]
    public sealed class SolidPrimitive : IDeserializable<SolidPrimitive>, IMessage
    {
        // Define box, sphere, cylinder, cone 
        // All shapes are defined to have their bounding boxes centered around 0,0,0.
        public const byte BOX = 1;
        public const byte SPHERE = 2;
        public const byte CYLINDER = 3;
        public const byte CONE = 4;
        // The type of the shape
        [DataMember (Name = "type")] public byte Type { get; set; }
        // The dimensions of the shape
        [DataMember (Name = "dimensions")] public double[] Dimensions { get; set; }
        // The meaning of the shape dimensions: each constant defines the index in the 'dimensions' array
        // For the BOX type, the X, Y, and Z dimensions are the length of the corresponding
        // sides of the box.
        public const byte BOX_X = 0;
        public const byte BOX_Y = 1;
        public const byte BOX_Z = 2;
        // For the SPHERE type, only one component is used, and it gives the radius of
        // the sphere.
        public const byte SPHERE_RADIUS = 0;
        // For the CYLINDER and CONE types, the center line is oriented along
        // the Z axis.  Therefore the CYLINDER_HEIGHT (CONE_HEIGHT) component
        // of dimensions gives the height of the cylinder (cone).  The
        // CYLINDER_RADIUS (CONE_RADIUS) component of dimensions gives the
        // radius of the base of the cylinder (cone).  Cone and cylinder
        // primitives are defined to be circular. The tip of the cone is
        // pointing up, along +Z axis.
        public const byte CYLINDER_HEIGHT = 0;
        public const byte CYLINDER_RADIUS = 1;
        public const byte CONE_HEIGHT = 0;
        public const byte CONE_RADIUS = 1;
    
        /// <summary> Constructor for empty message. </summary>
        public SolidPrimitive()
        {
            Dimensions = System.Array.Empty<double>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SolidPrimitive(byte Type, double[] Dimensions)
        {
            this.Type = Type;
            this.Dimensions = Dimensions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SolidPrimitive(ref Buffer b)
        {
            Type = b.Deserialize<byte>();
            Dimensions = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SolidPrimitive(ref b);
        }
        
        SolidPrimitive IDeserializable<SolidPrimitive>.RosDeserialize(ref Buffer b)
        {
            return new SolidPrimitive(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Type);
            b.SerializeStructArray(Dimensions, 0);
        }
        
        public void RosValidate()
        {
            if (Dimensions is null) throw new System.NullReferenceException(nameof(Dimensions));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += 8 * Dimensions.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "shape_msgs/SolidPrimitive";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d8f8cbc74c5ff283fca29569ccefb45d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACnWTUWvbMBDH3wP5Dgd5aMtM6LYyxsAPXeMtgdGOtIMkYwTVUmKBIxlJLsm3751kW2rK" +
                "CJic7+5/59/dTWAmdlIJeNbHDGxTCSMyKE+1VFwY/KfRNx5N4LauwVasERaYEcB9FgenoWIvAlwlpEGR" +
                "VnGp9qSGgaVQDvU4ZpADrjP8Tcej8aiVyn2F7w+r/GNvPP6eF8si/9Tbd+tfi/tZscw/D28e7ov8htIn" +
                "8FRhzVMjQO+odmitDyQHhfWBXB6EslIrexa+qzVzX27+/ktCYtpBMEUfk+Ykgd9AsLIiQtYx5Tok1scS" +
                "vSM+vXERcy4QhWGnUOOHNt6PGHzLmbdWGawzYIhrkzZO0MldC7V3Vd9TqY0RttGeOmlaybGFzotTmCas" +
                "t6v8OjXXET6ZG2L/trMwk645reoTPqjoASvibEFaaK3goV3pYC9fOgCGcdlSIyTn6fnVGtoJytvl7Wzx" +
                "55Haelu4H74Xprn7HmwgFNYKcEURNNYwkl7gltU6QKCgDbCjtFOgSRqx0x2+Xng7LxY/509wSeKdcRW/" +
                "jFQQYsI/fhpu+r5ywwS6U4FLOpWrUJDSh0rhG7tKwUgq/a/MGCUGiGGazA7r/r7qHU2GaPUuEmiMPEjn" +
                "Jc+O9hk1pCnbmplpOCbZxKXyYL2AxmnRDbQNDpn4woeObDzjM6Zxyc4Q0Lqlt/w+PhIKsa88TBSZngQA" +
                "AA==";
                
    }
}
