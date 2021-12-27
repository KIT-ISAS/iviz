#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;
using UnityEngine;
using Transform = Iviz.Msgs.GeometryMsgs.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.App
{
    public sealed class TfPublisherDialogData : DialogData
    {
        readonly Dictionary<string, TransformStamped> frames = new();
        readonly TfPublisherDialogContents panel;
        string? selectedFrameId;
        int frameCounter;

        public override IDialogPanelContents Panel => panel;
        public int NumFrames => frames.Count;

        public TfPublisherDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<TfPublisherDialogContents>(DialogPanelType.TfPublisher);
            UnselectFrame();
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();
            UpdateFrameList();
            if (selectedFrameId == null)
            {
                UnselectFrame();
            }
            else
            {
                SelectFrame(selectedFrameId);
            }

            panel.Close.Clicked += Close;
            panel.LinkClicked += SelectFrame;
            panel.Add.Clicked += AddFrame;

            panel.NameField.EndEdit += _ => UpdateSelected();
            panel.ParentField.EndEdit += _ => UpdateSelected();
            panel.Position.ValueChanged += _ => UpdateSelected();

            UpdateHints();
        }

        void AddFrame()
        {
            string frameId;
            do
            {
                frameId = "~frame_" + (frameCounter++);
            } while (frames.ContainsKey(frameId));

            frames[frameId] = new TransformStamped(
                new Header(0, default, ""),
                frameId,
                Transform.Identity
            );
            
            SelectFrame(frameId);
            UpdateFrameList();
        }

        void UpdateSelected()
        {
            if (selectedFrameId == null)
            {
                return; // ?
            }
            
            string newFrameId = panel.NameField.Value;
            string newFrameParentId = panel.ParentField.Value;
            var newPosition = panel.Position.Value;

            bool selectedFrameIdChanged = selectedFrameId != newFrameId;
            if (selectedFrameIdChanged)
            {
                frames.Remove(selectedFrameId);
                selectedFrameId = newFrameId;
            }

            var newTransform = Transform.Identity.WithTranslation(newPosition.ToRosVector3());
            frames[selectedFrameId] = new TransformStamped(
                new Header(0, default, newFrameParentId),
                selectedFrameId,
                newTransform
            );

            if (selectedFrameIdChanged)
            {
                UpdateFrameList();
            }
            
            TfListener.Publish(newFrameParentId, selectedFrameId, newTransform);
        }


        void UpdateFrameList()
        {
            if (frames.Count == 0)
            {
                panel.Text.text = "(Empty list)";
                return;
            }

            using var str = BuilderPool.Rent();
            foreach (string frameId in frames.Keys)
            {
                str.Append("<link=").Append(frameId).Append("><b>");

                if (frameId == selectedFrameId)
                {
                    str.Append("<color=blue>");
                    str.Append("<u>").Append(frameId).Append("</u>");
                    str.Append("</color>");
                }
                else if (frameId == TfListener.FixedFrameId)
                {
                    str.Append("<color=#008800>");
                    str.Append("<u>").Append(frameId).Append("</u>");
                    str.Append("</color>");
                }
                else
                {
                    str.Append("<u>").Append(frameId).Append("</u>");
                }

                str.Append("</b></link>").AppendLine();
            }

            panel.Text.SetText(str);
        }

        void UnselectFrame()
        {
            selectedFrameId = null;

            panel.NameField.Value = "(none)";
            panel.ParentField.Value = "(none)";
            panel.Position.Value = Vector3.zero;

            panel.NameField.Interactable = false;
            panel.ParentField.Interactable = false;
            panel.Position.Interactable = false;
        }

        void SelectFrame(string frameId)
        {
            if (!frames.TryGetValue(frameId, out var transformStamped))
            {
                UnselectFrame();
                return;
            }

            selectedFrameId = frameId;

            panel.NameField.Interactable = true;
            panel.ParentField.Interactable = true;
            panel.Position.Interactable = true;

            panel.NameField.Value = frameId;
            panel.ParentField.Value = string.IsNullOrEmpty(transformStamped.Header.FrameId)
                ? TfListener.FixedFrameId
                : transformStamped.Header.FrameId;
            panel.Position.Value = transformStamped.Transform.Translation.ToUnity();
        }

        public override void UpdatePanel()
        {
            UpdateHints();
            PublishAll();
        }

        void UpdateHints()
        {
            string[] hints = TfListener.FramesUsableAsHints.ToArray();
            panel.NameField.SetHints(hints);
            panel.ParentField.SetHints(hints);
        }

        void PublishAll()
        {
            foreach (var (header, childFrameId, transform) in frames.Values)
            {
                TfListener.Publish(header.FrameId, childFrameId, transform);
            }
        }
    }
}