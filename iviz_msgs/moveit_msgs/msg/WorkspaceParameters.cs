/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/WorkspaceParameters")]
    public sealed class WorkspaceParameters : IDeserializable<WorkspaceParameters>, IMessage
    {
        // This message contains a set of parameters useful in
        // setting up the volume (a box) in which the robot is allowed to move.
        // This is useful only when planning for mobile parts of 
        // the robot as well.
        // Define the frame of reference for the box corners
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The minumum corner of the box, with respect to the robot starting pose
        [DataMember (Name = "min_corner")] public GeometryMsgs.Vector3 MinCorner { get; set; }
        // The maximum corner of the box, with respect to the robot starting pose
        [DataMember (Name = "max_corner")] public GeometryMsgs.Vector3 MaxCorner { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public WorkspaceParameters()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public WorkspaceParameters(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MinCorner, in GeometryMsgs.Vector3 MaxCorner)
        {
            this.Header = Header;
            this.MinCorner = MinCorner;
            this.MaxCorner = MaxCorner;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public WorkspaceParameters(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            MinCorner = new GeometryMsgs.Vector3(ref b);
            MaxCorner = new GeometryMsgs.Vector3(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new WorkspaceParameters(ref b);
        }
        
        WorkspaceParameters IDeserializable<WorkspaceParameters>.RosDeserialize(ref Buffer b)
        {
            return new WorkspaceParameters(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            MinCorner.RosSerialize(ref b);
            MaxCorner.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/WorkspaceParameters";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d639a834e7b1f927e9f1d6c30e920016";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVVsW7bQAzdBfgfCGRIUiQukBQdAnQL2mYoECBB14CWKOnQ01G9O9lWv76PZ0dJgQ4d" +
                "Gi+WdXyPj+Tj+YQee5dokJS4E6o1ZHYhEVOSTNrSyJEHyRITTUnayZML1YmdZhc6mkbKvdBW/TQInTFt" +
                "dH+OENr1ru7LWdSNZkIS9l530lBWGnQr6+qY3C3UGvwMpAQaPYdgCVqNiN44LyYlJ9ME4AsxJ9qJ9+sK" +
                "b2+ldUHKYWuyLThKK1FCLYXKjiARhcaAmqqvwo1E6suXUTwiYHBhGqbhGGQkR9gF7VzuQZlGqbMV8qIj" +
                "ZcgzxaMmqTpRdC3OT0Pq0vvviNZ4bcRPB9IlFe/d26Ti/XOqVfXpP39W1beHLzfQ0RySHrq4Qk0PmUPD" +
                "sYGjMjecuXS9d10v8dLLVrypH0bYoJzmeZT02gmdQDKcMpsnildqHYYpuJozJuvg1Nd4IGE2Lt5w9eQ5" +
                "Il5j44KFFxMYe3Hsz6nY4O72xnyepJ6yg6AZDHUUTtbRu1uqJhfy9ZUBqpPHnV7ip3QYzpIco+DiaNmP" +
                "GJDp5HSDHO8Oxa3Bje4IsjSJzsq7J/xM54QkkCCjYjnOoPx+zr2Gww5xdLyBz0FcowNgPTXQ6fkrZpN9" +
                "Q4GDPtMfGF9y/AttWHitpsseM/NWfZo6NBCBY9StaxC6mQtJ7Z2ETN5tIse5MtQhZXXyuSxatvGVidii" +
                "p6S1wwCaYuIq5VhW2SKfXPN2hvzrKqye3RXFpoU67HrblkMzTxsFxYxcy9p8clcmW26iQRhFw4ILEsDG" +
                "RUCdhrUtMG4XjXJBLlOjkihoBsfAP0ApaDMBzeMIMng9ckieDWuvATmTdbfGptuNV6KsTcXUZQ1cTdF1" +
                "DltgSCQaFjDTsboLyu0V2uyPt+chGWYGkqi5AM7XdNfSrBPtrCA8xOP2KW0g8airuCSrXtjqHSn+7Oi9" +
                "YheWPwv8T2QsPm7e1ivnjx9ovzzNy9OvVfUbEt0eoWUGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
