using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class FontInfo
    {
        static readonly StringBuilder CachedStr = new StringBuilder(50);

        readonly Font font;
        readonly int dotWidth;
        readonly int arrowWidth;
        readonly Dictionary<char, int> charWidths = new Dictionary<char, int>();

        public FontInfo()
        {
            font = UnityEngine.Resources.Load<Font>("Fonts/selawk base");
            dotWidth = CharWidth('.') * 3; // ...
            arrowWidth = CharWidth('→') + CharWidth(' ');
        }

        [NotNull]
        public string Split([NotNull] string str, int maxWidth, int maxLines = 2)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            int usableWidth = maxWidth - dotWidth;
            
            int totalWidth = 0;
            foreach (var c in str)
            {
                totalWidth += CharWidth(c);
            }

            if (totalWidth <= usableWidth)
            {
                return str;
            }

            CachedStr.Length = 0;
            int usedWidth = 0;
            int numLines = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int charWidth = CharWidth(str[i]);
                if (usedWidth + charWidth > usableWidth)
                {
                    if (i >= str.Length - 2)
                    {
                        CachedStr.Append(str[i]);
                        continue;
                    }

                    if (numLines != maxLines - 1)
                    {
                        CachedStr.Append("...\n→ ").Append(str[i]);
                        usedWidth = arrowWidth;
                        numLines = 1;
                    }
                    else
                    {
                        CachedStr.Append("...");
                        return CachedStr.ToString();
                    }
                }
                else
                {
                    CachedStr.Append(str[i]);
                    usedWidth += charWidth;
                }
            }

            return CachedStr.ToString();
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