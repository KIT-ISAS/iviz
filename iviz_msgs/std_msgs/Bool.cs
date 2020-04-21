
namespace Iviz.Msgs.std_msgs
{
    public sealed class Bool : IMessage 
    {
        public bool data;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Bool";

        public IMessage Create() => new Bool();

        public int GetLength() => 1;

        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8b94c1b53db61fb6aed406028ad6332a";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE0vKz89RSEksSeQCAGFR0NcKAAAA";

    }
}
