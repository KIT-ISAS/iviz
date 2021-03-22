using Iviz.Msgs;

namespace Iviz.Rosbag
{
    internal static class Utils
    {
        static int ToInt(this byte[] intBytes)
        {
            return intBytes[0] + (intBytes[1] << 8) + (intBytes[2] << 16) + (intBytes[3] << 24);
        }

        public static int ToInt(this Rent<byte> intBytes) => intBytes.Array.ToInt();
    }
}