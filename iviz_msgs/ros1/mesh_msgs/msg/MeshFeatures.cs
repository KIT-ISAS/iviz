/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshFeatures : IDeserializable<MeshFeatures>, IMessage
    {
        [DataMember (Name = "map_uuid")] public string MapUuid;
        [DataMember (Name = "features")] public MeshMsgs.Feature[] Features;
    
        public MeshFeatures()
        {
            MapUuid = "";
            Features = System.Array.Empty<MeshMsgs.Feature>();
        }
        
        public MeshFeatures(string MapUuid, MeshMsgs.Feature[] Features)
        {
            this.MapUuid = MapUuid;
            this.Features = Features;
        }
        
        public MeshFeatures(ref ReadBuffer b)
        {
            b.DeserializeString(out MapUuid);
            b.DeserializeArray(out Features);
            for (int i = 0; i < Features.Length; i++)
            {
                Features[i] = new MeshMsgs.Feature(ref b);
            }
        }
        
        public MeshFeatures(ref ReadBuffer2 b)
        {
            b.DeserializeString(out MapUuid);
            b.DeserializeArray(out Features);
            for (int i = 0; i < Features.Length; i++)
            {
                Features[i] = new MeshMsgs.Feature(ref b);
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
            b.Serialize(MapUuid);
            b.Serialize(Features.Length);
            foreach (var t in Features)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (MapUuid is null) BuiltIns.ThrowNullReference();
            if (Features is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Features.Length; i++)
            {
                if (Features[i] is null) BuiltIns.ThrowNullReference(nameof(Features), i);
                Features[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(MapUuid) + WriteBuffer.GetArraySize(Features);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.AddLength(c, MapUuid);
            c = WriteBuffer2.Align4(c);
            c += 4; // Features.Length
            foreach (var t in Features)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
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
    }
}
