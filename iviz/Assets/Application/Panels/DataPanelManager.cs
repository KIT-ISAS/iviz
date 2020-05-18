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

        DisplayData SelectedDisplayData;
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
            foreach (Resource.Module resource in Enum.GetValues(typeof(Resource.Module)))
            {
                PanelByResourceType[resource] = DataPanelContents.AddTo(CreatePanelObject(resource + " Panel"), resource);
            }
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
            return PanelByResourceType.TryGetValue(resource, out DataPanelContents cm) ? cm : DefaultPanel;
        }

        public void SelectPanelFor(DisplayData newSelected)
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

        void ShowPanel(DisplayData newSelected)
        {
            SelectedDisplayData = newSelected;
            SelectedDisplayData.SetupPanel();
            SelectedDisplayData.Panel.Active = true;
            Active = true;
        }

        public void HidePanelFor(DisplayData newSelected)
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