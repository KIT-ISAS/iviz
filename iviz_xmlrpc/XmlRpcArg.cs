//#define DEBUG__

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using Iviz.Tools;

namespace Iviz.XmlRpc;

/// <summary>
/// A simplified version of <see cref="RosParameterValue"/>.
/// It converts a value directly to an XML-RPC string and stores that.
/// </summary>
public readonly struct XmlRpcArg
{
    readonly string? content;

    public bool IsValid => content != null;

    XmlRpcArg(bool f)
    {
        content = f
            ? "<value><boolean>1</boolean></value>\n"
            : "<value><boolean>0</boolean></value>\n";
    }

    XmlRpcArg(double f)
    {
        content = f == 0
            ? "<value><double>0</double></value>\n"
            : $"<value><double>{f.ToString(Defaults.Culture)}</double></value>\n";
    }

    XmlRpcArg(int f)
    {
        content = f switch
        {
            0 => "<value><i4>0</i4></value>\n",
            1 => "<value><i4>1</i4></value>\n",
            _ => $"<value><i4>{f.ToString(Defaults.Culture)}</i4></value>\n"
        };
    }

    XmlRpcArg(Uri f) : this(f.ToString())
    {
    }

    public XmlRpcArg(Uri[] f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
    {
    }

    XmlRpcArg(in DateTime f)
    {
        content = $"<value><dateTime.iso8601>{f.ToString("yyyy-MM-ddTHH:mm:ssZ")}</dateTime.iso8601></value>\n";
    }

    public XmlRpcArg(string f)
    {
        if (f == null)
        {
            BaseUtils.ThrowArgumentNull(nameof(f));
        }

        content = f.Length == 0
            ? "<value></value>\n"
            : $"<value>{HttpUtility.HtmlEncode(f)}</value>\n";
    }

    public XmlRpcArg(IReadOnlyList<string> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
    {
    }

    public XmlRpcArg(IEnumerable<string> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
    {
    }

    XmlRpcArg(IReadOnlyList<string[]> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
    {
    }

    XmlRpcArg((string A, string B) f) : this(((XmlRpcArg)f.A, (XmlRpcArg)f.B))
    {
    }

    public XmlRpcArg(IEnumerable<(string, string)> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
    {
    }

    public XmlRpcArg(IReadOnlyList<(string, string)> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
    {
    }

    XmlRpcArg(XmlRpcArg[] fs)
    {
        if (fs == null)
        {
            BaseUtils.ThrowArgumentNull(nameof(fs));
        }

        if (fs.Length == 0)
        {
            content = "<value><array><data></data></array></value>";
            return;
        }

        using var str = BuilderPool.Rent();

        str.Append("<value><array><data>");
        foreach (var f in fs)
        {
            str.Append(f.content);
        }

        str.Append("</data></array></value>");

        content = str.ToString();
    }

    public XmlRpcArg(int code, string message, XmlRpcArg arg)
    {
        using var str = BuilderPool.Rent();
        str.Append("<value><array><data>");
        str.Append(new XmlRpcArg(code).content);
        str.Append(new XmlRpcArg(message).content);
        str.Append(arg.content);
        str.Append("</data></array></value>");
        content = str.ToString();
    }

    XmlRpcArg((XmlRpcArg first, XmlRpcArg second) f)
    {
        content =
            "<value><array><data>" +
            f.first.content +
            f.second.content +
            "</data></array></value>";
    }

    XmlRpcArg((XmlRpcArg first, XmlRpcArg second, XmlRpcArg third) f)
    {
        using var str = BuilderPool.Rent();
        str.Append("<value><array><data>");
        str.Append(f.first.content);
        str.Append(f.second.content);
        str.Append(f.third.content);
        str.Append("</data></array></value>");
        content = str.ToString();
    }

    XmlRpcArg(XmlRpcArg[][] f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
    {
    }

    XmlRpcArg((XmlRpcArg, XmlRpcArg)[] f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
    {
    }

    XmlRpcArg(byte[] f)
    {
        if (f == null)
        {
            BaseUtils.ThrowArgumentNull(nameof(f));
        }

        content = f.Length == 0
            ? "<value><base64></base64></value>\n"
            : $"<value><base64>{Convert.ToBase64String(f)}</base64></value>\n";
    }

    public XmlRpcArg((string name, XmlRpcArg value)[] fs)
    {
        if (fs == null)
        {
            BaseUtils.ThrowArgumentNull(nameof(fs));
        }

        if (fs.Length == 0)
        {
            content = "<value><struct></struct></value>";
            return;
        }

        using var str = BuilderPool.Rent();
        str.Append("<value><struct>");

        foreach (var (name, arg) in fs)
        {
            str.Append("<member><name>");
            str.Append(name);
            str.Append("</name>");
            str.Append(arg.content);
            str.Append("</member>");
        }

        str.Append("</struct></value>");
        content = str.ToString();
    }

    public override string ToString()
    {
        return content ?? "[Invalid XmlRpcArg]";
    }

    public void ThrowIfEmpty()
    {
        if (!IsValid)
        {
            throw new InvalidOperationException("Arg is empty");
        }
    }

    public static implicit operator string(XmlRpcArg arg) => arg.ToString();

    public static implicit operator XmlRpcArg(bool f) => new(f);

    public static implicit operator XmlRpcArg(double f) => new(f);

    public static implicit operator XmlRpcArg(int f) => new(f);

    public static implicit operator XmlRpcArg(Uri f) => new(f);

    public static implicit operator XmlRpcArg(DateTime f) => new(f);

    public static implicit operator XmlRpcArg(Uri[] f) => new(f);

    public static implicit operator XmlRpcArg(byte[] f) => new(f);

    public static implicit operator XmlRpcArg(string f) => new(f);

    public static implicit operator XmlRpcArg(string[] f) => new(f);

    public static implicit operator XmlRpcArg(string[][] f) => new(f);

    public static implicit operator XmlRpcArg((string, string) f) => new(f);

    public static implicit operator XmlRpcArg((string, string)[] f) => new(f);

    public static implicit operator XmlRpcArg((string name, XmlRpcArg value)[] f) => new(f);

    public static implicit operator XmlRpcArg(XmlRpcArg[] f) => new(f);

    public static implicit operator XmlRpcArg(XmlRpcArg[][] f) => new(f);

    public static implicit operator XmlRpcArg((XmlRpcArg, XmlRpcArg) f) => new(f);

    public static implicit operator XmlRpcArg((XmlRpcArg, XmlRpcArg)[] f) => new(f);

    public static implicit operator XmlRpcArg((XmlRpcArg, XmlRpcArg, XmlRpcArg) f) => new(f);

    public static implicit operator XmlRpcArg(RosParameterValue f) => f.ValueType switch
    {
        RosParameterValue.Type.Integer => f.TryGetInteger(out int l) ? l : ThrowCannotHappen(),
        RosParameterValue.Type.Empty => throw new InvalidOperationException("Empty object"),
        RosParameterValue.Type.Double => f.TryGetDouble(out double l) ? l : ThrowCannotHappen(),
        RosParameterValue.Type.Boolean => f.TryGetBoolean(out bool l) ? l : ThrowCannotHappen(),
        RosParameterValue.Type.DateTime => f.TryGetDateTime(out DateTime l) ? l : ThrowCannotHappen(),
        RosParameterValue.Type.String => f.TryGetString(out string l) ? l : ThrowCannotHappen(),
        RosParameterValue.Type.Array => f.TryGetArray(out var l)
            ? l.Select(wrapper => (XmlRpcArg)wrapper).ToArray()
            : ThrowCannotHappen(),
        RosParameterValue.Type.Base64 => f.TryGetBase64(out byte[] l) ? l : ThrowCannotHappen(),
        RosParameterValue.Type.Struct => f.TryGetStruct(out var l)
            ? l.Select(entry => (entry.Key, (XmlRpcArg)entry.Value)).ToArray()
            : ThrowCannotHappen(),
        _ => throw new ArgumentOutOfRangeException()
    };

    [DoesNotReturn]
    static XmlRpcArg ThrowCannotHappen() => throw new InvalidOperationException();
}