﻿#nullable enable

using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class LaserScanListener : ListenerController
    {
        readonly RadialScanResource resource;
        readonly FrameNode node;
        readonly LaserScanConfiguration config = new();

        public override TfFrame? Frame => node.Parent;

        public Vector2 MeasuredIntensityBounds => resource.MeasuredIntensityBounds;

        public int Size => resource.Size;

        public LaserScanConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                PointSize = value.PointSize;
                Colormap = value.Colormap;
                UseIntensity = value.UseIntensity;
                UseLines = value.UseLines;
                ForceMinMax = value.ForceMinMax;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                resource.Visible = value;
            }
        }

        public float PointSize
        {
            get => config.PointSize;
            set
            {
                config.PointSize = value;
                resource.PointSize = value;
            }
        }

        public ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                resource.Colormap = value;
            }
        }

        public bool UseIntensity
        {
            get => config.UseIntensity;
            set
            {
                config.UseIntensity = value;
                resource.UseIntensityNotRange = value;
            }
        }

        public bool ForceMinMax
        {
            get => config.ForceMinMax;
            set
            {
                config.ForceMinMax = value;
                resource.OverrideMinMax = value;
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                resource.FlipMinMax = value;
            }
        }


        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                resource.MinIntensity = value;
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                resource.MaxIntensity = value;
            }
        }

        public bool UseLines
        {
            get => config.UseLines;
            set
            {
                config.UseLines = value;
                resource.UseLines = value;
            }
        }

        public override IListener Listener { get; }

        public LaserScanListener(LaserScanConfiguration? config, string topic)
        {
            Config = config ?? new LaserScanConfiguration
            {
                Topic = topic,
            };

            Listener = new Listener<LaserScan>(Config.Topic, Handler, RosTransportHint.PreferUdp);
            node = new FrameNode($"[{Config.Topic}]");
            resource = ResourcePool.RentDisplay<RadialScanResource>(node.Transform);
        }

        void Handler(LaserScan msg)
        {
            node.AttachTo(msg.Header);

            if (float.IsNaN(msg.AngleMin) || float.IsNaN(msg.AngleMax) ||
                float.IsNaN(msg.RangeMin) || float.IsNaN(msg.RangeMax) ||
                float.IsNaN(msg.AngleIncrement))
            {
                RosLogger.Info($"{this}: NaN in header!");
                return;
            }

            if (msg.AngleMin >= msg.AngleMax || msg.RangeMin >= msg.RangeMax)
            {
                RosLogger.Info($"{this}: Invalid angle or range dimensions!");
                return;
            }

            if (msg.Intensities.Length != 0 && msg.Intensities.Length != msg.Ranges.Length)
            {
                RosLogger.Info($"{this}: Invalid intensities length!");
                return;
            }

            try
            {
                resource.Set(msg.AngleMin, msg.AngleIncrement, msg.RangeMin, msg.RangeMax, msg.Ranges, msg.Intensities);
            }
            finally
            {
                msg.Ranges.TryReturn();
                msg.Intensities.TryReturn();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            resource.ReturnToPool();
            node.Dispose();
        }
    }
}