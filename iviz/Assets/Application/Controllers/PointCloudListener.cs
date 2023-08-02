#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays.Helpers;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class PointCloudListener : ListenerController, IMarkerDialogListener
    {
        readonly PointCloudConfiguration config = new();
        readonly PointCloudProcessor processor;

        public Vector2 MeasuredIntensityBounds => processor.MeasuredIntensityBounds;

        public int NumValidPoints => processor.NumValidPoints;

        public override TfFrame? Frame => processor.Node.Parent;

        public PointCloudConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                IntensityChannel = value.IntensityChannel;
                PointSize = value.PointSize;
                Colormap = value.Colormap;
                OverrideMinMax = value.OverrideMinMax;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
                PointCloudType = value.PointCloudType;
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                processor.Visible = value;
            }
        }

        public string IntensityChannel
        {
            get => config.IntensityChannel;
            set
            {
                config.IntensityChannel = value;
                processor.IntensityChannel = value;
            }
        }

        public float PointSize
        {
            get => config.PointSize;
            set
            {
                config.PointSize = value;
                processor.PointSize = value;
            }
        }

        public ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                processor.Colormap = value;
            }
        }

        public bool OverrideMinMax
        {
            get => config.OverrideMinMax;
            set
            {
                config.OverrideMinMax = value;
                processor.OverrideMinMax = value;
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                processor.FlipMinMax = value;
            }
        }


        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                processor.MinIntensity = value;
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                processor.MaxIntensity = value;
            }
        }

        public PointCloudType PointCloudType
        {
            get => config.PointCloudType;
            set
            {
                config.PointCloudType = value;
                processor.PointCloudType = value;
            }
        }

        public bool IsIntensityUsed => processor.IsIntensityUsed;

        public IEnumerable<string> FieldNames => processor.FieldNames;

        public override Listener Listener { get; }

        public string Description
        {
            get
            {
                var (x, y) = MeasuredIntensityBounds;
                string minIntensityStr = UnityUtils.FormatFloat(x);
                string maxIntensityStr = UnityUtils.FormatFloat(y);
                return $"<b>{NumValidPoints.ToString("N0")} Valid</b>\n" +
                       (NumValidPoints == 0
                           ? "Empty"
                           : IsIntensityUsed
                               ? $"[{minIntensityStr} .. {maxIntensityStr}]"
                               : "Color");
            }
        }

        public string Title => "PointCloud";

        public string Topic => Listener.Topic;

        public int NumEntriesForLog => processor.TryGetLastMessage(out var pc) ? pc.Fields.Length : 0;

        public string BriefDescription
        {
            get
            {
                if (!processor.TryGetLastMessage(out var pc)) return "<b>[Empty]</b>";
                return $"<b>{pc.Width.ToString("N0")} x {pc.Height.ToString("N0")} points</b>\n"
                       + $"Pointstep: {pc.PointStep.ToString()} | {pc.Fields.Length.ToString()} fields";
            }
        }

        public PointCloudListener(PointCloudConfiguration? config, string topic)
        {
            processor = new PointCloudProcessor();
            Config = config ?? new PointCloudConfiguration
            {
                Topic = topic,
            };

            processor.Node.Name = Config.Topic;

            Listener = new Listener<PointCloud2>(Config.Topic, processor.Handle, RosSubscriptionProfile.SensorKeepLast);
            processor.IsProcessingChanged += Listener.SetPause;
        }


        public override void Dispose()
        {
            base.Dispose();
            processor.Dispose();
        }

        public void GenerateLog(StringBuilder description, int minIndex, int numEntries)
        {
            if (!processor.TryGetLastMessage(out var pc))
            {
                description.Append("No message received yet.");
                return;
            }

            uint totalPoints = pc.Width * pc.Height;
            description.Append("<b>Size: </b>").Append(totalPoints.ToString("N0")).Append(" points ");
            if (totalPoints != NumValidPoints)
            {
                description.Append("(").Append(NumValidPoints.ToString("N0")).Append(" valid)");
            }

            description.AppendLine();
            description.Append("<b>Payload: </b>").Append(pc.Data.Length.ToString("N0")).Append(" bytes").AppendLine();
            description.Append("<b>Width:</b> ").Append(pc.Width).AppendLine();
            description.Append("<b>Height:</b> ").Append(pc.Height).AppendLine();
            description.Append("<b>Dense:</b> ").Append(pc.IsDense).AppendLine();
            description.Append("<b>Big Endian:</b> ").Append(pc.IsBigendian).AppendLine();
            description.Append("<b>Pointstep:</b> ").Append(pc.PointStep).AppendLine();
            description.AppendLine();

            foreach (var field in pc.Fields)
            {
                description.Append("<b>** Field '").Append(field.Name).Append("' **</b>").AppendLine();
                description.Append("Offset: ").Append(field.Offset).AppendLine();

                string type = field.Datatype switch
                {
                    0 => "unset (0)",
                    PointField.INT8 => "int8 (1)",
                    PointField.UINT8 => "uint8 (2)",
                    PointField.INT16 => "int16 (3)",
                    PointField.UINT16 => "uint16 (4)",
                    PointField.INT32 => "int32 (5)",
                    PointField.UINT32 => "uint32 (6)",
                    PointField.FLOAT32 => "float32 (7)",
                    PointField.FLOAT64 => "float64 (8)",
                    _ => $"unknown ({field.Datatype.ToString()})"
                };
                description.Append("Datatype: ").Append(type).AppendLine();
                description.Append("Count: ").Append(field.Count).AppendLine();
                description.AppendLine();
            }
        }

        public void HighlightId(string id)
        {
            // nothing to do
        }
    }
}