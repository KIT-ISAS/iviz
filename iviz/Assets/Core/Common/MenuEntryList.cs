using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Msgs.VisualizationMsgs;
using JetBrains.Annotations;

namespace Iviz.App
{
    public class MenuEntryList
    {
        readonly Entry root;

        public int Count { get; }

        public enum EntryType
        {
            Default,
            On,
            Off,
            Forward,
            Back
        }

        public sealed class Entry
        {
            [NotNull] readonly string title;
            public uint Id { get; }
            [CanBeNull] public Entry Parent { get; set; }
            public List<Entry> Children { get; } = new List<Entry>();
            public string Description { get; private set; }
            public EntryType Type { get; private set; }

            public Entry([CanBeNull] MenuEntry entry)
            {
                Id = entry?.Id ?? 0;
                title = entry?.Title ?? "Root";
            }

            public void UpdateDescription()
            {
                switch (IsCheckbox(title))
                {
                    case null when Children.Count != 0:
                        Type = EntryType.Forward;
                        Description = title;
                        break;
                    case true:
                        Type = EntryType.On;
                        Description = title.Substring(4);
                        break;
                    case false:
                        Type = EntryType.Off;
                        Description = title.Substring(4);
                        break;
                    default:
                        Type = EntryType.Default;
                        Description = title;
                        break;
                }
            }
        }

        public sealed class EntryDescription
        {
            public Entry LinkedEntry { get; }
            public string Title { get; }
            public uint Id { get; }
            public EntryType Type { get; }

            internal EntryDescription(Entry linkedEntry, string title, uint id, EntryType type)
            {
                LinkedEntry = linkedEntry;
                Title = title;
                Id = id;
                Type = type;
            }
        }

        public MenuEntryList([NotNull] IReadOnlyList<MenuEntry> menu, [NotNull] StringBuilder description, out int numErrors)
        {
            if (menu == null)
            {
                throw new ArgumentNullException(nameof(menu));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            numErrors = 0;
            root = new Entry(null);
            var entries = new Dictionary<uint, Entry> {[0] = root};
            foreach (var menuEntry in menu)
            {
                if (menuEntry.Id == 0)
                {
                    description.Append("A menu entry uses the reserved id 0").AppendLine();
                    numErrors++;
                    continue;
                }

                if (entries.ContainsKey(menuEntry.Id))
                {
                    description.Append("Duplicate menu entries for id ").Append(menuEntry.Id).AppendLine();
                    numErrors++;
                }

                entries[menuEntry.Id] = new Entry(menuEntry);
            }

            foreach (var menuEntry in menu)
            {
                if (!entries.TryGetValue(menuEntry.ParentId, out var parentEntry))
                {
                    description.Append("Menu entry ").Append(menuEntry.Id).Append(" has unknown parent ")
                        .Append(menuEntry.ParentId).AppendLine();
                    numErrors++;
                    continue;
                }

                var childEntry = entries[menuEntry.Id];
                childEntry.Parent = parentEntry;
                parentEntry.Children.Add(childEntry);
            }

            foreach (var entry in entries.Values)
            {
                entry.UpdateDescription();
            }

            Count = entries.Count;
        }

        [NotNull]
        static IEnumerable<EntryDescription> BackDescription(Entry parent) =>
            new[] {new EntryDescription(parent, "Back", 0u, EntryType.Back)};

        [NotNull]
        public IEnumerable<EntryDescription> GetDescriptionsFor([CanBeNull] Entry currentLevel,
            bool backEntryOnTop = true)
        {
            var children = currentLevel != null
                ? currentLevel.Children
                : root.Children;

            var childDescriptions = children.Select(child =>
                new EntryDescription(child.Children.Count != 0 ? child : null, child.Description, child.Id, child.Type)
            );

            if (currentLevel == null || currentLevel == root)
            {
                return childDescriptions;
            }

            return backEntryOnTop
                ? BackDescription(currentLevel.Parent).Concat(childDescriptions)
                : childDescriptions.Concat(BackDescription(currentLevel.Parent));
        }

        static bool? IsCheckbox([NotNull] string name)
        {
            if (name.Length < 4 || name[0] != '[' || name[2] != ']' || name[3] != ' ')
            {
                return null;
            }

            switch (name[1])
            {
                case ' ':
                    return false;
                case 'X':
                case 'x':
                    return true;
                default:
                    return null;
            }
        }
    }
}