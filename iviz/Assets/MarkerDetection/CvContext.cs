using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using AOT;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;
using JetBrains.Annotations;

namespace Iviz.MarkerDetection
{
    public class CvMarkerException : Exception
    {
        public CvMarkerException() : base("An error happened in the native call") {}
        public CvMarkerException(string message) : base(message)
        {
        }
    }

    public class CvContext : IDisposable
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
                    throw new CvMarkerException();
                }
            }
        }

        public CvContext(int width, int height)
        {
            if (!loggerSet)
            {
                Native.SetupDebug(Native.Debug);
                Native.SetupInfo(Native.Info);
                Native.SetupError(Native.Error);
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

        public int[] GetDetectedArucoIds()
        {
            int numDetected = Native.GetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<int>();
            }


            int[] indices = new int[numDetected];
            if (!Native.GetArucoMarkerIds(ContextPtr, indices, indices.Length))
            {
                throw new CvMarkerException();
            }

            return indices;
        }

        public string[] GetDetectedQrCodes()
        {
            int numDetected = Native.GetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<string>();
            }


            using (var pointers = new Rent<IntPtr>(numDetected))
            using (var pointerLengths = new Rent<int>(numDetected))
            {
                if (!Native.GetQrMarkerCodes(ContextPtr, pointers.Array, pointerLengths.Array, numDetected))
                {
                    throw new CvMarkerException();
                }

                string[] indices = new string[numDetected];
                for (int i = 0; i < numDetected; i++)
                {
                    indices[i] = Marshal.PtrToStringAnsi(pointers[i], pointerLengths[i]);
                }

                return indices;
            }
        }

        public ArucoMarkerPose[] EstimateArucoMarkerPoses(float markerSizeInM)
        {
            int numDetected = Native.GetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<ArucoMarkerPose>();
            }

            var markers = new ArucoMarkerPose[numDetected];

            using (var indices = new Rent<int>(numDetected))
            using (var rotations = new Rent<float>(3 * numDetected))
            using (var translations = new Rent<float>(3 * numDetected))
            {
                if (!Native.GetArucoMarkerIds(ContextPtr, indices.Array, indices.Length) ||
                    !Native.EstimateMarkerPoses(ContextPtr, markerSizeInM, rotations.Array, rotations.Length,
                        translations.Array, translations.Length))
                {
                    throw new CvMarkerException();
                }

                for (int i = 0; i < numDetected; i++)
                {
                    Vector3 translation = (
                        translations[3 * i + 0],
                        translations[3 * i + 1],
                        translations[3 * i + 2]);
                    Vector3 angleAxis = (
                        rotations[3 * i + 0],
                        rotations[3 * i + 1],
                        rotations[3 * i + 2]);
                    double angle = angleAxis.Norm;
                    var rotation = angle == 0
                        ? Quaternion.Identity
                        : Quaternion.AngleAxis(angle, angleAxis / angle);
                    markers[i] = new ArucoMarkerPose(indices[i], new Pose(translation, rotation));
                }
            }

            return markers;
        }

        public QrMarkerPose[] EstimateQrMarkerPoses(float markerSizeInM)
        {
            int numDetected = Native.GetNumDetectedMarkers(ContextPtr);
            if (numDetected < 0)
            {
                throw new CvMarkerException();
            }

            if (numDetected == 0)
            {
                return Array.Empty<QrMarkerPose>();
            }

            var markers = new QrMarkerPose[numDetected];

            using (var pointers = new Rent<IntPtr>(numDetected))
            using (var pointerLengths = new Rent<int>(numDetected))
            using (var rotations = new Rent<float>(3 * numDetected))
            using (var translations = new Rent<float>(3 * numDetected))
            {
                if (!Native.GetQrMarkerCodes(ContextPtr, pointers.Array, pointerLengths.Array, numDetected) ||
                    !Native.EstimateMarkerPoses(ContextPtr, markerSizeInM, rotations.Array, rotations.Length,
                        translations.Array, translations.Length))
                {
                    throw new CvMarkerException();
                }

                for (int i = 0; i < numDetected; i++)
                {
                    string code = Marshal.PtrToStringAnsi(pointers[i], pointerLengths[i]);
                    Vector3 translation = (
                        translations[3 * i + 0],
                        translations[3 * i + 1],
                        translations[3 * i + 2]);
                    Vector3 angleAxis = (
                        rotations[3 * i + 0],
                        rotations[3 * i + 1],
                        rotations[3 * i + 2]);
                    double angle = angleAxis.Norm;
                    var rotation = angle == 0
                        ? Quaternion.Identity
                        : Quaternion.AngleAxis(angle, angleAxis / angle);
                    markers[i] = new QrMarkerPose(code, new Pose(translation, rotation));
                }
            }

            return markers;
        }

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
                    string code = Marshal.PtrToStringAnsi(pointers[i], pointerLengths[i]);
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

        public static (float, Transform) Umeyama(Vector3f[] input, Vector3f[] output, bool estimateScale)
        {
            if (input.Length != output.Length)
            {
                throw new ArgumentException("Input and output lengths do not match");
            }

            using (var inputFloats = new Rent<float>(input.Length * 3))
            using (var outputFloats = new Rent<float>(output.Length * 3))
            using (var resultFloats = new Rent<float>(7))
            {
                int o = 0;
                foreach (var v in input)
                {
                    (inputFloats[o], inputFloats[o + 1], inputFloats[o + 2]) = v;
                    o += 3;
                }

                o = 0;
                foreach (var v in output)
                {
                    (outputFloats[o], outputFloats[o + 1], outputFloats[o + 2]) = v;
                    o += 3;
                }

                Native.EstimateUmeyama(inputFloats.Array, inputFloats.Length, outputFloats.Array, outputFloats.Length,
                    estimateScale, resultFloats.Array, resultFloats.Length);

                Vector3 translation = (resultFloats[3], resultFloats[4], resultFloats[5]);
                Vector3 angleAxis = (resultFloats[0], resultFloats[1], resultFloats[2]);
                double angle = angleAxis.Norm;
                var rotation = angle == 0
                    ? Quaternion.Identity
                    : Quaternion.AngleAxis(angle, angleAxis / angle);

                return (resultFloats[6], (translation, rotation));
            }
        }

        public static Pose SolvePnp(Vector2f[] input, Vector3f[] output, float fx, float cx, float fy, float cy)
        {
            if (input.Length != output.Length)
            {
                throw new ArgumentException("Input and output lengths do not match");
            }

            using (var inputFloats = new Rent<float>(input.Length * 2))
            using (var outputFloats = new Rent<float>(output.Length * 3))
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

                cameraFloats[0] = fx;
                cameraFloats[1] = 0;
                cameraFloats[2] = cx;
                cameraFloats[3] = 0;
                cameraFloats[4] = fy;
                cameraFloats[5] = cy;

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
            const string IvizOpencvDll = Settings.IsMobile ? "__Internal" : "IvizOpencv";

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

    public class ArucoMarkerPose
    {
        public int Id { get; }
        public Pose Pose { get; }

        public ArucoMarkerPose(int id, in Pose pose) => (Id, Pose) = (id, pose);

        public override string ToString() => "{\"Id\":" + Id + ", \"Pose\":" + Pose + "}";
    }

    public interface IMarkerCorners
    {
        ReadOnlyCollection<Vector2f> Corners { get; }
    }

    public sealed class ArucoMarkerCorners : IMarkerCorners
    {
        public int Id { get; }
        public ReadOnlyCollection<Vector2f> Corners { get; }

        public ArucoMarkerCorners(int id, Vector2f[] corners) => (Id, Corners) = (id, corners.AsReadOnly());

        public override string ToString() => "{\"Id\":" + Id + ", \"Corners\":" +
                                             string.Join(", ", Corners.Select(corner => corner.ToString())) + "}";
    }

    public sealed class QrMarkerPose
    {
        public string Code { get; }
        public Pose Pose { get; }

        public QrMarkerPose(string code, in Pose pose) => (Code, Pose) = (code, pose);

        public override string ToString() => "{\"Code\":" + Code + ", \"Pose\":" + Pose + "}";
    }

    public sealed class QrMarkerCorners : IMarkerCorners
    {
        public string Code { get; }
        public ReadOnlyCollection<Vector2f> Corners { get; }

        public QrMarkerCorners(string code, Vector2f[] corners) => (Code, Corners) = (code, corners.AsReadOnly());

        public override string ToString() => "{\"Code\":" + Code + ", \"Corners\":" +
                                             string.Join(", ", Corners.Select(corner => corner.ToString())) + "}";
    }
}