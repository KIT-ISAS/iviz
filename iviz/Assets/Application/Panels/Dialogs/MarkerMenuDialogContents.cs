using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    interface IMenuDialogContents
    {
        event Action<uint> MenuClicked;
        void Set([NotNull] MenuEntryList menu, Vector3 positionHint);
        void Close();
    }
    
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

        public event Action<uint> MenuClicked;

        void Awake()
        {
            yOffset = 1;
            buttonHeight = 45;
            
            ItemClicked += OnItemClicked;
            CloseClicked += Close;
        }

        public void Set(MenuEntryList menu, Vector3 positionHint)
        {
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
                MenuClicked?.Invoke(entry.Id);
                Close();
            }
        }

        static string DescriptionToString(MenuEntryList.EntryDescription description)
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

        public void Close()
        {
            gameObject.SetActive(false);
            MenuClicked = null;
        }
    }
}