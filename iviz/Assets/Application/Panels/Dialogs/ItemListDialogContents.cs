using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public class ItemListDialogContents : MonoBehaviour, IDialogPanelContents,
        IReadOnlyList<ItemListDialogContents.ItemEntry>
    {
        static float baseButtonHeight;

        static float BaseButtonHeight => baseButtonHeight != 0
            ? baseButtonHeight
            : baseButtonHeight = ((RectTransform) Resource.Widgets.TopicsButton.Object.transform).rect.height;


        readonly List<ItemEntry> itemEntries = new List<ItemEntry>();
        protected float yOffset = 5;
        protected float buttonHeight;

        [SerializeField] GameObject contentObject = null;
        [SerializeField] Text emptyText = null;
        [SerializeField] Text titleText = null;
        [SerializeField] TrashButtonWidget closeButton = null;
        [SerializeField] Canvas canvas = null;

        public event Action<int, string> ItemClicked;
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

        public class ItemEntry
        {
            readonly GameObject buttonObject;
            readonly Text text;
            readonly Button button;

            readonly float buttonHeight;
            readonly float yOffset;
            int index;

            [NotNull] RectTransform ButtonTransform => (RectTransform) buttonObject.transform;

            public ItemEntry(int index, [NotNull] GameObject parent, float buttonHeight, float yOffset,
                [NotNull] Action<int, string> callback)
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
                buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.TopicsButton, parent.transform, false);

                RectTransform mTransform = ButtonTransform;
                Vector2 sizeDelta = mTransform.sizeDelta;
                mTransform.sizeDelta = new Vector2(sizeDelta.x, buttonHeight);

                text = buttonObject.GetComponentInChildren<Text>();
                button = buttonObject.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => callback(Index, Text));

                Index = index;
                buttonObject.SetActive(true);
            }

            int Index
            {
                get => index;
                set
                {
                    index = value;
                    float y = yOffset + index * (yOffset + buttonHeight);
                    ButtonTransform.anchoredPosition = new Vector2(0, -y);
                }
            }

            public string Text
            {
                get => text.text;
                set
                {
                    text.text = value;
                    int lineBreaks = value.Count(x => x == '\n');
                    switch (lineBreaks)
                    {
                        case 2:
                            text.fontSize = 11;
                            break;
                        case 3:
                            text.fontSize = 10;
                            break;
                        default:
                            text.fontSize = 12;
                            break;
                    }
                }
            }

            public bool Interactable
            {
                get => button.interactable;
                set => button.interactable = value;
            }

            public void Invalidate()
            {
                button.onClick.RemoveAllListeners();
                button.interactable = true;

                if (!Mathf.Approximately(buttonHeight, BaseButtonHeight))
                {
                    RectTransform mTransform = ButtonTransform;
                    Vector2 sizeDelta = mTransform.sizeDelta;
                    mTransform.sizeDelta = new Vector2(sizeDelta.x, BaseButtonHeight);
                }

                ResourcePool.Dispose(Resource.Widgets.TopicsButton, buttonObject);
            }
        }

        [NotNull]
        public IEnumerable<string> Items
        {
            get => itemEntries.Select(x => x.Text);
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (buttonHeight == 0)
                {
                    buttonHeight = BaseButtonHeight;
                }

                if (value.Count() == itemEntries.Count)
                {
                    int i = 0;
                    foreach (string str in value)
                    {
                        itemEntries[i++].Text = str;
                    }
                }
                else if (value.Count() < itemEntries.Count)
                {
                    canvas.enabled = false;
                    int i = 0;
                    foreach (string str in value)
                    {
                        itemEntries[i++].Text = str;
                    }

                    for (int j = i; j < itemEntries.Count; j++)
                    {
                        itemEntries[j].Invalidate();
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
                            itemEntries.Add(new ItemEntry(i, contentObject, buttonHeight, yOffset, RaiseClicked));
                        }

                        itemEntries[i++].Text = str;
                    }

                    UpdateSize();
                    canvas.enabled = true;
                }
            }
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        // Use this for initialization
        protected virtual void Start()
        {
            closeButton.Clicked += RaiseClose;
        }

        void RaiseClicked(int id, string text)
        {
            ItemClicked?.Invoke(id, text);
        }

        void RaiseClose()
        {
            CloseClicked?.Invoke();
        }

        void UpdateSize()
        {
            RectTransform rectTransform = ((RectTransform) contentObject.transform);
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

            var t = (RectTransform) transform;
            t.sizeDelta = new Vector2(t.sizeDelta.x, sizeDelta + 45);
        }

        public virtual void ClearSubscribers()
        {
            ItemClicked = null;
            CloseClicked = null;
        }

        public List<ItemEntry>.Enumerator GetEnumerator()
        {
            return itemEntries.GetEnumerator();
        }

        IEnumerator<ItemEntry> IEnumerable<ItemEntry>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => itemEntries.Count;

        public ItemEntry this[int index] => itemEntries[index];
    }
}