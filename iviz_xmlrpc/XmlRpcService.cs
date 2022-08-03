//#define DEBUG__

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Iviz.Tools;

namespace Iviz.XmlRpc;

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

    static RosParameterValue Parse(XmlNode? value)
    {
        if (value == null)
        {
            throw new ParseException("Expected child node but received null!");
        }

        Assert(value.Name, "value");
        if (!value.HasChildNodes)
        {
            return new RosParameterValue(value.InnerText);
        }

        XmlNode? primitive = value.FirstChild;
        if (primitive is XmlText)
        {
            return new RosParameterValue(primitive.InnerText);
        }

        switch (primitive?.Name)
        {
            case null:
                throw new ParseException("Expected node name but received null!");
            case "double":
                return double.TryParse(primitive.InnerText, NumberStyles.Number, Defaults.Culture,
                    out double @double)
                    ? new RosParameterValue(@double)
                    : throw new ParseException($"Could not parse '{primitive.InnerText}' as double!");
            case "i4" or "int":
                return int.TryParse(primitive.InnerText, NumberStyles.Number, Defaults.Culture, out int @int)
                    ? new RosParameterValue(@int)
                    : throw new ParseException($"Could not parse '{primitive.InnerText}' as integer!");
            case "boolean":
                return new RosParameterValue(primitive.InnerText == "1");
            case "string":
                return new RosParameterValue(primitive.InnerText);
            case "array" when primitive.FirstChild?.Name != "data":
                throw new ParseException($"Expected 'data' but received '{primitive.FirstChild?.Name}'");
            case "array":
                return new RosParameterValue(primitive.FirstChild!.ChildNodes.Cast<XmlNode?>().Select(Parse).ToArray());
            case "dateTime.iso8601":
                return DateTime.TryParseExact(primitive.InnerText, "yyyy-MM-ddTHH:mm:ssZ",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt)
                    ? new RosParameterValue(dt)
                    : new RosParameterValue(DateTime.MinValue);
            case "base64":
                return TryParseDate(primitive);
            case "struct":
                return TryParseStruct(primitive);
            default:
                throw new ParseException($"Could not parse type '{primitive.Name}'");
        }
    }

    static RosParameterValue TryParseDate(XmlNode primitive)
    {
        try
        {
            return new RosParameterValue(Convert.FromBase64String(primitive.InnerText));
        }
        catch (FormatException e)
        {
            throw new ParseException($"Could not parse '{primitive.InnerText}' as base64!", e);
        }
    }

    static RosParameterValue TryParseStruct(XmlNode primitive)
    {
        var structValue = new List<(string, RosParameterValue)>();
        foreach (XmlNode? member in primitive.ChildNodes)
        {
            if (member is not { Name: "member" })
            {
                continue;
            }

            string? entryName = null;
            RosParameterValue entryValue = default;
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

            if (entryName is null || entryValue.IsEmpty)
            {
                Logger.Log($"{nameof(XmlRpcService)}: Invalid struct entry");
                continue;
            }

            structValue.Add((entryName, entryValue));
        }

        return new RosParameterValue(structValue.ToArray());                
    }
        
    internal static Rent<byte> CreateRequest(string method, XmlRpcArg[] args)
    {
        using var str = BuilderPool.Rent();
        str.Append("<?xml version=\"1.0\"?>\n" +
                   "<methodCall>\n");
        str.Append("<methodName>").Append(method).Append("</methodName>\n");

        str.Append("<params>\n");
        foreach (var arg in args)
        {
            str.Append("<param>").Append(arg.ToString()).Append("</param>\n");
        }

        str.Append("</params>\n" +
                   "</methodCall>\n");

        //Logger.Log(">> Payload " + str);

        return str.AsRent();
    }

    internal static RosParameterValue ProcessResponseOfMethodCall(string inData)
    {
        var document = new XmlDocument();
        try
        {
            document.LoadXml(inData);
        }
        catch (XmlException e)
        {
            throw new ParseException("XML response could not be parsed", e);
        }

        var root = document.FirstChild;
        while (root != null && root.Name != "methodResponse")
        {
            root = root.NextSibling;
        }

        if (root is null)
        {
            throw new ParseException("Response has no 'methodResponse' tag");
        }

        var child = root.FirstChild;

        switch (child?.Name)
        {
            case null:
                throw new ParseException("'methodResponse' tag has no children");
            case "params" when child.ChildNodes.Count == 0:
                throw new ParseException("Empty response");
            case "params" when child.ChildNodes.Count > 1:
                throw new ParseException("Function call returned too many arguments");
            case "params" when child.FirstChild?.Name != "param":
                throw new ParseException($"Expected 'param' but received '{child.FirstChild?.Name}'");
            case "params":
                return Parse(child.FirstChild?.FirstChild);
            case "fault" when child.FirstChild == null:
                throw new ParseException("Expected a child under 'fault', received none");
            case "fault":
                throw new FaultException(child.FirstChild.InnerXml);
            default:
                throw new ParseException($"Expected 'params' or 'fault', but got '{child.Name}'");
        }

        /*
                XmlNode? param = child.FirstChild;
                Assert(param?.Name, "param");
                return Parse(param!.FirstChild);
         */
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
    public static async ValueTask<RosParameterValue> MethodCallAsync(Uri remoteUri, Uri callerUri, string method,
        XmlRpcArg[] args,
        CancellationToken token = default)
    {
        if (remoteUri is null) BaseUtils.ThrowArgumentNull(nameof(remoteUri));
        if (callerUri is null) BaseUtils.ThrowArgumentNull(nameof(callerUri));
        if (args is null) BaseUtils.ThrowArgumentNull(nameof(args));

        string inData;

        try
        {
            using var outData = CreateRequest(method, args);
            using var request = new HttpRequest(callerUri, remoteUri);

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

        return ProcessResponseOfMethodCall(inData);
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
    public static RosParameterValue MethodCall(Uri remoteUri, Uri callerUri, string method, XmlRpcArg[] args,
        CancellationToken token = default)
    {
        return TaskUtils.RunSync(() => MethodCallAsync(remoteUri, callerUri, method, args, token));
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
    public static async ValueTask MethodResponseAsync(
        HttpListenerContext httpContext,
        IReadOnlyDictionary<string, Func<RosParameterValue[], XmlRpcArg>> methods,
        IReadOnlyDictionary<string, Func<RosParameterValue[], CancellationToken, ValueTask>>? lateCallbacks = null,
        CancellationToken token = default)
    {
        if (httpContext is null) BaseUtils.ThrowArgumentNull(nameof(httpContext));
        if (methods is null) BaseUtils.ThrowArgumentNull(nameof(methods));

        string inData = await httpContext.GetRequestAsync(token: token);

        try
        {
            var (methodName, args) = ProcessResponseOfMethodResponse(inData);

            if (!methods.TryGetValue(methodName, out var method))
            {
                throw new ParseException($"Unknown function '{methodName}' or invalid arguments");
            }

            var response = method(args);

            Rent<byte> outData;
            using (var str = BuilderPool.Rent())
            {
                str.Append("<?xml version=\"1.0\"?>\n");
                str.Append("<methodResponse>\n");
                str.Append("<params>\n");
                str.Append("<param>\n");
                str.Append(response.ToString()).Append("\n");
                str.Append("</param>\n");
                str.Append("</params>\n");
                str.Append("</methodResponse>\n");
                str.Append("\n");
                outData = str.AsRent();
            }

            using (outData)
            {
                await httpContext.RespondAsync(outData, token: token);
            }

            if (lateCallbacks != null &&
                lateCallbacks.TryGetValue(methodName, out var lateCallback))
            {
                await lateCallback(args, token);
            }
        }
        catch (ParseException e)
        {
            Rent<byte> response;
            using (var str = BuilderPool.Rent())
            {
                str.Append("<?xml version=\"1.0\"?>\n");
                str.Append("<methodResponse>\n");
                str.Append("<fault>\n");
                str.Append(new XmlRpcArg(e.Message).ToString()).Append("\n");
                str.Append("</fault>\n");
                str.Append("</methodResponse>\n");
                response = str.AsRent();
            }

            using (response)
            {
                await httpContext.RespondAsync(response, token: token);
            }
        }
        catch (Exception e)
        {
            if (e is IOException or SocketException or OperationCanceledException)
            {
                throw;
            }

            Logger.LogErrorFormat(nameof(XmlRpcService) + ": Error during parsing. {0}", e);
            await httpContext.RespondWithUnexpectedErrorAsync(token: token);
            throw;
        }
    }

    static (string methodName, RosParameterValue[] args) ProcessResponseOfMethodResponse(string inData)
    {
        var document = new XmlDocument();

        try
        {
            document.LoadXml(inData);
        }
        catch (XmlException e)
        {
            throw new ParseException("XML response could not be parsed", e);
        }

        var root = document.FirstChild;
        while (root != null && root.Name != "methodCall")
        {
            root = root.NextSibling;
        }

        if (root == null)
        {
            throw new ParseException("Malformed request: no 'methodCall' found");
        }

        string? methodName = null;
        RosParameterValue[]? args = null;
        XmlNode? child = root.FirstChild;
        do
        {
            switch (child?.Name)
            {
                case null:
                    throw new ParseException("Expected 'params', 'fault', or 'methodName'; got null instead");
                case "params":
                {
                    if (child.ChildNodes.Count == 0)
                    {
                        args = Array.Empty<RosParameterValue>();
                        break;
                    }

                    args = new RosParameterValue[child.ChildNodes.Count];
                    for (int i = 0; i < child.ChildNodes.Count; i++)
                    {
                        XmlNode? param = child.ChildNodes[i];
                        Assert(param?.Name, "param");
                        args[i] = Parse(param?.FirstChild);
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