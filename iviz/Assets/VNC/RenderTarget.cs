#nullable enable

using System;
using System.Buffers;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Iviz.Core;
using MarcusW.VncClient;
using MarcusW.VncClient.Rendering;
using UnityEngine;
using Screen = MarcusW.VncClient.Screen;

namespace VNC
{
    internal sealed class RenderTarget : IRenderTarget
    {
        readonly VncClient parent; 
        FrameBufferReference? cachedFrameBufferReference;

        public RenderTarget(VncClient parent)
        {
            this.parent = parent;
        }
        
        public IFramebufferReference GrabFramebufferReference(Size size, IImmutableSet<Screen> layout)
        {
            if (cachedFrameBufferReference != null && cachedFrameBufferReference.Size == size)
            {
                return cachedFrameBufferReference;
            }

            cachedFrameBufferReference = new FrameBufferReference(parent, size);
            return cachedFrameBufferReference;
        }

        sealed class FrameBufferReference : IFramebufferReference
        {
            readonly Action frameArrived;
            readonly byte[] buffer;

            public Span<byte> Address => buffer;
            public Size Size { get; }
            public double HorizontalDpi => 200;
            public double VerticalDpi => 200;
            public PixelFormat Format => PixelFormat.BgraCompatiblePixelFormat;

            public FrameBufferReference(VncClient parent, Size size)
            {
                int requested = size.Width * size.Height * 4;
                frameArrived = () => parent.OnFrameArrived(this);
                buffer = new byte[requested];
                Size = size;
            }

            public void NotifyUsedFormat(PixelFormat format)
            {
                if (!Format.IsBinaryCompatibleTo(format))
                {
                    Debug.Log("FrameBufferReference: Using inefficient format!");
                }
            }

            public void Dispose()
            {
                GameThread.Post(frameArrived);
            }
        }
    }
}