/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
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
        /// <summary> Header for time/frame information </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> Namespace to place this object in... used in conjunction with id to create a unique name for the object </summary>
        [DataMember (Name = "ns")] public string Ns;
        /// <summary> Object ID useful in conjunction with the namespace for manipulating and deleting the object later </summary>
        [DataMember (Name = "id")] public int Id;
        /// <summary> Type of object </summary>
        [DataMember (Name = "type")] public int Type;
        /// <summary> 0 add/modify an object, 1 (deprecated), 2 deletes an object, 3 deletes all objects </summary>
        [DataMember (Name = "action")] public int Action;
        /// <summary> Pose of the object </summary>
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        /// <summary> Scale of the object 1,1,1 means default (usually 1 meter square) </summary>
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale;
        /// <summary> Color [0.0-1.0] </summary>
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        /// <summary> How long the object should last before being automatically deleted.  0 means forever </summary>
        [DataMember (Name = "lifetime")] public duration Lifetime;
        /// <summary> If this marker should be frame-locked, i.e. retransformed into its frame every timestep </summary>
        [DataMember (Name = "frame_locked")] public bool FrameLocked;
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
    
        public Marker()
        {
            Ns = "";
            Points = System.Array.Empty<GeometryMsgs.Point>();
            Colors = System.Array.Empty<StdMsgs.ColorRGBA>();
            Text = "";
            MeshResource = "";
        }
        
        public Marker(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Ns);
            b.Deserialize(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out Action);
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            b.Deserialize(out Lifetime);
            b.Deserialize(out FrameLocked);
            b.DeserializeStructArray(out Points);
            b.DeserializeStructArray(out Colors);
            b.DeserializeString(out Text);
            b.DeserializeString(out MeshResource);
            b.Deserialize(out MeshUseEmbeddedMaterials);
        }
        
        public Marker(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Ns);
            b.Deserialize(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out Action);
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            b.Deserialize(out Lifetime);
            b.Deserialize(out FrameLocked);
            b.DeserializeStructArray(out Points);
            b.DeserializeStructArray(out Colors);
            b.DeserializeString(out Text);
            b.DeserializeString(out MeshResource);
            b.Deserialize(out MeshUseEmbeddedMaterials);
        }
        
        public Marker RosDeserialize(ref ReadBuffer b) => new Marker(ref b);
        
        public Marker RosDeserialize(ref ReadBuffer2 b) => new Marker(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Ns);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Action);
            b.Serialize(in Pose);
            b.Serialize(in Scale);
            b.Serialize(in Color);
            b.Serialize(Lifetime);
            b.Serialize(FrameLocked);
            b.SerializeStructArray(Points);
            b.SerializeStructArray(Colors);
            b.Serialize(Text);
            b.Serialize(MeshResource);
            b.Serialize(MeshUseEmbeddedMaterials);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Ns);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Action);
            b.Serialize(in Pose);
            b.Serialize(in Scale);
            b.Serialize(in Color);
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
            if (Ns is null) BuiltIns.ThrowNullReference();
            if (Points is null) BuiltIns.ThrowNullReference();
            if (Colors is null) BuiltIns.ThrowNullReference();
            if (Text is null) BuiltIns.ThrowNullReference();
            if (MeshResource is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 138;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Ns);
                size += 24 * Points.Length;
                size += 16 * Colors.Length;
                size += WriteBuffer.GetStringSize(Text);
                size += WriteBuffer.GetStringSize(MeshResource);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.AddLength(c, Ns);
            c = WriteBuffer2.Align4(c);
            c += 4;  // Id
            c += 4;  // Type
            c += 4;  // Action
            c = WriteBuffer2.Align8(c);
            c += 56;  // Pose
            c += 24;  // Scale
            c += 16;  // Color
            c += 8;  // Lifetime
            c += 1;  // FrameLocked
            c = WriteBuffer2.Align4(c);
            c += 4;  // Points length
            c = WriteBuffer2.Align8(c);
            c += 24 * Points.Length;
            c += 4;  // Colors length
            c += 16 * Colors.Length;
            c = WriteBuffer2.AddLength(c, Text);
            c = WriteBuffer2.AddLength(c, MeshResource);
            c += 1;  // MeshUseEmbeddedMaterials
            return c;
        }
    
        public const string MessageType = "visualization_msgs/Marker";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4048c9de2a16f4ae8e0538085ebf1b97";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
                "Vah35CEJNhZUCIPmAtsanNcmVTkf9zXO2vGxyLbMwc397hHTt5VJ6RQAraCBh4W/G4Dk/QAEs0IAnfZh" +
                "oO3+x2hrtNS72JczadYoUDjCMWr5BUxqGbCwRzD2R/AyhhFEScJcamnb743xaHcI1oBFFjqZ0zZcuF65" +
                "OSiHK/FWIGkTUB8UM19A6zMWerZzR3PuVeci17X6oHFt47+ozRu97NPuHMnLOAy2nCGSOFgYfatQSzRZ" +
                "eSVJpmSOsaUmRphV5NnRm4y2XntC8bTkU4NvYa1OFA8YPyTr4g38ptJfVZb35xMcPAYRcpIAv7oPTsPU" +
                "QpSmRjLjYHK3uNx4O63eK3+WxzfunbVsTJFnseZA9E/JvZZ7vetzv8tBQKlbCLXghMJE4Ww1+OGLCLy2" +
                "6W40zbRwL57Tl2a1alZffw/8dehqH5pEhbvIOp6b4Pnp8zruPODi6Cce1avl7/Gtut485Bjd+nebLsXM" +
                "VH1PKX4Y8OXAMQk2khBMFW5toQxHoFV/zcCsd5Rq6ccUdCzEDVRK9DdLi6KAMrAtXwSyEEp/DcAYjWdx" +
                "i5ZzmYdT/rLCKDwRq4SMmvGFur5CNMKCKuda5KYH6G++IDLmYAzlByVGh8TtxHxlWemSluwQFqbif83T" +
                "uMbl6clp3fKzPqh4oNabf95Q5w6T56dZ/zWpvn8HCCYxO0yzmjWrSbMSUfQNjzQLOCAPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
