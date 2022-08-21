/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class SceneInclude : IDeserializable<SceneInclude>, IMessage
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
            b.DeserializeString(out Uri);
            Pose = new Matrix4(ref b);
            Material = new ModelMaterial(ref b);
            b.DeserializeString(out Package);
        }
        
        public SceneInclude(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Uri);
            Pose = new Matrix4(ref b);
            Material = new ModelMaterial(ref b);
            b.Align4();
            b.DeserializeString(out Package);
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
            b.Serialize(Uri);
            Pose.RosSerialize(ref b);
            Material.RosSerialize(ref b);
            b.Serialize(Package);
        }
        
        public void RosValidate()
        {
            if (Uri is null) BuiltIns.ThrowNullReference();
            if (Pose is null) BuiltIns.ThrowNullReference();
            Pose.RosValidate();
            if (Material is null) BuiltIns.ThrowNullReference();
            Material.RosValidate();
            if (Package is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 72;
                size += WriteBuffer.GetStringSize(Uri);
                size += Material.RosMessageLength;
                size += WriteBuffer.GetStringSize(Package);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Uri);
            c = WriteBuffer2.Align4(c);
            c += 64; // Pose
            c = Material.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Package);
            return c;
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
    }
}
