using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ImageDisplayData : ListenerDisplayData
    {
        readonly ImageListener listener;
        readonly ImagePanelContents panel;
        RawImage anchor;

        protected override TopicListener Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Image;
        public override IConfiguration Configuration => listener.Config;

        public ImageListener Image => listener;


        public ImageDisplayData(DisplayDataConstructor constructor) :
            base(constructor.DisplayList,
                ((ImageConfiguration)constructor.Configuration)?.Topic ?? constructor.Topic,
                ((ImageConfiguration)constructor.Configuration)?.Type ?? constructor.Type)
        {
            GameObject displayObject = Resource.Listeners.Image.Instantiate();
            displayObject.name = "Image";

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Image) as ImagePanelContents;
            listener = displayObject.GetComponent<ImageListener>();
            if (constructor.Configuration != null)
            {
                listener.Config = (ImageConfiguration)constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
                listener.Config.Type = Type;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;
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

        /*
        public override JToken Serialize()
        {
            return JToken.FromObject(listener.Config);
        }
        */

        public override void AddToState(StateConfiguration config)
        {
            config.Images.Add(listener.Config);
        }
    }
}
