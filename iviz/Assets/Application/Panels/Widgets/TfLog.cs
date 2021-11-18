using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using TMPro;
using System;
using System.Linq;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class TfLog : MonoBehaviour, IWidget
    {
        enum PoseDisplayType
        {
            ToRoot,
            ToParent,
            ToFixed
        }

        [SerializeField] [CanBeNull] TMP_Text tfText = null;
        [SerializeField] [CanBeNull] TMP_Text tfName = null;
        [SerializeField] GameObject content = null;
        [SerializeField] Button gotoButton = null;
        [SerializeField] Button trail = null;

        [SerializeField] Button lockPivot = null;
        [SerializeField] Button fixedFrame = null;

        [SerializeField] Text trailText = null;
        [SerializeField] Text lockPivotText = null;

        [SerializeField] [CanBeNull] LinkResolver tfLink = null;
        [SerializeField] [CanBeNull] DropdownWidget showAs = null;
        [SerializeField] [CanBeNull] DropdownWidget poseAs = null;

        [NotNull] TMP_Text TfText => tfText.AssertNotNull(nameof(tfText)); 
        [NotNull] TMP_Text TfName => tfName.AssertNotNull(nameof(tfName)); 
        [NotNull] DropdownWidget ShowAs => showAs.AssertNotNull(nameof(showAs)); 
        [NotNull] DropdownWidget PoseAs => poseAs.AssertNotNull(nameof(poseAs)); 
        [NotNull] LinkResolver TfLink => tfLink.AssertNotNull(nameof(tfLink)); 
        
        readonly List<TfNode> nodes = new();

        bool showAsTree = true;
        PoseDisplayType poseDisplay;
        FrameNode placeHolder;

        uint? descriptionHash;
        uint? textHash;
        
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
                        value.Highlight();
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

                Flush();
                if (value != null)
                {
                    value.Highlight();
                }

                UpdateFrameButtons();
            }
        }

        void Awake()
        {
            Initialize();

            ShowAs.Options = new[]
            {
                "Show as Tree",
                "Show as List",
            };
            PoseAs.Options = new[]
            {
                "Pose to Root",
                "Relative to Parent",
                "Relative to Fixed"
            };

            ShowAs.ValueChanged += (i, _) =>
            {
                showAsTree = i == 0;
                Flush();
            };

            PoseAs.ValueChanged += (i, _) =>
            {
                poseDisplay = (PoseDisplayType)i;
                UpdateFrameText();
            };

            TfListener.Instance.ResetFrames += OnResetFrames;
        }

        void OnDestroy()
        {
            TfListener.Instance.ResetFrames -= OnResetFrames;
        }

        void OnResetFrames()
        {
            SelectedFrame = null;
        }

        void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            TfLink.LinkClicked += OnLinkClicked;
            SelectedFrame = null;

            placeHolder = FrameNode.Instantiate("TFLog Placeholder");

            gotoButton.interactable = false;
            trail.interactable = false;
            lockPivot.interactable = false;
            fixedFrame.interactable = false;

            UpdateFrameButtons();
        }

        void OnLinkClicked([CanBeNull] string frameId)
        {
            SelectedFrame = frameId == null || !TfListener.TryGetFrame(frameId, out TfFrame frame)
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
            var description = BuilderPool.Rent();
            try
            {
                new TfNode(TfListener.OriginFrame, SelectedFrame).Write(description);

                description.AppendLine().AppendLine();
                uint newHash = Crc32Calculator.Compute(description);
                if (newHash == descriptionHash)
                {
                    return;
                }

                descriptionHash = newHash;
                TfText.SetText(description);
                RectTransform cTransform = (RectTransform)content.transform;
                cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TfText.preferredWidth + 10);
                cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, TfText.preferredHeight + 10);
            }
            finally
            {
                BuilderPool.Return(description);
            }
        }

        void UpdateFrameListAsList()
        {
            nodes.Clear();
            new TfNode(TfListener.OriginFrame, SelectedFrame).AddTo(nodes);

            var description = BuilderPool.Rent();
            try
            {
                nodes.Sort();
                foreach (var node in nodes)
                {
                    node.WriteSingle(description);
                }

                description.AppendLine().AppendLine();
                uint newHash = Crc32Calculator.Compute(description);
                if (newHash == descriptionHash)
                {
                    return;
                }

                descriptionHash = newHash;
                TfText.SetText(description);

                var cTransform = (RectTransform)content.transform;
                cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TfText.preferredWidth + 10);
                cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, TfText.preferredHeight + 10);
            }
            finally
            {
                BuilderPool.Return(description);
            }
        }

        public void UpdateFrameText()
        {
            if (!isInitialized)
            {
                return;
            }

            var frame = SelectedFrame;
            var description = BuilderPool.Rent();
            try
            {
                if (frame == null)
                {
                    description.Append("<color=grey>[none]</color>");
                }
                else
                {
                    string id = frame.Id;
                    description.Append("<b>[")
                        .Append(id)
                        .AppendLine(id == TfListener.FixedFrameId
                            ? "]</b>  <i>[Fixed]</i>"
                            : "]</b>");

                    description.AppendLine(
                        frame.Parent == null || frame.Parent == TfListener.OriginFrame
                            ? "[no parent]"
                            : frame.Parent.Id);

                    if (frame.LastCallerId != null)
                    {
                        description.Append("[").Append(frame.LastCallerId).AppendLine("]");
                    }

                    Pose pose = poseDisplay switch
                    {
                        PoseDisplayType.ToRoot => frame.OriginWorldPose,
                        PoseDisplayType.ToFixed => TfListener.RelativePoseToFixedFrame(frame.AbsoluteUnityPose),
                        PoseDisplayType.ToParent => frame.Transform.AsLocalPose(),
                        _ => Pose.identity
                    };

                    RosUtils.FormatPose(pose, description);
                }

                uint newHash = Crc32Calculator.Compute(description);
                if (newHash == textHash)
                {
                    return;
                }

                textHash = newHash;
                TfName.SetText(description);
            }
            finally
            {
                BuilderPool.Return(description);
            }
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

                lockPivotText.text = GuiInputModule.Instance.OrbitCenterOverride == SelectedFrame
                    ? "Lock Pivot\n<b>On</b>"
                    : "Lock Pivot\nOff";
            }
        }

        public void OnGotoClicked()
        {
            if (SelectedFrame == null)
            {
                return;
            }

            SelectedFrame.Highlight();
            GuiInputModule.Instance.LookAt(SelectedFrame.AbsoluteUnityPose.position);
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
                SelectedFrame.Highlight();
                TfListener.FixedFrameId = SelectedFrame.Id;
            }

            UpdateFrameButtons();
        }

        public void OnLockPivotClicked()
        {
            GuiInputModule.Instance.OrbitCenterOverride =
                GuiInputModule.Instance.OrbitCenterOverride == SelectedFrame
                    ? null
                    : SelectedFrame;

            UpdateFrameButtons();
            Close?.Invoke();
        }


        public void OnLock1PVClicked()
        {
            GuiInputModule.Instance.CameraViewOverride =
                GuiInputModule.Instance.CameraViewOverride == SelectedFrame
                    ? null
                    : SelectedFrame;

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

            str.Append("<link=").Append(name).Append("><b>");

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

            str.AppendLine("</b></link>");

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