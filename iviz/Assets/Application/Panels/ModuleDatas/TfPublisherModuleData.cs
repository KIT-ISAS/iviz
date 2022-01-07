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
    /// <see cref="TfPublisherPanelContents"/> 
    /// </summary>
    public sealed class TfPublisherModuleData : ModuleData, IHasFrame
    {
        readonly TfPublisherConfiguration config = new();
        readonly TfPublisherPanelContents panel;
        readonly FrameOwner parentFrameOwner;

        public override ModuleType ModuleType => ModuleType.TFPublisher;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => config;
        public override IController? Controller => null;
        public TfFrame Frame { get; }

        public TfPublisherModuleData(TfFrame frame)
        {
            Frame = frame;
            parentFrameOwner = new FrameOwner { Frame = frame.Parent };
            panel = DataPanelManager.GetPanelByResourceType<TfPublisherPanelContents>(ModuleType);
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
            };
            panel.Pitch.ValueChanged += f =>
            {
                Frame.Transform.localRotation = GetLocalRosRpy().WithY(f * Mathf.Deg2Rad).RosRpy2Unity();
                TfListener.Publish(Frame);
            };
            panel.Yaw.ValueChanged += f =>
            {
                Frame.Transform.localRotation = GetLocalRosRpy().WithZ(f * Mathf.Deg2Rad).RosRpy2Unity();
                TfListener.Publish(Frame);
            };
            panel.Position.ValueChanged += f =>
            {
                Frame.Transform.localPosition = f.Ros2Unity();
                TfListener.Publish(Frame);
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

        public override void UpdatePanelFast()
        {
            UpdatePanelPose();
        }

        public override void UpdatePanel()
        {
            UpdateHints();
        }

        void UpdateHints()
        {
            panel.ParentId.Hints = TfListener.FramesUsableAsHints.Prepend(TfListener.OriginFrameId);
        }

        void UpdatePanelPose()
        {
            var (roll, pitch, yaw) = GetLocalRosRpy();
            panel.Roll.Value = UnityUtils.RegularizeAngle(roll * Mathf.Rad2Deg);
            panel.Pitch.Value = UnityUtils.RegularizeAngle(pitch * Mathf.Rad2Deg);
            panel.Yaw.Value = UnityUtils.RegularizeAngle(yaw * Mathf.Rad2Deg);
            panel.Position.Value = Frame.Transform.localPosition.Unity2Ros();
        }

        public override void UpdateConfiguration(string _, IEnumerable<string> __)
        {
            ResetPanel();
        }

        public override void AddToState(StateConfiguration _)
        {
        }

        public override void Close()
        {
            HidePanel();
        }

        public override string ToString() => "[TFPublisher]";

        class FrameOwner : IHasFrame
        {
            public TfFrame? Frame { get; set; }
        }
    }

    internal class TfPublisherConfiguration : IConfiguration
    {
        public string Id { get; set; } = "";
        public ModuleType ModuleType => ModuleType.TFPublisher;
        public bool Visible => true;
    }
}