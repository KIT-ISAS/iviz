#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Iviz.Core
{
    public static unsafe class NativeArrayUtils
    {
        /// <summary>
        /// Returns the pointer enclosed in the array as a ref. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetUnsafeRef<T>(this NativeArray<T> array) where T : unmanaged => ref *GetUnsafePtr(array);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* GetUnsafePtr<T>(this NativeArray<T> array) where T : unmanaged =>
            (T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(array);

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
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, AtomicSafetyHandle.Create());
#endif
            return array;
        }

        public static NativeArray<T> AsNativeArray<T>(this T[] ptr) where T : unmanaged
        {
            return CreateNativeArrayWrapper(ref ptr[0], ptr.Length);
        }

        public static NativeArray<T> AsNativeArray<T>(this List<T> ptr) where T : unmanaged
        {
            return CreateNativeArrayWrapper(ref ptr.AsSpan()[0], ptr.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NativeArray<TU> Cast<TT, TU>(this NativeArray<TT> ptr) where TT : unmanaged where TU : unmanaged
        {
            return ptr.Reinterpret<TU>(Unsafe.SizeOf<TT>());
        }
    }
}