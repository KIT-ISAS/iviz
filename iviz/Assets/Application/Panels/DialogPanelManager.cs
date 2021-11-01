using System;
using UnityEngine;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public enum DialogPanelType
    {
        AddModule,
        Connection,
        AddTopic,
        Image,
        Tf,
        Load,
        SaveAs,
        Marker,
        Network,
        Console,
        Settings,
        Echo,
        System,
        ARMarkers,
    }

    public class DialogPanelManager : MonoBehaviour
    {
        readonly Dictionary<DialogPanelType, IDialogPanelContents> panelByType =
            new Dictionary<DialogPanelType, IDialogPanelContents>();

        [CanBeNull] DialogData selectedDialogData;
        Canvas parentCanvas;
        bool started;

        public bool Active
        {
            get => parentCanvas.enabled;
            set
            {
                //parentCanvas.enabled = value;
                if (selectedDialogData != null)
                {
                    selectedDialogData.Panel.Active = value;
                } 
            }
        }

        void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();

            gameObject.SetActive(false);
            (DialogPanelType type, IDialogPanelContents panel)[] panels =
            {
                (DialogPanelType.AddModule, CreatePanel<ItemListDialogContents>(Resource.Widgets.ItemListPanel)),
                (DialogPanelType.Connection, CreatePanel<ConnectionDialogContents>(Resource.Widgets.ConnectionPanel)),
                //(DialogPanelType.Image, CreatePanel<ImageDialogContents>(Resource.Widgets.ImagePanel)),
                (DialogPanelType.Tf, CreatePanel<TfDialogContents>(Resource.Widgets.TfPanel)),
                (DialogPanelType.SaveAs, CreatePanel<SaveConfigDialogContents>(Resource.Widgets.SaveAsPanel)),
                (DialogPanelType.Load, CreatePanel<ItemListDialogContents>(Resource.Widgets.ItemListPanel)),
                (DialogPanelType.AddTopic, CreatePanel<AddTopicDialogContents>(Resource.Widgets.AddTopicPanel)),
                (DialogPanelType.Marker, CreatePanel<MarkerDialogContents>(Resource.Widgets.MarkerPanel)),
                (DialogPanelType.Network, CreatePanel<NetworkDialogContents>(Resource.Widgets.NetworkPanel)),
                (DialogPanelType.Console, CreatePanel<ConsoleDialogContents>(Resource.Widgets.ConsolePanel)),
                (DialogPanelType.Settings, CreatePanel<SettingsDialogContents>(Resource.Widgets.SettingsPanel)),
                (DialogPanelType.Echo, CreatePanel<EchoDialogContents>(Resource.Widgets.EchoPanel)),
                (DialogPanelType.System, CreatePanel<SystemDialogContents>(Resource.Widgets.SystemPanel)),
                (DialogPanelType.ARMarkers, CreatePanel<ARMarkerDialogContents>(Resource.Widgets.ARMarkerPanel)),
            };

            foreach (var (type, panel) in panels)
            {
                panelByType.Add(type, panel);
                panel.Active = false;
            }

            Active = true;
            gameObject.SetActive(true);
            started = true;

            GameThread.EverySecond += UpdateSelected;
            GameThread.EveryFastTick += UpdateSelectedFast;
        }

        [NotNull]
        T CreatePanel<T>([NotNull] Info<GameObject> source) where T : IDialogPanelContents
        {
            if (source == null)
            {
                throw new NullReferenceException("Requested a panel from source null!");
            }

            var panel = source.Instantiate<T>(transform);
            if (panel == null)
            {
                throw new NullReferenceException($"Panel '{source}' does not have a module of type {typeof(T)}");
            }

            return panel;
        }


        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateSelected;
            GameThread.EveryFastTick -= UpdateSelectedFast;
        }

        void UpdateSelected()
        {
            try
            {
                selectedDialogData?.UpdatePanel();
            }
            catch (Exception e)
            {
                Core.Logger.Error($"{this}: Exception during UpdatePanel: " + e);
            }
        }

        void UpdateSelectedFast()
        {
            try
            {
                selectedDialogData?.UpdatePanelFast();
            }
            catch (Exception e)
            {
                Core.Logger.Error($"{this}: Exception during UpdatePanel: " + e);
            }
        }

        [NotNull]
        public T GetPanelByType<T>(DialogPanelType resource) where T : IDialogPanelContents
        {
            if (!panelByType.TryGetValue(resource, out IDialogPanelContents cm))
            {
                throw new InvalidOperationException("There is no panel for this type!");
            }

            return cm is T contents
                ? contents
                : throw new InvalidOperationException("Panel type does not match!");
        }

        void SelectPanelFor([CanBeNull] DialogData newSelected)
        {
            if (!started)
            {
                return;
            }

            if (newSelected == selectedDialogData)
            {
                return;
            }

            HideSelectedPanel();
            if (newSelected != null && !newSelected.Detached)
            {
                ShowPanel(newSelected);
            }
        }

        void ShowPanel([NotNull] DialogData newSelected)
        {
            selectedDialogData = newSelected;
            try
            {
                selectedDialogData.SetupPanel();
            }
            catch (Exception e)
            {
                Core.Logger.Error($"{this}: Exception during SetupPanel: " + e);
            }

            selectedDialogData.Panel.Active = true;
            //Active = true;
        }

        void HideSelectedPanel()
        {
            if (selectedDialogData != null)
            {
                HidePanel(selectedDialogData);
                selectedDialogData = null;
            }
        }

        static void HidePanel([NotNull] DialogData dialogData)
        {
            dialogData.Panel.Active = false;
            dialogData.CleanupPanel();
            dialogData.Panel.ClearSubscribers();
            //Active = false;
        }


        public void HidePanelFor([CanBeNull] DialogData deselected)
        {
            if (deselected == null)
            {
                return;
            }
            
            if (deselected.Detached)
            {
                HidePanel(deselected);
                deselected.Detached = false;
            }

            if (selectedDialogData == deselected)
            {
                HideSelectedPanel();
            }
        }

        public void TogglePanel([CanBeNull] DialogData selected)
        {
            if (selectedDialogData == selected)
            {
                HideSelectedPanel();
            }
            else
            {
                SelectPanelFor(selected);
            }
        }

        public void DetachSelectedPanel()
        {
            if (selectedDialogData == null)
            {
                return;
            }
            
            selectedDialogData.Detached = true;
            selectedDialogData = null;
        } 

        [NotNull]
        public override string ToString()
        {
            return "[DialogPanelManager]";
        }
    }
}