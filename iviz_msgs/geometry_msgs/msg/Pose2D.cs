/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Pose2D")]
    public sealed class Pose2D : IDeserializable<Pose2D>, IMessage
    {
        // Deprecated
        // Please use the full 3D pose.
        // In general our recommendation is to use a full 3D representation of everything and for 2D specific applications make the appropriate projections into the plane for their calculations but optimally will preserve the 3D information during processing.
        // If we have parallel copies of 2D datatypes every UI and other pipeline will end up needing to have dual interfaces to plot everything. And you will end up with not being able to use 3D tools for 2D use cases even if they're completely valid, as you'd have to reimplement it with different inputs and outputs. It's not particularly hard to plot the 2D pose or compute the yaw error for the Pose message and there are already tools and libraries that can do this for you.
        // This expresses a position and orientation on a 2D manifold.
        [DataMember (Name = "x")] public double X { get; set; }
        [DataMember (Name = "y")] public double Y { get; set; }
        [DataMember (Name = "theta")] public double Theta { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Pose2D()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Pose2D(double X, double Y, double Theta)
        {
            this.X = X;
            this.Y = Y;
            this.Theta = Theta;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Pose2D(ref Buffer b)
        {
            X = b.Deserialize<double>();
            Y = b.Deserialize<double>();
            Theta = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Pose2D(ref b);
        }
        
        Pose2D IDeserializable<Pose2D>.RosDeserialize(ref Buffer b)
        {
            return new Pose2D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(Theta);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 24;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Pose2D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "938fa65709584ad8e77d238529be13b8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClVSvW4bMQzeA+QdCGTwUmRoi+4FvHjLkD4AfeLZbHWSQEl27u37UWc77SCAx+PP98MX" +
                "2ksxmbhJeH56obcoXIU6XjsLzT1G+rankqu8Pj95xSHRSZIYR8rdCL15WSQFbpoTaaWWRzs/ms03VElt" +
                "K8kzyUVsbWdNJ+IUaM5GX/dUi0w660RcStRpVFda+M+GBVnLxRRQCdFvmbYCTdjoBSVykjEMX2o0cZx6" +
                "vI059ka5NF04xpWuCmgDlV226cCpCb3LBjJ0c3TYM0mtCO/sZ7oKnRldhaFBlEhTLirVeYEEdOC2FnwP" +
                "kvTrMChm7DAqWiQqQI79EI16oSQSfBdYjLmhQ1qQEpsZyz1fYm7/iPZKP9G65v7fnKu2MyUUHmUIe4xy" +
                "9wLkWs6x3pX23ASfB0iYNrsE686QzUuJ0gQaXThq+EJcfdMubOAw0ES9Bp430ratDTrPYiOTSm9149yb" +
                "x690aLs6kBW2pu6JYf6ZLTzIuQUA5ndGwOgwOnz29MpXEjNkb9bSm1ct8IVPOAtscnER+YsmHNYbXf8X" +
                "9WhsblA7cwNtmOv3gkv1eeA2rHVz3z0pH34Xrg07HB3nMOhgyOOGkXK8CyedcwxjxBwztx/f6eMzXD9D" +
                "YGzsZX8BjQvjAnMDAAA=";
                
    }
}
