#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class FontInfo
    {
        readonly Font font;
        readonly int dotsWidth;
        readonly Dictionary<char, int> charWidths = new();

        public FontInfo()
        {
            font = ResourcePool.AssetHolder.BaseFont.AssertNotNull(nameof(font));
            dotsWidth = CharWidth('.') * 3;
        }

        /// <summary>
        /// Splits a string into multiple lines that fit into maxWidth. 
        /// </summary>
        public string Split(string str, int maxWidth, int maxLines = 2)
        {
            using var description = BuilderPool.Rent();
            Split(description, str, maxWidth, maxLines);
            return description.Length == str.Length ? str : description.ToString();
        }

        public void Split(StringBuilder description, string str, int maxWidth, int maxLines = 2)
        {
            ThrowHelper.ThrowIfNull(str, nameof(str));
            if (str.Length == 0)
            {
                return;
            }

            int usableWidth = maxWidth - dotsWidth;

            // quick check to see if we need the split at all
            int totalWidth = 0;
            foreach (char c in str)
            {
                totalWidth += CharWidth(c);
            }
            
            if (totalWidth <= usableWidth)
            {
                description.Append(str);
                return;
            }

            int usedWidth = 0;
            int numLines = 0;
            int? lastSeparator = null;

            for (int i = 0; i < str.Length; i++)
            {
                int charWidth = CharWidth(str[i]);

                // do we have enough space? append it
                if (usedWidth + charWidth <= usableWidth)
                {
                    description.Append(str[i]);
                    usedWidth += charWidth;

                    if (str[i] is ' ' or '_' or '-' or '/')
                    {
                        lastSeparator = i;
                    }

                    continue;
                }

                // are we almost there? just append it
                if (i >= str.Length - 2)
                {
                    description.Append(str[i]);
                    continue;
                }

                // are we out of lines? bail out
                if (numLines == maxLines - 1)
                {
                    description.Append("...");
                    return;
                }

                if (lastSeparator is { } separator && i - lastSeparator < 6)
                {
                    description.Length = separator + 1;
                    description.Append("\n");
                    i = separator;
                }
                else
                {
                    description.Append("...\n").Append(str[i]);
                }

                usedWidth = 0;
                lastSeparator = null;
                numLines++;
            }
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