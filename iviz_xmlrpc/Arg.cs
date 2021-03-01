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
    /// Wrapper around an XML-RPC function argument.
    /// </summary>
    public readonly struct Arg
    {
        readonly string? content;

        public bool IsValid => content != null;

        Arg(bool f)
        {
            content = f ? "<value><boolean>1</boolean></value>\n" : "<value><boolean>0</boolean></value>\n";
        }

        Arg(double f)
        {
            content = $"<value><double>{f.ToString(BuiltIns.Culture)}</double></value>\n";
        }

        Arg(int f)
        {
            content = f == 0
                ? "<value><i4>0</i4></value>\n"
                : $"<value><i4>{f.ToString(BuiltIns.Culture)}</i4></value>\n";
        }

        Arg(Uri f) : this(ThrowIfNull(f).ToString())
        {
        }

        public Arg(IEnumerable<Uri> f) : this(ThrowIfNull(f).Select(x => new Arg(x)).ToArray())
        {
        }

        Arg(in DateTime f)
        {
            content = $"<value><dateTime.iso8601>{f.ToString("yyyy-MM-ddTHH:mm:ssZ")}</dateTime.iso8601></value>\n";
        }

        public Arg(string f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            content = $"<value>{HttpUtility.HtmlEncode(f)}</value>\n";
        }

        public Arg(string[] f) : this(ThrowIfNull(f).Select(x => new Arg(x)).ToArray())
        {
        }

        public Arg(IEnumerable<string> f) : this(ThrowIfNull(f).Select(x => new Arg(x)).ToArray())
        {
        }

        public Arg(string[][] f) : this(ThrowIfNull(f).Select(x => new Arg(x)).ToArray())
        {
        }

        Arg((string A, string B) f) : this(new Arg[] {ThrowIfNull(f.A), ThrowIfNull(f.B)})
        {
        }

        public Arg(IEnumerable<(string, string)> f) : this(ThrowIfNull(f).Select(x => new Arg(x)).ToArray())
        {
        }

        public Arg((string, string)[] f) : this(ThrowIfNull(f).Select(x => new Arg(x)).ToArray())
        {
        }

        Arg(Arg[] f)
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

        public Arg(Arg[][] f) : this(ThrowIfNull(f).Select(x => new Arg(x)).ToArray())
        {
        }

        Arg(byte[] f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            content = $"<value><base64>{Convert.ToBase64String(f)}</base64></value>\n";
        }

        public Arg(List<(string name, Arg value)> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            StringBuilder builder = new StringBuilder(100);
            builder.Append("<value><struct>");
            foreach (var (name, arg) in f)
            {
                builder.Append("<member><name>").Append(name).Append("</name>").Append(arg.content).Append("</member>");
            }

            builder.Append("</struct></value>");
            content = builder.ToString();
        }

        public Arg(List<(string name, object value)> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            StringBuilder builder = new StringBuilder(100);
            builder.Append("<value><struct>");
            foreach (var (name, obj) in f)
            {
                builder.Append("<member><name>").Append(name).Append("</name>").Append(Create(obj).content)
                    .Append("</member>");
            }

            builder.Append("</struct></value>");
            content = builder.ToString();
        }

        static T ThrowIfNull<T>(T t)
        {
            return t ?? throw new NullReferenceException(nameof(t));
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

        public static implicit operator string(Arg arg)
        {
            return arg.ToString();
        }

        public static implicit operator Arg(bool f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(double f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(int f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(Uri f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(Uri[] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(string f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(string[] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(string[][] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg((string, string) f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg((string, string)[] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(Arg[] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(Arg[][] f)
        {
            return new Arg(f);
        }

        public static Arg Create(object o)
        {
            return o switch
            {
                bool i => new Arg(i),
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
    }
}