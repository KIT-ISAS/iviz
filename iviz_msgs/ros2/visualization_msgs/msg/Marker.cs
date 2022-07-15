/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class Marker : IDeserializable<Marker>, IMessageRos2
    {
        // See:
        //  - http://www.ros.org/wiki/rviz/DisplayTypes/Marker
        //  - http://www.ros.org/wiki/rviz/Tutorials/Markers%3A%20Basic%20Shapes
        //
        // for more information on using this message with rviz.
        public const int ARROW = 0;
        public const int CUBE = 1;
        public const int SPHERE = 2;
        public const int CYLINDER = 3;
        public const int LINE_STRIP = 4;
        public const int LINE_LIST = 5;
        public const int CUBE_LIST = 6;
        public const int SPHERE_LIST = 7;
        public const int POINTS = 8;
        public const int TEXT_VIEW_FACING = 9;
        public const int MESH_RESOURCE = 10;
        public const int TRIANGLE_LIST = 11;
        public const int ADD = 0;
        public const int MODIFY = 0;
        public const int DELETE = 2;
        public const int DELETEALL = 3;
        // Header for timestamp and frame id.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Namespace in which to place the object.
        // Used in conjunction with id to create a unique name for the object.
        [DataMember (Name = "ns")] public string Ns;
        // Object ID used in conjunction with the namespace for manipulating and deleting the object later.
        [DataMember (Name = "id")] public int Id;
        // Type of object.
        [DataMember (Name = "type")] public int Type;
        // Action to take; one of:
        //  - 0 add/modify an object
        //  - 1 (deprecated)
        //  - 2 deletes an object
        //  - 3 deletes all objects
        [DataMember (Name = "action")] public int Action;
        // Pose of the object with respect the frame_id specified in the header.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // Scale of the object; 1,1,1 means default (usually 1 meter square).
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale;
        // Color of the object; in the range: [0.0-1.0]
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        // How long the object should last before being automatically deleted.
        // 0 indicates forever.
        [DataMember (Name = "lifetime")] public duration Lifetime;
        // If this marker should be frame-locked, i.e. retransformed into its frame every timestep.
        [DataMember (Name = "frame_locked")] public bool FrameLocked;
        // Only used if the type specified has some use for them (eg. POINTS, LINE_STRIP, etc.)
        [DataMember (Name = "points")] public GeometryMsgs.Point[] Points;
        // Only used if the type specified has some use for them (eg. POINTS, LINE_STRIP, etc.)
        // The number of colors provided must either be 0 or equal to the number of points provided.
        // NOTE: alpha is not yet used
        [DataMember (Name = "colors")] public StdMsgs.ColorRGBA[] Colors;
        // Only used for text markers
        [DataMember (Name = "text")] public string Text;
        // Only used for MESH_RESOURCE markers.
        [DataMember (Name = "mesh_resource")] public string MeshResource;
        [DataMember (Name = "mesh_use_embedded_materials")] public bool MeshUseEmbeddedMaterials;
    
        /// Constructor for empty message.
        public Marker()
        {
            Ns = "";
            Points = System.Array.Empty<GeometryMsgs.Point>();
            Colors = System.Array.Empty<StdMsgs.ColorRGBA>();
            Text = "";
            MeshResource = "";
        }
        
        /// Constructor with buffer.
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
        
        public Marker RosDeserialize(ref ReadBuffer2 b) => new Marker(ref b);
    
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
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, Ns);
            WriteBuffer2.Advance(ref c, Id);
            WriteBuffer2.Advance(ref c, Type);
            WriteBuffer2.Advance(ref c, Action);
            WriteBuffer2.Advance(ref c, Pose);
            WriteBuffer2.Advance(ref c, Scale);
            WriteBuffer2.Advance(ref c, Color);
            WriteBuffer2.Advance(ref c, Lifetime);
            WriteBuffer2.Advance(ref c, FrameLocked);
            WriteBuffer2.Advance(ref c, Points);
            WriteBuffer2.Advance(ref c, Colors);
            WriteBuffer2.Advance(ref c, Text);
            WriteBuffer2.Advance(ref c, MeshResource);
            WriteBuffer2.Advance(ref c, MeshUseEmbeddedMaterials);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/Marker";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
