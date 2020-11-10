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
        Marker
    }

    public class DialogPanelManager : MonoBehaviour
    {
        readonly Dictionary<DialogPanelType, IDialogPanelContents> PanelByType =
            new Dictionary<DialogPanelType, IDialogPanelContents>();

        DialogData selectedDialogData;
        Canvas parentCanvas;
        bool started;

        public bool Active
        {
            get => parentCanvas.enabled;
            set => parentCanvas.enabled = value;
        }

        void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();

            gameObject.SetActive(false);
            PanelByType[DialogPanelType.ItemList] = Resource.Widgets.ItemListPanel.Instantiate(transform)
                .GetComponent<ItemListDialogContents>();
            PanelByType[DialogPanelType.Connection] = Resource.Widgets.ConnectionPanel.Instantiate(transform)
                .GetComponent<ConnectionDialogContents>();
            PanelByType[DialogPanelType.Image] =
                Resource.Widgets.ImagePanel.Instantiate(transform).GetComponent<ImageDialogContents>();
            PanelByType[DialogPanelType.Tf] =
                Resource.Widgets.TfPanel.Instantiate(transform).GetComponent<TFDialogContents>();
            PanelByType[DialogPanelType.SaveAs] = Resource.Widgets.SaveAsPanel.Instantiate(transform)
                .GetComponent<SaveConfigDialogContents>();
            PanelByType[DialogPanelType.AddTopic] = Resource.Widgets.AddTopicPanel.Instantiate(transform)
                .GetComponent<AddTopicDialogContents>();
            PanelByType[DialogPanelType.Marker] = Resource.Widgets.MarkerPanel.Instantiate(transform)
                .GetComponent<MarkerDialogContents>();

            PanelByType.Values.ForEach(x => x.Active = false);
            Active = false;
            gameObject.SetActive(true);
            started = true;

            GameThread.EverySecond += UpdateSelected;
        }

        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateSelected;
        }

        void UpdateSelected()
        {
            selectedDialogData?.UpdatePanel();
        }

        [NotNull]
        public T GetPanelByType<T>(DialogPanelType resource) where T : IDialogPanelContents
        {
            if (!PanelByType.TryGetValue(resource, out IDialogPanelContents cm))
            {
                throw new InvalidOperationException("There is no panel for this type!");
            }

            if (!(cm is T contents))
            {
                throw new InvalidOperationException("Panel type does not match!");
            }

            return contents;
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