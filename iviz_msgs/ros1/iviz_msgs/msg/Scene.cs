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
        [DataMember (Name = "includes")] public SceneInclude[] Includes;
        /// <summary> List of lights </summary>
        [DataMember (Name = "lights")] public SceneLight[] Lights;
    
        public Scene()
        {
            Name = "";
            Filename = "";
            Includes = System.Array.Empty<SceneInclude>();
            Lights = System.Array.Empty<SceneLight>();
        }
        
        public Scene(string Name, string Filename, SceneInclude[] Includes, SceneLight[] Lights)
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
                Includes[i] = new SceneInclude(ref b);
            }
            b.DeserializeArray(out Lights);
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new SceneLight(ref b);
            }
        }
        
        public Scene(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Align4();
            b.DeserializeString(out Filename);
            b.Align4();
            b.DeserializeArray(out Includes);
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new SceneInclude(ref b);
            }
            b.Align4();
            b.DeserializeArray(out Lights);
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new SceneLight(ref b);
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
                "H4sIAAAAAAAAE71WTW/cNhA9R7+CgC8tEKSN7Xy0QA7ySraJ6guS1qkRBAJX4u6ykUiBpHbt/PoOJUrm" +
                "1jk23sNKejMcvnkcknOGfFSLrm+ppkjVlFPkeUpLxneIk46+enWGEngisUV6b11mhy1r6eyUSrZjnLQL" +
                "6BXGFfO6HRr65Sti05sC34gpbQJ2oqGtQlqgDZ3tzTQuYru9hlGtebpjJsDzPv3PPy8ubv5E7MC+V53a" +
                "qd9c9t4ZyumWSspratgSjuiDptKkS5SiepFskAwZNdbwtIpNDjEBh4dL1As1ypXB8z8eRgxwo5JB2M6+" +
                "zIF7Un8ju3Eo3o7T7IlCZJIQVmVPO/oaojEFQjashuFqDG4HGtqKElnvPfTzxbPZettWEH1x/uXt+6+o" +
                "Q2dIiiNk9o+QL8HB1dMbGNcf0VUUJkEVhNf+OirRJ/T7Ce4HAS7xXQiGtyebwFuJVsiLc0S6DaNcL98N" +
                "224H9WSnHVOKHeicOBIgP9OPy/dm6PpK1aSF0Auo9owzTpV6jlTAgvKd3i8mSbctrTXkCWEn9hvYcE1l" +
                "KmEqohKKc5Bmz+np7QW2i1XAMpL2uZsZ2id5oWW3CthZy/ssrJI0CZ0VH7EAX1+vi2m9HbjIwtU68nPA" +
                "z13cj69wmJi6uXDhMMZFMZXNpYvfhvjm1ni/O+WRx35UAPz+ZM5bnOAkLIzhg2tIM3+Fy3uAP55SL7LI" +
                "X4XxROgP1xaZeWM/M3md5JuH11G4KnGaGNNJzuvkryT9POLnnjVAiAwnN9V1nsbV+s5Rb7YU2W2Yu/rN" +
                "htV9hJMgdCWcTVfp346CMwrJJK6CM/7E691MK82qGLYvzqJ7hxKgsH8dKgAU66sy91elwwLQAN/hIHQ4" +
                "GM84TctbG+HSwfFNEgYWXxh8zv2sMn/O/CO2ivw4cziMYIzzPHWVGNEgXPnRSOLpiIdtDg6wyeEIpw/W" +
                "Wz/2cyF3pO/NwTE5DYdq8lsOl/Ec2JJai3kDip5Kopng9vsoST+eFNXwDDm80KU6Xu2zlFmKE/ckDnA+" +
                "VegozqxjkaXTIp6cyo4+GyFaVBOlK7UnjTiqZ0f0cn4SvqPejoqOavk40crEpChczmwU68fmhklz8oJ9" +
                "DsY4p7KCiK1z5A96wbyfJekPCUKbUpoGoBZcE8bt/W9zMs0GgS/wBNpoKyk0c3A70V+OTO+RKR+mlfHq" +
                "IUumYMivbyAiBncFtw40iR2F9sw0EqAnGudExz10RQcqzTSKQQFCbKUpaUwgS+sNQhBnJmcj8WYsSxMb" +
                "AvZSdEKbwaDdWLMb1sIFNw6dR3ZwH5pOBl6hjWQ7PpHR5BtFQ49aME8ZGVYcuh3og2CznKFW2MQMH2ia" +
                "oI+ERu419F1GCSMSVA6dBBo5r1oxNGbuZVGfNtnTXf7d+xdBzvP0PQsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
