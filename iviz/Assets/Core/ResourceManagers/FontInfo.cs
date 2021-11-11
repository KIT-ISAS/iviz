using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Core;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class FontInfo
    {
        readonly Font font;
        readonly int dotWidth;
        readonly int arrowWidth;
        readonly Dictionary<char, int> charWidths = new();

        public FontInfo()
        {
            font = UnityEngine.Resources.Load<Font>("Fonts/selawk");
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

            int totalWidth = str.Sum(CharWidth);

            if (totalWidth <= usableWidth)
            {
                return str;
            }

            var description = BuilderPool.Rent();
            try
            {
                description.Length = 0;
                int usedWidth = 0;
                int numLines = 0;
                for (int i = 0; i < str.Length; i++)
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
                            description.Append("...\n→ ").Append(str[i]);
                            usedWidth = arrowWidth;
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
            finally
            {
                BuilderPool.Return(description);
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