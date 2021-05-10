using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using JetBrains.Annotations;

namespace Iviz.App
{
    [DataContract]
    public sealed class StateConfiguration
    {
        [DataMember] public List<string> Entries { get; set; } = new List<string>();

        [DataMember] public TfConfiguration Tf { get; set; }
        [DataMember] public List<GridConfiguration> Grids { get; set; } = new List<GridConfiguration>();
        [DataMember] public List<RobotConfiguration> SimpleRobots { get; set; } = new List<RobotConfiguration>();
        [DataMember] public List<PointCloudConfiguration> PointClouds { get; set; } = new List<PointCloudConfiguration>();
        [DataMember] public List<LaserScanConfiguration> LaserScans { get; set; } = new List<LaserScanConfiguration>();
        [DataMember] public List<JointStateConfiguration> JointStates { get; set; } = new List<JointStateConfiguration>();
        [DataMember] public List<ImageConfiguration> Images { get; set; } = new List<ImageConfiguration>();
        [DataMember] public List<MarkerConfiguration> Markers { get; set; } = new List<MarkerConfiguration>();
        [DataMember] public List<InteractiveMarkerConfiguration> InteractiveMarkers { get; set; } = new List<InteractiveMarkerConfiguration>();
        [DataMember] public List<DepthCloudConfiguration> DepthImageProjectors { get; set; } = new List<DepthCloudConfiguration>();
        [DataMember] public List<MagnitudeConfiguration> Odometries { get; set; } = new List<MagnitudeConfiguration>();
        [DataMember] public List<OccupancyGridConfiguration> OccupancyGrids { get; set; } = new List<OccupancyGridConfiguration>();
        [DataMember] public List<PathConfiguration> Paths { get; set; } = new List<PathConfiguration>();
        [DataMember] public List<GridMapConfiguration> GridMaps { get; set; } = new List<GridMapConfiguration>();
        [DataMember] public List<OctomapConfiguration> Octomaps { get; set; } = new List<OctomapConfiguration>();
        [DataMember] public List<GuiDialogConfiguration> Dialogs { get; set; } = new List<GuiDialogConfiguration>();
        [DataMember] public ARConfiguration AR { get; set; }
        [DataMember] public JoystickConfiguration Joystick { get; set; }

        [NotNull]
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
