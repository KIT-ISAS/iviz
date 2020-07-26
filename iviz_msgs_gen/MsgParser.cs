using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static string Sanitize(string name)
        {
            StringBuilder str = new StringBuilder();
            str.Append(char.ToUpper(name[0]));
            for (int i = 1; i < name.Length; i++)
            {
                if (name[i] == '_' && i != name.Length - 1)
                {
                    str.Append(char.ToUpper(name[i + 1]));
                    i++;
                }
                else
                {
                    str.Append(name[i]);
                }
            }
            return str.ToString();
        }

        public interface IElement
        {
            ElementType Type { get; }
            List<string> ToCString(bool forceUnroll = false);
        }

        public class Empty : IElement
        {
            public ElementType Type => ElementType.Empty;

            public override string ToString()
            {
                return "[]";
            }

            public List<string> ToCString(bool forceUnroll)
            {
                return new List<string>();
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

            public List<string> ToCString(bool forceUnroll)
            {
                return new List<string> { "//" + text };
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

            public bool IsArray => arraySize != -1;
            public bool IsDynamicSizeArray => arraySize == 0;
            public bool IsFixedSizeArray => arraySize > 0;

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

            public Variable(string comment, string classToken, string fieldName, string parentClassName)
            {
                this.comment = comment;
                this.classToken = classToken;

                rosFieldName = fieldName;

                this.fieldName = Sanitize(fieldName);
                if (this.fieldName == parentClassName)
                {
                    this.fieldName += "_";
                }

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

                int slashIndex;
                if (rosClassName == "Header")
                {
                    rosClassName = "std_msgs/Header";
                    className = "StdMsgs.Header";
                }
                else if (BuiltInsMaps.TryGetValue(rosClassName, out className))
                {
                    //
                }
                else if ((slashIndex = rosClassName.IndexOf('/')) != -1)
                {
                    string packageName = rosClassName.Substring(0, slashIndex);
                    string classProper = rosClassName.Substring(slashIndex + 1);
                    className = Sanitize(packageName) + "." + classProper;
                } else
                {
                    className = rosClassName;
                }

            }

            public override string ToString()
            {
                return "['" + classToken + "' '" + rosFieldName + "' # '" + comment + "']";
            }

            public List<string> ToCString(bool isInStruct)
            {

                List<string> list = new List<string>();

                string attrStr = (fieldName != rosFieldName) ? $"[DataMember (Name = \"{rosFieldName}\")]" : "[DataMember]";
                //list.Add(attrStr);

                string result;
                if (arraySize == -1)
                {
                    result = "public " + className + " " + fieldName + " { get; set; }";
                }
                else if (arraySize == 0)
                {
                    result = "public " + className + "[] " + fieldName + " { get; set; }";
                }
                else
                {
                    if (isInStruct)
                    {
                        result = "fixed " + className + " " + fieldName + "[" + arraySize + "];";
                    }
                    else
                    {
                        result = "public " + className + "[/*" + arraySize + "*/] " + fieldName + " { get; set; }";
                    }
                }
                if (comment != "")
                {
                    result += " //" + comment;
                }
                //list.Add(result);

                list.Add(attrStr + " " + result);

                if (arraySize > 0 && isInStruct)
                {
                    for (int i = 0; i < arraySize; i++) {
                        list.Add("public " + className + " " + fieldName + i);
                        list.Add("{");
                        list.Add("    readonly get => " + fieldName + "[" + i + "];");
                        list.Add("    set => " + fieldName + "[" + i + "] = value;");
                        list.Add("}");
                    }
                }

                return list;
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

            public List<string> ToCString(bool forceUnroll)
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
                return new List<string> { result };
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

            public List<string> ToCString(bool forceUnroll)
            {
                return new List<string> ();
            }
        }

        public class ServiceSeparator : IElement
        {
            public ElementType Type => ElementType.ServiceSeparator;

            public override string ToString()
            {
                return "[---]";
            }

            public List<string> ToCString(bool forceUnroll)
            {
                return new List<string>();
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

        public static List<IElement> ParseFile(string[] lines, string className)
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
                            elements.Add(new Variable("", terms[0], terms[1], className));
                            continue;
                        }
                        break;
                    case 3:
                        // class identifier # comment
                        if (terms[0] != "=" && terms[1] != "=" && terms[2][0] == '#')
                        {
                            elements.Add(new Variable(terms[2].Substring(1), terms[0], terms[1], className));
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
