using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{

    public class AnchorCanvas : MonoBehaviour
    {
        public RawImage Anchor_NW;
        public RawImage Anchor_SW;
        public RawImage Anchor_NE;
        public RawImage Anchor_SE;

        public static readonly List<string> AnchorNames = new List<string> { "None", "NW", "NE", "SW", "SE" };

        [JsonConverter(typeof(StringEnumConverter))]
        public enum AnchorType
        {
            None, NW, SW, NE, SE
        }

        public RawImage ImageFromAnchorType(AnchorType type)
        {
            switch(type)
            {
                default: return null;
                case AnchorType.NW: return Anchor_NW;
                case AnchorType.SW: return Anchor_SW;
                case AnchorType.NE: return Anchor_NE;
                case AnchorType.SE: return Anchor_SE;
            }
        }

    }
}
 