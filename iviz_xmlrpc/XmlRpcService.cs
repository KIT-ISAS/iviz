//#define DEBUG__

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.XmlRpc
{
    /// <summary>
    /// Parent class for the exceptions from this library.
    /// </summary>
    public class XmlRpcException : Exception
    {
        protected XmlRpcException(string message) : base(message)
        {
        }

        protected XmlRpcException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    /// Thrown when the remote call reported an exception. 
    /// </summary>
    public class FaultException : XmlRpcException
    {
        public FaultException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Thrown when the XML could not be parsed.
    /// </summary>
    public class ParseException : XmlRpcException
    {
        public ParseException(string message) : base(message)
        {
        }

        public ParseException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    /// Thrown when an error happened during the connection.
    /// </summary>    
    public class HttpConnectionException : XmlRpcException
    {
        public HttpConnectionException(string message) : base(message)
        {
        }

        public HttpConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class RpcConnectionException : HttpConnectionException
    {
        public RpcConnectionException(string message) : base(message)
        {
        }

        public RpcConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }


    /// <summary>
    /// Functions for XML-RPC calls.
    /// </summary>
    public static class XmlRpcService
    {
        static void Assert(string? received, string expected)
        {
            if (received != expected)
            {
                throw new ParseException($"Expected '{expected}' but received '{received}'");
            }
        }

        static object Parse(XmlNode? value)
        {
            if (value == null)
            {
                throw new ParseException("Expected child node but received null!");
            }

            Assert(value.Name, "value");
            if (!value.HasChildNodes)
            {
                return value.InnerText ?? throw new ParseException("Expected text node but received null!");
            }

            XmlNode? primitive = value.FirstChild;
            if (primitive is XmlText)
            {
                return primitive.InnerText ?? throw new ParseException("Expected text node but received null!");
            }

            switch (primitive?.Name)
            {
                case null:
                    throw new ParseException("Expected node name but received null!");
                case "double":
                    return double.TryParse(primitive.InnerText, NumberStyles.Number, BuiltIns.Culture,
                        out double @double)
                        ? (object) @double
                        : throw new ParseException($"Could not parse '{primitive.InnerText}' as double!");
                case "i4":
                case "int":
                    return int.TryParse(primitive.InnerText, NumberStyles.Number, BuiltIns.Culture, out int @int)
                        ? (object) @int
                        : throw new ParseException($"Could not parse '{primitive.InnerText}' as integer!");
                case "boolean":
                    return primitive.InnerText == "1";
                case "string":
                    return primitive.InnerText;
                case "array":
                    XmlNode? data = primitive.FirstChild;
                    Assert(data?.Name, "data");
                    return data!.ChildNodes.Cast<XmlNode?>().Select(Parse).ToArray();
                case "dateTime.iso8601":
                    return DateTime.TryParseExact(primitive.InnerText, "yyyy-MM-ddTHH:mm:ssZ",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt)
                        ? (object) dt
                        : (object) DateTime.MinValue;
                case "base64":
                    try
                    {
                        return Convert.FromBase64String(primitive.InnerText);
                    }
                    catch (FormatException e)
                    {
                        throw new ParseException($"Could not parse '{primitive.InnerText}' as base64!", e);
                    }
                case "struct":
                    List<(string, object)> structValue = new List<(string, object)>();
                    foreach (XmlNode? member in primitive.ChildNodes)
                    {
                        if (member == null || member.Name != "member")
                        {
                            continue;
                        }

                        string? entryName = null;
                        object? entryValue = null;
                        foreach (XmlNode? entry in member.ChildNodes)
                        {
                            switch (entry?.Name)
                            {
                                case null:
                                    break;
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
                    throw new ParseException($"Could not parse type '{primitive.Name}'");
            }
        }

        internal static string CreateRequest(string method, Arg[] args)
        {
            string[] entries = new string[4 + args.Length * 3];
            entries[0] = "<?xml version=\"1.0\"?>\n" +
                         "<methodCall>\n" +
                         "<methodName>";
            entries[1] = method;
            entries[2] = "</methodName>\n" +
                         "<params>\n";
            int o = 3;
            foreach (Arg arg in args)
            {
                entries[o++] = "<param>";
                entries[o++] = arg;
                entries[o++] = "</param>\n";
            }

            entries[o] = "</params>\n" +
                         "</methodCall>\n";

            return string.Concat(entries);
        }

        internal static object ProcessResponse(string inData)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(inData);
            XmlNode? root = document.FirstChild;
            while (root != null && root.Name != "methodResponse")
            {
                root = root.NextSibling;
            }

            if (root is null)
            {
                throw new ParseException("Response has no 'methodResponse' tag");
            }

            XmlNode? child = root.FirstChild;

            switch (child?.Name)
            {
                case null:
                    throw new ParseException("'methodResponse' tag has no children");
                case "params" when child.ChildNodes.Count == 0:
                    throw new ParseException("Empty response");
                case "params" when child.ChildNodes.Count > 1:
                    throw new ParseException("Function call returned too many arguments");
                case "params":
                {
                    XmlNode? param = child.FirstChild;
                    Assert(param?.Name, "param");
                    return Parse(param!.FirstChild);
                }
                case "fault":
                    if (child.FirstChild == null)
                    {
                        throw new ParseException("Expected a child under 'fault', received none. ");
                    }

                    throw new FaultException(child.FirstChild.InnerXml);
                default:
                    throw new ParseException($"Expected 'params' or 'fault', but got '{child.Name}'");
            }
        }

        /// <summary>
        /// Calls an XML-RPC method. The connection to the server is closed after the call.
        /// </summary>
        /// <param name="remoteUri">Uri of the callee.</param>
        /// <param name="callerUri">Uri of the caller.</param>
        /// <param name="method">Name of the XML-RPC method.</param>
        /// <param name="args">List of arguments.</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>The result of the remote call.</returns>
        /// <exception cref="ArgumentNullException">Thrown if one of the arguments is null.</exception>
        /// <exception cref="RpcConnectionException">An error happened during the connection.</exception>
        public static async ValueTask<object> MethodCallAsync(Uri remoteUri, Uri callerUri, string method, Arg[] args,
            CancellationToken token = default)
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

            using HttpRequest request = new (callerUri, remoteUri);
            try
            {
                await request.StartAsync(token);
                await request.SendRequestAsync(outData, false, false, token);
                (inData, _, _) = await request.GetResponseAsync(token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RpcConnectionException($"Error while calling RPC method '{method}' at {remoteUri}", e);
            }

            return ProcessResponse(inData);
        }

        /// <summary>
        /// Calls an XML-RPC method. The connection to the server is closed after the call.
        /// For async contexts use <see cref="MethodCallAsync"/>.
        /// </summary>
        /// <param name="remoteUri">Uri of the callee.</param>
        /// <param name="callerUri">Uri of the caller.</param>
        /// <param name="method">Name of the XML-RPC method.</param>
        /// <param name="args">List of arguments.</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns>The result of the remote call.</returns>
        /// <exception cref="ArgumentNullException">Thrown if one of the arguments is null.</exception>        
        public static object MethodCall(Uri remoteUri, Uri callerUri, string method, Arg[] args,
            CancellationToken token = default)
        {
            return Task.Run(() => MethodCallAsync(remoteUri, callerUri, method, args, token).AsTask(), token)
                .WaitAndRethrow();
        }

        /// <summary>
        /// Responds to an XML-RPC function call sent through an HTTP request.
        /// First deserializes the function arguments from the request, then it calls one
        /// of the methods in the list, and finally it responds with the serialized arguments. 
        /// </summary>
        /// <param name="httpContext">The context containing the HTTP request</param>
        /// <param name="methods">A list of available XML-RPC methods</param>
        /// <param name="lateCallbacks">Will be called after the response is sent</param>
        /// <param name="token">Optional cancellation token</param>
        /// <returns>An awaitable task</returns>
        /// <exception cref="ArgumentNullException">Thrown if the context or the method list is null</exception>
        /// <exception cref="ParseException">Thrown if the request could not be understood</exception>
        public static async Task MethodResponseAsync(
            HttpListenerContext httpContext,
            IReadOnlyDictionary<string, Func<object[], Arg[]>> methods,
            IReadOnlyDictionary<string, Func<object[], CancellationToken, Task>>? lateCallbacks = null,
            CancellationToken token = default)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (methods is null)
            {
                throw new ArgumentNullException(nameof(methods));
            }

            string inData = await httpContext.GetRequestAsync(token: token);

            try
            {
                var (methodName, args) = ParseResponseXml(inData);

                //Console.WriteLine(methodName);
                if (!methods.TryGetValue(methodName, out var method))
                {
                    throw new ParseException($"Unknown function '{methodName}' or invalid arguments");
                }

                Arg response = (Arg) method(args);

                string outData = "<?xml version=\"1.0\"?>\n" +
                                 "<methodResponse>\n" +
                                 "<params>\n" +
                                 "<param>\n" +
                                 $"{response.ToString()}\n" +
                                 "</param>\n" +
                                 "</params>\n" +
                                 "</methodResponse>\n" +
                                 "\n";

                await httpContext.RespondAsync(outData, token: token);

                if (lateCallbacks != null &&
                    lateCallbacks.TryGetValue(methodName, out var lateCallback))
                {
                    await lateCallback(args, token);
                }
            }
            catch (ParseException e)
            {
                string buffer = "<?xml version=\"1.0\"?>\n" +
                                "<methodResponse>\n" +
                                "<fault>\n" +
                                $"{new Arg(e.Message).ToString()}\n" +
                                "</fault>\n" +
                                "</methodResponse>\n";

                await httpContext.RespondAsync(buffer, token: token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("XmlRpcService: Error during parsing {0}", e.ToString());
                await httpContext.RespondWithUnexpectedErrorAsync(token: token);
                throw;
            }
        }

        static (string methodName, object[] args) ParseResponseXml(string inData)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(inData);

            XmlNode? root = document.FirstChild;
            while (root != null && root.Name != "methodCall")
            {
                root = root.NextSibling;
            }

            if (root == null)
            {
                throw new ParseException("Malformed request: no 'methodCall' found");
            }

            string? methodName = null;
            object[]? args = null;
            XmlNode? child = root.FirstChild;
            do
            {
                switch (child?.Name)
                {
                    case null:
                        throw new ParseException("Expected 'params', 'fault', or 'methodName'; got null instead");
                    case "params":
                    {
                        args = new object[child.ChildNodes.Count];
                        for (int i = 0; i < child.ChildNodes.Count; i++)
                        {
                            XmlNode? param = child.ChildNodes[i];
                            Assert(param?.Name, "param");
                            object? arg = Parse(param?.FirstChild);
                            args[i] = arg ??
                                      throw new ParseException(
                                          $"Could not parse argument '{param?.FirstChild?.InnerText}'");
                        }

                        break;
                    }
                    case "fault":
                        if (child.FirstChild == null)
                        {
                            throw new ParseException("Expected a child under 'fault', received none. ");
                        }

                        throw new FaultException(child.FirstChild.InnerXml);
                    case "methodName":
                        methodName = child.InnerText;
                        break;
                    default:
                        throw new ParseException(
                            $"Expected 'params', 'fault', or 'methodName', got '{child.Name}'");
                }
            } while ((child = child.NextSibling) != null);

            if (methodName == null || args == null)
            {
                throw new ParseException($"Unknown function '{methodName}' or invalid arguments");
            }

            return (methodName, args);
        }
    }
}