using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Iviz.Msgs;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public enum GuiARMarkerType
    {
        None,
        Qr,
        Aruco,
    }

    public enum GuiARMarkerAction
    {
        None,
    }
    
    public sealed class ARMarkerDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] GameObject[] rows = null;
        [SerializeField] TrashButtonWidget close = null;

        public TrashButtonWidget Close => close;
        public IReadOnlyList<DropdownWidget> Types { get; private set; }
        public IReadOnlyList<InputFieldWidget> Codes { get; private set; }
        public IReadOnlyList<InputFieldWidget> Sizes { get; private set; }
        public IReadOnlyList<DropdownWidget> Actions { get; private set; }

        public event Action<int, GuiARMarkerType> TypesChanged;
        public event Action<int, GuiARMarkerAction> ActionsChanged;
        public event Action<int, string> CodesEndEdit;
        public event Action<int, float> SizesEndEdit;

        void Awake()
        {
            var types = new List<DropdownWidget>();
            var codes = new List<InputFieldWidget>();
            var sizes = new List<InputFieldWidget>();
            var actions = new List<DropdownWidget>();
            foreach (var row in rows)
            {
                var mTransform = row.transform;
                types.Add(mTransform.GetChild(0).GetComponent<DropdownWidget>());
                codes.Add(mTransform.GetChild(1).GetComponent<InputFieldWidget>());
                sizes.Add(mTransform.GetChild(2).GetComponent<InputFieldWidget>());
                actions.Add(mTransform.GetChild(3).GetComponent<DropdownWidget>());
            }

            string[] typesStr =
            {
                "None", "QR", "Aruco"
            };

            foreach (var (widget, row) in WithIndex(types))
            {
                widget.ValueChanged += (index, _) => OnTypeChanged(row, (GuiARMarkerType) index);
                widget.Options = typesStr;
                widget.Index = (int) GuiARMarkerType.None;
            }

            foreach (var (widget, row) in WithIndex(codes))
            {
                widget.EndEdit += value => CodesEndEdit?.Invoke(row, value);
            }

            foreach (var (widget, row) in WithIndex(sizes))
            {
                widget.EndEdit += value => SizesEndEdit?.Invoke(row,
                    float.TryParse(value, NumberStyles.Float, BuiltIns.Culture, out float i) ? i : -1);
                widget.Placeholder = "100";
                widget.SetContentType(InputField.ContentType.DecimalNumber);
            }

            string[] actionsStr =
            {
                "None"
            };

            foreach (var (widget, row) in WithIndex(actions))
            {
                widget.ValueChanged += (index, _) => ActionsChanged?.Invoke(row, (GuiARMarkerAction) index);
                widget.Options = actionsStr;
                widget.Index = 0;
            }


            Types = types.AsReadOnly();
            Codes = codes.AsReadOnly();
            Sizes = sizes.AsReadOnly();
            Actions = actions.AsReadOnly();

            for (int row = 0; row < types.Count; row++)
            {
                OnTypeChanged(row, GuiARMarkerType.None);
            }
        }

        void OnTypeChanged(int id, GuiARMarkerType index)
        {
            TypesChanged?.Invoke(id, index);
            bool restEnabled = (index != GuiARMarkerType.None);
            Codes[id].Interactable = restEnabled;
            Sizes[id].Interactable = restEnabled;
            Actions[id].Interactable = restEnabled;
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
            CodesEndEdit = null;
            SizesEndEdit = null;
            TypesChanged = null;
            ActionsChanged = null;
        }

        [NotNull]
        static IEnumerable<(T item, int index)> WithIndex<T>([NotNull] IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }
    }
}