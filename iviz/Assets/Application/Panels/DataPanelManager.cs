using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public sealed class DataPanelManager : MonoBehaviour
    {
        readonly Dictionary<Resource.Module, DataPanelContents> panelByResourceType =
            new Dictionary<Resource.Module, DataPanelContents>();

        DataPanelContents defaultPanel;

        public ModuleData SelectedModuleData { get; private set; }
        Canvas parentCanvas;
        bool started;

        //public AnchorCanvas AnchorCanvas;

        bool Active
        {
            get => parentCanvas.enabled;
            set => parentCanvas.enabled = value;
        }

        void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();

            gameObject.SetActive(false);
            defaultPanel = CreatePanelObject("Default Panel").AddComponent<DefaultPanelContents>();
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
            if (panelByResourceType.TryGetValue(resource, out DataPanelContents cm))
            {
                return cm;
            }

            cm = DataPanelContents.AddTo(CreatePanelObject(resource + " Panel"), resource);
            if (cm is null)
            {
                return defaultPanel;
            }

            panelByResourceType[resource] = cm;
            return cm;
        }

        public void SelectPanelFor(ModuleData newSelected)
        {
            if (!started)
            {
                return;
            }

            if (newSelected == SelectedModuleData)
            {
                return;
            }

            HideSelectedPanel();
            if (newSelected?.Panel is null)
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
            SelectedModuleData = newSelected;
            SelectedModuleData.SetupPanel();
            SelectedModuleData.Panel.Active = true;
            Active = true;
        }

        public void HidePanelFor(ModuleData newSelected)
        {
            if (SelectedModuleData == newSelected)
            {
                HideSelectedPanel();
            }
        }

        void ShowDefaultPanel()
        {
            SelectedModuleData = null;
            defaultPanel.Active = true;
        }

        public void HideSelectedPanel()
        {
            if (SelectedModuleData == null)
            {
                return;
            }

            SelectedModuleData.Panel.Active = false;
            SelectedModuleData.CleanupPanel();
            SelectedModuleData.Panel.ClearSubscribers();
            SelectedModuleData = null;
            Active = false;
        }

        public void TogglePanel(ModuleData selected)
        {
            if (SelectedModuleData == selected)
            {
                HideSelectedPanel();
            }
            else
            {
                SelectPanelFor(selected);
            }
        }

        GameObject CreatePanelObject(string panelName)
        {
            GameObject o = Instantiate(UnityEngine.Resources.Load<GameObject>("Widgets/Data Panel"), transform);
            o.name = panelName;
            return o;
        }

        void UpdateSelected()
        {
            SelectedModuleData?.UpdatePanel();
        }
    }
}