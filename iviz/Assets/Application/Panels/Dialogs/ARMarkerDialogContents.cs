using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ARMarkerDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] GameObject[] rows = null;
        [SerializeField] TrashButtonWidget close = null;
        bool initialized;

        public TrashButtonWidget Close => close;
        public IReadOnlyList<DropdownWidget> Types { get; private set; }
        public IReadOnlyList<InputFieldWidget> Codes { get; private set; }
        public IReadOnlyList<InputFieldWidget> Sizes { get; private set; }
        public IReadOnlyList<DropdownWidget> Actions { get; private set; }

        public event Action<int, ARMarkerType> TypesChanged;
        public event Action<int, ARMarkerAction> ActionsChanged;
        public event Action<int, string> CodesEndEdit;
        public event Action<int, float> SizesEndEdit;

        void Awake()
        {
            Initialize();
        }
        
        void Initialize()
        {
            if (initialized)
            {
                return;
            }

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

            Types = types.AsReadOnly();
            Codes = codes.AsReadOnly();
            Sizes = sizes.AsReadOnly();
            Actions = actions.AsReadOnly();

            string[] typesStr = Enum.GetNames(typeof(ARMarkerType));

            foreach (var (widget, row) in types.WithIndex())
            {
                widget.ValueChanged += (index, _) => OnTypeChanged(row, (ARMarkerType) index);
                widget.Options = typesStr;
                widget.Index = (int) ARMarkerType.Unset;
            }

            foreach (var (widget, row) in codes.WithIndex())
            {
                widget.EndEdit += value => CodesEndEdit?.Invoke(row, value);
            }

            foreach (var (widget, row) in sizes.WithIndex())
            {
                widget.EndEdit += value => SizesEndEdit?.Invoke(row,
                    float.TryParse(value, NumberStyles.Float, BuiltIns.Culture, out float i) ? i : -1);
                widget.Placeholder = "0";
                widget.SetContentType(InputField.ContentType.DecimalNumber);
            }

            string[] actionsStr = Enum.GetNames(typeof(ARMarkerAction));

            foreach (var (widget, row) in actions.WithIndex())
            {
                widget.ValueChanged += (index, _) => ActionsChanged?.Invoke(row, (ARMarkerAction) index);
                widget.Options = actionsStr;
                widget.Index = 0;
            }

            initialized = true;
        }

        void OnTypeChanged(int id, ARMarkerType index)
        {
            TypesChanged?.Invoke(id, index);
            bool restEnabled = (index != ARMarkerType.Unset);
            Codes[id].Interactable = restEnabled;
            Sizes[id].Interactable = restEnabled;
            Actions[id].Interactable = restEnabled;
        }

        public void ResetAll()
        {
            Initialize();

            for (int id = 0; id < Codes.Count; id++)
            {
                Types[id].Index = (int) ARMarkerType.Unset;
                Codes[id].Interactable = false;
                Sizes[id].Interactable = false;
                Actions[id].Interactable = false;
            }
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
    }
}