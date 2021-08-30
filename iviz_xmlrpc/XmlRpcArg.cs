//#define DEBUG__

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Iviz.Tools;

namespace Iviz.XmlRpc
{
    /// <summary>
    /// A simplified version of <see cref="XmlRpcValue"/>.
    /// It converts the value directly to an XML-RPC string and stores that.
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
                throw new ArgumentNullException(nameof(f));
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

        XmlRpcArg((string A, string B) f) : this(((XmlRpcArg) f.A, (XmlRpcArg) f.B))
        {
        }

        public XmlRpcArg(IEnumerable<(string, string)> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
        {
        }

        public XmlRpcArg(IReadOnlyList<(string, string)> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
        {
        }

        XmlRpcArg(XmlRpcArg[] f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            if (f.Length == 0)
            {
                content = "<value><array><data></data></array></value>";
                return;
            }

            string?[] fs = new string[f.Length + 2];
            fs[0] = "<value><array><data>";
            for (int i = 0; i < f.Length; i++)
            {
                fs[i + 1] = f[i].content;
            }

            fs[f.Length + 1] = "</data></array></value>";

            content = string.Concat(fs);
        }


        XmlRpcArg((int code, string message, XmlRpcArg arg) f)
        {
            content =
                "<value><array><data>" +
                new XmlRpcArg(f.code).content +
                new XmlRpcArg(f.message).content +
                f.arg.content +
                "</data></array></value>";
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
            content =
                "<value><array><data>" +
                f.first.content +
                f.second.content +
                f.third.content +
                "</data></array></value>";
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
                throw new ArgumentNullException(nameof(f));
            }

            content = f.Length == 0
                ? "<value><base64></base64></value>\n"
                : $"<value><base64>{Convert.ToBase64String(f)}</base64></value>\n";
        }

        public XmlRpcArg((string name, XmlRpcArg value)[] f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            if (f.Length == 0)
            {
                content = "<value><struct></struct></value>";
                return;
            }

            StringBuilder builder = new(100);
            builder.Append("<value><struct>");
            foreach (var (name, arg) in f)
            {
                builder.Append("<member><name>").Append(name).Append("</name>").Append(arg.content)
                    .Append("</member>");
            }

            builder.Append("</struct></value>");
            content = builder.ToString();
        }

        public override string ToString()
        {
            return content ?? throw new InvalidOperationException("Arg has no valid value");
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

        public static implicit operator XmlRpcArg((int, string, XmlRpcArg) f) => new(f);
    }
}