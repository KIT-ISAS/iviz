#nullable enable

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using TMPro;
using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Tools;

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

        [SerializeField] TMP_Text? tfText = null;
        [SerializeField] TMP_Text? tfName = null;
        [SerializeField] RectTransform? contentTransform = null;
        [SerializeField] Button? gotoButton = null;
        [SerializeField] Button? trail = null;

        [SerializeField] Button? lockPivot = null;
        [SerializeField] Button? fixedFrame = null;

        [SerializeField] Text? trailText = null;
        [SerializeField] Text? lockPivotText = null;

        [SerializeField] LinkResolver? tfLink = null;
        [SerializeField] DropdownWidget? showAs = null;
        [SerializeField] DropdownWidget? poseAs = null;

        TMP_Text TfText => tfText.AssertNotNull(nameof(tfText));
        TMP_Text TfName => tfName.AssertNotNull(nameof(tfName));
        Button GotoButton => gotoButton.AssertNotNull(nameof(gotoButton));
        Button Trail => trail.AssertNotNull(nameof(trail));
        Button LockPivot => lockPivot.AssertNotNull(nameof(lockPivot));
        Button FixedFrame => fixedFrame.AssertNotNull(nameof(fixedFrame));
        DropdownWidget ShowAs => showAs.AssertNotNull(nameof(showAs));
        DropdownWidget PoseAs => poseAs.AssertNotNull(nameof(poseAs));
        LinkResolver TfLink => tfLink.AssertNotNull(nameof(tfLink));
        Text TrailText => trailText.AssertNotNull(nameof(trailText));
        Text LockPivotText => lockPivotText.AssertNotNull(nameof(lockPivotText));
        RectTransform ContentTransform => contentTransform.AssertNotNull(nameof(contentTransform));

        FrameNode PlaceHolder =>
            placeHolder != null ? placeHolder : (placeHolder = FrameNode.Instantiate("TFLog Placeholder"));

        readonly List<TfNode> nodes = new();

        bool showAsTree = true;
        PoseDisplayType poseDisplay;
        FrameNode? placeHolder;

        uint? descriptionHash;
        uint? textHash;

        bool isInitialized;
        TfFrame? selectedFrame;

        public event Action? Close;

        public TfFrame? SelectedFrame
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
                    selectedFrame.RemoveListener(PlaceHolder);
                }

                selectedFrame = value;

                if (selectedFrame != null)
                {
                    selectedFrame.AddListener(PlaceHolder);
                }


                bool interactable = selectedFrame != null;
                GotoButton.interactable = interactable;
                Trail.interactable = interactable;
                LockPivot.interactable = interactable;
                FixedFrame.interactable = interactable;

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

            GotoButton.interactable = false;
            Trail.interactable = false;
            LockPivot.interactable = false;
            FixedFrame.interactable = false;

            UpdateFrameButtons();
        }

        void OnLinkClicked(string? frameId)
        {
            SelectedFrame = frameId == null || !TfListener.TryGetFrame(frameId, out TfFrame? frame)
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
            using (var description = BuilderPool.Rent())
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
            }

            RectTransform cTransform = ContentTransform;
            cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TfText.preferredWidth + 10);
            cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, TfText.preferredHeight + 10);
        }

        void UpdateFrameListAsList()
        {
            nodes.Clear();
            new TfNode(TfListener.OriginFrame, SelectedFrame).AddTo(nodes);
            nodes.Sort();
            
            using (var description = BuilderPool.Rent())
            {
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
            }

            var cTransform = ContentTransform;
            cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TfText.preferredWidth + 10);
            cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, TfText.preferredHeight + 10);
        }

        public void UpdateFrameText()
        {
            if (!isInitialized)
            {
                return;
            }

            var frame = SelectedFrame;
            using var description = BuilderPool.Rent();
            if (frame == null)
            {
                description.Append("<color=grey>[none]</color>");
            }
            else
            {
                string id = frame.Id;
                description.Append("<b>[")
                    .Append(id)
                    .Append(id == TfListener.FixedFrameId
                        ? "]</b>  <i>[Fixed]</i>"
                        : "]</b>")
                    .AppendLine();

                description.Append(
                        frame.Parent == null || frame.Parent == TfListener.OriginFrame
                            ? "[no parent]"
                            : frame.Parent.Id)
                    .AppendLine();

                if (frame.LastCallerId != null)
                {
                    description.Append("[").Append(frame.LastCallerId).Append("]").AppendLine();
                }

                Pose pose = poseDisplay switch
                {
                    PoseDisplayType.ToRoot => frame.OriginWorldPose,
                    PoseDisplayType.ToFixed => TfListener.RelativeToFixedFrame(frame.AbsoluteUnityPose),
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

        public void UpdateFrameButtons()
        {
            if (SelectedFrame == null)
            {
                TrailText.text = "Trail:\nOff";
                LockPivotText.text = "Lock Pivot\nOff";
            }
            else
            {
                TrailText.text = SelectedFrame.TrailVisible ? "Trail:\n<b>On</b>" : "Trail:\nOff";

                LockPivotText.text = GuiInputModule.Instance.OrbitCenterOverride == SelectedFrame
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
            GuiInputModule.Instance.LookAt(SelectedFrame.Transform);
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

        // ------------------------------------------

        readonly struct TfNode : IComparable<TfNode>
        {
            readonly string name;
            readonly TfNode[] children;
            readonly bool hasTrail;
            readonly bool selected;

            public TfNode(TfFrame frame, TfFrame? selectedFrame)
            {
                name = frame.Id;
                hasTrail = frame.TrailVisible;
                selected = (frame == selectedFrame);
                children = new TfNode[frame.Children.Count];

                foreach (var (childFrame, i) in frame.Children.WithIndex())
                {
                    children[i] = new TfNode(childFrame, selectedFrame);
                }

                Array.Sort(children);
            }

            void Write(StringBuilder str, int level, bool withChildren)
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

            public void Write(StringBuilder str)
            {
                foreach (TfNode node in children)
                {
                    node.Write(str, 0, true);
                }
            }

            public void WriteSingle(StringBuilder str)
            {
                Write(str, 0, false);
            }

            public int CompareTo(TfNode other)
            {
                return string.CompareOrdinal(name, other.name);
            }

            public void AddTo(List<TfNode> nodes)
            {
                foreach (TfNode node in children)
                {
                    nodes.Add(node);
                    node.AddTo(nodes);
                }
            }
        }
    }
}