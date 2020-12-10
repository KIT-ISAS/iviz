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
    /// <see cref="GridPanelContents"/> 
    /// </summary>
    public sealed class GridModuleData : ModuleData
    {
        const float InteriorColorFactor = 0.25f;
        
        [NotNull] readonly GridController controller;
        [NotNull] readonly GridPanelContents panel;

        public override Resource.ModuleType ModuleType => Resource.ModuleType.Grid;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public GridModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<GridPanelContents>(Resource.ModuleType.Grid);

            controller = new GridController(this);
            if (constructor.Configuration != null)
            {
                controller.Config = (GridConfiguration)constructor.Configuration;
            }

            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();

            controller.StopController();
        }



        public override void SetupPanel()
        {
            panel.LineWidth.Value = controller.GridLineWidth;
            panel.NumberOfCells.Value = controller.NumberOfGridCells;
            //panel.CellSize.Value = controller.GridCellSize;
            //panel.Orientation.Index = (int)controller.Orientation;
            panel.ColorPicker.Value = controller.InteriorColor;
            panel.ShowInterior.Value = controller.InteriorVisible;
            panel.HideButton.State = controller.Visible;
            panel.Offset.Value = controller.Offset;
            panel.FollowCamera.Value = controller.FollowCamera;
            panel.HideInARMode.Value = controller.HideInARMode;
            panel.PublishLongTapPosition.Value = controller.PublishLongTapPosition;
            panel.Sender.Set(controller.SenderPoint);
            panel.LastTapPosition.Label = controller.LastTapPositionString;

            panel.LineWidth.ValueChanged += f =>
            {
                controller.GridLineWidth = f;
            };
            panel.NumberOfCells.ValueChanged += f =>
            {
                controller.NumberOfGridCells = (int)f;
            };
            /*
            panel.CellSize.ValueChanged += f =>
            {
                controller.GridCellSize = f;
            };
            */
            /*
            panel.Orientation.ValueChanged += (i, _) =>
            {
                controller.Orientation = (GridOrientation)i;
            };
            */
            panel.ColorPicker.ValueChanged += f =>
            {
                UpdateColor();
            };
            panel.ShowInterior.ValueChanged += f =>
            {
                controller.InteriorVisible = f;
                UpdateColor();
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.Offset.ValueChanged += f =>
            {
                controller.Offset = f;
            };
            panel.HideButton.Clicked += () =>
            {
                controller.Visible = !controller.Visible;
                panel.HideButton.State = controller.Visible;
                UpdateModuleButton();
            };
            panel.FollowCamera.ValueChanged += f =>
            {
                controller.FollowCamera = f;
            };
            panel.HideInARMode.ValueChanged += f =>
            {
                controller.HideInARMode = f;
            };
            panel.PublishLongTapPosition.ValueChanged += f =>
            {
                controller.PublishLongTapPosition = f;
                panel.Sender.Set(controller.SenderPoint);
            };
        }

        void UpdateColor()
        {
            Color f = panel.ColorPicker.Value;
            if (controller.InteriorVisible)
            {
                controller.GridColor = f * InteriorColorFactor;
                controller.InteriorColor = f;
            }
            else
            {
                controller.GridColor = f;
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
                        controller.Visible = config.Visible;
                        break;
                    case nameof(GridConfiguration.GridColor):
                        controller.GridColor = config.GridColor;
                        break;
                    case nameof(GridConfiguration.InteriorColor):
                        controller.InteriorColor = config.InteriorColor;
                        break;
                    case nameof(GridConfiguration.GridLineWidth):
                        controller.GridLineWidth = config.GridLineWidth;
                        break;
                    case nameof(GridConfiguration.GridCellSize):
                        controller.GridCellSize = config.GridCellSize;
                        break;
                    case nameof(GridConfiguration.NumberOfGridCells):
                        controller.NumberOfGridCells = config.NumberOfGridCells;
                        break;
                    case nameof(GridConfiguration.InteriorVisible):
                        controller.InteriorVisible = config.InteriorVisible;
                        break;
                    case nameof(GridConfiguration.FollowCamera):
                        controller.FollowCamera = config.FollowCamera;
                        break;
                    case nameof(GridConfiguration.HideInARMode):
                        controller.HideInARMode = config.HideInARMode;
                        break;
                    case nameof(GridConfiguration.Offset):
                        controller.Offset = config.Offset;
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
            config.Grids.Add(controller.Config);
        }

        public override void OnARModeChanged(bool value)
        {
            base.OnARModeChanged(value);
            if (!controller.HideInARMode)
            {
                return;
            }

            controller.Visible = !value;
            UpdateModuleButton();
        }
    }
}
