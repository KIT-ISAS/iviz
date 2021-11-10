using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.App
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ARMarkerAction
    {
        None,
        Publish,
        Origin
    }

    [DataContract]
    public sealed class ARExecutableMarker : JsonToString
    {
        [DataMember] public ARMarkerType Type { get; set; }
        [DataMember] public ARMarkerAction Action { get; set; }
        [DataMember, NotNull] public string Code { get; set; } = "";
        [DataMember] public float SizeInMm { get; set; }
    }

    [DataContract]
    public sealed class ARMarkersConfiguration : JsonToString
    {
        [DataMember] public float MaxMarkerDistanceInM { get; set; } = 0.5f;
        [DataMember, NotNull] public ARExecutableMarker[] Markers { get; set; } = Array.Empty<ARExecutableMarker>();
    }

    public sealed class ARMarkerDialogData : DialogData
    {
        [NotNull] readonly ARMarkerDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        [NotNull] public ARMarkersConfiguration Configuration { get; set; } = new ARMarkersConfiguration();

        public ARMarkerDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<ARMarkerDialogContents>(DialogPanelType.ARMarkers);
            ARController.ARStateChanged += OnARStateChanged;
        }


        public override void FinalizePanel()
        {
            ARController.ARStateChanged -= OnARStateChanged;
        }

        void OnARStateChanged(bool _)
        {
            if (ARController.Instance != null)
            {
                ARController.Instance.MarkerExecutor.Configuration = Configuration;
            }
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            WriteConfigurationToPanel();
            panel.Close.Clicked += Close;
            panel.ActionsChanged += (_, __) => WritePanelToConfiguration();
            panel.TypesChanged += (_, __) => WritePanelToConfiguration();
            panel.CodesEndEdit += (_, __) => WritePanelToConfiguration();
            panel.SizesEndEdit += (_, __) => WritePanelToConfiguration();

            UpdatePanel();
        }

        void WriteConfigurationToPanel()
        {
            panel.ResetAll();
            foreach (var (marker, index) in Configuration.Markers.WithIndex())
            {
                panel.Types[index].Index = (int) marker.Type;
                panel.Actions[index].Index = (int) marker.Action;
                panel.Codes[index].Value = marker.Code;
                panel.Sizes[index].Value = marker.SizeInMm.ToString(BuiltIns.Culture);
            }
        }

        void WritePanelToConfiguration()
        {
            var markers = new List<ARExecutableMarker>();
            for (int index = 0; index < panel.Types.Count; index++)
            {
                var type = (ARMarkerType) panel.Types[index].Index;
                if (type == ARMarkerType.Unset)
                {
                    continue;
                }

                if (!float.TryParse(panel.Sizes[index].Value, out float sizeInMm) || sizeInMm <= 0)
                {
                    RosLogger.Info($"{this}: Ignoring size for entry {index}, cannot parse '{panel.Sizes[index].Value}' " +
                                "into a positive number.");
                    continue;
                }

                string code = panel.Codes[index].Value.Trim();
                if (string.IsNullOrEmpty(code))
                {
                    RosLogger.Info($"{this}: Ignoring empty code for entry {index}.");
                    continue;
                }

                ARExecutableMarker marker = new ARExecutableMarker
                {
                    Type = type,
                    Action = (ARMarkerAction) panel.Actions[index].Index,
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

            if (ARController.Instance != null)
            {
                ARController.Instance.MarkerExecutor.Configuration = Configuration;
            }

            ModuleListPanel.UpdateSimpleConfigurationSettings();
        }
    }
}