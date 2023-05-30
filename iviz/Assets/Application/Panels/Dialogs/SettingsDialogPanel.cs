#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SettingsDialogPanel : DialogPanel
    {
        [SerializeField] SimpleButtonWidget? close;

        [SerializeField] DropdownWidget? qualityInView;
        [SerializeField] DropdownWidget? qualityInAr;
        [SerializeField] DropdownWidget? targetFps;
        [SerializeField] DropdownWidget? networkProcessing;

        [SerializeField] Button? clearModelCache;
        [SerializeField] Button? clearSavedFiles;
        [SerializeField] Button? clearHostHistory;

        [SerializeField] TMP_Text? modelCacheLabel;
        [SerializeField] TMP_Text? savedFilesLabel;
        [SerializeField] TMP_Text? hostHistoryLabel;


        Button ClearModelCache => clearModelCache.AssertNotNull(nameof(clearModelCache));
        Button ClearSavedFiles => clearSavedFiles.AssertNotNull(nameof(clearSavedFiles));
        Button ClearHostHistory => clearHostHistory.AssertNotNull(nameof(clearHostHistory));
        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public DropdownWidget QualityInView => qualityInView.AssertNotNull(nameof(qualityInView));
        public DropdownWidget QualityInAr => qualityInAr.AssertNotNull(nameof(qualityInAr));
        public DropdownWidget TargetFps => targetFps.AssertNotNull(nameof(targetFps));

        public DropdownWidget NetworkProcessing => networkProcessing.AssertNotNull(nameof(networkProcessing));

        //public DropdownWidget ModelService => modelService.AssertNotNull(nameof(modelService));
        public TMP_Text ModelCacheLabel => modelCacheLabel.AssertNotNull(nameof(modelCacheLabel));
        public TMP_Text SavedFilesLabel => savedFilesLabel.AssertNotNull(nameof(savedFilesLabel));
        public TMP_Text HostHistoryLabel => hostHistoryLabel.AssertNotNull(nameof(hostHistoryLabel));

        public event Action? ClearModelCacheClicked;
        public event Action? ClearSavedFilesClicked;
        public event Action? ClearHostHistoryClicked;

        void Awake()
        {
            ClearModelCache.onClick.AddListener(() => ClearModelCacheClicked.TryRaise(this));
            ClearSavedFiles.onClick.AddListener(() => ClearSavedFilesClicked.TryRaise(this));
            ClearHostHistory.onClick.AddListener(() => ClearHostHistoryClicked.TryRaise(this));
        }

        public override void ClearSubscribers()
        {
            QualityInView.ClearSubscribers();
            QualityInAr.ClearSubscribers();
            TargetFps.ClearSubscribers();
            NetworkProcessing.ClearSubscribers();
            Close.ClearSubscribers();

            ClearHostHistoryClicked = null;
            ClearModelCacheClicked = null;
            ClearSavedFilesClicked = null;
        }
    }
}