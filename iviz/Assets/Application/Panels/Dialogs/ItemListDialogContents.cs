using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public class ItemListDialogContents : MonoBehaviour, IDialogPanelContents
    {
        const float yOffset = 5;

        readonly List<ItemEntry> itemEntries = new List<ItemEntry>();

        public ReadOnlyCollection<ItemEntry> ItemEntries { get; }

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
            public float ButtonHeight { get; }

            public ItemEntry(int index, [NotNull] GameObject parent, [NotNull] Action<int, string> callback)
            {
                if (parent == null)
                {
                    throw new ArgumentNullException(nameof(parent));
                }

                if (callback == null)
                {
                    throw new ArgumentNullException(nameof(callback));
                }

                buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.TopicsButton, parent.transform, false);
                ButtonHeight = ((RectTransform) buttonObject.transform).rect.height;

                text = buttonObject.GetComponentInChildren<Text>();
                button = buttonObject.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => callback(Index, Text));

                Index = index;

                buttonObject.SetActive(true);
            }

            int index;

            public int Index
            {
                get => index;
                set
                {
                    if (index < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }

                    index = value;
                    float y = yOffset + index * (yOffset + ButtonHeight);
                    ((RectTransform) buttonObject.transform).anchoredPosition = new Vector2(0, -y);
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
                            itemEntries.Add(new ItemEntry(i, contentObject, RaiseClicked));
                        }

                        itemEntries[i++].Text = str;
                    }

                    UpdateSize();
                    canvas.enabled = true;
                }
            }
        }

        [NotNull] public ItemEntry this[int i] => itemEntries[i];

        public bool Empty => itemEntries.Count == 0;

        public int Count => itemEntries.Count;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public ItemListDialogContents()
        {
            ItemEntries = itemEntries.AsReadOnly();
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

        public int Add(string str)
        {
            int i = itemEntries.Count;
            itemEntries.Add(new ItemEntry(i, contentObject, RaiseClicked));
            itemEntries[i].Text = str;
            return i;
        }

        public bool Remove(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index >= itemEntries.Count)
            {
                return false;
            }

            canvas.enabled = false;
            itemEntries[index].Invalidate();
            itemEntries.RemoveAt(index);
            for (int i = index; i < itemEntries.Count; i++)
            {
                itemEntries[i].Index = i;
            }

            UpdateSize();
            canvas.enabled = true;
            return true;
        }

        void UpdateSize()
        {
            RectTransform rectTransform = ((RectTransform) contentObject.transform);
            rectTransform.sizeDelta =
                (itemEntries.Count == 0)
                    ? new Vector2(0, 2 * yOffset)
                    : new Vector2(0, 2 * yOffset + itemEntries.Count * (itemEntries[0].ButtonHeight + yOffset));

            emptyText.gameObject.SetActive(itemEntries.Count == 0);
            itemEntries.ForEach(x => x.Interactable = true);
        }

        public virtual void ClearSubscribers()
        {
            ItemClicked = null;
            CloseClicked = null;
        }
    }
}