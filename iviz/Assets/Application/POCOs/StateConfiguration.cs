#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Controllers.XR;

namespace Iviz.App
{
    [DataContract]
    public sealed class StateConfiguration
    {
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
        [DataMember] public List<GuiWidgetConfiguration> Dialogs { get; set; } = new();
        [DataMember] public TfPublisherConfiguration TfPublisher { get; set; } = new();
        [DataMember] public CameraConfiguration Camera { get; set; } = new();
        [DataMember] public ARConfiguration? AR { get; set; }
        [DataMember] public JoystickConfiguration? Joystick { get; set; }
        [DataMember] public XRConfiguration? XR { get; set; }

        public IEnumerable<IConfiguration> CreateListOfEntries() => new[]
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
            CreateSingleton(TfPublisher),
            CreateSingleton(AR),
            CreateSingleton(Joystick),
            CreateSingleton(XR),
        }.SelectMany(config => config);

        static IEnumerable<IConfiguration> CreateSingleton(IConfiguration? config) =>
            config != null ? new[] { config } : Enumerable.Empty<IConfiguration>();
    }
}