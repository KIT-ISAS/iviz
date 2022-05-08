#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class InputFieldWithHintsWidget : MonoBehaviour, IWidget
    {
        [SerializeField] InputFieldWidget? input;
        [SerializeField] TMP_Dropdown? dropdown;
        [SerializeField] Image? image;

        readonly List<TMP_Dropdown.OptionData> optionDatas = new();

        InputFieldWidget Input => input.AssertNotNull(nameof(input));

        TMP_Dropdown Dropdown
        {
            get => dropdown.AssertNotNull(nameof(dropdown));
            set => dropdown = value.CheckedNull() ?? throw new NullReferenceException("Cannot set dropdown to null!");
        }

        public event Action<string>? EndEdit;
        public event Action<int>? ValueChanged;

        public string Title
        {
            get => Input.Title;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                Input.Title = value;
            }
        }

        public string Value
        {
            get => Input.Value;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                Input.Value = value;
            }
        }

        public string Placeholder
        {
            get => Input.Placeholder;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                Input.Placeholder = value;
            }
        }

        public bool Interactable
        {
            get => Input.Interactable;
            set
            {
                Input.Interactable = value;
                Dropdown.interactable = value;
            }
        }

        public IEnumerable<string> Hints
        {
            set
            {
                optionDatas.Clear();
                optionDatas.AddRange(value.Select(x => new TMP_Dropdown.OptionData(x)));
                Dropdown.options = optionDatas;

                CheckPreferredWidth();
            }
        }

        public void SetHints<T>(T hints) where T : IReadOnlyList<string>
        {
            optionDatas.Clear();
            foreach (int i in ..hints.Count)
            {
                optionDatas.Add(new TMP_Dropdown.OptionData(hints[i]));
            }

            Dropdown.options = optionDatas;

            CheckPreferredWidth();
        }

        void CheckPreferredWidth()
        {
            // this is really hackish, go to the spawned item list, traverse all texts and find the preferred width
            // I can't stand Unity's content size fitters, half the time they give out negative widths
            if (Dropdown.transform.childCount < 3)
            {
                return;
            }

            var content = (RectTransform)Dropdown.transform.GetChild(2).GetChild(0).GetChild(0);
            var textFields = content.GetComponentsInChildren<TMP_Text>();
            float max = textFields.Length == 0 ? 0 : textFields.Max(text => text.preferredWidth);
            content.sizeDelta = content.sizeDelta.WithX(max + 20);
        }

        void Awake()
        {
            Input.EndEdit += OnEndEdit;

            // this is actually an image with transparency 0, just so that TMP allows us to select index -1
            Dropdown.placeholder = image;

            Dropdown.onValueChanged.AddListener(OnValueChanged);
            Dropdown.value = -1;
        }

        void OnValueChanged(int i)
        {
            if (i == -1)
            {
                return;
            }

            Input.Value = optionDatas[i].text;
            EndEdit?.Invoke(optionDatas[i].text);
            ValueChanged?.Invoke(i);
            Dropdown.value = -1;
        }

        void OnEndEdit(string f)
        {
            EndEdit?.Invoke(f);
        }

        public void ClearSubscribers()
        {
            EndEdit = null;
        }

        public InputFieldWithHintsWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public InputFieldWithHintsWidget SetValue(string f)
        {
            Value = f;
            return this;
        }

        public InputFieldWithHintsWidget SetPlaceholder(string f)
        {
            Placeholder = f;
            return this;
        }

        public InputFieldWithHintsWidget SetLabel(string f)
        {
            Title = f;
            return this;
        }

        public InputFieldWithHintsWidget SetOptions(IEnumerable<string> f)
        {
            Hints = f;
            return this;
        }

        public InputFieldWithHintsWidget SubscribeEndEdit(Action<string> f)
        {
            EndEdit += f;
            return this;
        }
    }
}