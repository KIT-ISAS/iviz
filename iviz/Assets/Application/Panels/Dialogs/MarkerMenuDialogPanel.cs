#nullable enable

using System;
using Iviz.Controllers.Markers;
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
                callback.TryRaise(entry.Id, this);
                Close();
            }
        }

        static string DescriptionToString(MenuEntryDescription description)
        {
            string title = description.Title;
            return description.Type switch
            {
                MenuEntryType.Forward => $"<b>{title}  →</b>",
                MenuEntryType.Off => $"<b>{title}</b>\nOff",
                MenuEntryType.On => $"<b>{title}</b>\nOn",
                MenuEntryType.Default => $"<b>{title}</b>",
                MenuEntryType.Back => $"<b>← {title}</b>",
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