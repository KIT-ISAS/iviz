/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/BoundingVolume")]
    public sealed class BoundingVolume : IDeserializable<BoundingVolume>, IMessage
    {
        // Define a volume in 3D
        // A set of solid geometric primitives that make up the volume to define (as a union)
        [DataMember (Name = "primitives")] public ShapeMsgs.SolidPrimitive[] Primitives { get; set; }
        // The poses at which the primitives are located
        [DataMember (Name = "primitive_poses")] public GeometryMsgs.Pose[] PrimitivePoses { get; set; }
        // In addition to primitives, meshes can be specified to add to the bounding volume (again, as union)
        [DataMember (Name = "meshes")] public ShapeMsgs.Mesh[] Meshes { get; set; }
        // The poses at which the meshes are located
        [DataMember (Name = "mesh_poses")] public GeometryMsgs.Pose[] MeshPoses { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public BoundingVolume()
        {
            Primitives = System.Array.Empty<ShapeMsgs.SolidPrimitive>();
            PrimitivePoses = System.Array.Empty<GeometryMsgs.Pose>();
            Meshes = System.Array.Empty<ShapeMsgs.Mesh>();
            MeshPoses = System.Array.Empty<GeometryMsgs.Pose>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public BoundingVolume(ShapeMsgs.SolidPrimitive[] Primitives, GeometryMsgs.Pose[] PrimitivePoses, ShapeMsgs.Mesh[] Meshes, GeometryMsgs.Pose[] MeshPoses)
        {
            this.Primitives = Primitives;
            this.PrimitivePoses = PrimitivePoses;
            this.Meshes = Meshes;
            this.MeshPoses = MeshPoses;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public BoundingVolume(ref Buffer b)
        {
            Primitives = b.DeserializeArray<ShapeMsgs.SolidPrimitive>();
            for (int i = 0; i < Primitives.Length; i++)
            {
                Primitives[i] = new ShapeMsgs.SolidPrimitive(ref b);
            }
            PrimitivePoses = b.DeserializeStructArray<GeometryMsgs.Pose>();
            Meshes = b.DeserializeArray<ShapeMsgs.Mesh>();
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i] = new ShapeMsgs.Mesh(ref b);
            }
            MeshPoses = b.DeserializeStructArray<GeometryMsgs.Pose>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new BoundingVolume(ref b);
        }
        
        BoundingVolume IDeserializable<BoundingVolume>.RosDeserialize(ref Buffer b)
        {
            return new BoundingVolume(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Primitives, 0);
            b.SerializeStructArray(PrimitivePoses, 0);
            b.SerializeArray(Meshes, 0);
            b.SerializeStructArray(MeshPoses, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Primitives is null) throw new System.NullReferenceException(nameof(Primitives));
            for (int i = 0; i < Primitives.Length; i++)
            {
                if (Primitives[i] is null) throw new System.NullReferenceException($"{nameof(Primitives)}[{i}]");
                Primitives[i].RosValidate();
            }
            if (PrimitivePoses is null) throw new System.NullReferenceException(nameof(PrimitivePoses));
            if (Meshes is null) throw new System.NullReferenceException(nameof(Meshes));
            for (int i = 0; i < Meshes.Length; i++)
            {
                if (Meshes[i] is null) throw new System.NullReferenceException($"{nameof(Meshes)}[{i}]");
                Meshes[i].RosValidate();
            }
            if (MeshPoses is null) throw new System.NullReferenceException(nameof(MeshPoses));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                foreach (var i in Primitives)
                {
                    size += i.RosMessageLength;
                }
                size += 56 * PrimitivePoses.Length;
                foreach (var i in Meshes)
                {
                    size += i.RosMessageLength;
                }
                size += 56 * MeshPoses.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/BoundingVolume";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "22db94010f39e9198032cb4a1aeda26e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WWW/UMBB+j9T/MBIPbcVqOYoQAu0D0IVW4ihtkXoIrdxkdmOR2MF2ll1+PTM+kmwP" +
                "eIF2HxrH42/m++ZwHsA+zqVCELDUVVsjSAV7+1vZVvYAXoNFB3oOVleygAXqGp2ROTRG1tLJJVpwpXBQ" +
                "i+8IbUMLTDBOQxGQd4Ql9FZJrXa3MluKBme1XdhHJ4x6lKAuvw1gg/9Tgmu0JS/k42cp89J7GHgXBqHS" +
                "uXBYbGUxvnVAP6KDQ8yZRwrAhwpEUdBrrTjQHnAENdqSgHOh4ArBNpjLucSCzegI/+MQrnSrCqkWie2O" +
                "WAipRkBUbyH6kTAplAD9R2rR+99psWFilE3+8V/28eT9S7gzUxR+LJorvRqRRiUaHEG+rqQq0NCTpj2u" +
                "n6oKKIFRqAevYSmWyISl6aUkMBYelSO4gk7wBjwe0W+cZa1U7gW8+Xw2eRKfT44OpsfTydO4fHv+4fDT" +
                "/vR4spdefP40nTzLothu3SCXMqvsY4pW/D5LRoWsUVnKn900nVdauOfPSPfeIp2pUSgOf3hgYPYSUFBy" +
                "SRLrhHJRBOttWa4VNxwvtvsz20TeiDV7eKeN3yXiPtSRX52N4JyKjeS5GMbMIvN2hWrhyhRRro1B22iv" +
                "MkFaWWDHj0Qf99rOziaPB6vzTmteXZDUw5CC/jEqrao1cNpzXZMrSiJI6gWLRYhTOljEeYFgRCFbDoHQ" +
                "vGa+gsYbeZ0dv94//HpC8Qx9piR7TE6wd2+DKqF0oOLKJOfaSH5BlVRpT5xtLkCspB0Dp87gXEfFEu7s" +
                "YHr4/uAUdhg7LnZ7TgRCug0U7zlRLS9K12keewF2uBd2gz863fkJ7KKfsBj4ucsLIXTahfQJi3f7fMsJ" +
                "YaXSFp2/NjwHPUnjLpcmbythxqFlZNPXkNeUz2tKEtd724yCsvAwippd68SoX1dS18hTcQ069YZxLwwb" +
                "/p8Rd3O0+lvPYEMdQ3kQ/oYgDXjScqPODXK5ipznHWeLCjzuh9uE1Q6F58+OITtiwTqD7EtLU90oj9vb" +
                "3RdB6Wv4tKT2oJQ6urNC9XbxExcRcrxJN41AWHVP6+7p1/2E30uXOHSJomLe0HMzeF796HWnrq+pWP/M" +
                "KD39vIfrlb8P0qU6SAPf8Tz8KmnDYDFSqEWF9tXg9liKqkVWYk6dz98yMZOWSS/ROJmjvfyWsY/TCED3" +
                "WIeVxcEocteKqjtx87vOR3NLPRFYOnRPUiUat0iWaG3bPiieJ3tPL/dCnLiakXAh2t+OnRIP/QoAAA==";
                
    }
}
