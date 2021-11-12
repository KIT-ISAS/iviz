#nullable enable

using System;
using UnityEngine;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;

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
        readonly Dictionary<DialogPanelType, IDialogPanelContents> panelByType = new();

        DialogData? selectedDialogData;
        bool started;

        public bool Active
        {
            set
            {
                if (selectedDialogData != null)
                {
                    selectedDialogData.Panel.Active = value;
                } 
            }
        }

        void Awake()
        {
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

        T CreatePanel<T>(Info<GameObject> source) where T : IDialogPanelContents
        {
            if (source == null)
            {
                throw new NullReferenceException("Requested a panel from source null!");
            }

            return source.Instantiate<T>(transform);
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
                RosLogger.Error($"{this}: Exception during UpdatePanel:", e);
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
                Core.RosLogger.Error($"{this}: Exception during UpdatePanel:", e);
            }
        }

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

        void SelectPanelFor(DialogData? newSelected)
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
            if (newSelected is {Detached: false})
            {
                ShowPanel(newSelected);
            }
        }

        void ShowPanel(DialogData newSelected)
        {
            selectedDialogData = newSelected;
            try
            {
                selectedDialogData.SetupPanel();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during SetupPanel: ", e);
            }

            selectedDialogData.Panel.Active = true;
        }

        void HideSelectedPanel()
        {
            if (selectedDialogData == null)
            {
                return;
            }
            
            HidePanel(selectedDialogData);
            selectedDialogData = null;
        }

        static void HidePanel(DialogData dialogData)
        {
            dialogData.Panel.Active = false;
            dialogData.CleanupPanel();
            dialogData.Panel.ClearSubscribers();
        }


        public void HidePanelFor(DialogData? deselected)
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

        public void TogglePanel(DialogData? selected)
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

        public override string ToString() => "[DialogPanelManager]";
    }
}