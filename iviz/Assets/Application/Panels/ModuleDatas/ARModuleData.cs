using System.Collections.Generic;
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

        public override Resource.ModuleType ModuleType => Resource.ModuleType.AugmentedReality;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public ARModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<ARPanelContents>(Resource.ModuleType.AugmentedReality);

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

            /*
            panel.SearchMarker.Value = controller.UseMarker;
            panel.MarkerHorizontal.Value = controller.MarkerHorizontal;
            panel.MarkerAngle.Value = controller.MarkerAngle;
            panel.MarkerFrame.Value = controller.MarkerFrame;

            List<string> frameHints = new List<string> {NoneString};
            frameHints.AddRange(TfListener.FramesUsableAsHints);
            panel.MarkerFrame.Hints = frameHints;

            panel.MarkerOffset.Value = controller.MarkerOffset;
            */

            panel.OcclusionQuality.Options = new[] {"Off", "Fast", "Medium", "Best"};
            panel.OcclusionQuality.Index = (int) controller.OcclusionQuality;

            //CheckInteractable();

            panel.WorldScale.ValueChanged += f => controller.WorldScale = f;
            
            /*
            panel.SearchMarker.ValueChanged += f =>
            {
                controller.UseMarker = f;
                CheckInteractable();
            };
            panel.MarkerHorizontal.ValueChanged += f => controller.MarkerHorizontal = f;
            panel.MarkerAngle.ValueChanged += f => controller.MarkerAngle = (int) f;
            panel.MarkerFrame.EndEdit += f =>
            {
                if (f == NoneString)
                {
                    panel.MarkerFrame.Value = "";
                    controller.MarkerFrame = "";
                }
                else
                {
                    controller.MarkerFrame = f;
                }

                CheckInteractable();
            };
            panel.MarkerOffset.ValueChanged += f => controller.MarkerOffset = f;
            */

            panel.OcclusionQuality.ValueChanged += (i, _) =>
                controller.OcclusionQuality = (OcclusionQualityType) i;

            panel.Description.Label = controller.Description;
            
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.HideButton.Clicked += () =>
            {
                controller.Visible = !controller.Visible;
                panel.HideButton.State = controller.Visible;
                UpdateModuleButton();
            };
        }

        public override void UpdatePanel()
        {
            panel.Description.Label = controller.Description;
        }

        /*
        void CheckInteractable()
        {
            panel.MarkerHorizontal.Interactable = controller.UseMarker;
            panel.MarkerAngle.Interactable = controller.UseMarker;
            panel.MarkerFrame.Interactable = controller.UseMarker;
            panel.MarkerOffset.Interactable = controller.UseMarker && controller.MarkerFrame.Length != 0;
        }
        */

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
                    case nameof(ARConfiguration.SearchMarker):
                        controller.Visible = config.Visible;
                        break;
                    case nameof(ARConfiguration.MarkerHorizontal):
                        controller.Visible = config.Visible;
                        break;
                    case nameof(ARConfiguration.MarkerAngle):
                        controller.Visible = config.Visible;
                        break;
                    case nameof(ARConfiguration.MarkerFrame):
                        controller.Visible = config.Visible;
                        break;
                    case nameof(ARConfiguration.MarkerOffset):
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