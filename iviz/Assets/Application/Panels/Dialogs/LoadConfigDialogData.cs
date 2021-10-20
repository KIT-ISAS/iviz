using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Iviz.Tools;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Logger = Iviz.Core.Logger;

namespace Iviz.App
{
    public sealed class LoadConfigDialogData : DialogData
    {
        internal const string Suffix = ".config.json";

        [NotNull] readonly ItemListDialogContents itemList;
        [NotNull] readonly List<SavedFileInfo> files = new List<SavedFileInfo>();
        public override IDialogPanelContents Panel => itemList;

        public LoadConfigDialogData()
        {
            itemList = DialogPanelManager.GetPanelByType<ItemListDialogContents>(DialogPanelType.Load);
            itemList.ButtonType = Resource.Widgets.ItemButtonWithDelete;
        }

        [NotNull]
        public static IEnumerable<SavedFileInfo> SavedFiles => Directory
            .GetFiles(Settings.SavedFolder)
            .Where(name => name.HasSuffix(Suffix))
            .Select(name => new SavedFileInfo(name));

        public override void SetupPanel()
        {
            ReadAllFiles();
            itemList.Title = "Load Config File";
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += Close;
            itemList.EmptyText = "No Config Files Found";
        }
        void ReadAllFiles()
        {
            files.Clear();
            files.AddRange(SavedFiles);
            files.Sort();
            files.Reverse();
            itemList.Items = files.Select(file => file.Description);
        }

        void OnItemClicked(int index, int subIndex)
        {
            switch (subIndex)
            {
                case 0:
                    ModuleListPanel.LoadStateConfiguration(files[index].FileName);
                    Close();
                    break;
                case 1:
                    string filename = files[index].FullPath;
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

    public sealed class SavedFileInfo : IComparable<SavedFileInfo>
    {
        [NotNull] public string FullPath { get; }
        [NotNull] public string FileName => Path.GetFileName(FullPath);

        DateTime date;
        DateTime Date => date != default ? date : (date = File.GetLastWriteTime(FullPath));

        [NotNull]
        public string Description
        {
            get
            {
                string fileName = FileName;
                string simpleName = fileName.Substring(0, fileName.Length - LoadConfigDialogData.Suffix.Length);
                string lastModified = Date.ToString("MM/dd/yyyy HH:mm");
                return $"<b>{simpleName}</b>\n[{lastModified}]";
            }
        } 

        public SavedFileInfo([NotNull] string fullPath)
        {
            FullPath = fullPath;
        }

        public int CompareTo([CanBeNull] SavedFileInfo other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Date.CompareTo(other.Date);
        }

        [NotNull]
        public override string ToString()
        {
            return FullPath;
        }
    } 
}