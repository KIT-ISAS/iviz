using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using MarcusW.VncClient;
using MarcusW.VncClient.Rendering;

namespace VNC
{
    internal sealed class RenderTarget : IRenderTarget, IDisposable
    {
        Size size;
        IntPtr address;
        FrameBufferReference cachedReference = new();
        bool disposed;

        public IFramebufferReference GrabFramebufferReference(Size newSize, IImmutableSet<Screen> layout)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(RenderTarget));
            }

            if (size == newSize)
            {
                return cachedReference;
            }

            if (address != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(address);
                address = IntPtr.Zero;
            }

            size = newSize;
            address = Marshal.AllocHGlobal(size.Width * size.Height * 4);
            cachedReference = new FrameBufferReference(address, size);
            return cachedReference;
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
            if (address != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(address);
            }
        }

        ~RenderTarget()
        {
            ReleaseUnmanagedResources();
        }

        sealed class FrameBufferReference : IFramebufferReference
        {
            public IntPtr Address { get; }
            public Size Size { get; }
            public PixelFormat Format => PixelFormat.Plain;
            public double HorizontalDpi => 200;
            public double VerticalDpi => 200;

            public FrameBufferReference(IntPtr address = default, Size size = default)
            {
                Address = address;
                Size = size;
            }

            public void Dispose()
            {
                Console.WriteLine("fb reference dispose!");
            }
        }
    }
}