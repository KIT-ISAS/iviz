using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ImagePanelContents"/> 
    /// </summary>
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
                constructor.GetConfiguration<ImageConfiguration>()?.Topic ?? constructor.Topic,
                constructor.GetConfiguration<ImageConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Image) as ImagePanelContents;
            listener = Resource.Listeners.Instantiate<ImageListener>();
            listener.name = "Image";
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

            panel.ShowBillboard.Value = listener.EnableBillboard;
            panel.BillboardSize.Value = listener.BillboardSize;

            panel.BillboardSize.Interactable = listener.EnableBillboard;

            panel.Colormap.ValueChanged += (i, _) =>
            {
                listener.Colormap = (Resource.ColormapId)i;
            };
            /*
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
            */
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
            panel.ShowBillboard.ValueChanged += f =>
            {
                panel.BillboardSize.Interactable = f;
                listener.EnableBillboard = f;
            };
            panel.BillboardSize.ValueChanged += f =>
            {
                listener.BillboardSize = f;
            };

        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.Colormap.Interactable = listener.IsMono;
            panel.Min.Interactable = listener.IsMono;
            panel.Max.Interactable = listener.IsMono;
            panel.Description.Label = listener.Description;
            panel.PreviewWidget.ToggleImageEnabled();
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
