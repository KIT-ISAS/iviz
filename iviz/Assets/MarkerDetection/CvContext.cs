#nullable enable

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using AOT;
using Iviz.Common;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Tools;
using Newtonsoft.Json;

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
                if (!Native.SetDictionary(ContextPtr, (int)value))
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

        public void SetImageData(byte[] image, int bpp)
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

        public void SetImageDataFlipY(byte[] image, int bpp)
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

        public QrMarkerCorners[] DetectQrMarkers()
        {
            Native.DetectQrMarkers(ContextPtr);

            int numDetected = Native.GetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<QrMarkerCorners>();
            }

            using var pointers = new Rent<IntPtr>(numDetected);
            using var pointerLengths = new Rent<int>(numDetected);
            using var corners = new Rent<float>(8 * numDetected);

            if (!Native.GetQrMarkerCodes(ContextPtr, ref pointers.Array[0], ref pointerLengths.Array[0], numDetected)
                || !Native.GetMarkerCorners(ContextPtr, ref corners.Array[0], corners.Length))
            {
                throw new CvMarkerException();
            }

            var srcCorners = MemoryMarshal.Cast<float, Vector2f>(corners);

            var markers = new QrMarkerCorners[numDetected];
            foreach (int i in ..numDetected)
            {
                /*
                using (var strBytes = new Rent<byte>(pointerLengths[i]))
                {
                    
                    Marshal.Copy(pointers[i], strBytes.Array, 0, strBytes.Length);
                    code = BuiltIns.UTF8.GetString(strBytes.Array, 0, strBytes.Length);
                }
                */

                ReadOnlySpan<byte> strBytes;
                unsafe
                {
                    strBytes = new ReadOnlySpan<byte>(pointers[i].ToPointer(), pointerLengths[i]);
                }

                string code = BuiltIns.UTF8.GetString(strBytes);
                var vectorCorners = new Vector2f[4];
                srcCorners.Slice(4 * i, 4).CopyTo(vectorCorners);
                markers[i] = new QrMarkerCorners(code, vectorCorners);
            }

            return markers;
        }

        public ArucoMarkerCorners[] DetectArucoMarkers()
        {
            Native.DetectArucoMarkers(ContextPtr);

            int numDetected = Native.GetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<ArucoMarkerCorners>();
            }

            using var indices = new Rent<int>(numDetected);
            using var corners = new Rent<float>(8 * numDetected);

            var srcCorners = MemoryMarshal.Cast<float, Vector2f>(corners.AsReadOnlySpan());

            if (!Native.GetArucoMarkerIds(ContextPtr, ref indices.Array[0], indices.Length) ||
                !Native.GetMarkerCorners(ContextPtr, ref corners.Array[0], corners.Length))
            {
                throw new CvMarkerException();
            }

            var markers = new ArucoMarkerCorners[numDetected];
            foreach (int i in ..numDetected)
            {
                var vectorCorners = new Vector2f[4];
                srcCorners.Slice(4 * i, 4).CopyTo(vectorCorners);
                markers[i] = new ArucoMarkerCorners(indices[i], vectorCorners);
            }

            return markers;
        }

        public static Pose SolvePnp(ReadOnlySpan<Vector2f> input, ReadOnlySpan<Vector3f> output, Intrinsic intrinsic)
        {
            if (input.Length != output.Length)
            {
                throw new ArgumentException("Input and output lengths do not match");
            }

            var inputFloats = MemoryMarshal.Cast<Vector2f, float>(input);
            var outputFloats = MemoryMarshal.Cast<Vector3f, float>(output);

            Span<float> cameraFloats = stackalloc float[6];
            Span<float> resultFloats = stackalloc float[6];

            intrinsic.CopyTo(cameraFloats);

            if (!Native.EstimatePnp(in inputFloats.GetPinnableReference(), inputFloats.Length,
                    in outputFloats.GetPinnableReference(), outputFloats.Length,
                    in cameraFloats.GetPinnableReference(), cameraFloats.Length,
                    ref resultFloats.GetPinnableReference(), resultFloats.Length))
            {
                throw new CvMarkerException();
            }

            var angleAxis = new Vector3(resultFloats[0], resultFloats[1], resultFloats[2]);
            var translation = new Point(resultFloats[3], resultFloats[4], resultFloats[5]);

            double angle = angleAxis.Norm;
            var rotation = angle == 0
                ? Quaternion.Identity
                : Quaternion.AngleAxis(angle, angleAxis / angle);

            return new Pose(translation, rotation);
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
                Settings.IsIPhone
                    ? "__Internal"
                    : Settings.IsAndroid
                        ? "ivizopencv-lib"
                        : "IvizOpencv";

            public delegate void Callback(string s);

            [MonoPInvokeCallback(typeof(Callback))]
            public static void Debug(string? t)
            {
                UnityEngine.Debug.Log(t);
            }

            [MonoPInvokeCallback(typeof(Callback))]
            public static void Info(string? t)
            {
                UnityEngine.Debug.LogWarning(t);
            }

            [MonoPInvokeCallback(typeof(Callback))]
            public static void Error(string? t)
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
            public static extern bool CopyTo( /* const */ IntPtr ctx, ref byte ptr, int size);

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
            public static extern bool GetArucoMarkerIds(IntPtr ctx, ref int arrayPtr, int arraySize);

            [DllImport(IvizOpencvDll)]
            public static extern bool GetQrMarkerCodes(IntPtr ctx, ref IntPtr arrayPtr, ref int arrayLengths,
                int arraySize);

            [DllImport(IvizOpencvDll)]
            public static extern bool GetMarkerCorners(IntPtr ctx, ref float arrayPtr, int arraySize);

            [DllImport(IvizOpencvDll)]
            public static extern bool SetCameraMatrix(IntPtr ctx, float[] arrayPtr, int arraySize);

            [DllImport(IvizOpencvDll)]
            public static extern bool EstimateMarkerPoses(IntPtr ctx, float markerSize, float[] rotations,
                int rotationsSize, float[] translations, int translationsSize);

            [DllImport(IvizOpencvDll)]
            public static extern bool EstimatePnp(in float inputs, int inputSize, in float outputs, int outputSize,
                in float cameraArray, int cameraArraySize, ref float result, int resultSize);

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
        Vector2f[] Corners { get; }
        string Code { get; }
        ARMarkerType Type { get; }
    }

    [DataContract]
    public sealed class ArucoMarkerCorners : IMarkerCorners
    {
        [DataMember] public ARMarkerType Type => ARMarkerType.Aruco;
        [DataMember] public string Code { get; }
        [DataMember] public Vector2f[] Corners { get; }

        internal ArucoMarkerCorners(int id, Vector2f[] corners) => (Code, Corners) = (id.ToString(), corners);
        public override string ToString() => BuiltIns.ToJsonString(this, false);
    }

    [DataContract]
    public sealed class QrMarkerCorners : IMarkerCorners
    {
        [DataMember] public ARMarkerType Type => ARMarkerType.QrCode;
        [DataMember] public string Code { get; }
        [DataMember] public Vector2f[] Corners { get; }

        internal QrMarkerCorners(string code, Vector2f[] corners) => (Code, Corners) = (code, corners);
        public override string ToString() => BuiltIns.ToJsonString(this, false);
    }
}