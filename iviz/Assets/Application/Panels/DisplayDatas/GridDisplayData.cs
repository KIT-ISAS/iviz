using Iviz.App.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class GridDisplayData : DisplayData
    {
        readonly Displays.Grid display;
        readonly GridPanelContents panel;

        public override Resource.Module Module => Resource.Module.Grid;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => display.Config;

        public GridDisplayData(DisplayDataConstructor constructor) :
            base(constructor.DisplayList, constructor.Topic, constructor.Type)
        {
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Listeners.Grid);
            displayObject.name = "Grid";

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Grid) as GridPanelContents;

            display = displayObject.GetComponent<Displays.Grid>();
            display.DisplayData = this;
            display.Parent = TFListener.ListenersFrame;
            if (constructor.Configuration != null)
            {
                display.Config = (GridConfiguration)constructor.Configuration;
            }

            UpdateButtonText();
        }

        /*
        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);

            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Listeners.Grid);
            displayObject.name = "Grid";

            display = displayObject.GetComponent<Displays.Grid>();
            display.DisplayData = this;

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Grid) as GridPanelContents;
            display.Parent = TFListener.ListenersFrame;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<GridConfiguration>();
            return this;
        }
        */

        public override void Stop()
        {
            base.Stop();

            display.Stop();
            ResourcePool.Dispose(Resource.Listeners.Grid, display.gameObject);
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
        }

        /*
        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
        */

        public override void AddToState(StateConfiguration config)
        {
            config.Grids.Add(display.Config);
        }
    }
}
