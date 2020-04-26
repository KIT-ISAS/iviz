using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ImageDisplayData : DisplayableListenerData
    {
        ImageListener display;
        ImagePanelContents panel;
        RawImage anchor;

        public override DisplayableListener Display => display;
        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Image;

        public ImageListener Image => display;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Displays.Image);
            displayObject.name = "Image";

            display = displayObject.GetComponent<ImageListener>();
            display.Parent = TFListener.DisplaysFrame;
            display.Config.topic = Topic;
            display.Config.type = Type;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Image) as ImagePanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<ImageListener.Configuration>();
            Topic = display.Config.topic;
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
            ResourcePool.Dispose(Resource.Displays.Image, display.gameObject);
            display = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;
            panel.Description.Label = display.Description;

            panel.PreviewWidget.Material = display.Material;

            panel.Min.Value = display.MinIntensity;
            panel.Max.Value = display.MaxIntensity;

            panel.Colormap.Index = (int)display.Colormap;

            panel.Colormap.Interactable = display.IsMono;
            panel.Min.Interactable = display.IsMono;
            panel.Max.Interactable = display.IsMono;

            panel.Colormap.ValueChanged += (i, _) =>
            {
                display.Colormap = (Resource.Colormaps.Id)i;
            };
            panel.Anchor.ValueChanged += (i, _) =>
            {
                RawImage newAnchor = DataPanelManager.AnchorCanvas.ImageFromAnchorType((AnchorCanvas.AnchorType)i);
                if (anchor == newAnchor)
                {
                    return;
                }
                if (anchor != null && anchor.material == display.Material)
                {
                    anchor.material = null;
                    anchor.gameObject.SetActive(false);
                }
                anchor = newAnchor;
                if (anchor != null)
                {
                    anchor.material = display.Material;
                    anchor.gameObject.SetActive(true);
                }
            };
            panel.Min.ValueChanged += f =>
            {
                display.MinIntensity = f;
            };
            panel.Max.ValueChanged += f =>
            {
                display.MaxIntensity = f;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.Colormap.Interactable = display.IsMono;
            panel.Min.Interactable = display.IsMono;
            panel.Max.Interactable = display.IsMono;
            panel.Description.Label = display.Description;
            panel.PreviewWidget.image.enabled = false;
            panel.PreviewWidget.image.enabled = true;
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
    }
}
