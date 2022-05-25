#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Tools;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class LineLog : MonoBehaviour, IWidget
    {
        const int MaxLines = 100;

        //static readonly char[] Separators = { '\n' };

        [SerializeField] TMP_Text? text;

        readonly List<string> lines = new();
        bool active = true;

        TMP_Text Text => text.AssertNotNull(nameof(text));

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

        public void Add(string str)
        {
            ThrowHelper.ThrowIfNull(str, nameof(str));

            if (str.IndexOf('\n') != -1)
            {
                //string[] subLines = str.Split(Separators);
                string[] subLines = str.Split('\n');
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

            if (lines.Count == 0)
            {
                Text.SetText("");
                return;
            }

            int overflow = lines.Count - MaxLines;
            if (overflow > 0)
            {
                lines.RemoveRange(0, overflow);
            }

            using var description = BuilderPool.Rent();
            description.Append(lines[0]);
            foreach (string line in lines.Skip(1))
            {
                description.Append('\n');
                description.Append(line);
            }

            Text.SetTextRent(description);
        }

        public void ClearSubscribers()
        {
            Active = false;
        }
    }
}