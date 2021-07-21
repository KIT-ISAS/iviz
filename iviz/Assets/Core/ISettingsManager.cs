using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Core
{
    public interface ISettingsManager
    {
        QualityType QualityInView { get; set; }
        QualityType QualityInAr { get; set; }
        int NetworkFrameSkip { get; set; }
        int TargetFps { get; set; }
        Color BackgroundColor { get; set; }
        int SunDirection { get; set; }
        [NotNull] SettingsConfiguration Config { get; set; }

        bool SupportsView { get; }
        bool SupportsAR { get; }
        [NotNull] IEnumerable<string> QualityLevelsInView { get; }
        [NotNull] IEnumerable<string> QualityLevelsInAR { get; }
    }
}