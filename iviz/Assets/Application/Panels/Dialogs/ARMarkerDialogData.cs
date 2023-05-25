﻿#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.App
{
    public sealed class ARMarkerDialogData : DialogData
    {
        readonly ARMarkerDialogPanel panel;
        public override IDialogPanel Panel => panel;

        public ARMarkersConfiguration Configuration { get; set; } = new();

        public ARMarkerDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<ARMarkerDialogPanel>(DialogPanelType.ARMarkers);
            //ARController.ARStateChanged += OnARStateChanged;
        }
        
        public override void Dispose()
        {
            //ARController.ARStateChanged -= OnARStateChanged;
        }

        /*
        void OnARStateChanged(bool _)
        {
            if (ARController.Instance != null)
            {
                ARController.Instance.MarkerExecutor.Configuration = Configuration;
            }
        }
        */

        public override void SetupPanel()
        {
            ResetPanelPosition();

            WriteConfigurationToPanel();
            panel.Close.Clicked += Close;
            panel.ActionsChanged += (_, _) => WritePanelToConfiguration();
            panel.TypesChanged += (_, _) => WritePanelToConfiguration();
            panel.CodesEndEdit += (_, _) => WritePanelToConfiguration();
            panel.SizesEndEdit += (_, _) => WritePanelToConfiguration();

            UpdatePanel();
        }

        void WriteConfigurationToPanel()
        {
            panel.ResetAll();
            foreach (var (marker, index) in Configuration.Markers.WithIndex())
            {
                panel.Types[index].Index = (int) marker.Type;
                //panel.Actions[index].Index = (int) marker.Action;
                panel.Codes[index].Value = marker.Code;
                panel.Sizes[index].Value = marker.SizeInMm.ToString(BuiltIns.Culture);
            }
        }

        void WritePanelToConfiguration()
        {
            var markers = new List<ARSeenMarker>();
            int typesCount = panel.Types.Count;
            for (int index = 0; index < typesCount; index++)
            {
                var type = (ARMarkerType) panel.Types[index].Index;
                if (type == ARMarkerType.Aruco)
                {
                    continue;
                }

                if (!float.TryParse(panel.Sizes[index].Value, out float sizeInMm) || sizeInMm < 0)
                {
                    RosLogger.Info($"{this}: Ignoring size for entry {index.ToString()}, " +
                                   $"cannot parse '{panel.Sizes[index].Value}' into a nonnegative number.");
                    continue;
                }

                string code = panel.Codes[index].Value.Trim();
                if (string.IsNullOrWhiteSpace(code))
                {
                    RosLogger.Info($"{this}: Ignoring empty code for entry {index.ToString()}.");
                    continue;
                }

                var marker = new ARSeenMarker
                {
                    Type = type,
                    //Action = (ARMarkerAction) panel.Actions[index].Index,
                    Code = code,
                    SizeInMm = sizeInMm,
                };
                markers.Add(marker);
            }

            Configuration = new ARMarkersConfiguration
            {
                MaxMarkerDistanceInM = 0.5f,
                Markers = markers.ToArray(),
            };

            //if (ARController.Instance != null)
            {
                //ARController.Instance.MarkerExecutor.Configuration = Configuration;
            }

            ModuleListPanel.UpdateSimpleConfigurationSettings();
        }
    }
}