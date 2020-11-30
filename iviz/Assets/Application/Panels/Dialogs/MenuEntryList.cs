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

        public class Entry
        {
            readonly string title;
            public uint Id { get; }
            [CanBeNull] public Entry Parent { get; set; }
            public List<Entry> Children { get; } = new List<Entry>();
            public string Description { get; private set; }

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
                        Description = $"<b>{title} →</b>";
                        break;
                    case true:
                        Description = $"<b>{title.Substring(4)}</b>\nOn";
                        break;
                    case false:
                        Description = $"<b>{title.Substring(4)}</b>\nOff";
                        break;
                    default:
                        Description = $"<b>{title}</b>";
                        break;
                }
            }
        }

        public MenuEntryList([NotNull] MenuEntry[] menu, [NotNull] StringBuilder description, ref int numErrors)
        {
            if (menu == null)
            {
                throw new ArgumentNullException(nameof(menu));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            root = new Entry(null);
            var entries = new Dictionary<uint, Entry> { [0] = root };
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
        static IEnumerable<(Entry, string, uint)> BackDescription(Entry parent) => new[] {(parent, "<b>← Back</b>", 0u)};

        [NotNull]
        public IEnumerable<(Entry LinkedEntry, string EntryDescription, uint EntryId)> GetDescriptionsFor([CanBeNull] Entry currentLevel)
        {
            var children = currentLevel != null
                ? currentLevel.Children
                : root.Children;

            var childDescriptions = children.Select(child =>
                (child.Children.Count != 0 ? child : null, child.Description, child.Id)
            );

            return currentLevel == null || currentLevel == root 
                ? childDescriptions 
                : BackDescription(currentLevel.Parent).Concat(childDescriptions);
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