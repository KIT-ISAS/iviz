using Iviz.Tools;

namespace Iviz.Rosbag
{
    internal static class Utils
    {
        static int ToInt(this byte[] intBytes)
        {
            return intBytes[0] + (intBytes[1] << 8) + (intBytes[2] << 16) + (intBytes[3] << 24);
        }

        public static int ToInt(this Rent<byte> intBytes) => intBytes.Array.ToInt();

        public static void WriteInt(this byte[] array, int value)
        {
            array[3] = (byte) (value >> 24);
            array[0] = (byte) value;
            array[1] = (byte) (value >> 8);
            array[2] = (byte) (value >> 16);
        }
    }
}