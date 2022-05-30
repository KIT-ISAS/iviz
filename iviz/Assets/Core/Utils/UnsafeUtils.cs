using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Iviz.Core
{
    public static unsafe class UnsafeUtils
    {
        /// <summary>
        /// Returns the pointer enclosed in the array as a ref. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetUnsafeRef<T>(this NativeArray<T> array) where T : unmanaged =>
            ref *(T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(array);

        /// <summary>
        /// Creates a temporary native array that lasts one frame.
        /// It should not be disposed manually. It should not be used in Burst.
        /// This is simply a wrapper for reading and writing to Unity objects such as meshes. 
        /// </summary>
        public static NativeArray<T> CreateNativeArrayWrapper<T>(ref T ptr, int length) where T : unmanaged
        {
            var array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(
                Unsafe.AsPointer(ref ptr), length, Allocator.None);
#if UNITY_EDITOR
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, AtomicSafetyHandle.GetTempMemoryHandle());
#endif
            return array;
        }

        public static NativeArray<T> CreateNativeArrayWrapper<T>(this Span<T> ptr) where T : unmanaged
        {
            return CreateNativeArrayWrapper(ref ptr[0], ptr.Length);
        }

        /// <summary>
        /// Creates a span from the given pointer and size. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<byte> AsSpan(this IntPtr ptr, int size) => new(ptr.ToPointer(), size);
    }
}