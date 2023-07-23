/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class Feature : IHasSerializer<Feature>, IMessage
    {
        [DataMember (Name = "location")] public GeometryMsgs.Point Location;
        [DataMember (Name = "descriptor")] public StdMsgs.Float32[] Descriptor;
    
        public Feature()
        {
            Descriptor = EmptyArray<StdMsgs.Float32>.Value;
        }
        
        public Feature(in GeometryMsgs.Point Location, StdMsgs.Float32[] Descriptor)
        {
            this.Location = Location;
            this.Descriptor = Descriptor;
        }
        
        public Feature(ref ReadBuffer b)
        {
            b.Deserialize(out Location);
            {
                int n = b.DeserializeArrayLength();
                StdMsgs.Float32[] array;
                if (n == 0) array = EmptyArray<StdMsgs.Float32>.Value;
                else
                {
                    array = new StdMsgs.Float32[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new StdMsgs.Float32(ref b);
                    }
                }
                Descriptor = array;
            }
        }
        
        public Feature(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Location);
            {
                int n = b.DeserializeArrayLength();
                StdMsgs.Float32[] array;
                if (n == 0) array = EmptyArray<StdMsgs.Float32>.Value;
                else
                {
                    array = new StdMsgs.Float32[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new StdMsgs.Float32(ref b);
                    }
                }
                Descriptor = array;
            }
        }
        
        public Feature RosDeserialize(ref ReadBuffer b) => new Feature(ref b);
        
        public Feature RosDeserialize(ref ReadBuffer2 b) => new Feature(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Location);
            b.Serialize(Descriptor.Length);
            foreach (var t in Descriptor)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align8();
            b.Serialize(in Location);
            b.Serialize(Descriptor.Length);
            foreach (var t in Descriptor)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Descriptor, nameof(Descriptor));
            foreach (var msg in Descriptor) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 28;
                size += 4 * Descriptor.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align8(size);
            size += 24; // Location
            size += 4; // Descriptor.Length
            size += 4 * Descriptor.Length;
            return size;
        }
    
        public const string MessageType = "mesh_msgs/Feature";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ac711cf3ef6eb8582240a7afe5b9a573";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7XQsQrCQAwG4D1PEfABhFYcBFedBMFuIhKuaRtoL8clg/XptRQ66KqZ/vxLPtKyDux5" +
                "vA/W2vqsEh17DeSiEczruT/0Sl4W1xvWbCFLcs0A+x8PnC7HHbbfIlhh1Ylh0Ogk0dA7xqQmkxK1QXpv" +
                "k1wiNpkZLVFgaCb1doOPJY1Lev6L//mz+WJZYE1O8AKMerHtbwEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Feature> CreateSerializer() => new Serializer();
        public Deserializer<Feature> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Feature>
        {
            public override void RosSerialize(Feature msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Feature msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Feature msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Feature msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Feature msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Feature>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Feature msg) => msg = new Feature(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Feature msg) => msg = new Feature(ref b);
        }
    }
}
