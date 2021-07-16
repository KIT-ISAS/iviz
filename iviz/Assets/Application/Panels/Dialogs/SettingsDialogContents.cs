using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class SettingsDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;

        [SerializeField] DropdownWidget qualityInView = null;
        [SerializeField] DropdownWidget qualityInAr = null;
        [SerializeField] DropdownWidget targetFps = null;
        [SerializeField] DropdownWidget networkProcessing = null;

        [SerializeField] ColorPickerWidget backgroundColor = null;
        [SerializeField] SliderWidget sunDirection = null;

        [SerializeField] Button clearModelCache = null;
        [SerializeField] Button clearSavedFiles = null;
        [SerializeField] Button clearHostHistory = null;
        [SerializeField] DropdownWidget modelService = null;

        [SerializeField] Text modelCacheLabel = null;
        [SerializeField] Text savedFilesLabel = null;
        [SerializeField] Text hostHistoryLabel = null;


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

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
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