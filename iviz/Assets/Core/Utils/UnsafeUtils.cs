using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Iviz.Core
{
    public static unsafe class UnsafeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetUnsafeRef<T>(this NativeArray<T> array) where T : unmanaged =>
            ref Unsafe.AsRef<T>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(array));
        
        /// Creates a temporary native array that lasts one frame.
        /// It should not be disposed manually. It should not be used in Burst.
        /// This is simply a wrapper for reading and writing to Unity objects such as meshes. 
        public static NativeArray<T> CreateNativeArrayWrapper<T>(in T ptr, int length) where T : unmanaged
        {
            ref T ptrRef = ref Unsafe.AsRef(in ptr);
            var array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(
                Unsafe.AsPointer(ref ptrRef), length, Allocator.None);
#if UNITY_EDITOR
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, AtomicSafetyHandle.GetTempMemoryHandle());
#endif
            return array;
        }        
    }
}