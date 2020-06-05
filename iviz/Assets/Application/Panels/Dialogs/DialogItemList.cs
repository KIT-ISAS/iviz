using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;

namespace Iviz.App
{
    public class DialogItemList : MonoBehaviour, IDialogPanelContents
    {
        const float yOffset = 5;

        readonly List<ItemEntry> items = new List<ItemEntry>();

        public GameObject contentObject;
        public Text emptyText;
        public Text titleText;
        public TrashButtonWidget closeButton;

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

            public ItemEntry(int index, GameObject parent, Action<int, string> callback)
            {
                buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.TopicsButton, parent.transform, false);
                ButtonHeight = ((RectTransform)buttonObject.transform).rect.height;

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
                    ((RectTransform)buttonObject.transform).anchoredPosition = new Vector2(0, -y);
                }
            }

            public string Text
            {
                get => text.text;
                set => text.text = value;
            }

            public bool Interactable
            {
                get => button.interactable;
                set => button.interactable = value;
            }

            public void Invalidate()
            {
                button.onClick.RemoveAllListeners();
                ResourcePool.Dispose(Resource.Widgets.TopicsButton, buttonObject);
            }
        }

        public IEnumerable<string> Items
        {
            get => items.Select(x => x.Text);
            set
            {
                if (value.Count() == items.Count)
                {
                    int i = 0;
                    foreach (string str in value)
                    {
                        items[i++].Text = str;
                    }
                }
                else if (value.Count() < items.Count)
                {
                    int i = 0;
                    foreach (string str in value)
                    {
                        items[i++].Text = str;
                    }
                    for (int j = i; j < items.Count; j++)
                    {
                        items[j].Invalidate();
                    }
                    items.RemoveRange(i, items.Count - i);
                    UpdateSize();
                }
                else
                {
                    int i = 0;
                    foreach (string str in value)
                    {
                        if (i >= items.Count)
                        {
                            items.Add(new ItemEntry(i, contentObject, RaiseClicked));
                        }
                        items[i++].Text = str;
                    }
                    UpdateSize();
                }
            }
        }

        public ItemEntry this[int i]
        {
            get => items[i];
        }

        public bool Empty => items.Count == 0;

        public int Count => items.Count;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        // Use this for initialization
        void Start()
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
            int i = items.Count;
            items.Add(new ItemEntry(i, contentObject, RaiseClicked));
            items[i].Text = str;
            return i;
        }

        public bool Remove(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (index >= items.Count)
            {
                return false;
            }

            items[index].Invalidate();
            items.RemoveAt(index);
            for (int i = index; i < items.Count; i++)
            {
                items[i].Index = i;
            }
            UpdateSize();
            return true;
        }

        void UpdateSize()
        {
            RectTransform rectTransform = ((RectTransform)contentObject.transform);
            rectTransform.sizeDelta =
                (items.Count == 0) ?
                new Vector2(0, 2 * yOffset) :
                new Vector2(0, 2 * yOffset + items.Count * (items[0].ButtonHeight + yOffset));

            emptyText.gameObject.SetActive(items.Count == 0);
        }

        public void ClearSubscribers()
        {
            ItemClicked = null;
            CloseClicked = null;
        }
    }
}