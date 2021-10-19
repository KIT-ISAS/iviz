using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Iviz.Core;
using Iviz.Tools;
using JetBrains.Annotations;
using TMPro;

namespace Iviz.App
{
    public sealed class LineLog : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text text2 = null;

        static readonly char[] Separators = { '\n' };

        readonly List<string> lines = new List<string>();

        const int MaxLines = 100;

        bool active = true;

        public bool Active
        {
            get => active;
            set
            {
                active = value;
                if (value)
                {
                    Flush();
                }
            }
        }

        public void Add([NotNull] string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.IndexOf('\n') != -1)
            {
                string[] subLines = str.Split(Separators);
                lines.AddRange(subLines);
            }
            else
            {
                lines.Add(str);
            }
        }

        public void Flush()
        {
            if (!Active)
            {
                return;
            }

            int overflow = lines.Count - MaxLines;
            if (overflow > 0)
            {
                lines.RemoveRange(0, overflow);
            }

            var description = BuilderPool.Rent();
            try
            {
                if (lines.Count != 0)
                {
                    description.Append(lines[0]);
                    foreach (string line in lines.Skip(1))
                    {
                        description.Append('\n');
                        description.Append(line);
                    }
                }

                text2.SetText(description);
            }
            finally
            {
                BuilderPool.Return(description);
            }
        }

        public void ClearSubscribers()
        {
            Active = false;
        }
    }
}