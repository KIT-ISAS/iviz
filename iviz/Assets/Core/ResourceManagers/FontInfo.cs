using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class FontInfo
    {
        readonly Font font;
        readonly int dotWidth;
        readonly int arrowWidth;
        readonly Dictionary<char, int> charWidths = new Dictionary<char, int>();

        public FontInfo()
        {
            font = UnityEngine.Resources.Load<Font>("Fonts/selawk");
            dotWidth = CharWidth('.') * 3; // ...
            arrowWidth = CharWidth('→') + CharWidth(' ');
        }

        [NotNull]
        public string Split([NotNull] string s, int maxWidth, int maxLines = 2)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            int usableWidth = maxWidth - dotWidth;
            StringBuilder str = new StringBuilder();
            int usedWidth = 0;
            int numLines = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int charWidth = CharWidth(s[i]);
                if (usedWidth + charWidth > usableWidth)
                {
                    if (i >= s.Length - 2)
                    {
                        str.Append(s[i]);
                        continue;
                    }

                    if (numLines != maxLines - 1)
                    {
                        str.Append("...\n→ ").Append(s[i]);
                        usedWidth = arrowWidth;
                        numLines = 1;
                    }
                    else
                    {
                        str.Append("...");
                        return str.ToString();
                    }
                }
                else
                {
                    str.Append(s[i]);
                    usedWidth += charWidth;
                }
            }

            return str.ToString();
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