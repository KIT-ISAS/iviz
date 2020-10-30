using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class InputFieldWithHintsWidget : MonoBehaviour, IWidget
    {
        [SerializeField] InputFieldWidget input = null;
        [SerializeField] Dropdown dropdown = null;
        Dropdown reserve = null;

        [NotNull]
        public string Label
        {
            get => input.Label;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                input.Label = value;
            }
        }

        [NotNull]
        public string Value
        {
            get => input.Value;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                input.Value = value;
            }
        }

        [NotNull]
        public string Placeholder
        {
            get => input.Placeholder;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                input.Placeholder = value;
            }
        }

        public bool Interactable
        {
            get => input.Interactable;
            set
            {
                input.Interactable = value;
                dropdown.interactable = value;
            }
        }

        readonly List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

        [NotNull]
        public IEnumerable<string> Hints
        {
            get => optionDatas.Select(x => x.text);
            set
            {
                optionDatas.Clear();
                optionDatas.AddRange(value.Select(x => new Dropdown.OptionData(x)));
                dropdown.options = optionDatas;
            }
        }

        void Awake()
        {
            input.EndEdit += OnEndEdit;
            dropdown.onValueChanged.AddListener(OnValueChanged);
            reserve = Instantiate(dropdown.gameObject, transform).GetComponent<Dropdown>();
            reserve.gameObject.SetActive(false);
        }

        public event Action<string> EndEdit;

        void OnValueChanged(int i)
        {
            input.Value = optionDatas[i].text;
            EndEdit?.Invoke(optionDatas[i].text);

            // awkward workaround for the fact that Unity won't allow the selection to be reset
            // however, we want the user to be able to select the same item multiple times
            // so we simply destroy the widget and recreate a saved version with no selection
            Destroy(dropdown.gameObject);
            dropdown = Instantiate(reserve.gameObject, transform).GetComponent<Dropdown>();
            dropdown.gameObject.SetActive(true);
            dropdown.onValueChanged.AddListener(OnValueChanged);
            dropdown.options = optionDatas;
        }

        void OnEndEdit(string f)
        {
            EndEdit?.Invoke(f);
        }

        public void ClearSubscribers()
        {
            EndEdit = null;
        }

        [NotNull]
        public InputFieldWithHintsWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        [NotNull]
        public InputFieldWithHintsWidget SetValue([NotNull] string f)
        {
            Value = f;
            return this;
        }

        [NotNull]
        public InputFieldWithHintsWidget SetPlaceholder([NotNull] string f)
        {
            Placeholder = f;
            return this;
        }

        [NotNull]
        public InputFieldWithHintsWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        [NotNull]
        public InputFieldWithHintsWidget SetOptions([NotNull] IEnumerable<string> f)
        {
            Hints = f;
            return this;
        }

        [NotNull]
        public InputFieldWithHintsWidget SubscribeEndEdit(Action<string> f)
        {
            EndEdit += f;
            return this;
        }
    }
}