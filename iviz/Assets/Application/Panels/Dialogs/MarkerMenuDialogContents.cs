#nullable enable

using UnityEngine;
using System;
using Iviz.Core;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class MarkerMenuDialogContents : ItemListDialogContents, IMenuDialogContents
    {
        const int MaxPanelSizeInEntries = 7;
        
        MenuEntryDescription[] entries = Array.Empty<MenuEntryDescription>();
        Action<uint>? callback;

        MenuEntryDescription[] Entries
        {
            set
            {
                entries = value;
                SetItems(entries.Select(DescriptionToString));
                TrimPanelSize(MaxPanelSizeInEntries);
            }
        }
        
        void Awake()
        {
            yOffset = 1;
            buttonHeight = 45;
            
            ItemClicked += OnItemClicked;
            CloseClicked += Close;
        }

        public void Set(MenuEntryList menu, Vector3 _, Action<uint> newCallback)
        {
            callback = newCallback ?? throw new ArgumentNullException(nameof(newCallback));
            var menuEntryList = menu ?? throw new ArgumentNullException(nameof(menu));
            Entries = menuEntryList.GetDescriptionsForRoot();
            gameObject.SetActive(true);
        }

        void OnItemClicked(int id, int _)
        {
            var entry = entries[id];
            if (!entry.IsLeaf)
            {
                Entries = entry.GetChildDescriptions();
            }
            else
            {
                callback?.Invoke(entry.Id);
                Close();
            }
        }

        static string DescriptionToString(MenuEntryDescription description)
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