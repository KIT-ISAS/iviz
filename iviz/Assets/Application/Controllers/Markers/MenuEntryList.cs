#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Core;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.Controllers.Markers
{
    internal sealed class MenuEntryList
    {
        readonly Entry root;

        public int Count { get; }

        public StringBuilder? ErrorMessages { get; }
        
        public MenuEntryList(MenuEntry[] menu)
        {
            ThrowHelper.ThrowIfNull(menu, nameof(menu));

            root = new Entry();

            var entries = new Dictionary<uint, Entry> { [0] = root };

            foreach (var menuEntry in menu)
            {
                if (menuEntry.Id == 0)
                {
                    ErrorMessages ??= new StringBuilder();
                    ErrorMessages.Append("A menu entry uses the reserved id 0").AppendLine();
                    continue;
                }

                if (entries.ContainsKey(menuEntry.Id))
                {
                    ErrorMessages ??= new StringBuilder();
                    ErrorMessages.Append("Duplicate menu entries for id ").Append(menuEntry.Id).AppendLine();
                }

                entries[menuEntry.Id] = new Entry(menuEntry);
            }

            foreach (var menuEntry in menu)
            {
                if (!entries.TryGetValue(menuEntry.ParentId, out var parentEntry))
                {
                    ErrorMessages ??= new StringBuilder();
                    ErrorMessages.Append("Menu entry ").Append(menuEntry.Id).Append(" has unknown parent ")
                        .Append(menuEntry.ParentId).AppendLine();
                    continue;
                }

                var unsealedEntry = entries[menuEntry.Id];
                var sealedEntry = unsealedEntry.Seal(parentEntry);
                entries[menuEntry.Id] = sealedEntry;
                parentEntry.Children.Add(sealedEntry);
            }

            Count = entries.Count;
        }

        public MenuEntryDescription[] RootEntries =>
            root.Children.Select(child => new MenuEntryDescription(child)).ToArray();


        internal sealed class Entry
        {
            readonly string title;
            readonly MenuCheckboxState checkboxState;

            public uint Id { get; }
            public Entry? Parent { get; }
            public List<Entry> Children { get; } = new();
            public string Description { get; }
            public MenuEntryType Type { get; }

            public Entry()
            {
                Id = 0;
                title = "Root";
                Description = "";
                checkboxState = MenuCheckboxState.NotACheckbox;
            }

            public Entry(MenuEntry entry)
            {
                Id = entry.Id;
                title = entry.Title;
                Description = "";
                checkboxState = GetCheckboxState(title);
            }

            Entry(Entry original, Entry? parent)
            {
                Parent = parent;
                Id = original.Id;
                title = original.title;
                checkboxState = original.checkboxState;

                Description = checkboxState switch
                {
                    MenuCheckboxState.NotACheckbox when Children.Count != 0 => title,
                    MenuCheckboxState.True or MenuCheckboxState.False => title[4..],
                    _ => title
                };
                Type = checkboxState switch
                {
                    MenuCheckboxState.NotACheckbox when Children.Count != 0 => MenuEntryType.Forward,
                    MenuCheckboxState.True => MenuEntryType.On,
                    MenuCheckboxState.False => MenuEntryType.Off,
                    _ => MenuEntryType.Default
                };
            }

            public Entry Seal(Entry? parent) => new(this, parent);

            static MenuCheckboxState GetCheckboxState(string name)
            {
                if (name.Length < 4 || name[0] != '[' || name[2] != ']' || name[3] != ' ')
                {
                    return MenuCheckboxState.NotACheckbox;
                }

                return name[1] switch
                {
                    ' ' => MenuCheckboxState.False,
                    'X' or 'x' => MenuCheckboxState.True,
                    _ => MenuCheckboxState.NotACheckbox
                };
            }

            enum MenuCheckboxState
            {
                NotACheckbox,
                True,
                False
            }
        }
    }

    public enum MenuEntryType
    {
        Default,
        On,
        Off,
        Forward,
        Back
    }

    public sealed class MenuEntryDescription
    {
        readonly MenuEntryList.Entry entry;
        public string Title { get; }
        public uint Id { get; }
        public MenuEntryType Type { get; }

        internal MenuEntryDescription(MenuEntryList.Entry entry)
        {
            this.entry = entry;
            Title = entry.Description;
            Id = entry.Id;
            Type = entry.Type;
        }

        MenuEntryDescription(MenuEntryList.Entry backEntry, string backTitle)
        {
            entry = backEntry;
            Title = backTitle;
            Id = uint.MaxValue;
            Type = MenuEntryType.Back;
        }

        public bool IsLeaf => entry.Children.Count == 0;

        public MenuEntryDescription[] GetChildDescriptions()
        {
            if (IsLeaf)
            {
                return Array.Empty<MenuEntryDescription>();
            }

            var childDescriptions = entry.Children.Select(child => new MenuEntryDescription(child));

            return entry.Parent is null
                ? childDescriptions.ToArray()
                : childDescriptions.Prepend(BackDescription(entry.Parent)).ToArray();
        }

        static MenuEntryDescription BackDescription(MenuEntryList.Entry parent) => new(parent, "Back");
    }
}