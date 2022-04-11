#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public class DropdownWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? label;
        [SerializeField] TMP_Dropdown? dropdown;

        readonly List<TMP_Dropdown.OptionData> optionDatas = new();

        TMP_Text Label => label.AssertNotNull(nameof(label));
        TMP_Dropdown Dropdown => dropdown.AssertNotNull(nameof(dropdown));
        
        public event Action<int, string>? ValueChanged;
        
        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        } 
        
        public string Text
        {
            get => Label.text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                Label.text = value;
                name = "Dropdown:" + value;
            }
        }

        public string Value
        {
            get => optionDatas[Index].text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                int index = optionDatas.FindIndex(data => data.text == value);
                if (index == -1)
                {
                    RosLogger.Error($"{nameof(DropdownWidget)}: Value {value} does not correspond to any index");
                    return;
                }

                Index = index;
            }
        }

        public int Index
        {
            get => Dropdown.value;
            set => Dropdown.value = value;
        }

        public bool Interactable
        {
            get => Dropdown.interactable;
            set
            {
                Label.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
                Dropdown.interactable = value;
            }
        }

        public IEnumerable<string> Options
        {
            get => Dropdown.options.Select(x => x.text);
            set
            {
                optionDatas.Clear();
                optionDatas.AddRange(value.Select(x => new TMP_Dropdown.OptionData(x)));
                Dropdown.options = optionDatas;
            }
        }

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
            Text = f;
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

        public DropdownWidget OverrideCaption(string f)
        {
            Dropdown.value = 0;
            Dropdown.captionText.text = f;
            return this;
        }
    }
}