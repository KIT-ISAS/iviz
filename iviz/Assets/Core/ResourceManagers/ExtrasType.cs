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

        public ExtrasType()
        {
            AssetHolder = UnityEngine.Resources.Load<GameObject>("Asset Holder")
                .GetComponent<AssetHolder>().AssertNotNull(nameof(AssetHolder));
            AppAssetHolder = UnityEngine.Resources.Load<GameObject>("App Asset Holder")
                .GetComponent<AppAssetHolder>().AssertNotNull(nameof(AppAssetHolder));
            WidgetAssetHolder = UnityEngine.Resources.Load<GameObject>("Widget Asset Holder")
                .GetComponent<WidgetAssetHolder>().AssertNotNull(nameof(WidgetAssetHolder));
        }
    }
}