/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Feature : IDeserializable<Feature>, IMessage
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
        internal Feature(ref Buffer b)
        {
            b.Deserialize(out Location);
            Descriptor = b.DeserializeArray<StdMsgs.Float32>();
            for (int i = 0; i < Descriptor.Length; i++)
            {
                Descriptor[i] = new StdMsgs.Float32(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Feature(ref b);
        
        Feature IDeserializable<Feature>.RosDeserialize(ref Buffer b) => new Feature(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Location);
            b.SerializeArray(Descriptor, 0);
        }
        
        public void RosValidate()
        {
            if (Descriptor is null) throw new System.NullReferenceException(nameof(Descriptor));
            for (int i = 0; i < Descriptor.Length; i++)
            {
                if (Descriptor[i] is null) throw new System.NullReferenceException($"{nameof(Descriptor)}[{i}]");
                Descriptor[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 28 + 4 * Descriptor.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/Feature";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ac711cf3ef6eb8582240a7afe5b9a573";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7XQsQrCQAwG4D1PEfABhFYcBFedBMFuIhKuaRtoL8clg/XptRQ66KqZ/vxLPtKyDux5" +
                "vA/W2vqsEh17DeSiEczruT/0Sl4W1xvWbCFLcs0A+x8PnC7HHbbfIlhh1Ylh0Ogk0dA7xqQmkxK1QXpv" +
                "k1wiNpkZLVFgaCb1doOPJY1Lev6L//mz+WJZYE1O8AKMerHtbwEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
