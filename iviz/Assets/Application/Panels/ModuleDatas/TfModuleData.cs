#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="TfModulePanel"/> 
    /// </summary>
    public sealed class TfModuleData : ModuleData
    {
        readonly TfListener listener;
        readonly TfModulePanel panel;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.TF;
        public override IConfiguration Configuration => listener.Config;
        public override IController Controller => listener;

        public TfModuleData(ModuleDataConstructor constructor)
        {
            panel = ModulePanelManager.GetPanelByResourceType<TfModulePanel>(ModuleType.TF);
            listener = new TfListener((TfConfiguration?)constructor.Configuration, id => new TfFrameDisplay(id));
            UpdateModuleButton();
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                listener.Dispose();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to dispose controller", e);
            }             
        }

        public void UpdateConfiguration(TfConfiguration configuration)
        {
            listener.Config = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
            panel.KeepAllFrames.Value = listener.KeepAllFrames;
            panel.FlipZ.Value = listener.FlipZ;
            panel.Sender.Set(listener.Publisher);
            //panel.TapSender.Set(listener.TapPublisher);
            panel.Publisher.UpdateText();

            panel.HideButton.Clicked += ToggleVisible;
            panel.ShowFrameLabels.ValueChanged += f => listener.FrameLabelsVisible = f;
            panel.FrameSize.ValueChanged += f => listener.FrameSize = f;
            panel.ConnectToParent.ValueChanged += f => listener.ParentConnectorVisible = f;
            panel.KeepAllFrames.ValueChanged += f => listener.KeepAllFrames = f;
            panel.FlipZ.ValueChanged += f => listener.FlipZ = f;
            panel.ResetButton.Clicked += () => listener.ResetController();
        }

        public override void Close()
        {
            // do nothing!
        }

        public override void UpdatePanel()
        {
            panel.Publisher.UpdateText();
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
                    case nameof(TfConfiguration.KeepAllFrames):
                        listener.KeepAllFrames = config.KeepAllFrames;
                        break;
                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Tf = listener.Config;
        }

        public override string ToString()
        {
            return $"[{ModuleType} Topic='{TfListener.DefaultTopic}' id='{Configuration.Id}']";
        }
    }
}