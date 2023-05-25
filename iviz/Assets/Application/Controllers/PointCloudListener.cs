#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays;
using Iviz.Displays.Helpers;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class PointCloudListener : ListenerController
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

        public PointCloudListener(PointCloudConfiguration? config, string topic)
        {
            processor = new PointCloudProcessor();
            Config = config ?? new PointCloudConfiguration
            {
                Topic = topic,
            };
            
            processor.Node.Name = Config.Topic;
            
            Listener = new Listener<PointCloud2>(Config.Topic, processor.Handle);
            processor.IsProcessingChanged += Listener.SetPause;
        }

        public override void Dispose()
        {
            base.Dispose();
            processor.Dispose();
        }
    }
}