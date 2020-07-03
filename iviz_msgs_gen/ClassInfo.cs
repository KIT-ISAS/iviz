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
        public readonly string rosPackage;
        public readonly string package;
        public readonly string name;
        //public readonly string[] lines;
        public readonly string fullMessage;
        public readonly List<MsgParser.IElement> elements;
        public int fixedSize = -2;

        public string md5File;
        string md5;

        readonly bool forceStruct;
        readonly bool fakeStruct;
        readonly bool hasStrings;

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
            "geometry_msgs/Point32",
            "geometry_msgs/Quaternion",
            "geometry_msgs/Pose",
            "geometry_msgs/Transform",
            "geometry_msgs/Twist",
            "geometry_msgs/Wrench",
            "geometry_msgs/Accel",
            "geometry_msgs/Inertia",
            "std_msgs/ColorRGBA",
            "std_msgs/Float32",
            "std_msgs/Float64",
            "std_msgs/Time",
            "std_msgs/Duration",
            "std_msgs/Int16",
            "std_msgs/Int32",
            "std_msgs/Int64",
            "std_msgs/Int8",
            "std_msgs/UInt16",
            "std_msgs/UInt32",
            "std_msgs/UInt64",
            "std_msgs/UInt8",
            "std_msgs/Byte",
            "std_msgs/Char",
            "mesh_msgs/TriangleIndices",
            "mesh_msgs/MeshMaterial",
            "iviz_msgs/Color",
            "iviz_msgs/Vector2",
            "iviz_msgs/Vector3",
            "iviz_msgs/Triangle",
            "iviz_msgs/BoundingBox",
        };

        public static readonly HashSet<string> FakeStructs = new HashSet<string>
        {
            "std_msgs/Header",
        };

        static readonly HashSet<string> forceSkip = new HashSet<string>
        {
            //"mesh_msgs/TriangleIndices",
        };

        public bool ForceSkip => forceSkip.Contains(rosPackage + "/" + name);

        public ClassInfo(string package, string path)
        {
            Console.WriteLine("-- Parsing '" + path + "'");

            this.rosPackage = package;
            this.package = MsgParser.Sanitize(package);
            name = Path.GetFileNameWithoutExtension(path);
            string[] lines = File.ReadAllLines(path);
            fullMessage = File.ReadAllText(path);
            elements = MsgParser.ParseFile(lines, name);

            forceStruct = ForceStructs.Contains(package + "/" + name);
            fakeStruct = FakeStructs.Contains(package + "/" + name);
            hasStrings = elements.Any(x =>
                ((x is MsgParser.Variable xv) && xv.className == "string") ||
                ((x is MsgParser.Constant xc) && xc.className == "string")
            );

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
            DoResolveClasses(packageInfo, rosPackage, variables);
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


        internal static List<string> CreateLengthProperty(List<MsgParser.Variable> variables, int fixedSize, bool forceStruct, bool fakeStruct)
        {
            string readOnlyId = (forceStruct || fakeStruct) ? "readonly " : "";
            if (fixedSize != -1)
            {
                return new List<string> {
                            "public " + readOnlyId + "int RosMessageLength => " + fixedSize + ";"
                };
            }
            else if (variables.Count == 0)
            {
                return new List<string> {
                            "public " + readOnlyId + "int RosMessageLength => 0;"
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
                                fieldsWithSize.Add("size += BuiltIns.UTF8.GetByteCount(" + variable.fieldName + ");");
                            }
                            else
                            {
                                fieldsWithSize.Add("size += " + variable.fieldName + ".RosMessageLength;");
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
                            if (variable.className == "string")
                            {
                                fieldsWithSize.Add("size += 4 * " + variable.fieldName + ".Length;");
                                fieldsWithSize.Add("for (int i = 0; i < " + variable.fieldName + ".Length; i++)");
                                fieldsWithSize.Add("{");
                                fieldsWithSize.Add("    size += BuiltIns.UTF8.GetByteCount(" + variable.fieldName + "[i]);");
                                fieldsWithSize.Add("}");
                            }
                            else
                            {

                                fieldsWithSize.Add("for (int i = 0; i < " + variable.fieldName + ".Length; i++)");
                                fieldsWithSize.Add("{");
                                fieldsWithSize.Add("    size += " + variable.fieldName + "[i].RosMessageLength;");
                                fieldsWithSize.Add("}");
                            }
                        }
                    }
                }
            }

            List<string> lines = new List<string>();
            lines.Add("public " + readOnlyId + "int RosMessageLength");
            lines.Add("{");
            lines.Add("    get {");
            lines.Add("        int size = " + fieldSize + ";");
            foreach (string entry in fieldsWithSize)
            {
                lines.Add("        " + entry);
            }
            lines.Add("        return size;");
            lines.Add("    }");
            lines.Add("}");

            return lines;
        }

        internal static List<string> CreateConstructors(List<MsgParser.Variable> variables, string name, bool forceStruct, bool fakeStruct)
        {
            List<string> lines = new List<string>();

            if (!forceStruct && !fakeStruct)
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
                            lines.Add("    " + variable.fieldName + " = System.Array.Empty<" + variable.className + ">();");
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
                        else if (variable.classInfo == null || !(variable.classInfo.forceStruct || variable.classInfo.fakeStruct))
                        {
                            lines.Add("    " + variable.fieldName + " = new " + variable.className + "();");
                        }
                    }
                }
                lines.Add("}");
                lines.Add("");

                //if (lines.Count == 5)
                //{
                //    lines.Clear();
                //}
            }
            else
            {

            }

            if (variables.Any())
            {
                lines.Add("/// <summary> Explicit constructor. </summary>");
                //
                static string ParamToArg(MsgParser.Variable v)
                {
                    if (v.arraySize != -1)
                    {
                        return v.className + "[] " + v.fieldName;
                    }
                    else if (v.classInfo != null && (v.classInfo.forceStruct || v.classInfo.fakeStruct))
                    {
                        return "in " + v.className + " " + v.fieldName;
                    }
                    else
                    {
                        return v.className + " " + v.fieldName;
                    }
                }
                //
                string args = string.Join(", ", variables.Select(ParamToArg));
                lines.Add("public " + name + "(" + args + ")");
                lines.Add("{");
                foreach (var variable in variables)
                {
                    if (variable.arraySize > 0 && forceStruct)
                    {
                        lines.Add("    if (" + variable.fieldName + " is null) throw new System.ArgumentNullException(nameof(" + variable.fieldName + "));");
                        lines.Add("    for (int i = 0; i < " + variable.arraySize + "; i++)");
                        lines.Add("    {");
                        lines.Add("        this." + variable.fieldName + "[i] = " + variable.fieldName + "[i];");
                        lines.Add("    }");
                    }
                    else
                    {
                        lines.Add("    this." + variable.fieldName + " = " + variable.fieldName + ";");
                    }
                }
                lines.Add("}");
                lines.Add("");
            }

            lines.Add("/// <summary> Constructor with buffer. </summary>");
            lines.Add("internal " + name + "(Buffer b)");
            lines.Add("{");
            if (forceStruct)
            {
                //lines.Add("    this = b.Deserialize<" + name + ">();");
                lines.Add("    b.Deserialize(out this);");
            }
            else
            {
                foreach (var variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.rosClassName))
                    {
                        if (variable.arraySize == -1)
                        {
                            if (variable.className == "string")
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeString();");
                            }
                            else
                            {
                                lines.Add("    " + variable.fieldName + " = b.Deserialize<" + variable.className + ">();");
                            }
                        }
                        else if (variable.arraySize == 0)
                        {
                            if (variable.className == "string")
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeStringArray();");
                            }
                            else
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeStructArray<" + variable.className + ">();");
                            }
                        }
                        else
                        {
                            if (variable.className == "string")
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeStringArray(" + variable.arraySize + ");");
                            }
                            else
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeStructArray<" + variable.className + ">(" + variable.arraySize + ");");
                            }
                        }
                    }
                    else
                    {
                        if (variable.arraySize == -1)
                        {
                            lines.Add("    " + variable.fieldName + " = new " + variable.className + "(b);");
                        }
                        else if (variable.arraySize == 0)
                        {
                            if (variable.classInfo.forceStruct)
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeStructArray<" + variable.className + ">();");
                            }
                            else
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeArray<" + variable.className + ">();");
                                lines.Add("    for (int i = 0; i < this." + variable.fieldName + ".Length; i++)");
                                lines.Add("    {");
                                lines.Add("        " + variable.fieldName + "[i] = new " + variable.className + "(b);");
                                lines.Add("    }");
                            }
                        }
                        else
                        {
                            if (variable.classInfo.forceStruct)
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeStructArray<" + variable.className + ">(" + variable.arraySize + ");");
                            }
                            else
                            {
                                lines.Add("    " + variable.fieldName + " = b.DeserializeArray<" + variable.className + ">(" + variable.arraySize + ");");
                                lines.Add("    for (int i = 0; i < " + variable.arraySize + "; i++)");
                                lines.Add("    {");
                                lines.Add("        " + variable.fieldName + "[i] = new " + variable.className + "(b);");
                                lines.Add("    }");
                            }
                        }
                    }
                }
            }
            lines.Add("}");
            lines.Add("");

            string readOnlyId = (forceStruct || fakeStruct) ? "readonly " : "";
            lines.Add("public " + readOnlyId + "ISerializable RosDeserialize(Buffer b)");
            lines.Add("{");
            lines.Add("    return new " + name + "(b ?? throw new System.ArgumentNullException(nameof(b)));");
            lines.Add("}");

            return lines;
        }

        public static List<string> CreateSerializers(List<MsgParser.Variable> variables, bool forceStruct, bool fakeStruct)
        {
            List<string> lines = new List<string>();

            string readOnlyId = (forceStruct || fakeStruct) ? "readonly " : "";

            lines.Add("public " + readOnlyId + "void RosSerialize(Buffer b)");
            lines.Add("{");
            lines.Add("    if (b is null) throw new System.ArgumentNullException(nameof(b));");
            if (forceStruct)
            {
                lines.Add("    b.Serialize(this);");
            }
            else
            {
                foreach (var variable in variables)
                {
                    if (BuiltInTypes.Contains(variable.rosClassName))
                    {
                        if (variable.arraySize == -1)
                        {
                            lines.Add("    b.Serialize(" + variable.fieldName + ");");
                        }
                        else
                        {
                            if (variable.className == "string")
                            {
                                lines.Add("    b.SerializeArray(" + variable.fieldName + ", " + variable.arraySize + ");");
                            }
                            else
                            {
                                lines.Add("    b.SerializeStructArray(" + variable.fieldName + ", " + variable.arraySize + ");");
                            }
                        }
                    }
                    else
                    {
                        if (variable.arraySize == -1)
                        {
                            lines.Add("    " + variable.fieldName + ".RosSerialize(b);");
                        }
                        else
                        {
                            if (variable.classInfo.forceStruct)
                            {
                                lines.Add("    b.SerializeStructArray(" + variable.fieldName + ", " + variable.arraySize + ");");
                            }
                            else
                            {
                                lines.Add("    b.SerializeArray(" + variable.fieldName + ", " + variable.arraySize + ");");
                            }
                        }
                    }
                }
            }
            lines.Add("}");

            lines.Add("");
            lines.Add("public " + readOnlyId + "void RosValidate()");
            lines.Add("{");
            foreach (var variable in variables)
            {
                if (variable.arraySize > 0)
                {
                    if (!forceStruct)
                    {
                        lines.Add("    if (" + variable.fieldName + " is null) throw new System.NullReferenceException();");
                        lines.Add("    if (" + variable.fieldName + ".Length != " + variable.arraySize + ") throw new System.IndexOutOfRangeException();");
                    }
                }
                else if (variable.arraySize == -1 &&
                  ((BuiltInTypes.Contains(variable.rosClassName) && variable.rosClassName != "string") ||
                  (variable.classInfo?.forceStruct ?? false)))
                {
                }
                else
                {
                    if (!(variable.classInfo?.fakeStruct ?? false))
                    {
                        lines.Add("    if (" + variable.fieldName + " is null) throw new System.NullReferenceException();");
                    }
                    if (variable.arraySize == -1 && variable.rosClassName != "string")
                    {
                        lines.Add("    " + variable.fieldName + ".RosValidate();");
                    }
                }
                if (variable.arraySize != -1)
                {
                    if (variable.rosClassName == "string")
                    {
                        lines.Add("    for (int i = 0; i < " + variable.fieldName + ".Length; i++)");
                        lines.Add("    {");
                        lines.Add("        if (" + variable.fieldName + "[i] is null) throw new System.NullReferenceException();");
                        lines.Add("    }");
                    }
                    else if (!BuiltInTypes.Contains(variable.rosClassName) && !(variable.classInfo?.forceStruct ?? false))
                    {
                        lines.Add("    for (int i = 0; i < " + variable.fieldName + ".Length; i++)");
                        lines.Add("    {");
                        if (!(variable.classInfo?.fakeStruct ?? false))
                        {
                            lines.Add("        if (" + variable.fieldName + "[i] is null) throw new System.NullReferenceException();");
                        }
                        lines.Add("        " + variable.fieldName + "[i].RosValidate();");
                        lines.Add("    }");
                    }
                }


            }
            lines.Add("}");

            return lines;
        }

        public string ToCString()
        {
            StringBuilder str = new StringBuilder();

            if (hasStrings)
            {
                //str.AppendLine("using System.Text;");
            }
            if (forceStruct)
            {
                str.AppendLine("using System.Runtime.InteropServices;");
            }
            str.AppendLine("using System.Runtime.Serialization;");
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

        static readonly UTF8Encoding UTF8 = new UTF8Encoding(false);

        public static List<string> Compress(string catDependencies)
        {
            List<string> lines = new List<string>();

            byte[] inputBytes = UTF8.GetBytes(catDependencies);
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
            lines.Add("[DataContract (Name = \"" + rosPackage + "/" + name + "\")]");
            if (forceStruct)
            {
                lines.Add("[StructLayout(LayoutKind.Sequential)]");
                if (variables.Any(x => x.arraySize > 0))
                {
                    lines.Add("public unsafe struct " + name + " : IMessage");
                }
                else
                {
                    lines.Add("public struct " + name + " : IMessage");
                }
            }
            else if (fakeStruct)
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
                var sublines = element.ToCString(forceStruct);
                foreach (var entry in sublines)
                {
                    lines.Add("    " + entry);
                }
            }
            if (elements.Count != 0)
            {
                lines.Add("");
            }
            List<string> deserializer = CreateConstructors(variables, name, forceStruct, fakeStruct);
            foreach (var entry in deserializer)
            {
                lines.Add("    " + entry);
            }

            lines.Add("");
            List<string> serializer = CreateSerializers(variables, forceStruct, fakeStruct);
            foreach (var entry in serializer)
            {
                lines.Add("    " + entry);
            }

            lines.Add("");
            List<string> lengthProperty = CreateLengthProperty(variables, fixedSize, forceStruct, fakeStruct);
            foreach (var entry in lengthProperty)
            {
                lines.Add("    " + entry);
            }

            //lines.Add("");
            //lines.Add("    IMessage IMessage.Create() => new " + name + "();");

            lines.Add("");
            string readOnlyId = (forceStruct || fakeStruct) ? "readonly " : "";
            lines.Add("    public " + readOnlyId + "string RosType => RosMessageType;");

            lines.Add("");
            lines.Add("    /// <summary> Full ROS name of this message. </summary>");
            lines.Add("    [Preserve] public const string RosMessageType = \"" + rosPackage + "/" + name + "\";");


            lines.Add("");
            string md5 = GetMd5Property();
            lines.Add("    /// <summary> MD5 hash of a compact representation of the message. </summary>");
            lines.Add("    [Preserve] public const string RosMd5Sum = \"" + md5 + "\";");

            lines.Add("");

            lines.Add("    /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>");
            lines.Add("    [Preserve] public const string RosDependenciesBase64 =");

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
                builder.AppendLine("MSG: " + classInfo.rosPackage + "/" + classInfo.name);
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
            byte[] data = md5Hash.ComputeHash(UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
