#nullable enable

using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class SettingsConfiguration : JsonToString
    {
        [DataMember] public QualityType QualityInView { get; set; } = QualityType.Ultra;
        [DataMember] public QualityType QualityInAr { get; set; } = QualityType.Low;
        
        /// How many frames to skip when processing data from the ROS network.
        /// 1 means to process in every frame, 2 means every second frame, and so on.  
        [DataMember] public int NetworkFrameSkip { get; set; } = 1; 
        
        /// The target FPS, or -1 for the Unity default for the current platform. 
        [DataMember] public int TargetFps { get; set; } = -1;
    }
}