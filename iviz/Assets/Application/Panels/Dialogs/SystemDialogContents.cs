using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.XmlRpc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SystemDialogContents : DetachablePanelContents
    {
        [SerializeField] Button topics = null;
        [SerializeField] Button services = null;
        [SerializeField] Button @params = null;
        [SerializeField] Button nodes = null;
        [SerializeField] Button aliases = null;

        [SerializeField] Text topicsText = null;
        [SerializeField] Text servicesText = null;
        [SerializeField] Text paramsText = null;
        [SerializeField] Text nodesText = null;
        [SerializeField] Text aliasesText = null;

        [SerializeField] TMP_Text textTop = null;
        [SerializeField] TMP_Text textBottom = null;

        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] LinkResolver link = null;

        [SerializeField] GameObject[] aliasesFields = null;

        InputFieldWithHintsWidget[] hostnames;
        InputFieldWidget[] addresses;

        [SerializeField] GameObject aliasesTab = null;
        [SerializeField] GameObject infoTab = null;

        public TrashButtonWidget Close => close;
        public TMP_Text TextTop => textTop;
        public TMP_Text TextBottom => textBottom;

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

        ModeType mode;

        public ModeType Mode
        {
            get => mode;
            private set
            {
                TextForMode(mode).fontStyle = FontStyle.Normal;
                mode = value;
                TextForMode(mode).fontStyle = FontStyle.Bold;
                ModeChanged?.Invoke(mode);

                aliasesTab.SetActive(mode == ModeType.Aliases);
                infoTab.SetActive(mode != ModeType.Aliases);
            }
        }

        Text TextForMode(ModeType m)
        {
            switch (m)
            {
                case ModeType.Aliases: return aliasesText;
                case ModeType.Nodes: return nodesText;
                case ModeType.Params: return paramsText;
                case ModeType.Services: return servicesText;
                case ModeType.Topics: return topicsText;
                default: throw new ArgumentException();
            }
        }

        public event Action<ModeType> ModeChanged;
        public event Action<string> LinkClicked;

        public event Action<int, string> HostnameEndEdit;
        public event Action<int, string> AddressEndEdit;

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

            for (int i = 0; i < hostnames.Length; i++)
            {
                int j = i;
                var hostname = hostnames[i];
                hostname.EndEdit += str => HostnameEndEdit?.Invoke(j, str);
            }

            for (int i = 0; i < addresses.Length; i++)
            {
                int j = i;
                var address = addresses[i];
                address.EndEdit += str => AddressEndEdit?.Invoke(j, str);
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