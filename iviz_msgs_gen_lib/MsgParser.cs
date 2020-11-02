using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Iviz.MsgsGen
{
    internal static class MsgParser
    {
        static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        
        internal static readonly Dictionary<string, string> BuiltInsMaps = new Dictionary<string, string>
        {
            {"bool", "bool"},
            {"int8", "sbyte"},
            {"uint8", "byte"},
            {"int16", "short"},
            {"uint16", "ushort"},
            {"int32", "int"},
            {"uint32", "uint"},
            {"int64", "long"},
            {"uint64", "ulong"},
            {"float32", "float"},
            {"float64", "double"},
            {"string", "string"},
            {"time", "time"},
            {"duration", "duration"},
            {"char", "sbyte"},
            {"byte", "byte"},
        };

        internal static string Sanitize(string name)
        {
            StringBuilder str = new StringBuilder();
            str.Append(char.ToUpper(name[0], Culture));
            for (int i = 1; i < name.Length; i++)
            {
                if (name[i] == '_' && i != name.Length - 1)
                {
                    str.Append(char.ToUpper(name[i + 1], Culture));
                    i++;
                }
                else
                {
                    str.Append(name[i]);
                }
            }

            return str.ToString();
        }


        static bool IsIdentifierLetter(char c)
        {
            return c == '[' || c == ']' || char.IsLetterOrDigit(c) || c == '_' || c == '/' || c == '-';
        }

        static bool IsWhitespace(char c)
        {
            return char.IsWhiteSpace(c);
        }

        internal static List<IElement> ParseFile(IEnumerable<string> lines, string className)
        {
            List<IElement> elements = new List<IElement>();

            foreach (string line in lines)
            {
                if (line == "---")
                {
                    elements.Add(new ServiceSeparatorElement());
                    continue;
                }

                int index = 0;
                List<string> terms = new List<string>();
                while (index < line.Length)
                {
                    if (IsWhitespace(line[index]))
                    {
                        while (index < line.Length && IsWhitespace(line[index]))
                        {
                            index++;
                        }
                    }
                    else if (IsIdentifierLetter(line[index]))
                    {
                        int indexId = index;
                        while (indexId < line.Length && IsIdentifierLetter(line[indexId]))
                        {
                            indexId++;
                        }

                        terms.Add(line.Substring(index, indexId - index));
                        index = indexId;
                    }
                    else if (line[index] == '=')
                    {
                        terms.Add("=");
                        index++;
                    }
                    else if (line[index] == '#')
                    {
                        terms.Add(line.Substring(index));
                        index = line.Length;
                    }
                    else if (line[index] == '\"')
                    {
                        int end = line.IndexOf('\"', index + 1);
                        if (end == -1)
                        {
                            index = line.Length;
                        }
                        else
                        {
                            terms.Add(line.Substring(index, end + 1 - index));
                            index = end + 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"** Unknown symbol '{line[index]}'");
                        index++;
                    }
                }

                switch (terms.Count)
                {
                    case 0:
                        elements.Add(new EmptyElement());
                        continue;
                    case 1:
                        // # comment
                        if (terms[0][0] == '#')
                        {
                            elements.Add(new CommentElement(terms[0].Substring(1)));
                            continue;
                        }

                        break;
                    case 2:
                        // class identifier
                        if (terms[0] != "=" && terms[1] != "=" && terms[1][0] != '#')
                        {
                            elements.Add(new VariableElement("", terms[0], terms[1], className));
                            continue;
                        }

                        break;
                    case 3:
                        // class identifier # comment
                        if (terms[0] != "=" && terms[1] != "=" && terms[2][0] == '#')
                        {
                            elements.Add(new VariableElement(terms[2].Substring(1), terms[0], terms[1], className));
                            continue;
                        }

                        break;
                    case 4:
                        // class identifier = constant
                        if (terms[0] != "=" && terms[1] != "=" && terms[2] == "=" && terms[3][0] != '#')
                        {
                            elements.Add(new ConstantElement("", terms[0], terms[1], terms[3]));
                            continue;
                        }

                        break;
                    case 5:
                        // class identifier = constant # comment
                        if (terms[0] != "=" && terms[1] != "=" && terms[2] == "=" && terms[3][0] != '#' &&
                            terms[4][0] == '#')
                        {
                            elements.Add(new ConstantElement(terms[4].Substring(1), terms[0], terms[1], terms[3]));
                            continue;
                        }

                        break;
                }

                elements.Add(new InvalidElement(line));
            }

            return elements;
        }
    }
}