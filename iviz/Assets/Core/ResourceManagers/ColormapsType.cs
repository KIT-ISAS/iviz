using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Displays;
using Iviz.Msgs.IvizCommonMsgs;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class ColormapsType
    {
        public const int Size = 14;
        public const int AtlasSize = 16;
        public ReadOnlyDictionary<ColormapId, Texture2D> Textures { get; }
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
            Dictionary<ColormapId, Texture2D> textures = new Dictionary<ColormapId, Texture2D>()
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

            Textures = new ReadOnlyDictionary<ColormapId, Texture2D>(textures);
        }
    }
}