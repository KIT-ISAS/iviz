#nullable enable

using System.Collections.Generic;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARPanelContents"/> 
    /// </summary>
    public sealed class ARModuleData : ModuleData
    {
        readonly ARFoundationController controller;
        readonly ARPanelContents panel;

        public override ModuleType ModuleType => ModuleType.AugmentedReality;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public ARModuleData(ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<ARPanelContents>(ModuleType.AugmentedReality);

            controller = Resource.Controllers.AR.Instantiate<ARFoundationController>();

            controller.ModuleData = this;
            if (constructor.Configuration != null)
            {
                controller.Config = (ARConfiguration)constructor.Configuration;
            }

            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();

            controller.StopController();
            Object.Destroy(controller.gameObject);
        }

        public override void SetupPanel()
        {
            panel.HideButton.State = controller.Visible;
            panel.Frame.Owner = controller;
            panel.WorldScale.Value = controller.WorldScale;

            panel.WorldScale.ValueChanged += f => controller.WorldScale = f;

            panel.AutoFocus.Value = controller.EnableAutoFocus;

            panel.Description.Label = controller.Description;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.ResetButton.Clicked += () => { controller.ResetSession(); };
            panel.PublishFrequency.Index = (int)controller.PublicationFrequency;


            panel.AutoFocus.ValueChanged += f => controller.EnableAutoFocus = f;

            panel.ARMarkers.Description = controller.MarkerExecutor.Description;
            panel.MarkerSender.Set(controller.MarkerSender);
            panel.PublishFrequency.ValueChanged += (f, _) => controller.PublicationFrequency = (PublicationFrequency)f;
            panel.ColorSender.Set(controller.ColorSender);
            panel.DepthSender.Set(controller.DepthSender);
            panel.DepthConfidenceSender.Set(controller.DepthConfidenceSender);

            panel.OcclusionQuality.Index = (int)controller.OcclusionQuality;
            panel.OcclusionQuality.ValueChanged += (f, _) => controller.OcclusionQuality = (OcclusionQualityType)f;
        }

        public override void UpdatePanel()
        {
            panel.Description.Label = controller.Description;
            panel.ARMarkers.Description = controller.MarkerExecutor.Description;
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<ARConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(ARConfiguration.Visible):
                        controller.Visible = config.Visible;
                        break;
                    default:
                        Core.RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.AR = controller.Config;
        }
    }
}