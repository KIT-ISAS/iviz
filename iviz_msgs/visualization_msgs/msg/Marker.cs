/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/Marker")]
    public sealed class Marker : IDeserializable<Marker>, IMessage
    {
        // See http://www.ros.org/wiki/rviz/DisplayTypes/Marker and http://www.ros.org/wiki/rviz/Tutorials/Markers%3A%20Basic%20Shapes for more information on using this message with rviz
        public const byte ARROW = 0;
        public const byte CUBE = 1;
        public const byte SPHERE = 2;
        public const byte CYLINDER = 3;
        public const byte LINE_STRIP = 4;
        public const byte LINE_LIST = 5;
        public const byte CUBE_LIST = 6;
        public const byte SPHERE_LIST = 7;
        public const byte POINTS = 8;
        public const byte TEXT_VIEW_FACING = 9;
        public const byte MESH_RESOURCE = 10;
        public const byte TRIANGLE_LIST = 11;
        public const byte ADD = 0;
        public const byte MODIFY = 0;
        public const byte DELETE = 2;
        public const byte DELETEALL = 3;
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // header for time/frame information
        [DataMember (Name = "ns")] public string Ns { get; set; } // Namespace to place this object in... used in conjunction with id to create a unique name for the object
        [DataMember (Name = "id")] public int Id { get; set; } // object ID useful in conjunction with the namespace for manipulating and deleting the object later
        [DataMember (Name = "type")] public int Type { get; set; } // Type of object
        [DataMember (Name = "action")] public int Action { get; set; } // 0 add/modify an object, 1 (deprecated), 2 deletes an object, 3 deletes all objects
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; } // Pose of the object
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale { get; set; } // Scale of the object 1,1,1 means default (usually 1 meter square)
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color { get; set; } // Color [0.0-1.0]
        [DataMember (Name = "lifetime")] public duration Lifetime { get; set; } // How long the object should last before being automatically deleted.  0 means forever
        [DataMember (Name = "frame_locked")] public bool FrameLocked { get; set; } // If this marker should be frame-locked, i.e. retransformed into its frame every timestep
        //Only used if the type specified has some use for them (eg. POINTS, LINE_STRIP, ...)
        [DataMember (Name = "points")] public GeometryMsgs.Point[] Points { get; set; }
        //Only used if the type specified has some use for them (eg. POINTS, LINE_STRIP, ...)
        //number of colors must either be 0 or equal to the number of points
        //NOTE: alpha is not yet used
        [DataMember (Name = "colors")] public StdMsgs.ColorRGBA[] Colors { get; set; }
        // NOTE: only used for text markers
        [DataMember (Name = "text")] public string Text { get; set; }
        // NOTE: only used for MESH_RESOURCE markers
        [DataMember (Name = "mesh_resource")] public string MeshResource { get; set; }
        [DataMember (Name = "mesh_use_embedded_materials")] public bool MeshUseEmbeddedMaterials { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Marker()
        {
            Ns = string.Empty;
            Points = System.Array.Empty<GeometryMsgs.Point>();
            Colors = System.Array.Empty<StdMsgs.ColorRGBA>();
            Text = string.Empty;
            MeshResource = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Marker(in StdMsgs.Header Header, string Ns, int Id, int Type, int Action, in GeometryMsgs.Pose Pose, in GeometryMsgs.Vector3 Scale, in StdMsgs.ColorRGBA Color, duration Lifetime, bool FrameLocked, GeometryMsgs.Point[] Points, StdMsgs.ColorRGBA[] Colors, string Text, string MeshResource, bool MeshUseEmbeddedMaterials)
        {
            this.Header = Header;
            this.Ns = Ns;
            this.Id = Id;
            this.Type = Type;
            this.Action = Action;
            this.Pose = Pose;
            this.Scale = Scale;
            this.Color = Color;
            this.Lifetime = Lifetime;
            this.FrameLocked = FrameLocked;
            this.Points = Points;
            this.Colors = Colors;
            this.Text = Text;
            this.MeshResource = MeshResource;
            this.MeshUseEmbeddedMaterials = MeshUseEmbeddedMaterials;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Marker(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Ns = b.DeserializeString();
            Id = b.Deserialize<int>();
            Type = b.Deserialize<int>();
            Action = b.Deserialize<int>();
            Pose = new GeometryMsgs.Pose(ref b);
            Scale = new GeometryMsgs.Vector3(ref b);
            Color = new StdMsgs.ColorRGBA(ref b);
            Lifetime = b.Deserialize<duration>();
            FrameLocked = b.Deserialize<bool>();
            Points = b.DeserializeStructArray<GeometryMsgs.Point>();
            Colors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
            Text = b.DeserializeString();
            MeshResource = b.DeserializeString();
            MeshUseEmbeddedMaterials = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Marker(ref b);
        }
        
        Marker IDeserializable<Marker>.RosDeserialize(ref Buffer b)
        {
            return new Marker(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Ns);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Action);
            Pose.RosSerialize(ref b);
            Scale.RosSerialize(ref b);
            Color.RosSerialize(ref b);
            b.Serialize(Lifetime);
            b.Serialize(FrameLocked);
            b.SerializeStructArray(Points, 0);
            b.SerializeStructArray(Colors, 0);
            b.Serialize(Text);
            b.Serialize(MeshResource);
            b.Serialize(MeshUseEmbeddedMaterials);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Ns is null) throw new System.NullReferenceException(nameof(Ns));
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
            if (Colors is null) throw new System.NullReferenceException(nameof(Colors));
            if (Text is null) throw new System.NullReferenceException(nameof(Text));
            if (MeshResource is null) throw new System.NullReferenceException(nameof(MeshResource));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 138;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Ns);
                size += 24 * Points.Length;
                size += 16 * Colors.Length;
                size += BuiltIns.UTF8.GetByteCount(Text);
                size += BuiltIns.UTF8.GetByteCount(MeshResource);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/Marker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4048c9de2a16f4ae8e0538085ebf1b97";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1X224aSRB9zkj+h5JQtPYKD75ks1lLPDiG2Ei+LZBkoyhCzUwDHQ/Tk+4eE/L1e6p7" +
                "GCCxk33YxEamb3WvOlVu0EBKmjlXnLRai8UiNtrG2kxbC3WnWuZefWl1lC0ysRwuC2lbV8LcSUMiT79P" +
                "NCydNkpkKwr79Pj06dHBS2FVgu/BTIAbTbShuTaSVI7lXDilc8KntCqfkpspS3NprZhKWig3I2YdRaXK" +
                "3Qs67fdv3rYPqt3Z65fd9mG1GdxedPvd9tHq7t1l77rT7bePqwNsu6PBsN+7bT/bPLrsDYbtPzY4hpPn" +
                "W2zD2Z/V2e1N73o4aL+otsPuP8PRm1737ejV6Vnv+rz9V3Vx1R1cjPrdwc3r/hkUXakNHU6vzy8rpoeH" +
                "tXGdTm3a1U2n9+pdve10L7vDtXFhe3p5CeuiCylSRGcWvh75aazu2ftOzWVrYsR8KwaRdYYjkNvHmARG" +
                "16CzhUgkOU1IEl5w0PT4o0wcOMZxjGDKFEtKdP6xzBMfYx9NlTJZYqRwkgSVufpUSspZF6/aTFaMIlh6" +
                "fMTvnzz5jjaV1F6HRU7K7EGhzDWv1fYJKHJVlBkMh8Wc2KnMpN+sNSBcS1Pp4VAJj2vSIK4U0pNt5UXQ" +
                "4XGqAxJp2prrVE2WUKOibtIh7aayMDKBBulek46CfqiejUfH68Msq05tNJV6Lp1ZjuZ2alu32koq+M+3" +
                "wv0dVN7w+TbxGxxpc0w2Edk2gwYN/NkWNR028YviFUihVE5EmTnaLW0J9ZYwCZyRgPZTKYzcQ7alQcqZ" +
                "zrTpn788Rdyw+kqOv6X3B/HB/mF88CFKSxMQI1MTBAxp86BjL/SCMr0dTjvTZZYiqtbRWCILJL58/IFb" +
                "XAOJVzQ4NY0J0Qm28NN7ZMJY64x83Ywyndwhwx+U3ZtUKBZgsxI7loF0P5A2ScUyJgNvQwQXoS8Y1IZy" +
                "kOiLk4UufbVaJ4soatzk0C+UVnC8z0pbyERNFE5nwpJFBPnNqpzmtCuncYVZzQ0YbBIKde+bfIEO7z8g" +
                "ZfBtf47ERl7Ox3AMksdHHJ4qERKJSsUp/HRAYCSRJxljhS/emmKl2PXNsHuCxC9mguDrXDtaSudVfSC1" +
                "YFEQBSdSINW1ZV5t+dlVAbMrIOSzx95vQfvXhIjXbGSk1aVJZMgafwTqkYQhaSqhIIMLN8toJ2r/zz87" +
                "0dXg/IRqP4QWsQNbBg5gJ0zKxShS4YS3ZqamcP1+hoTLQCXmBcz0txxuG4NwyBmNz1Tm0vg68b5gLNfz" +
                "OWCcoarK1Q16UAKQBRXCoL4AuAbvtUlVzs99mjN3fCwCLnPAc69zwghuZVI6BYWW4MD9wo8HwHnfAwGu" +
                "IIgaw4Xex1ZOkR21cKSMQCOyJD8DQy3rKewJZPwejIvBG96RkJJa2vVnI2ztHkEIVJCFTma0C81vl24G" +
                "sOEcvBcI1xigB8aMFOD6GxP9trfBmdU+Qa/J9Yp94LiW8V/Y5jVftml/hphlbL0tp3AgHhZG3ytkEY2X" +
                "nkmSKZmjYamxEWYZeVz0IqPGKw8lHpB8RPAtrNWJ4tbi2+MqbQOyqfTnJeS3vYlz8hQoyHGCBdUwyGUO" +
                "PIGjJgaTqm/bTU40Pk6re+Xfcu/G0LmijSnyEFY/iP4uudByz3f97tfZCGV2VvWDjHBCoaNwzGoTYA4K" +
                "xGu9ZXE0ybRwz5/R53q1rFdffpUFa//VZtThCuPI2qvb+vPu09r73OPi6AdGrVaLX2VeNeM8aBvd+8tt" +
                "q5BhaPEeXnxL4BHBMQ7WlCBMFWa3kI9DIKsfNtDxHaVa+mYFHnNxB5YStc7UoijADIDL4wDPpQwPPAyg" +
                "mcbTuEmLmczDKz+ysBYei1VCRk15rF4NEjWxoMq6JrnJEWqdx0TWOQhDEoKJ0SF2ezEPLktd0oINwsJU" +
                "LUBzT17p5aHKad30HT+weCDl63/hkO0O3edHgf/5PbCeBXaCVHQQU6+m9WpcrwRS8F/MbPElKg8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
