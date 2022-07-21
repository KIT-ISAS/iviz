﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Iviz.MsgsGen
{
    public sealed class ServiceInfo
    {
        readonly IElement[] elementsReq;
        readonly IElement[] elementsResp;

        readonly VariableElement[] variablesReq;
        readonly VariableElement[] variablesResp;

        int fixedSizeReq = ClassInfo.UninitializedSize;
        int fixedSizeResp = ClassInfo.UninitializedSize;

        string? md5;

        public ServiceInfo(string package, string path)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new ArgumentException($"File {path} does not exist.");
            }

            Console.WriteLine($"-- Parsing '{path}'");

            RosPackage = package;
            CsPackage = MsgParser.CsIfy(package);
            Name = Path.GetFileNameWithoutExtension(path);
            string[] lines = File.ReadAllLines(path);
            //File.ReadAllText(path);

            List<IElement> elements = MsgParser.ParseFile(lines, Name);
            int serviceSeparator = elements.FindIndex(x => x.Type == ElementType.ServiceSeparator);
            if (serviceSeparator == -1)
            {
                throw new MessageParseException("Service file has no separator");
            }

            elementsReq = elements.GetRange(0, serviceSeparator).ToArray();
            elementsResp = elements.GetRange(serviceSeparator + 1, elements.Count - serviceSeparator - 1).ToArray();

            variablesReq = elementsReq.OfType<VariableElement>().ToArray();
            variablesResp = elementsResp.OfType<VariableElement>().ToArray();
        }

        public string RosPackage { get; }
        public string CsPackage { get; }
        public string Name { get; }
        public string FullRosName => $"{RosPackage}/{Name}";

        internal void ResolveClasses(PackageInfo packageInfo)
        {
            ClassInfo.DoResolveClasses(packageInfo, RosPackage, variablesReq);
            ClassInfo.DoResolveClasses(packageInfo, RosPackage, variablesResp);
        }

        internal void CheckFixedSize()
        {
            if (fixedSizeReq == ClassInfo.UninitializedSize)
            {
                fixedSizeReq = ClassInfo.DoCheckFixedSize(variablesReq);
            }

            if (fixedSizeResp == ClassInfo.UninitializedSize)
            {
                fixedSizeResp = ClassInfo.DoCheckFixedSize(variablesResp);
            }
        }

        static IEnumerable<string> CreateClassContent(
            IReadOnlyCollection<IElement> elements,
            IReadOnlyCollection<VariableElement> variables,
            string service,
            int fixedSize,
            bool isRequest
        )
        {
            string strType = isRequest ? "Request" : "Response";
            string name = service + strType;

            var lines = new List<string>();
            lines.Add("[DataContract]");
            lines.Add(isRequest
                ? $"public sealed class {name} : IRequest<{service}, {service}Response>, IDeserializable<{name}>"
                : $"public sealed class {name} : IResponse, IDeserializable<{name}>");
            lines.Add("{");

            IEnumerable<string> entries = elements.SelectMany(element => element.ToCsString());
            foreach (string entry in entries)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");

            IEnumerable<string> deserializer =
                ClassInfo.CreateConstructors(variables, name, false, false, false, RosVersion.Common);
            foreach (string entry in deserializer)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            IEnumerable<string> serializer = ClassInfo.CreateSerializers(variables, false, false, RosVersion.Common);
            foreach (string entry in serializer)
            {
                lines.Add($"    {entry}");
            }


            lines.Add("");
            IEnumerable<string> lengthProperty =
                ClassInfo.CreateLengthProperty1(variables, fixedSize, false)
                    .Append("")
                    .Concat(ClassInfo.CreateLengthProperty2(variables, fixedSize, false, false));
 
            foreach (string entry in lengthProperty)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            lines.Add("    public override string ToString() => Extensions.ToString(this);");

            lines.Add("}");

            return lines;
        }

        string GetMd5()
        {
            if (md5 != null)
            {
                return md5;
            }

            var str = new StringBuilder(200);

            string[] constantsReq = elementsReq.OfType<ConstantElement>().Select(x => x.GetEntryForMd5Hash()).ToArray();

            if (constantsReq.Any())
            {
                str.Append(string.Join("\n", constantsReq));
                if (variablesReq.Any())
                {
                    str.Append('\n');
                }
            }

            string[] reqHashVariables = variablesReq.Select(x => x.GetEntryForMd5Hash(RosPackage)).ToArray();
            if (reqHashVariables.Any(md5String => md5String == null))
            {
                return "";
            }

            str.Append(string.Join("\n", reqHashVariables));

            string[] constantsHash =
                elementsResp.OfType<ConstantElement>().Select(x => x.GetEntryForMd5Hash()).ToArray();

            if (constantsHash.Any())
            {
                str.Append(string.Join("\n", constantsHash));
                if (variablesResp.Any())
                {
                    str.Append('\n');
                }
            }

            string[] respHashVariables = variablesResp.Select(x => x.GetEntryForMd5Hash(RosPackage)).ToArray();
            if (respHashVariables.Any(md5String => md5String == null))
            {
                return "";
            }

            str.Append(string.Join("\n", respHashVariables));

            string md5File = str.ToString();

            md5 = ClassInfo.GetMd5Hash(md5File);

            return md5;
        }

        IEnumerable<string> CreateServiceContent()
        {
            CheckFixedSize();
            string md5Property = GetMd5();
            return new[]
            {
                "/// Request message.",
                $"[DataMember] public {Name}Request Request {{ get; set; }}",
                "",
                "/// Response message.",
                $"[DataMember] public {Name}Response Response {{ get; set; }}",
                "",
                "/// Empty constructor.",
                $"public {Name}()",
                "{",
                fixedSizeReq == 0
                    ? $"    Request = {Name}Request.Singleton;"
                    : $"    Request = new {Name}Request();",
                fixedSizeResp == 0
                    ? $"    Response = {Name}Response.Singleton;"
                    : $"    Response = new {Name}Response();",
                "}",
                "",
                "/// Setter constructor.",
                $"public {Name}({Name}Request request)",
                "{",
                "    Request = request;",
                fixedSizeResp == 0
                    ? $"    Response = {Name}Response.Singleton;"
                    : $"    Response = new {Name}Response();",
                "}",
                "",
                "IRequest IService.Request",
                "{",
                "    get => Request;",
                $"    set => Request = ({Name}Request)value;",
                "}",
                "",
                "IResponse IService.Response",
                "{",
                "    get => Response;",
                $"    set => Response = ({Name}Response)value;",
                "}",
                "",
                //"string IService.RosType => RosServiceType;",
                $"public const string ServiceType = \"{RosPackage}/{Name}\";",
                $"public string RosServiceType => ServiceType;",
                "",
                //"/// Full ROS name of this service.",
                //$"[Preserve] public const string ServiceType = \"{RosPackage}/{Name}\";",
                //"",
                //"/// MD5 hash of a compact representation of the service.",
                //$"[Preserve] public const string RosMd5Sum = {(md5Property.Length == 0 ? "null" : $"\"{md5Property}\"")};",
                $"public string RosMd5Sum => {(md5Property.Length == 0 ? "null" : $"\"{md5Property}\"")};",
                "",
                "public override string ToString() => Extensions.ToString(this);",
            };
        }

        public string ToCsString()
        {
            var str = new StringBuilder(200);

            str.AppendNewLine("using System.Runtime.Serialization;");

            str.AppendNewLine("");
            str.AppendNewLine($"namespace Iviz.Msgs.{CsPackage}");
            str.AppendNewLine("{");
            str.AppendNewLine($"    [DataContract]");
            str.AppendNewLine($"    public sealed class {Name} : IService");
            str.AppendNewLine("    {");

            IEnumerable<string> mainClassLines = CreateServiceContent();
            foreach (string entry in mainClassLines)
            {
                str.Append("        ").AppendNewLine(entry);
            }

            str.AppendNewLine("    }");
            str.AppendNewLine();

            IEnumerable<string> linesReq = CreateClassContent(elementsReq, variablesReq, Name, fixedSizeReq, true);
            foreach (string entry in linesReq)
            {
                str.Append("    ").AppendNewLine(entry);
            }

            str.AppendNewLine();

            IEnumerable<string> linesResp = CreateClassContent(elementsResp, variablesResp, Name, fixedSizeResp, false);
            foreach (string entry in linesResp)
            {
                str.Append("    ").AppendNewLine(entry);
            }

            str.AppendNewLine("}");

            return str.ToString();
        }
    }
}