#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class ColormapsType
    {
        public const int AtlasSize = 16;
        public IReadOnlyDictionary<ColormapId, Texture2D> Textures { get; }
        public string[] Names { get; }
        
        public ColormapsType()
        {
            Names = new[]
            {
                "lines",
                "pink",
                "copper",
                "bone",
                "gray",
                "winter",
                "autumn",
                "summer",
                "spring",
                "cool",
                "hot",
                "hsv",
                "jet",
                "parula"
            };

            var assetHolder = ResourcePool.AssetHolder;
            Textures = new Dictionary<ColormapId, Texture2D>()
            {
                [ColormapId.autumn] = assetHolder.Autumn,
                [ColormapId.bone] = assetHolder.Bone,
                [ColormapId.cool] = assetHolder.Cool,
                [ColormapId.copper] = assetHolder.Copper,
                [ColormapId.gray] = assetHolder.Gray,
                [ColormapId.hot] = assetHolder.Hot,
                [ColormapId.hsv] = assetHolder.Hsv,
                [ColormapId.jet] = assetHolder.Jet,
                [ColormapId.lines] = assetHolder.Lines,
                [ColormapId.parula] = assetHolder.Parula,
                [ColormapId.pink] = assetHolder.Pink,
                [ColormapId.spring] = assetHolder.Spring,
                [ColormapId.summer] = assetHolder.Summer,
                [ColormapId.winter] = assetHolder.Winter,
            };
        }
    }
}