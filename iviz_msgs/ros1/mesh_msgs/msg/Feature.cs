/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class Feature : IDeserializable<Feature>, IMessageRos1
    {
        [DataMember (Name = "location")] public GeometryMsgs.Point Location;
        [DataMember (Name = "descriptor")] public StdMsgs.Float32[] Descriptor;
    
        /// Constructor for empty message.
        public Feature()
        {
            Descriptor = System.Array.Empty<StdMsgs.Float32>();
        }
        
        /// Explicit constructor.
        public Feature(in GeometryMsgs.Point Location, StdMsgs.Float32[] Descriptor)
        {
            this.Location = Location;
            this.Descriptor = Descriptor;
        }
        
        /// Constructor with buffer.
        public Feature(ref ReadBuffer b)
        {
            b.Deserialize(out Location);
            b.DeserializeArray(out Descriptor);
            for (int i = 0; i < Descriptor.Length; i++)
            {
                Descriptor[i] = new StdMsgs.Float32(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Feature(ref b);
        
        public Feature RosDeserialize(ref ReadBuffer b) => new Feature(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Location);
            b.SerializeArray(Descriptor);
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/Feature";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ac711cf3ef6eb8582240a7afe5b9a573";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7XQsQrCQAwG4D1PEfABhFYcBFedBMFuIhKuaRtoL8clg/XptRQ66KqZ/vxLPtKyDux5" +
                "vA/W2vqsEh17DeSiEczruT/0Sl4W1xvWbCFLcs0A+x8PnC7HHbbfIlhh1Ylh0Ogk0dA7xqQmkxK1QXpv" +
                "k1wiNpkZLVFgaCb1doOPJY1Lev6L//mz+WJZYE1O8AKMerHtbwEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
