/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class RobotPreview : IHasSerializer<RobotPreview>, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "joint_values")] public float[] JointValues;
        [DataMember (Name = "robot_description")] public string RobotDescription;
        [DataMember (Name = "source_node")] public string SourceNode;
        [DataMember (Name = "source_parameter")] public string SourceParameter;
        [DataMember (Name = "saved_robot_name")] public string SavedRobotName;
        [DataMember (Name = "tint")] public StdMsgs.ColorRGBA Tint;
        [DataMember (Name = "metallic")] public float Metallic;
        [DataMember (Name = "smoothness")] public float Smoothness;
        [DataMember (Name = "attached_to_tf")] public bool AttachedToTf;
        [DataMember (Name = "render_as_occlusion_only")] public bool RenderAsOcclusionOnly;
    
        public RobotPreview()
        {
            Id = "";
            JointNames = EmptyArray<string>.Value;
            JointValues = EmptyArray<float>.Value;
            RobotDescription = "";
            SourceNode = "";
            SourceParameter = "";
            SavedRobotName = "";
        }
        
        public RobotPreview(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.DeserializeString(out Id);
            b.DeserializeStringArray(out JointNames);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                JointValues = array;
            }
            b.DeserializeString(out RobotDescription);
            b.DeserializeString(out SourceNode);
            b.DeserializeString(out SourceParameter);
            b.DeserializeString(out SavedRobotName);
            b.Deserialize(out Tint);
            b.Deserialize(out Metallic);
            b.Deserialize(out Smoothness);
            b.Deserialize(out AttachedToTf);
            b.Deserialize(out RenderAsOcclusionOnly);
        }
        
        public RobotPreview(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.Align4();
            b.DeserializeString(out Id);
            b.Align4();
            b.DeserializeStringArray(out JointNames);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                JointValues = array;
            }
            b.DeserializeString(out RobotDescription);
            b.Align4();
            b.DeserializeString(out SourceNode);
            b.Align4();
            b.DeserializeString(out SourceParameter);
            b.Align4();
            b.DeserializeString(out SavedRobotName);
            b.Align4();
            b.Deserialize(out Tint);
            b.Deserialize(out Metallic);
            b.Deserialize(out Smoothness);
            b.Deserialize(out AttachedToTf);
            b.Deserialize(out RenderAsOcclusionOnly);
        }
        
        public RobotPreview RosDeserialize(ref ReadBuffer b) => new RobotPreview(ref b);
        
        public RobotPreview RosDeserialize(ref ReadBuffer2 b) => new RobotPreview(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.SerializeArray(JointNames);
            b.SerializeStructArray(JointValues);
            b.Serialize(RobotDescription);
            b.Serialize(SourceNode);
            b.Serialize(SourceParameter);
            b.Serialize(SavedRobotName);
            b.Serialize(in Tint);
            b.Serialize(Metallic);
            b.Serialize(Smoothness);
            b.Serialize(AttachedToTf);
            b.Serialize(RenderAsOcclusionOnly);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.SerializeArray(JointNames);
            b.SerializeStructArray(JointValues);
            b.Serialize(RobotDescription);
            b.Serialize(SourceNode);
            b.Serialize(SourceParameter);
            b.Serialize(SavedRobotName);
            b.Serialize(in Tint);
            b.Serialize(Metallic);
            b.Serialize(Smoothness);
            b.Serialize(AttachedToTf);
            b.Serialize(RenderAsOcclusionOnly);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (JointNames is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) BuiltIns.ThrowNullReference(nameof(JointNames), i);
            }
            if (JointValues is null) BuiltIns.ThrowNullReference();
            if (RobotDescription is null) BuiltIns.ThrowNullReference();
            if (SourceNode is null) BuiltIns.ThrowNullReference();
            if (SourceParameter is null) BuiltIns.ThrowNullReference();
            if (SavedRobotName is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 55;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetArraySize(JointNames);
                size += 4 * JointValues.Length;
                size += WriteBuffer.GetStringSize(RobotDescription);
                size += WriteBuffer.GetStringSize(SourceNode);
                size += WriteBuffer.GetStringSize(SourceParameter);
                size += WriteBuffer.GetStringSize(SavedRobotName);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Action
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, JointNames);
            size = WriteBuffer2.Align4(size);
            size += 4; // JointValues.Length
            size += 4 * JointValues.Length;
            size = WriteBuffer2.AddLength(size, RobotDescription);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, SourceNode);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, SourceParameter);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, SavedRobotName);
            size = WriteBuffer2.Align4(size);
            size += 16; // Tint
            size += 4; // Metallic
            size += 4; // Smoothness
            size += 1; // AttachedToTf
            size += 1; // RenderAsOcclusionOnly
            return size;
        }
    
        public const string MessageType = "iviz_msgs/RobotPreview";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ee7f5ef55ff746bca34ddf8e6ad97f9a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTwU7cMBC9+ytG2gNQCVropULisGUpRYJSAeoFVdHEnk1ceT3BnizN33ec7YYV6qGH" +
                "NrIU52XmzZt5du+jfID5+cPV7ZdqvljAGbwz/S54d3Fz++1C8eM/4fPra/11YkwWV61yk99+JnSUoB1f" +
                "ph6EAK14jhqSfGzAO/N7+/gdfrByVhFXlM0yMMr7kwldY+gV3uYlrlkqR9km3+0SZu6TpSqyo1dQh0mZ" +
                "RXVscVyTqzZMpeiL7HMOnO4uP85BtPZWC2g2huDtBOQVs7SRcjY1cwAUQdsqqXAlyw2WKGrvFeaKrQ19" +
                "Vq0VxzAYc/aPH3Nzf3kKr2ZvZnAvGB0mN+p3KAhLVk9801I6DLSmoEm46sjB+FeGjvKRJj60PoOuhiIl" +
                "7XyAPmuQMFherfroLaqh4tWv3XzN9BEQdODibR8waTwn52MJXxYXCruuTE89RUtwtTjVmJjJ9uJV0KAM" +
                "NhHm4tPVAsbTVgZOTzCDxzvOx9/N7OGZDxWnRo/YpAKkRSmq6WeX1BlVhflUi73ZdHmkRXRKpOVchv0R" +
                "q/QzH4BWUy3UsW1hX1v4OkjLUQkJ1pg81oEKsdVRKOteSdo72GGOI3XEyFv6DeNLjb+hjRNv6emwVfPC" +
                "eFz7RiepgV3itXcaWg8jiQ2eokDwdcI0mJK1KWlmn8qwNUizRmv0jTmz9eqEg2cv7fYyjLZU5Tr+52M5" +
                "3a3pFqVp10y7etqhMb8AZAoIe5gEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<RobotPreview> CreateSerializer() => new Serializer();
        public Deserializer<RobotPreview> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<RobotPreview>
        {
            public override void RosSerialize(RobotPreview msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(RobotPreview msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(RobotPreview msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(RobotPreview msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(RobotPreview msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<RobotPreview>
        {
            public override void RosDeserialize(ref ReadBuffer b, out RobotPreview msg) => msg = new RobotPreview(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out RobotPreview msg) => msg = new RobotPreview(ref b);
        }
    }
}
