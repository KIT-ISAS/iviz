/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/Path")]
    public sealed class Path : IDeserializable<Path>, IMessage
    {
        //An array of poses that represents a Path for a robot to follow
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "poses")] public GeometryMsgs.PoseStamped[] Poses { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Path()
        {
            Poses = System.Array.Empty<GeometryMsgs.PoseStamped>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Path(in StdMsgs.Header Header, GeometryMsgs.PoseStamped[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Path(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Poses = b.DeserializeArray<GeometryMsgs.PoseStamped>();
            for (int i = 0; i < Poses.Length; i++)
            {
                Poses[i] = new GeometryMsgs.PoseStamped(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Path(ref b);
        }
        
        Path IDeserializable<Path>.RosDeserialize(ref Buffer b)
        {
            return new Path(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Poses, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
            for (int i = 0; i < Poses.Length; i++)
            {
                if (Poses[i] is null) throw new System.NullReferenceException($"{nameof(Poses)}[{i}]");
                Poses[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Poses)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/Path";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6227e2b7e9cce15051f669a5e197bbf7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC9C/IfBnLY3dKk0JYeAj0slH4cClt2b6WEiT2xBbbkHcnJur++T/LG2SyU" +
                "9tDGGCzJM2/emw/Nrx2xKg/kt9T5IIFizZFUOpUgLgZiuuFY09Yrluo3PlL02DaN35vPwqUo1fljKvGt" +
                "RB3WbajCqxvA3UZuOym//xjBzcy8/8fPzHy9/bSiEMsx6shoZuaE2K5kLQmcuOTIWUNtq1p00chOGnhl" +
                "epT/xqGTsITjXW0D4a3EiXLTDNQHGEF14du2d7bgKBRtKyf+8LTIJnWs0RZ9wwp7r6V1yXyr3EpCxxvk" +
                "vhdXCH35sIKNC1L00YLQAIRChYN1FX6S6a2Lb14nBzO/2/sFtlIh4VPwsVwgKw+pYoknhxVivBjFLYGN" +
                "7AiilIEu89ka23BFCAIK0vmipkswvxli7R0AhXasljeNJOACGQDqRXK6uHqCnGivyLHzB/gR8Rjjb2Dd" +
                "hJs0LWrUrEnqQ18hgTDs1O9sCdPNkEGKxqIvqbEbZR1M8hpDmvnHlGMYwStXBF8OwRcWBShpb2NtQtSE" +
                "nquxtuX/a8jfzkJqzWtKB5kRRm0rmrvhebcQcnGs9LNZywBpqM4pYeQ+XQ4cLSr7eHOk5t+qoBgdF/Iy" +
                "zUo6Lh//22ybFHm1B98lQQd6ejIw33rIV5dxj3bn0wgys8MVgKaObF26EuUoAXIw45n1iWKzbTzHd2/p" +
                "YVoN0+rnuRQc8zfJeHqXn2T1lH/a3R+zj7uyXZo/iDqs9pD3CwKs5QNLBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
