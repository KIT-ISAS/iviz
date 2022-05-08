#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Core;
using Iviz.Core.XR;
using Iviz.Tools;
using Iviz.Urdf;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.Implementation.MessageTypes.Outgoing;
using MarcusW.VncClient.Protocol.Implementation.Native;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Material = UnityEngine.Material;
using Position = MarcusW.VncClient.Position;

namespace VNC
{
    public class VncScreen : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler, IPointerDownHandler,
        ISupportsDynamicBounds
    {
        static readonly (Key Key, KeySymbol Symbol)[] KeyMap =
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
            (Key.Home, KeySymbol.Home),
            (Key.End, KeySymbol.End),
        };

        Transform? mTransform;
        [SerializeField] Material? material;
        [SerializeField] MeshRenderer? mainRenderer;
        [SerializeField] XRSimpleInteractable? interactable;
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] VncController? controller;

        readonly bool[] mouseButtonStates = new bool[3];
        readonly Dictionary<Key, float> keyPressDown = new();
        readonly Dictionary<Key, CancellationTokenSource> keyRepeatTokens = new();

        Texture2D? texture;

        Position lastPosition;
        KeySymbol? lastSymbol;

        Transform? interactorTransform;
        IXRController? interactorController;

        Size? Size => texture != null ? new Size(texture.width, texture.height) : null;
        VncController Controller => controller.AssertNotNull(nameof(controller));
        Transform Transform => this.EnsureHasTransform(ref mTransform);
        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));
        VncClient Client => Controller.Client;

        public event Action? BoundsChanged;

        public VncScreen()
        {
            TurboJpeg.IsAvailable = true;
        }

        void Awake()
        {
            material = Instantiate(material);
            mainRenderer!.sharedMaterial = material;

            if (interactable != null)
            {
                interactable.hoverEntered.AddListener(OnHoverEnter);
                interactable.hoverExited.AddListener(OnHoverExit);
            }
            
            Cursor.lockState = CursorLockMode.Confined;
        }

        Texture2D EnsureTextureSize(Size newSize)
        {
            if (texture != null)
            {
                if (Size == newSize)
                {
                    return texture;
                }

                Destroy(texture);
            }

            if (material == null)
            {
                throw new NullReferenceException("Material has not been set!");
            }

            texture = new Texture2D(newSize.Width, newSize.Height, TextureFormat.RGBA32, true)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Trilinear
            };
            material.mainTexture = texture;

            float currentScaleY = Transform.localScale.y;
            float scaleRatio = (float)newSize.Width / newSize.Height;
            Transform.localScale = new Vector3(currentScaleY * scaleRatio, currentScaleY, 1);
            BoundsChanged?.Invoke();

            return texture;
        }

        public Span<byte> GetTextureSpan(Size requestedSize) => EnsureTextureSize(requestedSize).AsSpan();

        public void UpdateFrame(Size requestedSize) => EnsureTextureSize(requestedSize).Apply();

        void OnDisable()
        {
            foreach (var token in keyRepeatTokens.Values)
            {
                token.Cancel();
            }

            keyRepeatTokens.Clear();
            keyPressDown.Clear();
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
            if (TryGetPosition(eventData, out var localPosition))
            {
                Client.Enqueue(new PointerEventMessage(localPosition, GetButtons()));
            }
        }

        bool TryGetPosition(PointerEventData eventData, out Position position)
        {
            if (!eventData.pointerCurrentRaycast.isValid)
            {
                position = default;
                return false;
            }

            var worldPosition = eventData.pointerCurrentRaycast.worldPosition;
            return TryGetPosition(worldPosition, out position);
        }

        void OnHoverEnter(HoverEnterEventArgs args)
        {
            if (interactorTransform != null)
            {
                return;
            }

            //interactorTransform = args.interactorObject.transform;
            interactorTransform = args.interactor.transform;
            interactorController = interactorTransform.AssertHasComponent<IXRController>(nameof(interactorTransform));
            GameThread.EveryFrame += TriggerPointerMove;
        }

        void OnHoverExit(HoverExitEventArgs args)
        {
            //if (args.interactorObject.transform != interactorTransform)
            if (args.interactor.transform != interactorTransform)
            {
                return;
            }

            interactorTransform = null;
            interactorController = null;
            GameThread.EveryFrame -= TriggerPointerMove;
        }

        void TriggerPointerMove()
        {
            if (interactorTransform == null)
            {
                return;
            }

            var ray = new Ray(interactorTransform.position, interactorTransform.forward);
            ProcessXRPointer(ray);
        }

        void ProcessXRPointer(in Ray ray)
        {
            if (TryGetPosition(ray, out var localPosition))
            {
                Client.Enqueue(new PointerEventMessage(localPosition, GetXRButtons()));
            }
        }

        MouseButtons GetXRButtons()
        {
            if (interactorController == null)
            {
                return MouseButtons.None;
            }

            bool currentButtonDown = interactorController.ButtonState;
            return currentButtonDown ? MouseButtons.Left : MouseButtons.None;
        }

        bool TryGetPosition(in Ray ray, out Position position)
        {
            if (!BoxCollider.Raycast(ray, out var hitInfo, 10))
            {
                position = default;
                return false;
            }

            var worldPosition = hitInfo.point;
            return TryGetPosition(worldPosition, out position);
        }

        bool TryGetPosition(Vector3 worldPosition, out Position position)
        {
            if (Size is not { } currentSize)
            {
                position = default;
                return false;
            }

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

        void Update()
        {
            ProcessMouseWheel();
            ProcessKeyboard();

            /*
            lastPosition = new Position(
                (int)Mouse.current.position.x.ReadValue(),
                768 - (int)Mouse.current.position.y.ReadValue()
                );

            mouseButtonStates[0] = Mouse.current.leftButton.isPressed;
            mouseButtonStates[1] = Mouse.current.rightButton.isPressed;
            mouseButtonStates[2] = Mouse.current.middleButton.isPressed;
            
            var buttons = MouseButtons.None;
            if (mouseButtonStates[0]) buttons |= MouseButtons.Left;
            if (mouseButtonStates[1]) buttons |= MouseButtons.Right;
            if (mouseButtonStates[2]) buttons |= MouseButtons.Middle;
            
            Client.Enqueue(new PointerEventMessage(lastPosition, buttons));

            float mouseDeltaY = Mouse.current.scroll.y.ReadValue();
            if (mouseDeltaY != 0)
            {
                Client.Enqueue(new PointerEventMessage(lastPosition,
                    mouseDeltaY > 0 ? MouseButtons.WheelDown : MouseButtons.WheelUp));
                Client.Enqueue(new PointerEventMessage(lastPosition, MouseButtons.None));
            }
            */
        }

        void ProcessMouseWheel()
        {
            var (mouseDeltaX, mouseDeltaY) = Input.mouseScrollDelta;
            if (mouseDeltaX != 0)
            {
                Client.Enqueue(new PointerEventMessage(lastPosition,
                    mouseDeltaX > 0 ? MouseButtons.WheelRight : MouseButtons.WheelLeft));
                Client.Enqueue(new PointerEventMessage(lastPosition, MouseButtons.None));
            }

            if (mouseDeltaY != 0)
            {
                Client.Enqueue(new PointerEventMessage(lastPosition,
                    mouseDeltaY > 0 ? MouseButtons.WheelDown : MouseButtons.WheelUp));
                Client.Enqueue(new PointerEventMessage(lastPosition, MouseButtons.None));
            }
        }

        void ProcessKeyboard()
        {
            var keyboard = Keyboard.current;
            foreach (var (key, symbol) in KeyMap)
            {
                CheckKeyWithRepeat(key, symbol);
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
                    Client.Enqueue(new KeyEventMessage(true, keySymbol));
                    lastSymbol = keySymbol;
                }
                else if (lastSymbol is { } symbol)
                {
                    Client.Enqueue(new KeyEventMessage(false, symbol));
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
            {
                Client.Enqueue(new KeyEventMessage(false, keySymbol));
            }

            if (control.wasPressedThisFrame)
            {
                Client.Enqueue(new KeyEventMessage(true, keySymbol));
            }
        }

        void CheckKeyWithRepeat(Key key, KeySymbol keySymbol)
        {
            const float timePressedToRepeatInSec = 0.5f;

            var control = Keyboard.current[key];
            if (control.wasReleasedThisFrame)
            {
                Client.Enqueue(new KeyEventMessage(false, keySymbol));
                keyPressDown.Remove(key);
                if (keyRepeatTokens.TryGetValue(key, out var tokenSource))
                {
                    tokenSource.Cancel();
                    keyRepeatTokens.Remove(key);
                }
            }

            if (control.wasPressedThisFrame)
            {
                Client.Enqueue(new KeyEventMessage(true, keySymbol));
                keyPressDown[key] = Time.time;
            }

            if (keyPressDown.TryGetValue(key, out float time)
                && Time.time > time + timePressedToRepeatInSec
                && !keyRepeatTokens.ContainsKey(key))
            {
                LaunchKeyRepeatTask(key, keySymbol);
            }
        }

        void LaunchKeyRepeatTask(Key key, KeySymbol keySymbol)
        {
            const int keyRepeatPeriodInMs = 50;

            var tokenSource = new CancellationTokenSource();
            keyRepeatTokens[key] = tokenSource;

            var token = tokenSource.Token;
            TaskUtils.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    Client.Enqueue(new KeyEventMessage(true, keySymbol));
                    Client.Enqueue(new KeyEventMessage(false, keySymbol));
                    await Task.Delay(keyRepeatPeriodInMs, token).AwaitNoThrow(this);
                }
            }, token);
        }
    }
}