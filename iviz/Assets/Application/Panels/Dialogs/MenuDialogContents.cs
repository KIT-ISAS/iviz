using UnityEngine;
using UnityEngine.UI;
using System;
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
        void Set([NotNull] MenuEntryList menu);
        void Close();
    }
    
    public sealed class MenuDialogContents : ItemListDialogContents, IMenuDialogContents
    {
        MenuEntryList menuEntryList;
        (MenuEntryList.Entry LinkedEntry, string EntryDescription, uint EntryId)[] currentEntries;

        public event Action<uint> MenuClicked;

        void Awake()
        {
            yOffset = 1;
            buttonHeight = 45;
            
            ItemClicked += OnItemClicked;
            CloseClicked += Close;

            /*
            MenuEntry[] entries =
            {
                new MenuEntry
                {
                    Id = 1,
                    ParentId = 0,
                    Title = "My text!"
                },
                new MenuEntry
                {
                    Id = 2,
                    ParentId = 0,
                    Title = "My other entry!"
                },
                new MenuEntry
                {
                    Id = 3,
                    ParentId = 0,
                    Title = "[ ] My third entry!"
                },
                new MenuEntry
                {
                    Id = 4,
                    ParentId = 1,
                    Title = "My subentry!"
                },
                new MenuEntry
                {
                    Id = 5,
                    ParentId = 0,
                    Title = "My other subentry!"
                },
            };

            Set(entries);
            */
        }

        public void Set(MenuEntryList menu)
        {
            menuEntryList = menu ?? throw new ArgumentNullException(nameof(menu));
            currentEntries = menuEntryList.GetDescriptionsFor(null).ToArray();
            Items = currentEntries.Select(tuple => tuple.EntryDescription);
            TrimPanelSize();

            gameObject.SetActive(true);
        }

        void OnItemClicked(int id, string _)
        {
            var (linkedEntry, _, entryId) = currentEntries[id];
            if (linkedEntry != null)
            {
                currentEntries = menuEntryList.GetDescriptionsFor(linkedEntry).ToArray();
                Items = currentEntries.Select(tuple => tuple.EntryDescription);
                TrimPanelSize();
            }
            else
            {
                MenuClicked?.Invoke(entryId);
                Close();
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
            MenuClicked = null;
        }
    }
}