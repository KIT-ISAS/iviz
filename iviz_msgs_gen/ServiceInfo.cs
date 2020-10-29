using System;
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

        internal ServiceInfo(string package, string path)
        {
            Console.WriteLine($"-- Parsing '{path}'");

            RosPackage = package;
            Package = MsgParser.Sanitize(package);
            Name = Path.GetFileNameWithoutExtension(path);
            var lines = File.ReadAllLines(path);
            File.ReadAllText(path);

            var elements = MsgParser.ParseFile(lines, Name);
            var serviceSeparator = elements.FindIndex(x => x.Type == ElementType.ServiceSeparator);
            if (serviceSeparator == -1)
            {
                throw new ArgumentException();
            }

            elementsReq = elements.GetRange(0, serviceSeparator).ToArray();
            elementsResp = elements.GetRange(serviceSeparator + 1, elements.Count - serviceSeparator - 1).ToArray();

            variablesReq = elementsReq.OfType<VariableElement>().ToArray();
            variablesResp = elementsResp.OfType<VariableElement>().ToArray();
        }

        public string RosPackage { get; }
        public string Package { get; }
        public string Name { get; }

        internal void ResolveClasses(PackageInfo packageInfo)
        {
            ClassInfo.DoResolveClasses(packageInfo, RosPackage, variablesReq);
            ClassInfo.DoResolveClasses(packageInfo, RosPackage, variablesResp);
        }

        internal void CheckFixedSize()
        {
            ClassInfo.DoCheckFixedSize(ref fixedSizeReq, variablesReq);
            ClassInfo.DoCheckFixedSize(ref fixedSizeResp, variablesResp);
        }

        static IEnumerable<string> CreateClassContent(
            IReadOnlyCollection<IElement> elements,
            IReadOnlyCollection<VariableElement> variables,
            string service,
            int fixedSize,
            bool isRequest
        )
        {
            var name = service + (isRequest ? "Request" : "Response");

            if (elements.Count == 0)
            {
                return new[]
                {
                    $"public sealed class {name} : Internal.Empty{(isRequest ? "Request" : "Response")}",
                    "{",
                    "}"
                };
            }

            var lines = new List<string>();
            lines.Add(
                $"public sealed class {name} : {(isRequest ? "IRequest" : "IResponse")}, IDeserializable<{name}>");
            lines.Add("{");

            foreach (var element in elements)
            {
                var sublines = element.ToCsString();
                foreach (var entry in sublines)
                {
                    lines.Add($"    {entry}");
                }
            }

            lines.Add("");

            var deserializer = ClassInfo.CreateConstructors(variables, name, false);
            foreach (var entry in deserializer)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            var serializer = ClassInfo.CreateSerializers(variables, false);
            foreach (var entry in serializer)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("");
            var lengthProperty = ClassInfo.CreateLengthProperty(variables, fixedSize, false);
            foreach (var entry in lengthProperty)
            {
                lines.Add($"    {entry}");
            }

            lines.Add("}");

            return lines;
        }

        void AddDependencies(List<ClassInfo> dependencies, List<VariableElement> variables)
        {
            foreach (var variable in variables)
            {
                if (variable.ClassInfo != null &&
                    !dependencies.Contains(variable.ClassInfo))
                {
                    dependencies.Add(variable.ClassInfo);
                    variable.ClassInfo.AddDependencies(dependencies);
                }
            }
        }

        string GetMd5()
        {
            var str = new StringBuilder();

            var constantsReq = elementsReq.OfType<ConstantElement>().Select(x => x.ToMd5String()).ToArray();

            if (constantsReq.Any())
            {
                str.AppendJoin("\n", constantsReq);
                if (variablesReq.Any())
                {
                    str.Append("\n");
                }
            }

            str.AppendJoin("\n", variablesReq.Select(x => x.GetEntryForMd5Hash()));

            var constantsResp = elementsResp.OfType<ConstantElement>().Select(x => x.ToMd5String()).ToArray();

            if (constantsResp.Any())
            {
                str.AppendJoin("\n", constantsResp);
                if (variablesResp.Any())
                {
                    str.Append("\n");
                }
            }

            str.AppendJoin("\n", variablesResp.Select(x => x.GetEntryForMd5Hash()));

            var md5File = str.ToString();

            var md5 = ClassInfo.GetMd5Hash(MD5.Create(), md5File);

            return md5;
        }

        IEnumerable<string> CreateServiceContent()
        {
            return new[]
            {
                "/// <summary> Request message. </summary>",
                $"[DataMember] public {Name}Request Request {{ get; set; }}",
                "",
                "/// <summary> Response message. </summary>",
                $"[DataMember] public {Name}Response Response {{ get; set; }}",
                "",
                "/// <summary> Empty constructor. </summary>",
                $"public {Name}()",
                "{",
                $"    Request = new {Name}Request();",
                $"    Response = new {Name}Response();",
                "}",
                "",
                "/// <summary> Setter constructor. </summary>",
                $"public {Name}({Name}Request request)",
                "{",
                "    Request = request;",
                $"    Response = new {Name}Response();",
                "}",
                "",
                $"IService IService.Create() => new {Name}();",
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
                "/// <summary>",
                "/// An error message in case the call fails.",
                "/// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.",
                "/// </summary>",
                "public string ErrorMessage { get; set; }",
                "",
                "string IService.RosType => RosServiceType;",
                "",
                "/// <summary> Full ROS name of this service. </summary>",
                $"[Preserve] public const string RosServiceType = \"{RosPackage}/{Name}\";",
                "",
                "/// <summary> MD5 hash of a compact representation of the service. </summary>",
                $"[Preserve] public const string RosMd5Sum = \"{GetMd5()}\";"
            };
        }


        public string ToCString()
        {
            var str = new StringBuilder();

            str.AppendLine("using System.Runtime.Serialization;");

            str.AppendLine("");
            str.AppendLine($"namespace Iviz.Msgs.{Package}");
            str.AppendLine("{");
            str.AppendLine($"    [DataContract (Name = \"{RosPackage}/{Name}\")]");
            str.AppendLine($"    public sealed class {Name} : IService");
            str.AppendLine("    {");

            var mainClassLines = CreateServiceContent();
            foreach (var entry in mainClassLines)
            {
                str.Append("        ").AppendLine(entry);
            }

            str.AppendLine("    }");
            str.AppendLine();

            var linesReq = CreateClassContent(elementsReq, variablesReq, Name, fixedSizeReq, true);
            foreach (var entry in linesReq)
            {
                str.Append("    ").AppendLine(entry);
            }

            str.AppendLine();

            var linesResp = CreateClassContent(elementsResp, variablesResp, Name, fixedSizeResp, false);
            foreach (var entry in linesResp)
            {
                str.Append("    ").AppendLine(entry);
            }

            str.AppendLine("}");

            return str.ToString();
        }
    }
}