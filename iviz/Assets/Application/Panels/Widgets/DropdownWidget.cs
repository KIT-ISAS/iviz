using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class DropdownWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label = null;
        [SerializeField] Dropdown dropdown = null;

        public string Label
        {
            get => label.text;
            set
            {
                label.text = value;
                name = "Dropdown:" + value;
            }
        }

        public string Value
        {
            get => optionDatas[Index].text;
            set
            {
                Index = optionDatas.FindIndex(x => x.text == value);
            }
        }

        public int Index
        {
            get => dropdown.value;
            set
            {
                dropdown.value = value;
            }
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

        public DropdownWidget SetInteractable(bool f)
        {
            Interactable = f;
            return this;
        }

        public DropdownWidget SetIndex(int f)
        {
            Index = f;
            return this;
        }

        public DropdownWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public DropdownWidget SetOptions(IEnumerable<string> f)
        {
            Options = f;
            return this;
        }

        public DropdownWidget SubscribeValueChanged(Action<int, string> f)
        {
            ValueChanged += f;
            return this;
        }
    }
}