#nullable enable
using System;
using System.Runtime.InteropServices;
using Iviz.Core;

namespace Iviz.MarkerDetection
{
#if UNITY_IOS || UNITY_ANDROID
    public static class CvNative
    {
        public const bool IsEnabled = true;

        const string Library =
            Settings.IsIPhone
                ? "__Internal"
                : "iviz_opencv";

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

        [DllImport(Library)]
        public static extern IntPtr IvizCreateContext(int width, int height);

        [DllImport(Library)]
        public static extern void IvizDisposeContext(IntPtr ctx);

        [DllImport(Library)]
        public static extern void IvizSetupDebug(Callback callback);

        [DllImport(Library)]
        public static extern void IvizSetupInfo(Callback callback);

        [DllImport(Library)]
        public static extern void IvizSetupError(Callback callback);

        [DllImport(Library)]
        public static extern IntPtr IvizGetImagePtr(IntPtr ctx);

        [DllImport(Library)]
        public static extern bool IvizSetDictionary(IntPtr ctx, int value);

        [DllImport(Library)]
        public static extern bool IvizDetectArucoMarkers(IntPtr ctx);

        [DllImport(Library)]
        public static extern bool IvizDetectQrMarkers(IntPtr ctx);

        [DllImport(Library)]
        public static extern int IvizGetNumDetectedMarkers(IntPtr ctx);

        [DllImport(Library)]
        public static extern bool IvizGetArucoMarkerIds(IntPtr ctx, ref int arrayPtr, int arraySize);

        [DllImport(Library)]
        public static extern bool IvizGetQrMarkerCodes(IntPtr ctx, ref IntPtr arrayPtr, ref int arrayLengths,
            int arraySize);

        [DllImport(Library)]
        public static extern bool IvizGetMarkerCorners(IntPtr ctx, ref float arrayPtr, int arraySize);

        [DllImport(Library)]
        public static extern bool IvizEstimatePnp(in float inputs, int inputSize, in float outputs, int outputSize,
            in float cameraArray, int cameraArraySize, ref float result, int resultSize);
    }
#else
    internal static class CvNative
    {
        public const bool IsEnabled = false;

        public delegate void Callback(string s);

        [MonoPInvokeCallback(typeof(Callback))]
        public static void Debug(string? t)
        {
        }

        [MonoPInvokeCallback(typeof(Callback))]
        public static void Info(string? t)
        {
        }

        [MonoPInvokeCallback(typeof(Callback))]
        public static void Error(string? t)
        {
        }

        public static IntPtr IvizCreateContext(int width, int height) => throw new NotSupportedException();

        public static void IvizDisposeContext(IntPtr ctx) => throw new NotSupportedException();

        public static void IvizSetupDebug(Callback callback) => throw new NotSupportedException();

        public static void IvizSetupInfo(Callback callback) => throw new NotSupportedException();

        public static void IvizSetupError(Callback callback) => throw new NotSupportedException();

        public static IntPtr IvizGetImagePtr(IntPtr ctx) => throw new NotSupportedException();

        public static bool IvizSetDictionary(IntPtr ctx, int value) => throw new NotSupportedException();

        public static bool IvizDetectArucoMarkers(IntPtr ctx) => throw new NotSupportedException();

        public static bool IvizDetectQrMarkers(IntPtr ctx) => throw new NotSupportedException();

        public static int IvizGetNumDetectedMarkers(IntPtr ctx) => throw new NotSupportedException();

        public static bool IvizGetArucoMarkerIds(IntPtr ctx, ref int arrayPtr, int arraySize) =>
            throw new NotSupportedException();

        public static bool IvizGetQrMarkerCodes(IntPtr ctx, ref IntPtr arrayPtr, ref int arrayLengths,
            int arraySize) => throw new NotSupportedException();

        public static bool IvizGetMarkerCorners(IntPtr ctx, ref float arrayPtr, int arraySize) =>
            throw new NotSupportedException();

        public static bool IvizEstimatePnp(in float inputs, int inputSize, in float outputs, int outputSize,
            in float cameraArray, int cameraArraySize, ref float result, int resultSize) =>
            throw new NotSupportedException();
    }
#endif
}