#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Core.XR;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol;
using MarcusW.VncClient.Protocol.Implementation.MessageTypes.Outgoing;
using MarcusW.VncClient.Protocol.Implementation.Native;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Position = MarcusW.VncClient.Position;

namespace VNC
{
    public class VncScreen : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler, IPointerDownHandler,
        IPointerExitHandler, IRenderTargetCallback
    {
        Transform? mTransform;
        [SerializeField] Material? material;
        [SerializeField] MeshRenderer? mainRenderer;
        [SerializeField] XRSimpleInteractable? interactable;
        [SerializeField] BoxCollider? boxCollider;

        readonly Dictionary<(int, int), Texture2D> cachedTextures = new();
        
        readonly VncClient client = new();
        Texture2D? texture;
        Size? size;

        readonly bool[] mouseButtonStates = new bool[3];
        Position lastPosition;
        KeySymbol? lastSymbol;

        Transform? interactorTransform;
        IXRController? interactorController;

        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));

        void Awake()
        {
            TurboJpeg.IsAvailable = true;
        }

        void Start()
        {
            material = Instantiate(material);
            mainRenderer!.sharedMaterial = material;

            if (interactable != null)
            {
                interactable.hoverEntered.AddListener(OnHoverEnter);
                interactable.hoverExited.AddListener(OnHoverExit);
            }

            client.Start(this);
        }

        /*
        void OnFrameArrived(IFramebufferReference frame)
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

        public void ProcessFrame(IFramebufferReference frame)
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
            texture.Apply(false, true);
        }
        */

        Texture2D GetFromTextureCache(int width, int height)
        {
            Texture2D frameTexture;
            if (cachedTextures.TryGetValue((width, height), out var candidateTexture))
            {
                frameTexture = candidateTexture;
            }
            else
            {
                frameTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
                cachedTextures[(width, height)] = frameTexture;
            }

            return frameTexture;
        }

        public void ProcessFrame(IRenderFrame frame, in Rectangle rectangle, Size maxSize)
        {
            if (material == null)
            {
                return;
            }

            if (size != maxSize && texture != null)
            {
                Destroy(texture);
                texture = null;
            }

            if (texture == null)
            {
                texture = new Texture2D(maxSize.Width, maxSize.Height, TextureFormat.RGBA32, false);
                material.mainTexture = texture;

                size = maxSize;
                Transform.localScale = new Vector3((float)maxSize.Width / maxSize.Height, 1, 1);
            }

            // unity doesn't allow partial updates of a texture
            // so we upload to a smaller texture and then copy it to the big one
            
            int powWidth = frame.Size.Width;
            int powHeight = UnityUtils.ClosestPow2(rectangle.Size.Height);

            var frameTexture = GetFromTextureCache(powWidth, powHeight);
            frameTexture.GetRawTextureData<byte>().CopyFrom(frame.Address);
            frameTexture.Apply();

            Graphics.CopyTexture(frameTexture, 0, 0, 0, 0, rectangle.Size.Width, rectangle.Size.Height,
                texture, 0, 0, rectangle.Position.X, rectangle.Position.Y);
        }
        
        public void CopyFrame(in Rectangle dstRectangle, in Rectangle srcRectangle)
        {
            int powWidth = UnityUtils.ClosestPow2(srcRectangle.Size.Width);
            int powHeight = UnityUtils.ClosestPow2(srcRectangle.Size.Height);

            var frameTexture = GetFromTextureCache(powWidth, powHeight);

            // unity won't allow CopyTexture from different parts of the same texture
            // so we go the slow way of copying it to another texture, and then back
            
            Graphics.CopyTexture(texture, 0, 0, srcRectangle.Position.X, srcRectangle.Position.Y,
                srcRectangle.Size.Width, srcRectangle.Size.Height,
                frameTexture, 0, 0, 0, 0);

            Graphics.CopyTexture(frameTexture, 0, 0, 0, 0,
                srcRectangle.Size.Width, srcRectangle.Size.Height,
                texture, 0, 0, dstRectangle.Position.X, dstRectangle.Position.Y);
        }


        void OnDestroy()
        {
            client.Dispose();
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
                client.Enqueue(new PointerEventMessage(localPosition, GetButtons()));
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

            interactorTransform = args.interactorObject.transform;
            interactorController = interactorTransform.AssertHasComponent<IXRController>(nameof(interactorTransform));
            GameThread.EveryFrame += TriggerPointerMove;
        }

        void OnHoverExit(HoverExitEventArgs args)
        {
            if (args.interactorObject.transform != interactorTransform)
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
                client.Enqueue(new PointerEventMessage(localPosition, GetXRButtons()));
            }
        }

        MouseButtons GetXRButtons()
        {
            if (interactorController == null)
            {
                return MouseButtons.None;
            }

            bool currentButtonDown = interactorController.ButtonState || interactorController.IsNearInteraction;
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
            if (size is not { } currentSize)
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
                client.Enqueue(new PointerEventMessage(lastPosition,
                    mouseDeltaX > 0 ? MouseButtons.WheelRight : MouseButtons.WheelLeft));
                client.Enqueue(new PointerEventMessage(lastPosition, MouseButtons.None));
            }

            if (mouseDeltaY != 0)
            {
                client.Enqueue(new PointerEventMessage(lastPosition,
                    mouseDeltaY > 0 ? MouseButtons.WheelDown : MouseButtons.WheelUp));
                client.Enqueue(new PointerEventMessage(lastPosition, MouseButtons.None));
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
                    client.Enqueue(new KeyEventMessage(true, keySymbol));
                    lastSymbol = keySymbol;
                }
                else if (lastSymbol is { } symbol)
                {
                    client.Enqueue(new KeyEventMessage(false, symbol));
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
                client.Enqueue(new KeyEventMessage(false, keySymbol));
            if (control.wasPressedThisFrame)
                client.Enqueue(new KeyEventMessage(true, keySymbol));
        }
    }
}