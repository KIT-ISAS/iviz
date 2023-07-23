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
        [DataMember (Name = "hide_robot")] public bool HideRobot;
    
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
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            Id = b.DeserializeString();
            JointNames = b.DeserializeStringArray();
            {
                int n = b.DeserializeArrayLength();
                float[] array;
                if (n == 0) array = EmptyArray<float>.Value;
                else
                {
                    array = new float[n];
                    b.DeserializeStructArray(array);
                }
                JointValues = array;
            }
            RobotDescription = b.DeserializeString();
            SourceNode = b.DeserializeString();
            SourceParameter = b.DeserializeString();
            SavedRobotName = b.DeserializeString();
            b.Deserialize(out Tint);
            b.Deserialize(out Metallic);
            b.Deserialize(out Smoothness);
            b.Deserialize(out AttachedToTf);
            b.Deserialize(out RenderAsOcclusionOnly);
            b.Deserialize(out HideRobot);
        }
        
        public RobotPreview(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            b.Align4();
            Id = b.DeserializeString();
            b.Align4();
            JointNames = b.DeserializeStringArray();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                float[] array;
                if (n == 0) array = EmptyArray<float>.Value;
                else
                {
                    array = new float[n];
                    b.DeserializeStructArray(array);
                }
                JointValues = array;
            }
            RobotDescription = b.DeserializeString();
            b.Align4();
            SourceNode = b.DeserializeString();
            b.Align4();
            SourceParameter = b.DeserializeString();
            b.Align4();
            SavedRobotName = b.DeserializeString();
            b.Align4();
            b.Deserialize(out Tint);
            b.Deserialize(out Metallic);
            b.Deserialize(out Smoothness);
            b.Deserialize(out AttachedToTf);
            b.Deserialize(out RenderAsOcclusionOnly);
            b.Deserialize(out HideRobot);
        }
        
        public RobotPreview RosDeserialize(ref ReadBuffer b) => new RobotPreview(ref b);
        
        public RobotPreview RosDeserialize(ref ReadBuffer2 b) => new RobotPreview(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(JointNames.Length);
            b.SerializeArray(JointNames);
            b.Serialize(JointValues.Length);
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
            b.Serialize(HideRobot);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Align4();
            b.Serialize(Id);
            b.Align4();
            b.Serialize(JointNames.Length);
            b.SerializeArray(JointNames);
            b.Align4();
            b.Serialize(JointValues.Length);
            b.SerializeStructArray(JointValues);
            b.Serialize(RobotDescription);
            b.Align4();
            b.Serialize(SourceNode);
            b.Align4();
            b.Serialize(SourceParameter);
            b.Align4();
            b.Serialize(SavedRobotName);
            b.Align4();
            b.Serialize(in Tint);
            b.Serialize(Metallic);
            b.Serialize(Smoothness);
            b.Serialize(AttachedToTf);
            b.Serialize(RenderAsOcclusionOnly);
            b.Serialize(HideRobot);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            BuiltIns.ThrowIfNull(JointNames, nameof(JointNames));
            BuiltIns.ThrowIfNull(JointValues, nameof(JointValues));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 56;
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
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
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
            size += 1; // HideRobot
            return size;
        }
    
        public const string MessageType = "iviz_msgs/RobotPreview";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9e7dc3ba23162ce59180fc5747cce009";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTwW7bMAy96ysI5NB2QLq1uwwBesiargvQrkMa7FIUBi0xtgZFdCU5nf9+lN24QbHD" +
                "DpthwPIT+fjER7XWp08wv1wv774V88UCLuCDag/B1dXt3Y8rwc/+hM9vbmTrXKmYTLGNVXz/ldBQgLr/" +
                "qLJLBKiTZS8hwfoKrFEvy4dH+MnCWXjcUlQbx5g+no/oDl0r8D4vcMmpMBR1sM0hYeQ2aCo8G3oDNRiE" +
                "OYmOPY47MsXAlIu+yr5kx2F1/XkOSWrvtYBko3NWj0DcMqfaU4yqZHaAKaGuhTRxkTYDFsjL2QuMBWvt" +
                "2ihaC/auG3Zra2iQoC7+8aNu769n8MYKNYH7hN5gMP1xDCaEDYtFtqopTB3tyEkSbhsy0O+mrqF4Konr" +
                "2kaQtyJPQRrRQRslKDFo3m5bbzWKv8mKfYf5kmk9IEj/k9WtwyDxHIz1OXyTTcns8kZ6aslrguViJjE+" +
                "km6TFUGdMOhAGLNtywX0w5f7T08wgYcVx7NHNVk/81RwqmTiRhWQakxZNf1qghglqjDOpNi74ZSnUkS6" +
                "RFLORDjusUJ+4wlINdFCDesajuUI37tUsxdCgh0Gi6WjTKylFcJ6lJOOTg6YfU/t0fOefmB8rfE3tH7k" +
                "zWea1mKe66e3raSTEtgE3skUGSi7nkQ7Sz6Bs2XA0KmcNZRUky+52RIkWb018sUYWVtxwsCzTfX+bvS2" +
                "FPl2/uexHK/aeKnCuKrGVTmuUKnf2AtzAKcEAAA=";
                
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
