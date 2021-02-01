using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class ColormapsType
    {
        public const int Size = 14;
        public const int AtlasSize = 16;
        public ReadOnlyDictionary<Resource.ColormapId, Texture2D> Textures { get; }
        public ReadOnlyCollection<string> Names { get; }
        
        public ColormapsType()
        {
            string[] names =
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
            Names = new ReadOnlyCollection<string>(names);

            var assetHolder = UnityEngine.Resources.Load<GameObject>("Asset Holder").GetComponent<AssetHolder>();
            Dictionary<Resource.ColormapId, Texture2D> textures = new Dictionary<Resource.ColormapId, Texture2D>()
            {
                [Resource.ColormapId.autumn] = assetHolder.Autumn,
                [Resource.ColormapId.bone] = assetHolder.Bone,
                [Resource.ColormapId.cool] = assetHolder.Cool,
                [Resource.ColormapId.copper] = assetHolder.Copper,
                [Resource.ColormapId.gray] = assetHolder.Gray,
                [Resource.ColormapId.hot] = assetHolder.Hot,
                [Resource.ColormapId.hsv] = assetHolder.Hsv,
                [Resource.ColormapId.jet] = assetHolder.Jet,
                [Resource.ColormapId.lines] = assetHolder.Lines,
                [Resource.ColormapId.parula] = assetHolder.Parula,
                [Resource.ColormapId.pink] = assetHolder.Pink,
                [Resource.ColormapId.spring] = assetHolder.Spring,
                [Resource.ColormapId.summer] = assetHolder.Summer,
                [Resource.ColormapId.winter] = assetHolder.Winter,
            };

            Textures = new ReadOnlyDictionary<Resource.ColormapId, Texture2D>(textures);
        }
    }
}