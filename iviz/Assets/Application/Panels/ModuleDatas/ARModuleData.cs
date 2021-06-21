using System.Collections.Generic;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARPanelContents"/> 
    /// </summary>
    public sealed class ARModuleData : ModuleData
    {
        const string NoneString = "(none)";

        readonly ARFoundationController controller;
        readonly ARPanelContents panel;

        public override ModuleType ModuleType => ModuleType.AugmentedReality;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public ARModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<ARPanelContents>(ModuleType.AugmentedReality);

            controller = Resource.Controllers.AR.Instantiate().GetComponent<ARFoundationController>();

            controller.ModuleData = this;
            if (constructor.Configuration != null)
            {
                controller.Config = (ARConfiguration) constructor.Configuration;
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

            panel.OcclusionQuality.Options = new[] {"Off", "Fast", "Medium", "Best"};
            panel.OcclusionQuality.Index = (int) controller.OcclusionQuality;

            panel.WorldScale.ValueChanged += f => controller.WorldScale = f;

            panel.AutoFocus.Value = controller.EnableAutoFocus;
            panel.DetectArucos.Value = controller.EnableArucoDetection;
            panel.DetectQrs.Value = controller.EnableQrDetection;
            panel.EnableMeshing.Value = controller.EnableMeshing;

            panel.OcclusionQuality.ValueChanged += (i, _) =>
                controller.OcclusionQuality = (OcclusionQualityType) i;

            panel.Description.Label = controller.Description;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.ResetButton.Clicked += () =>
            {
                controller.ResetSetupMode();
            };

            panel.AutoFocus.ValueChanged += f => controller.EnableAutoFocus = f;
            panel.DetectArucos.ValueChanged += f => controller.EnableArucoDetection = f;
            panel.DetectQrs.ValueChanged += f => controller.EnableQrDetection = f;
            panel.EnableMeshing.ValueChanged += f => controller.EnableMeshing = f;
            
            panel.MarkerSender.Set(controller.MarkerSender);
        }
        
        public override void UpdatePanel()
        {
            panel.Description.Label = controller.Description;
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
                        Core.Logger.Error($"{this}: Unknown field '{field}'");
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