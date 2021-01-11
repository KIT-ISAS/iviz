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
        MenuEntryList.EntryDescription[] currentEntries;

        IEnumerable<MenuEntryList.EntryDescription> CurrentEntries
        {
            get => currentEntries;
            set
            {
                currentEntries = value.ToArray();
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
            CurrentEntries = menuEntryList.GetDescriptionsFor(null);
            gameObject.SetActive(true);
        }

        void OnItemClicked(int id, string _)
        {
            var entry = currentEntries[id];
            if (entry.LinkedEntry != null)
            {
                CurrentEntries = menuEntryList.GetDescriptionsFor(entry.LinkedEntry);
            }
            else
            {
                callback?.Invoke(entry.Id);
                Close();
            }
        }

        [NotNull]
        static string DescriptionToString([NotNull] MenuEntryList.EntryDescription description)
        {
            switch (description.Type)
            {
                case MenuEntryList.EntryType.Forward:
                    return $"<b>{description.Title}  →</b>";
                case MenuEntryList.EntryType.Off:
                    return $"<b>{description.Title}</b>\nOff";
                case MenuEntryList.EntryType.On:
                    return $"<b>{description.Title}</b>\nOn";
                case MenuEntryList.EntryType.Default:
                    return $"<b>{description.Title}</b>";
                case MenuEntryList.EntryType.Back:
                    return $"<b>← {description.Title}</b>";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void Close()
        {
            gameObject.SetActive(false);
            callback = null;
        }
    }
}