/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshFeatures : IHasSerializer<MeshFeatures>, IMessage
    {
        [DataMember (Name = "map_uuid")] public string MapUuid;
        [DataMember (Name = "features")] public MeshMsgs.Feature[] Features;
    
        public MeshFeatures()
        {
            MapUuid = "";
            Features = EmptyArray<MeshMsgs.Feature>.Value;
        }
        
        public MeshFeatures(string MapUuid, MeshMsgs.Feature[] Features)
        {
            this.MapUuid = MapUuid;
            this.Features = Features;
        }
        
        public MeshFeatures(ref ReadBuffer b)
        {
            MapUuid = b.DeserializeString();
            {
                int n = b.DeserializeArrayLength();
                MeshMsgs.Feature[] array;
                if (n == 0) array = EmptyArray<MeshMsgs.Feature>.Value;
                else
                {
                    array = new MeshMsgs.Feature[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MeshMsgs.Feature(ref b);
                    }
                }
                Features = array;
            }
        }
        
        public MeshFeatures(ref ReadBuffer2 b)
        {
            b.Align4();
            MapUuid = b.DeserializeString();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                MeshMsgs.Feature[] array;
                if (n == 0) array = EmptyArray<MeshMsgs.Feature>.Value;
                else
                {
                    array = new MeshMsgs.Feature[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MeshMsgs.Feature(ref b);
                    }
                }
                Features = array;
            }
        }
        
        public MeshFeatures RosDeserialize(ref ReadBuffer b) => new MeshFeatures(ref b);
        
        public MeshFeatures RosDeserialize(ref ReadBuffer2 b) => new MeshFeatures(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(MapUuid);
            b.Serialize(Features.Length);
            foreach (var t in Features)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(MapUuid);
            b.Align4();
            b.Serialize(Features.Length);
            foreach (var t in Features)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(MapUuid, nameof(MapUuid));
            BuiltIns.ThrowIfNull(Features, nameof(Features));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetStringSize(MapUuid);
                foreach (var msg in Features) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, MapUuid);
            size = WriteBuffer2.Align4(size);
            size += 4; // Features.Length
            foreach (var msg in Features) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshFeatures";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ea0bfd1049bc24f2cd76d68461f1f987";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7XQwQqCYAwH8PueYtADBBYdgq51CoK6RcjQqYP8Jt8mZE+fYniojrnTf2OwHzOPEkqs" +
                "qUnbVnKo2aq0ttKWeyZvI19vWIzJAHZ/LjieD1v8ugkla80eu3F6UgmOd83IRQOY5+/tu5Kvkl6Ys2VR" +
                "Gtc4l/GHCBZ4qcQw0+AkwdArxkZNBiVqgdR3g1wCFpEZraGMoRjUmzU+ptRN6TkX//Nn48VVgjk5wQu3" +
                "LIB6BQIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshFeatures> CreateSerializer() => new Serializer();
        public Deserializer<MeshFeatures> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshFeatures>
        {
            public override void RosSerialize(MeshFeatures msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshFeatures msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshFeatures msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshFeatures msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshFeatures msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshFeatures>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshFeatures msg) => msg = new MeshFeatures(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshFeatures msg) => msg = new MeshFeatures(ref b);
        }
    }
}
