using System;
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

        [SerializeField] Button clearModelCache = null;
        [SerializeField] Button clearSavedFiles = null;
        [SerializeField] Button clearHostHistory = null;

        public TrashButtonWidget Close => close;
        public DropdownWidget QualityInView => qualityInView;
        public DropdownWidget QualityInAr => qualityInAr;
        public DropdownWidget TargetFps => targetFps;
        public DropdownWidget NetworkProcessing => networkProcessing;
        public ColorPickerWidget BackgroundColor => backgroundColor;

        public event Action ClearModelCacheClicked; 
        public event Action ClearSavedFilesClicked; 
        public event Action ClearHostHistoryClicked;

        void Awake()
        {
            clearModelCache.onClick.AddListener(() => ClearModelCacheClicked?.Invoke());
            clearSavedFiles.onClick.AddListener(() => ClearSavedFilesClicked?.Invoke());
            clearHostHistory.onClick.AddListener(() => ClearHostHistoryClicked?.Invoke());
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

            ClearHostHistoryClicked = null;
            ClearModelCacheClicked = null;
            ClearSavedFilesClicked = null;
        }        
    }
}