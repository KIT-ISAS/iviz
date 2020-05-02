using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class RobotDisplayData : DisplayData
    {
        Robot display;
        RobotPanelContents panel;

        public string RobotName => display.LongName;
        public Robot Robot => display;
        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Robot;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);

            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Displays.Robot);
            display = displayObject.GetComponent<Robot>();
            display.Parent = TFListener.DisplaysFrame;
            display.DisplayData = this;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Robot) as RobotPanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<Robot.Configuration>();
            return this;
        }

        public override void Start()
        {
            base.Start();
            UpdateButtonText();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            display.Stop();
            ResourcePool.Dispose(Resource.Displays.Robot, display.gameObject);
            display = null;
        }

        public override void SetupPanel()
        {
            panel.ResourceType.Value = display.RobotResource;
            panel.FramePrefix.Value = display.FramePrefix;
            panel.FrameSuffix.Value = display.FrameSuffix;
            panel.AttachToTF.Value = display.AttachToTF;

            panel.ResourceType.ValueChanged += (_, f) =>
            {
                display.RobotResource = f;
                UpdateButtonText();
            };
            panel.AttachToTF.ValueChanged += f =>
            {
                display.AttachToTF = f;
            }; 
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
            panel.FramePrefix.EndEdit += f =>
            {
                display.FramePrefix = f;
            };
            panel.FrameSuffix.EndEdit += f =>
            {
                display.FrameSuffix = f;
            };
        }

        void UpdateButtonText()
        {
            const int maxLength = 20;
            if (display.LongName.Length > maxLength)
            {
                string nameShort = display.LongName.Substring(0, maxLength);
                ButtonText = $"{nameShort}...\n<b>{Module}</b>";
            }
            else
            {
                ButtonText = $"{display.LongName}\n<b>{Module}</b>";
            }
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
    }
}
