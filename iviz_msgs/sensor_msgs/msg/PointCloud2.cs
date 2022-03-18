/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PointCloud2 : IDeserializable<PointCloud2>, IMessage
    {
        // This message holds a collection of N-dimensional points, which may
        // contain additional information such as normals, intensity, etc. The
        // point data is stored as a binary blob, its layout described by the
        // contents of the "fields" array.
        // The point cloud data may be organized 2d (image-like) or 1d
        // (unordered). Point clouds organized as 2d images may be produced by
        // camera depth sensors such as stereo or time-of-flight.
        // Time of sensor data acquisition, and the coordinate frame ID (for 3d
        // points).
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // 2D structure of the point cloud. If the cloud is unordered, height is
        // 1 and width is the length of the point cloud.
        [DataMember (Name = "height")] public uint Height;
        [DataMember (Name = "width")] public uint Width;
        // Describes the channels and their layout in the binary data blob.
        [DataMember (Name = "fields")] public PointField[] Fields;
        /// <summary> Is this data bigendian? </summary>
        [DataMember (Name = "is_bigendian")] public bool IsBigendian;
        /// <summary> Length of a point in bytes </summary>
        [DataMember (Name = "point_step")] public uint PointStep;
        /// <summary> Length of a row in bytes </summary>
        [DataMember (Name = "row_step")] public uint RowStep;
        //uint8[] data         # Actual point data, size is (row_step*height)
        /// <summary> [Rent] </summary>
        [DataMember (Name = "data")] public System.Memory<byte> Data;
        /// <summary> True if there are no invalid points </summary>
        [DataMember (Name = "is_dense")] public bool IsDense;
    
        /// Constructor for empty message.
        public PointCloud2()
        {
            Fields = System.Array.Empty<PointField>();
            Data = System.Array.Empty<byte>();
        }
        
        /// Constructor with buffer.
        public PointCloud2(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            Fields = b.DeserializeArray<PointField>();
            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i] = new PointField(ref b);
            }
            IsBigendian = b.Deserialize<bool>();
            PointStep = b.Deserialize<uint>();
            RowStep = b.Deserialize<uint>();
            Data = b.DeserializeStructRent<byte>();
            IsDense = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PointCloud2(ref b);
        
        public PointCloud2 RosDeserialize(ref ReadBuffer b) => new PointCloud2(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.SerializeArray(Fields);
            b.Serialize(IsBigendian);
            b.Serialize(PointStep);
            b.Serialize(RowStep);
            b.SerializeStructArray(Data);
            b.Serialize(IsDense);
        }
        
        public void RosValidate()
        {
            if (Fields is null) BuiltIns.ThrowNullReference(nameof(Fields));
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i] is null) BuiltIns.ThrowNullReference($"{nameof(Fields)}[{i}]");
                Fields[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 26;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Fields);
                size += Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/PointCloud2";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1158d486dd51d683ce2f1be655c3c181";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VVTW8bNxA9d3/FIDpECiQVtlPXMCAUQQ03BlInaNxTEAjcJaUlyiU3JFfu9tf3DbmU" +
                "lLaHHlrBhrSzM49vvh5n9NTqQJ0KQewVtc7IQIIaZ4xqonaW3I4eV1J3ygY8CkO90zaGJT23ummpE2M1" +
                "g7+NQlsSUuqY3bTdOd+JhBEGeIpAli0GsUBgvDguScVmDRIKKAmZpIiCwClE55XkMEG1tsKPVBtXIzgG" +
                "MmJ0A3xVaLyu4VaPFBMIU1EgyMRhoRc7rZDUCxLei3FdwQWnTWc1xg0yn4hEqFbk/F5Y/QcQLyXNdYeq" +
                "rIz+TS3whi4koucD0pAK3BZr+nCCCWexII3wFB0Kcu+dHJpElWmKTnmBBPrYUkAxnA/HOoUIeMcnRhR+" +
                "5XarndH7Nmb2MHFyOSiTF82XQYdU+iUJK1PmjQNPFC4q2nkcRw93NEdT6EqWYofFunqrBLKhNn3xAZd3" +
                "IOCHJg5elSqelWtND9mWi4dOHQuyBArzhBE4F4nJs5bIEF4cYpTd4+kfQKsBv68uJ4DylIKZ093U6AzT" +
                "tMJaZUJJVfsyEJhBdpjmJdWGh2ZdpUbd8yh8+kx5JKqqds4QPjpsa71XVmphaUYPfAoY5/Dy4odCKvPe" +
                "okk9Ymf07piUmFICi3qMKhwjvHsu/n+NwKuT/4wDbsAwHV0+M3qDZpTVS++WFDBoXNZ5wX6VS7eozjG+" +
                "mQA+/YKd+DxljHQlhked8J/8ALDUFfRc4N86sDoIo+U0KVW1+Y8/1c8ff7rFqMltF/bh2zyH6PXHiLYK" +
                "L6FKUaRC8NC2SE75lVEHZRAkul5NmxvHXoV12mvUA39oGHbLmJGGAKfosApdN1jd8C7wSn0Vj0iWLuqF" +
                "j7oZjPB/Wx1Gx19QXwZlG96kWxaaoJohahAagdB4JYK2e16z0ngEVLOnZ7diwdsrfzocpRa8J6R+7z3U" +
                "N4nGLc54lZNbAxvFUTgFyjJPti0ewwKdZwqqdxCLOZh/GGPr8twfhNeiNmkwGlQAqC856OXiDNkmaCus" +
                "K/AZ8XTGv4G1R9ykUdhIaTj7MOxRQDhC7w5aHqUZe64xg2R07bGbFUflI6vZfZKn087hW4TgGo0GsIJA" +
                "A6BIjJ66sdXyf5vGJKp5IE+SUWbr62uSk8oXUF9uSmeLqiFVP05yhPCE9SMr3eURJd+PWflu6OHx6YaX" +
                "cUMXk+XXybShy5PPxXWyXJ35sGlDr08+rDiwfHfmw6YNXU+W+3fv37BpQ9+fW65fw3JTlVpbbsokEI8i" +
                "XzpJOct0u90uqJgd3uffO+867qqP7J1LkS+T6aDUYN5YDrorv5Ud+DrM91dQkKDaHVQ5p3GDjRORt5DL" +
                "TtiRlFFduucnyc/Mqj8B9mCArNEIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
