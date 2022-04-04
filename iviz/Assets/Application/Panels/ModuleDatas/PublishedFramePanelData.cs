#nullable enable

using System.Linq;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;

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
            panel.ParentId.Value = Frame.ParentId ?? TfModule.OriginFrameId;

            UpdateHints();
            UpdatePanelPose();

            panel.RollPitchYaw.ValueChanged += f =>
            {
                Frame.Transform.localRotation = (f * Mathf.Deg2Rad).RosRpy2Unity();
                TfListener.Publish(Frame);
                UpdatePanelPose();
            };
            panel.Position.ValueChanged += f =>
            {
                Frame.Transform.localPosition = f.Ros2Unity();
                TfListener.Publish(Frame);
                UpdatePanelPose();
            };
            
            panel.CloseButton.Clicked += Close;
            panel.ParentId.EndEdit += f =>
            {
                string validatedParent = string.IsNullOrEmpty(f) 
                    ? TfModule.OriginFrameId 
                    : TfModule.ResolveFrameId(f);
                
                var parentFrame = TfModule.GetOrCreateFrame(validatedParent);
                if (!Frame.TrySetParent(parentFrame))
                {
                    RosLogger.Error(
                        $"{this}: Failed to set '{f}' as a parent to '{Frame.Id}'. Reason: Cycle detected.");
                    panel.ParentId.Value = Frame.ParentId ?? TfModule.OriginFrameId;
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
            panel.ParentId.Hints = TfModule.FrameNames.Prepend(TfModule.OriginFrameId);
        }

        void UpdatePanelPose()
        {
            panel.RollPitchYaw.Value = UnityUtils.RegularizeRpy(GetLocalRosRpy() * Mathf.Rad2Deg);

            var position = Frame.Transform.localPosition.Unity2Ros();
            panel.Position.Value = position;
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