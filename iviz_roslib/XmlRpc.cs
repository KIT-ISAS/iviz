//#define DEBUG__

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.RoslibSharp.XmlRpc
{
    public class FaultException : Exception
    {
        public FaultException() { }
        public FaultException(string message) : base(message) { }
    }

    public class ParseException : Exception
    {
        public ParseException() { }
        public ParseException(string message) : base(message) { }
    }

    public readonly struct Arg
    {
        readonly string value;

        public Arg(bool f)
        {
            value = f ?
                "<value><boolean>1</boolean></value>\n" :
                "<value><boolean>0</boolean></value>\n";
        }
        public Arg(double f)
        {
            value = $"<value><double>{f}</double></value>\n";
        }
        public Arg(int f)
        {
            value = $"<value><i4>{f}</i4></value>\n";
        }
        public Arg(Uri f) : this(f.ToString())
        {
        }
        public Arg(string f)
        {
            //value = $"<value><string>{f}</string></value>\n";
            value = $"<value>{f}</value>\n";
        }
        public Arg(string[] f) : this(f.Select(x => new Arg(x)).ToArray())
        {
        }
        public Arg(string[][] f) : this(f.Select(x => new Arg(x)).ToArray())
        {
        }
        public Arg(Arg[] f)
        {
            value = $"<value><array><data>{string.Join("", f)}</data></array></value>";
        }
        public Arg(Arg[][] f) : this(f.Select(x => new Arg(x)).ToArray())
        {
        }
        public override string ToString()
        {
            return value;
        }
    }

    public static class Service
    {

        static void Assert(string received, string expected)
        {
            if (received != expected)
            {
                throw new ArgumentException($"Expected '{expected}' but received '{received}'");
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
            };
            switch (primitive.Name)
            {
                case "double": return double.Parse(primitive.InnerText);
                case "i4": return int.Parse(primitive.InnerText);
                case "int": return int.Parse(primitive.InnerText);
                case "boolean": return primitive.InnerText == "1";
                case "string": return primitive.InnerText;
                case "array":
                    XmlNode data = primitive.FirstChild;
                    Assert(data.Name, "data");
                    object[] children = new object[data.ChildNodes.Count];
                    for (int i = 0; i < data.ChildNodes.Count; i++)
                    {
                        children[i] = Parse(data.ChildNodes[i]);
                    }
                    return children;
                default:
                    return null;
            }
        }

        public static object MethodCall(Uri remoteUri, Uri callerUri, string method, Arg[] args, int timeoutInMs = 3000)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine("<?xml version=\"1.0\"?>");
            buffer.AppendLine("<methodCall>");
            buffer.AppendLine($"<methodName>{method}</methodName>");
            buffer.AppendLine("<params>");
            foreach (Arg arg in args)
            {
                buffer.AppendLine("<param>");
                buffer.AppendLine(arg.ToString());
                buffer.AppendLine("</param>");
            }
            buffer.AppendLine("</params>");
            buffer.AppendLine("</methodCall>");

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUri);
            httpRequest.Timeout = timeoutInMs;
            httpRequest.Method = "POST";
            httpRequest.ContentType = "text/xml";
            //httpRequest.Host = callerUri.Host;
            //httpRequest.UserAgent = "iviz.roslib";

            byte[] outData = BuiltIns.UTF8.GetBytes(buffer.ToString());
            httpRequest.ContentLength = outData.Length;

#if DEBUG__
            Logger.Log("--- MethodCall ---");
            Logger.Log(">> " + buffer);
#endif

            using (Stream requestStream = httpRequest.GetRequestStream())
            {
                requestStream.Write(outData, 0, outData.Length);
                requestStream.Flush();
            }

            string inData;
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (StreamReader stream = new StreamReader(httpResponse.GetResponseStream(), BuiltIns.UTF8))
            {
                inData = stream.ReadToEnd();
            }

#if DEBUG__
            Logger.Log("<< " + inData);
            Logger.Log("--- End MethodCall ---");
#endif

            XmlDocument document = new XmlDocument();
            document.LoadXml(inData);
            XmlNode root = document.FirstChild;
            while (root.Name != "methodResponse")
            {
                root = root.NextSibling;
            }
            XmlNode child = root.FirstChild;
            if (child.Name == "params")
            {
                if (child.ChildNodes.Count == 0)
                {
                    throw new ParseException("Empty response");
                }
                else if (child.ChildNodes.Count > 1)
                {
                    throw new ParseException("Function call returned too many arguments");
                }
                XmlNode param = child.FirstChild;
                Assert(param.Name, "param");
                return Parse(param.FirstChild);
            }
            else if (child.Name == "fault")
            {
                throw new FaultException(child.FirstChild.InnerXml);
            }
            else
            {
                throw new ParseException("Expected 'params' or 'fault'");
            }
        }

        public static void MethodResponse(HttpListenerContext httpContext, Dictionary<string, Func<object[], Arg[]>> methods)
        {
            string inData;
            using (StreamReader stream = new StreamReader(httpContext.Request.InputStream, BuiltIns.UTF8))
            {
                inData = stream.ReadToEnd();
            }

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
                    throw new ParseException("Malformed request");
                }
                string methodName = null;
                object[] args = null;
                XmlNode child = root.FirstChild;
                do
                {
                    if (child.Name == "params")
                    {
                        args = new object[child.ChildNodes.Count];
                        for (int i = 0; i < child.ChildNodes.Count; i++)
                        {
                            XmlNode param = child.ChildNodes[i];
                            Assert(param.Name, "param");
                            args[i] = Parse(param.FirstChild);

                        }
                    }
                    else if (child.Name == "fault")
                    {
                        throw new FaultException(child.FirstChild.InnerXml);
                    }
                    else if (child.Name == "methodName")
                    {
                        methodName = child.InnerText;
                    }
                    else
                    {
                        throw new ParseException("Expected 'params', 'fault', or 'methodName'");
                    }
                } while ((child = child.NextSibling) != null);

                StringBuilder buffer = new StringBuilder();
                if (methodName == null ||
                    !methods.TryGetValue(methodName, out Func<object[], Arg[]> method) ||
                    args == null)
                {
                    throw new ParseException($"Unknown function '{methodName}' or invalid arguments");
                }
                else
                {
                    Arg response = new Arg(method(args));

                    buffer.AppendLine("<?xml version=\"1.0\"?>");
                    buffer.AppendLine("<methodResponse>");
                    buffer.AppendLine("<params>");
                    buffer.AppendLine("<param>");
                    buffer.AppendLine(response.ToString());
                    buffer.AppendLine("</param>");
                    buffer.AppendLine("</params>");
                    buffer.AppendLine("</methodResponse>");
                    buffer.AppendLine();
                }

#if DEBUG__
                Logger.Log(">> " + buffer);
                Logger.Log("--- End MethodResponse ---");
#endif


                string str = buffer.ToString();
                httpContext.Response.ContentLength64 = str.Length;
                httpContext.Response.ContentType = "text/xml";

                byte[] bytes = BuiltIns.UTF8.GetBytes(str);
                httpContext.Response.OutputStream.Write(bytes, 0, bytes.Length);
                httpContext.Response.Close();
            }
            catch (ParseException e)
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine("<?xml version=\"1.0\"?>");
                buffer.AppendLine("<methodResponse>");
                buffer.AppendLine("<fault>");
                buffer.AppendLine(new Arg(e.Message).ToString());
                buffer.AppendLine("</fault>");
                buffer.AppendLine("</methodResponse>");
                using (StreamWriter stream = new StreamWriter(httpContext.Response.OutputStream, BuiltIns.UTF8))
                {
                    stream.Write(buffer.ToString());
                    stream.Flush();
                }

            }
        }
    }
}
