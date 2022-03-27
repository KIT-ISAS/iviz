#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using Material = UnityEngine.Material;

namespace Iviz.App.ARDialogs
{
    public sealed class ARDialog : MarkerDisplay, IRecyclable
    {
        const float PopupDuration = 0.1f;

        static readonly Color DefaultBackgroundColor = new Color(0, 0.2f, 0.5f);

        [SerializeField] TextMesh? title;
        [SerializeField] TMP_Text? caption;
        [SerializeField] XRButton? button1;
        [SerializeField] XRButton? button2;
        [SerializeField] XRButton? button3;
        [SerializeField] XRDialogConnector connector;
        [SerializeField] MeshRenderer? iconMeshRenderer;

        [SerializeField] Texture2D[] icons;

        [SerializeField] XRButton[] menuButtons;
        [SerializeField] XRButton? upButton;
        [SerializeField] XRButton? downButton;

        [SerializeField] Color backgroundColor = DefaultBackgroundColor;
        [SerializeField] MeshMarkerDisplay? background;

        [SerializeField] Vector3 socketPosition = Vector3.zero;

        public string Id { get; internal set; } = "";

        public event Action<ARDialog, int>? ButtonClicked;
        public event Action<ARDialog, int>? MenuEntryClicked;
        public event Action<ARDialog>? Expired;

        Material? iconMaterial;

        Material IconMaterial =>
            iconMaterial != null ? iconMaterial : (iconMaterial = Instantiate(iconMeshRenderer.material));

        TextMesh TitleObject => title.AssertNotNull(nameof(title));
        TMP_Text CaptionObject => caption.AssertNotNull(nameof(caption));

        FrameNode? node;
        string? pivotFrameId;

        Vector3? currentPosition;
        float scale;
        string[] menuEntries = Array.Empty<string>();
        int menuPage;
        bool resetOrientation;
        IconTextureType icon;


        FrameNode Node => node ??= FrameNode.Instantiate("Dialog Node");

        public TfFrame ParentFrame => Node.Parent ?? TfModule.DefaultFrame;

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

        static void SetBackgroundColor(XRButton? button, Color color)
        {
            if (button != null)
            {
                button.BackgroundColor = color;
            }
        }

        public string Caption
        {
            get => CaptionObject.text;
            set => CaptionObject.text = value;
        }

        public CaptionAlignmentType CaptionAlignment
        {
            get => (CaptionAlignmentType)CaptionObject.alignment;
            set => CaptionObject.alignment =
                value == CaptionAlignmentType.Default
                    ? TextAlignmentOptions.Center
                    : (TextAlignmentOptions)value;
        }

        public string Title
        {
            get => TitleObject.text;
            set => TitleObject.text = value;
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public string? PivotFrameId
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

                connector.Visible = pivotFrameId != null;
            }
        }

        public Vector3 DialogDisplacement { get; set; }
        public Vector3 PivotFrameOffset { get; set; }
        public Vector3 PivotDisplacement { get; set; }

        public void SetButtonMode(ButtonSetup value)
        {
            if (button1 == null)
            {
                RosLogger.Error($"{this}: Tried to set buttons in an asset that does not support them");
                return;
            }

            button1.Visible = false;
            if (button2 != null)
            {
                button2.Visible = false;
            }

            if (button3 != null)
            {
                button3.Visible = false;
            }

            switch (value)
            {
                case ButtonSetup.Ok:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Ok;
                    button1.Caption = "Ok";
                    break;
                case ButtonSetup.Forward:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Forward;
                    button1.Caption = "Ok";
                    break;
                case ButtonSetup.Backward:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Backward;
                    button1.Caption = "Back";
                    break;
                case ButtonSetup.YesNo:
                    if (button2 == null || button3 == null)
                    {
                        break;
                    }

                    button2.Visible = true;
                    button2.Icon = XRIcon.Ok;
                    button2.Caption = "Yes";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Cross;
                    button3.Caption = "No";
                    break;
                case ButtonSetup.ForwardBackward:
                    if (button2 == null || button3 == null)
                    {
                        break;
                    }

                    button2.Visible = true;
                    button2.Icon = XRIcon.Backward;
                    button2.Caption = "Back";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Forward;
                    button3.Caption = "Forward";
                    break;
                case ButtonSetup.OkCancel:
                    if (button2 == null || button3 == null)
                    {
                        break;
                    }

                    button2.Visible = true;
                    button2.Icon = XRIcon.Ok;
                    button2.Caption = "Ok";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Cross;
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

        IconTextureType Icon
        {
            get => icon;
            set
            {
                icon = value;
                IconMaterial.mainTexture = icons[(int)value];
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

        Vector3 BaseDisplacement => -socketPosition; /* * Scale; */

        void Awake()
        {
            /*
            connector.Start = () => Transform.localPosition - BaseDisplacement;
            connector.End = () =>
            {
                Vector3 framePosition = TfListener.RelativeToOrigin(Node.Transform.position) + PivotFrameOffset;

                return PivotDisplacement.MaxAbsCoeff() == 0
                    ? framePosition
                    : framePosition + GetFlatCameraRotation(framePosition) * PivotDisplacement;
            };
            */

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
            UpdatePosition();
            UpdateRotation();
        }

        void UpdateRotation()
        {
            var targetRotation = Quaternion.LookRotation(Transform.position - Settings.MainCameraTransform.position);
            if (resetOrientation)
            {
                Transform.rotation = targetRotation;
                resetOrientation = false;
                return;
            }

            var currentRotation = Transform.rotation;
            Transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, 0.05f);
        }

        void UpdatePosition()
        {
            if (PivotFrameId == null)
            {
                return;
            }

            var frameLocalPosition = TfModule.RelativeToOrigin(Node.Transform.position) + PivotFrameOffset;
            var cameraLocalRotation = GetFlatCameraRotation(frameLocalPosition);

            var targetLocalPosition = frameLocalPosition + cameraLocalRotation * DialogDisplacement + BaseDisplacement;
            var targetAbsolutePosition = TfModule.OriginTransform.TransformPoint(targetLocalPosition);

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
            var absolutePosition = TfModule.OriginTransform.TransformPoint(localPosition);
            (float x, _, float z) = absolutePosition - Settings.MainCameraTransform.position;
            float targetAngle = -Mathf.Atan2(z, x) * Mathf.Rad2Deg + 90;
            var absoluteRotation = Quaternion.AngleAxis(targetAngle, Vector3.up);

            return TfModule.OriginTransform.rotation.Inverse() * absoluteRotation;
        }

        public void Initialize()
        {
            Transform.SetParentLocal(TfModule.OriginTransform);

            connector.Visible = true;
            resetOrientation = true;
            Update();
        }

        public override void Suspend()
        {
            base.Suspend();
            connector.Visible = false;
            currentPosition = null;
            ButtonClicked = null;
            MenuEntryClicked = null;

            Expired?.Invoke(this);
            Expired = null;
        }

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
            node?.Dispose();
        }
    }
}