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
    public sealed class ModulePanelManager : MonoBehaviour
    {
        readonly Dictionary<ModuleType, ModulePanel> panelByResourceType = new();

        bool started;
        Canvas? parentCanvas;

        Canvas ParentCanvas => (parentCanvas != null) ? parentCanvas : (parentCanvas = GetComponentInParent<Canvas>());

        bool Active
        {
            set => ParentCanvas.enabled = value;
        }

        public IManagesPanel? SelectedModuleData { get; private set; }

        void Awake()
        {
            gameObject.SetActive(false);
            Active = false;
            gameObject.SetActive(true);
            started = true;
            GameThread.EverySecond += UpdateSelected;
            GameThread.EveryTenthSecond += UpdateSelectedFast;
        }

        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateSelected;
            GameThread.EveryTenthSecond -= UpdateSelectedFast;
        }

        public T GetPanelByResourceType<T>(ModuleType resource) where T : ModulePanel
        {
            if (panelByResourceType.TryGetValue(resource, out ModulePanel existingContents))
            {
                return existingContents is T validatedContents
                    ? validatedContents
                    : throw new InvalidOperationException($"Panel type for {resource}does not match!");
            }

            var newContents = ModulePanel.AddTo(CreatePanelObject($"{resource} Panel"), resource);
            if (newContents == null)
            {
                throw new InvalidOperationException($"There is no panel for type {resource}");
            }

            if (newContents is not T contents)
            {
                throw new InvalidOperationException($"Panel type for {resource} does not match!");
            }

            panelByResourceType[resource] = contents;

            var rootCanvas = contents.GetComponentInParent<Canvas>();
            rootCanvas.ProcessCanvasForXR();

            return contents;
        }

        public void SelectPanelFor(IManagesPanel newSelected)
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

        void ShowPanel(IManagesPanel newSelected)
        {
            SelectedModuleData = newSelected;
            SelectedModuleData.SetupPanel();
            SelectedModuleData.Panel.Active = true;
            Active = true;
        }

        public void HidePanelFor(IManagesPanel moduleData)
        {
            if (SelectedModuleData == moduleData)
            {
                HideSelectedPanel();
            }
        }

        void HideSelectedPanel()
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

        public void TogglePanel(IManagesPanel moduleData)
        {
            if (SelectedModuleData == moduleData)
            {
                HideSelectedPanel();
            }
            else
            {
                SelectPanelFor(moduleData);
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