#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Core;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModulePanel"/> 
    /// </summary>
    public sealed class ARModuleData : ModuleData
    {
        readonly ARFoundationController controller;
        readonly ARModulePanel panel;

        public override ModuleType ModuleType => ModuleType.AR;
        public override ModulePanel Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public ARModuleData(ModuleDataConstructor constructor)
        {
            panel = ModulePanelManager.GetPanelByResourceType<ARModulePanel>(ModuleType.AR);
            controller = new ARFoundationController((ARConfiguration?)constructor.Configuration);
            UpdateModuleButton();
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                controller.Dispose();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to dispose controller", e);
            }
        }

        public override void SetupPanel()
        {
            panel.HideButton.State = controller.Visible;
            panel.Frame.Owner = controller;
            panel.WorldScale.Value = controller.WorldScale;

            panel.WorldScale.ValueChanged += f => controller.WorldScale = f;

            panel.AutoFocus.Value = controller.EnableAutoFocus;

            panel.Description.Text = controller.Description;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.ResetButton.Clicked += controller.ResetSession;
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
            panel.Description.Text = controller.Description;
            panel.ARMarkers.Description = controller.MarkerExecutor.Description;
        }
        
        public void UpdateConfiguration(ARConfiguration configuration)
        {
            controller.Config = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }        

        public override void UpdateConfiguration(string configAsJson, string[] fields)
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
                        RosLogger.Error($"{this}: Unknown field '{field}'");
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