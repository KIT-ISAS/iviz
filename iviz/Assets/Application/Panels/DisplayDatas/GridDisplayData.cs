using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class GridDisplayData : DisplayData
    {
        Grid display;
        GridPanelContents panel;

        public override Resource.Module Module => Resource.Module.Grid;
        public override DataPanelContents Panel => panel;
        public Display Display => display;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);

            Resource.Displays.Initialize();
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Displays.Grid);
            displayObject.name = "Grid";

            display = displayObject.GetComponent<Grid>();
            display.DisplayData = this;

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Grid) as GridPanelContents;
            display.Parent = TFListener.DisplaysFrame;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<Grid.Configuration>();
            return this;
        }

        public override void Cleanup()
        {
            base.Cleanup();

            display.Stop();
            ResourcePool.Dispose(Resource.Displays.Grid, display.gameObject);
            display = null;
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
                display.Orientation = (Grid.OrientationType)i;
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
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
    }
}
