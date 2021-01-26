using System.Collections.Generic;
using System.IO;
using System.Linq;
using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class LoadConfigDialogData : DialogData
    {
        [NotNull] readonly ItemListDialogContents itemList;
        public override IDialogPanelContents Panel => itemList;

        const string Suffix = ".config.json";

        readonly List<string> files = new List<string>();

        public LoadConfigDialogData()
        {
            itemList = DialogPanelManager.GetPanelByType<ItemListDialogContents>(DialogPanelType.AddModule);
        }

        public static IEnumerable<string> SavedFiles => Directory.GetFiles(Settings.SavedFolder)
            .Where(x => Roslib.Utils.HasSuffix(x, Suffix));

        public override void SetupPanel()
        {
            files.Clear();
            files.AddRange(SavedFiles.Select(GetFileName));
            itemList.Title = "Load Config File";
            itemList.Items = files;
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += Close;
            itemList.EmptyText = "No Config Files Found";
        }

        [NotNull]
        static string GetFileName([NotNull] string s)
        {
            string fs = Path.GetFileName(s);
            return fs.Substring(0, fs.Length - Suffix.Length);
        }

        void OnItemClicked(int index, string _)
        {
            ModuleListPanel.LoadStateConfiguration(files[index] + Suffix);
            Close();
        }
    }
}