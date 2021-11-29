using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;

namespace Iviz.App
{
    public class ItemListDialogContents : PanelContents, IReadOnlyList<IItemEntry>
    {
        static float baseButtonHeight;

        static float BaseButtonHeight => baseButtonHeight != 0
            ? baseButtonHeight
            : baseButtonHeight = ((RectTransform)Resource.Widgets.ItemButton.Object.transform).rect.height;


        readonly List<ItemEntry> itemEntries = new();
        protected float yOffset = 5;
        protected float buttonHeight;

        [CanBeNull] Info<GameObject> buttonType;

        [NotNull]
        public Info<GameObject> ButtonType
        {
            get => buttonType ??= Resource.Widgets.ItemButton;
            set => buttonType = value;
        }

        [SerializeField] GameObject contentObject = null;
        [SerializeField] Text emptyText = null;
        [SerializeField] TMP_Text titleText = null;
        [SerializeField] TrashButtonWidget closeButton = null;
        [SerializeField] Canvas canvas = null;

        public event Action<int, int> ItemClicked;
        public event Action CloseClicked;

        public string Title
        {
            get => titleText.text;
            set => titleText.text = value;
        }

        public string EmptyText
        {
            get => emptyText.text;
            set => emptyText.text = value;
        }

        sealed class ItemEntry : IItemEntry
        {
            readonly ItemButton button;
            readonly float buttonHeight;
            readonly float yOffset;
            readonly Info<GameObject> buttonType;
            int index;

            public ItemEntry(int index,
                [NotNull] GameObject parent, float buttonHeight, float yOffset,
                [NotNull] Info<GameObject> buttonType,
                [NotNull] Action<int, int> callback)
            {
                if (parent == null)
                {
                    throw new ArgumentNullException(nameof(parent));
                }

                if (callback == null)
                {
                    throw new ArgumentNullException(nameof(callback));
                }

                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                this.buttonHeight = buttonHeight;
                this.yOffset = yOffset;
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
                    float y = yOffset + index * (yOffset + buttonHeight);
                    button.AnchoredPosition = new Vector2(0, -y);
                }
            }

            public string Text
            {
                get => button.Caption;
                set
                {
                    button.Caption = value;
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
                get => button.Interactable;
                set => button.Interactable = value;
            }

            public Color Color
            {
                get => button.Color;
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

        /*
        [NotNull]
        public IEnumerable<string> Items
        {
            set
            {

        }
        */

        public void SetItems<T>([NotNull] T value) where T : IReadOnlyCollection<string>
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (buttonHeight == 0)
            {
                buttonHeight = BaseButtonHeight;
            }

            if (value.Count == itemEntries.Count)
            {
                foreach (var (str, i) in value.WithIndex())
                {
                    itemEntries[i].Text = str;
                }
            }
            else if (value.Count < itemEntries.Count)
            {
                canvas.enabled = false;

                int i = 0;
                foreach (string str in value)
                {
                    itemEntries[i++].Text = str;
                }

                for (int j = i; j < itemEntries.Count; j++)
                {
                    itemEntries[j].Dispose();
                }

                itemEntries.RemoveRange(i, itemEntries.Count - i);
                UpdateSize();
                canvas.enabled = true;
            }
            else
            {
                canvas.enabled = false;
                int i = 0;
                foreach (string str in value)
                {
                    if (i >= itemEntries.Count)
                    {
                        itemEntries.Add(
                            new ItemEntry(i, contentObject, buttonHeight, yOffset, ButtonType, RaiseClicked));
                    }

                    itemEntries[i++].Text = str;
                }

                UpdateSize();
                canvas.enabled = true;
            }
        }

        protected virtual void Start()
        {
            closeButton.Clicked += RaiseClose;
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
            RectTransform rectTransform = ((RectTransform)contentObject.transform);
            rectTransform.sizeDelta = new Vector2(0, 2 * yOffset + itemEntries.Count * (buttonHeight + yOffset));

            emptyText.gameObject.SetActive(itemEntries.Count == 0);
            foreach (var x in itemEntries)
            {
                x.Interactable = true;
            }
        }

        protected void TrimPanelSize(int maxSizeInEntries)
        {
            int entriesCount = Math.Min(itemEntries.Count, maxSizeInEntries);
            float sizeDelta = 2 * yOffset + entriesCount * (buttonHeight + yOffset);

            var t = (RectTransform)transform;
            t.sizeDelta = new Vector2(t.sizeDelta.x, sizeDelta + 45);
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
    }

    public interface IItemEntry
    {
        bool Interactable { set; }
        Color Color { set; }
    }
}