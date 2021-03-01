/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/MeasurePositionsGoal")]
    public sealed class MeasurePositionsGoal : IDeserializable<MeasurePositionsGoal>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "points")] public GeometryMsgs.Point[] Points { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeasurePositionsGoal()
        {
            Points = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeasurePositionsGoal(in StdMsgs.Header Header, GeometryMsgs.Point[] Points)
        {
            this.Header = Header;
            this.Points = Points;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeasurePositionsGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Points = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeasurePositionsGoal(ref b);
        }
        
        MeasurePositionsGoal IDeserializable<MeasurePositionsGoal>.RosDeserialize(ref Buffer b)
        {
            return new MeasurePositionsGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Points, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += 24 * Points.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/MeasurePositionsGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2199cac4695ce1fc0f346db535dda30d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VTwWrcMBC96ysG9pCksAkkpYeF3kqbHAKB5FbKMiuN7QFZUjTjTdyv78gm2xZ66KE1" +
                "Akn2e29m3oxvCQNVGJbN9ZRH0jrvR+nl6iFz0q/foLRdnHMf//Hj7h+/7EA0rPFu1yQ28KiYAtYAlgwG" +
                "VIQuW47cD1S3kY4UjYRjoQDLV50LyaURnwYWsNVToooxzjCJgTSDz+M4JfaoBMoj/cY3JidAKFiV/RSx" +
                "Gj7XwKnBu4ojNXVbQs8TJU9w92lnmCTkJ2VLaDYFXwmFU28fwU3m2c11I7jN00ve2pV6c/oUHHRAbcnS" +
                "a6kkLU+UncV4txZ3adpmDlmUIHC+vNvbVS7AglgKVLIf4Nwyf5h1yMkECY5YGQ+RmrA3B0z1rJHOLn5R" +
                "Tot0wpTf5FfFnzH+RjaddFtN28F6Flv1MvVmoAFLzUcOBj3Mi4iPTEkh8qFinV1jrSHd5nPz2EDGWjpi" +
                "O4pkz9aAAC+sgxOtTX3pxp7D/5rGP/wCb4NlVilykqWYkoWVzZ7ctclpuDZEXSUrqqAn18WM+uE9vJ5O" +
                "8+n03bkfFMlsSXoDAAA=";
                
    }
}
