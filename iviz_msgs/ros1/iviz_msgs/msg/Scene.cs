/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Scene : IDeserializable<Scene>, IMessage
    {
        // A complete scene 
        /// <summary> Name of the scene </summary>
        [DataMember (Name = "name")] public string Name;
        /// <summary> Original filename </summary>
        [DataMember (Name = "filename")] public string Filename;
        /// <summary> List of models to be included </summary>
        [DataMember (Name = "includes")] public Include[] Includes;
        /// <summary> List of lights </summary>
        [DataMember (Name = "lights")] public Light[] Lights;
    
        public Scene()
        {
            Name = "";
            Filename = "";
            Includes = System.Array.Empty<Include>();
            Lights = System.Array.Empty<Light>();
        }
        
        public Scene(string Name, string Filename, Include[] Includes, Light[] Lights)
        {
            this.Name = Name;
            this.Filename = Filename;
            this.Includes = Includes;
            this.Lights = Lights;
        }
        
        public Scene(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Filename);
            b.DeserializeArray(out Includes);
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new Include(ref b);
            }
            b.DeserializeArray(out Lights);
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new Light(ref b);
            }
        }
        
        public Scene(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Filename);
            b.DeserializeArray(out Includes);
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new Include(ref b);
            }
            b.DeserializeArray(out Lights);
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new Light(ref b);
            }
        }
        
        public Scene RosDeserialize(ref ReadBuffer b) => new Scene(ref b);
        
        public Scene RosDeserialize(ref ReadBuffer2 b) => new Scene(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.Serialize(Includes.Length);
            foreach (var t in Includes)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Lights.Length);
            foreach (var t in Lights)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.Serialize(Includes.Length);
            foreach (var t in Includes)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Lights.Length);
            foreach (var t in Lights)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Filename is null) BuiltIns.ThrowNullReference();
            if (Includes is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Includes.Length; i++)
            {
                if (Includes[i] is null) BuiltIns.ThrowNullReference(nameof(Includes), i);
                Includes[i].RosValidate();
            }
            if (Lights is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Lights.Length; i++)
            {
                if (Lights[i] is null) BuiltIns.ThrowNullReference(nameof(Lights), i);
                Lights[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Filename);
                size += WriteBuffer.GetArraySize(Includes);
                size += WriteBuffer.GetArraySize(Lights);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.AddLength(c, Name);
            c = WriteBuffer2.AddLength(c, Filename);
            c = WriteBuffer2.Align4(c);
            c += 4; // Includes.Length
            foreach (var t in Includes)
            {
                c = t.AddRos2MessageLength(c);
            }
            c = WriteBuffer2.Align4(c);
            c += 4; // Lights.Length
            foreach (var t in Lights)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Scene";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "5e11da295d41bbfbd413d6274556c4a9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTW/cNhA9W7+CwF5aIEgb20nTAjnIK9kmqi9IWqdGEAhcibvLRiIFkvLa+fUdSpTM" +
                "hXOss4el9GY4fPPEIWeFfFSLrm+ppkjVlFPkeUpLxveIk46ena1QAiMSO6QP1mV22LGWzk6pZHvGSbuA" +
                "HuZ1OzT0y1fEpicFbhFT2sTqRENbhbRAWzrbGy9i+4OGCa0Z1ZnjPyGe9+l//nlxcfMXYg/se9WpvfrN" +
                "kvZWKKc7KimvqSFJOKKPmkqTIFGK6kWkQTJkiG5gtBpNDjEBh8dL1As1CpTB+MKDSgYRO/swx+xJ/Y3s" +
                "x1l4N65wIAqRSTT4BAfa0TcQiCmQrmE1TFdjXDvRMFaUyPrgodeXzCbq7VpB9MX5l3cfvqIOrZAUR8js" +
                "XyF/DodJwYFx/RFdRWESVEF47W+iEn1Cv5/gfhDgEt+FYHh3stm9tWiFvDhHpNsyyvXy3rDdblDPdtox" +
                "pdgDnXNGApRn+ml53w5dX6matBB6AdWBccapUi+RClhQvteHxSTprqW1hhQh7MR+C4XVVGYTeCXsxkGa" +
                "2tLT008oDZu8JSPtuJ/J2ZG8PhGbvF2wvM/CKkmT0PnOIxbg6+tNMX1lBy6ycL2J/Bzwcxf34yscJma3" +
                "XLhwGOOimDbLpYvfhvjm1ni/P+WRx35UAPzhZM1bnOAkLIzhD9eQZv4al/cAfzylXmSRvw7jidCfri0y" +
                "68Z+ZvI6yTcPr6NwXeI0MaaTnDfJ30n6ecTPPWuAEBlObqrrPI2rzZ2j3mwpstswd/WbDev7CCdB6Eo4" +
                "m67SfxwFZxSSSVwFZ/yZ1/uZVppVMRQtzqJ7hxKgULUOFQCKzVWZ++vSYQFogO9wEDocjGecpuWtjXDp" +
                "4PgmCQOLLww+535WmT9n/RFbR36cORxGMMZ5nrpKjGgQrv1oJPF8pkNxgwOUNpzZ9NF666d+3sgd6Xtz" +
                "XExOw0M1+S1Hylj9O1JrMdee6Kkkmglu34+S9OP5UA0vkIfXr8vx9p5VzFKcuEdvgPNpc466zBIWWTp9" +
                "v5Nj2JFmK0SLaqJ0pQ6kEUf14kxeDkzC99TbU9FRLZ8mRpmYxIQ7mI06/djcMGmOWrDPwRjnVFYQsXXO" +
                "+EEvmPdaav6QIHQjpbnsa8E1Ydze9TYn01MQeANPoI12kkKXBtcR/eXI9AGZncO0Ml49ZMkUTPn1LUTE" +
                "4K7gmoHur6PQfJmmAfRE45roeIDm54FKs4xisPcgttKUNCaQpfUWIYgzk7OReDPuSBMbAvZSdEKbyaDd" +
                "uF23rIUbbZw6z+zgAjRdCzxCk8j2fCKjyTeKhh61YJ4yMqw4dDbQ80CdrFArbGKGDzRI0ClCv/YG2iuj" +
                "hBEJdg6dBBo5r1sxNGbt5aM+19fz5f3d+w9YIbyDFgsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
