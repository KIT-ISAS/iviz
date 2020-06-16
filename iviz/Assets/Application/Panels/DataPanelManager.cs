using System;
using System.Collections.Generic;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public class DataPanelManager : MonoBehaviour
    {
        readonly Dictionary<Resource.Module, DataPanelContents> PanelByResourceType = new Dictionary<Resource.Module, DataPanelContents>();
        DataPanelContents DefaultPanel;

        ModuleData SelectedDisplayData;
        Canvas parentCanvas;
        bool started;

        public AnchorCanvas AnchorCanvas;

        public bool Active
        {
            get => parentCanvas.enabled;
            set
            {
                parentCanvas.enabled = value;
            }
        }

        void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();

            gameObject.SetActive(false);
            DefaultPanel = CreatePanelObject("Default Panel").AddComponent<DefaultPanelContents>();
            Active = false;
            gameObject.SetActive(true);
            started = true;

            GameThread.EverySecond += UpdateSelected;
        }

        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateSelected;
        }

        public DataPanelContents GetPanelByResourceType(Resource.Module resource)
        {
            if (PanelByResourceType.TryGetValue(resource, out DataPanelContents cm))
            {
                return cm;
            }
            cm = DataPanelContents.AddTo(CreatePanelObject(resource + " Panel"), resource);
            if (cm != null)
            {
                PanelByResourceType[resource] = cm;
                return cm;
            }
            return DefaultPanel;
        }

        public void SelectPanelFor(ModuleData newSelected)
        {
            if (!started)
            {
                return;
            }
            if (newSelected == SelectedDisplayData)
            {
                return;
            }
            HideSelectedPanel();
            if (newSelected == null || newSelected.Panel == null)
            {
                ShowDefaultPanel();
            }
            else
            {
                ShowPanel(newSelected);
            }
        }

        void ShowPanel(ModuleData newSelected)
        {
            SelectedDisplayData = newSelected;
            SelectedDisplayData.SetupPanel();
            SelectedDisplayData.Panel.Active = true;
            Active = true;
        }

        public void HidePanelFor(ModuleData newSelected)
        {
            if (SelectedDisplayData == newSelected)
            {
                HideSelectedPanel();
            }
        }

        public void ShowDefaultPanel()
        {
            SelectedDisplayData = null;
            DefaultPanel.Active = true;
        }

        public void HideSelectedPanel()
        {
            if (SelectedDisplayData == null)
            {
                return;
            }

            SelectedDisplayData.Panel.Active = false;
            SelectedDisplayData.CleanupPanel();
            SelectedDisplayData.Panel.ClearSubscribers();
            SelectedDisplayData = null;
            Active = false;
        }

        public void TogglePanel(ModuleData selected)
        {
            if (SelectedDisplayData == selected)
            {
                HideSelectedPanel();
            }
            else
            {
                SelectPanelFor(selected);
            }
        }

        GameObject CreatePanelObject(string name)
        {
            GameObject o = Instantiate(UnityEngine.Resources.Load<GameObject>("Widgets/Data Panel"), transform);
            o.name = name;
            return o;
        }

        void UpdateSelected()
        {
            SelectedDisplayData?.UpdatePanel();
        }
    }
}