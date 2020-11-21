/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract (Name = "nav_msgs/Path")]
    public sealed class Path : IDeserializable<Path>, IMessage
    {
        //An array of poses that represents a Path for a robot to follow
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "poses")] public GeometryMsgs.PoseStamped[] Poses { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Path()
        {
            Header = new StdMsgs.Header();
            Poses = System.Array.Empty<GeometryMsgs.PoseStamped>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Path(StdMsgs.Header Header, GeometryMsgs.PoseStamped[] Poses)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACr1VTWvcQAy9G/wfBDkkKU0KbelhoYdA6cehsCW5lRK0ttYesGcczTiJ++v7NM6usy2F" +
                "HNosu3jGIz29J2m0RxeeWJUnClsaQpRIqeVEKoNKFJ8iMa05tbQNiqWGTUiUArZdF+7K4rNwLUptfpRF" +
                "I6GXpNN1H5v4ag3Ay8T9IPX3HzN8WZTF+3/8KYuvl59WFFM9h505lcURIbivWWsCKa45cZbRuqYVPevk" +
                "Vjp4ZX6UT9M0SDw3z6vWRcK3ES/KXTfRGGEF5VXo+9G7ipNQcr0cAJirQ0ppYE2uGjtWOAStnTf7rXIv" +
                "Gd9+UW5G8ZXQlw8rWPko1ZgcSE3AqFQ4Ot/gEMaj8+nNa/OA49VdOMNeGiR+z2CuGxjLvZXOyHJcWZgX" +
                "s8ZzwCNJgkB1pJP87hrbeEqIAxYyhKqlE9BfT6kNHohCt6yON50YcoU8APbYnI5PH0Mb9RV59mGHP0Mu" +
                "QZ6C6xdgk3XWonidpSCODfIIy0HDrathu5kyStU59Ch1bqOsU1mY2xwUIB8t2TCDX64NnhxjqBwqUdOd" +
                "S21ZxKQWINfl2tX/sTv/ejNM7gXZi8wJV28rmtvi98Yh5GMp+B93L0PYJXtWFTP9/bzg5FDhh2FiV2Gr" +
                "gpIMXMlLuzr2un44d9nWRAV1O99zdPs6oL33FmXxbUQO1GfkxfIZZYLOfiigvxM7b4NSFhVQhEufeR+I" +
                "LottFzi9e0v3yxKdulv+fDYVSxL3Uh5P+YPUHmqw3c1SAozQHjPsCcp2S/xPlMUvPWDdeGsGAAA=";
                
    }
}
