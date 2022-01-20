using System;
using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    internal static class MsgParser
    {
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

        internal static string CsIfy(string name)
        {
            return BuiltIns.RosNameToCs(name);
        }


        static bool IsIdentifierLetter(char c)
        {
            return char.IsLetterOrDigit(c) || c is '[' or ']' or '_' or '/' or '-';
        }

        static bool IsWhiteSpace(char c)
        {
            return char.IsWhiteSpace(c);
        }

        internal static List<IElement> ParseFile(IEnumerable<string> lines, string className)
        {
            List<IElement> elements = new List<IElement>();

            foreach (string line in lines)
            {
                if (line.TrimEnd() == "---")
                {
                    elements.Add(new ServiceSeparatorElement());
                    continue;
                }

                int index = 0;
                List<string> terms = new List<string>();
                while (index < line.Length)
                {
                    if (IsWhiteSpace(line[index]))
                    {
                        while (index < line.Length && IsWhiteSpace(line[index]))
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
                        terms.Add(line[index..]);
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

                // special case for string constants
                if (terms.Count >= 3 && terms[0] == "string" && terms[1] != "=" && terms[2][0] == '=')
                {
                    int eqPosition = line.IndexOf("=", StringComparison.InvariantCulture);
                    string constStr = line[(eqPosition + 1)..].Trim().Replace("\"", "\\\"");
                    elements.Add(new ConstantElement("", "string", terms[1], constStr));
                    continue;
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