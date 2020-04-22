
namespace Iviz.Msgs.visualization_msgs
{
    public sealed class Marker : IMessage
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
        
        public std_msgs.Header header; // header for time/frame information
        public string ns; // Namespace to place this object in... used in conjunction with id to create a unique name for the object
        public int id; // object ID useful in conjunction with the namespace for manipulating and deleting the object later
        public int type; // Type of object
        public int action; // 0 add/modify an object, 1 (deprecated), 2 deletes an object, 3 deletes all objects
        public geometry_msgs.Pose pose; // Pose of the object
        public geometry_msgs.Vector3 scale; // Scale of the object 1,1,1 means default (usually 1 meter square)
        public std_msgs.ColorRGBA color; // Color [0.0-1.0]
        public duration lifetime; // How long the object should last before being automatically deleted.  0 means forever
        public bool frame_locked; // If this marker should be frame-locked, i.e. retransformed into its frame every timestep
        
        //Only used if the type specified has some use for them (eg. POINTS, LINE_STRIP, ...)
        public geometry_msgs.Point[] points;
        //Only used if the type specified has some use for them (eg. POINTS, LINE_STRIP, ...)
        //number of colors must either be 0 or equal to the number of points
        //NOTE: alpha is not yet used
        public std_msgs.ColorRGBA[] colors;
        
        // NOTE: only used for text markers
        public string text;
        
        // NOTE: only used for MESH_RESOURCE markers
        public string mesh_resource;
        public bool mesh_use_embedded_materials;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/Marker";
    
        public IMessage Create() => new Marker();
    
        public int GetLength()
        {
            int size = 138;
            size += header.GetLength();
            size += ns.Length;
            size += 24 * points.Length;
            size += 16 * colors.Length;
            size += text.Length;
            size += mesh_resource.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Marker()
        {
            header = new std_msgs.Header();
            ns = "";
            points = System.Array.Empty<geometry_msgs.Point>();
            colors = System.Array.Empty<std_msgs.ColorRGBA>();
            text = "";
            mesh_resource = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out ns, ref ptr, end);
            BuiltIns.Deserialize(out id, ref ptr, end);
            BuiltIns.Deserialize(out type, ref ptr, end);
            BuiltIns.Deserialize(out action, ref ptr, end);
            pose.Deserialize(ref ptr, end);
            scale.Deserialize(ref ptr, end);
            color.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out lifetime, ref ptr, end);
            BuiltIns.Deserialize(out frame_locked, ref ptr, end);
            BuiltIns.DeserializeStructArray(out points, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out colors, ref ptr, end, 0);
            BuiltIns.Deserialize(out text, ref ptr, end);
            BuiltIns.Deserialize(out mesh_resource, ref ptr, end);
            BuiltIns.Deserialize(out mesh_use_embedded_materials, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(ns, ref ptr, end);
            BuiltIns.Serialize(id, ref ptr, end);
            BuiltIns.Serialize(type, ref ptr, end);
            BuiltIns.Serialize(action, ref ptr, end);
            pose.Serialize(ref ptr, end);
            scale.Serialize(ref ptr, end);
            color.Serialize(ref ptr, end);
            BuiltIns.Serialize(lifetime, ref ptr, end);
            BuiltIns.Serialize(frame_locked, ref ptr, end);
            BuiltIns.SerializeStructArray(points, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(colors, ref ptr, end, 0);
            BuiltIns.Serialize(text, ref ptr, end);
            BuiltIns.Serialize(mesh_resource, ref ptr, end);
            BuiltIns.Serialize(mesh_use_embedded_materials, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4048c9de2a16f4ae8e0538085ebf1b97";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
