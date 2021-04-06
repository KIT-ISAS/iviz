using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class LoadConfigDialogData : DialogData
    {
        const string Suffix = ".config.json";

        [NotNull] readonly ItemListDialogContents itemList;
        [NotNull] readonly List<string> files = new List<string>();
        public override IDialogPanelContents Panel => itemList;

        public LoadConfigDialogData()
        {
            itemList = DialogPanelManager.GetPanelByType<ItemListDialogContents>(DialogPanelType.Load);
            itemList.ButtonType = Resource.Widgets.ItemButtonWithDelete;
        }

        public static IEnumerable<string> SavedFiles => Directory.GetFiles(Settings.SavedFolder)
            .Where(x => x.HasSuffix(Suffix));

        public override void SetupPanel()
        {
            files.Clear();
            files.AddRange(SavedFiles.Select(GetFileName));
            itemList.Title = "Load Config File";
            itemList.Items = files.Select(file => $"<b>{file}</b>");
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += Close;
            itemList.EmptyText = "No Config Files Found";
        }
        void ReadAllFiles()
        {
            files.Clear();
            files.AddRange(SavedFiles.Select(GetFileName));
            itemList.Items = files.Select(file => $"<b>{file}</b>");
        }
        
        [NotNull]
        static string GetFileName([NotNull] string s)
        {
            string fs = Path.GetFileName(s);
            return fs.Substring(0, fs.Length - Suffix.Length);
        }

        void OnItemClicked(int index, int subIndex)
        {
            switch (subIndex)
            {
                case 0:
                    ModuleListPanel.LoadStateConfiguration(files[index] + Suffix);
                    Close();
                    break;
                case 1:
                    string filename = $"{Settings.SavedFolder}/{files[index]}{Suffix}";
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (Exception e)
                    {
                        Logger.Internal("Error deleting config file", e);
                    }

                    ReadAllFiles();
                    break;
            }
        }
    }
}