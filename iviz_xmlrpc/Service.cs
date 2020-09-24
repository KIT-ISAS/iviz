//#define DEBUG__

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.XmlRpc
{
    public class FaultException : Exception
    {
        public FaultException(string message) : base(message)
        {
        }
    }

    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }

    public static class Service
    {
        static void Assert(string received, string expected)
        {
            if (received != expected)
            {
                throw new ParseException($"Expected '{expected}' but received '{received}'");
            }
        }

        static object Parse(XmlNode value)
        {
            Assert(value.Name, "value");
            if (!value.HasChildNodes)
            {
                return value.InnerText;
            }

            XmlNode primitive = value.FirstChild;
            if (primitive is XmlText)
            {
                return primitive.InnerText;
            }

            ;
            switch (primitive.Name)
            {
                case "double":
                    return double.TryParse(primitive.InnerText, NumberStyles.Number, BuiltIns.Culture,
                        out double @double)
                        ? (object) @double
                        : null;
                case "i4":
                case "int":
                    return int.TryParse(primitive.InnerText, NumberStyles.Number, BuiltIns.Culture, out int @int)
                        ? (object) @int
                        : null;
                case "boolean":
                    return primitive.InnerText == "1";
                case "string":
                    return primitive.InnerText;
                case "array":
                    XmlNode data = primitive.FirstChild;
                    Assert(data.Name, "data");
                    object[] children = new object[data.ChildNodes.Count];
                    for (int i = 0; i < data.ChildNodes.Count; i++)
                    {
                        children[i] = Parse(data.ChildNodes[i]);
                    }

                    return children;
                case "dateTime.iso8601":
                    return DateTime.TryParseExact(
                        primitive.InnerText,
                        "yyyy-MM-ddTHH:mm:ssZ",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime dt)
                        ? dt
                        : DateTime.MinValue;
                case "base64":
                    try
                    {
                        return Convert.FromBase64String(primitive.InnerText);
                    }
                    catch (FormatException)
                    {
                        Logger.Log("XmlRpc.Service: Failed to parse base64 parameter");
                        return null;
                    }
                case "struct":
                    List<(string, object)> structValue = new List<(string, object)>();
                    for (int i = 0; i < primitive.ChildNodes.Count; i++)
                    {
                        XmlNode member = primitive.ChildNodes[i];
                        if (member.Name != "member")
                        {
                            continue;
                        }

                        string entryName = null;
                        object entryValue = null;
                        for (int j = 0; j < member.ChildNodes.Count; j++)
                        {
                            XmlNode entry = member.ChildNodes[j];
                            switch (entry.Name)
                            {
                                case "name":
                                    entryName = entry.InnerText;
                                    break;
                                case "value":
                                    entryValue = Parse(entry);
                                    break;
                            }
                        }

                        if (entryName is null || entryValue is null)
                        {
                            Logger.Log("XmlRpc.Service: Invalid struct entry");
                            continue;
                        }

                        structValue.Add((entryName, entryValue));
                    }

                    return structValue;
                default:
                    Logger.Log("XmlRpc.Service: Parameter of unknown type");
                    return null;
            }
        }

        static string CreateRequest(string method, IEnumerable<Arg> args)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine("<?xml version=\"1.0\"?>");
            buffer.AppendLine("<methodCall>");
            buffer.Append("<methodName>").Append(method).AppendLine("</methodName>");
            buffer.AppendLine("<params>");
            foreach (Arg arg in args)
            {
                buffer.AppendLine("<param>");
                buffer.AppendLine(arg);
                buffer.AppendLine("</param>");
            }

            buffer.AppendLine("</params>");
            buffer.AppendLine("</methodCall>");

            return buffer.ToString();
        }

        static object ProcessResponse(string inData)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(inData);
            XmlNode root = document.FirstChild;
            while (root != null && root.Name != "methodResponse")
            {
                root = root.NextSibling;
            }

            if (root is null)
            {
                throw new ParseException("Response has no 'methodResponse' tag");
            }

            XmlNode child = root.FirstChild;

            switch (child?.Name)
            {
                case null: 
                    throw new ParseException("MethodResponse has no children");
                case "params" when child.ChildNodes.Count == 0:
                    throw new ParseException("Empty response");
                case "params" when child.ChildNodes.Count > 1:
                    throw new ParseException("Function call returned too many arguments");
                case "params":
                {
                    XmlNode param = child.FirstChild;
                    Assert(param.Name, "param");
                    return Parse(param.FirstChild);
                }
                case "fault":
                    throw new FaultException(child.FirstChild.InnerXml);
                default:
                    throw new ParseException($"Expected 'params' or 'fault', but got '{child.Name}'");
            }            
        }
        
        public static async Task<object> MethodCallAsync(Uri remoteUri, Uri callerUri, string method,
            IEnumerable<Arg> args, int timeoutInMs = 2000)
        {
            if (remoteUri is null)
            {
                throw new ArgumentNullException(nameof(remoteUri));
            }

            if (callerUri is null)
            {
                throw new ArgumentNullException(nameof(callerUri));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            string outData = CreateRequest(method, args);
            
            string inData;
            using (HttpRequest request = new HttpRequest(callerUri, remoteUri))
            {
                await request.StartAsync(timeoutInMs);
                inData = await request.RequestAsync(outData, timeoutInMs);
            }

            return ProcessResponse(inData);
        }
        
        public static object MethodCall(Uri remoteUri, Uri callerUri, string method, IEnumerable<Arg> args, int timeoutInMs = 2000)
        {
            if (remoteUri is null)
            {
                throw new ArgumentNullException(nameof(remoteUri));
            }

            if (callerUri is null)
            {
                throw new ArgumentNullException(nameof(callerUri));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            string outData = CreateRequest(method, args);
            
            string inData;
            using (HttpRequest request = new HttpRequest(callerUri, remoteUri))
            {
                request.Start(timeoutInMs);
                inData = request.Request(outData, timeoutInMs);
            }

            return ProcessResponse(inData);
        }        

        public static async Task MethodResponseAsync(
            HttpListenerContext httpContext,
            IReadOnlyDictionary<string, Func<object[], Arg[]>> methods,
            IReadOnlyDictionary<string, Func<object[], Task>> lateCallbacks = null)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (methods is null)
            {
                throw new ArgumentNullException(nameof(methods));
            }

            string inData = await httpContext.GetRequest();

#if DEBUG__
            Logger.Log("--- MethodResponse ---");
            Logger.Log("<< " + inData);
#endif

            XmlDocument document = new XmlDocument();
            document.LoadXml(inData);

            try
            {
                XmlNode root = document.FirstChild;
                while (root != null && root.Name != "methodCall")
                {
                    root = root.NextSibling;
                }

                if (root == null)
                {
                    throw new ParseException("Malformed request: no 'methodCall' found");
                }

                string methodName = null;
                object[] args = null;
                XmlNode child = root.FirstChild;
                do
                {
                    switch (child.Name)
                    {
                        case "params":
                        {
                            args = new object[child.ChildNodes.Count];
                            for (int i = 0; i < child.ChildNodes.Count; i++)
                            {
                                XmlNode param = child.ChildNodes[i];
                                Assert(param.Name, "param");
                                args[i] = Parse(param.FirstChild);
                            }

                            break;
                        }
                        case "fault":
                            throw new FaultException(child.FirstChild.InnerXml);
                        case "methodName":
                            methodName = child.InnerText;
                            break;
                        default:
                            throw new ParseException(
                                $"Expected 'params', 'fault', or 'methodName', got '{child.Name}'");
                    }
                } while ((child = child.NextSibling) != null);

                StringBuilder buffer = new StringBuilder();
                if (methodName == null ||
                    !methods.TryGetValue(methodName, out Func<object[], Arg[]> method) ||
                    args == null)
                {
                    throw new ParseException($"Unknown function '{methodName}' or invalid arguments");
                }

                Arg response = new Arg(method(args));

                buffer.AppendLine("<?xml version=\"1.0\"?>");
                buffer.AppendLine("<methodResponse>");
                buffer.AppendLine("<params>");
                buffer.AppendLine("<param>");
                buffer.AppendLine(response);
                buffer.AppendLine("</param>");
                buffer.AppendLine("</params>");
                buffer.AppendLine("</methodResponse>");
                buffer.AppendLine();

#if DEBUG__
                Logger.Log(">> " + buffer);
                Logger.Log("--- End MethodResponse ---");
#endif

                await httpContext.Respond(buffer.ToString());

                if (lateCallbacks != null && lateCallbacks.TryGetValue(methodName, out var lateCallback))
                {
                    await lateCallback(args);
                }
            }
            catch (ParseException e)
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine("<?xml version=\"1.0\"?>");
                buffer.AppendLine("<methodResponse>");
                buffer.AppendLine("<fault>");
                buffer.AppendLine(new Arg(e.Message));
                buffer.AppendLine("</fault>");
                buffer.AppendLine("</methodResponse>");

                await httpContext.Respond(buffer.ToString());
            }
        }
    }
}