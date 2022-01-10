using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class SettingsDialogPanel : DialogPanel
    {
        [SerializeField] TrashButtonWidget close;

        [SerializeField] DropdownWidget qualityInView;
        [SerializeField] DropdownWidget qualityInAr;
        [SerializeField] DropdownWidget targetFps;
        [SerializeField] DropdownWidget networkProcessing;

        [SerializeField] ColorPickerWidget backgroundColor;
        [SerializeField] SliderWidget sunDirection;

        [SerializeField] Button clearModelCache;
        [SerializeField] Button clearSavedFiles;
        [SerializeField] Button clearHostHistory;
        [SerializeField] DropdownWidget modelService;

        [SerializeField] Text modelCacheLabel;
        [SerializeField] Text savedFilesLabel;
        [SerializeField] Text hostHistoryLabel;


        public TrashButtonWidget Close => close;
        public DropdownWidget QualityInView => qualityInView;
        public DropdownWidget QualityInAr => qualityInAr;
        public DropdownWidget TargetFps => targetFps;
        public DropdownWidget NetworkProcessing => networkProcessing;
        public ColorPickerWidget BackgroundColor => backgroundColor;
        public SliderWidget SunDirection => sunDirection;
        public DropdownWidget ModelService => modelService;
        public Text ModelCacheLabel => modelCacheLabel;
        public Text SavedFilesLabel => savedFilesLabel;
        public Text HostHistoryLabel => hostHistoryLabel;

        public event Action ClearModelCacheClicked;
        public event Action ClearSavedFilesClicked;
        public event Action ClearHostHistoryClicked;
        
        void Awake()
        {
            clearModelCache.onClick.AddListener(() => ClearModelCacheClicked?.Invoke());
            clearSavedFiles.onClick.AddListener(() => ClearSavedFilesClicked?.Invoke());
            clearHostHistory.onClick.AddListener(() => ClearHostHistoryClicked?.Invoke());

            if (Settings.IsMobile)
            {
                modelService.Visible = false;
            }
        }

        public override void ClearSubscribers()
        {
            qualityInView.ClearSubscribers();
            qualityInAr.ClearSubscribers();
            targetFps.ClearSubscribers();
            networkProcessing.ClearSubscribers();
            backgroundColor.ClearSubscribers();
            close.ClearSubscribers();
            sunDirection.ClearSubscribers();
            modelService.ClearSubscribers();

            ClearHostHistoryClicked = null;
            ClearModelCacheClicked = null;
            ClearSavedFilesClicked = null;
        }
    }
}