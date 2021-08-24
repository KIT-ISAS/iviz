using System.Runtime.InteropServices;

namespace Iviz.Octree
{
    internal struct Reader
    {
        readonly sbyte[] buffer;
        uint offset;
        BytesToFloat helper;

        public Reader(sbyte[] buffer, uint offset)
        {
            this.buffer = buffer;
            this.offset = offset;
            helper = default;
        }

        public float ReadFloat()
        {
            helper.i0 = buffer[offset++];
            helper.i1 = buffer[offset++];
            helper.i2 = buffer[offset++];
            helper.i3 = buffer[offset++];

            return helper.f;
        }

        public int Size => buffer.Length;
        
        public void Skip(uint value) => offset += value;
        
        public sbyte ReadByte() => buffer[offset++];

        public ushort ReadUshort()
        {
            byte b0 = (byte) buffer[offset++];
            byte b1 = (byte) buffer[offset++];
            return (ushort) ((b0 << 0) + (b1 << 8));
        }

        [StructLayout(LayoutKind.Explicit)]
        struct BytesToFloat 
        {
            [FieldOffset(0)] public readonly float f;
            [FieldOffset(0)] public sbyte i0;
            [FieldOffset(1)] public sbyte i1;
            [FieldOffset(2)] public sbyte i2;
            [FieldOffset(3)] public sbyte i3;
        }     
    }
}