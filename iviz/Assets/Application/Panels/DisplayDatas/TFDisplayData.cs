using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class TFDisplayData : DisplayableListenerData
    {
        TFListener display;
        TFPanelContents panel;

        public override DisplayableListener Display => display;
        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.TF;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            Resource.DisplaysType.Initialize();
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.DisplaysType.TF);
            displayObject.name = "TF";

            display = displayObject.GetComponent<TFListener>();
            display.Parent = null;
            display.Config.topic = Topic;

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.TF) as TFPanelContents;

            return this;
        }

        public override void Start()
        {
            base.Start();
            display.StartListening();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            display.Stop();
            ResourcePool.Dispose(Resource.DisplaysType.TF, display.gameObject);
            display = null;
        }

        public override void SetupPanel()
        {
            panel.ShowAxes.Value = display.AxisVisible;
            panel.FrameSize.Value = display.AxisSize;
            panel.ShowAxes.Value = display.AxisVisible;
            panel.FrameLabelSize.Value = display.AxisLabelSize;
            panel.FrameLabelSize.Interactable = display.AxisLabelVisible;
            panel.ConnectToParent.Value = display.ParentConnectorVisible;
            panel.ShowAllFrames.Value = display.ShowAllFrames;

            panel.ShowAxes.ValueChanged += f =>
            {
                display.AxisVisible = f;
            };
            panel.ShowFrameLabels.ValueChanged += f =>
            {
                display.AxisLabelVisible = f;
                panel.FrameLabelSize.Interactable = f;
            };
            panel.FrameSize.ValueChanged += f =>
            {
                display.AxisSize = f;
            };
            panel.FrameLabelSize.ValueChanged += f =>
            {
                display.AxisLabelSize = f;
            };
            panel.ConnectToParent.ValueChanged += f =>
            {
                display.ParentConnectorVisible = f;
            };
            panel.ShowAllFrames.ValueChanged += f =>
            {
                display.ShowAllFrames = f;
            };
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<TFListener.Configuration>();
            Topic = display.Config.topic;
            return this;
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
    }
}
