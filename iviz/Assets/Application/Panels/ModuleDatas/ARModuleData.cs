using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
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

        readonly ARController controller;
        readonly ARPanelContents panel;

        public override Resource.ModuleType ModuleType => Resource.ModuleType.AugmentedReality;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public ARModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<ARPanelContents>(Resource.ModuleType.AugmentedReality);

            controller = Settings.IsHololens ?
                new GameObject("AR Hololens Controller").AddComponent<ARHololensController>() :
                Resource.Controllers.AR.Instantiate().GetComponent<ARController>();

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
            panel.WorldOffset.Mean = controller.WorldPosition;
            panel.WorldAngle.Value = controller.WorldAngle;

            panel.SearchMarker.Value = controller.UseMarker;
            //panel.MarkerSize.Value = controller.MarkerSize;
            panel.MarkerHorizontal.Value = controller.MarkerHorizontal;
            panel.MarkerAngle.Value = controller.MarkerAngle;
            panel.MarkerFrame.Value = controller.MarkerFrame;

            List<string> frameHints = new List<string> { NoneString };
            frameHints.AddRange(TfListener.FramesUsableAsHints);
            panel.MarkerFrame.Hints = frameHints;

            panel.MarkerOffset.Value = controller.MarkerOffset;

            //TFListener.RootMarker.PointerUp += CopyControlMarkerPoseToPanel;
            controller.WorldPoseChanged += OnWorldPoseChanged;

            
            CheckInteractable();

            panel.WorldScale.ValueChanged += f => { controller.WorldScale = f; };
            panel.WorldOffset.ValueChanged += f =>
            {
                controller.SetWorldPosition(f.Ros2Unity(), ARController.RootMover.ModuleData);
            };
            panel.WorldAngle.ValueChanged += f =>
            {
                controller.SetWorldAngle(f, ARController.RootMover.ModuleData);
            };
            panel.SearchMarker.ValueChanged += f =>
            {
                controller.UseMarker = f;
                CheckInteractable();
            };
            panel.MarkerHorizontal.ValueChanged += f => { controller.MarkerHorizontal = f; };
            panel.MarkerAngle.ValueChanged += f => { controller.MarkerAngle = (int)f; };
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
            panel.MarkerOffset.ValueChanged += f => { controller.MarkerOffset = f; };

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

        void OnWorldPoseChanged(ARController.RootMover mover)
        {
            if (mover == ARController.RootMover.ModuleData)
            {
                return;
            }
            
            panel.WorldOffset.Mean = controller.WorldPosition.Unity2Ros();
            panel.WorldAngle.Value = controller.WorldAngle;
        }

        public override void CleanupPanel()
        {
            base.CleanupPanel();
            controller.WorldPoseChanged -= OnWorldPoseChanged;            
        }

        void CheckInteractable()
        {
            panel.MarkerHorizontal.Interactable = controller.UseMarker;
            panel.MarkerAngle.Interactable = controller.UseMarker;
            panel.MarkerFrame.Interactable = controller.UseMarker;
            panel.MarkerOffset.Interactable = controller.UseMarker && controller.MarkerFrame.Length != 0;
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
                        Core.Logger.External(LogLevel.Warn, $"{this}: Unknown field '{field}'");
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