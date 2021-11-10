using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Tools;
using Iviz.Msgs.IvizCommonMsgs;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Material = UnityEngine.Material;

namespace Iviz.App.ARDialogs
{
    public sealed class ARDialog : MarkerResource, IRecyclable
    {
        const float PopupDuration = 0.1f;

        static readonly Color DefaultBackgroundColor = new Color(0, 0.2f, 0.5f);

        [SerializeField] TextMesh title = null;
        [SerializeField] TMP_Text caption = null;
        [SerializeField] ARButton button1 = null;
        [SerializeField] ARButton button2 = null;
        [SerializeField] ARButton button3 = null;
        [SerializeField] ARLineConnector connector = null;
        [SerializeField] MeshRenderer iconMeshRenderer = null;

        [SerializeField] Texture2D[] icons = null;

        [SerializeField] ARButton[] menuButtons = null;
        [SerializeField] ARButton upButton = null;
        [SerializeField] ARButton downButton = null;

        [SerializeField] Color backgroundColor = DefaultBackgroundColor;
        [SerializeField] MeshMarkerResource background = null;

        [SerializeField] Vector3 socketPosition = Vector3.zero;

        [NotNull] public string Id { get; internal set; } = "";

        public event Action<ARDialog, int> ButtonClicked;
        public event Action<ARDialog, int> MenuEntryClicked;
        public event Action<ARDialog> Expired;

        Material iconMaterial;

        [NotNull]
        Material IconMaterial =>
            iconMaterial != null ? iconMaterial : (iconMaterial = Instantiate(iconMeshRenderer.material));

        float? popupStartTime;

        //float? currentAngle;
        bool resetOrientation;
        Vector3? currentPosition;
        float scale;

        FrameNode node;

        [NotNull]
        FrameNode Node
        {
            get
            {
                if (this == null)
                {
                    Debug.Log($"{this}: Accessing dead dialog!");
                    throw new ObjectDisposedException(ToString());
                }

                return (node != null) ? node : node = FrameNode.Instantiate("Dialog Node");
            }
        }

        [NotNull] public TfFrame ParentFrame => Node.Parent.CheckedNull() ?? TfListener.DefaultFrame;

        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                if (backgroundColor == default)
                {
                    backgroundColor = DefaultBackgroundColor;
                }

                if (background != null)
                {
                    background.Color = backgroundColor;
                }

                SetBackgroundColor(button1, backgroundColor);
                SetBackgroundColor(button2, backgroundColor);
                SetBackgroundColor(button3, backgroundColor);
                SetBackgroundColor(upButton, backgroundColor);

