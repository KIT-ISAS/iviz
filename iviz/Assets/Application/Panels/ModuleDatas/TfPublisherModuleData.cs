#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="CameraPanelContents"/> 
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

            var (roll, pitch, yaw) = GetLocalRosRpy();
            panel.Roll.Value = roll;
            panel.Pitch.Value = pitch;
            panel.Yaw.Value = yaw;
            panel.Position.Value = Frame.Transform.localPosition;

            panel.Roll.ValueChanged += f => Frame.Transform.localRotation = GetLocalRosRpy().WithX(f).RosRpy2Unity();
            panel.Pitch.ValueChanged += f => Frame.Transform.localRotation = GetLocalRosRpy().WithY(f).RosRpy2Unity();
            panel.Yaw.ValueChanged += f => Frame.Transform.localRotation = GetLocalRosRpy().WithZ(f).RosRpy2Unity();
            panel.Position.ValueChanged += f => Frame.Transform.localPosition = f;
            panel.CloseButton.Clicked += () => DataPanelManager.HidePanelFor(this);
        }

        Vector3 GetLocalRosRpy() => Frame.Transform.localEulerAngles.Unity2RosRpy();

        public override void UpdatePanelFast()
        {
            var (roll, pitch, yaw) = GetLocalRosRpy();
            panel.Roll.Value = roll;
            panel.Pitch.Value = pitch;
            panel.Yaw.Value = yaw;
            panel.Position.Value = Frame.Transform.localPosition;

            var position = Frame.Transform.position;
            if (panel.Position.Value != position)
            {
                panel.Position.Value = Frame.Transform.position;
            }
        }

        public override void UpdateConfiguration(string _, IEnumerable<string> __)
        {
            ResetPanel();
        }

        public override void AddToState(StateConfiguration _)
        {
        }

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