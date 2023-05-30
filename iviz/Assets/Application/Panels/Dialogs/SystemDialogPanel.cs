#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SystemDialogPanel : DetachableDialogPanel
    {
        [SerializeField] Button? topics;
        [SerializeField] Button? services;
        [SerializeField] Button? @params;
        [SerializeField] Button? nodes;
        [SerializeField] Button? aliases;

        [SerializeField] TMP_Text? topicsText;
        [SerializeField] TMP_Text? servicesText;
        [SerializeField] TMP_Text? paramsText;
        [SerializeField] TMP_Text? nodesText;
        [SerializeField] TMP_Text? aliasesText;

        [SerializeField] TMP_Text? textTop;
        [SerializeField] TMP_Text? textBottom;

        [SerializeField] SimpleButtonWidget? close;
        [SerializeField] LinkResolver? link;

        [SerializeField] GameObject[] aliasesFields = Array.Empty<GameObject>();

        [SerializeField] GameObject? aliasesTab;
        [SerializeField] GameObject? infoTab;

        InputFieldWithHintsWidget[] hostnames = Array.Empty<InputFieldWithHintsWidget>();
        InputFieldWidget[] addresses = Array.Empty<InputFieldWidget>();
        ModeType mode;

        GameObject AliasesTab => aliasesTab.AssertNotNull(nameof(aliasesTab));
        GameObject InfoTab => infoTab.AssertNotNull(nameof(infoTab));
        LinkResolver Link => link.AssertNotNull(nameof(link));
        Button Topics => topics.AssertNotNull(nameof(topics));
        Button Services => services.AssertNotNull(nameof(services));
        Button Params => @params.AssertNotNull(nameof(@params));
        Button Nodes => nodes.AssertNotNull(nameof(nodes));
        Button Aliases => aliases.AssertNotNull(nameof(aliases));

        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public TMP_Text TextTop => textTop.AssertNotNull(nameof(textTop));
        public TMP_Text TextBottom => textBottom.AssertNotNull(nameof(textBottom));

        public IReadOnlyList<InputFieldWithHintsWidget> HostNames => hostnames;
        public IReadOnlyList<InputFieldWidget> Addresses => addresses;

        public enum ModeType
        {
            Topics,
            Services,
            Params,
            Nodes,
            Aliases
        }

        public ModeType Mode
        {
            get => mode;
            private set
            {
                TextForMode(mode).fontStyle = FontStyles.Normal;
                mode = value;
                TextForMode(mode).fontStyle = FontStyles.Bold;
                ModeChanged.TryRaise(mode, this);

                AliasesTab.SetActive(mode == ModeType.Aliases);
                InfoTab.SetActive(mode != ModeType.Aliases);
            }
        }

        public event Action<ModeType>? ModeChanged;
        public event Action<string>? LinkClicked;
        public event Action<int, string>? HostnameEndEdit;
        public event Action<int, string>? AddressEndEdit;

        void Awake()
        {
            Mode = ModeType.Topics;
            Link.LinkClicked += l => LinkClicked.TryRaise(l, this);
            Topics.onClick.AddListener(() => Mode = ModeType.Topics);
            Services.onClick.AddListener(() => Mode = ModeType.Services);
            Params.onClick.AddListener(() => Mode = ModeType.Params);
            Nodes.onClick.AddListener(() => Mode = ModeType.Nodes);
            Aliases.onClick.AddListener(() => Mode = ModeType.Aliases);

            hostnames = aliasesFields
                .Where(obj => obj != null)
                .Select(obj => obj.transform.GetChild(0).GetComponentInChildren<InputFieldWithHintsWidget>())
                .ToArray();
            addresses = aliasesFields
                .Where(obj => obj != null)
                .Select(obj => obj.transform.GetChild(1).GetComponentInChildren<InputFieldWidget>())
                .ToArray();

            foreach (var (hostname, i) in hostnames.WithIndex())
            {
                hostname.Placeholder = "Enter hostname...";
                hostname.Submit += str => HostnameEndEdit?.Invoke(i, str);
                hostname.EndEdit += str => HostnameEndEdit?.Invoke(i, str);
            }

            foreach (var (address, i) in addresses.WithIndex())
            {
                address.Placeholder = "Enter address...";
                address.Submit += str => AddressEndEdit?.Invoke(i, str);
                address.EndEdit += str => AddressEndEdit?.Invoke(i, str);
            }
        }

        TMP_Text TextForMode(ModeType modeType) => modeType switch
        {
            ModeType.Aliases => aliasesText.AssertNotNull(nameof(aliasesText)),
            ModeType.Nodes => nodesText.AssertNotNull(nameof(nodesText)),
            ModeType.Params => paramsText.AssertNotNull(nameof(paramsText)),
            ModeType.Services => servicesText.AssertNotNull(nameof(servicesText)),
            ModeType.Topics => topicsText.AssertNotNull(nameof(topicsText)),
            _ => throw new IndexOutOfRangeException()
        };

        public override void ClearSubscribers()
        {
            ModeChanged = null;
            Close.ClearSubscribers();
            LinkClicked = null;
            AddressEndEdit = null;
            HostnameEndEdit = null;
        }
    }
}