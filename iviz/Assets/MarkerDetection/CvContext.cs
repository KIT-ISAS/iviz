#nullable enable

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Tools;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using Quaternion = Iviz.Msgs.GeometryMsgs.Quaternion;
using Vector2f = UnityEngine.Vector2;
using Vector3 = Iviz.Msgs.GeometryMsgs.Vector3;
using Vector3f = UnityEngine.Vector3;

namespace Iviz.MarkerDetection
{
    public sealed class CvContext : IDisposable
    {
        static bool loggerSet;
        readonly IntPtr mContextPtr;
        readonly IntPtr imagePtr;
        readonly int imageSize;
        readonly int width;
        readonly int height;
        bool disposed;

        IntPtr ContextPtr => !disposed
            ? mContextPtr
            : throw new ObjectDisposedException(nameof(mContextPtr), "Context already disposed");

        Span<byte> ImageSpan => !disposed
            ? imagePtr.AsSpan(width * height * 3)
            : throw new ObjectDisposedException(nameof(mContextPtr), "Context already disposed");

        ArucoDictionaryName DictionaryName
        {
            set
            {
                if (!CvNative.IvizSetDictionary(ContextPtr, (int)value))
                {
                    throw new CvMarkerException();
                }
            }
        }

        public CvContext(int width, int height)
        {
            if (width <= 0)
            {
                ThrowHelper.ThrowArgumentOutOfRange(nameof(width));
            }

            if (height <= 0)
            {
                ThrowHelper.ThrowArgumentOutOfRange(nameof(width));
            }

            // these functions will fail if the OpenCV library is not present
            // only Dispose() may be called if this throws
            try
            {
                if (!loggerSet)
                {
                    CvNative.IvizSetupDebug(CvNative.Debug);
                    CvNative.IvizSetupInfo(CvNative.Info);
                    CvNative.IvizSetupError(CvNative.Error);
                    loggerSet = true;
                }

                mContextPtr = CvNative.IvizCreateContext(width, height);
                this.width = width;
                this.height = height;
                DictionaryName = ArucoDictionaryName.DictArucoOriginal;

                imageSize = width * height * 3;
                imagePtr = CvNative.IvizGetImagePtr(ContextPtr);
            }
            catch (Exception e) when (e is EntryPointNotFoundException or DllNotFoundException)
            {
                throw new CvNotAvailableException(e);
            }
        }

        public bool MatchesSize(int otherWidth, int otherHeight) => (otherWidth, otherHeight) == (width, height);

        public void SetImageData(ReadOnlySpan<byte> image)
        {
            var span = ImageSpan;
            if (span.Length > image.Length)
            {
                ThrowHelper.ThrowArgument("Image size is too small", nameof(image));
            }

            image.CopyTo(span);
        }

        public void SetImageDataFlipY(byte[] image, int bpp)
        {
            if (imageSize > image.Length)
            {
                ThrowHelper.ThrowArgument("Image size is too small", nameof(image));
            }

            if (disposed)
            {
                throw new ObjectDisposedException(nameof(mContextPtr), "Context already disposed");
            }

            if (bpp != 3)
            {
                return;
            }

            int stride = width * 3;
            for (int v = 0; v < height; v++)
            {
                Marshal.Copy(image, stride * v, imagePtr + stride * (height - v - 1), stride);
            }
        }

        public DetectedMarker[] DetectQrMarkers()
        {
            CvNative.IvizDetectQrMarkers(ContextPtr);

            int numDetected = CvNative.IvizGetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<DetectedMarker>();
            }

            using var pointers = new Rent<IntPtr>(numDetected);
            using var pointerLengths = new Rent<int>(numDetected);
            using var corners = new Rent<float>(8 * numDetected);

            if (!CvNative.IvizGetQrMarkerCodes(ContextPtr, ref pointers.Array[0], ref pointerLengths.Array[0],
                    numDetected)
                || !CvNative.IvizGetMarkerCorners(ContextPtr, ref corners.Array[0], corners.Length))
            {
                throw new CvMarkerException();
            }

            var srcCorners = MemoryMarshal.Cast<float, Vector2f>(corners);

            var markers = new DetectedMarker[numDetected];
            for (int i = 0; i < numDetected; i++)
            {
                var strBytes = pointers[i].AsSpan(pointerLengths[i]);
                string code = BuiltIns.UTF8.GetString(strBytes);
                var vectorCorners = new Vector2f[4];
                srcCorners.Slice(4 * i, 4).CopyTo(vectorCorners);
                markers[i] = new DetectedMarker(code, vectorCorners);
            }

            return markers;
        }

        public DetectedMarker[] DetectArucoMarkers()
        {
            CvNative.IvizDetectArucoMarkers(ContextPtr);

            int numDetected = CvNative.IvizGetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<DetectedMarker>();
            }

            using var indices = new Rent<int>(numDetected);
            using var corners = new Rent<float>(8 * numDetected);

            var srcCorners = MemoryMarshal.Cast<float, Vector2f>(corners.AsReadOnlySpan());

            if (!CvNative.IvizGetArucoMarkerIds(ContextPtr, ref indices.Array[0], indices.Length) ||
                !CvNative.IvizGetMarkerCorners(ContextPtr, ref corners.Array[0], corners.Length))
            {
                throw new CvMarkerException();
            }

            var markers = new DetectedMarker[numDetected];
            for (int i = 0; i < numDetected; i++)
            {
                var vectorCorners = new Vector2f[4];
                srcCorners.Slice(4 * i, 4).CopyTo(vectorCorners);
                markers[i] = new DetectedMarker(indices[i], vectorCorners);
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

            if (!CvNative.IvizEstimatePnp(in inputFloats[0], inputFloats.Length,
                    in outputFloats[0], outputFloats.Length,
                    in cameraFloats[0], cameraFloats.Length,
                    ref resultFloats[0], resultFloats.Length))
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
            if (mContextPtr != IntPtr.Zero)
            {
                CvNative.IvizDisposeContext(mContextPtr);
            }
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

    [DataContract]
    public sealed class DetectedMarker
    {
        [DataMember] public string Code { get; }
        [DataMember] public Vector2f[] Corners { get; }
        [DataMember] public ARMarkerType Type { get; }

        DetectedMarker(string code, Vector2f[] corners, ARMarkerType type)
        {
            Code = code;
            Corners = corners;
            Type = type;
        }
        
        public DetectedMarker(int id, Vector2f[] corners) : this(id.ToString(), corners, ARMarkerType.Aruco)
        {
        }
        
        public DetectedMarker(string code, Vector2f[] corners) : this(code, corners, ARMarkerType.QrCode)
        {
        }
    }
}