using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class MarkerMenuDialogContents : ItemListDialogContents, IMenuDialogContents
    {
        const int MaxPanelSizeInEntries = 7;
        
        MenuEntryList menuEntryList;
        MenuEntryDescription[] currentEntries;

        MenuEntryDescription[] CurrentEntries
        {
            set
            {
                currentEntries = value;
                Items = currentEntries.Select(DescriptionToString);
                TrimPanelSize(MaxPanelSizeInEntries);
            }
        }

        [CanBeNull] Action<uint> callback;

        void Awake()
        {
            yOffset = 1;
            buttonHeight = 45;
            
            ItemClicked += OnItemClicked;
            CloseClicked += Close;
        }

        public void Set(MenuEntryList menu, Vector3 positionHint, Action<uint> newCallback)
        {
            callback = newCallback ?? throw new ArgumentNullException(nameof(newCallback));
            menuEntryList = menu ?? throw new ArgumentNullException(nameof(menu));
            CurrentEntries = menuEntryList.GetDescriptionsForRoot();
            gameObject.SetActive(true);
        }

        void OnItemClicked(int id, int _)
        {
            var entry = currentEntries[id];
            if (!entry.IsLeaf)
            {
                CurrentEntries = entry.GetChildDescriptions();
            }
            else
            {
                callback?.Invoke(entry.Id);
                Close();
            }
        }

        [NotNull]
        static string DescriptionToString([NotNull] MenuEntryDescription description)
        {
            return description.Type switch
            {
                MenuEntryList.EntryType.Forward => $"<b>{description.Title}  →</b>",
                MenuEntryList.EntryType.Off => $"<b>{description.Title}</b>\nOff",
                MenuEntryList.EntryType.On => $"<b>{description.Title}</b>\nOn",
                MenuEntryList.EntryType.Default => $"<b>{description.Title}</b>",
                MenuEntryList.EntryType.Back => $"<b>← {description.Title}</b>",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        void Close()
        {
            gameObject.SetActive(false);
            callback = null;
        }
    }
}