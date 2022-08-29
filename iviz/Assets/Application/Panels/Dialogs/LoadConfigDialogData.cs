#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;

namespace Iviz.App
{
    public sealed class LoadConfigDialogData : DialogData
    {
        internal const string Suffix = ".config.json";

        readonly ItemListDialogPanel itemList;
        readonly List<ISavedFileInfo> files = new();
        
        public override IDialogPanel Panel => itemList;

        public LoadConfigDialogData()
        {
            itemList = DialogPanelManager.GetPanelByType<ItemListDialogPanel>(DialogPanelType.Load);
            itemList.ButtonType = Resource.Widgets.ItemButtonWithDelete;
        }

        public static IEnumerable<ISavedFileInfo> SavedFiles => Directory
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
            itemList.SetItems(files.Select(file => file.Description));
        }

        void OnItemClicked(int index, int subIndex)
        {
            switch (subIndex)
            {
                case 0:
                    ModuleListPanel.LoadStateConfigurationAsync(files[index].FileName);
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
                        RosLogger.Internal("Error deleting config file", e);
                    }

                    ReadAllFiles();
                    break;
            }
        }

        sealed class SavedFileInfo : ISavedFileInfo
        {
            DateTime date;
            DateTime Date => date != default ? date : (date = File.GetLastWriteTime(FullPath));

            public string FullPath { get; }
            public string FileName => Path.GetFileName(FullPath);
            
            public string Description
            {
                get
                {
                    string fileName = FileName;
                    string simpleName = fileName[..^Suffix.Length];
                    string lastModified = Date.ToString("MM/dd/yyyy HH:mm");
                    return $"<b>{simpleName}</b>\n[{lastModified}]";
                }
            }

            public SavedFileInfo(string fullPath)
            {
                FullPath = fullPath;
            }

            int CompareTo(SavedFileInfo? other)
            {
                if (other == this) return 0;
                return other == null ? 1 : Date.CompareTo(other.Date);
            }

            public int CompareTo(ISavedFileInfo? other) => CompareTo((SavedFileInfo?)other);
            
            public override string ToString() => FullPath;
        }
    }

    public interface ISavedFileInfo : IComparable<ISavedFileInfo>
    {
        public string FullPath { get; }
        public string FileName { get; }
        public string Description { get; }
    }
}