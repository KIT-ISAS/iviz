using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class DropdownWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label = null;
        [SerializeField] Dropdown dropdown = null;

        [NotNull]
        public string Label
        {
            get => label.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                label.text = value;
                name = "Dropdown:" + value;
            }
        }

        [NotNull]
        public string Value
        {
            get => optionDatas[Index].text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                int index = optionDatas.FindIndex(x => x.text == value);
                if (index == -1)
                {
                    throw new InvalidOperationException("Value does not correspond to any index");
                }

                Index = index;
            }
        }

        public int Index
        {
            get => dropdown.value;
            set => dropdown.value = value;
        }

        public bool Interactable
        {
            get => dropdown.interactable;
            set
            {
                label.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
                dropdown.interactable = value;
            }
        }

        readonly List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        [NotNull]
        public IEnumerable<string> Options
        {
            get => optionDatas.Select(x => x.text);
            set
            {
                optionDatas.Clear();
                optionDatas.AddRange(value.Select(x => new Dropdown.OptionData(x)));
                dropdown.options = optionDatas;
            }
        }

        public event Action<int, string> ValueChanged;

        public void OnValueChanged(int i)
        {
            ValueChanged?.Invoke(i, optionDatas[i].text);
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }

        [NotNull]
        public DropdownWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        [NotNull]
        public DropdownWidget SetIndex(int f)
        {
            Index = f;
            return this;
        }

        [NotNull]
        public DropdownWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        [NotNull]
        public DropdownWidget SetOptions([NotNull] IEnumerable<string> f)
        {
            Options = f;
            return this;
        }

        [NotNull]
        public DropdownWidget SubscribeValueChanged(Action<int, string> f)
        {
            ValueChanged += f;
            return this;
        }
    }
}