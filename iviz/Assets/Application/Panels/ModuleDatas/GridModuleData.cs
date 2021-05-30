using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using Iviz.Ros;
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
        
        [NotNull] public GridController GridController { get; }
        [NotNull] readonly GridPanelContents panel;

        public override ModuleType ModuleType => ModuleType.Grid;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => GridController.Config;
        public override IController Controller => GridController;

        public GridModuleData([NotNull] ModuleDataConstructor constructor) : base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<GridPanelContents>(ModuleType.Grid);

            GridController = new GridController(this);
            if (constructor.Configuration != null)
            {
                GridController.Config = (GridConfiguration)constructor.Configuration;
            }

            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            GridController.StopController();
        }



        public override void SetupPanel()
        {
            panel.ColorPicker.Value = GridController.InteriorColor;
            panel.ShowInterior.Value = GridController.InteriorVisible;
            panel.HideButton.State = GridController.Visible;
            panel.Offset.Value = GridController.Offset;
            panel.FollowCamera.Value = GridController.FollowCamera;
            panel.HideInARMode.Value = GridController.HideInARMode;
            panel.PublishLongTapPosition.Value = GridController.PublishLongTapPosition;
            panel.Sender.Set(GridController.SenderPoint);
            panel.LastTapPosition.Label = GridController.LastTapPositionString;
            panel.TapTopic.Value = GridController.TapTopic;
            panel.TapTopic.Interactable = GridController.PublishLongTapPosition;
            panel.TapTopic.Hints = GetTopicHints();

            panel.ColorPicker.ValueChanged += f =>
            {
                UpdateColor();
            };
            panel.ShowInterior.ValueChanged += f =>
            {
                GridController.InteriorVisible = f;
                UpdateColor();
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.Offset.ValueChanged += f =>
            {
                GridController.Offset = f;
            };
            panel.HideButton.Clicked += () =>
            {
                GridController.Visible = !GridController.Visible;
                panel.HideButton.State = GridController.Visible;
                UpdateModuleButton();
            };
            panel.FollowCamera.ValueChanged += f =>
            {
                GridController.FollowCamera = f;
            };
            panel.HideInARMode.ValueChanged += f =>
            {
                GridController.HideInARMode = f;
            };
            panel.PublishLongTapPosition.ValueChanged += f =>
            {
                GridController.PublishLongTapPosition = f;
                panel.Sender.Set(GridController.SenderPoint);
                panel.TapTopic.Interactable = f;
            };
            panel.TapTopic.EndEdit += f =>
            {
                GridController.TapTopic = f;
                panel.Sender.Set(GridController.SenderPoint);
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

        public override void UpdatePanel()
        {
            panel.TapTopic.Hints = GetTopicHints();
        }

        static readonly string[] OwnPublishedTopicName = {"clicked_point"};
        
        [NotNull]
        IEnumerable<string> GetTopicHints()
        {
            string ownResolvedTopic =
                (GridController.SenderPoint != null &&
                 GridController.SenderPoint.TryGetResolvedTopicName(out string topicName))
                    ? topicName
                    : OwnPublishedTopicName[0];
            return ConnectionManager.Connection.GetSystemPublishedTopicTypes()
                .Where(tuple => tuple.Type == PointStamped.RosMessageType && tuple.Topic != ownResolvedTopic)
                .Select(tuple => tuple.Topic)
                .Concat(OwnPublishedTopicName);
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
                    /*
                    case nameof(GridConfiguration.GridLineWidth):
                        controller.GridLineWidth = config.GridLineWidth;
                        break;
                    case nameof(GridConfiguration.GridCellSize):
                        controller.GridCellSize = config.GridCellSize;
                        break;
                    case nameof(GridConfiguration.NumberOfGridCells):
                        controller.NumberOfGridCells = config.NumberOfGridCells;
                        break;
                        */
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
                        Core.Logger.Error($"{this}: Unknown field '{field}'");
                        break;                    
                }
            }
            
            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Grids.Add(GridController.Config);
        }

        public override void OnARModeChanged(bool value)
        {
            if (!GridController.HideInARMode)
            {
                return;
            }

            GridController.Visible = !value;
            UpdateModuleButton();
        }
    }
}
