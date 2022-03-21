#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.MessageTypes.Outgoing;
using MarcusW.VncClient.Protocol.Implementation.Native;
using MarcusW.VncClient.Protocol.Implementation.Services.Transports;
using MarcusW.VncClient.Rendering;
using Microsoft.Extensions.Logging.Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Position = MarcusW.VncClient.Position;

namespace VNC
{
    public class VncClient : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler, IPointerDownHandler,
        IPointerExitHandler
    {
        Transform? mTransform;
        [SerializeField] Material? material;
        [SerializeField] MeshRenderer? mainRenderer;

        readonly SemaphoreSlim signal = new(0);
        readonly ConcurrentQueue<EventMessage> messages = new();
        readonly CancellationTokenSource tokenSource = new();
        Texture2D? texture;
        Size? size;

        readonly bool[] mouseButtonStates = new bool[3];
        Position lastPosition;
        KeySymbol? lastSymbol;

        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        void Awake()
        {
            TurboJpeg.IsAvailable = true;
        }

        void Start()
        {
            material = Instantiate(material);
            mainRenderer!.sharedMaterial = material;

            TaskUtils.Run(async () =>
            {
                try
                {
                    await StartAsync(tokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
            });
        }

        async Task StartAsync(CancellationToken token)
        {
            var vncClient = new MarcusW.VncClient.VncClient(NullLoggerFactory.Instance);

            var renderTarget = new RenderTarget(this);

            // Configure the connect parameters
            var parameters = new ConnectParameters
            {
                TransportParameters = new TcpTransportParameters
                {
                    Host = "192.168.0.17",
                    Port = 5901
                },
                AuthenticationHandler = new AuthenticationHandler(),
                InitialRenderTarget = renderTarget,
                JpegQualityLevel = 90,
                JpegSubsamplingLevel = JpegSubsamplingLevel.ChrominanceSubsampling16X,
            };

            // Start a new connection and save the returned connection object
            using var rfbConnection = await vncClient.ConnectAsync(parameters, token);

            while (!token.IsCancellationRequested)
            {
                await signal.WaitAsync(token);

                while (messages.TryDequeue(out var message))
                {
                    switch (message.type)
                    {
                        case EventMessage.Type.Pointer:
                            rfbConnection.EnqueueMessage(message.pointerEventMessage!);
                            break;
                        case EventMessage.Type.Key:
                            rfbConnection.EnqueueMessage(message.keyEventMessage!);
                            break;
                    }
                }
            }

            await rfbConnection.CloseAsync();
        }

        public void OnFrameArrived(IFramebufferReference frame)
        {
            try
            {
                ProcessFrame(frame);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception in {nameof(OnFrameArrived)}", e);
            }
        }

        void ProcessFrame(IFramebufferReference frame)
        {
            if (material == null)
            {
                return;
            }

            if (texture != null && size != frame.Size)
            {
                Destroy(texture);
                texture = null;
            }

            if (texture == null)
            {
                texture = new Texture2D(frame.Size.Width, frame.Size.Height, TextureFormat.RGBA32, false);
                material.mainTexture = texture;

                size = frame.Size;
                Transform.localScale = new Vector3((float)frame.Size.Width / frame.Size.Height, 1, 1);
            }

            texture.GetRawTextureData<byte>().CopyFrom(frame.Address);
            texture.Apply();
        }

        void OnDestroy()
        {
            tokenSource.Cancel();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            ProcessPointer(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            mouseButtonStates[(int)eventData.button] = false;
            ProcessPointer(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            mouseButtonStates[(int)eventData.button] = true;
            ProcessPointer(eventData);
        }

        void ProcessPointer(PointerEventData eventData)
        {
            if (!TryGetPosition(eventData, out var position))
            {
                return;
            }

            Enqueue(new PointerEventMessage(position, GetButtons()));
        }

        bool TryGetPosition(PointerEventData eventData, out Position position)
        {
            if (size is not { } currentSize || !eventData.pointerCurrentRaycast.isValid)
            {
                position = default;
                return false;
            }

            var worldPosition = eventData.pointerCurrentRaycast.worldPosition;
            var (localX, localY, _) = Transform.InverseTransformPoint(worldPosition);
            var (screenX, screenY) = ((localX + 0.5f) * currentSize.Width, (0.5f - localY) * currentSize.Height);
            var (posX, posY) = ((int)(screenX + 0.5f), (int)(screenY + 0.5f));

            position = new Position(posX, posY);
            lastPosition = position;
            return true;
        }

        MouseButtons GetButtons()
        {
            const int left = (int)PointerEventData.InputButton.Left;
            const int right = (int)PointerEventData.InputButton.Right;
            const int middle = (int)PointerEventData.InputButton.Middle;

            var buttons = MouseButtons.None;
            if (mouseButtonStates[left]) buttons |= MouseButtons.Left;
            if (mouseButtonStates[right]) buttons |= MouseButtons.Right;
            if (mouseButtonStates[middle]) buttons |= MouseButtons.Middle;
            return buttons;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseButtonStates.AsSpan().Fill(false);
        }

        void Update()
        {
            ProcessMouseWheel();
            ProcessKeyboard();
        }

        void ProcessMouseWheel()
        {
            var (mouseDeltaX, mouseDeltaY) = Input.mouseScrollDelta;
            if (mouseDeltaX != 0)
            {
                Enqueue(new PointerEventMessage(lastPosition,
                    mouseDeltaX > 0 ? MouseButtons.WheelRight : MouseButtons.WheelLeft));
                Enqueue(new PointerEventMessage(lastPosition, MouseButtons.None));
            }

            if (mouseDeltaY != 0)
            {
                Enqueue(new PointerEventMessage(lastPosition,
                    mouseDeltaY > 0 ? MouseButtons.WheelDown : MouseButtons.WheelUp));
                Enqueue(new PointerEventMessage(lastPosition, MouseButtons.None));
            }
        }

        static readonly (Key Key, KeySymbol Symbol)[] Map =
        {
            (Key.LeftAlt, KeySymbol.Alt_L),
            (Key.RightAlt, KeySymbol.Alt_R),
            (Key.LeftCtrl, KeySymbol.Control_L),
            (Key.RightCtrl, KeySymbol.Control_R),
            (Key.LeftCommand, KeySymbol.Meta_L),
            (Key.RightCommand, KeySymbol.Meta_R),
            (Key.Backspace, KeySymbol.BackSpace),
            (Key.Delete, KeySymbol.Delete),
            (Key.Enter, KeySymbol.Return),
            (Key.Tab, KeySymbol.Tab),
            (Key.Escape, KeySymbol.Escape),
            (Key.DownArrow, KeySymbol.Down),
            (Key.UpArrow, KeySymbol.Up),
            (Key.LeftArrow, KeySymbol.Left),
            (Key.RightArrow, KeySymbol.Right),
        };

        void ProcessKeyboard()
        {
            var keyboard = Keyboard.current;
            foreach (var (key, symbol) in Map)
            {
                CheckKey(key, symbol);
            }

            bool hasModifier = keyboard[Key.LeftCtrl].isPressed
                               || keyboard[Key.RightCtrl].isPressed
                               || keyboard[Key.LeftAlt].isPressed
                               || keyboard[Key.RightAlt].isPressed;

            if (!hasModifier)
            {
                if (Input.inputString.Length != 0)
                {
                    var keySymbol = (KeySymbol)Input.inputString[0];
                    Enqueue(new KeyEventMessage(true, keySymbol));
                    lastSymbol = keySymbol;
                }
                else if (lastSymbol is { } symbol)
                {
                    Enqueue(new KeyEventMessage(false, symbol));
                    lastSymbol = null;
                }

                return;
            }

            for (int i = (int)Key.A; i <= (int)Key.Z; i++)
            {
                CheckKey((Key)i, KeySymbol.a + (i - (int)Key.A));
            }

            CheckKey(Key.Digit0, KeySymbol.KP_0);
            for (int i = (int)Key.Digit1; i <= (int)Key.Digit9; i++)
            {
                CheckKey((Key)i, KeySymbol.KP_1 + (i - (int)Key.Digit1));
            }
        }

        void CheckKey(Key key, KeySymbol keySymbol)
        {
            var control = Keyboard.current[key];
            if (control.wasReleasedThisFrame)
                Enqueue(new KeyEventMessage(false, keySymbol));
            if (control.wasPressedThisFrame)
                Enqueue(new KeyEventMessage(true, keySymbol));
        }

        void Enqueue(in EventMessage msg)
        {
            messages.Enqueue(msg);
            signal.Release();
        }

        void Enqueue(KeyEventMessage msg) => Enqueue(new EventMessage(msg));

        void Enqueue(PointerEventMessage msg) => Enqueue(new EventMessage(msg));

        readonly struct EventMessage
        {
            public enum Type
            {
                Pointer,
                Key
            }

            public readonly Type type;
            public readonly PointerEventMessage? pointerEventMessage;
            public readonly KeyEventMessage? keyEventMessage;

            public EventMessage(PointerEventMessage msg) : this() => (type, pointerEventMessage) = (Type.Pointer, msg);
            public EventMessage(KeyEventMessage msg) : this() => (type, keyEventMessage) = (Type.Key, msg);
        }
    }
}