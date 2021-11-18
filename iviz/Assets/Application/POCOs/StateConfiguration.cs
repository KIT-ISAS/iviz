﻿#nullable enable

using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;

namespace Iviz.App
{
    [DataContract]
    public sealed class StateConfiguration
    {
        [DataMember] public List<string> Entries { get; set; } = new();
        [DataMember] public TfConfiguration Tf { get; set; } = new();
        [DataMember] public List<GridConfiguration> Grids { get; set; } = new();
        [DataMember] public List<RobotConfiguration> SimpleRobots { get; set; } = new();
        [DataMember] public List<PointCloudConfiguration> PointClouds { get; set; } = new();
        [DataMember] public List<LaserScanConfiguration> LaserScans { get; set; } = new();
        [DataMember] public List<JointStateConfiguration> JointStates { get; set; } = new();
        [DataMember] public List<ImageConfiguration> Images { get; set; } = new();
        [DataMember] public List<MarkerConfiguration> Markers { get; set; } = new();
        [DataMember] public List<InteractiveMarkerConfiguration> InteractiveMarkers { get; set; } = new();
        [DataMember] public List<DepthCloudConfiguration> DepthImageProjectors { get; set; } = new();
        [DataMember] public List<MagnitudeConfiguration> Odometries { get; set; } = new();
        [DataMember] public List<OccupancyGridConfiguration> OccupancyGrids { get; set; } = new();
        [DataMember] public List<PathConfiguration> Paths { get; set; } = new();
        [DataMember] public List<GridMapConfiguration> GridMaps { get; set; } = new();
        [DataMember] public List<OctomapConfiguration> Octomaps { get; set; } = new();
        [DataMember] public List<GuiDialogConfiguration> Dialogs { get; set; } = new();
        [DataMember] public ARConfiguration AR { get; set; } = new();
        [DataMember] public JoystickConfiguration Joystick { get; set; } = new();

        public IEnumerable<IEnumerable<IConfiguration>> CreateListOfEntries() => new IEnumerable<IConfiguration>[]
        {
            Grids,
            SimpleRobots,
            PointClouds,
            LaserScans,
            JointStates,
            Images,
            Markers,
            InteractiveMarkers,
            DepthImageProjectors,
            Odometries,
            OccupancyGrids,
            Paths,
            GridMaps,
            Octomaps,
            Dialogs,
            new[] { AR },
            new[] { Joystick },
        };

    }
}
