using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;

namespace Iviz.Msgs;

/// <summary>
/// These are operations used by Unity, as a walk-around that some generics operations
/// are slower in AOT and pointer operations are really fast in il2cpp.
/// Here we assume that there is no GC compacting and pinning spans is a free operation. 
/// </summary>
public static class BufferExtensions
{
#if NETSTANDARD2_1
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref byte AsRef(this in byte ptr)
    {
        fixed (byte *pPtr = &ptr) return ref *pPtr;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out byte t)
    {
        t = buffer.GetRefAndAdvance(sizeof(byte));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out sbyte t)
    {
        t = (sbyte) buffer.GetRefAndAdvance(sizeof(sbyte));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out bool t)
    {
        t = Unsafe.As<byte, bool>(ref buffer.GetRefAndAdvance(sizeof(bool)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out short t)
    {
        t = Unsafe.ReadUnaligned<short>(ref buffer.GetRefAndAdvance(sizeof(short)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out ushort t)
    {
        t = Unsafe.ReadUnaligned<ushort>(ref buffer.GetRefAndAdvance(sizeof(ushort)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out int t)
    {
        t = Unsafe.ReadUnaligned<int>(ref buffer.GetRefAndAdvance(sizeof(int)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out uint t)
    {
        t = Unsafe.ReadUnaligned<uint>(ref buffer.GetRefAndAdvance(sizeof(uint)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out long t)
    {
        t = Unsafe.ReadUnaligned<long>(ref buffer.GetRefAndAdvance(sizeof(long)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out ulong t)
    {
        t = Unsafe.ReadUnaligned<ulong>(ref buffer.GetRefAndAdvance(sizeof(ulong)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out float t)
    {
        t = Unsafe.ReadUnaligned<float>(ref buffer.GetRefAndAdvance(sizeof(float)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out double t)
    {
        t = Unsafe.ReadUnaligned<double>(ref buffer.GetRefAndAdvance(sizeof(double)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out time t)
    {
        t = Unsafe.ReadUnaligned<time>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<time>()));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out duration t)
    {
        t = Unsafe.ReadUnaligned<duration>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<duration>()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Vector3 t)
    {
        t = Unsafe.ReadUnaligned<Vector3>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Vector3>()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Point t)
    {
        t = Unsafe.ReadUnaligned<Point>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Point>()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Quaternion t)
    {
        t = Unsafe.ReadUnaligned<Quaternion>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Quaternion>()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Pose t)
    {
        t = Unsafe.ReadUnaligned<Pose>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Pose>()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Transform t)
    {
        t = Unsafe.ReadUnaligned<Transform>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Transform>()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out ColorRGBA t)
    {
        t = Unsafe.ReadUnaligned<ColorRGBA>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<ColorRGBA>()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Point32 t)
    {
        t = Unsafe.ReadUnaligned<Point32>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Point32>()));
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Vector3f t)
    {
        t = Unsafe.ReadUnaligned<Vector3f>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Vector3f>()));
    }    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Color32 t)
    {
        t = Unsafe.ReadUnaligned<Color32>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Color32>()));
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Vector2f t)
    {
        t = Unsafe.ReadUnaligned<Vector2f>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Vector2f>()));
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deserialize(this ref ReadBuffer buffer, out Triangle t)
    {
        t = Unsafe.ReadUnaligned<Triangle>(ref buffer.GetRefAndAdvance(Unsafe.SizeOf<Triangle>()));
    }
#endif
}