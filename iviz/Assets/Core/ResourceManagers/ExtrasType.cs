using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class ExtrasType
    {
        public AssetHolder AssetHolder { get; }
        public AppAssetHolder AppAssetHolder { get; }
        public WidgetAssetHolder WidgetAssetHolder { get; }
        public AudioAssetHolder AudioAssetHolder { get; }

        public ExtrasType()
        {
            AssetHolder = UnityEngine.Resources.Load<GameObject>("Asset Holder")
                .AssertHasComponent<AssetHolder>(nameof(AssetHolder));
            AppAssetHolder = UnityEngine.Resources.Load<GameObject>("App Asset Holder")
                .AssertHasComponent<AppAssetHolder>(nameof(AppAssetHolder));
            WidgetAssetHolder = UnityEngine.Resources.Load<GameObject>("Widget Asset Holder")
                .AssertHasComponent<WidgetAssetHolder>(nameof(WidgetAssetHolder));
            AudioAssetHolder = UnityEngine.Resources.Load<GameObject>("Audio Asset Holder")
                .AssertHasComponent<AudioAssetHolder>(nameof(AudioAssetHolder));
        }
    }
}