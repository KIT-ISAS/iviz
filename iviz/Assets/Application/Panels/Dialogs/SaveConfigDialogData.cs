﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class SaveConfigDialogData : DialogData
    {
        [NotNull] readonly SaveConfigDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        const string Suffix = LoadConfigDialogData.Suffix;

        readonly List<SavedFileInfo> files = new List<SavedFileInfo>();

        public SaveConfigDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<SaveConfigDialogContents>(DialogPanelType.SaveAs);
            panel.ButtonType = Resource.Widgets.ItemButtonWithDelete;
        }

        public override void SetupPanel()
        {
            ReadAllFiles();
            panel.ItemClicked += OnItemClicked;
            panel.CloseClicked += Close;
            panel.EmptyText = "No Config Files Found";
            panel.Input.Value = GameThread.Now.ToString("MM_dd_yyyy HH_mm");

            panel.SaveButton.Clicked += OnSaveClicked;
        }

        void ReadAllFiles()
        {
            files.Clear();
            files.AddRange(LoadConfigDialogData.SavedFiles);
            files.Sort();
            files.Reverse();
            panel.Items = files.Select(file => file.Description);
        }

        void OnItemClicked(int index, int subIndex)
        {
            switch (subIndex)
            {
                case 0:
                    ModuleListPanel.SaveStateConfiguration(files[index].FileName);
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

        void OnSaveClicked()
        {
            string name = panel.Input.Value;
            string validatedName = name.HasSuffix(Suffix) ? name : name + Suffix;
            ModuleListPanel.SaveStateConfiguration(validatedName);
            Close();
        }
    }
}