#nullable enable

using System.Collections.Generic;
using Iviz.Common;
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
        SettingsConfiguration Config { get; set; }

        IEnumerable<string> QualityLevelsInView { get; }
        IEnumerable<string> QualityLevelsInAR { get; }
    }
}