                foreach (var menuButton in menuButtons)
                {
                    SetBackgroundColor(menuButton, backgroundColor);
                }
            }
        }

        static void SetBackgroundColor(ARButton button, Color color)
        {
            if (button != null)
            {
                button.BackgroundColor = color;
            }
        }

        public string Caption
        {
            get => caption.text;
            set => caption.text = value;
        }

        public CaptionAlignmentType CaptionAlignment
        {
            get => (CaptionAlignmentType) caption.alignment;
            set => caption.alignment =
                value == CaptionAlignmentType.Default
                    ? TextAlignmentOptions.Center
                    : (TextAlignmentOptions) value;
        }

        public string Title
        {
            get => title.text;
            set => title.text = value;
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        string pivotFrameId;

        [CanBeNull]
        public string PivotFrameId
        {
            get => pivotFrameId;
            set
            {
                pivotFrameId = value;
                if (pivotFrameId != null)
                {
                    Node.AttachTo(pivotFrameId);
                    currentPosition = null;
                    resetOrientation = true;
                }
                else
                {
                    connector.End = null;
                }

                connector.Visible = pivotFrameId != null;
            }
        }

        public Vector3 DialogDisplacement { get; set; }
        public Vector3 PivotFrameOffset { get; set; }
        public Vector3 PivotDisplacement { get; set; }

        public void SetButtonMode(ButtonType value)
        {
            if (button1 == null)
            {
                RosLogger.Error($"{this}: Tried to set buttons in an asset that does not support them");
                return;
            }

            button1.Active = false;
            if (button2 != null)
            {
                button2.Active = false;
            }

            if (button3 != null)
            {
                button3.Active = false;
            }

            switch (value)
            {
                case ButtonType.Ok:
                    button1.Active = true;
                    button1.Icon = ARButton.ButtonIcon.Ok;
                    button1.Caption = "Ok";
                    break;
                case ButtonType.Forward:
                    button1.Active = true;
                    button1.Icon = ARButton.ButtonIcon.Forward;
                    button1.Caption = "Ok";
                    break;
                case ButtonType.Backward:
                    button1.Active = true;
                    button1.Icon = ARButton.ButtonIcon.Backward;
                    button1.Caption = "Back";
                    break;
                case ButtonType.YesNo:
                    if (button2 == null || button3 == null)
                    {
                        break;
                    }

                    button2.Active = true;
                    button2.Icon = ARButton.ButtonIcon.Ok;
                    button2.Caption = "Yes";
                    button3.Active = true;
                    button3.Icon = ARButton.ButtonIcon.Cross;
                    button3.Caption = "No";
                    break;
                case ButtonType.ForwardBackward:
                    if (button2 == null || button3 == null)
                    {
                        break;
                    }

                    button2.Active = true;
                    button2.Icon = ARButton.ButtonIcon.Backward;
                    button2.Caption = "Back";
                    button3.Active = true;
                    button3.Icon = ARButton.ButtonIcon.Forward;
                    button3.Caption = "Forward";
                    break;
                case ButtonType.OkCancel:
                    if (button2 == null || button3 == null)
                    {
                        break;
                    }

                    button2.Active = true;
                    button2.Icon = ARButton.ButtonIcon.Ok;
                    button2.Caption = "Ok";
                    button3.Active = true;
                    button3.Icon = ARButton.ButtonIcon.Cross;
                    button3.Caption = "Cancel";
                    break;
                default:
                    RosLogger.Error($"{this}: Invalid dialog type for mode {value}");
                    break;
            }
        }

        enum IconTextureType
        {
            Error,
            Info,
            Warn,
            Dialog,
            Dialogs,
            Question
        }

        IconTextureType icon;

        IconTextureType Icon
        {
            get => icon;
            set
            {
                icon = value;
                IconMaterial.mainTexture = icons[(int) value];
            }
        }

        public void SetIconMode(IconType value)
        {
            if (iconMeshRenderer == null)
            {
                throw new InvalidOperationException("This asset does not support icons");
            }

            switch (value)
            {
                case IconType.Error:
                    Icon = IconTextureType.Error;
                    break;
                case IconType.Info:
                    Icon = IconTextureType.Info;
                    break;
                case IconType.Warn:
                    Icon = IconTextureType.Warn;
                    break;
                case IconType.Dialog:
                    Icon = IconTextureType.Dialog;
                    break;
                case IconType.Dialogs:
                    Icon = IconTextureType.Dialogs;
                    break;
                case IconType.Question:
                    Icon = IconTextureType.Question;
                    break;
            }
        }

        string[] menuEntries = Array.Empty<string>();
        int menuPage;

        public IEnumerable<string> MenuEntries
        {
            get => menuEntries;
            set
            {
                menuEntries = value.ToArray();
                if (menuButtons.Length == 0)
                {
                    RosLogger.Error($"{this}: Trying to set a menu on an asset that does not support them");
                    return;
                }

                MenuPage = 0;
            }
        }

        int MenuPage
        {
            get => menuPage;
            set
            {
                menuPage = value;
                int offset = menuPage * menuButtons.Length;
                int numActives = Math.Min(menuEntries.Length - offset, menuButtons.Length);
                for (int i = 0; i < numActives; i++)
                {
                    menuButtons[i].Caption = menuEntries[i + offset];
                    menuButtons[i].Visible = true;
                }

                foreach (var menuButton in menuButtons.Skip(numActives))
                {
                    menuButton.Visible = false;
                }

                upButton.Visible = menuPage > 0;
                downButton.Visible = offset + menuButtons.Length < menuEntries.Length;
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                if (popupStartTime != null)
                {
                    transform.localScale = scale * Vector3.one;
                }
            }
        }

        void OnButtonClick(int index)
        {
            /*
            if (Listener == null)
            {
                Debug.Log($"{this}: No listener set for click {index}");
                return;
            }

            Listener.OnDialogButtonClicked(this, index);
            */
            ButtonClicked?.Invoke(this, index);
        }

        void OnMenuClick(int index)
        {
            /*
            if (Listener == null)
            {
                Debug.Log($"{this}: No listener set for menu click {index}");
                return;
            }

            Listener.OnDialogMenuEntryClicked(this, index);
            */
            MenuEntryClicked?.Invoke(this, index);
        }

        void OnScrollUpClick()
        {
            if (MenuPage > 0)
            {
                MenuPage--;
            }
        }

        void OnScrollDownClick()
        {
            if ((MenuPage + 1) * menuButtons.Length < menuEntries.Length)
            {
                MenuPage++;
            }
        }

        Vector3 BaseDisplacement => -socketPosition * Scale;

        protected override void Awake()
        {
            base.Awake();

            connector.Start = () => Transform.localPosition - BaseDisplacement;
            connector.End = () =>
            {
                Vector3 framePosition = TfListener.RelativePositionToOrigin(Node.Transform.position) + PivotFrameOffset;

                return PivotDisplacement.MaxAbsCoeff() == 0
                    ? framePosition
                    : framePosition + GetFlatCameraRotation(framePosition) * PivotDisplacement;
            };

            if (button1 != null)
            {
                button1.Clicked += () => OnButtonClick(0);
            }

            if (button2 != null)
            {
                button2.Clicked += () => OnButtonClick(0);
            }

            if (button3 != null)
            {
                button3.Clicked += () => OnButtonClick(1);
            }

            for (int i = 0; i < menuButtons.Length; i++)
            {
                int j = i;
                menuButtons[i].Clicked += () => OnMenuClick(j);
            }

            if (upButton != null)
            {
                upButton.Clicked += OnScrollUpClick;
            }

            if (downButton != null)
            {
                downButton.Clicked += OnScrollDownClick;
            }

            if (iconMeshRenderer != null)
            {
                iconMeshRenderer.material = IconMaterial;
            }
        }

        void Update()
        {
            if (popupStartTime != null)
            {
                float now = Time.time;
                if (now - popupStartTime.Value < PopupDuration)
                {
                    float tempScale = (now - popupStartTime.Value) / PopupDuration;
                    transform.localScale = (tempScale * 0.5f + 0.5f) * Scale * Vector3.one;
                }
                else
                {
                    transform.localScale = Scale * Vector3.one;
                    popupStartTime = null;
                }
            }

            UpdatePosition();
            UpdateRotation();
        }

        void UpdateRotation()
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(Transform.position - Settings.MainCameraTransform.position);
            if (resetOrientation)
            {
                Transform.rotation = targetRotation;
                resetOrientation = false;
                return;
            }

            Quaternion currentRotation = Transform.rotation;
            Transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, 0.05f);
        }

        void UpdatePosition()
        {
            if (PivotFrameId == null)
            {
                return;
            }

            Vector3 frameLocalPosition = TfListener.RelativePositionToOrigin(Node.Transform.position) + PivotFrameOffset;
            Quaternion cameraLocalRotation = GetFlatCameraRotation(frameLocalPosition);

            Vector3 targetLocalPosition = frameLocalPosition + cameraLocalRotation * DialogDisplacement + BaseDisplacement;
            Vector3 targetAbsolutePosition = TfListener.OriginFrame.Transform.TransformPoint(targetLocalPosition);
            if (currentPosition == null)
            {
                currentPosition = targetAbsolutePosition;
            }
            else
            {
                Vector3 deltaPosition = targetAbsolutePosition - Transform.position;
                if (deltaPosition.MaxAbsCoeff() < 0.001f)
                {
                    return;
                }

                currentPosition = currentPosition.Value + deltaPosition * 0.05f;
            }


            Transform.position = currentPosition.Value;
        }

        static Quaternion GetFlatCameraRotation(in Vector3 localPosition)
        {
            Vector3 absolutePosition = TfListener.OriginFrame.Transform.TransformPoint(localPosition);
            (float x, _, float z) = absolutePosition - Settings.MainCameraTransform.position;
            float targetAngle = -Mathf.Atan2(z, x) * Mathf.Rad2Deg + 90;
            Quaternion absoluteRotation = Quaternion.AngleAxis(targetAngle, Vector3.up);

            return Quaternion.Inverse(TfListener.OriginFrame.Transform.rotation) * absoluteRotation;
        }

        public void Initialize()
        {
            Transform.SetParentLocal(TfListener.OriginFrame.Transform);

            connector.Visible = true;
            resetOrientation = true;

            popupStartTime = Time.time;
            Update();
        }

        public override void Suspend()
        {
            base.Suspend();
            connector.Visible = false;
            currentPosition = null;
            popupStartTime = null;
            ButtonClicked = null;
            MenuEntryClicked = null;

            SuspendButton(button1);
            SuspendButton(button2);
            SuspendButton(button3);
            foreach (var menuButton in menuButtons)
            {
                SuspendButton(menuButton);
            }

            Expired?.Invoke(this);
            Expired = null;
        }

        static void SuspendButton([CanBeNull] ARButton button)
        {
            if (button != null)
            {
                button.OnDialogDisabled();
            }
        }

        [NotNull]
        public override string ToString()
        {
            return $"[Dialog Id='{Id}']";
        }

        public void SplitForRecycle()
        {
            connector.SplitForRecycle();
        }

        public void OnDestroy()
        {
            if (node == null)
            {
                return;
            }

            node.DestroySelf();
        }
    }
}