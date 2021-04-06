using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public sealed class DataPanelManager : MonoBehaviour
    {
        readonly Dictionary<Resource.ModuleType, DataPanelContents> panelByResourceType =
            new Dictionary<Resource.ModuleType, DataPanelContents>();

        DataPanelContents defaultPanel;

        public ModuleData SelectedModuleData { get; private set; }
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

        [NotNull] public T GetPanelByResourceType<T>(Resource.ModuleType resource) where T : DataPanelContents
        {
            if (panelByResourceType.TryGetValue(resource, out DataPanelContents existingContents))
            {
                if (!(existingContents is T tContents))
                {
                    throw new InvalidOperationException("Panel type does not match!");
                }
                
                return tContents;
            }

            DataPanelContents newContents = DataPanelContents.AddTo(CreatePanelObject(resource + " Panel"), resource);
            if (newContents == null)
            {
                throw new InvalidOperationException("There is no panel for this type");
            }
            
            if (!(newContents is T contents))
            {
                throw new InvalidOperationException("Panel type does not match!");
            }

            panelByResourceType[resource] = contents;
            return contents;
        }

        public void SelectPanelFor([CanBeNull] ModuleData newSelected)
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

        public void HidePanelFor([CanBeNull] ModuleData newSelected)
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
            //SelectedModuleData.CleanupPanel();
            SelectedModuleData.Panel.ClearSubscribers();
            SelectedModuleData = null;
            Active = false;
        }

        public void TogglePanel([CanBeNull] ModuleData selected)
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

        [NotNull]
        GameObject CreatePanelObject([NotNull] string panelName)
        {
            GameObject o = Resource.Widgets.DataPanel.Instantiate(transform);
            o.name = panelName;
            return o;
        }

        void UpdateSelected()
        {
            if (SelectedModuleData?.Panel is null)
            {
                return;
            }
            if (!SelectedModuleData.Panel.isActiveAndEnabled)
            {
                return;
            }
            SelectedModuleData.UpdatePanel();
        }
    }
}