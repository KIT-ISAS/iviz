﻿#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Resources;

namespace Iviz.App
{
    public abstract class ModuleData : IModuleData
    {
        protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;
        protected static DataPanelManager DataPanelManager => ModuleListPanel.DataPanelManager;

        string buttonText = "";

        public string ButtonText
        {
            get => buttonText;
            protected set
            {
                buttonText = value;
                ModuleListPanel.UpdateModuleButton(this, buttonText);
            }
        }

        string Description
        {
            get
            {
                if (string.IsNullOrEmpty(Topic))
                {
                    return $"<b>{ModuleType}</b>";
                }

                string type = string.IsNullOrEmpty(Type) ? ModuleType.ToString() : Type;
                int lastSlash = type.LastIndexOf('/');
                string shortType = (lastSlash == -1) ? type : type.Substring(lastSlash + 1);
                return $"{Topic}\n<b>{shortType}</b>";
            }
        }

        public string Topic { get; }
        protected string Type { get; }
        public abstract ModuleType ModuleType { get; }
        public abstract DataPanelContents Panel { get; }
        public abstract IController Controller { get; }
        public abstract IConfiguration Configuration { get; }

        protected bool IsSelected => DataPanelManager.SelectedModuleData == this;

        protected ModuleData(string topic, string type)
        {
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        protected virtual void UpdateModuleButton()
        {
            string text = Topic.Length != 0 && Type.Length != 0
                ? GetDescriptionForTopic(Topic, Type)
                : $"<b>{ModuleType}</b>";

            ButtonText = Controller.Visible ? text : $"<color=grey>{text}</color>";
        }

        protected static string GetDescriptionForTopic(string topic, string type)
        {
            string topicShort = Resource.Font.Split(topic, ModuleListPanel.ModuleDataCaptionWidth);
            int lastSlash = type.LastIndexOf('/');
            string shortType = (lastSlash == -1) ? type : type[(lastSlash + 1)..];
            string clampedType = Resource.Font.Split(shortType, ModuleListPanel.ModuleDataCaptionWidth);
            return $"{topicShort}\n<b>{clampedType}</b>";
        }

        public void ToggleVisible()
        {
            Controller.Visible = !Controller.Visible;
            if (Panel.Active && Panel.HideButton != null)
            {
                Panel.HideButton.State = Controller.Visible;
            }

            UpdateModuleButton();
        }

        public virtual void Close()
        {
            DataPanelManager.HideSelectedPanel();
            ModuleListPanel.RemoveModule(this);
        }

        public abstract void SetupPanel();

        public void ResetPanel()
        {
            if (!IsSelected)
            {
                return;
            }

            Panel.ClearSubscribers();
            SetupPanel();
        }

        /*
        public virtual void CleanupPanel()
        {
        }
        */

        public virtual void UpdatePanel()
        {
        }

        public void ToggleShowPanel()
        {
            DataPanelManager.TogglePanel(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        public void ShowPanel()
        {
            DataPanelManager.SelectPanelFor(this);
            ModuleListPanel.AllGuiVisible = true;
        }

        public abstract void UpdateConfiguration(string configAsJson, IEnumerable<string> fields);

        public abstract void AddToState(StateConfiguration config);

        public virtual void Dispose()
        {
            DataPanelManager.HidePanelFor(this);
        }

        public void ResetController()
        {
            Controller.ResetController();
        }

        public override string ToString()
        {
            return $"[{ModuleType} guid={Configuration.Id}]";
        }

        public static ModuleData CreateFromResource(ModuleDataConstructor c)
        {
            if (c is null)
            {
                throw new ArgumentNullException(nameof(c));
            }

            return c.ModuleType switch
            {
                ModuleType.TF => new TfModuleData(c),
                ModuleType.PointCloud => new PointCloudModuleData(c),
                ModuleType.Grid => new GridModuleData(c),
                ModuleType.Image => new ImageModuleData(c),
                ModuleType.Robot => new SimpleRobotModuleData(c),
                ModuleType.Marker => new MarkerModuleData(c),
                ModuleType.InteractiveMarker => new InteractiveMarkerModuleData(c),
                ModuleType.JointState => new JointStateModuleData(c),
                ModuleType.DepthCloud => new DepthCloudModuleData(c),
                ModuleType.LaserScan => new LaserScanModuleData(c),
                ModuleType.AugmentedReality => new ARModuleData(c),
                ModuleType.Magnitude => new MagnitudeModuleData(c),
                ModuleType.OccupancyGrid => new OccupancyGridModuleData(c),
                ModuleType.Joystick => new JoystickModuleData(c),
                ModuleType.Path => new PathModuleData(c),
                ModuleType.GridMap => new GridMapModuleData(c),
                ModuleType.Octomap => new OctomapModuleData(c),
                ModuleType.GuiDialog => new GuiDialogModuleData(c),
                ModuleType.XR => new XRModuleData(c),
                _ => throw new ArgumentException("Failed to find a module of the given type: " + c.ModuleType)
            };
        }
    }
}