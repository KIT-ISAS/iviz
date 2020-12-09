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
        ItemList,
        Connection,
        AddTopic,
        Image,
        Tf,
        SaveAs,
        Marker,
        Network,
        Console,
    }

    public class DialogPanelManager : MonoBehaviour
    {
        readonly Dictionary<DialogPanelType, IDialogPanelContents> PanelByType =
            new Dictionary<DialogPanelType, IDialogPanelContents>();

        DialogData selectedDialogData;
        Canvas parentCanvas;
        bool started;

        bool Active
        {
            get => parentCanvas.enabled;
            set => parentCanvas.enabled = value;
        }

        void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();

            gameObject.SetActive(false);
            (DialogPanelType type, IDialogPanelContents panel)[] list =
            {
                (DialogPanelType.ItemList, CreatePanel<ItemListDialogContents>(Resource.Widgets.ItemListPanel)),
                (DialogPanelType.Connection, CreatePanel<ConnectionDialogContents>(Resource.Widgets.ConnectionPanel)),
                (DialogPanelType.Image, CreatePanel<ImageDialogContents>(Resource.Widgets.ImagePanel)),
                (DialogPanelType.Tf, CreatePanel<TFDialogContents>(Resource.Widgets.TfPanel)),
                (DialogPanelType.SaveAs, CreatePanel<SaveConfigDialogContents>(Resource.Widgets.SaveAsPanel)),
                (DialogPanelType.AddTopic, CreatePanel<AddTopicDialogContents>(Resource.Widgets.AddTopicPanel)),
                (DialogPanelType.Marker, CreatePanel<MarkerDialogContents>(Resource.Widgets.MarkerPanel)),
                (DialogPanelType.Network, CreatePanel<NetworkDialogContents>(Resource.Widgets.NetworkPanel)),
                (DialogPanelType.Console, CreatePanel<ConsoleDialogContents>(Resource.Widgets.ConsolePanel)),
            };

            foreach (var (type, panel) in list)
            {
                PanelByType.Add(type, panel);
                panel.Active = false;
            }

            Active = false;
            gameObject.SetActive(true);
            started = true;

            GameThread.EverySecond += UpdateSelected;
        }

        T CreatePanel<T>(Info<GameObject> source) where T : IDialogPanelContents
        {
            return source.Instantiate(transform).GetComponent<T>();
        }


        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateSelected;
        }

        int round = 0;

        void UpdateSelected()
        {
            if (Settings.IsHololens)
            {
                round++;
                if (round % 5 != 0)
                {
                    return;
                }
            }

            selectedDialogData?.UpdatePanel();
        }

        [NotNull]
        public T GetPanelByType<T>(DialogPanelType resource) where T : IDialogPanelContents
        {
            if (!PanelByType.TryGetValue(resource, out IDialogPanelContents cm))
            {
                throw new InvalidOperationException("There is no panel for this type!");
            }

            return cm is T contents ? contents : throw new InvalidOperationException("Panel type does not match!");
        }

        void SelectPanelFor(DialogData newSelected)
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
            if (newSelected != null)
            {
                ShowPanel(newSelected);
            }
        }

        void ShowPanel(DialogData newSelected)
        {
            selectedDialogData = newSelected;
            selectedDialogData.SetupPanel();
            selectedDialogData.Panel.Active = true;
            Active = true;
        }

        void HideSelectedPanel()
        {
            if (selectedDialogData == null)
            {
                return;
            }

            selectedDialogData.Panel.Active = false;
            selectedDialogData.CleanupPanel();
            selectedDialogData.Panel.ClearSubscribers();
            selectedDialogData = null;
            Active = false;
        }


        public void HidePanelFor([CanBeNull] DialogData deselected)
        {
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
    }
}