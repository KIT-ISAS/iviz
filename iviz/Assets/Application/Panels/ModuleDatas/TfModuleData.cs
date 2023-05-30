﻿#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
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
        public override Controller Controller => listener;

        public TfModuleData(ModuleDataConstructor constructor)
        {
            panel = ModulePanelManager.GetPanelByResourceType<TfModulePanel>(ModuleType.TF);
            listener = new TfListener((TfConfiguration?)constructor.Configuration);
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

        protected override void UpdateModuleButton()
        {
            ModuleListButtonText = ModuleListPanel.CreateButtonTextForModule(this, listener.Config.Topic);
        }
        
        public override void SetupPanel()
        {
            panel.Frame.Owner = listener;
            panel.Listener.Listener = listener.Listener;
            panel.ListenerStatic.Listener = listener.ListenerStatic;
            panel.HideButton.State = listener.Visible;
            panel.FrameSize.Value = listener.FrameSize;
            panel.PreferUdp.Value = listener.PreferUdp;
            panel.ShowFrameLabels.Value = listener.LabelsVisible;
            panel.ConnectToParent.Value = listener.ParentConnectorVisible;
            panel.KeepAllFrames.Value = listener.KeepAllFrames;
            panel.FlipZ.Value = listener.FlipZ;
            panel.Interactable.Value = listener.Interactable;
            panel.Sender.Set(listener.Publisher);
            panel.Publisher.UpdateText();

            panel.HideButton.Clicked += ToggleVisible;
            panel.ShowFrameLabels.ValueChanged += f => listener.LabelsVisible = f;
            panel.FrameSize.ValueChanged += f => listener.FrameSize = f;
            panel.ConnectToParent.ValueChanged += f => listener.ParentConnectorVisible = f;
            panel.KeepAllFrames.ValueChanged += f => listener.KeepAllFrames = f;
            panel.FlipZ.ValueChanged += f => listener.FlipZ = f;
            panel.Interactable.ValueChanged += f => listener.Interactable = f;
            panel.ResetButton.Clicked += () => listener.ResetController();
            panel.PreferUdp.ValueChanged += f => listener.PreferUdp = f;

            panel.CloseButton.Clicked += () => ModulePanelManager.TogglePanel(this);
        }

        public override void Close()
        {
            // do nothing!
        }

        public override void UpdatePanel()
        {
            panel.Publisher.UpdateText();
        }

        public void UpdateConfiguration(TfConfiguration configuration)
        {
            ThrowHelper.ThrowIfNull(configuration, nameof(configuration));
            listener.Config = configuration;
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonUtils.DeserializeObject<TfConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(IConfiguration.ModuleType):
                        break;
                    case nameof(TfConfiguration.FrameSize):
                        listener.FrameSize = config.FrameSize;
                        break;
                    case nameof(TfConfiguration.FrameLabelsVisible):
                        listener.LabelsVisible = config.FrameLabelsVisible;
                        break;
                    case nameof(TfConfiguration.ParentConnectorVisible):
                        listener.ParentConnectorVisible = config.ParentConnectorVisible;
                        break;
                    case nameof(TfConfiguration.KeepAllFrames):
                        listener.KeepAllFrames = config.KeepAllFrames;
                        break;
                    case nameof(TfConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(TfConfiguration.Interactable):
                        listener.Interactable = config.Interactable;
                        break;
                    case nameof(TfConfiguration.FlipZ):
                        listener.FlipZ = config.FlipZ;
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
            return $"[{nameof(TfModuleData)} Topic='{TfModule.DefaultTopic}' id='{Configuration.Id}']";
        }
    }
}