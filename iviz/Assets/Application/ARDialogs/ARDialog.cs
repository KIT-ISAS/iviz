using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizCommonMsgs;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Material = UnityEngine.Material;

namespace Iviz.App.ARDialogs
{
    public class ARDialog : MarkerResource, IRecyclable
    {
        const float PopupDuration = 0.3f;
        
        [SerializeField] TextMesh title;
        [SerializeField] TMP_Text caption;
        [SerializeField] ARButton button1;
        [SerializeField] ARButton button2;
        [SerializeField] ARButton button3;
        [SerializeField] ARLineConnector connector;
        [SerializeField] MeshRenderer iconMeshRenderer;

        [SerializeField] Texture2D[] icons;

        [SerializeField] ARButton[] menuButtons;
        [SerializeField] ARButton upButton;
        [SerializeField] ARButton downButton;

        [SerializeField] Vector3 socketPosition;
        
        public string Id { get; internal set; }
        public GuiDialogListener Listener { get; internal set; }

        Material material;
        Material Material => material != null ? material : (material = Instantiate(iconMeshRenderer.material));

        float? popupStartTime;
        float? currentAngle;
        Vector3? currentPosition;

        FrameNode node;
        float scale;

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
                    node.AttachTo(pivotFrameId);
                    currentPosition = null;
                    currentAngle = null;
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

        public void SetButtonMode(DialogType value)
        {
            if (button1 == null || button2 == null || button3 == null)
            {
                Logger.Error($"{this}: Tried to set buttons in an asset that does not support them");
                return;
            }

            button1.Active = false;
            button2.Active = false;
            button3.Active = false;

            switch (value)
            {
                case DialogType.ButtonOk:
                    button1.Active = true;
                    button1.Icon = ARButton.ButtonIcon.Ok;
                    button1.Caption = "Ok";
                    break;
                case DialogType.ButtonForward:
                    button1.Active = true;
                    button1.Icon = ARButton.ButtonIcon.Forward;
                    button1.Caption = "Ok";
                    break;
                case DialogType.ButtonYesNo:
                    button2.Active = true;
                    button2.Icon = ARButton.ButtonIcon.Ok;
                    button2.Caption = "Yes";
                    button3.Active = true;
                    button3.Icon = ARButton.ButtonIcon.Cross;
                    button3.Caption = "No";
                    break;
                case DialogType.ButtonForwardBackward:
                    button2.Active = true;
                    button2.Icon = ARButton.ButtonIcon.Backward;
                    button2.Caption = "Back";
                    button3.Active = true;
                    button3.Icon = ARButton.ButtonIcon.Forward;
                    button3.Caption = "Forward";
                    break;
                case DialogType.ButtonOkCancel:
                    button2.Active = true;
                    button2.Icon = ARButton.ButtonIcon.Ok;
                    button2.Caption = "Ok";
                    button3.Active = true;
                    button3.Icon = ARButton.ButtonIcon.Cross;
                    button3.Caption = "Cancel";
                    break;
                default:
                    Logger.Error($"{this}: Invalid dialog type for mode {value}");
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
                Material.mainTexture = icons[(int) value];
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
                    Logger.Error($"{this}: Trying to set a menu on an asset that does not support them");
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

                for (int i = numActives; i < menuButtons.Length; i++)
                {
                    menuButtons[i].Visible = false;
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
                transform.localScale = scale * Vector3.one;
            }
        }

        void OnButtonClick(int index)
        {
            Listener.OnDialogButtonClicked(this, index);
        }

        void OnMenuClick(int index)
        {
            Listener.OnDialogMenuEntryClicked(this, index);
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


        //static readonly Vector3 BaseDisplacement = new Vector3(0, 0.6f, 0);
        Vector3 BaseDisplacement => -socketPosition * Scale;

        protected override void Awake()
        {
            base.Awake();

            connector.Start = () => Transform.position - BaseDisplacement;
            connector.End = () =>
            {
                Vector3 framePosition = node.Transform.position + PivotFrameOffset;

                return PivotDisplacement.MaxAbsCoeff() == 0
                    ? framePosition
                    : framePosition + GetFlatCameraRotation(framePosition) * PivotDisplacement;
            };

            node = FrameNode.Instantiate("ARDialog Node");

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
                iconMeshRenderer.material = Material;
            }
        }

        void Update()
        {
            if (popupStartTime != null)
            {
                float now = Time.time;
                if (now - popupStartTime.Value < PopupDuration)
                {
                    float scale = (now - popupStartTime.Value) / PopupDuration;
                    transform.localScale = (scale * 0.5f + 0.5f) * Vector3.one;
                }
                else
                {
                    transform.localScale = Vector3.one;
                    popupStartTime = null;
                }
            }

            UpdateRotation();
            UpdatePosition();
        }

        void UpdateRotation()
        {
            (float x, _, float z) = Transform.position - Settings.MainCameraTransform.position;
            float targetAngle = -Mathf.Atan2(z, x) * Mathf.Rad2Deg + 90;

            if (currentAngle == null)
            {
                currentAngle = targetAngle;
            }
            else
            {
                float alternativeAngle = targetAngle - 360;
                float closestAngle = Mathf.Abs(alternativeAngle - currentAngle.Value) <
                                     Mathf.Abs(targetAngle - currentAngle.Value)
                    ? alternativeAngle
                    : targetAngle;

                float deltaAngle = closestAngle - currentAngle.Value;
                if (Mathf.Abs(deltaAngle) < 1)
                {
                    return;
                }

                currentAngle = currentAngle.Value + deltaAngle * 0.05f;
                if (currentAngle > 180)
                {
                    currentAngle -= 360;
                }
                else if (currentAngle < -180)
                {
                    currentAngle += 360;
                }
            }

            Transform.rotation = Quaternion.AngleAxis(currentAngle.Value, Vector3.up);
        }

        void UpdatePosition()
        {
            if (PivotFrameId == null)
            {
                return;
            }

            Vector3 framePosition = node.Transform.position + PivotFrameOffset;
            Quaternion cameraRotation = GetFlatCameraRotation(framePosition);

            Vector3 targetPosition = framePosition + cameraRotation * DialogDisplacement + BaseDisplacement;

            if (currentPosition == null)
            {
                currentPosition = targetPosition;
            }
            else
            {
                Vector3 deltaPosition = targetPosition - Transform.position;
                if (deltaPosition.MaxAbsCoeff() < 0.001f)
                {
                    return;
                }

                currentPosition = currentPosition.Value + deltaPosition * 0.05f;
            }


            Transform.position = currentPosition.Value;
        }

        Quaternion GetFlatCameraRotation(in Vector3 framePosition)
        {
            (float x, _, float z) = framePosition - Settings.MainCameraTransform.position;
            float targetAngle = -Mathf.Atan2(z, x) * Mathf.Rad2Deg + 90;
            return Quaternion.AngleAxis(targetAngle, Vector3.up);
        }

        public void Initialize()
        {
            connector.Visible = true;
            popupStartTime = Time.time;
        }

        public void Shutdown()
        {
            connector.Visible = false;
        }

        public override string ToString()
        {
            return $"[Dialog Id='{Id}']";
        }

        public void SplitForRecycle()
        {
            connector.SplitForRecycle();
        }
    }
}