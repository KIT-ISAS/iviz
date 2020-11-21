/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/Marker")]
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
            Header = new StdMsgs.Header();
            Ns = "";
            Points = System.Array.Empty<GeometryMsgs.Point>();
            Colors = System.Array.Empty<StdMsgs.ColorRGBA>();
            Text = "";
            MeshResource = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Marker(StdMsgs.Header Header, string Ns, int Id, int Type, int Action, in GeometryMsgs.Pose Pose, in GeometryMsgs.Vector3 Scale, in StdMsgs.ColorRGBA Color, duration Lifetime, bool FrameLocked, GeometryMsgs.Point[] Points, StdMsgs.ColorRGBA[] Colors, string Text, string MeshResource, bool MeshUseEmbeddedMaterials)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACr1XWW8TSRB+xpL/Q0kRIlk54xwsy0byg4mdxFKutQ0sQshqz7Q9TcbTQ3dPjPn1+1XP" +
                "YZskiIeFEJi+6q76qtihkZQUO5edtNvL5TIw2gbazNtLdafa5l59a/eUzRKxGq8yadtXwtxJQyKNfkw0" +
                "zp02SiQVhX1+3H1+dPBGWBXiO4oFuNFMG1poI0mlWC6EUzol/OZWpXNysbK0kNaKuaSlcjEx62aj2chV" +
                "6l5Tdzi8ed85qLanb9/0O4fVbnR70R/2O0f17YfLwXWvP+wcVyfY9yej8XBw23m5dXY5GI07f26yLY5e" +
                "bfMuDv+qDm9vBtfjUed1tR/3/x1P3g367ydn3dPB9Xnn7+rmqj+6mAz7o5u3w1NoXBsAVbrX55cl48PD" +
                "DUt7vbWdVze9wdmH9b7Xv+yPNywt9t3LSza12biQIkLE4uLzxM9Odc8RcWoh2zMjFltxaTasMxyW1D7F" +
                "peB0DUKbiVCS04TM4QVHUk8/y9CBZRAEiLCMsKRQp5/zNPSB9yFWEZOFRgonSVCeqi+5pJSV8brFsmTU" +
                "bMDa4yMmePbsB+qUYgc9ljnLk0elMtu01tunpUhVlicwHSZzukcykX6zVoFwLU2liEOBPK3KDnEBkZ59" +
                "p74otHia7IBEFLUXOlKzFRQpyVt0SLuRzIwMoUO016KjQkNU1caj4/VhkpSnttmYS72QzqwmCzu37Vtt" +
                "JWX8z0Pp/g5Kb/p9m/odzrQ5JhuKZJvDDo382RY5HbbwB2UtkEeRnIk8cbSb2xwKrmAUOCMN7ZdcGLnH" +
                "ORcVYk51os3w/E0XwcPqO0H+lj4eBAf7h8HBp2Yjyk2BJomaIWzInkede6GXlOjtoNpY50mE2FpHU4lc" +
                "kPj4LACmcS2EXtXCsVFAiFBhDT+953yYap2QL6BJosM7ZPqjwgezEuIKTC3lTmVBul+QtkgFMiADh0MG" +
                "V6MvHNSIchDpq5SlrnzZWiczLvqdmxQqFkVWeN9np81kqGYKp7GwZBFGflMV1oJ25Twogay1gY8tQsnu" +
                "PUwbqPHxEzIHX/urZO6k+WIK7yCJfODhrhyBkahanMJZBwROEvmSMHD4Qq4patWub8b9E9RAFguCx1Pt" +
                "aCWdV/axHINVhTDvSyqodW2e111+dWXkbA2NfPg0yRbuP6BF8OKJkVbnJpRlDvkz0E8kLIoiCT0Zcbiv" +
                "spjO//zTbFyNzk+o9kfROtickQMEChNxeYpIOOENitUcQdhPkH8JqMQig6X+liNvA6Ycc4bjdy5TaXzh" +
                "eH8wxuvFAvDO+FXm7gYDJgVQC8qEQcUBiA0ItIlUyu993nv+/Nci/DIFcA96J4ztVoa5U1BqBR7cSvw4" +
                "gQ5QtEigLihAOF7qfezlHNlSa4AUEuhSluRXoKtlZYU9YTF/FDYGYA8nSQiKLO36swm2do8gB1rITIcx" +
                "7UL925WLgUGclPcCYZsCDcGZAQRsXzDRi71N1qz6CTpRqiv+Bcu1kJ/hm64Zs1n7MYKXsAtsPocf8TIz" +
                "+l4hn2i68lzCRMkU/UxNjTCrZsMjphcKJmceZDxU+djgK6zVoeLO4/tnncQF6KnoF2bnw97FdnaBkBwu" +
                "WFFOkVz9wBl4a2Yw4vrO3uKk4+OovFf+Lbd3TKsVbYA88eBWv2g2/sm58FLPef3yN5oJdepyQmY4odBx" +
                "OHS1FbAI5eL13jK62ZglWrhXL+nreokYV8tvv82KtRNrU+qoFWPL2rXbNvDuyzoE3AZR/T9hWbVc/jYj" +
                "y3noUQvp3l9u28bphmHAY45vGDxNOEbImhSUkcKoV2TnGKDr5xLMBo4iLX1DYyYLcQemEvXP5CLLwA1g" +
                "zJMDT7IMGTw3oOcG86BFy1imxSs/3ng9PE6rkIya8yheDR01taDSwBa52RGqn+dK1rqQhoxkLkYXUdwL" +
                "eMpZ6ZyWbBMWpmwQmnt3pZkHMKd1y48GJY9HKqD+3yCS36E5/VwO/KKwPxwbSqnoL2ia1XK+Xk7XS8Ga" +
                "/wd6z7vbfQ8AAA==";
                
    }
}
