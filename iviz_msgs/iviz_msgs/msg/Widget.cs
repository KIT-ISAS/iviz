/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Widget")]
    public sealed class Widget : IDeserializable<Widget>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "action")] public byte Action { get; set; }
        [DataMember (Name = "id")] public string Id { get; set; }
        [DataMember (Name = "type")] public byte Type { get; set; }
        [DataMember (Name = "main_color")] public StdMsgs.ColorRGBA MainColor { get; set; }
        [DataMember (Name = "secondary_color")] public StdMsgs.ColorRGBA SecondaryColor { get; set; }
        [DataMember (Name = "scale")] public double Scale { get; set; }
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Widget()
        {
            Id = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Widget(in StdMsgs.Header Header, byte Action, string Id, byte Type, in StdMsgs.ColorRGBA MainColor, in StdMsgs.ColorRGBA SecondaryColor, double Scale, in GeometryMsgs.Pose Pose)
        {
            this.Header = Header;
            this.Action = Action;
            this.Id = Id;
            this.Type = Type;
            this.MainColor = MainColor;
            this.SecondaryColor = SecondaryColor;
            this.Scale = Scale;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Widget(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Action = b.Deserialize<byte>();
            Id = b.DeserializeString();
            Type = b.Deserialize<byte>();
            MainColor = new StdMsgs.ColorRGBA(ref b);
            SecondaryColor = new StdMsgs.ColorRGBA(ref b);
            Scale = b.Deserialize<double>();
            Pose = new GeometryMsgs.Pose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Widget(ref b);
        }
        
        Widget IDeserializable<Widget>.RosDeserialize(ref Buffer b)
        {
            return new Widget(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            MainColor.RosSerialize(ref b);
            SecondaryColor.RosSerialize(ref b);
            b.Serialize(Scale);
            Pose.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 102;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Id);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Widget";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f58c2d09f3854ccb6a8bd72548889531";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTW/bMAy961cQ6KHtsGTANuwQYIduxboeBmRr7wEjM7YAWXIlOan36/ekxE6DDdgO" +
                "aw3D1gf59PhIKqZq1cY6vvkqXEmgpvzUekhCrJPxTsUUjKvJVPvVNHSCtYPbZ299+HHz6YpaNm6l8/RP" +
                "u1G0dxWH4WCysZ7Th/cUNVtRtfhWEjaL19JHoQ4fpdTH//yob3c3C4qnUaszukuc6VUEHlxxYtp4qGHq" +
                "RsLMylYsnLjtpKKym1WIczjeNyYS3lqcBLZ2oD7CKHnSvm17ZzRn0UwrJ/7wNI6YOg7J6N5ygL0PlXHZ" +
                "fBO4lYyON8pDL04L3V4vYOOgZJ8MCA1A0EE45uzcXpPqjUvv3mYHdXa/8zNMpUZOp8MpNZwyWXnsgsTM" +
                "k+MCZ7zaBzcH9uKQqkgXZW2FabwkHAIK0nnd0AWYL4fUeAdAoS0Hw2srGRjZtEA9z07nl0+QXYF27PwI" +
                "v0c8nvEvsG7CzTHNGuTM5uhjX0NAGHbBb00F0/VQQLQ14hJZsw6oPZW99keqsy9ZYxjBq2QEf47Ra4ME" +
                "VLQzqRkrv2Rjhfp/7mqc2mXfHshlmEb1NFpPI34uRr/3IyS/oiC5bCAo54uB/KZ0aS7kTRAI27GW17nu" +
                "83J12DfFFpkiH8zoOye19KjPyUB976F7cAX3aPdSAYLK2MuozoSrLJb6mfgjFjRroXwS7nSRPU6jYRr9" +
                "fBn6R+nGGKZEoaZP9Dwln2cPR91x47Vz9ZeIxtFOqV/yhEvMPAYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
