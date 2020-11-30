using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class MenuDialogContents : ItemListDialogContents
    {
        int currentParent;
        
        protected override void Start()
        {
            base.Start();
            YOffset = 1;
            ButtonHeight = 30;

            Items = new[] {"Entry 1", "Entry 2", "Entry 3", "Entry 4"};
            TrimPanelSize();

            currentParent = 0;
        }

        void Set(MenuEntry[] menu)
        {
            var entries = menu.Where(entry => entry.ParentId == currentParent);

            bool IsParent(uint id) => menu.Any(entry => entry.ParentId == id);

            Items = entries.Select(entry =>
            {
                switch (IsCheckbox(entry.Title))
                {
                    case null when IsParent(entry.Id):
                        return $"{entry.Title} →";
                    case null:
                        return $"<b>{entry.Title}</b>";
                    case true:
                        return $"✓ <b>{entry.Title}</b>";
                    case false:
                        return $"X <b>{entry.Title}</b>";
                }
            });

        }

        static bool? IsCheckbox(string name)
        {
            if (name.Length < 4)
            {
                return null;
            }

            if (name[0] != '[')
            {
                return null;
            }

            switch (name.Substring(0, 4))
            {
                case "[ ] ":
                    return false;
                case "[X] ":
                    return true;
                default:
                    return null;
            }
        }

        
    }
}