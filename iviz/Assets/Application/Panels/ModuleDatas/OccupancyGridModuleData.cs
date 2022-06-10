#nullable enable

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
    /// <see cref="OccupancyGridModulePanel"/> 
    /// </summary>
    public sealed class OccupancyGridModuleData : ListenerModuleData
    {
        readonly OccupancyGridListener listener;
        readonly OccupancyGridModulePanel panel;

        protected override ListenerController Listener => listener;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.OccupancyGrid;
        public override IConfiguration Configuration => listener.Config;


        public OccupancyGridModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<OccupancyGridModulePanel>(ModuleType.OccupancyGrid);
            listener = new OccupancyGridListener((OccupancyGridConfiguration?)constructor.Configuration, Topic);
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;

            panel.Colormap.Index = (int)listener.Colormap;
            panel.HideButton.State = listener.Visible;
            panel.FlipColors.Value = listener.FlipMinMax;
            panel.ScaleZ.Value = listener.ScaleZ;
            panel.ShowCubes.Value = listener.CubesVisible;
            panel.ShowTexture.Value = listener.TextureVisible;

            panel.OcclusionOnlyMode.Value = listener.RenderAsOcclusionOnly;
            panel.Tint.Value = listener.Tint;
            panel.Description.Text = listener.Description;

            panel.OcclusionOnlyMode.Interactable = listener.CubesVisible;
            panel.ScaleZ.Interactable = listener.CubesVisible;

            panel.Tint.ValueChanged += f =>
            {
                Color color = f;
                color.a = 1;
                listener.Tint = color;
            };

            panel.OcclusionOnlyMode.ValueChanged += f => { listener.RenderAsOcclusionOnly = f; };

            panel.FlipColors.ValueChanged += f => { listener.FlipMinMax = f; };
            panel.ScaleZ.ValueChanged += f => { listener.ScaleZ = f; };
            panel.ShowTexture.ValueChanged += f => { listener.TextureVisible = f; };
            panel.ShowCubes.ValueChanged += f =>
            {
                listener.CubesVisible = f;
                panel.OcclusionOnlyMode.Interactable = f;
                panel.ScaleZ.Interactable = f;
            };

            panel.Colormap.ValueChanged += (i, _) => { listener.Colormap = (ColormapId)i; };
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
        }

        public override void UpdatePanel()
        {
            panel.Description.Text = listener.Description;
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonConvert.DeserializeObject<OccupancyGridConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(IConfiguration.ModuleType):
                        break;
                    case nameof(OccupancyGridConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(OccupancyGridConfiguration.Colormap):
                        listener.Colormap = config.Colormap;
                        break;
                    case nameof(OccupancyGridConfiguration.FlipMinMax):
                        listener.FlipMinMax = config.FlipMinMax;
                        break;
                    case nameof(OccupancyGridConfiguration.ScaleZ):
                        listener.ScaleZ = config.ScaleZ;
                        break;
                    case nameof(OccupancyGridConfiguration.RenderAsOcclusionOnly):
                        listener.RenderAsOcclusionOnly = config.RenderAsOcclusionOnly;
                        break;
                    case nameof(OccupancyGridConfiguration.Tint):
                        listener.Tint = config.Tint.ToUnity();
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
            config.OccupancyGrids.Add(listener.Config);
        }
    }
}