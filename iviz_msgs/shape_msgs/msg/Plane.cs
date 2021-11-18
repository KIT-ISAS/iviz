/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [Preserve, DataContract (Name = "shape_msgs/Plane")]
    public sealed class Plane : IDeserializable<Plane>, IMessage
    {
        // Representation of a plane, using the plane equation ax + by + cz + d = 0
        // a := coef[0]
        // b := coef[1]
        // c := coef[2]
        // d := coef[3]
        [DataMember (Name = "coef")] public double[/*4*/] Coef;
    
        /// <summary> Constructor for empty message. </summary>
        public Plane()
        {
            Coef = new double[4];
        }
        
        /// <summary> Explicit constructor. </summary>
        public Plane(double[] Coef)
        {
            this.Coef = Coef;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Plane(ref Buffer b)
        {
            Coef = b.DeserializeStructArray<double>(4);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Plane(ref b);
        }
        
        Plane IDeserializable<Plane>.RosDeserialize(ref Buffer b)
        {
            return new Plane(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Coef, 4);
        }
        
        public void RosValidate()
        {
            if (Coef is null) throw new System.NullReferenceException(nameof(Coef));
            if (Coef.Length != 4) throw new RosInvalidSizeForFixedArrayException(nameof(Coef), Coef.Length, 4);
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 32;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "shape_msgs/Plane";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2c1b92ed8f31492f8e73f6a4a44ca796";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClNWCEotKEotTs0rSSzJzM9TyE9TSFQoyEnMS9VRKC3OzEtXKMlIhQgopBaWQhQlViho" +
                "KyRVAonkKiCRomCrYMDFpQzUaWWrkJyfmhZtEAvkJsG5hiBuMpxrBOKmwLnGsVxcaTn5iSVmJtEmsWAx" +
                "Ll4uAHsFt+abAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
