#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Core.Configurations;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridModulePanel"/> 
    /// </summary>
    public sealed class GridModuleData : ModuleData
    {
        const float InteriorColorFactor = 0.25f;

        readonly GridModulePanel panel;

        public GridController GridController { get; }
        public override ModuleType ModuleType => ModuleType.Grid;
        public override ModulePanel Panel => panel;
        public override IConfiguration Configuration => GridController.Config;
        public override Controller Controller => GridController;

        public GridModuleData(ModuleDataConstructor constructor)
        {
            panel = ModulePanelManager.GetPanelByResourceType<GridModulePanel>(ModuleType.Grid);
            GridController = new GridController((GridConfiguration?) constructor.Configuration);
            UpdateModuleButton();
            ARController.ARCameraViewChanged += OnARCameraViewChanged;
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                GridController.Dispose();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to dispose controller", e);
            }            
            
            ARController.ARCameraViewChanged -= OnARCameraViewChanged;
        }


        public override void SetupPanel()
        {
            panel.Frame.Owner = GridController;
            panel.ColorPicker.Value = GridController.InteriorColor;
            panel.ShowInterior.Value = GridController.InteriorVisible;
            panel.HideButton.State = GridController.Visible;
            panel.Offset.Value = GridController.Offset;
            panel.FollowCamera.Value = GridController.FollowCamera;
            panel.HideInARMode.Value = GridController.HideInARMode;
            panel.Interactable.Value = GridController.Interactable;
            panel.DarkMode.Value = GridController.DarkMode;
            panel.GridSize.Index = GridController.NumberOfGridCells switch
            {
                <= 10 => 0,
                <= 30 => 1,
                <= 50 => 2,
                <= 70 => 3,
                <= 90 => 4,
                _ => 5,
            };
            
            panel.Metallic.Value = GridController.Metallic;
            panel.Smoothness.Value = GridController.Smoothness;
            panel.OcclusionOnlyMode.Value = GridController.RenderAsOcclusionOnly;

            panel.ColorPicker.ValueChanged += _ => UpdateColor();
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
            panel.Interactable.ValueChanged += f => GridController.Interactable = f;
            panel.DarkMode.ValueChanged += f => GridController.DarkMode = f;
            panel.Metallic.ValueChanged += f => GridController.Metallic = f;
            panel.Smoothness.ValueChanged += f => GridController.Smoothness = f;
            panel.OcclusionOnlyMode.ValueChanged += f => GridController.RenderAsOcclusionOnly = f;
            panel.GridSize.ValueChanged += (i, _) => GridController.NumberOfGridCells = i switch
            {
                0 => 10,
                1 => 30,
                2 => 50,
                3 => 70,
                4 => 90,
                _ => 190
            };
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

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonUtils.DeserializeObject<GridConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(GridConfiguration.InteriorColor):
                        GridController.InteriorColor = config.InteriorColor.ToUnity();
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
                    case nameof(GridConfiguration.Visible):
                        GridController.Visible = config.Visible;
                        break;
                    case nameof(GridConfiguration.Interactable):
                        GridController.Interactable = config.Interactable;
                        break;
                    case nameof(GridConfiguration.Offset):
                        GridController.Offset = config.Offset.ToUnity();
                        break;
                    case nameof(IConfiguration.ModuleType):
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
            config.Grids.Add(GridController.Config);
        }

        void OnARCameraViewChanged(bool _)
        {
            GridController.Visible = GridController.Visible; // reread value
            UpdateModuleButton();
        }
    }
}