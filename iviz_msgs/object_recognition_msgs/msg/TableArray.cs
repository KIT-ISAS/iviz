/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/TableArray")]
    public sealed class TableArray : IDeserializable<TableArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Just an array of tables
        [DataMember (Name = "tables")] public ObjectRecognitionMsgs.Table[] Tables;
    
        /// <summary> Constructor for empty message. </summary>
        public TableArray()
        {
            Tables = System.Array.Empty<ObjectRecognitionMsgs.Table>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TableArray(in StdMsgs.Header Header, ObjectRecognitionMsgs.Table[] Tables)
        {
            this.Header = Header;
            this.Tables = Tables;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TableArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Tables = b.DeserializeArray<ObjectRecognitionMsgs.Table>();
            for (int i = 0; i < Tables.Length; i++)
            {
                Tables[i] = new ObjectRecognitionMsgs.Table(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TableArray(ref b);
        }
        
        TableArray IDeserializable<TableArray>.RosDeserialize(ref Buffer b)
        {
            return new TableArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Tables, 0);
        }
        
        public void RosValidate()
        {
            if (Tables is null) throw new System.NullReferenceException(nameof(Tables));
            for (int i = 0; i < Tables.Length; i++)
            {
                if (Tables[i] is null) throw new System.NullReferenceException($"{nameof(Tables)}[{i}]");
                Tables[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Tables);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/TableArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d1c853e5acd0ed273eb6682dc01ab428";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/UMBC9W+p/GKmHFkSLBIhDEQckBC0SUNS9IbSaTWYTl8RObWe36a/njdPNdqkE" +
                "HGijSPnwzJt5b559KlxKoDo/jNmnT31MxI44BB7ILynxopFo/OJSijQPUvjK2WS9m7exis9nuvz9xyZs" +
                "z7z9z9ee+Xzx8YRiKseCp7nVPbR6kdiVHEpqJXHJiWnpQcVWtYSjRlbSIIvbTkrKq2noJB4jcVbbSLgr" +
                "cRK4aQbqI4KSp8K3be9swUko2VZ28pFpIQx1HJIt+oYD4n0ordPwZeBWFB13lKteXCF09v4EMS5K0SeL" +
                "hgYgFEE4WldhkUxvXXr5QhPM/mztj/ApFQYyFadUc9Jm5boLErVPjieo8XQkdwxsqIOxuDLSYf43x2d8" +
                "QiiCFqTzRU2H6Px8SLV3ABRacbA6MAUuoABQDzTp4MkdZG37hBw7v4EfEbc1/gXWTbjK6ajGzBplH/sK" +
                "AiKwC35lS4QuhgxSNFZcosYuAofBaNZY0ux/UI0RhKw8ETw5Rl9YDKCktU21iSkoep7G3JYPZ8g/bgi1" +
                "55mDHds4DhCuadiBcN4nVHOkhYijUhJQdKgaU8EjjhoP/wHPmHu7cwZ9OowjR0YafJ8lS4Fd1GpjscQ/" +
                "ZVzzo6Jbl8YhJmmBpDtbM7WdZ1m6/O2DrTDT6FtZYxthkuNkx7aVg+B0KPM/oNwQX2MIDqW52ZTLUaYS" +
                "YKQwjKqca9Pa+S0LRdY8qnpG80nktvWpVulB8MvXGYyfBBWXfcAiBISDNAw4MOVKrqnum+bN6Arcl/kA" +
                "i7SE2His5WAFWosoYQWZbRoPgInpXR3uC6WA1kXY81bIqaDyuJCk+Z3HrkVBiKDW+y1wp8I9VZCJw3MM" +
                "n2fcBzPs/YGoS99RED1YsOWy6UZCGBa0WQaMJXZcwCE4GfV3ebueDZ+dAB03ucdkMqMpwHzroWVwGXcb" +
                "93gc0cze5sCHyIkxzNGjGwqgg72Zu95hbJaN5/T6FV1Pb8P0dvNYDLb6TTSmceHs21F1t3/9utqqr+Y8" +
                "Nn8htXlbg94vkPi4ARkIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
