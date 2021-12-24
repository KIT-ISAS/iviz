#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public sealed class DataPanelManager : MonoBehaviour
    {
        readonly Dictionary<ModuleType, DataPanelContents> panelByResourceType = new();

        Canvas? parentCanvas;
        Canvas ParentCanvas => (parentCanvas != null) ? parentCanvas : GetComponentInParent<Canvas>();

        bool started;

        bool Active
        {
            set => ParentCanvas.enabled = value;
        }

        public ModuleData? SelectedModuleData { get; private set; }

        void Awake()
        {
            gameObject.SetActive(false);
            Active = false;
            gameObject.SetActive(true);
            started = true;
            GameThread.EverySecond += UpdateSelected;
            GameThread.EveryFastTick += UpdateSelectedFast;
        }

        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateSelected;
            GameThread.EveryFastTick -= UpdateSelectedFast;
        }

        public T GetPanelByResourceType<T>(ModuleType resource) where T : DataPanelContents
        {
            if (panelByResourceType.TryGetValue(resource, out DataPanelContents existingContents))
            {
                return existingContents is T validatedContents
                    ? validatedContents
                    : throw new InvalidOperationException("Panel type for " + resource + "does not match!");
            }

            var newContents = DataPanelContents.AddTo(CreatePanelObject(resource + " Panel"), resource);
            if (newContents == null)
            {
                throw new InvalidOperationException("There is no panel for type " + resource);
            }

            if (newContents is not T contents)
            {
                throw new InvalidOperationException("Panel type for " + resource + " does not match!");
            }

            panelByResourceType[resource] = contents;

            var rootCanvas = contents.GetComponentInParent<Canvas>();
            rootCanvas.ProcessCanvasForXR();

            return contents;
        }

        public void SelectPanelFor(ModuleData newSelected)
        {
            if (!started)
            {
                return;
            }

            if (newSelected.Panel is null)
            {
                throw new NullReferenceException("Invalid selected panel: null");
            }

            if (SelectedModuleData == newSelected)
            {
                return;
            }

            HideSelectedPanel();
            ShowPanel(newSelected);
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

        public void HideSelectedPanel()
        {
            if (SelectedModuleData == null)
            {
                return;
            }

            SelectedModuleData.Panel.Active = false;
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
            var o = Resource.Widgets.DataPanel.Instantiate(transform);
            o.name = panelName;
            return o;
        }

        void UpdateSelected()
        {
            if (SelectedModuleData?.Panel is { isActiveAndEnabled: true })
            {
                SelectedModuleData.UpdatePanel();
            }
        }
        
        void UpdateSelectedFast()
        {
            if (SelectedModuleData?.Panel is { isActiveAndEnabled: true })
            {
                SelectedModuleData.UpdatePanelFast();
            }
        }
    }
}