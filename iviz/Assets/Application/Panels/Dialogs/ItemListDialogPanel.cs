#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ItemListDialogPanel : DialogPanel, IReadOnlyList<IItemEntry>
    {
        static float baseButtonHeight;

        static float BaseButtonHeight => baseButtonHeight != 0
            ? baseButtonHeight
            : baseButtonHeight = ((RectTransform)Resource.Widgets.ItemButton.Object.transform).rect.height;

        [SerializeField] GameObject? contentObject;
        [SerializeField] Text? emptyText;
        [SerializeField] TMP_Text? titleText;
        [SerializeField] SimpleButtonWidget? closeButton;
        [SerializeField] Canvas? canvas;

        readonly List<ItemEntry> itemEntries = new();

        ResourceKey<GameObject>? buttonType;

        Canvas Canvas => canvas.AssertNotNull(nameof(canvas));
        Text EmptyTextObject => emptyText.AssertNotNull(nameof(emptyText));
        TMP_Text TitleText => titleText.AssertNotNull(nameof(titleText));
        SimpleButtonWidget CloseButton => closeButton.AssertNotNull(nameof(closeButton));
        GameObject ContentObject => contentObject.AssertNotNull(nameof(contentObject));

        protected float VerticalOffset { get; set; } = 5;
        protected float ButtonHeight { get; set; }

        public event Action<int, int>? ItemClicked;
        public event Action? CloseClicked;

        public ResourceKey<GameObject> ButtonType
        {
            get => buttonType ??= Resource.Widgets.ItemButton;
            set => buttonType = value;
        }

        public string Title
        {
            set => TitleText.text = value;
        }

        public string EmptyText
        {
            set => EmptyTextObject.text = value;
        }

        public void SetItems<T>(T value) where T : IReadOnlyList<string>
        {
            if (ButtonHeight == 0)
            {
                ButtonHeight = BaseButtonHeight;
            }

            if (value.Count == itemEntries.Count)
            {
                foreach (int i in ..value.Count)
                {
                    itemEntries[i].Text = value[i];
                }
            }
            else if (value.Count < itemEntries.Count)
            {
                Canvas.enabled = false;

                foreach (int i in ..value.Count)
                {
                    itemEntries[i].Text = value[i];
                }

                foreach (int i in value.Count..itemEntries.Count)
                {
                    itemEntries[i].Dispose();
                }

                itemEntries.RemoveRange(value.Count, itemEntries.Count - value.Count);
                UpdateSize();
                Canvas.enabled = true;
            }
            else
            {
                Canvas.enabled = false;
                foreach (int i in ..value.Count)
                {
                    if (i >= itemEntries.Count)
                    {
                        itemEntries.Add(new ItemEntry(i, ContentObject, ButtonHeight, VerticalOffset, ButtonType,
                            RaiseClicked));
                    }

                    itemEntries[i].Text = value[i];
                }

                UpdateSize();
                Canvas.enabled = true;
            }
        }

        protected virtual void Start()
        {
            CloseButton.Clicked += RaiseClose;
        }

        void RaiseClicked(int id, int subId)
        {
            ItemClicked?.Invoke(id, subId);
        }

        void RaiseClose()
        {
            CloseClicked?.Invoke();
        }

        void UpdateSize()
        {
            var rectTransform = ((RectTransform)ContentObject.transform);
            rectTransform.sizeDelta =
                new Vector2(0, 2 * VerticalOffset + itemEntries.Count * (ButtonHeight + VerticalOffset));

            EmptyTextObject.gameObject.SetActive(itemEntries.Count == 0);
            foreach (var itemEntry in itemEntries)
            {
                itemEntry.Interactable = true;
            }
        }

        protected void TrimPanelSize(int maxSizeInEntries)
        {
            int entriesCount = Mathf.Min(itemEntries.Count, maxSizeInEntries);
            float sizeDelta = 2 * VerticalOffset + entriesCount * (ButtonHeight + VerticalOffset);

            var rectTransform = (RectTransform)transform;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, sizeDelta + 45);
        }

        public override void ClearSubscribers()
        {
            ItemClicked = null;
            CloseClicked = null;
        }

        public IEnumerator<IItemEntry> GetEnumerator()
        {
            return itemEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => itemEntries.Count;

        public IItemEntry this[int index] => itemEntries[index];

        sealed class ItemEntry : IItemEntry
        {
            readonly ItemButton button;
            readonly float buttonHeight;
            readonly float verticalOffset;
            readonly ResourceKey<GameObject> buttonType;
            int index;

            public ItemEntry(int index,
                GameObject parent, float buttonHeight, float verticalOffset,
                ResourceKey<GameObject> buttonType,
                Action<int, int> callback)
            {
                ThrowHelper.ThrowIfNull(parent, nameof(parent));
                ThrowHelper.ThrowIfNull(callback, nameof(callback));

                if (index < 0)
                {
                    ThrowHelper.ThrowArgumentOutOfRange(nameof(index));
                }

                this.buttonHeight = buttonHeight;
                this.verticalOffset = verticalOffset;
                this.buttonType = buttonType;

                button = ResourcePool.Rent<ItemButton>(buttonType, parent.transform, false);
                button.Height = buttonHeight;
                button.Clicked += subIndex => callback(Index, subIndex);
                button.Active = true;

                Index = index;
            }

            int Index
            {
                get => index;
                set
                {
                    index = value;
                    float y = verticalOffset + index * (verticalOffset + buttonHeight);
                    button.AnchoredPosition = new Vector2(0, -y);
                }
            }

            public string Text
            {
                set
                {
                    button.Caption = value ?? throw new NullReferenceException(nameof(value));
                    int lineBreaks = value.Count(x => x == '\n');
                    button.FontSize = lineBreaks switch
                    {
                        2 => 11,
                        3 => 10,
                        _ => 12
                    };
                }
            }

            public bool Interactable
            {
                set => button.Interactable = value;
            }

            public Color Color
            {
                set => button.Color = value;
            }

            public void Dispose()
            {
                button.Suspend();

                if (!Mathf.Approximately(buttonHeight, BaseButtonHeight))
                {
                    button.Height = BaseButtonHeight;
                }

                ResourcePool.Return(buttonType, button.gameObject);
            }
        }
    }

    public interface IItemEntry
    {
        bool Interactable { set; }
        Color Color { set; }
    }
}