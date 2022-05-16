#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Controllers.XR;
using Iviz.Core;
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
            GameThread.EveryTenthOfASecond += UpdateSelectedFast;
        }

        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateSelected;
            GameThread.EveryTenthOfASecond -= UpdateSelectedFast;
        }

        public T GetPanelByResourceType<T>(ModuleType resource) where T : ModulePanel
        {
            if (panelByResourceType.TryGetValue(resource, out ModulePanel existingContents))
            {
                return existingContents as T ??
                       throw new InvalidOperationException($"Panel type for {resource}does not match!");
            }

            var newDataPanel = CreateDataPanel($"{resource} Panel", transform);
            var newContents = ModulePanel.AddResourceToDataPanel(newDataPanel, resource);

            if (newContents is not T contents)
            {
                ThrowHelper.ThrowMissingAssetField($"Panel type for {resource} does not match!");
                return default; // unreachable
            }

            panelByResourceType[resource] = contents;

            var rootCanvas = contents.GetComponentInParent<Canvas>(true);
            if (rootCanvas == null)
            {
                 ThrowHelper.ThrowMissingAssetField(
                    $"Error creating canvas for {resource}. Reason: No canvas component");
            }

            rootCanvas.ProcessCanvasForXR();

            return contents;
        }

        public void SelectPanelFor(IManagesPanel newSelected)
        {
            if (!started)
            {
                return;
            }

            if (newSelected.Panel == null)
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

        static GameObject CreateDataPanel(string panelName, Transform transform)
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