
namespace Iviz.Msgs.std_msgs
{
    public sealed class UInt32 : IMessage 
    {
        public uint data;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt32";

        public IMessage Create() => new UInt32();

        public int GetLength() => 4;

        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "304a39449588c7f8ce2df6e8001c5fce";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAEyvNzCsxNlJISSxJ5AIAYOk1nQwAAAA=";

    }
}
