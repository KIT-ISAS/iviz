/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class SolidPrimitive : IDeserializable<SolidPrimitive>, IMessage
    {
        // Define box, sphere, cylinder, cone 
        // All shapes are defined to have their bounding boxes centered around 0,0,0.
        public const byte BOX = 1;
        public const byte SPHERE = 2;
        public const byte CYLINDER = 3;
        public const byte CONE = 4;
        // The type of the shape
        [DataMember (Name = "type")] public byte Type;
        // The dimensions of the shape
        [DataMember (Name = "dimensions")] public double[] Dimensions;
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
    
        /// Constructor for empty message.
        public SolidPrimitive()
        {
            Dimensions = System.Array.Empty<double>();
        }
        
        /// Explicit constructor.
        public SolidPrimitive(byte Type, double[] Dimensions)
        {
            this.Type = Type;
            this.Dimensions = Dimensions;
        }
        
        /// Constructor with buffer.
        internal SolidPrimitive(ref Buffer b)
        {
            Type = b.Deserialize<byte>();
            Dimensions = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SolidPrimitive(ref b);
        
        SolidPrimitive IDeserializable<SolidPrimitive>.RosDeserialize(ref Buffer b) => new SolidPrimitive(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Type);
            b.SerializeStructArray(Dimensions);
        }
        
        public void RosValidate()
        {
            if (Dimensions is null) throw new System.NullReferenceException(nameof(Dimensions));
        }
    
        public int RosMessageLength => 5 + 8 * Dimensions.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "shape_msgs/SolidPrimitive";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d8f8cbc74c5ff283fca29569ccefb45d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACnVTUWvbMBB+9684yENbZkK3lTEGfugabwmMdqQdJCkjqJYSCxzJSHJJ/v3uJNnWUorB" +
                "+Hx333f33d0EZmInlYAXfczBtrUwIofq1EjFhcEvjb5sArdNA7ZmrbDAjADukzg4DTV7FeBqIQ1idIpL" +
                "tScwDKyEcgjHMYMccJ3jM82yTir3Fb4/rIqP8fvx97xclsWnaN6tfy3uZ+Wy+Nz/eLgvi5sM63iqkezU" +
                "CtA7Ig01xSj6n/VBXB6EslIr+3/ortHMfbl5/ptE9DkHwRSVnyYkYd9AsKomSaxjykURrI8luY749sbF" +
                "mHOBzRt2IoYf2ngvNu5Lzb21ymGdA0N5NmnNJDK5G6H2ru4rqrQxwrbaq4yQVnLkj04UfTpqu10V14m1" +
                "HrQma4NSpyUF/WNVWjUnfBHbAalwiCAtdFbwUKd0sJevsW/DuOyoBETzmvkN6usIuNvl7Wzx5xHrSTn7" +
                "IXtMGrCnt0GVsDqAW4jSIryR9AM3qdG+cYrZADtKOwUanRE7HRXrcbfzcvFz/gSXhB2Nq7EnBEHdEsXH" +
                "nnCX97UbNI+3AJd0C1eBD7MHntBd5AlGwvMeSzYZtQvjY3bY6recdzQQUqp3YX5r5EE6D3h2ky8IIU3V" +
                "NcxMw8nIdtwhrynlaxwS7XvX4mRJWfgQRe2P9EzMYaXOmsflSi71TfAoDAX+A8JcXHlzBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
