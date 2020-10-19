using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using TMPro;
using System;
using Iviz.Controllers;

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
        //[SerializeField] Button lock1PV = null;

        [SerializeField] Text trailText = null;
        [SerializeField] Text lockPivotText = null;
        //[SerializeField] Text lock1PVText = null;

        [SerializeField] TFLink tfLink = null;

        static TFLog Instance;
        
        public event Action Close;

        SimpleDisplayNode dummy;

        TfFrame selectedFrame;

        public TfFrame SelectedFrame
        {
            get => selectedFrame;
            set
            {
                if (value == selectedFrame)
                {
                    return;
                }

                selectedFrame?.RemoveListener(dummy);
                selectedFrame = value;
                selectedFrame?.AddListener(dummy);


                bool interactable = selectedFrame != null;
                gotoButton.interactable = interactable;
                trail.interactable = interactable;
                lockPivot.interactable = interactable;
                //lock1PV.interactable = interactable;

                if (selectedFrame == null)
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

            dummy = SimpleDisplayNode.Instantiate("TFLog Dummy", TFListener.ListenersFrame.transform);

            gotoButton.interactable = false;
            trail.interactable = false;
            lockPivot.interactable = false;
            //lock1PV.interactable = false;
            tfName.text = "<color=grey>[ ⮑none ]</color>";

            UpdateFrameTexts();
        }

        void OnTfLinkClicked(string frameId)
        {
            if (frameId == null || !TFListener.TryGetFrame(frameId, out TfFrame frame))
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
            bool HasTrail { get; }
            bool Selected { get; }

            public TFNode(TfFrame frame)
            {
                Name = frame.Id;
                Children = new List<TFNode>();
                Pose = frame.WorldPose;
                HasTrail = frame.TrailVisible;
                Selected = (frame == Instance.SelectedFrame);

                var childrenList = frame.Children;
                foreach (TfFrame child in childrenList.Values)
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

                string decoratedName = Name;
                if (HasTrail)
                {
                    decoratedName = $"~{decoratedName}~";
                }
                if (Selected)
                {
                    decoratedName = $"<color=blue>{decoratedName}</color>";
                }
                
                str.Append(tabs).AppendLine($"<link={Name}><u><font=Bold>{decoratedName}</font></u> [{x}, {y}, {z}]</link>");

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
            Initialize();
            
            TFNode root = new TFNode(TFListener.RootFrame);

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
            TFListener.GuiCamera.LookAt(SelectedFrame.AbsolutePose.position);
            Close?.Invoke();
        }

        public void OnTrailClicked()
        {
            SelectedFrame.TrailVisible = !SelectedFrame.TrailVisible;
            UpdateFrameTexts();
        }

        public void OnLockPivotClicked()
        {
            if (TFListener.GuiCamera.OrbitCenterOverride == SelectedFrame)
            {
                TFListener.GuiCamera.OrbitCenterOverride = null;
            }
            else
            {
                TFListener.GuiCamera.OrbitCenterOverride = SelectedFrame;
            }

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

                if (TFListener.GuiCamera.OrbitCenterOverride == SelectedFrame)
                {
                    lockPivotText.text = "Lock Pivot\n<b>On</b>";
                }
                else
                {
                    lockPivotText.text = "Lock Pivot\nOff";
                }

                /*
                if (TFListener.GuiManager.CameraViewOverride == SelectedFrame)
                {
                    lock1PVText.text = "Lock 1PV\n<b>On</b>";
                }
                else
                {
                    lock1PVText.text = "Lock 1PV\nOff";
                }
                */
            }
        }

        public void OnLock1PVClicked()
        {
            if (TFListener.GuiCamera.CameraViewOverride == SelectedFrame)
            {
                TFListener.GuiCamera.CameraViewOverride = null;
            }
            else
            {
                TFListener.GuiCamera.CameraViewOverride = SelectedFrame;
            }

            UpdateFrameTexts();
            Close?.Invoke();
        }
    }
}