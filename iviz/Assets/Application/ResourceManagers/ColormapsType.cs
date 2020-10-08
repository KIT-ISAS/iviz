using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            Dictionary<Resource.ColormapId, Texture2D> textures = new Dictionary<Resource.ColormapId, Texture2D>();
            for (int i = 0; i < Names.Count; i++)
            {
                textures[(Resource.ColormapId) i] = UnityEngine.Resources.Load<Texture2D>("Colormaps/" + Names[i]);
            }

            Textures = new ReadOnlyDictionary<Resource.ColormapId, Texture2D>(textures);
        }
    }
}