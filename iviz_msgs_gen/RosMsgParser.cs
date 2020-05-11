using System;
using System.Collections.Generic;
using System.Linq;

namespace Iviz.MsgsGen
{
    public class MsgParser
    {
        public static readonly Dictionary<string, string> BuiltInsMaps = new Dictionary<string, string>
            {
                { "bool", "bool" },
                { "int8", "sbyte" },
                { "uint8", "byte" },
                { "int16", "short" },
                { "uint16", "ushort" },
                { "int32", "int" },
                { "uint32", "uint" },
                { "int64", "long" },
                { "uint64", "ulong" },
                { "float32", "float" },
                { "float64", "double" },
                { "string", "string" },
                { "time", "time" },
                { "duration", "duration" },
                { "char", "sbyte" },
                { "byte", "byte" },
            };

        public static readonly HashSet<string> BuiltIns = new HashSet<string>(BuiltInsMaps.Values);

        public enum ElementType
        {
            Empty,
            Comment,
            Variable,
            Constant,
            Invalid,
            ServiceSeparator
        }

        public interface IElement
        {
            ElementType Type { get; }
            string ToCString(bool forceUnroll = false);
        }

        public class Empty : IElement
        {
            public ElementType Type => ElementType.Empty;

            public override string ToString()
            {
                return "[]";
            }

            public string ToCString(bool forceUnroll)
            {
                return "";
            }
        }

        public class Comment : IElement
        {
            public ElementType Type => ElementType.Comment;

            public readonly string text;

            public Comment(string text)
            {
                this.text = text;
            }

            public override string ToString()
            {
                return "[# '" + text + "']";
            }

            public string ToCString(bool forceUnroll)
            {
                return "//" + text;
            }
        }

        public class Variable : IElement
        {
            public ElementType Type => ElementType.Variable;

            public readonly string comment;
            public readonly string classToken;
            public readonly string rosFieldName;
            public readonly string rosClassName;

            public readonly string fieldName;
            public readonly string className;

            public readonly int arraySize;

            public ClassInfo classInfo;

            static readonly HashSet<string> Keywords = new HashSet<string>
            {
                "default",
                "byte",
                "sbyte",
                "char",
                "short",
                "ushort",
                "int",
                "uint",
                "float",
                "double",
                "long",
                "ulong",
                "bool",
                "string",
                "readonly",
                "override",
            };

            public Variable(string comment, string classToken, string fieldName)
            {
                this.comment = comment;
                this.classToken = classToken;

                rosFieldName = fieldName;
                this.fieldName = fieldName;

                if (Keywords.Contains(fieldName))
                {
                    this.fieldName = "@" + fieldName;
                }

                int bracketLeft = classToken.IndexOf('[');
                int bracketRight = classToken.IndexOf(']');
                if (bracketLeft != -1 && bracketRight != -1)
                {
                    rosClassName = classToken.Substring(0, bracketLeft);
                    if (bracketRight - bracketLeft == 1)
                    {
                        arraySize = 0;
                    }
                    else
                    {
                        arraySize = int.Parse(classToken.Substring(bracketLeft + 1, bracketRight - bracketLeft - 1));
                    }
                }
                else
                {
                    rosClassName = classToken;
                    arraySize = -1;
                }

                if (rosClassName == "Header")
                {
                    rosClassName = "std_msgs/Header";
                    className = "std_msgs.Header";
                }
                else if (BuiltInsMaps.TryGetValue(rosClassName, out className))
                {
                    //
                }
                else
                {
                    className = rosClassName.Replace('/', '.');
                }

            }

            public override string ToString()
            {
                return "['" + classToken + "' '" + rosFieldName + "' # '" + comment + "']";
            }

            public string ToCString(bool forceUnroll)
            {
                string result;
                if (arraySize == -1)
                {
                    if (forceUnroll)
                    {
                        result = "public " + className + " " + fieldName + " { get; }";
                    } else
                    {
                        result = "public " + className + " " + fieldName + " { get; set; }";
                    }
                }
                else if (arraySize == 0)
                {
                    result = "public " + className + "[] " + fieldName + " { get; set; }";
                }
                else
                {
                    
                    //if (forceUnroll)
                    //{
                    //    result = "public " + className + " " + string.Join(", ", Enumerable.Range(0, arraySize).Select(x => fieldName + "_" + x)) + ";";
                    //}
                    //else
                    //{
                        result = "public " + className + "[/*" + arraySize + "*/] " + fieldName + " { get; set; }";
                    //}
                }
                if (comment != "")
                {
                    result += " //" + comment;
                }
                return result;
            }

