using Iviz.App.Displays;
using Iviz.App.Listeners;
using Iviz.Displays;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class ARDisplayData : DisplayData
    {
        readonly AugmentedReality display;
        readonly GridPanelContents panel;

        public override Resource.Module Module => Resource.Module.AR;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => display.Config;

        public ARDisplayData(DisplayDataConstructor constructor) :
            base(constructor.DisplayList, constructor.Topic, constructor.Type)
        {
            GameObject displayObject = Resource.Listeners.AR.Instantiate();    
            displayObject.name = "AR";

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.AR) as GridPanelContents;

            display = displayObject.GetComponent<AugmentedReality>();
            if (constructor.Configuration != null)
            {
                display.Config = (ARConfiguration)constructor.Configuration;
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
            panel.HideButton.Clicked += () =>
            {
                display.Visible = !display.Visible;
                panel.HideButton.State = display.Visible;
                UpdateButtonText();
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
