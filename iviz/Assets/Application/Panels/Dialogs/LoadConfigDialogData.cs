using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public sealed class LoadConfigDialogData : DialogData
    {
        ItemListDialogContents itemList;
        public override IDialogPanelContents Panel => itemList;

        const string Suffix = ".config.json";

        readonly List<string> files = new List<string>();

        public override void Initialize(DisplayListPanel panel)
        {
            base.Initialize(panel);
            itemList = (ItemListDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.ItemList);
        }

        public override void SetupPanel()
        {
            files.Clear();
            files.AddRange(Directory.GetFiles(ModuleListPanel.PersistentDataPath).
                Where(x => RoslibSharp.Utils.HasSuffix(x, Suffix)).
                Select(GetFileName));
            itemList.Title = "Load Config File";
            itemList.Items = files;
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;
            itemList.EmptyText = "No Config Files Found";
        }

        static string GetFileName(string s)
        {
            string fs = Path.GetFileName(s);
            return fs.Substring(0, fs.Length - Suffix.Length);
        }

        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            ModuleListPanel.LoadStateConfiguration(files[index] + Suffix);
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}