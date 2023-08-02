#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] TMP_Text? trailText;
        [SerializeField] TMP_Text? lockPivotText;
        [SerializeField] TMP_Text? publishingText;

        [SerializeField] LinkResolver? tfLink;
        [SerializeField] DropdownWidget? showAs;
        [SerializeField] DropdownWidget? poseAs;

        static TfPublisher TfPublisher => TfPublisher.Instance;
        TMP_Text TfText => tfText.AssertNotNull(nameof(tfText));
        TMP_Text TfName => tfName.AssertNotNull(nameof(tfName));
        Button Publishing => publishing.AssertNotNull(nameof(publishing));
        Button Trail => trail.AssertNotNull(nameof(trail));
        Button LockPivot => lockPivot.AssertNotNull(nameof(lockPivot));
        Button MakeFixed => fixedFrame.AssertNotNull(nameof(fixedFrame));
        DropdownWidget ShowAs => showAs.AssertNotNull(nameof(showAs));
        DropdownWidget PoseAs => poseAs.AssertNotNull(nameof(poseAs));
        LinkResolver TfLink => tfLink.AssertNotNull(nameof(tfLink));
        TMP_Text TrailText => trailText.AssertNotNull(nameof(trailText));
        TMP_Text LockPivotText => lockPivotText.AssertNotNull(nameof(lockPivotText));
        TMP_Text PublishingText => publishingText.AssertNotNull(nameof(publishingText));
        RectTransform ContentTransform => contentTransform.AssertNotNull(nameof(contentTransform));

        FrameNode PlaceHolder => placeHolder ??= new FrameNode("TFLog Placeholder");

        readonly List<TfFrame> nodesHelper = new();

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

                selectedFrame = value;

                PlaceHolder.Parent = selectedFrame ?? TfModule.DefaultFrame;

                if (selectedFrame != null)
                {
                    TfPublisher.ShowPanelIfPublishing(selectedFrame.Id);
                }

                bool interactable = selectedFrame != null;
                Publishing.interactable = interactable;
                Trail.interactable = interactable;
                LockPivot.interactable = interactable;
                MakeFixed.interactable = interactable;

                Flush();
                value?.Highlight();

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
                "Pose to Parent",
                "Pose to Fixed"
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

            MakeFixed.onClick.AddListener(OnMakeFixedClicked);
            Trail.onClick.AddListener(OnTrailClicked);
            LockPivot.onClick.AddListener(OnLockPivotClicked);
            Publishing.onClick.AddListener(OnPublishingClicked);

            TfModule.ResetFrames += OnResetFrames;
        }

        void OnDestroy()
        {
            if (TfModule.HasInstance)
            {
                TfModule.ResetFrames -= OnResetFrames;
            }
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
            MakeFixed.interactable = false;

            UpdateFrameButtons();
        }

        void OnLinkClicked(string? frameId)
        {
            SelectedFrame = frameId == null || !TfModule.TryGetFrame(frameId, out TfFrame? frame)
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
                EntryHelper.Write(TfModule.OriginFrame, selectedFrame, description);

                description.AppendLine().AppendLine();
                uint newHash = HashCalculator.Compute(description);
                if (newHash == descriptionHash)
                {
                    return;
                }

                descriptionHash = newHash;
                TfText.SetTextRent(description);
            }

            RectTransform cTransform = ContentTransform;
            cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TfText.preferredWidth + 10);
            cTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, TfText.preferredHeight + 10);
        }

        void UpdateFrameListAsList()
        {
            nodesHelper.Clear();
            EntryHelper.AddAllChildren(TfModule.OriginFrame, nodesHelper);
            nodesHelper.Sort((a, b) => string.Compare(a.Id, b.Id, StringComparison.Ordinal));

            using (var description = BuilderPool.Rent())
            {
                foreach (var node in nodesHelper)
                {
                    EntryHelper.WriteSingle(node, selectedFrame, description);
                }

                description.AppendLine().AppendLine();
                uint newHash = HashCalculator.Compute(description);
                if (newHash == descriptionHash)
                {
                    return;
                }

                descriptionHash = newHash;
                TfText.SetTextRent(description);
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
                    .Append("]</b>");

                description.Append(DecoratorFor(id)).AppendLine();

                description.Append(
                        frame.Parent == null || frame.Parent == TfModule.OriginFrame
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

            uint newHash = HashCalculator.Compute(description);
            if (newHash == textHash)
            {
                return;
            }

            textHash = newHash;
            TfName.SetTextRent(description);
        }

        static string DecoratorFor(string id) =>
            (id == TfModule.FixedFrameId, TfPublisher.IsPublishing(id)) switch
            {
                (true, true) => " [fixed, own]",
                (true, false) => " [fixed]",
                (false, true) => " [own]",
                _ => ""
            };

        public void UpdateFrameButtons()
        {
            const string offString = "<b>Off</b>";
            const string onString = "<b><color=blue>- On -</color></b>";

            if (SelectedFrame == null)
            {
                TrailText.text = "Trail:\n" + offString;
                LockPivotText.text = "Lock Pivot\n" + offString;
                PublishingText.text = "Publishing\n" + offString;
            }
            else
            {
                TrailText.text = SelectedFrame.TrailVisible
                    ? "Trail:\n" + onString
                    : "Trail:\n" + offString;

                LockPivotText.text = SelectedFrame == GuiInputModule.Instance.OrbitCenterOverride
                    ? "Lock Pivot\n" + onString
                    : "Lock Pivot\n" + offString;

                PublishingText.text = TfPublisher.IsPublishing(SelectedFrame.Id)
                    ? "Publishing\n" + onString
                    : "Publishing\n" + offString;
            }
        }

        void OnGotoClicked()
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
                var publishedFrame = TfPublisher.GetOrCreate(selectedFrameId, true);
                TfListener.Publish(publishedFrame.TfFrame);
            }

            Flush();
            UpdateFrameButtons();
        }

        void OnTrailClicked()
        {
            if (SelectedFrame != null)
            {
                SelectedFrame.TrailVisible = !SelectedFrame.TrailVisible;
            }

            UpdateFrameButtons();
        }

        void OnMakeFixedClicked()
        {
            if (SelectedFrame != null)
            {
                SelectedFrame.Highlight();
                TfListener.FixedFrameId = SelectedFrame.Id;
            }

            UpdateFrameButtons();
        }

        void OnLockPivotClicked()
        {
            GuiInputModule.Instance.OrbitCenterOverride =
                GuiInputModule.Instance.OrbitCenterOverride == SelectedFrame
                    ? null
                    : SelectedFrame;

            UpdateFrameButtons();
            Close.TryRaise(this);
        }

        void OnLock1PVClicked()
        {
            GuiInputModule.Instance.CameraViewOverride =
                GuiInputModule.Instance.CameraViewOverride == SelectedFrame
                    ? null
                    : SelectedFrame;

            UpdateFrameButtons();
            Close.TryRaise(this);
        }

        public void ClearSubscribers()
        {
        }

        // ------------------------------------------
        static class EntryHelper
        {
            const int MaxFramesToWrite = 1024;

            static void Write(TfFrame frame, StringBuilder str, TfFrame? selectedFrame, int level, bool withChildren,
                ref int written)
            {
                if (written >= MaxFramesToWrite)
                {
                    return;
                }

                written++;
                if (written == MaxFramesToWrite)
                {
                    str.Append("[... list truncated]").AppendLine();
                    return;
                }

                if (level != 0)
                {
                    str.Append(' ', level * 4);
                }

                str.Append("<link=").Append(frame.Id).Append("><b>");

                bool isSelectedFrame = frame == selectedFrame;
                if (isSelectedFrame)
                {
                    str.Append("<color=blue>");
                }

                str.Append("<u>")
                    .Append(frame.Id)
                    .Append("</u>")
                    .Append("</b></link>")
                    .Append(DecoratorFor(frame.Id))
                    .AppendLine();
                if (isSelectedFrame)
                {
                    str.Append("</color>");
                }


                if (!withChildren)
                {
                    return;
                }

                foreach (var child in frame.Children)
                {
                    Write(child, str, selectedFrame, level + 1, true, ref written);
                }
            }


            public static void Write(TfFrame frame, TfFrame? selectedFrame, StringBuilder str)
            {
                int written = 0;

                foreach (var child in frame.Children)
                {
                    Write(child, str, selectedFrame, 0, true, ref written);
                }
            }

            public static void WriteSingle(TfFrame frame, TfFrame? selectedFrame, StringBuilder str)
            {
                int written = 0;
                Write(frame, str, selectedFrame, 0, false, ref written);
            }

            public static void AddAllChildren(TfFrame frame, ICollection<TfFrame> nodes)
            {
                foreach (var child in frame.Children)
                {
                    nodes.Add(child);
                    AddAllChildren(child, nodes);
                }
            }
        }
    }
}