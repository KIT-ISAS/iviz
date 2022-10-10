/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class SceneInclude : IHasSerializer<SceneInclude>, IMessage
    {
        // Reference to an external asset
        /// <summary> Uri of the asset </summary>
        [DataMember (Name = "uri")] public string Uri;
        /// <summary> Pose of the asset </summary>
        [DataMember (Name = "pose")] public Matrix4 Pose;
        [DataMember (Name = "material")] public ModelMaterial Material;
        /// <summary> If uri has a model scheme, this indicates the package to search </summary>
        [DataMember (Name = "package")] public string Package;
    
        public SceneInclude()
        {
            Uri = "";
            Pose = new Matrix4();
            Material = new ModelMaterial();
            Package = "";
        }
        
        public SceneInclude(string Uri, Matrix4 Pose, ModelMaterial Material, string Package)
        {
            this.Uri = Uri;
            this.Pose = Pose;
            this.Material = Material;
            this.Package = Package;
        }
        
        public SceneInclude(ref ReadBuffer b)
        {
            Uri = b.DeserializeString();
            Pose = new Matrix4(ref b);
            Material = new ModelMaterial(ref b);
            Package = b.DeserializeString();
        }
        
        public SceneInclude(ref ReadBuffer2 b)
        {
            b.Align4();
            Uri = b.DeserializeString();
            Pose = new Matrix4(ref b);
            Material = new ModelMaterial(ref b);
            b.Align4();
            Package = b.DeserializeString();
        }
        
        public SceneInclude RosDeserialize(ref ReadBuffer b) => new SceneInclude(ref b);
        
        public SceneInclude RosDeserialize(ref ReadBuffer2 b) => new SceneInclude(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
            Pose.RosSerialize(ref b);
            Material.RosSerialize(ref b);
            b.Serialize(Package);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Uri);
            Pose.RosSerialize(ref b);
            Material.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Package);
        }
        
        public void RosValidate()
        {
            if (Uri is null) BuiltIns.ThrowNullReference(nameof(Uri));
            if (Pose is null) BuiltIns.ThrowNullReference(nameof(Pose));
            Pose.RosValidate();
            if (Material is null) BuiltIns.ThrowNullReference(nameof(Material));
            Material.RosValidate();
            if (Package is null) BuiltIns.ThrowNullReference(nameof(Package));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 72;
                size += WriteBuffer.GetStringSize(Uri);
                size += Material.RosMessageLength;
                size += WriteBuffer.GetStringSize(Package);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uri);
            size = WriteBuffer2.Align4(size);
            size += 64; // Pose
            size = Material.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Package);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/SceneInclude";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "89c6a6240009410a08d4bbcad467b364";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE72UyW7bMBCGz9FTEMi1QBtnaVIgB1mibaLaoCWpEQQCI1M2W2uBSDlJn74jiVLo5trG" +
                "B1P8Zjj8Z0jOKQpZzhpWZgzJCtESsRfJmpLuERWCScMQsuHlFrUNRycnpyiBscqR3DHl4FJweLlAdSVY" +
                "5xDA+JdHtWF7cGMNh7CF+hgD1zT7Rbf9UpL32+yoQBQV3Soksh0r2CeIxgXi5YZnsFz0wdXCTrZgtMl2" +
                "BjKM23/8M9xo+Q3xA/+dFmIrPqtsjXxfUXk+ezi7ekQFOkVN9QyZ/ayaj9Cg19NoeSmv0dzBnp3aeGEm" +
                "Toxu0Zcjbto2ickdBsPZdKIlLZhhVfuqOZ8hWjxxVsppvuF53oo3Oyu4EPzAxsRRBeXn8nWaP7VFnYqM" +
                "7iH0BMWOl7xkQrwnKahg5VbuJlPD8j3LJOQJYQf1T3tWbtLuJgyXKIbL2Tbs4RHJ4Uv8/2qrCihFjRq3" +
                "o0I10g86dlUBtWu8DnDq+R7WTrxnNlkskmg4bw1HAbYSxwyBz3RuunOCve7enOsYuySKhmtzofMVJstV" +
                "5315rCN0TScCfHW054p4xMNRZ/iqG/zAtEi8Bnx9LD0KHNPC7iDoRrc53b6uGXR5HeUb4oWDrZj4Xmc6" +
                "yjnxvnv+fc9nhjJAiIB4y3QR+m6a3GnVGy1RsMKhXr/RYK0d4tlYL+Fomvs/tAqOFJLx9AqO/E3X5SjL" +
                "D1IXni8JnLUmCSi8X00KgCiZx6FpxZoKoDa5IzbWNHSeru/HKxXhQuNk6WFb8UnBfWgGafen7d8zyzHd" +
                "QNPQQ5eEoa9Xoqc2tkynF/HW4uGZgwM8cmjh7EV5y9d6vMgFreuucQxO7SEd/Kbm0veBnGayGh9gVbOG" +
                "Sl6Vav7c0LrvFGn7jhwM4w/igY0L6AYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<SceneInclude> CreateSerializer() => new Serializer();
        public Deserializer<SceneInclude> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<SceneInclude>
        {
            public override void RosSerialize(SceneInclude msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(SceneInclude msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(SceneInclude msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(SceneInclude msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(SceneInclude msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<SceneInclude>
        {
            public override void RosDeserialize(ref ReadBuffer b, out SceneInclude msg) => msg = new SceneInclude(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out SceneInclude msg) => msg = new SceneInclude(ref b);
        }
    }
}
