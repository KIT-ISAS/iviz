#nullable enable

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
using Iviz.Tools;
using Object = UnityEngine.Object;

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

        [SerializeField] TMP_Text? tfText;
        [SerializeField] TMP_Text? tfName;
        [SerializeField] RectTransform? contentTransform;
        [SerializeField] Button? publishing;
        [SerializeField] Button? trail;

        [SerializeField] Button? lockPivot;
        [SerializeField] Button? fixedFrame;

        [SerializeField] Text? trailText;
        [SerializeField] Text? lockPivotText;
        [SerializeField] Text? publishingText;

        [SerializeField] LinkResolver? tfLink;
        [SerializeField] DropdownWidget? showAs;
        [SerializeField] DropdownWidget? poseAs;

        static TfPublisher TfPublisher => TfPublisher.Instance;
        TMP_Text TfText => tfText.AssertNotNull(nameof(tfText));
        TMP_Text TfName => tfName.AssertNotNull(nameof(tfName));
        Button Publishing => publishing.AssertNotNull(nameof(publishing));
        Button Trail => trail.AssertNotNull(nameof(trail));
        Button LockPivot => lockPivot.AssertNotNull(nameof(lockPivot));
        Button FixedFrame => fixedFrame.AssertNotNull(nameof(fixedFrame));
        DropdownWidget ShowAs => showAs.AssertNotNull(nameof(showAs));
        DropdownWidget PoseAs => poseAs.AssertNotNull(nameof(poseAs));
        LinkResolver TfLink => tfLink.AssertNotNull(nameof(tfLink));
        Text TrailText => trailText.AssertNotNull(nameof(trailText));
        Text LockPivotText => lockPivotText.AssertNotNull(nameof(lockPivotText));
        Text PublishingText => publishingText.AssertNotNull(nameof(publishingText));
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
                        TfPublisher.ShowPanelIfPublishing(value.Id);
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
                    TfPublisher.ShowPanelIfPublishing(selectedFrame.Id);
                }

                bool interactable = selectedFrame != null;
                Publishing.interactable = interactable;
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
            TfLink.LinkDoubleClicked += OnLinkDoubleClicked;
            SelectedFrame = null;

            Publishing.interactable = false;
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

        void OnLinkDoubleClicked(string? _)
        {
            OnGotoClicked();
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
                new TfNode(TfListener.OriginFrame).Write(description, SelectedFrame);

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
            new TfNode(TfListener.OriginFrame).AddTo(nodes);
            nodes.Sort();

            using (var description = BuilderPool.Rent())
            {
                foreach (var node in nodes)
                {
                    node.WriteSingle(description, SelectedFrame);
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
                    .Append(id);
                description.Append(id == TfListener.FixedFrameId
                        ? "]</b>  <i>[Fixed]</i>"
                        : TfPublisher.IsPublishing(id)
                            ? "]</b>  <i>[Publ]</i>"
                            : "]</b>")
                    .AppendLine();

                description.Append(
                        frame.Parent == null || frame.Parent == TfListener.OriginFrame
                            ? "[no parent]"
                            : frame.Parent.Id)
                    .AppendLine();

                /*
                if (frame.LastCallerId != null)
                {
                    description.Append("[").Append(frame.LastCallerId).Append("]").AppendLine();
                }
                */

                Pose pose = poseDisplay switch
                {
                    PoseDisplayType.ToRoot => frame.OriginWorldPose,
                    PoseDisplayType.ToFixed => frame.FixedWorldPose,
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
                TrailText.text = "Trail:\n<b>Off</b>";
                LockPivotText.text = "Lock Pivot\n<b>Off</b>";
                PublishingText.text = "Publishing\n<b>Off</b>";
            }
            else
            {
                TrailText.text = SelectedFrame.TrailVisible
                    ? "Trail:\n<b>On</b>"
                    : "Trail:\n<b>Off</b>";

                LockPivotText.text = SelectedFrame == GuiInputModule.Instance.OrbitCenterOverride
                    ? "Lock Pivot\n<b>On</b>"
                    : "Lock Pivot\n<b>Off</b>";

                PublishingText.text = TfPublisher.IsPublishing(SelectedFrame.Id)
                    ? "Publishing\n<b>On</b>"
                    : "Publishing\n<b>Off</b>";
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

        public void OnPublishingClicked()
        {
            if (SelectedFrame == null)
            {
                return;
            }

            string selectedFrameId = SelectedFrame.Id;
            if (!TfPublisher.Remove(selectedFrameId))
            {
                var publishedFrame  = TfPublisher.GetOrCreate(selectedFrameId, true);
                TfListener.Publish(publishedFrame.TfFrame);
            }

            UpdateFrameButtons();
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
            readonly TfFrame frame;

            string Name => frame.Id;
            bool HasTrail => frame.TrailVisible;

            public TfNode(TfFrame frame)
            {
                this.frame = frame;
            }

            void Write(StringBuilder str, TfFrame? selectedFrame, int level, bool withChildren)
            {
                if (level != 0)
                {
                    str.Append(' ', level * 4);
                }

                str.Append("<link=").Append(Name).Append("><b>");

                if (frame == selectedFrame)
                {
                    str.Append("<color=blue>");
                    AppendName(str);
                    str.Append("</color>");
                }
                else if (TfPublisher.IsPublishing(Name))
                {
                    str.Append("<color=#880000>");
                    AppendName(str);
                    str.Append("</color>");
                }
                else if (Name == TfListener.FixedFrameId)
                {
                    str.Append("<color=#008800>");
                    AppendName(str);
                    str.Append("</color>");
                }
                else
                {
                    str.Append("<u>").Append(Name).Append("</u>");
                }

                str.AppendLine("</b></link>");

                if (withChildren)
                {
                    foreach (var child in frame.Children)
                    {
                        new TfNode(child).Write(str, selectedFrame, level + 1, true);
                    }
                }
            }

            void AppendName(StringBuilder str)
            {
                if (HasTrail)
                {
                    str.Append("~");
                }

                str.Append("<u>").Append(Name).Append("</u>");
                if (HasTrail)
                {
                    str.Append("~");
                }
            }

            public void Write(StringBuilder str, TfFrame? selectedFrame)
            {
                foreach (var child in frame.Children)
                {
                    new TfNode(child).Write(str, selectedFrame, 0, true);
                }
            }

            public void WriteSingle(StringBuilder str, TfFrame? selectedFrame)
            {
                Write(str, selectedFrame, 0, false);
            }

            public int CompareTo(TfNode other)
            {
                return string.CompareOrdinal(Name, other.Name);
            }

            public void AddTo(List<TfNode> nodes)
            {
                foreach (var child in frame.Children)
                {
                    var node = new TfNode(child);
                    nodes.Add(node);
                    node.AddTo(nodes);
                }
            }
        }
    }
}