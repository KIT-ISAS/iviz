#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.Core
{
    public sealed class MenuEntryList
    {
        readonly Entry root;

        public int Count { get; }

        public StringBuilder? ErrorMessages { get; }

        public enum EntryType
        {
            Default,
            On,
            Off,
            Forward,
            Back
        }
        
        enum CheckboxState
        {
            NotACheckbox,
            True,
            False
        }

        internal sealed class Entry
        {
            readonly string title;
            readonly CheckboxState checkboxState;
            
            public uint Id { get; }
            public Entry? Parent { get; set; }
            public List<Entry> Children { get; } = new();

            public Entry()
            {
                Id = 0;
                title = "Root";
                checkboxState = CheckboxState.NotACheckbox;
            }
            
            public Entry(MenuEntry entry)
            {
                Id = entry.Id;
                title = entry.Title;
                checkboxState = GetCheckboxState(title);
            }

            public string Description => checkboxState switch
            {
                CheckboxState.NotACheckbox when Children.Count != 0 => title,
                CheckboxState.True or CheckboxState.False => title[4..],
                _ => title
            };

            public EntryType Type => checkboxState switch
            {
                CheckboxState.NotACheckbox when Children.Count != 0 => EntryType.Forward,
                CheckboxState.True => EntryType.On,
                CheckboxState.False => EntryType.Off,
                _ => EntryType.Default
            };
        }

        public MenuEntryList(MenuEntry[] menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException(nameof(menu));
            }

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

                var childEntry = entries[menuEntry.Id];
                childEntry.Parent = parentEntry;
                parentEntry.Children.Add(childEntry);
            }

            Count = entries.Count;
        }

        public MenuEntryDescription[] GetDescriptionsForRoot() =>
            root.Children.Select(child => new MenuEntryDescription(child)).ToArray();

        static CheckboxState GetCheckboxState(string name)
        {
            if (name.Length < 4 || name[0] != '[' || name[2] != ']' || name[3] != ' ')
            {
                return CheckboxState.NotACheckbox;
            }

            return name[1] switch
            {
                ' ' => CheckboxState.False,
                'X' or 'x' => CheckboxState.True,
                _ => CheckboxState.NotACheckbox
            };
        }
    }

    public sealed class MenuEntryDescription
    {
        readonly MenuEntryList.Entry entry;
        public string Title { get; }
        public uint Id { get; }
        public MenuEntryList.EntryType Type { get; }

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
            Type = MenuEntryList.EntryType.Back;
        }

        public bool IsLeaf => entry.Children.Count != 0;

        public MenuEntryDescription[] GetChildDescriptions(bool backEntryOnTop = true)
        {
            if (IsLeaf)
            {
                return Array.Empty<MenuEntryDescription>();
            }

            var childDescriptions = entry.Children.Select(child => new MenuEntryDescription(child));

            if (entry.Parent is null)
            {
                return childDescriptions.ToArray();
            }

            return backEntryOnTop
                ? BackDescription(entry.Parent).Concat(childDescriptions).ToArray()
                : childDescriptions.Concat(BackDescription(entry.Parent)).ToArray();
        }

        static IEnumerable<MenuEntryDescription> BackDescription(MenuEntryList.Entry parent) =>
            new[] { new MenuEntryDescription(parent, "Back") };
    }
}