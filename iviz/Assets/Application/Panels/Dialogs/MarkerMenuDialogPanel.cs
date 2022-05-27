#nullable enable

using System;
using Iviz.Core;
using Iviz.Tools;

namespace Iviz.App
{
    public sealed class MarkerMenuDialogPanel : ItemListDialogPanel, IMenuDialogContents
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
            VerticalOffset = 1;
            ButtonHeight = 45;
            
            ItemClicked += OnItemClicked;
            CloseClicked += Close;
        }

        public void Set(MenuEntryDescription[] menuEntries, Action<uint> newCallback)
        {
            ThrowHelper.ThrowIfNull(newCallback, nameof(newCallback));
            callback = newCallback;
            Entries = menuEntries;
            
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
                MenuEntryType.Forward => $"<b>{description.Title}  →</b>",
                MenuEntryType.Off => $"<b>{description.Title}</b>\nOff",
                MenuEntryType.On => $"<b>{description.Title}</b>\nOn",
                MenuEntryType.Default => $"<b>{description.Title}</b>",
                MenuEntryType.Back => $"<b>← {description.Title}</b>",
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