using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Sdf;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using Material = UnityEngine.Material;

namespace Iviz.App.ARDialogs
{
    public class ARDialog : MarkerResource
    {
        [SerializeField] TextMesh title;
        [SerializeField] TextMesh caption;
        [SerializeField] ARButton button1;
        [SerializeField] ARButton button2;
        [SerializeField] ARButton button3;
        [SerializeField] ARLineConnector connector;
        [SerializeField] MeshRenderer iconMeshRenderer;

        [SerializeField] Texture2D[] icons;

        [SerializeField] ARButton[] menuButtons;
        [SerializeField] ARButton upButton;
        [SerializeField] ARButton downButton;

        Material material;
        Material Material => material != null ? material : (material = Instantiate(iconMeshRenderer.material));

        float? currentAngle;
        Vector3? currentPosition;

        ModeType mode;
        FrameNode node;

        public event Action<int> ButtonClicked;
        public event Action<int> MenuClicked;

        public enum ModeType
        {
            Ok,
            YesNo,
            OkCancel,
            Forward,
            ForwardBackward,
        }

        public string Caption
        {
            get => caption.text;
            set => caption.text = value;
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
                    connector.End = () => node.Transform.position;
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

        public ModeType Mode
        {
            get => mode;
            set
            {
                mode = value;

                if (button1 == null || button2 == null || button3 == null)
                {
                    throw new InvalidOperationException("This asset does not support buttons");
                }

                button1.Active = false;
                button2.Active = false;
                button3.Active = false;
                switch (value)
                {
                    case ModeType.Ok:
                        button1.Active = true;
                        button1.Icon = ARButton.ButtonIcon.Ok;
                        button1.Caption = "Ok";
                        break;
                    case ModeType.Forward:
                        button1.Active = true;
                        button1.Icon = ARButton.ButtonIcon.Forward;
                        button1.Caption = "Ok";
                        break;
                    case ModeType.YesNo:
                        button2.Active = true;
                        button2.Icon = ARButton.ButtonIcon.Ok;
                        button2.Caption = "Yes";
                        button3.Active = true;
                        button3.Icon = ARButton.ButtonIcon.Cross;
                        button3.Caption = "No";
                        break;
                }
            }
        }

        public enum IconType
        {
            Error,
            Info,
            Warning,
            Dialog,
            Dialogs,
            Question
        }

        IconType icon;

        public IconType Icon
        {
            get => icon;
            set
            {
                icon = value;
                if (iconMeshRenderer == null)
                {
                    throw new InvalidOperationException("This asset does not support icons");
                }

                Material.mainTexture = icons[(int) value];
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
                    throw new InvalidOperationException("This asset does not support menus");
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
            }
        }

        void OnButtonClick(int index)
        {
            ButtonClicked?.Invoke(index);
        }

        void OnMenuClick(int index)
        {
            MenuClicked?.Invoke(index + menuButtons.Length * MenuPage);
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

        protected override void Awake()
        {
            base.Awake();

            connector.Start = () => Transform.position + new Vector3(0, -0.6f, 0);
            connector.End = () => node.Transform.position + PivotFrameOffset;

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

            MenuEntries = new[] {"A", "B", "C"};
        }

        void Update()
        {
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

            (float x, _, float z) = framePosition - Settings.MainCameraTransform.position;
            float targetAngle = -Mathf.Atan2(z, x) * Mathf.Rad2Deg + 90;
            Quaternion cameraRotation = Quaternion.AngleAxis(targetAngle, Vector3.up);

            Vector3 targetPosition = framePosition + cameraRotation * DialogDisplacement;
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

        public void Shutdown()
        {
        }
    }
}