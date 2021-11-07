using System;
using System.Collections.Generic;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public abstract class ModuleData : IModuleData
    {
        [NotNull] protected static ModuleListPanel ModuleListPanel => ModuleListPanel.Instance;
        protected static DataPanelManager DataPanelManager => ModuleListPanel.DataPanelManager;

        string buttonText = "";

        [NotNull]
        public string ButtonText
        {
            get => buttonText;
            protected set
            {
                buttonText = value;
                ModuleListPanel.UpdateModuleButton(this, buttonText);
            }
        }

        [NotNull]
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

        [NotNull] public string Topic { get; }
        [NotNull] protected string Type { get; }
        public abstract ModuleType ModuleType { get; }
        [NotNull] public abstract DataPanelContents Panel { get; }
        [NotNull] public abstract IController Controller { get; }
        [NotNull] public abstract IConfiguration Configuration { get; }

        protected bool IsSelected => DataPanelManager.SelectedModuleData == this;
        
        protected ModuleData([NotNull] string topic, [NotNull] string type)
        {
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        protected virtual void UpdateModuleButton()
        {
            string text;
            if (!string.IsNullOrEmpty(Topic))
            {
                string topicShort = Resource.Font.Split(Topic, ModuleListPanel.ModuleDataCaptionWidth);

                string type = string.IsNullOrEmpty(Type) ? ModuleType.ToString() : Type;
                int lastSlash = type.LastIndexOf('/');
                string shortType = (lastSlash == -1) ? type : type.Substring(lastSlash + 1);
                string clampedType = Resource.Font.Split(shortType, ModuleListPanel.ModuleDataCaptionWidth);
                text = $"{topicShort}\n<b>{clampedType}</b>";
            }
            else
            {
                text = $"<b>{ModuleType}</b>";
            }

            ButtonText = Controller.Visible ? text : $"<color=grey>{text}</color>";
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

        public abstract void AddToState([NotNull] StateConfiguration config);

        public virtual void Stop()
        {
            DataPanelManager.HidePanelFor(this);
        }

        public void ResetController()
        {
            Controller.ResetController();
        }

        [NotNull]
        public override string ToString()
        {
            return $"[{ModuleType} guid={Configuration.Id}]";
        }

        [NotNull]
        public static ModuleData CreateFromResource([NotNull] ModuleDataConstructor c)
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
                _ => throw new ArgumentException("Failed to find a module of the given type: " + c.ModuleType)
            };
        }
    }
}