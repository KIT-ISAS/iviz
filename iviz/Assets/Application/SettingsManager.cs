#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Core.Configurations;

namespace Iviz.App
{
    public sealed class SettingsManager : ISettingsManager
    {
        static readonly string[] QualityInViewOptions =
            { "Very Low", "Low", "Medium", "High", "Very High", "Ultra" };

        static readonly string[] QualityInArOptions = { "Very Low", "Low", "Medium", "High", "Very High", "Ultra" };

        static GuiInputModule GuiInputModule => GuiInputModule.Instance;
        readonly SettingsConfiguration config = new();

        public QualityType QualityInAr
        {
            get => config.QualityInAr;
            set
            {
                config.QualityInAr = ValidateQuality(value);
                if (!ARController.IsActive)
                {
                    return;
                }

                GuiInputModule.QualityType = config.QualityInAr;
            }
        }

        public QualityType QualityInView
        {
            get => config.QualityInView;
            set
            {
                config.QualityInView = ValidateQuality(value);

                var qualityToUse = Settings.IsHololens ? QualityType.VeryLow : config.QualityInView;

                if (ARController.IsActive)
                {
                    return;
                }

                GuiInputModule.QualityType = qualityToUse;
            }
        }

        public int NetworkFrameSkip
        {
            get => config.NetworkFrameSkip;
            set
            {
                config.NetworkFrameSkip = value;
                GuiInputModule.NetworkFrameSkip = value;
            }
        }

        public int TargetFps
        {
            get => config.TargetFps;
            set
            {
                config.TargetFps = value;
                GuiInputModule.TargetFps = value;
            }
        }

        public SettingsConfiguration Config
        {
            get => config;
            set
            {
                NetworkFrameSkip = value.NetworkFrameSkip;
                QualityInAr = value.QualityInAr;
                QualityInView = value.QualityInView;
                TargetFps = value.TargetFps;
            }
        }

        public IEnumerable<string> QualityLevelsInView => QualityInViewOptions;

        public IEnumerable<string> QualityLevelsInAR => QualityInArOptions;

        public SettingsManager()
        {
            Config = Config;
        }
        
        public void UpdateQualityLevel()
        {
            QualityInAr = QualityInAr;
            QualityInView = QualityInView;
        }        
        
        static QualityType ValidateQuality(QualityType qualityType)
        {
            return qualityType switch
            {
                < QualityType.VeryLow => QualityType.VeryLow,
                > QualityType.Ultra => QualityType.Ultra,
                _ => qualityType
            };
        }
    }
}