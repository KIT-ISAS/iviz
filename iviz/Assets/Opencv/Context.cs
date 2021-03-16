using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Msgs;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Iviz.Opencv
{
    public class Context : IDisposable
    {
        static bool loggerSet;
        readonly IntPtr mContextPtr;
        readonly int imageSize;
        readonly IntPtr imagePtr;
        bool disposed;

        ArucoDictionaryName dictionaryName;

        IntPtr ContextPtr => !disposed
            ? mContextPtr
            : throw new ObjectDisposedException(nameof(mContextPtr), "Context already disposed");

        public int Width { get; }
        public int Height { get; }

        public ArucoDictionaryName DictionaryName
        {
            get => dictionaryName;
            set
            {
                dictionaryName = value;
                if (!Native.SetDictionary(ContextPtr, (int) value))
                {
                    throw new InvalidOperationException("An error happened in the native call");
                }
            }
        }

        public unsafe Context(int width, int height)
        {
            if (!loggerSet)
            {
                Native.SetupDebug(Core.Logger.Debug);
                Native.SetupInfo(Core.Logger.Debug);
                Native.SetupError(Core.Logger.Error);
                loggerSet = true;
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            try
            {
                mContextPtr = Native.CreateContext(width, height);
                Width = width;
                Height = height;
                DictionaryName = ArucoDictionaryName.DictArucoOriginal;

                imageSize = width * height * 3;
                imagePtr = Native.GetImagePtr(ContextPtr);
            }
            catch (EntryPointNotFoundException e)
            {
                Debug.LogError(e);
            }
            catch (DllNotFoundException e)
            {
                Debug.LogError(e);
            }
        }

        public unsafe void SetImageData(in NativeArray<byte> image)
        {
            if (imageSize > image.Length)
            {
                throw new InvalidOperationException("Image size is too small");
            }

            if (disposed)
            {
                throw new ObjectDisposedException(nameof(mContextPtr), "Context already disposed");
            }

            UnsafeUtility.MemCpy(imagePtr.ToPointer(), image.GetUnsafePtr(), imageSize);
        }

        public void SetImageData([NotNull] byte[] image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (Width * Height * 3 > image.Length)
            {
                throw new InvalidOperationException("Image size is too small");
            }

            Native.CopyFrom(ContextPtr, image, image.Length);
        }

        public void GetImageData([NotNull] byte[] image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (Width * Height * 3 > image.Length)
            {
                throw new InvalidOperationException("Image size is too small");
            }

            Native.CopyTo(ContextPtr, image, image.Length);
        }

        public void SetCameraMatrix(float fx, float ox, float fy, float oy)
        {
            using (var array = new Rent<float>(9))
            {
                array.Array[0] = fx;
                array.Array[1] = 0;
                array.Array[2] = ox;

                array.Array[3] = 0;
                array.Array[4] = fy;
                array.Array[5] = oy;

                array.Array[6] = 0;
                array.Array[7] = 0;
                array.Array[8] = 1;
                if (!Native.SetCameraMatrix(ContextPtr, array.Array, array.Length))
                {
                    throw new InvalidOperationException("An error happened in the native call");
                }
            }
        }

        public int DetectArucoMarkers()
        {
            Native.DetectArucoMarkers(ContextPtr);
            return Native.GetNumDetectedArucoMarkers(ContextPtr);
        }

        public IEnumerable<int> GetDetectedArucoIds()
        {
            int numDetected = Native.GetNumDetectedArucoMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new InvalidOperationException("An error happened in the native call");
            }

            if (numDetected == 0)
            {
                yield break;
            }

            using (var indices = new Rent<int>(numDetected))
            {
                if (!Native.GetArucoMarkerIds(ContextPtr, indices.Array, indices.Length))
                {
                    throw new InvalidOperationException("An error happened in the native call");
                }

                foreach (int index in indices)
                {
                    yield return index;
                }
            }
        }


        public IEnumerable<DetectedArucoMarker> EstimateMarkerPoses(float markerSize)
        {
            int numDetected = Native.GetNumDetectedArucoMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new InvalidOperationException("An error happened in the native call");
            }

            if (numDetected == 0)
            {
                yield break;
            }

            using (var indices = new Rent<int>(numDetected))
            using (var rotations = new Rent<float>(3 * numDetected))
            using (var translations = new Rent<float>(3 * numDetected))
            {
                if (!Native.GetArucoMarkerIds(ContextPtr, indices.Array, indices.Length) ||
                    !Native.EstimateArucoPose(ContextPtr, markerSize, indices.Array, indices.Length, rotations.Array,
                        rotations.Length, translations.Array, translations.Length))
                {
                    throw new InvalidOperationException("An error happened in the native call");
                }

                for (int i = 0; i < numDetected; i++)
                {
                    Vector3 translation = new Vector3(
                        translations[3 * i + 0],
                        translations[3 * i + 1],
                        translations[3 * i + 2]).Ros2Unity();
                    Vector3 angleAxis = new Vector3(
                        rotations[3 * i + 0],
                        rotations[3 * i + 1],
                        rotations[3 * i + 2]).Ros2Unity();
                    float angle = angleAxis.sqrMagnitude;
                    Quaternion rotation = Mathf.Approximately(angle, 0)
                        ? Quaternion.identity
                        : Quaternion.AngleAxis(angle, angleAxis / angle);
                    yield return new DetectedArucoMarker(indices[i], new Pose(translation, rotation));
                }
            }
        }

        void ReleaseUnmanagedResources()
        {
            Native.DisposeContext(mContextPtr);
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
        
        
        
        

        ~Context()
        {
            ReleaseUnmanagedResources();
        }

        static class Native
        {
            public delegate void Callback(string s);

            [DllImport("IvizOpencv")]
            public static extern IntPtr CreateContext(int width, int height);

            [DllImport("IvizOpencv")]
            public static extern void DisposeContext(IntPtr ctx);

            [DllImport("IvizOpencv")]
            public static extern void SetupDebug(Callback callback);

            [DllImport("IvizOpencv")]
            public static extern void SetupInfo(Callback callback);

            [DllImport("IvizOpencv")]
            public static extern void SetupError(Callback callback);


            [DllImport("IvizOpencv")]
            public static extern int ImageWidth(IntPtr ctx);

            [DllImport("IvizOpencv")]
            public static extern int ImageHeight(IntPtr ctx);

            [DllImport("IvizOpencv")]
            public static extern int ImageFormat(IntPtr ctx);

            [DllImport("IvizOpencv")]
            public static extern int ImageSize(IntPtr ctx);

            [DllImport("IvizOpencv")]
            public static extern bool CopyFrom(IntPtr ctx, /* const */ byte[] ptr, int size);

            [DllImport("IvizOpencv")]
            public static extern bool CopyTo( /* const */ IntPtr ctx, byte[] ptr, int size);

            [DllImport("IvizOpencv")]
            public static extern IntPtr GetImagePtr(IntPtr ctx);

            [DllImport("IvizOpencv")]
            public static extern bool SetDictionary(IntPtr ctx, int value);

            [DllImport("IvizOpencv")]
            public static extern bool DetectArucoMarkers(IntPtr ctx);

            [DllImport("IvizOpencv")]
            public static extern int GetNumDetectedArucoMarkers(IntPtr ctx);

            [DllImport("IvizOpencv")]
            public static extern bool GetArucoMarkerIds(IntPtr ctx, int[] arrayPtr, int arraySize);

            [DllImport("IvizOpencv")]
            public static extern bool GetArucoMarkerCorners(IntPtr ctx, float[] arrayPtr, int arraySize);

            [DllImport("IvizOpencv")]
            public static extern bool SetCameraMatrix(IntPtr ctx, float[] arrayPtr, int arraySize);

            [DllImport("IvizOpencv")]
            public static extern bool EstimateArucoPose(IntPtr ctx, float markerSize, int[] markerIndices,
                int markerIndicesLength, float[] rotations, int rotationsSize, float[] translations,
                int translationsSize);
        }
    }

    public enum ArucoDictionaryName
    {
        Dict4X450 = 0,
        Dict4X4100,
        Dict4X4250,
        Dict4X41000,
        Dict5X550,
        Dict5X5100,
        Dict5X5250,
        Dict5X51000,
        Dict6X650,
        Dict6X6100,
        Dict6X6250,
        Dict6X61000,
        Dict7X750,
        Dict7X7100,
        Dict7X7250,
        Dict7X71000,
        DictArucoOriginal,

        /// 4x4 bits, minimum hamming distance between any two codes = 5, 30 codes
        DictApriltag16H5,

        /// 5x5 bits, minimum hamming distance between any two codes = 9, 35 codes
        DictApriltag25H9,

        /// 6x6 bits, minimum hamming distance between any two codes = 10, 2320 codes
        DictApriltag36H10,

        /// 6x6 bits, minimum hamming distance between any two codes = 11, 587 codes
        DictApriltag36H11
    };

    public readonly struct DetectedArucoMarker
    {
        public int Id { get; }
        public Pose Pose { get; }

        public DetectedArucoMarker(int id, in Pose pose)
        {
            Id = id;
            Pose = pose;
        }
    }

    static class OpencvUtils
    {
        static Vector3 OpencvToUnity(this Vector3 p)
        {
            return new Vector3(p.x, -p.y, p.z);
        }

        static Quaternion OpencvToUnity(this Quaternion q)
        {
            return new Quaternion(q.x, -q.y, q.z, -q.w);
        }
    }
}