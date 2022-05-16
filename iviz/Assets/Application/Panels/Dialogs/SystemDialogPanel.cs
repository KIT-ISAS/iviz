using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Tools;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SystemDialogPanel : DetachableDialogPanel
    {
        [SerializeField] Button topics;
        [SerializeField] Button services;
        [SerializeField] Button @params;
        [SerializeField] Button nodes;
        [SerializeField] Button aliases;

        [SerializeField] [CanBeNull] TMP_Text topicsText;
        [SerializeField] [CanBeNull] TMP_Text servicesText;
        [SerializeField] [CanBeNull] TMP_Text paramsText;
        [SerializeField] [CanBeNull] TMP_Text nodesText;
        [SerializeField] [CanBeNull] TMP_Text aliasesText;

        [SerializeField] TMP_Text textTop;
        [SerializeField] TMP_Text textBottom;

        [SerializeField] TrashButtonWidget close;
        [SerializeField] LinkResolver link;

        [SerializeField] [NotNull] GameObject[] aliasesFields = Array.Empty<GameObject>();

        [NotNull] InputFieldWithHintsWidget[] hostnames = Array.Empty<InputFieldWithHintsWidget>();
        [NotNull] InputFieldWidget[] addresses = Array.Empty<InputFieldWidget>();

        [SerializeField] GameObject aliasesTab;
        [SerializeField] GameObject infoTab;

        [NotNull] public TrashButtonWidget Close => close.AssertNotNull(nameof(close));
        [NotNull] public TMP_Text TextTop => textTop.AssertNotNull(nameof(textTop));
        [NotNull] public TMP_Text TextBottom => textBottom.AssertNotNull(nameof(textBottom));

        [NotNull] public IReadOnlyList<InputFieldWithHintsWidget> HostNames => hostnames;
        [NotNull] public IReadOnlyList<InputFieldWidget> Addresses => addresses;

        public enum ModeType
        {
            Topics,
            Services,
            Params,
            Nodes,
            Aliases
        }

        ModeType mode;

        public ModeType Mode
        {
            get => mode;
            private set
            {
                TextForMode(mode).fontStyle = FontStyles.Normal;
                mode = value;
                TextForMode(mode).fontStyle = FontStyles.Bold;
                ModeChanged?.Invoke(mode);

                aliasesTab.SetActive(mode == ModeType.Aliases);
                infoTab.SetActive(mode != ModeType.Aliases);
            }
        }

        [NotNull]
        TMP_Text TextForMode(ModeType m)
        {
            return m switch
            {
                ModeType.Aliases => aliasesText.AssertNotNull(nameof(aliasesText)),
                ModeType.Nodes => nodesText.AssertNotNull(nameof(nodesText)),
                ModeType.Params => paramsText.AssertNotNull(nameof(paramsText)),
                ModeType.Services => servicesText.AssertNotNull(nameof(servicesText)),
                ModeType.Topics => topicsText.AssertNotNull(nameof(topicsText)),
                _ => throw new IndexOutOfRangeException()
            };
        }

        [CanBeNull] public event Action<ModeType> ModeChanged;
        [CanBeNull] public event Action<string> LinkClicked;
        [CanBeNull] public event Action<int, string> HostnameEndEdit;
        [CanBeNull] public event Action<int, string> AddressEndEdit;

        void Awake()
        {
            Mode = ModeType.Topics;
            link.LinkClicked += l => LinkClicked?.Invoke(l);
            topics.onClick.AddListener(() => Mode = ModeType.Topics);
            services.onClick.AddListener(() => Mode = ModeType.Services);
            @params.onClick.AddListener(() => Mode = ModeType.Params);
            nodes.onClick.AddListener(() => Mode = ModeType.Nodes);
            aliases.onClick.AddListener(() => Mode = ModeType.Aliases);

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
                hostname.EndEdit += str => HostnameEndEdit?.Invoke(i, str);
            }

            foreach (var (address, i) in addresses.WithIndex())
            {
                address.Placeholder = "Enter address...";
                address.EndEdit += str => AddressEndEdit?.Invoke(i, str);
            }
        }

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