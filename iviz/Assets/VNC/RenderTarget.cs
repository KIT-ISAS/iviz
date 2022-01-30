#nullable enable

using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Iviz.Core;
using MarcusW.VncClient;
using MarcusW.VncClient.Rendering;

namespace VNC
{
    internal sealed class RenderTarget : IRenderTarget, IDisposable
    {
        readonly RenderBuffer renderBuffer;
        bool disposed;

        public event Action<IFramebufferReference>? FrameArrived; // event runs in game thread

        public RenderTarget()
        {
            renderBuffer = new RenderBuffer(this);
        }

        public IFramebufferReference GrabFramebufferReference(Size size, IImmutableSet<Screen> layout)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(RenderTarget));
            }

            renderBuffer.EnsureSize(size);
            return renderBuffer.Reference;
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        void ReleaseUnmanagedResources()
        {
            renderBuffer.Dispose();
        }

        ~RenderTarget()
        {
            ReleaseUnmanagedResources();
        }

        sealed class RenderBuffer
        {
            readonly RenderTarget parent;
            IntPtr address;
            Size size;
            bool disposed;
            FrameBufferReference cachedReference;

            public FrameBufferReference Reference =>
                !disposed ? cachedReference : throw new ObjectDisposedException(nameof(RenderBuffer));

            public RenderBuffer(RenderTarget parent)
            {
                this.parent = parent;
                cachedReference = new FrameBufferReference(parent, IntPtr.Zero, default);
            }

            public void EnsureSize(in Size newSize)
            {
                if (size == newSize)
                {
                    return;
                }

                if (disposed)
                {
                    throw new ObjectDisposedException(nameof(RenderBuffer));
                }

                if (address != IntPtr.Zero)
                {
                    // dispose in GameThread to avoid race conditions with texture management
                    GameThread.Post(() => Marshal.FreeHGlobal(address));
                }

                size = newSize;
                address = size != default
                    ? Marshal.AllocHGlobal(size.Width * size.Height * 4)
                    : IntPtr.Zero;
                cachedReference = new FrameBufferReference(parent, address, size);
            }


            public void Dispose()
            {
                if (disposed)
                {
                    return;
                }

                disposed = true;
                if (address != IntPtr.Zero)
                {
                    // dispose in GameThread to avoid race conditions with texture management
                    GameThread.Post(() => Marshal.FreeHGlobal(address));
                }
            }
        }

        /// <inheritdoc cref="IFramebufferReference"/>
        sealed class FrameBufferReference : IFramebufferReference
        {
            static readonly PixelFormat RgbaFormat = new("Plain RGBA", 32, 32,
                true, true, true,
                255, 255, 255, 255,
                24, 16, 8, 0);

            readonly RenderTarget parent;

            public IntPtr Address { get; }
            public Size Size { get; }
            public double HorizontalDpi => 200;
            public double VerticalDpi => 200;
            public PixelFormat Format => RgbaFormat;

            internal FrameBufferReference(RenderTarget parent, IntPtr address, Size size)
            {
                this.parent = parent;
                Address = address;
                Size = size;
            }

            /// <summary>
            /// Dispose is actually called when the frame finishes rendering.
            /// It should not dispose the underlying resources.
            /// </summary>
            public void Dispose()
            {
                GameThread.Post(() =>
                {
                    // run in GameThread to avoid race conditions
                    parent.FrameArrived?.Invoke(this);
                });
            }
        }
    }
}