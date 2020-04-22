using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Iviz.MsgsGen
{
    public class ClassInfo
    {
        public readonly string package;
        public readonly string name;
        //public readonly string[] lines;
        public readonly string fullMessage;
        public readonly List<MsgParser.IElement> elements;
        public int fixedSize = -2;

        public string md5File;
        string md5;

        readonly bool forceStruct;

        readonly List<MsgParser.Variable> variables = new List<MsgParser.Variable>();

        public static readonly Dictionary<string, int> BuiltInsSizes = new Dictionary<string, int>
            {
                { "bool", 1 },
                { "int8", 1 },
                { "uint8", 1 },
                { "int16", 2 },
                { "uint16", 2 },
                { "int32", 4 },
                { "uint32", 4 },
                { "int64", 8 },
                { "uint64", 8 },
                { "float32", 4 },
                { "float64", 8 },
                { "time", 8 },
                { "duration", 8 },
                { "char", 1 },
                { "byte", 1 },
            };


        public static readonly HashSet<string> BuiltInTypes = new HashSet<string>
            {
                 "bool" ,
                 "int8",
                 "uint8",
                 "int16",
                 "uint16",
                 "int32",
                 "uint32",
                 "int64",
                 "uint64",
                 "float32",
                 "float64",
                 "time",
                 "duration",
                 "char",
                 "byte",
                 "string",
            };

        public static readonly HashSet<string> ForceStructs = new HashSet<string>
        {
            "geometry_msgs/Vector3",
            "geometry_msgs/Point",
            "geometry_msgs/Quaternion",
            "geometry_msgs/Pose",
            "geometry_msgs/Transform",
            "std_msgs/ColorRGBA",
        };

        public ClassInfo(string package, string path)
        {
            Console.WriteLine("-- Parsing '" + path + "'");

            this.package = package;
            name = Path.GetFileNameWithoutExtension(path);
            string[] lines = File.ReadAllLines(path);
            fullMessage = File.ReadAllText(path);
            elements = MsgParser.ParseFile(lines);

            forceStruct = ForceStructs.Contains(package + "/" + name);

            variables = elements.
                Where(x => x.Type == MsgParser.ElementType.Variable).
                Cast<MsgParser.Variable>().
                ToList();

            /*
            foreach (var element in elements)
            {
                if (element.Type == MsgParser.ElementType.Variable)
                {
                    variables.Add((MsgParser.Variable)element);
                }
            }
            */
        }

        public static void DoResolveClasses(PackageInfo packageInfo, string package, List<MsgParser.Variable> variables)
        {
            foreach (var variable in variables)
            {
                if (!BuiltInsSizes.ContainsKey(variable.rosClassName) &&
                    packageInfo.TryGet(variable.rosClassName, package, out ClassInfo classInfo))
                {
                    variable.classInfo = classInfo;
                }
            }
        }
        public void ResolveClasses(PackageInfo packageInfo)
        {
            DoResolveClasses(packageInfo, package, variables);
        }

        public static int DoCheckFixedSize(ref int fixedSize, List<MsgParser.Variable> variables)
        {
            if (fixedSize != -2)
            {
                return fixedSize;
            }

            fixedSize = 0;
            foreach (var variable in variables)
            {
                if (variable.arraySize != 0)
                {
                    if (BuiltInsSizes.TryGetValue(variable.rosClassName, out int size))
                    {
                        fixedSize += (variable.arraySize == -1) ? size : size * variable.arraySize;
                    }
                    else if (variable.classInfo != null && variable.classInfo.CheckFixedSize() != -1)
                    {
                        fixedSize += (variable.arraySize == -1) ?
                            variable.classInfo.fixedSize :
                            size * variable.classInfo.fixedSize;
                    }
                    else
                    {
                        fixedSize = -1;
                        return -1;
                    }
                }
                else
                {
                    fixedSize = -1;
                    return -1;
                }
            }

            return fixedSize;
        }

        public int CheckFixedSize()
        {
            return DoCheckFixedSize(ref fixedSize, variables);
        }


        internal static List<string> CreateLengthProperty(List<MsgParser.Variable> variables, int fixedSize)
        {
            if (fixedSize != -1)
            {
                return new List<string> {
                            "public int GetLength() => " + fixedSize + ";"
                };
            }
            else if (variables.Count == 0)
            {
                return new List<string> {
                            "public int GetLength() => 0;"
                };
            }

            List<string> fieldsWithSize = new List<string>();
            int fieldSize = 0;
            foreach (var variable in variables)
            {
                if (BuiltInsSizes.TryGetValue(variable.rosClassName, out int size))
                {
                    if (variable.arraySize == -1)
                    {
                        fieldSize += size;
                    }
                    else if (variable.arraySize == 0)
                    {
                        fieldsWithSize.Add("size += " + size + " * " + variable.fieldName + ".Length;");
                        fieldSize += 4;
                    }
                    else
                    {
                        fieldSize += size * variable.arraySize;
                    }
                }
                else
                {
                    if (variable.arraySize == -1)
                    {
                        if (variable.classInfo != null && variable.classInfo.fixedSize != -1)
                        {
                            fieldSize += variable.classInfo.fixedSize;
                        }
                        else
                        {
                            if (variable.className == "string")
                            {
                                fieldSize += 4;
                                fieldsWithSize.Add("size += " + variable.fieldName + ".Length;");
                            }
                            else
                            {
                                fieldsWithSize.Add("size += " + variable.fieldName + ".GetLength();");
                            }
                        }
                    }
                    else
                    {
                        if (variable.arraySize == 0)
                        {
                            fieldSize += 4;
                        }
                        if (variable.classInfo != null && variable.classInfo.fixedSize != -1)
                        {
                            fieldsWithSize.Add("size += " + variable.classInfo.fixedSize + " * " + variable.fieldName + ".Length;");
                        }
                        else
                        {
                            fieldsWithSize.Add("for (int i = 0; i < " + variable.fieldName + ".Length; i++)");
                            fieldsWithSize.Add("{");
                            if (variable.className == "string")
                            {
                                fieldSize += 4;
                                fieldsWithSize.Add("    size += " + variable.fieldName + "[i].Length;");
                            }
                            else
                            {
                                fieldsWithSize.Add("    size += " + variable.fieldName + "[i].GetLength();");
                            }
                            fieldsWithSize.Add("}");
                        }
                    }
                }
            }

            List<string> lines = new List<string>();
            lines.Add("public int GetLength()");
            lines.Add("{");
            lines.Add("    int size = " + fieldSize + ";");
            foreach (string entry in fieldsWithSize)
            {
                lines.Add("    " + entry);
            }
            lines.Add("    return size;");
            lines.Add("}");

            return lines;
        }

        internal static List<string> CreateConstructors(List<MsgParser.Variable> variables, string name, bool forceStruct)
        {
            List<string> lines = new List<string>();

            if (!forceStruct)
            {
                lines.Add("/// <summary> Constructor for empty message. </summary>");
                lines.Add("public " + name + "()");
                lines.Add("{");
                foreach (var variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.rosClassName))
                    {
                        if (variable.rosClassName == "string" && variable.arraySize == -1)
                        {
                            lines.Add("    " + variable.fieldName + " = \"\";");
                        }
                        else if (variable.arraySize == 0)
                        {
                            lines.Add("    " + variable.fieldName + " = System.Array.Empty<" + variable.arraySize + ">();");
                        }
                        else if (variable.arraySize != -1)
                        {
                            lines.Add("    " + variable.fieldName + " = new " + variable.className + "[" + variable.arraySize + "];");
                        }
                    }
                    else
                    {
                        if (variable.arraySize == 0)
                        {
                            lines.Add("    " + variable.fieldName + " = System.Array.Empty<" + variable.className + ">();");
                        }
                        else if (variable.arraySize != -1)
                        {
                            lines.Add("    " + variable.fieldName + " = new " + variable.className + "[" + variable.arraySize + "];");
                        }
                        else if (!variable.classInfo.forceStruct)
                        {
                            lines.Add("    " + variable.fieldName + " = new " + variable.className + "();");
                        }
                    }
                }
                lines.Add("}");
                lines.Add("");

                if (lines.Count == 5)
                {
                    lines.Clear();
                }
            }

            lines.Add("public unsafe void Deserialize(ref byte* ptr, byte* end)");
            lines.Add("{");
            if (forceStruct)
            {
                lines.Add("    BuiltIns.DeserializeStruct(out this, ref ptr, end);");
            }
            else
            {
                foreach (var variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.rosClassName))
                    {
                        if (variable.arraySize == -1)
                        {
                            lines.Add("    BuiltIns.Deserialize(out " + variable.fieldName + ", ref ptr, end);");
                        }
                        else
                        {
                            lines.Add("    BuiltIns.Deserialize(out " + variable.fieldName + ", ref ptr, end, " + variable.arraySize + ");");
                        }
                    }
                    else
                    {
                        if (variable.arraySize == -1)
                        {
                            lines.Add("    " + variable.fieldName + ".Deserialize(ref ptr, end);");
                        }
                        else
                        {
                            if (variable.classInfo.forceStruct)
                            {
                                lines.Add("    BuiltIns.DeserializeStructArray(out " + variable.fieldName + ", ref ptr, end, " + variable.arraySize + ");");
                            }
                            else
                            {
                                lines.Add("    BuiltIns.DeserializeArray(out " + variable.fieldName + ", ref ptr, end, " + variable.arraySize + ");");
                            }
                        }
                    }
                }
            }
            lines.Add("}");

            return lines;
        }

        public static List<string> CreateSerializers(List<MsgParser.Variable> variables, bool forceStruct)
        {
            List<string> lines = new List<string>();

            lines.Add("public unsafe void Serialize(ref byte* ptr, byte* end)");
            lines.Add("{");
            if (forceStruct)
            {
                lines.Add("    BuiltIns.SerializeStruct(this, ref ptr, end);");
            }
            else
            {
                foreach (var variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.rosClassName))
                    {
                        if (variable.arraySize == -1)
                        {
                            lines.Add("    BuiltIns.Serialize(" + variable.fieldName + ", ref ptr, end);");
                        }
                        else
                        {
                            lines.Add("    BuiltIns.Serialize(" + variable.fieldName + ", ref ptr, end, " + variable.arraySize + ");");
                        }
                    }
                    else
                    {
                        if (variable.arraySize == -1)
                        {
                            lines.Add("    " + variable.fieldName + ".Serialize(ref ptr, end);");
                        }
                        else
                        {
                            if (variable.classInfo.forceStruct)
                            {
                                lines.Add("    BuiltIns.SerializeStructArray(" + variable.fieldName + ", ref ptr, end, " + variable.arraySize + ");");
                            }
                            else
                            {
                                lines.Add("    BuiltIns.SerializeArray(" + variable.fieldName + ", ref ptr, end, " + variable.arraySize + ");");
                            }
                        }
                    }
                }
            }
            lines.Add("}");

            return lines;
        }

        public string ToCString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine();

            str.AppendLine("namespace Iviz.Msgs." + package);
            str.AppendLine("{");

            foreach (var entry in CreateClassContent())
            {
                str.Append("    ").AppendLine(entry);
            }

            str.AppendLine("}");

            return str.ToString();
        }

        public static List<string> Compress(string catDependencies)
        {
            List<string> lines = new List<string>();

            byte[] inputBytes = Encoding.UTF8.GetBytes(catDependencies);
            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);
                }
                string base64 = Convert.ToBase64String(outputStream.ToArray());

                for (int i = 0; i < base64.Length; i += 80)
                {
                    bool last;
                    int end;
                    if (i + 80 < base64.Length)
                    {
                        last = false;
                        end = i + 80;
                    }
                    else
                    {
                        last = true;
                        end = base64.Length;
                    }
                    string sub = base64.Substring(i, end - i);
                    if (!last)
                    {
                        lines.Add("\"" + sub + "\" +");
                    }
                    else
                    {
                        lines.Add("\"" + sub + "\";");
                    }
                }
                lines.Add("");
            }
            return lines;
        }

        List<string> CreateClassContent()
        {
            List<string> lines = new List<string>();
            if (forceStruct)
            {
                lines.Add("public struct " + name + " : IMessage");
            }
            else
            {
                lines.Add("public sealed class " + name + " : IMessage");
            }
            lines.Add("{");
            foreach (var element in elements)
            {
                lines.Add("    " + element.ToCString());
            }

            lines.Add("");
            lines.Add("    /// <summary> Full ROS name of this message. </summary>");
            lines.Add("    public const string MessageType = \"" + package + "/" + name + "\";");

            lines.Add("");
            lines.Add("    public IMessage Create() => new " + name + "();");

            lines.Add("");


            List<string> lengthProperty = CreateLengthProperty(variables, fixedSize);
            foreach (var entry in lengthProperty)
            {
                lines.Add("    " + entry);
            }

            lines.Add("");
            List<string> deserializer = CreateConstructors(variables, name, forceStruct);
            foreach (var entry in deserializer)
            {
                lines.Add("    " + entry);
            }

            lines.Add("");
            List<string> serializer = CreateSerializers(variables, forceStruct);
            foreach (var entry in serializer)
            {
                lines.Add("    " + entry);
            }

            lines.Add("");
            string md5 = GetMd5Property();
            lines.Add("    /// <summary> MD5 hash of a compact representation of the message. </summary>");
            lines.Add("    public const string Md5Sum = \"" + md5 + "\";");

            lines.Add("");

            lines.Add("    /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>");
            lines.Add("    public const string DependenciesBase64 =");

            /*
            byte[] inputBytes = Encoding.UTF8.GetBytes(catDependencies);
            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);
                }
                string base64 = Convert.ToBase64String(outputStream.ToArray());

                for (int i = 0; i < base64.Length; i += 80)
                {
                    bool last;
                    int end;
                    if (i + 80 < base64.Length)
                    {
                        last = false;
                        end = i + 80;
                    }
                    else
                    {
                        last = true;
                        end = base64.Length;
                    }
                    string sub = base64.Substring(i, end - i);
                    if (!last)
                    {
                        lines.Add("            \"" + sub + "\" +");
                    }
                    else
                    {
                        lines.Add("            \"" + sub + "\";");
                    }
                }
                lines.Add("");
            }
            */

            string catDependencies = GetCatDependencies();
            List<string> compressedDeps = Compress(catDependencies);
            foreach (var entry in compressedDeps)
            {
                lines.Add("            " + entry);
            }

            lines.Add("}");

            return lines;



            /*
            StringBuilder str = new StringBuilder();
            str.AppendLine();

            str.AppendLine("namespace Iviz.Msgs." + package);
            str.AppendLine("{");
            if (forceStruct)
            {
                str.AppendLine("    public struct " + name + " : IMessage ");
            }
            else
            {
                str.AppendLine("    public sealed class " + name + " : IMessage ");
            }
            str.AppendLine("    {");
            foreach (var element in elements)
            {
                str.Append("        ").Append(element.ToCString()).AppendLine();
            }

            str.AppendLine();
            str.AppendLine("        /// <summary> Full ROS name of this message. </summary>");
            str.AppendLine("        public const string MessageType = \"" + package + "/" + name + "\";");

            str.AppendLine();
            str.AppendLine("        public IMessage Create() => new " + name + "();");

            str.AppendLine();
            List<string> lengthProperty = CreateLengthProperty();
            foreach (var entry in lengthProperty)
            {
                str.Append("        ").Append(entry).AppendLine();
            }

            str.AppendLine();
            List<string> deserializer = CreateConstructors();
            foreach (var entry in deserializer)
            {
                str.Append("        ").Append(entry).AppendLine();
            }

            str.AppendLine();
            List<string> serializer = CreateSerializers();
            foreach (var entry in serializer)
            {
                str.Append("        ").Append(entry).AppendLine();
            }

            str.AppendLine();
            string md5 = GetMd5Property();
            str.AppendLine("        /// <summary> MD5 hash of a compact representation of the message. </summary>");
            str.AppendLine("        public const string Md5Sum = \"" + md5 + "\";");

            str.AppendLine("");

            string catDependencies = GetCatDependencies();
            byte[] inputBytes = Encoding.UTF8.GetBytes(catDependencies);
            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);
                }
                string base64 = Convert.ToBase64String(outputStream.ToArray());

                str.AppendLine("        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>");
                str.AppendLine("        public const string DependenciesBase64 =");
                for (int i = 0; i < base64.Length; i += 80)
                {
                    bool last;
                    int end;
                    if (i + 80 < base64.Length)
                    {
                        last = false;
                        end = i + 80;
                    }
                    else
                    {
                        last = true;
                        end = base64.Length;
                    }
                    string sub = base64.Substring(i, end - i);
                    str.Append("            \"" + sub + "\"");
                    if (!last)
                    {
                        str.AppendLine(" +");
                    }
                    else
                    {
                        str.AppendLine(";");
                    }
                }
                str.AppendLine();
            }

            str.AppendLine("    }");
            str.AppendLine("}");


            return str.ToString();
            */
        }

        public void AddDependencies(List<ClassInfo> dependencies)
        {
            foreach (var variable in variables)
            {
                if (variable.classInfo != null &&
                    !dependencies.Contains(variable.classInfo))
                {
                    dependencies.Add(variable.classInfo);
                    variable.classInfo.AddDependencies(dependencies);
                }
            }
        }

        string GetCatDependencies()
        {
            List<ClassInfo> dependencies = new List<ClassInfo>();
            AddDependencies(dependencies);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(fullMessage);

            foreach (ClassInfo classInfo in dependencies)
            {
                builder.AppendLine("================================================================================");
                builder.AppendLine("MSG: " + classInfo.package + "/" + classInfo.name);
                builder.AppendLine(classInfo.fullMessage);
            }
            return builder.ToString();
        }

        public string GetMd5Property()
        {
            GetMd5();
            MD5 md5Hash = MD5.Create();
            return GetMd5Hash(md5Hash, md5File);
        }

        public string GetMd5()
        {
            if (md5 != null) return md5;

            StringBuilder str = new StringBuilder();

            var constants = elements.Where(x => x.Type == MsgParser.ElementType.Constant).
                Cast<MsgParser.Constant>().
                Select(x => x.ToMd5String());
            if (constants.Any())
            {
                str.AppendJoin("\n", constants);
                if (variables.Any())
                {
                    str.Append("\n");
                }
            }
            str.AppendJoin("\n", variables.Select(x => x.GetMd5Entry()));

            /*
            StringBuilder str = new StringBuilder();
            var constants = elements.Where(x => x.Type == MsgParser.ElementType.Constant).
                Cast<MsgParser.Constant>().
                Select(x => x.ToMd5String());

            str.Append(string.Join("\n", constants));
            if (str.Length != 0)
            {
                str.Append("\n");
            }
            str.Append(string.Join("\n", variables.Select(x => x.GetMd5Entry())));
            */

            md5File = str.ToString();

            //Console.WriteLine("------" + package + "/" + name);
            //Console.WriteLine(md5File);


            md5 = GetMd5Hash(MD5.Create(), md5File);
            //Console.WriteLine(">>>" + md5);

            return md5;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
