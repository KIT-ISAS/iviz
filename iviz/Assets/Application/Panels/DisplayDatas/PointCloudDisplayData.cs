using Iviz.App.Listeners;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public class PointCloudDisplayData : DisplayableListenerData
    {
        readonly PointCloudListener listener;
        readonly PointCloudPanelContents panel;

        protected override TopicListener Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.PointCloud;
        public override IConfiguration Configuration => listener.Config;


        public PointCloudDisplayData(DisplayDataConstructor constructor) :
        base(constructor.DisplayList, ((PointCloudConfiguration)constructor.Configuration)?.Topic ?? constructor.Topic, constructor.Type)
        {
            GameObject listenerObject = Resource.Listeners.PointCloud.Instantiate();
            listenerObject.name = "PointCloud:" + Topic;

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.PointCloud) as PointCloudPanelContents;
            listener = listenerObject.GetComponent<PointCloudListener>();
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (PointCloudConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;

            panel.Colormap.Index = (int)listener.Colormap;
            panel.PointSize.Value = listener.PointSize;
            panel.IntensityChannel.Options = listener.FieldNames;
            panel.IntensityChannel.Value = listener.IntensityChannel;
            panel.HideButton.State = listener.Visible;

            panel.ForceMinMax.Value = listener.ForceMinMax;
            panel.MinIntensity.Value = listener.MinIntensity.ToString(UnityUtils.Culture);
            panel.MaxIntensity.Value = listener.MaxIntensity.ToString(UnityUtils.Culture);
            panel.MinIntensity.Interactable = listener.ForceMinMax;
            panel.MaxIntensity.Interactable = listener.ForceMinMax;

            panel.PointSize.ValueChanged += f =>
            {
                listener.PointSize = f;
            };
            panel.IntensityChannel.ValueChanged += (_, s) =>
            {
                listener.IntensityChannel = s;
            };
            panel.Colormap.ValueChanged += (i, _) =>
            {
                listener.Colormap = (Resource.ColormapId)i;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
            panel.HideButton.Clicked += () =>
            {
                listener.Visible = !listener.Visible;
                panel.HideButton.State = listener.Visible;
                UpdateButtonText();
            };
            panel.ForceMinMax.ValueChanged += f =>
            {
                listener.ForceMinMax = f;
                panel.MinIntensity.Interactable = f;
                panel.MaxIntensity.Interactable = f;
            };
            panel.MinIntensity.ValueChanged += f =>
            {
                listener.MinIntensity = float.Parse(f);
            };
            panel.MaxIntensity.ValueChanged += f =>
            {
                listener.MaxIntensity = float.Parse(f);
            };
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.IntensityChannel.Options = listener.FieldNames;
        }

        public override void AddToState(StateConfiguration config)
        {
            config.PointClouds.Add(listener.Config);
        }
    }
}
