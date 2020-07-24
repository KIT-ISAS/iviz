using Iviz.Controllers;
using Iviz.Displays;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridPanelContents"/> 
    /// </summary>
    public sealed class GridModuleData : ModuleData
    {
        readonly GridController controller;
        readonly GridPanelContents panel;

        public override Resource.Module Module => Resource.Module.Grid;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public GridModuleData(ModuleDataConstructor constructor) :
            base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Grid) as GridPanelContents;

            //controller = Instantiate<GridController>();
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

            controller.Stop();
            //Object.Destroy(controller.gameObject);
        }

        const float InteriorColorFactor = 0.5f;

        public override void SetupPanel()
        {
            panel.LineWidth.Value = controller.GridLineWidth;
            panel.NumberOfCells.Value = controller.NumberOfGridCells;
            //panel.CellSize.Value = controller.GridCellSize;
            panel.Orientation.Index = (int)controller.Orientation;
            panel.ColorPicker.Value = controller.InteriorColor;
            panel.ShowInterior.Value = controller.ShowInterior;
            panel.HideButton.State = controller.Visible;
            panel.Offset.Value = controller.Offset;
            panel.FollowCamera.Value = controller.FollowCamera;
            panel.HideInARMode.Value = controller.HideInARMode;

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
            panel.Orientation.ValueChanged += (i, _) =>
            {
                controller.Orientation = (GridOrientation)i;
            };
            panel.ColorPicker.ValueChanged += f =>
            {
                controller.GridColor = f * InteriorColorFactor;
                controller.InteriorColor = f;
            };
            panel.ShowInterior.ValueChanged += f =>
            {
                controller.ShowInterior = f;
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
