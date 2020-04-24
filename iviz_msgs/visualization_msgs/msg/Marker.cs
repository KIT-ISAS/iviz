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
    
        public IMessage Create() => new Marker();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "visualization_msgs/Marker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "4048c9de2a16f4ae8e0538085ebf1b97";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAE71X227bOBB9rr5igKBosnDkXLrdbgA/pLGbGMhtbbfdoigMWqJtNrKoklRc9+t7hpTk" +
                "uEnafdg2MWKK4sycuZ1htmgoJc2dK47a7eVyGRttY21m7aW6UW1zq762u8oWmViNVoW07QthbqQhkac/" +
                "FhqVThslslrCPj08fnqw90pYleB7OBfQRlNtaKGNJJVjuRBO6ZzwKa3KZ+TmytJCWitmkpbKzYlVR1Gp" +
                "cveSjgeDq3edverp5M2rXme/ehhen/UGvc5B/e79ef+y2xt0DqsNPPbGw9Ggf915fnfrvD8cdf68ozHs" +
                "vNhQG/b+qvaur/qXo2HnZfU46v07Gr/t996NXx+f9C9PO39XLy56w7PxoDe8ejM4AdAaNjAcX56eV0r3" +
                "9xvnut3GtYurbv/1++ax2zvvjdbOhcfj83N4F51JkSI78/D1yM9W/Z6j79RCtqdGLDZyEFlnOAO5fUxJ" +
                "UHQJOVuIRJLThCLhBSdNTz7JxEFjHMdIpkyxpETnn8o88Tn22VQpiyVGCidJUJmrz6WknLF4aHNZKYrg" +
                "6eEBn3/y5AdoKqv9LpucltmDRllr3sD2BShyVZQZHIfHXNipzKQLBVgjILyWpsLh0AmPI9ki7hTS003w" +
                "ImB4XGqPRJq2FzpV0xVgVNIt2qftVBZGJkCQ7rToIOBD99w5dLjezLJq10YzqRfSmdV4YWe2fa2tpIL/" +
                "3Dfu3wHynZhvCr/FljaHZBORye+Eh35vQ5r2W/hF8wqUUCqnoswcbZe2BLwV8QuEk+znUhi5g2pLg5UT" +
                "nWkzOH11jLxh9Z0d/5Y+7MV7u/vx3scoLU1gjExNJRfyw4E900vK9GY67VyXWYqsWkcTOWUKmkiff/AW" +
                "90DigYagpjEhO8EXPnqLSphonZHvm3GmkxtU+IO2+9OKxQJtVmYnMojuBtEWqVjGZBBtmOAm9A2D3lDO" +
                "hpPERle+W62TRRRtXeXAF1orBN5XpS1koqYKu3NhySKDfKZupwVty1lccVbrDg22CI26c69egOHDR5QM" +
                "vu2vsbiVl4sJAoPi8RlHpEqkRKJTsYs47REUSdRJxlzhm7eRqIFdXo16Ryj8Yi4Isc61o5V0HuoDpQWP" +
                "gikEkYKobjzzsOUXVyXM1kTIe4+d36D27wWRr/nYSKtLk8hQNX4L0mMJR9JUAiCTCw/LKOr8zz/RxfD0" +
                "iJoohAEBR4YOTCdMyp0oUuGEd2WuZoj7boZqyyAkFgV89G851zaG4IjLGZ+ZzKXxTeIDwUSuFwtwOPNU" +
                "Vah35CEJNhZUCIPmAtsanNcmVTkf9zXO2vGxyLbMwc397hHTt5VJ6RQAraCBh4W/G4Dk/QAEs0Ig2hot" +
                "9S4e5UyatXHUi3AMVn4BgVrGKewRbPwRnIuhG8GRsJJa2vZ7YzzaHYIRQJCFTua0DeTXKzcH03AB3grk" +
                "agLGg2KmCWh9xkLPdu5ozr3qXOS6Vh80rm38F7V5o5d92p0jZxl7b8sZAoiDhdG3CiVEk5VXkmRK5phW" +
                "amKEWUWeFL3JaOu15xHPRj4j+BbW6kTxXPGzsa7ZQGsq/VXVeH8swcFj8B8nCfCra+A0DCtEaWokEw0G" +
                "dourjLfT6r3yZ3lq47pZy8YUefJqDkT/lNxiude7Pve7HASUunNQC04oDBLOVoMfvohAZ5vuRtNMC/fi" +
                "OX1pVqtm9fX3wF+HrvahSVS4gqzjuQmenz6v485zLY5+4lG9Wv4e36pbzUOO0a1/t+lSzATV95TiZwDf" +
                "CRxzXyMJwVThshbKcAQ29bcLjHhHqZZ+OkHHQtxApUR/s7QoCigDyfL8z0Io/fTH9IxncYuWc5mHU/6O" +
                "wig8/6qEjJrxPbq+OTTCgirnWuSmB+hvvhcy5mAM5QclRofE7cR8U1npkpbsEBamon3NQ7jG5enJad3y" +
                "Iz6oeKDWm//ZUOcOA+enWf81qb4/+oNJjAzTrGbNatKsRBR9A8qKJhkXDwAA";
                
    }
}
