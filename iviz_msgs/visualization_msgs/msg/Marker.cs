/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header; // header for time/frame information
        [DataMember (Name = "ns")] public string Ns; // Namespace to place this object in... used in conjunction with id to create a unique name for the object
        [DataMember (Name = "id")] public int Id; // object ID useful in conjunction with the namespace for manipulating and deleting the object later
        [DataMember (Name = "type")] public int Type; // Type of object
        [DataMember (Name = "action")] public int Action; // 0 add/modify an object, 1 (deprecated), 2 deletes an object, 3 deletes all objects
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose; // Pose of the object
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale; // Scale of the object 1,1,1 means default (usually 1 meter square)
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color; // Color [0.0-1.0]
        [DataMember (Name = "lifetime")] public duration Lifetime; // How long the object should last before being automatically deleted.  0 means forever
        [DataMember (Name = "frame_locked")] public bool FrameLocked; // If this marker should be frame-locked, i.e. retransformed into its frame every timestep
        //Only used if the type specified has some use for them (eg. POINTS, LINE_STRIP, ...)
        [DataMember (Name = "points")] public GeometryMsgs.Point[] Points;
        //Only used if the type specified has some use for them (eg. POINTS, LINE_STRIP, ...)
        //number of colors must either be 0 or equal to the number of points
        //NOTE: alpha is not yet used
        [DataMember (Name = "colors")] public StdMsgs.ColorRGBA[] Colors;
        // NOTE: only used for text markers
        [DataMember (Name = "text")] public string Text;
        // NOTE: only used for MESH_RESOURCE markers
        [DataMember (Name = "mesh_resource")] public string MeshResource;
        [DataMember (Name = "mesh_use_embedded_materials")] public bool MeshUseEmbeddedMaterials;
    
        /// Constructor for empty message.
        public Marker()
        {
            Ns = string.Empty;
            Points = System.Array.Empty<GeometryMsgs.Point>();
            Colors = System.Array.Empty<StdMsgs.ColorRGBA>();
            Text = string.Empty;
            MeshResource = string.Empty;
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal Marker(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Ns = b.DeserializeString();
            Id = b.Deserialize<int>();
            Type = b.Deserialize<int>();
            Action = b.Deserialize<int>();
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            Lifetime = b.Deserialize<duration>();
            FrameLocked = b.Deserialize<bool>();
            Points = b.DeserializeStructArray<GeometryMsgs.Point>();
            Colors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
            Text = b.DeserializeString();
            MeshResource = b.DeserializeString();
            MeshUseEmbeddedMaterials = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Marker(ref b);
        
        Marker IDeserializable<Marker>.RosDeserialize(ref Buffer b) => new Marker(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Ns);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Action);
            b.Serialize(ref Pose);
            b.Serialize(ref Scale);
            b.Serialize(ref Color);
            b.Serialize(Lifetime);
            b.Serialize(FrameLocked);
            b.SerializeStructArray(Points);
            b.SerializeStructArray(Colors);
            b.Serialize(Text);
            b.Serialize(MeshResource);
            b.Serialize(MeshUseEmbeddedMaterials);
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
                size += BuiltIns.GetStringSize(Ns);
                size += 24 * Points.Length;
                size += 16 * Colors.Length;
                size += BuiltIns.GetStringSize(Text);
                size += BuiltIns.GetStringSize(MeshResource);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/Marker";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4048c9de2a16f4ae8e0538085ebf1b97";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1X227bOBB9Xn3FAEbRZOHIuXS73QB+SGM3MZDb2m67RVEYtERbbGVRJam47tfvGVKW" +
                "4yZp92HbxIgpijNz5naGadFISsqcK487neVyGRttY23mnaX6pDrmVn3t9JQtc7Ear0ppO5fCfJKGRJF+" +
                "X2hcOW2UyNcS9snRyZPD/ZfCqgTfo0xAG820oYU2klSB5UI4pQvCp7KqmJPLlKWFtFbMJS2Vy4hVR1Gl" +
                "CveCTobD67fd/frp9PXLfvegfhjdnPeH/e7h+t27i8FVrz/sHtUbeOxPRuPh4Kb77O7WxWA07v5xR2PY" +
                "eb6lNuz9We/dXA+uxqPui/px3P9nPHkz6L+dvDo5HVyddf+qX1z2R+eTYX90/Xp4CqBr2MBwcnV2USs9" +
                "OGic6/Ua1y6ve4NX75rHXv+iP944Fx5PLi7gXXQuRYrsZOHrkZ/W+j1H36mF7MyMWGzlILLOcAYK+5iS" +
                "oOgKcrYUiSSnCUXCC06ann6UiYPGOI6RTJliSYkuPlZF4nPss6lSFkuMFE6SoKpQnytJBWPx0DJZK4rg" +
                "6dEhn//tt++gqa0OemxyVuUPGmWtRQPbF6AoVFnlcBwec2GnMpf+YYOA8FqaGodDJzyOpEXcKaRn2+BF" +
                "wPC41D6JNO0sdKpmK8Copdt0QDupLI1MgCDdbdNhwIfuuXPoaLOZ5/WujeZSL6Qzq8nCzm3nRltJJf+5" +
                "b9y/A+Q7Md8WfoMtbY7IJiLfVtCikd/bkqaDNn7RvAIllMqZqHJHO5WtAG8Fl6AZBWg/V8LIXVRbGqyc" +
                "6lyb4dnLE+QNq2/s+Lf0fj/e3zuI9z9EaWUCY+RqhoShbB4M7LleUq6302kzXeUpsmodTSWqQOLL5x+8" +
                "xT2QeKAhqGlMyE7whY/eohKmWufk+2aS6+QTKvxB24NZzWKBNmuzUxlE94Jom1QsYzKINkxwE/qGQW8o" +
                "B4u+OdnoynerdbKMotZ1AXyhtULgfVXaUiZqprCbCUsWGeQz63Za0I6cxzVnte/QYJvQqLv36gUY3n9A" +
                "yeDb/hyLraJaTBEYFI/POCJVISUSnYpdxGmfoEiiTnLmCt+8jcQa2NX1uH+Mwi8zQYh1oR2tpPNQHygt" +
                "eBRMIYgURHXjmYctv7g6YXZNhLz32Pktav9WEPnKJkZaXZlEhqrxW5CeSDiSphIAmVx4WEZR93/+iS5H" +
                "Z8fURCEMCDgycmA6YVLuRJEKJ7wrmZoj7ns5qi2HkFiU8NG/5VzbGIJjLmd85rKQxjeJDwQTuV4swOHM" +
                "U3Wh3pGHJNhYUCkMmgtsa3Bem1QVfNzXOGvHxyLbsgA3D3rHTN9WJpVTALSCBh4W/m4AkvcDEMwKgag1" +
                "Xuo9PMo5SqMxjnoRmEKW5BcQqGWcwh7Dxu/BuRi6ERwJK6mlHb83waPdJRgBBFnqJKMdIL9ZuQxMwwV4" +
                "K5CrKRgPipkmoPUpCz3dvaOZYR9j0BR6rT5o3Nj4L2qLRi/7tJchZzl7b6s5AoiDpdG3CiVE05VXkuRK" +
                "FphWamqEWUWeFL3JqPXK84hnI58RfAtrdaJ4rvjZuK7ZQGsq/VnVeH8swcET8B8nCfDrayA3OJgEUZoZ" +
                "3FH9wG5zlfF2Wr9X/ixPbVw317IxRZ68mgPR3xW3WOH1bs79KgcBZd05qAUnFAYJZ6vBD1/QGh7ylrvR" +
                "LNfCPX9GX5rVqll9/TXwN6Fb+9AkKlxBNvHcBs9Pnzdx57kWRz/waL1a/hrf6lvNQ47RrX+37RIKCzPd" +
                "U4qfAXwncMx9jSQEU4XLWijDMdjU3y4w4h2lWvrpBB0L8QkqJfqbpUVZQhlIluc/X0SZEnj6Y3rG87hN" +
                "y0wW4ZS/ozAKz78qIaPmfI9e3xwaYUG1c21ys0P0N98LGXMwhvKDEqND4nZjvqmsdEVLdggLU9O+5iG8" +
                "xuXpyWnd9iM+qHig1pv/2VDnDgPnh1n/Oam+P/qDSYwM06zmzWrarEQU/QvKiiYZFw8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
