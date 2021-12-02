/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Pose2D : IDeserializable<Pose2D>, IMessage
    {
        // Deprecated
        // Please use the full 3D pose.
        // In general our recommendation is to use a full 3D representation of everything and for 2D specific applications make the appropriate projections into the plane for their calculations but optimally will preserve the 3D information during processing.
        // If we have parallel copies of 2D datatypes every UI and other pipeline will end up needing to have dual interfaces to plot everything. And you will end up with not being able to use 3D tools for 2D use cases even if they're completely valid, as you'd have to reimplement it with different inputs and outputs. It's not particularly hard to plot the 2D pose or compute the yaw error for the Pose message and there are already tools and libraries that can do this for you.
        // This expresses a position and orientation on a 2D manifold.
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "theta")] public double Theta;
    
        /// Constructor for empty message.
        public Pose2D()
        {
        }
        
        /// Explicit constructor.
        public Pose2D(double X, double Y, double Theta)
        {
            this.X = X;
            this.Y = Y;
            this.Theta = Theta;
        }
        
        /// Constructor with buffer.
        internal Pose2D(ref Buffer b)
        {
            X = b.Deserialize<double>();
            Y = b.Deserialize<double>();
            Theta = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Pose2D(ref b);
        
        Pose2D IDeserializable<Pose2D>.RosDeserialize(ref Buffer b) => new Pose2D(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(Theta);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 24;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Pose2D";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "938fa65709584ad8e77d238529be13b8";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClVSvY7bMAze8xQEbshSZGiL7gWy3HZD7wEYi47ZypIgUcn57ftRzgXtYICm+PP98IXO" +
                "UqpMbBIOL/QWhZtQx2eL0NxjpG9nKrnJ6YD310RXSVI5Uu6V0JjXVVJg05xIG1kezfxsrT6+SbK9JM8k" +
                "N6mbLZquxCnQnCt9PVMrMumsE3EpUadR3WjlPzsSZGsuVYGTEP2WaS/QhI1eUCInGcPwp5UmjlOPjzGX" +
                "bpSL6coxbnRXQBuo6m2fDpya0LvuIEOvjg57JmkN4c59prvQwugpDAWiRJpyUWnOChSgAttW8D8o0vvr" +
                "IJixoVLRIlEBcWyHZNQLJZHgm8BhzA0dwoKS1Jmx2vMlZvtHshP9ROuW+39z7moLJRReZMh6ifLpBKhZ" +
                "zrF96uy5CR4PkLBsdgG2Y0U2ryWKCRS6cdTwhbj5pmPYwWFgFfUaOG6ktq8NOs9SRyaVbm3n3M3jE73a" +
                "sQ1kkMzUHamYv3ANT3JuAID5jREwOowOlz298Z2kVmQfxtKbV61wha84CmxycRH5F6tw2B50/S3qpXJ1" +
                "g2xhA21Y69eCO/V54AZj4ewvz8iHn4QLw45FxyUMLpjwPF+kHOzKSeccA/rnmNl+fKePZ7Q9I4AzPhz+" +
                "As9+JaJlAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
