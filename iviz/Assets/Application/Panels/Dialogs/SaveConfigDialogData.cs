using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class SaveConfigDialogData : DialogData
    {
        [NotNull] readonly SaveConfigDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        const string Suffix = ".config.json";

        readonly List<string> files = new List<string>();

        public SaveConfigDialogData()
        {
            this.panel = DialogPanelManager.GetPanelByType<SaveConfigDialogContents>(DialogPanelType.SaveAs);
        }

        public override void SetupPanel()
        {
            files.Clear();
            files.AddRange(Directory.GetFiles(Settings.SavedFolder).
                Where(x => Roslib.Utils.HasSuffix(x, Suffix)).
                Select(GetFileName));
            panel.Items = files;
            panel.ItemClicked += OnItemClicked;
            panel.CloseClicked += Close;
            panel.EmptyText = "No Config Files Found";
            panel.Input.Value = DateTime.Now.ToString("MM_dd_yyyy HH_mm");

            panel.SaveButton.Clicked += OnSaveClicked;
        }

        static string GetFileName(string s)
        {
            string fs = Path.GetFileName(s);
            return fs.Substring(0, fs.Length - Suffix.Length);
        }

        void OnItemClicked(int index, string _)
        {
            ModuleListPanel.SaveStateConfiguration(files[index] + Suffix);
            Close();
        }

        void OnSaveClicked()
        {
            ModuleListPanel.SaveStateConfiguration(panel.Input.Value + Suffix);
            Close();
        }
    }
}