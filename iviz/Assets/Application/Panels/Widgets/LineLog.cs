﻿#nullable enable

using System;
using UnityEngine;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Tools;
using TMPro;

namespace Iviz.App
{
    public sealed class LineLog : MonoBehaviour, IWidget
    {
        const int MaxLines = 100;
        
        static readonly char[] Separators = { '\n' };

        [SerializeField] TMP_Text? text2 = null;
        
        readonly List<string> lines = new();
        bool active = true;

        TMP_Text Text2 => text2.AssertNotNull(nameof(text2));
        
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

                Text2.SetText(description);
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