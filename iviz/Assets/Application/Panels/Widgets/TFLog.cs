using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using TMPro;
using System;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.App
{
    public class TFLog : MonoBehaviour, IWidget
    {
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
        [SerializeField] TFLink tfLink = null;

        static TFLog Instance;

        public event Action Close;

        FrameNode placeHolder;

        [CanBeNull] TfFrame selectedFrame;

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
                    tfName.text = "[ ⮑" + value.Id + "]";
                }

                Flush();
                UpdateFrameTexts();
            }
        }

        void Awake()
        {
            Initialize();
        }

        bool isAwake;

        void Initialize()
        {
            if (isAwake)
            {
                return;
            }

            isAwake = true;
            Instance = this;

            tfLink.LinkClicked += OnTfLinkClicked;
            SelectedFrame = null;

            placeHolder = FrameNode.Instantiate("TFLog Placeholder", TfListener.ListenersFrame.transform);

            gotoButton.interactable = false;
            trail.interactable = false;
            lockPivot.interactable = false;
            fixedFrame.interactable = false;
            //lock1PV.interactable = false;
            tfName.text = "<color=grey>[ ⮑none ]</color>";

            UpdateFrameTexts();
        }

        void OnTfLinkClicked([CanBeNull] string frameId)
        {
            SelectedFrame =
                frameId == null || !TfListener.TryGetFrame(frameId, out TfFrame frame)
                    ? null
                    : frame;
        }

        class TfNode
        {
            string Name { get; }
            List<TfNode> Children { get; }
            Pose Pose { get; }
            bool HasTrail { get; }
            bool Selected { get; }

            public TfNode([NotNull] TfFrame frame)
            {
                Name = frame.Id;
                Children = new List<TfNode>();
                Pose = frame.WorldPose;
                HasTrail = frame.TrailVisible;
                Selected = (frame == Instance.SelectedFrame);

                var childrenList = frame.Children;
                foreach (TfFrame child in childrenList)
                {
                    Children.Add(new TfNode(child));
                }

                Children.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            }

            void Write([NotNull] StringBuilder str, int level)
            {
                string tabs = new string(' ', level * 4);

                Vector3 position = Pose.position.Unity2Ros();
                string x = position.x.ToString("#,0.###", UnityUtils.Culture);
                string y = position.y.ToString("#,0.###", UnityUtils.Culture);
                string z = position.z.ToString("#,0.###", UnityUtils.Culture);

                string decoratedName = Name;
                if (HasTrail)
                {
                    decoratedName = $"~{decoratedName}~";
                }

                if (Selected)
                {
                    decoratedName = $"<color=blue>{decoratedName}</color>";
                }
                else if (Name == TfListener.FixedFrameId)
                {
                    decoratedName = $"<color=green>{decoratedName}</color>";
                }


                str.Append(tabs)
                    .AppendLine($"<link={Name}><u><font=Bold>{decoratedName}</font></u> [{x}, {y}, {z}]</link>");

                foreach (TfNode node in Children)
                {
                    node.Write(str, level + 1);
                }
            }

            public void Write([NotNull] StringBuilder str)
            {
                foreach (TfNode node in Children)
                {
                    node.Write(str, 0);
                }
            }
        }

        public void Flush()
        {
            if (Settings.IsHololens)
            {
                return;
            }
            
            Initialize();

            TfNode root = new TfNode(TfListener.OriginFrame);

            StringBuilder str = new StringBuilder();
            root.Write(str);

            tfText.text = str.ToString();

            RectTransform ctransform = (RectTransform) content.transform;
            ctransform.sizeDelta = new Vector2(tfText.preferredWidth + 10, tfText.preferredHeight + 10);
        }

        public void ClearSubscribers()
        {
        }

        public void OnGotoClicked()
        {
            if (SelectedFrame != null)
            {
                TfListener.GuiInputModule.LookAt(SelectedFrame.AbsolutePose.position);
            }

            Close?.Invoke();
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
                TfListener.FixedFrameId = SelectedFrame.Id;
            }

            UpdateFrameTexts();
        }

        public void OnLockPivotClicked()
        {
            TfListener.GuiInputModule.OrbitCenterOverride =
                TfListener.GuiInputModule.OrbitCenterOverride == SelectedFrame
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
                //lock1PVText.text = "Lock 1PV\nOff";
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

                if (TfListener.GuiInputModule.OrbitCenterOverride == SelectedFrame)
                {
                    lockPivotText.text = "Lock Pivot\n<b>On</b>";
                }
                else
                {
                    lockPivotText.text = "Lock Pivot\nOff";
                }
            }
        }

        public void OnLock1PVClicked()
        {
            TfListener.GuiInputModule.CameraViewOverride =
                TfListener.GuiInputModule.CameraViewOverride == SelectedFrame
                    ? null
                    : SelectedFrame;

            UpdateFrameTexts();
            Close?.Invoke();
        }
    }
}