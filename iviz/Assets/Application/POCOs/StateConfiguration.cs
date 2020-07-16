using Iviz.App.Listeners;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Iviz.App
{
    [DataContract]
    public sealed class StateConfiguration
    {
        [DataMember] public Uri MasterUri { get; set; } = null;
        [DataMember] public Uri MyUri { get; set; } = null;
        [DataMember] public string MyId { get; set; } = null;

        [DataMember] public List<Guid> Entries { get; set; } = new List<Guid>();

        [DataMember] public TFConfiguration Tf { get; set; } = null;
        [DataMember] public List<GridConfiguration> Grids { get; set; } = new List<GridConfiguration>();
        [DataMember] public List<RobotConfiguration> Robots { get; set; } = new List<RobotConfiguration>();
        [DataMember] public List<PointCloudConfiguration> PointClouds { get; set; } = new List<PointCloudConfiguration>();
        [DataMember] public List<LaserScanConfiguration> LaserScans { get; set; } = new List<LaserScanConfiguration>();
        [DataMember] public List<JointStateConfiguration> JointStates { get; set; } = new List<JointStateConfiguration>();
        [DataMember] public List<ImageConfiguration> Images { get; set; } = new List<ImageConfiguration>();
        [DataMember] public List<MarkerConfiguration> Markers { get; set; } = new List<MarkerConfiguration>();
        [DataMember] public List<InteractiveMarkerConfiguration> InteractiveMarkers { get; set; } = new List<InteractiveMarkerConfiguration>();
        [DataMember] public List<DepthImageProjectorConfiguration> DepthImageProjectors { get; set; } = new List<DepthImageProjectorConfiguration>();
        [DataMember] public List<MagnitudeConfiguration> Odometries { get; set; } = new List<MagnitudeConfiguration>();
        [DataMember] public List<OccupancyGridConfiguration> OccupancyGrids { get; set; } = new List<OccupancyGridConfiguration>();
        [DataMember] public List<PathConfiguration> Paths { get; set; } = new List<PathConfiguration>();
        [DataMember] public List<GridMapConfiguration> GridMaps { get; set; } = new List<GridMapConfiguration>();
        [DataMember] public ARConfiguration AR { get; set; } = null;
        [DataMember] public JoystickConfiguration Joystick { get; set; } = null;

        public List<IReadOnlyList<IConfiguration>> CreateListOfEntries() => new List<IReadOnlyList<IConfiguration>>
        {
            Grids,
            Robots,
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
            new[] { AR },
            new[] { Joystick },
        };

    }
}
