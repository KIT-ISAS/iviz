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
        [SerializeField] TMP_Text tfText = null;
        [SerializeField] TMP_Text tfName = null;
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

        [SerializeField] DropdownWidget showAs = null;
        [SerializeField] DropdownWidget poseAs = null;

        bool showAsTree = true;

        enum PoseDisplayType
        {
            ToRoot,
            ToParent,
            ToFixed
        }

        PoseDisplayType poseDisplay;

        readonly StringBuilder description = new StringBuilder(65536);
        uint? descriptionHash;
        uint? textHash;

        readonly List<TfNode> nodes = new List<TfNode>();

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
                    if (value != null)
                    {
                        TfListener.Instance.HighlightFrame(value.Id);
                    }

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

                Flush();
                if (value != null)
                {
                    TfListener.Instance.HighlightFrame(value.Id);
                }

                UpdateFrameButtons();
            }
        }

        void Awake()
        {
            Initialize();

            showAs.Options = new[]
            {
                "Show as Tree",
                "Show as List",
            };
            poseAs.Options = new[]
            {
                "Pose to Root",
                "Relative to Parent",
                "Relative to Fixed"
            };

            showAs.ValueChanged += (i, _) =>
            {
                showAsTree = i == 0;
                Flush();
            };

            poseAs.ValueChanged += (i, _) =>
            {
                poseDisplay = (PoseDisplayType) i;
                UpdateFrameText();
            };
        }

        void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            tfLink.LinkClicked += OnLinkClicked;
            SelectedFrame = null;

            placeHolder = FrameNode.Instantiate("TFLog Placeholder");

            gotoButton.interactable = false;
            trail.interactable = false;
            lockPivot.interactable = false;
            fixedFrame.interactable = false;
            //lock1PV.interactable = false;
            SelectedFrame = TfListener.GetOrCreateFrame("map");

            UpdateFrameButtons();
        }

        void OnLinkClicked([CanBeNull] string frameId)
        {
            SelectedFrame =
                frameId == null || !TfListener.TryGetFrame(frameId, out TfFrame frame)
                    ? null
                    : frame;
        }

        public void Flush()
        {
            Initialize();

            if (showAsTree)
            {
                UpdateFrameListAsTree();
            }
            else
            {
                UpdateFrameListAsList();
            }

            UpdateFrameText();
        }

        void UpdateFrameListAsTree()
        {
            description.Clear();
            new TfNode(TfListener.OriginFrame, SelectedFrame).Write(description);

            description.AppendLine().AppendLine();
            uint newHash = Crc32Calculator.Instance.Compute(description);
            if (newHash == descriptionHash)
            {
                return;
            }

            descriptionHash = newHash;
            tfText.SetText(description);
            RectTransform cTransform = (RectTransform) content.transform;
            cTransform.sizeDelta = new Vector2(tfText.preferredWidth + 10, tfText.preferredHeight + 10);
        }

        void UpdateFrameListAsList()
        {
            nodes.Clear();
            new TfNode(TfListener.OriginFrame, SelectedFrame).AddTo(nodes);

            description.Clear();
            nodes.Sort();
            foreach (var node in nodes)
            {
                node.WriteSingle(description);
            }

            description.AppendLine().AppendLine();
            uint newHash = Crc32Calculator.Instance.Compute(description);
            if (newHash == descriptionHash)
            {
                return;
            }

            descriptionHash = newHash;
            tfText.SetText(description);

            RectTransform cTransform = (RectTransform) content.transform;
            cTransform.sizeDelta = new Vector2(tfText.preferredWidth + 10, tfText.preferredHeight + 10);
        }

        void UpdateFrameText()
        {
            if (!isInitialized)
            {
                return;
            }

            description.Clear();
            if (SelectedFrame == null)
            {
                description.Append("<color=grey>[none]</color>");
            }
            else
            {
                string id = SelectedFrame.Id;
                string nameText = id == TfListener.FixedFrameId
                    ? $"<font=Bold>[{id}]</font>  <i>[Fixed]</i>"
                    : $"<font=Bold>[{id}]</font>";
                string parentId =
                    SelectedFrame.Parent == null || SelectedFrame.Parent == TfListener.OriginFrame
                        ? "[no parent]"
                        : SelectedFrame.Parent.Id;


                Pose pose;
                switch (poseDisplay)
                {
                    case PoseDisplayType.ToRoot:
                        pose = SelectedFrame.OriginWorldPose;
                        break;
                    case PoseDisplayType.ToFixed:
                        pose = TfListener.RelativePoseToFixedFrame(SelectedFrame.AbsoluteUnityPose);
                        break;
                    case PoseDisplayType.ToParent:
                        pose = SelectedFrame.Transform.AsLocalPose();
                        break;
                    default:
                        pose = Pose.identity; // shouldn't happen
                        break;
                }

                var ((pX, pY, pZ), (rX, rY, rZ, rW)) = pose.Unity2RosPose();
                string px = pX.ToString("#,0.###", UnityUtils.Culture);
                string py = pY.ToString("#,0.###", UnityUtils.Culture);
                string pz = pZ.ToString("#,0.###", UnityUtils.Culture);

                string rx = rX.ToString("#,0.###", UnityUtils.Culture);
                string ry = rY.ToString("#,0.###", UnityUtils.Culture);
                string rz = rZ.ToString("#,0.###", UnityUtils.Culture);
                string rw = rW.ToString("#,0.###", UnityUtils.Culture);

                string poseStr = $"t:[{px}, {py}, {pz}]  r:[{rx}, {ry}, {rz}, {rw}]";

                description.AppendLine(nameText).AppendLine(parentId).AppendLine(poseStr);
            }
            
            uint newHash = Crc32Calculator.Instance.Compute(description);
            if (newHash == textHash)
            {
                return;
            }

            textHash = newHash;
            tfName.SetText(description);
        }

        public void UpdateFrameButtons()
        {
            if (SelectedFrame == null)
            {
                trailText.text = "Trail:\nOff";
                lockPivotText.text = "Lock Pivot\nOff";
            }
            else
            {
                trailText.text = SelectedFrame.TrailVisible ? "Trail:\n<b>On</b>" : "Trail:\nOff";

                if (ModuleListPanel.GuiInputModule != null)
                {
                    lockPivotText.text = ModuleListPanel.GuiInputModule.OrbitCenterOverride == SelectedFrame
                        ? "Lock Pivot\n<b>On</b>"
                        : "Lock Pivot\nOff";
                }
            }
        }

        public void OnGotoClicked()
        {
            if (SelectedFrame == null)
            {
                return;
            }

            TfListener.Instance.HighlightFrame(SelectedFrame.Id);
            if (ModuleListPanel.GuiInputModule != null)
            {
                ModuleListPanel.GuiInputModule.LookAt(SelectedFrame.AbsoluteUnityPose.position);
            }
        }

        public void OnTrailClicked()
        {
            if (SelectedFrame != null)
            {
                SelectedFrame.TrailVisible = !SelectedFrame.TrailVisible;
            }

            UpdateFrameButtons();
        }

        public void OnFixedFrameClicked()
        {
            if (SelectedFrame != null)
            {
                TfListener.Instance.HighlightFrame(SelectedFrame.Id);
                TfListener.FixedFrameId = SelectedFrame.Id;
            }

            UpdateFrameButtons();
        }

        public void OnLockPivotClicked()
        {
            if (ModuleListPanel.GuiInputModule != null)
            {
                ModuleListPanel.GuiInputModule.OrbitCenterOverride =
                    ModuleListPanel.GuiInputModule.OrbitCenterOverride == SelectedFrame
                        ? null
                        : SelectedFrame;
            }

            UpdateFrameButtons();
            Close?.Invoke();
        }


        public void OnLock1PVClicked()
        {
            if (ModuleListPanel.GuiInputModule != null)
            {
                ModuleListPanel.GuiInputModule.CameraViewOverride =
                    ModuleListPanel.GuiInputModule.CameraViewOverride == SelectedFrame
                        ? null
                        : SelectedFrame;
            }

            UpdateFrameButtons();
            Close?.Invoke();
        }

        public void ClearSubscribers()
        {
        }
    }

    internal readonly struct TfNode : IComparable<TfNode>
    {
        readonly string name;
        readonly TfNode[] children;
        readonly bool hasTrail;
        readonly bool selected;

        public TfNode([NotNull] TfFrame frame, [CanBeNull] TfFrame selectedFrame)
        {
            name = frame.Id;
            hasTrail = frame.TrailVisible;
            selected = (frame == selectedFrame);
            children = new TfNode[frame.Children.Count];

            int i = 0;
            foreach (var childFrame in frame.Children)
            {
                children[i++] = new TfNode(childFrame, selectedFrame);
            }

            Array.Sort(children);
        }

        void Write([NotNull] StringBuilder str, int level, bool withChildren)
        {
            if (level != 0)
            {
                str.Append(' ', level * 4);
            }

            str.Append("<link=").Append(name).Append("><font=Bold>");

            if (selected)
            {
                str.Append("<color=blue>");
                if (hasTrail)
                {
                    str.Append("~");
                }

                str.Append("<u>").Append(name).Append("</u>");
                if (hasTrail)
                {
                    str.Append("~");
                }

                str.Append("</color>");
            }
            else if (name == TfListener.FixedFrameId)
            {
                str.Append("<color=#008800>");
                if (hasTrail)
                {
                    str.Append("~");
                }

                str.Append("<u>").Append(name).Append("</u>");
                if (hasTrail)
                {
                    str.Append("~");
                }

                str.Append("</color>");
            }
            else
            {
                str.Append("<u>").Append(name).Append("</u>");
            }

            str.AppendLine("</font></link>");

            if (withChildren)
            {
                foreach (TfNode node in children)
                {
                    node.Write(str, level + 1, true);
                }
            }
        }

        public void Write([NotNull] StringBuilder str)
        {
            foreach (TfNode node in children)
            {
                node.Write(str, 0, true);
            }
        }

        public void WriteSingle([NotNull] StringBuilder str)
        {
            Write(str, 0, false);
        }


        public int CompareTo(TfNode other)
        {
            return string.CompareOrdinal(name, other.name);
        }

        public void AddTo([NotNull] List<TfNode> nodes)
        {
            foreach (TfNode node in children)
            {
                nodes.Add(node);
                node.AddTo(nodes);
            }
        }
    }
}