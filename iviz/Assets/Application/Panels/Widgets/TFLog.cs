using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using Iviz.App.Listeners;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace Iviz.App
{
    public class TFLog : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text tfText = null;
        [SerializeField] Text tfName = null;
        [SerializeField] GameObject content = null;

        [SerializeField] Button gotoButton;
        [SerializeField] Button trail;
        [SerializeField] Button lockPivot;
        [SerializeField] Button lock1PV;

        [SerializeField] Text trailText;
        [SerializeField] Text lockPivotText;
        [SerializeField] Text lock1PVText;

        [SerializeField] TFLink tfLink = null;

        public event Action Close;

        TFFrame selectedFrame;
        public TFFrame SelectedFrame
        {
            get => selectedFrame;
            set
            {
                selectedFrame = value;

                gotoButton.interactable = selectedFrame != null;
                trail.interactable = selectedFrame != null;
                lockPivot.interactable = selectedFrame != null;
                lock1PV.interactable = selectedFrame != null;

                if (selectedFrame == null)
                {
                    tfName.text = "<color=grey>[ ⮑none ]</color>";
                }
                else
                {
                    tfName.text = "[ ⮑" + value.Id + "]";
                }
                UpdateText();
            }
        }

        void Awake()
        {
            tfLink.LinkClicked += OnTfLinkClicked;
            SelectedFrame = null;

        }

        void OnTfLinkClicked(string frameId)
        {
            if (frameId == null || !TFListener.TryGetFrame(frameId, out TFFrame frame))
            {
                SelectedFrame = null;
            }
            else
            {
                SelectedFrame = frame;
            }
        }

        class TFNode
        {
            public string Name { get; }
            List<TFNode> Children { get; }
            Pose Pose { get; }

            public TFNode(TFFrame frame)
            {
                Name = frame.Id;
                Children = new List<TFNode>();
                Pose = frame.WorldPose;

                var childrenList = frame.Children;
                foreach (TFFrame child in childrenList.Values)
                {
                    Children.Add(new TFNode(child));
                }
                Children.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            }

            void Write(StringBuilder str, int level)
            {
                string tabs = new string(' ', level * 4);

                Vector3 position = Pose.position.Unity2Ros();
                string x = position.x.ToString("#,0.#", UnityUtils.Culture);
                string y = position.y.ToString("#,0.#", UnityUtils.Culture);
                string z = position.z.ToString("#,0.#", UnityUtils.Culture);

                str.Append(tabs).AppendLine($"<link={Name}><u><font=Bold>{Name}</font></u> [{x}, {y}, {z}]</link>");

                foreach (TFNode node in Children)
                {
                    node.Write(str, level + 1);
                }
            }

            public void Write(StringBuilder str)
            {
                foreach (TFNode node in Children)
                {
                    node.Write(str, 0);
                }
            }
        }

        public void Flush()
        {

            TFNode root = new TFNode(TFListener.RootFrame);

            StringBuilder str = new StringBuilder();
            root.Write(str);

            tfText.text = str.ToString();

            RectTransform ctransform = (RectTransform)content.transform;
            ctransform.sizeDelta = new Vector2(tfText.preferredWidth + 10, tfText.preferredHeight + 10);
        }

        public void ClearSubscribers()
        {
        }

        public void OnGotoClicked()
        {
            TFListener.GuiManager.LookAt(SelectedFrame.AbsolutePose.position);
            Close?.Invoke();
        }

        public void OnTrailClicked()
        {
            SelectedFrame.TrailVisible = !SelectedFrame.TrailVisible;
            UpdateText();
        }

        public void OnLockPivotClicked()
        {
            if (TFListener.GuiManager.OrbitCenterOverride == SelectedFrame)
            {
                TFListener.GuiManager.OrbitCenterOverride = null;
            }
            else
            {
                TFListener.GuiManager.OrbitCenterOverride = SelectedFrame;
            }
            UpdateText();
            Close?.Invoke();
        }

        void UpdateText()
        {
            if (SelectedFrame == null)
            {
                trailText.text = "Trail:\nOff";
                lockPivotText.text = "Lock Pivot\nOff";
                lock1PVText.text = "Lock 1PV\nOff";
            }
            else
            {
                if (SelectedFrame.TrailVisible)
                {
                    trailText.text = "Trail:\n<b>On</b>";
                }
                else
                {
                    trailText.text = "Trail:\nOff";
                }
                if (TFListener.GuiManager.OrbitCenterOverride == SelectedFrame)
                {
                    lockPivotText.text = "Lock Pivot\n<b>On</b>";
                }
                else
                {
                    lockPivotText.text = "Lock Pivot\nOff";
                }
                if (TFListener.GuiManager.CameraViewOverride == SelectedFrame)
                {
                    lock1PVText.text = "Lock 1PV\n<b>On</b>";
                }
                else
                {
                    lock1PVText.text = "Lock 1PV\nOff";
                }
            }
        }

        public void OnLock1PVClicked()
        {
            if (TFListener.GuiManager.CameraViewOverride == SelectedFrame)
            {
                TFListener.GuiManager.CameraViewOverride = null;
            }
            else
            {
                TFListener.GuiManager.CameraViewOverride = SelectedFrame;
            }
            UpdateText();
            Close?.Invoke();
        }
    }
}