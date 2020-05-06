using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ImageDisplayData : DisplayableListenerData
    {
        ImageListener listener;
        ImagePanelContents panel;
        RawImage anchor;

        public override TopicListener Listener => listener;
        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Image;

        public ImageListener Image => listener;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Listeners.Image);
            displayObject.name = "Image";

            listener = displayObject.GetComponent<ImageListener>();
            //display.Parent = TFListener.DisplaysFrame;
            listener.Config.topic = Topic;
            listener.Config.type = Type;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Image) as ImagePanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<ImageListener.Configuration>();
            Topic = listener.Config.topic;
            return this;
        }

        public override void Start()
        {
            base.Start();
            listener.StartListening();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            listener.Stop();
            ResourcePool.Dispose(Resource.Listeners.Image, listener.gameObject);
            listener = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;
            panel.Description.Label = listener.Description;

            panel.PreviewWidget.Material = listener.Material;

            panel.Min.Value = listener.MinIntensity;
            panel.Max.Value = listener.MaxIntensity;

            panel.Colormap.Index = (int)listener.Colormap;

            panel.Colormap.Interactable = listener.IsMono;
            panel.Min.Interactable = listener.IsMono;
            panel.Max.Interactable = listener.IsMono;

            panel.Colormap.ValueChanged += (i, _) =>
            {
                listener.Colormap = (Resource.ColormapId)i;
            };
            panel.Anchor.ValueChanged += (i, _) =>
            {
                RawImage newAnchor = DataPanelManager.AnchorCanvas.ImageFromAnchorType((AnchorCanvas.AnchorType)i);
                if (anchor == newAnchor)
                {
                    return;
                }
                if (anchor != null && anchor.material == listener.Material)
                {
                    anchor.material = null;
                    anchor.gameObject.SetActive(false);
                }
                anchor = newAnchor;
                if (anchor != null)
                {
                    anchor.material = listener.Material;
                    anchor.gameObject.SetActive(true);
                }
            };
            panel.Min.ValueChanged += f =>
            {
                listener.MinIntensity = f;
            };
            panel.Max.ValueChanged += f =>
            {
                listener.MaxIntensity = f;
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
            panel.Colormap.Interactable = listener.IsMono;
            panel.Min.Interactable = listener.IsMono;
            panel.Max.Interactable = listener.IsMono;
            panel.Description.Label = listener.Description;
            panel.PreviewWidget.image.enabled = false;
            panel.PreviewWidget.image.enabled = true;
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(listener.Config);
        }
    }
}
