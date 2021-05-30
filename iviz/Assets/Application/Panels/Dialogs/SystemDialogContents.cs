using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SystemDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] Button topics;
        [SerializeField] Button services;
        [SerializeField] Button @params;
        [SerializeField] Button nodes;
        [SerializeField] Button aliases;

        [SerializeField] Text topicsText;
        [SerializeField] Text servicesText;
        [SerializeField] Text paramsText;
        [SerializeField] Text nodesText;
        [SerializeField] Text aliasesText;

        [SerializeField] TMP_Text textTop;
        [SerializeField] TMP_Text textBottom;

        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] LinkResolver link = null;

        public TrashButtonWidget Close => close;
        public TMP_Text TextTop => textTop;
        public TMP_Text TextBottom => textBottom;
        
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

        void Awake()
        {
            Mode = ModeType.Topics;
            link.LinkClicked += l => LinkClicked?.Invoke(l);
            topics.onClick.AddListener(() => Mode = ModeType.Topics);
            services.onClick.AddListener(() => Mode = ModeType.Services);
            @params.onClick.AddListener(() => Mode = ModeType.Params);
            nodes.onClick.AddListener(() => Mode = ModeType.Nodes);
            aliases.onClick.AddListener(() => Mode = ModeType.Aliases);
        }
        
        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            ModeChanged = null;
            Close.ClearSubscribers();
            LinkClicked = null;
        }
    }
}
