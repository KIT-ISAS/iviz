using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="OccupancyGridPanelContents"/> 
    /// </summary>

    public sealed class OccupancyGridModuleData : ListenerModuleData
    {
        [NotNull] readonly OccupancyGridListener listener;
        [NotNull] readonly OccupancyGridPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.OccupancyGrid;
        public override IConfiguration Configuration => listener.Config;


        public OccupancyGridModuleData([NotNull] ModuleDataConstructor constructor) :
        base(constructor.ModuleList,
            constructor.GetConfiguration<OccupancyGridConfiguration>()?.Topic ?? constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<OccupancyGridPanelContents>(Resource.ModuleType.OccupancyGrid);
            listener = new OccupancyGridListener(this);
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (OccupancyGridConfiguration)constructor.Configuration;
            }
            listener.StartListening();
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

            panel.OcclusionOnlyMode.Value = listener.RenderAsOcclusionOnly;
            panel.Tint.Value = listener.Tint;
            //panel.Alpha.Value = listener.Tint.a;

            panel.Tint.ValueChanged += f =>
            {
                Color color = f;
                color.a = 1;
                listener.Tint = color;
            };

            panel.OcclusionOnlyMode.ValueChanged += f =>
            {
                listener.RenderAsOcclusionOnly = f;
            };

            panel.FlipColors.ValueChanged += f =>
            {
                listener.FlipMinMax = f;
            };
            panel.ScaleZ.ValueChanged += f =>
            {
                listener.ScaleZ = f;
            };

            panel.Colormap.ValueChanged += (i, _) =>
            {
                listener.Colormap = (Resource.ColormapId)i;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.HideButton.Clicked += () =>
            {
                listener.Visible = !listener.Visible;
                panel.HideButton.State = listener.Visible;
                UpdateModuleButton();
            };
        }
        
        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<OccupancyGridConfiguration>(configAsJson);
            
            foreach (string field in fields)
            {
                switch (field) 
                {
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
                        listener.Tint = config.Tint;
                        break;

                    default:
                        Logger.External(LogLevel.Warn, $"{this}: Unknown field '{field}'");
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