            public string GetMd5Entry()
            {
                if (classInfo != null)
                {
                    return classInfo.GetMd5() + " " + rosFieldName;
                }
                else
                {
                    if (arraySize == -1)
                    {
                        return rosClassName + " " + rosFieldName;
                    }
                    else if (arraySize == 0)
                    {
                        return rosClassName + "[] " + rosFieldName;
                    }
                    else
                    {
                        return rosClassName + "[" + arraySize + "] " + rosFieldName;
                        //return rosClassName + "[] " + rosFieldName;
                    }
                }
            }
        }

        public class Constant : IElement
        {
            public ElementType Type => ElementType.Constant;

            public readonly string comment;
            public readonly string className;
            public readonly string fieldName;
            public readonly string value;

            public Constant(string comment, string className, string fieldName, string value)
            {
                this.comment = comment;
                this.className = className;
                this.fieldName = fieldName;
                this.value = value;
            }

            public override string ToString()
            {
                return "['" + className + "' '" + fieldName + "' = '" + value + "' # '" + comment + "']";
            }

            public string ToCString(bool forceUnroll)
            {
                string result = "";
                if (BuiltInsMaps.TryGetValue(className, out string alias))
                {
                    if (alias != "time" && alias != "duration")
                    {
                        result = "public const " + alias + " " + fieldName + " = " + value + ";";
                    }
                }
                if (comment != "")
                {
                    result += " //" + comment;
                }
                return result;
            }

            public string ToMd5String()
            {
                return className + " " + fieldName + "=" + value;
            }
        }

        public class Invalid : IElement
        {
            public ElementType Type => ElementType.Invalid;

            public readonly string text;

            public Invalid(string text)
            {
                Console.WriteLine("Adding invalid '" + text + "'");
                this.text = text;
            }

            public override string ToString()
            {
                return "[XXX '" + text + "']";
            }

            public string ToCString(bool forceUnroll)
            {
                return "";
            }
        }

        public class ServiceSeparator : IElement
        {
            public ElementType Type => ElementType.ServiceSeparator;

            public override string ToString()
            {
                return "[---]";
            }

            public string ToCString(bool forceUnroll)
            {
                return "";
            }
        }


        static bool IsIdentifierLetter(char c)
        {
            return c == '[' || c == ']' || char.IsLetterOrDigit(c) || c == '_' || c == '/' || c == '-';
        }

        static bool IsWhitespace(char c)
        {
            return char.IsWhiteSpace(c);
        }

        public static List<IElement> ParseFile(string[] lines)
        {
            List<IElement> elements = new List<IElement>();

            foreach (string line in lines)
            {
                if (line == "---")
                {
                    elements.Add(new ServiceSeparator());
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
                        int index_id = index;
                        while (index_id < line.Length && IsIdentifierLetter(line[index_id]))
                        {
                            index_id++;
                        }
                        terms.Add(line.Substring(index, index_id - index));
                        index = index_id;
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
                        Console.WriteLine("Unknown symbol '" + line[index] + "'");
                        index++;
                    }
                }

                switch (terms.Count)
                {
                    case 0:
                        elements.Add(new Empty());
                        continue;
                    case 1:
                        // # comment
                        if (terms[0][0] == '#')
                        {
                            elements.Add(new Comment(terms[0].Substring(1)));
                            continue;
                        }
                        break;
                    case 2:
                        // class identifier
                        if (terms[0] != "=" && terms[1] != "=" && terms[1][0] != '#')
                        {
                            elements.Add(new Variable("", terms[0], terms[1]));
                            continue;
                        }
                        break;
                    case 3:
                        // class identifier # comment
                        if (terms[0] != "=" && terms[1] != "=" && terms[2][0] == '#')
                        {
                            elements.Add(new Variable(terms[2].Substring(1), terms[0], terms[1]));
                            continue;
                        }
                        break;
                    case 4:
                        // class identifier = constant
                        if (terms[0] != "=" && terms[1] != "=" && terms[2] == "=" && terms[3][0] != '#')
                        {
                            elements.Add(new Constant("", terms[0], terms[1], terms[3]));
                            continue;
                        }
                        break;
                    case 5:
                        // class identifier = constant # comment
                        if (terms[0] != "=" && terms[1] != "=" && terms[2] == "=" && terms[3][0] != '#' && terms[4][0] == '#')
                        {
                            elements.Add(new Constant(terms[4].Substring(1), terms[0], terms[1], terms[3]));
                            continue;
                        }
                        break;
                }
                elements.Add(new Invalid(line));
            }

            return elements;
        }
    }
}
