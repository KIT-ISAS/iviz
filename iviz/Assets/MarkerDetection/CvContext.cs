using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using AOT;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib.Utils;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.MarkerDetection
{
    public sealed class CvMarkerException : Exception
    {
        public CvMarkerException() : base("An error happened in the native call")
        {
        }

        public CvMarkerException(string message) : base(message)
        {
        }
    }

    public sealed class CvContext : IDisposable
    {
        static bool loggerSet;
        readonly IntPtr mContextPtr;
        readonly IntPtr imagePtr;
        readonly int imageSize;
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
                    throw new CvMarkerException();
                }
            }
        }

        public CvContext(int width, int height)
        {
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
                if (!loggerSet)
                {
                    Native.SetupDebug(Native.Debug);
                    Native.SetupInfo(Native.Info);
                    Native.SetupError(Native.Error);
                    loggerSet = true;
                }


                mContextPtr = Native.CreateContext(width, height);
                Width = width;
                Height = height;
                DictionaryName = ArucoDictionaryName.DictArucoOriginal;

                imageSize = width * height * 3;
                imagePtr = Native.GetImagePtr(ContextPtr);
            }
            catch (EntryPointNotFoundException e)
            {
                UnityEngine.Debug.LogError(e);
            }
            catch (DllNotFoundException e)
            {
                UnityEngine.Debug.LogError(e);
            }
        }

        public void SetImageData([NotNull] byte[] image, int bpp)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (bpp != 3)
            {
                return;
            }

            if (disposed)
            {
                throw new ObjectDisposedException(nameof(mContextPtr), "Context already disposed");
            }

            if (Width * Height * bpp > image.Length)
            {
                throw new ArgumentException("Image size is too small", nameof(image));
            }


            Marshal.Copy(image, 0, imagePtr, Width * Height * 3);
        }

        public void SetImageDataFlipY([NotNull] byte[] image, int bpp)
        {
            if (imageSize > image.Length)
            {
                throw new ArgumentException("Image size is too small", nameof(image));
            }

            if (disposed)
            {
                throw new ObjectDisposedException(nameof(mContextPtr), "Context already disposed");
            }

            if (bpp != 3)
            {
                return;
            }

            int stride = Width * 3;
            for (int v = 0; v < Height; v++)
            {
                Marshal.Copy(image, stride * v, imagePtr + stride * (Height - v - 1), stride);
            }
        }

        public void GetImageData([NotNull] byte[] image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (Width * Height * 3 > image.Length)
            {
                throw new ArgumentException("Image size is too small", nameof(image));
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
                    throw new CvMarkerException();
                }
            }
        }

        public int DetectArucoMarkers()
        {
            Native.DetectArucoMarkers(ContextPtr);
            return Native.GetNumDetectedMarkers(ContextPtr);
        }

        public int DetectQrMarkers()
        {
            Native.DetectQrMarkers(ContextPtr);
            return Native.GetNumDetectedMarkers(ContextPtr);
        }

        [NotNull]
        public QrMarkerCorners[] GetDetectedQrCorners()
        {
            int numDetected = Native.GetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<QrMarkerCorners>();
            }

            var markers = new QrMarkerCorners[numDetected];

            using (var pointers = new Rent<IntPtr>(numDetected))
            using (var pointerLengths = new Rent<int>(numDetected))
            using (var corners = new Rent<float>(8 * numDetected))
            {
                if (!Native.GetQrMarkerCodes(ContextPtr, pointers.Array, pointerLengths.Array, numDetected) ||
                    !Native.GetMarkerCorners(ContextPtr, corners.Array, corners.Length))
                {
                    throw new CvMarkerException();
                }

                int o = 0;
                for (int i = 0; i < numDetected; i++)
                {
                    string code;
                    using (var strBytes = new Rent<byte>(pointerLengths[i]))
                    {
                        Marshal.Copy(pointers[i], strBytes.Array, 0, strBytes.Length);
                        code = BuiltIns.UTF8.GetString(strBytes.Array, 0, strBytes.Length);
                    }

                    markers[i] = new QrMarkerCorners(code, new Vector2f[]
                    {
                        (corners[o++], corners[o++]),
                        (corners[o++], corners[o++]),
                        (corners[o++], corners[o++]),
                        (corners[o++], corners[o++]),
                    });
                }
            }

            return markers;
        }

        [NotNull]
        public ArucoMarkerCorners[] GetDetectedArucoCorners()
        {
            int numDetected = Native.GetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<ArucoMarkerCorners>();
            }

            var markers = new ArucoMarkerCorners[numDetected];

            using (var indices = new Rent<int>(numDetected))
            using (var corners = new Rent<float>(8 * numDetected))
            {
                if (!Native.GetArucoMarkerIds(ContextPtr, indices.Array, indices.Length) ||
                    !Native.GetMarkerCorners(ContextPtr, corners.Array, corners.Length))
                {
                    throw new CvMarkerException();
                }

                int o = 0;
                for (int i = 0; i < numDetected; i++)
                {
                    markers[i] = new ArucoMarkerCorners(indices[i], new Vector2f[]
                    {
                        (corners[o++], corners[o++]),
                        (corners[o++], corners[o++]),
                        (corners[o++], corners[o++]),
                        (corners[o++], corners[o++]),
                    });
                }
            }

            return markers;
        }

        public static Pose SolvePnp([NotNull] IReadOnlyList<Vector2f> input,
            [NotNull] IReadOnlyList<Vector3f> output,
            in Intrinsic intrinsic,
            SolvePnPMethod method = SolvePnPMethod.Iterative)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (input.Count != output.Count)
            {
                throw new ArgumentException("Input and output lengths do not match");
            }

            using (var inputFloats = new Rent<float>(input.Count * 2))
            using (var outputFloats = new Rent<float>(output.Count * 3))
            using (var cameraFloats = new Rent<float>(6))
            using (var resultFloats = new Rent<float>(6))
            {
                int o = 0;
                foreach (var v in input)
                {
                    (inputFloats[o], inputFloats[o + 1]) = v;
                    o += 2;
                }

                o = 0;
                foreach (var v in output)
                {
                    (outputFloats[o], outputFloats[o + 1], outputFloats[o + 2]) = v;
                    o += 3;
                }

                cameraFloats[0] = intrinsic.Fx;
                cameraFloats[1] = 0;
                cameraFloats[2] = intrinsic.Cx;
                cameraFloats[3] = 0;
                cameraFloats[4] = intrinsic.Fy;
                cameraFloats[5] = intrinsic.Cy;

                if (!Native.EstimatePnp(inputFloats.Array, inputFloats.Length, outputFloats.Array, outputFloats.Length,
                    cameraFloats.Array, cameraFloats.Length, resultFloats.Array, resultFloats.Length))
                {
                    throw new CvMarkerException();
                }

                Vector3 translation = (resultFloats[3], resultFloats[4], resultFloats[5]);
                Vector3 angleAxis = (resultFloats[0], resultFloats[1], resultFloats[2]);
                double angle = angleAxis.Norm;
                var rotation = angle == 0
                    ? Quaternion.Identity
                    : Quaternion.AngleAxis(angle, angleAxis / angle);

                return (translation, rotation);
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


        ~CvContext()
        {
            ReleaseUnmanagedResources();
        }

        static class Native
        {
            const string IvizOpencvDll =
                Settings.IsIphone
                    ? "__Internal"
                    : Settings.IsAndroid
                        ? "ivizopencv-lib"
                        : "IvizOpencv";

            public delegate void Callback(string s);

            [MonoPInvokeCallback(typeof(Callback))]
            public static void Debug([CanBeNull] string t)
            {
                UnityEngine.Debug.Log(t);
            }

            [MonoPInvokeCallback(typeof(Callback))]
            public static void Info([CanBeNull] string t)
            {
                UnityEngine.Debug.LogWarning(t);
            }

            [MonoPInvokeCallback(typeof(Callback))]
            public static void Error([CanBeNull] string t)
            {
                UnityEngine.Debug.LogError(t);
            }

            [DllImport(IvizOpencvDll)]
            public static extern IntPtr CreateContext(int width, int height);

            [DllImport(IvizOpencvDll)]
            public static extern void DisposeContext(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern void SetupDebug(Callback callback);

            [DllImport(IvizOpencvDll)]
            public static extern void SetupInfo(Callback callback);

            [DllImport(IvizOpencvDll)]
            public static extern void SetupError(Callback callback);


            [DllImport(IvizOpencvDll)]
            public static extern int ImageWidth(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern int ImageHeight(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern int ImageFormat(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern int ImageSize(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern bool CopyFrom(IntPtr ctx, /* const */ byte[] ptr, int size);

            [DllImport(IvizOpencvDll)]
            public static extern bool CopyTo( /* const */ IntPtr ctx, byte[] ptr, int size);

            [DllImport(IvizOpencvDll)]
            public static extern IntPtr GetImagePtr(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern bool SetDictionary(IntPtr ctx, int value);

            [DllImport(IvizOpencvDll)]
            public static extern bool DetectArucoMarkers(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern bool DetectQrMarkers(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern int GetNumDetectedMarkers(IntPtr ctx);

            [DllImport(IvizOpencvDll)]
            public static extern bool GetArucoMarkerIds(IntPtr ctx, int[] arrayPtr, int arraySize);

            [DllImport(IvizOpencvDll)]
            public static extern bool GetQrMarkerCodes(IntPtr ctx, IntPtr[] arrayPtr, int[] arrayLengths,
                int arraySize);

            [DllImport(IvizOpencvDll)]
            public static extern bool GetMarkerCorners(IntPtr ctx, float[] arrayPtr, int arraySize);

            [DllImport(IvizOpencvDll)]
            public static extern bool SetCameraMatrix(IntPtr ctx, float[] arrayPtr, int arraySize);

            [DllImport(IvizOpencvDll)]
            public static extern bool EstimateMarkerPoses(IntPtr ctx, float markerSize, float[] rotations,
                int rotationsSize, float[] translations, int translationsSize);

            [DllImport(IvizOpencvDll)]
            public static extern bool EstimatePnp(float[] inputs, int inputSize, float[] outputs, int outputSize,
                float[] cameraArray, int cameraArraySize, float[] result, int resultSize);

            [DllImport(IvizOpencvDll)]
            public static extern bool EstimateUmeyama(float[] inputs, int inputSize, float[] outputs, int outputSize,
                bool estimateScale, float[] result, int resultSize);
        }
    }

    public enum SolvePnPMethod
    {
        Iterative = 0,

        /// EPnP: Efficient Perspective-n-Point Camera Pose Estimation @cite lepetit2009epnp
        Epnp = 1,

        /// Complete Solution Classification for the Perspective-Three-Point Problem @cite gao2003complete
        P3P = 2,

        /// A Direct Least-Squares (DLS) Method for PnP @cite hesch2011direct
        /// **Broken implementation. Using this flag will fallback to EPnP.**
        DLS = 3,

        /// Exhaustive Linearization for Robust Camera Pose and Focal Length Estimation @cite penate2013exhaustive
        /// **Broken implementation. Using this flag will fallback to EPnP.** \n
        UPnP = 4,

        /// An Efficient Algebraic Solution to the Perspective-Three-Point Problem @cite Ke17
        AP3P = 5,

        /// Infinitesimal Plane-Based Pose Estimation.        
        /// Object points must be coplanar.
        IPPE = 6,

        /// Infinitesimal Plane-Based Pose Estimation.
        /// This is a special case suitable for marker pose estimation.
        /// 4 coplanar object points must be defined in the following order:
        ///   - point 0: [-squareLength / 2,  squareLength / 2, 0]
        ///   - point 1: [ squareLength / 2,  squareLength / 2, 0]
        ///   - point 2: [ squareLength / 2, -squareLength / 2, 0]
        ///   - point 3: [-squareLength / 2, -squareLength / 2, 0]
        IPPESquare = 7,

        /// SQPnP: A Consistently Fast and Globally OptimalSolution to the Perspective-n-Point Problem @cite Terzakis20
        SQPnP = 8,
    };

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

    public interface IMarkerCorners
    {
        ReadOnlyCollection<Vector2f> Corners { get; }
        [NotNull] string Code { get; }
        ARMarkerType Type { get; }
    }

    public sealed class ArucoMarkerCorners : IMarkerCorners
    {
        public ARMarkerType Type => ARMarkerType.Aruco;
        public int Id { get; }
        public string Code => Id.ToString();
        [NotNull] public ReadOnlyCollection<Vector2f> Corners { get; }

        internal ArucoMarkerCorners(int id, [NotNull] IList<Vector2f> corners) =>
            (Id, Corners) = (id, corners.AsReadOnly());

        [NotNull]
        public override string ToString() => "{\"Id\":" + Id + ", \"Corners\":" +
                                             string.Join(", ", Corners.Select(corner => corner.ToString())) + "}";
    }

    public sealed class QrMarkerCorners : IMarkerCorners
    {
        public ARMarkerType Type => ARMarkerType.QrCode;
        public string Code { get; }
        public ReadOnlyCollection<Vector2f> Corners { get; }

        internal QrMarkerCorners(string code, [NotNull] IList<Vector2f> corners) =>
            (Code, Corners) = (code, corners.AsReadOnly());

        [NotNull]
        public override string ToString() => "{\"Code\":" + Code + ", \"Corners\":" +
                                             string.Join(", ", Corners.Select(corner => corner.ToString())) + "}";
    }
}