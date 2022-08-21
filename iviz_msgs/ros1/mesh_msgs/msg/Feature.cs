/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class Feature : IDeserializable<Feature>, IMessage
    {
        [DataMember (Name = "location")] public GeometryMsgs.Point Location;
        [DataMember (Name = "descriptor")] public StdMsgs.Float32[] Descriptor;
    
        public Feature()
        {
            Descriptor = System.Array.Empty<StdMsgs.Float32>();
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
                Descriptor = n == 0
                    ? System.Array.Empty<StdMsgs.Float32>()
                    : new StdMsgs.Float32[n];
                for (int i = 0; i < n; i++)
                {
                    Descriptor[i] = new StdMsgs.Float32(ref b);
                }
            }
        }
        
        public Feature(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Location);
            {
                int n = b.DeserializeArrayLength();
                Descriptor = n == 0
                    ? System.Array.Empty<StdMsgs.Float32>()
                    : new StdMsgs.Float32[n];
                for (int i = 0; i < n; i++)
                {
                    Descriptor[i] = new StdMsgs.Float32(ref b);
                }
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
            b.Serialize(in Location);
            b.Serialize(Descriptor.Length);
            foreach (var t in Descriptor)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Descriptor is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Descriptor.Length; i++)
            {
                if (Descriptor[i] is null) BuiltIns.ThrowNullReference(nameof(Descriptor), i);
                Descriptor[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 28 + 4 * Descriptor.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align8(c);
            c += 24; // Location
            c += 4; // Descriptor length
            c += 4 * Descriptor.Length;
            return c;
        }
    
        public const string MessageType = "mesh_msgs/Feature";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ac711cf3ef6eb8582240a7afe5b9a573";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7XQsQrCQAwG4D1PEfABhFYcBFedBMFuIhKuaRtoL8clg/XptRQ66KqZ/vxLPtKyDux5" +
                "vA/W2vqsEh17DeSiEczruT/0Sl4W1xvWbCFLcs0A+x8PnC7HHbbfIlhh1Ylh0Ogk0dA7xqQmkxK1QXpv" +
                "k1wiNpkZLVFgaCb1doOPJY1Lev6L//mz+WJZYE1O8AKMerHtbwEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
