using Iviz.App.Displays;
using Iviz.App.Listeners;
using Iviz.Displays;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridPanelContents"/> 
    /// </summary>
    public class GridDisplayData : DisplayData
    {
        readonly Listeners.Grid display;
        readonly GridPanelContents panel;

        public override Resource.Module Module => Resource.Module.Grid;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => display.Config;
        public override IController Controller => display;

        public GridDisplayData(DisplayDataConstructor constructor) :
            base(constructor.DisplayList, constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Grid) as GridPanelContents;

            display = Resource.Listeners.Instantiate<Listeners.Grid>();
            display.DisplayData = this;
            if (constructor.Configuration != null)
            {
                display.Config = (GridConfiguration)constructor.Configuration;
            }

            UpdateButtonText();
        }

        public override void Stop()
        {
            base.Stop();

            display.Stop();
            Object.Destroy(display.gameObject);
        }

        const float InteriorColorFactor = 0.5f;

        public override void SetupPanel()
        {
            panel.LineWidth.Value = display.GridLineWidth;
            panel.NumberOfCells.Value = display.NumberOfGridCells;
            panel.CellSize.Value = display.GridCellSize;
            panel.Orientation.Index = (int)display.Orientation;
            panel.ColorPicker.Value = display.InteriorColor;
            panel.ShowInterior.Value = display.ShowInterior;
            panel.HideButton.State = display.Visible;
            panel.Offset.Value = display.Offset;
            panel.FollowCamera.Value = display.FollowCamera;

            panel.LineWidth.ValueChanged += f =>
            {
                display.GridLineWidth = f;
            };
            panel.NumberOfCells.ValueChanged += f =>
            {
                display.NumberOfGridCells = (int)f;
            };
            panel.CellSize.ValueChanged += f =>
            {
                display.GridCellSize = f;
            };
            panel.Orientation.ValueChanged += (i, _) =>
            {
                display.Orientation = (GridOrientation)i;
            };
            panel.ColorPicker.ValueChanged += f =>
            {
                display.GridColor = f * InteriorColorFactor;
                display.InteriorColor = f;
            };
            panel.ShowInterior.ValueChanged += f =>
            {
                display.ShowInterior = f;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
            panel.Offset.ValueChanged += f =>
            {
                display.Offset = f;
            };
            panel.HideButton.Clicked += () =>
            {
                display.Visible = !display.Visible;
                panel.HideButton.State = display.Visible;
                UpdateButtonText();
            };
            panel.FollowCamera.ValueChanged += f =>
            {
                display.FollowCamera = f;
            };
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Grids.Add(display.Config);
        }
    }
}
