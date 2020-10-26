using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Iviz.MsgsGen
{
    public class ServiceInfo
    {
        public readonly string rosPackage;
        public readonly string package;
        public readonly string name;
        public readonly string fullMessage;

        public readonly List<MsgParser.IElement> elementsReq;
        public readonly List<MsgParser.IElement> elementsResp;

        public readonly List<MsgParser.Variable> variablesReq;
        public readonly List<MsgParser.Variable> variablesResp;

        int fixedSizeReq = -2;
        int fixedSizeResp = -2;

        readonly bool hasStrings;

        public ServiceInfo(string package, string path)
        {
            Console.WriteLine("-- Parsing '" + path + "'");

            this.rosPackage = package;
            this.package = MsgParser.Sanitize(package);
            name = Path.GetFileNameWithoutExtension(path);
            string[] lines = File.ReadAllLines(path);
            fullMessage = File.ReadAllText(path);

            List<MsgParser.IElement> elements = MsgParser.ParseFile(lines, name);
            int serviceSeparator = elements.FindIndex(x => x.Type == MsgParser.ElementType.ServiceSeparator);
            if (serviceSeparator == -1)
            {
                throw new ArgumentException();
            }
            elementsReq = elements.GetRange(0, serviceSeparator);
            elementsResp = elements.GetRange(serviceSeparator + 1, elements.Count - serviceSeparator - 1);

            variablesReq = elementsReq.
                Where(x => x.Type == MsgParser.ElementType.Variable).
                Cast<MsgParser.Variable>().
                ToList();
            
            variablesResp = elementsResp.
                Where(x => x.Type == MsgParser.ElementType.Variable).
                Cast<MsgParser.Variable>().
                ToList();

            hasStrings = elements.Any(x =>
                ((x is MsgParser.Variable xv) && xv.className == "string") ||
                ((x is MsgParser.Constant xc) && xc.className == "string")
            );
        }

        public void ResolveClasses(PackageInfo packageInfo)
        {
            ClassInfo.DoResolveClasses(packageInfo, rosPackage, variablesReq);
            ClassInfo.DoResolveClasses(packageInfo, rosPackage, variablesResp);
        }

        public void CheckFixedSize()
        {
            ClassInfo.DoCheckFixedSize(ref fixedSizeReq, variablesReq);
            ClassInfo.DoCheckFixedSize(ref fixedSizeResp, variablesResp);
        }

        static List<string> CreateClassContent(
            List<MsgParser.IElement> elements,
            List<MsgParser.Variable> variables,
            string service,
            int fixedSize,
            bool isRequest
            )
        {
            const bool forceStruct = false;

            string name = service + (isRequest ? "Request" : "Response");

            List<string> lines = new List<string>();
            if (elements.Count == 0)
            {
                lines.Add("public sealed class " + name + " : Internal.Empty" + (isRequest ? "Request" : "Response"));
                lines.Add("{");
                lines.Add("}");
                return lines;
            }

            lines.Add("public sealed class " + name + " : " + (isRequest ? "IRequest" : "IResponse") + ", IDeserializable<" + name + ">");

            lines.Add("{");

            foreach (var element in elements)
            {
                var sublines = element.ToCString(forceStruct);
                foreach (var entry in sublines)
                {
                    lines.Add("    " + entry);
                }
            }

            lines.Add("");

            List<string> deserializer = ClassInfo.CreateConstructors(variables, name, forceStruct);
            foreach (var entry in deserializer)
            {
                lines.Add("    " + entry);
            }

            lines.Add("");
            List<string> serializer = ClassInfo.CreateSerializers(variables, forceStruct);
            foreach (var entry in serializer)
            {
                lines.Add("    " + entry);
            }

            lines.Add("");
            List<string> lengthProperty = ClassInfo.CreateLengthProperty(variables, fixedSize, false);
            foreach (var entry in lengthProperty)
            {
                lines.Add("    " + entry);
            }

            lines.Add("}");

            return lines;
        }

        public void AddDependencies(List<ClassInfo> dependencies, List<MsgParser.Variable> variables)
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

        string GetMd5()
        {
            StringBuilder str = new StringBuilder();

            string[] constantsReq = elementsReq.
                Where(x => x.Type == MsgParser.ElementType.Constant).
                Cast<MsgParser.Constant>().
                Select(x => x.ToMd5String()).
                ToArray();
            
            if (constantsReq.Any())
            {
                str.AppendJoin("\n", constantsReq);
                if (variablesReq.Any())
                {
                    str.Append("\n");
                }
            }
            str.AppendJoin("\n", variablesReq.Select(x => x.GetMd5Entry()));

            var constantsResp = elementsResp.
                Where(x => x.Type == MsgParser.ElementType.Constant).
                Cast<MsgParser.Constant>().
                Select(x => x.ToMd5String()).
                ToArray();
            
            if (constantsResp.Any())
            {
                str.AppendJoin("\n", constantsResp);
                if (variablesResp.Any())
                {
                    str.Append("\n");
                }
            }
            str.AppendJoin("\n", variablesResp.Select(x => x.GetMd5Entry()));

            string md5File = str.ToString();

            string md5 = ClassInfo.GetMd5Hash(MD5.Create(), md5File);

            return md5;
        }

        List<string> CreateServiceContent()
        {
            List<string> lines = new List<string>();

            lines.Add("/// <summary> Request message. </summary>");
            lines.Add("[DataMember] public " + name + "Request Request { get; set; }");

            lines.Add("");
            lines.Add("/// <summary> Response message. </summary>");
            lines.Add("[DataMember] public " + name + "Response Response { get; set; }");

            lines.Add("");
            lines.Add("/// <summary> Empty constructor. </summary>");
            lines.Add("public " + name + "()");
            lines.Add("{");
            lines.Add("    Request = new " + name + "Request();");
            lines.Add("    Response = new " + name + "Response();");
            lines.Add("}");

            lines.Add("");
            lines.Add("/// <summary> Setter constructor. </summary>");
            lines.Add("public " + name + "(" + name + "Request request)");
            lines.Add("{");
            lines.Add("    Request = request;");
            lines.Add("    Response = new " + name + "Response();");
            lines.Add("}");

            lines.Add("");
            lines.Add("IService IService.Create() => new " + name + "();");

            lines.Add("");
            lines.Add("IRequest IService.Request");
            lines.Add("{");
            lines.Add("    get => Request;");
            lines.Add("    set => Request = (" + name + "Request)value;");
            lines.Add("}");

            lines.Add("");
            lines.Add("IResponse IService.Response");
            lines.Add("{");
            lines.Add("    get => Response;");
            lines.Add("    set => Response = (" + name + "Response)value;");
            lines.Add("}");

            lines.Add("");
            lines.Add("/// <summary>");
            lines.Add("/// An error message in case the call fails.");
            lines.Add("/// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.");
            lines.Add("/// </summary>");
            lines.Add("public string ErrorMessage { get; set; }");

            lines.Add("");
            lines.Add("string IService.RosType => RosServiceType;");

            lines.Add("");
            lines.Add("/// <summary> Full ROS name of this service. </summary>");
            lines.Add("[Preserve] public const string RosServiceType = \"" + rosPackage + "/" + name + "\";");

            lines.Add("");
            string md5 = GetMd5();
            lines.Add("/// <summary> MD5 hash of a compact representation of the service. </summary>");
            lines.Add("[Preserve] public const string RosMd5Sum = \"" + md5 + "\";");


            return lines;
        }


        public string ToCString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("using System.Runtime.Serialization;");
            
            str.AppendLine("");
            str.AppendLine("namespace Iviz.Msgs." + package);
            str.AppendLine("{");
            str.AppendLine("    [DataContract (Name = \"" + rosPackage + "/" + name + "\")]");
            str.AppendLine("    public sealed class " + name + " : IService");
            str.AppendLine("    {");

            List<string> mainClassLines = CreateServiceContent();
            foreach (var entry in mainClassLines)
            {
                str.Append("        ").AppendLine(entry);
            }

            str.AppendLine("    }");
            str.AppendLine();

            List<string> linesReq = CreateClassContent(elementsReq, variablesReq, name, fixedSizeReq, true);
            foreach (var entry in linesReq)
            {
                str.Append("    ").AppendLine(entry);
            }

            str.AppendLine();

            List<string> linesResp = CreateClassContent(elementsResp, variablesResp, name, fixedSizeResp, false);
            foreach (var entry in linesResp)
            {
                str.Append("    ").AppendLine(entry);
            }
            str.AppendLine("}");


            return str.ToString();
        }

    }
}
