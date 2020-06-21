using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public sealed class SaveConfigDialogData : DialogData
    {
        SaveConfigDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        readonly List<string> files = new List<string>();

        public override void Initialize(DisplayListPanel panel)
        {
            base.Initialize(panel);
            this.panel = (SaveConfigDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.SaveAs);
        }

        public override void SetupPanel()
        {
            files.Clear();
            files.AddRange(Directory.GetFiles(Application.persistentDataPath).
                Where(x => RoslibSharp.Utils.HasSuffix(x, "config.json")).
                Select(Path.GetFileName));
            panel.Items = files;
            panel.ItemClicked += OnItemClicked;
            panel.CloseClicked += OnCloseClicked;
            panel.EmptyText = "No Config Files Found";

            panel.saveButton.Clicked += OnSaveClicked;
        }

        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            ModuleListPanel.SaveStateConfiguration(files[index]);
            Close();
        }

        void OnSaveClicked()
        {
            ModuleListPanel.SaveStateConfiguration(panel.input.Value + ".config.json");
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}