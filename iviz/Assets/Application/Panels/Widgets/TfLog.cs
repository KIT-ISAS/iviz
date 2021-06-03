using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using TMPro;
using System;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class TfLog : MonoBehaviour, IWidget
    {
        static TfLog Instance;

        [SerializeField] TMP_Text tfText = null;
        [SerializeField] Text tfName = null;
        [SerializeField] GameObject content = null;
        [SerializeField] Button gotoButton = null;
        [SerializeField] Button trail = null;

        [SerializeField] Button lockPivot = null;
        [SerializeField] Button fixedFrame = null;

        //[SerializeField] Button lock1PV = null;
        [SerializeField] Text trailText = null;
        [SerializeField] Text lockPivotText = null;

        //[SerializeField] Text lock1PVText = null;
        [SerializeField] LinkResolver tfLink = null;

        readonly StringBuilder str = new StringBuilder(65536);
        FrameNode placeHolder;
        bool isInitialized;
        [CanBeNull] TfFrame selectedFrame;

        public event Action Close;

        [CanBeNull]
        public TfFrame SelectedFrame
        {
            get => selectedFrame;
            set
            {
                if (value == selectedFrame)
                {
                    return;
                }

                if (selectedFrame != null)
                {
                    selectedFrame.RemoveListener(placeHolder);
                }

                selectedFrame = value;

                if (selectedFrame != null)
                {
                    selectedFrame.AddListener(placeHolder);
                }


                bool interactable = selectedFrame != null;
                gotoButton.interactable = interactable;
                trail.interactable = interactable;
                lockPivot.interactable = interactable;
                fixedFrame.interactable = interactable;
                //lock1PV.interactable = interactable;

                if (value == null)
                {
                    tfName.text = "<color=grey>[ ⮑none ]</color>";
                }
                else
                {
                    tfName.text = value.Id == TfListener.FixedFrameId
                        ? $"[ ⮑{value.Id}] <i>[Fixed]</i>"
                        : $"[ ⮑{value.Id}]";
                    TfListener.Instance.HighlightFrame(value.Id);
                }

                Flush();
                UpdateFrameTexts();
            }
        }

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;
            Instance = this;

            tfLink.LinkClicked += OnLinkClicked;
            SelectedFrame = null;

            placeHolder = FrameNode.Instantiate("TFLog Placeholder");

            gotoButton.interactable = false;
            trail.interactable = false;
            lockPivot.interactable = false;
            fixedFrame.interactable = false;
            //lock1PV.interactable = false;
            tfName.text = "<color=grey>[ ⮑none ]</color>";

            UpdateFrameTexts();
        }

        void OnLinkClicked([CanBeNull] string frameId)
        {
            SelectedFrame =
                frameId == null || !TfListener.TryGetFrame(frameId, out TfFrame frame)
                    ? null
                    : frame;
        }

        readonly struct TfNode : IComparable<TfNode>
        {
            readonly string name;
            readonly TfNode[] children;
            readonly Pose pose;
            readonly bool hasTrail;
            readonly bool selected;

            public TfNode([NotNull] TfFrame frame)
            {
                name = frame.Id;
                pose = frame.OriginWorldPose;
                hasTrail = frame.TrailVisible;
                selected = (frame == Instance.SelectedFrame);
                children = new TfNode[frame.Children.Count];

                int i = 0;
                foreach (var childFrame in frame.Children)
                {
                    children[i++] = new TfNode(childFrame);
                }

                Array.Sort(children);
            }

            void Write([NotNull] StringBuilder str, int level)
            {
                str.Append(' ', level * 4);
                str.Append("<link=").Append(name).Append("><u><font=Bold>");

                Vector3 position = pose.position.Unity2Ros();
                string x = position.x.ToString("#,0.###", UnityUtils.Culture);
                string y = position.y.ToString("#,0.###", UnityUtils.Culture);
                string z = position.z.ToString("#,0.###", UnityUtils.Culture);

                if (selected)
                {
                    str.Append("<color=blue>");
                    if (hasTrail)
                    {
                        str.Append("~");
                    }

                    str.Append(name);
                    if (hasTrail)
                    {
                        str.Append("~");
                    }

                    str.Append("</color>");
                }
                else if (name == TfListener.FixedFrameId)
                {
                    str.Append("<color=green>");
                    if (hasTrail)
                    {
                        str.Append("~");
                    }

                    str.Append(name);
                    if (hasTrail)
                    {
                        str.Append("~");
                    }

                    str.Append("</color>");
                }
                else
                {
                    str.Append(name);
                }

                str.Append("</font></u> [")
                    .Append(x).Append(", ")
                    .Append(y).Append(", ")
                    .Append(z)
                    .Append("]</link>")
                    .AppendLine();

                foreach (TfNode node in children)
                {
                    node.Write(str, level + 1);
                }
            }

            public void Write([NotNull] StringBuilder str)
            {
                foreach (TfNode node in children)
                {
                    node.Write(str, 0);
                }
            }

            public int CompareTo(TfNode other)
            {
                return string.CompareOrdinal(name, other.name);
            }
        }

        public void Flush()
        {
            Initialize();

            str.Length = 0;

            new TfNode(TfListener.OriginFrame).Write(str);

            str.AppendLine().AppendLine();

            tfText.SetText(str);

            RectTransform cTransform = (RectTransform) content.transform;
            cTransform.sizeDelta = new Vector2(tfText.preferredWidth + 10, tfText.preferredHeight + 10);
        }

        public void ClearSubscribers()
        {
        }

        public void OnGotoClicked()
        {
            if (SelectedFrame == null)
            {
                return;
            }

            TfListener.Instance.HighlightFrame(SelectedFrame.Id);
            ModuleListPanel.GuiInputModule.LookAt(SelectedFrame.AbsoluteUnityPose.position);
        }

        public void OnTrailClicked()
        {
            if (SelectedFrame != null)
            {
                SelectedFrame.TrailVisible = !SelectedFrame.TrailVisible;
            }

            UpdateFrameTexts();
        }

        public void OnFixedFrameClicked()
        {
            if (SelectedFrame != null)
            {
                TfListener.Instance.HighlightFrame(SelectedFrame.Id);
                TfListener.FixedFrameId = SelectedFrame.Id;
            }

            UpdateFrameTexts();
        }

        public void OnLockPivotClicked()
        {
            ModuleListPanel.GuiInputModule.OrbitCenterOverride =
                ModuleListPanel.GuiInputModule.OrbitCenterOverride == SelectedFrame
                    ? null
                    : SelectedFrame;

            UpdateFrameTexts();
            Close?.Invoke();
        }

        public void UpdateFrameTexts()
        {
            if (SelectedFrame == null)
            {
                trailText.text = "Trail:\nOff";
                lockPivotText.text = "Lock Pivot\nOff";
            }
            else
            {
                trailText.text = SelectedFrame.TrailVisible ? "Trail:\n<b>On</b>" : "Trail:\nOff";

                lockPivotText.text = ModuleListPanel.GuiInputModule.OrbitCenterOverride == SelectedFrame
                    ? "Lock Pivot\n<b>On</b>"
                    : "Lock Pivot\nOff";
            }
        }

        public void OnLock1PVClicked()
        {
            ModuleListPanel.GuiInputModule.CameraViewOverride =
                ModuleListPanel.GuiInputModule.CameraViewOverride == SelectedFrame
                    ? null
                    : SelectedFrame;

            UpdateFrameTexts();
            Close?.Invoke();
        }
    }
}