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
        public readonly string package;
        public readonly string name;
        //public readonly string[] lines;
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

            this.package = package;
            name = Path.GetFileNameWithoutExtension(path);
            string[] lines = File.ReadAllLines(path);
            fullMessage = File.ReadAllText(path);

            List<MsgParser.IElement> elements = MsgParser.ParseFile(lines);
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
            ClassInfo.DoResolveClasses(packageInfo, package, variablesReq);
            ClassInfo.DoResolveClasses(packageInfo, package, variablesResp);
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

            lines.Add("public sealed class " + name + " : " + (isRequest ? "IRequest" : "IResponse"));

            lines.Add("{");

            foreach (var element in elements)
            {
                lines.Add("    " + element.ToCString());
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
            List<string> lengthProperty = ClassInfo.CreateLengthProperty(variables, fixedSize);
            foreach (var entry in lengthProperty)
            {
                lines.Add("    " + entry);
            }

            /*
            if (isRequest)
            {
                lines.Add("");
                lines.Add("    public Response Call(IServiceCaller caller)");
                lines.Add("    {");
                lines.Add("        " + service + " s = new " + service + "(this);");
                lines.Add("        caller.Call(s);");
                lines.Add("        return s.response;");
                lines.Add("    }");
            }
            */
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

        /*
        string GetCatDependencies()
        {
            List<ClassInfo> dependencies = new List<ClassInfo>();
            AddDependencies(dependencies, variablesReq);
            AddDependencies(dependencies, variablesResp);

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
        */

        public string GetMd5()
        {
            StringBuilder str = new StringBuilder();

            var constantsReq = elementsReq.Where(x => x.Type == MsgParser.ElementType.Constant).
                Cast<MsgParser.Constant>().
                Select(x => x.ToMd5String());
            if (constantsReq.Any())
            {
                str.AppendJoin("\n", constantsReq);
                if (variablesReq.Any())
                {
                    str.Append("\n");
                }
            }
            str.AppendJoin("\n", variablesReq.Select(x => x.GetMd5Entry()));

            var constantsResp = elementsResp.Where(x => x.Type == MsgParser.ElementType.Constant).
                Cast<MsgParser.Constant>().
                Select(x => x.ToMd5String());
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

            //Console.WriteLine("------" + package + "/" + name);
            //Console.WriteLine(md5File);

            string md5 = ClassInfo.GetMd5Hash(MD5.Create(), md5File);
            //Console.WriteLine(">>>" + md5);

            return md5;
        }

        List<string> CreateServiceContent()
        {
            List<string> lines = new List<string>();

            /*
            {
                lines.Add("");
                lines.Add("    /// <summary> Full ROS name of the parent service. </summary>");
                lines.Add("    public const string MessageType = " + service + ".MessageType;");
                lines.Add("");
                lines.Add("    /// <summary> MD5 hash of a compact representation of the parent service. </summary>");
                lines.Add("    public const string Md5Sum = " + service + ".Md5Sum;");
                lines.Add("");
                lines.Add("    /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>");
                lines.Add("    public const string DependenciesBase64 = " + service + ".DependenciesBase64;");
                lines.Add("");
                lines.Add("    public IResponse CreateResponse() => new Response();");
                lines.Add("");
                lines.Add("    public bool IsResponseType<T>()");
                lines.Add("    {");
                lines.Add("        return typeof(T).Equals(typeof(Response));");
                lines.Add("    }");
                lines.Add("");
                */

            /*
            lines.Add("");
            lines.Add("/// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>");
            lines.Add("public const string DependenciesBase64 =");
            string catDependencies = GetCatDependencies();
            List<string> compressedDeps = ClassInfo.Compress(catDependencies);
            foreach (var entry in compressedDeps)
            {
                lines.Add("    " + entry);
            }
            */

            lines.Add("/// <summary> Request message. </summary>");
            lines.Add("public " + name + "Request Request { get; }");

            lines.Add("");
            lines.Add("/// <summary> Response message. </summary>");
            lines.Add("public " + name + "Response Response { get; set; }");

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
            lines.Add("public IService Create() => new " + name + "();");

            lines.Add("");
            lines.Add("IRequest IService.Request => Request;");

            lines.Add("");
            lines.Add("IResponse IService.Response => Response;");

            lines.Add("");
            lines.Add("public string ErrorMessage { get; set; }");

            lines.Add("");
            lines.Add("[IgnoreDataMember]");
            lines.Add("public string RosType => RosServiceType;");

            lines.Add("");
            lines.Add("/// <summary> Full ROS name of this service. </summary>");
            lines.Add("public const string RosServiceType = \"" + package + "/" + name + "\";");

            lines.Add("");
            string md5 = GetMd5();
            lines.Add("/// <summary> MD5 hash of a compact representation of the service. </summary>");
            lines.Add("public const string RosMd5Sum = \"" + md5 + "\";");


            return lines;
        }


        public string ToCString()
        {
            StringBuilder str = new StringBuilder();
            if (hasStrings)
            {
                str.AppendLine("using System.Text;");
            }
            str.AppendLine("using System.Runtime.Serialization;");
            str.AppendLine("");
            str.AppendLine("namespace Iviz.Msgs." + package);
            str.AppendLine("{");
            str.AppendLine("    public sealed class " + name + " : IService");
            str.AppendLine("    {");

            List<string> mainClassLines = CreateServiceContent();
            foreach (var entry in mainClassLines)
            {
                str.Append("        ").AppendLine(entry);
            }

            /*
            str.AppendLine("        public static readonly System.Type RequestType = typeof(Request);");
            str.AppendLine();
            str.AppendLine("        public static readonly System.Type ResponseType = typeof(Response);");
            */

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
