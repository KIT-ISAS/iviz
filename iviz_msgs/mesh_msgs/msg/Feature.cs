/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/Feature")]
    public sealed class Feature : IDeserializable<Feature>, IMessage
    {
        [DataMember (Name = "location")] public GeometryMsgs.Point Location { get; set; }
        [DataMember (Name = "descriptor")] public StdMsgs.Float32[] Descriptor { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Feature()
        {
            Descriptor = System.Array.Empty<StdMsgs.Float32>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Feature(in GeometryMsgs.Point Location, StdMsgs.Float32[] Descriptor)
        {
            this.Location = Location;
            this.Descriptor = Descriptor;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Feature(ref Buffer b)
        {
            Location = new GeometryMsgs.Point(ref b);
            Descriptor = b.DeserializeArray<StdMsgs.Float32>();
            for (int i = 0; i < Descriptor.Length; i++)
            {
                Descriptor[i] = new StdMsgs.Float32(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Feature(ref b);
        }
        
        Feature IDeserializable<Feature>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Location.RosSerialize(ref b);
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
    
        public int RosMessageLength
        {
            get {
                int size = 28;
                size += 4 * Descriptor.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/Feature";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ac711cf3ef6eb8582240a7afe5b9a573";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7XQsQrCQAwG4D1PEfABhFYcBFedBMFuIhKuaRtoL8clg/XptRQ66KqZ/vxLPtKyDux5" +
                "vA/W2vqsEh17DeSiEczruT/0Sl4W1xvWbCFLcs0A+x8PnC7HHbbfIlhh1Ylh0Ogk0dA7xqQmkxK1QXpv" +
                "k1wiNpkZLVFgaCb1doOPJY1Lev6L//mz+WJZYE1O8AKMerHtbwEAAA==";
                
    }
}
