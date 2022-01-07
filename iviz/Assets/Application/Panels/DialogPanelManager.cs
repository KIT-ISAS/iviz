#nullable enable

using System;
using UnityEngine;
using System.Collections.Generic;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine.UI;

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
        readonly HashSet<DialogData> detachedDialogDatas = new();

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

            this.ProcessCanvasForXR();

            Active = true;
            gameObject.SetActive(true);
            started = true;

            GameThread.EverySecond += UpdateAll;
            GameThread.EveryTenthSecond += UpdateAllFast;
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
            GameThread.EverySecond -= UpdateAll;
            GameThread.EveryTenthSecond -= UpdateAllFast;
        }

        void UpdateAll()
        {
            if (selectedDialogData != null)
            {
                UpdateDialogData(selectedDialogData);
            }

            foreach (var dialogData in detachedDialogDatas)
            {
                UpdateDialogData(dialogData);
            }
        }

        void UpdateDialogData(DialogData dialogData)
        {
            try
            {
                dialogData.UpdatePanel();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during UpdatePanel:", e);
            }            
        }

        void UpdateAllFast()
        {
            if (selectedDialogData != null)
            {
                UpdateDialogDataFast(selectedDialogData);
            }

            foreach (var dialogData in detachedDialogDatas)
            {
                UpdateDialogDataFast(dialogData);
            }
        }
        
        void UpdateDialogDataFast(DialogData dialogData)
        {
            try
            {
                dialogData.UpdatePanelFast();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception during UpdatePanelFast:", e);
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
                detachedDialogDatas.Remove(deselected);
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
            
            detachedDialogDatas.Add(selectedDialogData);
            selectedDialogData.Detached = true;
            selectedDialogData = null;
        }

        public bool IsActive(DialogData dialogData) => selectedDialogData == dialogData || dialogData.Detached;

        public override string ToString() => "[DialogPanelManager]";
    }
}