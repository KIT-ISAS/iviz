
namespace Iviz.Msgs.std_msgs
{
    public sealed class UInt64 : IMessage 
    {
        public ulong data;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt64";

        public IMessage Create() => new UInt64();

        public int GetLength() => 8;

        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1b2a79973e8bf53d7b53acb71299cb57";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAEyvNzCsxM1FISSxJ5AIAPtIFtgwAAAA=";

    }
}
