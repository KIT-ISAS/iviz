#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class FontInfo
    {
        readonly Font font;
        readonly int dotWidth;
        readonly int spaceWidth;
        readonly Dictionary<char, int> charWidths = new();

        public FontInfo()
        {
            font = Resource.Extras.AssetHolder.BaseFont.AssertNotNull(nameof(font));
            dotWidth = CharWidth('.') * 3; // ...
            spaceWidth = CharWidth(' ');
        }

        public string Split(string str, int maxWidth, int maxLines = 2)
        {
            ThrowHelper.ThrowIfNull(str, nameof(str));

            int usableWidth = maxWidth - dotWidth;

            int totalWidth = str.Sum(CharWidth);

            if (totalWidth <= usableWidth)
            {
                return str;
            }

            using var description = BuilderPool.Rent();
            int usedWidth = 0;
            int numLines = 0;
            foreach (int i in ..str.Length)
            {
                int charWidth = CharWidth(str[i]);
                if (usedWidth + charWidth > usableWidth)
                {
                    if (i >= str.Length - 2)
                    {
                        description.Append(str[i]);
                        continue;
                    }

                    if (numLines != maxLines - 1)
                    {
                        description.Append("...\n  ").Append(str[i]);
                        usedWidth = 2 * spaceWidth;
                        numLines = 1;
                    }
                    else
                    {
                        description.Append("...");
                        return description.ToString();
                    }
                }
                else
                {
                    description.Append(str[i]);
                    usedWidth += charWidth;
                }
            }

            return description.ToString();
        }

        int CharWidth(char c)
        {
            if (charWidths.TryGetValue(c, out int width))
            {
                return width;
            }

            font.GetCharacterInfo(c, out CharacterInfo ci, 12);
            charWidths[c] = ci.advance;
            return ci.advance;
        }
    }
}