//#define DEBUG__

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Iviz.Msgs;

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
            content = f ? "<value><boolean>1</boolean></value>\n" : "<value><boolean>0</boolean></value>\n";
        }

        XmlRpcArg(double f)
        {
            content = $"<value><double>{f.ToString(BuiltIns.Culture)}</double></value>\n";
        }

        XmlRpcArg(int f)
        {
            content = f switch
            {
                0 => "<value><i4>0</i4></value>\n",
                1 => "<value><i4>1</i4></value>\n",
                _ => $"<value><i4>{f.ToString(BuiltIns.Culture)}</i4></value>\n"
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

            content = $"<value>{HttpUtility.HtmlEncode(f)}</value>\n";
        }

        public XmlRpcArg(string[] f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
        {
        }

        public XmlRpcArg(IEnumerable<string> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
        {
        }

        XmlRpcArg(string[][] f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
        {
        }

        XmlRpcArg((string A, string B) f) : this(((XmlRpcArg) f.A, (XmlRpcArg) f.B))
        {
        }

        public XmlRpcArg(IEnumerable<(string, string)> f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
        {
        }

        XmlRpcArg((string, string)[] f) : this(f.Select(x => new XmlRpcArg(x)).ToArray())
        {
        }

        XmlRpcArg(XmlRpcArg[] f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
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

            content = $"<value><base64>{Convert.ToBase64String(f)}</base64></value>\n";
        }

        public XmlRpcArg((string name, XmlRpcArg value)[] f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
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

        /*
        public static XmlRpcArg Create(object o)
        {
            return o switch
            {
                bool i => new XmlRpcArg(i),
                double i => new XmlRpcArg(i),
                int i => new XmlRpcArg(i),
                string i => new XmlRpcArg(i),
                object[] i => new XmlRpcArg(i.Select(Create).ToArray()),
                byte[] i => new XmlRpcArg(i),
                DateTime i => new XmlRpcArg(i),
                (string, object)[] i => new XmlRpcArg(i),
                _ => throw new InvalidOperationException("Type is not supported")
            };
        }
        */

        /*
        public static Arg Create(in ObjectWrapper o)
        {
            return o.Type switch
            {
                ObjectType.Integer => o.TryGetInteger(out var value) ? value : throw new InvalidCastException(),
                ObjectType.Double => o.TryGetDouble(out var value) ? value : throw new InvalidCastException(),
                double i => new Arg(i),
                int i => new Arg(i),
                string i => new Arg(i),
                object[] i => new Arg(i.Select(Create).ToArray()),
                byte[] i => new Arg(i),
                DateTime i => new Arg(i),
                List<(string, object)> i => new Arg(i),
                _ => throw new InvalidOperationException("Type is not supported")
            };
        }
        */
    }
}