/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PoseWithCovarianceStamped : IDeserializable<PoseWithCovarianceStamped>, IMessage
    {
        // This expresses an estimated pose with a reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "pose")] public PoseWithCovariance Pose;
    
        /// Constructor for empty message.
        public PoseWithCovarianceStamped()
        {
            Pose = new PoseWithCovariance();
        }
        
        /// Explicit constructor.
        public PoseWithCovarianceStamped(in StdMsgs.Header Header, PoseWithCovariance Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        /// Constructor with buffer.
        internal PoseWithCovarianceStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Pose = new PoseWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PoseWithCovarianceStamped(ref b);
        
        PoseWithCovarianceStamped IDeserializable<PoseWithCovarianceStamped>.RosDeserialize(ref Buffer b) => new PoseWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
        }
    
        public int RosMessageLength => 344 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseWithCovarianceStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "953b798c0f514ff060a53a3498ce6246";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/TQBC9768YKYe2KAkSRTlE4oBAQA9IhVbiS6ia2GN7kb3r7q6bmF/P23XipKRC" +
                "HKBRpNjrmTdv3rxxJnRdaU+yaZ14L57YkPigGw6SU2u90FqHipicFOLEZEKZtS7XBhFUOG4EOTkhBXnc" +
                "tEq9E87FUZV+1CUwPgHilb1jpzkCRFilXvzjj3p/9XZJPuQ3jS/904GFmtBVAD92OTUSOOfAVFiw02Ul" +
                "blbLndSUiKPf9DT0rfi52iqDbylGHNd1T51HULBQoGk6o7Mowdj4Lh+Z2kCwll3QWVezO1IsouPr5bZL" +
                "il68XiLGeMm6oEGoB0LmhL02JR6S6rQJ589igppcr+0Mt1JC47E4hYoDHUwyJ/ZL1HgyNDcHNsQRVMk9" +
                "naazG9z6M0IRUJDWZhWdgvllHyprACiUJraqJQJnUACoJzHp5OwAOdJekmFjd/AD4r7G38CaETf2NKsw" +
                "szp277sSAiKwdfZO5whd9Qkkq7WYQLVeOXa9illDSTV5k1wZ4vjSRPDL3ttMJ1NHPysfXERP07jR+f9y" +
                "YykWrnP9YMnjVdi5zEmcGvoB02HroFjhBC21nG13sEOCC4zZ93OV9mq7SRP6aNezhn/A1yMSBw25bZHE" +
                "WmwWMNi4gNhupzepuJB1egyHZyFIEOej18Gl0BvJZ7w55JhCo4UvgO+wZNNU4yCXnUTvnW6m1E/p55Sc" +
                "3Rbgle0CfaaIeHT85eHjr+n4TBW15bB4/u188f2gmccbHTp6+YC+x+OaxhdEPM63z/XQDV6TB2LPCTPE" +
                "MMcA9aGDQZ1JuPu4x2oQVHZ2xBpHn/lhrjv+6CWaM1K+167aDoY241U/Xv18HPp76R5aqXt6/rZauLvd" +
                "646/hgbL9eeOdldrpX4BuY0YXkAHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
