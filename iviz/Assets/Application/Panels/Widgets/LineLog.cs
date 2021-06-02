using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class LineLog : MonoBehaviour, IWidget
    {
        [SerializeField] Text text = null;
        [SerializeField] GameObject content = null;

        static readonly char[] Separators = {'\n'};

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

            string[] subLines = str.Split(Separators);
            lines.AddRange(subLines);
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

            text.text = string.Join("\n", lines);
            float y = text.preferredHeight;

            RectTransform ctransform = (RectTransform)content.transform;
            ctransform.sizeDelta = new Vector2(0, y);
        }

        public void ClearSubscribers()
        {
            Active = false;
        }
    }
}