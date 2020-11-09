using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="TfPanelContents"/> 
    /// </summary>
    public sealed class TfModuleData : ListenerModuleData
    {
        [NotNull] readonly TfListener listener;
        [NotNull] readonly TfPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.TF;
        public override IConfiguration Configuration => listener.Config;

        public TfModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.ModuleList,
                constructor.GetConfiguration<TfConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<TfPanelContents>(Resource.ModuleType.TF);
            listener = new TfListener(this);
            if (constructor.Configuration != null)
            {
                listener.Config = (TfConfiguration)constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
            }
            listener.StartListening();
            UpdateModuleButton();
        }

        public void UpdateConfiguration(TfConfiguration configuration)
        {
            listener.Config = configuration;
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = listener;
            panel.Listener.Listener = listener.Listener;
            panel.ListenerStatic.Listener = listener.ListenerStatic;
            panel.HideButton.State = listener.FramesVisible;
            panel.FrameSize.Value = listener.FrameSize;
            panel.ShowFrameLabels.Value = listener.FrameLabelsVisible;
            panel.ConnectToParent.Value = listener.ParentConnectorVisible;
            panel.KeepOnlyUsedFrames.Value = listener.KeepOnlyUsedFrames;
            panel.Sender.Set(listener.Publisher);
            
            panel.HideButton.Clicked += () =>
            {
                listener.FramesVisible = !listener.FramesVisible;
                panel.HideButton.State = listener.FramesVisible;
            };
            panel.ShowFrameLabels.ValueChanged += f =>
            {
                listener.FrameLabelsVisible = f;
            };
            panel.FrameSize.ValueChanged += f =>
            {
                listener.FrameSize = f;
            };
            panel.ConnectToParent.ValueChanged += f =>
            {
                listener.ParentConnectorVisible = f;
            };
            panel.KeepOnlyUsedFrames.ValueChanged += f =>
            {
                listener.KeepOnlyUsedFrames = f;
            };
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<TfConfiguration>(configAsJson);
            
            foreach (string field in fields)
            {
                switch (field) 
                {
                    case nameof(TfConfiguration.Visible):
                        listener.FramesVisible = config.Visible;
                        break;
                    case nameof(TfConfiguration.FrameSize):
                        listener.FrameSize = config.FrameSize;
                        break;
                    case nameof(TfConfiguration.FrameLabelsVisible):
                        listener.FrameLabelsVisible = config.FrameLabelsVisible;
                        break;
                    case nameof(TfConfiguration.ParentConnectorVisible):
                        listener.ParentConnectorVisible = config.ParentConnectorVisible;
                        break;
                    case nameof(TfConfiguration.KeepOnlyUsedFrames):
                        listener.KeepOnlyUsedFrames = config.KeepOnlyUsedFrames;
                        break;
                    default:
                        Logger.External(LogLevel.Warn, $"{this}: Unknown field '{field}'");
                        break;                    
                }
            }
            
            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Tf = listener.Config;
        }

        public override void OnARModeChanged(bool value)
        {
            listener.OnARModeChanged(value);
        }
    }
}
