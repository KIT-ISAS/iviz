#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridPanelContents"/> 
    /// </summary>
    public sealed class GridModuleData : ModuleData
    {
        const float InteriorColorFactor = 0.25f;

        readonly GridPanelContents panel;

        public GridController GridController { get; }
        public override ModuleType ModuleType => ModuleType.Grid;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => GridController.Config;
        public override IController Controller => GridController;

        public GridModuleData(ModuleDataConstructor constructor) : base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<GridPanelContents>(ModuleType.Grid);

            GridController = new GridController(this);
            if (constructor.Configuration != null)
            {
                GridController.Config = (GridConfiguration) constructor.Configuration;
            }

            UpdateModuleButton();

            ARController.ARCameraViewChanged += OnARCameraViewChanged;
        }

        public override void Dispose()
        {
            base.Dispose();
            GridController.Dispose();
            
            ARController.ARCameraViewChanged -= OnARCameraViewChanged;
        }


        public override void SetupPanel()
        {
            panel.ColorPicker.Value = GridController.InteriorColor;
            panel.ShowInterior.Value = GridController.InteriorVisible;
            panel.HideButton.State = GridController.Visible;
            panel.Offset.Value = GridController.Offset;
            panel.FollowCamera.Value = GridController.FollowCamera;
            panel.HideInARMode.Value = GridController.HideInARMode;

            panel.ColorPicker.ValueChanged += f => UpdateColor();
            panel.ShowInterior.ValueChanged += f =>
            {
                GridController.InteriorVisible = f;
                UpdateColor();
            };
            panel.CloseButton.Clicked += Close;
            panel.Offset.ValueChanged += f => GridController.Offset = f;
            panel.HideButton.Clicked += ToggleVisible;
            panel.FollowCamera.ValueChanged += f => GridController.FollowCamera = f;
            panel.HideInARMode.ValueChanged += f => GridController.HideInARMode = f;
        }

        void UpdateColor()
        {
            Color f = panel.ColorPicker.Value;
            if (GridController.InteriorVisible)
            {
                GridController.GridColor = f * InteriorColorFactor;
                GridController.InteriorColor = f;
            }
            else
            {
                GridController.GridColor = f;
            }
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            GridConfiguration config = JsonConvert.DeserializeObject<GridConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(GridConfiguration.Visible):
                        GridController.Visible = config.Visible;
                        break;
                    case nameof(GridConfiguration.GridColor):
                        GridController.GridColor = config.GridColor;
                        break;
                    case nameof(GridConfiguration.InteriorColor):
                        GridController.InteriorColor = config.InteriorColor;
                        break;
                    case nameof(GridConfiguration.InteriorVisible):
                        GridController.InteriorVisible = config.InteriorVisible;
                        break;
                    case nameof(GridConfiguration.FollowCamera):
                        GridController.FollowCamera = config.FollowCamera;
                        break;
                    case nameof(GridConfiguration.HideInARMode):
                        GridController.HideInARMode = config.HideInARMode;
                        break;
                    case nameof(GridConfiguration.Offset):
                        GridController.Offset = config.Offset;
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
            config.Grids.Add(GridController.Config);
        }

        void OnARCameraViewChanged(bool _)
        {
            GridController.Visible = GridController.Visible; // reread value
            UpdateModuleButton();
        }
    }
}