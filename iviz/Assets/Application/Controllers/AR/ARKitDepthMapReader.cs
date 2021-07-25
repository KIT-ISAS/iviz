using System;
using System.Runtime.InteropServices;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

// Adapted from https://github.com/Unity-Technologies/arfoundation-samples/issues/615

namespace Iviz.Controllers.ARKit
{
    public sealed class ARKitDepthMapReader : IDisposable
    {
        [CanBeNull]
        public Screenshot GetDepthImage([NotNull] XRSessionSubsystem subsystem, in Pose pose)
        {
            var sessionNativePtr = subsystem.nativePtr;
            if (sessionNativePtr == IntPtr.Zero)
            {
                throw new NullReferenceException("No session was provided!");
            }

            using (var handle = TrueDepthMapInterface.GetDepthMap(sessionNativePtr))
            {
                if (handle.IsNull() || handle.Width == 0 || handle.Height == 0)
                {
                    //Debug.Log("Handle is null, " + handle.Width);
                    return null;
                }

                byte[] bytes = new byte[4 * handle.Width * handle.Height];
                Marshal.Copy(handle.TextureDepth, bytes, 0, bytes.Length);
                return new Screenshot(ScreenshotFormat.Float, handle.Width, handle.Height, new Intrinsic(), pose,
                    bytes);
            }
        }

        void ReleaseUnmanagedResources()
        {
            TrueDepthMapInterface.UnityUnloadMetalCache();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~ARKitDepthMapReader()
        {
            ReleaseUnmanagedResources();
        }


        static class TrueDepthMapInterface
        {
            [DllImport("__Internal")]
            static extern DepthTextureHandlesStruct UnityGetDepthMap(IntPtr ptr);

            [DllImport("__Internal")]
            public static extern void UnityUnloadMetalCache();

            [DllImport("__Internal")]
            public static extern void ReleaseDepthTextureHandles(DepthTextureHandlesStruct handles);

            [NotNull]
            public static DepthTextureHandles GetDepthMap(IntPtr nativePtr)
            {
#if !UNITY_EDITOR && UNITY_IOS
                return new DepthTextureHandles(UnityGetDepthMap(nativePtr));
#else
                return new DepthTextureHandles(new DepthTextureHandlesStruct());
#endif
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        readonly struct DepthTextureHandlesStruct
        {
            // Native (Metal) texture handles for the device camera buffer
            public readonly IntPtr textureDepth;
            public readonly double depthTimestamp;
            public readonly int width;
            public readonly int height;
        }

        sealed class DepthTextureHandles : IDisposable
        {
            readonly DepthTextureHandlesStruct mDepthTextureHandlesStruct;
            public IntPtr TextureDepth => mDepthTextureHandlesStruct.textureDepth;
            public int Width => mDepthTextureHandlesStruct.width;
            public int Height => mDepthTextureHandlesStruct.height;

            public DepthTextureHandles(in DepthTextureHandlesStruct arTextureHandlesStruct)
            {
                mDepthTextureHandlesStruct = arTextureHandlesStruct;
            }

            public bool IsNull()
            {
                return (mDepthTextureHandlesStruct.textureDepth == IntPtr.Zero);
            }

            void ReleaseUnmanagedResources()
            {
#if !UNITY_EDITOR && UNITY_IOS
                TrueDepthMapInterface.ReleaseDepthTextureHandles(mDepthTextureHandlesStruct);
#endif
            }

            public void Dispose()
            {
                ReleaseUnmanagedResources();
                GC.SuppressFinalize(this);
            }

            ~DepthTextureHandles()
            {
                ReleaseUnmanagedResources();
            }
        }
    }
}