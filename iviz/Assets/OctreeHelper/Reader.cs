namespace Iviz.Octree
{
    internal struct Reader
    {
        readonly sbyte[] buffer;
        uint offset;

        public Reader(sbyte[] buffer, uint offset)
        {
            this.buffer = buffer;
            this.offset = offset;
        }

        public float ReadFloat()
        {
            byte b0 = (byte) buffer[offset++];
            byte b1 = (byte) buffer[offset++];
            byte b2 = (byte) buffer[offset++];
            byte b3 = (byte) buffer[offset++];

            int tmp = (b3 << 24) + (b2 << 16) + (b1 << 8) + b0;
            return Int32ToSingleBits(tmp);
        }

        static unsafe float Int32ToSingleBits(int bits) => *(float*) &bits;
        public int Size => buffer.Length;
        public void Skip(uint value) => offset += value;
        public sbyte ReadByte() => buffer[offset++];

        public ushort ReadUshort()
        {
            byte b0 = (byte) buffer[offset++];
            byte b1 = (byte) buffer[offset++];
            return (ushort) ((b0 << 0) + (b1 << 8));
        }
    }
}