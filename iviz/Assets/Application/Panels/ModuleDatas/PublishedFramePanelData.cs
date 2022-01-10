#nullable enable

using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="PublishedFramePanel"/> 
    /// </summary>
    public sealed class PublishedFramePanelData : ModulePanelData, IHasFrame
    {
        readonly PublishedFramePanel panel;
        readonly FrameOwner parentFrameOwner;
        
        public TfFrame Frame { get; }
        public override ModulePanel Panel => panel;

        public PublishedFramePanelData(TfFrame frame)
        {
            Frame = frame;
            parentFrameOwner = new FrameOwner { Frame = frame.Parent };
            panel = ModulePanelManager.GetPanelByResourceType<PublishedFramePanel>(ModuleType.TFPublisher);
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = this;
            panel.Parent.Owner = parentFrameOwner;
            panel.CloseButton.Clicked += Close;
            panel.ParentId.Value = Frame.ParentId ?? TfListener.OriginFrameId;

            UpdateHints();
            UpdatePanelPose();

            panel.Roll.ValueChanged += f =>
            {
                Frame.Transform.localRotation = GetLocalRosRpy().WithX(f * Mathf.Deg2Rad).RosRpy2Unity();
                TfListener.Publish(Frame);
                UpdateBottom();
            };
            panel.Pitch.ValueChanged += f =>
            {
                Frame.Transform.localRotation = GetLocalRosRpy().WithY(f * Mathf.Deg2Rad).RosRpy2Unity();
                TfListener.Publish(Frame);
                UpdateBottom();
            };
            panel.Yaw.ValueChanged += f =>
            {
                Frame.Transform.localRotation = GetLocalRosRpy().WithZ(f * Mathf.Deg2Rad).RosRpy2Unity();
                TfListener.Publish(Frame);
                UpdateBottom();
            };
            panel.Position.ValueChanged += f =>
            {
                Frame.Transform.localPosition = f.Ros2Unity();
                TfListener.Publish(Frame);
                UpdateBottom();
            };
            
            panel.InputRpy.EndEdit += f =>
            {
                Frame.Transform.localRotation = (f * Mathf.Deg2Rad).RosRpy2Unity();
                TfListener.Publish(Frame);
                UpdateTop();
            };

            panel.InputPosition.EndEdit += f =>
            {
                Frame.Transform.localPosition = f.Ros2Unity();
                TfListener.Publish(Frame);
                UpdateTop();
            };
            
            panel.CloseButton.Clicked += Close;
            panel.ParentId.EndEdit += f =>
            {
                string validatedParent = string.IsNullOrEmpty(f) 
                    ? TfListener.OriginFrameId 
                    : TfListener.ResolveFrameId(f);
                
                var parentFrame = TfListener.GetOrCreateFrame(validatedParent);
                if (!Frame.TrySetParent(parentFrame))
                {
                    RosLogger.Error(
                        $"{this}: Failed to set '{f}' as a parent to '{Frame.Id}'. Reason: Cycle detected.");
                    panel.ParentId.Value = Frame.ParentId ?? TfListener.OriginFrameId;
                    parentFrameOwner.Frame = Frame.Parent;
                    return;
                }

                ModuleListPanel.FlushTfDialog();
                parentFrameOwner.Frame = parentFrame;
            };
        }

        Vector3 GetLocalRosRpy() => Frame.Transform.localEulerAngles.Unity2RosRpy();

        public override void UpdatePanel()
        {
            UpdateHints();
        }

        void UpdateHints()
        {
            panel.ParentId.Hints = TfListener.FramesUsableAsHints.Prepend(TfListener.OriginFrameId);
        }

        void UpdateTop()
        {
            var (roll, pitch, yaw) = UnityUtils.RegularizeRpy(GetLocalRosRpy() * Mathf.Rad2Deg);
            panel.Roll.Value = roll;
            panel.Pitch.Value = pitch;
            panel.Yaw.Value = yaw;

            var position = Frame.Transform.localPosition.Unity2Ros();
            panel.Position.Value = position;
        }

        void UpdateBottom()
        {
            var rpy = UnityUtils.RegularizeRpy(GetLocalRosRpy() * Mathf.Rad2Deg);
            var position = Frame.Transform.localPosition.Unity2Ros();
            panel.InputRpy.Value = rpy;
            panel.InputPosition.Value = position;
        }

        void UpdatePanelPose()
        {
            UpdateTop();
            UpdateBottom();
        }

        void Close()
        {
            ModulePanelManager.HidePanelFor(this);
        }

        public override string ToString() => "[TFPublisher]";

        sealed class FrameOwner : IHasFrame
        {
            public TfFrame? Frame { get; set; }
        }
    }
}